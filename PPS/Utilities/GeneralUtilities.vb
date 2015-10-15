'
'
'
'
'
' Last modified: 08/09/2015
' Author: Julien Monnereau


Imports Microsoft.Office.Interop
Imports System.Collections.Generic
Imports System.Collections


Friend Class GeneralUtilities


#Region "Arrays Utilities"

    ' Function lookUpArray()
    ' Checks if a value is in an array and returns its position if true
    ' Returns false if the value is not in the array
    'Public Function lookUpArray(lookUpValue As Object, inputArray As Object) As Object

    '    Try
    '        lookUpArray = GlobalVariables.apps.WorksheetFunction.Match(lookUpValue, inputArray, 0)
    '    Catch ex As Exception
    '        lookUpArray = False
    '    End Try

    'End Function


    ' Function getDimensions()
    ' Returns the dimension of an array
    ' Takes an array as parameter, returns the number of dimensions as integer

    Protected Friend Function getDimension(var As Object) As Integer
        On Error GoTo Err
        Dim i As Integer
        Dim Tmp As Integer
        i = 0
        Do While True
            i = i + 1
            Tmp = UBound(var, i)
        Loop
Err:
        getDimension = i - 1
    End Function

    ' twoDimToOneDimArray
    ' Transforms a 2 dimension arryay into a 1 dimension array
    ' Returns input array if 1 dimension, error if > 2 dimensions

    Protected Friend Function twoDimToOneDimArray(inputArray(,) As Object) As Object

        Dim dimension As Integer
        Dim outputArray() As Object
        dimension = getDimension(inputArray)

        If dimension = 2 Then
            Dim i As Integer, j As Integer, K As Integer
            ReDim outputArray(0 To (UBound(inputArray, 1) + 1) * UBound(inputArray, 2))

            For i = LBound(inputArray, 1) To UBound(inputArray, 1)
                For j = LBound(inputArray, 2) To UBound(inputArray, 2)
                    outputArray(K) = inputArray(i, j)
                    K = K + 1
                Next j
            Next i

            twoDimToOneDimArray = outputArray

        ElseIf dimension = 1 Then
            twoDimToOneDimArray = inputArray
        Else
            twoDimToOneDimArray = ""
            MsgBox("Input array is more than 2 dimensions")
        End If

    End Function


    ' Function getColumn()
    ' Returns an array corresponding to the specified column of a given input array
    ' Careful : Takes a 2 dimensions array as input

    'Protected Friend Function getColumn(inputArray As Object, column As Integer) As Object

    '    ' INDEX FUNCTION !!
    '    ' to be replaced..

    '    Dim ResultArray(0 To UBound(inputArray, 1)) As Object

    '    Dim i As Integer, dimension As Integer
    '    dimension = getDimension(inputArray)

    '    If dimension = 2 Then
    '        For i = LBound(inputArray, 1) To UBound(inputArray, 1)
    '            ResultArray(i) = inputArray(i, column)
    '        Next i
    '    Else
    '        getColumn = ""
    '        MsgBox("Number of dimension of the array <> 2")
    '    End If

    '    getColumn = ResultArray

    'End Function

#End Region


#Region "Excel Utilities"

    ' Function GetRealLastCell()
    ' starting from "A1", finds the cell closing the range really used within the worksheet
    ' Inlude if err.number > 0 then ?
    Protected Friend Shared Function GetRealLastCell(ByRef WS As Excel.Worksheet) As Excel.Range

        Dim lRealLastRow As Long
        Dim lRealLastColumn As Long

        Try
            lRealLastRow = WS.Cells.Find("*", WS.Cells(1, 1), , , Excel.XlSearchOrder.xlByRows, _
                             Excel.XlSearchDirection.xlPrevious).Row

            lRealLastColumn = WS.Cells.Find("*", WS.Cells(1, 1), , , Excel.XlSearchOrder.xlByColumns, _
                                            Excel.XlSearchDirection.xlPrevious).Column

            Return WS.Cells(lRealLastRow, lRealLastColumn)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Protected Friend Function IsRange(ByVal sRangeAddress As String) As Boolean

        Dim TestRange As Excel.Range

        IsRange = True
        On Error Resume Next
        TestRange = GlobalVariables.APPS.Range(sRangeAddress)
        If Err.Number <> 0 Then
            IsRange = False
        End If
        Err.Clear()
        On Error GoTo 0
        TestRange = Nothing
    End Function

    '----------------------------------------------------------------
    ' Function: Check Address
    ' Input: address
    ' Output: valid address
    '----------------------------------------------------------------
    Protected Friend Function CheckAddress(sAddress As String) As String
        Dim rng As Microsoft.Office.Interop.Excel.Range
        Dim sFullAddress As String

        If Left$(sAddress, 1) = "=" Then sAddress = Mid$(sAddress, 2, 256)
        If Left$(sAddress, 1) = Chr(34) Then sAddress = Mid$(sAddress, 2, 255)
        If Right$(sAddress, 1) = Chr(34) Then sAddress = Left$(sAddress, Len(sAddress) - 1)

        On Error Resume Next
        sAddress = GlobalVariables.APPS.ConvertFormula(sAddress, Excel.XlReferenceStyle.xlR1C1, Excel.XlReferenceStyle.xlA1)

        If IsRange(sAddress) Then
            rng = GlobalVariables.APPS.Range(sAddress)
        End If

        If Not rng Is Nothing Then
            sFullAddress = rng.Address(, , GlobalVariables.APPS.ReferenceStyle, True)
            If Left$(sFullAddress, 1) = "'" Then
                sAddress = "'"
            Else
                sAddress = ""
            End If
            sAddress = sAddress + Mid$(sFullAddress, InStr(sFullAddress, "]") + 1)

            rng.Parent.Activate()

            CheckAddress = sAddress
        End If

    End Function


#End Region


#Region "Strings Utilities"

    ' Transforms an array of accounts strings into a new array of words separated by special characters( (),.;:=+-*/)
    '    Friend Shared Function StringClean(inputArray() As String) As String()

    '        Dim j As Integer
    '        Dim word As Integer
    '        Dim finalIndex As Integer
    '        Dim tempWordsArray() As String
    '        Dim finalWordsArray(0) As String
    '        finalIndex = 0

    '        For word = LBound(inputArray) To UBound(inputArray)                                 ' Loop in each word of the input array
    '            For Each specialCharacter In FormulasParser.FORMULAS_TOKEN_CHARACTERS

    '                If InStr(1, inputArray(word), specialCharacter) Then                        ' If special character found then
    '                    tempWordsArray = Split(inputArray(word), specialCharacter)              ' Subsplit the word into array of cleaner words
    '                    tempWordsArray = StringClean(tempWordsArray)                            ' Recursive call

    '                    ReDim Preserve finalWordsArray(UBound(finalWordsArray) + UBound(tempWordsArray))
    '                    For j = LBound(tempWordsArray) To UBound(tempWordsArray)                 ' Fill finalWordsArray
    '                        finalWordsArray(finalIndex) = tempWordsArray(j)
    '                        finalIndex = finalIndex + 1
    '                    Next j

    '                    GoTo nextWord                                                            ' Exit for : to be checked

    '                End If
    '            Next

    '            finalWordsArray(finalIndex) = inputArray(word)
    '            ReDim Preserve finalWordsArray(UBound(finalWordsArray) + 1)
    '            finalIndex = finalIndex + 1

    'nextWord:
    '        Next word

    '        StringClean = finalWordsArray

    '    End Function

    ' Levenshtein function
    ' Returns the number of characters to be changed to get sthe strings similar

    Protected Friend Shared Function Levenshtein(S1 As String, S2 As String)

        Dim i As Integer
        Dim j As Integer
        Dim L1 As Integer
        Dim L2 As Integer
        Dim D(,) As Integer
        Dim min1 As Integer
        Dim min2 As Integer

        L1 = Len(S1)
        L2 = Len(S2)
        ReDim D(L1, L2)
        For i = 0 To L1
            D(i, 0) = i
        Next
        For j = 0 To L2
            D(0, j) = j
        Next
        For i = 1 To L1
            For j = 1 To L2
                If Mid(S1, i, 1) = Mid(S2, j, 1) Then
                    D(i, j) = D(i - 1, j - 1)
                Else
                    min1 = D(i - 1, j) + 1
                    min2 = D(i, j - 1) + 1
                    If min2 < min1 Then
                        min1 = min2
                    End If
                    min2 = D(i - 1, j - 1) + 1
                    If min2 < min1 Then
                        min1 = min2
                    End If
                    D(i, j) = min1
                End If
            Next
        Next
        Levenshtein = D(L1, L2)
    End Function

    ' Recursive function returning a string describing the formula
    ' Return a string which divides operands and operators by the FORMULA_SEPARATOR
    'Friend Shared Function DivideFormula(initialStr As String) As String

    '    '1. Loook for operators
    '    Dim substring1, substring2, operator1 As String
    '    Dim flag As Boolean
    '    Dim OperatorPosition, index As Integer
    '    Dim operatorsList As New List(Of String)
    '    For Each character In FormulasTranslations.FORMULAS_TOKEN_CHARACTERS
    '        operatorsList.Add(character)
    '    Next

    '    For Each item As String In initialStr
    '        If operatorsList.Contains(item) Then
    '            OperatorPosition = index
    '            operator1 = item
    '            flag = True
    '            Exit For
    '        End If
    '        index = index + 1
    '    Next

    '    'a. Operator found
    '    If flag = True Then

    '        substring1 = Left(initialStr, OperatorPosition)
    '        substring2 = Right(initialStr, Len(initialStr) - OperatorPosition - 1)
    '        Return DivideFormula(substring1) + FormulasTranslations.FORMULA_SEPARATOR + operator1 + FormulasTranslations.FORMULA_SEPARATOR + DivideFormula(substring2)

    '        'c. nothing found
    '    Else
    '        Return initialStr
    '    End If

    'End Function

    ' Function findOperator()
    ' Returns true if an operator is in the string parameter
    ' and set the instance variable operator to the corresponding operator
    'Friend Shared Function findOperator(str As String) As Integer

    '    Dim Operator1 As String
    '    Dim charPosition As Integer
    '    For Each specialCharacter As String In FormulasParser.FORMULAS_TOKEN_CHARACTERS

    '        On Error Resume Next
    '        charPosition = GlobalVariables.APPS.WorksheetFunction.Find(specialCharacter, str)
    '        If Err.Number = 0 And charPosition >= 1 Then

    '            findOperator = charPosition
    '            Operator1 = specialCharacter
    '            Exit Function

    '        End If
    '    Next

    '   End Function


#End Region


#Region "Lists Utilities"

    Friend Shared Function GetShortList(ByRef list1 As List(Of String), _
                                                  ByRef list2 As List(Of String)) As List(Of String)

        Dim tmp_list As New List(Of String)
        For Each value In list1
            If list2.Contains(value) Then tmp_list.Add(value)
        Next
        Return tmp_list

    End Function

    Friend Shared Function ListsEqualityCheck(ByRef list1 As List(Of String), _
                                              ByRef list2 As List(Of String)) As Boolean

        For Each element As Object In list1
            If list2.Contains(element) = False Then Return False
        Next
        For Each element As Object In list2
            If list1.Contains(element) = False Then Return False
        Next
        Return True

    End Function

    Friend Shared Function getStringsList(ByRef input_array As String()) As List(Of String)

        Dim tmp_list As New List(Of String)
        For Each value As String In input_array
            tmp_list.Add(value)
        Next
        Return tmp_list

    End Function



#End Region


#Region "Dictionaries Utilities"

    Friend Shared Sub AddOrSetValueToDict(ByRef dict As Dictionary(Of String, String), _
                                                    ByRef key As String, _
                                                    ByRef value As String)

        If dict.ContainsKey(key) Then
            dict(key) = value
        Else
            dict.Add(key, value)
        End If

    End Sub

    Friend Shared Function GetDictionaryCopy(ByRef input_dict As Dictionary(Of String, String)) As Dictionary(Of String, String)

        Dim tmp_dict As New Dictionary(Of String, String)
        For Each key As String In input_dict.Keys
            tmp_dict.Add(key, input_dict(key))
        Next
        Return tmp_dict

    End Function

    Friend Shared Function CountNbValueIs(ByRef value_to_be_counted As String, _
                                        ByRef dict As Hashtable) As Int32

        Dim count As Int32
        For Each key As String In dict.Keys
            If dict(key) = value_to_be_counted Then count = count + 1
        Next
        Return count

    End Function

    Friend Shared Function DictsCompare(ByRef dict1 As Dictionary(Of Int32, List(Of Int32)), _
                                        ByRef dict2 As Dictionary(Of Int32, List(Of Int32))) As Boolean

        For Each filterId As Int32 In dict1.Keys
            If dict2.ContainsKey(filterId) = False Then Return False
            For Each filterValueId As Int32 In dict1(filterId)
                If dict2(filterId).Contains(filterValueId) = False Then Return False
            Next
        Next
        Return True

    End Function


#End Region


#Region "Password check"
    Enum CheckResult
        Success
        Fail
        Aborted
    End Enum
    Public Shared Function AskPasswordConfirmation(ByRef p_message As String, ByRef p_title As String) As CheckResult
        p_message = p_message + Chr(13) + Chr(10) + Chr(13) + Chr(10) + "Please type your password to confirm:"
        Dim result = InputBox(p_message, p_title)

        If result = "" Then Return CheckResult.Aborted
        If result <> ConnectionsFunctions.pwd Then Return CheckResult.Fail
        Return CheckResult.Success
    End Function
#End Region

    Public Shared Function getSHA1Hash(ByVal strToHash As String) As String

        Dim sha1Obj As New Security.Cryptography.SHA1CryptoServiceProvider
        Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)

        bytesToHash = sha1Obj.ComputeHash(bytesToHash)

        Dim strResult As String = ""

        For Each b As Byte In bytesToHash
            strResult += b.ToString("x2")
        Next

        Return strResult

    End Function


    Friend Shared Sub SetSelectedItem(ByRef p_combobox As VIBlend.WinForms.Controls.vComboBox, _
                                      ByRef p_itemValue As String)

        For Each item As VIBlend.WinForms.Controls.ListItem In p_combobox.Items
            If item.Value = p_itemValue Then p_combobox.SelectedItem = item
        Next

    End Sub


End Class
