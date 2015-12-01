Imports VIBlend.WinForms.Controls

' AllocationKeysView.vb
'
' User interface for managing allocation keys
'
'
' Created on: 30/11/2015
' Last modified: 30/11/2015


Public Class AllocationKeysView

#Region "Instance variables"

    Private m_accountId As Int32
    Private m_entitiesTreeview As New vTreeView

#End Region


#Region "Initialize"

    Friend Sub New(ByRef p_accountId As Int32)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.



    End Sub

    Private Sub InitializeMultilanguage()

        GlobalVariables.AxisElems.LoadEntitiesTV(m_entitiesTreeview)


    End Sub

    Private Sub InitializeDataGridView()




    End Sub

#End Region







End Class