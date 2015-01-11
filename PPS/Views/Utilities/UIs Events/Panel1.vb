' Panel1.vb
'
' Manages display customizations for the panel1 in forms (mainly border color)
'
'
'
'
'
'
'
'


Imports System.Windows.Forms
Imports System.Drawing


Module Panel1



    Public Sub Panel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs)

        sender.BorderStyle = BorderStyle.None

        e.Graphics.DrawRectangle(Pens.Purple,
                                 e.ClipRectangle.Left,
                                 e.ClipRectangle.Top,
                                 e.ClipRectangle.Width - 1,
                                 e.ClipRectangle.Height - 1)


    End Sub




End Module
