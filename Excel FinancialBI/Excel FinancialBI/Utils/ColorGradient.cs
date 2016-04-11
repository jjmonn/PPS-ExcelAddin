using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Utils
{
  class ColorGradient
  {
    public static List<Color> Create(Color p_start, Color p_end, int steps)
    {
      List<Color> l_list = new List<Color>();

      int stepA = ((p_end.A - p_start.A) / (steps - 1));
      int stepR = ((p_end.R - p_start.R) / (steps - 1));
      int stepG = ((p_end.G - p_start.G) / (steps - 1));
      int stepB = ((p_end.B - p_start.B) / (steps - 1));

      for (int i = 0; i < steps; i++)
      {
        l_list.Add(Color.FromArgb(p_start.A + (stepA * i),
          p_start.R + (stepR * i),
          p_start.G + (stepG * i),
          p_start.B + (stepB * i)));
      }
      return (l_list);
    }
  }
}
