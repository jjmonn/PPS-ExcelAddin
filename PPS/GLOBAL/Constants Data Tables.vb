' Constants Data Tables.vb  
'
' Databases and tables name 
'
'
'
' To do: 
'      
'
' Last modified: 01/09/2015
' Author: Julien Monnereau



Module Data_Tables_Constants




#Region "Common All Tables"

    Friend Const ID_VARIABLE As String = "id"
    Friend Const PARENT_ID_VARIABLE As String = "parent_id"
    Friend Const NAME_VARIABLE As String = "name"
    Friend Const IMAGE_VARIABLE As String = "image"
    Friend Const IS_FOLDER_VARIABLE = "is_folder"
    Friend Const ITEMS_POSITIONS As String = "item_position"
    Friend Const NAMES_MAX_LENGTH As Int32 = 100

#End Region


#Region "Operational and Financials Items"

#Region "Accounts Table"

    ' Accounts table
    Friend Const ACCOUNT_FORMULA_TYPE_VARIABLE = "account_formula_type"
    Friend Const ACCOUNT_FORMULA_VARIABLE = "account_formula"
    Friend Const ACCOUNT_IMAGE_VARIABLE = "image"
    Friend Const ACCOUNT_SELECTED_IMAGE_VARIABLE = "selected_image"
    Friend Const ACCOUNT_FORMAT_VARIABLE = "format_id"
    Friend Const ACCOUNT_TAB_VARIABLE = "account_tab"
    Friend Const ACCOUNT_TYPE_VARIABLE = "type_id"
    Friend Const ACCOUNT_CONSOLIDATION_OPTION_VARIABLE = "computation_option"
    Friend Const ACCOUNT_CONVERSION_OPTION_VARIABLE = "conversion_flag"

#End Region

#Region "FactLog Table"

    ' Accounts table
    Friend Const FACTLOG_USER_VARIABLE = "factlog_user"
    Friend Const FACTLOG_DATE_VARIABLE = "factlog_date"
    Friend Const FACTLOG_CLIENT_ID_VARIABLE = "factlog_client_id"
    Friend Const FACTLOG_PRODUCT_ID_VARIABLE = "factlog_product_id"
    Friend Const FACTLOG_ADJUSTMENT_ID_VARIABLE = "factlog_adjustment_id"
    Friend Const FACTLOG_VALUE_VARIABLE = "factlog_value"

#End Region

#Region "Accounts Types DataTable"

    Friend Const ACCOUNT_TYPE_TABLE = "account_types"
    Friend Const ACCOUNT_TYPE_CODE_VARIABLE = "id"
    Friend Const ACCOUNT_TYPE_NAME_VARIABLE = "type_name"

    Friend Const DEFAULT_ACCOUNT_TYPE_VALUE = "MO"      ' should be in DB
    Friend Const MONETARY_ACCOUNT_TYPE As String = "MO" ' should be in DB
    Friend Const NORMAL_ACCOUNT_TYPE As String = "NU"
    Friend Const MONETARY_NAME_TYPE As String = "Monetary"

#End Region

#Region "Formula Types Tables"

    Friend Const FORMULA_TYPES_TABLE As String = "formula_types"
    Friend Const FORMULA_TYPES_CODE_VARIABLE As String = "formula_type_code"
    Friend Const FORMULA_TYPES_NAME_VARIABLE As String = "formula_type_name"
    Friend Const FORMULA_TYPES_MODEL_CODE As String = "formula_type_model_code"
    Friend Const FORMULA_TYPES_MODEL_INCLUSION As String = "included_in_modeling"
    Friend Const FORMULA_TYPES_MUST_HAVE_F As String = "must_contain_formula"


#End Region

#Region "Account reference table"

    Friend Const ACCOUNTS_REFERENCES_TABLE = "accounts_references"
    Friend Const ACCOUNTS_REFERENCES_ID_VARIABLE = "acc_ref_ID"
    Friend Const ACCOUNTS_REFERENCES_WORD_VARIABLE = "acc_Ref_Desc"

#End Region


#End Region


#Region "Currencies and Exchange Rates"

    Friend Const CURRENCY_SYMBOL_VARIABLE As String = "symbol"
    Friend Const CURRENCY_IN_USE_VARIABLE As String = "in_use"
    Friend Const EX_RATES_DESTINATION_CURR_VAR As String = "local_currency"
    Friend Const EX_RATES_RATE_VERSION As String = "rate_version_id"
    Friend Const EX_RATES_PERIOD_VARIABLE As String = "rate_period"
    Friend Const EX_RATES_RATE_VARIABLE As String = "rate_value"

#End Region


#Region "Formats table"

    Friend Const FORMATS_TABLE_NAME = "formats"
    Friend Const FORMAT_CODE_VARIABLE = "code"
    Friend Const FORMAT_TEXT_COLOR_VARIABLE = "text_color"
    Friend Const FORMAT_BOLD_VARIABLE = "font_bold"
    Friend Const FORMAT_ITALIC_VARIABLE = "font_italic"
    Friend Const FORMAT_BORDER_VARIABLE = "border"
    Friend Const FORMAT_BCKGD_VARIABLE = "background"
    Friend Const FORMAT_NAME_VARIABLE = "name"
    Friend Const FORMAT_ICON_VARIABLE = "icon"
    Friend Const FORMAT_INDENT_VARIABLE = "indent"
    Friend Const FORMAT_DESTINATION_VARIABLE = "destination"
    Friend Const FORMAT_UP_BORDER_VARIABLE = "up_border_color"
    Friend Const FORMAT_BOTTOM_BORDER_VARIABLE = "bottom_border_color"

    Friend Const REPORT_FORMAT_CODE = "report"          ' Value for report style formats
    Friend Const INPUT_FORMAT_CODE = "input"            ' Value for input style formats

    Friend Const TITLE_FORMAT_CODE As String = "t"
    Friend Const HV_FORMAT_CODE As String = "l"
    Friend Const DEFAULT_FORMAT_STRING As String = "{0:C0}"
    Friend Const PRCT_FORMAT_STRING As String = "{0:P0}"


#End Region


#Region "Versioning Table"

    Friend Const VERSIONS_CREATION_DATE_VARIABLE As String = "created_at"
    Friend Const VERSIONS_LOCKED_VARIABLE As String = "locked"
    Friend Const VERSIONS_LOCKED_DATE_VARIABLE As String = "locked_date"
    Friend Const VERSIONS_TIME_CONFIG_VARIABLE As String = "time_config"
    Friend Const VERSIONS_RATES_VERSION_ID_VAR As String = "rates_version_id"
    Friend Const VERSIONS_START_PERIOD_VAR As String = "start_period"
    Friend Const VERSIONS_NB_PERIODS_VAR As String = "nb_periods"
    Friend Const VERSIONS_GLOBAL_FACT_VERSION_ID As String = "global_fact_version_id"


    ' Associated constants values
    Friend Const YEARLY_TIME_CONFIGURATION As String = "years"
    Friend Const MONTHLY_TIME_CONFIGURATION As String = "months"
  
 

#End Region


#Region "Financial Modeling Accounts Table"

    Friend Const FINANCIAL_MODELLING_TABLE = "fmodeling_accounts"
    Friend Const FINANCIAL_MODELLING_ID_VARIABLE = "id"
    Friend Const FINANCIAL_MODELLING_PARENT_ID_VARIABLE = "parent_id"
    Friend Const FINANCIAL_MODELLING_NAME_VARIABLE = "name"
    Friend Const FINANCIAL_MODELLING_TYPE_VARIABLE = "type"
    Friend Const FINANCIAL_MODELLING_FORMAT_VARIABLE = "format_string"
    Friend Const FINANCIAL_MODELLING_ACCOUNT_ID_VARIABLE = "account_id"
    Friend Const FINANCIAL_MODELLING_MAPPED_ENTITY_VARIABLE = "mapped_entity"
    Friend Const FINANCIAL_MODELLING_SERIE_COLOR_VARIABLE = "serie_color"
    Friend Const FINANCIAL_MODELLING_SERIE_TYPE_VARIABLE = "serie_type"
    Friend Const FINANCIAL_MODELLING_SERIE_CHART_VARIABLE = "serie_chart"

    Friend Const FINANCIAL_MODELLING_INPUT_TYPE = "input"
    Friend Const FINANCIAL_MODELLING_OUTPUT_TYPE = "output"
    Friend Const FINANCIAL_MODELLING_FORMULA_TYPE = "formula"
    Friend Const FINANCIAL_MODELLING_EXPORT_TYPE = "export"
    Friend Const FINANCIAL_MODELLING_CONSTRAINT_TYPE = "constraint"

#End Region


#Region "Operators"

    Friend Const OPERATORS_TABLE As String = "operators"
    Friend Const OPERATOR_ID_VARIABLE As String = "id"
    Friend Const OPERATOR_SYMBOL_VARIABLE As String = "symbol"


#End Region


#Region "Controls"

    Friend Const CONTROLS_TABLE As String = "controls"
    Friend Const CONTROL_ID_VARIABLE As String = "id"
    Friend Const CONTROL_NAME_VARIABLE As String = "name"
    Friend Const CONTROL_ITEM1_VARIABLE As String = "item1"
    Friend Const CONTROL_ITEM2_VARIABLE As String = "item2"
    Friend Const CONTROL_OPERATOR_ID_VARIABLE As String = "operator_id"
    Friend Const CONTROL_PERIOD_OPTION_VARIABLE As String = "period_option"

    Friend Const CONTROLS_PERIOD_OPTION As String = "AP"
    Friend Const CONTROLS_SUM_PERIOD_OPTION As String = "SU"
    Friend Const CONTROLS_TOKEN_SIZE As Int32 = 3

#End Region


#Region "Control Options"

    Friend Const CONTROL_OPTIONS_TABLE As String = "control_options"
    Friend Const CONTROL_OPTION_ID_VARIABLE As String = "id"
    Friend Const CONTROL_OPTION_NAME_VARIABLE As String = "name"


#End Region


#Region "Charts"

#Region "Charts (Control)"

    Friend Const CONTROL_CHART_TABLE As String = "control_charts"
    Friend Const CONTROL_CHART_ID_VARIABLE As String = "id"
    Friend Const CONTROL_CHART_NAME_VARIABLE As String = "name"
    Friend Const CONTROL_CHART_PARENT_ID_VARIABLE As String = "parent_id"
    Friend Const CONTROL_CHART_ACCOUNT_ID_VARIABLE As String = "account_id"
    Friend Const CONTROL_CHART_TYPE_VARIABLE As String = "type"
    Friend Const CONTROL_CHART_COLOR_VARIABLE As String = "color"
    Friend Const CONTROL_CHART_PALETTE_VARIABLE As String = "palette"

    Friend Const CONTROL_CHARTS_TOKEN_SIZE As Int32 = 5

#End Region


#Region "Serie Colors and Serie Types"

    Friend Const SERIE_COLORS_TABLE As String = "serie_colors"
    Friend Const SERIE_COLORS_ID_VARIABLE As String = "id"

    Friend Const SERIE_TYPE_TABLE As String = "serie_types"
    Friend Const SERIE_TYPE_ID_VARIABLE As String = "id"

#End Region


#Region "Palettes"

    Friend Const PALETTES_TABLE As String = "palettes"
    Friend Const PALETTES_ID_VARIABLES As String = "id"


#End Region

#End Region


#Region "Operational Items Conversions"

    Friend Const OPERATIONAL_UNITS_TABLE As String = "operational_units"
    Friend Const OPERATIONAL_UNITS_ID_VARIABLE As String = "id"

    Friend Const OPERATIONAL_UNITS_CONVERSION_TABLE As String = "operational_unit_conversions"
    Friend Const OPERATIONAL_UNITS_CONVERSION_ID_VAR As String = "id"
    Friend Const OPERATIONAL_UNITS_CONVERSION_RATE_VAR As String = "rate"

#End Region


#Region "Market Indexes"

    Friend Const MARKET_INDEXES_TABLE As String = "market_indexes"
    Friend Const MARKET_INDEXES_ID_VARIABLE As String = "id"
  
    Friend Const MARKET_INDEXES_PRICES_TABLE As String = "market_index_prices"
    Friend Const MARKET_INDEXES_PRICES_ID_VAR As String = "id"
    Friend Const MARKET_INDEXES_PRICES_VERSION_VAR As String = "version_id"
    Friend Const MARKET_INDEXES_PRICES_PERIOD_VAR As String = "period"
    Friend Const MARKET_INDEXES_PRICES_VALUE_VAR As String = "value"

    Friend Const MARKET_INDEXES_VERSIONS_TABLE As String = "market_index_versions"
    Friend Const MARKET_INDEXES_VERSIONS_ID_VAR As String = "id"
    Friend Const MARKET_INDEXES_VERSIONS_PARENT_ID_VAR As String = "parent_id"
    Friend Const MARKET_INDEXES_VERSIONS_NAME_VAR As String = "name"
    Friend Const MARKET_INDEXES_VERSIONS_IS_FOLDER_VAR As String = "is_folder"
    Friend Const MARKET_INDEXES_VERSIONS_START_PERIOD_VAR As String = "start_period"
    Friend Const MARKET_INDEXES_VERSIONS_NB_PERIODS_VAR As String = "nb_periods"
    Friend Const MARKET_INDEXES_VERSIONS_TOKEN_SIZE As Int32 = 3

#End Region


#Region "PPS Colors Palette"

    Friend Const PPS_COLORS_TABLE As String = "pps_colors"
    Friend Const PPS_COLORS_ID_VAR As String = "id"
    Friend Const PPS_COLORS_RED_VAR As String = "red"
    Friend Const PPS_COLORS_GREEN_VAR As String = "green"
    Friend Const PPS_COLORS_BLUE_VAR As String = "blue"

#End Region


#Region "Data Tables"

    Friend Const ENTITY_ID_VARIABLE = "entity_id"
    Friend Const ACCOUNT_ID_VARIABLE = "account_id"
    Friend Const PERIOD_VARIABLE = "period"
    Friend Const VERSION_ID_VARIABLE = "version_id"
    Friend Const CLIENT_ID_VARIABLE = "client_id"
    Friend Const PRODUCT_ID_VARIABLE = "product_id"
    Friend Const ADJUSTMENT_ID_VARIABLE = "adjustment_id"
    Friend Const VALUE_VARIABLE = "value"
    Friend Const GLOBAL_FACT_ID_VARIABLE = "global_fact_id"

#End Region


#Region "Log Table"

    Friend Const LOG_TIME_VARIABLE As String = "date_time"
    Friend Const LOG_USER_ID_VARIABLE As String = "user_id"


#End Region


#Region "Axis and Filters"

    Friend Const ENTITIES_CURRENCY_VARIABLE As String = "entity_currency"
    Friend Const ENTITIES_ALLOW_EDITION_VARIABLE As String = "allow_edition"
    Friend Const ADJUSTMENTS_TABLE As String = "adjustments"
    Friend Const CLIENTS_TABLE As String = "clients"
    Friend Const PRODUCTS_TABLE As String = "products"
    Friend Const FILTER_VALUE_ID_VARIABLE As String = "filter_value_id"
 
    ' filters
    Friend Const AXIS_ID_VARIABLE As String = "axis_id"
    Friend Const FILTER_IS_PARENT_VARIABLE As String = "is_parent"
    Friend Const NON_ATTRIBUTED_SUFIX As String = "na"

    ' filters_values
    Friend Const FILTER_ID_VARIABLE As String = "filter_id"
    Friend Const PARENT_FILTER_VALUE_ID_VARIABLE As String = "parent_filter_value_id"

    Friend Const AXIS_NAME_FORBIDEN_CHARS = ","
    Friend Const DEFAULT_ANALYSIS_AXIS_ID As Int32 = 1


#End Region


#Region "users"


    Friend Const USERS_TABLE As String = "users"
    Friend Const USERS_ID_VARIABLE As String = "id"
    Friend Const USERS_IS_FOLDER_VARIABLE As String = "is_folder"
    Friend Const USERS_ENTITY_ID_VARIABLE As String = "entity_id"
    Friend Const USERS_PARENT_ID_VARIABLE As String = "parent_id"
    Friend Const USERS_CREDENTIAL_LEVEL_VARIABLE As String = "credential_level"
    Friend Const USERS_CREDENTIAL_TYPE_VARIABLE As String = "credential_type"
    Friend Const USERS_EMAIL_VARIABLE As String = "user_email"

    Friend Const USERS_TOKEN_SIZE As Int32 = 3
    Friend Const USERS_ID_MAX_SIZE As Int32 = 15


#End Region

   
    Friend Const REPORTS_AXIS1_NAME_VAR As String = "report_axis1_name"
    Friend Const REPORTS_AXIS2_NAME_VAR As String = "report_axis2_name"

    Friend Const REPORTS_PALETTE_VAR As String = "palette_var"
    Friend Const REPORTS_DISPLAY_VALUES_VAR As String = "display_values_var"
    Friend Const REPORTS_SERIE_WIDTH_VAR As String = "serie_width_var"
    Friend Const REPORTS_SERIE_AXIS_VAR As String = "serie_axis_var"
    Friend Const REPORTS_TYPE_VAR As String = "report_type_var"
    Friend Const REPORTS_COLOR_VAR As String = "report_color_var"
    Friend Const REPORTS_SERIE_UNIT_VAR As String = "serie_unit_var"
    Friend Const CHART_REPORT_TYPE As String = "chart_report_type_var"
    Friend Const TABLE_REPORT_TYPE As String = "table_report_type_var"

   


End Module
