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
' Last modified: 19/01/2015


Imports ADODB
Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic


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
        If RST.EOF = False AndAlso RST.BOF = False Then
            If RST.Fields(field).Value <> value Then
                RST.Fields(field).Value = value
                RST.Update()
            End If
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

    Friend Function InitializeVersionsArray(ByRef versionsNodesList As List(Of TreeNode),
                                            ByRef periodsList As List(Of Integer), _
                                            ByRef versionsComparisonFlag As Int32, _
                                            ByRef versions_id_array As String(), _
                                            ByRef versions_name_array As String()) As Dictionary(Of String, Hashtable)

        versionsComparisonFlag = -1
        Dim versions_dict As Dictionary(Of String, Hashtable)
        If Not periodsList Is Nothing Then periodsList = Nothing
        If versionsNodesList.Count = 0 Then
            versions_id_array = {GLOBALCurrentVersionCode}
            versions_name_array = {Version_Label.Caption}
            versions_dict = GetVersionsDictionary(versions_id_array)
            periodsList = versions_dict(versions_id_array(0))(PERIOD_LIST)
        Else
            ReDim versions_id_array(versionsNodesList.Count - 1)
            ReDim versions_name_array(versionsNodesList.Count - 1)
            Dim i As Int32
            For Each Version As TreeNode In versionsNodesList
                If Version.Nodes.Count = 0 Then
                    versions_id_array(i) = Version.Name
                    versions_name_array(i) = Version.Text
                    i = i + 1
                End If
            Next
            ReDim Preserve versions_id_array(i - 1)
            ReDim Preserve versions_name_array(i - 1)
            versions_dict = GetVersionsDictionary(versions_id_array)

            If versions_id_array.Length > 1 Then
                IdentifyVersionsComparison(versions_id_array, versionsComparisonFlag, versions_dict)
                periodsList = GetSeveralVersionsPeriodsList(versions_id_array, versionsComparisonFlag, versions_dict)
            Else
                periodsList = versions_dict(versions_id_array(0))(PERIOD_LIST)
            End If
        End If
        Return versions_dict

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

    Private Sub IdentifyVersionsComparison(ByRef versions_id_array As String(), _
                                           ByRef versionsComparisonFlag As Int32, _
                                           ByRef versions_dict As Dictionary(Of String, Hashtable))

        Dim timeSetup As String = versions_dict(versions_id_array(0))(VERSIONS_TIME_CONFIG_VARIABLE)
        For i = 0 To versions_id_array.Length - 1
            If timeSetup <> versions_dict(versions_id_array(i))(VERSIONS_TIME_CONFIG_VARIABLE) Then
                versionsComparisonFlag = YEARLY_MONTHLY_VERSIONS_COMPARISON
                Exit Sub
            End If
        Next

        If timeSetup = MONTHLY_TIME_CONFIGURATION Then
            versionsComparisonFlag = MONTHLY_VERSIONS_COMPARISON
        Else
            versionsComparisonFlag = YEARLY_VERSIONS_COMPARISON
        End If

    End Sub

    ' Return Union Periods List
    Private Function GetSeveralVersionsPeriodsList(ByRef versions_id_array As String(), _
                                                   ByRef versionsComparisonFlag As Int32, _
                                                   ByRef versions_dict As Dictionary(Of String, Hashtable)) As List(Of Integer)

        Dim periods_list As New List(Of Int32)
        Select Case versionsComparisonFlag

            Case YEARLY_VERSIONS_COMPARISON, MONTHLY_VERSIONS_COMPARISON
                For Each version_id In versions_id_array
                    For Each period_int In versions_dict(version_id)(period_list)
                        If PERIOD_LIST.Contains(period_int) = False Then periods_list.Add(period_int)
                    Next
                Next
                Return periods_list

            Case YEARLY_MONTHLY_VERSIONS_COMPARISON
                For Each version_id In versions_id_array
                    If versions_dict(version_id)(VERSIONS_TIME_CONFIG_VARIABLE) = YEARLY_TIME_CONFIGURATION Then
                        For Each period_int In versions_dict(version_id)(PERIOD_LIST)
                            If PERIOD_LIST.Contains(period_int) = False Then periods_list.Add(period_int)
                        Next
                    Else
                        Dim period_ = versions_dict(version_id)(VERSIONS_START_PERIOD_VAR)
                        If PERIOD_LIST.Contains(period_) = False Then periods_list.Add(period_)
                    End If
                Next
                Return periods_list

            Case Else
                ' PPS Error tracking
                Return Nothing
        End Select


    End Function

    Private Function GetVersionsDictionary(ByRef versions_id_array As String()) As Dictionary(Of String, Hashtable)

        Dim versions_dict As New Dictionary(Of String, Hashtable)
        For Each version_id In versions_id_array
            versions_dict.Add(version_id, GetRecord(version_id))
            versions_dict(version_id).Add(PERIOD_LIST, GetPeriodList(version_id))
        Next
        Return versions_dict

    End Function

    ' Return a list of Integer Periods corresponding to the param version code
    Friend Function GetPeriodList(ByRef version_id) As List(Of Integer)

        Dim time_configuration As String = ReadVersion(version_id, VERSIONS_TIME_CONFIG_VARIABLE)
        Select Case time_configuration
            Case YEARLY_TIME_CONFIGURATION : Return Period.GetYearlyPeriodList(ReadVersion(version_id, VERSIONS_START_PERIOD_VAR), ReadVersion(version_id, VERSIONS_NB_PERIODS_VAR))
            Case MONTHLY_TIME_CONFIGURATION : Return Period.GetMonthlyPeriodsList(ReadVersion(version_id, VERSIONS_START_PERIOD_VAR), ReadVersion(version_id, VERSIONS_NB_PERIODS_VAR), True)
            Case Else
                MsgBox("PPS Error N°9: Unknown Time Configuration")
                Return Nothing
        End Select

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
