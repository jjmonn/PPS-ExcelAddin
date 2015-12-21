' Account2.vb
'
' CRUD for accounts table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 16/07/2015
' Last modified: 10/09/2015


Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
Imports CRUD

Friend Class AccountManager : Inherits NamedCRUDManager(Of NamedHierarchyCRUDEntity)

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_CREATE_ACCOUNT
        ReadCMSG = ClientMessage.CMSG_READ_ACCOUNT
        UpdateCMSG = ClientMessage.CMSG_UPDATE_ACCOUNT
        UpdateListCMSG = ClientMessage.CMSG_CRUD_ACCOUNT_LIST
        DeleteCMSG = ClientMessage.CMSG_DELETE_ACCOUNT
        ListCMSG = ClientMessage.CMSG_LIST_ACCOUNT

        CreateSMSG = ServerMessage.SMSG_CREATE_ACCOUNT_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_ACCOUNT_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_ACCOUNT_ANSWER
        UpdateListSMSG = ServerMessage.SMSG_CRUD_ACCOUNT_LIST_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_ACCOUNT_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_ACCOUNT_ANSWER

        Build = AddressOf Account.BuildAccount

        InitCallbacks()

    End Sub

#End Region

#Region "Mappings"

    Friend Function GetAccountsList(ByRef LookupOption As String, _
                                    ByRef p_process As Account.AccountProcess) As List(Of Account)

        Dim tmp_list As New List(Of Account)
        Dim selection As New List(Of Account.FormulaTypes)
        Select Case LookupOption
            Case GlobalEnums.AccountsLookupOptions.LOOKUP_ALL
                selection.Add(Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS)
                selection.Add(Account.FormulaTypes.FIRST_PERIOD_INPUT)
                selection.Add(Account.FormulaTypes.FORMULA)
                selection.Add(Account.FormulaTypes.HARD_VALUE_INPUT)

            Case GlobalEnums.AccountsLookupOptions.LOOKUP_INPUTS
                selection.Add(Account.FormulaTypes.FIRST_PERIOD_INPUT)
                selection.Add(Account.FormulaTypes.HARD_VALUE_INPUT)

            Case GlobalEnums.AccountsLookupOptions.LOOKUP_OUTPUTS
                selection.Add(Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS)
                selection.Add(Account.FormulaTypes.FIRST_PERIOD_INPUT)
                selection.Add(Account.FormulaTypes.FORMULA)

            Case GlobalEnums.AccountsLookupOptions.LOOKUP_TITLES
                selection.Add(Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS)
                selection.Add(Account.FormulaTypes.FIRST_PERIOD_INPUT)
                selection.Add(Account.FormulaTypes.FORMULA)
        End Select

        For Each id In m_CRUDDic.Keys
            Dim l_account As Account = GetValue(id)

            If l_account Is Nothing Then Continue For
            If selection.Contains(l_account.FormulaType) _
            AndAlso l_account.Process = p_process Then
                tmp_list.Add(l_account)
            End If
        Next
        Return tmp_list

    End Function

#End Region

#Region "Utilities"

    Friend Sub LoadAccountsTV(ByRef p_treeview As Windows.Forms.TreeView)
        TreeViewsUtilities.LoadTreeview(p_treeview, m_CRUDDic)

    End Sub

    Friend Sub LoadAccountsTV(ByRef p_treeview As VIBlend.WinForms.Controls.vTreeView)
        VTreeViewUtil.LoadTreeview(p_treeview, m_CRUDDic)
        VTreeViewUtil.LoadTreeviewIcons(p_treeview, m_CRUDDic)
    End Sub

    Friend Sub LoadRHAccountsTV(ByRef p_treeview As VIBlend.WinForms.Controls.vTreeView)
        Dim l_RHCRUDDict As New MultiIndexDictionary(Of UInt32, String, NamedHierarchyCRUDEntity)
        For Each l_value As Account In m_CRUDDic.Values
            If l_value.Process = Account.AccountProcess.RH Then
                l_RHCRUDDict.Set(l_value.Id, l_value.Name, l_value)
            End If
        Next
        VTreeViewUtil.LoadTreeview(p_treeview, l_RHCRUDDict)
        VTreeViewUtil.LoadTreeviewIcons(p_treeview, l_RHCRUDDict)
    End Sub

#End Region

End Class
