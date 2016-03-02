using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using FBI.MVC.Model.CRUD;
  using Microsoft.Office.Interop.Excel;
  
  
  class EditedRHFact : EditedFactBase
  {
    public UInt32 EditedClientId {get; set;}
    public FactTag EditedFactTag;
    public FactTag ModelFactTag { get; set; }
    private string m_address;

    public EditedFactStatus EditedFactTagStatus { get; private set; }
  

    public EditedRHFact(UInt32 p_accountId, UInt32 p_entityId, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId, 
                        UInt32 p_employeeId, UInt32 p_versionId, PeriodDimension p_period, Range p_cell) 
                        : base(p_accountId, p_entityId, p_clientId, p_productId, p_adjustmentId, p_employeeId, p_versionId, 
                               p_period, p_cell)
    {
      FactTag l_factTag = new FactTag();
      l_factTag.Tag = FactTag.TagType.NONE;
      this.EditedFactTag = l_factTag;
      this.ModelFactTag = new FactTag();
      this.ModelFactTag.Tag = FactTag.TagType.NONE;
      Value = 1;
      m_address = Cell.Address;
      EditedFactTagStatus = EditedFactStatus.FactTagEqual;   
    }

    public void SetEditedClient(UInt32 p_clientId)
    {
      EditedClientId = p_clientId;
      SetCellStatusRH();
    }

    public void SetEditedFactType(FactTag.TagType p_tagType)
    {
      EditedFactTag.Tag = p_tagType;
      SetCellStatusRH();
    }

    public void UpdateRHFact(Fact p_fact, FactTag p_factTag)
    {
      UpdateFactBase(p_fact);
      EditedFactTag.Id = p_fact.Id;
      if (p_factTag != null)
        ModelFactTag.Tag = p_factTag.Tag;
     // SetCellStatusRH();
    }

    public void SetCellStatusRH()
    {
      if (EditedFactTag.Tag != FactTag.TagType.NONE && EditedFactTag.Tag == ModelFactTag.Tag)
      {
        SetFactTagDifferenceStatus();
        return;
      }
      if (EditedClientId != ClientId && EditedFactTag.Tag == ModelFactTag.Tag)  
      {
        SetClientDifferenceStatus();
        return;
      }
      SetFactTagDifferenceStatus();
    }

    private void SetClientDifferenceStatus()
    {
      if (EditedClientId != this.ClientId)
        SetFactStatus(EditedFactStatus.DifferentInput);
      else
        SetFactStatus(EditedFactStatus.InputEqual);
    }

    private void SetFactTagDifferenceStatus()
    {
      if (EditedFactTag.Tag != ModelFactTag.Tag)
        SetFactStatus(EditedFactStatus.FactTagDifferent);
      else
        SetFactStatus(EditedFactStatus.FactTagEqual);
    }

    private void SetFactTagStatus(EditedFactStatus p_editedFactTagStatus)
    {
      EditedFactTagStatus = p_editedFactTagStatus;
      RaiseEditedFactSatus(p_editedFactTagStatus);
    }


  }
}
