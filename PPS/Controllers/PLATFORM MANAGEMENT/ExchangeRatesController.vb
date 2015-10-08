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



Friend Class ExchangeRatesController


#Region "Instance Variables"

    ' Objects
    Private m_view As ExchangeRatesView
    Private m_exchangeRates As New ExchangeRate
    Private m_ratesVersionTV As New vTreeView
    Private m_newRatesVersionUI As NewRatesVersionUI
    Private m_excelImport As ExcelRatesImportUI
    Private mpBar As PBarUI
    Private m_PlatformManagementUI As PlatformMGTGeneralUI

    ' Variables
    Friend m_isValid As Boolean
    Friend m_currentRatesVersionId As Int32
    Friend m_MonthsIdList As List(Of Int32)


#End Region


#Region "Initialize"

    Friend Sub New()

        If GlobalVariables.Currencies.currencies_hash.ContainsKey(CInt(GlobalVariables.Currencies.mainCurrency)) = False Then
            MsgBox("Main currency must be set up.")
            m_isValid = False
            Exit Sub
        End If

        GlobalVariables.RatesVersions.LoadRateVersionsTV(m_ratesVersionTV)
        m_view = New ExchangeRatesView(Me, m_ratesVersionTV, GlobalVariables.Currencies.mainCurrency)
        m_currentRatesVersionId = GlobalVariables.Versions.versions_hash(My.Settings.version_id)(EX_RATES_RATE_VERSION)
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
        m_view.DisplayRatesVersionValues(m_exchangeRates.m_exchangeRatesHash)
        m_view.rates_version_TB.Text = GlobalVariables.RatesVersions.rate_versions_hash(m_currentRatesVersionId)(NAME_VARIABLE)

    End Sub


#End Region


#Region "Rates Controller"

    Friend Sub UpdateRate(ByRef p_destinationCurrency As Int32, _
                          ByRef p_period As Int32, _
                          ByVal p_value As Double)

        Dim ht As New Hashtable
        ht.Add(EX_RATES_DESTINATION_CURR_VAR, p_destinationCurrency)
        ht.Add(EX_RATES_RATE_VERSION, m_currentRatesVersionId)
        ht.Add(EX_RATES_PERIOD_VARIABLE, p_period)
        ht.Add(EX_RATES_RATE_VARIABLE, p_value)
        m_exchangeRates.CMSG_UPDATE_EXCHANGE_RATE(ht)

    End Sub

    Private Sub AfterRateUpdate(ByRef status As Boolean, _
                                ByRef destination_currency_id As Int32, _
                                ByRef rates_version_id As Int32, _
                                ByRef period As Int32)

        ' to be implemented priority normal

    End Sub

#End Region


#Region "Rates Version Controller"

    Friend Sub CreateVersion(ByRef p_parentId As Int32, _
                             ByRef p_name As String, _
                             ByRef p_isFolder As Boolean, _
                             ByRef p_startPeriodYear As Int32, _
                             ByRef p_nbPeriods As Int32)

        Dim tmpHT As New Hashtable
        tmpHT.Add(PARENT_ID_VARIABLE, p_parentId)
        tmpHT.Add(NAME_VARIABLE, p_name)
        tmpHT.Add(IS_FOLDER_VARIABLE, p_isFolder)
        tmpHT.Add(ITEMS_POSITIONS, 1)
        tmpHT.Add(VERSIONS_START_PERIOD_VAR, DateSerial(p_startPeriodYear, 12, 31).ToOADate())
        tmpHT.Add(VERSIONS_NB_PERIODS_VAR, p_nbPeriods)

        GlobalVariables.RatesVersions.CMSG_CREATE_RATE_VERSION(tmpHT)

    End Sub

    Friend Sub UpdateVersionName(ByRef version_id As Int32, ByRef name As String)

        Update(version_id, NAME_VARIABLE, name)

    End Sub

    Private Sub Update(ByRef id As Int32, _
                       ByRef variable As String, _
                       ByRef value As Object)

        Dim ht As Hashtable = GlobalVariables.RatesVersions.rate_versions_hash(id)

        If ht Is Nothing Then Exit Sub
        ht(variable) = value
        GlobalVariables.RatesVersions.CMSG_UPDATE_RATE_VERSION(ht)

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
        GlobalVariables.RatesVersions.CMSG_DELETE_RATE_VERSION(p_ratesVersionId)
        Return True

    End Function


#Region "Events"

    Private Sub RatesVersionUpdateFromServer(ByRef p_status As Boolean, ByRef p_ratesVersionHt As Hashtable)

        If m_view Is Nothing Then Exit Sub
        If p_status = True Then
            m_view.TVUpdate(p_ratesVersionHt(ID_VARIABLE), _
                            p_ratesVersionHt(PARENT_ID_VARIABLE), _
                            p_ratesVersionHt(NAME_VARIABLE), _
                            p_ratesVersionHt(IS_FOLDER_VARIABLE))
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
        Return GlobalVariables.RatesVersions.rate_versions_hash(versionId)(IS_FOLDER_VARIABLE) = True
    End Function

    Private Sub UpdateVersionsPositions()

        Dim positions_dic = VTreeViewUtil.GeneratePositionsDictionary(m_ratesVersionTV)
        For Each id In positions_dic.Keys
            Update(id, ITEMS_POSITIONS, positions_dic(id))
        Next

    End Sub

#Region "Import Rates from Excel"

    Friend Sub ImportRatesFromExcel()

        m_excelImport = New ExcelRatesImportUI(Me, GlobalVariables.Currencies.currencies_hash.Keys)
        m_excelImport.Show()

    End Sub

    Friend Sub InputRangesCallBack(ByRef period() As Integer, ByRef rates() As Double, ByRef curr As String)

        For i = 0 To period.Length - 1
            m_view.UpdateCell(curr, period(i), rates(i))
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
