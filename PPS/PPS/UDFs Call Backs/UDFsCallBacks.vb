Imports Microsoft.Office.Interop

Module UDFsCallBacks

    Public Function getDataCallBack(entity As Object, account As Object, period As Object, currency_ As Object) As Double

        '---------------------------------------------------------------------------------------
        '
        '---------------------------------------------------------------------------------------
        Dim entityString As String
        Dim accountString As String
        Dim periodInteger As Integer
        Dim currencyString As String

        If TypeOf (entity) Is Excel.Range Then
            Dim rng As Excel.Range = CType(entity, Excel.Range)
            entityString = rng.Value2
        Else
            entityString = CType(entity, String)
        End If

        If TypeOf (account) Is Excel.Range Then
            Dim rng As Excel.Range = CType(account, Excel.Range)
            accountString = rng.Value2
        Else
            accountString = CType(account, String)
        End If

        If TypeOf (period) Is Excel.Range Then
            Dim rng As Excel.Range = CType(period, Excel.Range)
            periodInteger = rng.Value2
        Else
            periodInteger = CType(period, Integer)
        End If

        If TypeOf (currency_) Is Excel.Range Then
            Dim rng As Excel.Range = CType(currency_, Excel.Range)
            currencyString = rng.Value2
        Else
            currencyString = CType(currency_, String)
        End If

        Dim MAPP As New ModelMapping
        Dim accountKey As String = MAPP.IsAccountInMapping(accountString)
        Dim entityKey As String = MAPP.IsAssetInMapping(entityString)
        If accountKey = "" Then
            MsgBox("This account is not mapped")
            Return vbNull
            Exit Function
        End If
        If entityKey = "" Then
            MsgBox("This entity is not mapped")
            Return vbNull
            Exit Function
        End If

        Dim MGD As ModelGetData
        MGD = New ModelGetData
        getDataCallBack = MGD.PPSKeys(entityKey, accountKey, periodInteger, currencyString)

        MGD = Nothing
        MAPP = Nothing


    End Function


End Module
