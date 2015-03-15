' CControlingCONTROLER.vb
'
' Model for Controlling User iterfaces: display consolidated data
'
' To do:
'       - Adapt display adjustments method to several versions case
'       - Hide adjsutments method implementation
'       - Consolidated adjustments
'       - the list of nodes filtered after category selection applied can be catch and displayed to the user (with opyion to not display message again)
'
'
' Known bugs:
'
'
' Author: Julien Monnereau
' Last modified: 10/03/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections


Friend Class ControllingUI2Controller


#Region "Instance Variable"

    ' Objects
    Private Model As ControllingUIModel
    Private ESB As New EntitiesSelectionBuilderClass
    Private View As ControllingUI_2        ' Can hold a WinForm or Task Pane ;)

    ' Variables
    Private TVsDict As New Dictionary(Of String, TreeView)
    Private RowsTVsList As New List(Of TreeView)
    Private ColumnsTVsList As New List(Of TreeView)

    Friend currentEntity As String
    Friend CurrentEntityKey As String
    Friend globalPeriodsList As List(Of Integer)
    Protected Friend versions_id_array As String()
    Protected Friend versions_name_array As String()
    Private versionsComparisonFlag As Int32
    Private versions_dict As Dictionary(Of String, Hashtable)

    ' Const
    Friend Const ACCOUNTS_CODE As String = "Accounts"
    Friend Const ENTITIES_CODE As String = "Entities"
    Friend Const YEARS_CODE As String = "Years"
    Friend Const MONTHS_CODE As String = "Months"
    Friend Const VERSIONS_CODE As String = "Versions"
    Friend Const CATEGORY_CODE As String = "Categories"


#End Region


#Region "Initialization"

    Friend Sub New(ByRef inputView As Object)

        View = inputView

    End Sub

#End Region


#Region "View Interface"

    ' config setup (trees)
    ' determination de la period list ou global_period_dic à afficher en fonction des versions

    'View.VIEWInitialization(globalPeriodsList, _
    '                            versions_name_array, _
    '                            versionsComparisonFlag, _
    '                            versions_dict(versions_id_array(0))(VERSIONS_TIME_CONFIG_VARIABLE), _
    '                            , entity_node)



#End Region



#Region "Model Interface"




    Friend Sub compute_entity_complete(ByRef entity_node As TreeNode)


        Dim versions As New Version
        Dim categories_array As String()
        Pbar_init_complete()

        ' versions init to be reviewed
        ' define the periods list and comp flag for display
        ' the computation doesn t care !!
        versions_dict = versions.InitializeVersionsArray(TreeViewsUtilities.GetCheckedNodeCollection(View.versionsTV), _
                                                         globalPeriodsList, _
                                                         versionsComparisonFlag, _
                                                         versions_id_array)

        versions.Close()


        View.PBar.AddProgress(1)
        format_view_after_computation(entity_node)

    End Sub

    Protected Friend Sub LoadAdjustments()

        ' Adapt to several versions case !
        View.DisplayAdjustments(Model.GetAdjustments(versions_id_array(0), _
                                                     View.CurrenciesCLB.CheckedItems(0), _
                                                     GetAdjustmentsFilter))

    End Sub

#End Region


#Region "Utilities"

    Private Sub Pbar_init_complete()

        Dim LoadingBarMax As Integer = (Model.inputs_entities_list.Count + 5) * versions_id_array.Length + 2
        View.PBar.Launch(1, LoadingBarMax)

    End Sub

    ' Categories Update: triggered by VIEW, update category list and entities treeview items according to category selection
    Friend Sub CategoriesUpdate()

        ESB.BuildCategoriesFilterFromTreeview(View.categoriesTV)
        Dim expansionDict = TreeViewsUtilities.SaveNodesExpansionsLevel(View.entitiesTV)
        Dim checkedList = TreeViewsUtilities.SaveCheckedStates(View.entitiesTV)
        Entity.LoadEntitiesTree(View.entitiesTV, ESB.StrSqlQueryForEntitiesUploadFunctions)
        TreeViewsUtilities.ResumeExpansionsLevel(View.entitiesTV, expansionDict)
        TreeViewsUtilities.ResumeCheckedStates(View.entitiesTV, checkedList)    ' a list of nodes filtered can be displayed

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

        Model.delete_model()

    End Sub

    Private Function GetAdjustmentsFilter() As List(Of String)

        Dim tmp_list As List(Of String) = TreeViewsUtilities.GetCheckedNodesID(View.adjustmentsTV)
        If tmp_list.Count <> View.adjustmentsTV.Nodes.Count Then
            Return tmp_list
        Else
            Return Nothing
        End If

    End Function

#End Region


End Class
