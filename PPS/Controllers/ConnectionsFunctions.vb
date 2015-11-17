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
' Last modified: 31/08/2015
' Author: Julien Monnereau



Friend Class ConnectionsFunctions


#Region "Instance Variables"

    ' Variables
    Public Event ConnectionFailedEvent()
    Private userName As String
    Public Shared pwd As String
    Private Shared connectionFailed As Boolean

    ' Flags
    Friend globalInitFlag As Boolean = False
    Friend globalAuthenticated As Boolean = False

    Private globalVariablesInitFlags As New Collections.Generic.Dictionary(Of GlobalEnums.GlobalModels, Boolean)

#End Region


    Friend Function NetworkConnection(ByRef p_hostname As String, _
                                      ByVal p_port As UShort, _
                                      ByRef p_userName As String, _
                                      ByVal p_pwd As String) As Boolean

        userName = p_userName
        pwd = p_pwd
        globalVariablesInitFlags.Clear()
        globalInitFlag = False
        globalAuthenticated = False
        InitializeGlobalModels()
        If GlobalVariables.ConnectionState = True Then
            CloseNetworkConnection()
        End If
        GlobalVariables.NetworkConnect = New NetworkLauncher()
        GlobalVariables.ConnectionState = GlobalVariables.NetworkConnect.Launch(p_hostname, p_port, _
                                                                                Sub()
                                                                                    System.Diagnostics.Debug.WriteLine("Connection to server lost, attempt reconnection")
                                                                                    Dim failed As Boolean = True
                                                                                    Dim nbTry As Int32 = 10
                                                                                    AddinModule.DisplayConnectionStatus(False)
                                                                                    While failed And nbTry > 0
                                                                                        System.Threading.Thread.Sleep(3000)
                                                                                        ConnectionsFunctions.Connect(Me, failed, userName, pwd, False)
                                                                                        nbTry -= 1
                                                                                    End While
                                                                                    If failed Then MsgBox("Connection to server lost")
                                                                                    If Not failed Then AddinModule.DisplayConnectionStatus(True)

                                                                                End Sub)

        If GlobalVariables.ConnectionState = True Then
            ' request auth token
            NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_AUTH_REQUEST_ANSWER, AddressOf SMSG_AUTH_REQUEST_ANSWER)
            Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_AUTH_REQUEST, UShort))
            packet.Release()
            NetworkManager.GetInstance().Send(packet)
            System.Diagnostics.Debug.WriteLine("Authtoken requested")
            Return True
        Else
            CloseNetworkConnection()
            System.Diagnostics.Debug.WriteLine("The system did not respond")
            Return False
        End If

    End Function

    Private Sub SMSG_AUTH_REQUEST_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then

            Dim authToken As String = packet.ReadString()
            NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_AUTH_ANSWER, AddressOf SMSG_AUTH_ANSWER)
            Dim answer As New ByteBuffer(CType(ClientMessage.CMSG_AUTHENTIFICATION, UShort))
            answer.WriteString(userName)
            answer.WriteString(GeneralUtilities.getSHA1Hash(GeneralUtilities.getSHA1Hash(pwd & userName) & authToken))
            answer.Release()

            NetworkManager.GetInstance().Send(answer)
            System.Diagnostics.Debug.WriteLine("Authentication asked")
        Else
            System.Diagnostics.Debug.WriteLine("The server did not reply to the authentication request.")
            CloseNetworkConnection()
            RaiseEvent ConnectionFailedEvent()
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_AUTH_REQUEST_ANSWER, AddressOf SMSG_AUTH_REQUEST_ANSWER)

    End Sub

    Private Sub SMSG_AUTH_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            If packet.ReadBool() = True Then
                System.Diagnostics.Debug.WriteLine("Authentication succeeded")
                GlobalVariables.Users.currentUserName = userName
                globalAuthenticated = True
            Else
                globalInitFlag = True
                System.Diagnostics.Debug.WriteLine("Authentication Failed!")
                CloseNetworkConnection()
                '    MsgBox("Authentication failed. Please review your ID and password.")
                RaiseEvent ConnectionFailedEvent()
            End If
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_AUTH_ANSWER, AddressOf SMSG_AUTH_ANSWER)

    End Sub

    Private Sub InitializeGlobalModels()

        AddHandler GlobalVariables.AxisElems.ObjectInitialized, AddressOf AfterAxisElemInit
        AddHandler GlobalVariables.AxisFilters.ObjectInitialized, AddressOf AfterAxisFilterInit
        AddHandler GlobalVariables.FModelingsAccounts.ObjectInitialized, AddressOf AfterFModelingAccountsInit
        AddHandler GlobalVariables.Accounts.ObjectInitialized, AddressOf AfterAccountsInit
        AddHandler GlobalVariables.EntityCurrencies.ObjectInitialized, AddressOf AfterEntityCurrenciesInit
        AddHandler GlobalVariables.Filters.ObjectInitialized, AddressOf AfterFiltersInit
        AddHandler GlobalVariables.FiltersValues.ObjectInitialized, AddressOf AfterFiltersValuesInit
        AddHandler GlobalVariables.Versions.ObjectInitialized, AddressOf AfterFactsVersionsInit
        AddHandler GlobalVariables.Currencies.ObjectInitialized, AddressOf AfterCurrenciesInit
        AddHandler GlobalVariables.RatesVersions.ObjectInitialized, AddressOf AfterRatesVersionsInit
        AddHandler GlobalVariables.GlobalFacts.ObjectInitialized, AddressOf AfterGlobalFactsInit
        AddHandler GlobalVariables.GlobalFactsDatas.ObjectInitialized, AddressOf AfterGlobalFactsDataInit
        AddHandler GlobalVariables.GlobalFactsVersions.ObjectInitialized, AddressOf AfterGlobalFactsVersionInit
        AddHandler GlobalVariables.Users.ObjectInitialized, AddressOf AfterUserInit
        AddHandler GlobalVariables.Groups.ObjectInitialized, AddressOf AfterGroupInit
        AddHandler GlobalVariables.GroupAllowedEntities.ObjectInitialized, AddressOf AfterGroupAllowedEntityInit
        AddHandler GlobalVariables.EntityDistribution.ObjectInitialized, AddressOf AfterEntityDistributionInit

        globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.ACCOUNTS, False)
        globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.ENTITYCURRENCY, False)
        globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.FILTERS, False)
        globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.FILTERSVALUES, False)
        globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.FACTSVERSIONS, False)
        globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.CURRENCIES, False)
        globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.RATESVERSIONS, False)
        globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.GLOBALFACT, False)
        globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.GLOBALFACTSDATA, False)
        globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.GLOBALFACTSVERSION, False)
        globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.USER, False)
        globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.GROUP, False)
        globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.GROUPALLOWEDENTITY, False)
        globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.FMODELINGACCOUNT, False)
        globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.AXIS_ELEM, False)
        globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.AXIS_FILTER, False)
        globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.ENTITYDISTRIBUTION, False)

    End Sub

    Friend Shared Sub Connect(ByRef connectionFunction As ConnectionsFunctions, ByRef connectionFailed As Boolean, _
                              ByRef id As String, ByRef pwd As String, Optional verbose As Boolean = True)
        Dim start_time As Date
        Dim secs As Single
        connectionFailed = False

        If connectionFunction.NetworkConnection(My.Settings.serverIp, _
                                                My.Settings.port_number, _
                                                id, _
                                                pwd) = True Then

            start_time = Now
            Do While connectionFunction.globalInitFlag = False
                secs = DateDiff("s", start_time, Now)
                If secs > 6 Then Exit Do
            Loop
        Else
            ConnectionsFunctions.CloseNetworkConnection()
        End If
        If connectionFunction.globalAuthenticated = False Then
            If verbose Then MsgBox("Connection failed")
            connectionFailed = True
        End If
    End Sub

    Friend Shared Sub CloseNetworkConnection()

        On Error Resume Next
        GlobalVariables.NetworkConnect.Stop()
        GlobalVariables.Connection_Toggle_Button.Image = 0
        GlobalVariables.Connection_Toggle_Button.Caption = "Not connected"
        GlobalVariables.ConnectionState = True

    End Sub


#Region "Call backs global variables (CRUDs) Init"

    Private Sub AfterAxisFilterInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.AXIS_FILTER) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()

    End Sub

    Private Sub AfterAxisElemInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.AXIS_ELEM) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()

    End Sub

    Private Sub AfterFModelingAccountsInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.FMODELINGACCOUNT) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()

    End Sub

    Private Sub AfterAccountsInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.ACCOUNTS) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()

    End Sub

    Private Sub AfterEntityCurrenciesInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.ENTITYCURRENCY) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()

    End Sub

    Private Sub AfterFiltersInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.FILTERS) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()

    End Sub

    Private Sub AfterFiltersValuesInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.FILTERSVALUES) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()

    End Sub

    Private Sub AfterFactsVersionsInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.FACTSVERSIONS) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()

    End Sub

    Private Sub AfterCurrenciesInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.CURRENCIES) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()

    End Sub

    Private Sub AfterRatesVersionsInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.RATESVERSIONS) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()

    End Sub

    Private Sub AfterGlobalFactsInit()
        globalVariablesInitFlags(GlobalEnums.GlobalModels.GLOBALFACT) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()
    End Sub

    Private Sub AfterGlobalFactsDataInit()
        globalVariablesInitFlags(GlobalEnums.GlobalModels.GLOBALFACTSDATA) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()
    End Sub

    Private Sub AfterGlobalFactsVersionInit()
        globalVariablesInitFlags(GlobalEnums.GlobalModels.GLOBALFACTSVERSION) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()
    End Sub

    Private Sub AfterUserInit()
        globalVariablesInitFlags(GlobalEnums.GlobalModels.USER) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()
    End Sub

    Private Sub AfterGroupInit()
        globalVariablesInitFlags(GlobalEnums.GlobalModels.GROUP) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()
    End Sub

    Private Sub AfterGroupAllowedEntityInit()
        globalVariablesInitFlags(GlobalEnums.GlobalModels.GROUPALLOWEDENTITY) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()
    End Sub

    Private Sub AfterEntityDistributionInit()
        globalVariablesInitFlags(GlobalEnums.GlobalModels.ENTITYDISTRIBUTION) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()
    End Sub
#End Region


#Region "Utils"

    Private Function CheckGlobalVariablesInitFlag() As Boolean

        For Each value As Boolean In globalVariablesInitFlags.Values
            If value = False Then Return False
        Next
        GlobalVariables.AuthenticationFlag = True
        System.Diagnostics.Debug.WriteLine("Global variables initialized")
        Return True

    End Function


#End Region


End Class

