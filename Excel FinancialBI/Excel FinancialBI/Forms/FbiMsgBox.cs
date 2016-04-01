using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace FBI.Forms
{
  class MsgBox
  {
    static public void Show(string p_text)
    {
      Task.Factory.StartNew(() => { MessageBox.Show(p_text); });
    }

    static public void Show(string p_text, string p_caption)
    {
      Task.Factory.StartNew(() => { MessageBox.Show(p_text, p_caption); });
    }

    static public void Show(string p_text, string p_caption, MessageBoxButtons p_buttons)
    {
      Task.Factory.StartNew(() => { MessageBox.Show(p_text, p_caption, p_buttons); });
    }

    static public void Show(string p_text, string p_caption, MessageBoxButtons p_buttons, MessageBoxIcon p_icons)
    {
      Task.Factory.StartNew(() => { MessageBox.Show(p_text, p_caption, p_buttons, p_icons); });
    }
  }
}
