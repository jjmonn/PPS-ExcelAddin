' ExcelAddinModule1.vb
'
' For PPSBI user defined formula
'
' To do:
'       - security > currently takes public global variables in AddinModule.vb !
'       - -> find a way to have those global variables accessible here and not public
'
'
' Known bugs:
'       - 
'       - 
'
'
' Author: Julien Monnereau
' Last modified: 08/09/2014


Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports AddinExpress.MSO
Imports Excel = Microsoft.Office.Interop.Excel


'Add-in Express Excel Add-in Module
<GuidAttribute("E4BF1423-B62C-42B8-8CE3-E1F58FDF38B4"), _
ProgIdAttribute("PPS.ExcelAddinModule1"), ClassInterface(ClassInterfaceType.AutoDual)> _
Public Class ExcelAddinModule1
    Inherits AddinExpress.MSO.ADXExcelAddinModule
    Public setUpFlag As Boolean

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


#Region "Initialize"


    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer
        InitializeComponent()

    End Sub

    Private Sub ExcelAddinModule1_AddinInitialize(sender As Object, e As EventArgs) Handles MyBase.AddinInitialize

        APPS = Me.HostApplication
        If APPS.COMAddIns.Item("PPS.AddinModule").Object.setupflag = True Then

            InitializeGlobalVariables()
            InitializeComputer()
            setUpFlag = True

        End If

    End Sub

    ' Initialize Global Variables
    Private Function InitializeGlobalVariables() As Boolean

        ConnectioN = APPS.COMAddIns.Item("PPS.AddinModule").Object.GetAddinConnection()
        User_Credential = APPS.COMAddIns.Item("PPS.AddinModule").Object.GetUserCredential()
        Entities_View = APPS.COMAddIns.Item("PPS.AddinModule").Object.GetEntitiesView()
        Version_Label = APPS.COMAddIns.Item("PPS.AddinModule").Object.GetVersionLabel()
        If ConnectioN Is Nothing Then
            Return False
        Else
            Return True
        End If

    End Function

    ' Initialize the DLL computer instance
    Private Sub InitializeComputer()

        GENERICDCGLobalInstance = New GenericSingleEntityDLL3Computer
        UDFCALLBACKINSTANCE = New cPPSBIControl

    End Sub


#End Region

    ' To do
    ' Need for a flag at PPS level raised when model is updated
    ' before PPSBI below computes need to check for update flag
    ' if model has been updated -> reinitialize computerInstance !!!
    Public Function PPSBI(ByRef entity As Object, _
                          ByRef account As Object, _
                          ByRef period As Object, _
                          ByRef currency_ As Object, _
                          ByRef Version As Object, _
                          Optional ByRef filter1 As Object = Nothing, _
                          Optional ByRef filter2 As Object = Nothing, _
                          Optional ByRef filter3 As Object = Nothing, _
                          Optional ByRef filter4 As Object = Nothing, _
                          Optional ByRef filter5 As Object = Nothing, _
                          Optional ByRef filter6 As Object = Nothing, _
                          Optional ByRef filter7 As Object = Nothing, _
                          Optional ByRef filter8 As Object = Nothing, _
                          Optional ByRef filter9 As Object = Nothing, _
                          Optional ByRef filter10 As Object = Nothing, _
                          Optional ByRef filter11 As Object = Nothing) As Object

        If setUpFlag = False Then
            '  Version_Label = APPS.COMAddIns.Item("PPS.AddinModule").Object.GetVersionLabel()
            If UDFCALLBACKINSTANCE Is Nothing Then
                If InitializeGlobalVariables() = True Then
                    InitializeComputer()
                    setUpFlag = True
                End If
            End If

        End If

        If ConnectioN Is Nothing Then
            Return "WAITING FOR CONNECTION"
        End If

        Return UDFCALLBACKINSTANCE.getDataCallBack(entity, _
                                                   account, _
                                                   period, _
                                                   currency_,
                                                   Version, _
                                                   filter1, _
                                                   filter2, _
                                                   filter3, _
                                                   filter4, _
                                                   filter5, _
                                                   filter6, _
                                                   filter7, _
                                                   filter8, _
                                                   filter9, _
                                                   filter10, _
                                                   filter11)


    End Function


End Class

