Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms
Imports VIBlend.WinForms.Controls

Friend Class GlobalFactController


#Region "Instance variables"

    ' Objects
    Private m_view As GlobalFactUI
    Private m_versionTV As New vTreeView
    Private m_platformMGTUI As PlatformMGTGeneralUI
    Private m_excelImport As ExcelRatesImportUI
    Friend m_currentVersionId As Int32
    Friend m_MonthsIdList As List(Of Int32)
    Private m_newFactVersionUI As NewGlobalFactVersionUI
    Private m_newFactUI As NewGlobalFactUI

#End Region


#Region "Initialize"

    Friend Sub New()

        GlobalVariables.GlobalFactsVersions.LoadVersionsTV(m_versionTV)
        m_view = New GlobalFactUI(Me, m_versionTV)
        m_currentVersionId = GlobalVariables.Versions.versions_hash(My.Settings.version_id)(EX_RATES_RATE_VERSION)
        m_newFactVersionUI = New NewGlobalFactVersionUI(Me)
        m_newFactUI = New NewGlobalFactUI(Me)

        AddHandler GlobalVariables.GlobalFactsVersions.Read, AddressOf GlobalFactsVersionUpdateFromServer
        AddHandler GlobalVariables.GlobalFactsVersions.CreationEvent, AddressOf AfterGlobalFactsVersionCreate
        AddHandler GlobalVariables.GlobalFactsVersions.UpdateEvent, AddressOf AfterGlobalFactsVersionUpdate
        AddHandler GlobalVariables.GlobalFactsVersions.DeleteEvent, AddressOf AfterGlobalFactsVersionDelete

    End Sub

    Public Sub Close()

        m_view.closeControl()
        m_view.Dispose()
        m_view = Nothing

    End Sub

    Friend Sub DisplayFacts(ByRef p_versionid As Int32)

        m_currentVersionId = p_versionid
        m_MonthsIdList = GlobalVariables.GlobalFacts.GetMonthsList(p_versionid)
        m_view.InitializeDGV(GlobalVariables.Currencies.GetInUseCurrenciesIdList(), m_MonthsIdList, p_versionid)
        'm_view.DisplayRatesVersionValues(m_exchangeRates.m_exchangeRatesHash)
        m_view.version_TB.Text = GlobalVariables.GlobalFactsVersions.globalFact_versions_hash(m_currentVersionId)(NAME_VARIABLE)

    End Sub

    Public Sub AddControlToPanel(ByRef p_destPanel As Panel, _
                             ByRef p_platformMGUI As PlatformMGTGeneralUI)

        m_platformMGTUI = p_platformMGUI
        p_destPanel.Controls.Add(m_view)
        m_view.Dock = Windows.Forms.DockStyle.Fill

    End Sub

#End Region


#Region "Interface"

#End Region


#Region "Utilities"

    Friend Function GetGlobalFactList() As Hashtable
        Return GlobalVariables.GlobalFacts.globalFact_hash
    End Function

    Friend Function GetFact(ByRef p_period As Int32, ByRef p_factId As Int32, ByRef p_versionId As Int32) As Double
        Dim key = New Tuple(Of Int32, Int32, Int32)(p_factId, p_period, p_versionId)
        If (Not GlobalVariables.GlobalFactsDatas.globalFactDataHash.ContainsKey(key)) Then Return 0
        Return GlobalVariables.GlobalFactsDatas.globalFactDataHash(key)
    End Function

    Friend Function IsFolderVersion(ByRef versionId As Int32) As Boolean
        Return GlobalVariables.GlobalFactsVersions.globalFact_versions_hash(versionId)(IS_FOLDER_VARIABLE) = True
    End Function

    Private Sub UpdateVersionsPositions()

        Dim positions_dic = VTreeViewUtil.GeneratePositionsDictionary(m_versionTV)
        For Each id In positions_dic.Keys
            UpdateVersion(id, ITEMS_POSITIONS, positions_dic(id))
        Next

    End Sub

    Private Sub UpdateVersion(ByRef id As Int32, _
                   ByRef variable As String, _
                   ByRef value As Object)

        Dim ht As Hashtable = GlobalVariables.GlobalFactsVersions.globalFact_versions_hash(id)
        ht(variable) = value
        GlobalVariables.GlobalFacts.CMSG_UPDATE_GLOBAL_FACT(ht)

    End Sub

    Friend Sub UpdateFactData(ByRef p_period As Int32, ByRef p_factId As Int32, ByRef p_versionId As Int32, ByRef p_value As Double)
        Dim key = New Tuple(Of Int32, Int32, Int32)(p_factId, p_period, p_versionId)
        If GlobalVariables.GlobalFactsDatas.globalFactDataHash.ContainsKey(key) Then
            GlobalVariables.GlobalFactsDatas.CMSG_UPDATE_GLOBAL_FACT_DATA(p_factId, p_period, p_versionId, p_value)
        Else
            GlobalVariables.GlobalFactsDatas.CMSG_CREATE_GLOBAL_FACT_DATA(p_factId, p_period, p_versionId, p_value)
        End If
    End Sub

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

        GlobalVariables.GlobalFactsVersions.CMSG_CREATE_GLOBAL_FACT_VERSION(tmpHT)

    End Sub

    Friend Sub CreateFact(ByRef p_name As String)

        Dim tmpHT As New Hashtable
        tmpHT.Add(NAME_VARIABLE, p_name)
        GlobalVariables.GlobalFacts.CMSG_CREATE_GLOBAL_FACT(tmpHT)

    End Sub

    Friend Function DeleteRatesVersion(ByRef p_ratesVersionId As Int32) As Boolean

        If p_ratesVersionId = m_currentVersionId Then
            m_currentVersionId = 0
        End If
        GlobalVariables.GlobalFactsVersions.CMSG_DELETE_GLOBAL_FACT_VERSION(p_ratesVersionId)
        Return True

    End Function

#End Region

#Region "Import Rates from Excel"

    Friend Sub InputRangesCallBack(ByRef period() As Integer, ByRef rates() As Double, ByRef curr As String)

        If m_view Is Nothing Then Exit Sub
        For i = 0 To period.Length - 1
            m_view.UpdateCell(curr, period(i), rates(i))
        Next
        m_excelImport.Dispose()

    End Sub

#End Region

    Friend Sub ShowNewFactVersion(ByRef p_parentId As Int32)

        m_newFactVersionUI.m_parentId = p_parentId
        m_newFactVersionUI.Show()

    End Sub

    Friend Sub ShowNewFact()

        m_newFactUI.Show()

    End Sub

#Region "Events"

    Private Sub GlobalFactsVersionUpdateFromServer(ByRef p_status As Boolean, ByRef p_versionHt As Hashtable)

        If m_view Is Nothing Then Exit Sub
        If p_status = True Then
            m_view.TVUpdate(p_versionHt(ID_VARIABLE), _
                            p_versionHt(PARENT_ID_VARIABLE), _
                            p_versionHt(NAME_VARIABLE), _
                            p_versionHt(IS_FOLDER_VARIABLE))
        End If

    End Sub

    Private Sub AfterGlobalFactsVersionCreate(ByRef p_status As Boolean, ByRef id As Int32)

        If p_status = False Then
            MsgBox("The version could not be created")
        End If

    End Sub

    Private Sub AfterGlobalFactsVersionUpdate(ByRef status As Boolean, ByRef id As Int32)


    End Sub

    Private Sub AfterGlobalFactsVersionDelete(ByRef p_status As Boolean, ByRef p_id As Int32)
        If m_view Is Nothing Then Exit Sub

        If p_status = True Then
            m_view.TVNodeDelete(p_id)
        End If

    End Sub


#End Region

End Class
