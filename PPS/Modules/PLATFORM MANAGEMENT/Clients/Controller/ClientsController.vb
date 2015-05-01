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
' Last modified: 26/04/2015


Friend Class ClientsController


#Region "Instance Variables"

    ' Objects
    Private view As clientsControl
    Private clients As client
    Private clients_categoriesTV As New TreeView
    Private PlatformMGTUI As PlatformMGTGeneralUI

    ' Variable
    Private clients_list As List(Of String)
    Friend categoriesNameKeyDic As Hashtable


#End Region


#Region "Initialize"

    Protected Friend Sub New()

        clients = New client
        AnalysisAxisCategory.LoadCategoryCodeTV(clients_categoriesTV, ControllingUI2Controller.client_CATEGORY_CODE)
        clients_list = clientsMapping.GetclientsIDList()
        categoriesNameKeyDic = CategoriesMapping.GetCategoryDictionary(ControllingUI2Controller.client_CATEGORY_CODE, _
                                                                       ANALYSIS_AXIS_NAME_VAR, _
                                                                       ANALYSIS_AXIS_ID_VAR)
        view = New clientsControl(Me, _
                                   clients_categoriesTV, _
                                   getclientsHash(), _
                                   categoriesNameKeyDic)

    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.PlatformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(view)
        view.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Private Function getclientsHash() As Dictionary(Of String, Hashtable)

        Dim tmp_dict As New Dictionary(Of String, Hashtable)
        For Each client_id As String In clients_list
            tmp_dict.Add(client_id, clients.GetRecord(client_id, clients_categoriesTV))
        Next
        Return tmp_dict

    End Function

    Public Sub close()

        View.closeControl()
        view.Dispose()
        clients.RST.Close()
        PlatformMGTUI.displayControl()

    End Sub

  
#End Region


#Region "Interface"

    Protected Friend Sub createclient(ByRef hash As Hashtable)

        If clients.isNameValid(hash(ANALYSIS_AXIS_NAME_VAR)) = True Then
            hash.Add(ANALYSIS_AXIS_ID_VAR, clients.getNewId())
            clients.Createclient(hash)
            view.addclientRow(hash(ANALYSIS_AXIS_ID_VAR), hash)
        Else
            MsgBox("Invalid Name. Names must be unique and not empty.")
        End If

    End Sub

    Protected Friend Function updateclientName(ByRef item_id As String, _
                                                ByRef value As String) As Boolean

        If clients.isNameValid(value) = True Then
            clients.Updateclient(item_id, ANALYSIS_AXIS_NAME_VAR, value)
            Return True
        Else
            MsgBox("Invalid Name. Names must be unique and not empty.")
            Return False
        End If

    End Function

    Protected Friend Sub updateclientCategory(ByRef item_id As String, _
                                               ByRef category_id As String, _
                                               ByRef value As String)

        clients.Updateclient(item_id, category_id, value)

    End Sub

    Protected Friend Sub deleteclient(ByRef client_id As String)

        clients.deleteclient(client_id)

    End Sub

#End Region

End Class
