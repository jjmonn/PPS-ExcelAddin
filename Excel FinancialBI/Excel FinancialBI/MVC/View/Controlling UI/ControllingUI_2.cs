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

namespace FBI.MVC.View
{
  using Controller;
  using Utils;
  using Network;
  using Model;
  using Model.CRUD;
  using Forms;

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
      this.SplitContainer1.Panel2.Controls.Add(l_resultView);
      l_leftPane.Dock = DockStyle.Fill;
      l_rightPane.Dock = DockStyle.Fill;
      l_resultView.Dock = DockStyle.Fill;

      BusinessControlToolStripMenuItem.Enabled = false;
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
      m_chartBT.MouseDown += OnChartButtonMouseDown;
    }

    override protected void OnClosed(EventArgs e)
    {
      base.OnClosed(e);
      LegacyComputeModel.Instance.ComputeCompleteEvent -= OnComputeResult;
    }

    private void MultilangueSetup()
    {
      this.RefreshRightClick.Text = Local.GetValue("CUI.refresh");
      this.SelectAllToolStripMenuItem.Text = Local.GetValue("CUI.select_all");
      this.UnselectAllToolStripMenuItem.Text = Local.GetValue("CUI.unselect_all");
      this.SelectAllToolStripMenuItem1.Text = Local.GetValue("CUI.select_all");
      this.UnselectAllToolStripMenuItem1.Text = Local.GetValue("CUI.unselect_all");

      this.m_currencyLabel.Text = Local.GetValue("general.currency");
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
      this.m_chartBT.Text = Local.GetValue("CUI.charts");
      this.Text = Local.GetValue("CUI.financials");
    }

    delegate void OnComputeResult_delegate(ErrorMessage p_status, AComputeRequest p_request, SafeDictionary<UInt32, ComputeResult> p_result);
    void OnComputeResult(ErrorMessage p_status, AComputeRequest p_request, SafeDictionary<UInt32, ComputeResult> p_result)
    {
      if (InvokeRequired)
      {
        OnComputeResult_delegate func = new OnComputeResult_delegate(OnComputeResult);
        Invoke(func, p_status, p_request, p_result);
      }
      else
      {
        if (p_status == ErrorMessage.SUCCESS && p_result != null)
        {
          m_controller.LastResult = p_result;
          LegacyComputeRequest l_request = p_request as LegacyComputeRequest;

          if (l_request != null)
          {
            BusinessControlToolStripMenuItem.Enabled = l_request.IsDiff;
            CurrencyTB.Text = CurrencyModel.Instance.GetValueName(l_request.CurrencyId);
            EntityTB.Text = AxisElemModel.Instance.GetValueName(l_request.EntityId);
          }
          else
            BusinessControlToolStripMenuItem.Enabled = false;
          m_controller.ResultController.DisplayResult(p_result);
        }
        else
          MsgBox.Show(Error.GetMessage(p_status));
      }
    }

    void OnRefreshButtonMouseDown(object sender, MouseEventArgs e)
    {
      m_controller.Compute();
    }

    void OnChartButtonMouseDown(object sender, MouseEventArgs e)
    {
      m_controller.ShowCharts();
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
