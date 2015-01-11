Imports System.Linq

Public Class ModelDatabase

    '-----------------------------------------------------------------------------------------
    ' Manage requests and update on the DATA TABLE
    '-----------------------------------------------------------------------------------------

    Private rst As ADODB.Recordset
    Private srv As ModelServer
    Public Property OpeningPeriod As Integer
    Public Property periodList As Integer()                                           ' Array holding the Complete period list ?
    Public mapp As ModelMapping                                                 '        
    Public Property dataHT As New Collections.Generic.Dictionary(Of String, Collections.Generic.Dictionary(Of Integer, Double))
    Private HVInputsList() As String

    Public Sub New()
        '--------------------------------------------------------------
        ' Sub: Initialize
        '--------------------------------------------------------------
        srv = New ModelServer
        rst = New ADODB.Recordset
        mapp = New ModelMapping

        HVInputsList = mapp.getHVInputs


    End Sub

    Public Sub UpdateDataBase(InputDataArray(,) As Object)

        '--------------------------------------------------------------------------
        ' Update the Data Base with the input array (from ModelDataSet)
        '--------------------------------------------------------------------------
        Dim i As Integer
        Dim criteria As String
        Dim Fields(), Values() As Object
        Dim FormulaType() As String
        Dim Flag As Boolean
        Dim t0 As Date
        t0 = Now

        Fields = {DATA_ASSET_ID_VARIABLE, DATA_ACCOUNT_ID_TABLE, _
                  DATA_PERIOD_VARIABLE, DATA_VALUE_VARIABLE, DATA_ID_VARIABLE}

        ' Translate accounts and assets into IDs
        ReDim FormulaType(UBound(InputDataArray, 1))
        Dim Currencies(UBound(InputDataArray, 1))
        For i = LBound(InputDataArray, 1) To UBound(InputDataArray, 1)
            InputDataArray(i, DATA_ARRAY_ASSET_COLUMN) = mapp.IsAssetInMapping(CStr(InputDataArray(i, DATA_ARRAY_ASSET_COLUMN)))
            InputDataArray(i, DATA_ARRAY_ACCOUNT_COLUMN) = mapp.IsAccountInMapping(CStr(InputDataArray(i, DATA_ARRAY_ACCOUNT_COLUMN)))
            FormulaType(i) = mapp.GetFormulaType(CStr(InputDataArray(i, DATA_ARRAY_ACCOUNT_COLUMN)))       ' Save formula type
        Next i

        rst = srv.openRst(DATA_TABLE)                                               ' Open data table recordset

        ' Update DB
        For i = LBound(InputDataArray, 1) To UBound(InputDataArray, 1)

            Flag = True
            If FormulaType(i) = "BS" Then                                            ' Balance sheet accounts treatment
                If InputDataArray(i, DATA_ARRAY_PERIOD_COLUMN) = OpeningPeriod Then
                    InputDataArray(i, DATA_ARRAY_ACCOUNT_COLUMN) = InputDataArray(i, DATA_ARRAY_ACCOUNT_COLUMN) & "OB"
                Else
                    Flag = False
                End If
            End If

            If Flag = True Then
                Values = {CStr(InputDataArray(i, DATA_ARRAY_ASSET_COLUMN)) _
                              , CStr(InputDataArray(i, DATA_ARRAY_ACCOUNT_COLUMN)) _
                              , InputDataArray(i, DATA_ARRAY_PERIOD_COLUMN) _
                              , InputDataArray(i, DATA_ARRAY_DATA_COLUMN) _
                              , CStr(InputDataArray(i, DATA_ARRAY_ASSET_COLUMN)) _
                                & CStr(InputDataArray(i, DATA_ARRAY_ACCOUNT_COLUMN)) _
                                & CStr(InputDataArray(i, DATA_ARRAY_PERIOD_COLUMN))}             ' Save Values array

                ' Look for existing record through primary key
                criteria = DATA_ID_VARIABLE & "=" & "'" & Values(4) & "'"
                rst.Find(criteria, , , 1)

                If rst.EOF = True Or rst.BOF = True Then           ' If record NOT FOUND
                    rst.AddNew(Fields, Values)                       ' Add records to database
                Else
                    If rst.Fields(DATA_ARRAY_DATA_COLUMN).Value <> Values(3) Then
                        rst.Fields(DATA_ARRAY_DATA_COLUMN).Value = Values(3)
                    End If
                End If

            End If
        Next i
        rst.Update()
        rst.Close()
        MsgBox("Upload successful")
        ' PUT A LOADING PROGRESSION BAR HERE ?!!

    End Sub

    Public Sub CreateAggregDataHT(AffiliatesList() As String)

        '-------------------------------------------------------------------------
        ' Function: Create Assets Aggregated DataArray in the case of aggregated assets (OLAP QUERY)
        ' Loop through hv accounts and given periods to produce a data matrix (accounts in lines, periods in columns)
        '-------------------------------------------------------------------------
        Dim Affiliates, strSQL As String
        Dim TempValue As Double
        Dim MC As ModelCurrencies = New ModelCurrencies

        MC.RatesLoad()

        Affiliates = "'" & Join(AffiliatesList, "','") & "'"                        ' Affiliates list build

        strSQL = "SELECT " & "D." & DATA_PERIOD_VARIABLE & ", " _
                & "D." & DATA_ACCOUNT_ID_TABLE & ", " _
                & "A." & ASSETS_CURRENCY_VARIABLE & ", " _
                & "SUM(" & DATA_VALUE_VARIABLE & ")" _
                & " FROM " & DATA_TABLE & " D" & ", " & ASSETS_TABLE & " A" _
                & " WHERE " & "D." & DATA_ASSET_ID_VARIABLE & "=" & "A." & ASSETS_TREE_ID_VARIABLE _
                & " AND " & "A." & ASSETS_AFFILIATE_ID_VARIABLE & " IN " & "(" & Affiliates & ")" _
                & " GROUP BY " & "D." & DATA_PERIOD_VARIABLE & ", " _
                & "D." & DATA_ACCOUNT_ID_TABLE & ", " & "A." & ASSETS_CURRENCY_VARIABLE

        rst = srv.openRstSQL(strSQL)

        For Each account As String In HVInputsList
            Dim TempDictionary As New Collections.Generic.Dictionary(Of Integer, Double)
            For Each period As Integer In periodList

                rst.Filter = DATA_ACCOUNT_ID_TABLE & "=" & account & " And " _
                             & DATA_PERIOD_VARIABLE & "=" & period

                If rst.EOF Or rst.BOF Then              ' Item not found
                    TempDictionary.Add(period, 0)
                Else                                    ' Item found
                    TempValue = 0
                    rst.MoveFirst()
                    Do While rst.EOF = False And rst.BOF = False                ' Currencies loop
                        If rst.Fields(ASSETS_CURRENCY_VARIABLE).Value <> MAIN_CURRENCY Then
                            TempValue = TempValue + _
                                        MC.Convert(CDbl(rst.Fields("SUM(" & DATA_VALUE_VARIABLE & ")").Value), _
                                                   CStr(rst.Fields(ASSETS_CURRENCY_VARIABLE).Value), _
                                                   MAIN_CURRENCY, period)
                        Else
                            TempValue = TempValue + CDbl(rst.Fields("SUM(" & DATA_VALUE_VARIABLE & ")").Value)
                        End If
                        rst.MoveNext()
                    Loop
                    TempDictionary.Add(period, TempValue)
                End If
            Next
            dataHT.Add(account, TempDictionary)
        Next

        rst.Close()
        MC = Nothing

    End Sub





End Class
