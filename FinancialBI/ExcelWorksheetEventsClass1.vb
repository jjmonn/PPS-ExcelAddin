Imports System
Imports System.Windows.Forms
Imports AddinExpress.MSO
Imports Microsoft.Office.Interop

'Add-in Express Excel Worksheet Events Class
Public Class ExcelWorksheetEventsClass1
    Inherits AddinExpress.MSO.ADXExcelWorksheetEvents

    Private m_addinModule As AddinModule

    Public Sub New(ByVal ADXModule As AddinExpress.MSO.ADXAddinModule)

        MyBase.New(ADXModule)
        m_addinModule = CType(ADXModule, AddinModule)

    End Sub



#Region "Used events"

    Public Overrides Sub ProcessChange(ByVal Target As Object)

        If m_addinModule Is Nothing Then Exit Sub
        Dim l_cell As Excel.Range = CType(Target, Excel.Range)
        If l_cell Is Nothing Then Exit Sub
        If m_addinModule.ReportUploadControlersDictionary IsNot Nothing _
        AndAlso m_addinModule.ReportUploadControlersDictionary.ContainsKey(l_cell.Worksheet.Name) Then
            m_addinModule.ReportUploadControlersDictionary(l_cell.Worksheet.Name).Worksheet_Change(l_cell)
        End If

    End Sub

    Public Overrides Sub ProcessSelectionChange(ByVal Target As Object)

        If m_addinModule Is Nothing Then Exit Sub
        Dim l_cell As Excel.Range = CType(Target, Excel.Range)
        If l_cell Is Nothing Then Exit Sub
        If m_addinModule.ReportUploadControlersDictionary IsNot Nothing _
        AndAlso m_addinModule.ReportUploadControlersDictionary.ContainsKey(l_cell.Worksheet.Name) Then
            m_addinModule.ReportUploadControlersDictionary(l_cell.Worksheet.Name).Worksheet_SelectionChange(l_cell)
        End If

    End Sub

    Public Overrides Sub ProcessBeforeRightClick(ByVal Target As Object, ByVal E As AddinExpress.MSO.ADXCancelEventArgs)

         If m_addinModule Is Nothing Then Exit Sub
        Dim l_cell As Excel.Range = CType(Target, Excel.Range)
        If l_cell Is Nothing Then Exit Sub
        If m_addinModule.ReportUploadControlersDictionary IsNot Nothing _
        AndAlso m_addinModule.ReportUploadControlersDictionary.ContainsKey(l_cell.Worksheet.Name) Then
            m_addinModule.ReportUploadControlersDictionary(l_cell.Worksheet.Name).Worksheet_BeforeRightClick(l_cell, False)
        End If


    End Sub

#End Region

#Region "Unused Events"

    Public Overrides Sub ProcessBeforeDoubleClick(ByVal Target As Object, ByVal ElementID As Integer, ByVal Arg1 As Integer, ByVal Arg2 As Integer, ByVal E As AddinExpress.MSO.ADXCancelEventArgs)
        ' TODO: Add some code
    End Sub

    Public Overrides Sub ProcessActivate()
        ' TODO: Add some code
    End Sub

    Public Overrides Sub ProcessDeactivate()
        ' TODO: Add some code
    End Sub

    Public Overrides Sub ProcessCalculate()
        ' TODO: Add some code
    End Sub



    Public Overrides Sub ProcessFollowHyperlink(ByVal Target As Object)
        ' TODO: Add some code
    End Sub

    Public Overrides Sub ProcessPivotTableUpdate(ByVal Target As Object)
        ' TODO: Add some code
    End Sub

    Public Overrides Sub ProcessResize()
        ' TODO: Add some code
    End Sub

    Public Overrides Sub ProcessMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        ' TODO: Add some code
    End Sub

    Public Overrides Sub ProcessMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        ' TODO: Add some code
    End Sub

    Public Overrides Sub ProcessMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        ' TODO: Add some code
    End Sub

    Public Overrides Sub ProcessDragPlot()
        ' TODO: Add some code
    End Sub

    Public Overrides Sub ProcessDragOver()
        ' TODO: Add some code
    End Sub

    Public Overrides Sub ProcessSelect(ByVal ElementID As Integer, ByVal Arg1 As Integer, ByVal Arg2 As Integer)
        ' TODO: Add some code
    End Sub

    Public Overrides Sub ProcessSeriesChange(ByVal SeriesIndex As Integer, ByVal PointIndex As Integer)
        ' TODO: Add some code
    End Sub

    Public Overrides Sub ProcessPivotTableAfterValueChange(ByVal TargetPivotTable As Object, ByVal TargetRange As Object)
        ' TODO: Add some code
    End Sub

    Public Overrides Sub ProcessPivotTableBeforeAllocateChanges(ByVal TargetPivotTable As Object, ByVal ValueChangeStart As Integer, ByVal ValueChangeEnd As Integer, ByVal E As AddinExpress.MSO.ADXCancelEventArgs)
        ' TODO: Add some code
    End Sub

    Public Overrides Sub ProcessPivotTableBeforeCommitChanges(ByVal TargetPivotTable As Object, ByVal ValueChangeStart As Integer, ByVal ValueChangeEnd As Integer, ByVal E As AddinExpress.MSO.ADXCancelEventArgs)
        ' TODO: Add some code
    End Sub

    Public Overrides Sub ProcessPivotTableBeforeDiscardChanges(ByVal targetPivotTable As Object, ByVal ValueChangeStart As Integer, ByVal ValueChangeEnd As Integer)
        ' TODO: Add some code
    End Sub

    Public Overrides Sub ProcessPivotTableChangeSync(ByVal target As Object)
        ' TODO: Add some code
    End Sub


#End Region


End Class

