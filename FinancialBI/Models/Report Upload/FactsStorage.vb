Imports CRUD
Imports System.Collections.Generic
Imports System.Linq


Friend Class FactsStorage

#Region "Instance variables"

    Friend m_FactsDict As New SafeDictionary(Of String, SafeDictionary(Of String, SafeDictionary(Of String, Fact)))
    ' m_FactsDict dimensions : (accountsName)(employeeName)(periodToken) -> Fact
    Private m_requestIdDict As New SafeDictionary(Of UInt32, String())

    Public Event FactsDownloaded(ByRef p_status As Boolean)
    Private m_factDownloadErrorFlag As Boolean

#End Region

#Region "Interface"

    Friend Sub LoadRHFacts(ByRef p_entityName As String, _
                           ByRef p_accountsList As List(Of String), _
                           ByRef p_employeeList As List(Of String), _
                           ByRef p_versionId As UInt32, _
                           ByRef p_startPeriod As UInt32, _
                           ByRef p_endPeriod As UInt32)


        Dim l_entity As AxisElem = GlobalVariables.AxisElems.GetValue(AxisType.Entities, p_entityName)
        ' attention dans ce cas message user
        If l_entity Is Nothing Then Exit Sub

        AddHandler FactsManager.Read, AddressOf LoadRHFacts_ThreadSafe
        m_requestIdDict.Clear()
        m_factDownloadErrorFlag = False
        For Each l_accountName As String In p_accountsList
            Dim l_account As Account = GlobalVariables.Accounts.GetValue(l_accountName)
            If l_account Is Nothing Then Continue For

            For Each l_employeeName As String In p_employeeList
                Dim l_employee As AxisElem = GlobalVariables.AxisElems.GetValue(AxisType.Employee, l_employeeName)
                If l_employee Is Nothing Then Continue For

                m_requestIdDict.Add(FactsManager.CMSG_GET_FACT(l_account.Id, l_entity.Id, l_employee.Id, p_versionId, p_startPeriod, p_endPeriod), _
                                    {l_accountName, l_employeeName})
            Next
        Next

    End Sub

    Friend Function GetRHFact(ByRef p_accountName As String, ByRef p_employeeName As String, ByRef p_periodToken As String) As Fact

        If m_FactsDict.ContainsKey(p_accountName) _
        AndAlso m_FactsDict(p_accountName).ContainsKey(p_employeeName) _
        AndAlso m_FactsDict(p_accountName)(p_employeeName).ContainsKey(p_periodToken) Then
            Return m_FactsDict(p_accountName)(p_employeeName)(p_periodToken)
        Else
            Return Nothing
        End If

    End Function

    Friend Sub Flush()

        If m_FactsDict IsNot Nothing Then m_FactsDict.Clear()
        If m_requestIdDict IsNot Nothing Then m_requestIdDict.Clear()

    End Sub

#End Region

#Region "Events"

    Private Sub LoadRHFacts_ThreadSafe(p_status As Boolean, _
                                      p_requestId As UInt32, _
                                      p_factsList As List(Of Fact))

        If p_status <> False Then
            Dim l_accountName As String = m_requestIdDict(p_requestId)(0)
            Dim l_employeeName As String = m_requestIdDict(p_requestId)(1)

            If m_FactsDict.ContainsKey(l_accountName) = False Then
                Dim l_accountFactsDict As New SafeDictionary(Of String, SafeDictionary(Of String, Fact))
                m_FactsDict.Add(l_accountName, l_accountFactsDict)
            End If
            Dim l_factsDict As New SafeDictionary(Of String, Fact)
            BuildRHPeriodsFactsDict(p_factsList, l_factsDict)
            m_FactsDict(l_accountName)(l_employeeName) = l_factsDict
            m_requestIdDict.Remove(p_requestId)
            If m_requestIdDict.Count = 0 Then
                RaiseEvent FactsDownloaded(True)
                RemoveHandler FactsManager.Read, AddressOf LoadRHFacts_ThreadSafe
            End If
        Else
            RaiseEvent FactsDownloaded(False)
            RemoveHandler FactsManager.Read, AddressOf LoadRHFacts_ThreadSafe
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
