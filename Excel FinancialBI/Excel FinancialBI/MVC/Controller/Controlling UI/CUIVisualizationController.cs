using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FBI.MVC.Controller
{
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

    public ComputeConfig LastConfig
    {
      get { return (m_parentController.LastConfig); }
    }

    public SafeDictionary<uint, ComputeResult> LastResult
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

    public void ShowSettingsView()
    {
      m_viewChartSettings.ShowDialog();
    }

    public void ShowSettingsView(UInt32 p_settingsId)
    {
      ChartSettings settings = ChartSettingsModel.Instance.GetValue(p_settingsId);

      m_viewChartSettings.LoadSettings(settings);
      this.ShowSettingsView();
    }
  }
}
