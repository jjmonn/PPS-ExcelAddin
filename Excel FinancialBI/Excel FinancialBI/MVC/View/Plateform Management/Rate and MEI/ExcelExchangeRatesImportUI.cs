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

  public partial class ExcelExchangeRatesImportUI : Form, IView
  {
    public ExcelExchangeRatesImportUI()
    {
      InitializeComponent();
      MultilangueSetup();
    }

    public void SetController(IController p_controller)
    {

    }
    private void MultilangueSetup()
    {
      this.m_periodsLabel.Text = Local.GetValue("upload.periods");
      this.m_ratesLabel.Text = Local.GetValue("upload.rates");
      this.import_BT.Text = Local.GetValue("upload.upload");
      this.m_currencyLabel.Text = Local.GetValue("general.currency");
      this.Text = Local.GetValue("upload.exchange_rates_upload");
    }
  }
}
