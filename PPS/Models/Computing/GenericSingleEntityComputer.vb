' CGenericDataComputer.vb
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
' Last modified: 01/12/2014


Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms
Imports System.ComponentModel


Friend Class GenericSingleEntityComputer


#Region "Instance Variables"

    ' Objects
    Private DBDOWNLOADER As New DataBaseDataDownloader
    Private COMPUTER As DLL3_Interface
    Friend VERSIONSMGT As New CVersionsForControlingUIs
    Private EntitiesTV As New TreeView

    ' Variables
    Friend currentVersionCode As String
    Friend currentEntityCode As String
    Friend currentCurrency As String
    Friend currentStrSqlQuery As String

#End Region


#Region "Initialize"

    Friend Sub New()

        COMPUTER = New DLL3_Interface
        Entity.LoadEntitiesTree(EntitiesTV)
        cTreeViews_Functions.CheckAllNodes(EntitiesTV)

    End Sub



#End Region


#Region "Interface"


    ' entity_id must be an input entity
    ' check below !!!!
    Friend Function ComputeSingleEntity(ByRef versionCode As String, _
                                        ByRef entityCode As String, _
                                        Optional ByRef strSqlQuery As String = "") As Boolean

        Dim viewName As String = versionCode & User_Credential
        Dim inputNode As TreeNode = EntitiesTV.Nodes.Find(entityCode, True)(0)

        Dim versionTimeSetup As String = VERSIONSMGT.versionsCodeTimeSetUpDict(versionCode)(VERSIONS_TIME_CONFIG_VARIABLE)
        If versionTimeSetup <> COMPUTER.dll3TimeSetup Then
            Dim periodsList As List(Of Integer) = VERSIONSMGT.GetPeriodList(versionCode)
            COMPUTER.InitDllPeriods(periodsList, versionTimeSetup)
        End If

        COMPUTER.InitializeDLLOutput(1, 0)
        Select Case inputNode.Nodes.Count
            Case 0
                If DBDOWNLOADER.GetEntityInputsNonConverted(entityCode, _
                                                            viewName) Then

                    COMPUTER.ComputeEntity(DBDOWNLOADER.AccKeysArray, _
                                           DBDOWNLOADER.PeriodArray, _
                                           DBDOWNLOADER.ValuesArray, 1)

                    currentEntityCode = entityCode
                    currentVersionCode = versionCode
                    Return True
                End If
        End Select

        Return False

    End Function

    Friend Function GetDataFromComputer(ByRef accountCode As String, _
                                        ByRef period As Integer, _
                                        ByRef currency As String) As Double

        Return COMPUTER.GetDataFromComputer(accountCode, period, currency)

    End Function

    ' Renitialize the instance : set current variables(entity, version and currency to "") -> ideally should empty the c_computerInstance ?
    Friend Sub ReinitializeGenericDataComputer()

        currentEntityCode = ""
        currentCurrency = ""
        currentVersionCode = ""

    End Sub

    Friend Function CheckParserFormula(ByRef formula_str As String) As Boolean

        Return COMPUTER.CheckParserFormula(formula_str)

    End Function

    Friend Sub CloseComputerInstance()

        COMPUTER.destroy_dll()

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
