using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace FBI.MVC.Model
{
  using CRUD;
  using Network;

  abstract public class ICRUDModel<T> where T : class, CRUDEntity
  {

	#region "Instance variables"

	// Variables

	public bool IsInit { get; protected set; }
	// Events
	public event ObjectInitializedEventHandler ObjectInitialized;
  public delegate void ObjectInitializedEventHandler(ErrorMessage p_status, Type p_type);
	public event ReadEventHandler ReadEvent;
	public delegate void ReadEventHandler(ErrorMessage status, T attributes);
	public event CreationEventHandler CreationEvent;
	public delegate void CreationEventHandler(ErrorMessage status, UInt32 id);
	public event UpdateEventHandler UpdateEvent;
	public delegate void UpdateEventHandler(ErrorMessage status, UInt32 id);
	public event DeleteEventHandler DeleteEvent;
	public delegate void DeleteEventHandler(ErrorMessage status, UInt32 id);
	public event UpdateListEventHandler UpdateListEvent;
  public delegate void UpdateListEventHandler(ErrorMessage status, SafeDictionary<CRUDAction, SafeDictionary<UInt32, ErrorMessage>> updateResults);

	protected ServerMessage CreateSMSG;
	protected ServerMessage ReadSMSG;
	protected ServerMessage UpdateSMSG;
	protected ServerMessage UpdateListSMSG;
	protected ServerMessage DeleteSMSG;

	protected ServerMessage ListSMSG;
	protected ClientMessage CreateCMSG;
	protected ClientMessage ReadCMSG;
	protected ClientMessage UpdateCMSG;
	protected ClientMessage UpdateListCMSG;
	protected ClientMessage DeleteCMSG;

	protected ClientMessage ListCMSG;
	protected delegate CRUDEntity BuildDelegate(ByteBuffer p_packet);

	protected BuildDelegate Build;

	protected ICRUDModel()
	{
		IsInit = false;
	}

	~ICRUDModel()
	{
		NetworkManager.RemoveCallback((UInt16)ReadSMSG, ReadAnswer_Intern);
    NetworkManager.RemoveCallback((UInt16)DeleteSMSG, DeleteAnswer_Intern);
    NetworkManager.RemoveCallback((UInt16)ListSMSG, ListAnswer_Intern);
    NetworkManager.RemoveCallback((UInt16)UpdateSMSG, UpdateAnswer_Intern);
    NetworkManager.RemoveCallback((UInt16)UpdateListSMSG, UpdateListAnswer_Intern);
    NetworkManager.RemoveCallback((UInt16)CreateSMSG, CreateAnswer_Intern);
	}

	protected void InitCallbacks()
	{
    NetworkManager.SetCallback((UInt16)ReadSMSG, ReadAnswer_Intern);
    NetworkManager.SetCallback((UInt16)DeleteSMSG, DeleteAnswer_Intern);
    NetworkManager.SetCallback((UInt16)ListSMSG, ListAnswer_Intern);
    NetworkManager.SetCallback((UInt16)UpdateSMSG, UpdateAnswer_Intern);
    NetworkManager.SetCallback((UInt16)UpdateListSMSG, UpdateListAnswer_Intern);
    NetworkManager.SetCallback((UInt16)CreateSMSG, CreateAnswer_Intern);
	}

	protected abstract void ListAnswer(ByteBuffer p_packet);
	protected abstract void ReadAnswer(ByteBuffer p_packet);
	protected abstract void DeleteAnswer(ByteBuffer p_packet);

	protected void UpdateAnswer(ByteBuffer p_packet)
	{
		if (UpdateEvent != null) {
			UpdateEvent(p_packet.GetError(), p_packet.ReadUint32());
		}
	}

  protected void UpdateListAnswer(ByteBuffer p_packet)
  {
    if (p_packet.GetError() == ErrorMessage.SUCCESS)
    {
      SafeDictionary<CRUDAction, SafeDictionary<UInt32, ErrorMessage>> l_resultList =
        new SafeDictionary<CRUDAction, SafeDictionary<UInt32, ErrorMessage>>();
      Int32 nbResult = p_packet.ReadInt32();

      for (Int32 i = 1; i <= nbResult; i++)
      {
        CRUDAction l_action = (CRUDAction)p_packet.ReadUint8();
        UInt32 id = p_packet.ReadUint32();
        bool l_success = p_packet.ReadBool();
        ErrorMessage l_error = (ErrorMessage)p_packet.ReadUint32();

        l_resultList[l_action][id] = l_error;
      }

      if (UpdateListEvent != null)
        UpdateListEvent(p_packet.GetError(), l_resultList);
    }
    else
    {
      if (UpdateListEvent != null)
        UpdateListEvent(p_packet.GetError(), null);
    }
  }

  protected void CreateAnswer(ByteBuffer p_packet)
  {
    if (CreationEvent != null)
      CreationEvent(p_packet.GetError(), p_packet.ReadUint32());
  }

	protected virtual bool List()
	{
		ByteBuffer packet = new ByteBuffer(Convert.ToUInt16(ListCMSG));
		packet.Release();
		return NetworkManager.Send(packet);
	}


  public virtual bool Create(T p_crud)
	{
		ByteBuffer packet = new ByteBuffer(Convert.ToUInt16(CreateCMSG));
		p_crud.Dump(packet, false);
		packet.Release();
		return NetworkManager.Send(packet);
	}


  public virtual bool Update(T p_crud)
	{
		ByteBuffer packet = new ByteBuffer(Convert.ToUInt16(UpdateCMSG));
		p_crud.Dump(packet, true);
		packet.Release();
		return NetworkManager.Send(packet);
	}


  public virtual bool UpdateList(List<T> p_crudList, CRUDAction p_action)
	{
		ByteBuffer packet = new ByteBuffer(Convert.ToUInt16(UpdateListCMSG));

		packet.WriteInt32(p_crudList.Count);
		foreach (T l_crud in p_crudList)
    {
			packet.WriteUint8((byte)p_action);
			l_crud.Dump(packet, p_action != CRUDAction.CREATE);
		}
		packet.Release();
		return NetworkManager.Send(packet);
	}

  public virtual bool Delete(UInt32 p_id)
	{
		ByteBuffer packet = new ByteBuffer(Convert.ToUInt16(DeleteCMSG));
		packet.WriteUint32(p_id);
		packet.Release();
		return NetworkManager.Send(packet);
	}
	#endregion

	#region "Intern"

  private void ReadAnswer_Intern(ByteBuffer p_packet)
  {
    try
    {
      ReadAnswer(p_packet);
    }
    catch (Exception ex)
    {
      Debug.WriteLine("ReadAnswer: " + ex.Message);
      if ((ex.InnerException != null))
        Debug.WriteLine("ReadAnswer: " + ex.InnerException.Message);
    }
  }

  private void UpdateAnswer_Intern(ByteBuffer p_packet)
  {
    try
    {
      UpdateAnswer(p_packet);
    }
    catch (Exception ex)
    {
      Debug.WriteLine("UpdateAnswer: " + ex.Message);
      if ((ex.InnerException != null))
        Debug.WriteLine("UpdateAnswer: " + ex.InnerException.Message);
    }
  }

  private void UpdateListAnswer_Intern(ByteBuffer p_packet)
  {
    try
    {
      UpdateListAnswer(p_packet);
    }
    catch (Exception ex)
    {
      Debug.WriteLine("UpdateListAnswer: " + ex.Message);
      if ((ex.InnerException != null))
        Debug.WriteLine("UpdateListAnswer: " + ex.InnerException.Message);
    }
  }

  private void CreateAnswer_Intern(ByteBuffer p_packet)
  {
    try
    {
      CreateAnswer(p_packet);
    }
    catch (Exception ex)
    {
      Debug.WriteLine("CreateAnswer: " + ex.Message);
      if ((ex.InnerException != null))
        Debug.WriteLine("CreateAnswer: " + ex.InnerException.Message);
    }
  }

  private void DeleteAnswer_Intern(ByteBuffer p_packet)
  {
    try
    {
      DeleteAnswer(p_packet);
    }
    catch (Exception ex)
    {
      Debug.WriteLine("DeleteAnswer: " + ex.Message);
      if ((ex.InnerException != null))
        Debug.WriteLine("DeleteAnswer: " + ex.InnerException.Message);
    }
  }

  private void ListAnswer_Intern(ByteBuffer p_packet)
  {
    try
    {
      ListAnswer(p_packet);
    }
    catch (Exception ex)
    {
      Debug.WriteLine("ListAnswer: " + ex.Message);
      if ((ex.InnerException != null))
        Debug.WriteLine("ListAnswer: " + ex.InnerException.Message);
    }
  }

	#endregion

	#region "Mappings"

	public abstract T GetValue(UInt32 p_id);

	#endregion

	#region "Events"

  protected void RaiseObjectInitializedEvent(ErrorMessage p_status, Type p_type)
  {
    if (ObjectInitialized != null)
      ObjectInitialized(p_status, p_type);
  }

  protected void RaiseReadEvent(ErrorMessage p_status, T p_attributes)
  {
    if (ReadEvent != null)
      ReadEvent(p_status, p_attributes);
  }

	protected void RaiseCreationEvent(ErrorMessage p_status, UInt32 p_id)
	{
		if (CreationEvent != null)
			CreationEvent(p_status, p_id);
	}

	protected void RaiseUpdateEvent(ErrorMessage p_status, UInt32 p_id)
	{
		if (UpdateEvent != null)
			UpdateEvent(p_status, p_id);
	}

  protected void RaiseUpdateListEvent(ErrorMessage p_status, SafeDictionary<CRUDAction, SafeDictionary<UInt32, ErrorMessage>> p_updateResults)
	{
		if (UpdateListEvent != null)
			UpdateListEvent(p_status, p_updateResults);
	}

	protected void RaiseDeleteEvent(ErrorMessage p_status, UInt32 p_id)
	{
		if (DeleteEvent != null)
			DeleteEvent(p_status, p_id);
	}

	#endregion

  }
}