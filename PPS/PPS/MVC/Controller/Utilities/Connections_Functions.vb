Module Connections_Functions

    ' Creates a connection
    ' Uses the Connection public variable
    '------------------------------------------------------
    Public Function OpenConnection() As ADODB.Connection

        ' Read type and location of the database, user login and password
        '=> To be done : set a reference to Microsoft ActiveX Data Objects , latest version

        'You need to set a reference to "Microsfot ActiveX Data Objects" (tools/references).

        Dim connectionString As String
        Dim server_name As String = SERVER_LOCATION
        Dim data_base As String = DATABASE
        Dim user_ID As String = USER
        Dim pwd As String = PASSWORD


        '2) Build the connection string depending on the source

        ' => To be done : detection of the mySQL driver version

        'a) MySQL ODBC 5.2 ANSI Driver case

        'connectionString = "Driver={MySQL ODBC 5.2 ANSI Driver};Server=" & server_name & ";Database=" & data_base & _
        '";Uid=" & user_ID & ";Pwd=" & pass_word & ";"

        'b) MySQL ODBC 3.51 Driver case

        connectionString = "DRIVER={MySQL ODBC 3.51 Driver};SERVER=localhost;DATABASE=test;user id=root ; password=root"
        'connectionString = "DRIVER={MySQL ODBC 3.51 Driver};SERVER=173.194.81.170;DATABASE=test;user id=root ; password=surfer"

        '-------------------------------------------------------------------------------------
        ' Create and open a new connection to the selected source
        OpenConnection = New ADODB.Connection
        Call OpenConnection.Open(connectionString)

        '-------------------------------------------------------------------------------------
        ' Create an ADODB Command
        cmd = New ADODB.Command
        cmd.ActiveConnection = Connection
        cmd.CommandType = ADODB.CommandTypeEnum.adCmdText
        'adCmdText ! Check above

    End Function
    '---------------------------------------------------------------------

    ' Function sqlQuery()
    ' Executes a SQL query to the server
    ' Returns true if everything went right, false if there has been some error
    ' Uses the cmd public variable(ADODB.command object)

    Public Function sqlQuery(strSQL As String) As Boolean

        On Error GoTo queryError

        cmd.CommandText = strSQL
        cmd.Execute()

        sqlQuery = True

        Exit Function

queryError:
        sqlQuery = False
        MsgBox(Err.Description)

    End Function
    '----------------------------------------------------------------------

    ' Function GetState()
    ' Get status of a connection function : returns open or closed
    ' Use the Connection public variable

    Public Function GetState(intState As Integer) As String

        Select Case intState
            Case 0 'adStateClosed = 0 ?
                GetState = "adStateClosed"
            Case 1 'adStateOpen = 1 ?
                GetState = "adStateOpen"
            Case Else
                GetState = "Unknown State"
        End Select

    End Function

    '---------------------------------------------------------------
    ' Function openRst()
    ' Opens and returns a recordset
    ' Use the Connection public variable
    ' Input: table name
    ' Output: RecordSet
    '---------------------------------------------------------------

    Public Function openRst(TableName As String) As ADODB.Recordset

        On Error GoTo ErrorHandler
        ' Open up a recordset
        Dim rst As ADODB.Recordset
        rst = New ADODB.Recordset
        rst.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        'adUseClient check above
        rst.Open(TableName, Connection, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1)    ' check the -1 = adcmdtable
        openRst = rst
        Exit Function

ErrorHandler:
        MsgBox(Err.Number & Err.Description)
        Exit Function

    End Function
    '-------------------------------------------------------------------------



End Module

