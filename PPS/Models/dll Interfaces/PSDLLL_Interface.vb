' PSDLLL_Interface.vb
'
' Interface with the Prices Scenarios DLL
'
' To do:
'       - 
'
'
'
' Author: Julien Monnereau
' Last modified: 13/01/2015


Imports System.Runtime.InteropServices
Imports System.Collections.Generic



Friend Class PSDLLL_Interface

#Region "Instance Variables"

    ' Objects
    Private objptr As Integer

    ' Variables
    Private mEntitiesList As String()
    Private mNbentities As Int32
    Private mNbperiods As Int32

    ' Const
    Protected Friend Const INCREMENTAL_REVENUES As String = "incr_rev"
    Protected Friend Const INCREMENTAL_NET_RESULT As String = "incr_NR"
    Protected Friend Const SENSITIVITIES As String = "sensis"

#End Region


#Region "FDLL Functions"

    <DllImport("PSDLL.dll")>
    Private Shared Function CreatePSDLL() As Integer
    End Function

    <DllImport("PSDLL.dll")>
    Private Shared Sub InitPSDLL(ByVal objptr As Integer, _
                                      <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_BSTR)> ByRef index_list() As String, _
                                      ByVal nb_indexes As Integer, _
                                      <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_BSTR)> ByRef entities_list() As String, _
                                      <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_BSTR)> ByRef formulas() As String, _
                                      ByVal nb_entities As Integer, ByVal nb_periods As Integer)
    End Sub

    <DllImport("PSDLL.dll")>
    Private Shared Sub DestroyPSDLL(ByVal objptr As Integer)
    End Sub

    <DllImport("PSDLL.dll")>
    Private Shared Sub RegisterMarketCurvePSDLL(ByVal objptr As Integer, _
                                                     <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_R8)> ByRef inputs_accounts() As Double, _
                                                     <MarshalAs(UnmanagedType.BStr)> ByVal index_id As String)
    End Sub

    <DllImport("PSDLL.dll")>
    Private Shared Sub ComputePSDLL(ByVal objptr As Integer, _
                                    <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_R8)> ByRef volumes() As Double, _
                                    <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_R8)> ByRef base_revenues() As Double, _
                                    <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_R8)> ByRef tax_rates() As Double)
    End Sub

    <DllImport("PSDLL.dll")>
    Private Shared Function GetArraysPSDLL(ByVal objptr As Integer, _
                                          <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_R8)> _
                                          <System.Runtime.InteropServices.Out()> ByRef incr_rev() As Double, _
                                          <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_R8)> _
                                          <System.Runtime.InteropServices.Out()> ByRef incr_net_result() As Double, _
                                          <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_R8)> _
                                          <System.Runtime.InteropServices.Out()> ByRef sensis() As Double) As Integer

    End Function


#End Region


#Region "Interface"


    Protected Friend Sub New(ByRef index_list As String(), _
                             ByRef entities As String(), _
                             ByRef formulas As String(), _
                             ByRef nb_periods As Int32)

        objptr = CreatePSDLL()
        InitPSDLL(objptr, index_list, index_list.Length, entities, formulas, entities.Length, nb_periods)

        mEntitiesList = entities
        mNbentities = entities.Length
        mNbperiods = nb_periods

    End Sub

    Protected Overrides Sub finalize()

        Try
            DestroyPSDLL(objptr)
        Catch ex As Exception
        End Try

    End Sub

    Protected Friend Sub ResgisterIndexMarketPrices(ByRef prices As Double(), _
                                                    ByRef index_id As String)

        RegisterMarketCurvePSDLL(objptr, prices, index_id)

    End Sub

    Protected Friend Sub Compute(ByRef volumes As Double(), _
                                 ByRef base_revenues As Double(), _
                                 ByRef tax_rates As Double())

        ComputePSDLL(objptr, volumes, base_revenues, tax_rates)

    End Sub

    ' Sensitivities Dictionary: (sensitivity_id)(item)(entity_id)(period)
    ' Items are fixed (PSdll): incr_rev, incr_NR, incr_sensi
    Protected Friend Function GetResultsDict() As Dictionary(Of String, Dictionary(Of String, Double()))

        Dim result_dict As New Dictionary(Of String, Dictionary(Of String, Double()))
        Dim incr_rev_dict As New Dictionary(Of String, Double())
        Dim incr_NR_dict As New Dictionary(Of String, Double())
        Dim incr_sensis_dict As New Dictionary(Of String, Double())

        Dim incr_rev, incr_net_result, sensis As Double()
        GetArraysPSDLL(objptr, incr_rev, incr_net_result, sensis)

        Dim index As Int32 = 0
        For i As Int32 = 0 To mNbentities - 1
            Dim incr_rev2(mNbperiods - 1) As Double
            Dim incr_NR2(mNbperiods - 1) As Double
            Dim incr_sensi2(mNbperiods - 1) As Double

            For j As Int32 = 0 To mNbperiods - 1
                incr_rev2(j) = incr_rev(index)
                incr_NR2(j) = incr_net_result(index)
                incr_sensi2(j) = sensis(index)
                index = index + 1
            Next

            incr_rev_dict.Add(mEntitiesList(i), incr_rev2)
            incr_NR_dict.Add(mEntitiesList(i), incr_NR2)
            incr_sensis_dict.Add(mEntitiesList(i), incr_sensi2)
        Next

        result_dict.Add(INCREMENTAL_REVENUES, incr_rev_dict)
        result_dict.Add(INCREMENTAL_NET_RESULT, incr_NR_dict)
        result_dict.Add(SENSITIVITIES, incr_sensis_dict)

        Return result_dict

    End Function

#End Region



End Class
