'
'
'
'
'
'
'
'
'
'
'
' Last modified: 01/10/2014
' Authors: Julien Monnereau


Imports Microsoft.Office.Interop
Imports System.Collections.Generic


Class Utilities_Functions


#Region "Public Declarations"

    '   Public Operator1 As String

#End Region


#Region "Arrays Utilities"

    ' Function lookUpArray()
    ' Checks if a value is an array and returns its position if true
    ' Returns false if the value is not in the array
    Public Function lookUpArray(lookUpValue As Object, inputArray As Object) As Object

        Try
            lookUpArray = APPS.WorksheetFunction.Match(lookUpValue, inputArray, 0)
        Catch ex As Exception
            lookUpArray = False
        End Try

    End Function


    ' Function getDimensions()
    ' Returns the dimension of an array
    ' Takes an array as parameter, returns the number of dimensions as integer

    Public Function getDimension(var As Object) As Integer
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

    Public Function twoDimToOneDimArray(inputArray(,) As Object) As Object

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

    Public Function getColumn(inputArray As Object, column As Integer) As Object

        ' INDEX FUNCTION !!
        ' to be replaced..

        Dim ResultArray(0 To UBound(inputArray, 1)) As Object

        Dim i As Integer, dimension As Integer
        dimension = getDimension(inputArray)

        If dimension = 2 Then
            For i = LBound(inputArray, 1) To UBound(inputArray, 1)
                ResultArray(i) = inputArray(i, column)
            Next i
        Else
            getColumn = ""
            MsgBox("Number of dimension of the array <> 2")
        End If

        getColumn = ResultArray

    End Function

#End Region


#Region "Excel Utilities"

    ' Function GetRealLastCell()
    ' starting from "A1", finds the cell closing the range really used within the worksheet
    ' Inlude if err.number > 0 then ?
    Public Shared Function GetRealLastCell(ByRef WS As Excel.Worksheet) As Excel.Range

        Dim lRealLastRow As Long
        Dim lRealLastColumn As Long
        On Error GoTo errorHandler
        lRealLastRow = WS.Cells.Find("*", WS.Cells(1, 1), , , Excel.XlSearchOrder.xlByRows, _
                                     Excel.XlSearchDirection.xlPrevious).Row

        lRealLastColumn = WS.Cells.Find("*", WS.Cells(1, 1), , , Excel.XlSearchOrder.xlByColumns, _
                                        Excel.XlSearchDirection.xlPrevious).Column

        Return WS.Cells(lRealLastRow, lRealLastColumn)

errorHandler:
        Return Nothing

    End Function

    Public Function IsRange(ByVal sRangeAddress As String) As Boolean

        Dim TestRange As Excel.Range

        IsRange = True
        On Error Resume Next
        TestRange = APPS.Range(sRangeAddress)
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
    Public Function CheckAddress(sAddress As String) As String
        Dim rng As Microsoft.Office.Interop.Excel.Range
        Dim sFullAddress As String

        If Left$(sAddress, 1) = "=" Then sAddress = Mid$(sAddress, 2, 256)
        If Left$(sAddress, 1) = Chr(34) Then sAddress = Mid$(sAddress, 2, 255)
        If Right$(sAddress, 1) = Chr(34) Then sAddress = Left$(sAddress, Len(sAddress) - 1)

        On Error Resume Next
        sAddress = APPS.ConvertFormula(sAddress, Excel.XlReferenceStyle.xlR1C1, Excel.XlReferenceStyle.xlA1)

        If IsRange(sAddress) Then
            rng = APPS.Range(sAddress)
        End If

        If Not rng Is Nothing Then
            sFullAddress = rng.Address(, , APPS.ReferenceStyle, True)
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


#Region "MATRIX FUNCTIONS"


    '------------------------------------------------------------------------------
    ' Function: Sum of the columns of a matrix
    '------------------------------------------------------------------------------

    Public Function sumColMatrix(Matrix(,) As Double) As Double()

        Dim matrix2(UBound(Matrix, 2)) As Double
        For L = LBound(Matrix, 2) To UBound(Matrix, 2)
            With APPS.WorksheetFunction
                matrix2(L) = .Sum(.Index(Matrix, 0, L + 1))
            End With
        Next L
        sumColMatrix = matrix2

    End Function



    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Multiply a matrix with a scalar quantity
    ' Function returns the solution or errors
    ' 
    ' -> To be written for a vector also !! 
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Function ScalarMultiply(Value As Double, Mat(,) As Double) As Double(,)
        Dim i As Integer, j As Integer
        Dim Mat1(,) As Double, sol(,) As Double

        On Error GoTo Error_Handler

        Mat1 = Find_R_C(Mat)
        ReDim sol(Mat1(0, 0) - 1, Mat1(0, 1) - 1)

        For i = 1 To Mat1(0, 0)
            For j = 1 To Mat1(0, 1)
                sol(i - 1, j - 1) = Mat1(i, j) * Value
            Next j
        Next i

        ScalarMultiply = sol

        Exit Function

Error_Handler:
        Err.Raise("5022", , "Matrix was not assigned")
    End Function

    '====================================================================
    ' ALGEBRA FUNCTIONS
    '====================================================================

    '--------------------------------------------------------------------
    ' Function RMatrixMult
    ' Carry on matrix A * matrix B
    ' inputs: 1. Matrix A(m,k)
    '         2. Matrix B(k,n)
    '
    ' Output: Matrix C(m,n)
    '--------------------------------------------------------------------

    Public Function RMatrixMult(A(,) As Double, B(,) As Double) As Double(,)

        Dim m As Integer = UBound(A, 1) - LBound(A, 1) + 1
        Dim K As Integer = UBound(A, 2) - LBound(A, 2) + 1
        Dim n As Integer = UBound(B, 2) - LBound(B, 2) + 1
        Dim C(m - 1, n - 1) As Double
        'Call RMatrixGEMM(m, n, K, 1, A, 0, 0, 0, B, 0, 0, 0, 0, C, 0, 0)
        RMatrixMult = C

    End Function
    '--------------------------------------------------------------------

    '--------------------------------------------------------------------
    ' Function RMatrixMultV
    ' Carry on matrix A * vector
    ' inputs: 1. Matrix A(m,n)
    '         2. VectorX(n,1)
    '
    ' Output: Vector Y(m,n)
    '--------------------------------------------------------------------
    Public Function RMatrixMultV(A(,) As Double, X() As Double) As Double(,)

        Dim m As Integer = UBound(A, 1) - LBound(A, 1) + 1
        Dim n As Integer = UBound(A, 2) - LBound(A, 2) + 1
        Dim Y(m, m) As Double
        'Call RMatrixMV(m, n, A, 0, 0, 0, X, 0, Y, 0)
        RMatrixMultV = Y

    End Function

    '-------------------------------------------------------------------
    ' Function MatrixTranspose
    ' Input: Matrix A
    ' Output: A'
    '-------------------------------------------------------------------

    Public Function MatrixTranspose(A(,) As Object) As Double(,)

        Dim m As Integer = UBound(A, 1) - LBound(A, 1) + 1
        Dim n As Integer = UBound(A, 2) - LBound(A, 2) + 1
        Dim Y(m, m) As Double
        'Call RMatrixTranspose(m, n, A, 0, 0, Y, 0, 0)
        MatrixTranspose = Y

    End Function


    '--------------------------------------------------------------------
    ' Function MatrixAdd()
    ' Inputs: 1. Matrix A
    '         2. Matrix B
    ' Output: Matrix C
    '--------------------------------------------------------------------


    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Add two matrices, their dimensions should be compatible!
    ' Function returns the summation or errors due to
    ' dimensions incompatibility
    ' Example:
    '  Check Main Form !!
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Function MatrixAdd(Mat_1(,) As Double, Mat_2(,) As Double) As Double(,)
        Dim Mat1(,) As Double, Mat2(,) As Double
        Dim sol(,) As Double
        Dim i As Integer, j As Integer

        On Error GoTo Error_Handler

        Mat1 = Find_R_C(Mat_1)
        Mat2 = Find_R_C(Mat_2)

        If Mat1(0, 0) <> Mat2(0, 0) Or Mat1(0, 1) <> Mat2(0, 1) Then
            GoTo Error_Dimension
        End If

        ReDim sol(Mat1(0, 0) - 1, Mat1(0, 1) - 1)
        For i = 1 To Mat1(0, 0)
            For j = 1 To Mat1(0, 1)
                sol(i - 1, j - 1) = Mat1(i, j) + Mat2(i, j)
            Next j
        Next i

        MatrixAdd = sol
        Erase sol

        Exit Function

Error_Dimension:
        Err.Raise("5005", , "Dimensions of the two matrices do not match !")

Error_Handler:
        If Err.Number = 5005 Then
            Err.Raise("5005", , "Dimensions of the two matrices do not match !")
        Else
            Err.Raise("5022", , "One or both of the matrices are null, this operation cannot be done !!")
        End If

    End Function

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Subtracts two matrices from each other, their
    ' dimensions should be compatible!
    ' Function returns the solution or errors due to
    ' dimensions incompatibility
    ' Example:
    '  Check Main Form !!
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Function Subtract(Mat_1(,) As Double, Mat_2(,) As Double) As Double(,)
        Dim Mat1(,) As Double, Mat2(,) As Double
        Dim i As Integer, j As Integer, sol(,) As Double

        On Error GoTo Error_Handler

        Mat1 = Find_R_C(Mat_1)
        Mat2 = Find_R_C(Mat_2)

        If Mat1(0, 0) <> Mat2(0, 0) Or Mat1(0, 1) <> Mat2(0, 1) Then
            GoTo Error_Dimension
        End If

        ReDim sol(Mat1(0, 0) - 1, Mat1(0, 1) - 1)

        For i = 1 To Mat1(0, 0)
            For j = 1 To Mat1(0, 1)
                sol(i - 1, j - 1) = Mat1(i, j) - Mat2(i, j)
            Next j
        Next i

        Subtract = sol
        Erase sol
        Exit Function

Error_Dimension:
        Err.Raise("5007", , "Dimensions of the two matrices do not match !")

Error_Handler:
        If Err.Number = 5007 Then
            Err.Raise("5007", , "Dimensions of the two matrices do not match !")
        Else
            Err.Raise("5022", , "One or both of the matrices are null, this operation cannot be done !!")
        End If

    End Function

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' The dimensions of the matrix are checked
    ' Here
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Function Find_R_C(Mat As Object) As Double(,)
        Dim Rows As Integer, Columns As Integer
        Dim i As Integer, j As Integer
        Dim Result(,) As Double
        Columns = 0
        If Mat_1D(Mat, Rows) Then

            ReDim Result(Rows, 1)
            Result(0, 0) = Rows
            Result(0, 1) = Columns + 1

            For i = 1 To Rows
                Result(i, 1) = Mat(i - 1)
            Next i
        Else
            Call Mat_2D(Mat, Rows, Columns)
            ReDim Result(Rows, Columns)
            Result(0, 0) = Rows
            Result(0, 1) = Columns

            For i = 1 To Rows
                For j = 1 To Columns '- 1
                    Result(i, j) = Mat(i - 1, j - 1)
                Next j
            Next i
        End If
        Find_R_C = Result
    End Function



    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Check if matrix has only one column
    ' shift the matrix one level and keep
    ' its dimensions details in Mat(0,0) and Mat(0,1)
    ' Mat(0,0)= no of rows
    ' Mat(0,1)= no of columns
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Function Mat_1D(Mat As Object, m As Integer) As Boolean
        Dim Temp_MAT As Double
        On Error GoTo Error_Handler
        Temp_MAT = Mat(0, 0)
        Mat_1D = False
        Exit Function
Error_Handler:
        Mat_1D = True
        m = UBound(Mat) + 1
    End Function


    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Check if matrix has more than one column
    ' if so return the dimension as described above
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub Mat_2D(Mat As Object, m As Integer, n As Integer)
        Dim Temp_MAT As Double, i As Integer
        i = 0
        m = UBound(Mat) + 1
        On Error GoTo Error_Handler
        Do Until i < -1
            Temp_MAT = Mat(0, i)
            i = i + 1
        Loop
Error_Handler:
        n = i
    End Sub

#End Region


#Region "Strings Utilities"

    ' Transforms an array of accounts strings into a new array of words separated by special characters( (),.;:=+-*/)
    Public Shared Function StringClean(inputArray() As String) As String()

        Dim j As Integer
        Dim word As Integer
        Dim finalIndex As Integer
        Dim tempWordsArray() As String
        Dim finalWordsArray(0) As String
        finalIndex = 0

        For word = LBound(inputArray) To UBound(inputArray)                                 ' Loop in each word of the input array
            For Each specialCharacter In ModelFormulasMGT.FORMULAS_TOKEN_CHARACTERS

                If InStr(1, inputArray(word), specialCharacter) Then                        ' If special character found then
                    tempWordsArray = Split(inputArray(word), specialCharacter)              ' Subsplit the word into array of cleaner words
                    tempWordsArray = StringClean(tempWordsArray)                            ' Recursive call

                    ReDim Preserve finalWordsArray(UBound(finalWordsArray) + UBound(tempWordsArray))
                    For j = LBound(tempWordsArray) To UBound(tempWordsArray)                 ' Fill finalWordsArray
                        finalWordsArray(finalIndex) = tempWordsArray(j)
                        finalIndex = finalIndex + 1
                    Next j

                    GoTo nextWord                                                            ' Exit for : to be checked

                End If
            Next

            finalWordsArray(finalIndex) = inputArray(word)
            ReDim Preserve finalWordsArray(UBound(finalWordsArray) + 1)
            finalIndex = finalIndex + 1

nextWord:
        Next word

        StringClean = finalWordsArray

    End Function

    ' Levenshtein function
    ' Returns the number of characters to be changed to get sthe strings similar

    Public Shared Function Levenshtein(S1 As String, S2 As String)

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
    Public Shared Function DivideFormula(initialStr As String) As String

        '1. Loook for operators
        Dim substring1, substring2, operator1 As String
        Dim flag As Boolean
        Dim OperatorPosition, index As Integer
        Dim operatorsList As New List(Of String)
        For Each character In ModelFormulasMGT.FORMULAS_TOKEN_CHARACTERS
            operatorsList.Add(character)
        Next

        For Each item As String In initialStr
            If operatorsList.Contains(item) Then
                OperatorPosition = index
                operator1 = item
                flag = True
                Exit For
            End If
            index = index + 1
        Next

        'a. Operator found
        If flag = True Then

            substring1 = Left(initialStr, OperatorPosition)
            substring2 = Right(initialStr, Len(initialStr) - OperatorPosition - 1)
            Return DivideFormula(substring1) + FORMULA_SEPARATOR + operator1 + FORMULA_SEPARATOR + DivideFormula(substring2)

            'c. nothing found
        Else
            Return initialStr
        End If

    End Function

    ' Function findOperator()
    ' Returns true if an operator is in the string parameter
    ' and set the instance variable operator to the corresponding operator
    Public Shared Function findOperator(str As String) As Integer

        Dim Operator1 As String
        Dim charPosition As Integer
        For Each specialCharacter As String In ModelFormulasMGT.FORMULAS_TOKEN_CHARACTERS

            On Error Resume Next
            charPosition = APPS.WorksheetFunction.Find(specialCharacter, str)
            If Err.Number = 0 And charPosition >= 1 Then

                findOperator = charPosition
                Operator1 = specialCharacter
                Exit Function

            End If
        Next

    End Function


#End Region



End Class
