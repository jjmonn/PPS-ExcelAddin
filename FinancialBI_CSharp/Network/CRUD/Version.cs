using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
  public enum TimeConfig
  {
    YEARS = 1,
    MONTHS
  }

  public class Version : NamedHierarchyCRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public UInt32 ParentId { get; set; }
    public string Name { get; set; }
    public bool Locked { get; set; }
    public string LockDate { get; set; }
    public bool IsFolder { get; set; }
    public Int32 ItemPosition { get; set; }
    public TimeConfig TimeConfiguration { get; set; }
    public UInt32 RateVersionId { get; set; }
    public UInt32 StartPeriod { get; set; }
    public UInt16 NbPeriod { get; set; }
    public string CreatedAt { get; set; }
    public UInt32 GlobalFactVersionId { get; set; }
    public UInt32 Image { get; set; }

    public Version() { }
    private Version(UInt32 p_id)
    {
      Id = p_id;
    }

    public static Version BuildVersion(ByteBuffer p_packet)
    {
      Version l_version = new Version(p_packet.ReadUint32());

      l_version.ParentId = p_packet.ReadUint32();
      l_version.Name = p_packet.ReadString();
      l_version.Locked = p_packet.ReadBool();
      l_version.LockDate = p_packet.ReadString();
      l_version.IsFolder = p_packet.ReadBool();
      l_version.ItemPosition = p_packet.ReadInt32();
      l_version.TimeConfiguration = (TimeConfig)p_packet.ReadUint32();
      l_version.RateVersionId = p_packet.ReadUint32();
      l_version.StartPeriod = p_packet.ReadUint32();
      l_version.NbPeriod = p_packet.ReadUint16();
      l_version.CreatedAt = p_packet.ReadString();
      l_version.GlobalFactVersionId = p_packet.ReadUint32();

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
      p_packet.WriteBool(Locked);
      p_packet.WriteString(LockDate);
      p_packet.WriteBool(IsFolder);
      p_packet.WriteInt32(ItemPosition);
      p_packet.WriteUint32((UInt32)TimeConfiguration);
      p_packet.WriteUint32(RateVersionId);
      p_packet.WriteUint32(StartPeriod);
      p_packet.WriteUint16(NbPeriod);
      p_packet.WriteString(CreatedAt);
      p_packet.WriteUint32(GlobalFactVersionId);
    }

    public void CopyFrom(Version p_model)
    {
      ParentId = p_model.ParentId;
      Name = p_model.Name;
      Locked = p_model.Locked;
      LockDate = p_model.LockDate;
      IsFolder = p_model.IsFolder;
      ItemPosition = p_model.ItemPosition;
      TimeConfiguration = p_model.TimeConfiguration;
      RateVersionId = p_model.RateVersionId;
      StartPeriod = p_model.StartPeriod;
      NbPeriod = p_model.NbPeriod;
      CreatedAt = p_model.CreatedAt;
      GlobalFactVersionId = p_model.GlobalFactVersionId;
      Image = p_model.Image;
    }

    public Version Clone()
    {
      Version l_clone = new Version(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      Version l_cmp = p_obj as Version;

      if (l_cmp == null)
        return 0;
      if (l_cmp.ItemPosition > ItemPosition)
        return -1;
      else
        return 1;
    }
  }
}
