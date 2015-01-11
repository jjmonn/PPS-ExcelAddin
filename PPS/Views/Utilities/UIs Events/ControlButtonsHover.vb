' ControlButtonsHover.vb
' 
' MouseHover and MouseLeave events for the controls buttons of the forms:
'       - Close button
'       - Minimize window button
'       - Maximize window button
'
'
' MouseClick events for those controls
'
'
'
' Author: Julien Monnereau
' Last modified: 03/07/2014


Module ControlButtonsHover


    ' Close Button - Mouse Hover
    Public Sub CloseBT_MouseHover(sender As Object, e As EventArgs) 
        sender.imageindex = 0
    End Sub

    ' Close Button - Mouse Leave
    Public Sub CloseBT_MouseLeave(sender As Object, e As EventArgs)
        sender.imageindex = 1
    End Sub

    ' Close Button - Mouse Click
    Public Sub CloseBT_MouseClick(sender As Object, e As EventArgs)
        sender.imageindex = 0
    End Sub







End Module
