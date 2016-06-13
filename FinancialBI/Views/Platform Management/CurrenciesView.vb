' CurrenciesView.vb
'
' User interface for currencies in use or not
'
'
'
' Author: Julien Monnereau
' Created: 18/09/2015
' Last modified: 09/11/2015

Imports VIBlend.WinForms.DataGridView
Imports System.Collections
Imports System.Collections.Generic
Imports VIBlend.WinForms.Controls
Imports CRUD


Friend Class CurrenciesView


#Region "Instance Variables"

    Private m_controller As CurrenciesController
    Private m_CurrentCell As GridCell = Nothing
    Private m_columnsWidth As Single = 110
    Private m_rightMgr As New RightManager

#End Region


#Region "Initialize"

    Friend Sub New(ByRef p_currenciesController As CurrenciesController, _
                   ByRef p_currenciesHash As MultiIndexDictionary(Of UInt32, String, NamedCRUDEntity))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = p_currenciesController
        m_currenciesDataGridView.RowsHierarchy.Visible = False
        InitializeCurrenciesdgvColumns()
        CreateCurrenciesRows(p_currenciesHash)
        m_currenciesDataGridView.RowsHierarchy.SortBy(m_currenciesDataGridView.ColumnsHierarchy.Items(1), SortingDirection.Ascending)

        AddHandler m_currenciesDataGridView.CellMouseEnter, AddressOf DataGridView_CellMouseEnter
        DefineUIPermissions()
        DesactivateUnallowed()
        MultilanguageSetup()

    End Sub

    Private Sub DefineUIPermissions()
        m_rightMgr(ValidateButton) = Group.Permission.EDIT_CURRENCIES
    End Sub

    Private Sub DesactivateUnallowed()
        m_rightMgr.Enable(GlobalVariables.Users.GetCurrentUserRights())
    End Sub

    Private Sub InitializeCurrenciesdgvColumns()

        ' In use column
        Dim inUseColumn As HierarchyItem = m_currenciesDataGridView.ColumnsHierarchy.Items.Add("Active")
        inUseColumn.ItemValue = CURRENCY_IN_USE_VARIABLE
        inUseColumn.TextAlignment = Drawing.ContentAlignment.MiddleCenter
        inUseColumn.CellsTextAlignment = Drawing.ContentAlignment.MiddleCenter
        inUseColumn.Width = m_columnsWidth

        ' Currency short name
        Dim nameColumn As HierarchyItem = m_currenciesDataGridView.ColumnsHierarchy.Items.Add("Currency")
        nameColumn.ItemValue = NAME_VARIABLE
        nameColumn.Width = m_columnsWidth

        ' Currency Symbol
        Dim symbolColum As HierarchyItem = m_currenciesDataGridView.ColumnsHierarchy.Items.Add("Symbol")
        symbolColum.TextAlignment = Drawing.ContentAlignment.MiddleCenter
        symbolColum.CellsTextAlignment = Drawing.ContentAlignment.MiddleCenter
        symbolColum.Width = m_columnsWidth
        symbolColum.ItemValue = CURRENCY_SYMBOL_VARIABLE

    End Sub

    Friend Sub CreateCurrenciesRows(ByRef p_currenciesHash As MultiIndexDictionary(Of UInt32, String, NamedCRUDEntity))

        For Each currency As Currency In p_currenciesHash.Values
            Dim currencyRow As HierarchyItem = m_currenciesDataGridView.RowsHierarchy.Items.Add("")
            Dim inUsecheckBoxEditor As New CheckBoxEditor
            currencyRow.ItemValue = currency.Id
            If GlobalVariables.Users.CurrentUserHasRight(Group.Permission.EDIT_CURRENCIES) Then
                m_currenciesDataGridView.CellsArea.SetCellEditor(currencyRow, m_currenciesDataGridView.ColumnsHierarchy.Items(0), inUsecheckBoxEditor)
            End If
            m_currenciesDataGridView.CellsArea.SetCellValue(currencyRow, m_currenciesDataGridView.ColumnsHierarchy.Items(0), currency.InUse)
            m_currenciesDataGridView.CellsArea.SetCellValue(currencyRow, m_currenciesDataGridView.ColumnsHierarchy.Items(1), currency.Name)
            m_currenciesDataGridView.CellsArea.SetCellValue(currencyRow, m_currenciesDataGridView.ColumnsHierarchy.Items(2), currency.Symbol)
            AddHandler inUsecheckBoxEditor.CheckedChanged, AddressOf DataGridView_CheckedChanged

        Next

    End Sub

    Private Sub MultilanguageSetup()

        Me.SetMainCurrencyCallBack.Text = Local.GetValue("currencies.set_main_currency")
        Me.ValidateButton.Text = Local.GetValue("general.save")



    End Sub


#End Region


#Region "Events and Call Backs"

    Private Sub SetMainCurrency_Click(sender As Object, e As EventArgs) Handles SetMainCurrencyCallBack.Click

        If m_CurrentCell Is Nothing Then
            MsgBox(Local.GetValue("currencies.msg_select_currency"))
            Exit Sub
        End If
        m_controller.SetMainCurrency(m_CurrentCell.RowItem.ItemValue)

    End Sub

    Private Sub DataGridView_CellMouseEnter(ByVal sender As Object, ByVal args As CellEventArgs)

        m_CurrentCell = args.Cell

    End Sub

    Private Sub DataGridView_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub ValidateButton_Click(sender As Object, e As EventArgs) Handles ValidateButton.Click
        m_controller.UpdateCurrencies(m_currenciesDataGridView)
    End Sub

#End Region

End Class
