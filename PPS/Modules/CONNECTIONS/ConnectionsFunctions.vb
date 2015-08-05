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
' Last modified: 05/08/2015
' Author: Julien Monnereau



Friend Class ConnectionsFunctions


#Region "Instance Variables"

    Private globalVariablesInitFlags As New Collections.Generic.Dictionary(Of UInt32, Boolean)
    Friend globalInitFlag As Boolean = False

#End Region


    Friend Function NetworkConnection(ByRef p_hostname As String, ByVal p_port As UShort) As Boolean

        globalVariablesInitFlags.clear()
        globalInitFlag = False
        GlobalVariables.NetworkConnect = New NetworkLauncher()
        Dim connection_success As Boolean = (GlobalVariables.NetworkConnect.Launch(p_hostname, p_port))

        ' below :
        ' check connection state before
        ' place init code elsewhere !!?
        ' priority normal
        If connection_success = True Then

            GlobalVariables.Accounts = New Account
            GlobalVariables.Entities = New Entity
            GlobalVariables.Filters = New Filter
            GlobalVariables.FiltersValues = New FilterValue
            GlobalVariables.Clients = New Client
            GlobalVariables.Products = New Product
            GlobalVariables.Adjustments = New Adjustment
            GlobalVariables.EntitiesFilters = New EntitiesFilter
            GlobalVariables.ClientsFilters = New ClientsFilter
            GlobalVariables.ProductsFilters = New ProductsFilter
            GlobalVariables.AdjustmentsFilters = New AdjustmentFilter
            GlobalVariables.Versions = New FactsVersion

            AddHandler GlobalVariables.Accounts.ObjectInitialized, AddressOf AfterAccountsInit
            AddHandler GlobalVariables.Entities.ObjectInitialized, AddressOf AfterEntitiesInit
            AddHandler GlobalVariables.Filters.ObjectInitialized, AddressOf AfterFiltersInit
            AddHandler GlobalVariables.FiltersValues.ObjectInitialized, AddressOf AfterFiltersValuesInit
            AddHandler GlobalVariables.Clients.ObjectInitialized, AddressOf AfterClientsInit
            AddHandler GlobalVariables.Products.ObjectInitialized, AddressOf AfterProductsInit
            AddHandler GlobalVariables.Adjustments.ObjectInitialized, AddressOf AfterAdjustmentsInit
            AddHandler GlobalVariables.EntitiesFilters.ObjectInitialized, AddressOf AfterEntitiesFiltersInit
            AddHandler GlobalVariables.ClientsFilters.ObjectInitialized, AddressOf AfterClientsFiltersInit
            AddHandler GlobalVariables.ProductsFilters.ObjectInitialized, AddressOf AfterProductsFiltersInit
            AddHandler GlobalVariables.AdjustmentsFilters.ObjectInitialized, AddressOf AfterAdjustmentsFiltersInit
            AddHandler GlobalVariables.Versions.ObjectInitialized, AddressOf AfterFactsVersionsInit

            globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.ACCOUNTS, False)
            globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.ENTITIES, False)
            globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.FILTERS, False)
            globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.FILTERSVALUES, False)
            globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.CLIENTS, False)
            globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.PRODUCTS, False)
            globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.ADJUSTMENTS, False)
            globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.ENTITIESFILTERS, False)
            globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.CLIENTSFILTERS, False)
            globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.PRODUCTSFILTERS, False)
            globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.ADJUSTMENTSFILTERS, False)
            globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.FACTSVERSIONS, False)

            Do While globalInitFlag = False

                ' waiting for all global variables to be initialized
                ' implement a timeout 
                ' Reiterate server query for variables init ?
                ' priority high !!!

            Loop
            Return True
        Else
            Return False
        End If

    End Function

    Friend Shared Sub CloseNetworkConnection()

        GlobalVariables.NetworkConnect.Stop()
        GlobalVariables.Connection_Toggle_Button.Image = 0
        GlobalVariables.Connection_Toggle_Button.Caption = "Not connected"

    End Sub


#Region "DB mysql connection"

    Friend Shared Function Connection(ByRef addin As AddinModule, _
                                   ByRef user_id As String, _
                                   ByRef pwd As String) As Boolean

        GlobalVariables.Connection = OpenConnection(user_id, pwd)
        If Not GlobalVariables.Connection Is Nothing Then

            If My.Settings.user <> user_id Then My.Settings.user = user_id
            GlobalVariables.Connection_Toggle_Button.Image = 1
            GlobalVariables.Connection_Toggle_Button.Caption = "Connected"
            addin.setUpFlag = True

            If GlobalVariables.Versions.versions_hash.ContainsKey(My.Settings.version_id) Then
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

    Friend Shared Function OpenConnection(ByRef userID As String, ByVal pwd As String) As ADODB.Connection

        Dim connectionString As String
        pwd = pwd + SNOW_KEY

        connectionString = "DRIVER={" + DRIVER_NAME + "};" _
                           & "SERVER=" + My.Settings.oldIp + ";" _
                           & "PORT=" & My.Settings.oldPort & ";" _
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

#End Region


#Region "Call backs global variables (CRUDs) Init"

    Private Sub AfterAccountsInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.ACCOUNTS) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()

    End Sub

    Private Sub AfterEntitiesInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.ENTITIES) = True
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

    Private Sub AfterClientsInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.CLIENTS) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()

    End Sub

    Private Sub AfterProductsInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.PRODUCTS) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()

    End Sub

    Private Sub AfterAdjustmentsInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.ADJUSTMENTS) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()

    End Sub

    Private Sub AfterEntitiesFiltersInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.ENTITIESFILTERS) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()

    End Sub

    Private Sub AfterClientsFiltersInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.CLIENTSFILTERS) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()

    End Sub

    Private Sub AfterProductsFiltersInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.PRODUCTSFILTERS) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()

    End Sub

    Private Sub AfterAdjustmentsFiltersInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.ADJUSTMENTSFILTERS) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()

    End Sub

    Private Sub AfterFactsVersionsInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.FACTSVERSIONS) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()

    End Sub


    Private Function CheckGlobalVariablesInitFlag() As Boolean

        For Each value As Boolean In globalVariablesInitFlags.Values
            If value = False Then Return False
        Next
        Return True

    End Function


#End Region



End Class

