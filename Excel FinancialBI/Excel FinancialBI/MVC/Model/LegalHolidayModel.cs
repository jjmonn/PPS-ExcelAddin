using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using CRUD;
  using Network;
  using Utils;

  class LegalHolidayModel : ICRUDModel<LegalHoliday>
  {
    static LegalHolidayModel s_instance = new LegalHolidayModel();
    public static LegalHolidayModel Instance { get { return (s_instance); } }
    SortedDictionary<UInt32, MultiIndexDictionary<UInt32, UInt32, LegalHoliday>> m_legalHolidayDic =
      new SortedDictionary<UInt32, MultiIndexDictionary<UInt32, UInt32, LegalHoliday>>();

    LegalHolidayModel()
    {
      CreateCMSG = ClientMessage.CMSG_CREATE_LEGAL_HOLIDAY;
      DeleteCMSG = ClientMessage.CMSG_DELETE_LEGAL_HOLIDAY;
      ListCMSG = ClientMessage.CMSG_LIST_LEGAL_HOLIDAY;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_LEGAL_HOLIDAY;
      UpdateListCMSG = ClientMessage.CMSG_CRUD_LEGAL_HOLIDAY;

      CreateSMSG = ServerMessage.SMSG_CREATE_LEGAL_HOLIDAY_ANSWER;
      ReadSMSG = ServerMessage.SMSG_READ_LEGAL_HOLIDAY_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_LEGAL_HOLIDAY_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_LEGAL_HOLIDAY_ANSWER;
      UpdateListSMSG = ServerMessage.SMSG_CRUD_LEGAL_HOLIDAY_LIST_ANSWER;

      Build = LegalHoliday.BuildLegalHoliday;

      InitCallbacks();
    }

    #region CRUD

    public override bool Delete(UInt32 p_id)
    {
      ByteBuffer l_packet = new ByteBuffer((UInt16)DeleteCMSG);
      LegalHoliday l_lh = GetValue(p_id);

      if (l_lh == null)
        return (false);
      return (Delete(l_lh.EmployeeId, l_lh.Period));
    }

    public bool Delete(UInt32 p_employeeId, UInt32 p_period)
    {
      ByteBuffer packet = new ByteBuffer((UInt16)DeleteCMSG);

      packet.WriteUint32(p_employeeId);
      packet.WriteUint32(p_period);
      packet.Release();
      return NetworkManager.Send(packet);
    }

    protected override void ListAnswer(ByteBuffer p_packet)
    {
      if (p_packet.GetError() == ErrorMessage.SUCCESS)
      {
        UInt32 count = p_packet.ReadUint32();
        for (Int32 i = 1; i <= count; i++)
        {
          LegalHoliday l_lh = Build(p_packet) as LegalHoliday;

          if (m_legalHolidayDic.ContainsKey(l_lh.EmployeeId) == false)
            m_legalHolidayDic[l_lh.EmployeeId] = new MultiIndexDictionary<UInt32, UInt32, LegalHoliday>();
          m_legalHolidayDic[l_lh.EmployeeId].Set(l_lh.Id, l_lh.Period, l_lh);
        }
        RaiseObjectInitializedEvent(p_packet.GetError(), typeof(LegalHoliday));
        IsInit = true;
      }
      else
      {
        RaiseObjectInitializedEvent(p_packet.GetError(), typeof(LegalHoliday));
        IsInit = false;
      }
    }

    protected override void ReadAnswer(ByteBuffer packet)
    {
      LegalHoliday l_lh = Build(packet) as LegalHoliday;

      if (m_legalHolidayDic.ContainsKey(l_lh.EmployeeId) == false)
        m_legalHolidayDic[l_lh.EmployeeId] = new MultiIndexDictionary<UInt32, UInt32, LegalHoliday>();
      m_legalHolidayDic[l_lh.EmployeeId].Set(l_lh.Id, l_lh.Period, l_lh);
      RaiseReadEvent(packet.GetError(), l_lh);
    }

    protected override void DeleteAnswer(ByteBuffer packet)
    {
      UInt32 Id = packet.ReadUint32();

      foreach (MultiIndexDictionary<UInt32, UInt32, LegalHoliday> lhDic in m_legalHolidayDic.Values)
      {
        if (lhDic.ContainsKey(Id))
        {
          lhDic.RemovePrimary(Id);
          break;
        }
      }
      RaiseDeleteEvent(packet.GetError(), Id);
    }
    #endregion

    #region Mapping

    public override LegalHoliday GetValue(UInt32 p_id)
    {
      foreach (MultiIndexDictionary<UInt32, UInt32, LegalHoliday> lhDic in m_legalHolidayDic.Values)
        if (lhDic.ContainsKey(p_id))
          return (lhDic.PrimaryKeyItem(p_id));
      return (null);
    }

    public LegalHoliday GetValue(UInt32 p_employeeId, UInt32 p_period)
    {
      if (m_legalHolidayDic.ContainsKey(p_employeeId) == false)
        return (null);
      MultiIndexDictionary<UInt32, UInt32, LegalHoliday> l_lhDic = m_legalHolidayDic[p_employeeId];

      return (l_lhDic.SecondaryKeyItem(p_period));
    }

    public SortedDictionary<UInt32, MultiIndexDictionary<UInt32, UInt32, LegalHoliday>> GetDictionary()
    {
      return (m_legalHolidayDic);
    }

    public MultiIndexDictionary<UInt32, UInt32, LegalHoliday> GetDictionary(UInt32 p_employeeId)
    {
      if (m_legalHolidayDic.ContainsKey(p_employeeId) == false)
        return (null);
      return (m_legalHolidayDic[p_employeeId]);
    }

    #endregion
  }
}
