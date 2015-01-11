Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Windows.Forms
Imports AddinExpress.MSO
Imports Excel = Microsoft.Office.Interop.Excel

'Add-in Express Add-in Module
<GuidAttribute("C5985605-3A21-426D-8DC3-B38EEBDA50C8"), ProgIdAttribute("PPS.AddinModule")> _
Public Class AddinModule
    Inherits AddinExpress.MSO.ADXAddinModule
    Friend WithEvents AdxRibbonButton1 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonGroup2 As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents AdxRibbonGroup1 As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents AdxRibbonTab1 As AddinExpress.MSO.ADXRibbonTab
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents AdxRibbonButton5 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonButton2 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonButton3 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonButton4 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonGroup3 As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents AdxRibbonButton6 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonButton7 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonButton8 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonGroup4 As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents AdxRibbonGroup5 As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents adxExcelEvents As AddinExpress.MSO.ADXExcelAppEvents

#Region " Component Designer generated code. "
    'Required by designer
    Private components As System.ComponentModel.IContainer

    'Required by designer - do not modify
    'the following method
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddinModule))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.AdxRibbonTab1 = New AddinExpress.MSO.ADXRibbonTab(Me.components)
        Me.AdxRibbonGroup1 = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.AdxRibbonButton5 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonGroup2 = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.AdxRibbonButton1 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonButton2 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonButton3 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonButton4 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonGroup3 = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.AdxRibbonButton6 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonButton7 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonButton8 = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.AdxRibbonGroup4 = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.AdxRibbonGroup5 = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.adxExcelEvents = New AddinExpress.MSO.ADXExcelAppEvents(Me.components)
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "refresh (1).png")
        Me.ImageList1.Images.SetKeyName(1, "refresh.png")
        Me.ImageList1.Images.SetKeyName(2, "refresh0.1.png")
        '
        'AdxRibbonTab1
        '
        Me.AdxRibbonTab1.Caption = "PPS Business Insight"
        Me.AdxRibbonTab1.Controls.Add(Me.AdxRibbonGroup1)
        Me.AdxRibbonTab1.Controls.Add(Me.AdxRibbonGroup2)
        Me.AdxRibbonTab1.Controls.Add(Me.AdxRibbonGroup3)
        Me.AdxRibbonTab1.Controls.Add(Me.AdxRibbonGroup4)
        Me.AdxRibbonTab1.Controls.Add(Me.AdxRibbonGroup5)
        Me.AdxRibbonTab1.Id = "adxRibbonTab_b30b165b93e0463887478085c350e723"
        Me.AdxRibbonTab1.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonGroup1
        '
        Me.AdxRibbonGroup1.Caption = "Data Acquisition"
        Me.AdxRibbonGroup1.Controls.Add(Me.AdxRibbonButton5)
        Me.AdxRibbonGroup1.Id = "adxRibbonGroup_6d270a7302274c0bb0cb396921e59e09"
        Me.AdxRibbonGroup1.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonGroup1.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonButton5
        '
        Me.AdxRibbonButton5.Caption = "Snapshot"
        Me.AdxRibbonButton5.Id = "adxRibbonButton_4bc310353ac24cecbc9d51abdb4fd682"
        Me.AdxRibbonButton5.ImageMso = "SmartArtStylesGallery"
        Me.AdxRibbonButton5.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonButton5.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.AdxRibbonButton5.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonGroup2
        '
        Me.AdxRibbonGroup2.Caption = "Download Data"
        Me.AdxRibbonGroup2.Controls.Add(Me.AdxRibbonButton1)
        Me.AdxRibbonGroup2.Controls.Add(Me.AdxRibbonButton2)
        Me.AdxRibbonGroup2.Controls.Add(Me.AdxRibbonButton3)
        Me.AdxRibbonGroup2.Controls.Add(Me.AdxRibbonButton4)
        Me.AdxRibbonGroup2.Id = "adxRibbonGroup_13e7ba6b0acf4975af1793d5cfec00ed"
        Me.AdxRibbonGroup2.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonGroup2.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonButton1
        '
        Me.AdxRibbonButton1.Caption = "Refresh"
        Me.AdxRibbonButton1.Id = "adxRibbonButton_87f8b7409d6b41e19a24024d33bd3085"
        Me.AdxRibbonButton1.Image = 2
        Me.AdxRibbonButton1.ImageList = Me.ImageList1
        Me.AdxRibbonButton1.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonButton1.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.AdxRibbonButton1.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonButton2
        '
        Me.AdxRibbonButton2.Caption = "Download"
        Me.AdxRibbonButton2.Id = "adxRibbonButton_7d5683509a144f39bf95ea6b3db155b9"
        Me.AdxRibbonButton2.ImageMso = "PictureBackgroundRemovalMarkForeground"
        Me.AdxRibbonButton2.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonButton2.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.AdxRibbonButton2.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonButton3
        '
        Me.AdxRibbonButton3.Caption = "Template"
        Me.AdxRibbonButton3.Id = "adxRibbonButton_fa007761116d48808cb59df31cdb45b0"
        Me.AdxRibbonButton3.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonButton3.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.AdxRibbonButton3.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonButton4
        '
        Me.AdxRibbonButton4.Caption = "Edit Template"
        Me.AdxRibbonButton4.Id = "adxRibbonButton_7178e81ea9d24f24aae44cf31f92cf10"
        Me.AdxRibbonButton4.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonButton4.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.AdxRibbonButton4.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonGroup3
        '
        Me.AdxRibbonGroup3.Caption = "Model Management"
        Me.AdxRibbonGroup3.Controls.Add(Me.AdxRibbonButton6)
        Me.AdxRibbonGroup3.Controls.Add(Me.AdxRibbonButton7)
        Me.AdxRibbonGroup3.Controls.Add(Me.AdxRibbonButton8)
        Me.AdxRibbonGroup3.Id = "adxRibbonGroup_472aee773e454c20851d757e92f14553"
        Me.AdxRibbonGroup3.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonGroup3.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonButton6
        '
        Me.AdxRibbonButton6.Caption = "Currencies"
        Me.AdxRibbonButton6.Id = "adxRibbonButton_96722be640da4622838225c71ed313ff"
        Me.AdxRibbonButton6.ImageMso = "AutoSum"
        Me.AdxRibbonButton6.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonButton6.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.AdxRibbonButton6.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonButton7
        '
        Me.AdxRibbonButton7.Caption = "Entities"
        Me.AdxRibbonButton7.Id = "adxRibbonButton_f0f162203bcf4c8d89476065c7be84c7"
        Me.AdxRibbonButton7.ImageMso = "SmartArtOrganizationChartLeftHanging"
        Me.AdxRibbonButton7.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonButton7.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.AdxRibbonButton7.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonButton8
        '
        Me.AdxRibbonButton8.Caption = "Chart of accounts"
        Me.AdxRibbonButton8.Id = "adxRibbonButton_1f4c59e0ebe74522aeec6e1f98f166f7"
        Me.AdxRibbonButton8.ImageMso = "PictureBackgroundRemovalMarkForeground"
        Me.AdxRibbonButton8.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonButton8.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.AdxRibbonButton8.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'AdxRibbonGroup4
        '
        Me.AdxRibbonGroup4.Caption = "New Group"
        Me.AdxRibbonGroup4.Id = "adxRibbonGroup_86622fc1b0e94630a3bbb3f68bdfa8cb"
        Me.AdxRibbonGroup4.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonGroup4.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'AdxRibbonGroup5
        '
        Me.AdxRibbonGroup5.Caption = "Settings"
        Me.AdxRibbonGroup5.Id = "adxRibbonGroup_e6eee948b5d24daa83135f3148b80d3d"
        Me.AdxRibbonGroup5.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.AdxRibbonGroup5.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
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

        apps = Me.HostApplication
        ConnectioN = OpenConnection()

    End Sub





    Private Sub AdxRibbonButton1_onclick(sender As System.Object,
                                         control As AddinExpress.MSO.IRibbonControl,
                                         pressed As System.Boolean) Handles AdxRibbonButton1.OnClick

        '------------------------------------------------------------------------
        ' Refresh callback
        '------------------------------------------------------------------------
        refreshCallBack()

    End Sub


    Private Sub AdxRibbonButton5_onclick(sender As System.Object,
                                         control As AddinExpress.MSO.IRibbonControl,
                                         pressed As System.Boolean) Handles AdxRibbonButton5.OnClick

        '------------------------------------------------------------------------
        ' Snpashot callback
        '------------------------------------------------------------------------
        Dim SSUI As SnapShotUI = New SnapShotUI()
        SSUI.Show()

    End Sub

    Private Sub AdxRibbonButton2_onclick(sender As System.Object,
                                        control As AddinExpress.MSO.IRibbonControl,
                                        pressed As System.Boolean) Handles AdxRibbonButton2.OnClick

        '------------------------------------------------------------------------
        ' Download callback
        '------------------------------------------------------------------------
        Dim DWNUI As DownloadUI = New DownloadUI()
        DWNUI.Show()

    End Sub

    Private Sub AddinModule_AddinBeginShutdown(sender As Object, e As EventArgs) Handles MyBase.AddinBeginShutdown
        If Not ModelingApp Is Nothing Then kill(ModelingApp)
    End Sub


End Class

