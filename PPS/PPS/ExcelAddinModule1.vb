Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports AddinExpress.MSO
Imports Excel = Microsoft.Office.Interop.Excel

'Add-in Express Excel Add-in Module
<GuidAttribute("E4BF1423-B62C-42B8-8CE3-E1F58FDF38B4"), _
ProgIdAttribute("PPS.ExcelAddinModule1"), ClassInterface(ClassInterfaceType.AutoDual)> _
Public Class ExcelAddinModule1
    Inherits AddinExpress.MSO.ADXExcelAddinModule
 
#Region " Component Designer generated code. "
    'Required by designer
    Private components As System.ComponentModel.IContainer
 
    'Required by designer - do not modify
    'the following method
    Private Sub InitializeComponent()
        '
        'ExcelAddinModule1
        '

    End Sub
 
#End Region
 
#Region " Add-in Express automatic code "
 
    <ComRegisterFunctionAttribute()> _
    Public Shared Sub AddinRegister(ByVal t As Type)
        AddinExpress.MSO.ADXExcelAddinModule.ADXExcelAddinRegister(t)
    End Sub
 
    <ComUnregisterFunctionAttribute()> _
    Public Shared Sub AddinUnregister(ByVal t As Type)
        AddinExpress.MSO.ADXExcelAddinModule.ADXExcelAddinUnregister(t)
    End Sub
 
#End Region
 
    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer
        InitializeComponent()
        
    End Sub

    Private Sub ExcelAddinModule1_AddinInitialize(sender As Object, e As EventArgs) Handles MyBase.AddinInitialize
        apps = Me.HostApplication
        ConnectioN = OpenConnection()
        ModelingApp = New Excel.Application

    End Sub

    Public Function GetData(entity As Object, account As Object, period As Object, currency_ As Object) As Double

        Return getDataCallBack(entity, account, period, currency_)

    End Function


    Private Sub ExcelAddinModule1_AddinBeginShutdown(sender As Object, e As EventArgs) Handles MyBase.AddinBeginShutdown
        kill(ModelingApp)
        ModelingApp = Nothing
        GLOBALMC = Nothing
    End Sub
End Class

