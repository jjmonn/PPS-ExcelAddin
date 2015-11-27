'
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 06/01/2015


Imports System.Collections
Imports System.Collections.Generic



Friend Class SubmissionControlModel


#Region "Instance Variables"

    ' Variables
    Friend controls_dic As Dictionary(Of String, Hashtable)
    Private periods_list As List(Of Int32)
    Private data_dic As Dictionary(Of String, Double())


#End Region


#Region "Initialize"

    Friend Sub New()

        '     Dim Controls As New Control
        '  controls_dic = Controls.ReadAll

    End Sub

#End Region


#Region "Interface"

    Protected Friend Function CheckSubmission(ByRef input_periods_list As List(Of Int32), _
                                              ByRef input_data_dic As Dictionary(Of String, Double())) As List(Of String)

        Dim successful_controls As New List(Of String)
        periods_list = input_periods_list
        data_dic = input_data_dic

        For Each control_id In controls_dic.Keys
            If CheckControl(controls_dic(control_id)) = True Then successful_controls.Add(control_id)
        Next
        Return successful_controls

    End Function


#End Region


    Private Function CheckControl(ByVal controlHT As Hashtable) As Boolean

        Dim left_side, right_side As Double
        For j As Int32 = 0 To periods_list.Count - 1
            If Not IsNumeric(controlHT(CONTROL_ITEM1_VARIABLE)) Then
                left_side = Math.Round(data_dic(controlHT(CONTROL_ITEM1_VARIABLE))(j), 2)
            Else
                left_side = controlHT(CONTROL_ITEM1_VARIABLE)
            End If

            If Not IsNumeric(controlHT(CONTROL_ITEM2_VARIABLE)) Then
                Select Case controlHT(CONTROL_PERIOD_OPTION_VARIABLE)
                    Case CONTROLS_PERIOD_OPTION : right_side = Math.Round(data_dic(controlHT(CONTROL_ITEM2_VARIABLE))(j), 2)
                    Case CONTROLS_SUM_PERIOD_OPTION
                        right_side = 0
                        For period_index As Int32 = j To periods_list.Count - 1
                            right_side = right_side + Math.Round(data_dic(controlHT(CONTROL_ITEM2_VARIABLE))(period_index), 2)
                        Next
                End Select
            Else
                right_side = controlHT(CONTROL_ITEM2_VARIABLE)
            End If

            Select Case controlHT(CONTROL_OPERATOR_ID_VARIABLE)
                Case "IRT_EQ" : If Not (left_side = right_side) Then Return False
                Case "IRT_GQ" : If Not (left_side >= right_side) Then Return False
                Case "IRT_GR" : If Not (left_side > right_side) Then Return False
                Case "IRT_LE" : If Not (left_side < right_side) Then Return False
                Case "IRT_LQ" : If Not (left_side <= right_side) Then Return False
                Case "IRT_NQ" : If (left_side = right_side) Then Return False
            End Select

        Next
        Return True

    End Function


End Class
