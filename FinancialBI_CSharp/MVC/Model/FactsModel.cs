﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using CRUD;

  class FactsModel
  {
    static FactsModel s_instance = new FactsModel();
    public static FactsModel Instance { get { return (s_instance); } }
    public event UpdateEventHandler UpdateEvent;
    public delegate void UpdateEventHandler(ErrorMessage p_status, Dictionary<string, ErrorMessage> p_resultsDict);
    public event DeleteEventHandler DeleteEvent;
    public delegate void DeleteEventHandler(ErrorMessage p_status, Int32 p_requestId);
    public event ReadEventHandler ReadEvent;
    public delegate void ReadEventHandler(ErrorMessage p_status, Int32 p_requestId, List<Fact> p_fact_list);
    private SafeDictionary<Int32, List<string>> m_requestIdCommitDic;

    FactsModel()
    {
      NetworkManager.GetInstance().SetCallback((UInt16)ServerMessage.SMSG_UPDATE_FACT_LIST_ANSWER, UpdateListAnswer);
      NetworkManager.GetInstance().SetCallback((UInt16)ServerMessage.SMSG_DELETE_FACT_ANSWER, DeleteFactAnswer);
    }

    ~FactsModel()
    {
      NetworkManager.GetInstance().RemoveCallback((UInt16)ServerMessage.SMSG_UPDATE_FACT_LIST_ANSWER, UpdateListAnswer);
      NetworkManager.GetInstance().RemoveCallback((UInt16)ServerMessage.SMSG_DELETE_FACT_ANSWER, DeleteFactAnswer);
    }

    Int32 GetFact(UInt32 p_accountId, UInt32 p_entityId, UInt32 p_employeeId, UInt32 p_versionId, UInt32 p_startPeriod, UInt32 p_endPeriod)
    {

      NetworkManager.GetInstance().SetCallback((UInt16)ServerMessage.SMSG_GET_FACT_ANSWER, GetFactAnswer);
      ByteBuffer packet = new ByteBuffer((UInt16)ClientMessage.CMSG_GET_FACT);
      Int32 requestId = packet.AssignRequestId();

      packet.WriteUint32(p_accountId);
      packet.WriteUint32(p_employeeId);
      packet.WriteUint32(p_versionId);
      packet.WriteUint32(p_startPeriod);
      packet.WriteUint32(p_endPeriod);
      packet.WriteUint32(p_entityId);

      packet.Release();
      NetworkManager.GetInstance().Send(packet);
      return requestId;
    }

    void UpdateList(List<Fact> factsValues, List<string> cellsAddresses)
    {
      ByteBuffer packet = new ByteBuffer(Convert.ToUInt16(ClientMessage.CMSG_UPDATE_FACT_LIST));

      packet.AssignRequestId();
      packet.WriteInt32(factsValues.Count);
      foreach (Fact fact_value in factsValues)
        fact_value.Dump(packet, false);
      packet.Release();
      NetworkManager.GetInstance().Send(packet);
    }

    Int32 Delete(Fact p_fact)
    {

      ByteBuffer l_packet = new ByteBuffer(Convert.ToUInt16(ClientMessage.CMSG_DELETE_FACT));
      Int32 l_requestId = l_packet.AssignRequestId();
      l_packet.WriteUint32(p_fact.Id);
      l_packet.Release();
      NetworkManager.GetInstance().Send(l_packet);
      return l_requestId;

    }

    private void UpdateListAnswer(ByteBuffer packet)
    {
      if (packet.GetError() == ErrorMessage.SUCCESS)
      {
        Int32 requestId = packet.GetRequestId();
        SafeDictionary<string, ErrorMessage> resultsDict = new SafeDictionary<string, ErrorMessage>();
        packet.ReadUint32();
        if (m_requestIdCommitDic.ContainsKey(requestId))
        {
          foreach (string cell_address in m_requestIdCommitDic[requestId])
          {
            packet.ReadUint32();
            resultsDict.Add(cell_address, (ErrorMessage)packet.ReadUint8());
          }
          m_requestIdCommitDic.Remove(requestId);
        }
        else
          System.Diagnostics.Debug.WriteLine("FACTS UDPATE LIST request id not in dictionary");
        if (UpdateEvent != null)
          UpdateEvent(packet.GetError(), resultsDict);
      }
      else
        if (UpdateEvent != null)
          UpdateEvent(packet.GetError(), null);
    }

    private void GetFactAnswer(ByteBuffer p_packet)
    {
      List<Fact> l_factList = new List<Fact>();
      if (p_packet.GetError() == ErrorMessage.SUCCESS)
      {
        Int32 l_requestId = p_packet.GetRequestId();
        UInt32 nbResult = p_packet.ReadUint32();
        for (UInt32 i = 1; i <= nbResult; i++)
        {
          Fact hl_fact = Fact.BuildFact(p_packet);
          l_factList.Add(hl_fact);
        }
        if (ReadEvent != null)
          ReadEvent(p_packet.GetError(), l_requestId, l_factList);
      }
      else
        if (ReadEvent != null)
          ReadEvent(p_packet.GetError(), 0, null);
    }

    private void DeleteFactAnswer(ByteBuffer packet)
    {
      if (packet.GetError() == ErrorMessage.SUCCESS)
      {
        Int32 l_requestId = packet.GetRequestId();
        if (DeleteEvent != null)
          DeleteEvent(packet.GetError(), l_requestId);
      }
      else
        if (DeleteEvent != null)
          DeleteEvent(packet.GetError(), 0);
    }
  }
}
