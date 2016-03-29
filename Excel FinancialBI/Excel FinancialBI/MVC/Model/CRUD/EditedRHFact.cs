using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Microsoft.Office.Interop.Excel;
  
  class EditedRHFact : EditedFactBase
  {
    public Int32 EditedClientId {get; set;}
    public FactTag EditedFactTag;
    public FactTag ModelFactTag {get; set;}
    public LegalHoliday EditedLegalHoliday {get; set;}
    public LegalHoliday ModelLegalHoliday {get; set;}
    private string m_address;


    public EditedRHFact(UInt32 p_accountId, UInt32 p_entityId, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId, 
                        UInt32 p_employeeId, UInt32 p_versionId, PeriodDimension p_period, Range p_cell) 
                        : base(p_accountId, p_entityId, p_clientId, p_productId, p_adjustmentId, p_employeeId, p_versionId, 
                               p_period, p_cell)
    {   
      Value = 1;
      m_address = Cell.Address;
      InitFactTag();
      InitLegalHoliday();
    }

    private void InitFactTag()
    {
      EditedFactTag = new FactTag();
      EditedFactTag.Tag = FactTag.TagType.NONE;
      EditedFactTag.Id = Id;
 
      ModelFactTag = new FactTag();
      ModelFactTag.Id = Id;
      ModelFactTag.Tag = FactTag.TagType.NONE;
    }

    private void InitLegalHoliday()
    {
      EditedLegalHoliday = new LegalHoliday();
      EditedLegalHoliday.Id = 0;
      EditedLegalHoliday.EmployeeId = EmployeeId;
      EditedLegalHoliday.Period = (UInt32)Period;
      EditedLegalHoliday.Tag = LegalHolidayTag.NONE;

      ModelLegalHoliday = new LegalHoliday();
      ModelLegalHoliday.Id = 0;
      ModelLegalHoliday.EmployeeId = EmployeeId;
      ModelLegalHoliday.Period = (UInt32)Period;
      ModelLegalHoliday.Tag = LegalHolidayTag.NONE;
    }

    public void SetId(UInt32 p_id)
    {
      Id = p_id;
      EditedFactTag.Id = p_id;
      ModelFactTag.Id = p_id;
    }

    public void SetEditedClient(UInt32 p_clientId, bool p_setCellStatus)
    {
      EditedClientId = (Int32)p_clientId;
      EditedLegalHoliday.Tag = LegalHolidayTag.NONE;
      EditedFactTag.Tag = FactTag.TagType.NONE;
   
      if (p_setCellStatus == true)
        SetFactValueStatus();
    }

    public void SetEditedFactType(FactTag.TagType p_tagType , bool p_setCellStatus)
    {
      EditedFactTag.Tag = p_tagType;

      if (p_setCellStatus == true)
        SetFactValueStatus();
    }

    public void SetEditedLegalHoliday(LegalHolidayTag p_legalHolidayTag, bool p_setCellStatus)
    {
       if (p_legalHolidayTag == LegalHolidayTag.FER)
      {
        EditedLegalHoliday.Tag = LegalHolidayTag.FER;
        EditedClientId = 0;
        EditedFactTag.Tag = FactTag.TagType.NONE;
      }
      else
        EditedLegalHoliday.Tag = LegalHolidayTag.NONE;
       
      if (p_setCellStatus == true)
         SetFactValueStatus();
    }

    public void UpdateRHFactModels(Fact p_fact, FactTag p_factTag, LegalHoliday p_legalHoliday)
    {
      Value = p_fact.Value;
      UpdateFactBase(p_fact);
      SetId(p_fact.Id);

      if (p_factTag != null)
        ModelFactTag.Tag = p_factTag.Tag;

      if (p_legalHoliday != null)
      {
        ModelLegalHoliday.Id = p_legalHoliday.Id;
        ModelLegalHoliday.Tag = LegalHolidayTag.FER;
      }

      if (p_factTag != null && p_legalHoliday != null)
      {
        System.Diagnostics.Debug.WriteLine("Inconsistency in commit: fact tag and legal holiday on the same employee same period. Employee_id: " + p_legalHoliday.EmployeeId.ToString());
        System.Diagnostics.Debug.WriteLine("period: " + p_legalHoliday.Period.ToString() + "factTag_id: " + p_factTag.Id.ToString());
      }
    }

    public override EditedFactStatus SetFactValueStatus()
    {
      if (EditedLegalHoliday.Tag == LegalHolidayTag.FER || ModelLegalHoliday.Tag == LegalHolidayTag.FER)
        return SetLegalHolidayDifferenceStatus();
      //return RaiseStatusEvent(SetLegalHolidayDifferenceStatus());

      if (EditedFactTag.Tag != FactTag.TagType.NONE && EditedFactTag.Tag == ModelFactTag.Tag)
        return SetFactTagDifferenceStatus();
      //return RaiseStatusEvent(SetFactTagDifferenceStatus());

      if (EditedClientId != ClientId && EditedFactTag.Tag == ModelFactTag.Tag)
        return SetClientDifferenceStatus();
      //return RaiseStatusEvent(SetClientDifferenceStatus());

      return SetFactTagDifferenceStatus();
      //return RaiseStatusEvent(SetFactTagDifferenceStatus());
    }

    private EditedFactStatus SetClientDifferenceStatus()
    {
      if (EditedClientId != this.ClientId)
        return EditedFactStatus.InputDifferent;
      else
        return EditedFactStatus.InputEqual;
    }

    private EditedFactStatus SetFactTagDifferenceStatus()
    {
      if (EditedFactTag.Tag != ModelFactTag.Tag)
        return EditedFactStatus.FactTagDifferent;
      else
        return EditedFactStatus.FactTagEqual;
    }

    private EditedFactStatus SetLegalHolidayDifferenceStatus()
    {
      if (EditedLegalHoliday.Tag == ModelLegalHoliday.Tag)
        return EditedFactStatus.LegalHolidayEqual;
      else
        return EditedFactStatus.LegalHolidayDifferent;
    }
  
  }
}
