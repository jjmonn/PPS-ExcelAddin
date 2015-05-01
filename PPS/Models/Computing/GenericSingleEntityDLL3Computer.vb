' CGenericDataDLL3Computer.vb
' 
' Manages DB Downloads and dll computations to provide data directly
'
' To do:
'       - Need to develop the cache : filters and current computers version/ entity ids
'
'
' Known Bugs:
'       - 
'
'
' Author: Julien Monnereau
' Last modified: 25/05/2015


Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms
Imports System.ComponentModel


Friend Class GenericSingleEntityDLL3Computer


#Region "Instance Variables"

    ' Objects
    Private DBDownloader As DataBaseDataDownloader
    Private DLL3Computer As DLL3_Interface
    Private EntitiesTV As New TreeView
    Private entity_node As TreeNode

    ' Variables
    Protected Friend current_version_id As String = ""
    Protected Friend current_currency As String = ""
    Protected Friend clients_id_filters_list As List(Of String)
    Protected Friend products_id_filters_list As List(Of String)
    Protected Friend adjustments_id_filters_list As List(Of String)
    Protected Friend current_state As Boolean = False
    Protected Friend period_list As List(Of Int32)
    Protected Friend time_config As String


#End Region


#Region "Initialize"

    Friend Sub New(ByRef input_DBDownloader As DataBaseDataDownloader, _
                   Optional ByRef input_dll3_interface As DLL3_Interface = Nothing)

        DBDownloader = input_DBDownloader
        If input_dll3_interface Is Nothing Then
            DLL3Computer = New DLL3_Interface
        Else
            DLL3Computer = input_dll3_interface
        End If
        Entity.LoadEntitiesTree(EntitiesTV)
        TreeViewsUtilities.CheckAllNodes(EntitiesTV)

    End Sub

#End Region


#Region "Interface"

    Friend Function ComputeSingleEntity(ByRef versionCode As String, _
                                        ByRef input_entity_node As TreeNode, _
                                        Optional ByRef clients_id_list As List(Of String) = Nothing, _
                                        Optional ByRef products_id_list As List(Of String) = Nothing, _
                                        Optional ByVal adjustment_id_list As List(Of String) = Nothing) As Boolean

        Dim viewName As String = versionCode & GlobalVariables.User_Credential
        entity_node = input_entity_node
        Dim Versions As New Version
        time_config = Versions.ReadVersion(versionCode, VERSIONS_TIME_CONFIG_VARIABLE)
        If time_config <> DLL3Computer.dll3TimeSetup Then
            period_list = Versions.GetPeriodList(versionCode)
            DLL3Computer.InitDllPeriods(period_list, time_config)
        End If

        DLL3Computer.InitializeDLLOutput(1, 0)
        Select Case entity_node.Nodes.Count
            Case 0
                If DBDownloader.GetEntityInputsNonConverted(entity_node.Name, _
                                                            viewName, _
                                                            clients_id_list, _
                                                            products_id_list, _
                                                            adjustment_id_list) Then

                    DLL3Computer.ComputeEntity(DBDownloader.AccKeysArray, _
                                               DBDownloader.PeriodArray, _
                                               DBDownloader.ValuesArray, 1)

                    current_version_id = versionCode
                    clients_id_filters_list = clients_id_list
                    products_id_filters_list = products_id_list
                    adjustments_id_filters_list = adjustment_id_list
                    current_state = True
                    Return True
                End If
        End Select
        current_state = False
        Return False

    End Function

    Friend Function ComputeAggregatedEntity(ByRef input_entity_node As TreeNode, _
                                            ByRef version_id As String, _
                                            ByRef destination_currency As String, _
                                            Optional ByRef clients_id_list As List(Of String) = Nothing, _
                                            Optional ByRef products_id_list As List(Of String) = Nothing, _
                                            Optional ByVal adjustment_id_list As List(Of String) = Nothing) As Boolean

        Dim entities_id_list As List(Of String)
        entity_node = input_entity_node
        Dim Versions As New Version
        time_config = Versions.ReadVersion(version_id, VERSIONS_TIME_CONFIG_VARIABLE)
        If time_config <> DLL3Computer.dll3TimeSetup Then
            period_list = Versions.GetPeriodList(version_id)
            DLL3Computer.InitDllPeriods(period_list, time_config)
        End If

        DLL3Computer.InitializeDLLOutput(1, 0)
        Select Case entity_node.Nodes.Count
            Case 0
                entities_id_list = New List(Of String)
                entities_id_list.Add(entity_node.Name)
            Case Else
                Dim all_entities_id_list = TreeViewsUtilities.GetNodesKeysList(entity_node)
                entities_id_list = TreeViewsUtilities.GetNoChildrenNodesList(all_entities_id_list, EntitiesTV)
        End Select

        If DBDownloader.GetAggregatedConvertedInputs(entities_id_list, _
                                                     version_id, _
                                                     destination_currency, _
                                                     clients_id_list, _
                                                     products_id_list, _
                                                     adjustment_id_list) Then

            DLL3Computer.ComputeEntity(DBDownloader.AccKeysArray, _
                                       DBDownloader.PeriodArray, _
                                       DBDownloader.ValuesArray, 1)

            current_version_id = version_id
            current_currency = destination_currency
            clients_id_filters_list = clients_id_list
            products_id_filters_list = products_id_list
            adjustments_id_filters_list = adjustment_id_list
            Return True
        Else
            Return False
        End If

    End Function

    Friend Function GetDataFromDLL3Computer(ByRef accountCode As String, _
                                            ByRef period As Integer) As Double

        If current_state = True Then
            Return DLL3Computer.GetDataFromComputer(accountCode, period)
        Else
            Return 0
        End If

    End Function

    Friend Sub ReinitializeGenericDataDLL3Computer()

        entity_node = Nothing
        current_currency = ""
        current_version_id = ""
        clients_id_filters_list = Nothing
        products_id_filters_list = Nothing
        adjustments_id_filters_list = Nothing

    End Sub

    Friend Function CheckParserFormula(ByRef formula_str As String) As Boolean

        Return DLL3Computer.CheckParserFormula(formula_str)

    End Function

    Friend Sub CloseDLL3ComputerInstance()

        DLL3Computer.destroy_dll()

    End Sub

#End Region


#Region "Utilities"

    ' Recursively adds the entity (param1) to list (param2)
    'Public Shared Sub addEntitiesToEntitiesIDList(ByRef inputNode As TreeNode, _
    '                                              ByRef entitiesIDList As List(Of String))

    '    If inputNode.Checked = True AndAlso _
    '    inputNode.Nodes.Count = 0 Then entitiesIDList.Add(inputNode.Name)
    '    For Each child As TreeNode In inputNode.Nodes
    '        If child.Nodes.Count = 0 Then addEntitiesToEntitiesIDList(child, entitiesIDList)
    '    Next

    'End Sub

    Protected Friend Function CheckCache(ByRef input_entity_node As TreeNode, _
                                         ByRef clients_id As List(Of String), _
                                         ByRef products_id As List(Of String), _
                                         ByRef adjustments_id As List(Of String)) As Boolean

        If Utilities_Functions.ListsEqualityCheck(TreeViewsUtilities.GetCheckedNodesID(input_entity_node), _
                                                  TreeViewsUtilities.GetCheckedNodesID(entity_node)) = False Then Return False
        If Utilities_Functions.ListsEqualityCheck(clients_id, clients_id_filters_list) = False Then Return False
        If Utilities_Functions.ListsEqualityCheck(products_id, products_id_filters_list) = False Then Return False
        If Utilities_Functions.ListsEqualityCheck(adjustments_id, adjustments_id_filters_list) = False Then Return False
        Return True

    End Function

    Protected Friend Function GetEntityNode(ByRef entity_id As String) As TreeNode

        Return EntitiesTV.Nodes.Find(entity_id, True)(0)

    End Function

#End Region


End Class
