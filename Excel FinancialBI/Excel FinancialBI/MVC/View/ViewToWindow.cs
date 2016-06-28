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
  public partial class ViewToWindow : Form
  {
    Control m_parent;
    Control m_control;
    Form m_originForm;
    DockStyle m_dock;
    Action m_onClose;

    public ViewToWindow(Control p_control, Form p_origin, Action p_onClose = null)
    {
      InitializeComponent();

      Size = new Size(p_control.Size.Width + 15, p_control.Size.Height + 38);
      m_onClose = p_onClose;
      m_originForm = p_origin;
      m_parent = p_control.Parent;
      m_control = p_control;
      m_dock = m_control.Dock;
      Controls.Add(p_control);
      p_control.Dock = DockStyle.Fill;
      SubscribeEvents();
      Show();
    }

    void SubscribeEvents()
    {
      m_parent.Disposed += OnParentDisposed;
      m_originForm.Resize += OnOriginFormResize;
    }

    void OnOriginFormResize(object sender, EventArgs e)
    {
      if (m_originForm.WindowState == FormWindowState.Minimized)
        Hide();
      else if (this.Visible == false)
        Show();
    }

    void OnParentDisposed(object sender, EventArgs e)
    {
      Close();
    }

    private void OnFormClosing(object sender, FormClosingEventArgs e)
    {
      Controls.Remove(m_control);
      m_parent.Controls.Add(m_control);
      m_control.Dock = m_dock;
      m_parent.Disposed -= OnParentDisposed;
      m_originForm.Resize -= OnOriginFormResize;
      if (m_onClose != null)
        m_onClose();
    }
  }
}
