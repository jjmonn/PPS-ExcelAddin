Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports AddinExpress.MSO

'Add-in Express Excel Add-in Module
<GuidAttribute("8972945E-7DAA-44E7-8FD9-2FC4E588C1EB"), _
ProgIdAttribute("FinancialBI.FinancialBIExcelFunctions"), ClassInterface(ClassInterfaceType.AutoDual)> _
Public Partial Class FinancialBIExcelFunctions
    Inherits AddinExpress.MSO.ADXExcelAddinModule

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

    Public Function FBI(ByRef p_entityName As Object, _
                        ByRef p_accountName As Object, _
                        ByRef p_period As Object, _
                        ByRef p_currency As Object, _
                        ByRef p_versionName As Object, _
                        ByRef p_clientsFilters As Object, _
                        ByRef p_productsFilters As Object, _
                        ByRef p_adjustmentsFilters As Object, _
                        ByRef p_categoriesFilters As Object) As Object


        If Me.HostApplication.COMAddIns.Item("FinancialBI.AddinModule").Object.ppsbi_refresh_flag = True Then
            Return Me.HostApplication.COMAddIns.Item("FinancialBI.AddinModule").Object.GetPPSBIResult(p_entityName, _
                                                                                                        p_accountName, _
                                                                                                        p_period, _
                                                                                                        p_currency,
                                                                                                        p_versionName, _
                                                                                                        p_clientsFilters, _
                                                                                                        p_productsFilters, _
                                                                                                        p_adjustmentsFilters, _
                                                                                                        p_categoriesFilters)
        Else
            Return "Not connected"
        End If


    End Function


End Class

