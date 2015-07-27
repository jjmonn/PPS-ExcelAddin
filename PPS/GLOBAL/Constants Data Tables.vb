' Constants Data Tables.vb  
'
' Databases and tables name 
'
'
'
' To do: 
'      
'
' Last modified: 25/06/2015
' Author: Julien Monnereau



Module Data_Tables_Constants

    Friend Const ITEMS_POSITIONS As String = "item_position"
    Friend Const NAMES_MAX_LENGTH As Int32 = 100


#Region "Common All Tables"

    Friend Const ID_VARIABLE As String = "id"
    Friend Const PARENT_ID_VARIABLE As String = "parent_id"
    Friend Const NAME_VARIABLE As String = "name"
    Friend Const IMAGE_VARIABLE As String = "image"


#End Region


#Region "ACF_Config Database"

    ' Stores the model definition tables
  

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

    ' constant to be stored in extra data table -> below = STUB !!
    Friend Const DEFAULT_F_TYPE_ACC_CREATION As String = "Hard Value Input"
    Friend Const AGGREGATION_F_TYPE_CODE As String = "SOAC"
    Friend Const HARD_VALUE_F_TYPE_CODE As String = "HV"

#End Region

#Region "Account reference table"

    Friend Const ACCOUNTS_REFERENCES_TABLE = "accounts_references"
    Friend Const ACCOUNTS_REFERENCES_ID_VARIABLE = "acc_ref_ID"
    Friend Const ACCOUNTS_REFERENCES_WORD_VARIABLE = "acc_Ref_Desc"

#End Region


#End Region



#Region "Currencies and Exchange Rates"

#Region "Currencies and Currencies Symbol Tables"

    Friend Const CURRENCIES_TABLE_NAME As String = "currencies"
    Friend Const CURRENCIES_SYMBOLS_TABLE_NAME As String = "currencies_symbols"
    Friend Const CURRENCIES_KEY_VARIABLE As String = "id"
    Friend Const CURRENCIES_SYMBOL_VARIABLE As String = "symbol"
    Friend Const CURRENCIES_NAME_VARIABLE As String = "name"
    Friend Const CURRENCIES_TOKEN_SIZE As Int32 = 3


#End Region


#Region "Exchange Rates table"

    Friend Const EXCHANGE_RATES_TABLE_NAME = "exchange_rates"
    Friend Const EX_RATES_CURRENCY_VARIABLE = "currency"
    Friend Const EX_RATES_PERIOD_VARIABLE = "period"
    Friend Const EX_RATES_RATE_VARIABLE = "value"
    Friend Const EX_RATES_RATE_ID_VARIABLE = "id"
    Friend Const EX_RATES_RATE_VERSION As String = "rates_version_id"

    Friend Const RATE_ID_SIZE As Int32 = 3

#End Region


#Region "Exchange Rates Versions Table"

    Friend Const RATES_VERSIONS_TABLE As String = "exchange_rates_versions"
    Friend Const RATES_VERSIONS_ID_VARIABLE As String = "id"
    Friend Const RATES_VERSIONS_NAME_VARIABLE As String = "name"
    Friend Const RATES_VERSIONS_IS_FOLDER_VARIABLE As String = "is_folder"
    Friend Const RATES_VERSIONS_PARENT_CODE_VARIABLE As String = "parent_id"

    Friend Const RATES_VERSIONS_START_PERIOD_VAR As String = "start_period"
    Friend Const RATES_VERSIONS_NB_PERIODS_VAR As String = "nb_periods"
    Friend Const RATES_VERSIONS_TOKEN_SIZE As Int32 = 3


#End Region

#End Region


#Region "ExtraData table"

    Friend Const EXTRA_DATA_TABLE_NAME = "extradatas"
    Friend Const EXTRA_DATA_KEY_VARIABLE = "variable"
    Friend Const EXTRA_DATA_VALUE_VARIABLE = "stored_value"

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

    Friend Const VERSIONS_TABLE = "versions"
    Friend Const VERSIONS_NAME_VARIABLE = "name"
    Friend Const VERSIONS_CREATION_DATE_VARIABLE = "created_at"
    Friend Const VERSIONS_LOCKED_VARIABLE = "locked"
    Friend Const VERSIONS_LOCKED_DATE_VARIABLE = "locked_date"
    Friend Const VERSIONS_CODE_VARIABLE = "id"
    Friend Const VERSIONS_PARENT_CODE_VARIABLE = "parent_id"
    Friend Const VERSIONS_IS_FOLDER_VARIABLE = "is_folder"
    Friend Const VERSIONS_TIME_CONFIG_VARIABLE = "time_config"
    Friend Const VERSIONS_RATES_VERSION_ID_VAR = "rates_version_id"
    Friend Const VERSIONS_START_PERIOD_VAR = "start_period"
    Friend Const VERSIONS_NB_PERIODS_VAR = "nb_periods"


    ' Associated constants values
    Friend Const VERSIONS_TOKEN_SIZE As Int32 = 3
    Friend Const YEARLY_TIME_CONFIGURATION = "years"
    Friend Const MONTHLY_TIME_CONFIGURATION = "months"
  
    ' Table information: Holds information on data versioning.
    '          
    ' Primary key: version_Name
    '
    ' Each version corresponds to a copy of a Data Table
    ' 
    ' Privileges: 
    '       a) Write: only databases administrator have writting privilege on this table
    '       b) Read: reading is granted for everybody
    '
    ' 
    ' VIEWS : no views


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


#End Region


#Region "ACF_Data"

    ' Stores the data tables


#Region "Data Tables"

    Friend Const DATA_ENTITY_ID_VARIABLE = "entity_id"
    Friend Const DATA_ACCOUNT_ID_VARIABLE = "account_id"
    Friend Const DATA_PERIOD_VARIABLE = "period"
    Friend Const DATA_VALUE_VARIABLE = "value"
    Friend Const DATA_ADJUSTMENT_ID_VARIABLE = "adjustment_id"
    Friend Const DATA_CLIENT_ID_VARIABLE = "client_id"
    Friend Const DATA_PRODUCT_ID_VARIABLE = "product_id"

#End Region

#Region "Log Table"

    Friend Const LOG_TABLE_NAME As String = "datalog"
    Friend Const LOG_TIME_VARIABLE As String = "date_time"
    Friend Const LOG_USER_ID_VARIABLE As String = "user_id"
    Friend Const LOG_VERSION_ID_VARIABLE As String = "version_id"
    Friend Const LOG_VALUE_VARIABLE As String = "value"
    Friend Const LOG_ENTITY_ID_VARIABLE As String = "entity_id"
    Friend Const LOG_ACCOUNT_ID_VARIABLE As String = "account_id"
    Friend Const LOG_ADJUSTMENT_ID_VARIABLE As String = "adjustment_id"
    Friend Const LOG_CLIENT_ID_VARIABLE As String = "client_id"
    Friend Const LOG_PRODUCT_ID_VARIABLE As String = "product_id"
    Friend Const LOG_PERIOD_VARIABLE As String = "period"


#End Region


#End Region


#Region "Axis and Filters"

    Friend Const ENTITIES_CURRENCY_VARIABLE = "entity_currency"
    Friend Const ENTITIES_ALLOW_EDITION_VARIABLE = "allow_edition"


    Friend Const ADJUSTMENTS_TABLE As String = "adjustments"
    Friend Const CLIENTS_TABLE As String = "clients"
    Friend Const PRODUCTS_TABLE As String = "products"

    Friend Const FILTER_VALUE_ID_VARIABLE As String = "filter_value_id"
    Friend Const ENTITY_ID_VARIABLE As String = "entity_id"
    Friend Const CLIENT_ID_VARIABLE As String = "client_id"
    Friend Const PRODUCT_ID_VARIABLE As String = "product_id"
    Friend Const ADJUSTMENT_ID_VARIABLE As String = "adjustment_id"


    ' filters
    Friend Const FILTER_AXIS_ID_VARIABLE As String = "axis_id"
    Friend Const FILTER_IS_PARENT_VARIABLE As String = "is_parent"
    Friend Const NON_ATTRIBUTED_SUFIX As String = "na"

    ' filters_values
    Friend Const FILTER_ID_VARIABLE As String = "filter_id"
    Friend Const PARENT_FILTER_VALUE_ID_VARIABLE As String = "parent_filter_value_id"

    Friend Const AXIS_NAME_FORBIDEN_CHARS = ","
    Friend Const DEFAULT_ANALYSIS_AXIS_ID As String = "aaa"


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

    ' DATASET ARRAY COLUMNS INDEXING - 
#Region "DataSet DataArray indexes"

    Friend Const DATA_ARRAY_ASSET_COLUMN = 0
    Friend Const DATA_ARRAY_ACCOUNT_COLUMN = 1
    Friend Const DATA_ARRAY_PERIOD_COLUMN = 2
    Friend Const DATA_ARRAY_DATA_COLUMN = 3

#End Region



    ' Snapshot DataTable
    Friend Const SELECTION_COLUMN_TITLE = "Selection"


   


End Module
