'
'
'
'
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 21/12/2014


Imports System.Runtime.InteropServices
Imports System.Collections.Generic


Friend Class FDLL_Interface

#Region "Instance Variables"

    ' Objects
    Private objptr As Integer

    ' Variables
    Private IsModelAlive As Boolean = False

#End Region


#Region "FDLL Functions"

    <DllImport("FDLL.dll")>
    Private Shared Function CreateDll3() As Integer
    End Function

    <DllImport("FDLL.dll")>
    Private Shared Sub DestroyDll3(ByVal objptr As Integer)
    End Sub

    <DllImport("FDLL.dll")>
    Private Shared Function ComputeFdll(ByVal objptr As Integer, _
                                        ByVal nb_periods As Integer, _
                                        <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_BSTR)> ByRef inputs_accounts() As String, _
                                        <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_INT)> ByRef inputs_periods() As Integer, _
                                        <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_INT)> ByRef inputs_values() As Int32, _
                                         ByVal nb_inputs As Integer, _
                                        <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_BSTR)> ByRef constraints_accounts() As String, _
                                        <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_INT)> ByRef constraints_periods() As Integer, _
                                        <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_INT)> ByRef constraints_targets() As Int32, _
                                        ByVal nb_constraints As Integer, _
                                        ByVal dbt_mvt_locked As Integer) As Integer
    End Function

    <DllImport("FDLL.dll")>
    Private Shared Function ReturnAccountArrayFDll(<MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_R8)> _
                                                   <System.Runtime.InteropServices.Out()> ByRef Output() As Double, _
                                                    ByVal objptr As Integer, _
                                                   <MarshalAs(UnmanagedType.BStr)> ByVal account_id As String) As Integer

    End Function


#End Region


#Region "Initialize"

    Friend Sub New()

        objptr = CreateDll3()
        IsModelAlive = True

    End Sub

#End Region


#Region "Interface"

    Friend Function Compute(ByVal inputs_list As String(), _
                            ByVal inputs_periods As Int32(), _
                            ByVal inputs_values As Int32(), _
                            ByVal constraints_list As String(), _
                            ByVal constraints_periods As Int32(), _
                            ByVal constraints_values As Int32(), _
                            ByVal nb_periods As Int32, _
                            ByRef debt_mvt_locked As Int32) As Int32

        Dim nb_inputs = inputs_list.Length
        Dim nb_constraints = constraints_list.Length

        Return ComputeFdll(objptr, _
                            nb_periods, _
                            inputs_list, inputs_periods, inputs_values, _
                            inputs_list.Length, _
                            constraints_list, constraints_periods, constraints_values, _
                            constraints_list.Length, _
                            debt_mvt_locked)

    End Function

    Friend Function GetOutputMatrix(ByRef accounts_id_list) As Dictionary(Of String, Double())

        Dim tmpDict As New Dictionary(Of String, Double())
        For Each account_id In accounts_id_list
            Dim tmpDataArray() As Double
            ' Gérer la possibilité d'un plantage au niveau c++ !!
            ' ne doit pas faire planter tout le programme si un account ne pas pas être retourné
            ' -> si passage en réseau le tout sera retourné en array donc ok
            ReturnAccountArrayFDll(tmpDataArray, objptr, account_id)
            tmpDict.Add(account_id, tmpDataArray)
        Next
        Return tmpDict

    End Function

    Friend Function GetAccountArray(ByRef account_id As String) As Double()

        Dim tmpDataArray() As Double
        ReturnAccountArrayFDll(tmpDataArray, objptr, account_id)
        Return tmpDataArray

    End Function

    Friend Sub DestroyDll()

        If IsModelAlive = True Then
            DestroyDll3(objptr)
            IsModelAlive = False
        End If

    End Sub


#End Region


    Protected Overrides Sub finalize()

        If IsModelAlive = True Then
            DestroyDll3(objptr)
            IsModelAlive = False
        End If

    End Sub



End Class
