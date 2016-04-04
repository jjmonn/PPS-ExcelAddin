using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using AddinExpress.XL;
  
namespace FBI.MVC.View
{
  using FBI;
  using Utils;
  using Model.CRUD;

  public partial class ReportUploadAccountInfoSidePane : AddinExpress.XL.ADXExcelTaskPane
  {
    public  bool m_shown { set; get; }
    public ReportUploadAccountInfoSidePane()
    {
      InitializeComponent();
      MultilangueSetup();
      m_shown = false;
    }

    private void MultilangueSetup()
    {
      m_accountLabel.Text = Local.GetValue("general.account");
      m_formulaLabel.Text = Local.GetValue("general.formula");
      m_descriptionLabel.Text = Local.GetValue("general.description");
      m_formulaType.Text = Local.GetValue("accounts.type");
    }

    public void SelectAccount(Account p_account)
    {
      m_shown = true;
      m_accountTextBox.Text = p_account.Name;
      m_descriptionTextBox.Text = p_account.Description;
      m_formulaTextBox.Text = p_account.Formula;
      switch (p_account.FormulaType)
      {
        case Account.FormulaTypes.TITLE:
          m_formulaTypeTB.Text = Local.GetValue("account.formula_type_title");
          break;
        case Account.FormulaTypes.HARD_VALUE_INPUT:
          m_formulaTypeTB.Text = Local.GetValue("account.formula_type_input");
          break;
        case Account.FormulaTypes.FORMULA:
          m_formulaTypeTB.Text = Local.GetValue("account.formula_type_formula");
          break;
        case Account.FormulaTypes.FIRST_PERIOD_INPUT:
          m_formulaTypeTB.Text = Local.GetValue("account.formula_type_first");
          break;
        case Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS:
          m_formulaTypeTB.Text = Local.GetValue("account.formula_type_sub");
          break;
      }
    }

    private void ReportUploadAccountInfoSidePane_ADXBeforeTaskPaneShow(object sender, ADXBeforeTaskPaneShowEventArgs e)
    {
      this.Visible = m_shown;
    }
  }
}
