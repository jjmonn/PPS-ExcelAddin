﻿' PPSBIController.vb
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
    Private computingCache As ComputingCache

    ' Compute Params
    Private periodToken As String
    Private entity_id As Int32
    Private account_id As Int32
    Private currency_id As Int32
    Private version_id As Int32
    Private adjustment_id As Int32

    ' Variables
    Private error_message As String
    Private requestIdComputeFlagDict As New Dictionary(Of Int32, Boolean)
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
        error_message = ""
        emptyCellFlag = False

        ' Checks
        If CheckAccount(p_account_str) = False Then Return error_message
        If CheckEntity(p_entity_str) = False Then Return error_message
        If CheckCurrency(p_currency_str) = False Then Return error_message
        If CheckVersion(p_version_str) = False Then Return error_message
        If CheckDate(p_period_str) = False Then Return error_message

        ' STUB !!! filters priority high !!
        Dim filters = New Dictionary(Of Int32, List(Of Int32))
       
        ' Axis Filters building
        Dim axis_filters = New Dictionary(Of Int32, List(Of Int32))()
        BuildAxisFilter(p_clients_filters, GlobalVariables.Clients, GlobalEnums.AnalysisAxis.CLIENTS, axis_filters)
        BuildAxisFilter(p_products_filters, GlobalVariables.Products, GlobalEnums.AnalysisAxis.PRODUCTS, axis_filters)
        BuildAxisFilter(p_adjustments_filters, GlobalVariables.Adjustments, GlobalEnums.AnalysisAxis.ADJUSTMENTS, axis_filters)


        Dim token As String = version_id & Computer.TOKEN_SEPARATOR & _
                                "0" & Computer.TOKEN_SEPARATOR & _
                                entity_id & Computer.TOKEN_SEPARATOR & _
                                account_id & Computer.TOKEN_SEPARATOR & _
                                periodToken

        ' Check Cache and Compute if necessary
        If mustUpdateFlag = False _
        AndAlso computingCache.MustCompute(entity_id, _
                                            currency_id, _
                                            {version_id}, _
                                            filters, _
                                            axis_filters) = True Then

            Compute(filters, axis_filters)
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

    Private Sub Compute(ByRef filters As Dictionary(Of Int32, List(Of Int32)), _
                        ByRef axis_filters As Dictionary(Of Int32, List(Of Int32)))

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
        ' Cache Registering
        computingCache.cacheEntityID = entity_id
        computingCache.cacheCurrencyId = currency_id
        computingCache.cacheVersions = {version_id}
        computingCache.cacheFilters = filters
        computingCache.cacheAxisFilters = axis_filters
        mustUpdateFlag = False

    End Sub

#End Region


#Region "Checks"

    Private Function CheckAccount(ByRef accountObject As Object) As Boolean

        Dim accountName As String = ReturnValueFromRange(accountObject)
        If Not accountName Is Nothing Then
            account_id = GlobalVariables.Accounts.GetIdFromName(accountName)
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
            entity_id = GlobalVariables.Entities.GetEntityId(entityName)
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
            currency_id = GlobalVariables.Currencies.GetCurrencyId(currencyName)
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
            version_id = GlobalVariables.Versions.GetVersionsIDFromName(version_name)
            If version_id = 0 Then
                error_message = "Invalid Version"
                Return False
            Else
                Return True
            End If
        Else
            ' if my.settings version valid
            ' else -> error message = "set up version
            ' priority normal"
            version_id = My.Settings.version_id
            Return True
        End If

    End Function

    Private Function CheckDate(ByRef p_period_str As Object) As Boolean

        On Error GoTo ReturnError
        Dim periodAsInt As Int32 = ReturnValueFromRange(p_period_str)
        Dim periodsList() As Int32 = GlobalVariables.Versions.GetPeriodsList(version_id)
        Dim periodIdentifyer As Char
        Select Case GlobalVariables.Versions.versions_hash(version_id)(VERSIONS_TIME_CONFIG_VARIABLE)
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
        GoTo ReturnError

ReturnError:
        error_message = "Invalid Period or Period format"
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


#Region "Filters Dictionaries Building"

    Private Sub BuildAxisFilter(ByRef p_axis_filters_object As Object, _
                                ByRef CRUDModel As SuperAxisCRUD, _
                                ByRef axisId As Int32, _
                                ByRef axis_filters As Dictionary(Of Int32, List(Of Int32)))

        Dim axisFiltersList As New List(Of Int32)
        For Each axisFilter In p_axis_filters_object
            Dim axisName As String = ReturnValueFromRange(axisFilter)
            If Not axisName Is Nothing Then
                Dim axisValueId As Int32 = CRUDModel.GetAxisId(axisName)
                If axisValueId <> 0 Then
                    axisFiltersList.Add(axisValueId)
                End If
            End If
        Next
        If axisFiltersList.Count > 0 Then axis_filters.Add(axisId, axisFiltersList)

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
