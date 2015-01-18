' gdfsuezAlternativeScenarios.vb
'
' Description of the module + constants
'
'
'
'
' Author: Julien Monnereau
' Last modified: 15/01/2015



Module gdfsuezAlternativeScenarios

#Region "DataBase Specifics"

    ' Data Base Constants
    Friend Const GDF_SENSITIVITIES_LIST_TABLE As String = "gdfsuez_sensitivities"
    Friend Const GDF_SENSITIVITIES_ITEM_VAR As String = "item"
    Friend Const GDF_SENSITIVITIES_VOLUMES_ACCOUNT_ID_VAR As String = "volumes_account_id"
    Friend Const GDF_SENSITIVITIES_REVENUES_ACCOUNT_ID_VAR As String = "revenues_account_id"
    Friend Const GDF_SENSITIVITIES_INITIAL_UNIT_VAR As String = "initial_unit"
    Friend Const GDF_SENSITIVITIES_DEST_UNIT_VAR As String = "destination_unit"
    Friend Const GDF_SENSITIVITIES_FORMULA_NAME_VAR As String = "formula_name"

    Friend Const GDF_ENTITIES_AS_ATTRIBUTES_TABLE As String = "gdfsuez_entities_as_attributes"
    Friend Const GDF_ENTITIES_AS_ENTITY_ID_VAR As String = "entity_id"
    Friend Const GDF_ENTITIES_AS_GAS_FORMULA_VAR As String = "gas_formula"
    Friend Const GDF_ENTITIES_AS_LIQUID_VAR As String = "liquid_formula"
    Friend Const GDF_ENTITIES_AS_TAX_RATE_VAR As String = "tax_rate"

    Friend Const GDF_AS_ACCOUNTS_TABLES As String = "gdfsuez_as_accounts"
    Friend Const GDF_AS_ACCOUNT_ID_VAR As String = "account_id"
    Friend Const GDF_AS_ITEM_VAR As String = "as_item"

    Friend Const GDF_AS_REPORTS_TABLE As String = "gdfsuez_as_reports"
    Friend Const GDF_AS_REPORTS_ID_VAR As String = "id"
    Friend Const GDF_AS_REPORTS_PARENT_ID_VAR As String = "parent_id"
    Friend Const GDF_AS_REPORTS_NAME_VAR As String = "name"
    Friend Const GDF_AS_REPORTS_ACCOUNT_ID As String = "account_id"
    Friend Const GDF_AS_REPORTS_TYPE_VAR As String = "type"
    Friend Const GDF_AS_REPORTS_COLOR_VAR As String = "color"
    Friend Const GDF_AS_REPORTS_PALETTE_VAR As String = "palette"
    Friend Const GDF_AS_REPORTS_AXIS1_NAME_VAR As String = "axis1"
    Friend Const GDF_AS_REPORTS_AXIS2_NAME_VAR As String = "axis2"
    Friend Const GDF_AS_REPORTS_SERIE_AXIS_VAR As String = "serie_axis"
    Friend Const GDF_AS_REPORTS_SERIE_UNIT_VAR As String = "serie_unit"
    Friend Const GDF_AS_REPORTS_DISPLAY_VALUES_VAR As String = "display_values"

    Friend Const GDF_CHART_REPORT_TYPE As String = "C"
    Friend Const GDF_TABLE_REPORT_TYPE As String = "T"


#End Region



End Module
