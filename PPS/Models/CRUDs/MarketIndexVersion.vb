' MarketIndexVersion.vb : CRUD model for market_index_versions table
'
'
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 16/01/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports ADODB


Friend Class MarketIndexVersion


#Region "Instance Variables"

    ' Objects
    Private SRV As ModelServer
    Friend RST As Recordset

    ' Variables
    Friend object_is_alive As Boolean

#End Region


#Region "Initialize"

    Protected Friend Sub New()

        SRV = New ModelServer
        Dim q_result = SRV.OpenRst(CONFIG_DATABASE & "." & MARKET_INDEXES_VERSIONS_TABLE, ModelServer.DYNAMIC_CURSOR)
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

        RST.Filter = MARKET_INDEXES_VERSIONS_ID_VAR + "='" + version_id + "'"
        If RST.EOF Then Return Nothing
        Return RST.Fields(field).Value

    End Function

    Protected Friend Function ReadVersions() As Dictionary(Of String, Hashtable)

        Dim tmp_dic As New Dictionary(Of String, Hashtable)
        RST.Filter = ""
        RST.MoveFirst()
        While RST.EOF = False
            Dim hash As New Hashtable
            hash.Add(MARKET_INDEXES_VERSIONS_NAME_VAR, RST.Fields(MARKET_INDEXES_VERSIONS_NAME_VAR).Value)
            hash.Add(MARKET_INDEXES_VERSIONS_IS_FOLER_VAR, RST.Fields(MARKET_INDEXES_VERSIONS_IS_FOLER_VAR).Value)
            If IsDBNull(RST.Fields(MARKET_INDEXES_VERSIONS_PARENT_ID_VAR).Value) Then
                hash.Add(MARKET_INDEXES_VERSIONS_PARENT_ID_VAR, "")
            Else
                hash.Add(MARKET_INDEXES_VERSIONS_PARENT_ID_VAR, RST.Fields(MARKET_INDEXES_VERSIONS_PARENT_ID_VAR).Value)
            End If
            hash.Add(ITEMS_POSITIONS, RST.Fields(ITEMS_POSITIONS).Value)

            tmp_dic.Add(RST.Fields(MARKET_INDEXES_VERSIONS_ID_VAR).Value, hash)
            RST.MoveNext()
        End While
        Return tmp_dic

    End Function

    Protected Friend Sub UpdateVersion(ByRef version_id As String, ByRef versionAttributes As Hashtable)

        RST.Filter = MARKET_INDEXES_VERSIONS_ID_VAR + "='" + version_id + "'"
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

        RST.Filter = MARKET_INDEXES_VERSIONS_ID_VAR + "='" + version_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            If RST.Fields(field).Value <> value Then
                RST.Fields(field).Value = value
                RST.Update()
            End If
        End If

    End Sub

    Protected Friend Sub DeleteVersion(ByRef version_id As String)

        RST.Filter = MARKET_INDEXES_VERSIONS_ID_VAR + "='" + version_id + "'"
        If RST.EOF = False Then
            RST.Delete()
            RST.Update()
        End If


    End Sub

#End Region


#Region "Utilities"

    Protected Friend Shared Sub load_market_index_version_tv(ByRef TV As TreeView)

        Dim srv As New ModelServer
        srv.OpenRst(CONFIG_DATABASE & "." & MARKET_INDEXES_VERSIONS_TABLE, ModelServer.FWD_CURSOR)
        Dim rst = srv.rst
        Dim nodeX, ParentNode() As TreeNode
        TV.Nodes.Clear()
        rst.Filter = ""
        rst.Sort = ITEMS_POSITIONS
        If rst.RecordCount > 0 Then
            rst.MoveFirst()
            Do While rst.EOF = False

                If IsDBNull(rst.Fields(MARKET_INDEXES_VERSIONS_PARENT_ID_VAR).Value) Then
                    nodeX = TV.Nodes.Add(Trim(rst.Fields(MARKET_INDEXES_VERSIONS_ID_VAR).Value), _
                                         Trim(rst.Fields(MARKET_INDEXES_VERSIONS_NAME_VAR).Value), _
                                         rst.Fields(MARKET_INDEXES_VERSIONS_IS_FOLER_VAR).Value, _
                                         rst.Fields(MARKET_INDEXES_VERSIONS_IS_FOLER_VAR).Value)
                Else
                    ParentNode = TV.Nodes.Find(Trim(rst.Fields(MARKET_INDEXES_VERSIONS_PARENT_ID_VAR).Value), True)
                    nodeX = ParentNode(0).Nodes.Add(Trim(rst.Fields(MARKET_INDEXES_VERSIONS_ID_VAR).Value), _
                                                    Trim(rst.Fields(MARKET_INDEXES_VERSIONS_NAME_VAR).Value), _
                                                    rst.Fields(MARKET_INDEXES_VERSIONS_IS_FOLER_VAR).Value, _
                                                    rst.Fields(MARKET_INDEXES_VERSIONS_IS_FOLER_VAR).Value)
                End If
                nodeX.EnsureVisible()
                rst.MoveNext()
            Loop
        End If
        rst.Close()

    End Sub

    Protected Friend Shared Function GetMarketIndexesVersionsList(field As String) As List(Of String)

        Dim tmpList As New List(Of String)
        Dim srv As New ModelServer
        srv.OpenRst(CONFIG_DATABASE & "." & MARKET_INDEXES_VERSIONS_TABLE, ModelServer.FWD_CURSOR)
        Dim rst = srv.rst

        rst.Filter = MARKET_INDEXES_VERSIONS_IS_FOLER_VAR + "= 0"
        If rst.EOF = False And rst.BOF = False Then
            rst.MoveFirst()
            Do While rst.EOF = False
                tmpList.Add(rst.Fields(field).Value)
                rst.MoveNext()
            Loop
        End If

        rst.Close()
        Return tmpList

    End Function

    Protected Overrides Sub finalize()

        RST.Close()
        MyBase.Finalize()

    End Sub


#End Region



End Class
