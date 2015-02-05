﻿' CGenericDataDLL3Computer.vb
' 
' Manages DB Downloads and dll computations to provide data directly
'
' To do:
'       - 
'
'
' Known Bugs:
'       - 
'
'
' Author: Julien Monnereau
' Last modified: 20/01/2015


Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms
Imports System.ComponentModel


Friend Class GenericSingleEntityDLL3Computer


#Region "Instance Variables"

    ' Objects
    Private DBDownloader As New DataBaseDataDownloader
    Private DLL3Computer As DLL3_Interface
    Private EntitiesTV As New TreeView

    ' Variables
    Friend current_version_id As String
    Friend current_entity_id As String
    Friend currentCurrency As String
    Friend currentStrSqlQuery As String
    Protected Friend period_list As List(Of Int32)
    Protected Friend time_config As String


#End Region


#Region "Initialize"

    Friend Sub New()

        DLL3Computer = New DLL3_Interface
        Entity.LoadEntitiesTree(EntitiesTV)
        cTreeViews_Functions.CheckAllNodes(EntitiesTV)

    End Sub



#End Region


#Region "Interface"


    Friend Function ComputeSingleEntity(ByRef versionCode As String, _
                                        ByRef entity_id As String, _
                                        Optional ByVal adjustment_id As String = "") As Boolean

        Dim viewName As String = versionCode & User_Credential
        Dim inputNode As TreeNode = EntitiesTV.Nodes.Find(entity_id, True)(0)
        Dim Versions As New Version
        time_config = Versions.ReadVersion(versionCode, VERSIONS_TIME_CONFIG_VARIABLE)
        If time_config <> DLL3Computer.dll3TimeSetup Then
            period_list = Versions.GetPeriodList(versionCode)
            DLL3Computer.InitDllPeriods(period_list, time_config)
        End If

        DLL3Computer.InitializeDLLOutput(1, 0)
        Select Case inputNode.Nodes.Count
            Case 0
                If DBDownloader.GetEntityInputsNonConverted(entity_id, _
                                                            viewName, _
                                                            adjustment_id) Then

                    DLL3Computer.ComputeEntity(DBDownloader.AccKeysArray, _
                                               DBDownloader.PeriodArray, _
                                               DBDownloader.ValuesArray, 1)

                    current_entity_id = entity_id
                    current_version_id = versionCode
                    Return True
                End If
        End Select

        Return False

    End Function

    Friend Function GetDataFromDLL3Computer(ByRef accountCode As String, _
                                        ByRef period As Integer) As Double

        Return DLL3Computer.GetDataFromComputer(accountCode, period)

    End Function

    ' Renitialize the instance : set current variables(entity, version and currency to "") -> ideally should empty the c_DLL3ComputerInstance ?
    Friend Sub ReinitializeGenericDataDLL3Computer()

        current_entity_id = ""
        currentCurrency = ""
        current_version_id = ""

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

#End Region


End Class