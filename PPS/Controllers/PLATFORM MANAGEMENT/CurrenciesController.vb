Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports CRUD

Friend Class CurrenciesController


#Region "Instance Variables"

    Private m_platformManagementInterface As PlatformMGTGeneralUI
    Private m_view As CurrenciesView


#End Region


#Region "Initialize"

    Friend Sub New()

        m_view = New CurrenciesView(Me, GlobalVariables.Currencies.GetDictionary())

    End Sub

    Public Sub addControlToPanel(ByRef p_destinationPanel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        m_platformManagementInterface = PlatformMGTUI
        p_destinationPanel.Controls.Add(m_view)
        m_view.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Public Sub close()

        m_view.Dispose()

    End Sub

#End Region


#Region "Interface"

    Friend Sub UpdateCurrency(ByRef p_currencyId As Int32, _
                              ByRef p_inUse As Boolean)

        ' register into temp updates for update list - priority normal
        Dim currencyHT As Currency = GetCurrencyCopy(p_currencyId)

        If currencyHT Is Nothing Then Exit Sub
        currencyHT.InUse = p_inUse
        GlobalVariables.Currencies.Update(currencyHT)

    End Sub

    Friend Sub UpdateCurrencies(ByRef p_dataGridView As vDataGridView)

        Dim listCurrencies As New List(Of CRUDEntity)

        For Each row As HierarchyItem In p_dataGridView.RowsHierarchy.Items
            Dim l_currency As Currency = GetCurrencyCopy(row.ItemValue)

            If l_currency Is Nothing Then Continue For
            For Each column As HierarchyItem In p_dataGridView.ColumnsHierarchy.Items
                Dim l_cellValue = p_dataGridView.CellsArea.GetCellValue(row, column)

                Select Case column.ItemValue
                    Case NAME_VARIABLE
                        l_currency.Name = CType(l_cellValue, String)
                    Case CURRENCY_SYMBOL_VARIABLE
                        l_currency.Symbol = CType(l_cellValue, String)
                    Case CURRENCY_IN_USE_VARIABLE
                        l_currency.InUse = CType(l_cellValue, Boolean)
                End Select
            Next
            listCurrencies.Add(l_currency)
        Next

        GlobalVariables.Currencies.UpdateList(listCurrencies)

    End Sub

    Friend Sub SendUpdates()

        ' update list implement !!! 
        '    GlobalVariables.Currencies.u

    End Sub

    Friend Sub SetMainCurrency(ByRef p_currencyId As Int32)

        GlobalVariables.Currencies.CMSG_SET_MAIN_CURRENCY(p_currencyId)

    End Sub


#End Region

#Region "Events"

    ' priority low
    ' listen READ
    ' listen update 


#End Region

#Region "Utilities"

    Friend Function GetCurrency(ByVal p_id As UInt32) As Currency
        Return GlobalVariables.Currencies.GetValue(p_id)
    End Function

    Friend Function GetCurrencyCopy(ByVal p_id As UInt32) As Currency
        Dim l_currency = GetCurrency(p_id)

        If l_currency Is Nothing Then Return Nothing
        Return l_currency.Clone()
    End Function
#End Region
End Class
