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

    public void InitView()
    {
      this.MultilangueSetup();

      this.m_controller.CreatePane();
      this.SplitContainer1.Panel1.Controls.Add(this.m_controller.LeftPaneController.View as CUI2LeftPane);
      this.SplitContainer2.Panel2.Controls.Add(this.m_controller.RightPaneController.View as CUI2RightPane);
      this.m_DGVsControlTab.Controls.Add(m_controller.ResultController.View as ResultView);

      this.Show();
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

      this.m_currencyLabel.Text = Local.GetValue("CUI.currency");
      this.m_versionLabel.Text = Local.GetValue("CUI.version");
      this.m_entityLabel.Text = Local.GetValue("CUI.entity");

      this.MainMenu.Text = Local.GetValue("CUI.main_menu");
      this.RefreshToolStripMenuItem.Text = Local.GetValue("CUI.refresh");
      this.ExcelToolStripMenuItem.ToolTipText = Local.GetValue("CUI.drop_on_excel_tooltip");
      this.ExcelToolStripMenuItem.Text = Local.GetValue("CUI.drop_on_excel");
      this.DropOnExcelToolStripMenuItem.Text = Local.GetValue("CUI.drop_on_excel");
      this.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem.Text = Local.GetValue("CUI.drop_on_excel_visible_part");
      this.BusinessControlToolStripMenuItem.Text = Local.GetValue("CUI.performance_review");
      this.BusinessControlToolStripMenuItem.ToolTipText = Local.GetValue("CUI.performance_review_tooltip");
      this.VersionsComparisonToolStripMenuItem.Text = Local.GetValue("CUI.display_versions_comparison");
      this.SwitchVersionsToolStripMenuItem.Text = Local.GetValue("CUI.switch_versions");
      this.HideVersionsComparisonToolStripMenuItem.Text = Local.GetValue("CUI.take_off_comparison");
      this.RefreshToolStripMenuItem.ToolTipText = Local.GetValue("CUI.refresh_tooltip");
      this.ChartBT.Text = Local.GetValue("CUI.charts");
      this.Text = Local.GetValue("CUI.financials");
    }

    #endregion

  }
}
