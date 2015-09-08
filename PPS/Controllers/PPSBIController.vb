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
' Last modified: 08/09/2015
'


Imports Microsoft.Office.Interop
Imports System.Collections
Imports System.Collections.Generic
Imports System.Windows.Forms


Friend Class PPSBIController


#Region "Instance Variables"

    ' Objects
    Private Computer As New Computer

    ' Variables
    Private requestIdComputeFlagDict As New Dictionary(Of Int32, Boolean)
    Private emptyCellFlag As Boolean
    Friend filterList As New List(Of String)
   
#End Region


#Region "Initialize"

    Friend Sub New()

        emptyCellFlag = False
       AddHandler Computer.ComputationAnswered, AddressOf AfterCompute

    End Sub

#End Region


#Region "Interface"

    ' Stubs in this function - clients/ products adjustments filters should come as param or computed here ?!!!
    ' Period input: date as integer 
    Friend Function getDataCallBack(ByRef p_entity_str As Object, _
                                   ByRef p_account_str As Object, _
                                   ByRef p_period_str As Object, _
                                   ByRef p_currency_str As Object, _
                                   ByRef p_version_str As Object, _
                                   Optional ByRef p_adjustment_str As Object = Nothing, _
                                   Optional ByRef p_filtersArray As Object = Nothing) As Object

        Dim entityString, accountString, periodString, currencyString, adjustmentString, versionString, error_message As String
        Dim entity_id, account_id, currency_id, version_id, adjustment_id, period As Int32

        emptyCellFlag = False
        entityString = ReturnValueFromRange(p_entity_str)
        accountString = ReturnValueFromRange(p_account_str)
        periodString = ReturnValueFromRange(p_period_str)
        currencyString = ReturnValueFromRange(p_currency_str)
        adjustmentString = ReturnValueFromRange(p_adjustment_str)
        versionString = ReturnValueFromRange(p_version_str)

        filterList.Clear()
        'If Not p_filtersArray Is Nothing Then
        '    For Each filter_ In p_filtersArray
        '        If Not filter_ Is Nothing Then AddFilterValueToFiltersList(filter_)
        '    Next
        'End If

        If CheckParameters(accountString, account_id, _
                           entityString, entity_id, _
                           adjustmentString, adjustment_id, _
                           versionString, version_id, _
                           error_message) _
                           = False Then
            Return error_message
        End If


        ' STUB !!! filters priority high !!
        Dim filters As Object = Nothing
        Dim axis_filters As Object = Nothing

        ' check periods to be reimplemented !!
        'If CheckDate(Period, period_int, GlobalVariables.GenericGlobalSingleEntityComputer.period_list) = False Then
        '    Return "Invalid Period or Period format"
        'End If
        period = "y42004"

        Dim token As String = version_id & Computer.TOKEN_SEPARATOR & _
                               "0" & Computer.TOKEN_SEPARATOR & _
                               entity_id & Computer.TOKEN_SEPARATOR & _
                               account_id & Computer.TOKEN_SEPARATOR & _
                               period

        Dim request_id As Int32 = Computer.CMSG_COMPUTE_REQUEST({version_id}, _
                                                                 entity_id, _
                                                                 currency_id, _
                                                                 filters, _
                                                                 axis_filters, _
                                                                 Nothing)
        requestIdComputeFlagDict.Add(request_id, False)
        While requestIdComputeFlagDict(request_id) = False
            ' timeout ?
        End While
        If Computer.GetData.ContainsKey(token) Then
            Return Computer.GetData(token)
        Else
            Return "Unable to Compute"
        End If

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

        account_id = GlobalVariables.Accounts.GetIdFromName(accountstring)
        If account_id = 0 Then
            error_message = "Invalid Account"
            Return False
        End If

        entity_id = GlobalVariables.Entities.GetEntityId(entityString)
        If entity_id = 0 Then
            error_message = "Invalid Entity"
            Return False
        End If

        If adjustmentstring <> "" Then
            adjustment_id = GlobalVariables.Adjustments.GetAxisId(adjustmentstring)
            If adjustment_id = 0 Then
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
            version_id = My.Settings.version_id
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


#Region "Events"

    Private Sub AfterCompute(ByRef entity_id As Int32, ByRef status As Boolean, ByRef request_id As Int32)

        If status = True Then
            requestIdComputeFlagDict(request_id) = True
        End If

    End Sub

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

    'Private Sub AddFilterValueToFiltersList(ByRef filter As Object)

    '    Dim filterValue As String = ReturnValueFromRange(filter)
    '    If Not filterValue Is Nothing AndAlso EntitiesCategoriesNameKeyDictionary.ContainsKey(filterValue) Then filterList.Add(EntitiesCategoriesNameKeyDictionary.Item(filterValue))

    'End Sub

#End Region


End Class
