Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
Imports CRUD

Public MustInherit Class CRUDManager

#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean

    ' Events
    Public Event ObjectInitialized()
    Public Event Read(ByRef status As ErrorMessage, ByRef attributes As CRUDEntity)
    Public Event CreationEvent(ByRef status As ErrorMessage, ByRef id As UInt32)
    Public Event UpdateEvent(ByRef status As ErrorMessage, ByRef id As UInt32)
    Public Event DeleteEvent(ByRef status As ErrorMessage, ByRef id As UInt32)
    Public Event UpdateListEvent(ByRef status As ErrorMessage, ByRef updateResults As Dictionary(Of UInt32, Boolean))

    Protected CreateSMSG As ServerMessage
    Protected ReadSMSG As ServerMessage
    Protected UpdateSMSG As ServerMessage
    Protected UpdateListSMSG As ServerMessage
    Protected DeleteSMSG As ServerMessage
    Protected ListSMSG As ServerMessage

    Protected CreateCMSG As ClientMessage
    Protected ReadCMSG As ClientMessage
    Protected UpdateCMSG As ClientMessage
    Protected UpdateListCMSG As ClientMessage
    Protected DeleteCMSG As ClientMessage
    Protected ListCMSG As ClientMessage

    Protected Delegate Function BuildDelegate(p_packet As ByteBuffer) As CRUDEntity
    Protected Build As BuildDelegate

#End Region

#Region "Init"

    Friend Sub New()

        state_flag = False

    End Sub

    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ReadSMSG, AddressOf ReadAnswer)
        NetworkManager.GetInstance().RemoveCallback(DeleteSMSG, AddressOf DeleteAnswer)
        NetworkManager.GetInstance().RemoveCallback(ListSMSG, AddressOf ListAnswer)
        NetworkManager.GetInstance().RemoveCallback(UpdateSMSG, AddressOf UpdateAnswer)
        NetworkManager.GetInstance().RemoveCallback(UpdateListSMSG, AddressOf UpdateListAnswer)
        NetworkManager.GetInstance().RemoveCallback(CreateSMSG, AddressOf CreateAnswer)
        MyBase.Finalize()

    End Sub

    Protected Sub InitCallbacks()
        NetworkManager.GetInstance().SetCallback(ReadSMSG, AddressOf ReadAnswer)
        NetworkManager.GetInstance().SetCallback(DeleteSMSG, AddressOf DeleteAnswer)
        NetworkManager.GetInstance().SetCallback(ListSMSG, AddressOf ListAnswer)
        NetworkManager.GetInstance().SetCallback(UpdateSMSG, AddressOf UpdateAnswer)
        NetworkManager.GetInstance().SetCallback(UpdateListSMSG, AddressOf UpdateListAnswer)
        NetworkManager.GetInstance().SetCallback(CreateSMSG, AddressOf CreateAnswer)
    End Sub

#End Region

#Region "CRUD"

    Protected MustOverride Sub ListAnswer(packet As ByteBuffer)
    Protected MustOverride Sub ReadAnswer(packet As ByteBuffer)
    Protected MustOverride Sub DeleteAnswer(packet As ByteBuffer)

    Protected Sub UpdateAnswer(packet As ByteBuffer)
        RaiseEvent UpdateEvent(packet.GetError(), packet.ReadUint32())
    End Sub

    Protected Sub UpdateListAnswer(packet As ByteBuffer)
        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim resultList As New Dictionary(Of UInt32, Boolean)
            Dim nbResult As Int32 = packet.ReadInt32()

            For i As Int32 = 1 To nbResult
                Dim id As UInt32 = packet.ReadUint32()
                If (resultList.ContainsKey(id)) Then
                    resultList(id) = packet.ReadBool()
                Else
                    resultList.Add(id, packet.ReadBool)
                End If
            Next

            RaiseEvent UpdateListEvent(True, resultList)
        Else
            RaiseEvent UpdateListEvent(False, Nothing)
        End If
    End Sub

    Protected Sub CreateAnswer(packet As ByteBuffer)
        RaiseEvent CreationEvent(packet.GetError(), packet.ReadUint32())
    End Sub

    Friend Overridable Sub List()
        Dim packet As New ByteBuffer(CType(ListCMSG, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Friend Overridable Sub Create(ByRef p_crud As CRUDEntity)

        Dim packet As New ByteBuffer(CType(CreateCMSG, UShort))
        p_crud.Dump(packet, False)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Overridable Sub Update(ByRef p_crud As CRUDEntity)

        Dim packet As New ByteBuffer(CType(UpdateCMSG, UShort))
        p_crud.Dump(packet, True)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Overridable Sub UpdateList(ByRef p_crudList As List(Of CRUDEntity))

        Dim packet As New ByteBuffer(CType(UpdateListCMSG, UShort))
        packet.WriteUint32(p_crudList.Count)

        For Each l_crud In p_crudList
            packet.WriteUint8(CRUDAction.UPDATE)
            l_crud.Dump(packet, True)
        Next
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Overridable Sub Delete(ByRef p_id As UInt32)

        Dim packet As New ByteBuffer(CType(DeleteCMSG, UShort))
        packet.WriteUint32(p_id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

#End Region

#Region "Mappings"

    Public MustOverride Function GetValue(ByVal p_id As UInt32) As CRUDEntity
    Public MustOverride Function GetValue(ByVal p_id As Int32) As CRUDEntity

#End Region

#Region "Events"

    Protected Sub RaiseObjectInitializedEvent()
        RaiseEvent ObjectInitialized()
    End Sub

    Protected Sub RaiseReadEvent(ByRef status As ErrorMessage, ByRef attributes As CRUDEntity)
        RaiseEvent Read(status, attributes)
    End Sub

    Protected Sub RaiseCreationEvent(ByRef status As ErrorMessage, ByRef id As UInt32)
        RaiseEvent CreationEvent(status, id)
    End Sub

    Protected Sub RaiseUpdateEvent(ByRef status As ErrorMessage, ByRef id As UInt32)
        RaiseEvent UpdateEvent(status, id)
    End Sub

    Protected Sub RaiseUpdateListEvent(ByRef status As ErrorMessage, ByRef updateResults As Dictionary(Of UInt32, Boolean))
        RaiseEvent UpdateListEvent(status, updateResults)
    End Sub

    Protected Sub RaiseDeleteEvent(ByRef status As ErrorMessage, ByRef id As UInt32)
        RaiseEvent DeleteEvent(status, id)
    End Sub

#End Region

End Class
