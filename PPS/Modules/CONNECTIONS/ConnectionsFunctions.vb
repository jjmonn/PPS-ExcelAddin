﻿' ConnectionsFunctions.vb
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
' Last modified: 17/08/2015
' Author: Julien Monnereau



Friend Class ConnectionsFunctions


#Region "Instance Variables"

    ' Variables
    Public Event ConnectionFailedEvent()
    Private userName As String
    Private pwd As String

    ' Flags
     Friend globalInitFlag As Boolean = False
    Private globalVariablesInitFlags As New Collections.Generic.Dictionary(Of UInt32, Boolean)

#End Region


    Friend Function NetworkConnection(ByRef p_hostname As String, _
                                     ByVal p_port As UShort, _
                                     ByRef p_userName As String, _
                                     ByVal p_pwd As String) As Boolean

        userName = p_userName
        pwd = p_pwd

         globalVariablesInitFlags.Clear()
        globalInitFlag = False
        If GlobalVariables.ConnectionState = True Then
            GlobalVariables.NetworkConnect.Stop()
        End If
        GlobalVariables.NetworkConnect = New NetworkLauncher()
        GlobalVariables.ConnectionState = (GlobalVariables.NetworkConnect.Launch(p_hostname, p_port))

        If GlobalVariables.ConnectionState = True Then
            ' request auth token
            NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_AUTH_REQUEST_ANSWER, AddressOf SMSG_AUTH_REQUEST_ANSWER)
            Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_AUTH_REQUEST, UShort))
            packet.Release()
            NetworkManager.GetInstance().Send(packet)
            System.Diagnostics.Debug.WriteLine("Authtoken requested")
            Return True
        Else
            System.Diagnostics.Debug.WriteLine("The system did not respond")
            Return False
        End If

    End Function

    Private Sub SMSG_AUTH_REQUEST_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then

            Dim authToken As String = packet.ReadString()

            ' Authentication
            'MsgBox("before auth, user name: " & userName & Chr(13) & _
            '       " pwd: " & pwd & Chr(13) & _
            '       " received authtoken: " & authToken)
            System.Diagnostics.Debug.WriteLine("user: " & userName)
            System.Diagnostics.Debug.WriteLine("pwd: " & pwd)
            System.Diagnostics.Debug.WriteLine("authtoken: " & authToken)

            NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_AUTH_ANSWER, AddressOf SMSG_AUTH_ANSWER)
            Dim answer As New ByteBuffer(CType(ClientMessage.CMSG_AUTHENTIFICATION, UShort))
            answer.WriteString(userName)
            answer.WriteString(Utilities_Functions.getSHA1Hash(Utilities_Functions.getSHA1Hash(pwd & userName) & authToken))
            answer.Release()
            NetworkManager.GetInstance().Send(answer)
            System.Diagnostics.Debug.WriteLine("Authentication asked")
        Else
            System.Diagnostics.Debug.WriteLine("The server did not reply to the authentication request.")
            RaiseEvent ConnectionFailedEvent()
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_AUTH_REQUEST_ANSWER, AddressOf SMSG_AUTH_REQUEST_ANSWER)

    End Sub

    Private Sub SMSG_AUTH_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            If packet.ReadBool() = True Then
                InitializeGlobalModels()
                System.Diagnostics.Debug.WriteLine("Authentication suceed")
            Else
                System.Diagnostics.Debug.WriteLine("Authentication Failed!")
                '    MsgBox("Authentication failed. Please review your ID and password.")
                RaiseEvent ConnectionFailedEvent()
            End If
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_AUTH_ANSWER, AddressOf SMSG_AUTH_ANSWER)

    End Sub

    Private Sub InitializeGlobalModels()

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
        GlobalVariables.Currencies = New Currency

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
        AddHandler GlobalVariables.Currencies.ObjectInitialized, AddressOf AfterCurrenciesInit

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
        globalVariablesInitFlags.Add(GlobalEnums.GlobalModels.CURRENCIES, False)

    End Sub

    Friend Shared Sub CloseNetworkConnection()

        GlobalVariables.NetworkConnect.Stop()
        GlobalVariables.Connection_Toggle_Button.Image = 0
        GlobalVariables.Connection_Toggle_Button.Caption = "Not connected"

    End Sub


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

    Private Sub AfterCurrenciesInit()

        globalVariablesInitFlags(GlobalEnums.GlobalModels.CURRENCIES) = True
        globalInitFlag = CheckGlobalVariablesInitFlag()
  
    End Sub

    ' exchange rates

    ' exchange rates versions



#End Region


#Region "Utils"

    Private Function CheckGlobalVariablesInitFlag() As Boolean

        For Each value As Boolean In globalVariablesInitFlags.Values
            If value = False Then Return False
        Next
        System.Diagnostics.Debug.WriteLine("Global variables initialized")
        Return True

    End Function

  
#End Region


End Class

