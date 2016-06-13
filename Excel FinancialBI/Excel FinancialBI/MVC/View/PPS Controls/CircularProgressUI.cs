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

  public partial class CircularProgressUI : Form
  {
    string m_titel;
    string m_text;
    static CircularProgressUI currentUI = null;

    public static CircularProgressUI Open(string p_titel, string p_text)
    {
      currentUI = new CircularProgressUI(p_titel, p_text);
      currentUI.Show();
      currentUI.vCircularProgressBar1.Start();
      return (currentUI);
    }

    public delegate void Close_delegate();
    public static new void Close()
    {
      currentUI.Hide();
      currentUI = null;
    }

    public CircularProgressUI(string p_titel, string p_text)
    {
      InitializeComponent();
      m_text = p_text;
      m_titel = p_titel;
      LoadView();
    }

    public void LoadView()
    {
      Label1.Text = m_text;
      Text = m_titel;
    }
  }
}
