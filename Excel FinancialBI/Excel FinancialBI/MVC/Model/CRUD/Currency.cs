using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public class Currency : NamedCRUDEntity, System.IComparable
  {
    public UInt32 Id { get; private set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public bool InUse { get; set; }
    public UInt32 Image { get; set; }

    public Currency() { }
    private Currency(System.UInt32 p_id)
    {
      Id = p_id;
    }

    public static CRUD.Currency BuildCurrency(ByteBuffer p_packet)
    {
      CRUD.Currency l_currency = new CRUD.Currency(p_packet.ReadUint32());

      l_currency.Name = p_packet.ReadString();
      l_currency.Symbol = p_packet.ReadString();
      l_currency.InUse = p_packet.ReadBool();

      return (l_currency);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteString(Name);
      p_packet.WriteString(Symbol);
      p_packet.WriteBool(InUse);
    }

    public void CopyFrom(CRUD.Currency p_model)
    {
      Name = p_model.Name;
      Symbol = p_model.Symbol;
      InUse = p_model.InUse;
    }

    public CRUD.Currency Clone()
    {
      CRUD.Currency l_clone = new CRUD.Currency(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      CRUD.Currency l_cmpCurrency = p_obj as CRUD.Currency;

      if (l_cmpCurrency == null)
        return 0;
      return string.Compare(Name.ToUpper(), l_cmpCurrency.Name.ToUpper());
    }
  }
}
