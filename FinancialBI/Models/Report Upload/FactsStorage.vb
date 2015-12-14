Imports CRUD
Imports System.Collections.Generic
Imports System.Linq


Friend Class FactsStorage

#Region "Instance variables"

    Private m_factController As New FactController
    Friend m_FactsDict As New SafeDictionary(Of String, SafeDictionary(Of String, SafeDictionary(Of String, Fact)))
    ' m_FactsDict dimensions : (accountsName)(productName)(period) -> Fact


#End Region

#Region "Interface"

    Friend Sub LoadRHFacts(ByRef p_accountsList As List(Of String), _
                           ByRef p_productList As List(Of String), _
                           ByRef p_versionId As UInt32, _
                           ByRef p_startPeriod As UInt32, _
                           ByRef p_endPeriod As UInt32)

        For Each l_accountName As String In p_accountsList
            Dim l_account As Account = GlobalVariables.Accounts.GetValue(l_accountName)
            If l_account Is Nothing Then Continue For

            For Each l_productName As String In p_productList
                Dim l_product As AxisElem = GlobalVariables.AxisElems.GetValue(AxisType.Product, l_productName)
                If l_product Is Nothing Then Continue For

                Dim l_factsList As New Action(Of List(Of Fact))(AddressOf LoadRHFacts_ThreadSafe)
                m_factController.GetFact(l_account.Id, l_product.Id, p_versionId, p_startPeriod, p_endPeriod, l_factsList)
            Next
        Next

    End Sub

#End Region

#Region "Events"

    Private Sub LoadRHFacts_ThreadSafe(p_factsList As List(Of Fact))

        If p_factsList.Count > 0 Then
            Dim l_account As Account = GlobalVariables.Accounts.GetValue(p_factsList.ElementAt(0).AccountId)
            If l_account Is Nothing Then Exit Sub
            Dim l_accountName As String = l_account.Name

            Dim l_product As AxisElem = GlobalVariables.AxisElems.GetValue(AxisType.Product, p_factsList.ElementAt(0).ProductId)
            If l_product Is Nothing Then Exit Sub
            Dim l_productName As String = l_product.Name

            If m_FactsDict.ContainsKey(l_accountName) = False Then
                Dim l_accountFactsDict As New SafeDictionary(Of UInt32, SafeDictionary(Of String, Fact))
            End If
            Dim l_factsDict As New SafeDictionary(Of String, Fact)
            BuildRHPeriodsFactsDict(p_factsList, l_factsDict)
            m_FactsDict(l_accountName)(l_productName) = l_factsDict
        End If

    End Sub

    Private Sub BuildRHPeriodsFactsDict(ByRef p_factsList As List(Of Fact), _
                                        ByRef p_factsDict As SafeDictionary(Of String, Fact))

        For Each l_fact As Fact In p_factsList
            p_factsDict.Add(Computer.DAY_PERIOD_IDENTIFIER & l_fact.Period, l_fact)
        Next

    End Sub

#End Region


End Class
