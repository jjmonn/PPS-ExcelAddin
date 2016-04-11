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
      if (this.ChartAreas != null) //Error throw nullException, 'cause WTF.
        this.ChartAreas.Clear();
      if (this.Series != null)
        this.Series.Clear();
      this.Titles.Clear();
    }

    public void SetChart(string p_name)
    {
      this.Clear();
      this.Titles.Add(p_name);
      this.CreateArea();
    }

    public bool HasSettings()
    {
      return (m_settings != null);
    }

    private bool HasDeconstruction(ComputeConfig p_config)
    {
      if (p_config.Request.Versions.Count > 1 ||
        p_config.Request.SortList.Count > 0)
        return (true);
      return (false);
    }

    private SeriesChartType GetChartType(ComputeConfig p_config)
    {
      if (p_config.Request.Versions.Count > 1 || p_config.Request.SortList.Count > 0)
        return (SeriesChartType.StackedColumn);
      return (SeriesChartType.Spline);
    }

    private UInt32 GetVersion(ComputeConfig p_compute, UInt32? p_version)
    {
      return (p_version.HasValue ? p_version.Value : p_compute.Request.Versions[0]);
    }

    private ResultKey GetKey(ComputeConfig p_config, Account p_account, int p_period, UInt32 p_version, UInt32 p_value = 0)
    {
      if (p_config.Request.SortList.Count == 0)
      {
        return (new ResultKey(p_account.Id, "", "", p_config.BaseTimeConfig, p_period, p_version, true));
      }
      if (p_config.Request.SortList[0].Item1 && p_config.Request.SortList[0].Item2 == AxisType.Entities)
      {
        return (new ResultKey(p_account.Id, "", ResultKey.GetSortKey(p_config.Request.SortList[0].Item1,
          AxisType.Entities, p_value), p_config.BaseTimeConfig, p_period, p_version, true));
      }
      return (new ResultKey(p_account.Id, ResultKey.GetSortKey(p_config.Request.SortList[0].Item1,
        p_config.Request.SortList[0].Item2, p_value), "", p_config.BaseTimeConfig, p_period, p_version, true));
    }

    private SafeDictionary<string, double> GetValuesVersion(SafeDictionary<string, double> p_dic, Computation p_compute, Account p_account, int p_period)
    {
      ResultKey l_key;
      Version l_version;

      foreach (UInt32 l_versionId in p_compute.Config.Request.Versions)
      {
        l_key = this.GetKey(p_compute.Config, p_account, p_period, l_versionId);
        l_version = VersionModel.Instance.GetValue(l_versionId);
        p_dic[l_version.Name] = p_compute.Result[l_versionId].Values[l_key];
      }
      return (p_dic);
    }

    private SafeDictionary<string, double> GetValuesDeconstruction(SafeDictionary<string, double> p_dic, Computation p_compute, Account p_account, int p_period, UInt32? p_version)
    {
      ResultKey l_key;
      UInt32 l_version = this.GetVersion(p_compute.Config, p_version);
      Tuple<bool, AxisType, UInt32> l_parent;

      l_parent = p_compute.Config.Request.SortList[0];
      if (!l_parent.Item1) //Is filter
      {
        foreach (FilterValue l_filterValue in FilterValueModel.Instance.GetDictionary(l_parent.Item3).Values)
        {
          l_key = this.GetKey(p_compute.Config, p_account, p_period, l_version, l_filterValue.Id);
          p_dic[l_filterValue.Name] = p_compute.Result[l_version].Values[l_key];
        }
      }
      else //Is AxisElem
      {
        List<AxisElem> l_axisList = new List<AxisElem>(); //GOTO ResultView AxisElemBuilder() to know what's going on, 'cause I have no idea...

        if (l_parent.Item3 == 0 && l_parent.Item2 == AxisType.Entities)
          l_axisList.Add(AxisElemModel.Instance.GetValue(p_compute.Config.Request.EntityId));
        else
          l_axisList = AxisElemModel.Instance.GetChildren(l_parent.Item2, l_parent.Item3);
        foreach (AxisElem l_axisElem in l_axisList)
        {
          l_key = this.GetKey(p_compute.Config, p_account, p_period, l_version, l_axisElem.Id);
          p_dic[l_axisElem.Name] = p_compute.Result[l_version].Values[l_key];
        }
      }
      return (p_dic);
    }

    private SafeDictionary<string, double> GetValues(Computation p_compute, Account p_account, int p_period, UInt32? p_version = null)
    {
      SafeDictionary<string, double> l_dic = new SafeDictionary<string, double>();

      if (!this.HasDeconstruction(p_compute.Config))
      {
        return (null);
      }
      if (!p_version.HasValue && p_compute.Config.Request.Versions.Count > 1) //If the version is not defined, and there is multiple versions
      {
        return (this.GetValuesVersion(l_dic, p_compute, p_account, p_period));
      }
      return (this.GetValuesDeconstruction(l_dic, p_compute, p_account, p_period, p_version));
    }

    private double GetValue(Computation p_compute, Account p_account, int p_period)
    {
      UInt32 l_version = this.GetVersion(p_compute.Config, null);
      return (p_compute.Result[l_version].Values[this.GetKey(p_compute.Config, p_account, p_period, l_version)]);
    }

    //TODO Display caption
    //TODO Resolve null bugS
    //TODO bool IsAmbigious() -> Display error asking to choose version or deconstruction !

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
      if (this.HasDeconstruction(p_compute.Config))
      {
        this.DisplayAsStack(p_settings, p_compute, p_periods, p_displayPeriods);
        return;
      }
      this.DisplayAsLine(p_settings, p_compute, p_periods, p_displayPeriods);
    }

    private void DisplayAsLine(ChartSettings p_settings, Computation p_compute,
      List<int> p_periods, SafeDictionary<int, string> p_displayPeriods)
    {
      int l_pIndex;
      double l_value;

      foreach (Serie l_serie in p_settings.Series)
      {
        Series l_series = this.CreateSeries(this.GetChartType(p_compute.Config), l_serie.Color);
        foreach (int l_period in p_periods)
        {
          l_value = this.GetValue(p_compute, l_serie.Account, l_period);
          l_pIndex = l_series.Points.AddXY(p_displayPeriods[l_period], l_value);
        }
        this.Series.Add(l_series);
      }
    }

    private void DisplayAsStack(ChartSettings p_settings, Computation p_compute,
      List<int> p_periods, SafeDictionary<int, string> p_displayPeriods)
    {
      int i, l_pIndex;
      SafeDictionary<string, double> l_values;
      List<Series> l_series = this.CreateMultipleSeries(p_settings, p_compute, ELEM_DISPLAYED);

      //
      Random randomGen = new Random();
      KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
      //

      foreach (int l_period in p_periods)
      {
        foreach (Serie l_serie in p_settings.Series)
        {
          i = 0;
          l_values = this.GetValues(p_compute, l_serie.Account, l_period);
          l_values = this.GetSignificantValues(l_values);
          foreach (KeyValuePair<string, double> l_val in l_values)
          {
            l_pIndex = l_series[i].Points.AddXY(p_displayPeriods[l_period], l_val.Value);
            l_series[i].Points[l_pIndex].Color = Color.FromKnownColor(names[randomGen.Next(names.Length)]); //TMP
            if (!this.Series.Contains(l_series[i]))
              this.Series.Add(l_series[i]);
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
      for (; i < 3 && i < l_sortedValues.Count; ++i)
      {
        l_dic[l_sortedValues[i].Key] = l_sortedValues[i].Value;
      }
      for (; i < l_sortedValues.Count; ++i)
      {
        l_dic["Other"] += l_sortedValues[i].Value;
      }
      return (l_dic);
    }

    private List<Series> CreateMultipleSeries(ChartSettings p_settings, Computation p_compute, int n)
    {
      List<Series> l_list = new List<Series>();

      for (int i = 0; i < n; ++i)
      {
        l_list.Add(this.CreateSeries(this.GetChartType(p_compute.Config)));
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