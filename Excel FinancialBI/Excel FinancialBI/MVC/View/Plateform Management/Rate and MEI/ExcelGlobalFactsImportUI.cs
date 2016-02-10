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
  using FBI;
  using Utils;

  public partial class ExcelGlobalFactsImportUI : Form, IView
  {
    public ExcelGlobalFactsImportUI()
    {
      InitializeComponent();
      MultilanguageSetup();
    }

    public void SetController(IController p_controller)
    {

    }

    private void MultilanguageSetup()
    {
      this.rates_edit_BT.Name = "rates_edit_BT";
      this.m_periodsLabel.Text = Local.GetValue("upload.periods");
      this.m_valuesLabel.Text = Local.GetValue("upload.values");
      this.import_BT.Text = Local.GetValue("upload.upload");
      this.m_globalFactLabel.Text = Local.GetValue("general.macro_economic_indicator");
      this.Text = Local.GetValue("upload.global_facts_upload");
    }
  }
}
