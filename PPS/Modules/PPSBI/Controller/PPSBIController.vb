' PPSBIController.vb
' 
' - Currently PPSBI user defined function call back
' - Manage the checks, launch computing and return formula result
'
' To do:
'      - Finalize version check
'      - Additional check -> period wihtin the right range
'      - Simplification : loop through optional parameters
'      - Version and currency dllcomputerinstance current checks to add when those params are operationals
'      - Must be able to identify dates more globally -> regex for example     

'  Known bugs:
'       - if input = range and no value -> error !!! Fix ASAP ! (should be fixed -> check
'           -> Add antibug -> if nothing into a range param: consider as no param
'       - Version à la place de "Currency"
'
'
' Author: Julien Monnereau
' Last modified: 20/02/2015
'



Imports Microsoft.Office.Interop
Imports System.Collections
Imports System.Collections.Generic


Friend Class PPSBIController


#Region "Instance Variables"

    ' Objects
    Private ESB As EntitiesSelectionBuilderClass


    ' Variables
    Private AccountsNameKeyDictionary As Hashtable
    Private EntitiesNameKeyDictionary As Hashtable
    Private CategoriesNameKeyDictionary As Hashtable
    Private emptyCellFlag As Boolean
    Friend filterList As New List(Of String)


#End Region


    Protected Friend Sub New()

        ESB = New EntitiesSelectionBuilderClass

        AccountsNameKeyDictionary = AccountsMapping.GetAccountsDictionary(ACCOUNT_NAME_VARIABLE, ACCOUNT_ID_VARIABLE)
        EntitiesNameKeyDictionary = EntitiesMapping.GetEntitiesDictionary(ASSETS_NAME_VARIABLE, ASSETS_TREE_ID_VARIABLE)
        CategoriesNameKeyDictionary = CategoriesMapping.GetCategoriesDictionary(CATEGORY_NAME_VARIABLE, CATEGORY_ID_VARIABLE)
        emptyCellFlag = False

    End Sub


    ' Period input: date as integer 
    Public Function getDataCallBack(ByRef entity As Object, _
                                    ByRef account As Object, _
                                    ByRef period As Object, _
                                    ByRef currency_ As Object, _
                                    ByRef Version As Object, _
                                    Optional ByRef filter1 As Object = Nothing, _
                                    Optional ByRef filter2 As Object = Nothing, _
                                    Optional ByRef filter3 As Object = Nothing, _
                                    Optional ByRef filter4 As Object = Nothing, _
                                    Optional ByRef filter5 As Object = Nothing, _
                                    Optional ByRef filter6 As Object = Nothing, _
                                    Optional ByRef filter7 As Object = Nothing, _
                                    Optional ByRef filter8 As Object = Nothing, _
                                    Optional ByRef filter9 As Object = Nothing, _
                                    Optional ByRef filter10 As Object = Nothing, _
                                    Optional ByRef filter11 As Object = Nothing) As Object

        Dim entityString As String
        Dim accountString As String
        Dim entity_id As String
        Dim accountKey As String
        Dim periodString As String
        Dim periodInteger As Integer
        Dim currencyString As String
        Dim version_id As String
        emptyCellFlag = False

        entityString = ReturnValueFromRange(entity)
        accountString = ReturnValueFromRange(account)
        periodString = ReturnValueFromRange(period)
        currencyString = ReturnValueFromRange(currency_)

        filterList.Clear()
        If Not filter1 Is Nothing Then AddFilterValueToFiltersList(filter1)
        If Not filter2 Is Nothing Then AddFilterValueToFiltersList(filter2)
        If Not filter3 Is Nothing Then AddFilterValueToFiltersList(filter3)
        If Not filter4 Is Nothing Then AddFilterValueToFiltersList(filter4)
        If Not filter5 Is Nothing Then AddFilterValueToFiltersList(filter5)
        If Not filter6 Is Nothing Then AddFilterValueToFiltersList(filter6)
        If Not filter7 Is Nothing Then AddFilterValueToFiltersList(filter7)
        If Not filter8 Is Nothing Then AddFilterValueToFiltersList(filter8)
        If Not filter9 Is Nothing Then AddFilterValueToFiltersList(filter9)
        If Not filter10 Is Nothing Then AddFilterValueToFiltersList(filter10)
        If Not filter11 Is Nothing Then AddFilterValueToFiltersList(filter11)

        ' Checks
        If AccountsNameKeyDictionary.ContainsKey(accountString) Then
            accountKey = AccountsNameKeyDictionary.Item(accountString)
        Else
            Return "Account not registered"
        End If

        If EntitiesNameKeyDictionary.ContainsKey(entityString) Then
            entity_id = EntitiesNameKeyDictionary.Item(entityString)
        Else
            Return "Entity not registered"
        End If

        If emptyCellFlag = True Then
            Return "One of the function parameters is empty"
        End If

        If CheckVersion(Version, version_id) = False Then
            Return "Invalid version name. Please check spelling or refer to the versions interface."
        End If

        ESB.BuildCategoriesFilterFromFilterList(filterList)

        If GENERICDCGLobalInstance.current_entity_id <> entity_id _
        Or GENERICDCGLobalInstance.currentStrSqlQuery <> ESB.StrSqlQuery _
        Or GENERICDCGLobalInstance.current_version_id <> version_id _
        Or GENERICDCGLobalInstance.currentCurrency <> currencyString _
        Then
            GENERICDCGLobalInstance.ComputeAggregatedEntity(entity_id, _
                                                            version_id, _
                                                            currencyString, _
                                                            ESB.StrSqlQuery)
        End If

        If CheckDate(period, periodInteger, GENERICDCGLobalInstance.period_list) = False Then
            Return "Invalid Period or Period format"
        End If

        Try
            Return GENERICDCGLobalInstance.GetDataFromDLL3Computer(accountKey, periodInteger)
        Catch ex As Exception
            Return "Invalid parameters"
        End Try

    End Function


#Region "Utilities"

    ' Return the value in a range or the simple value if not range
    Private Function ReturnValueFromRange(ByRef input As Object) As Object

        If TypeOf (input) Is Excel.Range Then
            Dim rng As Excel.Range = CType(input, Excel.Range)
            If rng.Value2 Is Nothing Then emptyCellFlag = True
            Return rng.Value2
        Else
            Return input
        End If

    End Function

    Private Function CheckVersion(ByRef version_name As Object, _
                                  ByRef version_id As String) As String

        If Not version_name Is Nothing Then
            Dim versionString As String = ReturnValueFromRange(version_name)
            version_id = VersionsMapping.GetVersionsIDFromName(versionString)
            If version_id <> "" Then Return True Else Return False
        Else
            version_id = GLOBALCurrentVersionCode
            Return True
        End If

    End Function

    Private Function CheckDate(ByRef input_period_object As Object, _
                               ByRef periodInteger As Integer, _
                               ByRef periodslist As List(Of Int32)) As Boolean

        Dim periodstr As String = ReturnValueFromRange(input_period_object)
        If IsDate(periodstr) Then
            Dim periodAsDate As Date = CDate(periodstr)
            If periodslist.Contains(periodAsDate.ToOADate) Then
                periodInteger = periodAsDate.ToOADate
                Return True
            Else
                Return False
            End If
        Else
            If periodslist.Contains(periodstr) Then
                periodInteger = periodstr
                Return True
            Else
                Select Case GENERICDCGLobalInstance.time_config
                    Case MONTHLY_TIME_CONFIGURATION
                        For Each period As Integer In periodslist
                            If Month(DateTime.FromOADate(period)) = periodstr Then
                                periodInteger = period
                                periodstr = DateTime.FromOADate(period)
                                Return True
                            End If
                        Next
                    Case YEARLY_TIME_CONFIGURATION
                        If IsNumeric(periodstr) Then
                            For Each period As Integer In periodslist
                                If Year(DateTime.FromOADate(period)) = periodstr Then
                                    periodInteger = period
                                    Return True
                                End If
                            Next
                        End If
                End Select
            End If

        End If
        Return False

    End Function

    Private Sub AddFilterValueToFiltersList(ByRef filter As Object)

        Dim filterValue As String = ReturnValueFromRange(filter)
        If filterValue.Contains(",") = True Then
            For Each value As String In filterValue.Split(PPSBI_FORMULA_CATEGORIES_SEPARATOR)
                If CategoriesNameKeyDictionary.ContainsKey(value) Then filterList.Add(CategoriesNameKeyDictionary.Item(value))
            Next
        Else
            If CategoriesNameKeyDictionary.ContainsKey(filterValue) Then filterList.Add(CategoriesNameKeyDictionary.Item(filterValue))
        End If

    End Sub

#End Region




End Class
