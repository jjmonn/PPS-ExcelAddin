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

  public partial class CUI2Visualization : UserControl, IView
  {
    public CUI2Visualization()
    {
      InitializeComponent();
      MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      m_entityLabel.Text = Local.GetValue("CUI.entity");
      m_currencyLabel.Text = Local.GetValue("CUI.currency");
      m_versionLabel.Text = Local.GetValue("CUI.version");
      m_refreshButton.Text = Local.GetValue("CUI.refresh");
    }

    public void SetController(IController p_controller)
    {

    }
  }
}
