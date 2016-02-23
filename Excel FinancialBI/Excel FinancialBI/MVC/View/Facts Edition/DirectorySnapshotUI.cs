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

  public partial class DirectorySnapshotUI : Form, IView
  {
    public DirectorySnapshotUI()
    {
      InitializeComponent();
    }

    private void MultilangueSetup()
    {
      this.Text = Local.GetValue("upload.periods_selection");
      m_validateButton.Text = Local.GetValue("general.validate");
      m_accountSelectionLabel.Text = Local.GetValue("upload.accounts_selection");
      m_directoryLabel.Text = Local.GetValue("upload.directory");
      m_worksheetNameLabel.Text = Local.GetValue("upload.worksheet_name");
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
