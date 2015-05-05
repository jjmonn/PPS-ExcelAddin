' AddinModule.vb
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
' Last modified: 01/05/2015


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


    Friend WithEvents AdxRibbonGroup2 As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents AdxRibbonGroup1 As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents MaintTab As AddinExpress.MSO.ADXRibbonTab
    Friend WithEvents WSUplaodBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents ControlingUI2BT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonGroup3 As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents adxExcelEvents As AddinExpress.MSO.ADXExcelAppEvents
    Friend WithEvents UploadBT As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents UplodBT1 As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents WBUplaodBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents SettingsBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdvancedModelingBT As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents AdxRibbonMenu2 As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents BreakLinksBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonGroup6 As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents LightsImageList As System.Windows.Forms.ImageList
    Friend WithEvents VersionBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents Addin_Version_label As AddinExpress.MSO.ADXRibbonLabel
    Friend WithEvents ConnectionIcons As System.Windows.Forms.ImageList
    Friend WithEvents ConnectionBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonSeparator1 As AddinExpress.MSO.ADXRibbonSeparator
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
    Friend WithEvents VersionTBSubRibbon As AddinExpress.MSO.ADXRibbonEditBox
    Friend WithEvents CancelBT2 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents SubmissionRibbonIL As System.Windows.Forms.ImageList
    Friend WithEvents SubmissionStatus As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents CurrentLinkedWSBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonSeparator6 As AddinExpress.MSO.ADXRibbonSeparator
    Friend WithEvents AutoComitBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents EntCurrTB As AddinExpress.MSO.ADXRibbonEditBox
    Friend WithEvents RefreshInputsBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents entityEditBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents VersionBT2 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents EditRangesMenuBT As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents EditRangesMenu As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents SelectAccRangeBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents SelectEntitiesRangeBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents SelectPeriodsRangeBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxExcelTaskPanesManager1 As AddinExpress.XL.ADXExcelTaskPanesManager
    Friend WithEvents InputSelectionTaskPaneItem As AddinExpress.XL.ADXExcelTaskPanesCollectionItem
    Friend WithEvents AdxRibbonLabel1 As AddinExpress.MSO.ADXRibbonLabel
    Friend WithEvents VersionSelectionTaskPaneItem As AddinExpress.XL.ADXExcelTaskPanesCollectionItem
    Friend WithEvents EntitySelectionTaskPaneItem As AddinExpress.XL.ADXExcelTaskPanesCollectionItem
    Friend WithEvents Addin_rates_version_label As AddinExpress.MSO.ADXRibbonLabel
    Friend WithEvents LogBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents SubmissionControlBT As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents AdxRibbonMenu3 As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents FunctionDesigner As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents AdxRibbonMenu1 As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents AlternativeScenariosBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdjustmentDD As AddinExpress.MSO.ADXRibbonDropDown
    Friend WithEvents MainTabImageList As System.Windows.Forms.ImageList
    Friend WithEvents InputReportLaunchBT As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents AdxRibbonMenu7 As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents ModellingConfigBT As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents AdxRibbonMenu8 As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents MarketPricesMGT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents ASReportsMGTBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents ASEntitiesAttributesTabBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents ConfigImageList As System.Windows.Forms.ImageList
    Friend WithEvents SubmissionOptionsBT As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents AdxRibbonMenu5 As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents AdxRibbonSeparator4 As AddinExpress.MSO.ADXRibbonSeparator
    Friend WithEvents AdxRibbonSeparator5 As AddinExpress.MSO.ADXRibbonSeparator
    Friend WithEvents NewICOs As System.Windows.Forms.ImageList
    Friend WithEvents NewIcosSmall As System.Windows.Forms.ImageList
    Friend WithEvents ConfigurationRibbonBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonButton1 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonButton2 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonButton3 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonButton4 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents ClientsDropDown As AddinExpress.MSO.ADXRibbonDropDown
    Friend WithEvents ProductsDropDown As AddinExpress.MSO.ADXRibbonDropDown
    Friend WithEvents WSCB As AddinExpress.MSO.ADXRibbonDropDown
    Friend WithEvents RefreshBT As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents RefreshMenu As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents RefreshSelectionBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RefreshWorksheetBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RefreshWorkbookBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonMenuSeparator1 As AddinExpress.MSO.ADXRibbonMenuSeparator
    Friend WithEvents AutoRefreshBT As AddinExpress.MSO.ADXRibbonCheckBox
    Friend WithEvents AdxRibbonButton5 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonMenuSeparator2 As AddinExpress.MSO.ADXRibbonMenuSeparator


#End Region


#Region " Component Designer generated code. "
    'Required by designer
    Private components As System.ComponentModel.IContainer

    'Required by designer - do not modify
    'the following method
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddinModule))
        Me.MaintTab = New AddinExpress.MSO.ADXRibbonTab(Me.components)
        Me.AdxRibbonGroup6 = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.ConnectionBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.ConnectionIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.AdxRibbonSeparator1 = New AddinExpress.MSO.ADXRibbonSeparator(Me.components)
        Me.VersionBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.NewICOs = New System.Windows.Forms.ImageList(Me.components)
        Me.Addin_Version_label = New AddinExpress.MSO.ADXRibbonLabel(Me.components)
        Me.Addin_rates_version_label = New AddinExpress.MSO.ADXRibbonLabel(Me.components)
        Me.AdxRibbonGroup1 = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.UploadBT = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.UplodBT1 = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.WSUplaodBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.WBUplaodBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.CurrentLinkedWSBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.MainTabImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.InputReportLaunchBT = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.AdxRibbonMenu7 = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.RefreshBT = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.RefreshMenu = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.RefreshSelectionBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RefreshWorksheetBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RefreshWorkbookBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonMenuSeparator1 = New AddinExpress.MSO.ADXRibbonMenuSeparator(Me.components)
        Me.AutoRefreshBT = New AddinExpress.MSO.ADXRibbonCheckBox(Me.components)
        Me.AdxRibbonGroup2 = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.ControlingUI2BT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.FunctionDesigner = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.AdxRibbonMenu1 = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.BreakLinksBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdvancedModelingBT = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.AdxRibbonMenu2 = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.AdxRibbonButton5 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.NewIcosSmall = New System.Windows.Forms.ImageList(Me.components)
        Me.AlternativeScenariosBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonMenuSeparator2 = New AddinExpress.MSO.ADXRibbonMenuSeparator(Me.components)
        Me.ModellingConfigBT = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.AdxRibbonMenu8 = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.MarketPricesMGT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.ASReportsMGTBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.ASEntitiesAttributesTabBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.SubmissionControlBT = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.AdxRibbonMenu3 = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.LogBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonGroup3 = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.ConfigurationRibbonBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.SettingsBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.ConfigImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.SubmissionRibbonIL = New System.Windows.Forms.ImageList(Me.components)
        Me.LightsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.adxExcelEvents = New AddinExpress.MSO.ADXExcelAppEvents(Me.components)
        Me.SubmissionModeRibbon = New AddinExpress.MSO.ADXRibbonTab(Me.components)
        Me.SubmissionnGroup = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.SubmitBT2 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonSeparator4 = New AddinExpress.MSO.ADXRibbonSeparator(Me.components)
        Me.AutoComitBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonSeparator6 = New AddinExpress.MSO.ADXRibbonSeparator(Me.components)
        Me.SubmissionStatus = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.CancelBT2 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonSeparator5 = New AddinExpress.MSO.ADXRibbonSeparator(Me.components)
        Me.StateSelectionGroup = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.entityEditBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonButton1 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.VersionBT2 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.CurrentEntityTB = New AddinExpress.MSO.ADXRibbonEditBox(Me.components)
        Me.EntCurrTB = New AddinExpress.MSO.ADXRibbonEditBox(Me.components)
        Me.VersionTBSubRibbon = New AddinExpress.MSO.ADXRibbonEditBox(Me.components)
        Me.AdxRibbonButton2 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonButton3 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonButton4 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdjustmentDD = New AddinExpress.MSO.ADXRibbonDropDown(Me.components)
        Me.ClientsDropDown = New AddinExpress.MSO.ADXRibbonDropDown(Me.components)
        Me.ProductsDropDown = New AddinExpress.MSO.ADXRibbonDropDown(Me.components)
        Me.EditSelectionGroup = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.SubmissionOptionsBT = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.AdxRibbonMenu5 = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.ShowReportBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RefreshInputsBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.HighlightBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.EditRangesMenuBT = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.EditRangesMenu = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.SelectAccRangeBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.SelectEntitiesRangeBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.SelectPeriodsRangeBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.WSCB = New AddinExpress.MSO.ADXRibbonDropDown(Me.components)
        Me.AdxRibbonGroup9 = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.CloseBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxExcelTaskPanesManager1 = New AddinExpress.XL.ADXExcelTaskPanesManager(Me.components)
        Me.InputSelectionTaskPaneItem = New AddinExpress.XL.ADXExcelTaskPanesCollectionItem(Me.components)
        Me.VersionSelectionTaskPaneItem = New AddinExpress.XL.ADXExcelTaskPanesCollectionItem(Me.components)
        Me.EntitySelectionTaskPaneItem = New AddinExpress.XL.ADXExcelTaskPanesCollectionItem(Me.components)
        Me.AdxRibbonLabel1 = New AddinExpress.MSO.ADXRibbonLabel(Me.components)
        '
        'MaintTab
        '
        Me.MaintTab.Caption = "PPS®"
        Me.MaintTab.Controls.Add(Me.AdxRibbonGroup6)
        Me.MaintTab.Controls.Add(Me.AdxRibbonGroup1)
        Me.MaintTab.Controls.Add(Me.AdxRibbonGroup2)
        Me.MaintTab.Controls.Add(Me.AdxRibbonGroup3)
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
        Me.ConnectionIcons.Images.SetKeyName(0, "favicon(4).ico")
        Me.ConnectionIcons.Images.SetKeyName(1, "favicon(3).ico")
        Me.ConnectionIcons.Images.SetKeyName(2, "refresh_red.ico")
        Me.ConnectionIcons.Images.SetKeyName(3, "refresh_green.ico")
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
        Me.VersionBT.Image = 2
        Me.VersionBT.ImageList = Me.NewICOs
        Me.VersionBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.VersionBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'NewICOs
        '
        Me.NewICOs.ImageStream = CType(resources.GetObject("NewICOs.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.NewICOs.TransparentColor = System.Drawing.Color.Transparent
        Me.NewICOs.Images.SetKeyName(0, "window_gear.ico")
        Me.NewICOs.Images.SetKeyName(1, "users_relation2.ico")
        Me.NewICOs.Images.SetKeyName(2, "versions.ico.ico")
        Me.NewICOs.Images.SetKeyName(3, "breakpoints.ico")
        Me.NewICOs.Images.SetKeyName(4, "sizes.ico")
        Me.NewICOs.Images.SetKeyName(5, "element_branch2.ico")
        Me.NewICOs.Images.SetKeyName(6, "upload.ico")
        Me.NewICOs.Images.SetKeyName(7, "window_equalizer.ico")
        Me.NewICOs.Images.SetKeyName(8, "symbol_dollar_euro.ico")
        Me.NewICOs.Images.SetKeyName(9, "barcode.ico")
        Me.NewICOs.Images.SetKeyName(10, "users_relation2.ico")
        Me.NewICOs.Images.SetKeyName(11, "registry.ico")
        Me.NewICOs.Images.SetKeyName(12, "favicon.ico")
        Me.NewICOs.Images.SetKeyName(13, "magnifying_glass.ico")
        Me.NewICOs.Images.SetKeyName(14, "ok.ico")
        Me.NewICOs.Images.SetKeyName(15, "breakpoints.ico")
        Me.NewICOs.Images.SetKeyName(16, "solar_panel.ico")
        Me.NewICOs.Images.SetKeyName(17, "favicon.ico")
        Me.NewICOs.Images.SetKeyName(18, "favicon(1).ico")
        Me.NewICOs.Images.SetKeyName(19, "favicon(2).ico")
        Me.NewICOs.Images.SetKeyName(20, "chart2.ico")
        Me.NewICOs.Images.SetKeyName(21, "spreadsheet.ico.ico")
        Me.NewICOs.Images.SetKeyName(22, "edit.ico.ico")
        Me.NewICOs.Images.SetKeyName(23, "upload.ico")
        Me.NewICOs.Images.SetKeyName(24, "selection_refresh.ico")
        Me.NewICOs.Images.SetKeyName(25, "favicon(2).ico")
        Me.NewICOs.Images.SetKeyName(26, "money.ico")
        Me.NewICOs.Images.SetKeyName(27, "favicon(6).ico")
        Me.NewICOs.Images.SetKeyName(28, "favicon(11).ico")
        Me.NewICOs.Images.SetKeyName(29, "favicon(10).ico")
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
        Me.AdxRibbonGroup1.Controls.Add(Me.InputReportLaunchBT)
        Me.AdxRibbonGroup1.Controls.Add(Me.RefreshBT)
        Me.AdxRibbonGroup1.Id = "adxRibbonGroup_6d270a7302274c0bb0cb396921e59e09"
        Me.AdxRibbonGroup1.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonGroup1.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'UploadBT
        '
        Me.UploadBT.Caption = "Snapshot"
        Me.UploadBT.Controls.Add(Me.UplodBT1)
        Me.UploadBT.Id = "adxRibbonSplitButton_d2989ac910ad415381c6cc902b2051e5"
        Me.UploadBT.Image = 4
        Me.UploadBT.ImageList = Me.MainTabImageList
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
        Me.WSUplaodBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.WSUplaodBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.WSUplaodBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'WBUplaodBT
        '
        Me.WBUplaodBT.Caption = "Workbook Upload"
        Me.WBUplaodBT.Id = "adxRibbonButton_24b9e7a516574e889eda47fea0a5cc43"
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
        'MainTabImageList
        '
        Me.MainTabImageList.ImageStream = CType(resources.GetObject("MainTabImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.MainTabImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.MainTabImageList.Images.SetKeyName(0, "favicon(262).ico")
        Me.MainTabImageList.Images.SetKeyName(1, "favicon(251).ico")
        Me.MainTabImageList.Images.SetKeyName(2, "financial-graph ctrl bcgk(1).ico")
        Me.MainTabImageList.Images.SetKeyName(3, "favicon(238).ico")
        Me.MainTabImageList.Images.SetKeyName(4, "favicon(208).ico")
        Me.MainTabImageList.Images.SetKeyName(5, "favicon(16).ico")
        Me.MainTabImageList.Images.SetKeyName(6, "favicon (6).ico")
        Me.MainTabImageList.Images.SetKeyName(7, "favicon(13).ico")
        Me.MainTabImageList.Images.SetKeyName(8, "favicon(248).ico")
        Me.MainTabImageList.Images.SetKeyName(9, "favicon(236).ico")
        Me.MainTabImageList.Images.SetKeyName(10, "favicon(242).ico")
        Me.MainTabImageList.Images.SetKeyName(11, "Edit glossy.ico")
        Me.MainTabImageList.Images.SetKeyName(12, "Controlling glossy.ico")
        Me.MainTabImageList.Images.SetKeyName(13, "favicon(12).ico")
        Me.MainTabImageList.Images.SetKeyName(14, "favicon(249).ico")
        Me.MainTabImageList.Images.SetKeyName(15, "favicon(8).ico")
        Me.MainTabImageList.Images.SetKeyName(16, "favicon(233).ico")
        Me.MainTabImageList.Images.SetKeyName(17, "favicon(9).ico")
        Me.MainTabImageList.Images.SetKeyName(18, "favicon(7).ico")
        Me.MainTabImageList.Images.SetKeyName(19, "favicon(11).ico")
        Me.MainTabImageList.Images.SetKeyName(20, "break link orange.png")
        Me.MainTabImageList.Images.SetKeyName(21, "Scenario.ico")
        Me.MainTabImageList.Images.SetKeyName(22, "db Purple big.ico")
        Me.MainTabImageList.Images.SetKeyName(23, "favicon(13).ico")
        Me.MainTabImageList.Images.SetKeyName(24, "favicon(15).ico")
        Me.MainTabImageList.Images.SetKeyName(25, "symbol_dollar_euro.ico")
        '
        'InputReportLaunchBT
        '
        Me.InputReportLaunchBT.Caption = "Edition"
        Me.InputReportLaunchBT.Controls.Add(Me.AdxRibbonMenu7)
        Me.InputReportLaunchBT.Id = "adxRibbonSplitButton_815e28e10c3d4a28a2eee8165efe3ff1"
        Me.InputReportLaunchBT.Image = 22
        Me.InputReportLaunchBT.ImageList = Me.NewICOs
        Me.InputReportLaunchBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.InputReportLaunchBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.InputReportLaunchBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonMenu7
        '
        Me.AdxRibbonMenu7.Caption = "AdxRibbonMenu7"
        Me.AdxRibbonMenu7.Id = "adxRibbonMenu_14409f6805984f4b8af7d826c391ebff"
        Me.AdxRibbonMenu7.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonMenu7.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'RefreshBT
        '
        Me.RefreshBT.Caption = "Refresh"
        Me.RefreshBT.Controls.Add(Me.RefreshMenu)
        Me.RefreshBT.Id = "adxRibbonSplitButton_8aab3e36ecdf4fdfbd62b6bc29af40ee"
        Me.RefreshBT.Image = 19
        Me.RefreshBT.ImageList = Me.MainTabImageList
        Me.RefreshBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RefreshBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RefreshBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'RefreshMenu
        '
        Me.RefreshMenu.Caption = "AdxRibbonMenu4"
        Me.RefreshMenu.Controls.Add(Me.RefreshSelectionBT)
        Me.RefreshMenu.Controls.Add(Me.RefreshWorksheetBT)
        Me.RefreshMenu.Controls.Add(Me.RefreshWorkbookBT)
        Me.RefreshMenu.Controls.Add(Me.AdxRibbonMenuSeparator1)
        Me.RefreshMenu.Controls.Add(Me.AutoRefreshBT)
        Me.RefreshMenu.Id = "adxRibbonMenu_0ae167392d0a46098646510a5dbc2853"
        Me.RefreshMenu.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RefreshMenu.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'RefreshSelectionBT
        '
        Me.RefreshSelectionBT.Caption = "Refresh Selection"
        Me.RefreshSelectionBT.Id = "adxRibbonButton_3054a08e726d43dcba22a017f6d8905c"
        Me.RefreshSelectionBT.Image = 24
        Me.RefreshSelectionBT.ImageList = Me.NewICOs
        Me.RefreshSelectionBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RefreshSelectionBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'RefreshWorksheetBT
        '
        Me.RefreshWorksheetBT.Caption = "Refresh Worksheet"
        Me.RefreshWorksheetBT.Id = "adxRibbonButton_a4e368eb8aff4079a3fa585ebe79d4fe"
        Me.RefreshWorksheetBT.Image = 21
        Me.RefreshWorksheetBT.ImageList = Me.NewICOs
        Me.RefreshWorksheetBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RefreshWorksheetBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'RefreshWorkbookBT
        '
        Me.RefreshWorkbookBT.Caption = "Refresh Workbook"
        Me.RefreshWorkbookBT.Id = "adxRibbonButton_456f45077bc4422dbde8ae3912d4a635"
        Me.RefreshWorkbookBT.Image = 25
        Me.RefreshWorkbookBT.ImageList = Me.NewICOs
        Me.RefreshWorkbookBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RefreshWorkbookBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonMenuSeparator1
        '
        Me.AdxRibbonMenuSeparator1.Caption = "Option"
        Me.AdxRibbonMenuSeparator1.Id = "adxRibbonMenuSeparator_0e263d2a409c42da887efa81f01ad042"
        Me.AdxRibbonMenuSeparator1.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AutoRefreshBT
        '
        Me.AutoRefreshBT.Caption = "Auto Refresh"
        Me.AutoRefreshBT.Id = "adxRibbonCheckBox_b2ef6805d0174a5d81ade5a1952862c7"
        Me.AutoRefreshBT.Pressed = True
        Me.AutoRefreshBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonGroup2
        '
        Me.AdxRibbonGroup2.Caption = "Data Mining"
        Me.AdxRibbonGroup2.Controls.Add(Me.ControlingUI2BT)
        Me.AdxRibbonGroup2.Controls.Add(Me.FunctionDesigner)
        Me.AdxRibbonGroup2.Controls.Add(Me.AdvancedModelingBT)
        Me.AdxRibbonGroup2.Controls.Add(Me.SubmissionControlBT)
        Me.AdxRibbonGroup2.Id = "adxRibbonGroup_13e7ba6b0acf4975af1793d5cfec00ed"
        Me.AdxRibbonGroup2.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonGroup2.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'ControlingUI2BT
        '
        Me.ControlingUI2BT.Caption = "Financials"
        Me.ControlingUI2BT.Id = "adxRibbonButton_7d5683509a144f39bf95ea6b3db155b9"
        Me.ControlingUI2BT.Image = 12
        Me.ControlingUI2BT.ImageList = Me.NewICOs
        Me.ControlingUI2BT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ControlingUI2BT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.ControlingUI2BT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'FunctionDesigner
        '
        Me.FunctionDesigner.Caption = "PPSBI"
        Me.FunctionDesigner.Controls.Add(Me.AdxRibbonMenu1)
        Me.FunctionDesigner.Id = "adxRibbonSplitButton_5986acbd4f414054bdd1177565ae2cf3"
        Me.FunctionDesigner.Image = 21
        Me.FunctionDesigner.ImageList = Me.NewICOs
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
        Me.BreakLinksBT.Image = 20
        Me.BreakLinksBT.ImageList = Me.MainTabImageList
        Me.BreakLinksBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.BreakLinksBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdvancedModelingBT
        '
        Me.AdvancedModelingBT.Caption = "Modeling"
        Me.AdvancedModelingBT.Controls.Add(Me.AdxRibbonMenu2)
        Me.AdvancedModelingBT.Id = "adxRibbonSplitButton_48193c7e13704758a3814aa6156d5aee"
        Me.AdvancedModelingBT.Image = 27
        Me.AdvancedModelingBT.ImageList = Me.NewICOs
        Me.AdvancedModelingBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdvancedModelingBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.AdvancedModelingBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonMenu2
        '
        Me.AdxRibbonMenu2.Caption = "Menu1"
        Me.AdxRibbonMenu2.Controls.Add(Me.AdxRibbonButton5)
        Me.AdxRibbonMenu2.Controls.Add(Me.AlternativeScenariosBT)
        Me.AdxRibbonMenu2.Controls.Add(Me.AdxRibbonMenuSeparator2)
        Me.AdxRibbonMenu2.Controls.Add(Me.ModellingConfigBT)
        Me.AdxRibbonMenu2.Id = "adxRibbonMenu_e6e084f75db842218b137a40a859729b"
        Me.AdxRibbonMenu2.ImageMso = "EquationInsertGallery"
        Me.AdxRibbonMenu2.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonMenu2.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.AdxRibbonMenu2.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonButton5
        '
        Me.AdxRibbonButton5.Caption = "Financial Modeling"
        Me.AdxRibbonButton5.Id = "adxRibbonButton_543572754ac3483d8751176b3133ad1d"
        Me.AdxRibbonButton5.Image = 0
        Me.AdxRibbonButton5.ImageList = Me.NewIcosSmall
        Me.AdxRibbonButton5.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonButton5.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.AdxRibbonButton5.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'NewIcosSmall
        '
        Me.NewIcosSmall.ImageStream = CType(resources.GetObject("NewIcosSmall.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.NewIcosSmall.TransparentColor = System.Drawing.Color.Transparent
        Me.NewIcosSmall.Images.SetKeyName(0, "university.ico")
        Me.NewIcosSmall.Images.SetKeyName(1, "tables.ico")
        Me.NewIcosSmall.Images.SetKeyName(2, "element_branch2.ico")
        '
        'AlternativeScenariosBT
        '
        Me.AlternativeScenariosBT.Caption = "Prices Scenarii"
        Me.AlternativeScenariosBT.Id = "adxRibbonButton_6dd871acf7b0458eb10d6781b75ced2b"
        Me.AlternativeScenariosBT.Image = 1
        Me.AlternativeScenariosBT.ImageList = Me.NewIcosSmall
        Me.AlternativeScenariosBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AlternativeScenariosBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.AlternativeScenariosBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonMenuSeparator2
        '
        Me.AdxRibbonMenuSeparator2.Caption = " "
        Me.AdxRibbonMenuSeparator2.Id = "adxRibbonMenuSeparator_cd62524bad69495687241bedb004ef72"
        Me.AdxRibbonMenuSeparator2.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'ModellingConfigBT
        '
        Me.ModellingConfigBT.Caption = "Configuration"
        Me.ModellingConfigBT.Controls.Add(Me.AdxRibbonMenu8)
        Me.ModellingConfigBT.Id = "adxRibbonSplitButton_275360996f3a4611ad07fb3f83b0eda5"
        Me.ModellingConfigBT.Image = 2
        Me.ModellingConfigBT.ImageList = Me.NewIcosSmall
        Me.ModellingConfigBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ModellingConfigBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonMenu8
        '
        Me.AdxRibbonMenu8.Caption = "AdxRibbonMenu8"
        Me.AdxRibbonMenu8.Controls.Add(Me.MarketPricesMGT)
        Me.AdxRibbonMenu8.Controls.Add(Me.ASReportsMGTBT)
        Me.AdxRibbonMenu8.Controls.Add(Me.ASEntitiesAttributesTabBT)
        Me.AdxRibbonMenu8.Id = "adxRibbonMenu_135b85f63a8d4b47a0cd17f0a349474e"
        Me.AdxRibbonMenu8.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonMenu8.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'MarketPricesMGT
        '
        Me.MarketPricesMGT.Caption = "Market Prices"
        Me.MarketPricesMGT.Id = "adxRibbonButton_0f92ce9b8e174002914213df4f0e9695"
        Me.MarketPricesMGT.Image = 2
        Me.MarketPricesMGT.ImageList = Me.MainTabImageList
        Me.MarketPricesMGT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.MarketPricesMGT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'ASReportsMGTBT
        '
        Me.ASReportsMGTBT.Caption = "Reports"
        Me.ASReportsMGTBT.Id = "adxRibbonButton_8831fac669b747eaad49c81f3aac35d3"
        Me.ASReportsMGTBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ASReportsMGTBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'ASEntitiesAttributesTabBT
        '
        Me.ASEntitiesAttributesTabBT.Caption = "Formulas and Tax rates"
        Me.ASEntitiesAttributesTabBT.Id = "adxRibbonButton_e576c2ab0d484106a08b784c65084217"
        Me.ASEntitiesAttributesTabBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ASEntitiesAttributesTabBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'SubmissionControlBT
        '
        Me.SubmissionControlBT.Caption = "Controls"
        Me.SubmissionControlBT.Controls.Add(Me.AdxRibbonMenu3)
        Me.SubmissionControlBT.Id = "adxRibbonSplitButton_c948ce7f779a4f6cbe647f9de2d15b60"
        Me.SubmissionControlBT.Image = 14
        Me.SubmissionControlBT.ImageList = Me.NewICOs
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
        Me.LogBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.LogBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonGroup3
        '
        Me.AdxRibbonGroup3.Caption = "Configuration"
        Me.AdxRibbonGroup3.Controls.Add(Me.ConfigurationRibbonBT)
        Me.AdxRibbonGroup3.Controls.Add(Me.SettingsBT)
        Me.AdxRibbonGroup3.Id = "adxRibbonGroup_472aee773e454c20851d757e92f14553"
        Me.AdxRibbonGroup3.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonGroup3.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'ConfigurationRibbonBT
        '
        Me.ConfigurationRibbonBT.Caption = "Configuration"
        Me.ConfigurationRibbonBT.Id = "adxRibbonButton_d4cfb8ca7c6d487b9892703b68ee0fce"
        Me.ConfigurationRibbonBT.Image = 29
        Me.ConfigurationRibbonBT.ImageList = Me.NewICOs
        Me.ConfigurationRibbonBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ConfigurationRibbonBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.ConfigurationRibbonBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'SettingsBT
        '
        Me.SettingsBT.Caption = "PPS Settings"
        Me.SettingsBT.Id = "adxRibbonButton_aa28ec782b5541edb1482374e14ceaa6"
        Me.SettingsBT.Image = 28
        Me.SettingsBT.ImageList = Me.NewICOs
        Me.SettingsBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.SettingsBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.SettingsBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'ConfigImageList
        '
        Me.ConfigImageList.ImageStream = CType(resources.GetObject("ConfigImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ConfigImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ConfigImageList.Images.SetKeyName(0, "favicon(10).ico")
        Me.ConfigImageList.Images.SetKeyName(1, "favicon(14).ico")
        Me.ConfigImageList.Images.SetKeyName(2, "favicon(15).ico")
        Me.ConfigImageList.Images.SetKeyName(3, "favicon(10).ico")
        Me.ConfigImageList.Images.SetKeyName(4, "config blue circle.ico")
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
        Me.SubmissionRibbonIL.Images.SetKeyName(31, "imageres_89.ico")
        '
        'LightsImageList
        '
        Me.LightsImageList.ImageStream = CType(resources.GetObject("LightsImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.LightsImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.LightsImageList.Images.SetKeyName(0, "favicon.ico")
        Me.LightsImageList.Images.SetKeyName(1, "ok.ico")
        Me.LightsImageList.Images.SetKeyName(2, "delete.ico")
        '
        'SubmissionModeRibbon
        '
        Me.SubmissionModeRibbon.Caption = "Data Submission"
        Me.SubmissionModeRibbon.Controls.Add(Me.SubmissionnGroup)
        Me.SubmissionModeRibbon.Controls.Add(Me.StateSelectionGroup)
        Me.SubmissionModeRibbon.Controls.Add(Me.EditSelectionGroup)
        Me.SubmissionModeRibbon.Controls.Add(Me.AdxRibbonGroup9)
        Me.SubmissionModeRibbon.Id = "adxRibbonTab_7e9a50d1d91c429697388afda23d5b15"
        Me.SubmissionModeRibbon.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'SubmissionnGroup
        '
        Me.SubmissionnGroup.Caption = "Submission"
        Me.SubmissionnGroup.Controls.Add(Me.SubmitBT2)
        Me.SubmissionnGroup.Controls.Add(Me.AdxRibbonSeparator4)
        Me.SubmissionnGroup.Controls.Add(Me.AutoComitBT)
        Me.SubmissionnGroup.Controls.Add(Me.AdxRibbonSeparator6)
        Me.SubmissionnGroup.Controls.Add(Me.SubmissionStatus)
        Me.SubmissionnGroup.Controls.Add(Me.CancelBT2)
        Me.SubmissionnGroup.Controls.Add(Me.AdxRibbonSeparator5)
        Me.SubmissionnGroup.Id = "adxRibbonGroup_d13fa5b1f4584ad99081875d975057c9"
        Me.SubmissionnGroup.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.SubmissionnGroup.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'SubmitBT2
        '
        Me.SubmitBT2.Caption = "Submit"
        Me.SubmitBT2.Id = "adxRibbonButton_f781efe7b70b4e2387fe5a59f0772128"
        Me.SubmitBT2.Image = 23
        Me.SubmitBT2.ImageList = Me.NewICOs
        Me.SubmitBT2.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.SubmitBT2.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.SubmitBT2.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonSeparator4
        '
        Me.AdxRibbonSeparator4.Id = "adxRibbonSeparator_90768c84873042c0b7a443753b999540"
        Me.AdxRibbonSeparator4.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
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
        'AdxRibbonSeparator6
        '
        Me.AdxRibbonSeparator6.Id = "adxRibbonSeparator_b9b1011c15a54de3b20d1b83916ab7ce"
        Me.AdxRibbonSeparator6.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'SubmissionStatus
        '
        Me.SubmissionStatus.Caption = "Status"
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
        Me.CancelBT2.Image = 31
        Me.CancelBT2.ImageList = Me.SubmissionRibbonIL
        Me.CancelBT2.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.CancelBT2.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonSeparator5
        '
        Me.AdxRibbonSeparator5.Id = "adxRibbonSeparator_4d06e3f646cb46e7bfbd9ed5fff3c739"
        Me.AdxRibbonSeparator5.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'StateSelectionGroup
        '
        Me.StateSelectionGroup.Caption = "Entity Information"
        Me.StateSelectionGroup.Controls.Add(Me.entityEditBT)
        Me.StateSelectionGroup.Controls.Add(Me.AdxRibbonButton1)
        Me.StateSelectionGroup.Controls.Add(Me.VersionBT2)
        Me.StateSelectionGroup.Controls.Add(Me.CurrentEntityTB)
        Me.StateSelectionGroup.Controls.Add(Me.EntCurrTB)
        Me.StateSelectionGroup.Controls.Add(Me.VersionTBSubRibbon)
        Me.StateSelectionGroup.Controls.Add(Me.AdxRibbonButton2)
        Me.StateSelectionGroup.Controls.Add(Me.AdxRibbonButton3)
        Me.StateSelectionGroup.Controls.Add(Me.AdxRibbonButton4)
        Me.StateSelectionGroup.Controls.Add(Me.AdjustmentDD)
        Me.StateSelectionGroup.Controls.Add(Me.ClientsDropDown)
        Me.StateSelectionGroup.Controls.Add(Me.ProductsDropDown)
        Me.StateSelectionGroup.Id = "adxRibbonGroup_31e139cc6d9d421dbf833740d494b4a0"
        Me.StateSelectionGroup.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.StateSelectionGroup.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'entityEditBT
        '
        Me.entityEditBT.Caption = "Legal Entity"
        Me.entityEditBT.Id = "adxRibbonButton_4134f852978240a19f9e57bcd1ce4b13"
        Me.entityEditBT.Image = 5
        Me.entityEditBT.ImageList = Me.NewICOs
        Me.entityEditBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.entityEditBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonButton1
        '
        Me.AdxRibbonButton1.Caption = "Currency"
        Me.AdxRibbonButton1.Id = "adxRibbonButton_761717ad648d4c9e9bcf165ae54c587a"
        Me.AdxRibbonButton1.Image = 8
        Me.AdxRibbonButton1.ImageList = Me.NewICOs
        Me.AdxRibbonButton1.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonButton1.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'VersionBT2
        '
        Me.VersionBT2.Caption = "Version"
        Me.VersionBT2.Id = "adxRibbonButton_2a8644b2388844f9aa05d2c0c48d6969"
        Me.VersionBT2.Image = 15
        Me.VersionBT2.ImageList = Me.NewICOs
        Me.VersionBT2.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.VersionBT2.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'CurrentEntityTB
        '
        Me.CurrentEntityTB.Caption = " "
        Me.CurrentEntityTB.Enabled = False
        Me.CurrentEntityTB.Id = "adxRibbonEditBox_994a7df7764c43dbb7a7f4392d489c7a"
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
        Me.VersionTBSubRibbon.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.VersionTBSubRibbon.MaxLength = 30
        Me.VersionTBSubRibbon.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.VersionTBSubRibbon.SizeString = "wwwwwwwwwww"
        '
        'AdxRibbonButton2
        '
        Me.AdxRibbonButton2.Caption = "Adjustment"
        Me.AdxRibbonButton2.Id = "adxRibbonButton_2e9c88867cbf42739ff76362741a9df5"
        Me.AdxRibbonButton2.Image = 7
        Me.AdxRibbonButton2.ImageList = Me.NewICOs
        Me.AdxRibbonButton2.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonButton2.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonButton3
        '
        Me.AdxRibbonButton3.Caption = "Client"
        Me.AdxRibbonButton3.Id = "adxRibbonButton_f4417fc36c7146838b067b42e3c2ce9c"
        Me.AdxRibbonButton3.Image = 1
        Me.AdxRibbonButton3.ImageList = Me.NewICOs
        Me.AdxRibbonButton3.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonButton3.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonButton4
        '
        Me.AdxRibbonButton4.Caption = "Product"
        Me.AdxRibbonButton4.Id = "adxRibbonButton_e6a542c796824b86acbcf89f89d6ae83"
        Me.AdxRibbonButton4.Image = 9
        Me.AdxRibbonButton4.ImageList = Me.NewICOs
        Me.AdxRibbonButton4.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonButton4.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdjustmentDD
        '
        Me.AdjustmentDD.Caption = " "
        Me.AdjustmentDD.Id = "adxRibbonDropDown_3f4be5b43d974d048c6f5ea42f91efd4"
        Me.AdjustmentDD.ImageList = Me.NewICOs
        Me.AdjustmentDD.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdjustmentDD.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.AdjustmentDD.SelectedItemIndex = 0
        '
        'ClientsDropDown
        '
        Me.ClientsDropDown.Caption = " "
        Me.ClientsDropDown.Id = "adxRibbonDropDown_432cc9be8ce74aeebdf0fc7c1c9a0f81"
        Me.ClientsDropDown.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ClientsDropDown.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'ProductsDropDown
        '
        Me.ProductsDropDown.Caption = " "
        Me.ProductsDropDown.Id = "adxRibbonDropDown_dcb69c26dbd748ba85a3a85aa9199939"
        Me.ProductsDropDown.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ProductsDropDown.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'EditSelectionGroup
        '
        Me.EditSelectionGroup.Caption = " Settings"
        Me.EditSelectionGroup.Controls.Add(Me.SubmissionOptionsBT)
        Me.EditSelectionGroup.Controls.Add(Me.WSCB)
        Me.EditSelectionGroup.Id = "adxRibbonGroup_845aac06fe0b43ffa1a5ab1c8cf56d7a"
        Me.EditSelectionGroup.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.EditSelectionGroup.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'SubmissionOptionsBT
        '
        Me.SubmissionOptionsBT.Caption = " Options"
        Me.SubmissionOptionsBT.Controls.Add(Me.AdxRibbonMenu5)
        Me.SubmissionOptionsBT.Id = "adxRibbonSplitButton_ed37a10f5b0a4990ad8f167982663559"
        Me.SubmissionOptionsBT.Image = 24
        Me.SubmissionOptionsBT.ImageList = Me.SubmissionRibbonIL
        Me.SubmissionOptionsBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.SubmissionOptionsBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.SubmissionOptionsBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonMenu5
        '
        Me.AdxRibbonMenu5.Caption = "AdxRibbonMenu5"
        Me.AdxRibbonMenu5.Controls.Add(Me.ShowReportBT)
        Me.AdxRibbonMenu5.Controls.Add(Me.RefreshInputsBT)
        Me.AdxRibbonMenu5.Controls.Add(Me.HighlightBT)
        Me.AdxRibbonMenu5.Controls.Add(Me.EditRangesMenuBT)
        Me.AdxRibbonMenu5.Id = "adxRibbonMenu_22c008855d864379ba85629a938602dc"
        Me.AdxRibbonMenu5.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonMenu5.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
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
        'RefreshInputsBT
        '
        Me.RefreshInputsBT.Caption = "Refresh Inputs"
        Me.RefreshInputsBT.Id = "adxRibbonButton_88ae883ce9de414bacaf9f25f558dffc"
        Me.RefreshInputsBT.Image = 29
        Me.RefreshInputsBT.ImageList = Me.SubmissionRibbonIL
        Me.RefreshInputsBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RefreshInputsBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'HighlightBT
        '
        Me.HighlightBT.Caption = "Highlight Inputs"
        Me.HighlightBT.Id = "adxRibbonButton_703868e94c394267b617fb17c2879b3c"
        Me.HighlightBT.Image = 25
        Me.HighlightBT.ImageList = Me.SubmissionRibbonIL
        Me.HighlightBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.HighlightBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.HighlightBT.ToggleButton = True
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
        'WSCB
        '
        Me.WSCB.Caption = "Excel Worksheet"
        Me.WSCB.Id = "adxRibbonDropDown_051d087fb85b425c947f7c3a0fe28700"
        Me.WSCB.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.WSCB.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonGroup9
        '
        Me.AdxRibbonGroup9.Caption = "Exit"
        Me.AdxRibbonGroup9.Controls.Add(Me.CloseBT)
        Me.AdxRibbonGroup9.Id = "adxRibbonGroup_0a2b25ca3df2428895a2a7148bf5329d"
        Me.AdxRibbonGroup9.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonGroup9.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
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
    Friend GRSControlersDictionary As New Dictionary(Of Excel.Worksheet, GeneralSubmissionControler)
    Private ctrlsTextWSDictionary As New Dictionary(Of String, Excel.Worksheet)
    Public setUpFlag As Boolean
    Public ppsbi_refresh_flag As Boolean = True
    Private CurrentGRSControler As GeneralSubmissionControler
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
        VIBlend.Utilities.Licensing.LicenseContent = "0g1g635664091223436417g90e96c0507a72043860957103d347f6e2aff|FAm4zjmJK8JlBTfmliEZJprE1CQtP5o4nLEaO1P8Jg1Ruvdu29b8pYhiy1w6gs7VuzD3JFAdcGgdcXLAmYbK7P4suihb6MdQLW6pYShfS/hp1VldcuSxmveQnpm0mVnbpoub6Zf9uU4f4Tz9kiMVIdiQHtri4VQC73nxfJF7tLw="
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

        GlobalVariables.APPS = Me.HostApplication

        ' Main Ribbon Initialize
        GlobalVariables.SubmissionStatusButton = SubmissionStatus
        GlobalVariables.Connection_Toggle_Button = Me.ConnectionBT
        GlobalVariables.Version_Label = Me.Addin_Version_label
        ConnectionBT.Image = 0

        ' Submission Ribbon Initialize
        WSCB.Items.RemoveAt(0)
        SubmissionModeRibbon.Visible = False
        GlobalVariables.Version_label_Sub_Ribbon = VersionTBSubRibbon
        GlobalVariables.ClientsIDDropDown = ClientsDropDown
        GlobalVariables.ProductsIDDropDown = ProductsDropDown
        GlobalVariables.AdjustmentIDDropDown = AdjustmentDD

    End Sub

    ' (Triggered by ExcelAddinModule1)
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
        Return GlobalVariables.User_Credential
    End Function

    ' Returns the entities View variable
    Public Function GetEntitiesView() As String
        Return GlobalVariables.Entities_View
    End Function

    ' Returns the entities View variable
    Public Function GetVersionLabel() As ADXRibbonLabel
        Return GlobalVariables.Version_Label
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
            If CDbl(GlobalVariables.APPS.Version.Replace(".", ",")) > EXCEL_MIN_VERSION Then
                GlobalVariables.InputSelectionPaneVisible = True
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

        If GRSControlersDictionary.ContainsKey(GlobalVariables.APPS.ActiveSheet) Then
            CurrentGRSControler = GRSControlersDictionary(GlobalVariables.APPS.ActiveSheet)
            CurrentGRSControler.UpdateRibbon()
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

        Dim ctrl As ADXRibbonItem = AddButtonToDropDown(WSCB, GlobalVariables.APPS.ActiveSheet.name, GlobalVariables.APPS.ActiveSheet.name)
        Dim CGRSControlerInstance As New GeneralSubmissionControler(ctrl, Me)

        GRSControlersDictionary.Add(GlobalVariables.APPS.ActiveSheet, CGRSControlerInstance)
        ctrlsTextWSDictionary.Add(ctrl.Caption, GlobalVariables.APPS.ActiveSheet)
        CurrentGRSControler = CGRSControlerInstance
        CurrentGRSControler.LaunchDatasetSnapshotAndAssociateModel()
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


#Region "Refresh"

    Private Sub AdxRibbonSplitButton1_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RefreshBT.OnClick, RefreshWorksheetBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            If Not GRSControlersDictionary.ContainsKey(GlobalVariables.APPS.ActiveSheet) Then
                Dim cREFRESH As New RefreshGetDataBatch
                cREFRESH.RefreshWorksheet()
            Else
                MsgBox("Cannot Refresh while the worksheet is being edited. " + Chr(13) + _
                       "The submission must be closed for this worksheet in order to refresh it.")
            End If
        End If

    End Sub

    Private Sub RefreshSelectionBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RefreshSelectionBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            If Not GRSControlersDictionary.ContainsKey(GlobalVariables.APPS.ActiveSheet) Then
                Dim cREFRESH As New RefreshGetDataBatch
                Dim ws As Excel.Worksheet = GlobalVariables.APPS.ActiveSheet
                cREFRESH.RefreshWorksheet(GlobalVariables.APPS.Selection)
            Else
                MsgBox("Cannot Refresh while the worksheet is being edited. " + Chr(13) + _
                       "The submission must be closed for this worksheet in order to refresh it.")
            End If
        End If

    End Sub

    Private Sub RefreshWorkbookBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RefreshWorkbookBT.OnClick

        ' to be implemented !

    End Sub

    Private Sub AutoRefreshBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles AutoRefreshBT.OnClick

        If ppsbi_refresh_flag = True Then

            ppsbi_refresh_flag = False
        Else

            ppsbi_refresh_flag = True
        End If


    End Sub

#End Region


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

    Private Sub BreakLinksBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles BreakLinksBT.OnClick

        RefreshGetDataBatch.BreakLinks()

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

#Region "Modelling"

    Private Sub AdvancedModelingBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles AdvancedModelingBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim FModellingController As New FModellingSimulationsControler
        End If

    End Sub

#Region "Configuration"

    Private Sub MarketPricesMGT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles MarketPricesMGT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim MarketPricesMGT As New MarketPricesController
        End If

    End Sub

    Private Sub ASReportsMGTBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles ASReportsMGTBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim ReportDesigner As New ReportsDesignerController(GDF_AS_REPORTS_TABLE, GDFSUEZASAccountsMapping.GetAlternativeScenarioAccountsList)
        End If

    End Sub

    Private Sub ASEntitiesAttributesTabBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles ASEntitiesAttributesTabBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim EntitiesAttributesMGT As New ASEntitiesAttributesController
        End If

    End Sub

#End Region

#End Region

    Private Sub AlternativeScenariosBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles AlternativeScenariosBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim ASController As New AlternativeScenariosController
        End If

    End Sub


#Region "Platform Management"

    Private Sub ConfigurationRibbonBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles ConfigurationRibbonBT.OnClick

        If ConnectioN Is Nothing Then
            Dim CONNUI As New ConnectionUI(Me)
            CONNUI.Show()
        Else
            Dim MGTUI As New PlatformMGTGeneralUI
            MGTUI.Show()
        End If


    End Sub


#End Region


#Region "Export"

    'Private Sub ReportFMTBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) _
    '        Handles ReportFmtBT.OnClick, FormattingBT.OnClick

    '    If ConnectioN Is Nothing Then
    '        Dim CONNUI As New ConnectionUI(Me)
    '        CONNUI.Show()
    '    Else
    '        CExcelFormatting.FormatExcelRange(GlobalVariables.APPS.ActiveSheet, REPORT_FORMAT_CODE)
    '    End If

    'End Sub

    'Private Sub InputFMTBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles InputFmtBT.OnClick

    '    If ConnectioN Is Nothing Then
    '        Dim CONNUI As New ConnectionUI(Me)
    '        CONNUI.Show()
    '    Else
    '        CExcelFormatting.FormatExcelRange(GlobalVariables.APPS.ActiveSheet, INPUT_FORMAT_CODE)
    '    End If

    'End Sub

#End Region


#Region "Settings"

    Private Sub SettingsBT_onclick(sender As System.Object,
                                    control As AddinExpress.MSO.IRibbonControl,
                                    pressed As System.Boolean) Handles SettingsBT.OnClick

        Dim SETTINGSUI As New SettingMainUI
        SETTINGSUI.Show()


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

    Private Sub ReportOptionsBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean)
        ' to implement !!
    End Sub

    Private Sub CloseBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles CloseBT.OnClick
        ClearSubmissionMode()
    End Sub

#End Region

#Region "Analysis Axis Drop Down Buttons Loading"

    Protected Friend Shared Sub loadDropDownsSubmissionButtons()

        GlobalVariables.ClientsIDDropDown.Items.Clear()
        Dim clients_dict = ClientsMapping.GetClientsDictionary(ANALYSIS_AXIS_ID_VAR, ANALYSIS_AXIS_NAME_VAR)
        For Each key As String In clients_dict.Keys
            AddButtonToDropDown(GlobalVariables.ClientsIDDropDown, _
                                key, _
                                clients_dict(key))
        Next
        GlobalVariables.ClientsIDDropDown.SelectedItemId = DEFAULT_ANALYSIS_AXIS_ID

        GlobalVariables.ProductsIDDropDown.Items.Clear()
        Dim products_dict = ProductsMapping.GetproductsDictionary(ANALYSIS_AXIS_ID_VAR, ANALYSIS_AXIS_NAME_VAR)
        For Each key As String In products_dict.Keys
            AddButtonToDropDown(GlobalVariables.ProductsIDDropDown, _
                                key, _
                                products_dict(key))
        Next
        GlobalVariables.ProductsIDDropDown.SelectedItemId = DEFAULT_ANALYSIS_AXIS_ID

        GlobalVariables.AdjustmentIDDropDown.Items.Clear()
        Dim adjustments_dict = AdjustmentsMapping.GetAdjustmentsDictionary(ANALYSIS_AXIS_ID_VAR, ANALYSIS_AXIS_NAME_VAR)
        For Each key As String In adjustments_dict.Keys
            AddButtonToDropDown(GlobalVariables.AdjustmentIDDropDown, _
                                key, _
                                adjustments_dict(key))
        Next
        GlobalVariables.AdjustmentIDDropDown.SelectedItemId = DEFAULT_ANALYSIS_AXIS_ID

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
    Private Sub WSCB_OnAction(sender As Object, Control As IRibbonControl, selectedId As String, selectedIndex As Integer)

        Try
            If Not CurrentGRSControler.associatedWorksheet.Name = selectedId Then
                CurrentGRSControler = GRSControlersDictionary(ctrlsTextWSDictionary(selectedId))
                CurrentGRSControler.UpdateRibbon()
                ctrlsTextWSDictionary(selectedId).Activate()
            End If
        Catch ex As Exception
            GRSControlersDictionary.Remove(ctrlsTextWSDictionary(selectedId))
            ctrlsTextWSDictionary.Remove(selectedId)
            MsgBox("The worksheet has been deleted.")
        End Try

    End Sub

    ' Change in Clients DropDown
    Private Sub ClientsDropDown_OnAction(sender As Object, Control As IRibbonControl, selectedId As String, selectedIndex As Integer) Handles ClientsDropDown.OnAction

        CurrentGRSControler.UpdateAfterAnalysisAxisChanged(selectedId, _
                                                           ProductsDropDown.SelectedItemId, _
                                                           AdjustmentDD.SelectedItemId,
                                                           True)

    End Sub

    ' Change in Products DropDown
    Private Sub ProductsDropDown_OnAction(sender As Object, Control As IRibbonControl, selectedId As String, selectedIndex As Integer) Handles ProductsDropDown.OnAction

        CurrentGRSControler.UpdateAfterAnalysisAxisChanged(ClientsDropDown.SelectedItemId, _
                                                              ProductsDropDown.SelectedItemId, _
                                                              selectedId, _
                                                              True)

    End Sub

    ' Change in Adjustments DropDown
    Private Sub AdjustmentDD_OnAction(sender As Object, Control As IRibbonControl, selectedId As String, selectedIndex As Integer) Handles AdjustmentDD.OnAction

        CurrentGRSControler.UpdateAfterAnalysisAxisChanged(ClientsDropDown.SelectedItemId, _
                                                              ProductsDropDown.SelectedItemId, _
                                                              selectedId, _
                                                              True)

    End Sub

#End Region

#Region "Addin Events"

    Private Sub AddinModule_Finalize(sender As Object, e As EventArgs) Handles MyBase.AddinFinalize
        If GRSControlersDictionary.Count > 0 Then ClearSubmissionMode()
    End Sub


#End Region

#End Region


#Region "Task Panes Launches and call backs"

    Friend Sub InputReportPaneCallBack_ReportCreation()

        '   GlobalVariables.APPS.ScreenUpdating = False
        Dim entity_id As String = Me.InputReportTaskPane.EntitiesTV.SelectedNode.Name
        Dim entity_name As String = Me.InputReportTaskPane.EntitiesTV.SelectedNode.Text
        Dim currency As String = EntitiesMapping.GetEntityCurrency(entity_id)
        Dim currentcell As Excel.Range = WorksheetWrittingFunctions.CreateReceptionWS(entity_name, _
                                                                                       {"Entity", "Currency", "Version"}, _
                                                                                       {entity_name, currency, GlobalVariables.Version_Label.Caption})

        Dim Versions As New Version
        Dim periodlist As List(Of Date) = Versions.GetPeriodsDatesList(GlobalVariables.GLOBALCurrentVersionCode)
        WorksheetWrittingFunctions.InsertInputReportOnWS(currentcell, _
                                                          periodlist, _
                                                          Versions.ReadVersion(GlobalVariables.GLOBALCurrentVersionCode, VERSIONS_TIME_CONFIG_VARIABLE))

        Me.InputReportTaskPane.Hide()
        Me.InputReportTaskPane.Close()
        Versions.Close()
        CExcelFormatting.FormatExcelRange(currentcell, INPUT_FORMAT_CODE, currency, periodlist(0))
        GlobalVariables.APPS.ScreenUpdating = True
        AssociateGRSControler()

    End Sub

#Region "Version Selection"

    Public Sub SetVersion(ByRef version_id As String)

        Me.VersionSelectionTaskPane.SetVersion(version_id)

    End Sub

    Public Sub LaunchVersionSelection()

        If CDbl(GlobalVariables.APPS.Version.Replace(".", ",")) > EXCEL_MIN_VERSION Then  ' 
            GlobalVariables.VersionsSelectionPaneVisible = True
            Me.VersionSelectionTaskPane.Init()
            Me.VersionSelectionTaskPane.Show()
        Else
            ' Implement settings versions for version selection without panes
            Dim VERSELUI As New VersionSelectionUI
            VERSELUI.Show()
        End If

    End Sub

#End Region

#Region "Entity Selection"

    Friend Sub LaunchEntitySelectionTP(ByRef parentObject As Object, Optional ByRef restrictionToInputsEntities As Boolean = False)

        If CDbl(GlobalVariables.APPS.Version.Replace(".", ",")) > EXCEL_MIN_VERSION Then
            GlobalVariables.EntitySelectionPaneVisible = True
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

    ' Add an item to a DropDown Control
    Friend Shared Function AddButtonToDropDown(ByRef DropDownMenu As ADXRibbonDropDown, _
                                                ByVal id As String, _
                                                ByVal name As String) As ADXRibbonItem

        Dim adxRibbonItem As ADXRibbonItem = New AddinExpress.MSO.ADXRibbonItem()
        adxRibbonItem.Caption = name
        adxRibbonItem.Id = id
        adxRibbonItem.ImageTransparentColor = System.Drawing.Color.Transparent
        DropDownMenu.Items.Add(adxRibbonItem)
        Return adxRibbonItem

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

        GlobalVariables.Version_Label.Caption = versionName
        GlobalVariables.Version_label_Sub_Ribbon.Text = versionName
        GlobalVariables.GLOBALCurrentVersionCode = versionCode

    End Sub

#End Region


    Private Sub PPTExportBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean)

        Dim testHTTPUI As New HTTP_test
        testHTTPUI.Show()

    End Sub


  
End Class

