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
  using MVC.Model;
  using MVC.Model.CRUD;

  class FbiChart : Chart
  {
    private static UInt32 m_chartId = 0;

    private UInt32 m_id;
    private ChartArea m_chartArea;
    private ChartSettings m_settings = null;

    public FbiChart() : base()
    {
      m_chartArea = new ChartArea();

      m_id = m_chartId++;
      this.ChartAreas.Add(m_chartArea);
      m_chartArea.AxisX.MajorGrid.Enabled = false;
      m_chartArea.AxisY.MajorGrid.Enabled = false;
      m_chartArea.IsSameFontSizeForAllAxes = true;

      m_chartArea.AxisY.TitleFont = new Font("calibri", 8);
      m_chartArea.AxisX.TitleFont = new Font("calibri", 8);
      m_chartArea.AxisX.LabelStyle.Angle = -45;
      m_chartArea.AxisY.LabelAutoFitMaxFontSize = 10;
      m_chartArea.AxisX.LabelAutoFitMaxFontSize = 10;
    }

    public UInt32 Id
    {
      get { return(m_id); }
    }

    public ChartSettings Settings
    {
      get { return (m_settings); }
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

      foreach (Serie l_serie in p_settings.Series)
      {
        foreach (int l_period in l_periods)
        {
          l_key = this.GetKey(l_serie.Account, p_config, l_period, l_version);

          double l_value = p_result[l_version].Values[l_key];
          Debug.WriteLine(">> " + l_value);
        }
      }
      m_settings = p_settings;
    }

    public void AddSerie(IEnumerable p_dataX, IEnumerable p_dataY)
    {
      Series l_series = new Series();

      l_series.ChartArea = this.ChartAreas.First().Name;
      l_series.Points.DataBindXY(p_dataX, p_dataY);
      this.Series.Add(l_series);
    }
  }
}
