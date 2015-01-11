

Public Class ModelServer


    Public Sub New()
    End Sub


    Public Function openRst(strSQL As String) As ADODB.Recordset

        '--------------------------------------------------------------------------------
        ' Opens and returns a recordset
        ' Use the Connection public variable, take a table as argument
        '--------------------------------------------------------------------------------
        ' Error handler -> Where to put connections checks ? public utility function, check connection before anything
        Dim rst As New ADODB.Recordset 
        rst.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        rst.Open(strSQL, _
                      ConnectioN, _
                      ADODB.CursorTypeEnum.adOpenDynamic, _
                      ADODB.LockTypeEnum.adLockOptimistic, _
                      ADODB.CommandTypeEnum.adCmdTable)
        Return rst
        'openRst = Me.rst
        rst.Close()

    End Function


    Public Function openRstSQL(sqlQuery As String) As ADODB.Recordset

        '---------------------------------------------------------------------------
        ' Opens and returns a recordset
        ' Use the Connection public variable, take a query as argument
        '---------------------------------------------------------------------------
        Dim rst As New ADODB.Recordset
        rst.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        rst.Open(sqlQuery, _
                         ConnectioN, _
                         ADODB.CursorTypeEnum.adOpenDynamic, _
                         ADODB.LockTypeEnum.adLockOptimistic, _
                         ADODB.CommandTypeEnum.adCmdText)
        Return rst
        rst.Close()

    End Function




End Class
