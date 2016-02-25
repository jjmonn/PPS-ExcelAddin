using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBI.MVC.View
{
  using Controller;
  using Utils;
  using Network;
  using Model;
  using Model.CRUD;

  public partial class ControllingUI_2 : Form, IView
  {

    #region Variables

    private CUIController m_controller = null;

    #endregion

    #region Initialize

    public ControllingUI_2()
    {
      InitializeComponent();
      
    }

    public void SetController(IController p_controller)
    {
      this.m_controller = p_controller as CUIController;
    }

    public void LoadView()
    {
      this.MultilangueSetup();

      this.m_controller.CreatePane();
      CUI2LeftPane l_leftPane = this.m_controller.LeftPaneController.View as CUI2LeftPane;
      CUI2RightPane l_rightPane = this.m_controller.RightPaneController.View as CUI2RightPane;
      ResultView l_resultView =  m_controller.ResultController.View as ResultView;
      this.SplitContainer1.Panel1.Controls.Add(l_leftPane);
      this.SplitContainer2.Panel2.Controls.Add(l_rightPane);
      this.m_DGVsControlTab.Controls.Add(l_resultView);
      l_leftPane.Dock = DockStyle.Fill;
      l_rightPane.Dock = DockStyle.Fill;
      l_resultView.Dock = DockStyle.Fill;
      
      this.Show();
      SuscribeEvents();
    }

    public void SuscribeEvents()
    {
      LegacyComputeModel.Instance.ComputeCompleteEvent += OnComputeResult;
      m_refreshButton.MouseDown += OnRefreshButtonMouseDown;
      m_versionComparisonButton.MouseDown += OnVersionComparisionButtonMouseDown;
      m_versionSwitchButton.MouseDown += OnVersionSwitchButtonMouseDown;
      m_hideVersionButton.MouseDown += OnHideVersionButtonMouseDown;
    }

    private void MultilangueSetup()
    {
      this.RefreshRightClick.Text = Local.GetValue("CUI.refresh");
      this.SelectAllToolStripMenuItem.Text = Local.GetValue("CUI.select_all");
      this.UnselectAllToolStripMenuItem.Text = Local.GetValue("CUI.unselect_all");
      this.ExpandAllRightClick.Text = Local.GetValue("CUI.expand_all");
      this.CollapseAllRightClick.Text = Local.GetValue("CUI.collapse_all");
      this.LogRightClick.Text = Local.GetValue("CUI.log");
      this.DGVFormatsButton.Text = Local.GetValue("CUI.display_options");
      this.ColumnsAutoSize.Text = Local.GetValue("CUI.adjust_columns_size");
      this.ColumnsAutoFitBT.Text = Local.GetValue("CUI.automatic_columns_adjustment");
      this.SelectAllToolStripMenuItem1.Text = Local.GetValue("CUI.select_all");
      this.UnselectAllToolStripMenuItem1.Text = Local.GetValue("CUI.unselect_all");

      this.m_currencyLabel.Text = Local.GetValue("general.currency");
      this.m_versionLabel.Text = Local.GetValue("general.version");
      this.m_entityLabel.Text = Local.GetValue("general.entity");

      this.MainMenu.Text = Local.GetValue("CUI.main_menu");
      this.m_refreshButton.Text = Local.GetValue("CUI.refresh");
      this.ExcelToolStripMenuItem.ToolTipText = Local.GetValue("CUI.drop_on_excel_tooltip");
      this.ExcelToolStripMenuItem.Text = Local.GetValue("CUI.drop_on_excel");
      this.DropOnExcelToolStripMenuItem.Text = Local.GetValue("CUI.drop_on_excel");
      this.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem.Text = Local.GetValue("CUI.drop_on_excel_visible_part");
      this.BusinessControlToolStripMenuItem.Text = Local.GetValue("CUI.performance_review");
      this.BusinessControlToolStripMenuItem.ToolTipText = Local.GetValue("CUI.performance_review_tooltip");
      this.m_versionComparisonButton.Text = Local.GetValue("CUI.display_versions_comparison");
      this.m_versionSwitchButton.Text = Local.GetValue("CUI.switch_versions");
      this.m_hideVersionButton.Text = Local.GetValue("CUI.take_off_comparison");
      this.m_refreshButton.ToolTipText = Local.GetValue("CUI.refresh_tooltip");
      this.ChartBT.Text = Local.GetValue("CUI.charts");
      this.Text = Local.GetValue("CUI.financials");
    }

    void OnComputeResult(ErrorMessage p_status, AComputeRequest p_request, SafeDictionary<UInt32, ComputeResult> p_result)
    {
      if (p_status == ErrorMessage.SUCCESS && p_result != null)
        m_controller.ResultController.DisplayResult(p_result);
      else
        MessageBox.Show(Error.GetMessage(p_status));
    }

    void OnRefreshButtonMouseDown(object sender, MouseEventArgs e)
    {
      m_controller.Compute();
    }

    void OnVersionComparisionButtonMouseDown(object sender, MouseEventArgs e)
    {
      m_controller.ResultController.DisplayVersionComparaison();
    }

    void OnVersionSwitchButtonMouseDown(object sender, MouseEventArgs e)
    {
      m_controller.ResultController.SwitchVersionComparaison();
    }

    void OnHideVersionButtonMouseDown(object sender, MouseEventArgs e)
    {
      m_controller.ResultController.HideVersionComparaison();
    }

    #endregion

  }
}
