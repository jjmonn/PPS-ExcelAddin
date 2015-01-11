' PasswordMGT.vb
'
' Generates passwords
' 
'
'
' to do :
'       - 
'
'
' Known bugs:
'       - 
'
'
' Last modified: 02/07/2014
' Author: Julien Monnereau



Public Class PasswordGenerator


    ' Return a new password
    Public Function GeneratePassword() As String

        Dim pwd As String = ""
        For i = 0 To PWD_LENGHT - 1
            Randomize()
            Dim randomValue As Integer = CInt(Math.Floor((ANSII_CEILING_PWD_CHAR - ANSII_FLOOR_PWD_CHAR + 1) * Rnd())) + ANSII_FLOOR_PWD_CHAR
            pwd = pwd + Chr(randomValue)
        Next i
        Return pwd

    End Function



End Class
