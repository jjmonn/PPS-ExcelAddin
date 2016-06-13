using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public abstract class AComputeRequest
  {
    private static Int32 m_lastId = 0;
    private Int32 m_id;
    public Account.AccountProcess Process { get; set; }
    public UInt32 GlobalFactVersionId { get; set; }
    public UInt32 RateVersionId { get; set; }
    public UInt32 CurrencyId { get; set; }
    public Int32 StartPeriod { get; set; }
    public Int32 NbPeriods { get; set; }
    public List<UInt32> AccountList { get; set; }

    public override bool Equals(object p_obj)
    {
      return (GetHashCode() == p_obj.GetHashCode());
    }

    public override int GetHashCode()
    {
      return m_id;
    }

    public AComputeRequest()
    {
      m_id = ++m_lastId;
      AccountList = new List<uint>();
    }

    public void Dump(ByteBuffer p_packet, UInt32 p_versionId, UInt32 p_entityId)
    {
      Version l_version = VersionModel.Instance.GetValue(p_versionId);

      p_packet.WriteUint32((UInt32)Process);
      p_packet.WriteUint32(p_versionId);
      p_packet.WriteUint32(GlobalFactVersionId);
      p_packet.WriteUint32(RateVersionId);
      p_packet.WriteUint32(p_entityId);
      p_packet.WriteUint32(CurrencyId);
      if (Process == Account.AccountProcess.RH || l_version == null)
      {
        p_packet.WriteInt32(StartPeriod);
        p_packet.WriteInt32(NbPeriods);
      }
      else
      {
        p_packet.WriteUint32(l_version.StartPeriod);
        p_packet.WriteUint32(l_version.NbPeriod);
      }
      p_packet.WriteUint32((UInt32)AccountList.Count);
      foreach (UInt32 l_account in AccountList)
        p_packet.WriteUint32(l_account);
    }
  }
}
