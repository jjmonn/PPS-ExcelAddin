Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices

Module MGD_Util

    Public Declare Function GetWindowThreadProcessId Lib "user32.dll" (ByVal hWnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer
    Public Declare Function FindWindow Lib "user32.dll" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr


    Dim proc As System.Diagnostics.Process
    Dim intPID As Integer
    Dim intResult As Integer
    Dim iHandle As IntPtr
    Dim strVer As String

    Public Sub kill(App As Excel.Application)

        Try

            App.Caption = "test"
            App.DisplayAlerts = False
            strVer = App.Version
            iHandle = IntPtr.Zero

            strVer = Replace(strVer, ".", ",")
            If CInt(strVer) > 9 Then
                iHandle = New IntPtr(CType(App.Parent.Hwnd, Integer))
            Else
                iHandle = FindWindow(Nothing, App.Caption)

            End If

            App.Workbooks.Close()

            App.Quit()

            Marshal.ReleaseComObject(App)
            App = Nothing
            intResult = GetWindowThreadProcessId(iHandle, intPID)
            proc = System.Diagnostics.Process.GetProcessById(intPID)
            proc.Kill()

        Catch ex As Exception

        End Try

    End Sub

End Module
