using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using FBI.MVC.Model.CRUD;
  using FBI.Network;

  public class PeriodDimension : CRUDEntity
  {
    UInt32 m_id;

    public UInt32 Id 
    { 
      get {return m_id;}

    }
    public DateTime Date { get; set; }
    public UInt32 Image { get; set; }

    public PeriodDimension(UInt32 p_Id)
    {
      m_id = p_Id;
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      GetHashCode();  
    }

    public override bool Equals(object p_obj)
    {
      if (p_obj.GetType() != this.GetType())
        return (false);
      PeriodDimension l_obj = p_obj as PeriodDimension;

      return (l_obj.Id == Id);
    }

    public override Int32 GetHashCode()
    {
      return (Int32)Id;
    }

    public static bool operator ==(PeriodDimension a, PeriodDimension b)
    {
      // If both are null, or both are same instance, return true.
      if (System.Object.ReferenceEquals(a, b))
      {
        return true;
      }

      // If one is null, but not both, return false.
      if (((object)a == null) || ((object)b == null))
      {
        return false;
      }

      // Return true if the fields match:
      return a.Id == b.Id;
    }

    public static bool operator !=(PeriodDimension a, PeriodDimension b)
    {
      return !(a == b);
    }


  }
}
