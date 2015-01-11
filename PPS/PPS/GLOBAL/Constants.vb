Imports Microsoft.Office.Interop

Module Constants


    '**********************************************************************
    ' Module: VARIABLE AND CONSTANTS
    '
    ' List all constant values used in the modules
    '
    '
    '
    '**********************************************************************


    '---------------------------------------------------------------------
    '          VARIABLES
    '---------------------------------------------------------------------


    '---------------------------------------------------
    ' --> Ribbon variables
    '---------------------------------------------------

    'Ribbon object

    'Connction toggle button state
    Public connectionToggle As Boolean

    Public apps As Excel.Application
  
    '---------------------------------------------------
    ' --> Connections
    '---------------------------------------------------

    'ADODB Connection object
    Public ConnectioN As ADODB.Connection

    'ADODB Command object
    Public cmd As ADODB.Command
   
    'Code for Userforms

    Public Structure RECT
        Dim Left As Long
        Dim Top As Long
        Dim Right As Long
        Dim Bottom As Long
    End Structure


    Public Declare Function FindWindowA Lib "user32" _
            (ByVal lpClassName As String, ByVal lpWindowName As String) As Long

    Public Declare Function GetWindowRect Lib "user32" _
            (ByVal hwnd As Long, lpRect As RECT) As Long

    Public Declare Function GetWindowLong Lib "user32" Alias _
            "GetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long) As Long

    Public Declare Function SetWindowLong Lib "user32" Alias _
            "SetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long, _
            ByVal dwNewLong As Long) As Long

    Public Declare Function SetWindowPos Lib "user32" _
            (ByVal hwnd As Long, ByVal hWndInsertAfter As Long, ByVal X As Long, _
            ByVal Y As Long, ByVal CX As Long, ByVal CY As Long, _
            ByVal wFlags As Long) As Long



    '---------------------------------------------------------------------
    '           CONSTANTS
    '---------------------------------------------------------------------


    '---------------------------------------------------
    '---> Connection <---
    '---------------------------------------------------

    Public Const DRIVER_NAME = "MySQL ODBC 5.2w Driver"
    Public Const SOURCE = "MySQL"
    Public Const SERVER_LOCATION = "173.194.81.170"
    Public Const DATABASE = "test"
    Public Const USER = "root"
    Public Const PASSWORD = "surfer"

    '---------------------------------------------------
    '---> Excel Set Up <---
    '---------------------------------------------------

    Public Const ADDIN_NAME = "ExcelAddIn2"

    '---------------------------------------------------
    '---> Databases and tables name <---
    '---------------------------------------------------

    ' Affiliates table
    Public Const AFFILIATES_TABLE = "affiliates"
    Public Const AFFILIATES_ID_VARIABLE = "affiliateID"
    Public Const AFFILIATES_NAME_VARIABLE = "affiliateName"

    ' Assets table
    Public Const ASSETS_TABLE = "assets"
    Public Const ASSETS_NAME_VARIABLE = "assetName"
    Public Const ASSETS_TREE_ID_VARIABLE = "assetID"
    Public Const ASSETS_AFFILIATE_ID_VARIABLE = "affiliateID"
    Public Const ASSETS_PARENT_ID_VARIABLE = "Ass_Par_ID"
    Public Const ASSETS_CATEGORY_VARIABLE = "assetCategory"
    Public Const ASSETS_COUNTRY_VARIABLE = "assetCountry"
    Public Const ASSETS_CURRENCY_VARIABLE = "assetCurrency"
    Public Const ASSETS_IMAGE_VARIABLE = "Ass_Image"
    Public Const ASSETS_SELECTED_IMAGE_VARIABLE = "Ass_Selected_Image"
    Public Const ASSETS_POSITION_VARIABLE = "ass_position"

    Public Const ASSETS_COLUMN_NB_ASSETID = 1


    ' Accounts table
    Public Const ACCOUNTS_TEMP_TABLE = "accounts3"
    Public Const ACCOUNT_TREE_VARIABLE = "acc_Tree"
    Public Const ACCOUNT_TREE_ID_VARIABLE = "acc_tree_ID"
    Public Const ACCOUNT_NAME_VARIABLE = "acc_Name"
    Public Const ACCOUNT_POSITION_VARIABLE = "acc_Pos"
    Public Const ACCOUNT_PARENT_ID_VARIABLE = "acc_Par_ID"
    Public Const ACCOUNT_FORMULA_TYPE_VARIABLE = "acc_Formula_Type"
    Public Const ACCOUNT_FORMULA_VARIABLE = "acc_Formula"
    Public Const ACCOUNT_IMAGE_VARIABLE = "acc_image"
    Public Const ACCOUNT_SELECTED_IMAGE_VARIABLE = "acc_Sel_Image"

    Public Const ACCOUNT_COLUMN_NB_ACCOUNTID = 2

    ' Data table
    Public Const DATA_TABLE = "data"
    Public Const DATA_ID_VARIABLE = "data_ID"
    Public Const DATA_ASSET_ID_VARIABLE = "data_ass_ID"
    Public Const DATA_ACCOUNT_ID_TABLE = "data_acc_ID"
    Public Const DATA_PERIOD_VARIABLE = "data_period"
    Public Const DATA_VALUE_VARIABLE = "data_value"
    Public Const DATA_CURRENCY_VARIABLE = "data_currency"

    ' Account reference table
    Public Const ACCOUNTS_REFERENCES_TABLE = "accounts_references"
    Public Const ACCOUNTS_REFERENCES_ID_VARIABLE = "acc_ref_ID"
    Public Const ACCOUNTS_REFERENCES_WORD_VARIABLE = "acc_Ref_Desc"

    ' Assets Selection Table
    Public Const ASSETS_SELECTION_TABLE = "assets_selection"
    Public Const ASSETS_SELECTION_ASSET_VARIABLE = "AssetID"
    Public Const ASSETS_SELECTION_SELECTION_VARIABLE = "Selection"

    ' Currencies table
    Public Const EXCHANGE_RATES_TABLE_NAME = "exchange_rates"
    Public Const EX_TABLE_CURRENCY_VARIABLE = "currency"
    Public Const EX_TABLE_PERIOD_VARIABLE = "period"
    Public Const EX_TABLE_RATE_VARIABLE = "rate"
    Public Const EX_TABLE_RATE_ID_VARIABLE = "rateID"

    ' DataSet Array
    Public Const DATA_ARRAY_ASSET_COLUMN = 0
    Public Const DATA_ARRAY_ACCOUNT_COLUMN = 1
    Public Const DATA_ARRAY_PERIOD_COLUMN = 2
    Public Const DATA_ARRAY_DATA_COLUMN = 3

    ' AssetExtraData Array
    Public Const ASSET_EXTRA_ARRAY_AFFILIATE = 0
    Public Const ASSET_EXTRA_ARRAY_CATEGORY = 1
    Public Const ASSET_EXTRA_ARRAY_COUNTRY = 2
    Public Const ASSET_EXTRA_ARRAY_CURRENCY = 3

    ' ExtraData table
    Public Const EXTRA_DATA_TABLE_NAME = "extradata"
    Public Const EXTRA_DATA_KEY_VARIABLE = "Variable"
    Public Const EXTRA_DATA_VALUE_VARIABLE = "Value_"

    ' Snapshot DataTable
    Public Const SELECTION_COLUMN_TITLE = "Selection"

    ' DownloadDataTable
    ' Note : columns names are equals to data table on servers
    Public Const NB_COLUMNS_DOWNLOAD_DATATABLE As Integer = 4


    '---------------------------------------------------
    '---> Algorithms parameters <---
    '---------------------------------------------------

    Public Const LEVENSTEIN_THRESOLHD = 0.4
    Public Const ACCOUNTS_SEARCH_ALGO_THRESHOLD = 0.6
    Public Const EPSILON_PLUS = 60                                          'Dates search range +
    Public Const EPSILON_MINUS = 10                                         'Dates search range -

    '---------------------------------------------------
    '---> Look up Functions <---
    '---------------------------------------------------

    Public Const CHARL = "{ ()+,.-/=}"
    Public Const FORMULA_SEPARATOR = "|"

    '---------------------------------------------------
    '---> Userforms style management<---
    '---------------------------------------------------

    Public Const GWL_STYLE = (-16)
    Public Const WS_CAPTION = &HC00000
    Public Const SWP_FRAMECHANGED = &H20
    Public Const MAX_RATES_DISPLAY_HEIGHT = 400
    Public Const CELLS_HEIGHT = 15
    Public Const CELLS_WIDTH = 50

    '---------------------------------------------------
    ' --> Formulas <--
    '---------------------------------------------------

    Public Const FORMULA_TYPE_TITLE = "T"
    Public Const FORMULA_TYPE_HARD_VALUE = "HV"
    Public Const FORMULA_TYPE_SUM_OF_CHILDREN = "SOAC"
    Public Const FORMULA_TYPE_FORMULA = "F"
    Public Const FORMULA_TYPE_OTHER_LINE = "OL"
    Public Const FORMULA_TYPE_BALANCE_SHEET = "BS"
    Public Const FORMULA_TYPE_OPENING_BALANCE = "OB"

    '---------------------------------------------------
    ' --> Others <--
    '---------------------------------------------------

    Public Const VALUE_FLAG = "V"
    Public Const ADDRESS_FLAG = "A"
    Public Const ACCOUNT_FLAG = "Account"
    Public Const ASSET_FLAG = "Asset"
    Public Const PERIOD_FLAG = "Period"
    Public Const VERSION_FLAG = "Version"

    Public Const CIRCULAR_ERROR = 35614
    Public Const TV_ROOT_KEY = "."
    Public Const STARTING_PERIOD_KEY = "SP"
    Public Const ENDING_PERIOD_KEY = "EP"
    Public Const OPERATORS_FLAG = "OPERATORFlag"
    Public Const OPERANDS_FLAG = "OPERANDFlag"
    Public Const MAIN_CURRENCY = "EUR"
    Public Const CURRENCIES_SEPARATOR = "/"
    Public Const INDEX_SEPARATOR = "."


    '---------------------------------------------------
    ' --> ModelingApp and GLOBALMC <--
    '---------------------------------------------------

    Public ModelingApp As Excel.Application
    Public GLOBALMC As ModelComputer

    Public Const MODEL_BUILT_CELL_FLAG = "BB9999"
    Public Const MODEL_BUILT_FLAG_TRUE = "modelBuilt"
    Public Const MODEL_BUILT_FLAG_FALSE = "modelNotBuilt"
    Public Const MODELING_WS_NAME = "01MWSPPS1"


    '---------------------------------------------------
    ' --> Download UI <--
    '---------------------------------------------------

    Public Const BUTTON_HEIGHT As Double = 27
    Public Const BUTTON_WIDTH As Double = 27
    Public Const CONTROLS_LEFT As Double = 10
    Public Const LABEL_LEFT As Double = 50
    Public Const LABEL_WIDTH As Double = 200

    Public Const BT_ASSETS_ORIGINAL_TOP As Double = 20
    Public Const BT_ACCOUNTS_ORIGINAL_TOP As Double = 80
    Public Const BT_PERIODS_ORIGINAL_TOP As Double = 140
    Public Const BT_ASSETS_LINE_ORIGINAL_TOP As Double = 60
    Public Const BT_ACCOUNTS_LINE_ORIGINAL_TOP As Double = 120
    Public Const BT_PERIODS_LINE_ORIGINAL_TOP As Double = 180
    Public Const LINE_WIDTH As Double = 1
    Public Const LINE_LENGHT As Double = 730

    Public Const V_MARGINS As Double = 20
    Public Const GROUPS_LEFT As Double = 20
    Public Const GROUPS_HEIGHT As Double = 400
    Public Const GROUPS_WIDTH As Double = 740

    Public Const TREEVIEWS_LEFT As Double = 30
    Public Const TREEVIEWS_TOP As Double = 65
    Public Const TREEVIEWS_HEIGHT As Double = 317
    Public Const TREEVIEWS_WIDTH As Double = 360

    Public Const LISTS_LEFT As Double = 420
    Public Const LISTS_TOP As Double = 65
    Public Const LISTS_HEIGHT As Double = 317
    Public Const LISTS_WIDTH As Double = 288



    '-------------------------------------------------------------
    '               ERRORS
    '-------------------------------------------------------------

    ' PP001:
    ' PP002: Asset key not in mapping. Althought assetID come from ModelDataBase for injection in DB.
    ' 003:
    ' PP004: Asset Currency for the period is not in exchange rate table
    ' PP005: A wrong string has been passed to a function with mapping input "assets" or "accounts"


End Module
