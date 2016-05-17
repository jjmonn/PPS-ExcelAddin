using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIBlend.WinForms.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections;
using System.Diagnostics;

namespace FBI.MVC.View
{
  using Controller;
  using Utils;
  using Model;
  using Model.CRUD;
  using Forms;
  using Network;

  public partial class CUIVisualization : Form, IView
  {
    CUIVisualizationController m_controller;

    private FbiChart m_lastClickedChart;
    private List<FbiChart> m_charts = new List<FbiChart>();

    private List<Control> m_panels = new List<Control>();
    private System.Windows.Forms.Orientation m_lastOrientationPanel = System.Windows.Forms.Orientation.Vertical;

    private UInt32[] m_expectedChartSettings = { 0, 0, 0 };
    private UInt32[] m_expectedChartAccounts = { 0, 0, 0 };
    private ChartAccount m_lastChartAccountRcvd = null;
    private ChartSettings m_lastChartSettingsRcvd = null;

    public CUIVisualization()
    {
      this.InitializeComponent();
      this.MultilangueSetup();
      m_panels.Add(m_panel);
    }

    private void MultilangueSetup()
    {
      m_entityLabel.Text = Local.GetValue("CUI.entity");
      m_currencyLabel.Text = Local.GetValue("CUI.currency");
      m_versionLabel.Text = Local.GetValue("CUI.version");
      m_refreshButton.Text = Local.GetValue("CUI.refresh");
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as CUIVisualizationController;
    }

    public void LoadView()
    {
      this.SuscribeEvents();
    }

    public void LoadPanel(UInt32 p_panelId)
    {
      var l_chartSettings = ChartSettingsModel.Instance.GetDictionary(p_panelId);

      if (l_chartSettings == null)
        return;
      foreach (var l_settings in l_chartSettings.Values)
      {
        this.UpdateChart(null, l_settings);
      }
    }

    private void SuscribeEvents()
    {
      m_panel.ContextMenuStrip = m_panelRightClick;
      m_horizontalSplitBT.Click += OnSplitHorizontalClick;
      m_splitVerticalBT.Click += OnSplitVerticalClick;
      m_chartEdit.Click += OnChartClick;
      m_refreshButton.Click += OnRefreshClick;
      m_delete.Click += OnDeleteClick;

      ChartSettingsModel.Instance.CreationEvent += OnChartSettingsCreated;
      ChartSettingsModel.Instance.UpdateEvent += OnChartSettingsUpdated;
      ChartSettingsModel.Instance.DeleteEvent += OnChartSettingsDeleted;
      ChartSettingsModel.Instance.ReadEvent += OnChartSettingsRead;

      ChartAccountModel.Instance.CreationEvent += OnChartAccountCreated;
      ChartAccountModel.Instance.UpdateEvent += OnChartAccountUpdated;
      ChartAccountModel.Instance.DeleteEvent += OnChartAccountDeleted;
      ChartAccountModel.Instance.ReadEvent += OnChartAccountRead;
    }

    private void CloseView()
    {
      m_controller.ClosePanel();
      ChartSettingsModel.Instance.CreationEvent -= OnChartSettingsCreated;
      ChartSettingsModel.Instance.UpdateEvent -= OnChartSettingsUpdated;
      ChartSettingsModel.Instance.DeleteEvent -= OnChartSettingsDeleted;
      ChartSettingsModel.Instance.ReadEvent -= OnChartSettingsRead;

      ChartAccountModel.Instance.CreationEvent -= OnChartAccountCreated;
      ChartAccountModel.Instance.UpdateEvent -= OnChartAccountUpdated;
      ChartAccountModel.Instance.DeleteEvent -= OnChartAccountDeleted;
      ChartAccountModel.Instance.ReadEvent -= OnChartAccountRead;
    }

    #region Split

    void OnSplitHorizontalClick(object p_sender, EventArgs p_args)
    {
      ToolStripMenuItem l_item = p_sender as ToolStripMenuItem;
      ContextMenuStrip l_menu = l_item.Owner as ContextMenuStrip;

      if (l_menu != null)
        Split(l_menu.SourceControl, System.Windows.Forms.Orientation.Vertical);
    }

    void OnSplitVerticalClick(object p_sender, EventArgs p_args)
    {
      ToolStripMenuItem l_item = p_sender as ToolStripMenuItem;
      ContextMenuStrip l_menu = l_item.Owner as ContextMenuStrip;

      if (l_menu != null)
        Split(l_menu.SourceControl, System.Windows.Forms.Orientation.Horizontal);
    }

    void Split(Control p_control, System.Windows.Forms.Orientation p_orientation)
    {
      vSplitContainer l_newSplit = new vSplitContainer();
      l_newSplit.Panel1.ContextMenuStrip = m_panelRightClick;
      l_newSplit.Panel2.ContextMenuStrip = m_panelRightClick;
      l_newSplit.Orientation = p_orientation;
      foreach (Control l_control in p_control.Controls)
        l_newSplit.Panel1.Controls.Add(l_control);

      p_control.Controls.Clear();
      p_control.Controls.Add(l_newSplit);
      l_newSplit.Dock = DockStyle.Fill;

      m_lastOrientationPanel = p_orientation;
      m_panels.Add(l_newSplit.Panel1);
      m_panels.Add(l_newSplit.Panel2);
    }

    private void Merge(Control p_control)
    {
      if (p_control.Parent == null)
        return;

      p_control.Controls.Clear();
      if (p_control.GetType() == typeof(vSplitterPanel))
      {
        vSplitContainer l_split = (vSplitContainer)p_control.Parent;
        Control l_otherPanel = (l_split.Panel1 == p_control ? l_split.Panel2 : l_split.Panel1);
        Control l_container = l_split.Parent;

        l_split.Controls.Remove(p_control);
        foreach (Control l_control in l_otherPanel.Controls)
          l_container.Controls.Add(l_control);
        l_container.Controls.Remove(l_split);
      }
    }

    #endregion

    #region Events

    private void OnFormClosed(object sender, FormClosedEventArgs e)
    {
      CloseView();
    }

    private void OnRefreshClick(object sender, EventArgs e)
    {
      foreach (FbiChart l_chart in m_charts)
      {
        this.UpdateChart(l_chart, l_chart.Settings);
      }
    }

    private void OnChartClick(object sender, EventArgs e)
    {
      FbiChart l_chart;
      ToolStripMenuItem l_toolStrip = (ToolStripMenuItem)sender;
      Control l_clickedControl = ((ContextMenuStrip)(((ToolStripMenuItem)sender).Owner)).SourceControl;

      if ((l_chart = (FbiChart)this.GetObjectFromControl(l_clickedControl, typeof(FbiChart))) == null)
      {
        l_chart = this.AddChart(l_clickedControl);
      }
      m_lastClickedChart = l_chart;
      m_controller.ShowSettingsView(l_chart.Settings);
    }

    private void OnDeleteClick(object sender, EventArgs e)
    {
      FbiChart l_chart;
      ToolStripMenuItem l_toolStrip = (ToolStripMenuItem)sender;
      Control l_clickedControl = ((ContextMenuStrip)(((ToolStripMenuItem)sender).Owner)).SourceControl;

      if ((l_chart = (FbiChart)this.GetObjectFromControl(l_clickedControl, typeof(FbiChart))) != null && l_chart.HasSettings)
      {
        m_controller.DChartSettings(l_chart.Settings.Id);
        this.Merge(l_clickedControl);
      }
    }

    #region Model

    private void OnChartSettingsCreated(ErrorMessage p_status, UInt32 p_id)
    {
      this.OnChartSettingsChanged(p_status, p_id, CUIVisualizationController.CREATE);
    }

    private void OnChartSettingsUpdated(ErrorMessage p_status, UInt32 p_id)
    {
      this.OnChartSettingsChanged(p_status, p_id, CUIVisualizationController.UPDATE);
    }

    private void OnChartSettingsDeleted(ErrorMessage p_status, UInt32 p_id)
    {
      this.OnChartSettingsChanged(p_status, p_id, CUIVisualizationController.DELETE);
    }

    private void OnChartAccountCreated(ErrorMessage p_status, UInt32 p_id)
    {
      this.OnChartAccountChanged(p_status, p_id, CUIVisualizationController.CREATE);
    }

    private void OnChartAccountUpdated(ErrorMessage p_status, UInt32 p_id)
    {
      this.OnChartAccountChanged(p_status, p_id, CUIVisualizationController.UPDATE);
    }

    private void OnChartAccountDeleted(ErrorMessage p_status, UInt32 p_id)
    {
      this.OnChartAccountChanged(p_status, p_id, CUIVisualizationController.DELETE);
    }


    delegate void OnChartSettingsChanged_delegate(ErrorMessage p_status, UInt32 p_id, Int32 p_val);
    private void OnChartSettingsChanged(ErrorMessage p_status, UInt32 p_id, Int32 p_val)
    {
      if (InvokeRequired)
      {
        OnChartSettingsChanged_delegate func = new OnChartSettingsChanged_delegate(OnChartSettingsChanged);
        Invoke(func, p_status, p_id, p_val);
      }
      else
      {
        this.UpdateChartFromCSModel(m_expectedChartSettings, p_val, p_id, p_status);
      }
    }

    delegate void OnChartSettingsRead_delegate(ErrorMessage p_status, ChartSettings p_settings);
    private void OnChartSettingsRead(ErrorMessage p_status, ChartSettings p_settings)
    {
      if (InvokeRequired)
      {
        OnChartSettingsRead_delegate func = new OnChartSettingsRead_delegate(OnChartSettingsRead);
        Invoke(func, p_status, p_settings);
      }
      else
      {
        if (p_settings.PanelId == m_controller.PanelId)
          m_lastChartSettingsRcvd = p_settings;
      }
    }

    delegate void OnChartAccountChanged_delegate(ErrorMessage p_status, UInt32 p_id, Int32 p_val);
    private void OnChartAccountChanged(ErrorMessage p_status, UInt32 p_id, Int32 p_val)
    {
      if (InvokeRequired)
      {
        OnChartAccountChanged_delegate func = new OnChartAccountChanged_delegate(OnChartAccountChanged);
        Invoke(func, p_status, p_id, p_val);
      }
      else
      {
        this.UpdateChartFromCAModel(m_expectedChartAccounts, p_val, p_id, p_status);
      }
    }

    delegate void OnChartAccountRead_delegate(ErrorMessage p_status, ChartAccount p_account);
    private void OnChartAccountRead(ErrorMessage p_status, ChartAccount p_account)
    {
      if (InvokeRequired)
      {
        OnChartAccountRead_delegate func = new OnChartAccountRead_delegate(OnChartAccountRead);
        Invoke(func, p_status, p_account);
      }
      else
      {
        if (m_lastChartSettingsRcvd != null && p_account.ChartId == m_lastChartSettingsRcvd.Id)
          m_lastChartAccountRcvd = p_account;
      }
    }

    #endregion

    #endregion

    #region Chart

    #region UpdateFromModel

    private bool UpdateChartFromModel(UInt32[] p_expected, Int32 p_value, UInt32 p_id, ErrorMessage p_status)
    {
      if (p_status != ErrorMessage.SUCCESS)
      {
        MessageBox.Show(Local.GetValue("CUI_Charts.error.settings") + " " + Error.GetMessage(p_status));
        ArrayUtils.Set<UInt32>(p_expected, 0);
        return (false);
      }
      p_expected[p_value] += 1;
      return (this.HasCompleteChart());
    }

    private void UpdateChartFromCSModel(UInt32[] p_expected, Int32 p_value, UInt32 p_id, ErrorMessage p_status)
    {
      if (m_lastChartSettingsRcvd == null || m_lastChartSettingsRcvd.Id != p_id)
        return;
      if (p_value == CUIVisualizationController.DELETE)
        this.RemoveChart(p_id);
      else if (this.UpdateChartFromModel(p_expected, p_value, p_id, p_status))
      {
        this.UpdateChart(m_lastClickedChart, ChartSettingsModel.Instance.GetValue(p_id));
        this.ResetExpectedElemsForUpdate();
      }
    }

    private void UpdateChartFromCAModel(UInt32[] p_expected, Int32 p_value, UInt32 p_id, ErrorMessage p_status)
    {
      ChartAccount l_acc;

      if (m_lastChartSettingsRcvd == null || (l_acc = ChartAccountModel.Instance.GetValue(p_id)) == null ||
        l_acc.ChartId != m_lastChartSettingsRcvd.Id)
        return;
      if (this.UpdateChartFromModel(p_expected, p_value, p_id, p_status))
      {
        ChartSettings l_sett = ChartSettingsModel.Instance.GetValue(m_lastChartAccountRcvd.ChartId);
        this.UpdateChart(m_lastClickedChart, l_sett);
        this.ResetExpectedElemsForUpdate();
      }
    }

    private void ResetExpectedElemsForUpdate()
    {
      ArrayUtils.Set<UInt32>(m_expectedChartSettings, 0);
      ArrayUtils.Set<UInt32>(m_expectedChartAccounts, 0);
      m_lastChartAccountRcvd = null;
      m_lastChartSettingsRcvd = null;
    }

    #endregion

    private FbiChart AddChart(Control p_control)
    {
      if (p_control == null)
        return (null);

      FbiChart l_chart = new FbiChart();

      l_chart.Dock = DockStyle.Fill;
      p_control.Controls.Add(l_chart);
      m_charts.Add(l_chart);
      return (l_chart);
    }

    private void UpdateChart(FbiChart p_chart, ChartSettings p_settings)
    {
      if (p_settings == null)
        return;

      if (p_chart == null)
      {
        Control l_control = this.GetOrCreateEmptyPanel();
        if ((p_chart = this.AddChart(l_control)) == null)
          return;
      }
      p_chart.Settings = p_settings;
      if (m_controller.ApplyLastCompute(p_settings))
      {
        m_versionLabel.Text = this.GetVersionName(p_settings);
        m_entityLabel.Text = AxisElemModel.Instance.GetValueName(m_controller.LastConfig.Request.EntityId);
        m_currencyLabel.Text = CurrencyModel.Instance.GetValueName(m_controller.LastConfig.Request.CurrencyId);
        p_chart.Assign(p_settings, m_controller.LastComputation);
      }
    }

    private void RemoveChart(UInt32 p_chartId)
    {
      foreach (FbiChart l_chart in m_charts)
      {
        if (l_chart.HasSettings && l_chart.Settings.Id == p_chartId)
        {
          l_chart.Clear();
          l_chart.Settings = null;
          m_charts.Remove(l_chart);
          return;
        }
      }
    }

    #endregion

    #region Utils

    private string GetVersionName(ChartSettings p_settings)
    {
      return (p_settings.Versions != null && p_settings.Versions.Count == 1 ?
        VersionModel.Instance.GetValue(p_settings.Versions[0]).Name : "N/A");
    }

    private Control GetObjectFromControl(Control p_control, Type p_type)
    {
      foreach (Control l_control in p_control.Controls)
      {
        if (l_control.GetType() == p_type)
          return (l_control);
      }
      return (null);
    }

    private Control GetOrCreateEmptyPanel()
    {
      Control l_control;

      if ((l_control = m_panels.Find(x => x.Controls.Count == 0)) == null)
      {
        l_control = m_panels[m_panels.Count - 1];
        m_lastOrientationPanel = this.OpposedOrientation(m_lastOrientationPanel);
        this.Split(l_control, m_lastOrientationPanel);
        l_control = m_panels[m_panels.Count - 1];
      }
      return (l_control);
    }

    private bool HasCompleteChart()
    {
      for (int i = 0; i < m_controller.ExpectedChartSettings.Length; ++i)
      {
        if (m_expectedChartSettings[i] != m_controller.ExpectedChartSettings[i])
          return (false);
      }
      for (int i = 0; i < m_controller.ExpectedChartAccounts.Length; ++i)
      {
        if (m_expectedChartAccounts[i] != m_controller.ExpectedChartAccounts[i])
          return (false);
      }
      return (true);
    }

    private System.Windows.Forms.Orientation OpposedOrientation(System.Windows.Forms.Orientation p_orientation)
    {
      if (p_orientation == System.Windows.Forms.Orientation.Horizontal)
        return (System.Windows.Forms.Orientation.Vertical);
      return (System.Windows.Forms.Orientation.Horizontal);
    }

    #endregion

  }
}
