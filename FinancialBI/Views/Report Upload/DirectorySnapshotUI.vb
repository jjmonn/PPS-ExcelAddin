Imports System.Collections.Generic
Imports Microsoft.Office.Interop
Imports System.Collections
Imports System.ComponentModel
Imports System.IO


Public Class DirectorySnapshotUI

    Private m_periodSelectionControl As PeriodRangeSelectionControl
    Private m_reportUploadController As ReportUploadControler
    Private m_errors As New List(Of String)
    Private m_workbookIndex As Int32
    Private m_currentWorkbook As Excel.Workbook
    Private m_filePaths As String()

#Region "Initialization"

    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeRHComboBoxChoices()
        MultilanguageSetup()
        Me.m_FolderBrowserDialog1.ShowNewFolderButton = False
        m_periodSelectionControl = New PeriodRangeSelectionControl(My.Settings.version_id)
        m_periodSelectionPanel.Controls.Add(m_periodSelectionControl)
        m_periodSelectionControl.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Private Sub MultilanguageSetup()

        Me.Text = Local.GetValue("upload.periods_selection")
        m_validateButton.Text = Local.GetValue("general.validate")
        m_accountSelectionLabel.Text = Local.GetValue("upload.accounts_selection")

    End Sub

    Private Sub InitializeRHComboBoxChoices()

        Dim l_rhAccounts As List(Of CRUD.Account) = GlobalVariables.Accounts.GetAccountsList(GlobalEnums.AccountsLookupOptions.LOOKUP_INPUTS, CRUD.Account.AccountProcess.RH)
        Select Case l_rhAccounts.Count
            Case 0
            Case 1
                m_accountSelectionComboBox.SelectedItem = GeneralUtilities.AddItemToCombobox(m_accountSelectionComboBox, l_rhAccounts(0).Id, l_rhAccounts(0).Name)
                m_accountSelectionComboBox.Enabled = False

            Case Else
                For Each l_account As CRUD.Account In l_rhAccounts
                    GeneralUtilities.AddItemToCombobox(m_accountSelectionComboBox, l_account.Id, l_account.Name)
                Next
                m_accountSelectionComboBox.SelectedItem = m_accountSelectionComboBox.Items(0)

        End Select

    End Sub

#End Region

#Region "Call backs and events"

    Private Sub m_validateButton_Click(sender As Object, e As EventArgs) Handles m_validateButton.Click
        If m_directoryTextBox.Text <> "" _
        AndAlso m_worksheetTargetName.Text <> "" _
        AndAlso m_accountSelectionComboBox.Text <> "" Then
            Me.Hide()
            '  Dim l_BulkUpload As New FolderBulkReportUpload
            LoopInDirectoryFiles(m_directoryTextBox.Text, _
                                 m_worksheetTargetName.Text, _
                                 m_accountSelectionComboBox.Text, _
                                 m_periodSelectionControl.GetPeriodList())
        End If

    End Sub

    Private Sub m_directoryTextBox_Enter(sender As Object, e As EventArgs) Handles m_directoryTextBox.Enter

        Dim result = m_FolderBrowserDialog1.ShowDialog()
        If (result = Windows.Forms.DialogResult.OK) Then
            m_directoryTextBox.Text = m_FolderBrowserDialog1.SelectedPath
        End If

    End Sub


#End Region


#Region "Folder's Loop"

    Friend Sub LoopInDirectoryFiles(ByRef p_folderPath As String, _
                                     ByRef p_targetWorksheetName As String, _
                                     ByRef p_RHaccountName As String, _
                                     ByRef p_periodsRange As List(Of Int32))

        m_filePaths = GetFiles(p_folderPath, "*.xls|*.xlsx|*.xlsm", SearchOption.AllDirectories)
        If m_filePaths IsNot Nothing Then
            Console.WriteLine("Started Directory Snapshot. Loop in files.")
            Dim l_calculation As Excel.XlCalculation = GlobalVariables.APPS.Calculation
            GlobalVariables.APPS.Calculation = Excel.XlCalculation.xlCalculationManual
            IterateThroughWorkbooks(0)
            GlobalVariables.APPS.Calculation = l_calculation
        Else
            MsgBox("Invalid directory path")
        End If

    End Sub

    Private Sub IterateThroughWorkbooks(ByRef p_workbookIndex As Int32)

            If p_workbookIndex <= m_filePaths.Length - 1 Then
                Dim l_file = m_filePaths(p_workbookIndex)
                m_workbookIndex = p_workbookIndex
                m_currentWorkbook = GlobalVariables.APPS.Workbooks.Open(l_file, 0, False, 5, "", "", False, Excel.XlPlatform.xlWindows, "\t", False, False, 0, False, True, False)
                IterateThroughWorksheets(1)
            Else
                MsgBox("Directory snapshot ended")
                Me.Close()
            End If
  
    End Sub

    Private Delegate Sub IterateThroughWorksheets_Delegate(ByRef p_startIndex As Int32)
    Private Sub IterateThroughWorksheets(ByRef p_startIndex As Int32)

        If Me.InvokeRequired Then
            Dim MyDelegate As New IterateThroughWorksheets_Delegate(AddressOf IterateThroughWorksheets)
            Me.Invoke(MyDelegate, New Object() {p_startIndex})
        Else
            SyncLock (m_currentWorkbook)
                If p_startIndex <= m_currentWorkbook.Sheets.Count Then
                    For l_worksheetIndex As Int32 = p_startIndex To m_currentWorkbook.Sheets.Count
                        Dim l_ws As Excel.Worksheet = m_currentWorkbook.Sheets(l_worksheetIndex)
                        If l_ws.Name = m_worksheetTargetName.Text Then
                            SnapshotWorksheet(l_ws)
                            Exit Sub
                        End If
                    Next
                    CloseWorkbook()
                Else
                    CloseWorkbook()
                End If
            End SyncLock
        End If

    End Sub

    Private Sub CloseWorkbook()
        m_currentWorkbook.Close(False, "", False)
        IterateThroughWorkbooks(m_workbookIndex + 1)
    End Sub

    Private Delegate Sub SnapshotWorksheet_Delegate(ByRef p_worksheet As Excel.Worksheet)
    Private Sub SnapshotWorksheet(ByRef p_worksheet As Excel.Worksheet)

        If Me.InvokeRequired Then
            Dim MyDelegate As New SnapshotWorksheet_Delegate(AddressOf SnapshotWorksheet)
            Me.Invoke(MyDelegate, New Object() {p_worksheet})
        Else
            p_worksheet.Activate()
            Console.WriteLine("Starting Snapshot for worksheet: " & p_worksheet.Name)
            m_reportUploadController = GlobalVariables.Addin.AssociateReportUploadControler(False, _
                                                                                           m_periodSelectionControl.GetPeriodList, _
                                                                                           m_accountSelectionComboBox.Text)
            If m_reportUploadController IsNot Nothing Then
                AddHandler m_reportUploadController.AfterSnapshotInitialized, AddressOf AfterSnapshotInitialized
                AddHandler m_reportUploadController.AfterSubmission, AddressOf AfterSubmission
            Else
                IterateThroughWorksheets(p_worksheet.Index + 1)
            End If
        End If

    End Sub

   
#End Region

#Region "Events"

    Private Sub AfterSnapshotInitialized(ByRef p_status As Boolean)
        If p_status = True Then
            m_reportUploadController.DataSubmission()
        End If
    End Sub

    Private Sub AfterSubmission(ByRef p_status As Boolean, ByRef p_ws As Excel.Worksheet)
        SyncLock (m_reportUploadController)
            If p_status = False Then
                m_errors.Add(p_ws.Application.ActiveWorkbook.Name)
            End If
            GlobalVariables.Addin.ClearSubmissionMode(m_reportUploadController)
            IterateThroughWorksheets(p_ws.Index + 1)
        End SyncLock

    End Sub

#End Region


#Region "Utilities"

    '    /// <summary>
    '/// Returns file names from given folder that comply to given filters
    '/// </summary>
    '/// <param name="SourceFolder">Folder with files to retrieve</param>
    '/// <param name="Filter">Multiple file filters separated by | character</param>
    '/// <param name="searchOption">File.IO.SearchOption, 
    '/// could be AllDirectories or TopDirectoryOnly</param>
    '/// <returns>Array of FileInfo objects that presents collection of file names that 
    '/// meet given filter</returns>
    Private Shared Function GetFiles(SourceFolder As String, Filter As String, searchOption As System.IO.SearchOption) As String()

        On Error GoTo ErrorHandler
        ' ArrayList will hold all file names
        Dim alFiles As ArrayList = New ArrayList()

        ' Create an array of filter string
        Dim MultipleFilters As String() = Filter.Split("|")

        ' for each filter find mathing file names
        For Each FileFilter As String In MultipleFilters
            ' add found file names to array list
            alFiles.AddRange(Directory.GetFiles(SourceFolder, FileFilter, searchOption))
        Next

        ' returns string array of relevant file names
        Dim l_files(alFiles.Count - 1) As String
        Dim i As Int32 = 0
        For Each l_file As String In alFiles
            l_files(i) = l_file
            i += 1
        Next
        Return l_files

ErrorHandler:
        Return Nothing

    End Function

#End Region



End Class