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

  public partial class CUI2VisualizationContainer : Form, IView
  {
    public CUI2VisualizationContainer()
    {
      InitializeComponent();
      MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      this.Text = Local.GetValue("CUI_Charts.CUI2_vizualization");
    }

    public void SetController(IController p_controller)
    {

    }
  }
}
