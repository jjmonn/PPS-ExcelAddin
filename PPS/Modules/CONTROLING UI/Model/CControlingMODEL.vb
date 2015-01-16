' CControlingMODEL
' 
' Aim: 
'      Builds datasources for the Controling User Interface : Build Data Arrays (enitty)(account)(period)
'
'
' To do: 
'       - Clear dll-> empty computation memory but not model
'       - Computation of an entity without childrend -> currently the strSqlFilter is not applied = should be
'       - currently currency selection but entities currencies not implemented
'       - set formulasTypes and formulasCodes as constants
'       - dynamic with settings displays N-1, N-2,  N-3 
'       - GetDataFromComputer: currency should be a param at this stage -> already computed
'
' Known Bugs:
'       - 
'       - erreur si pas de taux -> si nb records = 0 la matrice de devrait pas être lancée
'
' Last modified: 25/11/2014
' Author: Julien Monnereau


Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Linq
Imports System.Data
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports H4xCode


Friend Class CControlingMODEL


#Region " Instance Variables"

    ' Objects
    Private CCOMPUTERINT As New DLL3_Interface
    Private DBDOWNLOADER As New DataBaseDataDownloader

    ' Complete mode
    Private global_periods_list As List(Of Integer)
    Friend complete_data_dictionary As Dictionary(Of String, Double())
    Friend entities_id_list As List(Of String)
    Friend inputs_entities_list As List(Of String)


#End Region


#Region "Initialization"

    Public Sub New(ByRef period_list As List(Of Integer))

        global_periods_list = period_list
        CCOMPUTERINT.InitDllCurrencyConvertorPeriods(global_periods_list)

    End Sub

#End Region


#Region "Complete Mode"

    Friend Sub init_computer_complete_mode(ByVal entity_node As TreeNode)

        clear_complete_data_dictionary()
        entities_id_list = CCOMPUTERINT.InitializeEntitiesAggregation(entity_node)

        inputs_entities_list = cTreeViews_Functions.GetNoChildrenNodesList(entities_id_list, entity_node.TreeView)


    End Sub

    Friend Sub compute_selection_complete(ByRef VersionCode As String, _
                                          ByRef PBar As ProgressBarControl, _
                                          ByRef VersionTimeSetup As String, _
                                          ByRef rates_version As String, _
                                          ByRef periods_list As List(Of Integer), _
                                          ByVal destinationCurrency As String, _
                                          Optional ByRef ref_period As Int32 = 0, _
                                          Optional ByRef strSqlQuery As String = "")

        load_needed_currencies(VersionTimeSetup, rates_version, destinationCurrency, ref_period)

        CCOMPUTERINT.SetUpEABeforeCompute(periods_list, _
                                          destinationCurrency, _
                                          VersionTimeSetup, _
                                          rates_version, _
                                          ref_period)

        Dim viewName As String = VersionCode & User_Credential
        If DBDOWNLOADER.BuildDataRSTForEntityLoop(inputs_entities_list.ToArray, _
                                                  viewName, _
                                                  strSqlQuery) Then

            For Each entity_id In inputs_entities_list
                If DBDOWNLOADER.FilterOnEntityID(entity_id) Then
                    CCOMPUTERINT.ComputeInputEntity(entity_id, _
                                                    DBDOWNLOADER.AccKeysArray, _
                                                    DBDOWNLOADER.PeriodArray, _
                                                    DBDOWNLOADER.ValuesArray)
                End If
                PBar.AddProgress(1)
            Next
            DBDOWNLOADER.CloseRST()
        End If
        CCOMPUTERINT.ComputeAggregation()
        PBar.AddProgress(2)

    End Sub

    Protected Friend Sub LoadOutputMatrix(ByRef PBar As ProgressBarControl)

        complete_data_dictionary = CCOMPUTERINT.GetOutputMatrix()
        PBar.AddProgress(2)

    End Sub

    Protected Friend Function GetEntityArray(ByRef entity_id As String) As Double()

        Return CCOMPUTERINT.GetEntityDataArray(entity_id)

    End Function


#End Region


#Region "Complete Mode Currency Convertor Management"

    Private Sub load_needed_currencies(ByRef time_config As String, _
                                       ByRef rates_version As String, _
                                       ByRef dest_currency As String, _
                                       Optional ByRef ref_period As Integer = 0)

        Dim currencies_tokens_list As New List(Of String)
        For Each currency In get_unique_currencies()
            Dim simple_currency_token As String = currency & CURRENCIES_SEPARATOR & dest_currency
            Dim complex_currency_token As String = rates_version & simple_currency_token & ref_period

            If CCOMPUTERINT.convertor_currencies_token_list.Contains(complex_currency_token) = False _
            AndAlso currency <> dest_currency _
            AndAlso currency <> "" _
            Then currencies_tokens_list.Add(simple_currency_token)
        Next

        For Each simple_currency_token In currencies_tokens_list
            Dim complex_currency_token As String = rates_version & simple_currency_token & ref_period

            Dim ratesList As New List(Of Double)
            Dim ratesPeriodsList As New List(Of Int32)
            Dim inverse_flag As Int32 = 0
            If dest_currency <> MAIN_CURRENCY Then inverse_flag = 1

            ExchangeRatesMapping.FillRatesLists(simple_currency_token, _
                                                     rates_version, _
                                                     time_config, _
                                                     inverse_flag, _
                                                     ratesPeriodsList, _
                                                     ratesList, _
                                                     ref_period)

            If time_config = MONTHLY_TIME_CONFIGURATION Then
                CCOMPUTERINT.AddMonthlyCurrenciesRatesToConvertor(complex_currency_token, _
                                                                  ratesList.ToArray)
            Else
                CCOMPUTERINT.AddYearlyCurrenciesRatesToConvertor(complex_currency_token, _
                                                                 ratesPeriodsList.ToArray, _
                                                                 ratesList.ToArray, _
                                                                 ratesList.Count)
            End If
        Next

    End Sub


#End Region


#Region "Utilities"

    Friend Function get_model_accounts_list() As String()

        Return CCOMPUTERINT.accounts_array

    End Function

    Friend Sub clear_complete_data_dictionary()

        If Not complete_data_dictionary Is Nothing Then
            For Each entity In entities_id_list
                Erase complete_data_dictionary(entity)
            Next
            complete_data_dictionary.Clear()
        End If

    End Sub

    Friend Sub delete_model()

        CCOMPUTERINT.destroy_dll()

    End Sub

    Friend Function get_unique_currencies() As List(Of String)

        Dim unique_currencies As New List(Of String)
        For Each currency In CCOMPUTERINT.entities_currencies
            If unique_currencies.Contains(currency) = False Then unique_currencies.Add(currency)
        Next
        Return unique_currencies

    End Function

#End Region


End Class
