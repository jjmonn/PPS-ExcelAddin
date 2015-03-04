' FormatsController.vb
'
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 03/03/2015


Imports System.Collections
Imports System.Collections.Generic


Friend Class FormatsController


#Region "Instance Variables"

    ' Object
    Private formats As New FormatM()
    Private view As FormatMGTUI

    ' Intance Variables
    Friend formats_name_id_dic As Hashtable


#End Region


#Region "Initialize"

    Protected Friend Sub New()

        formats_name_id_dic = FormatsMapping.GetFormatsDictionary(FORMAT_NAME_VARIABLE, FORMAT_CODE_VARIABLE, INPUT_FORMAT_CODE)
        view = New FormatMGTUI(Me, formats_name_id_dic)
        InitializeView()
        view.Show()

    End Sub

    Private Sub InitializeView()

        Dim formats_dic As New Dictionary(Of String, Hashtable)
        For Each format_id As String In formats_name_id_dic.Values
            formats_dic.Add(format_id, formats.GetFormatHT(format_id, INPUT_FORMAT_CODE))
        Next
        view.FillDGV(formats_dic)

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub UpdateTextColor(ByRef format_id As String, ByRef argb_color As Int32)

        formats.Updateformat(format_id, INPUT_FORMAT_CODE, FORMAT_TEXT_COLOR_VARIABLE, argb_color)
        view.FormatPreview(format_id, formats.GetFormatHT(format_id, INPUT_FORMAT_CODE))

    End Sub

    Protected Friend Sub UpdateBckgdColor(ByRef format_id As String, ByRef argb_color As Int32)

        formats.Updateformat(format_id, INPUT_FORMAT_CODE, FORMAT_BCKGD_VARIABLE, argb_color)
        view.FormatPreview(format_id, formats.GetFormatHT(format_id, INPUT_FORMAT_CODE))

    End Sub

    Protected Friend Sub UpdateBold(ByRef format_id As String, ByRef bold As Int32)

        formats.Updateformat(format_id, INPUT_FORMAT_CODE, FORMAT_BOLD_VARIABLE, bold)
        view.FormatPreview(format_id, formats.GetFormatHT(format_id, INPUT_FORMAT_CODE))

    End Sub

    Protected Friend Sub UpdateItalic(ByRef format_id As String, ByRef italic As Int32)

        formats.Updateformat(format_id, INPUT_FORMAT_CODE, FORMAT_ITALIC_VARIABLE, italic)
        view.FormatPreview(format_id, formats.GetFormatHT(format_id, INPUT_FORMAT_CODE))

    End Sub

    Protected Friend Sub UpdateBorder(ByRef format_id As String, ByRef border As Int32)

        formats.Updateformat(format_id, INPUT_FORMAT_CODE, FORMAT_BORDER_VARIABLE, border)

    End Sub


#End Region


#Region "Utilities"


    Protected Friend Sub Close()

        MyBase.Finalize()

    End Sub
    
#End Region



End Class
