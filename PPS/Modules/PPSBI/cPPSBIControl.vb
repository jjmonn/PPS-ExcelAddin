' UDFsCallBacks
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
' Last modified: 08/09/2014
'



Imports Microsoft.Office.Interop
Imports System.Collections
Imports System.Collections.Generic

Public Class cPPSBIControl


#Region "Instance Variables"

    Private AccountsNameKeyDictionary As Hashtable
    Private EntitiesNameKeyDictionary As Hashtable
    Private CategoriesNameKeyDictionary As Hashtable
    Private emptyCellFlag As Boolean
    Private ESB As EntitiesSelectionBuilderClass
    Friend filterList As New List(Of String)


#End Region


    Public Sub New()

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
        Dim entityKey As String
        Dim accountKey As String
        Dim periodString As String
        Dim periodInteger As Integer
        Dim currencyString As String
        Dim versionCode As String
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
            entityKey = EntitiesNameKeyDictionary.Item(entityString)
        Else
            Return "Entity not registered"
        End If

        If emptyCellFlag = True Then
            Return "One of the function parameters is empty"
        End If

        If CheckVersion(Version, versionCode) = False Then
            Return "Invalid version name. Please check spelling or refer to the versions interface."
        End If
        If CheckDate(periodString, periodInteger, versionCode) = False Then
            Return "Invalid Period. The period format must be dd/mm/yyyy (e.g. 31/12/2014)."
        End If

        ESB.BuildCategoriesFilterFromFilterList(filterList)

        If GENERICDCGLobalInstance.currentEntityCode <> entityKey _
        Or GENERICDCGLobalInstance.currentStrSqlQuery <> ESB.StrSqlQuery _
        Or GENERICDCGLobalInstance.currentVersionCode <> versionCode _
        Or GENERICDCGLobalInstance.currentCurrency <> currencyString _
        Then
            '  NEW COMPUTATION (currencies) IMPLMEENTATION !!!!!
            '  GENERICDCGLobalInstance.ComputeSingleEntity(versionCode, entityKey, currencyString, ESB.StrSqlQuery)
        End If


        Return GENERICDCGLobalInstance.GetDataFromComputer(accountKey, periodInteger, currencyString)

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

    Private Function CheckVersion(ByRef version As Object, _
                                  ByRef versionCode As String) As String

        If Not version Is Nothing Then
            Dim versionString As String = ReturnValueFromRange(version)
            ' need to check if data version exists !!
            ' get version code from versionmapping !!!
            versionCode = ""
            ' must return true if ok
        Else
            versionCode = GLOBALCurrentVersionCode
            Return True
        End If

    End Function

    Private Function CheckDate(ByRef periodStr As String, _
                               ByRef periodInteger As Integer, _
                               ByRef versionCode As String) As Boolean

        Dim periodsList As List(Of Integer) = GENERICDCGLobalInstance.VERSIONSMGT.GetPeriodList(versionCode)

        If IsDate(periodStr) Then
            Dim periodAsDate As Date = CDate(periodStr)

            If periodsList.Contains(periodAsDate.ToOADate) Then
                periodInteger = periodAsDate.ToOADate
                Return True
            Else
                Return False
            End If

        Else
            If periodsList.Contains(periodStr) Then
                periodInteger = periodStr
                Return True
            Else
                Select Case GENERICDCGLobalInstance.VERSIONSMGT.versionsCodeTimeSetUpDict(versionCode)(VERSIONS_TIME_CONFIG_VARIABLE)
                    Case MONTHLY_TIME_CONFIGURATION
                        For Each period As Integer In periodsList
                            If Month(DateTime.FromOADate(period)) = periodStr Then
                                periodInteger = period
                                periodStr = DateTime.FromOADate(period)
                                Return True
                            End If
                        Next
                    Case YEARLY_TIME_CONFIGURATION
                        If IsNumeric(periodStr) Then
                            For Each period As Integer In periodsList
                                If Year(DateTime.FromOADate(period)) = periodStr Then
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
