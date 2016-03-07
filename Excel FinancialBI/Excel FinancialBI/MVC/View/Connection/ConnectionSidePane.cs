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
  using Utils;

  public partial class ConnectionSidePane : AddinExpress.XL.ADXExcelTaskPane
  {
    public bool m_shown { set; get; }

    #region Initialize

    public ConnectionSidePane()
    {
      InitializeComponent();
   
      SuscribeEvents();
      SetupMultilangue();
      IsLoading = false;
      ConnectionBT.Visible = true;
    }

    private void SuscribeEvents()
    {
      Authenticator.AuthenticationEvent += OnAuthentification;
      Addin.InitializationEvent += OnInitComplete;
      Addin.ConnectionStateEvent += OnConnectionChanged;

      this.ADXBeforeTaskPaneShow += new AddinExpress.XL.ADXBeforeTaskPaneShowEventHandler(this.ConnectionSidePane_ADXBeforeTaskPaneShow);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnectionSidePane_FormClosing);
      this.ConnectionBT.Click += new System.EventHandler(this.ConnectionBT_Click);
      this.m_cancelButton.Click += new System.EventHandler(this.m_cancelButton_Click);
    }

    private void SetupMultilangue()
    {
      this.m_userLabel.Text = Local.GetValue("connection.user_id");
      this.m_passwordLabel.Text = Local.GetValue("connection.password");
      this.ConnectionBT.Text = Local.GetValue("connection.connection");
      this.m_cancelButton.Text = Local.GetValue("general.cancel");
      this.Text = Local.GetValue("connection.connection");
    }

    bool m_isloading;
    bool IsLoading
    {
      set
      {
        m_isloading = value;
        ConnectionBT.Visible = !value;
        m_cancelButton.Visible = value;
        m_circularProgress2.Visible = value;
        m_circularProgress2.Enabled = value;
      }
      get { return m_isloading;}
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
          if (p_status == ErrorMessage.PERMISSION_DENIED)
            Forms.MsgBox.Show(Local.GetValue("connection.error.wrong_credentials"));
          else if (p_status != ErrorMessage.SUCCESS)
            Forms.MsgBox.Show(Error.GetMessage(p_status));
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
        IsLoading = false;
        Hide(); 
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
        }
      }
    }

    private void ConnectionBT_Click(object sender, EventArgs e)
    {

      this.BackgroundWorker1 = new BackgroundWorker();
      this.BackgroundWorker1.DoWork += BackgroundWorker1_DoWork;
      this.BackgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
      this.BackgroundWorker1.WorkerSupportsCancellation = true;

      IsLoading = true;
      m_circularProgress2.Start();
      BackgroundWorker1.RunWorkerAsync();
     
    }

    private void m_cancelButton_Click(object sender, EventArgs e)
    {
      IsLoading = false;
    }

    #endregion

    #region Connection background worker

    private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
    {
      Addin.Disconnect();
      if (Addin.Connect(m_userNameTextBox.Text, m_passwordTextBox.Text) == false)
      {
        this.BackgroundWorker1.CancelAsync();
        Forms.MsgBox.Show(Local.GetValue("connection.error.connection_refused"));
      }
      else
      {
        while (IsLoading == true) { }
        bool test = IsLoading;
      }
    }

    private delegate void AfterConnectionAttemp_Delegate(object sender, RunWorkerCompletedEventArgs e);
    private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (InvokeRequired)
      {
        AfterConnectionAttemp_Delegate MyDelegate = new AfterConnectionAttemp_Delegate(BackgroundWorker1_RunWorkerCompleted);
        this.Invoke(MyDelegate, new object[] {sender, e});
      }
      else
      {
      //  IsLoading = false;
        m_circularProgress2.Stop();
      }
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
      if (m_shown == false) { this.Visible = false; }
    }

  }
}

