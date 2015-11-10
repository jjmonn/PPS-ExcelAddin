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
' Last modified: 23/09/2015


Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports VIBlend.WinForms.Controls
Imports CRUD

Friend Class ExchangeRatesController


#Region "Instance Variables"

    ' Objects
    Private m_view As ExchangeRatesView
    Private m_exchangeRates As New ExchangeRateManager
    Private m_ratesVersionTV As New vTreeView
    Private m_newRatesVersionUI As NewRatesVersionUI
    Private m_excelImport As ExcelExchangeRatesImportUI
    Private mpBar As PBarUI
    Private m_PlatformManagementUI As PlatformMGTGeneralUI

    ' Variables
    Friend m_isValid As Boolean
    Friend m_currentRatesVersionId As Int32
    Friend m_MonthsIdList As List(Of Int32)


#End Region


#Region "Initialize"

    Friend Sub New()

        If GlobalVariables.Currencies.GetValue(GlobalVariables.Currencies.GetMainCurrency()) Is Nothing Then
            MsgBox("Main currency must be set up.")
            m_isValid = False
            Exit Sub
        End If

        Dim version As Version = GlobalVariables.Versions.GetValue(My.Settings.version_id)
        If version Is Nothing Then
            MsgBox("Invalid version in settings.")
            m_isValid = False
            Exit Sub
        End If
        GlobalVariables.RatesVersions.LoadRateVersionsTV(m_ratesVersionTV)
        m_view = New ExchangeRatesView(Me, m_ratesVersionTV, GlobalVariables.Currencies.GetMainCurrency())
        m_currentRatesVersionId = version.RateVersionId
        m_newRatesVersionUI = New NewRatesVersionUI(Me)

        AddHandler m_exchangeRates.UpdateEvent, AddressOf AfterRateUpdate
        AddHandler GlobalVariables.RatesVersions.Read, AddressOf RatesVersionUpdateFromServer
        AddHandler GlobalVariables.RatesVersions.CreationEvent, AddressOf AfterVersionCreate
        AddHandler GlobalVariables.RatesVersions.UpdateEvent, AddressOf AfterVersionUpdate
        AddHandler GlobalVariables.RatesVersions.DeleteEvent, AddressOf AfterVersionDelete
        m_isValid = True

    End Sub

    Public Sub AddControlToPanel(ByRef dest_panel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        If m_isValid = True Then
            Me.m_PlatformManagementUI = PlatformMGTUI
            dest_panel.Controls.Add(m_view)
            m_view.Dock = Windows.Forms.DockStyle.Fill
        End If

    End Sub

    Public Sub close()

        If m_isValid = True Then
            m_view.closeControl()
            m_view.Dispose()
            m_view = Nothing
        End If

    End Sub

    Friend Sub DisplayRates(ByRef p_ratesVersionid As Int32)

        m_currentRatesVersionId = p_ratesVersionid
        m_MonthsIdList = GlobalVariables.RatesVersions.GetMonthsList(p_ratesVersionid)
        m_view.InitializeDGV(GlobalVariables.Currencies.GetInUseCurrenciesIdList(), m_MonthsIdList, p_ratesVersionid)
        m_view.DisplayRatesVersionValues(m_exchangeRates)
        m_view.rates_version_TB.Text = GlobalVariables.RatesVersions.GetValueName(m_currentRatesVersionId)

    End Sub


#End Region


#Region "Rates Controller"

    Friend Sub UpdateRate(ByRef p_destinationCurrency As Int32, _
                          ByRef p_period As Int32, _
                          ByVal p_value As Double)

        Dim rate As ExchangeRate = m_exchangeRates.GetValue(p_destinationCurrency, m_currentRatesVersionId, p_period)

        If rate Is Nothing Then Exit Sub
        rate.Value = p_value
        m_exchangeRates.Update(rate)

    End Sub

    Private Sub AfterRateUpdate(ByRef status As ErrorMessage, _
                                ByRef p_id As UInt32)

        ' to be implemented priority normal

    End Sub

#End Region


#Region "Rates Version Controller"

    Friend Sub CreateVersion(ByRef p_parentId As Int32, _
                             ByRef p_name As String, _
                             ByRef p_isFolder As Boolean, _
                             ByRef p_startPeriodYear As Int32, _
                             ByRef p_nbPeriods As Int32)


        Dim tmpHT As New ExchangeRateVersion
        tmpHT.ParentId = p_parentId
        tmpHT.Name = p_name
        tmpHT.IsFolder = p_isFolder
        tmpHT.ItemPosition = 1
        tmpHT.StartPeriod = DateSerial(p_startPeriodYear, 12, 31).ToOADate()
        tmpHT.NbPeriod = p_nbPeriods

        GlobalVariables.RatesVersions.Create(tmpHT)

    End Sub

    Friend Sub UpdateVersionName(ByRef version_id As UInt32, ByRef name As String)

        Dim l_version As ExchangeRateVersion = GlobalVariables.GlobalFactsVersions.GetValue(version_id)
        If l_version Is Nothing Then Exit Sub

        l_version = l_version.Clone()
        l_version.Name = name
        GlobalVariables.GlobalFactsVersions.Update(l_version)

    End Sub

    Friend Function DeleteRatesVersion(ByRef p_ratesVersionId As Int32) As Boolean

        ' ---------------------------------------------------------------------------------------------------
        ' caution PRIORITY HIGH => rule : cannot delete exchange rates version if binded to a fact verions !!!!!!!!!!!
        ' checks before delete
        ' return false
        ' ---------------------------------------------------------------------------------------------------

        If p_ratesVersionId = m_currentRatesVersionId Then
            m_currentRatesVersionId = 0
            ' not ok to be reviewed! 
            '      m_view.InitializeDGV(GlobalVariables.Currencies.currencies_hash.Keys, m_MonthsIdList, p_ratesVersionId)
        End If
        GlobalVariables.RatesVersions.Delete(p_ratesVersionId)
        Return True

    End Function


#Region "Events"

    Private Sub RatesVersionUpdateFromServer(ByRef p_status As Boolean, ByRef p_ratesVersionHt As ExchangeRateVersion)

        If m_view Is Nothing Then Exit Sub
        If p_status = True Then
            m_view.TVUpdate(p_ratesVersionHt.Id, _
                            p_ratesVersionHt.ParentId, _
                            p_ratesVersionHt.Name, _
                            p_ratesVersionHt.IsFolder)
        End If

    End Sub

    Private Sub AfterVersionCreate(ByRef p_status As Boolean, ByRef id As Int32)

        If p_status = False Then
            MsgBox("The version could not be created")
            ' register error from CRUD and display details -> priority normal V1 
        End If

    End Sub

    Private Sub AfterVersionUpdate(ByRef status As Boolean, ByRef id As Int32)

        ' to be implemented -> priority normal

    End Sub

    Private Sub AfterVersionDelete(ByRef p_status As ErrorMessage, ByRef p_id As Int32)

        If m_view Is Nothing Then Exit Sub

        Select Case p_status
            Case ErrorMessage.SUCCESS
                m_view.TVNodeDelete(p_id)
            Case ErrorMessage.DEPENDENT_PARENT
                MsgBox("This exchange rate version is linked to a general version")
            Case Else
                MsgBox("Unable to delete this version")
        End Select

    End Sub


#End Region


#End Region


#Region "Utilities"

    Friend Function IsFolderVersion(ByRef versionId As Int32) As Boolean
        Dim l_version As ExchangeRateVersion = GlobalVariables.RatesVersions.GetValue(versionId)
        If l_version Is Nothing Then Return False

        Return l_version.IsFolder
    End Function

    Private Sub UpdateVersionsPositions()

        Dim positions_dic = VTreeViewUtil.GeneratePositionsDictionary(m_ratesVersionTV)
        Dim listVersion As New List(Of CRUDEntity)

        For Each id In positions_dic.Keys
            Dim l_version As ExchangeRateVersion = GlobalVariables.RatesVersions.GetValue(id)
            If l_version Is Nothing Then Continue For

            l_version = l_version.Clone()
            listVersion.Add(l_version)
        Next
        GlobalVariables.RatesVersions.UpdateList(listVersion)

    End Sub

#Region "Import Rates from Excel"

    Friend Sub ImportRatesFromExcel(Optional ByRef p_destinationCurrencyId As Int32 = -1)

        m_excelImport = New ExcelExchangeRatesImportUI(Me)
        If p_destinationCurrencyId <> -1 Then
            m_excelImport.m_destinationCurrency = p_destinationCurrencyId
        End If
        m_excelImport.Show()

    End Sub

    Friend Sub InputRangesCallBack(ByRef p_periods() As Int32, _
                                   ByRef p_rates() As Double,
                                   ByRef p_currencyId As Int32)

        For i = 0 To p_periods.Length - 1
            m_view.UpdateCell(p_currencyId, p_periods(i), p_rates(i))
        Next
        m_excelImport.Dispose()

    End Sub

#End Region

    Friend Sub ShowNewRatesVersion(ByRef p_parentId As Int32)

        m_newRatesVersionUI.m_parentId = p_parentId
        m_newRatesVersionUI.Show()

    End Sub

#End Region


End Class
