using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace FBI.Forms
{
  class FbiToolStripMenuItem : ToolStripMenuItem
  {
    Control m_control;
    ContextMenuStrip m_menu;

    public void SetContextMenuStrip(ContextMenuStrip p_menu, Control p_control)
    {
      m_control = p_control;
      m_menu = p_menu;
      Click += OpenMenu;
    }

    void OpenMenu(object p_sender, EventArgs p_e)
    {
      Point l_position = m_control.PointToScreen(this.Bounds.Location);

      l_position.Y += Height;
      m_menu.Show(l_position);
    }
  }
}
