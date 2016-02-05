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
  using Network;

  public partial class ConnectionSidePane : AddinExpress.XL.ADXExcelTaskPane, IView
  {
    internal bool m_shown { set; get; }
    ConnectionSidePaneController m_controller;

    #region Initialize

    public ConnectionSidePane()
    {
      InitializeComponent();
      Authenticator.AuthenticationEvent += OnAuthentification;
      Addin.InitializationEvent += OnInitComplete;
      Addin.ConnectionStateEvent += OnConnectionChanged;
    }

    public void SetController(IController p_controller)
    {

    }

    #endregion

    #region Callbacks

    delegate void OnConnectionOrAuth_delegate(ErrorMessage p_status);
    void OnAuthentification(ErrorMessage p_status)
    {
      if (InvokeRequired)
      {
        OnAuthResult_delegate func = new OnAuthResult_delegate(OnInitComplete);
        Invoke(func);
      }
      else
        FBI.AddinModule.CurrentInstance.SetConnectionIcon((p_status == ErrorMessage.SUCCESS));
    }

    delegate void OnAuthResult_delegate();
    void OnInitComplete()
    {
      if (InvokeRequired)
      {
        OnAuthResult_delegate func = new OnAuthResult_delegate(OnInitComplete);
        Invoke(func);
      }
      else
      {
        Hide();
        FBI.AddinModule.CurrentInstance.SetConnectionIcon(true);
      }
    }

    delegate void OnConnectionChanged_delegate(bool p_connected);
    void OnConnectionChanged(bool p_connected)
    {
      if (InvokeRequired)
      {
        OnConnectionChanged_delegate func = new OnConnectionChanged_delegate(OnConnectionChanged);
        Invoke(func);
      }
      else
      {
        if (p_connected == false)
          FBI.AddinModule.CurrentInstance.SetConnectionIcon(false);
      }
    }

    private void ConnectionBT_Click(object sender, EventArgs e)
    {
      if (Addin.Connect(m_userNameTextBox.Text, m_passwordTextBox.Text) == false)
        MessageBox.Show("The temporary connection function did not suceed.");
    }

    private void m_cancelButton_Click(object sender, EventArgs e)
    {
    }

    private void ConnectionSidePane_ADXBeforeTaskPaneShow(object sender, AddinExpress.XL.ADXBeforeTaskPaneShowEventArgs e)
    {
      if (m_shown == false) { this.Visible = false; }
    }

    #endregion

    private void ConnectionSidePane_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (m_circularProgress2 != null) { m_circularProgress2.Stop(); }
      m_passwordTextBox.Text = "";
      e.Cancel = true;
    }


  }

}

