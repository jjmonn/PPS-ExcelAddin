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
    public const Int32 CREATE = 0;
    public const Int32 UPDATE = 1;
    public const Int32 DELETE = 2;

    CUIVisualization m_view;

    public string Error { get; set; }
    public IView View { get { return (m_view); } }

    CUI2VisualisationChartsSettings m_viewChartSettings;
    ChartPanelSelection m_viewPanelSelection;
    CUIController m_parentController;

    private UInt32 m_panel = 0;

    private UInt32[] m_chartSettings = { 0, 0, 0 };
    private UInt32[] m_chartAccounts = { 0, 0, 0 };

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
      {
        m_view.Show();
        m_view.LoadPanel(m_panel);
      }
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

    public UInt32 PanelId
    {
      get { return (m_panel); }
      set { m_panel = value; }
    }

    public UInt32[] ExpectedChartSettings
    {
      get { return (m_chartSettings); }
    }

    public UInt32[] ExpectedChartAccounts
    {
      get { return (m_chartAccounts); }
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

    public bool CRUDChartAccounts(ChartSettings p_settings, List<Tuple<string, Color>> p_accountList)
    {
      Account l_account;
      HashSet<UInt32> l_accountIds = new HashSet<UInt32>();
      var l_chartAccounts = ChartAccountModel.Instance.GetDictionary(p_settings.Id);

      ArrayUtils.Set<UInt32>(m_chartAccounts, 0);
      foreach (Tuple<string, Color> l_item in p_accountList)
      {
        if ((l_account = AccountModel.Instance.GetValue(l_item.Item1)) == null)
        {
          Error = Local.GetValue("CUI_Charts.error.invalid_account");
          return (false);
        }
        if (!this.CRUChartAccount(p_settings, l_chartAccounts, l_account.Id, l_item))
          return (false);
        l_accountIds.Add(l_account.Id);
      }
      this.DChartAccounts(l_accountIds, l_chartAccounts);
      return (true);
    }

    private bool CRUChartAccount(ChartSettings p_settings, SafeDictionary<UInt32, ChartAccount> p_chartAccounts,
      UInt32 p_accountId, Tuple<string, Color> p_value)
    {
      UInt32 l_chartAccId;
      ChartAccount l_chartAccount;
      bool l_create = false;

      if (this.HasAccount(p_chartAccounts, p_accountId, out l_chartAccId))
      {
        l_chartAccount = p_chartAccounts[l_chartAccId];
        m_chartAccounts[UPDATE]++;
      }
      else
      {
        l_create = true;
        l_chartAccount = new ChartAccount();
        m_chartAccounts[CREATE]++;
      }
      l_chartAccount.AccountId = p_accountId;
      l_chartAccount.Color = p_value.Item2.ToArgb();
      l_chartAccount.ChartId = p_settings.Id;
      return (l_create ? ChartAccountModel.Instance.Create(l_chartAccount) :
         ChartAccountModel.Instance.Update(l_chartAccount));
    }

    public bool CRUChartSettings(ChartSettings p_settings, string p_name)
    {
      ArrayUtils.Set<UInt32>(m_chartSettings, 0);
      if (p_settings == null)
      {
        p_settings = new ChartSettings();
        p_settings.PanelId = m_panel;
        p_settings.Name = p_name;
        m_chartSettings[CREATE]++;
        return (ChartSettingsModel.Instance.Create(p_settings));
      }
      p_settings.Name = p_name;
      m_chartSettings[UPDATE]++;
      return (ChartSettingsModel.Instance.Update(p_settings));
    }

    public bool DChartSettings(UInt32 p_settingsId)
    {
      return (ChartSettingsModel.Instance.Delete(p_settingsId));
    }

    private void DChartAccounts(HashSet<UInt32> p_accountIds, SafeDictionary<UInt32, ChartAccount> p_chartAccounts)
    {
      HashSet<UInt32> l_unused = new HashSet<uint>();

      if (p_chartAccounts == null)
        return;
      foreach (var l_account in p_chartAccounts)
      {
        if (!p_accountIds.Contains(l_account.Value.AccountId))
          l_unused.Add(l_account.Key);
      }
      foreach (var l_accountId in l_unused)
      {
        ChartAccountModel.Instance.Delete(l_accountId);
        m_chartAccounts[DELETE]++;
      }
    }

    private bool HasAccount(SafeDictionary<UInt32, ChartAccount> p_chartAccounts, UInt32 p_accountId, out UInt32 p_id)
    {
      p_id = 0;
      if (p_chartAccounts == null)
        return (false);
      foreach (var l_item in p_chartAccounts)
      {
        if (l_item.Value.AccountId == p_accountId)
        {
          p_id = l_item.Key;
          return (true);
        }
      }
      return (false);
    }

    public bool IsLastConfigAmbigious(ChartSettings p_settings)
    {
      var l_chart = ChartAccountModel.Instance.GetDictionary(p_settings.Id);

      if (l_chart == null)
        return (false);
      return (this.LastConfig.Request.SortList.Count >= 1 &&
        this.LastConfig.Request.Versions.Count > 1 &&
        l_chart.Count > 1);
    }

    public bool ApplyLastCompute(ChartSettings p_settings, bool p_bypass = false)
    {
      if (this.LastConfig == null || this.LastResult == null)
        return (false);

      if (!this.IsLastConfigAmbigious(p_settings) || p_bypass)
      {
        p_settings.HasDeconstruction = this.LastConfig.Request.SortList.Count >= 1;
        p_settings.Versions = this.LastConfig.Request.Versions;
        p_settings.Deconstruction = (p_settings.HasDeconstruction ? this.LastConfig.Request.SortList[0] : null);
      }
      return (true);
    }

    public void ShowSettingsView(ChartSettings p_settings)
    {
      m_viewChartSettings.Reload();
      m_viewChartSettings.LoadSettings(p_settings);
      m_viewChartSettings.ShowDialog();
    }
  }
}
