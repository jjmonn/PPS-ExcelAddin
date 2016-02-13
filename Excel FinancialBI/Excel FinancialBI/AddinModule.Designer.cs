namespace FBI
{
    partial class AddinModule
    {
        /// <summary>
        /// Required by designer
        /// </summary>
        private System.ComponentModel.IContainer components;
 
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary>
        /// Required by designer support - do not modify
        /// the following method
        /// </summary>
        private void InitializeComponent()
        {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddinModule));
      this.m_financialbiRibbon = new AddinExpress.MSO.ADXRibbonTab(this.components);
      this.m_connectionGroup = new AddinExpress.MSO.ADXRibbonGroup(this.components);
      this.m_connectionButton = new AddinExpress.MSO.ADXRibbonButton(this.components);
      this.m_mainRibbonImageList = new System.Windows.Forms.ImageList(this.components);
      this.m_versionGroup = new AddinExpress.MSO.ADXRibbonGroup(this.components);
      this.m_versionRibbonButton = new AddinExpress.MSO.ADXRibbonButton(this.components);
      this.m_processRibbonButton = new AddinExpress.MSO.ADXRibbonSplitButton(this.components);
      this.adxRibbonMenu4 = new AddinExpress.MSO.ADXRibbonMenu(this.components);
      this.m_financialProcessRibbonButton = new AddinExpress.MSO.ADXRibbonButton(this.components);
      this.m_RHProcessRibbonButton = new AddinExpress.MSO.ADXRibbonButton(this.components);
      this.m_uploadGroup = new AddinExpress.MSO.ADXRibbonGroup(this.components);
      this.m_snapshotRibbonSplitButton = new AddinExpress.MSO.ADXRibbonSplitButton(this.components);
      this.adxRibbonMenu1 = new AddinExpress.MSO.ADXRibbonMenu(this.components);
      this.m_directoryRibbonButton = new AddinExpress.MSO.ADXRibbonButton(this.components);
      this.m_reportUploadRibbonButton = new AddinExpress.MSO.ADXRibbonButton(this.components);
      this.m_visualizationGroup = new AddinExpress.MSO.ADXRibbonGroup(this.components);
      this.m_CUIRibbonButton = new AddinExpress.MSO.ADXRibbonButton(this.components);
      this.m_submissionsTrackingRibbonButton = new AddinExpress.MSO.ADXRibbonButton(this.components);
      this.m_fbiRibbonButton = new AddinExpress.MSO.ADXRibbonSplitButton(this.components);
      this.adxRibbonMenu2 = new AddinExpress.MSO.ADXRibbonMenu(this.components);
      this.m_fbiBreakLinksRibbonButton = new AddinExpress.MSO.ADXRibbonButton(this.components);
      this.m_refreshRibbonButton = new AddinExpress.MSO.ADXRibbonSplitButton(this.components);
      this.adxRibbonMenu3 = new AddinExpress.MSO.ADXRibbonMenu(this.components);
      this.m_resfreshSelectionRibbonButton = new AddinExpress.MSO.ADXRibbonButton(this.components);
      this.m_refreshWorksheetRibbonButton = new AddinExpress.MSO.ADXRibbonButton(this.components);
      this.m_refreshWorkbookRibbonButton = new AddinExpress.MSO.ADXRibbonButton(this.components);
      this.adxRibbonMenuSeparator1 = new AddinExpress.MSO.ADXRibbonMenuSeparator(this.components);
      this.m_autoRefreshRibbonChackBox = new AddinExpress.MSO.ADXRibbonCheckBox(this.components);
      this.m_configurationGroup = new AddinExpress.MSO.ADXRibbonGroup(this.components);
      this.m_platformManagementButton = new AddinExpress.MSO.ADXRibbonButton(this.components);
      this.m_settingsRibbonButton = new AddinExpress.MSO.ADXRibbonButton(this.components);
      this.adxExcelTaskPanesManager1 = new AddinExpress.XL.ADXExcelTaskPanesManager(this.components);
      this.ConnectionSidePaneItem = new AddinExpress.XL.ADXExcelTaskPanesCollectionItem(this.components);
      this.VersionSelectionSidePaneItem = new AddinExpress.XL.ADXExcelTaskPanesCollectionItem(this.components);
      this.ReportUploadAccountInfoSidePaneItem = new AddinExpress.XL.ADXExcelTaskPanesCollectionItem(this.components);
      this.ReportUploadEntitySelectionSidePaneItem = new AddinExpress.XL.ADXExcelTaskPanesCollectionItem(this.components);
      this.adxExcelAppEvents1 = new AddinExpress.MSO.ADXExcelAppEvents(this.components);
      // 
      // m_financialbiRibbon
      // 
      this.m_financialbiRibbon.Caption = "Financial BI";
      this.m_financialbiRibbon.Controls.Add(this.m_connectionGroup);
      this.m_financialbiRibbon.Controls.Add(this.m_versionGroup);
      this.m_financialbiRibbon.Controls.Add(this.m_uploadGroup);
      this.m_financialbiRibbon.Controls.Add(this.m_visualizationGroup);
      this.m_financialbiRibbon.Controls.Add(this.m_configurationGroup);
      this.m_financialbiRibbon.Id = "adxRibbonTab_363c78cf66514881bd3ac6840928d341";
      this.m_financialbiRibbon.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      // 
      // m_connectionGroup
      // 
      this.m_connectionGroup.Caption = "Connection";
      this.m_connectionGroup.Controls.Add(this.m_connectionButton);
      this.m_connectionGroup.Id = "adxRibbonGroup_6ee62d676b364deb9ef3cf0a499f81d3";
      this.m_connectionGroup.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_connectionGroup.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      // 
      // m_connectionButton
      // 
      this.m_connectionButton.Caption = "Connection";
      this.m_connectionButton.Id = "adxRibbonButton_87d984c0090c4786ab30e0b923514d13";
      this.m_connectionButton.Image = 0;
      this.m_connectionButton.ImageList = this.m_mainRibbonImageList;
      this.m_connectionButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_connectionButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      this.m_connectionButton.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large;
      this.m_connectionButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.m_connectionButton_OnClick);
      // 
      // m_mainRibbonImageList
      // 
      this.m_mainRibbonImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_mainRibbonImageList.ImageStream")));
      this.m_mainRibbonImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.m_mainRibbonImageList.Images.SetKeyName(0, "client_network.ico");
      this.m_mainRibbonImageList.Images.SetKeyName(1, "connected.ico");
      this.m_mainRibbonImageList.Images.SetKeyName(2, "cloud_dark.ico");
      this.m_mainRibbonImageList.Images.SetKeyName(3, "dna.ico");
      this.m_mainRibbonImageList.Images.SetKeyName(4, "ok.ico");
      this.m_mainRibbonImageList.Images.SetKeyName(5, "tablet_computer.ico");
      this.m_mainRibbonImageList.Images.SetKeyName(6, "financial_bi.ico");
      this.m_mainRibbonImageList.Images.SetKeyName(7, "font.ico");
      this.m_mainRibbonImageList.Images.SetKeyName(8, "snapshot 3.0.ico");
      this.m_mainRibbonImageList.Images.SetKeyName(9, "calendar_52.ico");
      this.m_mainRibbonImageList.Images.SetKeyName(10, "rotate_left.ico");
      this.m_mainRibbonImageList.Images.SetKeyName(11, "system-settings-icon.ico");
      this.m_mainRibbonImageList.Images.SetKeyName(12, "spreadsheed_cell.ico");
      this.m_mainRibbonImageList.Images.SetKeyName(13, "selection_refresh.ico");
      this.m_mainRibbonImageList.Images.SetKeyName(14, "Excel Blue 32x32.ico");
      // 
      // m_versionGroup
      // 
      this.m_versionGroup.Caption = "Version";
      this.m_versionGroup.Controls.Add(this.m_versionRibbonButton);
      this.m_versionGroup.Controls.Add(this.m_processRibbonButton);
      this.m_versionGroup.Id = "adxRibbonGroup_c1bf35d451f742078971f5a6282def5f";
      this.m_versionGroup.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_versionGroup.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      // 
      // m_versionRibbonButton
      // 
      this.m_versionRibbonButton.Caption = "Select version";
      this.m_versionRibbonButton.Id = "adxRibbonButton_6cfcb8ebcb194f86a9b5b938a4d5161a";
      this.m_versionRibbonButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_versionRibbonButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      this.m_versionRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.m_versionRibbonButton_OnClick);
      // 
      // m_processRibbonButton
      // 
      this.m_processRibbonButton.Caption = "Process";
      this.m_processRibbonButton.Controls.Add(this.adxRibbonMenu4);
      this.m_processRibbonButton.Id = "adxRibbonSplitButton_3de81472eae0421f91e03dfcd92b0d4f";
      this.m_processRibbonButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_processRibbonButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      // 
      // adxRibbonMenu4
      // 
      this.adxRibbonMenu4.Caption = "adxRibbonMenu4";
      this.adxRibbonMenu4.Controls.Add(this.m_financialProcessRibbonButton);
      this.adxRibbonMenu4.Controls.Add(this.m_RHProcessRibbonButton);
      this.adxRibbonMenu4.Id = "adxRibbonMenu_490233da36544ff39d6369c104d6ac85";
      this.adxRibbonMenu4.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.adxRibbonMenu4.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      // 
      // m_financialProcessRibbonButton
      // 
      this.m_financialProcessRibbonButton.Caption = "Financial";
      this.m_financialProcessRibbonButton.Id = "adxRibbonButton_a77bbbc9cdd344fcb5b7e1651689bb4a";
      this.m_financialProcessRibbonButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_financialProcessRibbonButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      this.m_financialProcessRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.m_financialProcessRibbonButton_OnClick);
      // 
      // m_RHProcessRibbonButton
      // 
      this.m_RHProcessRibbonButton.Caption = "Human ressources";
      this.m_RHProcessRibbonButton.Id = "adxRibbonButton_c5e2cabc5bd44fbc85a297f95569b467";
      this.m_RHProcessRibbonButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_RHProcessRibbonButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      this.m_RHProcessRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.m_RHProcessRibbonButton_OnClick);
      // 
      // m_uploadGroup
      // 
      this.m_uploadGroup.Caption = "Upload";
      this.m_uploadGroup.Controls.Add(this.m_snapshotRibbonSplitButton);
      this.m_uploadGroup.Controls.Add(this.m_reportUploadRibbonButton);
      this.m_uploadGroup.Id = "adxRibbonGroup_dc13586cef694dfebbc092395168a22d";
      this.m_uploadGroup.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_uploadGroup.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      // 
      // m_snapshotRibbonSplitButton
      // 
      this.m_snapshotRibbonSplitButton.Caption = "Snapshot";
      this.m_snapshotRibbonSplitButton.Controls.Add(this.adxRibbonMenu1);
      this.m_snapshotRibbonSplitButton.Id = "adxRibbonSplitButton_63d5590d8056447ba96844f7a66baa10";
      this.m_snapshotRibbonSplitButton.Image = 8;
      this.m_snapshotRibbonSplitButton.ImageList = this.m_mainRibbonImageList;
      this.m_snapshotRibbonSplitButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_snapshotRibbonSplitButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      this.m_snapshotRibbonSplitButton.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large;
      this.m_snapshotRibbonSplitButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.m_snapshotRibbonSplitButton_OnClick);
      // 
      // adxRibbonMenu1
      // 
      this.adxRibbonMenu1.Caption = "adxRibbonMenu1";
      this.adxRibbonMenu1.Controls.Add(this.m_directoryRibbonButton);
      this.adxRibbonMenu1.Id = "adxRibbonMenu_16f054e7735b4e8487ab527f0db53df6";
      this.adxRibbonMenu1.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.adxRibbonMenu1.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      // 
      // m_directoryRibbonButton
      // 
      this.m_directoryRibbonButton.Caption = "Directory Snapshot";
      this.m_directoryRibbonButton.Id = "adxRibbonButton_3c5c89ebdd9a4ea595bbc1e5a46be7ed";
      this.m_directoryRibbonButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_directoryRibbonButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      this.m_directoryRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.m_directoryRibbonButton_OnClick);
      // 
      // m_reportUploadRibbonButton
      // 
      this.m_reportUploadRibbonButton.Caption = "Edition";
      this.m_reportUploadRibbonButton.Id = "adxRibbonButton_f72105c2b2b24035a3fd0c26ed0db1e4";
      this.m_reportUploadRibbonButton.Image = 2;
      this.m_reportUploadRibbonButton.ImageList = this.m_mainRibbonImageList;
      this.m_reportUploadRibbonButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_reportUploadRibbonButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      this.m_reportUploadRibbonButton.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large;
      this.m_reportUploadRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.m_reportUploadRibbonButton_OnClick);
      // 
      // m_visualizationGroup
      // 
      this.m_visualizationGroup.Caption = "Visualization";
      this.m_visualizationGroup.Controls.Add(this.m_CUIRibbonButton);
      this.m_visualizationGroup.Controls.Add(this.m_submissionsTrackingRibbonButton);
      this.m_visualizationGroup.Controls.Add(this.m_fbiRibbonButton);
      this.m_visualizationGroup.Controls.Add(this.m_refreshRibbonButton);
      this.m_visualizationGroup.Id = "adxRibbonGroup_f70db40b70724aebb403638f3fcccbef";
      this.m_visualizationGroup.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_visualizationGroup.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      // 
      // m_CUIRibbonButton
      // 
      this.m_CUIRibbonButton.Caption = "Financials";
      this.m_CUIRibbonButton.Id = "adxRibbonButton_804dd699b80a4aa39f738feb43e6091e";
      this.m_CUIRibbonButton.Image = 5;
      this.m_CUIRibbonButton.ImageList = this.m_mainRibbonImageList;
      this.m_CUIRibbonButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_CUIRibbonButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      this.m_CUIRibbonButton.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large;
      this.m_CUIRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.m_CUIRibbonButton_OnClick);
      // 
      // m_submissionsTrackingRibbonButton
      // 
      this.m_submissionsTrackingRibbonButton.Caption = "Submission tracking";
      this.m_submissionsTrackingRibbonButton.Id = "adxRibbonButton_dddfbfa165464d13bc1086a560e15cc5";
      this.m_submissionsTrackingRibbonButton.Image = 9;
      this.m_submissionsTrackingRibbonButton.ImageList = this.m_mainRibbonImageList;
      this.m_submissionsTrackingRibbonButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_submissionsTrackingRibbonButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      this.m_submissionsTrackingRibbonButton.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large;
      this.m_submissionsTrackingRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.m_submissionsTrackingRibbonButton_OnClick);
      // 
      // m_fbiRibbonButton
      // 
      this.m_fbiRibbonButton.Caption = "GetData()";
      this.m_fbiRibbonButton.Controls.Add(this.adxRibbonMenu2);
      this.m_fbiRibbonButton.Id = "adxRibbonSplitButton_c2ae2d61bb7d44cbb5318a2a9567a3c3";
      this.m_fbiRibbonButton.Image = 6;
      this.m_fbiRibbonButton.ImageList = this.m_mainRibbonImageList;
      this.m_fbiRibbonButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_fbiRibbonButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      this.m_fbiRibbonButton.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large;
      this.m_fbiRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.m_fbiRibbonButton_OnClick);
      // 
      // adxRibbonMenu2
      // 
      this.adxRibbonMenu2.Caption = "adxRibbonMenu2";
      this.adxRibbonMenu2.Controls.Add(this.m_fbiBreakLinksRibbonButton);
      this.adxRibbonMenu2.Id = "adxRibbonMenu_c45468a6272a4805bd2a024c0124d225";
      this.adxRibbonMenu2.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.adxRibbonMenu2.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      // 
      // m_fbiBreakLinksRibbonButton
      // 
      this.m_fbiBreakLinksRibbonButton.Caption = "Break links";
      this.m_fbiBreakLinksRibbonButton.Id = "adxRibbonButton_42d2a242746f4962b9e5764c52ecad18";
      this.m_fbiBreakLinksRibbonButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_fbiBreakLinksRibbonButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      this.m_fbiBreakLinksRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.m_fbiBreakLinksRibbonButton_OnClick);
      // 
      // m_refreshRibbonButton
      // 
      this.m_refreshRibbonButton.Caption = "Refresh";
      this.m_refreshRibbonButton.Controls.Add(this.adxRibbonMenu3);
      this.m_refreshRibbonButton.Id = "adxRibbonSplitButton_3dd1d97d2fc9454a98e0a40e9a9f12ab";
      this.m_refreshRibbonButton.Image = 10;
      this.m_refreshRibbonButton.ImageList = this.m_mainRibbonImageList;
      this.m_refreshRibbonButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_refreshRibbonButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      this.m_refreshRibbonButton.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large;
      this.m_refreshRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.m_refreshRibbonButton_OnClick);
      // 
      // adxRibbonMenu3
      // 
      this.adxRibbonMenu3.Caption = "adxRibbonMenu3";
      this.adxRibbonMenu3.Controls.Add(this.m_resfreshSelectionRibbonButton);
      this.adxRibbonMenu3.Controls.Add(this.m_refreshWorksheetRibbonButton);
      this.adxRibbonMenu3.Controls.Add(this.m_refreshWorkbookRibbonButton);
      this.adxRibbonMenu3.Controls.Add(this.adxRibbonMenuSeparator1);
      this.adxRibbonMenu3.Controls.Add(this.m_autoRefreshRibbonChackBox);
      this.adxRibbonMenu3.Id = "adxRibbonMenu_37135023878040eda8d69e900be0a10a";
      this.adxRibbonMenu3.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.adxRibbonMenu3.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      // 
      // m_resfreshSelectionRibbonButton
      // 
      this.m_resfreshSelectionRibbonButton.Caption = "Refresh selection";
      this.m_resfreshSelectionRibbonButton.Id = "adxRibbonButton_061e1da9ff4f4fa3adaafb8fb9820102";
      this.m_resfreshSelectionRibbonButton.Image = 13;
      this.m_resfreshSelectionRibbonButton.ImageList = this.m_mainRibbonImageList;
      this.m_resfreshSelectionRibbonButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_resfreshSelectionRibbonButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      this.m_resfreshSelectionRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.m_resfreshSelectionRibbonButton_OnClick);
      // 
      // m_refreshWorksheetRibbonButton
      // 
      this.m_refreshWorksheetRibbonButton.Caption = "Refresh Worksheet";
      this.m_refreshWorksheetRibbonButton.Id = "adxRibbonButton_946906a1e9cb4205b4ae870f55aa174e";
      this.m_refreshWorksheetRibbonButton.Image = 12;
      this.m_refreshWorksheetRibbonButton.ImageList = this.m_mainRibbonImageList;
      this.m_refreshWorksheetRibbonButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_refreshWorksheetRibbonButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      this.m_refreshWorksheetRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.m_refreshWorksheetRibbonButton_OnClick);
      // 
      // m_refreshWorkbookRibbonButton
      // 
      this.m_refreshWorkbookRibbonButton.Caption = "Refresh Workbook";
      this.m_refreshWorkbookRibbonButton.Id = "adxRibbonButton_f226d5d027434790a16dbedf9778128a";
      this.m_refreshWorkbookRibbonButton.ImageList = this.m_mainRibbonImageList;
      this.m_refreshWorkbookRibbonButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_refreshWorkbookRibbonButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      this.m_refreshWorkbookRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.m_refreshWorkbookRibbonButton_OnClick);
      // 
      // adxRibbonMenuSeparator1
      // 
      this.adxRibbonMenuSeparator1.Caption = " ";
      this.adxRibbonMenuSeparator1.Id = "adxRibbonMenuSeparator_244998d594e64b43bde8618b197ba5d4";
      this.adxRibbonMenuSeparator1.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      // 
      // m_autoRefreshRibbonChackBox
      // 
      this.m_autoRefreshRibbonChackBox.Caption = "Autorefresh";
      this.m_autoRefreshRibbonChackBox.Id = "adxRibbonCheckBox_986328ffc6074da4baef7fef5a55785c";
      this.m_autoRefreshRibbonChackBox.Pressed = true;
      this.m_autoRefreshRibbonChackBox.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      this.m_autoRefreshRibbonChackBox.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.m_autoRefreshRibbonChackBox_OnClick);
      // 
      // m_configurationGroup
      // 
      this.m_configurationGroup.Caption = "Configuration";
      this.m_configurationGroup.Controls.Add(this.m_platformManagementButton);
      this.m_configurationGroup.Controls.Add(this.m_settingsRibbonButton);
      this.m_configurationGroup.Id = "adxRibbonGroup_311dedcb650f4a449cc73511782bd584";
      this.m_configurationGroup.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_configurationGroup.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      // 
      // m_platformManagementButton
      // 
      this.m_platformManagementButton.Caption = "Platform Management";
      this.m_platformManagementButton.Id = "adxRibbonButton_7046a93cfe02483390fbbd2cbfca24e8";
      this.m_platformManagementButton.Image = 3;
      this.m_platformManagementButton.ImageList = this.m_mainRibbonImageList;
      this.m_platformManagementButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_platformManagementButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      this.m_platformManagementButton.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large;
      this.m_platformManagementButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.m_platformManagementButton_OnClick);
      // 
      // m_settingsRibbonButton
      // 
      this.m_settingsRibbonButton.Caption = "Settings";
      this.m_settingsRibbonButton.Id = "adxRibbonButton_e3563bb1328649b9b86b20f4b46d8f49";
      this.m_settingsRibbonButton.Image = 11;
      this.m_settingsRibbonButton.ImageList = this.m_mainRibbonImageList;
      this.m_settingsRibbonButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.m_settingsRibbonButton.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook;
      this.m_settingsRibbonButton.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large;
      this.m_settingsRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.m_settingsRibbonButton_OnClick);
      // 
      // adxExcelTaskPanesManager1
      // 
      this.adxExcelTaskPanesManager1.Items.Add(this.ConnectionSidePaneItem);
      this.adxExcelTaskPanesManager1.Items.Add(this.VersionSelectionSidePaneItem);
      this.adxExcelTaskPanesManager1.Items.Add(this.ReportUploadAccountInfoSidePaneItem);
      this.adxExcelTaskPanesManager1.Items.Add(this.ReportUploadEntitySelectionSidePaneItem);
      this.adxExcelTaskPanesManager1.SetOwner(this);
      // 
      // ConnectionSidePaneItem
      // 
      this.ConnectionSidePaneItem.AllowedDropPositions = ((AddinExpress.XL.ADXExcelAllowedDropPositions)((AddinExpress.XL.ADXExcelAllowedDropPositions.Right | AddinExpress.XL.ADXExcelAllowedDropPositions.Left)));
      this.ConnectionSidePaneItem.AlwaysShowHeader = true;
      this.ConnectionSidePaneItem.CloseButton = true;
      this.ConnectionSidePaneItem.Position = AddinExpress.XL.ADXExcelTaskPanePosition.Left;
      this.ConnectionSidePaneItem.TaskPaneClassName = "FBI.MVC.View.ConnectionSidePane";
      this.ConnectionSidePaneItem.UseOfficeThemeForBackground = true;
      // 
      // VersionSelectionSidePaneItem
      // 
      this.VersionSelectionSidePaneItem.AllowedDropPositions = ((AddinExpress.XL.ADXExcelAllowedDropPositions)((AddinExpress.XL.ADXExcelAllowedDropPositions.Right | AddinExpress.XL.ADXExcelAllowedDropPositions.Left)));
      this.VersionSelectionSidePaneItem.AlwaysShowHeader = true;
      this.VersionSelectionSidePaneItem.CloseButton = true;
      this.VersionSelectionSidePaneItem.IsDragDropAllowed = true;
      this.VersionSelectionSidePaneItem.Position = AddinExpress.XL.ADXExcelTaskPanePosition.Left;
      this.VersionSelectionSidePaneItem.TaskPaneClassName = "FBI.MVC.View.VersionSelectionPane";
      this.VersionSelectionSidePaneItem.UseOfficeThemeForBackground = true;
      // 
      // ReportUploadAccountInfoSidePaneItem
      // 
      this.ReportUploadAccountInfoSidePaneItem.AllowedDropPositions = ((AddinExpress.XL.ADXExcelAllowedDropPositions)((AddinExpress.XL.ADXExcelAllowedDropPositions.Right | AddinExpress.XL.ADXExcelAllowedDropPositions.Left)));
      this.ReportUploadAccountInfoSidePaneItem.AlwaysShowHeader = true;
      this.ReportUploadAccountInfoSidePaneItem.CloseButton = true;
      this.ReportUploadAccountInfoSidePaneItem.DefaultRegionState = AddinExpress.XL.ADXRegionState.Hidden;
      this.ReportUploadAccountInfoSidePaneItem.Position = AddinExpress.XL.ADXExcelTaskPanePosition.Left;
      this.ReportUploadAccountInfoSidePaneItem.TaskPaneClassName = "FBI.MVC.View.ReportUploadAccountInfoSidePane";
      this.ReportUploadAccountInfoSidePaneItem.UseOfficeThemeForBackground = true;
      // 
      // ReportUploadEntitySelectionSidePaneItem
      // 
      this.ReportUploadEntitySelectionSidePaneItem.AllowedDropPositions = ((AddinExpress.XL.ADXExcelAllowedDropPositions)((AddinExpress.XL.ADXExcelAllowedDropPositions.Right | AddinExpress.XL.ADXExcelAllowedDropPositions.Left)));
      this.ReportUploadEntitySelectionSidePaneItem.AlwaysShowHeader = true;
      this.ReportUploadEntitySelectionSidePaneItem.CloseButton = true;
      this.ReportUploadEntitySelectionSidePaneItem.Position = AddinExpress.XL.ADXExcelTaskPanePosition.Left;
      this.ReportUploadEntitySelectionSidePaneItem.TaskPaneClassName = "FBI.MVC.View.ReportUploadEntitySelectionSidePane";
      this.ReportUploadEntitySelectionSidePaneItem.UseOfficeThemeForBackground = true;
      // 
      // AddinModule
      // 
      this.AddinName = "FinancialBI";
      this.SupportedApps = AddinExpress.MSO.ADXOfficeHostApp.ohaExcel;
      this.AddinInitialize += new AddinExpress.MSO.ADXEvents_EventHandler(this.AddinModule_AddinInitialize);

        }
        #endregion

        private AddinExpress.MSO.ADXRibbonGroup m_connectionGroup;
        private AddinExpress.MSO.ADXRibbonButton m_connectionButton;
        private AddinExpress.MSO.ADXRibbonGroup m_versionGroup;
        private AddinExpress.MSO.ADXRibbonButton m_versionRibbonButton;
        private AddinExpress.MSO.ADXRibbonGroup m_uploadGroup;
        private AddinExpress.MSO.ADXRibbonSplitButton m_snapshotRibbonSplitButton;
        private AddinExpress.MSO.ADXRibbonMenu adxRibbonMenu1;
        private AddinExpress.MSO.ADXRibbonButton m_directoryRibbonButton;
        private AddinExpress.MSO.ADXRibbonGroup m_visualizationGroup;
        private AddinExpress.MSO.ADXRibbonButton m_CUIRibbonButton;
        private System.Windows.Forms.ImageList m_mainRibbonImageList;
        private AddinExpress.MSO.ADXRibbonButton m_submissionsTrackingRibbonButton;
        private AddinExpress.MSO.ADXRibbonSplitButton m_fbiRibbonButton;
        private AddinExpress.MSO.ADXRibbonMenu adxRibbonMenu2;
        private AddinExpress.MSO.ADXRibbonSplitButton m_refreshRibbonButton;
        private AddinExpress.MSO.ADXRibbonMenu adxRibbonMenu3;
        private AddinExpress.MSO.ADXRibbonGroup m_configurationGroup;
        private AddinExpress.MSO.ADXRibbonButton m_platformManagementButton;
        private AddinExpress.MSO.ADXRibbonButton m_settingsRibbonButton;
        private AddinExpress.MSO.ADXRibbonButton m_reportUploadRibbonButton;
        private AddinExpress.MSO.ADXRibbonButton m_fbiBreakLinksRibbonButton;
        private AddinExpress.MSO.ADXRibbonButton m_resfreshSelectionRibbonButton;
        private AddinExpress.MSO.ADXRibbonButton m_refreshWorksheetRibbonButton;
        private AddinExpress.MSO.ADXRibbonButton m_refreshWorkbookRibbonButton;
        private AddinExpress.MSO.ADXRibbonMenuSeparator adxRibbonMenuSeparator1;
        private AddinExpress.MSO.ADXRibbonCheckBox m_autoRefreshRibbonChackBox;
        private AddinExpress.XL.ADXExcelTaskPanesManager adxExcelTaskPanesManager1;
        public AddinExpress.MSO.ADXRibbonTab m_financialbiRibbon;
        public AddinExpress.XL.ADXExcelTaskPanesCollectionItem ConnectionSidePaneItem;
        public AddinExpress.XL.ADXExcelTaskPanesCollectionItem VersionSelectionSidePaneItem;
        public AddinExpress.XL.ADXExcelTaskPanesCollectionItem ReportUploadAccountInfoSidePaneItem;
        private AddinExpress.MSO.ADXExcelAppEvents adxExcelAppEvents1;
        public AddinExpress.XL.ADXExcelTaskPanesCollectionItem ReportUploadEntitySelectionSidePaneItem;
        private AddinExpress.MSO.ADXRibbonSplitButton m_processRibbonButton;
        private AddinExpress.MSO.ADXRibbonMenu adxRibbonMenu4;
        private AddinExpress.MSO.ADXRibbonButton m_financialProcessRibbonButton;
        private AddinExpress.MSO.ADXRibbonButton m_RHProcessRibbonButton;
    }
}

