using System;
using System.Drawing;
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

  public partial class ReportUploadEntitySelectionSidePane : AddinExpress.XL.ADXExcelTaskPane
  {
    public bool m_shown { set; get; }
    FbiTreeView<AxisElem> m_entitiesTreeview;
//    ReportBuilder m_controller;
    Account.AccountProcess m_process;
    PeriodRangeSelectionControl m_periodRangeSelectionControl;

    public ReportUploadEntitySelectionSidePane()
    {
      InitializeComponent();
      this.ADXBeforeTaskPaneShow += new AddinExpress.XL.ADXBeforeTaskPaneShowEventHandler(this.ReportUploadEntitySelectionSidePane_ADXBeforeTaskPaneShow);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReportUploadEntitySelectionSidePane_FormClosing);
      MultilangueSetup();
    }
  
    public void InitView(Account.AccountProcess p_process)
    {
      m_shown = true;
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
     }

    private void InitRHProcessComponents()
    {
      if (m_periodRangeSelectionControl == null)
      {
        m_periodRangeSelectionControl = new PeriodRangeSelectionControl(FBI.Properties.Settings.Default.version_id);
        m_periodsSelectionPanel.Controls.Add(m_periodRangeSelectionControl);
        m_periodRangeSelectionControl.Dock = DockStyle.Fill;
      }
      InitRHAccountsCombobox();
      SetRHComponentVisible(true);
    }

    private void InitRHAccountsCombobox()
    {
      m_accountSelectionComboBox.Items.Clear();
      foreach (Account l_account in AccountModel.Instance.GetDictionary().Values)
      {
        if (l_account.Process == Account.AccountProcess.RH)
        {
          if (l_account.FormulaType == Account.FormulaTypes.HARD_VALUE_INPUT || l_account.FormulaType == Account.FormulaTypes.FIRST_PERIOD_INPUT)
          {
          ListItem l_listItem = new ListItem();
          l_listItem.Text = l_account.Name;
          l_listItem.Value = l_account.Id;
          m_accountSelectionComboBox.Items.Add(l_listItem);
          }
        }
      }
    }

    private void EntitiesTreeviewKeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == (char)Keys.Return)
      {
        ReportBuilder l_reportBuilder = new ReportBuilder(FBI.Properties.Settings.Default.version_id);
        bool l_inputsValids = l_reportBuilder.CanLaunchReport(m_entitiesTreeview.SelectedNode, m_process, m_accountSelectionComboBox.SelectedItem, m_periodRangeSelectionControl.GetPeriodList());
        if (l_inputsValids)
        {
          bool l_reportResult = l_reportBuilder.CreateReport();
          if (l_reportResult)

            // TO DO : launch snapshot
            //.Addin.AssociateReportUploadControler(true, p_periodList, p_RHaccountName);
            return;
        }
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

