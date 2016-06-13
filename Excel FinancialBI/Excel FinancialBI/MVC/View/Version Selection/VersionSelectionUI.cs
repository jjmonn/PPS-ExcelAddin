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

  public partial class VersionSelectionUI : Form, IView
  {
    public VersionSelectionUI()
    {
      InitializeComponent();
      MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      this.Text = Local.GetValue("versions.select_version");
      VersionsTreeComboBox.Text = Local.GetValue("versions.select_version");
      ValidateButton.Text = Local.GetValue("general.validate");
    }

    public void SetController(IController p_controller)
    {

    }
  }
}
