Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections

' clientsControl.vb
'
' We can have only an analysisaxiscontroller
'
'
'
' Author: Julien Monnereau
' Last modified: 22/07/2015


Friend Class ClientsController


#Region "Instance Variables"

    ' Objects
    Private view As clientsControl
    Private clients As client
    Private clientsFiltersTV As New TreeView
    Private PlatformMGTUI As PlatformMGTGeneralUI

    ' Variable
    Private clients_list As List(Of UInt32)
    Friend categoriesNameKeyDic As Hashtable


#End Region


#Region "Initialize"

    Friend Sub New()

        clients = New Client
        ' ->  Analysis Axis Filter controller (generic) 
        ' to be implemented after entities_filters, clients_filters, products_filters
        ' A ce niveau le CategoryTV est utilisé pour itérer à travers les variables des axis 
        ' priority normal


        '     AnalysisAxisCategory.LoadCategoryCodeTV(clientsFiltersTV, GlobalEnums.AnalysisAxis.CLIENTS)
        clients_list = GlobalVariables.Clients.clients_hash.Keys
        categoriesNameKeyDic = GlobalVariables.Filters.GetFiltersDictionary(GlobalEnums.AnalysisAxis.CLIENTS, _
                                                                            NAME_VARIABLE, _
                                                                            ID_VARIABLE)
        view = New ClientsControl(Me, _
                                   clientsFiltersTV, _
                                   clients.clients_hash, _
                                   categoriesNameKeyDic)

        AddHandler GlobalVariables.Clients.ClientCreationEvent, AddressOf AfterClientCreation
        AddHandler GlobalVariables.Clients.ClientUpdateEvent, AddressOf AfterClientUpdate
        AddHandler GlobalVariables.Clients.ClientDeleteEvent, AddressOf AfterClientDelete

    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.PlatformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(view)
        view.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Public Sub close()

        view.closeControl()
        view.Dispose()
        PlatformMGTUI.displayControl()

    End Sub

  
#End Region


#Region "Interface"

    Friend Sub createclient(ByRef hash As Hashtable)

        If GlobalVariables.Clients.IsNameValid(hash(NAME_VARIABLE)) = True Then
            GlobalVariables.Clients.CMSG_CREATE_CLIENT(hash)

            view.addclientRow(hash(ID_VARIABLE), hash)
        Else
            MsgBox("Invalid Name. Names must be unique and not empty.")
        End If

    End Sub

    Private Sub AfterClientCreation(ByRef ht As Hashtable)



    End Sub

    Friend Function updateclientName(ByRef item_id As String, _
                                      ByRef value As String) As Boolean

        If clients.IsNameValid(value) = True Then
            GlobalVariables.Clients.CMSG_UPDATE_CLIENT(item_id, NAME_VARIABLE, value)
            Return True
        Else
            MsgBox("Invalid Name. Names must be unique and not empty.")
            Return False
        End If

    End Function

    Private Sub AfterClientUpdate(ByRef ht As Hashtable)


        ' to be implemented priority normal !!


    End Sub

    Friend Sub updateclientCategory(ByRef item_id As String, _
                                               ByRef category_id As String, _
                                               ByRef value As String)

        GlobalVariables.Clients.CMSG_UPDATE_CLIENT(item_id, category_id, value)

    End Sub

    Friend Sub deleteclient(ByRef client_id As String)

        GlobalVariables.Clients.CMSG_DELETE_CLIENT(client_id)

    End Sub

    Private Sub AfterClientDelete(ByRef id As UInt32)

        ' to be implemented priority normal !!

    End Sub


#End Region

End Class
