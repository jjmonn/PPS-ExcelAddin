' CControlingCONTROLER.vb
'
' Model for Controlling User iterfaces: display consolidated data
'
' To do:
'       - 
'       - 
'       - the list of nodes filtered after category selection applied can be catch and displayed to the user (with opyion to not display message again)
'
'
' Known bugs:
'
'
' Author: Julien Monnereau
' Last modified: 24/11/2014

Imports System.Windows.Forms
Imports System.Collections.Generic


Friend Class CControlingCONTROLER


#Region "Instance Variable"

    ' Objects
    Friend MODEL As CControlingMODEL
    Private ESB As New EntitiesSelectionBuilderClass
    Friend VERSIONSMGT As New CVersionsForControlingUIs
    Private VIEWOBJECT As ControllingUI_2        ' Can hold a WinForm or Task Pane ;)

    ' Variables
    Friend currentEntity As String
    Friend CurrentEntityKey As String
    Friend globalPeriodsList As List(Of Integer)
    Friend rates_versions_list As New List(Of String)
    Friend rates_versions_names As New List(Of String)


#End Region


#Region "Initialization"

    Friend Sub New(ByRef inputVIEWOBJECT As Object)

        MODEL = New CControlingMODEL(VERSIONSMGT.PERIODSMGT.yearlyPeriodList)
        VIEWOBJECT = inputVIEWOBJECT

    End Sub

#End Region


#Region "Interface"

    Friend Sub compute_entity_complete(ByRef entity_node As TreeNode)

        If rates_version_selection_init() Then
            VERSIONSMGT.InitializeVersionsArray(cTreeViews_Functions.GetCheckedNodeCollection(VIEWOBJECT.versionsTV), globalPeriodsList)
            MODEL.init_computer_complete_mode(entity_node)

            Pbar_init_complete()
            VIEWOBJECT.VIEWInitialization(globalPeriodsList, _
                                         VERSIONSMGT.VersionsNameArray, _
                                         rates_versions_names, _
                                         VERSIONSMGT.versionsComparisonFlag, _
                                         VERSIONSMGT.versionsCodeTimeSetUpDict(VERSIONSMGT.VersionsCodeArray(0))(VERSIONS_TIME_CONFIG_VARIABLE), _
                                         , entity_node)
            VIEWOBJECT.PBar.AddProgress(1)

            If VERSIONSMGT.VersionsCodeArray.Length > 1 Then
                compute_data_version_loop(entity_node)
            Else
                If rates_versions_list.Count > 1 Then
                    compute_rates_versions_loop(entity_node)
                Else
                    compute(entity_node, VERSIONSMGT.VersionsCodeArray(0), rates_versions_list(0))
                    VIEWOBJECT.Dispatch_complete(entity_node, _
                                                 MODEL.complete_data_dictionary, _
                                                 VERSIONSMGT.GetPeriodList(VERSIONSMGT.VersionsCodeArray(0)), _
                                                 MODEL.get_model_accounts_list)
                End If
            End If
            format_view_after_computation(entity_node)
        End If

    End Sub

#Region "Computations Loops Control"

    Private Sub compute_rates_versions_loop(ByRef entity_node As TreeNode)

        Dim version_index As Int32 = 0
        For Each rates_version_code As String In rates_versions_list
            compute(entity_node, VERSIONSMGT.VersionsCodeArray(0), rates_version_code)
            VIEWOBJECT.Dispatch_complete(entity_node, _
                                         MODEL.complete_data_dictionary, _
                                         VERSIONSMGT.GetPeriodList(VERSIONSMGT.VersionsCodeArray(0)), _
                                         MODEL.get_model_accounts_list, _
                                         version_index)

            version_index = version_index + 1
            VIEWOBJECT.PBar.AddProgress(1)
        Next


    End Sub

    Private Sub compute_data_version_loop(ByRef entity_node As TreeNode)

        Dim versionIndex As Int32 = 0
        Select Case VERSIONSMGT.versionsComparisonFlag

            Case CVersionsForControlingUIs.YEARLY_VERSIONS_COMPARISON, CVersionsForControlingUIs.MONTHLY_VERSIONS_COMPARISON

                For Each VersionCode As String In VERSIONSMGT.VersionsCodeArray
                    compute(entity_node, VersionCode, rates_versions_list(0))
                    VIEWOBJECT.Dispatch_complete(entity_node, _
                                                 MODEL.complete_data_dictionary, _
                                                 VERSIONSMGT.GetPeriodList(VersionCode), _
                                                 MODEL.get_model_accounts_list, _
                                                 versionIndex)
                    versionIndex = versionIndex + 1
                    VIEWOBJECT.PBar.AddProgress(1)
                Next

            Case CVersionsForControlingUIs.YEARLY_MONTHLY_VERSIONS_COMPARISON

                MsgBox("Monthly Aggregation not implemented so far, ready, on demand.")
                'For Each VersionCode As String In VERSIONSMGT.VersionsCodeArray
                '    Dim periodList As List(Of Integer) = VERSIONSMGT.GetPeriodList(VersionCode)
                '    compute(entity_node, VersionCode, rates_versions_list(0))

                '    If VERSIONSMGT.versionsCodeTimeSetUpDict(VersionCode)(VERSIONS_TIME_CONFIG_VARIABLE) = MONTHLY_TIME_CONFIGURATION Then
                '        ' dispatch special months aggregation column select
                '    Else
                '        ' dispatch to version
                '        ' VIEWOBJECT.DispatchDataArrayToDGVs(MODEL.DataArray, periodList.ToArray, versionIndex)
                '    End If
                '    versionIndex = versionIndex + 1
                '    VIEWOBJECT.PBar.AddProgress(1)
                'Next

        End Select

    End Sub

    Private Sub compute(ByRef entity_node As TreeNode, _
                        ByRef data_version As String, _
                        ByRef rates_version As String)

        Dim ref_period As Integer = 0
        If VERSIONSMGT.versionsCodeTimeSetUpDict(data_version)(VERSIONS_TIME_CONFIG_VARIABLE) = MONTHLY_TIME_CONFIGURATION _
        Then ref_period = VERSIONSMGT.versionsCodeTimeSetUpDict(data_version)(VERSIONS_REFERENCE_YEAR_VARIABLE)

        MODEL.compute_selection_complete(data_version, _
                                         VIEWOBJECT.PBar, _
                                         VERSIONSMGT.versionsCodeTimeSetUpDict(data_version)(VERSIONS_TIME_CONFIG_VARIABLE), _
                                         rates_version, _
                                         VERSIONSMGT.GetPeriodList(data_version), _
                                         VIEWOBJECT.CurrenciesCLB.CheckedItems(0), _
                                         ref_period, _
                                         ESB.StrSqlQuery)

        MODEL.LoadOutputMatrix(VIEWOBJECT.PBar)

    End Sub

#End Region

    Private Sub Pbar_init_complete()

        Dim LoadingBarMax As Integer = (MODEL.inputs_entities_list.Count + 5) * VERSIONSMGT.VersionsCodeArray.Length + 2
        VIEWOBJECT.PBar.Launch(1, LoadingBarMax)

    End Sub

    Protected Friend Sub ShowDataLog(ByRef entity_id As String, _
                                     ByRef version_id As String, _
                                     ByRef account_id As String,
                                     ByRef period As Int32)



    End Sub


#End Region


#Region "Checks"

    Private Function rates_version_selection_init() As Boolean

        rates_versions_list.Clear()
        rates_versions_names.Clear()
        Dim selected_rates_versions = cTreeViews_Functions.GetCheckedNodeCollection(VIEWOBJECT.rates_versionsTV)
        If selected_rates_versions.Count > 1 Then
            If VERSIONSMGT.VersionsCodeArray.Length > 1 Then
                MsgBox("Several FX Rates Versions and Data Versions are selected." + Chr(13) + _
                        "Select either one FX Rates Version and several Data Version or " + _
                        "one Data Version and several FX Rates Versions.")
                Return False
            End If
        End If

        For Each version_node In selected_rates_versions
            rates_versions_list.Add(version_node.Name)
            rates_versions_names.Add(version_node.Text)
        Next
        Return True

    End Function

#End Region


#Region "Utilities"

    ' Categories Update: triggered by VIEW, update category list and entities treeview items according to category selection
    Friend Sub CategoriesUpdate()

        ESB.BuildCategoriesFilterFromTreeview(VIEWOBJECT.categoriesTV)
        Dim expansionDict = cTreeViews_Functions.SaveNodesExpansionsLevel(VIEWOBJECT.entitiesTV)
        Dim checkedList = cTreeViews_Functions.SaveCheckedStates(VIEWOBJECT.entitiesTV)
        Entity.LoadEntitiesTree(VIEWOBJECT.entitiesTV, ESB.StrSqlQueryForEntitiesUploadFunctions)
        cTreeViews_Functions.ResumeExpansionsLevel(VIEWOBJECT.entitiesTV, expansionDict)
        cTreeViews_Functions.ResumeCheckedStates(VIEWOBJECT.entitiesTV, checkedList)    ' a list of nodes filtered can be displayed
      
    End Sub

    Friend Sub format_view_after_computation(ByRef input_node As TreeNode)

        VIEWOBJECT.PBar.EndProgress()
        VIEWOBJECT.FormatVIEWDataDisplay()
        currentEntity = input_node.Text
        CurrentEntityKey = input_node.Name
        VIEWOBJECT.entityTB.Text = currentEntity
        VIEWOBJECT.VersionTB.Text = String.Join(" ; ", VERSIONSMGT.VersionsNameArray)
        VIEWOBJECT.CurrencyTB.Text = VIEWOBJECT.CurrenciesCLB.CheckedItems(0)
        ' display rates version case > 1

    End Sub

    Friend Sub close_model()

        MODEL.delete_model()

    End Sub

#End Region


End Class
