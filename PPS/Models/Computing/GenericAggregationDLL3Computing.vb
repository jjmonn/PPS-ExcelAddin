﻿' GenericAggregationDLL3Computing.vb
' 
' Aim: 
'      Builds datasources for the Controling User Interface : Build Data Arrays (entity)(account)(period)
'
'
' To do: 
'       - Clear dll-> empty computation memory but not model
'       - Computation of an entity without children -> currently the strSqlFilter is not applied = should be
'       - currently currency selection but entities currencies not implemented
'       - set formulasTypes and formulasCodes as constants
'       - dynamic with settings displays N-1, N-2,  N-3 
'    
'
' Known Bugs:
'       - erreur si pas de taux -> si nb records = 0 la matrice de devrait pas être lancée
'
'
' Last modified: 23/02/2015
' Author: Julien Monnereau


Imports System.Collections.Generic
Imports System.Collections


Friend Class GenericAggregationDLL3Computing


#Region " Instance Variables"

    ' Objects
    Private Dll3Computer As DLL3_Interface
    Private DBDOWNLOADER As DataBaseDataDownloader

    ' Variables
    Protected Friend complete_data_dictionary As Dictionary(Of String, Double())
    Protected Friend entities_id_list As List(Of String)
    Protected Friend inputs_entities_list As List(Of String)
    Protected Friend current_version_id As String = ""
    Protected Friend current_currency As String = ""
    Protected Friend current_sql_filter_query As String = ""
    Protected Friend current_adjustment_id As String = ""
    Protected Friend periods_list As List(Of Int32)

#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef input_DBDownloader As DataBaseDataDownloader, _
                             Optional ByRef input_dll3_interface As DLL3_Interface = Nothing)

        DBDOWNLOADER = input_DBDownloader
        If input_dll3_interface Is Nothing Then
            Dll3Computer = New DLL3_Interface
        Else
            Dll3Computer = input_dll3_interface
        End If

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub init_computer_complete_mode(ByVal entity_node As System.Windows.Forms.TreeNode)

        clear_complete_data_dictionary()
        entities_id_list = Dll3Computer.InitializeEntitiesAggregation(entity_node)

        inputs_entities_list = TreeViewsUtilities.GetNoChildrenNodesList(entities_id_list, entity_node.TreeView)
        TreeViewsUtilities.FilterSelectedNodes(entity_node, entities_id_list)
       
    End Sub

    Protected Friend Sub compute_selection_complete(ByRef version_id As String, _
                                                  ByRef VersionTimeSetup As String, _
                                                  ByRef rates_version As String, _
                                                  ByRef input_periods_list As List(Of Integer), _
                                                  ByVal destinationCurrency As String, _
                                                  ByRef start_period As Int32, _
                                                  ByRef nb_periods As Int32, _
                                                  Optional ByRef PBar As ProgressBarControl = Nothing, _
                                                  Optional ByRef strSqlQuery As String = "", _
                                                  Optional ByRef adjustments_id_list As List(Of String) = Nothing)

        periods_list = input_periods_list
        ResetDllCurrencyPeriodsConfigIfNeeded(periods_list, VersionTimeSetup, start_period, nb_periods)
        load_needed_currencies(VersionTimeSetup, rates_version, destinationCurrency, start_period, nb_periods)

        Dll3Computer.SetUpEABeforeCompute(periods_list, _
                                          destinationCurrency, _
                                          VersionTimeSetup, _
                                          rates_version, _
                                          start_period)

        Dim viewName As String = version_id & GlobalVariables.User_Credential
        If DBDOWNLOADER.BuildDataRSTForEntityLoop(inputs_entities_list.ToArray, _
                                                  viewName, _
                                                  strSqlQuery, _
                                                  adjustments_id_list) Then

            For Each entity_id In inputs_entities_list
                If DBDOWNLOADER.FilterOnEntityID(entity_id) Then
                    Dll3Computer.ComputeInputEntity(entity_id, _
                                                    DBDOWNLOADER.AccKeysArray, _
                                                    DBDOWNLOADER.PeriodArray, _
                                                    DBDOWNLOADER.ValuesArray)
                End If
                If Not PBar Is Nothing Then PBar.AddProgress(1)
            Next
            DBDOWNLOADER.CloseRST()
        End If
        Dll3Computer.ComputeAggregation()
        If Not PBar Is Nothing Then PBar.AddProgress(2)
        current_currency = destinationCurrency
        current_version_id = version_id
        current_sql_filter_query = strSqlQuery
        If adjustments_id_list Is Nothing Then current_adjustment_id = "" Else current_adjustment_id = adjustments_id_list(0)
        ' above: we should store a string with adjustments id directly !!

    End Sub

    Protected Friend Sub LoadOutputMatrix(Optional ByRef PBar As ProgressBarControl = Nothing)

        complete_data_dictionary = Dll3Computer.GetOutputMatrix()
        If Not PBar Is Nothing Then PBar.AddProgress(2)

    End Sub

    Protected Friend Sub ReinitializeComputerCache()

        current_version_id = ""
        current_currency = ""
        current_sql_filter_query = ""

    End Sub

    Protected Friend Function GetValueFromComputer(ByRef entity_id As String, _
                                                   ByRef account_id As String, _
                                                   ByRef period As Int32) As Object

        Return complete_data_dictionary(entity_id)((Array.IndexOf(Dll3Computer.accounts_array, account_id) _
                                                  * periods_list.Count) _
                                                  + periods_list.IndexOf(period))

    End Function

#End Region


#Region "Currencies Conversion Management"

    Private Sub load_needed_currencies(ByRef time_config As String, _
                                       ByRef rates_version As String, _
                                       ByRef dest_currency As String, _
                                       ByRef start_period As Int32, _
                                       ByRef nb_periods As Int32)

        Dim currencies_tokens_list As New List(Of String)
        For Each currency In get_unique_currencies()
            Dim simple_currency_token As String = currency & CURRENCIES_SEPARATOR & dest_currency
            Dim complex_currency_token As String = rates_version & simple_currency_token & start_period

            If Dll3Computer.convertor_currencies_token_list.Contains(complex_currency_token) = False _
            AndAlso currency <> dest_currency _
            AndAlso currency <> "" _
            Then currencies_tokens_list.Add(simple_currency_token)
        Next

        For Each simple_currency_token In currencies_tokens_list
            Dim complex_currency_token As String = rates_version & simple_currency_token & start_period

            Dim ratesList As New List(Of Double)
            Dim ratesPeriodsList As New List(Of Int32)
            Dim inverse_flag As Int32 = 0
            If dest_currency <> MAIN_CURRENCY Then inverse_flag = 1

            ExchangeRatesMapping.FillRatesLists(simple_currency_token, _
                                                rates_version, _
                                                inverse_flag, _
                                                ratesPeriodsList, _
                                                ratesList, _
                                                start_period, _
                                                nb_periods)

            If time_config = MONTHLY_TIME_CONFIGURATION Then
                Dll3Computer.AddMonthlyCurrenciesRatesToConvertor(complex_currency_token, _
                                                                  ratesList.ToArray)
            Else
                Dll3Computer.AddYearlyCurrenciesRatesToConvertor(complex_currency_token, _
                                                                 ratesPeriodsList.ToArray, _
                                                                 ratesList.ToArray, _
                                                                 ratesList.Count)
            End If
        Next

    End Sub

#End Region


#Region "Adjustments"

    Protected Friend Function GetAdjustments(ByRef version_id As String, _
                                             ByVal destination_currency As String,
                                             Optional ByRef adjustments_id_list As List(Of String) = Nothing)

        Return DBDOWNLOADER.GetAdjustments(version_id, _
                                           inputs_entities_list.ToArray, _
                                           destination_currency, _
                                           adjustments_id_list)

    End Function

#End Region


#Region "Utilities"

    Friend Function get_model_accounts_list() As String()

        Return Dll3Computer.accounts_array

    End Function

    Protected Friend Function GetEntityArray(ByRef entity_id As String) As Double()

        Return Dll3Computer.GetEntityDataArray(entity_id)

    End Function

    Friend Sub clear_complete_data_dictionary()

        If Not complete_data_dictionary Is Nothing Then
            For Each entity In entities_id_list
                Erase complete_data_dictionary(entity)
            Next
            complete_data_dictionary.Clear()
        End If

    End Sub

    Protected Friend Sub delete_model()

        Dll3Computer.destroy_dll()

    End Sub

    Friend Function get_unique_currencies() As List(Of String)

        Dim unique_currencies As New List(Of String)
        For Each currency In Dll3Computer.entities_currencies
            If unique_currencies.Contains(currency) = False Then unique_currencies.Add(currency)
        Next
        Return unique_currencies

    End Function

    Private Sub ResetDllCurrencyPeriodsConfigIfNeeded(ByRef periods_list As List(Of Int32), _
                                                      ByRef time_config As String, _
                                                      ByRef start_period As Int32, _
                                                      ByRef nb_periods As Int32)

        If Dll3Computer.current_start_period <> Int(DateSerial(start_period, 12, 31).ToOADate()) _
        Or Dll3Computer.current_nb_periods <> nb_periods Then
            Dll3Computer.InitDllCurrencyConvertorPeriods(periods_list, time_config, start_period, nb_periods)
        End If

    End Sub

    Protected Friend Function IsEntityAlreadyComputed(ByRef entity_id As String) As Boolean

        If entities_id_list Is Nothing Then Return False
        If entities_id_list.Contains(entity_id) Then Return True Else Return False
      
    End Function

#End Region


End Class