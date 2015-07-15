' ConnectionsFunctions.vb
'
'
' to do:
'       - connections messages error (idéalement trouver les codes erreurs possibles ? - put mess err
'       - 
'
'
' known bugs:
'
'
'
' Last modified: 26/06/2015
' Author: Julien Monnereau



Friend Class ConnectionsFunctions


    Friend Shared Function Connection(ByRef addin As AddinModule, _
                                      ByRef user_id As String, _
                                      ByRef pwd As String) As Boolean

        GlobalVariables.Connection = OpenConnection(user_id, pwd)
        If Not GlobalVariables.Connection Is Nothing Then

            GlobalVariables.GlobalDBDownloader = New DataBaseDataDownloader
            GlobalVariables.GlobalDll3Interface = New DLL3_Interface
            GlobalVariables.GenericGlobalSingleEntityComputer = New GenericSingleEntityDLL3Computer(GlobalVariables.GlobalDBDownloader, _
                                                                                                    GlobalVariables.GlobalDll3Interface)
            GlobalVariables.GenericGlobalAggregationComputer = New GenericAggregationDLL3Computing(GlobalVariables.GlobalDBDownloader, _
                                                                                                    GlobalVariables.GlobalDll3Interface)
            If My.Settings.user <> user_id Then My.Settings.user = user_id
            GlobalVariables.Connection_Toggle_Button.Image = 1
            GlobalVariables.Connection_Toggle_Button.Caption = "Connected"
            addin.setUpFlag = True

            If VersionsMapping.IsVersionValid(My.Settings.version_id) Then
                addin.SetVersion(My.Settings.version_id)
            Else
                addin.LaunchVersionSelection()
            End If
            Return True
        Else
            GlobalVariables.Connection.Close()
            GlobalVariables.Connection_Toggle_Button.Image = 0
            MsgBox("Connection failed")
            Return False
        End If

    End Function

    Friend Shared Function NetworkConnection(ByRef p_hostname As String, ByVal p_port As UShort) As Boolean
        Return (GlobalVariables.NetworkConnect.Launch(p_hostname, p_port))
    End Function

    Friend Shared Function OpenConnection(ByRef userID As String, ByVal pwd As String) As ADODB.Connection

        Dim connectionString As String
        pwd = pwd + SNOW_KEY

        connectionString = "DRIVER={" + DRIVER_NAME + "};" _
                           & "SERVER=" + My.Settings.server + ";" _
                           & "PORT=" & My.Settings.port_number & ";" _
                           & "DATABASE=" + My.Settings.database + ";" _
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

    Friend Shared Sub CloseConnection()

        GlobalVariables.Connection.Close()
        GlobalVariables.Connection = Nothing
        GlobalVariables.Connection_Toggle_Button.Image = 0
        GlobalVariables.Connection_Toggle_Button.Caption = "Not connected"

    End Sub

    Friend Shared Sub CloseNetworkConnection()
        GlobalVariables.NetworkConnect.Stop()
    End Sub

    'a) MySQL ODBC 5.2 ANSI Driver case
    'connectionString = "Driver={MySQL ODBC 5.2 ANSI Driver};Server=" + server_name + ";Database=" + data_base + _
    '";Uid=" + user_ID + ";Pwd=" + pass_word + ";"

    'b) MySQL ODBC 5.1 Driver case
    'connectionString = "DRIVER={MySQL ODBC 5.1 Driver};SERVER=localhost;UID=user2;DATABASE=ACF_Config;Password=user2"
    'connectionString = "DRIVER={MySQL ODBC 5.1 Driver};SERVER=173.194.251.206UID=user2;DATABASE=ACF_Config;Password=user2;UseCompression=True"


End Class

