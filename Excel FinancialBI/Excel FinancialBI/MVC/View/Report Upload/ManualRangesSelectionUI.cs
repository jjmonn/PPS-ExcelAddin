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

  public partial class ManualRangesSelectionUI : Form, IView
  {
    public ManualRangesSelectionUI()
    {
      InitializeComponent();
      MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      Label1.Text = Local.GetValue("upload.msg_selection_instruction");
      Label2.Text = Local.GetValue("upload.accounts_selection");
      Label3.Text = Local.GetValue("upload.entity_selection");
      Label4.Text = Local.GetValue("upload.periods_selection");
      Validate_Cmd.Text = Local.GetValue("general.validate");
      this.Text = Local.GetValue("upload.title_input_ranges_edition");
    }

    public void SetController(IController p_controller)
    {

    }
  }
}
