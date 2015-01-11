﻿' AddinModule.vb
'
' Manage addin loading and ribbons
'
'
' To do:
'       - careful GRS Controlers and worksheet names change -> should update dictionary
'       - How to highlight submission ribbon
'       - > need a check that a version is selected everywhere !!!
'
'       - > Need a flag or trigger indicating when the model or mappings has changed so that ExcelAddinModule1 is udpated when necessary
'           find a workaround to share objects between the two addins module...!!
'
' Known bugs:
'       -
'
'
' Author: Julien Monnereau/ Addin Express automated code
' Last modified: 23/12/2014


Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Windows.Forms
Imports AddinExpress.MSO
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Collections.Generic


'Add-in Express Add-in Module"
<GuidAttribute("C5985605-3A21-426D-8DC3-B38EEBDA50C8"), ProgIdAttribute("PPS.AddinModule")> _
Public Class AddinModule

    Inherits AddinExpress.MSO.ADXAddinModule


#Region "instance Variables"


    Friend WithEvents RefreshBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonGroup2 As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents AdxRibbonGroup1 As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents MaintTab As AddinExpress.MSO.ADXRibbonTab
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents WSUplaodBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents ControlingUI2BT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonGroup3 As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents EntitiesMGTBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AccountsMGTBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonGroup5 As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents adxExcelEvents As AddinExpress.MSO.ADXExcelAppEvents
    Friend WithEvents UploadBT As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents UplodBT1 As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents WBUplaodBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonSeparator2 As AddinExpress.MSO.ADXRibbonSeparator
    Friend WithEvents SettingsBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents usersMGTBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdvancesModelingBT As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents AdxRibbonMenu2 As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents BreakLinksBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonGroup6 As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents AdxRibbonMenu5 As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents VersionsMGTBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonGroup7 As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents PPTExportBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents ReportFmtBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents LightsImageList As System.Windows.Forms.ImageList
    Friend WithEvents AdxRibbonSeparator8 As AddinExpress.MSO.ADXRibbonSeparator
    Friend WithEvents PlatformMGTBT As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents AdxRibbonMenu4 As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents CategoriesMGTBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents FormattingBT As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents AdxRibbonMenu6 As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents InputFmtBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents VersionBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents Addin_Version_label As AddinExpress.MSO.ADXRibbonLabel
    Friend WithEvents ConnectionIcons As System.Windows.Forms.ImageList
    Friend WithEvents ConnectionBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonSeparator1 As AddinExpress.MSO.ADXRibbonSeparator
    Friend WithEvents CurrenciesBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents SubmissionModeRibbon As AddinExpress.MSO.ADXRibbonTab
    Friend WithEvents EditSelectionGroup As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents HighlightBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents ShowReportBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents SubmissionnGroup As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents SubmitBT2 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents StateSelectionGroup As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents CurrentEntityTB As AddinExpress.MSO.ADXRibbonEditBox
    Friend WithEvents AdxRibbonGroup9 As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents CloseBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents OpenTemplateBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents SaveTemplateBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents VersionTBSubRibbon As AddinExpress.MSO.ADXRibbonEditBox
    Friend WithEvents CancelBT2 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents SubmissionRibbonIL As System.Windows.Forms.ImageList
    Friend WithEvents AdxRibbonSeparator4 As AddinExpress.MSO.ADXRibbonSeparator
    Friend WithEvents AdxRibbonSeparator5 As AddinExpress.MSO.ADXRibbonSeparator
    Friend WithEvents AdxRibbonSeparator3 As AddinExpress.MSO.ADXRibbonSeparator
    Friend WithEvents AccountStatusBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents SubmissionStatus As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents CurrentLinkedWSBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonSeparator6 As AddinExpress.MSO.ADXRibbonSeparator
    Friend WithEvents AutoComitBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents EntCurrTB As AddinExpress.MSO.ADXRibbonEditBox
    Friend WithEvents ReportOptionsBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RefreshInputsBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents entityEditBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents CurrencyLabel As AddinExpress.MSO.ADXRibbonLabel
    Friend WithEvents VersionBT2 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonSeparator7 As AddinExpress.MSO.ADXRibbonSeparator
    Friend WithEvents EditRangesMenuBT As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents EditRangesMenu As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents SelectAccRangeBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents SelectEntitiesRangeBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents SelectPeriodsRangeBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents InputReportLaunchBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxExcelTaskPanesManager1 As AddinExpress.XL.ADXExcelTaskPanesManager
    Friend WithEvents InputSelectionTaskPaneItem As AddinExpress.XL.ADXExcelTaskPanesCollectionItem
    Friend WithEvents AdxRibbonLabel1 As AddinExpress.MSO.ADXRibbonLabel
    Friend WithEvents VersionSelectionTaskPaneItem As AddinExpress.XL.ADXExcelTaskPanesCollectionItem
    Friend WithEvents EntitySelectionTaskPaneItem As AddinExpress.XL.ADXExcelTaskPanesCollectionItem
    Friend WithEvents WSCB As AddinExpress.MSO.ADXRibbonDropDown
    Friend WithEvents AdxRibbonItem2 As AddinExpress.MSO.ADXRibbonItem
    Friend WithEvents Addin_rates_version_label As AddinExpress.MSO.ADXRibbonLabel
    Friend WithEvents ControlsMGTBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents LogBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents SubmissionControlBT As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents AdxRibbonMenu3 As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents FunctionDesigner As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents AdxRibbonMenu1 As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents AdxRibbonSeparator9 As AddinExpress.MSO.ADXRibbonSeparator
    Friend WithEvents AdxRibbonSeparator10 As AddinExpress.MSO.ADXRibbonSeparator
    Friend WithEvents AdxRibbonSeparator11 As AddinExpress.MSO.ADXRibbonSeparator
    Friend WithEvents AdxRibbonSeparator12 As AddinExpress.MSO.ADXRibbonSeparator
    Friend WithEvents AdxRibbonSeparator13 As AddinExpress.MSO.ADXRibbonSeparator


#End Region


#Region " Component Designer generated code. "
    'Required by designer
    Private components As System.ComponentModel.IContainer

    'Required by designer - do not modify
    'the following method
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddinModule))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.MaintTab = New AddinExpress.MSO.ADXRibbonTab(Me.components)
        Me.AdxRibbonGroup6 = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.ConnectionBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.ConnectionIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.AdxRibbonSeparator1 = New AddinExpress.MSO.ADXRibbonSeparator(Me.components)
        Me.VersionBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.Addin_Version_label = New AddinExpress.MSO.ADXRibbonLabel(Me.components)
        Me.Addin_rates_version_label = New AddinExpress.MSO.ADXRibbonLabel(Me.components)
        Me.AdxRibbonGroup1 = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.UploadBT = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.UplodBT1 = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.WSUplaodBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.WBUplaodBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.CurrentLinkedWSBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonSeparator9 = New AddinExpress.MSO.ADXRibbonSeparator(Me.components)
        Me.InputReportLaunchBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonSeparator8 = New AddinExpress.MSO.ADXRibbonSeparator(Me.components)
        Me.RefreshBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonMenu5 = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.AdxRibbonGroup2 = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.ControlingUI2BT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.SubmissionControlBT = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.AdxRibbonMenu3 = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.LogBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonSeparator2 = New AddinExpress.MSO.ADXRibbonSeparator(Me.components)
        Me.FunctionDesigner = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.AdxRibbonMenu1 = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.BreakLinksBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonSeparator10 = New AddinExpress.MSO.ADXRibbonSeparator(Me.components)
        Me.AdvancesModelingBT = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.AdxRibbonMenu2 = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.AdxRibbonGroup3 = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.PlatformMGTBT = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.AdxRibbonMenu4 = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.AccountsMGTBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.EntitiesMGTBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.CategoriesMGTBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.VersionsMGTBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.ControlsMGTBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonSeparator11 = New AddinExpress.MSO.ADXRibbonSeparator(Me.components)
        Me.CurrenciesBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonSeparator12 = New AddinExpress.MSO.ADXRibbonSeparator(Me.components)
        Me.usersMGTBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonGroup7 = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.PPTExportBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonSeparator13 = New AddinExpress.MSO.ADXRibbonSeparator(Me.components)
        Me.FormattingBT = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.AdxRibbonMenu6 = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.ReportFmtBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.InputFmtBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonGroup5 = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.SettingsBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.SubmissionRibbonIL = New System.Windows.Forms.ImageList(Me.components)
        Me.LightsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.adxExcelEvents = New AddinExpress.MSO.ADXExcelAppEvents(Me.components)
        Me.SubmissionModeRibbon = New AddinExpress.MSO.ADXRibbonTab(Me.components)
        Me.EditSelectionGroup = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.HighlightBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RefreshInputsBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonSeparator4 = New AddinExpress.MSO.ADXRibbonSeparator(Me.components)
        Me.EditRangesMenuBT = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.EditRangesMenu = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.SelectAccRangeBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.SelectEntitiesRangeBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.SelectPeriodsRangeBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonSeparator7 = New AddinExpress.MSO.ADXRibbonSeparator(Me.components)
        Me.AccountStatusBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.WSCB = New AddinExpress.MSO.ADXRibbonDropDown(Me.components)
        Me.AdxRibbonItem2 = New AddinExpress.MSO.ADXRibbonItem(Me.components)
        Me.AdxRibbonSeparator5 = New AddinExpress.MSO.ADXRibbonSeparator(Me.components)
        Me.OpenTemplateBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.SaveTemplateBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.SubmissionnGroup = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.SubmitBT2 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.SubmissionStatus = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.CancelBT2 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonSeparator6 = New AddinExpress.MSO.ADXRibbonSeparator(Me.components)
        Me.AutoComitBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.StateSelectionGroup = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.entityEditBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.CurrencyLabel = New AddinExpress.MSO.ADXRibbonLabel(Me.components)
        Me.VersionBT2 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.CurrentEntityTB = New AddinExpress.MSO.ADXRibbonEditBox(Me.components)
        Me.EntCurrTB = New AddinExpress.MSO.ADXRibbonEditBox(Me.components)
        Me.VersionTBSubRibbon = New AddinExpress.MSO.ADXRibbonEditBox(Me.components)
        Me.AdxRibbonGroup9 = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.ShowReportBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.ReportOptionsBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonSeparator3 = New AddinExpress.MSO.ADXRibbonSeparator(Me.components)
        Me.CloseBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxExcelTaskPanesManager1 = New AddinExpress.XL.ADXExcelTaskPanesManager(Me.components)
        Me.InputSelectionTaskPaneItem = New AddinExpress.XL.ADXExcelTaskPanesCollectionItem(Me.components)
        Me.VersionSelectionTaskPaneItem = New AddinExpress.XL.ADXExcelTaskPanesCollectionItem(Me.components)
        Me.EntitySelectionTaskPaneItem = New AddinExpress.XL.ADXExcelTaskPanesCollectionItem(Me.components)
        Me.AdxRibbonLabel1 = New AddinExpress.MSO.ADXRibbonLabel(Me.components)
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "PPS ICON.png")
        Me.ImageList1.Images.SetKeyName(1, "imageres_89.ico")
        Me.ImageList1.Images.SetKeyName(2, "imageres_9.ico")
        Me.ImageList1.Images.SetKeyName(3, "imageres_8.ico")
        Me.ImageList1.Images.SetKeyName(4, "favicon (44).ico")
        Me.ImageList1.Images.SetKeyName(5, "favicon (45).ico")
        Me.ImageList1.Images.SetKeyName(6, "chart.ico")
        Me.ImageList1.Images.SetKeyName(7, "cloud_download.ico")
        Me.ImageList1.Images.SetKeyName(8, "globe_down.ico")
        Me.ImageList1.Images.SetKeyName(9, "world.ico")
        Me.ImageList1.Images.SetKeyName(10, "favicon (6).ico")
        Me.ImageList1.Images.SetKeyName(11, "favicon (50).ico")
        Me.ImageList1.Images.SetKeyName(12, "favicon (51).ico")
        Me.ImageList1.Images.SetKeyName(13, "favicon (53).ico")
        Me.ImageList1.Images.SetKeyName(14, "favicon (64).ico")
        Me.ImageList1.Images.SetKeyName(15, "favicon (70).ico")
        Me.ImageList1.Images.SetKeyName(16, "imageres_190.ico")
        Me.ImageList1.Images.SetKeyName(17, "favicon(20).ico")
        Me.ImageList1.Images.SetKeyName(18, "favicon(23).ico")
        Me.ImageList1.Images.SetKeyName(19, "favicon (28).ico")
        Me.ImageList1.Images.SetKeyName(20, "favicon(40).ico")
        Me.ImageList1.Images.SetKeyName(21, "favicon(44).ico")
        Me.ImageList1.Images.SetKeyName(22, "favicon(45).ico")
        Me.ImageList1.Images.SetKeyName(23, "favicon(63).ico")
        Me.ImageList1.Images.SetKeyName(24, "favicon(62).ico")
        Me.ImageList1.Images.SetKeyName(25, "ppt1.jpg")
        Me.ImageList1.Images.SetKeyName(26, "favicon(65).ico")
        Me.ImageList1.Images.SetKeyName(27, "favicon(69).ico")
        Me.ImageList1.Images.SetKeyName(28, "favicon(77).ico")
        Me.ImageList1.Images.SetKeyName(29, "favicon(78).ico")
        Me.ImageList1.Images.SetKeyName(30, "favicon(142).ico")
        Me.ImageList1.Images.SetKeyName(31, "favicon(143).ico")
        Me.ImageList1.Images.SetKeyName(32, "favicon(144).ico")
        Me.ImageList1.Images.SetKeyName(33, "favicon(145).ico")
        Me.ImageList1.Images.SetKeyName(34, "favicon(191).ico")
        Me.ImageList1.Images.SetKeyName(35, "favicon(149).ico")
        Me.ImageList1.Images.SetKeyName(36, "favicon(148).ico")
        Me.ImageList1.Images.SetKeyName(37, "favicon(150).ico")
        Me.ImageList1.Images.SetKeyName(38, "favicon(161).ico")
        Me.ImageList1.Images.SetKeyName(39, "favicon(160).ico")
        Me.ImageList1.Images.SetKeyName(40, "favicon(159).ico")
        Me.ImageList1.Images.SetKeyName(41, "favicon(158).ico")
        Me.ImageList1.Images.SetKeyName(42, "favicon(157).ico")
        Me.ImageList1.Images.SetKeyName(43, "favicon(156).ico")
        Me.ImageList1.Images.SetKeyName(44, "favicon(155).ico")
        Me.ImageList1.Images.SetKeyName(45, "favicon(154).ico")
        Me.ImageList1.Images.SetKeyName(46, "favicon(153).ico")
        Me.ImageList1.Images.SetKeyName(47, "favicon(152).ico")
        Me.ImageList1.Images.SetKeyName(48, "favicon(151).ico")
        Me.ImageList1.Images.SetKeyName(49, "favicon(192).ico")
        Me.ImageList1.Images.SetKeyName(50, "Settings.ico")
        Me.ImageList1.Images.SetKeyName(51, "favicon(167).ico")
        Me.ImageList1.Images.SetKeyName(52, "favicon(166).ico")
        Me.ImageList1.Images.SetKeyName(53, "favicon(165).ico")
        Me.ImageList1.Images.SetKeyName(54, "favicon(164).ico")
        Me.ImageList1.Images.SetKeyName(55, "favicon(163).ico")
        Me.ImageList1.Images.SetKeyName(56, "favicon(162).ico")
        Me.ImageList1.Images.SetKeyName(57, "lens_icon.ico")
        Me.ImageList1.Images.SetKeyName(58, "favicon(193).ico")
        Me.ImageList1.Images.SetKeyName(59, "favicon(201).ico")
        Me.ImageList1.Images.SetKeyName(60, "favicon(203).ico")
        Me.ImageList1.Images.SetKeyName(61, "favicon(205).ico")
        Me.ImageList1.Images.SetKeyName(62, "favicon(206).ico")
        Me.ImageList1.Images.SetKeyName(63, "favicon(208).ico")
        Me.ImageList1.Images.SetKeyName(64, "favicon(216).ico")
        Me.ImageList1.Images.SetKeyName(65, "favicon(215).ico")
        Me.ImageList1.Images.SetKeyName(66, "favicon(198).ico")
        Me.ImageList1.Images.SetKeyName(67, "favicon(223).ico")
        Me.ImageList1.Images.SetKeyName(68, "favicon(225).ico")
        Me.ImageList1.Images.SetKeyName(69, "favicon(240).ico")
        Me.ImageList1.Images.SetKeyName(70, "favicon(111).ico")
        Me.ImageList1.Images.SetKeyName(71, "favicon(242).ico")
        Me.ImageList1.Images.SetKeyName(72, "1420498403_340208.ico")
        Me.ImageList1.Images.SetKeyName(73, "favicon(243).ico")
        Me.ImageList1.Images.SetKeyName(74, "favicon(244).ico")
        Me.ImageList1.Images.SetKeyName(75, "favicon(245).ico")
        Me.ImageList1.Images.SetKeyName(76, "connect blue.png")
        Me.ImageList1.Images.SetKeyName(77, "favicon(246).ico")
        Me.ImageList1.Images.SetKeyName(78, "blue-icon-md.png")
        Me.ImageList1.Images.SetKeyName(79, "currencies blue.png")
        Me.ImageList1.Images.SetKeyName(80, "favicon(248).ico")
        Me.ImageList1.Images.SetKeyName(81, "favicon(234).ico")
        Me.ImageList1.Images.SetKeyName(82, "favicon(247).ico")
        Me.ImageList1.Images.SetKeyName(83, "favicon(250).ico")
        Me.ImageList1.Images.SetKeyName(84, "social-myspace-button-blue-icon.png")
        Me.ImageList1.Images.SetKeyName(85, "favicon(252).ico")
        Me.ImageList1.Images.SetKeyName(86, "favicon(259).ico")
        Me.ImageList1.Images.SetKeyName(87, "favicon(263).ico")
        Me.ImageList1.Images.SetKeyName(88, "favicon(264).ico")
        '
        'MaintTab
        '
        Me.MaintTab.Caption = "Corporate Intelligence"
        Me.MaintTab.Controls.Add(Me.AdxRibbonGroup6)
        Me.MaintTab.Controls.Add(Me.AdxRibbonGroup1)
        Me.MaintTab.Controls.Add(Me.AdxRibbonGroup2)
        Me.MaintTab.Controls.Add(Me.AdxRibbonGroup3)
        Me.MaintTab.Controls.Add(Me.AdxRibbonGroup7)
        Me.MaintTab.Controls.Add(Me.AdxRibbonGroup5)
        Me.MaintTab.Id = "adxRibbonTab_b30b165b93e0463887478085c350e723"
        Me.MaintTab.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonGroup6
        '
        Me.AdxRibbonGroup6.Caption = "Connection"
        Me.AdxRibbonGroup6.CenterVertically = True
        Me.AdxRibbonGroup6.Controls.Add(Me.ConnectionBT)
        Me.AdxRibbonGroup6.Controls.Add(Me.AdxRibbonSeparator1)
        Me.AdxRibbonGroup6.Controls.Add(Me.VersionBT)
        Me.AdxRibbonGroup6.Controls.Add(Me.Addin_Version_label)
        Me.AdxRibbonGroup6.Controls.Add(Me.Addin_rates_version_label)
        Me.AdxRibbonGroup6.Id = "adxRibbonGroup_89e20fb41d2445e782d9c54beb6faae8"
        Me.AdxRibbonGroup6.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonGroup6.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'ConnectionBT
        '
        Me.ConnectionBT.Caption = "Connection "
        Me.ConnectionBT.Id = "adxRibbonButton_f2e70238179d4fb68a52b7d77fde6971"
        Me.ConnectionBT.Image = 0
        Me.ConnectionBT.ImageList = Me.ConnectionIcons
        Me.ConnectionBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ConnectionBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.ConnectionBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'ConnectionIcons
        '
        Me.ConnectionIcons.ImageStream = CType(resources.GetObject("ConnectionIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ConnectionIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.ConnectionIcons.Images.SetKeyName(0, "favicon(155).ico")
        Me.ConnectionIcons.Images.SetKeyName(1, "favicon(154).ico")
        Me.ConnectionIcons.Images.SetKeyName(2, "connection red 3.png")
        Me.ConnectionIcons.Images.SetKeyName(3, "connection green 2.png")
        Me.ConnectionIcons.Images.SetKeyName(4, "favicon(153).ico")
        '
        'AdxRibbonSeparator1
        '
        Me.AdxRibbonSeparator1.Id = "adxRibbonSeparator_efa53406578a435ea76b4e776683ce26"
        Me.AdxRibbonSeparator1.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'VersionBT
        '
        Me.VersionBT.Caption = "Versions selection"
        Me.VersionBT.Id = "adxRibbonButton_47a60ea441584fe3b0b975b2829b6ec1"
        Me.VersionBT.Image = 48
        Me.VersionBT.ImageList = Me.ImageList1
        Me.VersionBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.VersionBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'Addin_Version_label
        '
        Me.Addin_Version_label.Caption = "Data Version"
        Me.Addin_Version_label.Id = "adxRibbonLabel_b8b0a822a6f6432fb5ed3a82568b76ea"
        Me.Addin_Version_label.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'Addin_rates_version_label
        '
        Me.Addin_rates_version_label.Caption = "FX Rates Version"
        Me.Addin_rates_version_label.Id = "adxRibbonLabel_43aa53308e4d4b3a8e3d7193f8945b6f"
        Me.Addin_rates_version_label.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonGroup1
        '
        Me.AdxRibbonGroup1.Caption = "Data Upload"
        Me.AdxRibbonGroup1.Controls.Add(Me.UploadBT)
        Me.AdxRibbonGroup1.Controls.Add(Me.AdxRibbonSeparator9)
        Me.AdxRibbonGroup1.Controls.Add(Me.InputReportLaunchBT)
        Me.AdxRibbonGroup1.Controls.Add(Me.AdxRibbonSeparator8)
        Me.AdxRibbonGroup1.Controls.Add(Me.RefreshBT)
        Me.AdxRibbonGroup1.Controls.Add(Me.AdxRibbonMenu5)
        Me.AdxRibbonGroup1.Id = "adxRibbonGroup_6d270a7302274c0bb0cb396921e59e09"
        Me.AdxRibbonGroup1.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonGroup1.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'UploadBT
        '
        Me.UploadBT.Caption = "Snapshot"
        Me.UploadBT.Controls.Add(Me.UplodBT1)
        Me.UploadBT.Id = "adxRibbonSplitButton_d2989ac910ad415381c6cc902b2051e5"
        Me.UploadBT.Image = 76
        Me.UploadBT.ImageList = Me.ImageList1
        Me.UploadBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.UploadBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.UploadBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'UplodBT1
        '
        Me.UplodBT1.Caption = "AdxRibbonMenu1"
        Me.UplodBT1.Controls.Add(Me.WSUplaodBT)
        Me.UplodBT1.Controls.Add(Me.WBUplaodBT)
        Me.UplodBT1.Controls.Add(Me.CurrentLinkedWSBT)
        Me.UplodBT1.Id = "adxRibbonMenu_e0b4ef4400e94c5daab76efc03b1c471"
        Me.UplodBT1.ImageMso = "SmartArtChangeColorGallery"
        Me.UplodBT1.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.UplodBT1.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.UplodBT1.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'WSUplaodBT
        '
        Me.WSUplaodBT.Caption = "Current Worksheet Upload"
        Me.WSUplaodBT.Id = "adxRibbonButton_4bc310353ac24cecbc9d51abdb4fd682"
        Me.WSUplaodBT.ImageList = Me.ImageList1
        Me.WSUplaodBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.WSUplaodBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.WSUplaodBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'WBUplaodBT
        '
        Me.WBUplaodBT.Caption = "Workbook Upload"
        Me.WBUplaodBT.Id = "adxRibbonButton_24b9e7a516574e889eda47fea0a5cc43"
        Me.WBUplaodBT.ImageList = Me.ImageList1
        Me.WBUplaodBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.WBUplaodBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.WBUplaodBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'CurrentLinkedWSBT
        '
        Me.CurrentLinkedWSBT.Caption = "Show current Linked worksheets"
        Me.CurrentLinkedWSBT.Id = "adxRibbonButton_c06e146922f940e9b666907620e328c9"
        Me.CurrentLinkedWSBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.CurrentLinkedWSBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonSeparator9
        '
        Me.AdxRibbonSeparator9.Id = "adxRibbonSeparator_2d8067dbedd44cb98a6fa6abf83dcbeb"
        Me.AdxRibbonSeparator9.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'InputReportLaunchBT
        '
        Me.InputReportLaunchBT.Caption = "Entity Edition"
        Me.InputReportLaunchBT.Id = "adxRibbonButton_7756d2a54a394f7b8fd2124ccddfa8e3"
        Me.InputReportLaunchBT.Image = 74
        Me.InputReportLaunchBT.ImageList = Me.ImageList1
        Me.InputReportLaunchBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.InputReportLaunchBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.InputReportLaunchBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonSeparator8
        '
        Me.AdxRibbonSeparator8.Id = "adxRibbonSeparator_88a3c0fb5346408b873557552bc2daf3"
        Me.AdxRibbonSeparator8.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'RefreshBT
        '
        Me.RefreshBT.Caption = "Refresh"
        Me.RefreshBT.Id = "adxRibbonButton_87f8b7409d6b41e19a24024d33bd3085"
        Me.RefreshBT.Image = 81
        Me.RefreshBT.ImageList = Me.ImageList1
        Me.RefreshBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RefreshBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonMenu5
        '
        Me.AdxRibbonMenu5.Caption = "Worksheets"
        Me.AdxRibbonMenu5.Id = "adxRibbonMenu_afc8f5e1d4994f2eba49f3c1fd9d0f3c"
        Me.AdxRibbonMenu5.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonMenu5.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonGroup2
        '
        Me.AdxRibbonGroup2.Caption = "Data Download"
        Me.AdxRibbonGroup2.Controls.Add(Me.ControlingUI2BT)
        Me.AdxRibbonGroup2.Controls.Add(Me.SubmissionControlBT)
        Me.AdxRibbonGroup2.Controls.Add(Me.AdxRibbonSeparator2)
        Me.AdxRibbonGroup2.Controls.Add(Me.FunctionDesigner)
        Me.AdxRibbonGroup2.Controls.Add(Me.AdxRibbonSeparator10)
        Me.AdxRibbonGroup2.Controls.Add(Me.AdvancesModelingBT)
        Me.AdxRibbonGroup2.Id = "adxRibbonGroup_13e7ba6b0acf4975af1793d5cfec00ed"
        Me.AdxRibbonGroup2.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonGroup2.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'ControlingUI2BT
        '
        Me.ControlingUI2BT.Caption = "Financial Statements"
        Me.ControlingUI2BT.Id = "adxRibbonButton_7d5683509a144f39bf95ea6b3db155b9"
        Me.ControlingUI2BT.Image = 73
        Me.ControlingUI2BT.ImageList = Me.ImageList1
        Me.ControlingUI2BT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ControlingUI2BT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.ControlingUI2BT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'SubmissionControlBT
        '
        Me.SubmissionControlBT.Caption = "Submissions Control"
        Me.SubmissionControlBT.Controls.Add(Me.AdxRibbonMenu3)
        Me.SubmissionControlBT.Id = "adxRibbonSplitButton_c948ce7f779a4f6cbe647f9de2d15b60"
        Me.SubmissionControlBT.Image = 72
        Me.SubmissionControlBT.ImageList = Me.ImageList1
        Me.SubmissionControlBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.SubmissionControlBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.SubmissionControlBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonMenu3
        '
        Me.AdxRibbonMenu3.Caption = "AdxRibbonMenu3"
        Me.AdxRibbonMenu3.Controls.Add(Me.LogBT)
        Me.AdxRibbonMenu3.Id = "adxRibbonMenu_d792f3eb075e45e5b8f825e6af002b3c"
        Me.AdxRibbonMenu3.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonMenu3.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'LogBT
        '
        Me.LogBT.Caption = "Log"
        Me.LogBT.Id = "adxRibbonButton_545fba9d563749dbbafe3968577cf15d"
        Me.LogBT.Image = 42
        Me.LogBT.ImageList = Me.ImageList1
        Me.LogBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.LogBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonSeparator2
        '
        Me.AdxRibbonSeparator2.Id = "adxRibbonSeparator_d6452408b9fd42a2ae10a9ca89e3b555"
        Me.AdxRibbonSeparator2.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'FunctionDesigner
        '
        Me.FunctionDesigner.Caption = "Get Data()"
        Me.FunctionDesigner.Controls.Add(Me.AdxRibbonMenu1)
        Me.FunctionDesigner.Id = "adxRibbonSplitButton_5986acbd4f414054bdd1177565ae2cf3"
        Me.FunctionDesigner.Image = 86
        Me.FunctionDesigner.ImageList = Me.ImageList1
        Me.FunctionDesigner.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.FunctionDesigner.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.FunctionDesigner.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonMenu1
        '
        Me.AdxRibbonMenu1.Caption = "AdxRibbonMenu1"
        Me.AdxRibbonMenu1.Controls.Add(Me.BreakLinksBT)
        Me.AdxRibbonMenu1.Id = "adxRibbonMenu_6e1a0370a3d0401780f6447d7d60358b"
        Me.AdxRibbonMenu1.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonMenu1.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'BreakLinksBT
        '
        Me.BreakLinksBT.Caption = "Break Links"
        Me.BreakLinksBT.Id = "adxRibbonButton_ac7f61741e004304a0942f8512a8456b"
        Me.BreakLinksBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.BreakLinksBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonSeparator10
        '
        Me.AdxRibbonSeparator10.Id = "adxRibbonSeparator_6870de15e9444c02a982637f9e485d3f"
        Me.AdxRibbonSeparator10.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdvancesModelingBT
        '
        Me.AdvancesModelingBT.Caption = "Financial Modeling"
        Me.AdvancesModelingBT.Controls.Add(Me.AdxRibbonMenu2)
        Me.AdvancesModelingBT.Id = "adxRibbonSplitButton_48193c7e13704758a3814aa6156d5aee"
        Me.AdvancesModelingBT.Image = 88
        Me.AdvancesModelingBT.ImageList = Me.ImageList1
        Me.AdvancesModelingBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdvancesModelingBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.AdvancesModelingBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonMenu2
        '
        Me.AdxRibbonMenu2.Caption = "Menu1"
        Me.AdxRibbonMenu2.Id = "adxRibbonMenu_e6e084f75db842218b137a40a859729b"
        Me.AdxRibbonMenu2.ImageMso = "EquationInsertGallery"
        Me.AdxRibbonMenu2.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonMenu2.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.AdxRibbonMenu2.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonGroup3
        '
        Me.AdxRibbonGroup3.Caption = "Configuration"
        Me.AdxRibbonGroup3.Controls.Add(Me.PlatformMGTBT)
        Me.AdxRibbonGroup3.Controls.Add(Me.AdxRibbonSeparator11)
        Me.AdxRibbonGroup3.Controls.Add(Me.CurrenciesBT)
        Me.AdxRibbonGroup3.Controls.Add(Me.AdxRibbonSeparator12)
        Me.AdxRibbonGroup3.Controls.Add(Me.usersMGTBT)
        Me.AdxRibbonGroup3.Id = "adxRibbonGroup_472aee773e454c20851d757e92f14553"
        Me.AdxRibbonGroup3.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonGroup3.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'PlatformMGTBT
        '
        Me.PlatformMGTBT.Caption = "Configuration"
        Me.PlatformMGTBT.Controls.Add(Me.AdxRibbonMenu4)
        Me.PlatformMGTBT.Id = "adxRibbonSplitButton_110ae5a8dc4b41d390a63c9d291591e7"
        Me.PlatformMGTBT.Image = 82
        Me.PlatformMGTBT.ImageList = Me.ImageList1
        Me.PlatformMGTBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PlatformMGTBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.PlatformMGTBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonMenu4
        '
        Me.AdxRibbonMenu4.Caption = "AdxRibbonMenu4"
        Me.AdxRibbonMenu4.Controls.Add(Me.AccountsMGTBT)
        Me.AdxRibbonMenu4.Controls.Add(Me.EntitiesMGTBT)
        Me.AdxRibbonMenu4.Controls.Add(Me.CategoriesMGTBT)
        Me.AdxRibbonMenu4.Controls.Add(Me.VersionsMGTBT)
        Me.AdxRibbonMenu4.Controls.Add(Me.ControlsMGTBT)
        Me.AdxRibbonMenu4.Id = "adxRibbonMenu_d285a2bbfc2943619d6d0764c3a4873b"
        Me.AdxRibbonMenu4.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonMenu4.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AccountsMGTBT
        '
        Me.AccountsMGTBT.Caption = "Financial and Operational Configuration"
        Me.AccountsMGTBT.Id = "adxRibbonButton_1f4c59e0ebe74522aeec6e1f98f166f7"
        Me.AccountsMGTBT.Image = 66
        Me.AccountsMGTBT.ImageList = Me.ImageList1
        Me.AccountsMGTBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AccountsMGTBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.AccountsMGTBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'EntitiesMGTBT
        '
        Me.EntitiesMGTBT.Caption = "Entities Configuration"
        Me.EntitiesMGTBT.Id = "adxRibbonButton_f0f162203bcf4c8d89476065c7be84c7"
        Me.EntitiesMGTBT.Image = 47
        Me.EntitiesMGTBT.ImageList = Me.ImageList1
        Me.EntitiesMGTBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.EntitiesMGTBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.EntitiesMGTBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'CategoriesMGTBT
        '
        Me.CategoriesMGTBT.Caption = "Categories Configuration"
        Me.CategoriesMGTBT.Id = "adxRibbonButton_0b711e24648a4bf0a1cd38ed823f25e0"
        Me.CategoriesMGTBT.Image = 9
        Me.CategoriesMGTBT.ImageList = Me.ImageList1
        Me.CategoriesMGTBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.CategoriesMGTBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'VersionsMGTBT
        '
        Me.VersionsMGTBT.Caption = "Versioning Configuration"
        Me.VersionsMGTBT.Id = "adxRibbonButton_210f77f8673344bc9f7a48e4ddb29501"
        Me.VersionsMGTBT.Image = 48
        Me.VersionsMGTBT.ImageList = Me.ImageList1
        Me.VersionsMGTBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.VersionsMGTBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.VersionsMGTBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'ControlsMGTBT
        '
        Me.ControlsMGTBT.Caption = "Submissions Controls Configuration"
        Me.ControlsMGTBT.Id = "adxRibbonButton_6fddb34dff474a7dbc254bb824b8d3ae"
        Me.ControlsMGTBT.Image = 40
        Me.ControlsMGTBT.ImageList = Me.ImageList1
        Me.ControlsMGTBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ControlsMGTBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonSeparator11
        '
        Me.AdxRibbonSeparator11.Id = "adxRibbonSeparator_28b372262f1943259fc3446c76d4e7a1"
        Me.AdxRibbonSeparator11.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'CurrenciesBT
        '
        Me.CurrenciesBT.Caption = "Exchange Rates"
        Me.CurrenciesBT.Id = "adxRibbonButton_06345cdcc1334bcea33718be31afaa83"
        Me.CurrenciesBT.Image = 80
        Me.CurrenciesBT.ImageList = Me.ImageList1
        Me.CurrenciesBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.CurrenciesBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.CurrenciesBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonSeparator12
        '
        Me.AdxRibbonSeparator12.Id = "adxRibbonSeparator_94584bece699423996d155660f42b06a"
        Me.AdxRibbonSeparator12.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'usersMGTBT
        '
        Me.usersMGTBT.Caption = "Users"
        Me.usersMGTBT.Id = "adxRibbonButton_133031e6e71047ca81f5498a1be8e7b2"
        Me.usersMGTBT.Image = 84
        Me.usersMGTBT.ImageList = Me.ImageList1
        Me.usersMGTBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.usersMGTBT.InsertBeforeIdMso = "ReviewAllowUsersToEditRanges"
        Me.usersMGTBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.usersMGTBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonGroup7
        '
        Me.AdxRibbonGroup7.Caption = "Export"
        Me.AdxRibbonGroup7.Controls.Add(Me.PPTExportBT)
        Me.AdxRibbonGroup7.Controls.Add(Me.AdxRibbonSeparator13)
        Me.AdxRibbonGroup7.Controls.Add(Me.FormattingBT)
        Me.AdxRibbonGroup7.Id = "adxRibbonGroup_01ea01d30dcf40a9a03f63e1d0b965b8"
        Me.AdxRibbonGroup7.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonGroup7.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'PPTExportBT
        '
        Me.PPTExportBT.Caption = "Export"
        Me.PPTExportBT.Id = "adxRibbonButton_4a7a400a8fe4439fb34f7edb9ebd6bb7"
        Me.PPTExportBT.Image = 56
        Me.PPTExportBT.ImageList = Me.ImageList1
        Me.PPTExportBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PPTExportBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.PPTExportBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonSeparator13
        '
        Me.AdxRibbonSeparator13.Id = "adxRibbonSeparator_c4e5e8dfd76f4f779a6842322a1a9f53"
        Me.AdxRibbonSeparator13.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'FormattingBT
        '
        Me.FormattingBT.Caption = "Formatting"
        Me.FormattingBT.Controls.Add(Me.AdxRibbonMenu6)
        Me.FormattingBT.Id = "adxRibbonSplitButton_310b0b24062947479934b5576ee4ced1"
        Me.FormattingBT.Image = 85
        Me.FormattingBT.ImageList = Me.ImageList1
        Me.FormattingBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.FormattingBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.FormattingBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonMenu6
        '
        Me.AdxRibbonMenu6.Caption = "AdxRibbonMenu6"
        Me.AdxRibbonMenu6.Controls.Add(Me.ReportFmtBT)
        Me.AdxRibbonMenu6.Controls.Add(Me.InputFmtBT)
        Me.AdxRibbonMenu6.Id = "adxRibbonMenu_be2c85c16a6744799af1411f4234d1b4"
        Me.AdxRibbonMenu6.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonMenu6.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'ReportFmtBT
        '
        Me.ReportFmtBT.Caption = "Format Report"
        Me.ReportFmtBT.Id = "adxRibbonButton_d561533b72d348bcb2b83d3f51d6250d"
        Me.ReportFmtBT.Image = 10
        Me.ReportFmtBT.ImageList = Me.ImageList1
        Me.ReportFmtBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ReportFmtBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.ReportFmtBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'InputFmtBT
        '
        Me.InputFmtBT.Caption = "Come back to input format"
        Me.InputFmtBT.Id = "adxRibbonButton_df4a98711f4c41daad9fb7d1ae242eaa"
        Me.InputFmtBT.ImageList = Me.ImageList1
        Me.InputFmtBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.InputFmtBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonGroup5
        '
        Me.AdxRibbonGroup5.Caption = "Settings"
        Me.AdxRibbonGroup5.Controls.Add(Me.SettingsBT)
        Me.AdxRibbonGroup5.Id = "adxRibbonGroup_e6eee948b5d24daa83135f3148b80d3d"
        Me.AdxRibbonGroup5.ImageMso = "AddInManager"
        Me.AdxRibbonGroup5.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonGroup5.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'SettingsBT
        '
        Me.SettingsBT.Caption = "PPS Settings"
        Me.SettingsBT.Id = "adxRibbonButton_aa28ec782b5541edb1482374e14ceaa6"
        Me.SettingsBT.Image = 50
        Me.SettingsBT.ImageList = Me.ImageList1
        Me.SettingsBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.SettingsBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.SettingsBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'SubmissionRibbonIL
        '
        Me.SubmissionRibbonIL.ImageStream = CType(resources.GetObject("SubmissionRibbonIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.SubmissionRibbonIL.TransparentColor = System.Drawing.Color.Transparent
        Me.SubmissionRibbonIL.Images.SetKeyName(0, "favicon(187).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(1, "favicon(129).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(2, "favicon(131).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(3, "favicon(133).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(4, "favicon(134).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(5, "favicon(138).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(6, "favicon(137).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(7, "favicon(136).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(8, "favicon(135).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(9, "favicon(170).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(10, "favicon(169).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(11, "favicon(168).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(12, "favicon(161).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(13, "favicon(160).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(14, "favicon(186).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(15, "favicon(152).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(16, "favicon(151).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(17, "favicon(149).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(18, "favicon(146).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(19, "favicon(171).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(20, "favicon(175).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(21, "favicon(188).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(22, "favicon(178).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(23, "favicon(196).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(24, "favicon(180).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(25, "favicon(185).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(26, "favicon(194).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(27, "favicon(195).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(28, "favicon(197).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(29, "refresh 3.ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(30, "favicon(199).ico")
        '
        'LightsImageList
        '
        Me.LightsImageList.ImageStream = CType(resources.GetObject("LightsImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.LightsImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.LightsImageList.Images.SetKeyName(0, "favicon(172).ico")
        Me.LightsImageList.Images.SetKeyName(1, "favicon(173).ico")
        Me.LightsImageList.Images.SetKeyName(2, "favicon(174).ico")
        Me.LightsImageList.Images.SetKeyName(3, "transparent light.ico")
        Me.LightsImageList.Images.SetKeyName(4, "green 5 ok.ico")
        Me.LightsImageList.Images.SetKeyName(5, "red 2 ok(74).ico")
        '
        'SubmissionModeRibbon
        '
        Me.SubmissionModeRibbon.Caption = "Data Submission"
        Me.SubmissionModeRibbon.Controls.Add(Me.EditSelectionGroup)
        Me.SubmissionModeRibbon.Controls.Add(Me.SubmissionnGroup)
        Me.SubmissionModeRibbon.Controls.Add(Me.StateSelectionGroup)
        Me.SubmissionModeRibbon.Controls.Add(Me.AdxRibbonGroup9)
        Me.SubmissionModeRibbon.Id = "adxRibbonTab_7e9a50d1d91c429697388afda23d5b15"
        Me.SubmissionModeRibbon.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'EditSelectionGroup
        '
        Me.EditSelectionGroup.Caption = "Edit Selection"
        Me.EditSelectionGroup.Controls.Add(Me.HighlightBT)
        Me.EditSelectionGroup.Controls.Add(Me.RefreshInputsBT)
        Me.EditSelectionGroup.Controls.Add(Me.AdxRibbonSeparator4)
        Me.EditSelectionGroup.Controls.Add(Me.EditRangesMenuBT)
        Me.EditSelectionGroup.Controls.Add(Me.AdxRibbonSeparator7)
        Me.EditSelectionGroup.Controls.Add(Me.AccountStatusBT)
        Me.EditSelectionGroup.Controls.Add(Me.WSCB)
        Me.EditSelectionGroup.Controls.Add(Me.AdxRibbonSeparator5)
        Me.EditSelectionGroup.Controls.Add(Me.OpenTemplateBT)
        Me.EditSelectionGroup.Controls.Add(Me.SaveTemplateBT)
        Me.EditSelectionGroup.Id = "adxRibbonGroup_845aac06fe0b43ffa1a5ab1c8cf56d7a"
        Me.EditSelectionGroup.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.EditSelectionGroup.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'HighlightBT
        '
        Me.HighlightBT.Caption = "Highlight Inputs"
        Me.HighlightBT.Id = "adxRibbonButton_703868e94c394267b617fb17c2879b3c"
        Me.HighlightBT.Image = 25
        Me.HighlightBT.ImageList = Me.SubmissionRibbonIL
        Me.HighlightBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.HighlightBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.HighlightBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        Me.HighlightBT.ToggleButton = True
        '
        'RefreshInputsBT
        '
        Me.RefreshInputsBT.Caption = "Refresh Inputs"
        Me.RefreshInputsBT.Id = "adxRibbonButton_88ae883ce9de414bacaf9f25f558dffc"
        Me.RefreshInputsBT.Image = 29
        Me.RefreshInputsBT.ImageList = Me.SubmissionRibbonIL
        Me.RefreshInputsBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RefreshInputsBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RefreshInputsBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonSeparator4
        '
        Me.AdxRibbonSeparator4.Id = "adxRibbonSeparator_5f185d7b38614bf9b956f926424bfbf5"
        Me.AdxRibbonSeparator4.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'EditRangesMenuBT
        '
        Me.EditRangesMenuBT.Caption = "Edit Input Ranges"
        Me.EditRangesMenuBT.Controls.Add(Me.EditRangesMenu)
        Me.EditRangesMenuBT.Id = "adxRibbonSplitButton_ce463d91e7174578abd269d924569f1d"
        Me.EditRangesMenuBT.Image = 30
        Me.EditRangesMenuBT.ImageList = Me.SubmissionRibbonIL
        Me.EditRangesMenuBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.EditRangesMenuBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.EditRangesMenuBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'EditRangesMenu
        '
        Me.EditRangesMenu.Caption = "Edit"
        Me.EditRangesMenu.Controls.Add(Me.SelectAccRangeBT)
        Me.EditRangesMenu.Controls.Add(Me.SelectEntitiesRangeBT)
        Me.EditRangesMenu.Controls.Add(Me.SelectPeriodsRangeBT)
        Me.EditRangesMenu.Id = "adxRibbonMenu_2ac0a0f424dc41c7a02ef8758071e5be"
        Me.EditRangesMenu.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.EditRangesMenu.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'SelectAccRangeBT
        '
        Me.SelectAccRangeBT.Caption = "Select Accounts"
        Me.SelectAccRangeBT.Id = "adxRibbonButton_c96e52e08e2d4080876d4d320e7a2705"
        Me.SelectAccRangeBT.Image = 14
        Me.SelectAccRangeBT.ImageList = Me.SubmissionRibbonIL
        Me.SelectAccRangeBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.SelectAccRangeBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'SelectEntitiesRangeBT
        '
        Me.SelectEntitiesRangeBT.Caption = "Select Entities Range"
        Me.SelectEntitiesRangeBT.Id = "adxRibbonButton_d24e5ee960704f1e9ca2612d5eebfa53"
        Me.SelectEntitiesRangeBT.Image = 27
        Me.SelectEntitiesRangeBT.ImageList = Me.SubmissionRibbonIL
        Me.SelectEntitiesRangeBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.SelectEntitiesRangeBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'SelectPeriodsRangeBT
        '
        Me.SelectPeriodsRangeBT.Caption = "Select Periods Range"
        Me.SelectPeriodsRangeBT.Id = "adxRibbonButton_ab9e3107c41a4638be170f1f98a20b70"
        Me.SelectPeriodsRangeBT.Image = 8
        Me.SelectPeriodsRangeBT.ImageList = Me.SubmissionRibbonIL
        Me.SelectPeriodsRangeBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.SelectPeriodsRangeBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonSeparator7
        '
        Me.AdxRibbonSeparator7.Id = "adxRibbonSeparator_de1d27298bdd46ca997d2614961b5e5b"
        Me.AdxRibbonSeparator7.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AccountStatusBT
        '
        Me.AccountStatusBT.Caption = "Accounts Status"
        Me.AccountStatusBT.Id = "adxRibbonButton_77f99b4575a04d8ab1fa6b2aa162a839"
        Me.AccountStatusBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AccountStatusBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'WSCB
        '
        Me.WSCB.Caption = "Worksheet"
        Me.WSCB.Id = "adxRibbonDropDown_a04f347dc2b64e7b8e52b334f1b8f173"
        Me.WSCB.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.WSCB.Items.Add(Me.AdxRibbonItem2)
        Me.WSCB.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.WSCB.SelectedItemId = "adxRibbonItem_62461e2afe1245b4875d9cd13a3de6d9"
        Me.WSCB.SizeString = "wwwwwwwwwwwww"
        '
        'AdxRibbonItem2
        '
        Me.AdxRibbonItem2.Caption = "AdxRibbonItem2"
        Me.AdxRibbonItem2.Id = "adxRibbonItem_62461e2afe1245b4875d9cd13a3de6d9"
        Me.AdxRibbonItem2.ImageTransparentColor = System.Drawing.Color.Transparent
        '
        'AdxRibbonSeparator5
        '
        Me.AdxRibbonSeparator5.Id = "adxRibbonSeparator_29017f85fa54485a94f66e1f8935631d"
        Me.AdxRibbonSeparator5.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'OpenTemplateBT
        '
        Me.OpenTemplateBT.Caption = "Open Template"
        Me.OpenTemplateBT.Id = "adxRibbonButton_d89e114fe49a4c7391a0926231d9f0e7"
        Me.OpenTemplateBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.OpenTemplateBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'SaveTemplateBT
        '
        Me.SaveTemplateBT.Caption = "Save Template"
        Me.SaveTemplateBT.Id = "adxRibbonButton_1d75b4a7acfb448183e66f0adf619fe0"
        Me.SaveTemplateBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.SaveTemplateBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'SubmissionnGroup
        '
        Me.SubmissionnGroup.Caption = "Submission"
        Me.SubmissionnGroup.Controls.Add(Me.SubmitBT2)
        Me.SubmissionnGroup.Controls.Add(Me.SubmissionStatus)
        Me.SubmissionnGroup.Controls.Add(Me.CancelBT2)
        Me.SubmissionnGroup.Controls.Add(Me.AdxRibbonSeparator6)
        Me.SubmissionnGroup.Controls.Add(Me.AutoComitBT)
        Me.SubmissionnGroup.Id = "adxRibbonGroup_d13fa5b1f4584ad99081875d975057c9"
        Me.SubmissionnGroup.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.SubmissionnGroup.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'SubmitBT2
        '
        Me.SubmitBT2.Caption = "Submit"
        Me.SubmitBT2.Id = "adxRibbonButton_f781efe7b70b4e2387fe5a59f0772128"
        Me.SubmitBT2.Image = 0
        Me.SubmitBT2.ImageList = Me.SubmissionRibbonIL
        Me.SubmitBT2.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.SubmitBT2.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.SubmitBT2.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'SubmissionStatus
        '
        Me.SubmissionStatus.Caption = "Submission Status"
        Me.SubmissionStatus.Id = "adxRibbonButton_83d245af23e34559ab274b13e9a52e0c"
        Me.SubmissionStatus.Image = 0
        Me.SubmissionStatus.ImageList = Me.LightsImageList
        Me.SubmissionStatus.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.SubmissionStatus.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'CancelBT2
        '
        Me.CancelBT2.Caption = "Cancel"
        Me.CancelBT2.Id = "adxRibbonButton_b3163cbf2cf444a5af4934c23e854aa6"
        Me.CancelBT2.Image = 1
        Me.CancelBT2.ImageList = Me.ImageList1
        Me.CancelBT2.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.CancelBT2.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonSeparator6
        '
        Me.AdxRibbonSeparator6.Id = "adxRibbonSeparator_b9b1011c15a54de3b20d1b83916ab7ce"
        Me.AdxRibbonSeparator6.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AutoComitBT
        '
        Me.AutoComitBT.Caption = "Auto Submit"
        Me.AutoComitBT.Id = "adxRibbonButton_f68c1069d4d74f95bf5c5e89cebda486"
        Me.AutoComitBT.Image = 21
        Me.AutoComitBT.ImageList = Me.SubmissionRibbonIL
        Me.AutoComitBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AutoComitBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.AutoComitBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        Me.AutoComitBT.ToggleButton = True
        '
        'StateSelectionGroup
        '
        Me.StateSelectionGroup.Caption = "Entity Information"
        Me.StateSelectionGroup.Controls.Add(Me.entityEditBT)
        Me.StateSelectionGroup.Controls.Add(Me.CurrencyLabel)
        Me.StateSelectionGroup.Controls.Add(Me.VersionBT2)
        Me.StateSelectionGroup.Controls.Add(Me.CurrentEntityTB)
        Me.StateSelectionGroup.Controls.Add(Me.EntCurrTB)
        Me.StateSelectionGroup.Controls.Add(Me.VersionTBSubRibbon)
        Me.StateSelectionGroup.Id = "adxRibbonGroup_31e139cc6d9d421dbf833740d494b4a0"
        Me.StateSelectionGroup.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.StateSelectionGroup.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'entityEditBT
        '
        Me.entityEditBT.Caption = "Current Entity"
        Me.entityEditBT.Id = "adxRibbonButton_4134f852978240a19f9e57bcd1ce4b13"
        Me.entityEditBT.ImageList = Me.SubmissionRibbonIL
        Me.entityEditBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.entityEditBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'CurrencyLabel
        '
        Me.CurrencyLabel.Caption = "Entity Currency"
        Me.CurrencyLabel.Id = "adxRibbonLabel_3a0c25b5abe942cc818d9ac7f7fed517"
        Me.CurrencyLabel.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'VersionBT2
        '
        Me.VersionBT2.Caption = "Version Selection"
        Me.VersionBT2.Id = "adxRibbonButton_2a8644b2388844f9aa05d2c0c48d6969"
        Me.VersionBT2.ImageList = Me.SubmissionRibbonIL
        Me.VersionBT2.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.VersionBT2.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'CurrentEntityTB
        '
        Me.CurrentEntityTB.Caption = " "
        Me.CurrentEntityTB.Enabled = False
        Me.CurrentEntityTB.Id = "adxRibbonEditBox_994a7df7764c43dbb7a7f4392d489c7a"
        Me.CurrentEntityTB.ImageList = Me.ImageList1
        Me.CurrentEntityTB.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.CurrentEntityTB.MaxLength = 30
        Me.CurrentEntityTB.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.CurrentEntityTB.SizeString = "wwwwwwwwwww"
        '
        'EntCurrTB
        '
        Me.EntCurrTB.Caption = " "
        Me.EntCurrTB.Enabled = False
        Me.EntCurrTB.Id = "adxRibbonEditBox_442b9a2b340041749e101a147e75eb43"
        Me.EntCurrTB.ImageList = Me.ImageList1
        Me.EntCurrTB.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.EntCurrTB.MaxLength = 30
        Me.EntCurrTB.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.EntCurrTB.SizeString = "wwwwwwwwwww"
        '
        'VersionTBSubRibbon
        '
        Me.VersionTBSubRibbon.Caption = " "
        Me.VersionTBSubRibbon.Enabled = False
        Me.VersionTBSubRibbon.Id = "adxRibbonEditBox_9227e5e58139412a8eb816391abe45ae"
        Me.VersionTBSubRibbon.ImageList = Me.ImageList1
        Me.VersionTBSubRibbon.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.VersionTBSubRibbon.MaxLength = 30
        Me.VersionTBSubRibbon.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.VersionTBSubRibbon.SizeString = "wwwwwwwwwww"
        '
        'AdxRibbonGroup9
        '
        Me.AdxRibbonGroup9.Caption = "Options"
        Me.AdxRibbonGroup9.Controls.Add(Me.ShowReportBT)
        Me.AdxRibbonGroup9.Controls.Add(Me.ReportOptionsBT)
        Me.AdxRibbonGroup9.Controls.Add(Me.AdxRibbonSeparator3)
        Me.AdxRibbonGroup9.Controls.Add(Me.CloseBT)
        Me.AdxRibbonGroup9.Id = "adxRibbonGroup_0a2b25ca3df2428895a2a7148bf5329d"
        Me.AdxRibbonGroup9.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonGroup9.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'ShowReportBT
        '
        Me.ShowReportBT.Caption = "Display Report"
        Me.ShowReportBT.Id = "adxRibbonButton_85590099a4f24030b2944f997d3926c9"
        Me.ShowReportBT.Image = 23
        Me.ShowReportBT.ImageList = Me.SubmissionRibbonIL
        Me.ShowReportBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ShowReportBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.ShowReportBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'ReportOptionsBT
        '
        Me.ReportOptionsBT.Caption = "Report Options"
        Me.ReportOptionsBT.Id = "adxRibbonButton_f1868923f3aa45699f090d49a0d174af"
        Me.ReportOptionsBT.Image = 24
        Me.ReportOptionsBT.ImageList = Me.SubmissionRibbonIL
        Me.ReportOptionsBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ReportOptionsBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.ReportOptionsBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonSeparator3
        '
        Me.AdxRibbonSeparator3.Id = "adxRibbonSeparator_d609b23d54c041129445fc192ab9f0ee"
        Me.AdxRibbonSeparator3.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'CloseBT
        '
        Me.CloseBT.Caption = "Close"
        Me.CloseBT.Id = "adxRibbonButton_f7a01388d2f243fc85709ca59b1af989"
        Me.CloseBT.ImageMso = "PrintPreviewClose"
        Me.CloseBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.CloseBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.CloseBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxExcelTaskPanesManager1
        '
        Me.AdxExcelTaskPanesManager1.Items.Add(Me.InputSelectionTaskPaneItem)
        Me.AdxExcelTaskPanesManager1.Items.Add(Me.VersionSelectionTaskPaneItem)
        Me.AdxExcelTaskPanesManager1.Items.Add(Me.EntitySelectionTaskPaneItem)
        Me.AdxExcelTaskPanesManager1.SetOwner(Me)
        '
        'InputSelectionTaskPaneItem
        '
        Me.InputSelectionTaskPaneItem.AlwaysShowHeader = True
        Me.InputSelectionTaskPaneItem.CloseButton = True
        Me.InputSelectionTaskPaneItem.Position = AddinExpress.XL.ADXExcelTaskPanePosition.Right
        Me.InputSelectionTaskPaneItem.TaskPaneClassName = "CInputSelectionPane"
        Me.InputSelectionTaskPaneItem.UseOfficeThemeForBackground = True
        '
        'VersionSelectionTaskPaneItem
        '
        Me.VersionSelectionTaskPaneItem.AlwaysShowHeader = True
        Me.VersionSelectionTaskPaneItem.CloseButton = True
        Me.VersionSelectionTaskPaneItem.Position = AddinExpress.XL.ADXExcelTaskPanePosition.Right
        Me.VersionSelectionTaskPaneItem.TaskPaneClassName = "CVersionSelectionPane"
        Me.VersionSelectionTaskPaneItem.UseOfficeThemeForBackground = True
        '
        'EntitySelectionTaskPaneItem
        '
        Me.EntitySelectionTaskPaneItem.AlwaysShowHeader = True
        Me.EntitySelectionTaskPaneItem.Position = AddinExpress.XL.ADXExcelTaskPanePosition.Right
        Me.EntitySelectionTaskPaneItem.TaskPaneClassName = "EntitySelectionTP"
        Me.EntitySelectionTaskPaneItem.UseOfficeThemeForBackground = True
        '
        'AdxRibbonLabel1
        '
        Me.AdxRibbonLabel1.Caption = "Associated with"
        Me.AdxRibbonLabel1.Id = "adxRibbonLabel_f8272bc6694448f6955f882ef772da9e"
        Me.AdxRibbonLabel1.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AddinModule
        '
        Me.AddinName = "PPS"
        Me.SupportedApps = AddinExpress.MSO.ADXOfficeHostApp.ohaExcel

    End Sub

#End Region


#Region " Add-in Express automatic code "

    'Required by Add-in Express - do not modify
    'the methods within this region

    Public Overrides Function GetContainer() As System.ComponentModel.IContainer
        If components Is Nothing Then
            components = New System.ComponentModel.Container
        End If
        GetContainer = components
    End Function

    <ComRegisterFunctionAttribute()> _
    Public Shared Sub AddinRegister(ByVal t As Type)
        AddinExpress.MSO.ADXAddinModule.ADXRegister(t)
    End Sub

    <ComUnregisterFunctionAttribute()> _
    Public Shared Sub AddinUnregister(ByVal t As Type)
        AddinExpress.MSO.ADXAddinModule.ADXUnregister(t)
    End Sub

    Public Overrides Sub UninstallControls()
        MyBase.UninstallControls()
    End Sub

#End Region


#Region "My Instance Variables"

    Friend Ribbon As IRibbonUI
    Friend GRSControlersDictionary As New Dictionary(Of Excel.Worksheet, CGeneralReportSubmissionControler)
    Private ctrlsTextWSDictionary As New Dictionary(Of String, Excel.Worksheet)
    Public setUpFlag As Boolean
    Private CurrentGRSControler As CGeneralReportSubmissionControler
    Private Const EXCEL_MIN_VERSION As Double = 9


#End Region


#Region "Task Panes"

    Private ReadOnly Property InputReportTaskPane() As CInputSelectionPane

        Get
            Dim taskPaneInstance As AddinExpress.XL.ADXExcelTaskPane = Nothing
            taskPaneInstance = InputSelectionTaskPaneItem.TaskPaneInstance

            If taskPaneInstance Is Nothing Then
                taskPaneInstance = InputSelectionTaskPaneItem.CreateTaskPaneInstance()
            End If

            Return TryCast(taskPaneInstance, CInputSelectionPane)
        End Get
    End Property

    Private ReadOnly Property VersionSelectionTaskPane() As CVersionSelectionPane
        Get
            Dim taskPaneInstance As AddinExpress.XL.ADXExcelTaskPane = Nothing
            taskPaneInstance = VersionSelectionTaskPaneItem.TaskPaneInstance

            If taskPaneInstance Is Nothing Then
                taskPaneInstance = VersionSelectionTaskPaneItem.CreateTaskPaneInstance()
            End If
            Return TryCast(taskPaneInstance, CVersionSelectionPane)
        End Get
    End Property

    Friend ReadOnly Property EntitySelectionTaskPane As EntitySelectionTP
        Get
            Dim taskPaneInstance As AddinExpress.XL.ADXExcelTaskPane = Nothing
            taskPaneInstance = EntitySelectionTaskPaneItem.TaskPaneInstance

            If taskPaneInstance Is Nothing Then
                taskPaneInstance = EntitySelectionTaskPaneItem.CreateTaskPaneInstance()
            End If
            Return TryCast(taskPaneInstance, EntitySelectionTP)
        End Get
    End Property

#End Region


#Region "Initialize"


    Public Sub New()
        MyBase.New()

        Application.EnableVisualStyles()
        'This call is required by the Component Designer
        InitializeComponent()

        'Please add any initialization code to the AddinInitialize event handler

    End Sub

    Public Shared Shadows ReadOnly Property CurrentInstance() As AddinModule
        Get
            Return CType(AddinExpress.MSO.ADXAddinModule.CurrentInstance, AddinModule)
        End Get
    End Property

    Public ReadOnly Property ExcelApp() As Excel._Application
        Get
            Return CType(HostApplication, Excel._Application)
        End Get
    End Property

    Private Sub AddinModule_AddinInitialize(sender As Object, e As EventArgs) Handles MyBase.AddinInitialize

        APPS = Me.HostApplication

        ' Main Ribbon Initialize
        SubmissionStatusButton = SubmissionStatus
        Connection_Toggle_Button = Me.ConnectionBT
        Version_Label = Me.Addin_Version_label
        Rates_Version_Label = Me.Addin_rates_version_label
        ConnectionBT.Image = 0

        ' Submission Ribbon Initialize
        WSCB.Items.RemoveAt(0)
        SubmissionModeRibbon.Visible = False
        Version_label_Sub_Ribbon = VersionTBSubRibbon


    End Sub

    ' Launch connection (Triggered by ExcelAddinModule1)
    Public Sub SetUpConnection()

        Dim CONNECTIONUI As New ConnectionUI(Me)
        CONNECTIONUI.Show()
        setUpFlag = True

    End Sub


#Region "Properties Getters"

    ' Returns the connection instance variable
    Public Function GetAddinConnection() As ADODB.Connection
        Return ConnectioN
    End Function

    ' Returns the user credential instance variable
    Public Function GetUserCredential() As Int32
        Return User_Credential
    End Function

    ' Returns the entities View variable
    Public Function GetEntitiesView() As String
        Return Entities_View
    End Function

    ' Returns the entities View variable
    Public Function GetVersionLabel() As ADXRibbonLabel
        Return Version_Label
    End Function


#End Region


#End Region


#Region "Call Backs"


#Region "Main Ribbon"

#Region "Connection"

    Private Sub ConnectionBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles ConnectionBT.OnClick
        SetUpConnection()
        ' set up disconnection
    End Sub

    Private Sub VersionBT_OnClick_1(sender As Object, control As IRibbonControl, pressed As Boolean) Handles VersionBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            LaunchVersionSelection()
        End If

    End Sub


#End Region

#Region "PPS Data Acquisition"

    Private Sub UploadBT_onclick(sender As System.Object,
                                              control As AddinExpress.MSO.IRibbonControl,
                                              pressed As System.Boolean) Handles UploadBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            LaunchGRSControler()
        End If

    End Sub

    Private Sub WSUploadBT_onclick(sender As System.Object,
                                  control As AddinExpress.MSO.IRibbonControl,
                                  pressed As System.Boolean) Handles WSUplaodBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            '    Dim DSDUI As New SubmissionControlUI
            '    DSDUI.Show()
        End If

    End Sub

    Private Sub WBUploadBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles WBUplaodBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim MSA As New MultipleSheetsAcquisition
            MSA.Show()
        End If

    End Sub

    Private Sub CurrentLinkedWSBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles CurrentLinkedWSBT.OnClick

    End Sub

    Private Sub InputReportLaunchBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles InputReportLaunchBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            If CDbl(APPS.Version.Replace(".", ",")) > EXCEL_MIN_VERSION Then
                InputSelectionPaneVisible = True
                Me.InputReportTaskPane.InitializeSelectionChoices(Me)
                Me.InputReportTaskPane.Show()
            Else
                ' display UI selection
            End If
        End If

    End Sub


#Region "GRS Controler Display Functions"

    ' GRS Controler display or construction
    Private Sub LaunchGRSControler()

        If GRSControlersDictionary.ContainsKey(APPS.ActiveSheet) Then
            CurrentGRSControler = GRSControlersDictionary(APPS.ActiveSheet)
            CurrentGRSControler.ActivateControler()
            SubmissionModeRibbon.Visible = True
        Else
            'Dim confirm As Integer = MessageBox.Show("In order to submit data the software must identify the information provided in the worksheet" + Chr(13) + Chr(13) + _
            '                                         "Click on confirm to launch the automatic recognition." + Chr(13) + Chr(13) + _
            '                                         "You will be able to edit data and identified items afterwards", "Automated Data Recognition", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            'If confirm = DialogResult.Yes Then
            ' Add circular progress
            AssociateGRSControler()
            'End If
        End If

    End Sub

    ' Create GRS Conctroler and display
    Private Sub AssociateGRSControler()

        Dim ctrl As ADXRibbonItem = AddButtonToDropDown(WSCB, APPS.ActiveSheet.name)
        Dim CGRSControlerInstance As New CGeneralReportSubmissionControler(ctrl, _
                                                                           Me)
        GRSControlersDictionary.Add(APPS.ActiveSheet, CGRSControlerInstance)
        ctrlsTextWSDictionary.Add(ctrl.Caption, APPS.ActiveSheet)
        CurrentGRSControler = CGRSControlerInstance
        CurrentGRSControler.LaunchDataSetSnapshotAndAssociateModel()
        SubmissionModeRibbon.Activate()
        SubmissionModeRibbon.Visible = True
        WSCB.SelectedItemId = ctrl.Id

    End Sub

    ' Disable Submission Buttons (Call back from GRSController)
    Friend Sub modifySubmissionControlsStatus(ByRef buttonsStatus As Boolean)

        RefreshInputsBT.Enabled = buttonsStatus
        SubmitBT2.Enabled = buttonsStatus
        SubmissionStatus.Enabled = buttonsStatus
        CancelBT2.Enabled = buttonsStatus
        AutoComitBT.Enabled = buttonsStatus
        ShowReportBT.Enabled = buttonsStatus

    End Sub

#End Region


#End Region

#Region "Download Data"

    Private Sub ControllingUI2BT_onclick(sender As System.Object,
                                        control As AddinExpress.MSO.IRibbonControl,
                                        pressed As System.Boolean) Handles ControlingUI2BT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim CONTROLLING As New ControllingUI_2
            CONTROLLING.Show()
        End If

    End Sub

    Private Sub RefreshBT_onclick(sender As System.Object,
                                         control As AddinExpress.MSO.IRibbonControl,
                                         pressed As System.Boolean) Handles RefreshBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            If Not GRSControlersDictionary.ContainsKey(APPS.ActiveSheet) Then
                Dim cREFRESH As New CRefreshGetDataBatch
                cREFRESH.RefreshWorksheet()
            Else
                MsgBox("Cannot Refresh while the worksheet is being edited. " + Chr(13) + _
                       "The submission must be closed for this worksheet in order to refresh it.")
            End If
        End If

    End Sub

    Private Sub PPSBIFuncBT_onclick(sender As System.Object,
                                    control As AddinExpress.MSO.IRibbonControl,
                                    pressed As System.Boolean) Handles FunctionDesigner.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim PPSBI As New PPSBI_UI
            PPSBI.Show()
        End If

    End Sub

    Private Sub SubmissionsControlBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles SubmissionControlBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim SubmissionControl As New SubmissionsControlsController
        End If

    End Sub

    Private Sub LogBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles LogBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim logController As New LogController
        End If


    End Sub

#End Region

#Region "Configuration"

#Region "Platform Management"

    Private Sub PlatformMGTBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles PlatformMGTBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim MGTUI As New PlatformMGTUI
            MGTUI.Show()
        End If

    End Sub

    Private Sub AccountsMGTBT_onclick(sender As System.Object,
                                      control As AddinExpress.MSO.IRibbonControl,
                                      pressed As System.Boolean) Handles AccountsMGTBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim accounts_controller As New AccountsController
        End If

    End Sub

    Private Sub EntitiesMGTBT_onclick(sender As System.Object,
                                       control As AddinExpress.MSO.IRibbonControl,
                                       pressed As System.Boolean) Handles EntitiesMGTBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim ENTMGT As New EntitiesController
        End If

    End Sub

    Private Sub VersionsMGTBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles VersionsMGTBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim VMMGT As New DataVersionsController
        End If

    End Sub

    Private Sub CategoriesMGTBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles CategoriesMGTBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim CATMGT As New CategoriesController
        End If

    End Sub

    Private Sub ControlsMGTBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles ControlsMGTBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim ControlsMGT As New ControlsMGTController
        End If

    End Sub

#End Region

    Private Sub CurrenciesBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles CurrenciesBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim CURRMGTUI As New CurrenciesManagementUI
            CURRMGTUI.Show()
        End If
    End Sub

#End Region

#Region "Export"

    Private Sub ReportFMTBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) _
            Handles ReportFmtBT.OnClick, FormattingBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim EXCELFORMATER As New CExcelFormatting
            EXCELFORMATER.FormatExcelRange(APPS.ActiveSheet, REPORT_FORMAT_CODE)
            EXCELFORMATER = Nothing
        End If

    End Sub

    Private Sub InputFMTBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles InputFmtBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim EXCELFORMATER As New CExcelFormatting
            EXCELFORMATER.FormatExcelRange(APPS.ActiveSheet, INPUT_FORMAT_CODE)
            EXCELFORMATER = Nothing
        End If

    End Sub

#End Region

#Region "Modelling"

    Private Sub AdvancesModelingBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles AdvancesModelingBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim FModellingController As New FModellingSimulationsControler
        End If

    End Sub

#End Region

#Region "Settings"

    Private Sub SettingsBT_onclick(sender As System.Object,
                                    control As AddinExpress.MSO.IRibbonControl,
                                    pressed As System.Boolean) Handles SettingsBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim SETTINGSUI As New SettingMainUI
            SETTINGSUI.Show()
        End If

    End Sub

    Private Sub UsersMGTBT_onclick(sender As System.Object,
                                       control As AddinExpress.MSO.IRibbonControl,
                                       pressed As System.Boolean) Handles usersMGTBT.OnClick
        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim USERSMGT As New UsersManagementUI
            USERSMGT.Show()
        End If

    End Sub

#End Region

#End Region


#Region "Submission Ribbon"

#Region "Edit Selection"

    Private Sub HighlightBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles HighlightBT.OnClick

        CurrentGRSControler.HighlightItemsAndDataRegions()

    End Sub

    Private Sub RefreshInputsBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RefreshInputsBT.OnClick

        ' Relaunch dataset.snapshot, orientation and get data if ok !!

        ' if successful and GRS state was false -> enable buttons - state = true

    End Sub

    Private Sub EditRangesMenuBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles EditRangesMenuBT.OnClick

        CurrentGRSControler.RangeEdition()

    End Sub

    Private Sub SelectAccRangeBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles SelectAccRangeBT.OnClick

    End Sub

    Private Sub SelectEntitiesRangeBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles SelectEntitiesRangeBT.OnClick

    End Sub

    Private Sub SelectPeriodsRangeBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles SelectPeriodsRangeBT.OnClick

    End Sub

    Private Sub OpenTemplateBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles OpenTemplateBT.OnClick

    End Sub

    Private Sub SaveTemplateBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles SaveTemplateBT.OnClick

    End Sub

#End Region

#Region "Submission"

    Private Sub SubmitBT2_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles SubmitBT2.OnClick
        CurrentGRSControler.Submit()
    End Sub

    Private Sub SubmissionStatus_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles SubmissionStatus.OnClick

        CurrentGRSControler.DisplayUploadStatusAndErrorsUI()

    End Sub

    Private Sub CancelBT2_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles CancelBT2.OnClick
        ' allow to come back a few steps before (cf log ?)
    End Sub

    Private Sub AutoCommitBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles AutoComitBT.OnClick

        If CurrentGRSControler.autoCommitFlag = False Then
            CurrentGRSControler.autoCommitFlag = True
        Else
            CurrentGRSControler.autoCommitFlag = False
        End If

    End Sub

#End Region

#Region "Submission items"

    Private Sub entityEditBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles entityEditBT.OnClick
        LaunchEntitySelectionTP(Me, True)
    End Sub

    Private Sub VersionBT2_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles VersionBT2.OnClick

        ' Open side pane to select version ?
        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            LaunchVersionSelection()
        End If

    End Sub

#End Region

#Region "Display"

    Private Sub ShowReportBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles ShowReportBT.OnClick
        CurrentGRSControler.ShowACQUI()
    End Sub

    Private Sub ReportOptionsBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles ReportOptionsBT.OnClick
        ' to implement !!
    End Sub

    Private Sub CloseBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles CloseBT.OnClick
        ClearSubmissionMode()
    End Sub

#End Region

#End Region


#End Region


#Region "Events"

#Region "Main Ribbon"

    Private Sub AddinModule_OnRibbonLoaded(sender As Object, ribbon As IRibbonUI) Handles Me.OnRibbonLoaded
        Me.Ribbon = ribbon
    End Sub

#End Region

#Region "Submission module"

    ' Associated Worksheet combobox on change event
    Private Sub WSCB_OnAction(sender As Object, Control As IRibbonControl, selectedId As String, selectedIndex As Integer) Handles WSCB.OnAction

        Try
            If Not CurrentGRSControler.associatedWorksheet.Name = selectedId Then
                CurrentGRSControler = GRSControlersDictionary(ctrlsTextWSDictionary(selectedId))
                CurrentGRSControler.UpdateRibbonTextBoxes()
                ctrlsTextWSDictionary(selectedId).Activate()
            End If
        Catch ex As Exception
            GRSControlersDictionary.Remove(ctrlsTextWSDictionary(selectedId))
            ctrlsTextWSDictionary.Remove(selectedId)
            MsgBox("The worksheet has been deleted.")
        End Try

    End Sub


#End Region

#Region "Addin Events"

    Private Sub AddinModule_Finalize(sender As Object, e As EventArgs) Handles MyBase.AddinFinalize
        If GRSControlersDictionary.Count > 0 Then ClearSubmissionMode()
    End Sub


#End Region

#End Region


#Region "Task Panes Launches and call backs"


#Region "Input Report Selection"

    Friend Sub InputReportPaneCallBack_ReportCreation()

        APPS.ScreenUpdating = False
        Dim currentcell As Excel.Range = CWorksheetWrittingFunctions.CreateReceptionWS(Me.InputReportTaskPane.EntitiesTV.SelectedNode.Text)
        Dim entityCode As String = Me.InputReportTaskPane.EntitiesTV.SelectedNode.Name
        Dim VERSIONSMGT As New CVersionsForControlingUIs
        Dim periodlist As List(Of Date) = VERSIONSMGT.GetPeriodsDatesList(GLOBALCurrentVersionCode)
        CWorksheetWrittingFunctions.InsertInputReportOnWS(currentcell, _
                                                          periodlist, _
                                                          VERSIONSMGT.versionsCodeTimeSetUpDict(GLOBALCurrentVersionCode)(VERSIONS_TIME_CONFIG_VARIABLE))

        Me.InputReportTaskPane.Hide()
        Me.InputReportTaskPane.Close()
        CRefreshGetDataBatch.RefreshReport(EntitiesMapping.GetEntityCurrency(entityCode))
        Dim EXCELFORMATTING As New CExcelFormatting
        EXCELFORMATTING.FormatExcelRange(APPS.ActiveSheet, INPUT_FORMAT_CODE, periodlist(0))
        APPS.ScreenUpdating = True
        AssociateGRSControler()

    End Sub

#End Region

#Region "Version Selection"

    Public Sub LaunchVersionSelection(Optional ByRef input_mode As Int32 = 0)

        If CDbl(APPS.Version.Replace(".", ",")) > EXCEL_MIN_VERSION Then
            VersionsSelectionPaneVisible = True
            Me.VersionSelectionTaskPane.Init(input_mode)
            Me.VersionSelectionTaskPane.Show()
        Else
            Dim VERSELUI As New VersionSelectionUI
            VERSELUI.Show()
        End If

    End Sub

#End Region

#Region "Entity Selection"

    Friend Sub LaunchEntitySelectionTP(ByRef parentObject As Object, Optional ByRef restrictionToInputsEntities As Boolean = False)

        If CDbl(APPS.Version.Replace(".", ",")) > EXCEL_MIN_VERSION Then
            EntitySelectionPaneVisible = True
            Me.EntitySelectionTaskPane.Init(parentObject, True)
            Me.EntitySelectionTaskPane.Show()
        Else

        End If

    End Sub

    ' call back from entitySelectionTP for setting up new entity in submission control mode
    Public Sub ValidateEntitySelection(ByRef entityName As String)

        CurrentGRSControler.ChangeCurrentEntity(entityName)
        Me.EntitySelectionTaskPane.ClearAndClose()

    End Sub

#End Region


#End Region


#Region "Utilities"

    ' Add an item to a Combo Box
    Private Function AddButtonToDropDown(ByRef DropDownMenu As ADXRibbonDropDown, ByRef BTName As String) As ADXRibbonItem

        Dim ctrl As New ADXRibbonItem
        ctrl.Caption = BTName
        ctrl.Id = BTName
        DropDownMenu.Items.Add(ctrl)
        Return ctrl

    End Function

    Private Sub ClearSubmissionMode()

        ' shouldn' t suppress all GRS.. only the one associated to WS
        ' check that the relation to input task pane is killed

        For Each GRS In GRSControlersDictionary.Values
            ctrlsTextWSDictionary.Remove(GRS.wsComboboxMenuItem.Caption)
            WSCB.Items.Remove(GRS.wsComboboxMenuItem)
            GRS.CloseInstance()
            GRS = Nothing
        Next
        SubmissionModeRibbon.Visible = False
        GRSControlersDictionary.Clear()

    End Sub

    Public Shared Sub SetRibbonVersionName(ByRef versionName As String, ByRef versionCode As String)

        Version_Label.Caption = versionName
        Version_label_Sub_Ribbon.Text = versionName
        GLOBALCurrentVersionCode = versionCode

    End Sub


#End Region





    Private Sub PPTExportBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles PPTExportBT.OnClick

        Dim testHTTPUI As New HTTP_test
        testHTTPUI.Show()

    End Sub






  
   
End Class

