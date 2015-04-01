' Category.vb
'
' categories table CRUD model
'
' To do: 
'
'
'
'
' Author: Julien Monnereau
' Last modified: 24/03/2015


Imports ADODB
Imports System.Windows.Forms
Imports System.Collections


Friend Class Category


#Region "Instance Variables"

    ' Objects
    Private SRV As ModelServer
    Friend RST As Recordset

    ' Constants
    Friend object_is_alive As Boolean

#End Region


#Region "Initialize"

    Protected Friend Sub New()

        SRV = New ModelServer
        Dim i As Int32 = 0
        object_is_alive = SRV.OpenRst(CONFIG_DATABASE & "." & CATEGORIES_TABLE_NAME, ModelServer.DYNAMIC_CURSOR)
        RST = SRV.rst

    End Sub

#End Region


#Region "CRUD Interface"

    Protected Friend Sub CreateCategory(ByRef hash As Hashtable)

        Dim fieldsArray(hash.Count - 1) As Object
        Dim valuesArray(hash.Count - 1) As Object
        hash.Keys.CopyTo(fieldsArray, 0)
        hash.Values.CopyTo(valuesArray, 0)
        RST.AddNew(fieldsArray, valuesArray)

    End Sub

    Protected Friend Function ReadCategory(ByRef category_id As String, ByRef field As String) As Object

        RST.Filter = CATEGORY_ID_VARIABLE + "='" + category_id + "'"
        If RST.EOF Then Return Nothing
        Return RST.Fields(field).Value

    End Function

    Protected Friend Sub UpdateCategory(ByRef category_id As String, ByRef hash As Hashtable)

        RST.Filter = CATEGORY_ID_VARIABLE + "='" + category_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            For Each Attribute In hash.Keys
                If RST.Fields(Attribute).Value <> hash(Attribute) Then
                    RST.Fields(Attribute).Value = hash(Attribute)
                    RST.Update()
                End If
            Next
        End If

    End Sub

    Protected Friend Sub UpdateCategory(ByRef category_id As String, _
                              ByRef field As String, _
                              ByVal value As Object)

        RST.Filter = CATEGORY_ID_VARIABLE + "='" + category_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            If RST.Fields(field).Value <> value Then
                RST.Fields(field).Value = value
                RST.Update()
            End If
        End If

    End Sub

    Protected Friend Sub DeleteCategory(ByRef category_id As String)

        RST.Filter = CATEGORY_ID_VARIABLE + "='" + category_id + "'"
        If RST.EOF = False Then
            RST.Delete()
            RST.Update()
        End If

    End Sub


#End Region


#Region "Utilities"

    Protected Friend Shared Sub LoadCategoryCodeTV(ByRef TV As TreeView, ByRef code As String)

        Dim srv As New ModelServer
        If srv.OpenRst(CONFIG_DATABASE & "." & CATEGORIES_TABLE_NAME, ModelServer.FWD_CURSOR) Then
            srv.rst.Filter = CATEGORY_CODE_VARIABLE & "='" & code & "'"

            Dim currentNode, ParentNode() As TreeNode
            TV.Nodes.Clear()
            srv.rst.Sort = ITEMS_POSITIONS

            Do While srv.rst.EOF = False
                If IsDBNull(srv.rst.Fields(CATEGORY_PARENT_ID_VARIABLE).Value) Then
                    currentNode = TV.Nodes.Add(Trim(srv.rst.Fields(CATEGORY_ID_VARIABLE).Value), _
                                                Trim(srv.rst.Fields(CATEGORY_NAME_VARIABLE).Value), 1, 1)
                Else
                    ParentNode = TV.Nodes.Find(Trim(srv.rst.Fields(CATEGORY_PARENT_ID_VARIABLE).Value), True)
                    If ParentNode.Length = 0 Then
                        currentNode = TV.Nodes.Add(Trim(srv.rst.Fields(CATEGORY_ID_VARIABLE).Value), _
                        Trim(srv.rst.Fields(CATEGORY_NAME_VARIABLE).Value), 1, 1)
                    Else
                        currentNode = ParentNode(0).Nodes.Add(Trim(srv.rst.Fields(CATEGORY_ID_VARIABLE).Value), _
                                                            Trim(srv.rst.Fields(CATEGORY_NAME_VARIABLE).Value), 0, 0)
                    End If
                End If
                currentNode.Checked = True
                srv.rst.MoveNext()
            Loop
            srv.rst.Close()
        End If

    End Sub

    Protected Friend Shared Function GetCategoryCodeNode(ByRef code As String) As TreeNode

        Dim category_node As New TreeNode
        Dim srv As New ModelServer
        If srv.OpenRst(CONFIG_DATABASE & "." & CATEGORIES_TABLE_NAME, ModelServer.FWD_CURSOR) Then
            srv.rst.Filter = CATEGORY_CODE_VARIABLE & "='" & code & "'"

            Dim currentNode, ParentNode() As TreeNode
            srv.rst.Sort = ITEMS_POSITIONS
            Do While srv.rst.EOF = False

                If IsDBNull(srv.rst.Fields(CATEGORY_PARENT_ID_VARIABLE).Value) Then
                    currentNode = category_node.Nodes.Add(Trim(srv.rst.Fields(CATEGORY_ID_VARIABLE).Value), _
                                               Trim(srv.rst.Fields(CATEGORY_NAME_VARIABLE).Value))
                Else
                    ParentNode = category_node.Nodes.Find(Trim(srv.rst.Fields(CATEGORY_PARENT_ID_VARIABLE).Value), True)
                    If ParentNode.Length = 0 Then
                        currentNode = category_node.Nodes.Add(Trim(srv.rst.Fields(CATEGORY_ID_VARIABLE).Value), _
                       Trim(srv.rst.Fields(CATEGORY_NAME_VARIABLE).Value))
                    Else
                        currentNode = ParentNode(0).Nodes.Add(Trim(srv.rst.Fields(CATEGORY_ID_VARIABLE).Value), _
                                                         Trim(srv.rst.Fields(CATEGORY_NAME_VARIABLE).Value))
                    End If
                End If
                currentNode.Checked = True
                srv.rst.MoveNext()
            Loop
            srv.rst.Close()
        End If
        Return category_node

    End Function

    Protected Overrides Sub finalize()

        RST.Close()
        MyBase.Finalize()

    End Sub

#End Region



End Class
