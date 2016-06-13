using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  public class ExchangeRateVersion : NamedHierarchyCRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public UInt32 ParentId { get; set; }
    public string Name { get; set; }
    public bool IsFolder { get; set; }
    public Int32 ItemPosition { get; set; }
    public UInt32 StartPeriod { get; set; }
    public UInt16 NbPeriod { get; set; }
    public UInt32 Image { get; set; }

    public ExchangeRateVersion() { }
    private ExchangeRateVersion(UInt32 p_id)
    {
      Id = p_id;
    }

    public static ExchangeRateVersion BuildExchangeRateVersion(ByteBuffer p_packet)
    {
      ExchangeRateVersion l_version = new ExchangeRateVersion(p_packet.ReadUint32());

      l_version.ParentId = p_packet.ReadUint32();
      l_version.Name = p_packet.ReadString();
      l_version.IsFolder = p_packet.ReadBool();
      l_version.ItemPosition = p_packet.ReadInt32();
      l_version.StartPeriod = p_packet.ReadUint32();
      l_version.NbPeriod = p_packet.ReadUint16();
      if (l_version.IsFolder == true)
          l_version.Image = 1;
      else
          l_version.Image = 0;

      return (l_version);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32(ParentId);
      p_packet.WriteString(Name);
      p_packet.WriteBool(IsFolder);
      p_packet.WriteInt32(ItemPosition);
      p_packet.WriteUint32(StartPeriod);
      p_packet.WriteUint16(NbPeriod);
    }

    public void CopyFrom(ExchangeRateVersion p_model)
    {
      ParentId = p_model.ParentId;
      Name = p_model.Name;
      IsFolder = p_model.IsFolder;
      ItemPosition = p_model.ItemPosition;
      StartPeriod = p_model.StartPeriod;
      NbPeriod = p_model.NbPeriod;
      Image = p_model.Image;
    }

    public ExchangeRateVersion Clone()
    {
      ExchangeRateVersion l_clone = new ExchangeRateVersion(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      ExchangeRateVersion l_cmpVersion = p_obj as ExchangeRateVersion;

      if (l_cmpVersion == null)
        return 0;
      if (l_cmpVersion.ItemPosition > ItemPosition)
        return -1;
      else
        return 1;
    }
  }
}
