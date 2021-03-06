using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using AddinExpress.XL;

namespace FBI.MVC.View
{
  using FBI;
  using Utils;
  using FBI.Forms;
  using FBI.MVC.Model;
  using FBI.MVC.Model.CRUD;
  using VIBlend.WinForms.Controls;
  using FBI.MVC.Controller;

  public partial class ReportEditionSidePane : AddinExpress.XL.ADXExcelTaskPane
  {
    public bool m_shown { set; get; }
    FbiTreeView<AxisElem> m_entitiesTreeview;
    ReportEditionController m_controller;
    Account.AccountProcess m_process;
    PeriodRangeSelectionController m_periodRangeSelectionController;

    public ReportEditionSidePane()
    {
      InitializeComponent();
      this.ADXBeforeTaskPaneShow += new AddinExpress.XL.ADXBeforeTaskPaneShowEventHandler(this.ReportUploadEntitySelectionSidePane_ADXBeforeTaskPaneShow);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReportUploadEntitySelectionSidePane_FormClosing);
      this.m_validateButton.Click += new System.EventHandler(this.m_validateButton_Click);
      MultilangueSetup();
    }

    public void SetController(ReportEditionController p_controller)
    {
      m_controller = p_controller;
    }

    public void LoadView(Account.AccountProcess p_process, PeriodRangeSelectionController p_periodRangeController)
    {
      m_shown = true;
      m_periodRangeSelectionController = p_periodRangeController;
      m_process = p_process;
      InitCommonProcessComponents();
      if (m_process == Account.AccountProcess.RH)
        InitRHProcessComponents();
    }

    private void MultilangueSetup()
    {
      this.Text = Local.GetValue("upload.edition");
      m_entitySelectionLabel.Text = Local.GetValue("upload.entity_selection");
      m_periodsSelectionLabel.Text = Local.GetValue("upload.periods_selection");
      m_accountSelectionLabel.Text = Local.GetValue("upload.accounts_selection");
      m_validateButton.Text = Local.GetValue("general.validate");
    }

    private void InitCommonProcessComponents()
    {
      m_treeviewPanel.Controls.Clear();
      m_entitiesTreeview = new FbiTreeView<AxisElem>(AxisElemModel.Instance.GetDictionary(AxisType.Entities));
      m_entitiesTreeview.KeyPress += EntitiesTreeviewKeyPress;
      m_entitiesTreeview.ImageList = m_entitiesImageList;
      m_treeviewPanel.Controls.Add(m_entitiesTreeview);
      m_entitiesTreeview.Dock = DockStyle.Fill;
      SetRHComponentVisible(false);
      m_periodsSelectionPanel.Controls.Clear();
    }

    private void SetRHComponentVisible(bool p_visible)
    {
      this.m_accountSelectionLabel.Visible = p_visible;
      this.m_accountSelectionComboBox.Visible = p_visible;
      this.m_periodsSelectionLabel.Visible = p_visible;
      m_periodsSelectionPanel.Visible = p_visible;
    }

    private void InitRHProcessComponents()
    {
      PeriodRangeSelectionControl l_periodRangeSelectionControl = m_periodRangeSelectionController.View as PeriodRangeSelectionControl;
      m_periodsSelectionPanel.Controls.Add(l_periodRangeSelectionControl);
      l_periodRangeSelectionControl.Dock = DockStyle.Fill;
      InitRHAccountsCombobox(m_accountSelectionComboBox);
      SetRHComponentVisible(true);
    }

    public static void InitRHAccountsCombobox(vComboBox p_comboBox)
    {
      p_comboBox.Items.Clear();
      foreach (Account l_account in AccountModel.Instance.GetDictionary().Values)
      {
        if (l_account.Process == Account.AccountProcess.RH)
        {
          if (l_account.FormulaType == Account.FormulaTypes.HARD_VALUE_INPUT || l_account.FormulaType == Account.FormulaTypes.FIRST_PERIOD_INPUT)
          {
            ListItem l_listItem = new ListItem();
            l_listItem.Text = l_account.Name;
            l_listItem.Value = l_account.Id;
            p_comboBox.Items.Add(l_listItem);
          }
          if (p_comboBox.Items.Count > 0)
            p_comboBox.SelectedItem = p_comboBox.Items.ElementAt(0);
        }
      }
    }

    private void EntitiesTreeviewKeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == (char)Keys.Return)
      {
        LaunchReportCreation();
      }
    }

    private void m_validateButton_Click(object sender, EventArgs e)
    {
      LaunchReportCreation();
    }

    private void LaunchReportCreation()
    {
      if (m_controller.CanLaunchReport(m_entitiesTreeview.SelectedNode, m_accountSelectionComboBox.SelectedItem, m_periodRangeSelectionController.GetPeriodList()))
      {
        if (m_controller.CreateReport() == true)
          this.Hide();
        else
          Forms.MsgBox.Show(m_controller.Error);
      }
    }

    private void ReportUploadEntitySelectionSidePane_ADXBeforeTaskPaneShow(object sender, ADXBeforeTaskPaneShowEventArgs e)
    {
      this.Visible = m_shown;
    }

    private void ReportUploadEntitySelectionSidePane_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.Hide();
      e.Cancel = true;
    }

  }
}

