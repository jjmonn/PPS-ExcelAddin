' Entity.vb
'
' Entities table CRUD model
'
' To do: 
'
'
'
'
' Author: Julien Monnereau
' Last modified: 08/12/2014


Imports ADODB
Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic


Friend Class Entity


#Region "Instance Variables"

    ' Objects
    Private SRV As New ModelServer
    Private RST As Recordset

    ' Constants
    Friend object_is_alive As Boolean
    Private Const NB_CONNECTIONS_TRIALS = 10

#End Region


#Region "Initialize"

    Protected Friend Sub New()

        Dim i As Int32 = 0
        Dim q_result = SRV.openRst(LEGAL_ENTITIES_DATABASE & "." & ENTITIES_TABLE, ModelServer.DYNAMIC_CURSOR)
        While q_result = False AndAlso i < NB_CONNECTIONS_TRIALS
            q_result = SRV.openRst(LEGAL_ENTITIES_DATABASE & "." & ENTITIES_TABLE, ModelServer.DYNAMIC_CURSOR)
            i = i + 1
        End While
        SRV.rst.Sort = ITEMS_POSITIONS
        RST = SRV.rst
        object_is_alive = q_result

    End Sub

#End Region


#Region "CRUD Interface"

    Protected Friend Sub CreateEntity(ByRef hash As Hashtable)

        Dim fieldsArray(hash.Count - 1) As Object
        Dim valuesArray(hash.Count - 1) As Object
        hash.Keys.CopyTo(fieldsArray, 0)
        hash.Values.CopyTo(valuesArray, 0)
        RST.AddNew(fieldsArray, valuesArray)

    End Sub

    Protected Friend Function ReadEntity(ByRef entity_id As String, ByRef field As String) As Object

        RST.Filter = ASSETS_TREE_ID_VARIABLE + "='" + entity_id + "'"
        If RST.EOF Then Return Nothing
        Return RST.Fields(field).Value

    End Function

    Protected Friend Function GetRecord(ByRef entity_id As String, ByRef categoriesTV As TreeView) As Hashtable

        RST.Filter = ASSETS_TREE_ID_VARIABLE + "='" + entity_id + "'"
        If RST.EOF Then Return Nothing
        Dim hash As New Hashtable
        hash.Add(ASSETS_NAME_VARIABLE, RST.Fields(ASSETS_NAME_VARIABLE).Value)
        hash.Add(ASSETS_CURRENCY_VARIABLE, RST.Fields(ASSETS_CURRENCY_VARIABLE).Value)
        hash.Add(ASSETS_ALLOW_EDITION_VARIABLE, RST.Fields(ASSETS_ALLOW_EDITION_VARIABLE).Value)
        For Each category_node In categoriesTV.Nodes
            hash.Add(category_node.name, RST.Fields(category_node.name).Value)
        Next
        Return hash

    End Function

    Protected Friend Sub UpdateEntity(ByRef entity_id As String, ByRef hash As Hashtable)

        RST.Filter = ASSETS_TREE_ID_VARIABLE + "='" + entity_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            For Each Attribute In hash.Keys
                If RST.Fields(Attribute).Value <> hash(Attribute) Then
                    RST.Fields(Attribute).Value = hash(Attribute)
                    RST.Update()
                End If
            Next
        End If

    End Sub

    Protected Friend Sub UpdateEntity(ByRef entity_id As String, _
                             ByRef field As String, _
                             ByVal value As Object)

        RST.Filter = ASSETS_TREE_ID_VARIABLE + "='" + entity_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            If RST.Fields(field).Value <> value Then
                RST.Fields(field).Value = value
                RST.Update()
            End If
        End If

    End Sub

    Protected Friend Sub DeleteEntity(ByRef entity_id As String)

        RST.Filter = ASSETS_TREE_ID_VARIABLE + "='" + entity_id + "'"
        If RST.EOF = False Then
            RST.Delete()
            RST.Update()
        End If

    End Sub

#End Region


#Region "Utilities"

    Protected Friend Shared Sub LoadEntitiesTree(ByRef TV As TreeView, Optional ByRef strSqlQuery As String = "")

        Dim srv As New ModelServer
        Dim q_result As Boolean
        If strSqlQuery <> "" Then
            q_result = srv.openRstSQL("SELECT * FROM " + VIEWS_DATABASE + "." + GlobalVariables.Entities_View + " WHERE " + strSqlQuery, ModelServer.FWD_CURSOR)
        Else
            q_result = srv.openRst(VIEWS_DATABASE & "." & GlobalVariables.Entities_View, ModelServer.FWD_CURSOR)
        End If
        If q_result = True Then

            Dim currentNode, ParentNode() As TreeNode
            TV.Nodes.Clear()
            srv.rst.Sort = ITEMS_POSITIONS

            If srv.rst.RecordCount > 0 Then
                srv.rst.MoveFirst()

                Do While srv.rst.EOF = False

                    Dim image_index As Int32 = srv.rst.Fields(ASSETS_ALLOW_EDITION_VARIABLE).Value

                    If IsDBNull(srv.rst.Fields(ASSETS_PARENT_ID_VARIABLE).Value) Then
                        currentNode = TV.Nodes.Add(Trim(srv.rst.Fields(ASSETS_TREE_ID_VARIABLE).Value), _
                                                   Trim(srv.rst.Fields(ASSETS_NAME_VARIABLE).Value), _
                                                   image_index, image_index)
                    Else

                        ParentNode = TV.Nodes.Find(Trim(srv.rst.Fields(ASSETS_PARENT_ID_VARIABLE).Value), True)
                        If ParentNode.Length = 0 Then
                            currentNode = TV.Nodes.Add(Trim(srv.rst.Fields(ASSETS_TREE_ID_VARIABLE).Value), _
                                                       Trim(srv.rst.Fields(ASSETS_NAME_VARIABLE).Value), _
                                                      image_index, image_index)
                        Else
                            currentNode = ParentNode(0).Nodes.Add(Trim(srv.rst.Fields(ASSETS_TREE_ID_VARIABLE).Value), _
                                                                  Trim(srv.rst.Fields(ASSETS_NAME_VARIABLE).Value), _
                                                                  image_index, image_index)
                        End If
                    End If
                    currentNode.EnsureVisible()
                    srv.rst.MoveNext()
                Loop
            End If
            srv.rst.Close()
        End If

    End Sub

    Protected Friend Shared Sub LoadEntitiesTree(ByRef TV As TreeView, _
                                                 ByRef nodes_icon_dic As Dictionary(Of String, Int32))

        Dim srv As New ModelServer
        Dim q_result As Boolean
        q_result = srv.openRst(VIEWS_DATABASE & "." & GlobalVariables.Entities_View, ModelServer.FWD_CURSOR)
        If q_result = True Then

            Dim currentNode, ParentNode() As TreeNode
            TV.Nodes.Clear()
            srv.rst.Sort = ITEMS_POSITIONS

            If srv.rst.RecordCount > 0 Then
                srv.rst.MoveFirst()

                Do While srv.rst.EOF = False

                    Dim image_index As Int32 = nodes_icon_dic(srv.rst.Fields(ASSETS_TREE_ID_VARIABLE).Value)

                    If IsDBNull(srv.rst.Fields(ASSETS_PARENT_ID_VARIABLE).Value) Then
                        currentNode = TV.Nodes.Add(Trim(srv.rst.Fields(ASSETS_TREE_ID_VARIABLE).Value), _
                                                   Trim(srv.rst.Fields(ASSETS_NAME_VARIABLE).Value), _
                                                   image_index, image_index)
                    Else

                        ParentNode = TV.Nodes.Find(Trim(srv.rst.Fields(ASSETS_PARENT_ID_VARIABLE).Value), True)
                        If ParentNode.Length = 0 Then
                            currentNode = TV.Nodes.Add(Trim(srv.rst.Fields(ASSETS_TREE_ID_VARIABLE).Value), _
                                                       Trim(srv.rst.Fields(ASSETS_NAME_VARIABLE).Value), _
                                                       image_index, image_index)
                        Else
                            currentNode = ParentNode(0).Nodes.Add(Trim(srv.rst.Fields(ASSETS_TREE_ID_VARIABLE).Value), _
                                                                  Trim(srv.rst.Fields(ASSETS_NAME_VARIABLE).Value), _
                                                                  image_index, image_index)
                        End If
                    End If
                    currentNode.EnsureVisible()
                    srv.rst.MoveNext()
                Loop
            End If
            srv.rst.Close()
        End If

    End Sub

    Protected Friend Shared Sub LoadEntitiesCredentialTree(ByRef TV As TreeView)

        Dim nodeX, ParentNode() As Windows.Forms.TreeNode
        Dim srv As New ModelServer

        TV.Nodes.Clear()
        srv.openRst(VIEWS_DATABASE + "." + GlobalVariables.Entities_View, ModelServer.FWD_CURSOR)
        srv.rst.Sort = ITEMS_POSITIONS

        If srv.rst.RecordCount > 0 Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False

                If IsDBNull(srv.rst.Fields(ASSETS_PARENT_ID_VARIABLE).Value) Then
                    nodeX = TV.Nodes.Add(Trim(srv.rst.Fields(ASSETS_TREE_ID_VARIABLE).Value), _
                                         Trim(srv.rst.Fields(ASSETS_CREDENTIAL_ID_VARIABLE).Value))
                Else
                    ParentNode = TV.Nodes.Find(Trim(srv.rst.Fields(ASSETS_PARENT_ID_VARIABLE).Value), True)
                    If ParentNode.Length = 0 Then
                        nodeX = TV.Nodes.Add(Trim(srv.rst.Fields(ASSETS_TREE_ID_VARIABLE).Value), _
                                             Trim(srv.rst.Fields(ASSETS_CREDENTIAL_ID_VARIABLE).Value))
                    Else
                        nodeX = ParentNode(0).Nodes.Add(Trim(srv.rst.Fields(ASSETS_TREE_ID_VARIABLE).Value), _
                                                        Trim(srv.rst.Fields(ASSETS_CREDENTIAL_ID_VARIABLE).Value))
                    End If
                End If
                nodeX.EnsureVisible()

                srv.rst.MoveNext()
            Loop
        End If
        srv.rst.Close()
        srv = Nothing

    End Sub

    Protected Friend Shared Sub UpdateEntitiesCredentialLevels(ByRef entitiesIDCredentialsTV As TreeView)

        Dim srv As New ModelServer
        srv.openRst(LEGAL_ENTITIES_DATABASE + "." + ENTITIES_TABLE, ModelServer.STATIC_CURSOR)

        Dim entities_id_list = TreeViewsUtilities.GetNodesKeysList(entitiesIDCredentialsTV)
        Dim node As TreeNode
        For Each entity_id In entities_id_list

            node = entitiesIDCredentialsTV.Nodes.Find(entity_id, True)(0)
            srv.rst.Filter = ASSETS_TREE_ID_VARIABLE + "='" + entity_id + "'"
            If srv.rst.EOF = False AndAlso srv.rst.BOF = False Then
                If srv.rst.Fields(ASSETS_CREDENTIAL_ID_VARIABLE).Value <> node.Text Then
                    srv.rst.Fields(ASSETS_CREDENTIAL_ID_VARIABLE).Value = node.Text
                End If
            End If
        Next
        srv.rst.Update()
        srv.CloseRst()

    End Sub

    Protected Overrides Sub finalize()

        RST.Close()
        MyBase.Finalize()

    End Sub

#End Region



End Class
