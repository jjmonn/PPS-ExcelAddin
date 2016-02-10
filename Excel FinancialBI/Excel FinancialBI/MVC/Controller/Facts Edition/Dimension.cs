using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using FBI.Utils;
  using Microsoft.Office.Interop.Excel;
  using FBI.MVC.Model.CRUD;

  class Dimension<T> 
  {
    public enum Alignment
    {
      UNDEFINED = 0,
      VERTICAL,
      HORIZONTAL,
      UNCLEAR
    }
    
    public SafeDictionary<Range, T> m_values = new SafeDictionary<Range, T>();
    Alignment m_aligment;
    Type DimensionType 
    {
      get {return typeof(T);}
    }

    int NbValues
    {
      get {return m_values.Count; }
    }

    public void AddValue(Range p_range, T p_dimensionObject)
    {
      m_values.Add(p_range, p_dimensionObject);
    }
                               
    public void DefineAlignment()
    {
      // TO DO : set the alignment value of m_alignment
      //         based on the differences between rows and columns
    }




  }
}
