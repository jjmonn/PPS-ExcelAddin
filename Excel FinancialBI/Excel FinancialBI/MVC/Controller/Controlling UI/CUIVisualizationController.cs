using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace FBI.MVC.Controller
{
  using Forms;
  using Model;
  using Model.CRUD;
  using View;
  using Utils;

  class CUIVisualizationController : IController
  {
    CUIVisualization m_view;

    public string Error { get; set; }
    public IView View { get { return (m_view); } }

    CUI2VisualisationChartsSettings m_viewChartSettings;
    ChartPanelSelection m_viewPanelSelection;
    CUIController m_parentController;

    //Filled by views
    private Tuple<string, UInt32> m_lastPanel = null;
    private ChartSettings m_chartSettings = null;
    private SafeDictionary<UInt32, ChartAccount> m_chartAccounts = null;

    public CUIVisualizationController(CUIController p_parentController)
    {
      m_parentController = p_parentController;
      m_view = new CUIVisualization();
      m_view.SetController(this);
      m_viewChartSettings = new CUI2VisualisationChartsSettings();
      m_viewPanelSelection = new ChartPanelSelection();
      m_viewChartSettings.SetController(this);
      m_viewPanelSelection.SetController(this);
      this.LoadView();
    }

    void LoadView()
    {
      m_view.LoadView();
      m_viewPanelSelection.LoadView();
      if (m_viewPanelSelection.ShowDialog() != DialogResult.Cancel)
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

    public Tuple<string, UInt32> LastPanel
    {
      get { return (m_lastPanel); }
      set { m_lastPanel = value; }
    }

    public ChartSettings ChartSettings
    {
      get { return (m_chartSettings); }
      set
      {
        m_chartSettings = value;
        m_chartAccounts = ChartAccountModel.Instance.GetDictionary(m_chartSettings.Id);
      }
    }

    public bool CRUPanel(UInt32 p_panelId, string p_panelName)
    {
      ChartPanel l_panel;

      if (p_panelId == ChartPanel.INVALID_ID)
      {
        l_panel = new ChartPanel();
        l_panel.Name = p_panelName;
        l_panel.UserId = UserModel.Instance.GetCurrentUser().Id;
        return (ChartPanelModel.Instance.Create(l_panel));
      }
      if ((l_panel = ChartPanelModel.Instance.GetValue(p_panelId)) == null)
        return (false);
      l_panel.Name = p_panelName;
      return (ChartPanelModel.Instance.Update(l_panel));
    }

    /*
     * 
     * TODO
     * Create ChartSetting in Controller, then catch events in ViewChartSettings
     * When events triggered and ChartSetting is OK, AddAccounts in Controller !
     * 
     * ChartView: ChartAccountModel && ChartSettingsEvents !
     *
     */

    public bool EditSettings(string p_name, List<Tuple<string, Color>> p_accountList)
    {
      Account l_account;
      List<UInt32> l_accountList = new List<uint>();

      foreach (Tuple<string, Color> l_item in p_accountList)
      {
        if ((l_account = AccountModel.Instance.GetValue(l_item.Item1)) == null)
        {
          Error = Local.GetValue("CUI_Charts.error.invalid_account");
          return (false);
        }
        if (!this.CRUChartAccount(l_account.Id, l_item))
          return (false);
        l_accountList.Add(l_account.Id);
      }
      //Delete chartAccount if unused
      if (m_chartAccounts != null)
      {
        foreach (UInt32 l_accountId in m_chartAccounts.Keys)
        {
          if (!l_accountList.Contains(l_accountId))
            ChartAccountModel.Instance.Delete(l_accountId);
        }
      }
      if (!this.CRUChartSetting(p_name))
      {
        Error = Local.GetValue("CUI_Charts.error.cannot_update");
        return (false);
      }
      return (true);
    }

    private bool CRUChartAccount(UInt32 p_accountId, Tuple<string, Color> p_value)
    {
      bool l_update = true;
      ChartAccount l_chartAccount;

      if (m_chartAccounts == null ||
        (l_chartAccount = m_chartAccounts[p_accountId]) == null)
      {
        l_chartAccount = new ChartAccount();
        l_update = false;
      }
      l_chartAccount.AccountId = p_accountId;
      l_chartAccount.Color = p_value.Item2.ToArgb();
      l_chartAccount.ChartId = m_chartSettings.Id;
      return (l_update ? ChartAccountModel.Instance.Update(l_chartAccount) :
        ChartAccountModel.Instance.Create(l_chartAccount));
    }

    private bool CRUChartSetting(string p_name)
    {
      if (m_chartSettings == null)
      {
        this.ChartSettings = new ChartSettings();
        m_chartSettings.PanelId = this.LastPanel.Item2;
        m_chartSettings.Name = p_name;
        return (ChartSettingsModel.Instance.Create(m_chartSettings));
      }
      m_chartSettings.Name = p_name;
      return (ChartSettingsModel.Instance.Update(m_chartSettings));
    }

    public bool IsLastConfigAmbigious()
    {
      var l_chart = ChartAccountModel.Instance.GetDictionary(m_chartSettings.Id);

      if (l_chart == null)
        return (true);
      return (this.LastConfig.Request.SortList.Count >= 1 &&
        this.LastConfig.Request.Versions.Count > 1 &&
        l_chart.Count > 1); //Number of series
    }

    public bool ApplyLastCompute(bool p_bypass = false)
    {
      if (this.LastConfig == null || this.LastResult == null)
        return (false);

      if (!this.IsLastConfigAmbigious() || p_bypass)
      {
        m_chartSettings.HasDeconstruction = this.LastConfig.Request.SortList.Count >= 1;
        m_chartSettings.Versions = this.LastConfig.Request.Versions;
        m_chartSettings.Deconstruction = (m_chartSettings.HasDeconstruction ? this.LastConfig.Request.SortList[0] : null);
      }
      return (true);
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
