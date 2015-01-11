' User.vb
'
' users table CRUD model
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



Friend Class User


#Region "Instance Variables"

    ' Objects
    Private SRV As ModelServer
    Friend RST As Recordset

    ' Constants
    Friend object_is_alive As Boolean
    Private Const NB_CONNECTIONS_TRIALS = 10

#End Region


#Region "Initialize"

    Protected Friend Sub New()

        SRV = New ModelServer
        Dim i As Int32 = 0
        Dim q_result = SRV.openRst(CONFIG_DATABASE & "." & USERS_TABLE, ModelServer.DYNAMIC_CURSOR)
        While q_result = False AndAlso i < NB_CONNECTIONS_TRIALS
            q_result = SRV.openRst(CONFIG_DATABASE & "." & USERS_TABLE, ModelServer.DYNAMIC_CURSOR)
            i = i + 1
        End While
        SRV.rst.Sort = ITEMS_POSITIONS
        RST = SRV.rst
        object_is_alive = q_result

    End Sub

#End Region


#Region "CRUD Interface"

    Protected Friend Sub CreateUser(ByRef hash As Hashtable)

        Dim fieldsArray(hash.Count - 1) As Object
        Dim valuesArray(hash.Count - 1) As Object
        hash.Keys.CopyTo(fieldsArray, 0)
        hash.Values.CopyTo(valuesArray, 0)
        RST.AddNew(fieldsArray, valuesArray)

    End Sub

    Protected Friend Function ReadUser(ByRef user_id As String, ByRef field As String) As Object

        RST.Filter = USERS_ID_VARIABLE + "='" + user_id + "'"
        If RST.EOF Then Return Nothing
        Return RST.Fields(field).Value

    End Function

    Protected Friend Sub UpdateUser(ByRef user_id As String, ByRef hash As Hashtable)

        RST.Filter = USERS_ID_VARIABLE + "='" + user_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            For Each Attribute In hash.Keys
                If RST.Fields(Attribute).Value <> hash(Attribute) Then
                    RST.Fields(Attribute).Value = hash(Attribute)
                    RST.Update()
                End If
            Next
        End If

    End Sub

    Protected Friend Sub UpdateUser(ByRef user_id As String, _
                          ByRef field As String, _
                          ByVal value As Object)

        RST.Filter = USERS_ID_VARIABLE + "='" + user_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            If RST.Fields(field).Value <> value Then
                RST.Fields(field).Value = value
                RST.Update()
            End If
        End If

    End Sub

    Protected Friend Sub DeleteUser(ByRef user_id As String)

        RST.Filter = USERS_ID_VARIABLE + "='" + user_id + "'"
        If RST.EOF = False Then
            RST.Delete()
            RST.Update()
        End If

    End Sub


#End Region


#Region "Utilities"

    ' Users->DB Manager -> should only see users within his credential level's scope
    ' !! Send the sql_query like in entities credential filter
    Protected Friend Shared Sub LoadUsersTree(ByRef TV As TreeView, _
                                    Optional ByRef strSqlQuery As String = "")

        Dim srv As New ModelServer
        Dim q_result As Boolean
        If strSqlQuery <> "" Then
            q_result = srv.openRstSQL("SELECT * FROM " + CONFIG_DATABASE + "." + USERS_TABLE + " WHERE " + strSqlQuery, ModelServer.FWD_CURSOR)
        Else
            q_result = srv.openRst(CONFIG_DATABASE & "." & USERS_TABLE, ModelServer.FWD_CURSOR)
        End If
        If q_result = True Then

            Dim currentNode, ParentNode() As TreeNode
            TV.Nodes.Clear()
            srv.rst.Sort = ITEMS_POSITIONS

            If srv.rst.RecordCount > 0 Then
                srv.rst.MoveFirst()

                Do While srv.rst.EOF = False

                    If IsDBNull(srv.rst.Fields(USERS_PARENT_ID_VARIABLE).Value) Then
                        currentNode = TV.Nodes.Add(Trim(srv.rst.Fields(USERS_ID_VARIABLE).Value), _
                                                   Trim(srv.rst.Fields(USERS_ID_VARIABLE).Value))
                    Else
                        ParentNode = TV.Nodes.Find(Trim(srv.rst.Fields(USERS_PARENT_ID_VARIABLE).Value), True)
                        If ParentNode.Length = 0 Then
                            currentNode = TV.Nodes.Add(Trim(srv.rst.Fields(USERS_ID_VARIABLE).Value), _
                                                       Trim(srv.rst.Fields(USERS_ID_VARIABLE).Value))
                        Else
                            currentNode = ParentNode(0).Nodes.Add(Trim(srv.rst.Fields(USERS_ID_VARIABLE).Value), _
                                                                  Trim(srv.rst.Fields(USERS_ID_VARIABLE).Value))
                        End If
                    End If
                    currentNode.EnsureVisible()
                    currentNode.StateImageIndex = Trim(srv.rst.Fields(USERS_IS_FOLDER_VARIABLE).Value)
                    currentNode.SelectedImageIndex = Trim(srv.rst.Fields(USERS_IS_FOLDER_VARIABLE).Value)
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
