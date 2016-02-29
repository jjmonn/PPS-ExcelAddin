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
  using FBI.MVC.Controller;
  using Utils;


  public partial class ProgressBarView : Form, IView 
  {
    IProgressBarController m_controller;

    public ProgressBarView(bool p_menuVisble)
    {
      InitializeComponent();
      if (p_menuVisble == false)
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.TopMost = true;
      SetupMultilangue();
      SubscribeEvents();
    }

    private void SetupMultilangue()
    {
      this.m_cancelButton.Text = Local.GetValue("general.cancel");
    }

    private void SubscribeEvents()
    {
      this.m_cancelButton.Click += new System.EventHandler(this.m_cancelButton_Click);
    }

    //public void SetController(IProgressBarController p_controller)
    //{
    //  m_controller = p_controller;
    //}

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as IProgressBarController;
    }

    public void SetLabelText(string p_text)
    {
      this.m_label.Text = p_text;
    }

    public void SetupProgressBar(int p_maxValue)
    {
      m_progressBar.Value = 0;
      m_progressBar.Maximum = p_maxValue;
    }

    public void AddProgress(int p_progress)
    {
      if ((m_progressBar.Value + p_progress) >= m_progressBar.Maximum)
        m_progressBar.Value = m_progressBar.Maximum;
      else
        m_progressBar.Value = m_progressBar.Value + p_progress;
    }

    public void SetToMaxProgress()
    {
      m_progressBar.Value = m_progressBar.Maximum;
    }

    private void m_cancelButton_Click(object sender, EventArgs e)
    {
      m_controller.Cancel();
      this.Close();
    }


  }
}
