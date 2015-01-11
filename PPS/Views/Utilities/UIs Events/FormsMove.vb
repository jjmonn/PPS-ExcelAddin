Imports System.Drawing



Module FormsMove


    Private IsFormBeingDragged As Boolean = False
    Private MouseDownX As Integer
    Private MouseDownY As Integer

    Public Sub form_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs)

        If e.Button = Windows.Forms.MouseButtons.Left Then
            IsFormBeingDragged = True
            MouseDownX = e.X
            MouseDownY = e.Y
        End If

    End Sub

    Public Sub form_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs)

        If IsFormBeingDragged Then
            Dim temp As Point = New Point()

            temp.X = sender.Location.X + (e.X - MouseDownX)
            temp.Y = sender.Location.Y + (e.Y - MouseDownY)
            sender.Location = temp
            temp = Nothing
        End If

    End Sub


    Public Sub panel1_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs)

        If IsFormBeingDragged Then
            Dim temp As Point = New Point()

            temp.X = sender.parent.Location.X + (e.X - MouseDownX)
            temp.Y = sender.parent.Location.Y + (e.Y - MouseDownY)
            sender.parent.Location = temp
            temp = Nothing
        End If

    End Sub



    Public Sub form_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs)

        If e.Button = Windows.Forms.MouseButtons.Left Then
            IsFormBeingDragged = False
        End If


    End Sub











End Module
