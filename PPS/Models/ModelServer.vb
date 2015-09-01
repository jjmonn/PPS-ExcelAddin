' ModelServer.vb
'
' Executes sql queries and open RSTs
'
' To do:
'       - Track server deconection error
'       - to be deleted -> priority high
'
' Known bugs:
'       - 
'
'
'
' Last modified: 05/01/2015
' Author: Julien Monnereau


Imports ADODB


Friend Class ModelServer


#Region "Instance Variables"

    ' Objects
    Friend cmd As Command
    Public rst As Recordset
    Private adoRS As Object

    ' Variable
    Private ErrorMessage As String

    ' Constants
    Public Const FWD_CURSOR As Int32 = 0
    Public Const STATIC_CURSOR As Int32 = 1
    Public Const KEYSET As Int32 = 2
    Public Const DYNAMIC_CURSOR As Int32 = 3
    Friend Const NB_QUERIES_ATTEMPS As Int32 = 10


#End Region


    Protected Friend Sub New()

        'rst = New Recordset
        'rst.CursorLocation = CursorLocationEnum.adUseClient

        'cmd = New Command
        'cmd.ActiveConnection = GlobalVariables.Connection
        'cmd.CommandType = CommandTypeEnum.adCmdText
        '' adLockOptimistic


    End Sub


#Region "Interface"

    Protected Friend Function OpenRst(strSQL As String, _
                                      ByRef cursor_option As Int32, _
                                      Optional ByRef lock_type As LockTypeEnum = Nothing) As Boolean

        If lock_type = Nothing Then lock_type = LockTypeEnum.adLockOptimistic
        Dim nb_attemps As Int32 = 0
        Dim success As Boolean
        While success = False _
        AndAlso nb_attemps <= NB_QUERIES_ATTEMPS
            success = RstTableConnection(strSQL, cursor_option, lock_type)
            nb_attemps = nb_attemps + 1
        End While

        If success = False Then
            MsgBox(ErrorMessage)
            Return False
        Else
            ErrorMessage = ""
            Return True
        End If

    End Function

    Protected Friend Function openRstSQL(sqlQuery As String, _
                                         ByRef cursor_option As Int32, _
                                         Optional ByRef lock_type As LockTypeEnum = Nothing) As Boolean

        If lock_type = Nothing Then lock_type = LockTypeEnum.adLockOptimistic
        Dim nb_attemps As Int32 = 0
        Dim success As Boolean
        While success = False AndAlso nb_attemps <= NB_QUERIES_ATTEMPS
            success = RstQueryConnection(sqlQuery, cursor_option, lock_type)
            nb_attemps = nb_attemps + 1
        End While

        If success = False Then
            MsgBox(ErrorMessage)
            Return False
        Else
            ErrorMessage = ""
            Return True
        End If

    End Function

    Protected Friend Function sqlQuery(strSQL As String) As Boolean

        Dim nb_attemps As Int32 = 0
        Dim success As Boolean
        While success = False AndAlso nb_attemps <= NB_QUERIES_ATTEMPS
            success = Query(strSQL)
            nb_attemps = nb_attemps + 1
        End While

        If success = False Then
            MsgBox(ErrorMessage)
            Return False
        Else
            ErrorMessage = ""
            Return True
        End If

    End Function

#End Region


#Region "Connection Queries"

    Private Function RstTableConnection(strSQL As String, _
                                       ByRef cursor_option As Int32, _
                                       ByRef lock_type As LockTypeEnum) As Boolean

        '        On Error GoTo queryError
        '        rst.Open(strSQL, _
        '                 GlobalVariables.Connection, _
        '                 GetCursor(cursor_option), _
        '                 lock_type, _
        '                 CommandTypeEnum.adCmdTable)
        '        Return True
        '        Exit Function

        'queryError:
        '        ErrorMessage = Err.Description
        '        Return False

    End Function

    Private Function RstQueryConnection(ByRef sqlQuery As String, _
                                        ByRef cursor_option As Int32, _
                                        ByRef lock_type As LockTypeEnum) As Boolean

        '        On Error GoTo queryError
        '        rst.Open(sqlQuery, _
        '                 GlobalVariables.Connection, _
        '                 GetCursor(cursor_option), _
        '                 lock_type, _
        '                 ADODB.CommandTypeEnum.adCmdText)
        '        Return True
        '        Exit Function

        'queryError:
        '        ErrorMessage = Err.Description
        '        Return False

    End Function

    Private Function Query(ByRef sqlQuery As String) As Boolean

        On Error GoTo queryError
        cmd.CommandText = sqlQuery
        adoRS = cmd.Execute()
        Return True
        Exit Function

queryError:
        ErrorMessage = Err.Description
        Return False

    End Function

#End Region


#Region "Utilities"

    Protected Friend Sub CloseRst()

        rst.Close()

    End Sub

    Private Function GetCursor(ByRef option_ As Int32) As ADODB.CursorTypeEnum

        Dim cursor As ADODB.CursorTypeEnum
        Select Case option_
            Case 0 : cursor = ADODB.CursorTypeEnum.adOpenForwardOnly
            Case 1 : cursor = ADODB.CursorTypeEnum.adOpenStatic
            Case 2 : cursor = ADODB.CursorTypeEnum.adOpenKeyset
            Case 3 : cursor = ADODB.CursorTypeEnum.adOpenDynamic
        End Select
        Return cursor

    End Function

#End Region


End Class
