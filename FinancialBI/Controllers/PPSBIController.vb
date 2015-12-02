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
' Last modified: 09/09/2015
'


Imports Microsoft.Office.Interop
Imports System.Collections
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Linq
Imports CRUD

Public Class PPSBIController


#Region "Instance Variables"

    ' Objects
    Private Computer As New Computer
    Private computingCache As ComputingCache

    ' Compute Params
    Private periodToken As String
    Private entity_id As Int32
    Private account_id As Int32
    Private currency_id As Int32
    Private m_versionId As Int32
    Private adjustment_id As Int32

    ' Variables
    Private error_message As String
    Private requestIdComputeFlagDict As New SafeDictionary(Of Int32, Boolean)
    Private emptyCellFlag As Boolean
    Private cacheInitFlag As Boolean = False
    Friend mustUpdateFlag As Boolean = False

#End Region


#Region "Initialize"

    Friend Sub New()

        emptyCellFlag = False
        AddHandler Computer.ComputationAnswered, AddressOf AfterCompute
        If GlobalVariables.AuthenticationFlag = True Then InitCache()

    End Sub

    Private Sub InitCache()

        computingCache = New ComputingCache(False)
        cacheInitFlag = True

    End Sub

#End Region


#Region "Interface"

    Friend Sub ReinitializeCache()

        computingCache.ResetCache()

    End Sub

    Friend Function GetDataCallBack(ByRef p_entity_str As Object, _
                                   ByRef p_account_str As Object, _
                                   ByRef p_period_str As Object, _
                                   ByRef p_currency_str As Object, _
                                   ByRef p_version_str As Object, _
                                   ByRef p_clients_filters As Object, _
                                   ByRef p_products_filters As Object, _
                                   ByRef p_adjustments_filters As Object, _
                                   Optional ByRef p_filtersArray As Object = Nothing) As Object

        If cacheInitFlag = False Then InitCache()
        If GlobalVariables.g_mustResetCache = True Then ReinitializeCache()
        error_message = ""
        emptyCellFlag = False

        ' Checks
        If CheckAccount(p_account_str) = False Then Return error_message
        If CheckEntity(p_entity_str) = False Then Return error_message
        If CheckCurrency(p_currency_str) = False Then Return error_message
        If CheckVersion(p_version_str) = False Then Return error_message
        If CheckDate(p_period_str) = False Then Return error_message

        ' Filters Building
        Dim filters = New SafeDictionary(Of Int32, List(Of Int32))
        BuildFilters(p_filtersArray, filters)
        Dim filtersToken As String = "0"

        ' Axis Filters building
        Dim axis_filters = New SafeDictionary(Of Int32, List(Of Int32))()
        BuildAxisFilter(p_clients_filters, GlobalVariables.AxisElems, AxisType.Client, axis_filters)
        BuildAxisFilter(p_products_filters, GlobalVariables.AxisElems, AxisType.Product, axis_filters)
        BuildAxisFilter(p_adjustments_filters, GlobalVariables.AxisElems, AxisType.Adjustment, axis_filters)

        ' -> filter token to be created !!!
        Dim token As String = m_versionId & Computer.TOKEN_SEPARATOR & _
                              filtersToken & Computer.TOKEN_SEPARATOR & _
                              entity_id & Computer.TOKEN_SEPARATOR & _
                              account_id & Computer.TOKEN_SEPARATOR & _
                              periodToken

        ' Check Cache and Compute if necessary
        If mustUpdateFlag = False _
        AndAlso computingCache.MustCompute(entity_id, _
                                            currency_id, _
                                            {m_versionId}, _
                                            filters, _
                                            axis_filters) = True Then

            Compute(filters, axis_filters)
        End If

        ' Return Data
        If Computer.GetData.ContainsKey(token) Then
            Return Computer.GetData(token)
        Else
            Return 0
        End If

    End Function

    Private Sub Compute(ByRef filters As Dictionary(Of Int32, List(Of Int32)), _
                        ByRef axis_filters As Dictionary(Of Int32, List(Of Int32)))

        Dim request_id As Int32 = Computer.CMSG_COMPUTE_REQUEST({m_versionId}, _
                                                                 {entity_id}.tolist, _
                                                                 currency_id, _
                                                                 filters, _
                                                                 axis_filters, _
                                                                 Nothing)
        requestIdComputeFlagDict.Add(request_id, False)
        While requestIdComputeFlagDict(request_id) = False
            ' timeout ? priority high
        End While
        ' Cache Registering
        computingCache.cacheEntityID = entity_id
        computingCache.cacheCurrencyId = currency_id
        computingCache.cacheVersions = {m_versionId}
        computingCache.cacheFilters = filters
        computingCache.cacheAxisFilters = axis_filters
        mustUpdateFlag = False

    End Sub

#End Region


#Region "Checks"

    Private Function CheckAccount(ByRef accountObject As Object) As Boolean

        Dim accountName As String = ReturnValueFromRange(accountObject)
        If Not accountName Is Nothing Then
            account_id = GlobalVariables.Accounts.GetValueId(accountName)
            If account_id = 0 Then
                GoTo ReturnError
            Else
                Return True
            End If
            GoTo ReturnError
        End If

ReturnError:
        error_message = "Invalid Account"
        Return False

    End Function

    Private Function CheckEntity(ByRef entityObject As Object) As Boolean

        Dim entityName As String = ReturnValueFromRange(entityObject)
        If Not entityName Is Nothing Then
            entity_id = GlobalVariables.AxisElems.GetValueId(AxisType.Entities, entityName)
            If entity_id = 0 Then
                GoTo ReturnError
            Else
                Return True
            End If
            GoTo ReturnError
        End If

ReturnError:
        error_message = "Invalid Entity"
        Return False

    End Function

    Private Function CheckCurrency(ByRef currencyObject As Object) As Boolean

        Dim currencyName As String = ReturnValueFromRange(currencyObject)
        If Not currencyName Is Nothing Then
            currency_id = GlobalVariables.Currencies.GetValueId(currencyName)
            If currency_id = 0 Then
                error_message = "Invalid Currency"
                Return False
            Else
                Return True
            End If
        Else
            error_message = "Invalid Currency"
            Return True
        End If

    End Function

    Private Function CheckVersion(ByRef versionObject As Object) As Boolean

        Dim version_name As String = ReturnValueFromRange(versionObject)
        If Not version_name Is Nothing Then
            m_versionId = GlobalVariables.Versions.GetValueId(version_name)
            If m_versionId = 0 Then
                error_message = "Invalid Version"
                Return False
            Else
                Return True
            End If
        Else
            ' if my.settings version valid
            ' else -> error message = "set up version
            ' priority normal"
            m_versionId = My.Settings.version_id
            Return True
        End If

    End Function

    Private Function CheckDate(ByRef p_period_str As Object) As Boolean

        ' must be able to read strings or integer !! 
        On Error GoTo ReturnError
        Dim version As Version = GlobalVariables.Versions.GetValue(m_versionId)
        If version Is Nothing Then GoTo ReturnError

        Dim periodsList() As Int32 = GlobalVariables.Versions.GetPeriodsList(m_versionId)
        Dim periodIdentifyer As Char
        Select Case version.TimeConfiguration
            Case CRUD.TimeConfig.YEARS
                periodIdentifyer = Computer.YEAR_PERIOD_IDENTIFIER
            Case CRUD.TimeConfig.MONTHS
                periodIdentifyer = Computer.MONTH_PERIOD_IDENTIFIER
        End Select
        Dim periodObject = ReturnValueFromRange(p_period_str)

        If TypeOf (periodObject) Is Int32 Then
            GoTo PeriodIntegerIdentification
        ElseIf TypeOf (periodObject) Is String Then
            periodObject = CDate(periodObject).ToOADate()
            GoTo PeriodIntegerIdentification
        ElseIf TypeOf (periodObject) Is Date Then
            periodObject = periodObject.ToOADate()
            GoTo PeriodIntegerIdentification
        End If

PeriodIntegerIdentification:
        For Each p In periodsList
            If p = periodObject Then
                periodToken = periodIdentifyer & periodObject
                Return True
            End If
        Next
        GoTo ReturnError


ReturnError:
        error_message = "Invalid Period or Period format"
        Return False

    End Function

#End Region


#Region "Events"

    Private Sub AfterCompute(ByRef entity_id As Int32, ByRef status As ErrorMessage, ByRef request_id As Int32)

        If status = ErrorMessage.SUCCESS Then
            requestIdComputeFlagDict(request_id) = True
        Else
            ' raise error message in this case ? priority high 
            requestIdComputeFlagDict(request_id) = True
        End If

    End Sub

#End Region


#Region "Filters Dictionaries Building"

    Private Sub BuildFilters(ByRef p_filters_object As Object, _
                             ByRef filters As Dictionary(Of Int32, List(Of Int32)))

        Dim filterValue As FilterValue
        Dim filterId As Int32
        For Each filterObject In p_filters_object
            Dim filterValueName As String = ReturnValueFromRange(filterObject)
            If Not filterValueName Is Nothing Then
                filterValue = GlobalVariables.FiltersValues.GetValue(filterValueName)
                If Not filterValue Is Nothing Then
                    filterId = filterValue.FilterId
                    If filters.ContainsKey(filterId) = False Then
                        filters.Add(filterId, New List(Of Int32))
                    End If
                    filters(filterId).Add(filterValue.Id)
                End If
            End If
        Next

    End Sub

    Private Sub BuildAxisFilter(ByRef p_axis_filters_object As Object, _
                                ByRef CRUDModel As AxisElemManager, _
                                ByRef axisType As AxisType, _
                                ByRef axis_filters As Dictionary(Of Int32, List(Of Int32)))

        Dim axisFiltersList As New List(Of Int32)
        For Each axisFilter In p_axis_filters_object
            Dim axisName As String = ReturnValueFromRange(axisFilter)
            If Not axisName Is Nothing Then
                Dim axisValueId As Int32 = CRUDModel.GetValueId(axisType, axisName)
                If axisValueId <> 0 Then
                    axisFiltersList.Add(axisValueId)
                End If
            End If
        Next
        If axisFiltersList.Count > 0 Then axis_filters.Add(axisType, axisFiltersList)

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

    Private Sub AddAxisFilterToFiltersList(ByRef filter As Object, _
                                            ByRef filterList As List(Of Int32))

        'Dim filterValue As String = ReturnValueFromRange(filter)
        'If Not filterValue Is Nothing AndAlso EntitiesCategoriesNameKeyDictionary.ContainsKey(filterValue) Then
        '    filterList.Add(EntitiesCategoriesNameKeyDictionary.Item(filterValue))
        'End If

    End Sub

#End Region


End Class
