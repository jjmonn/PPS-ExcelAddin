' Constants.vb
'
'
'
'
'
' To do:
'
'
' Known Bugs:
'
'
' Author: Julien Monnereau
' Last modified: 23/02/2015




Module Constants


#Region "Tokens"

    Friend Const COST_OF_DEBT_ID As String = "icod"
    Friend Const CASH_CAPITALIZATION_ID As String = "iccap"
    Friend Const MARGINAL_TAX_RATE As String = "itrate"


#End Region


    ' Upload report
    Friend Const STATUS_FULL As String = "Full Chart of Accounts"
    Friend Const STATUS_PARTIAL As String = "Incomplete Chart of Accounts"


#Region "Security"

    Friend Const SNOW_KEY As String = "9!yh_U"
    Friend Const ANSII_FLOOR_TOKEN_CHAR = 97
    Friend Const ANSII_CEILING_TOKEN_CHAR = 122
    Friend Const ANSII_FLOOR_PWD_CHAR = 64
    Friend Const ANSII_CEILING_PWD_CHAR = 122
    Friend Const PWD_LENGHT = 7

#End Region

    ' Algorithms parameters 
    Friend Const LEVENSTEIN_THRESOLHD = 0.35
    Friend Const ACCOUNTS_SEARCH_ALGO_THRESHOLD = 0.6
    Friend Const EPSILON_PLUS = 60                                          'Dates search range +
    Friend Const EPSILON_MINUS = 10                                         'Dates search range -

    ' Look up Functions 
    Friend Const FORMULA_SEPARATOR = "|"
    Friend Const REFRESH_WAITING_TEXT = "#REFRESHING"


   
    '---------------------------------------------------
    ' --> Others <--
    '---------------------------------------------------
    Friend Const MONTHLY_TIME_PERIOD_FORMAT As String = ""
    Friend Const YEARLY_TIME_PERIOD_FORMAT As String = ""

    Friend Const Addin_Name = "ExcelAddIn2"
    Friend Const VALUE_FLAG = "V"
    Friend Const ADDRESS_FLAG = "A"
    Friend Const ACCOUNT_FLAG = "Account"
    Friend Const ASSET_FLAG = "Entity"
    Friend Const PERIOD_FLAG = "Period"
    Friend Const VERSION_FLAG = "Version"
    Friend Const ORIENTATION_ERROR_FLAG = "Orientation_error"

    Friend Const CIRCULAR_ERROR = 35614

    Friend Const OPERATORS_FLAG = "OPERATORFlag"
    Friend Const OPERANDS_FLAG = "OPERANDFlag"
    Friend Const CURRENCIES_SEPARATOR = "/"
    Friend Const INDEX_SEPARATOR = "."

    Friend Const QUERY_FLAG_FIND_PARENT = "withParent"
    Friend Const QUERY_FLAG_FIND_ID = "withoutParent"
    Friend Const PPSBI_FORMULA_CATEGORIES_SEPARATOR As String = ","



    '-------------------------------------------------------------
    ' -->  PPSBI FORMULA ARRAY INDEXES <--
    '-------------------------------------------------------------

    Friend Const FBI_FORMULA_ENTITY_INDEX As Integer = 1
    Friend Const FBI_FORMULA_ACCOUNT_INDEX As Integer = 2
    Friend Const FBI_FORMULA_PERIOD_INDEX As Integer = 3
    Friend Const FBI_FORMULA_CURRENCY_INDEX As Integer = 4

    Friend Const UDF_FORMULA_GET_DATA_NAME As String = "PPSBI"
    Friend Const NB_MONTHS = 12

    '-------------------------------------------------------------
    '               ERRORS
    '-------------------------------------------------------------

    ' PP001:
    ' PP002: Asset key not in mapping. Althought assetID come from ModelDataBase for injection in DB.
    ' 003:
    ' PP004: Asset Currency for the period is not in exchange rate table
    ' PP005: A wrong string has been passed to a function with mapping input "assets" or "accounts"
    ' PP006: The selected version corresponds to a versions folder. A version name should be selected
    ' PP007: UserID not present in the acf_uacs table -> could not give a credential
    ' PPS8: Error during data table creation 
    ' PPS9: Unknown or NULL time configuration


End Module
