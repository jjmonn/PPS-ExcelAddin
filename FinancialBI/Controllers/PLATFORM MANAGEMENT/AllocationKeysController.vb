Imports VIBlend.WinForms.Controls
Imports System.Collections.Generic
Imports CRUD


Public Class AllocationKeysController

#Region "Instance variables"

    Private m_allocationKeysView As AllocationKeysView
    Private m_entitiesTreeview As New vTreeView
    Private m_accountId As Int32

#End Region

#Region "Initialize"

    Friend Sub New(ByRef p_accountId As Int32, _
                   ByRef p_accountName As String)

        m_accountId = p_accountId
        GlobalVariables.AxisElems.LoadEntitiesTV(m_entitiesTreeview)
        m_allocationKeysView = New AllocationKeysView(Me, p_accountName, m_entitiesTreeview)
        Dim l_entitiesAllocationKeysDictionary As New Dictionary(Of Int32, Double)
        Dim l_entityDistributionMultiIndexDictionary = GlobalVariables.EntityDistribution.GetDictionary(p_accountId)
        If l_entityDistributionMultiIndexDictionary IsNot Nothing Then
            For Each l_entityDistribution As CRUD.EntityDistribution In l_entityDistributionMultiIndexDictionary.Values
                l_entitiesAllocationKeysDictionary.Add(l_entityDistribution.EntityId, l_entityDistribution.Percentage)
            Next
        End If
        ComputeCalculatedAllocationKeys(l_entitiesAllocationKeysDictionary)
        m_allocationKeysView.FillAllocationKeysDataGridView_ThreadSafe(l_entitiesAllocationKeysDictionary)
        m_allocationKeysView.Show()

        ' Server Events listening
        AddHandler GlobalVariables.EntityDistribution.Read, AddressOf EntityDistributionRead
        AddHandler GlobalVariables.EntityDistribution.UpdateEvent, AddressOf EntityDistributionUpdate

    End Sub

#End Region

#Region "Interface"

    Friend Sub UpdateAllocationKey(ByRef p_entityId As Int32, _
                                   ByRef p_value As Double)

        Dim l_entityDistribution As EntityDistribution = GetEntityDistributionCopy(p_entityId, m_accountId)
        If l_entityDistribution Is Nothing Then
            l_entityDistribution = New EntityDistribution()
            l_entityDistribution.AccountId = m_accountId
            l_entityDistribution.EntityId = p_entityId
            l_entityDistribution.Percentage = p_value
            GlobalVariables.EntityDistribution.Create(l_entityDistribution)
        Else
            l_entityDistribution.Percentage = p_value
            GlobalVariables.EntityDistribution.Update(l_entityDistribution)
        End If

    End Sub

    Private Function ComputeEntityAllocationKey(ByRef p_node As vTreeNode, _
                                                ByRef p_entitiesAllocationKeysDictionary As Dictionary(Of Int32, Double)) As Double

        If p_entitiesAllocationKeysDictionary.ContainsKey(p_node.Value) = False Then
            Dim l_consoKey As Double = 0
            For Each l_childEntity As vTreeNode In p_node.Nodes
                l_consoKey += ComputeEntityAllocationKey(l_childEntity, p_entitiesAllocationKeysDictionary)
            Next
            p_entitiesAllocationKeysDictionary.Add(p_node.Value, l_consoKey)
            Return l_consoKey
        Else
            Return p_entitiesAllocationKeysDictionary(p_node.Value)
        End If

    End Function

#End Region

#Region "Servers Events"

    Private Sub EntityDistributionRead(ByRef status As ErrorMessage, ByRef p_entityDistribution As EntityDistribution)

        If status = ErrorMessage.SUCCESS Then
            Dim l_entitiesAllocationKeysDictionary As Dictionary(Of Int32, Double) = m_allocationKeysView.GetEntityAllocationKeysDictionary
            l_entitiesAllocationKeysDictionary(p_entityDistribution.EntityId) = p_entityDistribution.Percentage
            ComputeCalculatedAllocationKeys(l_entitiesAllocationKeysDictionary)
            m_allocationKeysView.FillAllocationKeysDataGridView_ThreadSafe(l_entitiesAllocationKeysDictionary)
        Else
            m_allocationKeysView.SetAllocationKeyValue_ThreadSafe(p_entityDistribution)
        End If

    End Sub

    Private Sub EntityDistributionUpdate(ByRef status As ErrorMessage, ByRef id As UInt32)

        If status <> ErrorMessage.SUCCESS Then
            MsgBox(Local.GetValue("allocationKeys.msg_error_update"))
        End If

    End Sub


#End Region

#Region "Utilities"

    Friend Sub ComputeCalculatedAllocationKeys(ByRef p_entitiesAllocationKeysDictionary As Dictionary(Of Int32, Double))

        For Each l_node As vTreeNode In m_entitiesTreeview.Nodes
            ComputeEntityAllocationKey(l_node, p_entitiesAllocationKeysDictionary)
        Next

    End Sub

    Friend Function GetEntityDistribution(ByVal p_entityId As UInt32, ByVal p_accountId As UInt32) As CRUD.EntityDistribution
        Return GlobalVariables.EntityDistribution.GetValue(p_entityId, p_accountId)
    End Function

    Friend Function GetEntityDistributionCopy(ByVal p_entityId As UInt32, ByVal p_accountId As UInt32) As CRUD.EntityDistribution
        Dim l_entityDistribution As CRUD.EntityDistribution = GetEntityDistribution(p_entityId, p_accountId)
        If l_entityDistribution Is Nothing Then Return Nothing
        Return l_entityDistribution.Clone()
    End Function

#End Region

End Class
