using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FBI.MVC.Controller
{
  using Forms;
  using Model;
  using Model.CRUD;
  using View;
  using GraphSortedDic = SafeDictionary<UInt32, SafeDictionary<Int32, SafeDictionary<Tuple<bool, Model.CRUD.AxisType, UInt32>, double>>>;
  using GraphUnSortedDic = SafeDictionary<UInt32, SafeDictionary<Int32, List<double>>>;

  class CUIVisualizationController : IController
  {
    CUIVisualization m_view;

    public string Error { get; set; }
    public IView View { get { return (m_view); } }

    CUI2VisualisationChartsSettings m_viewChartSettings;
    CUIController m_parentController;

    public CUIVisualizationController(CUIController p_parentController)
    {
      m_parentController = p_parentController;
      m_view = new CUIVisualization();
      m_view.SetController(this);
      m_viewChartSettings = new CUI2VisualisationChartsSettings();
      m_viewChartSettings.SetController(this);
      this.LoadView();
    }

    void LoadView()
    {
      m_view.LoadView();
      m_view.Show();
    }

    public FbiChart.Computation LastComputation
    {
      get
      {
        return (new FbiChart.Computation(m_parentController.LastConfig,
          m_parentController.LastResult));
      }
    }

    public ComputeConfig LastConfig
    {
      get { return (m_parentController.LastConfig); }
    }

    public SafeDictionary<UInt32, ComputeResult> LastResult
    {
      get { return (m_parentController.LastResult); }
    }

    public bool CreateUpdateSettings(ChartSettings p_settings)
    {
      if (p_settings.Id == ChartSettingsModel.INVALID)
      {
        ChartSettingsModel.Instance.Create(p_settings);
        return (true);
      }
      ChartSettingsModel.Instance.Update(p_settings.Id, p_settings);
      return (true);
    }

    public bool IsLastConfigAmbigious(ChartSettings p_settings)
    {
      return (this.LastConfig.Request.SortList.Count >= 1 &&
        this.LastConfig.Request.Versions.Count > 1 &&
        p_settings.Series.Count > 1);
    }

    public void ApplyLastCompute(ChartSettings p_settings, bool p_bypass = false)
    {
      if (!this.IsLastConfigAmbigious(p_settings) || p_bypass)
      {
        p_settings.HasDeconstruction = this.LastConfig.Request.SortList.Count >= 1;
        p_settings.Versions = this.LastConfig.Request.Versions;
        p_settings.Deconstruction = (p_settings.HasDeconstruction ? this.LastConfig.Request.SortList[0] : null);
      }
    }

    public void ShowSettingsView()
    {
      m_viewChartSettings.ShowDialog();
    }

    public void ShowSettingsView(ChartSettings p_settings)
    {
      m_viewChartSettings.LoadSettings(p_settings);
      this.ShowSettingsView();
    }
  }
}
