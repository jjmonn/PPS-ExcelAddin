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

  public partial class PasswordBox : Form
  {
    public PasswordBox()
    {
      InitializeComponent();
      MultilanguageSetup();
    }

    private void MultilanguageSetup()
    {
      this.Text = Local.GetValue("general.password");
      this.AcceptBT.Text = Local.GetValue("general.validate");
      this.CancelBT.Text = Local.GetValue("general.cancel");
    }

    public const string Canceled = "PBoxCanceled";

    public static string m_returnValue = "";
    public static string Open(string p_message, string p_title = "")
    {
      PasswordBox window = new PasswordBox();

      window.Text = p_title;
      window.DescTB.Text = p_message;
      window.ShowDialog();
      if (m_returnValue == "")
        m_returnValue = Canceled;
      string returnValue = m_returnValue;
      m_returnValue = "";
      return (returnValue);

    }

    private void AcceptBT_Click(object sender, EventArgs e)
    {
      m_returnValue = m_passwordTextBox.Text;
      Close();
    }

    private void CancelBT_Click(object sender, EventArgs e)
    {
      m_returnValue = Canceled;
      Close();
    }
  }
}
