'' MarketPricesMGTController.vb
''
'' To do:
''       - Indexes deletion
''       - excel import to be copied from currencies !
''
''
''
'' Author: Julien Monnereau
'' Last Modified: 24/03/2015


'Imports System.Collections.Generic
'Imports System.Windows.Forms
'Imports System.Collections


'Friend Class MarketPricesController


'#Region "Instance Variables"

'    ' Objects
'    Private MarketPrices As MarketPrice
'    Private MarketIndexVersions As New MarketIndexVersion
'    Private MarketIndexes As New MarketIndex
'    Private View As MarketPricesUI
'    Private NewMarketPricesVersion As NewMarketPricesVersionUI
'    Private ExcelImport As ExcelRatesImportUI

'    ' Variables
'    Protected Friend indexes_list As List(Of String)
'    Protected Friend current_version As String
'    Protected Friend global_periods_dictionary As Dictionary(Of Int32, List(Of Int32))

'    ' Const 
'    Private MARKET_INDEXES_MAX_TOKEN_SIZE As Int32 = 50

'#End Region


'#Region "Initialize"

'    Protected Friend Sub New()

'        View = New MarketPricesUI(Me)
'        MarketIndexVersion.load_market_index_version_tv(View.versionsTV)
'        indexes_list = MarketIndexesMapping.GetMarketIndexesList
'        NewMarketPricesVersion = New NewMarketPricesVersionUI(Me)
'        View.Show()

'    End Sub

'#End Region


'#Region "Market Prices Controller"

'    Friend Sub UpdateMarketPrice(ByRef id As String, _
'                                 ByRef period As Int32, _
'                                 ByVal value As Double)

'        If Not MarketPrices Is Nothing Then MarketPrices.UpdateMarketPrice(id, period, value)

'    End Sub

'    Friend Sub ChangeVersion(ByRef market_prices_version_id As String)

'        MarketPrices = New MarketPrice(market_prices_version_id)
'        If MarketPrices.object_is_alive = True Then
'            current_version = market_prices_version_id
'            global_periods_dictionary = MarketIndexVersions.GetPeriodsDictionary(market_prices_version_id)
'            View.marketPricesView.InitializeDGV(indexes_list, global_periods_dictionary)
'            View.marketPricesView.DisplayPricesVersionValuesinDGV(get_market_index_prices_dictionary())
'            View.index_version_TB.Text = MarketIndexVersions.ReadVersion(current_version, MARKET_INDEXES_VERSIONS_NAME_VAR)
'        Else
'            MarketPrices = Nothing
'        End If

'    End Sub

'#End Region


'#Region "Indexes Controller"

'    Friend Sub AddNewIndex()

'        Dim index As String = InputBox("Enter the name of the new Index:")
'        If index <> "" Then
'            If indexes_list.Contains(index) Then
'                MsgBox("This Market Index already exists. Please enter an Index which name isn't already in the list")
'            Else
'                If TypeOf (index) Is String AndAlso Len(index) < MARKET_INDEXES_MAX_TOKEN_SIZE Then
'                    MarketIndexes.Createindex(index)
'                    indexes_list.Add(index)
'                    View.marketPricesView.InitializeDGV(indexes_list, global_periods_dictionary)
'                    ChangeVersion(current_version)
'                Else
'                    MsgBox("The format of the new Index is not valid or exceeds maximum size (" & MARKET_INDEXES_MAX_TOKEN_SIZE & ")")
'                End If
'            End If
'        End If

'    End Sub

'    Friend Sub DeleteIndex(ByRef index As String)

'        ' To be Implemented

'    End Sub

'#End Region


'#Region "Market Prices Version Controller"

'    Friend Sub CreateVersion(ByRef name As String, _
'                             ByRef isFolder As Boolean, _
'                             Optional ByRef start_period As Int32 = 0, _
'                             Optional ByRef nb_periods As Int32 = 0, _
'                             Optional ByRef parent_node As TreeNode = Nothing)

'        'Dim tmpHT As New Hashtable
'        'tmpHT.Add(MARKET_INDEXES_VERSIONS_NAME_VAR, name)
'        'tmpHT.Add(ITEMS_POSITIONS, 1)

'        'If parent_node Is Nothing Then tmpHT.Add(MARKET_INDEXES_VERSIONS_PARENT_ID_VAR, DBNull.Value) Else tmpHT.Add(MARKET_INDEXES_VERSIONS_PARENT_ID_VAR, parent_node.Name)
'        'If isFolder = True Then
'        '    tmpHT.Add(MARKET_INDEXES_VERSIONS_IS_FOLDER_VAR, 1)
'        'Else
'        '    tmpHT.Add(MARKET_INDEXES_VERSIONS_IS_FOLDER_VAR, 0)
'        '    tmpHT.Add(MARKET_INDEXES_VERSIONS_START_PERIOD_VAR, start_period)
'        '    tmpHT.Add(MARKET_INDEXES_VERSIONS_NB_PERIODS_VAR, nb_periods)
'        'End If

'        'MarketIndexVersions.CreateVersion(tmpHT)
'        'If parent_node Is Nothing Then View.versionsTV.Nodes.Add(new_version_id, name) Else parent_node.Nodes.Add(new_version_id, name)
'        'UpdateVersionsPositions()
'        'MarketIndexVersion.load_market_index_version_tv(View.versionsTV)
'        'If isFolder = False Then ChangeVersion(new_version_id)

'    End Sub

'    Friend Sub UpdateVersionName(ByRef version_id As String, ByRef name As String)

'        MarketIndexVersions.UpdateVersion(version_id, MARKET_INDEXES_VERSIONS_NAME_VAR, name)

'    End Sub

'    Friend Sub DeleteVersionsOrFolder(ByRef version_node As TreeNode)

'        Dim versions_list = TreeViewsUtilities.GetNodesKeysList(version_node)
'        versions_list.Reverse()
'        For Each version_id In versions_list
'            If MarketIndexVersions.ReadVersion(version_id, MARKET_INDEXES_VERSIONS_IS_FOLDER_VAR) = 0 Then
'                If DeleteVersion(version_id) = True Then
'                    DeleteFromTreeAndModel(version_id)
'                Else
'                    ' Quid: if we delete parent node !!
'                End If
'            Else
'                DeleteFromTreeAndModel(version_id)
'            End If
'        Next

'    End Sub

'    Private Function DeleteVersion(ByRef version_id As String) As Boolean

'        Dim tmp_version = current_version
'        If MarketPrice.DeleteAllMarketPrices(version_id) Then
'            If version_id = current_version Then
'                current_version = ""
'                View.marketPricesView.InitializeDGV(indexes_list, global_periods_dictionary)
'            End If
'            Return True
'        Else
'            Return False
'        End If

'    End Function

'    Private Sub DeleteFromTreeAndModel(ByRef version_id)

'        MarketIndexVersions.DeleteVersion(version_id)
'        On Error Resume Next
'        View.versionsTV.Nodes.Find(version_id, True)(0).Remove()

'    End Sub

'#End Region


'#Region "Utilities"

'    Friend Function IsFolderVersion(ByRef versionKey As String) As Boolean

'        If MarketIndexVersions.ReadVersion(versionKey, MARKET_INDEXES_VERSIONS_IS_FOLDER_VAR) = 1 Then
'            Return True
'        Else
'            Return False
'        End If

'    End Function

'    Private Function get_market_index_prices_dictionary() As Dictionary(Of String, Hashtable)

'        Dim tmp_dic As New Dictionary(Of String, Hashtable)
'        For Each index In indexes_list
'            Dim hash As New Hashtable
'            For Each period In global_periods_dictionary.Keys
'                For Each month_period In global_periods_dictionary(period)
'                    hash.Add(month_period, MarketPrices.ReadMarketPrice(index, month_period))
'                Next
'            Next
'            tmp_dic.Add(index, hash)
'        Next
'        Return tmp_dic

'    End Function


'    Private Sub UpdateVersionsPositions()

'        Dim positions_dic = TreeViewsUtilities.GeneratePositionsDictionary(View.versionsTV)
'        For Each id In positions_dic.Keys
'            MarketIndexVersions.UpdateVersion(id, ITEMS_POSITIONS, positions_dic(id))
'        Next

'    End Sub

'#Region "Import Prices from Excel"

'    Friend Sub ImportPricessFromExcel()

'        'ExcelImport = New ExcelRatesImportUI(Me, indexes_list)
'        'ExcelImport.Show()

'    End Sub

'    Friend Sub InputRangesCallBack(ByRef period() As Integer, _
'                                   ByRef prices() As Double,
'                                   ByRef index As String)

'        For i = 0 To period.Length - 1
'            View.marketPricesView.UpdateCell(index, period(i), prices(i))
'        Next
'        ExcelImport.Dispose()

'    End Sub


'#End Region

'    Protected Friend Sub ShowNewPricesVersion(Optional ByRef parent_node As TreeNode = Nothing)

'        NewMarketPricesVersion.parent_node = parent_node
'        NewMarketPricesVersion.Show()

'    End Sub

'#End Region


'End Class
