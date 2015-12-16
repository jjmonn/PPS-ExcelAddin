' FBIFunctionController.vb
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
' Last modified: 16/15/2015
'


Imports Microsoft.Office.Interop
Imports System.Collections
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Linq
Imports CRUD

Public Class FBIFunctionController


#Region "Instance Variables"

    ' Objects
    Private Computer As New Computer
    Private m_computingCache As ComputingCache

    ' Compute Params
    Private m_periodToken As String
    Private m_entityId As Int32
    Private m_accountId As Int32
    Private m_currencyId As Int32
    Private m_versionId As Int32
    Private m_adjustmentId As Int32

    ' Variables
    Private m_errorMessage As String
    Private m_requestIdComputeFlagDict As New SafeDictionary(Of Int32, Boolean)
    Private m_emptyCellFlag As Boolean
    Private m_cacheInitFlag As Boolean = False
    Private m_mustUpdateFlag As Boolean = False
    Private Const m_filtersToken As String = "0"


#End Region


#Region "Initialize"

    Friend Sub New()

        m_emptyCellFlag = False
        AddHandler Computer.ComputationAnswered, AddressOf AfterCompute
        If GlobalVariables.AuthenticationFlag = True Then InitCache()

    End Sub

    Private Sub InitCache()

        m_computingCache = New ComputingCache(False)
        m_cacheInitFlag = True

    End Sub

#End Region


#Region "Interface"

    Friend Sub ReinitializeCache()

        m_computingCache.ResetCache()

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

        If m_cacheInitFlag = False Then InitCache()
        If GlobalVariables.g_mustResetCache = True Then ReinitializeCache()
        m_errorMessage = ""
        m_emptyCellFlag = False

        ' Checks
        If CheckAccount(p_account_str) = False Then Return m_errorMessage
        If CheckEntity(p_entity_str) = False Then Return m_errorMessage
        If CheckCurrency(p_currency_str) = False Then Return m_errorMessage
        If CheckVersion(p_version_str) = False Then Return m_errorMessage
        If CheckDate(p_period_str) = False Then Return m_errorMessage

        ' Filters Building
        Dim filters = New SafeDictionary(Of Int32, List(Of Int32))
        Dim l_filtersError As String = BuildFilters(p_filtersArray, filters)
        If l_filtersError <> "" Then
            Return l_filtersError & Local.GetValue("ppsbi.msg_invalid_filter")
        End If

        ' Axis Filters building
        Dim axis_filters = New SafeDictionary(Of Int32, List(Of Int32))()
        Dim l_axisFilterError As String = BuildAxisFilter(p_clients_filters, GlobalVariables.AxisElems, AxisType.Client, axis_filters)
        If l_axisFilterError <> "" Then Return l_axisFilterError & Local.GetValue("ppsbi.msg_invalid_filter")
        BuildAxisFilter(p_products_filters, GlobalVariables.AxisElems, AxisType.Product, axis_filters)
        BuildAxisFilter(p_adjustments_filters, GlobalVariables.AxisElems, AxisType.Adjustment, axis_filters)

        Dim token As String = m_versionId & Computer.TOKEN_SEPARATOR & _
                              m_filtersToken & Computer.TOKEN_SEPARATOR & _
                              m_entityId & Computer.TOKEN_SEPARATOR & _
                              m_accountId & Computer.TOKEN_SEPARATOR & _
                              m_periodToken

        ' Check Cache and Compute if necessary
        If m_mustUpdateFlag = False _
        AndAlso m_computingCache.MustCompute(m_entityId, _
                                            m_currencyId, _
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
                                                                 {m_entityId}.ToList, _
                                                                 CRUD.Account.AccountProcess.FINANCIAL, _
                                                                 m_currencyId, _
                                                                 filters, _
                                                                 axis_filters, _
                                                                 Nothing)
        m_requestIdComputeFlagDict.Add(request_id, False)
        While m_requestIdComputeFlagDict(request_id) = False
            ' timeout ? priority high
        End While
        ' Cache Registering
        m_computingCache.cacheEntityID = m_entityId
        m_computingCache.cacheCurrencyId = m_currencyId
        m_computingCache.cacheVersions = {m_versionId}
        m_computingCache.cacheFilters = filters
        m_computingCache.cacheAxisFilters = axis_filters
        m_mustUpdateFlag = False

    End Sub

#End Region


#Region "Checks"

    Private Function CheckAccount(ByRef accountObject As Object) As Boolean

        Dim accountName As String = ReturnValueFromRange(accountObject)
        If Not accountName Is Nothing Then
            m_accountId = GlobalVariables.Accounts.GetValueId(accountName)
            If m_accountId = 0 Then
                GoTo ReturnError
            Else
                Return True
            End If
            GoTo ReturnError
        End If

ReturnError:
        m_errorMessage = "Invalid Account"
        Return False

    End Function

    Private Function CheckEntity(ByRef entityObject As Object) As Boolean

        Dim entityName As String = ReturnValueFromRange(entityObject)
        If Not entityName Is Nothing Then
            m_entityId = GlobalVariables.AxisElems.GetValueId(AxisType.Entities, entityName)
            If m_entityId = 0 Then
                GoTo ReturnError
            Else
                Return True
            End If
            GoTo ReturnError
        End If

ReturnError:
        m_errorMessage = "Invalid Entity"
        Return False

    End Function

    Private Function CheckCurrency(ByRef currencyObject As Object) As Boolean

        Dim currencyName As String = ReturnValueFromRange(currencyObject)
        If Not currencyName Is Nothing Then
            m_currencyId = GlobalVariables.Currencies.GetValueId(currencyName)
            If m_currencyId = 0 Then
                m_errorMessage = "Invalid Currency"
                Return False
            Else
                Return True
            End If
        Else
            m_errorMessage = "Invalid Currency"
            Return True
        End If

    End Function

    Private Function CheckVersion(ByRef versionObject As Object) As Boolean

        Dim version_name As String = ReturnValueFromRange(versionObject)
        If Not version_name Is Nothing Then
            m_versionId = GlobalVariables.Versions.GetValueId(version_name)
            If m_versionId = 0 Then
                m_errorMessage = "Invalid Version"
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
                m_periodToken = periodIdentifyer & periodObject
                Return True
            End If
        Next
        GoTo ReturnError


ReturnError:
        m_errorMessage = "Invalid Period or Period format"
        Return False

    End Function

#End Region


#Region "Events"

    Private Sub AfterCompute(ByRef entity_id As Int32, ByRef status As ErrorMessage, ByRef request_id As Int32)

        If status = ErrorMessage.SUCCESS Then
            m_requestIdComputeFlagDict(request_id) = True
        Else
            ' raise error message in this case ? priority high 
            m_requestIdComputeFlagDict(request_id) = True
        End If

    End Sub

#End Region


#Region "Filters Dictionaries Building"

    Private Function BuildFilters(ByRef p_filters_object As Object, _
                                  ByRef filters As Dictionary(Of Int32, List(Of Int32))) As String

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
                Else
                    Return filterValueName
                End If
            End If
        Next
        Return ""

    End Function

    Private Function BuildAxisFilter(ByRef p_axis_filters_object As Object, _
                                     ByRef CRUDModel As AxisElemManager, _
                                     ByRef axisType As AxisType, _
                                     ByRef axis_filters As Dictionary(Of Int32, List(Of Int32))) As String

        Dim axisFiltersList As New List(Of Int32)
        For Each axisFilter In p_axis_filters_object
            Dim axisName As String = ReturnValueFromRange(axisFilter)
            If Not axisName Is Nothing Then
                Dim axisValueId As Int32 = CRUDModel.GetValueId(axisType, axisName)
                If axisValueId <> 0 Then
                    axisFiltersList.Add(axisValueId)
                Else
                    Return axisName
                End If
            End If
        Next
        If axisFiltersList.Count > 0 Then axis_filters.Add(axisType, axisFiltersList)
        Return ""

    End Function

#End Region


#Region "Utilities"

    Private Function ReturnValueFromRange(ByRef input As Object) As Object

        If input Is Nothing Then Return ""
        If TypeOf (input) Is Excel.Range Then
            Dim rng As Excel.Range = CType(input, Excel.Range)
            If rng.Value2 Is Nothing Then m_emptyCellFlag = True
            Return rng.Value2
        Else
            Return input
        End If

    End Function

    'Private Sub AddAxisFilterToFiltersList(ByRef filter As Object, _
    '                                        ByRef filterList As List(Of Int32))

    '    'Dim filterValue As String = ReturnValueFromRange(filter)
    '    'If Not filterValue Is Nothing AndAlso EntitiesCategoriesNameKeyDictionary.ContainsKey(filterValue) Then
    '    '    filterList.Add(EntitiesCategoriesNameKeyDictionary.Item(filterValue))
    '    'End If

    'End Sub

#End Region


End Class
