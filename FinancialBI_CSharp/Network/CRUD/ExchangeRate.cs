using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
  public class ExchangeRate : CRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public UInt32 DestCurrencyId { get; private set; }
    public UInt32 RateVersionId { get; private set; }
    public UInt32 Period { get; private set; }
    public double Value { get; set; }
    public UInt32 Image { get; set; }

    public ExchangeRate() { }
    private ExchangeRate(UInt32 p_id)
    {
      Id = p_id;
    }

    public static ExchangeRate BuildExchangeRate(ByteBuffer p_packet)
    {
      ExchangeRate l_rate = new ExchangeRate(p_packet.ReadUint32());

      l_rate.DestCurrencyId = p_packet.ReadUint32();
      l_rate.RateVersionId = p_packet.ReadUint32();
      l_rate.Period = p_packet.ReadUint32();
      l_rate.Value = p_packet.ReadDouble();

      return (l_rate);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      p_packet.WriteUint32(DestCurrencyId);
      p_packet.WriteUint32(RateVersionId);
      p_packet.WriteUint32(Period);
      p_packet.WriteDouble(Value);
    }

    public void CopyFrom(ExchangeRate p_model)
    {
      DestCurrencyId = p_model.DestCurrencyId;
      RateVersionId = p_model.RateVersionId;
      Period = p_model.Period;
      Value = p_model.Value;
    }

    public ExchangeRate Clone()
    {
      ExchangeRate l_clone = new ExchangeRate(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      ExchangeRate l_cmpRate = p_obj as ExchangeRate;

      if (l_cmpRate == null)
        return 0;
      if (l_cmpRate.DestCurrencyId > DestCurrencyId)
        return -1;
      else
        return 1;
    }
  }
}
