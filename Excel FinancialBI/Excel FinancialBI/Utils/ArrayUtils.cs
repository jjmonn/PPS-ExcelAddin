using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Utils
{
  class ArrayUtils
  {
    public static void Set<T>(T[] p_array, T p_val)
    {
      for (int i = 0; i < p_array.Length; ++i)
        p_array[i] = p_val;
    }
  }
}
