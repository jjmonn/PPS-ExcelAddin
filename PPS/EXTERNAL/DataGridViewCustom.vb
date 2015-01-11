Imports System.Windows.Forms
Imports System.Drawing

Module DataGridViewCustom


#Region " Constants "
    Public Enum DGVHeaderImageAlignments As Int32
        [Default] = 0
        FillCell = 1
        SingleCentered = 2
        SingleLeft = 3
        SingleRight = 4
        Stretch = [Default]
        Tile = 5
    End Enum
#End Region


#Region " Methods "

    Public Sub GridDrawCustomHeaderColumns(ByVal dgv As DataGridView, _
     ByVal e As DataGridViewCellPaintingEventArgs, ByVal img As Image, _
     ByVal Style As DGVHeaderImageAlignments)
        ' All of the graphical Processing is done here.
        Dim gr As Graphics = e.Graphics
        ' Fill the BackGround with the BackGroud Color of Headers.
        ' This step is necessary, for transparent images, or what's behind
        ' would be painted instead.
        gr.FillRectangle( _
         New SolidBrush(dgv.ColumnHeadersDefaultCellStyle.BackColor), _
         e.CellBounds)
        If img IsNot Nothing Then
            Select Case Style
                Case DGVHeaderImageAlignments.FillCell
                    gr.DrawImage( _
                     img, e.CellBounds.X, e.CellBounds.Y, _
                     e.CellBounds.Width, e.CellBounds.Height)
                Case DGVHeaderImageAlignments.SingleCentered
                    gr.DrawImage(img, _
                     ((e.CellBounds.Width - img.Width) \ 2) + e.CellBounds.X, _
                     ((e.CellBounds.Height - img.Height) \ 2) + e.CellBounds.Y, _
                     img.Width, img.Height)
                Case DGVHeaderImageAlignments.SingleLeft
                    gr.DrawImage(img, e.CellBounds.X, _
                     ((e.CellBounds.Height - img.Height) \ 2) + e.CellBounds.Y, _
                     img.Width, img.Height)
                Case DGVHeaderImageAlignments.SingleRight
                    gr.DrawImage(img, _
                     (e.CellBounds.Width - img.Width) + e.CellBounds.X, _
                     ((e.CellBounds.Height - img.Height) \ 2) + e.CellBounds.Y, _
                     img.Width, img.Height)
                Case DGVHeaderImageAlignments.Tile
                    ' ********************************************************
                    ' To correct: It sould display just a stripe of images,
                    ' long as the whole header, but centered in the header's
                    ' height.
                    ' This code WON'T WORK.
                    ' Any one got any better solution?
                    'Dim rect As New Rectangle(e.CellBounds.X, _
                    ' ((e.CellBounds.Height - img.Height) \ 2), _
                    ' e.ClipBounds.Width, _
                    ' ((e.CellBounds.Height \ 2 + img.Height \ 2)))
                    'Dim br As New TextureBrush(img, Drawing2D.WrapMode.Tile, _
                    ' rect)
                    ' ********************************************************
                    ' This one works... but poorly (the image is repeated
                    ' vertically, too).
                    Dim br As New TextureBrush(img, Drawing2D.WrapMode.Tile)
                    gr.FillRectangle(br, e.ClipBounds)
                Case Else
                    gr.DrawImage( _
                     img, e.CellBounds.X, e.CellBounds.Y, _
                     e.ClipBounds.Width, e.CellBounds.Height)
            End Select
        End If
        'e.PaintContent(e.CellBounds)
        If e.Value Is Nothing Then
            e.Handled = True
            Return
        End If
        Using sf As New StringFormat
            With sf
                Select Case dgv.ColumnHeadersDefaultCellStyle.Alignment
                    Case DataGridViewContentAlignment.BottomCenter
                        .Alignment = StringAlignment.Center
                        .LineAlignment = StringAlignment.Far
                    Case DataGridViewContentAlignment.BottomLeft
                        .Alignment = StringAlignment.Near
                        .LineAlignment = StringAlignment.Far
                    Case DataGridViewContentAlignment.BottomRight
                        .Alignment = StringAlignment.Far
                        .LineAlignment = StringAlignment.Far
                    Case DataGridViewContentAlignment.MiddleCenter
                        .Alignment = StringAlignment.Center
                        .LineAlignment = StringAlignment.Center
                    Case DataGridViewContentAlignment.MiddleLeft
                        .Alignment = StringAlignment.Near
                        .LineAlignment = StringAlignment.Center
                    Case DataGridViewContentAlignment.MiddleRight
                        .Alignment = StringAlignment.Far
                        .LineAlignment = StringAlignment.Center
                    Case DataGridViewContentAlignment.TopCenter
                        .Alignment = StringAlignment.Center
                        .LineAlignment = StringAlignment.Near
                    Case DataGridViewContentAlignment.TopLeft
                        .Alignment = StringAlignment.Near
                        .LineAlignment = StringAlignment.Near
                    Case DataGridViewContentAlignment.TopRight
                        .Alignment = StringAlignment.Far
                        .LineAlignment = StringAlignment.Near
                End Select
                ' This part could be handled...
                'Select Case dgv.ColumnHeadersDefaultCellStyle.WrapMode
                '	Case DataGridViewTriState.False
                '		.FormatFlags = StringFormatFlags.NoWrap
                '	Case DataGridViewTriState.NotSet
                '		.FormatFlags = StringFormatFlags.NoWrap
                '	Case DataGridViewTriState.True
                '		.FormatFlags = StringFormatFlags.FitBlackBox
                'End Select
                .HotkeyPrefix = Drawing.Text.HotkeyPrefix.None
                .Trimming = StringTrimming.None
            End With
            With dgv.ColumnHeadersDefaultCellStyle
                gr.DrawString(e.Value.ToString, .Font, _
                 New SolidBrush(.ForeColor), e.CellBounds, sf)
            End With
        End Using
        e.Handled = True
    End Sub


#End Region







End Module
