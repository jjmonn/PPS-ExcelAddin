Imports System.Xml
Imports System.Collections.Generic

Public Class Local
    Private Shared m_localDic As New Dictionary(Of String, String)

    Public Shared Function GetValue(ByRef p_name As String)
        If m_localDic.ContainsKey("FBI." & p_name) Then Return m_localDic("FBI." & p_name)
        System.Diagnostics.Debug.WriteLine("Local " & p_name & " never defined.")
        Return "[" & p_name & "]"
    End Function

    Public Shared Function LoadLocalFile(ByRef p_path As String) As Boolean
        Try
            If LoadLocalFile_intern(p_path) = True Then Return True
            m_localDic.Clear()
            Return False
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Private Shared Function LoadLocalFile_intern(ByRef p_path As String) As Boolean
        Dim reader As XmlTextReader = New XmlTextReader(p_path)
        Dim currentPath As New String("")
        Dim insideString As Boolean = False
        Dim insideCategory As Int32 = 0
        Dim currentStringName As String = ""

        Do While (reader.Read())
            Select Case reader.NodeType
                Case XmlNodeType.Element
                    Select Case reader.Name
                        Case "category"
                            If currentPath <> "" Then currentPath &= "."
                            currentPath &= reader.GetAttribute("name")
                            insideCategory += 1
                        Case "string"
                            If insideCategory <= 0 Then
                                System.Diagnostics.Debug.WriteLine("Local file is bad formated: found opening <string> outside of any <category> at line " _
                                                                   & reader.LineNumber & ". Abort loading.")
                            End If
                            currentStringName = reader.GetAttribute("name")
                            insideString = True
                        Case Else
                            System.Diagnostics.Debug.WriteLine("Local file contain unknown tag at line " & reader.LineNumber & ". Abort loading.")
                            Return False
                    End Select
                Case XmlNodeType.EndElement
                    Select Case reader.Name
                        Case "category"
                            If insideCategory <= 0 Then
                                System.Diagnostics.Debug.WriteLine("Local file is bad formated: closing tag </category> without matching <category> at line " _
                                   & reader.LineNumber & ". Abort loading.")
                            End If
                            currentPath = RemoveLastPathElem(currentPath)
                            insideCategory -= 1
                        Case "string"
                            If insideString = False Then
                                System.Diagnostics.Debug.WriteLine("Local file is bad formated: closing tag </string> without matching <string> at line " _
                                                                   & reader.LineNumber & ". Abort loading.")
                                Return False
                            End If
                            insideString = False
                        Case Else
                            System.Diagnostics.Debug.WriteLine("Local file contain unknown tag at line " & reader.LineNumber & ". Abort loading.")
                            Return False
                    End Select
                Case XmlNodeType.Text
                    If insideString = False Then Continue Do
                    SetLocalEntry(currentPath & "." & currentStringName, reader.Value)
            End Select
        Loop
        reader.Close()
        Return True
    End Function

    Private Shared Sub SetLocalEntry(ByRef p_path As String, ByRef p_value As String)
        If m_localDic.ContainsKey(p_path) Then
            m_localDic(p_path) = p_value
            System.Diagnostics.Debug.WriteLine("Warning: " & p_path & " defined multiple times")
        Else
            m_localDic.Add(p_path, p_value)
        End If
    End Sub

    Private Shared Function RemoveLastPathElem(ByRef p_path As String) As String
        Dim newEnd As Int32 = p_path.Length
        Dim newPath As New String("")

        For i As Int32 = 0 To p_path.Length - 1
            If (p_path.Chars(p_path.Length - i - 1) = "."c) Then
                newEnd = p_path.Length - i - 1
            End If
        Next
        For i As Int32 = 0 To newEnd - 1
            newPath &= p_path.Chars(i)
        Next
        Return newPath
    End Function

End Class
