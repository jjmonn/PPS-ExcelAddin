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

  public partial class ConnectionSidePane : AddinExpress.XL.ADXExcelTaskPane
  {
    internal bool m_shown { set; get; }

    #region Initialize

    public ConnectionSidePane()
    {
      InitializeComponent();
      Authenticator.AuthenticationEvent += OnAuthentification;
      Addin.InitializationEvent += OnInitComplete;
      Addin.ConnectionStateEvent += OnConnectionChanged;
      IsLoading = false;
    }

    bool IsLoading
    {
      set
      {
        ConnectionBT.Visible = !value;
        m_cancelButton.Visible  = value;
      }
    }

    #endregion

    #region Callbacks

    delegate void OnAuthentification_delegate(ErrorMessage p_status);
    void OnAuthentification(ErrorMessage p_status)
    {
      if (InvokeRequired)
      {
        OnAuthentification_delegate func = new OnAuthentification_delegate(OnAuthentification);
        Invoke(func, p_status);
      }
      else
      {
        if (p_status != ErrorMessage.SUCCESS)
        {
          IsLoading = false;
          FBI.AddinModule.CurrentInstance.SetConnectionIcon(false);
        }
      }
    }

    delegate void OnInitComplete_delegate();
    void OnInitComplete()
    {
      if (InvokeRequired)
      {
        OnInitComplete_delegate func = new OnInitComplete_delegate(OnInitComplete);
        Invoke(func);
      }
      else
      {
        Hide();
        FBI.AddinModule.CurrentInstance.SetConnectionIcon(true);
        IsLoading = false;
      }
    }

    delegate void OnConnectionChanged_delegate(bool p_connected);
    void OnConnectionChanged(bool p_connected)
    {
      if (InvokeRequired)
      {
        OnConnectionChanged_delegate func = new OnConnectionChanged_delegate(OnConnectionChanged);
        Invoke(func, p_connected);
      }
      else
      {
        if (p_connected == false)
        {
          IsLoading = false;
          FBI.AddinModule.CurrentInstance.SetConnectionIcon(false);
        }
      }
    }

    private void ConnectionBT_Click(object sender, EventArgs e)
    {
      IsLoading = true;
      if (Addin.Connect(m_userNameTextBox.Text, m_passwordTextBox.Text) == false)
        MessageBox.Show("The temporary connection function did not suceed.");
    }

    private void m_cancelButton_Click(object sender, EventArgs e)
    {
      IsLoading = false;
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

