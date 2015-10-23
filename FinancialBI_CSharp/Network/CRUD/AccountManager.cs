using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
class AccountManager
{

  //  Variables
  internal bool state_flag;

  private MultiIndexDictionary<UInt32, string, Account> m_accountList = new MultiIndexDictionary<UInt32, string, Account>();

  public event EventHandler ObjectInitialized;

  public event EventHandler Read;

  private bool status;

  private Account attributes;

  public event EventHandler CreationEvent;

  private bool status;

  private Int32 id;

  public event EventHandler UpdateEvent;

  private ErrorMessage status;

  private Int32 id;

  public event EventHandler DeleteEvent;

  private bool status;

  private UInt32 id;

  public event EventHandler UpdateListEvent;

  private bool status;

  private List<Tuple<byte, bool, string>> updateResults;

  internal AccountManager()
  {
    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_ACCOUNT_ANSWER, &SMSG_READ_ACCOUNT_ANSWER));
    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_ACCOUNT_ANSWER, new System.EventHandler(this.SMSG_DELETE_ACCOUNT_ANSWER));
    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_ACCOUNT_ANSWER, new System.EventHandler(this.SMSG_LIST_ACCOUNT_ANSWER));
    state_flag = false;
  }

  internal void CMSG_LIST_ACCOUNT()
  {
    ByteBuffer packet = new ByteBuffer(((UShort)(ClientMessage.CMSG_LIST_ACCOUNT)));
    packet.Release();
    NetworkManager.GetInstance().Send(packet);
  }

  private void SMSG_LIST_ACCOUNT_ANSWER(ByteBuffer packet)
  {
    if ((packet.GetError() == 0))
    {
      object nb_accounts = packet.ReadInt32();
      for (Int32 i = 1; (i <= nb_accounts); i++)
      {
        object tmp_account = Account.BuildAccount(packet);
        m_accountList.Set(tmp_account.Id, tmp_account.Name, tmp_account);
      }

      state_flag = true;
      ObjectInitialized();
    }
    else
    {
      state_flag = false;
    }

  }

  internal void CMSG_CREATE_ACCOUNT(ref Account p_account)
  {
    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_ACCOUNT_ANSWER, new System.EventHandler(this.SMSG_CREATE_ACCOUNT_ANSWER));
    ByteBuffer packet = new ByteBuffer(((UShort)(ClientMessage.CMSG_CREATE_ACCOUNT)));
    p_account.Dump(packet, false);
    packet.Release();
    NetworkManager.GetInstance().Send(packet);
  }

  private void SMSG_CREATE_ACCOUNT_ANSWER(ByteBuffer packet)
  {
    if ((packet.GetError() == 0))
    {
      CreationEvent(true, int.Parse(packet.ReadUint32()));
    }
    else
    {
      CreationEvent(false, null);
    }

    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_ACCOUNT_ANSWER, new System.EventHandler(this.SMSG_CREATE_ACCOUNT_ANSWER));
  }

  private void SMSG_READ_ACCOUNT_ANSWER(ByteBuffer packet)
  {
    if ((packet.GetError() == 0))
    {
      object l_account = Account.BuildAccount(packet);
      m_accountList.Set(l_account.Id, l_account.Name, l_account);
      Read(true, l_account);
    }
    else
    {
      Read(false, null);
    }

  }

  internal void CMSG_UPDATE_ACCOUNT(ref Account p_account)
  {
    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ACCOUNT_ANSWER, new System.EventHandler(this.SMSG_UPDATE_ACCOUNT_ANSWER));
    ByteBuffer packet = new ByteBuffer(((UShort)(ClientMessage.CMSG_UPDATE_ACCOUNT)));
    p_account.Dump(packet, true);
    packet.Release();
    NetworkManager.GetInstance().Send(packet);
  }

  private void SMSG_UPDATE_ACCOUNT_ANSWER(ByteBuffer packet)
  {
    if ((packet.GetError() == ErrorMessage.SUCCESS))
    {
      UpdateEvent(packet.GetError(), int.Parse(packet.ReadUint32()));
    }
    else
    {
      UpdateEvent(packet.GetError(), null);
    }

    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_ACCOUNT_ANSWER, new System.EventHandler(this.SMSG_UPDATE_ACCOUNT_ANSWER));
  }

  internal void CMSG_CRUD_ACCOUNT_LIST(ref Dictionary<Int32, CRUDAction> p_operations) {
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ACCOUNT_LIST_ANSWER, new System.EventHandler(this.SMSG_UPDATE_ACCOUNT_LIST_ANSWER));
        ByteBuffer packet = new ByteBuffer(((UShort)(ClientMessage.CMSG_UPDATE_ACCOUNT_LIST)));
        packet.WriteUint32(p_operations.Count);
        foreach (op in p_operations) {
            object l_account = this.GetAccount(op.Key);
            if ((l_account == null)) {
                // TODO: Continue For... Warning!!! not translated
            }
            
            packet.WriteUint8(op.Value);
            if ((op.Value == CRUDAction.DELETE)) {
                packet.WriteInt32(op.Key);
            }
            else {
                l_account.Dump(packet, true);
            }
            
        }
        
        packet.Release();
        NetworkManager.GetInstance().Send(packet);
    }

  internal void SMSG_UPDATE_ACCOUNT_LIST_ANSWER(ByteBuffer packet)
  {
    if ((packet.GetError() == 0))
    {
      List<Tuple<byte, bool, string>> updatesStatus = new List<Tuple<byte, bool, string>>();
      for (Int32 i = 0; (i <= packet.ReadUint32()); i++)
      {
        CRUDAction action = packet.ReadUint8();
        if (((action == CRUDAction.DELETE)
                    || (action == CRUDAction.UPDATE)))
        {
          packet.ReadInt32();
        }

        //  ignore id
        updatesStatus.Add(new Tuple<byte, bool, string>(action, packet.ReadBool(), packet.ReadString()));
      }

      UpdateListEvent(true, updatesStatus);
    }
    else
    {
      UpdateListEvent(false, null);
    }

    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_ACCOUNT_LIST_ANSWER, new System.EventHandler(this.SMSG_UPDATE_ACCOUNT_LIST_ANSWER));
  }

  internal void CMSG_DELETE_ACCOUNT(ref UInt32 id)
  {
    ByteBuffer packet = new ByteBuffer(((UShort)(ClientMessage.CMSG_DELETE_ACCOUNT)));
    packet.WriteUint32(id);
    packet.Release();
    NetworkManager.GetInstance().Send(packet);
  }

  private void SMSG_DELETE_ACCOUNT_ANSWER(ByteBuffer packet)
  {
    if ((packet.GetError() == 0))
    {
      UInt32 id = packet.ReadInt32;
      m_accountList.Remove(id);
      DeleteEvent(true, id);
    }
    else
    {
      DeleteEvent(false, 0);
    }

  }

  public Account GetAccount(UInt32 p_id)
  {
    return m_accountList[p_id];
  }

  public Account GetAccount(Int32 p_id)
  {
    return m_accountList[System.UInt32.Parse(p_id)];
  }

  public Account GetAccount(string p_name)
  {
    return m_accountList[p_name];
  }

  internal List<Account> GetAccountsList(ref string LookupOption) {
        List<Account> tmp_list = new List<Account>();
        List<Account.FormulaTypes> selection = new List<Account.FormulaTypes>();
        switch (LookupOption) {
            case GlobalEnums.AccountsLookupOptions.LOOKUP_ALL:
                selection.Add(Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS);
                selection.Add(Account.FormulaTypes.FIRST_PERIOD_INPUT);
                selection.Add(Account.FormulaTypes.FORMULA);
                selection.Add(Account.FormulaTypes.HARD_VALUE_INPUT);
                break;
            case GlobalEnums.AccountsLookupOptions.LOOKUP_INPUTS:
                selection.Add(Account.FormulaTypes.FIRST_PERIOD_INPUT);
                selection.Add(Account.FormulaTypes.HARD_VALUE_INPUT);
                break;
            case GlobalEnums.AccountsLookupOptions.LOOKUP_OUTPUTS:
                selection.Add(Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS);
                selection.Add(Account.FormulaTypes.FIRST_PERIOD_INPUT);
                selection.Add(Account.FormulaTypes.FORMULA);
                break;
            case GlobalEnums.AccountsLookupOptions.LOOKUP_TITLES:
                selection.Add(Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS);
                selection.Add(Account.FormulaTypes.FIRST_PERIOD_INPUT);
                selection.Add(Account.FormulaTypes.FORMULA);
                break;
        }
        foreach (id in m_accountList.Keys) {
            object l_account = this.GetAccount(id);
            if ((l_account == null)) {
                // TODO: Continue For... Warning!!! not translated
            }
            
            if (selection.Contains(l_account.FormulaType)) {
                tmp_list.Add(l_account);
            }
            
        }
        
        return tmp_list;
    }

  internal MultiIndexDictionary<UInt32, string, Account> GetAccountsDictionary()
  {
    return m_accountList;
  }

  internal Int32 GetIdFromName(ref string name)
  {
    if ((m_accountList[name] == null))
    {
      return 0;
    }

    return m_accountList[name].Id;
  }

  internal void LoadAccountsTV(ref Windows.Forms.TreeView TV)
  {
    TreeViewsUtilities.LoadTreeview(TV, m_accountList);
  }

  internal void LoadAccountsTV(ref VIBlend.WinForms.Controls.vTreeView TV)
  {
    VTreeViewUtil.LoadTreeview(TV, m_accountList);
  }

  protected override void finalize()
  {
    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_ACCOUNT_ANSWER, new System.EventHandler(this.SMSG_READ_ACCOUNT_ANSWER));
    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_ACCOUNT_ANSWER, new System.EventHandler(this.SMSG_DELETE_ACCOUNT_ANSWER));
    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_ACCOUNT_ANSWER, new System.EventHandler(this.SMSG_LIST_ACCOUNT_ANSWER));
    base.Finalize();
  }
}