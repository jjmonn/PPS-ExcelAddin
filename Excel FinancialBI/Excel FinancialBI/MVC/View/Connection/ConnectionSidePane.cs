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

  public partial class ConnectionSidePane : AddinExpress.XL.ADXExcelTaskPane, IView
  {

    internal bool m_showned { set; get; }


    #region Initialize

    public ConnectionSidePane()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {

    }

    #endregion


    #region Callbacks

    private void ConnectionBT_Click(object sender, EventArgs e)
    {

    }

    private void m_cancelButton_Click(object sender, EventArgs e)
    {

    }

    #endregion


    private void ConnectionSidePane_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (m_circularProgress2 != null) { m_circularProgress2.Stop(); }
      m_passwordTextBox.Text = "";
      e.Cancel = true;
    }

    private void ConnectionSidePane_ADXBeforeTaskPaneShow(object sender, AddinExpress.XL.ADXBeforeTaskPaneShowEventArgs e)
    {
      if (m_showned == false) { this.Visible = false; }
    }


  }
}
