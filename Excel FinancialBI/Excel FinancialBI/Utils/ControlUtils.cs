using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIBlend.WinForms.Controls;

namespace FBI.Utils
{
  class ControlUtils
  {
    public static T Clone<T>(T p_toCopy) where T : Control, new()
    {
      T l_copy = new T();

      l_copy.Text = p_toCopy.Text;
      l_copy.BackColor = p_toCopy.BackColor;
      l_copy.Bounds = p_toCopy.Bounds;
      l_copy.Font = p_toCopy.Font;
      l_copy.ForeColor = p_toCopy.ForeColor;
      l_copy.Left = p_toCopy.Left;
      l_copy.Location = p_toCopy.Location;
      l_copy.Margin = p_toCopy.Margin;
      l_copy.Name = p_toCopy.Name;
      l_copy.Padding = p_toCopy.Padding;
      l_copy.Tag = p_toCopy.Tag;
      l_copy.Size = p_toCopy.Size;
      return (l_copy);
    }
  }
}
