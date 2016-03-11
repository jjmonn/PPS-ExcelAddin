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
  using Model.CRUD;

  public partial class CUIVisualization : Form, IView
  {
    CUIVisualizationController m_controller;
    Random m_rand = new Random();
    SafeDictionary<ResultKey, double> m_values;

    public CUIVisualization()
    {
      InitializeComponent();
      MultilangueSetup();
      SafeDictionary<UInt32, SafeDictionary<Int32, List<double>>> l_versionPeriodValueDic;
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
      SuscribeEvents();
    }

    void SuscribeEvents()
    {
      panel2.ContextMenuStrip = m_panelRightClick;
      m_horizontalSplitBT.Click += OnSplitHorizontalClick;
      m_splitVerticalBT.Click += OnSplitVerticalClick;
    }

    void LoadData(SafeDictionary<ResultKey, double> p_values)
    {
      m_values = p_values;
      SelectAccount(1);
    }

    void SelectAccount(UInt32 p_accountId)
    {
      SafeDictionary<ResultKey, double> l_accountValues = new SafeDictionary<ResultKey, double>();

      foreach (KeyValuePair<ResultKey, double> l_pair in m_values)
        if (l_pair.Key.AccountId == p_accountId)
          l_accountValues[l_pair.Key] = l_pair.Value;
    }

    Chart CreateChart()
    {
      Chart l_chart = new Chart();
      ChartArea l_area = new ChartArea();

      l_chart.ChartAreas.Add(l_area);
      l_area.AxisX.MajorGrid.Enabled = false;
      l_area.AxisY.MajorGrid.Enabled = false;
      l_area.IsSameFontSizeForAllAxes = true;

      l_area.AxisY.TitleFont = new Font("calibri", 8);
      l_area.AxisX.TitleFont = new Font("calibri", 8);
      l_area.AxisX.LabelStyle.Angle = -45;
      l_area.AxisY.LabelAutoFitMaxFontSize = 10;
      l_area.AxisX.LabelAutoFitMaxFontSize = 10;
      
      return (l_chart);
    }

    void BindSeries(Chart p_chart, IEnumerable p_dataX, IEnumerable p_dataY)
    {
      Series l_series = new Series();

      l_series.ChartArea = p_chart.ChartAreas.First().Name;
      l_series.Points.DataBindXY(p_dataX, p_dataY);
    }

    #region Split

    void OnSplitHorizontalClick(object p_sender, EventArgs p_args)
    {
      ToolStripMenuItem l_item = p_sender as ToolStripMenuItem;
      ContextMenuStrip l_menu = l_item.Owner as ContextMenuStrip;

      if (l_menu != null)
        Split(l_menu.SourceControl, Orientation.Vertical);
    }

    void OnSplitVerticalClick(object p_sender, EventArgs p_args)
    {
      ToolStripMenuItem l_item = p_sender as ToolStripMenuItem;
      ContextMenuStrip l_menu = l_item.Owner as ContextMenuStrip;

      if (l_menu != null)
        Split(l_menu.SourceControl, Orientation.Horizontal);
    }

    void Split(Control p_control, Orientation p_orientation)
    {
      vSplitContainer l_newSplit = new vSplitContainer();
      l_newSplit.Panel1.ContextMenuStrip = m_panelRightClick;
      l_newSplit.Panel2.ContextMenuStrip = m_panelRightClick;
      l_newSplit.Orientation = p_orientation;
      foreach (Control l_control in p_control.Controls)
        l_newSplit.Panel1.Controls.Add(l_control);

      Chart l_chart = CreateChart();

      p_control.Controls.Clear();
      p_control.Controls.Add(l_newSplit);
      l_newSplit.Dock = DockStyle.Fill;
      l_newSplit.Panel2.Controls.Add(l_chart);
      l_chart.Dock = DockStyle.Fill;
    }

    #endregion

  }
}
