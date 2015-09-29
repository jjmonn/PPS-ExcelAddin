' CurrenciesView.vb
'
' User interface for currencies in use or not
'
'
'
' Author: Julien Monnereau
' Created: 18/09/2015
' Last modified: 18/09/2015

Imports VIBlend.WinForms.DataGridView
Imports System.Collections
Imports VIBlend.WinForms.Controls



Friend Class CurrenciesView


#Region "Instance Variables"

    Private m_controller As CurrenciesController
    Private m_CurrentCell As GridCell = Nothing
    Private m_columnsWidth As Single = 110

#End Region


#Region "Initialize"

    Friend Sub New(ByRef p_currenciesController As CurrenciesController, _
                   ByRef p_currenciesHash As Hashtable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = p_currenciesController
        m_currenciesDataGridView.RowsHierarchy.Visible = False
        InitializeCurrenciesdgvColumns()
        CreateCurrenciesRows(p_currenciesHash)
        m_currenciesDataGridView.RowsHierarchy.SortBy(m_currenciesDataGridView.ColumnsHierarchy.Items(1), SortingDirection.Ascending)

        AddHandler m_currenciesDataGridView.CellMouseEnter, AddressOf DataGridView_CellMouseEnter
        DesactivateUnallowed()
    End Sub

    Private Sub DesactivateUnallowed()
        If Not GlobalVariables.Users.CurrentUserIsAdmin() Then
            ValidateButton.Enabled = False
        End If
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

    End Sub

    Friend Sub CreateCurrenciesRows(ByRef p_currenciesHash As Hashtable)

        For Each currencyId As Int32 In p_currenciesHash.Keys
            Dim currencyRow As HierarchyItem = m_currenciesDataGridView.RowsHierarchy.Items.Add("")
            Dim inUsecheckBoxEditor As New CheckBoxEditor
            currencyRow.ItemValue = currencyId
            If GlobalVariables.Users.CurrentUserIsAdmin() Then
                m_currenciesDataGridView.CellsArea.SetCellEditor(currencyRow, m_currenciesDataGridView.ColumnsHierarchy.Items(0), inUsecheckBoxEditor)
            End If
            m_currenciesDataGridView.CellsArea.SetCellValue(currencyRow, m_currenciesDataGridView.ColumnsHierarchy.Items(0), p_currenciesHash(currencyId)(CURRENCY_IN_USE_VARIABLE))
            m_currenciesDataGridView.CellsArea.SetCellValue(currencyRow, m_currenciesDataGridView.ColumnsHierarchy.Items(1), p_currenciesHash(currencyId)(NAME_VARIABLE))
            m_currenciesDataGridView.CellsArea.SetCellValue(currencyRow, m_currenciesDataGridView.ColumnsHierarchy.Items(2), p_currenciesHash(currencyId)(CURRENCY_SYMBOL_VARIABLE))
            AddHandler inUsecheckBoxEditor.CheckedChanged, AddressOf DataGridView_CheckedChanged

        Next

    End Sub


#End Region


#Region "Events and Call Backs"

    Private Sub SetMainCurrency_Click(sender As Object, e As EventArgs) Handles SetMainCurrencyCallBack.Click

        If m_CurrentCell Is Nothing Then
            MsgBox("A Currency must be selected first")
            Exit Sub
        End If
        m_controller.SetMainCurrency(m_CurrentCell.RowItem.ItemValue)

    End Sub

    Private Sub DataGridView_CellMouseEnter(ByVal sender As Object, ByVal args As CellEventArgs)

        m_CurrentCell = args.Cell

    End Sub

    Private Sub DataGridView_CheckedChanged(sender As Object, e As EventArgs)

        If Not m_CurrentCell Is Nothing Then
            Dim checkBox As vCheckBox = TryCast(m_currenciesDataGridView.CellsArea.GetCellEditor(m_CurrentCell.RowItem, m_currenciesDataGridView.ColumnsHierarchy.Items(0)).Control, vCheckBox)
            m_controller.UpdateCurrency(m_CurrentCell.RowItem.ItemValue, _
                                        checkBox.Checked)
        End If

    End Sub


#End Region



End Class
