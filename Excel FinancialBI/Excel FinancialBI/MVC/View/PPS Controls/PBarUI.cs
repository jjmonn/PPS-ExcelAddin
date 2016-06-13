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

  public partial class PBarUI : Form
  {
    public PBarUI(string p_title, Int32 p_maxValue)
    {
      InitializeComponent();
      Text = p_title;
      m_progressBar.Maximum = p_maxValue;
    }

    public int Value
    {
      set { 
        m_progressBar.Value = value;
        m_progressBar.Refresh();
      }
      get { return (m_progressBar.Value); }
    }
  }
}
