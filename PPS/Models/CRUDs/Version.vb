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
' Last modified: 05/01/2015


Imports ADODB
Imports System.Windows.Forms
Imports System.Collections


Friend Class Version


#Region "Instance Variables"

    ' Objects
    Private SRV As ModelServer
    Friend RST As Recordset

    ' Constants
    Friend object_is_alive As Boolean
    Private Const NB_CONNECTIONS_TRIALS = 10

#End Region


#Region "Initialize"

    Friend Sub New()

        SRV = New ModelServer
        Dim i As Int32 = 0
        Dim q_result = SRV.openRst(CONFIG_DATABASE & "." & VERSIONS_TABLE, ModelServer.DYNAMIC_CURSOR)
        While q_result = False AndAlso i < NB_CONNECTIONS_TRIALS
            q_result = SRV.openRst(CONFIG_DATABASE & "." & VERSIONS_TABLE, ModelServer.DYNAMIC_CURSOR)
            i = i + 1
        End While
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
        hash.Add(VERSIONS_REFERENCE_YEAR_VARIABLE, RST.Fields(VERSIONS_REFERENCE_YEAR_VARIABLE).Value)
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
        If srv.openRst(CONFIG_DATABASE & "." & VERSIONS_TABLE, ModelServer.FWD_CURSOR) Then

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

    Protected Overrides Sub finalize()

        RST.Close()
        MyBase.Finalize()

    End Sub


#End Region



End Class
