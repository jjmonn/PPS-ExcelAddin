' PPSBIController.vb
' 
' - PPSBI user defined function call back
' 
'
' To do:
'      - Simplification : loop through optional parameters
'      - Version and currency dllcomputerinstance current checks to add when those params are operationals
'      - Must be able to identify dates more globally -> regex for example     
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
    Private computingCache As New ComputingCache(False)

    ' Variables
    Private requestIdComputeFlagDict As New Dictionary(Of Int32, Boolean)
    Private emptyCellFlag As Boolean
    Friend filterList As New List(Of String)
    Friend mustUpdateFlag As Boolean = False

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
    Friend Function GetDataCallBack(ByRef p_entity_str As Object, _
                                   ByRef p_account_str As Object, _
                                   ByRef p_period_str As Object, _
                                   ByRef p_currency_str As Object, _
                                   ByRef p_version_str As Object, _
                                   Optional ByRef p_adjustment_str As Object = Nothing, _
                                   Optional ByRef p_filtersArray As Object = Nothing) As Object

        Dim entityString, accountString, currencyString, adjustmentString, versionString, periodToken, error_message As String
        Dim entity_id, account_id, currency_id, version_id, adjustment_id As Int32

        emptyCellFlag = False
        entityString = ReturnValueFromRange(p_entity_str)
        accountString = ReturnValueFromRange(p_account_str)
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

        If CheckDate(p_period_str, periodToken, version_id, error_message) = False Then
            Return error_message
        End If

        Dim token As String = version_id & Computer.TOKEN_SEPARATOR & _
                               "0" & Computer.TOKEN_SEPARATOR & _
                               entity_id & Computer.TOKEN_SEPARATOR & _
                               account_id & Computer.TOKEN_SEPARATOR & _
                               periodToken

        ' Computing order
        If mustUpdateFlag = False _
        AndAlso computingCache.CheckCache(entity_id, _
                                          currency_id, _
                                         {version_id}, _
                                         filters, _
                                         axis_filters) = True Then

            Dim request_id As Int32 = Computer.CMSG_COMPUTE_REQUEST({version_id}, _
                                                                     entity_id, _
                                                                     currency_id, _
                                                                     filters, _
                                                                     axis_filters, _
                                                                     Nothing)
            requestIdComputeFlagDict.Add(request_id, False)
            While requestIdComputeFlagDict(request_id) = False
                ' timeout ? priority high
            End While
            ' Cache registering
            computingCache.cacheEntityID = entity_id
            computingCache.cacheCurrencyId = currency_id
            computingCache.cacheVersions = {version_id}
            computingCache.cacheFilters = filters
            computingCache.cacheAxisFilters = axis_filters
            mustUpdateFlag = False
            GoTo ReturnData
        Else
            GoTo ReturnData
        End If

ReturnData:
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
            ' if my.settings version valid
            ' else -> error message = "set up version
            ' priority normal"
            version_id = My.Settings.version_id
            Return True
        End If

    End Function

    Private Function CheckDate(ByRef p_period_str As Object, _
                               ByRef periodToken As String, _
                               ByRef versionId As Int32, _
                               ByRef errorMessage As String) As Boolean

        Dim periodstr As String = ReturnValueFromRange(p_period_str)
        If IsDate(periodstr) Then
            Dim periodAsInt As Int32 = CDate(periodstr).ToOADate
            Dim periodsList() As Int32 = GlobalVariables.Versions.GetPeriodsList(versionId)
            Dim periodIdentifyer As Char
            Select Case GlobalVariables.Versions.versions_hash(VERSION_ID_VARIABLE)(VERSIONS_TIME_CONFIG_VARIABLE)
                Case GlobalEnums.TimeConfig.YEARS
                    periodIdentifyer = Computer.YEAR_PERIOD_IDENTIFIER
                Case GlobalEnums.TimeConfig.MONTHS
                    periodIdentifyer = Computer.MONTH_PERIOD_IDENTIFIER
            End Select
            For Each p In periodsList
                If p = periodAsInt Then
                    periodToken = periodIdentifyer & periodAsInt
                    Return True
                End If
            Next
        End If
        errorMessage = "Invalid Period or Period format"
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
