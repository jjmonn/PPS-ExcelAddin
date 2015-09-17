Class Formats

    Friend Structure FinancialBIFormat

        Public name As String
        Public textColor
        Public backColor
        Public bordersColor
        Public isBold
        Public isItalic
        Public bordersPresent

    End Structure

    Friend Shared Function GetFormat(ByRef formatName As String) As Formats.FinancialBIFormat

        Dim format As Formats.FinancialBIFormat
        Select Case formatName
            Case "Title"
                format.textColor = My.Settings.titleFontColor
                format.backColor = My.Settings.titleBackColor
                format.isBold = My.Settings.titleFontBold
                format.isItalic = My.Settings.titleFontItalic
                format.bordersPresent = My.Settings.titleBordersPresent
                format.bordersColor = My.Settings.titleBordersColor
            Case "Important"
                format.textColor = My.Settings.importantFontColor
                format.backColor = My.Settings.importantBackColor
                format.isBold = My.Settings.importantFontBold
                format.isItalic = My.Settings.importantFontItalic
                format.bordersPresent = My.Settings.importantBordersPresent
                format.bordersColor = My.Settings.importantBordersColor
            Case "Normal"
                format.textColor = My.Settings.normalFontColor
                format.backColor = My.Settings.normalBackColor
                format.isBold = My.Settings.normalFontBold
                format.isItalic = My.Settings.normalFontItalic
                format.bordersPresent = My.Settings.normalBordersPresent
                format.bordersColor = My.Settings.normalBordersColor
            Case "Detail"
                format.textColor = My.Settings.detailFontColor
                format.backColor = My.Settings.detailBackColor
                format.isBold = My.Settings.detailFontBold
                format.isItalic = My.Settings.detailFontItalic
                format.bordersPresent = My.Settings.detailBordersPresent
                format.bordersColor = My.Settings.detailBordersColor
        End Select
        Return format

    End Function




End Class

