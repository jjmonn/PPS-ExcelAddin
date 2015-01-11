'Module Algebra

'    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'    'Same as CMatrixGEMM, but for real numbers.
'    'OpType may be only 0 or 1.
'    '
'    '  -- ALGLIB routine --
'    '     16.12.2009
'    '     Bochkanov Sergey
'    '
'    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'    Public Sub RMatrixGEMM(ByVal m As Long, _
'             ByVal n As Long, _
'             ByVal K As Long, _
'             ByVal Alpha As Double, _
'             ByRef A(,) As Double, _
'             ByVal IA As Long, _
'             ByVal JA As Long, _
'             ByVal OpTypeA As Long, _
'             ByRef B(,) As Double, _
'             ByVal IB As Long, _
'             ByVal JB As Long, _
'             ByVal OpTypeB As Long, _
'             ByVal Beta As Double, _
'             ByRef C(,) As Double, _
'             ByVal IC As Long, _
'             ByVal JC As Long)
'        Dim S1 As Long
'        Dim S2 As Long
'        Dim BS As Long

'        BS = ABLASBlockSize(A)
'        If m <= BS And n <= BS And K <= BS Then
'            Call RMatrixGEMMK(m, n, K, Alpha, A, IA, JA, OpTypeA, B, IB, JB, OpTypeB, Beta, C, IC, JC)
'            Exit Sub
'        End If
'        If m >= n And m >= K Then

'            '
'            ' A*B = (A1 A2)^T*B
'            '
'            Call ABLASSplitLength(A, m, S1, S2)
'            If OpTypeA = 0.0# Then
'                Call RMatrixGEMM(S1, n, K, Alpha, A, IA, JA, OpTypeA, B, IB, JB, OpTypeB, Beta, C, IC, JC)
'                Call RMatrixGEMM(S2, n, K, Alpha, A, IA + S1, JA, OpTypeA, B, IB, JB, OpTypeB, Beta, C, IC + S1, JC)
'            Else
'                Call RMatrixGEMM(S1, n, K, Alpha, A, IA, JA, OpTypeA, B, IB, JB, OpTypeB, Beta, C, IC, JC)
'                Call RMatrixGEMM(S2, n, K, Alpha, A, IA, JA + S1, OpTypeA, B, IB, JB, OpTypeB, Beta, C, IC + S1, JC)
'            End If
'            Exit Sub
'        End If
'        If n >= m And n >= K Then

'            '
'            ' A*B = A*(B1 B2)
'            '
'            Call ABLASSplitLength(A, n, S1, S2)
'            If OpTypeB = 0.0# Then
'                Call RMatrixGEMM(m, S1, K, Alpha, A, IA, JA, OpTypeA, B, IB, JB, OpTypeB, Beta, C, IC, JC)
'                Call RMatrixGEMM(m, S2, K, Alpha, A, IA, JA, OpTypeA, B, IB, JB + S1, OpTypeB, Beta, C, IC, JC + S1)
'            Else
'                Call RMatrixGEMM(m, S1, K, Alpha, A, IA, JA, OpTypeA, B, IB, JB, OpTypeB, Beta, C, IC, JC)
'                Call RMatrixGEMM(m, S2, K, Alpha, A, IA, JA, OpTypeA, B, IB + S1, JB, OpTypeB, Beta, C, IC, JC + S1)
'            End If
'            Exit Sub
'        End If
'        If K >= m And K >= n Then

'            '
'            ' A*B = (A1 A2)*(B1 B2)^T
'            '
'            Call ABLASSplitLength(A, K, S1, S2)
'            If OpTypeA = 0.0# And OpTypeB = 0.0# Then
'                Call RMatrixGEMM(m, n, S1, Alpha, A, IA, JA, OpTypeA, B, IB, JB, OpTypeB, Beta, C, IC, JC)
'                Call RMatrixGEMM(m, n, S2, Alpha, A, IA, JA + S1, OpTypeA, B, IB + S1, JB, OpTypeB, 1.0#, C, IC, JC)
'            End If
'            If OpTypeA = 0.0# And OpTypeB <> 0.0# Then
'                Call RMatrixGEMM(m, n, S1, Alpha, A, IA, JA, OpTypeA, B, IB, JB, OpTypeB, Beta, C, IC, JC)
'                Call RMatrixGEMM(m, n, S2, Alpha, A, IA, JA + S1, OpTypeA, B, IB, JB + S1, OpTypeB, 1.0#, C, IC, JC)
'            End If
'            If OpTypeA <> 0.0# And OpTypeB = 0.0# Then
'                Call RMatrixGEMM(m, n, S1, Alpha, A, IA, JA, OpTypeA, B, IB, JB, OpTypeB, Beta, C, IC, JC)
'                Call RMatrixGEMM(m, n, S2, Alpha, A, IA + S1, JA, OpTypeA, B, IB + S1, JB, OpTypeB, 1.0#, C, IC, JC)
'            End If
'            If OpTypeA <> 0.0# And OpTypeB <> 0.0# Then
'                Call RMatrixGEMM(m, n, S1, Alpha, A, IA, JA, OpTypeA, B, IB, JB, OpTypeB, Beta, C, IC, JC)
'                Call RMatrixGEMM(m, n, S2, Alpha, A, IA + S1, JA, OpTypeA, B, IB, JB + S1, OpTypeB, 1.0#, C, IC, JC)
'            End If
'            Exit Sub
'        End If
'    End Sub

'    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'    'Returns block size - subdivision size where  cache-oblivious  soubroutines
'    'switch to the optimized kernel.
'    '
'    'INPUT PARAMETERS
'    '    A   -   real matrix, is passed to ensure that we didn't split
'    '            complex matrix using real splitting subroutine.
'    '            matrix itself is not changed.
'    '
'    '  -- ALGLIB routine --
'    '     15.12.2009
'    '     Bochkanov Sergey
'    '
'    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'    Public Function ABLASBlockSize(ByRef A() As Double) As Long
'        Dim Result As Long

'        Result = 32.0#

'        ABLASBlockSize = Result
'    End Function

'    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'    'GEMM kernel
'    '
'    '  -- ALGLIB routine --
'    '     16.12.2009
'    '     Bochkanov Sergey
'    '
'    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'    Private Sub RMatrixGEMMK(ByVal m As Long, _
'             ByVal n As Long, _
'             ByVal K As Long, _
'             ByVal Alpha As Double, _
'             ByRef A(,) As Double, _
'             ByVal IA As Long, _
'             ByVal JA As Long, _
'             ByVal OpTypeA As Long, _
'             ByRef B(,) As Double, _
'             ByVal IB As Long, _
'             ByVal JB As Long, _
'             ByVal OpTypeB As Long, _
'             ByVal Beta As Double, _
'             ByRef C(,) As Double, _
'             ByVal IC As Long, _
'             ByVal JC As Long)
'        Dim i As Long
'        Dim j As Long
'        Dim V As Double
'        Dim i_ As Long
'        Dim i1_ As Long


'        '
'        ' if matrix size is zero
'        '
'        If m * n = 0.0# Then
'            Exit Sub
'        End If

'        '
'        ' Try optimized code
'        '
'        If RMatrixGEMMF(m, n, K, Alpha, A, IA, JA, OpTypeA, B, IB, JB, OpTypeB, Beta, C, IC, JC) Then
'            Exit Sub
'        End If

'        '
'        ' if K=0, then C=Beta*C
'        '
'        If K = 0.0# Then
'            If Beta <> 1.0# Then
'                If Beta <> 0.0# Then
'                    For i = 0.0# To m - 1.0# Step 1
'                        For j = 0.0# To n - 1.0# Step 1
'                            C(IC + i, JC + j) = Beta * C(IC + i, JC + j)
'                        Next j
'                    Next i
'                Else
'                    For i = 0.0# To m - 1.0# Step 1
'                        For j = 0.0# To n - 1.0# Step 1
'                            C(IC + i, JC + j) = 0.0#
'                        Next j
'                    Next i
'                End If
'            End If
'            Exit Sub
'        End If

'        '
'        ' General case
'        '
'        If OpTypeA = 0.0# And OpTypeB <> 0.0# Then

'            '
'            ' A*B'
'            '
'            For i = 0.0# To m - 1.0# Step 1
'                For j = 0.0# To n - 1.0# Step 1
'                    If K = 0.0# Or Alpha = 0.0# Then
'                        V = 0.0#
'                    Else
'                        i1_ = (JB) - (JA)
'                        V = 0.0#
'                        For i_ = JA To JA + K - 1.0# Step 1
'                            V = V + A(IA + i, i_) * B(IB + j, i_ + i1_)
'                        Next i_
'                    End If
'                    If Beta = 0.0# Then
'                        C(IC + i, JC + j) = Alpha * V
'                    Else
'                        C(IC + i, JC + j) = Beta * C(IC + i, JC + j) + Alpha * V
'                    End If
'                Next j
'            Next i
'            Exit Sub
'        End If
'        If OpTypeA = 0.0# And OpTypeB = 0.0# Then

'            '
'            ' A*B
'            '
'            For i = 0.0# To m - 1.0# Step 1
'                If Beta <> 0.0# Then
'                    For i_ = JC To JC + n - 1.0# Step 1
'                        C(IC + i, i_) = Beta * C(IC + i, i_)
'                    Next i_
'                Else
'                    For j = 0.0# To n - 1.0# Step 1
'                        C(IC + i, JC + j) = 0.0#
'                    Next j
'                End If
'                If Alpha <> 0.0# Then
'                    For j = 0.0# To K - 1.0# Step 1
'                        V = Alpha * A(IA + i, JA + j)
'                        i1_ = (JB) - (JC)
'                        For i_ = JC To JC + n - 1.0# Step 1
'                            C(IC + i, i_) = C(IC + i, i_) + V * B(IB + j, i_ + i1_)
'                        Next i_
'                    Next j
'                End If
'            Next i
'            Exit Sub
'        End If
'        If OpTypeA <> 0.0# And OpTypeB <> 0.0# Then

'            '
'            ' A'*B'
'            '
'            For i = 0.0# To m - 1.0# Step 1
'                For j = 0.0# To n - 1.0# Step 1
'                    If Alpha = 0.0# Then
'                        V = 0.0#
'                    Else
'                        i1_ = (JB) - (IA)
'                        V = 0.0#
'                        For i_ = IA To IA + K - 1.0# Step 1
'                            V = V + A(i_, JA + i) * B(IB + j, i_ + i1_)
'                        Next i_
'                    End If
'                    If Beta = 0.0# Then
'                        C(IC + i, JC + j) = Alpha * V
'                    Else
'                        C(IC + i, JC + j) = Beta * C(IC + i, JC + j) + Alpha * V
'                    End If
'                Next j
'            Next i
'            Exit Sub
'        End If
'        If OpTypeA <> 0.0# And OpTypeB = 0.0# Then

'            '
'            ' A'*B
'            '
'            If Beta = 0.0# Then
'                For i = 0.0# To m - 1.0# Step 1
'                    For j = 0.0# To n - 1.0# Step 1
'                        C(IC + i, JC + j) = 0.0#
'                    Next j
'                Next i
'            Else
'                For i = 0.0# To m - 1.0# Step 1
'                    For i_ = JC To JC + n - 1.0# Step 1
'                        C(IC + i, i_) = Beta * C(IC + i, i_)
'                    Next i_
'                Next i
'            End If
'            If Alpha <> 0.0# Then
'                For j = 0.0# To K - 1.0# Step 1
'                    For i = 0.0# To m - 1.0# Step 1
'                        V = Alpha * A(IA + j, JA + i)
'                        i1_ = (JB) - (JC)
'                        For i_ = JC To JC + n - 1.0# Step 1
'                            C(IC + i, i_) = C(IC + i, i_) + V * B(IB + j, i_ + i1_)
'                        Next i_
'                    Next i
'                Next j
'            End If
'            Exit Sub
'        End If
'    End Sub

'    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'    'Fast kernel
'    '
'    '  -- ALGLIB routine --
'    '     19.01.2010
'    '     Bochkanov Sergey
'    '
'    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'    Public Function RMatrixGEMMF(ByVal m As Long, _
'             ByVal n As Long, _
'             ByVal K As Long, _
'             ByVal Alpha As Double, _
'             ByRef A(,) As Double, _
'             ByVal IA As Long, _
'             ByVal JA As Long, _
'             ByVal OpTypeA As Long, _
'             ByRef B(,) As Double, _
'             ByVal IB As Long, _
'             ByVal JB As Long, _
'             ByVal OpTypeB As Long, _
'             ByVal Beta As Double, _
'             ByRef C(,) As Double, _
'             ByVal IC As Long, _
'             ByVal JC As Long) As Boolean
'        Dim Result As Boolean

'        Result = False

'        RMatrixGEMMF = Result
'    End Function

'    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'    'Splits matrix length in two parts, left part should match ABLAS block size
'    '
'    'INPUT PARAMETERS
'    '    A   -   real matrix, is passed to ensure that we didn't split
'    '            complex matrix using real splitting subroutine.
'    '            matrix itself is not changed.
'    '    N   -   length, N>0
'    '
'    'OUTPUT PARAMETERS
'    '    N1  -   length
'    '    N2  -   length
'    '
'    'N1+N2=N, N1>=N2, N2 may be zero
'    '
'    '  -- ALGLIB routine --
'    '     15.12.2009
'    '     Bochkanov Sergey
'    '
'    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'    Public Sub ABLASSplitLength(ByRef A() As Double, _
'             ByVal n As Long, _
'             ByRef N1 As Long, _
'             ByRef N2 As Long)

'        If n > ABLASBlockSize(A) Then
'            Call ABLASInternalSplitLength(n, ABLASBlockSize(A), N1, N2)
'        Else
'            Call ABLASInternalSplitLength(n, ABLASMicroBlockSize(), N1, N2)
'        End If
'    End Sub

'    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'    'Complex ABLASSplitLength
'    '
'    '  -- ALGLIB routine --
'    '     15.12.2009
'    '     Bochkanov Sergey
'    '
'    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'    Private Sub ABLASInternalSplitLength(ByVal n As Long, _
'             ByVal NB As Long, _
'             ByRef N1 As Long, _
'             ByRef N2 As Long)
'        Dim R As Long

'        If n <= NB Then

'            '
'            ' Block size, no further splitting
'            '
'            N1 = n
'            N2 = 0.0#
'        Else

'            '
'            ' Greater than block size
'            '
'            If n Mod NB <> 0.0# Then

'                '
'                ' Split remainder
'                '
'                N2 = n Mod NB
'                N1 = n - N2
'            Else

'                '
'                ' Split on block boundaries
'                '
'                N2 = n \ 2.0#
'                N1 = n - N2
'                If N1 Mod NB = 0.0# Then
'                    Exit Sub
'                End If
'                R = NB - N1 Mod NB
'                N1 = N1 + R
'                N2 = N2 - R
'            End If
'        End If
'    End Sub

'    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'    'Complex ABLASSplitLength
'    '
'    '  -- ALGLIB routine --
'    '     15.12.2009
'    '     Bochkanov Sergey
'    '
'    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'    Private Sub ABLASInternalSplitLength(ByVal n As Long, _
'             ByVal NB As Long, _
'             ByRef N1 As Long, _
'             ByRef N2 As Long)
'        Dim R As Long

'        If n <= NB Then

'            '
'            ' Block size, no further splitting
'            '
'            N1 = n
'            N2 = 0.0#
'        Else

'            '
'            ' Greater than block size
'            '
'            If n Mod NB <> 0.0# Then

'                '
'                ' Split remainder
'                '
'                N2 = n Mod NB
'                N1 = n - N2
'            Else

'                '
'                ' Split on block boundaries
'                '
'                N2 = n \ 2.0#
'                N1 = n - N2
'                If N1 Mod NB = 0.0# Then
'                    Exit Sub
'                End If
'                R = NB - N1 Mod NB
'                N1 = N1 + R
'                N2 = N2 - R
'            End If
'        End If
'    End Sub

'    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'    'Microblock size
'    '
'    '  -- ALGLIB routine --
'    '     15.12.2009
'    '     Bochkanov Sergey
'    '
'    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'    Public Function ABLASMicroBlockSize() As Long
'        Dim Result As Long

'        Result = 8.0#

'        ABLASMicroBlockSize = Result
'    End Function


'End Module
