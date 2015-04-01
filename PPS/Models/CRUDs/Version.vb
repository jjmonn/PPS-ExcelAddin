' version.vb
'
' versions table CRUD model
'
' To do: 
'
'
'
'
' Author: Julien Monnereau
' Last modified: 27/03/2015


Imports ADODB
Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq


Friend Class Version


#Region "Instance Variables"

    ' Objects
    Private SRV As ModelServer
    Friend RST As Recordset

    ' Constants
    Friend object_is_alive As Boolean

    Friend Const YEARLY_VERSIONS_COMPARISON As Int32 = 0
    Friend Const MONTHLY_VERSIONS_COMPARISON As Int32 = 1
    Friend Const YEARLY_MONTHLY_VERSIONS_COMPARISON As Int32 = 2
    Friend Const PERIOD_LIST As String = "PeriodsList"

#End Region


#Region "Initialize"

    Friend Sub New()

        SRV = New ModelServer
        Dim i As Int32 = 0
        Dim q_result = SRV.OpenRst(CONFIG_DATABASE & "." & VERSIONS_TABLE, ModelServer.DYNAMIC_CURSOR)
        SRV.rst.Sort = ITEMS_POSITIONS
        RST = SRV.rst
        object_is_alive = q_result

    End Sub

#End Region


#Region "CRUD Interface"

    Protected Friend Sub CreateVersion(ByRef hash As Hashtable)

        Dim fieldsArray(hash.Count - 1) As Object
        Dim valuesArray(hash.Count - 1) As Object
        hash.Keys.CopyTo(fieldsArray, 0)
        hash.Values.CopyTo(valuesArray, 0)
        RST.AddNew(fieldsArray, valuesArray)

    End Sub

    Protected Friend Function ReadVersion(ByRef version_id As String, ByRef field As String) As Object

        RST.Filter = VERSIONS_CODE_VARIABLE + "='" + version_id + "'"
        If RST.EOF Then Return Nothing
        Return RST.Fields(field).Value

    End Function

    Protected Friend Function GetRecord(ByRef version_id As String) As Hashtable

        RST.Filter = VERSIONS_CODE_VARIABLE + "='" + version_id + "'"
        If RST.EOF Then Return Nothing
        Dim hash As New Hashtable
        hash.Add(VERSIONS_NAME_VARIABLE, RST.Fields(VERSIONS_NAME_VARIABLE).Value)
        hash.Add(VERSIONS_IS_FOLDER_VARIABLE, RST.Fields(VERSIONS_IS_FOLDER_VARIABLE).Value)
        hash.Add(VERSIONS_CREATION_DATE_VARIABLE, RST.Fields(VERSIONS_CREATION_DATE_VARIABLE).Value)
        hash.Add(VERSIONS_LOCKED_VARIABLE, RST.Fields(VERSIONS_LOCKED_VARIABLE).Value)
        hash.Add(VERSIONS_LOCKED_DATE_VARIABLE, RST.Fields(VERSIONS_LOCKED_DATE_VARIABLE).Value)
        hash.Add(VERSIONS_TIME_CONFIG_VARIABLE, RST.Fields(VERSIONS_TIME_CONFIG_VARIABLE).Value)
        hash.Add(VERSIONS_RATES_VERSION_ID_VAR, RST.Fields(VERSIONS_RATES_VERSION_ID_VAR).Value)
        hash.Add(VERSIONS_START_PERIOD_VAR, RST.Fields(VERSIONS_START_PERIOD_VAR).Value)
        hash.Add(VERSIONS_NB_PERIODS_VAR, RST.Fields(VERSIONS_NB_PERIODS_VAR).Value)
        Return hash

    End Function

    Protected Friend Sub UpdateVersion(ByRef version_id As String, ByRef hash As Hashtable)

        RST.Filter = VERSIONS_CODE_VARIABLE + "='" + version_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            For Each Attribute In hash.Keys
                If RST.Fields(Attribute).Value <> hash(Attribute) Then
                    RST.Fields(Attribute).Value = hash(Attribute)
                    RST.Update()
                End If
            Next
        End If

    End Sub

    Protected Friend Sub UpdateVersion(ByRef version_id As String, _
                                      ByRef field As String, _
                                      ByVal value As Object)

        RST.Filter = VERSIONS_CODE_VARIABLE + "='" + version_id + "'"
        If RST.EOF = False Then

            If IsDBNull(RST.Fields(field).Value) AndAlso Not IsDBNull(value) Then
                RST.Fields(field).Value = value
                RST.Update()
                Exit Sub
            End If

            If IsDBNull(value) Then
                RST.Fields(field).Value = DBNull.Value
            Else
                If RST.Fields(field).Value <> value Then RST.Fields(field).Value = value
            End If
            RST.Update()
        End If

    End Sub

    Protected Friend Sub DeleteVersion(ByRef version_id As String)

        RST.Filter = VERSIONS_CODE_VARIABLE + "='" + version_id + "'"
        If RST.EOF = False Then
            RST.Delete()
            RST.Update()
        End If

    End Sub


#End Region


#Region "Utilities"

    Protected Friend Shared Sub LoadVersionsTree(ByRef TV As TreeView)

        Dim srv As New ModelServer
        If srv.OpenRst(CONFIG_DATABASE & "." & VERSIONS_TABLE, ModelServer.FWD_CURSOR) Then

            Dim currentNode, ParentNode() As TreeNode
            TV.Nodes.Clear()
            srv.rst.Sort = ITEMS_POSITIONS

            If srv.rst.RecordCount > 0 Then
                srv.rst.MoveFirst()

                Do While srv.rst.EOF = False

                    If IsDBNull(srv.rst.Fields(VERSIONS_PARENT_CODE_VARIABLE).Value) Then
                        currentNode = TV.Nodes.Add(Trim(srv.rst.Fields(VERSIONS_CODE_VARIABLE).Value), _
                                                   Trim(srv.rst.Fields(VERSIONS_NAME_VARIABLE).Value), _
                                                   srv.rst.Fields(VERSIONS_IS_FOLDER_VARIABLE).Value, _
                                                   srv.rst.Fields(VERSIONS_IS_FOLDER_VARIABLE).Value)
                    Else
                        ParentNode = TV.Nodes.Find(Trim(srv.rst.Fields(VERSIONS_PARENT_CODE_VARIABLE).Value), True)
                        currentNode = ParentNode(0).Nodes.Add(Trim(srv.rst.Fields(VERSIONS_CODE_VARIABLE).Value), _
                                                              Trim(srv.rst.Fields(VERSIONS_NAME_VARIABLE).Value), _
                                                              srv.rst.Fields(VERSIONS_IS_FOLDER_VARIABLE).Value, _
                                                              srv.rst.Fields(VERSIONS_IS_FOLDER_VARIABLE).Value)

                    End If
                    srv.rst.MoveNext()
                Loop
            End If
            srv.rst.Close()
        End If

    End Sub

    Protected Friend Function GetVersionsDictionary(ByRef versionsNodesList As List(Of String)) As Dictionary(Of String, Hashtable)

        Dim versions_dict As Dictionary(Of String, Hashtable)
        Dim versions_id_list As New List(Of String)
        If versionsNodesList.Count = 0 Then
            versions_dict = GetVersionsDictionary({GlobalVariables.GLOBALCurrentVersionCode})
        Else
            For Each version_id As String In versionsNodesList
                If ReadVersion(version_id, VERSIONS_IS_FOLDER_VARIABLE) = 0 Then
                    versions_id_list.Add(version_id)
                End If
            Next
            versions_dict = GetVersionsDictionary(versions_id_list.ToArray)
        End If
        Return versions_dict

    End Function

    Protected Friend Function GetPeriodsNode(ByRef versions_dict As Dictionary(Of String, Hashtable)) As TreeNode

        Select Case IdentifyVersionsComparison(versions_dict)
            Case MONTHLY_TIME_CONFIGURATION, YEARLY_MONTHLY_VERSIONS_COMPARISON
                Return GetVersionsComparisonPeriodsNode(versions_dict)
            Case Else
                Return GetYearlyPeriodsNode(versions_dict)
        End Select

    End Function

    ' Return a list of Dates corresponding to the param version code
    Friend Function GetPeriodsDatesList(ByRef versionCode) As List(Of Date)

        Dim periodsList As List(Of Integer) = GetPeriodList(versionCode)
        Dim datesList As New List(Of Date)

        For Each period In periodsList
            datesList.Add(DateTime.FromOADate(period))
        Next

        Return datesList

    End Function


#Region "Utilities"

    Private Function IdentifyVersionsComparison(ByRef versions_dict As Dictionary(Of String, Hashtable)) As String

        Dim comparison_flag As String
        Dim timeSetup As String = versions_dict.Values(0)(VERSIONS_TIME_CONFIG_VARIABLE)
        For Each version_id As String In versions_dict.Keys
            If timeSetup <> versions_dict(version_id)(VERSIONS_TIME_CONFIG_VARIABLE) Then
                comparison_flag = YEARLY_MONTHLY_VERSIONS_COMPARISON
                Return comparison_flag
            End If
        Next
        Return timeSetup

    End Function

    Private Function GetVersionsDictionary(ByRef versions_id_array As String()) As Dictionary(Of String, Hashtable)

        Dim versions_dict As New Dictionary(Of String, Hashtable)
        For Each version_id In versions_id_array
            versions_dict.Add(version_id, GetRecord(version_id))
            versions_dict(version_id).Add(PERIOD_LIST, GetPeriodList(version_id))
        Next
        Return versions_dict

    End Function

    Private Function GetVersionsComparisonPeriodsNode(ByRef versions_dict As Dictionary(Of String, Hashtable)) As TreeNode

        Dim year_nodes As New Dictionary(Of String, TreeNode)
        For Each version_id In versions_dict.Keys
            Dim years_list As List(Of Int32) = Period.GetYearlyPeriodList(versions_dict(version_id)(VERSIONS_START_PERIOD_VAR), versions_dict(version_id)(VERSIONS_NB_PERIODS_VAR))
            For Each year_id In years_list
                If year_nodes.ContainsKey(year_id) = False Then
                    Dim tmp_node = New TreeNode(Format(DateTime.FromOADate(year_id), "yyyy"))
                    tmp_node.Name = year_id
                    For Each month_id In Period.GetMonthlyPeriodsList(year_id, 1, False)
                        tmp_node.Nodes.Add(month_id, Format(DateTime.FromOADate(month_id), "MMM-yy"))
                    Next
                End If
            Next
        Next
        Dim keys As List(Of String) = year_nodes.Keys.ToList
        Dim periods_node As New TreeNode
        keys.Sort()
        For Each year_id As String In keys
            periods_node.Nodes.Add(year_nodes(year_id))
        Next
        Return periods_node

    End Function

    Private Function GetYearlyPeriodsNode(ByRef versions_dict As Dictionary(Of String, Hashtable))

        Dim year_dic As New Dictionary(Of String, String)
        For Each version_id In versions_dict.Keys
            Dim years_list As List(Of Int32) = Period.GetYearlyPeriodList(versions_dict(version_id)(VERSIONS_START_PERIOD_VAR), versions_dict(version_id)(VERSIONS_NB_PERIODS_VAR))
            For Each year_id In years_list
                If year_dic.ContainsKey(year_id) = False Then
                    year_dic.Add(year_id, Format(DateTime.FromOADate(year_id), "yyyy"))
                End If
            Next
        Next
        Dim keys As List(Of String) = year_dic.Keys.ToList
        Dim periods_node As New TreeNode
        keys.Sort()
        For Each year_id As String In keys
            periods_node.Nodes.Add(year_id, year_dic(year_id))
        Next
        Return periods_node

    End Function

    ' Return a list of Integer Periods corresponding to the param version code
    Friend Function GetPeriodList(ByRef version_id As String) As List(Of Integer)

        Dim time_configuration As String = ReadVersion(version_id, VERSIONS_TIME_CONFIG_VARIABLE)
        Select Case time_configuration
            Case YEARLY_TIME_CONFIGURATION : Return Period.GetYearlyPeriodList(ReadVersion(version_id, VERSIONS_START_PERIOD_VAR), ReadVersion(version_id, VERSIONS_NB_PERIODS_VAR))
            Case MONTHLY_TIME_CONFIGURATION : Return Period.GetMonthlyPeriodsList(ReadVersion(version_id, VERSIONS_START_PERIOD_VAR), ReadVersion(version_id, VERSIONS_NB_PERIODS_VAR), True)
            Case Else
                MsgBox("PPS Error N°9: Unknown Time Configuration")
                Return Nothing
        End Select

    End Function

    Protected Friend Function GetGlobalPeriodsDict(ByRef version_id As String) As Dictionary(Of Int32, List(Of Int32))


    End Function

#End Region

    Protected Friend Sub Close()

        finalize()

    End Sub

    Protected Overrides Sub finalize()

        Try
            RST.Close()
        Catch ex As Exception

        End Try
        MyBase.Finalize()

    End Sub


#End Region



End Class
