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

namespace FBI.MVC.View
{
  using Controller;
  using Utils;
  using Model;
  using Model.CRUD;
  using System.Diagnostics;
  using Forms;
  using Network;

  public partial class CUIVisualization : Form, IView
  {
    CUIVisualizationController m_controller;

    private FbiChart m_lastClickedChart;

    public CUIVisualization()
    {
      this.InitializeComponent();
      this.MultilangueSetup();
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
      panel2.ContextMenuStrip = m_panelRightClick;
      m_horizontalSplitBT.Click += OnSplitHorizontalClick;
      m_splitVerticalBT.Click += OnSplitVerticalClick;
      m_chartEdit.Click += OnChartClick;

      ChartSettingsModel.Instance.CreationEvent += OnSettingsUpdated;
      ChartSettingsModel.Instance.UpdateEvent += OnSettingsUpdated;
    }

    private void CloseView()
    {
      ChartSettingsModel.Instance.CreationEvent -= OnSettingsUpdated;
      ChartSettingsModel.Instance.UpdateEvent -= OnSettingsUpdated;
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
    }

    #endregion


    private void OnFormClosed(object sender, FormClosedEventArgs e)
    {
      CloseView();
    }
    
    private void OnSettingsUpdated(ErrorMessage p_msg, UInt32 p_id)
    {
      this.UpdateChart(m_lastClickedChart, ChartSettingsModel.Instance.GetValue(p_id));
    }

    private void OnChartClick(object sender, EventArgs e)
    {
      FbiChart l_chart;
      ToolStripMenuItem l_toolStrip = (ToolStripMenuItem)sender;
      Control l_clickedControl = ((ContextMenuStrip)(((ToolStripMenuItem)sender).Owner)).SourceControl;

      if ((l_chart = this.GetObjectFromControl(l_clickedControl, typeof(FbiChart))) == null)
      {
        l_chart = new FbiChart();
        l_chart.Dock = DockStyle.Fill;
        l_clickedControl.Controls.Add(l_chart);
      }
      m_lastClickedChart = l_chart;

      if (l_chart.HasSettings())
      {
        m_controller.ShowSettingsView(l_chart.Settings.Id);
      }
      else
      {
        m_controller.ShowSettingsView();
      }
    }

    #region Utils

    private void UpdateChart(FbiChart p_chart, ChartSettings p_settings)
    {
      if (p_chart != null)
        p_chart.Assign(p_settings, m_controller.LastComputation);
    }

    private FbiChart GetObjectFromControl(Control p_control, Type p_type)
    {
      foreach (Control l_control in p_control.Controls)
      {
        if (l_control.GetType() == p_type)
          return ((FbiChart)l_control);
      }
      return (null);
    }

    #endregion

  }
}
