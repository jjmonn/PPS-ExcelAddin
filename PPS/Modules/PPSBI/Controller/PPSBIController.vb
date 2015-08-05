' PPSBIController.vb
' 
' - Currently PPSBI user defined function call back
' - Manage the checks, launch computing and return formula result
'
' To do:
'      - Simplification : loop through optional parameters
'      - Version and currency dllcomputerinstance current checks to add when those params are operationals
'      - Must be able to identify dates more globally -> regex for example     
'       - EntitiesTV can be stored in aggregation computer
'
'
'  Known bugs:
'      
'
' Author: Julien Monnereau
' Last modified: 17/07/2015
'


Imports Microsoft.Office.Interop
Imports System.Collections
Imports System.Collections.Generic
Imports System.Windows.Forms


Friend Class PPSBIController


#Region "Instance Variables"

    ' Objects
  
    ' Variables
    Private AccountsNameKeyDictionary As Hashtable
    Private EntitiesNameKeyDictionary As Hashtable
    Private EntitiesCategoriesNameKeyDictionary As Hashtable
    Private Adjustments_name_id_dic As Hashtable
    Private emptyCellFlag As Boolean
    Friend filterList As New List(Of String)
    Private aggregation_computed_accounts_types As New List(Of String)

#End Region


#Region "Initialize"

    Friend Sub New()

        AccountsNameKeyDictionary = GlobalVariables.Accounts.GetAccountsDictionary(NAME_VARIABLE, ID_VARIABLE)
        EntitiesNameKeyDictionary = GlobalVariables.Entities.GetEntitiesDictionary(NAME_VARIABLE, ID_VARIABLE)
        EntitiesCategoriesNameKeyDictionary = GlobalVariables.FiltersValues.GetFiltervaluesDictionary(GlobalEnums.AnalysisAxis.ENTITIES, NAME_VARIABLE, ID_VARIABLE)
        Adjustments_name_id_dic = GlobalVariables.Adjustments.GetAdjustmentsDictionary(NAME_VARIABLE, ID_VARIABLE)
        emptyCellFlag = False
        aggregation_computed_accounts_types.Add(GlobalEnums.FormulaTypes.FORMULA)
        aggregation_computed_accounts_types.Add(GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT)
        aggregation_computed_accounts_types.Add(GlobalEnums.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS)

    End Sub

#End Region


#Region "Interface"


    ' Stubs in this function - clients/ products adjustments filters should come as param or computed here ?!!!
    ' Period input: date as integer 
    Friend Function getDataCallBack(ByRef entity_name As Object, _
                                    ByRef account As Object, _
                                    ByRef period As Object, _
                                    ByRef currency As Object, _
                                    ByRef version As Object, _
                                    Optional ByRef adjustment_filter As Object = Nothing, _
                                    Optional ByRef filtersArray As Object = Nothing) As Object

        Dim entityString, entity_id, account_id, accountString, periodString, currencyString, version_id, adjustment_string, adjustment_id, error_message As String
        emptyCellFlag = False
        entityString = ReturnValueFromRange(entity_name)
        accountString = ReturnValueFromRange(account)
        periodString = ReturnValueFromRange(period)
        currencyString = ReturnValueFromRange(currency)
        adjustment_string = ReturnValueFromRange(adjustment_filter)

        filterList.Clear()
        If Not filtersArray Is Nothing Then
            For Each filter_ In filtersArray
                If Not filter_ Is Nothing Then AddFilterValueToFiltersList(filter_)
            Next
        End If

        If CheckParameters(accountString, account_id, _
                           entityString, entity_id, _
                           adjustment_string, adjustment_id, _
                           version, version_id, _
                           error_message) _
                           = False Then Return error_message

        ' construction des clients_id_filter
        '                  proudcts_id_filters
        ' STUB !!!
        Dim clients_id_filters As New List(Of String)
        Dim products_id_filters As New List(Of String)
        Dim adjustments_id_filters As New List(Of String)

        '   Dim entity_node As TreeNode = getEntityNode(entity_id)
        '  If entity_node Is Nothing Then Return "Entity not in selection"

        'If CheckDate(period, period_int, GlobalVariables.GenericGlobalSingleEntityComputer.period_list) = False Then
        '    Return "Invalid Period or Period format"
        'End If

        ' launch computation => computer.vb

        'ComputeViaAggregationComputer(entity_node, _
        '                                      account_id, _
        '                                      version_id, _
        '                                      currencyString, _
        '                                      period, _
        '                                      clients_id_filters, _
        '                                      products_id_filters, _
        '                                      adjustments_id_filters)
       

    End Function

  
#End Region


#Region "Checks"

    Private Function CheckParameters(ByRef accountstring As String, _
                                     ByRef account_id As String, _
                                     ByRef entityString As String, _
                                     ByRef entity_id As String, _
                                     ByRef adjustmentstring As String, _
                                     ByRef adjustment_id As String, _
                                     ByRef version As Object, _
                                     ByRef version_id As String, _
                                     ByRef error_message As String) As Boolean

        If AccountsNameKeyDictionary.ContainsKey(accountstring) Then
            account_id = AccountsNameKeyDictionary.Item(accountstring)
        Else
            error_message = "Invalid Account"
            Return False
        End If

        If EntitiesNameKeyDictionary.ContainsKey(entityString) Then
            entity_id = EntitiesNameKeyDictionary.Item(entityString)
        Else
            error_message = "Invalid Entity"
            Return False
        End If

        If adjustmentstring <> "" Then
            If Adjustments_name_id_dic.ContainsKey(adjustmentstring) Then
                adjustment_id = Adjustments_name_id_dic(adjustmentstring)
            Else
                error_message = "Invalid Adjustment"
                Return False
            End If
        Else
            adjustment_id = ""
        End If

        If emptyCellFlag = True Then
            error_message = "Missing Parameter"
            Return False
        End If

        If CheckVersion(version, version_id) = False Then
            error_message = "Invalid version"
            Return False
        End If
        Return True

    End Function

    Private Function CheckVersion(ByRef version_name As Object, _
                                  ByRef version_id As String) As String

        If Not version_name Is Nothing Then
            Dim versionString As String = ReturnValueFromRange(version_name)
            version_id = GlobalVariables.Versions.GetVersionsIDFromName(versionString)
            If version_id <> "" Then Return True Else Return False
        Else
            version_id = GlobalVariables.GLOBALCurrentVersionCode
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
            If Not periodslist Is Nothing Then
                If periodslist.Contains(periodstr) Then
                    periodInteger = periodstr
                    Return True
                Else

                    ' period check to be reimplemented !!

                End If
            End If
            End If
        Return False

    End Function

#End Region


#Region " Selections Builders"

    'Private Function getEntityNode(ByRef entity_id As String) As TreeNode

    '    Dim entitiesTV As New TreeView      ' Store in computers ?
    '    ESB.BuildCategoriesFilterFromFilterList(filterList)
    '    Globalvariables.Entities.LoadEntitiesTV(entitiesTV, ESB.StrSqlQueryForEntitiesUploadFunctions)
    '    Dim lookup_result As TreeNode() = entitiesTV.Nodes.Find(entity_id, True)

    '    ' !!! Result even if entity_id not in selection ! -> we still have the children !

    '    If lookup_result.Length > 0 Then Return lookup_result(0) Else Return Nothing

    'End Function



#End Region


#Region "Utilities"

    Private Function ReturnValueFromRange(ByRef input As Object) As Object

        If input Is Nothing Then Return ""
        If TypeOf (input) Is Excel.Range Then
            Dim rng As Excel.Range = CType(input, Excel.Range)
            If rng.Value2 Is Nothing Then emptyCellFlag = True
            Return rng.Value2
        Else
            Return input
        End If

    End Function

    Private Sub AddFilterValueToFiltersList(ByRef filter As Object)

        Dim filterValue As String = ReturnValueFromRange(filter)
        If Not filterValue Is Nothing AndAlso EntitiesCategoriesNameKeyDictionary.ContainsKey(filterValue) Then filterList.Add(EntitiesCategoriesNameKeyDictionary.Item(filterValue))
      
    End Sub

#End Region


End Class
