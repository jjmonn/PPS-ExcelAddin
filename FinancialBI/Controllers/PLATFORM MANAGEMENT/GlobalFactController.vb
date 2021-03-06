﻿Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms
Imports VIBlend.WinForms.Controls
Imports CRUD

Friend Class GlobalFactController


#Region "Instance variables"

    ' Objects
    Private m_view As GlobalFactUI
    Private m_versionTV As New vTreeView
    Private m_platformMGTUI As PlatformMGTGeneralUI
    Private m_excelImport As ExcelFactsValuesImportUI
    Friend m_currentVersionId As Int32
    Friend m_MonthsIdList As List(Of Int32)
    Private m_newFactVersionUI As NewGlobalFactVersionUI
    Private m_newFactUI As NewGlobalFactUI

#End Region


#Region "Initialize"

    Friend Sub New()

        GlobalVariables.GlobalFactsVersions.LoadVersionsTV(m_versionTV)
        m_view = New GlobalFactUI(Me, m_versionTV)
        m_newFactVersionUI = New NewGlobalFactVersionUI(Me)
        m_newFactUI = New NewGlobalFactUI(Me)

        AddHandler GlobalVariables.GlobalFactsVersions.Read, AddressOf AfterGlobalFactsVersionRead
        AddHandler GlobalVariables.GlobalFactsVersions.CreationEvent, AddressOf AfterGlobalFactsVersionCreate
        AddHandler GlobalVariables.GlobalFactsVersions.UpdateEvent, AddressOf AfterGlobalFactsVersionUpdate
        AddHandler GlobalVariables.GlobalFactsVersions.DeleteEvent, AddressOf AfterGlobalFactsVersionDelete
        AddHandler GlobalVariables.GlobalFacts.DeleteEvent, AddressOf AfterGlobalFactDelete

    End Sub

    Public Sub Close()

        RemoveHandler GlobalVariables.GlobalFactsVersions.Read, AddressOf AfterGlobalFactsVersionRead
        RemoveHandler GlobalVariables.GlobalFactsVersions.CreationEvent, AddressOf AfterGlobalFactsVersionCreate
        RemoveHandler GlobalVariables.GlobalFactsVersions.UpdateEvent, AddressOf AfterGlobalFactsVersionUpdate
        RemoveHandler GlobalVariables.GlobalFactsVersions.DeleteEvent, AddressOf AfterGlobalFactsVersionDelete
        RemoveHandler GlobalVariables.GlobalFacts.DeleteEvent, AddressOf AfterGlobalFactDelete
        m_view.Dispose()
        m_view = Nothing

    End Sub

    Friend Sub DisplayFacts(ByRef p_versionid As Int32)

        If m_view Is Nothing Then Exit Sub
        m_currentVersionId = p_versionid
        m_MonthsIdList = GlobalVariables.GlobalFacts.GetMonthsList(p_versionid)
        m_view.InitializeDGV(m_MonthsIdList, p_versionid)
        'm_view.DisplayRatesVersionValues(m_exchangeRates.m_exchangeRatesHash)
        m_view.version_TB.Text = GlobalVariables.GlobalFactsVersions.GetValueName(m_currentVersionId)

    End Sub

    Public Sub AddControlToPanel(ByRef p_destPanel As Panel, _
                             ByRef p_platformMGUI As PlatformMGTGeneralUI)

        If m_view Is Nothing Then Exit Sub
        m_platformMGTUI = p_platformMGUI
        p_destPanel.Controls.Add(m_view)
        m_view.Dock = Windows.Forms.DockStyle.Fill

    End Sub

#End Region


#Region "Interface"

    Friend Sub UpdateFactName(ByRef p_factId As UInt32, ByRef p_name As String)
        Dim fact As GlobalFact = GlobalVariables.GlobalFacts.GetValue(p_factId)
        If fact Is Nothing Then Exit Sub

        fact = fact.Clone()
        fact.Name = p_name
        GlobalVariables.GlobalFacts.Update(fact)

    End Sub

    Friend Sub DeleteFact(ByRef p_factId As UInt32)
        Dim fact As GlobalFact = GlobalVariables.GlobalFacts.GetValue(p_factId)
        If fact Is Nothing Then Exit Sub

        GlobalVariables.GlobalFacts.Delete(p_factId)
    End Sub

    Friend Function IsUsedName(ByRef p_name As String) As Boolean
        For Each fact In GetGlobalFactList().Values
            If fact.Name = p_name Then Return True
        Next
        For Each account In GlobalVariables.Accounts.GetDictionary().Values
            If account.Name = p_name Then Return True
        Next
        Return False
    End Function

    Friend Function GetGlobalFactList() As MultiIndexDictionary(Of UInt32, String, NamedCRUDEntity)
        Return GlobalVariables.GlobalFacts.GetDictionary()
    End Function

    Friend Function GetFact(ByRef p_period As Int32, ByRef p_factId As Int32, ByRef p_versionId As Int32) As Double
        Dim factData As GlobalFactData = GlobalVariables.GlobalFactsDatas.GetValue(p_factId, p_period, p_versionId)
        If factData Is Nothing Then Return 0
        Return factData.Value
    End Function

    Friend Function IsFolderVersion(ByRef versionId As UInt32) As Boolean
        Dim version As GlobalFactVersion = GlobalVariables.GlobalFactsVersions.GetValue(versionId)

        If version Is Nothing Then Return False
        Return version.IsFolder
    End Function

    Private Sub UpdateVersionsPositions()

        Dim positions_dic = VTreeViewUtil.GeneratePositionsDictionary(m_versionTV)
        Dim listVersion As New List(Of CRUDEntity)

        For Each id In positions_dic.Keys
            Dim version As GlobalFactVersion = GlobalVariables.GlobalFactsVersions.GetValue(id)
            If version Is Nothing Then Continue For

            version = version.Clone()
            version.ItemPosition = positions_dic(id)
            listVersion.Add(version)
        Next
        GlobalVariables.GlobalFactsVersions.UpdateList(listVersion)

    End Sub

    Friend Sub UpdateVersionName(ByRef p_versionId As UInt32, ByRef p_name As String)
        Dim version As GlobalFactVersion = GlobalVariables.GlobalFactsVersions.GetValue(p_versionId)
        If version Is Nothing Then Exit Sub

        version = version.Clone()
        version.Name = p_name
        GlobalVariables.GlobalFactsVersions.Update(version)
    End Sub

    Friend Sub UpdateFactData(ByRef p_period As Int32, ByRef p_factId As Int32, ByRef p_versionId As Int32, ByRef p_value As Double)
        Dim fact As GlobalFactData = GlobalVariables.GlobalFactsDatas.GetValue(p_factId, p_period, p_versionId)

        If Not fact Is Nothing Then
            fact = fact.Clone()

            fact.Value = p_value
            GlobalVariables.GlobalFactsDatas.Update(fact)
        Else
            fact = New GlobalFactData

            fact.GlobalFactId = p_factId
            fact.Period = p_period
            fact.VersionId = p_versionId
            fact.Value = p_value
            GlobalVariables.GlobalFactsDatas.Create(fact)
        End If
    End Sub

    Friend Sub CreateVersion(ByRef p_parentId As Int32, _
                           ByRef p_name As String, _
                           ByRef p_isFolder As Boolean, _
                           ByRef p_startPeriodYear As Int32, _
                           ByRef p_nbPeriods As Int32)

        Dim tmpHT As New GlobalFactVersion
        tmpHT.ParentId = p_parentId
        tmpHT.Name = p_name
        tmpHT.IsFolder = p_isFolder
        tmpHT.ItemPosition = 1
        tmpHT.StartPeriod = DateSerial(p_startPeriodYear, 1, 31).ToOADate()
        tmpHT.NbPeriod = p_nbPeriods

        GlobalVariables.GlobalFactsVersions.Create(tmpHT)

    End Sub

    Friend Sub CreateFact(ByRef p_name As String)

        Dim tmpHT As New GlobalFact

        tmpHT.Name = p_name
        GlobalVariables.GlobalFacts.Create(tmpHT)

    End Sub

    Friend Function DeleteFactsVersion(ByRef p_factsVersionId As Int32) As Boolean

        If p_factsVersionId = m_currentVersionId Then
            m_currentVersionId = 0
        End If
        GlobalVariables.GlobalFactsVersions.Delete(p_factsVersionId)
        Return True

    End Function

#End Region


#Region "Import Facts from Excel"

    Friend Sub ImportRatesFromExcel(Optional ByRef p_globalFactId As Int32 = -1)

        m_excelImport = New ExcelFactsValuesImportUI(Me)
        m_excelImport.m_globalFactId = p_globalFactId
        m_excelImport.Show()

    End Sub

    Friend Sub InputRangesCallBack(ByRef p_periods() As Int32, _
                                   ByRef p_rates() As Double, _
                                   ByRef p_globalFactId As Int32)

        For i = 0 To p_periods.Length - 1
            m_view.UpdateCell(p_globalFactId, p_periods(i), p_rates(i))
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

    Private Sub AfterGlobalFactDelete(ByRef p_status As ErrorMessage, ByRef p_id As UInt32)
        If p_status <> ErrorMessage.SUCCESS Then Exit Sub
        m_view.ReloadUI()
    End Sub

    Private Sub AfterGlobalFactsVersionRead(ByRef p_status As ErrorMessage, ByRef p_versionHt As CRUDEntity)

        Dim versionHT As GlobalFactVersion = p_versionHt
        If m_view Is Nothing Then Exit Sub
        If p_status = ErrorMessage.SUCCESS Then
            m_view.TVUpdate(versionHT.Id, _
                            versionHT.ParentId, _
                            versionHT.Name, _
                            versionHT.IsFolder)
        End If

    End Sub

    Private Sub AfterGlobalFactsVersionCreate(ByRef p_status As ErrorMessage, ByRef id As UInt32)

        m_newFactVersionUI.CreationBackgroundWorker_AfterWork()
        If p_status <> ErrorMessage.SUCCESS Then
            MsgBox("The version could not be created")
        End If

    End Sub

    Private Sub AfterGlobalFactsVersionUpdate(ByRef status As ErrorMessage, ByRef id As UInt32)


    End Sub

    Private Sub AfterGlobalFactsVersionDelete(ByRef p_status As ErrorMessage, ByRef p_id As UInt32)

        If m_view Is Nothing Then Exit Sub
        m_view.AfterDeleteBackgroundWorker()

        Select Case p_status
            Case ErrorMessage.SUCCESS
                m_view.TVNodeDelete(p_id)
            Case ErrorMessage.DEPENDENT_PARENT
                MsgBox("This economic indicator version is linked to a general version")
            Case Else
                MsgBox("Unable to delete this version")
        End Select

    End Sub


#End Region

End Class
