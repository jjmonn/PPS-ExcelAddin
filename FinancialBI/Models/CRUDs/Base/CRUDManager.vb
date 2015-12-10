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

        NetworkManager.GetInstance().RemoveCallback(ReadSMSG, AddressOf ReadAnswer_Intern)
        NetworkManager.GetInstance().RemoveCallback(DeleteSMSG, AddressOf DeleteAnswer_Intern)
        NetworkManager.GetInstance().RemoveCallback(ListSMSG, AddressOf ListAnswer_Intern)
        NetworkManager.GetInstance().RemoveCallback(UpdateSMSG, AddressOf UpdateAnswer_Intern)
        NetworkManager.GetInstance().RemoveCallback(UpdateListSMSG, AddressOf UpdateListAnswer_Intern)
        NetworkManager.GetInstance().RemoveCallback(CreateSMSG, AddressOf CreateAnswer_Intern)
        MyBase.Finalize()

    End Sub

    Protected Sub InitCallbacks()
        NetworkManager.GetInstance().SetCallback(ReadSMSG, AddressOf ReadAnswer_Intern)
        NetworkManager.GetInstance().SetCallback(DeleteSMSG, AddressOf DeleteAnswer_Intern)
        NetworkManager.GetInstance().SetCallback(ListSMSG, AddressOf ListAnswer_Intern)
        NetworkManager.GetInstance().SetCallback(UpdateSMSG, AddressOf UpdateAnswer_Intern)
        NetworkManager.GetInstance().SetCallback(UpdateListSMSG, AddressOf UpdateListAnswer_Intern)
        NetworkManager.GetInstance().SetCallback(CreateSMSG, AddressOf CreateAnswer_Intern)
    End Sub

#End Region

#Region "CRUD"

#Region "Available"
    Protected MustOverride Sub ListAnswer(packet As ByteBuffer)
    Protected MustOverride Sub ReadAnswer(packet As ByteBuffer)
    Protected MustOverride Sub DeleteAnswer(packet As ByteBuffer)

    Protected Sub UpdateAnswer(packet As ByteBuffer)
        packet.GetError()
        RaiseEvent UpdateEvent(packet.GetError(), packet.ReadUint32())
    End Sub

    Protected Sub UpdateListAnswer(packet As ByteBuffer)
        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim resultList As New SafeDictionary(Of UInt32, Boolean)
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

#Region "Intern"

    Private Sub ReadAnswer_Intern(packet As ByteBuffer)
        Try
            ReadAnswer(packet)
        Catch ex As Exception
            Diagnostics.Debug.WriteLine("ReadAnswer: " & ex.Message)
            If (ex.InnerException IsNot Nothing) Then Diagnostics.Debug.WriteLine("ReadAnswer: " & ex.InnerException.Message)
        End Try
    End Sub

    Private Sub UpdateAnswer_Intern(packet As ByteBuffer)
        Try
            UpdateAnswer(packet)
        Catch ex As Exception
            Diagnostics.Debug.WriteLine("UpdateAnswer: " & ex.Message)
            If (ex.InnerException IsNot Nothing) Then Diagnostics.Debug.WriteLine("UpdateAnswer: " & ex.InnerException.Message)
        End Try
    End Sub

    Private Sub UpdateListAnswer_Intern(packet As ByteBuffer)
        Try
            UpdateListAnswer(packet)
        Catch ex As Exception
            Diagnostics.Debug.WriteLine("UpdateListAnswer: " & ex.Message)
            If (ex.InnerException IsNot Nothing) Then Diagnostics.Debug.WriteLine("UpdateListAnswer: " & ex.InnerException.Message)
        End Try
    End Sub

    Private Sub CreateAnswer_Intern(packet As ByteBuffer)
        Try
            CreateAnswer(packet)
        Catch ex As Exception
            Diagnostics.Debug.WriteLine("CreateAnswer: " & ex.Message)
            If (ex.InnerException IsNot Nothing) Then Diagnostics.Debug.WriteLine("CreateAnswer: " & ex.InnerException.Message)
        End Try
    End Sub

    Private Sub DeleteAnswer_Intern(packet As ByteBuffer)
        Try
            DeleteAnswer(packet)
        Catch ex As Exception
            Diagnostics.Debug.WriteLine("DeleteAnswer: " & ex.Message)
            If (ex.InnerException IsNot Nothing) Then Diagnostics.Debug.WriteLine("DeleteAnswer: " & ex.InnerException.Message)
        End Try
    End Sub

    Private Sub ListAnswer_Intern(packet As ByteBuffer)
        Try
            ListAnswer(packet)
        Catch ex As Exception
            Diagnostics.Debug.WriteLine("ListAnswer: " & ex.Message)
            If (ex.InnerException IsNot Nothing) Then Diagnostics.Debug.WriteLine("ListAnswer: " & ex.InnerException.Message)
        End Try
    End Sub

#End Region

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
