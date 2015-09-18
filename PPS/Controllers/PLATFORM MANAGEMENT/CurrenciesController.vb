Imports System.Windows.Forms
Imports System.Collections




Friend Class CurrenciesController


#Region "Instance Variables"

    Private m_platformManagementInterface As PlatformMGTGeneralUI
    Private m_view As CurrenciesView


#End Region


#Region "Initialize"

    Friend Sub New()

        m_view = New CurrenciesView(Me, _
                                    GlobalVariables.Currencies.m_allCurrenciesHash)


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
        Dim currencyHT As Hashtable = GlobalVariables.Currencies.m_allCurrenciesHash(p_currencyId).clone
        currencyHT(CURRENCY_IN_USE_VARIABLE) = p_inUse
        GlobalVariables.Currencies.CMSG_UPDATE_CURRENCY(currencyHT)

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


End Class
