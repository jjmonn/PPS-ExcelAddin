using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace CRUD
{

  public class Account : NamedHierarchyCRUDEntity, IComparable
  {

    #region Enums

    public enum AccountProcess
    {
      FINANCIAL = 0x01,
      RH        = 0x02
    }

    public enum FormulaTypes
    {
      HARD_VALUE_INPUT = 1,
      FORMULA,
      AGGREGATION_OF_SUB_ACCOUNTS,
      FIRST_PERIOD_INPUT,
      TITLE
    }

    public enum ConversionOptions
    {
      AVERAGE_RATE = 1,
      END_OF_PERIOD_RATE,
      NO_CONVERSION
    }

    public enum ConsolidationOptions
    {
      AGGREGATION = 1,
      RECOMPUTATION,
      NONE
    }

    public enum AccountType
    {
      MONETARY = 1,
      NUMBER,
      PERCENTAGE,
      DATE_
    }

    #endregion

    public UInt32 Id { get; private set; }
    public UInt32 ParentId { get; set; }
    public string Name { get; set; }
    public FormulaTypes FormulaType { get; set; }
    public string Formula { get; set; }
    public AccountType Type { get; set; }
    public ConsolidationOptions ConsolidationOptionId { get; set; }
    public ConversionOptions ConversionOptionId { get; set; }
    public string FormatId { get; set; }
    public UInt32 Image { get; set; }
    public Int32 ItemPosition { get; set; }
    public Int32 AccountTab { get; set; }
    public string Description { get; set; }
    public AccountProcess Process { get; set; }

    public Account() { Process = AccountProcess.FINANCIAL; }
    private Account(UInt32 p_id)
    {
      Id = p_id;
    }

    public static CRUDEntity BuildAccount(ByteBuffer p_packet)
    {
      Account l_account = new Account(p_packet.ReadUint32());

      l_account.ParentId = p_packet.ReadUint32();
      l_account.Name = p_packet.ReadString();
      l_account.FormulaType = (FormulaTypes)p_packet.ReadUint32();
      l_account.Formula = p_packet.ReadString();
      l_account.Type = (AccountType)p_packet.ReadUint32();
      l_account.ConsolidationOptionId = (ConsolidationOptions)p_packet.ReadUint32();
      l_account.ConversionOptionId = (ConversionOptions)p_packet.ReadInt32();
      l_account.FormatId = p_packet.ReadString();
      l_account.Image = p_packet.ReadUint32();
      l_account.ItemPosition = p_packet.ReadInt32();
      l_account.AccountTab = p_packet.ReadInt32();
      l_account.Description = p_packet.ReadString();
      l_account.Process = (AccountProcess)p_packet.ReadUint8();

        // Currently image corresponds to formula type:
      l_account.Image = (UInt32)l_account.FormulaType;

      return (l_account);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32(ParentId);
      p_packet.WriteString(Name);
      p_packet.WriteInt32((Int32)FormulaType);
      p_packet.WriteString(Formula);
      p_packet.WriteInt32((Int32)Type);
      p_packet.WriteInt32((Int32)ConsolidationOptionId);
      p_packet.WriteInt32((Int32)ConversionOptionId);
      p_packet.WriteString(FormatId);
      p_packet.WriteUint32(Image);
      p_packet.WriteInt32(ItemPosition);
      p_packet.WriteInt32(AccountTab);
      p_packet.WriteString(Description);
      p_packet.WriteUint8((byte)Process);
    }

    public void CopyFrom(Account p_model)
    {
      ParentId = p_model.ParentId;
      Name = p_model.Name;
      FormulaType = p_model.FormulaType;
      Formula = p_model.Formula;
      Type = p_model.Type;
      ConsolidationOptionId = p_model.ConsolidationOptionId;
      ConversionOptionId = p_model.ConversionOptionId;
      FormatId = p_model.FormatId;
      Image = p_model.Image;
      ItemPosition = p_model.ItemPosition;
      AccountTab = p_model.AccountTab;
      Description = p_model.Description;
      Process = p_model.Process;
    }

    public Account Clone()
    {
      Account l_clone = new Account(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      Account l_cmpAccount = p_obj as Account;

      if (l_cmpAccount == null)
        return 0;
      if (l_cmpAccount.ItemPosition > ItemPosition)
        return -1;
      else
        return 1;
    }

  }
}