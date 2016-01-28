using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace FBI.MVC.Model
{
  using CRUD;

  abstract class ICRUDModel<T> where T : class, CRUDEntity
  {

	#region "Instance variables"

	// Variables

	public bool IsInit { get; protected set; }
	// Events
	public event ObjectInitializedEventHandler ObjectInitialized;
	public delegate void ObjectInitializedEventHandler();
	public event ReadEventHandler ReadEvent;
	public delegate void ReadEventHandler(ErrorMessage status, T attributes);
	public event CreationEventHandler CreationEvent;
	public delegate void CreationEventHandler(ErrorMessage status, UInt32 id);
	public event UpdateEventHandler UpdateEvent;
	public delegate void UpdateEventHandler(ErrorMessage status, UInt32 id);
	public event DeleteEventHandler DeleteEvent;
	public delegate void DeleteEventHandler(ErrorMessage status, UInt32 id);
	public event UpdateListEventHandler UpdateListEvent;
	public delegate void UpdateListEventHandler(ErrorMessage status, Dictionary<UInt32, bool> updateResults);

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
		NetworkManager.GetInstance().RemoveCallback((UInt16)ReadSMSG, ReadAnswer_Intern);
    NetworkManager.GetInstance().RemoveCallback((UInt16)DeleteSMSG, DeleteAnswer_Intern);
    NetworkManager.GetInstance().RemoveCallback((UInt16)ListSMSG, ListAnswer_Intern);
    NetworkManager.GetInstance().RemoveCallback((UInt16)UpdateSMSG, UpdateAnswer_Intern);
    NetworkManager.GetInstance().RemoveCallback((UInt16)UpdateListSMSG, UpdateListAnswer_Intern);
    NetworkManager.GetInstance().RemoveCallback((UInt16)CreateSMSG, CreateAnswer_Intern);
	}

	protected void InitCallbacks()
	{
    NetworkManager.GetInstance().SetCallback((UInt16)ReadSMSG, ReadAnswer_Intern);
    NetworkManager.GetInstance().SetCallback((UInt16)DeleteSMSG, DeleteAnswer_Intern);
    NetworkManager.GetInstance().SetCallback((UInt16)ListSMSG, ListAnswer_Intern);
    NetworkManager.GetInstance().SetCallback((UInt16)UpdateSMSG, UpdateAnswer_Intern);
    NetworkManager.GetInstance().SetCallback((UInt16)UpdateListSMSG, UpdateListAnswer_Intern);
    NetworkManager.GetInstance().SetCallback((UInt16)CreateSMSG, CreateAnswer_Intern);
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
      SafeDictionary<UInt32, bool> resultList = new SafeDictionary<UInt32, bool>();
      Int32 nbResult = p_packet.ReadInt32();

      for (Int32 i = 1; i <= nbResult; i++)
      {
        UInt32 id = p_packet.ReadUint32();
        if ((resultList.ContainsKey(id)))
          resultList[id] = p_packet.ReadBool();
        else
          resultList.Add(id, p_packet.ReadBool());
      }

      if (UpdateListEvent != null)
        UpdateListEvent(p_packet.GetError(), resultList);
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

	protected virtual void List()
	{
		ByteBuffer packet = new ByteBuffer(Convert.ToUInt16(ListCMSG));
		packet.Release();
		NetworkManager.GetInstance().Send(packet);
	}


	protected virtual void Create(T p_crud)
	{
		ByteBuffer packet = new ByteBuffer(Convert.ToUInt16(CreateCMSG));
		p_crud.Dump(packet, false);
		packet.Release();
		NetworkManager.GetInstance().Send(packet);

	}


	protected virtual void Update(T p_crud)
	{
		ByteBuffer packet = new ByteBuffer(Convert.ToUInt16(UpdateCMSG));
		p_crud.Dump(packet, true);
		packet.Release();
		NetworkManager.GetInstance().Send(packet);

	}


	protected virtual void UpdateList(List<T> p_crudList)
	{
		ByteBuffer packet = new ByteBuffer(Convert.ToUInt16(UpdateListCMSG));

		packet.WriteInt32(p_crudList.Count);
		foreach (T l_crud in p_crudList)
    {
			packet.WriteUint8((byte)CRUDAction.UPDATE);
			l_crud.Dump(packet, true);
		}
		packet.Release();
		NetworkManager.GetInstance().Send(packet);
	}

	protected virtual void Delete(UInt32 p_id)
	{
		ByteBuffer packet = new ByteBuffer(Convert.ToUInt16(DeleteCMSG));
		packet.WriteUint32(p_id);
		packet.Release();
		NetworkManager.GetInstance().Send(packet);

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

  protected void RaiseObjectInitializedEvent()
  {
    if (ObjectInitialized != null)
      ObjectInitialized();
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

	protected void RaiseUpdateListEvent(ErrorMessage p_status, Dictionary<UInt32, bool> p_updateResults)
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