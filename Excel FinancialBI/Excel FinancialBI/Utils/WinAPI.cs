using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

public class WinApi
{
  [DllImport("user32.dll")]
  public static extern bool GetCaretPos(out System.Drawing.Point lpPoint);
}
