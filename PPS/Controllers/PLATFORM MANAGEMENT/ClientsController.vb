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

        AddHandler GlobalVariables.Clients.CreationEvent, AddressOf AfterClientCreation
        AddHandler GlobalVariables.Clients.UpdateEvent, AddressOf AfterClientUpdate
        AddHandler GlobalVariables.Clients.DeleteEvent, AddressOf AfterClientDelete

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

    Private Sub AfterClientCreation(ByRef status As Boolean, ByRef id As Int32)



    End Sub

    Friend Function UpdateClientName(ByRef client_id As Int32, _
                                      ByRef new_name As String) As Boolean

        If clients.IsNameValid(new_name) = True Then
            Dim ht As Hashtable = GlobalVariables.Clients.clients_hash(client_id)
            ht(NAME_VARIABLE) = new_name
            GlobalVariables.Clients.CMSG_UPDATE_CLIENT(ht)
            Return True
        Else
            MsgBox("Invalid Name. Names must be unique and not empty.")
            Return False
        End If

    End Function

    Private Sub AfterClientUpdate(ByRef status As Boolean, ByRef id As Int32)


        ' to be implemented priority normal !!


    End Sub

    Friend Sub UpdateclientFilter(ByRef item_id As String, _
                                  ByRef category_id As String, _
                                  ByRef value As String)

        ' to be implemented 
        ' caution: clientsfilters table must only be updated with the most nested filter_id

        ' decide whether users can change regions or only sub filters (with upper filters updated automatically)
        ' do this in entities first

        ' 1) find the most nested

        'GlobalVariables.Clients.CMSG_UPDATE_CLIENT(item_id, category_id, value)

    End Sub

    Friend Sub deleteclient(ByRef client_id As String)

        GlobalVariables.Clients.CMSG_DELETE_CLIENT(client_id)

    End Sub

    Private Sub AfterClientDelete(ByRef status As Boolean, ByRef id As UInt32)

        ' to be implemented priority normal !!

    End Sub


#End Region

End Class
