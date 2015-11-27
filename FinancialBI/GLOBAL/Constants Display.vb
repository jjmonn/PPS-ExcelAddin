Module Display_Constants


    Public Const WINDOWS_TOP_MARGIN As Integer = 100
    Public Const WINDOWS_LEFT_MARGIN As Integer = 50


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

    Public Const CELLS_FORMAT = "n2"

    '---------------------------------------------------
    '---> Userforms style management<---
    '---------------------------------------------------

    Public Const GWL_STYLE = (-16)
    Public Const WS_CAPTION = &HC00000
    Public Const SWP_FRAMECHANGED = &H20
    Public Const MAX_RATES_DISPLAY_HEIGHT = 400
    Public Const CELLS_HEIGHT = 15
    Public Const CELLS_WIDTH = 50

    'Code for Userforms

    Public Structure RECT
        Dim Left As Long
        Dim Top As Long
        Dim Right As Long
        Dim Bottom As Long
    End Structure



End Module
