using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;
  using Utils;

  public enum TimeConfig
  {
    YEARS = 1,
    MONTHS,
    DAYS,
    WEEK
  }

  public class TimeUtils
  {
    public static TimeConfig GetParentConfig(TimeConfig p_config)
    {
      if (p_config == TimeConfig.MONTHS)
        return (TimeConfig.YEARS);
      if (p_config == TimeConfig.DAYS)
        return (TimeConfig.WEEK);
      return (p_config);
    }

    public static bool IsParentConfig(TimeConfig p_configA, TimeConfig p_configB)
    {
      if (p_configA == TimeConfig.YEARS && p_configB == TimeConfig.MONTHS)
        return (true);
      if (p_configA == TimeConfig.WEEK && p_configB == TimeConfig.DAYS)
        return (true);
      return (false);
    }

    public static TimeConfig GetLowestTimeConfig(List<TimeConfig> p_list)
    {
      TimeConfig l_config = TimeConfig.YEARS;

      foreach (TimeConfig elem in p_list)
      {
        if (elem == TimeConfig.MONTHS && l_config == TimeConfig.YEARS)
          l_config = elem;
        if (elem == TimeConfig.DAYS && (l_config == TimeConfig.WEEK || l_config == TimeConfig.YEARS))
          l_config = elem;
      }
      return (l_config);
    }

    public static Int32 GetParentConfigNbPeriods(TimeConfig p_config, Int32 p_nbPeriods)
    {
      if (p_config == TimeConfig.MONTHS)
        return ((Int32)Math.Ceiling(p_nbPeriods / 12.0));
      else if (p_config == TimeConfig.DAYS)
        return ((Int32)Math.Ceiling(p_nbPeriods / 7.0));
      return (p_nbPeriods);
    }

    public static Int32 GetChildConfigNbPeriods(TimeConfig p_config, Int32 p_nbPeriods)
    {
      if (p_config == TimeConfig.YEARS)
        return ((Int32)Math.Ceiling(p_nbPeriods * 12.0));
      else if (p_config == TimeConfig.WEEK)
        return ((Int32)Math.Ceiling(p_nbPeriods * 7.0));
      return (p_nbPeriods);
    }

    public static string GetLocal(TimeConfig p_config)
    {
      switch (p_config)
      {
        case TimeConfig.DAYS:
          return (Local.GetValue("period.day"));
        case TimeConfig.WEEK:
          return (Local.GetValue("period.week"));
        case TimeConfig.MONTHS:
          return (Local.GetValue("period.month"));
        case TimeConfig.YEARS:
          return (Local.GetValue("period.year"));
        default:
          return ("");
      }
    }

    public static TimeConfig Parse(string p_config)
    {

      if (p_config == Local.GetValue("period.day"))
        return (TimeConfig.DAYS);
      if (p_config == Local.GetValue("period.week"))
        return (TimeConfig.WEEK);
      if (p_config == Local.GetValue("period.month"))
        return (TimeConfig.MONTHS);
      if (p_config == Local.GetValue("period.year"))
        return (TimeConfig.YEARS);
      return ((TimeConfig)0);
    }
  }

  public class Version : BaseVersion, IComparable, NamedHierarchyCRUDEntity
  {
    public bool Locked { get; set; }
    public string LockDate { get; set; }
    public TimeConfig TimeConfiguration { get; set; }
    public UInt32 RateVersionId { get; set; }
    public string CreatedAt { get; set; }
    public UInt32 GlobalFactVersionId { get; set; }
    public UInt32 FormulaPeriodIndex { get; set; }
    public UInt32 FormulaNbPeriod { get; set; }

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
      l_version.FormulaPeriodIndex = p_packet.ReadUint32();
      l_version.FormulaNbPeriod = p_packet.ReadUint32();

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
      p_packet.WriteUint32(FormulaPeriodIndex);
      p_packet.WriteUint32(FormulaNbPeriod);
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
      FormulaPeriodIndex = p_model.FormulaPeriodIndex;
      FormulaNbPeriod = p_model.FormulaNbPeriod;
      Image = p_model.Image;
    }

    public Version Clone()
    {
      Version l_clone = new Version(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public override BaseVersion BaseClone()
    {
      return (Clone());
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

    public Account.AccountProcess Process
    {
      get
      {
        if (TimeConfiguration == TimeConfig.DAYS || TimeConfiguration == TimeConfig.WEEK)
          return (Account.AccountProcess.RH);
        return (Account.AccountProcess.FINANCIAL);
      }
    }
  }
}
