' CExchangeRatesCONTROLER.vb
'
' CONTROLER for the exchange rates and rates Versions Management
'
' To do:
'       -position management high !!!!
'       - 
'
'
' Known Bugs:
'       - 
'
'
' Author: Julien Monnereau
' Last modified: 24/08/2015


Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports VIBlend.WinForms.Controls

'Imports VIBlend.WinForms.Controls


Friend Class ExchangeRatesController


#Region "Instance Variables"

    ' Objects
    Private View As CurrenciesControl
    Private ExchangeRates As New ExchangeRate
    Private rates_versionsTV As New vTreeView
    Private NewRatesVersionUI As NewRatesVersionUI
    Private ExcelImport As ExcelRatesImportUI
    Private PBar As PBarUI
    Private PlatformMGTUI As PlatformMGTGeneralUI

    ' Variables
    Friend currentRatesVersionId As Int32
    Friend monthsIdList As List(Of Int32)


#End Region


#Region "Initialize"

    Friend Sub New()

        View = New CurrenciesControl(Me, rates_versionsTV)
        currentRatesVersionId = GlobalVariables.Versions.versions_hash(My.Settings.version_id)(EX_RATES_RATE_VERSION)
        NewRatesVersionUI = New NewRatesVersionUI(Me)

        AddHandler ExchangeRates.UpdateEvent, AddressOf AfterRateUpdate
        AddHandler GlobalVariables.RatesVersions.CreationEvent, AddressOf AfterVersionCreate
        AddHandler GlobalVariables.RatesVersions.UpdateEvent, AddressOf AfterVersionUpdate
        AddHandler GlobalVariables.RatesVersions.DeleteEvent, AddressOf AfterVersionDelete


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

    Friend Sub ChangeVersion(ByRef versionId As Int32)

        currentRatesVersionId = versionId
        monthsIdList = GlobalVariables.RatesVersions.GetMonthsList(versionId)
        View.ratesView.InitializeDGV(GlobalVariables.Currencies.currencies_hash.Keys, _
                                     monthsIdList)
        View.ratesView.DisplayRatesVersionValuesinDGV(ExchangeRates.exchangeRatesDict)
        View.rates_version_TB.Text = GlobalVariables.RatesVersions.rate_versions_hash(currentRatesVersionId)(NAME_VARIABLE)

    End Sub

    Private Sub AfterRateUpdate(ByRef status As Boolean, _
                                ByRef destination_currency_id As Int32, _
                                ByRef rates_version_id As Int32, _
                                ByRef period As Int32)

        ' to be implemented priority normal

    End Sub



#End Region


#Region "Rates Version Controller"

    Friend Sub CreateVersion(ByRef name As String, _
                             ByRef isFolder As Boolean, _
                             Optional ByRef start_period As Int32 = 0, _
                             Optional ByRef nb_periods As Int32 = 0, _
                             Optional ByRef parentId As Int32 = 0)

        Dim tmpHT As New Hashtable
        tmpHT.Add(NAME_VARIABLE, name)
        tmpHT.Add(ITEMS_POSITIONS, 1)

        If parentId <> 0 Then tmpHT.Add(PARENT_ID_VARIABLE, 0) Else tmpHT.Add(PARENT_ID_VARIABLE, parentId)
        If isFolder = True Then
            tmpHT.Add(IS_FOLDER_VARIABLE, 1)
        Else
            tmpHT.Add(IS_FOLDER_VARIABLE, 0)
            tmpHT.Add(VERSIONS_START_PERIOD_VAR, start_period)
            tmpHT.Add(VERSIONS_NB_PERIODS_VAR, nb_periods)
        End If

        GlobalVariables.RatesVersions.CMSG_CREATE_RATE_VERSION(tmpHT)

    End Sub

    Private Sub AfterVersionCreate(ByRef status As Boolean, ByRef id As Int32)

        ' to be reimplemented priority high


        'If ht(PARENT_ID_VARIABLE) = 0 Then
        '    VTreeViewUtil.AddNode(ht(ID_VARIABLE), ht(NAME_VARIABLE), rates_versionsTV)
        'Else
        '    Dim parentNode As vTreeNode = VTreeViewUtil.FindNode(rates_versionsTV, ht(PARENT_ID_VARIABLE))
        '    If Not parentNode Is Nothing Then
        '        VTreeViewUtil.AddNode(ht(ID_VARIABLE), ht(NAME_VARIABLE), parentNode)
        '    End If
        'End If

        'If ht(IS_FOLDER_VARIABLE) = False Then
        '    ChangeVersion(ht(ID_VARIABLE))
        'End If

    End Sub

    Friend Sub UpdateVersionName(ByRef version_id As Int32, ByRef name As String)

        '  GlobalVariables.RatesVersions.UpdateVersion(version_id, NAME_VARIABLE, name)

    End Sub

    Private Sub Update(ByRef id As Int32, _
                       ByRef variable As String, _
                       ByRef value As Object)

        Dim ht As Hashtable = GlobalVariables.RatesVersions.rate_versions_hash(id)
        ht(variable) = value
        GlobalVariables.RatesVersions.CMSG_UPDATE_RATE_VERSION(ht)

    End Sub



    Private Sub AfterVersionUpdate(ByRef status As Boolean, ByRef id As Int32)

        ' to be implemented -> priority normal

    End Sub

    Friend Function DeleteRatesVersion(ByRef version_id As Int32) As Boolean

        ' ---------------------------------------------------------------------------------------------------
        ' caution PRIORITY HIGH => rule : cannot delete exchange rates version if binded to a fact verions !!!!!!!!!!!
        ' checks before delete
        ' return false
        ' ---------------------------------------------------------------------------------------------------

        If version_id = currentRatesVersionId Then
            currentRatesVersionId = ""
            View.ratesView.InitializeDGV(GlobalVariables.Currencies.currencies_hash.Keys, _
                                         monthsIdList)
        End If
        GlobalVariables.RatesVersions.CMSG_DELETE_RATE_VERSION(version_id)
        Return True

    End Function

    Private Sub AfterVersionDelete(ByRef status As Boolean, ByRef id As Int32)

        ' if version is currently displayed -> set another one
        ' priority high
        Dim nodeToBeRemoved As vTreeNode = VTreeViewUtil.FindNode(rates_versionsTV, id)
        If Not nodeToBeRemoved Is Nothing Then
            nodeToBeRemoved.Remove()
        End If

    End Sub


#End Region


#Region "Utilities"

    Friend Function IsFolderVersion(ByRef versionId As Int32) As Boolean
        Return GlobalVariables.RatesVersions.rate_versions_hash(versionId)(IS_FOLDER_VARIABLE) = True
    End Function

    Private Sub UpdateVersionsPositions()

        Dim positions_dic = VTreeViewUtil.GeneratePositionsDictionary(rates_versionsTV)
        For Each id In positions_dic.Keys
            Update(id, ITEMS_POSITIONS, positions_dic(id))
        Next

    End Sub

#Region "Import Rates from Excel"

    Friend Sub ImportRatesFromExcel()

        ExcelImport = New ExcelRatesImportUI(Me, GlobalVariables.Currencies.currencies_hash.Keys)
        ExcelImport.Show()

    End Sub

    Friend Sub InputRangesCallBack(ByRef period() As Integer, ByRef rates() As Double, ByRef curr As String)

        For i = 0 To period.Length - 1
            View.ratesView.UpdateCell(curr, period(i), rates(i))
        Next
        ExcelImport.Dispose()

    End Sub

#End Region

    Friend Sub ShowNewRatesVersion(Optional ByRef parent_node As vTreeNode = Nothing)

        NewRatesVersionUI.parent_node = parent_node
        NewRatesVersionUI.Show()

    End Sub

#End Region


End Class
