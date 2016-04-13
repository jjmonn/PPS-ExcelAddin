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

  public class FbiChart : Chart
  {
    private const int ELEM_DISPLAYED = 4;
    private const int GRADIENT_MULTIPLIER = 2;

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

    private ChartArea CreateArea()
    {
      ChartArea l_chartArea = new ChartArea();

      this.ChartAreas.Add(l_chartArea);
      l_chartArea.AxisX.MajorGrid.Enabled = false;
      l_chartArea.AxisY.MajorGrid.Enabled = false;
      l_chartArea.IsSameFontSizeForAllAxes = true;
      l_chartArea.AxisY.TitleFont = new Font("calibri", 8);
      l_chartArea.AxisX.TitleFont = new Font("calibri", 8);
      l_chartArea.AxisX.LabelStyle.Angle = -45;
      l_chartArea.AxisY.LabelAutoFitMaxFontSize = 10;
      l_chartArea.AxisX.LabelAutoFitMaxFontSize = 10;
      return (l_chartArea);
    }

    public UInt32 Id
    {
      get { return (m_id); }
    }

    public ChartSettings Settings
    {
      get { return (m_settings); }
    }

    public void Clear()
    {
      this.ChartAreas.Clear();
      this.Series.Clear();
      this.Titles.Clear();
      this.Legends.Clear();
    }

    public void SetChart(string p_name)
    {
      this.Clear();
/*      this.Legends.Add(p_name);
      this.Legends[p_name].IsDockedInsideChartArea = true;*/
      this.Titles.Add(new Title(p_name, Docking.Bottom, new Font("calibri", 15), Color.Black));
      this.CreateArea();
    }

    public bool HasSettings()
    {
      return (m_settings != null);
    }

    private SeriesChartType GetChartType(ChartSettings p_settings)
    {
      if (p_settings.HasDeconstruction)
        return (SeriesChartType.StackedColumn);
      return (SeriesChartType.Spline);
    }

    private ResultKey GetKey(ChartSettings p_settings, Account p_account, int p_period, UInt32 p_version, UInt32 p_value = 0)
    {
      if (!p_settings.HasDeconstruction)
      {
        return (new ResultKey(p_account.Id, "", "", p_settings.TimeConfig, p_period, p_version, true));
      }
      if (p_settings.Deconstruction.Item1 && p_settings.Deconstruction.Item2 == AxisType.Entities)
      {
        return (new ResultKey(p_account.Id, "", ResultKey.GetSortKey(p_settings.Deconstruction.Item1,
          AxisType.Entities, p_value), p_settings.TimeConfig, p_period, p_version, true));
      }
      return (new ResultKey(p_account.Id, ResultKey.GetSortKey(p_settings.Deconstruction.Item1,
        p_settings.Deconstruction.Item2, p_value), "", p_settings.TimeConfig, p_period, p_version, true));
    }

    private SafeDictionary<string, double> GetValuesVersion(ChartSettings p_settings, Computation p_compute,
      Account p_account, int p_period)
    {
      ResultKey l_key;
      Version l_version;
      SafeDictionary<string, double> l_dic = new SafeDictionary<string, double>();

      foreach (UInt32 l_versionId in p_settings.Versions)
      {
        l_key = this.GetKey(p_settings, p_account, p_period, l_versionId);
        l_version = VersionModel.Instance.GetValue(l_versionId);
        l_dic[l_version.Name] = p_compute.Result[l_versionId].Values[l_key];
      }
      return (l_dic);
    }

    private SafeDictionary<string, double> GetValuesDeconstruction(ChartSettings p_settings, Computation p_compute,
      Account p_account, int p_period, UInt32 p_version)
    {
      ResultKey l_key;
      SafeDictionary<string, double> l_dic = new SafeDictionary<string, double>();

      if (!p_settings.Deconstruction.Item1) //Is filter
      {
        foreach (FilterValue l_filterValue in FilterValueModel.Instance.GetDictionary(p_settings.Deconstruction.Item3).Values)
        {
          l_key = this.GetKey(p_settings, p_account, p_period, p_version, l_filterValue.Id);
          l_dic[l_filterValue.Name] = p_compute.Result[p_version].Values[l_key];
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
          l_key = this.GetKey(p_settings, p_account, p_period, p_version, l_axisElem.Id);
          l_dic[l_axisElem.Name] = p_compute.Result[p_version].Values[l_key];
        }
      }
      return (l_dic);
    }

    private SafeDictionary<string, double> GetValues(ChartSettings p_settings, Computation p_compute,
      Account p_account, int p_period, UInt32 p_version)
    {
      SafeDictionary<string, double> l_dic = new SafeDictionary<string, double>();

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

    private double GetValue(ChartSettings p_settings, SafeDictionary<UInt32, ComputeResult> p_result,
      Account p_account, int p_period, UInt32 p_version)
    {
      return (p_result[p_version].Values[this.GetKey(p_settings, p_account, p_period, p_version)]);
    }

    public void Assign(ChartSettings p_settings, Computation p_compute)
    {
      List<int> l_periods = PeriodModel.GetPeriodList(p_compute.Config.Request.StartPeriod,
        p_compute.Config.Request.NbPeriods,
        p_compute.Config.BaseTimeConfig);
      SafeDictionary<int, string> l_displayPeriods = PeriodUtils.ToList(l_periods, p_compute.Config.BaseTimeConfig);

      this.SetChart(p_settings.Name);
      this.Display(p_settings, p_compute, l_periods, l_displayPeriods);

      m_settings = p_settings;
    }

    public void Display(ChartSettings p_settings, Computation p_compute,
      List<int> p_periods, SafeDictionary<int, string> p_displayPeriods)
    {
      if (p_settings.HasDeconstruction && p_settings.Versions.Count > 1)
      {
        this.DisplayAsStackVersion(p_settings, p_compute, p_periods, p_displayPeriods);
      }
      else if (p_settings.HasDeconstruction)
      {
        this.DisplayAsStack(p_settings, p_compute, p_periods, p_displayPeriods);
      }
      else
      {
        this.DisplayAsLine(p_settings, p_compute, p_periods, p_displayPeriods);
      }
    }

    private void DisplayAsLine(ChartSettings p_settings, Computation p_compute,
      List<int> p_periods, SafeDictionary<int, string> p_displayPeriods)
    {
      double l_value;
      Color l_color = Color.FromArgb(0, 0, 0, 0);

      foreach (UInt32 l_version in p_settings.Versions)
      {
        foreach (Serie l_serie in p_settings.Series)
        {
          Series l_series = this.CreateSeries(this.GetChartType(p_settings), ColorGradient.Sub(l_serie.Color, l_color));
          l_series.LegendText = l_serie.Account.Name + "\n" + VersionModel.Instance.GetValue(l_version).Name;
          foreach (int l_period in p_periods)
          {
            l_value = this.GetValue(p_settings, p_compute.Result, l_serie.Account, l_period, l_version);
            l_series.Points.AddXY(p_displayPeriods[l_period], l_value);
          }
          this.Series.Add(l_series);
        }
        l_color = ColorGradient.Add(l_color, (p_settings.Versions.Count * 8) * GRADIENT_MULTIPLIER);
      }
    }

    private void DisplayAsStack(ChartSettings p_settings, Computation p_compute,
      List<int> p_periods, SafeDictionary<int, string> p_displayPeriods)
    {
      int i, l_pIndex;
      List<Color> l_colors;
      SafeDictionary<string, double> l_values;
      List<Series> l_series = this.CreateMultipleSeries(p_settings, p_compute, ELEM_DISPLAYED);

      this.AddSeries(l_series);
      foreach (int l_period in p_periods)
      {
        foreach (Serie l_serie in p_settings.Series)
        {
          i = 0;
          l_colors = ColorGradient.Create(l_serie.Color, Color.Black, ELEM_DISPLAYED * GRADIENT_MULTIPLIER);
          l_values = this.GetValues(p_settings, p_compute, l_serie.Account, l_period, p_settings.Versions[0]);
          l_values = this.GetSignificantValues(l_values);
          foreach (KeyValuePair<string, double> l_val in l_values)
          {
            l_pIndex = l_series[i].Points.AddXY(p_displayPeriods[l_period], l_val.Value);
            l_series[i].Points[l_pIndex].Color = l_colors[i];
            ++i;
          }
        }
      }
    }

    private void DisplayAsStackVersion(ChartSettings p_settings, Computation p_compute,
      List<int> p_periods, SafeDictionary<int, string> p_displayPeriods)
    {
      int i, l_pIndex;
      List<Color> l_colors;
      Serie l_serie = p_settings.Series[0];
      SafeDictionary<string, double> l_values;
      List<Series> l_series = this.CreateMultipleSeries(p_settings, p_compute, ELEM_DISPLAYED);

      this.AddSeries(l_series);
      foreach (int l_period in p_periods)
      {
        foreach (UInt32 l_version in p_settings.Versions)
        {
          i = 0;
          l_colors = ColorGradient.Create(l_serie.Color, Color.Black, ELEM_DISPLAYED * GRADIENT_MULTIPLIER);
          l_values = this.GetValues(p_settings, p_compute, l_serie.Account, l_period, l_version);
          l_values = this.GetSignificantValues(l_values);
          foreach (KeyValuePair<string, double> l_val in l_values)
          {
            l_pIndex = l_series[i].Points.AddXY(p_displayPeriods[l_period], l_val.Value);
            l_series[i].Points[l_pIndex].Color = l_colors[i];
            ++i;
          }
        }
      }
    }

    private SafeDictionary<string, double> GetSignificantValues(SafeDictionary<string, double> p_dic)
    {
      int i = 0;
      SafeDictionary<string, double> l_dic;
      
      var l_sortedValues = p_dic.ToList();
      l_sortedValues.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
      l_dic = new SafeDictionary<string, double>();
      for (; i < ELEM_DISPLAYED - 1 && i < l_sortedValues.Count; ++i)
      {
        l_dic[l_sortedValues[i].Key] = l_sortedValues[i].Value;
      }
      for (; i < l_sortedValues.Count; ++i)
      {
        l_dic[Local.GetValue("CUI_Charts.other")] += l_sortedValues[i].Value;
      }
      return (l_dic);
    }

    private void AddSeries(List<Series> p_series)
    {
      foreach (Series l_series in p_series)
      {
        this.Series.Add(l_series);
      }
    }

    private List<Series> CreateMultipleSeries(ChartSettings p_settings, Computation p_compute, int n)
    {
      List<Series> l_list = new List<Series>();

      for (int i = 0; i < n; ++i)
      {
        l_list.Add(this.CreateSeries(this.GetChartType(p_settings)));
      }
      return (l_list);
    }

    private Series CreateSeries(SeriesChartType p_chartType, Color? p_color = null, bool p_margin = false, int p_borderWidth = 4)
    {
      Series l_series = new Series();

      l_series.ChartArea = this.ChartAreas.First().Name;
      l_series.ChartType = p_chartType;
      l_series.BorderWidth = p_borderWidth;
      if (p_color != null)
      {
        l_series.Color = p_color.Value;
      }
      this.ChartAreas[0].AxisX.IsMarginVisible = p_margin;
      return (l_series);
    }
  }
}