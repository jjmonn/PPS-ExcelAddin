' CControlingCONTROLER.vb
'
' Model for Controlling User iterfaces: display consolidated data
'
' To do:
'       - Adapt display adjustments method to several versions case
'       - 
'       - the list of nodes filtered after category selection applied can be catch and displayed to the user (with opyion to not display message again)
'
'
' Known bugs:
'
'
' Author: Julien Monnereau
' Last modified: 27/01/2015

Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections


Friend Class ControlingUI2Controller


#Region "Instance Variable"

    ' Objects
    Friend MODEL As ControlingUI2MODEL
    Private ESB As New EntitiesSelectionBuilderClass
    Private View As ControllingUI_2        ' Can hold a WinForm or Task Pane ;)

    ' Variables
    Friend currentEntity As String
    Friend CurrentEntityKey As String
    Friend globalPeriodsList As List(Of Integer)
    Protected Friend versions_id_array As String()
    Private versions_name_array As String()
    Private versionsComparisonFlag As Int32
    Private versions_dict As Dictionary(Of String, Hashtable)

#End Region


#Region "Initialization"

    Friend Sub New(ByRef inputView As Object)

        MODEL = New ControlingUI2MODEL()
        View = inputView

    End Sub

#End Region


#Region "Interface"

#Region "Compute"

    Friend Sub compute_entity_complete(ByRef entity_node As TreeNode)

        Dim Versions As New Version
        versions_dict = Versions.InitializeVersionsArray(cTreeViews_Functions.GetCheckedNodeCollection(View.versionsTV), _
                                                            globalPeriodsList, _
                                                            versionsComparisonFlag, _
                                                            versions_id_array, _
                                                            versions_name_array)
        MODEL.init_computer_complete_mode(entity_node)
        Versions.Close()

        Pbar_init_complete()
        View.VIEWInitialization(globalPeriodsList, _
                                        versions_name_array, _
                                        versionsComparisonFlag, _
                                        versions_dict(versions_id_array(0))(VERSIONS_TIME_CONFIG_VARIABLE), _
                                        , entity_node)
        View.PBar.AddProgress(1)

        If versions_id_array.Length > 1 Then
            compute_data_version_loop(entity_node)
        Else
            compute(entity_node, versions_id_array(0), versions_dict(versions_id_array(0))(VERSIONS_RATES_VERSION_ID_VAR))
            View.Dispatch_complete(entity_node, _
                                    MODEL.complete_data_dictionary, _
                                    versions_dict(versions_id_array(0))(Version.PERIOD_LIST), _
                                    MODEL.get_model_accounts_list)
        End If
        format_view_after_computation(entity_node)

    End Sub

#Region "Computations Loops Control"

    Private Sub compute_data_version_loop(ByRef entity_node As TreeNode)

        Dim versionIndex As Int32 = 0
        Select Case versionsComparisonFlag

            Case Version.YEARLY_VERSIONS_COMPARISON, Version.MONTHLY_VERSIONS_COMPARISON

                For Each Version_id As String In versions_id_array
                    compute(entity_node, Version_id, versions_dict(Version_id)(VERSIONS_RATES_VERSION_ID_VAR))
                    View.Dispatch_complete(entity_node, _
                                                 MODEL.complete_data_dictionary, _
                                                 versions_dict(Version_id)(Version.PERIOD_LIST), _
                                                 MODEL.get_model_accounts_list, _
                                                 versionIndex)
                    versionIndex = versionIndex + 1
                    View.PBar.AddProgress(1)
                Next

            Case Version.YEARLY_MONTHLY_VERSIONS_COMPARISON

                MsgBox("Monthly Aggregation not implemented so far, ready, on demand.")
                'For Each VersionCode As String In VERSIONSMGT.versions_id_array
                '    Dim periodList As List(Of Integer) = VERSIONSMGT.GetPeriodList(VersionCode)
                '    compute(entity_node, VersionCode, rates_versions_list(0))

                '    If VERSIONSMGT.versionsCodeTimeSetUpDict(VersionCode)(VERSIONS_TIME_CONFIG_VARIABLE) = MONTHLY_TIME_CONFIGURATION Then
                '        ' dispatch special months aggregation column select
                '    Else
                '        ' dispatch to version
                '        ' View.DispatchDataArrayToDGVs(MODEL.DataArray, periodList.ToArray, versionIndex)
                '    End If
                '    versionIndex = versionIndex + 1
                '    View.PBar.AddProgress(1)
                'Next

        End Select

    End Sub

    Private Sub compute(ByRef entity_node As TreeNode, _
                        ByRef version_id As String, _
                        ByRef rates_version As String)

        Dim start_period, nb_periods As Int32
        start_period = versions_dict(version_id)(VERSIONS_START_PERIOD_VAR)
        nb_periods = versions_dict(version_id)(VERSIONS_NB_PERIODS_VAR)

        MODEL.compute_selection_complete(version_id, _
                                         View.PBar, _
                                         versions_dict(version_id)(VERSIONS_TIME_CONFIG_VARIABLE), _
                                         rates_version, _
                                         versions_dict(version_id)(Version.PERIOD_LIST), _
                                         View.CurrenciesCLB.CheckedItems(0), _
                                         start_period, _
                                         nb_periods, _
                                         ESB.StrSqlQuery, _
                                         GetAdjustmentsFilter)

        MODEL.LoadOutputMatrix(View.PBar)

    End Sub

#End Region

#End Region

    Private Sub Pbar_init_complete()

        Dim LoadingBarMax As Integer = (MODEL.inputs_entities_list.Count + 5) * versions_id_array.Length + 2
        View.PBar.Launch(1, LoadingBarMax)

    End Sub

    Protected Friend Sub ShowDataLog(ByRef entity_id As String, _
                                     ByRef version_id As String, _
                                     ByRef account_id As String,
                                     ByRef period As Int32)



    End Sub

    Protected Friend Sub LoadAdjustments()

        ' Adapt to several versions case
        View.DisplayAdjustments(MODEL.GetAdjustments(versions_id_array(0), _
                                                     View.CurrenciesCLB.CheckedItems(0), _
                                                     GetAdjustmentsFilter))

    End Sub

#End Region


#Region "Utilities"

    ' Categories Update: triggered by VIEW, update category list and entities treeview items according to category selection
    Friend Sub CategoriesUpdate()

        ESB.BuildCategoriesFilterFromTreeview(View.categoriesTV)
        Dim expansionDict = cTreeViews_Functions.SaveNodesExpansionsLevel(View.entitiesTV)
        Dim checkedList = cTreeViews_Functions.SaveCheckedStates(View.entitiesTV)
        Entity.LoadEntitiesTree(View.entitiesTV, ESB.StrSqlQueryForEntitiesUploadFunctions)
        cTreeViews_Functions.ResumeExpansionsLevel(View.entitiesTV, expansionDict)
        cTreeViews_Functions.ResumeCheckedStates(View.entitiesTV, checkedList)    ' a list of nodes filtered can be displayed

    End Sub

    Friend Sub format_view_after_computation(ByRef input_node As TreeNode)

        View.PBar.EndProgress()
        View.FormatVIEWDataDisplay()
        currentEntity = input_node.Text
        CurrentEntityKey = input_node.Name
        View.entityTB.Text = currentEntity
        View.VersionTB.Text = String.Join(" ; ", versions_name_array)
        View.CurrencyTB.Text = View.CurrenciesCLB.CheckedItems(0)
        ' display rates version case > 1

    End Sub

    Friend Sub close_model()

        MODEL.delete_model()

    End Sub

    Private Function GetAdjustmentsFilter() As List(Of String)

        Dim tmp_list As List(Of String) = cTreeViews_Functions.GetCheckedNodesID(View.adjustmentsTV)
        If tmp_list.Count <> View.adjustmentsTV.Nodes.Count Then
            Return tmp_list
        Else
            Return Nothing
        End If

    End Function

#End Region


End Class
