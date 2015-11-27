Imports System.Windows.Forms

Partial Public Class AddinModule

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        Application.EnableVisualStyles()

        'This call is required by the Component Designer
        InitializeComponent()

        'Please add any initialization code to the AddinInitialize event handler

    End Sub

    'Component overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        '
        'AddinModule
        '
        Me.AddinName = "FinancialBI"

        Me.SupportedApps = CType((AddinExpress.MSO.ADXOfficeHostApp.ohaExcel),  _
                                  AddinExpress.MSO.ADXOfficeHostApp)

        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddinModule))
        Me.MaintTab = New AddinExpress.MSO.ADXRibbonTab(Me.components)
        Me.ConnectionGroup = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.ConnectionBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.ConnectionIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.AdxRibbonSeparator1 = New AddinExpress.MSO.ADXRibbonSeparator(Me.components)
        Me.VersionBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.NewICOs = New System.Windows.Forms.ImageList(Me.components)
        Me.Addin_Version_label = New AddinExpress.MSO.ADXRibbonLabel(Me.components)
        Me.Addin_rates_version_label = New AddinExpress.MSO.ADXRibbonLabel(Me.components)
        Me.DataUploadGroup = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.UploadBT = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.UplodBT1 = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.WSUplaodBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.WBUplaodBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.CurrentLinkedWSBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.Menu3 = New System.Windows.Forms.ImageList(Me.components)
        Me.EditionMainRibbonBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RefreshBT = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.RefreshMenu = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.RefreshSelectionBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RefreshWorksheetBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RefreshWorkbookBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonMenuSeparator1 = New AddinExpress.MSO.ADXRibbonMenuSeparator(Me.components)
        Me.AutoRefreshBT = New AddinExpress.MSO.ADXRibbonCheckBox(Me.components)
        Me.ComputeGroup = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.ControlingUI2BT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.FunctionDesigner = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.AdxRibbonMenu1 = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.BreakLinksBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.MainTabImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.financialModelingBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.ConfigurationGroup = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.ConfigurationRibbonBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.SettingsBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.FormatButton = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.NewIcosSmall = New System.Windows.Forms.ImageList(Me.components)
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
        Me.AdjustmentDropDown = New AddinExpress.MSO.ADXRibbonDropDown(Me.components)
        Me.ClientsDropDown = New AddinExpress.MSO.ADXRibbonDropDown(Me.components)
        Me.ProductsDropDown = New AddinExpress.MSO.ADXRibbonDropDown(Me.components)
        Me.EditSelectionGroup = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.SubmissionOptionsBT = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.AdxRibbonMenu5 = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.m_reportUploadAccountInfoButton = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.ShowReportBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RefreshInputsBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.EditRangesMenuBT = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.EditRangesMenu = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.SelectAccRangeBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.SelectEntitiesRangeBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.SelectPeriodsRangeBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.m_submissionWorksheetCombobox = New AddinExpress.MSO.ADXRibbonDropDown(Me.components)
        Me.AdxRibbonGroup9 = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.CloseBT = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxExcelTaskPanesManager1 = New AddinExpress.XL.ADXExcelTaskPanesManager(Me.components)
        Me.InputSelectionTaskPaneItem = New AddinExpress.XL.ADXExcelTaskPanesCollectionItem(Me.components)
        Me.VersionSelectionTaskPaneItem = New AddinExpress.XL.ADXExcelTaskPanesCollectionItem(Me.components)
        Me.EntitySelectionTaskPaneItem = New AddinExpress.XL.ADXExcelTaskPanesCollectionItem(Me.components)
        Me.ConnectionTaskPaneItem = New AddinExpress.XL.ADXExcelTaskPanesCollectionItem(Me.components)
        Me.ReportUploadTaskPaneItem = New AddinExpress.XL.ADXExcelTaskPanesCollectionItem(Me.components)
        Me.AdxRibbonLabel1 = New AddinExpress.MSO.ADXRibbonLabel(Me.components)
        Me.AdxRibbonMenu3 = New AddinExpress.MSO.ADXRibbonMenu(Me.components)
        Me.SubmissionControlBT = New AddinExpress.MSO.ADXRibbonSplitButton(Me.components)
        Me.AdxRibbonQuickAccessToolbar1 = New AddinExpress.MSO.ADXRibbonQuickAccessToolbar(Me.components)
        Me.AdxRibbonButton5 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.BindingNavigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
        '
        'MaintTab
        '
        Me.MaintTab.Caption = "Financial BI®"
        Me.MaintTab.Controls.Add(Me.ConnectionGroup)
        Me.MaintTab.Controls.Add(Me.DataUploadGroup)
        Me.MaintTab.Controls.Add(Me.ComputeGroup)
        Me.MaintTab.Controls.Add(Me.ConfigurationGroup)
        Me.MaintTab.Id = "adxRibbonTab_b30b165b93e0463887478085c350e723"
        Me.MaintTab.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'ConnectionGroup
        '
        Me.ConnectionGroup.Caption = "Connection"
        Me.ConnectionGroup.CenterVertically = True
        Me.ConnectionGroup.Controls.Add(Me.ConnectionBT)
        Me.ConnectionGroup.Controls.Add(Me.AdxRibbonSeparator1)
        Me.ConnectionGroup.Controls.Add(Me.VersionBT)
        Me.ConnectionGroup.Controls.Add(Me.Addin_Version_label)
        Me.ConnectionGroup.Controls.Add(Me.Addin_rates_version_label)
        Me.ConnectionGroup.Id = "adxRibbonGroup_89e20fb41d2445e782d9c54beb6faae8"
        Me.ConnectionGroup.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ConnectionGroup.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'ConnectionBT
        '
        Me.ConnectionBT.Caption = "Connection "
        Me.ConnectionBT.Id = "adxRibbonButton_f2e70238179d4fb68a52b7d77fde6971"
        Me.ConnectionBT.Image = 0
        Me.ConnectionBT.ImageList = Me.ConnectionIcons
        Me.ConnectionBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ConnectionBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.ConnectionBT.ScreenTip = "Click to open the connection with Financial BI server (your identification and pa" & _
    "ssword will be required)"
        Me.ConnectionBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'ConnectionIcons
        '
        Me.ConnectionIcons.ImageStream = CType(resources.GetObject("ConnectionIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ConnectionIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.ConnectionIcons.Images.SetKeyName(0, "favicon(4).ico")
        Me.ConnectionIcons.Images.SetKeyName(1, "client_network.ico")
        '
        'AdxRibbonSeparator1
        '
        Me.AdxRibbonSeparator1.Id = "adxRibbonSeparator_efa53406578a435ea76b4e776683ce26"
        Me.AdxRibbonSeparator1.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'VersionBT
        '
        Me.VersionBT.Caption = "Select Version"
        Me.VersionBT.Id = "adxRibbonButton_47a60ea441584fe3b0b975b2829b6ec1"
        Me.VersionBT.ImageList = Me.NewICOs
        Me.VersionBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.VersionBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.VersionBT.ScreenTip = "Click to select the data version you want to work on."
        Me.VersionBT.SuperTip = "Click to select a version"
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
        Me.NewICOs.Images.SetKeyName(30, "arrow_mix.ico")
        Me.NewICOs.Images.SetKeyName(31, "pieces.ico")
        '
        'Addin_Version_label
        '
        Me.Addin_Version_label.Caption = " "
        Me.Addin_Version_label.Id = "adxRibbonLabel_b8b0a822a6f6432fb5ed3a82568b76ea"
        Me.Addin_Version_label.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'Addin_rates_version_label
        '
        Me.Addin_rates_version_label.Caption = " "
        Me.Addin_rates_version_label.Id = "adxRibbonLabel_43aa53308e4d4b3a8e3d7193f8945b6f"
        Me.Addin_rates_version_label.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'DataUploadGroup
        '
        Me.DataUploadGroup.Caption = "Data Upload"
        Me.DataUploadGroup.Controls.Add(Me.UploadBT)
        Me.DataUploadGroup.Controls.Add(Me.EditionMainRibbonBT)
        Me.DataUploadGroup.Controls.Add(Me.RefreshBT)
        Me.DataUploadGroup.Id = "adxRibbonGroup_6d270a7302274c0bb0cb396921e59e09"
        Me.DataUploadGroup.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.DataUploadGroup.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'UploadBT
        '
        Me.UploadBT.Caption = "Snapshot"
        Me.UploadBT.Controls.Add(Me.UplodBT1)
        Me.UploadBT.Id = "adxRibbonSplitButton_d2989ac910ad415381c6cc902b2051e5"
        Me.UploadBT.Image = 10
        Me.UploadBT.ImageList = Me.Menu3
        Me.UploadBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.UploadBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.UploadBT.ScreenTip = "Capture the Worksheet and opens Data Edition"
        Me.UploadBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        Me.UploadBT.SuperTip = "Capture the Worksheet and open the Data Edition"
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
        'Menu3
        '
        Me.Menu3.ImageStream = CType(resources.GetObject("Menu3.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Menu3.TransparentColor = System.Drawing.Color.Transparent
        Me.Menu3.Images.SetKeyName(0, "spreadsheed_cell.ico")
        Me.Menu3.Images.SetKeyName(1, "window_equalizer.ico")
        Me.Menu3.Images.SetKeyName(2, "element_branch2.ico")
        Me.Menu3.Images.SetKeyName(3, "chart_area.ico")
        Me.Menu3.Images.SetKeyName(4, "window_equalizer.ico")
        Me.Menu3.Images.SetKeyName(5, "Financial BI Ico.ico")
        Me.Menu3.Images.SetKeyName(6, "favicon(16).ico")
        Me.Menu3.Images.SetKeyName(7, "Financial BI Green small.ico")
        Me.Menu3.Images.SetKeyName(8, "favicon(19).ico")
        Me.Menu3.Images.SetKeyName(9, "Financial BI Ico.ico")
        Me.Menu3.Images.SetKeyName(10, "snapshot 3.0.ico")
        Me.Menu3.Images.SetKeyName(11, "Export classic green bigger.ico")
        Me.Menu3.Images.SetKeyName(12, "tablet_computer.ico")
        Me.Menu3.Images.SetKeyName(13, "Financial BI Blue.ico")
        Me.Menu3.Images.SetKeyName(14, "ok.ico")
        Me.Menu3.Images.SetKeyName(15, "dna.ico")
        Me.Menu3.Images.SetKeyName(16, "system-settings-icon.ico")
        Me.Menu3.Images.SetKeyName(17, "font.ico")
        Me.Menu3.Images.SetKeyName(18, "cloud_dark.ico")
        '
        'EditionMainRibbonBT
        '
        Me.EditionMainRibbonBT.Caption = "Edition"
        Me.EditionMainRibbonBT.Id = "adxRibbonButton_617536f4e5cc44f795a1688e24afdeb8"
        Me.EditionMainRibbonBT.Image = 18
        Me.EditionMainRibbonBT.ImageList = Me.Menu3
        Me.EditionMainRibbonBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.EditionMainRibbonBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.EditionMainRibbonBT.ScreenTip = "Open the data edition mode to submit data on the cloud"
        Me.EditionMainRibbonBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'RefreshBT
        '
        Me.RefreshBT.Caption = "Refresh"
        Me.RefreshBT.Controls.Add(Me.RefreshMenu)
        Me.RefreshBT.Id = "adxRibbonSplitButton_8aab3e36ecdf4fdfbd62b6bc29af40ee"
        Me.RefreshBT.Image = 11
        Me.RefreshBT.ImageList = Me.Menu3
        Me.RefreshBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RefreshBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RefreshBT.ScreenTip = "Click to Refresh Data in Financial BI Formulas"
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
        'ComputeGroup
        '
        Me.ComputeGroup.Caption = " "
        Me.ComputeGroup.Controls.Add(Me.ControlingUI2BT)
        Me.ComputeGroup.Controls.Add(Me.FunctionDesigner)
        Me.ComputeGroup.Controls.Add(Me.financialModelingBT)
        Me.ComputeGroup.Id = "adxRibbonGroup_13e7ba6b0acf4975af1793d5cfec00ed"
        Me.ComputeGroup.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ComputeGroup.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'ControlingUI2BT
        '
        Me.ControlingUI2BT.Caption = "Financials"
        Me.ControlingUI2BT.Id = "adxRibbonButton_7d5683509a144f39bf95ea6b3db155b9"
        Me.ControlingUI2BT.Image = 12
        Me.ControlingUI2BT.ImageList = Me.Menu3
        Me.ControlingUI2BT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ControlingUI2BT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.ControlingUI2BT.ScreenTip = resources.GetString("ControlingUI2BT.ScreenTip")
        Me.ControlingUI2BT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        Me.ControlingUI2BT.SuperTip = "General Data Crunching interface"
        '
        'FunctionDesigner
        '
        Me.FunctionDesigner.Caption = "PPSBI"
        Me.FunctionDesigner.Controls.Add(Me.AdxRibbonMenu1)
        Me.FunctionDesigner.Id = "adxRibbonSplitButton_5986acbd4f414054bdd1177565ae2cf3"
        Me.FunctionDesigner.Image = 13
        Me.FunctionDesigner.ImageList = Me.Menu3
        Me.FunctionDesigner.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.FunctionDesigner.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.FunctionDesigner.ScreenTip = resources.GetString("FunctionDesigner.ScreenTip")
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
        'financialModelingBT
        '
        Me.financialModelingBT.Caption = "Financial Modeling"
        Me.financialModelingBT.Id = "adxRibbonButton_92cac61b68be42c19c331fc2988b85c5"
        Me.financialModelingBT.Image = 3
        Me.financialModelingBT.ImageList = Me.Menu3
        Me.financialModelingBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.financialModelingBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.financialModelingBT.ScreenTip = resources.GetString("financialModelingBT.ScreenTip")
        Me.financialModelingBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'ConfigurationGroup
        '
        Me.ConfigurationGroup.Caption = "Configuration"
        Me.ConfigurationGroup.Controls.Add(Me.ConfigurationRibbonBT)
        Me.ConfigurationGroup.Controls.Add(Me.SettingsBT)
        Me.ConfigurationGroup.Controls.Add(Me.FormatButton)
        Me.ConfigurationGroup.Id = "adxRibbonGroup_472aee773e454c20851d757e92f14553"
        Me.ConfigurationGroup.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ConfigurationGroup.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'ConfigurationRibbonBT
        '
        Me.ConfigurationRibbonBT.Caption = "Configuration"
        Me.ConfigurationRibbonBT.Id = "adxRibbonButton_d4cfb8ca7c6d487b9892703b68ee0fce"
        Me.ConfigurationRibbonBT.Image = 15
        Me.ConfigurationRibbonBT.ImageList = Me.Menu3
        Me.ConfigurationRibbonBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ConfigurationRibbonBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.ConfigurationRibbonBT.ScreenTip = "Click to open your Financial BI configuration (Operational and Financials Account" & _
    "s, Entities Hierarchy, etc.)"
        Me.ConfigurationRibbonBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'SettingsBT
        '
        Me.SettingsBT.Caption = "Settings"
        Me.SettingsBT.Id = "adxRibbonButton_aa28ec782b5541edb1482374e14ceaa6"
        Me.SettingsBT.Image = 16
        Me.SettingsBT.ImageList = Me.Menu3
        Me.SettingsBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.SettingsBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.SettingsBT.ScreenTip = "Click to open the Setting Interface"
        Me.SettingsBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'FormatButton
        '
        Me.FormatButton.Caption = "Format"
        Me.FormatButton.Id = "adxRibbonButton_0609b73e6104420c9944e6db704cb0e9"
        Me.FormatButton.Image = 17
        Me.FormatButton.ImageList = Me.Menu3
        Me.FormatButton.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.FormatButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.FormatButton.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'NewIcosSmall
        '
        Me.NewIcosSmall.ImageStream = CType(resources.GetObject("NewIcosSmall.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.NewIcosSmall.TransparentColor = System.Drawing.Color.Transparent
        Me.NewIcosSmall.Images.SetKeyName(0, "university.ico")
        Me.NewIcosSmall.Images.SetKeyName(1, "tables.ico")
        Me.NewIcosSmall.Images.SetKeyName(2, "element_branch2.ico")
        '
        'SubmissionRibbonIL
        '
        Me.SubmissionRibbonIL.ImageStream = CType(resources.GetObject("SubmissionRibbonIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.SubmissionRibbonIL.TransparentColor = System.Drawing.Color.Transparent
        Me.SubmissionRibbonIL.Images.SetKeyName(0, "favicon(137).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(1, "favicon(186).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(2, "favicon(152).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(3, "favicon(151).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(4, "favicon(149).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(5, "favicon(188).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(6, "favicon(196).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(7, "favicon(180).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(8, "favicon(185).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(9, "favicon(195).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(10, "favicon(197).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(11, "refresh 3.ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(12, "favicon(199).ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(13, "imageres_89.ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(14, "door_exit.ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(15, "spreadsheed.ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(16, "currency_euro.ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(17, "elements_branch.ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(18, "barcode.ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(19, "element_branch2.ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(20, "users_relation.ico")
        Me.SubmissionRibbonIL.Images.SetKeyName(21, "breakpoints.ico")
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
        Me.SubmissionModeRibbon.Caption = "Financial BI® Submission"
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
        Me.AutoComitBT.Image = 5
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
        Me.CancelBT2.Image = 13
        Me.CancelBT2.ImageList = Me.SubmissionRibbonIL
        Me.CancelBT2.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.CancelBT2.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
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
        Me.StateSelectionGroup.Controls.Add(Me.AdjustmentDropDown)
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
        Me.entityEditBT.Image = 19
        Me.entityEditBT.ImageList = Me.SubmissionRibbonIL
        Me.entityEditBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.entityEditBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonButton1
        '
        Me.AdxRibbonButton1.Caption = "Currency"
        Me.AdxRibbonButton1.Id = "adxRibbonButton_761717ad648d4c9e9bcf165ae54c587a"
        Me.AdxRibbonButton1.Image = 16
        Me.AdxRibbonButton1.ImageList = Me.SubmissionRibbonIL
        Me.AdxRibbonButton1.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonButton1.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'VersionBT2
        '
        Me.VersionBT2.Caption = "Version"
        Me.VersionBT2.Id = "adxRibbonButton_2a8644b2388844f9aa05d2c0c48d6969"
        Me.VersionBT2.Image = 21
        Me.VersionBT2.ImageList = Me.SubmissionRibbonIL
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
        Me.AdxRibbonButton2.Image = 15
        Me.AdxRibbonButton2.ImageList = Me.SubmissionRibbonIL
        Me.AdxRibbonButton2.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonButton2.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonButton3
        '
        Me.AdxRibbonButton3.Caption = "Client"
        Me.AdxRibbonButton3.Id = "adxRibbonButton_f4417fc36c7146838b067b42e3c2ce9c"
        Me.AdxRibbonButton3.Image = 20
        Me.AdxRibbonButton3.ImageList = Me.SubmissionRibbonIL
        Me.AdxRibbonButton3.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonButton3.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonButton4
        '
        Me.AdxRibbonButton4.Caption = "Product"
        Me.AdxRibbonButton4.Id = "adxRibbonButton_e6a542c796824b86acbcf89f89d6ae83"
        Me.AdxRibbonButton4.Image = 18
        Me.AdxRibbonButton4.ImageList = Me.SubmissionRibbonIL
        Me.AdxRibbonButton4.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonButton4.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdjustmentDropDown
        '
        Me.AdjustmentDropDown.Caption = " "
        Me.AdjustmentDropDown.Id = "adxRibbonDropDown_3f4be5b43d974d048c6f5ea42f91efd4"
        Me.AdjustmentDropDown.ImageList = Me.NewICOs
        Me.AdjustmentDropDown.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdjustmentDropDown.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.AdjustmentDropDown.SelectedItemId = "1"
        '
        'ClientsDropDown
        '
        Me.ClientsDropDown.Caption = " "
        Me.ClientsDropDown.Id = "adxRibbonDropDown_432cc9be8ce74aeebdf0fc7c1c9a0f81"
        Me.ClientsDropDown.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ClientsDropDown.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.ClientsDropDown.SelectedItemId = "1"
        '
        'ProductsDropDown
        '
        Me.ProductsDropDown.Caption = " "
        Me.ProductsDropDown.Id = "adxRibbonDropDown_dcb69c26dbd748ba85a3a85aa9199939"
        Me.ProductsDropDown.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ProductsDropDown.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.ProductsDropDown.SelectedItemId = "1"
        '
        'EditSelectionGroup
        '
        Me.EditSelectionGroup.Caption = " Settings"
        Me.EditSelectionGroup.Controls.Add(Me.SubmissionOptionsBT)
        Me.EditSelectionGroup.Controls.Add(Me.m_submissionWorksheetCombobox)
        Me.EditSelectionGroup.Id = "adxRibbonGroup_845aac06fe0b43ffa1a5ab1c8cf56d7a"
        Me.EditSelectionGroup.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.EditSelectionGroup.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'SubmissionOptionsBT
        '
        Me.SubmissionOptionsBT.Caption = " Options"
        Me.SubmissionOptionsBT.Controls.Add(Me.AdxRibbonMenu5)
        Me.SubmissionOptionsBT.Id = "adxRibbonSplitButton_ed37a10f5b0a4990ad8f167982663559"
        Me.SubmissionOptionsBT.Image = 16
        Me.SubmissionOptionsBT.ImageList = Me.Menu3
        Me.SubmissionOptionsBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.SubmissionOptionsBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.SubmissionOptionsBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonMenu5
        '
        Me.AdxRibbonMenu5.Caption = "AdxRibbonMenu5"
        Me.AdxRibbonMenu5.Controls.Add(Me.m_reportUploadAccountInfoButton)
        Me.AdxRibbonMenu5.Controls.Add(Me.ShowReportBT)
        Me.AdxRibbonMenu5.Controls.Add(Me.RefreshInputsBT)
        Me.AdxRibbonMenu5.Controls.Add(Me.EditRangesMenuBT)
        Me.AdxRibbonMenu5.Id = "adxRibbonMenu_22c008855d864379ba85629a938602dc"
        Me.AdxRibbonMenu5.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonMenu5.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'm_reportUploadAccountInfoButton
        '
        Me.m_reportUploadAccountInfoButton.Caption = "Display Account information side pane"
        Me.m_reportUploadAccountInfoButton.Id = "adxRibbonButton_d6ccc3789816424192506d30e6008981"
        Me.m_reportUploadAccountInfoButton.Image = 8
        Me.m_reportUploadAccountInfoButton.ImageList = Me.SubmissionRibbonIL
        Me.m_reportUploadAccountInfoButton.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.m_reportUploadAccountInfoButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'ShowReportBT
        '
        Me.ShowReportBT.Caption = "Display Report"
        Me.ShowReportBT.Id = "adxRibbonButton_85590099a4f24030b2944f997d3926c9"
        Me.ShowReportBT.Image = 6
        Me.ShowReportBT.ImageList = Me.SubmissionRibbonIL
        Me.ShowReportBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ShowReportBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.ShowReportBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        Me.ShowReportBT.Visible = False
        '
        'RefreshInputsBT
        '
        Me.RefreshInputsBT.Caption = "Refresh Inputs"
        Me.RefreshInputsBT.Id = "adxRibbonButton_88ae883ce9de414bacaf9f25f558dffc"
        Me.RefreshInputsBT.Image = 11
        Me.RefreshInputsBT.ImageList = Me.SubmissionRibbonIL
        Me.RefreshInputsBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RefreshInputsBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'EditRangesMenuBT
        '
        Me.EditRangesMenuBT.Caption = "Edit Input Ranges"
        Me.EditRangesMenuBT.Controls.Add(Me.EditRangesMenu)
        Me.EditRangesMenuBT.Id = "adxRibbonSplitButton_ce463d91e7174578abd269d924569f1d"
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
        Me.SelectAccRangeBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.SelectAccRangeBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'SelectEntitiesRangeBT
        '
        Me.SelectEntitiesRangeBT.Caption = "Select Entities Range"
        Me.SelectEntitiesRangeBT.Id = "adxRibbonButton_d24e5ee960704f1e9ca2612d5eebfa53"
        Me.SelectEntitiesRangeBT.ImageList = Me.SubmissionRibbonIL
        Me.SelectEntitiesRangeBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.SelectEntitiesRangeBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'SelectPeriodsRangeBT
        '
        Me.SelectPeriodsRangeBT.Caption = "Select Periods Range"
        Me.SelectPeriodsRangeBT.Id = "adxRibbonButton_ab9e3107c41a4638be170f1f98a20b70"
        Me.SelectPeriodsRangeBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.SelectPeriodsRangeBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'm_submissionWorksheetCombobox
        '
        Me.m_submissionWorksheetCombobox.Caption = "Excel Worksheet"
        Me.m_submissionWorksheetCombobox.Id = "adxRibbonDropDown_051d087fb85b425c947f7c3a0fe28700"
        Me.m_submissionWorksheetCombobox.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.m_submissionWorksheetCombobox.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.m_submissionWorksheetCombobox.SelectedItemId = "1"
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
        Me.CloseBT.Caption = "Close Edition Mode"
        Me.CloseBT.Id = "adxRibbonButton_f7a01388d2f243fc85709ca59b1af989"
        Me.CloseBT.Image = 14
        Me.CloseBT.ImageList = Me.SubmissionRibbonIL
        Me.CloseBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.CloseBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.CloseBT.ScreenTip = "Close the current Entity Editor"
        Me.CloseBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxExcelTaskPanesManager1
        '
        Me.AdxExcelTaskPanesManager1.Items.Add(Me.InputSelectionTaskPaneItem)
        Me.AdxExcelTaskPanesManager1.Items.Add(Me.VersionSelectionTaskPaneItem)
        Me.AdxExcelTaskPanesManager1.Items.Add(Me.EntitySelectionTaskPaneItem)
        Me.AdxExcelTaskPanesManager1.Items.Add(Me.ConnectionTaskPaneItem)
        Me.AdxExcelTaskPanesManager1.Items.Add(Me.ReportUploadTaskPaneItem)
        Me.AdxExcelTaskPanesManager1.SetOwner(Me)
        '
        'InputSelectionTaskPaneItem
        '
        Me.InputSelectionTaskPaneItem.AllowedDropPositions = CType((((AddinExpress.XL.ADXExcelAllowedDropPositions.Top Or AddinExpress.XL.ADXExcelAllowedDropPositions.Bottom) _
            Or AddinExpress.XL.ADXExcelAllowedDropPositions.Right) _
            Or AddinExpress.XL.ADXExcelAllowedDropPositions.Left), AddinExpress.XL.ADXExcelAllowedDropPositions)
        Me.InputSelectionTaskPaneItem.AlwaysShowHeader = True
        Me.InputSelectionTaskPaneItem.CloseButton = True
        Me.InputSelectionTaskPaneItem.Position = AddinExpress.XL.ADXExcelTaskPanePosition.Left
        Me.InputSelectionTaskPaneItem.TaskPaneClassName = "InputSelectionPane"
        Me.InputSelectionTaskPaneItem.UseOfficeThemeForBackground = True
        '
        'VersionSelectionTaskPaneItem
        '
        Me.VersionSelectionTaskPaneItem.AlwaysShowHeader = True
        Me.VersionSelectionTaskPaneItem.CloseButton = True
        Me.VersionSelectionTaskPaneItem.Position = AddinExpress.XL.ADXExcelTaskPanePosition.Right
        Me.VersionSelectionTaskPaneItem.TaskPaneClassName = "VersionSelectionPane"
        Me.VersionSelectionTaskPaneItem.UseOfficeThemeForBackground = True
        '
        'EntitySelectionTaskPaneItem
        '
        Me.EntitySelectionTaskPaneItem.AlwaysShowHeader = True
        Me.EntitySelectionTaskPaneItem.Position = AddinExpress.XL.ADXExcelTaskPanePosition.Right
        Me.EntitySelectionTaskPaneItem.TaskPaneClassName = "EntitySelectionTP"
        Me.EntitySelectionTaskPaneItem.UseOfficeThemeForBackground = True
        '
        'ConnectionTaskPaneItem
        '
        Me.ConnectionTaskPaneItem.AllowedDropPositions = CType((((AddinExpress.XL.ADXExcelAllowedDropPositions.Top Or AddinExpress.XL.ADXExcelAllowedDropPositions.Bottom) _
            Or AddinExpress.XL.ADXExcelAllowedDropPositions.Right) _
            Or AddinExpress.XL.ADXExcelAllowedDropPositions.Left), AddinExpress.XL.ADXExcelAllowedDropPositions)
        Me.ConnectionTaskPaneItem.AlwaysShowHeader = True
        Me.ConnectionTaskPaneItem.CloseButton = True
        Me.ConnectionTaskPaneItem.Position = AddinExpress.XL.ADXExcelTaskPanePosition.Left
        Me.ConnectionTaskPaneItem.TaskPaneClassName = "ConnectionTP"
        Me.ConnectionTaskPaneItem.UseOfficeThemeForBackground = True
        '
        'ReportUploadTaskPaneItem
        '
        Me.ReportUploadTaskPaneItem.AllowedDropPositions = CType((((AddinExpress.XL.ADXExcelAllowedDropPositions.Top Or AddinExpress.XL.ADXExcelAllowedDropPositions.Bottom) _
            Or AddinExpress.XL.ADXExcelAllowedDropPositions.Right) _
            Or AddinExpress.XL.ADXExcelAllowedDropPositions.Left), AddinExpress.XL.ADXExcelAllowedDropPositions)
        Me.ReportUploadTaskPaneItem.AlwaysShowHeader = True
        Me.ReportUploadTaskPaneItem.CloseButton = True
        Me.ReportUploadTaskPaneItem.Position = AddinExpress.XL.ADXExcelTaskPanePosition.Right
        Me.ReportUploadTaskPaneItem.TaskPaneClassName = "ReportUploadSidePane"
        Me.ReportUploadTaskPaneItem.UseOfficeThemeForBackground = True
        '
        'AdxRibbonLabel1
        '
        Me.AdxRibbonLabel1.Caption = "Associated with"
        Me.AdxRibbonLabel1.Id = "adxRibbonLabel_f8272bc6694448f6955f882ef772da9e"
        Me.AdxRibbonLabel1.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonMenu3
        '
        Me.AdxRibbonMenu3.Caption = "AdxRibbonMenu3"
        Me.AdxRibbonMenu3.Id = "adxRibbonMenu_d792f3eb075e45e5b8f825e6af002b3c"
        Me.AdxRibbonMenu3.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonMenu3.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'SubmissionControlBT
        '
        Me.SubmissionControlBT.Caption = "Controls"
        Me.SubmissionControlBT.Controls.Add(Me.AdxRibbonMenu3)
        Me.SubmissionControlBT.Id = "adxRibbonSplitButton_c948ce7f779a4f6cbe647f9de2d15b60"
        Me.SubmissionControlBT.ImageList = Me.Menu3
        Me.SubmissionControlBT.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.SubmissionControlBT.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.SubmissionControlBT.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonQuickAccessToolbar1
        '
        Me.AdxRibbonQuickAccessToolbar1.Controls.Add(Me.AdxRibbonButton5)
        Me.AdxRibbonQuickAccessToolbar1.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonButton5
        '
        Me.AdxRibbonButton5.Caption = "AdxRibbonButton5"
        Me.AdxRibbonButton5.Id = "adxRibbonButton_e7993e8e71f646d7a54ec7540bff2ee8"
        Me.AdxRibbonButton5.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonButton5.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.AddNewItem = Me.BindingNavigatorAddNewItem
        Me.BindingNavigator1.CountItem = Me.BindingNavigatorCountItem
        Me.BindingNavigator1.DeleteItem = Me.BindingNavigatorDeleteItem
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorAddNewItem, Me.BindingNavigatorDeleteItem})
        Me.BindingNavigator1.Location = New System.Drawing.Point(0, 0)
        Me.BindingNavigator1.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.BindingNavigator1.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.BindingNavigator1.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.BindingNavigator1.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.BindingNavigator1.Name = "BindingNavigator1"
        Me.BindingNavigator1.PositionItem = Me.BindingNavigatorPositionItem
        Me.BindingNavigator1.Size = New System.Drawing.Size(100, 25)
        Me.BindingNavigator1.TabIndex = 0
        Me.BindingNavigator1.Text = "BindingNavigator1"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Position"
        Me.BindingNavigatorPositionItem.AutoSize = False
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 23)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "Current position"
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(35, 15)
        Me.BindingNavigatorCountItem.Text = "of {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 6)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 20)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 20)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 6)
        '
        'BindingNavigatorAddNewItem
        '
        Me.BindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorAddNewItem.Image = CType(resources.GetObject("BindingNavigatorAddNewItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorAddNewItem.Name = "BindingNavigatorAddNewItem"
        Me.BindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorAddNewItem.Size = New System.Drawing.Size(23, 20)
        Me.BindingNavigatorAddNewItem.Text = "Add new"
        '
        'BindingNavigatorDeleteItem
        '
        Me.BindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorDeleteItem.Image = CType(resources.GetObject("BindingNavigatorDeleteItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorDeleteItem.Name = "BindingNavigatorDeleteItem"
        Me.BindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorDeleteItem.Size = New System.Drawing.Size(23, 20)
        Me.BindingNavigatorDeleteItem.Text = "Delete"
        '
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()

    End Sub

End Class

