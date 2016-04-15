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

    private void SuscribeEvents()
    {
      m_panel.ContextMenuStrip = m_panelRightClick;
      m_horizontalSplitBT.Click += OnSplitHorizontalClick;
      m_splitVerticalBT.Click += OnSplitVerticalClick;
      m_chartEdit.Click += OnChartClick;
      m_refreshButton.Click += OnRefreshClick;

      ChartSettingsModel.Instance.CreationEvent += OnSettingsCreated;
      ChartSettingsModel.Instance.UpdateEvent += OnSettingsUpdated;
      ChartSettingsModel.Instance.DeleteEvent += OnSettingsDeleted;
    }

    private void CloseView()
    {
      ChartSettingsModel.Instance.CreationEvent -= OnSettingsCreated;
      ChartSettingsModel.Instance.UpdateEvent -= OnSettingsUpdated;
      ChartSettingsModel.Instance.DeleteEvent -= OnSettingsDeleted;
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

    #endregion

    #region Events

    private void OnFormClosed(object sender, FormClosedEventArgs e)
    {
      CloseView();
    }

    private void OnSettingsCreated(ErrorMessage p_msg, UInt32 p_id)
    {
      this.CreateChart(ChartSettingsModel.Instance.GetValue(p_id));
    }

    private void OnSettingsUpdated(ErrorMessage p_msg, UInt32 p_id)
    {
      this.UpdateChart(m_lastClickedChart, ChartSettingsModel.Instance.GetValue(p_id));
    }

    private void OnSettingsDeleted(ErrorMessage p_msg, UInt32 p_id)
    {
      foreach (FbiChart l_chart in m_charts)
      {
        if (l_chart != null && l_chart.HasSettings && l_chart.Settings.Id == p_id)
          this.RemoveChart(l_chart);
      }
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

      if (l_chart.HasSettings)
      {
        m_controller.ShowSettingsView(l_chart.Settings);
      }
      else
      {
        m_controller.ShowSettingsView();
      }
    }

    #endregion

    #region Chart

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

    private void CreateChart(ChartSettings p_settings)
    {
      FbiChart l_chart;

      if (p_settings == null)
        return;

      Control l_control = this.GetOrCreateEmptyPanel();
      if ((l_chart = this.AddChart(l_control)) == null)
        return;
      this.UpdateChart(l_chart, p_settings);
    }

    private void UpdateChart(FbiChart p_chart, ChartSettings p_settings)
    {
      if (p_settings == null || p_chart == null)
        return;

      m_controller.ApplyLastCompute(p_settings);
      m_versionLabel.Text = this.GetVersionName(p_settings);
      m_entityLabel.Text = AxisElemModel.Instance.GetValueName(m_controller.LastConfig.Request.EntityId);
      m_currencyLabel.Text = CurrencyModel.Instance.GetValueName(m_controller.LastConfig.Request.CurrencyId);
      p_chart.Assign(p_settings, m_controller.LastComputation);
    }

    private void RemoveChart(FbiChart l_chart)
    {
      l_chart.Settings = null;
      m_charts.Remove(l_chart);
    }

    #endregion

    #region Utils

    private string GetVersionName(ChartSettings p_settings)
    {
      return (p_settings.Versions.Count == 1 ?
        VersionModel.Instance.GetValue(p_settings.Versions[0]).Name :
        "N/A");
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

    private System.Windows.Forms.Orientation OpposedOrientation(System.Windows.Forms.Orientation p_orientation)
    {
      if (p_orientation == System.Windows.Forms.Orientation.Horizontal)
        return (System.Windows.Forms.Orientation.Vertical);
      return (System.Windows.Forms.Orientation.Horizontal);
    }

    #endregion

  }
}
