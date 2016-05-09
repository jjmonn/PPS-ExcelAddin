using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections;
using System.Diagnostics;

namespace FBI.Forms
{
  using Utils;
  using MVC.Model;
  using MVC.Model.CRUD;
  using ChartValue = Tuple<string, double>;
  using ChartValues = List<Tuple<string, double>>;
  using ChartSeries = SafeDictionary<int, List<Series>>;

  public class FbiChart : Chart
  {
    private const int ELEM_DISPLAYED = 4;
    private const int GRADIENT_MULTIPLIER = 2; //Used for stackColumn. Nb of stacks * GRADIENT_MULTIPLIER => Nb of steps of gradient
    private const int GRADIENT_DIVIDED = 225; //Used for lines. Nb of lines / GRADIENT_DIVIDED

    private const int TITLE_SIZE = 12;
    private const int LEGEND_SIZE = 10;
    private const int AXIS_SIZE = 8;
    private const string DEFAULT_FONT = "calibri";

    public class Computation
    {
      public ComputeConfig Config;
      public SafeDictionary<uint, ComputeResult> Result;

      public Computation(ComputeConfig p_config, SafeDictionary<uint, ComputeResult> p_result)
      {
        this.Config = p_config;
        this.Result = p_result;
      }
    }

    private static UInt32 m_chartId = 0;

    private UInt32 m_id;
    private ChartSettings m_settings = null;

    public FbiChart()
      : base()
    {
      m_id = m_chartId++;
    }

    public UInt32 Id
    {
      get { return (m_id); }
    }

    public ChartSettings Settings
    {
      get { return (m_settings); }
      set { m_settings = value; }
    }

    public void Clear()
    {
      this.ChartAreas.Clear();
      this.Series.Clear();
      this.Titles.Clear();
      this.Legends.Clear();
    }

    private ChartArea CreateArea()
    {
      ChartArea l_chartArea = new ChartArea();

      this.ChartAreas.Add(l_chartArea);
      l_chartArea.AxisX.MajorGrid.Enabled = false;
      l_chartArea.AxisY.MajorGrid.Enabled = false;
      l_chartArea.IsSameFontSizeForAllAxes = true;
      l_chartArea.AxisY.TitleFont = new Font(DEFAULT_FONT, AXIS_SIZE);
      l_chartArea.AxisX.TitleFont = new Font(DEFAULT_FONT, AXIS_SIZE);
      l_chartArea.AxisX.LabelStyle.Angle = -45;
      l_chartArea.AxisY.LabelAutoFitMaxFontSize = 10;
      l_chartArea.AxisX.LabelAutoFitMaxFontSize = 10;
      l_chartArea.AxisX.IsMarginVisible = false;
      l_chartArea.AxisY.IsMarginVisible = true;
      return (l_chartArea);
    }

    private void CreateLegend()
    {
      this.Legends.Add(new Legend());
      this.Legends[0].Docking = Docking.Bottom;
      this.Legends[0].IsDockedInsideChartArea = false;
      this.Legends[0].TableStyle = LegendTableStyle.Auto;
      this.Legends[0].Alignment = StringAlignment.Center;
      this.Legends[0].Font = new Font(DEFAULT_FONT, LEGEND_SIZE);
    }

    public void SetChart(string p_name)
    {
      this.Clear();
      this.Titles.Add(new Title(p_name, Docking.Bottom));
      this.Titles[0].Font = new Font(DEFAULT_FONT, TITLE_SIZE, FontStyle.Bold);
      this.BorderlineWidth = 1;
      this.BorderlineColor = Color.Gray;
      this.CreateArea();
      this.CreateLegend();
    }

    public bool HasSettings
    {
      get { return (m_settings != null); }
    }

    private SeriesChartType GetChartType(ChartSettings p_settings)
    {
      if (p_settings.HasDeconstruction)
        return (SeriesChartType.StackedColumn);
      return (SeriesChartType.Spline);
    }

    private ResultKey GetKey(ChartSettings p_settings, ComputeConfig p_config, Account p_account, int p_period, UInt32 p_version, UInt32 p_value = 0)
    {
      if (!p_settings.HasDeconstruction)
      {
        return (new ResultKey(p_account.Id, "", "", p_config.BaseTimeConfig, p_period, p_version, true));
      }
      if (p_settings.Deconstruction.Item1 && p_settings.Deconstruction.Item2 == AxisType.Entities)
      {
        return (new ResultKey(p_account.Id, "", ResultKey.GetSortKey(p_settings.Deconstruction.Item1,
          AxisType.Entities, p_value), p_config.BaseTimeConfig, p_period, p_version, true));
      }
      return (new ResultKey(p_account.Id, ResultKey.GetSortKey(p_settings.Deconstruction.Item1,
        p_settings.Deconstruction.Item2, p_value), "", p_config.BaseTimeConfig, p_period, p_version, true));
    }

    private ChartValues GetValuesVersion(ChartSettings p_settings, Computation p_compute,
      Account p_account, int p_period)
    {
      ResultKey l_key;
      Version l_version;
      ChartValues l_values = new ChartValues();

      foreach (UInt32 l_versionId in p_settings.Versions)
      {
        l_key = this.GetKey(p_settings, p_compute.Config, p_account, p_period, l_versionId);
        l_version = VersionModel.Instance.GetValue(l_versionId);
        l_values.Add(new Tuple<string, double>(l_version.Name, p_compute.Result[l_versionId].Values[l_key]));
      }
      return (l_values);
    }

    private ChartValues GetValuesDeconstruction(ChartSettings p_settings, Computation p_compute,
      Account p_account, int p_period, UInt32 p_version)
    {
      ResultKey l_key;
      ChartValues l_values = new ChartValues();

      if (!p_settings.Deconstruction.Item1) //Is filter
      {
        foreach (FilterValue l_filterValue in FilterValueModel.Instance.GetDictionary(p_settings.Deconstruction.Item3).Values)
        {
          l_key = this.GetKey(p_settings, p_compute.Config, p_account, p_period, p_version, l_filterValue.Id);
          l_values.Add(new Tuple<string, double>(l_filterValue.Name, p_compute.Result[p_version].Values[l_key]));
        }
      }
      else //Is AxisElem
      {
        List<AxisElem> l_axisList = new List<AxisElem>(); //GOTO ResultView AxisElemBuilder() to know what's going on, 'cause I have no idea...

        if (p_settings.Deconstruction.Item3 == 0 && p_settings.Deconstruction.Item2 == AxisType.Entities)
          l_axisList.Add(AxisElemModel.Instance.GetValue(p_compute.Config.Request.EntityId));
        else
          l_axisList = AxisElemModel.Instance.GetChildren(p_settings.Deconstruction.Item2, p_settings.Deconstruction.Item3);
        foreach (AxisElem l_axisElem in l_axisList)
        {
          l_key = this.GetKey(p_settings, p_compute.Config, p_account, p_period, p_version, l_axisElem.Id);
          l_values.Add(new Tuple<string, double>(l_axisElem.Name, p_compute.Result[p_version].Values[l_key]));
        }
      }
      return (l_values);
    }

    private ChartValues GetValues(ChartSettings p_settings, Computation p_compute,
      Account p_account, int p_period, UInt32 p_version)
    {
      if (!p_settings.HasDeconstruction && p_settings.Versions.Count == 1)
      {
        return (null);
      }
      if (!p_settings.HasDeconstruction && p_settings.Versions.Count > 1)
      {
        return (this.GetValuesVersion(p_settings, p_compute, p_account, p_period));
      }
      return (this.GetValuesDeconstruction(p_settings, p_compute, p_account, p_period, p_version));
    }

    private double GetValue(ChartSettings p_settings, Computation p_compute,
      Account p_account, int p_period, UInt32 p_version)
    {
      return (p_compute.Result[p_version].Values[this.GetKey(p_settings, p_compute.Config, p_account, p_period, p_version)]);
    }

    public bool IsAmbigious(ChartSettings p_settings, List<Serie> p_series)
    {
      return (p_settings.Versions == null || p_settings.HasDeconstruction && p_settings.Versions.Count > 1 && p_series.Count > 1);
    }

    public void Assign(ChartSettings p_settings, Computation p_compute)
    {
      Currency l_currency;
      List<Serie> l_series = Serie.FromChartSettings(p_settings);
      List<int> l_periods = PeriodModel.GetPeriodList(p_compute.Config.Request.StartPeriod,
        p_compute.Config.Request.NbPeriods,
        p_compute.Config.BaseTimeConfig);
      SafeDictionary<int, string> l_displayPeriods = PeriodUtils.ToList(l_periods, p_compute.Config.BaseTimeConfig);

      this.SetChart(p_settings.Name);

      if (this.IsAmbigious(p_settings, l_series))
      {
        this.Titles[0].Text = Local.GetValue("CUI_Charts.error.ambigious_settings");
      }
      else
      {
        if ((l_currency = CurrencyModel.Instance.GetValue(p_compute.Config.Request.CurrencyId)) != null)
        {
          this.ChartAreas[0].AxisY.LabelStyle.Format = "{### ### ### ### ### ###} " + l_currency.Symbol; //Bullshit to have spacing between numbers. There is a better way, no worries.
        }
        this.Display(p_settings, l_series, p_compute, l_periods, l_displayPeriods);
      }
      m_settings = p_settings;
    }

    public void Display(ChartSettings p_settings, List<Serie> p_series,
      Computation p_compute, List<int> p_periods, SafeDictionary<int, string> p_displayPeriods)
    {
      if (p_settings.HasDeconstruction && p_settings.Versions.Count > 1)
      {
        this.DisplayAsDeconstructionPerVersion(p_settings, p_series, p_compute, p_periods, p_displayPeriods);
      }
      else if (p_settings.HasDeconstruction)
      {
        this.DisplayAsDeconstruction(p_settings, p_series, p_compute, p_periods, p_displayPeriods);
      }
      else
      {
        this.DisplayAsVersion(p_settings, p_series, p_compute, p_periods, p_displayPeriods);
      }
    }

    private void DisplayAsVersion(ChartSettings p_settings, List<Serie> p_series,
      Computation p_compute, List<int> p_periods, SafeDictionary<int, string> p_displayPeriods)
    {
      double l_value;
      Color l_color = Color.FromArgb(0, 0, 0, 0);

      foreach (UInt32 l_version in p_settings.Versions)
      {
        foreach (Serie l_serie in p_series)
        {
          Series l_series = this.CreateSeries(this.GetChartType(p_settings), ColorUtils.Sub(l_serie.Color, l_color));
          foreach (int l_period in p_periods)
          {
            l_value = this.GetValue(p_settings, p_compute, l_serie.Account, l_period, l_version);
            l_series.Points.AddXY(p_displayPeriods[l_period], l_value);
          }
          l_series.LegendText = l_serie.Account.Name + "\n" + VersionModel.Instance.GetValue(l_version).Name;
          this.Series.Add(l_series);
        }
        l_color = ColorUtils.Add(l_color, GRADIENT_DIVIDED / p_settings.Versions.Count);
      }
    }

    private void DisplayAsDeconstruction(ChartSettings p_settings, List<Serie> p_series,
      Computation p_compute, List<int> p_periods, SafeDictionary<int, string> p_displayPeriods)
    {
      List<Color> l_colors;
      ChartValues l_values;
      string[] l_significantsKeys = this.GetSignificantKeys(p_settings, p_series, p_compute, p_periods);
      ChartSeries l_series = this.CreateMultipleSeries(p_settings, p_compute, p_series.Count, this.GetNumberOfDeconstruction(p_settings));

      for (int i = 0; i < p_series.Count; ++i)
      {
        Serie l_serie = p_series[i];
        l_colors = ColorUtils.Gradient(l_serie.Color, Color.Black, ELEM_DISPLAYED * GRADIENT_MULTIPLIER);

        foreach (int l_period in p_periods)
        {
          l_values = this.GetValues(p_settings, p_compute, l_serie.Account, l_period, p_settings.Versions[0]);
          l_values = this.GetSignificantValues(l_values, l_significantsKeys);
          for (int j = 0; j < l_values.Count; ++j)
          {
            l_series[i][j].Points.AddXY(p_displayPeriods[l_period], l_values[j].Item2);
            l_series[i][j].Color = l_colors[j];
            l_series[i][j].LegendText = l_serie.Account.Name + "\n" + l_values[j].Item1;
            l_series[i][j].ToolTip = l_values[j].Item1;
            l_series[i][j]["StackedGroupName"] = l_serie.Account.Name + i.ToString();
          }
        }
      }
      this.AddSeries(l_series);
    }

    private void DisplayAsDeconstructionPerVersion(ChartSettings p_settings, List<Serie> p_series,
      Computation p_compute, List<int> p_periods, SafeDictionary<int, string> p_displayPeriods)
    {
      List<Color> l_colors;
      ChartValues l_values;
      string[] l_significantsKeys = this.GetSignificantKeys(p_settings, p_series, p_compute, p_periods);
      ChartSeries l_series = this.CreateMultipleSeries(p_settings, p_compute, p_settings.Versions.Count, this.GetNumberOfDeconstruction(p_settings));

      for (int i = 0; i < p_settings.Versions.Count; ++i)
      {
        Serie l_serie = p_series[0];
        UInt32 l_version = p_settings.Versions[i];
        l_colors = ColorUtils.Gradient(ColorUtils.RandomBrightColor(255, Color.Gray), Color.Black, ELEM_DISPLAYED * GRADIENT_MULTIPLIER);

        foreach (int l_period in p_periods)
        {
          l_values = this.GetValues(p_settings, p_compute, l_serie.Account, l_period, l_version);
          l_values = this.GetSignificantValues(l_values, l_significantsKeys);
          for (int j = 0; j < l_values.Count; ++j)
          {
            string l_versionName = VersionModel.Instance.GetValue(l_version).Name;
            l_series[i][j].Points.AddXY(p_displayPeriods[l_period], l_values[j].Item2);
            l_series[i][j].Color = l_colors[j];
            l_series[i][j].LegendText = l_versionName + "\n" + l_values[j].Item1;
            l_series[i][j].ToolTip = l_versionName + " - " + l_values[j].Item1;
            l_series[i][j]["StackedGroupName"] = l_serie.Account.Name + i.ToString();
          }
        }
      }
      this.AddSeries(l_series);
    }

    private int GetNumberOfDeconstruction(ChartSettings p_settings)
    {
      int l_nbValues = 0;

      if (!p_settings.HasDeconstruction)
        return (ELEM_DISPLAYED);

      if (p_settings.Deconstruction.Item1)
      {
        l_nbValues = (p_settings.Deconstruction.Item3 == 0 && p_settings.Deconstruction.Item2 == AxisType.Entities ? 1 :
          AxisElemModel.Instance.GetChildren(p_settings.Deconstruction.Item2, p_settings.Deconstruction.Item3).Count);
      }
      else
      {
        l_nbValues = FilterValueModel.Instance.GetDictionary(p_settings.Deconstruction.Item3).Count;
      }
      return (l_nbValues > ELEM_DISPLAYED ? ELEM_DISPLAYED : l_nbValues);
    }

    #region SignificantsUtils

    private ChartValues GetSignificantValues(ChartValues p_values, string[] p_signValues)
    {
      double l_sum = 0;
      ChartValues l_values = new ChartValues();

      for (int i = 0; i < p_signValues.Length; ++i)
      {
        if (p_signValues[i] == null) //If we encountered a null, we don't process the 'Other'. Used when a deconstruction has less that 4 ELEM_DISPLAYED values.
          return (l_values);
        foreach (ChartValue l_val in p_values)
        {
          if (p_signValues[i] == l_val.Item1)
            l_values.Add(l_val);
        }
      }
      foreach (ChartValue l_val in p_values)
      {
        if (!p_signValues.Contains(l_val.Item1))
          l_sum += l_val.Item2;
      }
      l_values.Add(new ChartValue(Local.GetValue("CUI_Charts.other"), l_sum));
      return (l_values);
    }

    private string[] GetSignificantKeys(ChartSettings p_settings,
      List<Serie> p_series, Computation p_compute, List<int> p_periods)
    {
      int i = 0;
      ChartValues l_values;
      string[] l_list = new string[ELEM_DISPLAYED - 1];
      SafeDictionary<string, double> l_sum = new SafeDictionary<string, double>();

      foreach (int l_period in p_periods)
      {
        foreach (Serie l_serie in p_series)
        {
          l_values = this.GetValues(p_settings, p_compute, l_serie.Account, l_period, p_settings.Versions[0]);
          l_values.Sort((x, y) => y.Item2.CompareTo(x.Item2));
          foreach (ChartValue l_val in l_values)
            l_sum[l_val.Item1] += l_val.Item2;
        }
      }

      var l_sortedSum = from entry in l_sum orderby entry.Value descending select entry;
      foreach (KeyValuePair<string, double> l_item in l_sortedSum)
      {
        if (i >= l_list.Length)
          return (l_list);
        l_list[i] = l_item.Key;
        ++i;
      }
      for (; i < l_list.Length; ++i) //Fill the end with null values
        l_list[i] = null;
      return (l_list);
    }

    #endregion

    #region SeriesUtils

    private void AddSeries(ChartSeries p_series)
    {
      foreach (KeyValuePair<int, List<Series>> l_list in p_series)
      {
        foreach (Series l_series in l_list.Value)
        {
          this.Series.Add(l_series);
        }
      }
    }

    private ChartSeries CreateMultipleSeries(ChartSettings p_settings, Computation p_compute, int p_dim1, int p_dim2)
    {
      ChartSeries l_series = new ChartSeries();

      for (int i = 0; i < p_dim1; ++i)
      {
        l_series[i] = new List<Series>();
        for (int j = 0; j < p_dim2; ++j)
        {
          l_series[i].Add(this.CreateSeries(this.GetChartType(p_settings)));
        }
      }
      return (l_series);
    }

    private Series CreateSeries(SeriesChartType p_chartType, Color? p_color = null,
      ChartValueType p_chartX = ChartValueType.Double, ChartValueType p_chartY = ChartValueType.String)
    {
      Series l_series = new Series();

      l_series.ChartArea = this.ChartAreas.First().Name;
      l_series.ChartType = p_chartType;
      l_series.BorderWidth = Properties.Settings.Default.chartBorderWidth;
      if (p_color != null)
      {
        l_series.Color = p_color.Value;
      }
      l_series.XValueType = p_chartX;
      l_series.YValueType = p_chartY;
      return (l_series);
    }

    #endregion
  }
}