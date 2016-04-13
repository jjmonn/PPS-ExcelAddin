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

    public static Color Add(Color p_a, Color p_b)
    {
      int a = (p_a.A + p_b.A > 255 ? 255 : p_a.A + p_b.A);
      int r = (p_a.R + p_b.R > 255 ? 255 : p_a.R + p_b.R);
      int g = (p_a.G + p_b.G > 255 ? 255 : p_a.G + p_b.G);
      int b = (p_a.B + p_b.B > 255 ? 255 : p_a.B + p_b.B);
      return (Color.FromArgb(a, r, g, b));
    }

    public static Color Add(Color p_a, int n)
    {
      return (ColorGradient.Add(p_a, Color.FromArgb(n, n, n, n)));
    }

    public static Color Sub(Color p_a, Color p_b)
    {
      int a = (p_a.A - p_b.A < 0 ? 0 : p_a.A - p_b.A);
      int r = (p_a.R - p_b.R < 0 ? 0 : p_a.R - p_b.R);
      int g = (p_a.G - p_b.G < 0 ? 0 : p_a.G - p_b.G);
      int b = (p_a.B - p_b.B < 0 ? 0 : p_a.B - p_b.B);
      return (Color.FromArgb(a, r, g, b));
    }

    public static Color Sub(Color p_a, int n, bool p_useAlpha = false)
    {
      return (ColorGradient.Sub(p_a, Color.FromArgb((p_useAlpha ? n : 0), n, n, n)));
    }
  }
}
