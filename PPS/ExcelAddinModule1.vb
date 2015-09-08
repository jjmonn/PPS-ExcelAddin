﻿' ExcelAddinModule1.vb
'
' For PPSBI user defined formula
'
' To do:
'       - security > currently takes public global variables in AddinModule.vb !
'       - Allow text without "'" in formula inputs
'       - -> find a way to have those global variables accessible here and not public
'
'
' Known bugs:
'       - 
'       - 
'
'
' Author: Julien Monnereau
' Last modified: 08/09/2015


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

        GlobalVariables.APPS = Me.HostApplication
        If GlobalVariables.apps.COMAddIns.Item("PPS.AddinModule").Object.setupflag = True Then
             setUpFlag = True
        End If

    End Sub

    Private Sub InitializeComputer()

        GlobalVariables.GlobalPPSBIController = New PPSBIController

    End Sub

#End Region

    Public Function PPSBI(ByRef entity As Object, _
                        ByRef account As Object, _
                        ByRef period As Object, _
                        ByRef currency As Object, _
                        ByRef version As Object, _
                        ByRef clients_filters As Object, _
                        ByRef products_filters As Object, _
                        ByRef adjustments_filters As Object, _
                        ByRef filters As Object) As Object

        If GlobalVariables.APPS.COMAddIns.Item("PPS.AddinModule").Object.ppsbi_refresh_flag = True Then
            If setUpFlag = False Then
                If GlobalVariables.GlobalPPSBIController Is Nothing Then
                    InitializeComputer()
                    setUpFlag = True
                End If
            End If

            If GlobalVariables.AuthenticationFlag = False Then
                Return "Not connected"
            End If
            Return GlobalVariables.GlobalPPSBIController.GetDataCallBack(entity, _
                                                                       account, _
                                                                       period, _
                                                                       currency,
                                                                       version, _
                                                                       clients_filters, _
                                                                       products_filters, _
                                                                       adjustments_filters, _
                                                                       filters)
        End If
        Return "Not connected"

    End Function


End Class

