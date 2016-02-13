using System;
using System.Drawing;
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
    FactsEditionController m_factsEditionController;

    public ReportUploadEntitySelectionSidePane()
    {
      InitializeComponent();
      this.ADXBeforeTaskPaneShow += new AddinExpress.XL.ADXBeforeTaskPaneShowEventHandler(this.ReportUploadEntitySelectionSidePane_ADXBeforeTaskPaneShow);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReportUploadEntitySelectionSidePane_FormClosing);
      MultilangueSetup();
    }

    private void InitView()
    {
      m_shown = true;
      m_entitiesTreeview = new FbiTreeView<AxisElem>(AxisElemModel.Instance.GetDictionary(AxisType.Entities));
      this.TableLayoutPanel1.Controls.Add(m_entitiesTreeview, 0, 1);
      m_entitiesTreeview.Dock = DockStyle.Fill;

      m_entitiesTreeview.KeyDown += EntitiesTreeviewKeyDown;
    }

    private void MultilangueSetup()
    {
      m_entitySelectionLabel.Text = Local.GetValue("upload.entities_selection");
      m_periodsSelectionLabel.Text = Local.GetValue("upload.periods_selection");
      m_accountSelectionLabel.Text = Local.GetValue("upload.accounts_selection");
      m_validateButton.Text = Local.GetValue("general.validate");
    }


    private void EntitiesTreeviewKeyDown(object sender, KeyEventArgs e)
    {
      vTreeNode l_node = m_entitiesTreeview.SelectedNode;
      if (l_node != null)
      {
        Account.AccountProcess l_process = (Account.AccountProcess)FBI.Properties.Settings.Default.processId;
        m_factsEditionController = new FactsEditionController(l_process);
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

