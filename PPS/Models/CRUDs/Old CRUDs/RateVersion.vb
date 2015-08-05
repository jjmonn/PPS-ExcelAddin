' RateVersion.vb : CRUD model for rate_versions table
'
'
'
' -> action on rst update automatically ...
'
'
'
'
' Author: Julien Monnereau
' Last modified: 20/01/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports ADODB


Friend Class RateVersion


#Region "Instance Variables"

    ' Objects
    Private SRV As ModelServer
    Friend RST As Recordset

    ' Variables
    Friend isModified As Boolean
    Friend object_is_alive As Boolean

#End Region


#Region "Initialize"

    Protected Friend Sub New()

        SRV = New ModelServer
        Dim i As Int32 = 0
        Dim q_result = SRV.openRst(GlobalVariables.database & "." & RATES_VERSIONS_TABLE, ModelServer.DYNAMIC_CURSOR)
        While q_result = False AndAlso i < 10
            q_result = SRV.openRst(GlobalVariables.database & "." & RATES_VERSIONS_TABLE, ModelServer.DYNAMIC_CURSOR)
            i = i + 1
        End While
        object_is_alive = q_result
        SRV.rst.Sort = ITEMS_POSITIONS
        RST = SRV.rst

    End Sub


#End Region


#Region "CRUD Interface"

    Protected Friend Sub CreateVersion(ByRef new_version_attributes As Hashtable)

        Dim fieldsArray(new_version_attributes.Count - 1) As Object
        Dim valuesArray(new_version_attributes.Count - 1) As Object
        new_version_attributes.Keys.CopyTo(fieldsArray, 0)
        new_version_attributes.Values.CopyTo(valuesArray, 0)
        RST.AddNew(fieldsArray, valuesArray)

    End Sub

    Protected Friend Function ReadVersion(ByRef version_id As String, ByRef field As String) As Object

        RST.Filter = RATES_VERSIONS_ID_VARIABLE + "='" + version_id + "'"
        If RST.EOF Then Return Nothing
        Return RST.Fields(field).Value

    End Function

    Protected Friend Function ReadVersions() As Dictionary(Of String, Hashtable)

        Dim tmp_dic As New Dictionary(Of String, Hashtable)
        RST.Filter = ""
        RST.MoveFirst()
        While RST.EOF = False
            Dim hash As New Hashtable
            hash.Add(NAME_VARIABLE, RST.Fields(NAME_VARIABLE).Value)
            hash.Add(RATES_VERSIONS_IS_FOLDER_VARIABLE, RST.Fields(RATES_VERSIONS_IS_FOLDER_VARIABLE).Value)
            If IsDBNull(RST.Fields(RATES_parent_id).Value) Then
                hash.Add(RATES_parent_id, "")
            Else
                hash.Add(RATES_parent_id, RST.Fields(RATES_parent_id).Value)
            End If
            hash.Add(ITEMS_POSITIONS, RST.Fields(ITEMS_POSITIONS).Value)
            hash.Add(RATES_VERSIONS_START_PERIOD_VAR, RST.Fields(RATES_VERSIONS_START_PERIOD_VAR).Value)
            hash.Add(RATES_VERSIONS_NB_PERIODS_VAR, RST.Fields(RATES_VERSIONS_NB_PERIODS_VAR).Value)

            tmp_dic.Add(RST.Fields(RATES_VERSIONS_ID_VARIABLE).Value, hash)
            RST.MoveNext()
        End While
        Return tmp_dic

    End Function

    Protected Friend Sub UpdateVersion(ByRef version_id As String, ByRef versionAttributes As Hashtable)

        RST.Filter = RATES_VERSIONS_ID_VARIABLE + "='" + version_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            For Each Attribute In versionAttributes.Keys
                If RST.Fields(Attribute).Value <> versionAttributes(Attribute) Then RST.Fields(Attribute).Value = versionAttributes(Attribute)
            Next
            RST.Update()
        End If

    End Sub

    Protected Friend Sub UpdateVersion(ByRef version_id As String, _
                             ByRef field As String, _
                             ByVal value As Object)

        RST.Filter = RATES_VERSIONS_ID_VARIABLE + "='" + version_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            If RST.Fields(field).Value <> value Then
                RST.Fields(field).Value = value
                RST.Update()
            End If
        End If

    End Sub

    Protected Friend Sub DeleteVersion(ByRef version_id As String)

        RST.Filter = RATES_VERSIONS_ID_VARIABLE + "='" + version_id + "'"
        If RST.EOF = False Then
            RST.Delete()
            RST.Update()
        End If


    End Sub

    Protected Friend Sub UpdateModel()

        RST.Update()
        isModified = False

    End Sub

#End Region


#Region "Utilities"

    Protected Friend Shared Sub load_rates_version_tv(ByRef TV As TreeView)

        Dim srv As New ModelServer
        srv.openRst(GlobalVariables.database & "." & RATES_VERSIONS_TABLE, ModelServer.FWD_CURSOR)
        Dim rst = srv.rst
        Dim nodeX, ParentNode() As TreeNode
        TV.Nodes.Clear()
        rst.Filter = ""
        rst.Sort = ITEMS_POSITIONS
        If rst.RecordCount > 0 Then
            rst.MoveFirst()
            Do While rst.EOF = False

                If IsDBNull(rst.Fields(RATES_parent_id).Value) Then
                    nodeX = TV.Nodes.Add(Trim(rst.Fields(RATES_VERSIONS_ID_VARIABLE).Value), _
                                         Trim(rst.Fields(NAME_VARIABLE).Value), _
                                         rst.Fields(RATES_VERSIONS_IS_FOLDER_VARIABLE).Value, _
                                         rst.Fields(RATES_VERSIONS_IS_FOLDER_VARIABLE).Value)
                Else
                    ParentNode = TV.Nodes.Find(Trim(rst.Fields(RATES_parent_id).Value), True)
                    nodeX = ParentNode(0).Nodes.Add(Trim(rst.Fields(RATES_VERSIONS_ID_VARIABLE).Value), _
                                                    Trim(rst.Fields(NAME_VARIABLE).Value), _
                                                    rst.Fields(RATES_VERSIONS_IS_FOLDER_VARIABLE).Value, _
                                                    rst.Fields(RATES_VERSIONS_IS_FOLDER_VARIABLE).Value)
                End If
                nodeX.EnsureVisible()
                rst.MoveNext()
            Loop
        End If
        rst.Close()

    End Sub

    Protected Friend Function GetRatesVersionsList(field As String) As List(Of String)

        Dim tmpList As New List(Of String)
        RST.Filter = RATES_VERSIONS_IS_FOLDER_VARIABLE + "= 0"
        If RST.EOF = False And RST.BOF = False Then
            RST.MoveFirst()
            Do While RST.EOF = False
                tmpList.Add(RST.Fields(field).Value)
                RST.MoveNext()
            Loop
        End If
        Return tmpList

    End Function

    Protected Friend Function GetPeriodList(ByRef rates_version_id As String) As List(Of Integer)

        Return Period.GetYearlyPeriodList(ReadVersion(rates_version_id, RATES_VERSIONS_START_PERIOD_VAR), _
                                          ReadVersion(rates_version_id, RATES_VERSIONS_NB_PERIODS_VAR))

    End Function

    Protected Friend Function GetPeriodsDictionary(ByRef rates_version_id As String) As Dictionary(Of Int32, List(Of Int32))

        Return Period.GetGlobalPeriodsDictionary(GetPeriodList(rates_version_id))

    End Function

    Protected Overrides Sub finalize()

        RST.Close()
        MyBase.Finalize()

    End Sub


#End Region




End Class
