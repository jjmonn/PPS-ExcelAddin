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

  class FbiChart : Chart
  {
    private static UInt32 m_chartId = 0;

    private UInt32 m_id;
    private ChartSettings m_settings = null;

    public FbiChart() : base()
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
      get { return(m_id); }
    }

    public ChartSettings Settings
    {
      get { return (m_settings); }
    }

    public void Clear()
    {
      if (this.ChartAreas != null)
        this.ChartAreas.Clear();
      if (this.Series != null)
        this.Series.Clear();
    }

    public bool HasSettings()
    {
      return (m_settings != null);
    }

    public bool NeedsDeconstruction(ComputeConfig p_config)
    {
      return (p_config.Request.Versions.Count > 1 || p_config.Request.SortList.Count > 0);
    }

    private ResultKey GetKey(Account p_account, ComputeConfig p_config, int p_period, UInt32 p_version)
    {
      if (p_config.Request.SortList.Count == 0)
      {
        return (new ResultKey(p_account.Id, "", "", p_config.BaseTimeConfig, p_period, p_version, true));
      }
      if (p_config.Request.SortList[0].Item2 == AxisType.Entities)
      {
        return (new ResultKey(p_account.Id, "", ResultKey.GetSortKey(p_config.Request.SortList[0].Item1,
          AxisType.Entities,
          p_config.Request.SortList[0].Item3), p_config.BaseTimeConfig, p_period, p_version, true));
      }
      return (new ResultKey(p_account.Id, ResultKey.GetSortKey(p_config.Request.SortList[0].Item1,
        p_config.Request.SortList[0].Item2,
        p_config.Request.SortList[0].Item3), "", p_config.BaseTimeConfig, p_period, p_version, true));
    }

    public void Assign(ChartSettings p_settings, ComputeConfig p_config, SafeDictionary<uint, ComputeResult> p_result)
    {
      ResultKey l_key;
      UInt32 l_version = p_config.Request.Versions[0];
      List<int> l_periods = PeriodModel.GetPeriodList(p_config.Request.StartPeriod, p_config.Request.NbPeriods, p_config.BaseTimeConfig);
      List<string> l_displayPeriods = PeriodUtils.ToList(l_periods, p_config.BaseTimeConfig);
      List<double> l_values = new List<double>();

      this.Clear();
      this.CreateArea();
      foreach (Serie l_serie in p_settings.Series)
      {
        foreach (int l_period in l_periods)
        {
          l_key = this.GetKey(l_serie.Account, p_config, l_period, l_version);
          l_values.Add(p_result[l_version].Values[l_key]);
        }
        this.AddSerie(l_displayPeriods, l_values, l_serie.Color);
        l_values.Clear();
      }
      m_settings = p_settings;
    }

    public void AddSerie(IList p_dataX, IList p_dataY, Color p_color, SeriesChartType p_chartType = SeriesChartType.Spline, int p_borderWidth = 4)
    {
      Series l_series = new Series();

      l_series.ChartArea = this.ChartAreas.First().Name;
      l_series.ChartType = p_chartType;
      l_series.BorderWidth = p_borderWidth;
      l_series.Color = p_color;
      l_series.Points.DataBindXY(p_dataX, p_dataY);
      this.ChartAreas[0].AxisX.IsMarginVisible = false; //Set X origin to the first point
      this.Series.Add(l_series);
    }
  }
}