' CExchangeRatesCONTROLER.vb
'
' CONTROLER for the exchange rates and rates Versions Management
'
' To do:
'       -
'       - 
'
'
' Known Bugs:
'       - 
'
'
' Author: Julien Monnereau
' Last modified: 05/08/2015


Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView


Friend Class ExchangeRatesController


#Region "Instance Variables"

    ' Objects
    Private View As CurrenciesControl
    Private ExchangeRates As New ExchangeRate2
    Private RatesVersions As New RateVersion
    Private rates_versionsTV As New TreeView
    Private Periods As New Period
    Private NewRatesVersionUI As NewRatesVersionUI
    Private ExcelImport As ExcelRatesImportUI
    Private PBar As PBarUI
    Private PlatformMGTUI As PlatformMGTGeneralUI

    ' Variables
    Friend current_version As String
    Friend currencies_list As List(Of UInt32)
    Friend object_is_alive As Boolean
    Protected Friend global_periods_dictionary As Dictionary(Of Int32, List(Of Int32))


#End Region


#Region "Initialize"

    Friend Sub New()

        If RatesVersions.object_is_alive Then
            object_is_alive = True
            View = New CurrenciesControl(Me, rates_versionsTV)
            RateVersion.load_rates_version_tv(rates_versionsTV)
            current_version = GlobalVariables.Versions.versions_hash(My.Settings.version_id)(EX_RATES_RATE_VERSION)
            currencies_list = GlobalVariables.Currencies.currencies_hash.Keys
            NewRatesVersionUI = New NewRatesVersionUI(Me)
            If Not current_version Is Nothing Then ChangeVersion(current_version)
        Else
            object_is_alive = False
        End If

        AddHandler ExchangeRates.ExchangeRateUpdateEvent, AddressOf AfterRateUpdate
        
    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.PlatformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(View)
        View.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Public Sub close()

        View.closeControl()
        View.Dispose()
        View = Nothing
        PlatformMGTUI.displayControl()

    End Sub


#End Region


#Region "Rates Controller"

    Friend Sub UpdateRate(ByRef curr As String, _
                          ByRef period As Integer, _
                          ByVal value As Double)


        Dim ht As New Hashtable
        ' fill ht priority high !!!!!!!!!!!!!!!!!
        ' quid origin currency
        '
        ExchangeRates.CMSG_UPDATE_EXCHANGE_RATE(ht)

    End Sub

    Friend Sub ChangeVersion(ByRef version_id As String)


        ' change rates_version

        current_version = version_id
        global_periods_dictionary = RatesVersions.GetPeriodsDictionary(version_id)
        View.ratesView.InitializeDGV(currencies_list, global_periods_dictionary)
        View.ratesView.DisplayRatesVersionValuesinDGV(ExchangeRates.exchangeRates_hash(version_id))
        View.rates_version_TB.Text = RatesVersions.ReadVersion(current_version, NAME_VARIABLE)

    End Sub

    Private Sub AfterRateUpdate(ByRef ht As Hashtable)

        ' to be implemented priority normal

    End Sub



#End Region


#Region "Currencies Controller"

    Friend Sub AddNewCurrency()

        Dim curr As String = InputBox("Enter the name of the new currency." + Chr(13) + _
                                      "The name of the currecny must be 3 letters long (e.g. 'USD').")
        If currencies_list.Contains(curr) Then
            MsgBox("This currency already exists. Please enter a currency which name isn't already in the list")
        Else
            If TypeOf (curr) Is String AndAlso Len(curr) = NAMES_MAX_LENGTH Then
                ' to be reimplemented !!
                ' priority normal
                'GlobalVariables.Currencies.CMSG_CREATE_CURRENCY(curr)
                currencies_list.Add(curr)
                View.ratesView.InitializeDGV(currencies_list, global_periods_dictionary)
                ChangeVersion(current_version)
            Else
                MsgBox("The format of the new currency is not valid." + Chr(13) + _
                       "Please provide a 3 letters currency name (e.g. 'USD').")
            End If
        End If

    End Sub

    'Friend Sub DeleteCurrency(ByRef curr As String)

    '    ' Specific action - PPS Team take care of this

    'End Sub

#End Region


#Region "Rates Version Controller"

    Friend Sub CreateVersion(ByRef name As String, _
                             ByRef isFolder As Boolean, _
                             Optional ByRef start_period As Int32 = 0, _
                             Optional ByRef nb_periods As Int32 = 0, _
                             Optional ByRef parent_node As TreeNode = Nothing)

        Dim tmpHT As New Hashtable
        tmpHT.Add(NAME_VARIABLE, name)
        tmpHT.Add(ITEMS_POSITIONS, 1)

        If parent_node Is Nothing Then tmpHT.Add(RATES_parent_id, DBNull.Value) Else tmpHT.Add(RATES_parent_id, parent_node.Name)
        If isFolder = True Then
            tmpHT.Add(RATES_IS_FOLDER_VARIABLE, 1)
        Else
            tmpHT.Add(RATES_IS_FOLDER_VARIABLE, 0)
            tmpHT.Add(RATES_VERSIONS_START_PERIOD_VAR, start_period)
            tmpHT.Add(RATES_VERSIONS_NB_PERIODS_VAR, nb_periods)
        End If

        RatesVersions.CreateVersion(tmpHT)

        ' below -> after create
        ' priority normal , new crud to be implemented !!

        'If parent_node Is Nothing Then rates_versionsTV.Nodes.Add(key, name) Else parent_node.Nodes.Add(key, name)
        'UpdateVersionsPositions()
        'RateVersion.load_rates_version_tv(rates_versionsTV)
        'If isFolder = False Then ChangeVersion(key)

    End Sub

    Friend Sub UpdateVersionName(ByRef version_id As String, ByRef name As String)

        RatesVersions.UpdateVersion(version_id, NAME_VARIABLE, name)

    End Sub

    Friend Sub DeleteVersionsOrFolder(ByRef version_node As TreeNode)

        Dim versions_list = TreeViewsUtilities.GetNodesKeysList(version_node)
        versions_list.Reverse()
        For Each version_id In versions_list
            If RatesVersions.ReadVersion(version_id, RATES_IS_FOLDER_VARIABLE) = 0 Then
                If DeleteVersion(version_id) = True Then
                    DeleteFromTreeAndModel(version_id)
                Else
                    ' Quid: if we delete parent node !!
                End If
            Else
                DeleteFromTreeAndModel(version_id)
            End If
        Next

        ' ---------------------------------------------------------------------------------------------------
        ' caution PRIORITY HIGH => rule : cannot delete exchange rates version if binded to a fact verions !!!!!!!!!!!
        ' ---------------------------------------------------------------------------------------------------

    End Sub

    Private Function DeleteVersion(ByRef version_id As String) As Boolean

        Dim tmp_version = current_version
        ExchangeRates.DeleteAllRatesForVersion(version_id)

        If version_id = current_version Then
            current_version = ""
            View.ratesView.InitializeDGV(currencies_list, global_periods_dictionary)
        End If
        Return True
     
    End Function

    Private Sub DeleteFromTreeAndModel(ByRef version_id)

        RatesVersions.DeleteVersion(version_id)
        On Error Resume Next
        rates_versionsTV.Nodes.Find(version_id, True)(0).Remove()

    End Sub


#End Region


#Region "Utilities"

    Friend Function IsFolderVersion(ByRef versionKey As String) As Boolean

        If RatesVersions.ReadVersion(versionKey, RATES_IS_FOLDER_VARIABLE) = 1 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub UpdateVersionsPositions()

        Dim positions_dic = TreeViewsUtilities.GeneratePositionsDictionary(rates_versionsTV)
        For Each id In positions_dic.Keys
            RatesVersions.UpdateVersion(id, ITEMS_POSITIONS, positions_dic(id))
        Next

    End Sub

#Region "Import Rates from Excel"

    Friend Sub ImportRatesFromExcel()

        ExcelImport = New ExcelRatesImportUI(Me, currencies_list)
        ExcelImport.Show()

    End Sub

    Friend Sub InputRangesCallBack(ByRef period() As Integer, ByRef rates() As Double, ByRef curr As String)

        For i = 0 To period.Length - 1
            View.ratesView.UpdateCell(curr, period(i), rates(i))
        Next
        ExcelImport.Dispose()

    End Sub

#End Region

    Friend Sub ShowNewRatesVersion(Optional ByRef parent_node As TreeNode = Nothing)

        NewRatesVersionUI.parent_node = parent_node
        NewRatesVersionUI.Show()

    End Sub

#End Region


End Class
