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

  public partial class FBIFunctionUI : Form, IView
  {
    public FBIFunctionUI()
    {
      InitializeComponent();
    }

    private void MultilangueSetup()
    {
      this.validate_cmd.Text = Local.GetValue("ppsbi.insert_formula");
      this.m_categoryFilterLabel.Text = Local.GetValue("ppsbi.categories_filter");
      this.m_productFilterLabel.Text = Local.GetValue("ppsbi.products_filter");
      this.m_clientFilterLabel.Text = Local.GetValue("ppsbi.clients_filter");
      this.m_versionLabel.Text = Local.GetValue("general.version");
      this.m_currencyLabel.Text = Local.GetValue("general.currency");
      this.m_entityLabel.Text = Local.GetValue("general.entity");
      this.m_accountLabel.Text = Local.GetValue("general.account");
      this.m_periodLabel.Text = Local.GetValue("general.period");
      this.m_adjustmentFilterLabel.Text = Local.GetValue("ppsbi.adjustments_filter");
      this.Text = Local.GetValue("ppsbi.title");
    }

    public void SetController(IController p_controller)
    {

    }

    public void LoadView()
    {
      MultilangueSetup();
    }
  }
}
