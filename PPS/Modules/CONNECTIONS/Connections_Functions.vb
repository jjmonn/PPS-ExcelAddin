' Connections_Functions.vb
'
'
'
' to do:
'       - CONNECTION INITIALE :try ->
'       - connections messages error (idéalement trouver les codes erreurs possibles ? - put mess err
'       - 
'
'
' known bugs:
'
'
'
' Last modified: 07/07/2014
' Author: Julien Monnereau



Module Connections_Functions


    ' Creates a connection  --  Uses the Connection public variable
    Public Function OpenConnection(ByRef userID As String, ByVal pwd As String) As ADODB.Connection

        Dim connectionString As String
        pwd = pwd + SNOW_KEY
       
        connectionString = "DRIVER={" + DRIVER_NAME + "};" _
                           & "SERVER=" + SERVER_LOCATION + ";" _
                           & "PORT=3306" _
                           & "DATABASE=" + OPENING_DATABASE + ";" _
                           & "UID=" + userID + ";" _
                           & "PASSWORD=" + pwd + ";" _
                           & "SSLKEY=" & My.Settings.certificatespath & "\client-key.pem;" _
                           & "SSLCERT=" & My.Settings.certificatespath & "\client-cert.pem;" _
                           & "SSLCA=" & My.Settings.certificatespath & "\server-ca.pem;" _
                           & "Pooling=True;"

        ' C:\Users\PPS\Documents\Purple Sun\CA
        ' & "OPTION=3" _
        ' & "OPTION=" & 2 + 3 + 8 + 32 + 2048 + 16384
            GlobalVariables.Current_User_ID = userID

            Try
                Dim tmpConnection = New ADODB.Connection
                Call tmpConnection.Open(connectionString)
                Return tmpConnection

            Catch ex As Exception
                MsgBox(ex.Message)
                Return Nothing
                ' save error

            End Try


    End Function

  
    'a) MySQL ODBC 5.2 ANSI Driver case
    'connectionString = "Driver={MySQL ODBC 5.2 ANSI Driver};Server=" + server_name + ";Database=" + data_base + _
    '";Uid=" + user_ID + ";Pwd=" + pass_word + ";"

    'b) MySQL ODBC 5.1 Driver case
    'connectionString = "DRIVER={MySQL ODBC 5.1 Driver};SERVER=localhost;UID=user2;DATABASE=ACF_Config;Password=user2"
    'connectionString = "DRIVER={MySQL ODBC 5.1 Driver};SERVER=173.194.251.206UID=user2;DATABASE=ACF_Config;Password=user2;UseCompression=True"





End Module

