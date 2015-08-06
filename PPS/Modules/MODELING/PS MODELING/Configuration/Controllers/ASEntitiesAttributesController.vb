' ASEntitiesAttributesController.vb
'
' To do:
'       -
'
'
'
' Author: Julien Monnereau
' Last modified: 10/02/2015 


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic



Friend Class ASEntitiesAttributesController

#Region "Instance Variables"

    ' Objects
    Private View As ASEntitiesAttributesUI
    Private EntitiesAttributes As New GDFSUEZEntitiesAttribute

    ' Variables
    Private entitiesTV As New TreeView
    Private entitiesNameKeyDic As Hashtable
    Private market_indexes_list As List(Of String)
   

#End Region


#Region "Initialize"

    Friend Sub New()

        Globalvariables.Entities.LoadEntitiesTV(entitiesTV)
        entitiesNameKeyDic = GlobalVariables.Entities.GetEntitiesDictionary(NAME_VARIABLE, ID_VARIABLE)
        market_indexes_list = MarketIndexesMapping.GetMarketIndexesList
        View = New ASEntitiesAttributesUI(Me, entitiesNameKeyDic, entitiesTV)
        DisplayDGVData()
        View.Show()

    End Sub

    Private Sub DisplayDGVData()

        Dim entities_dic As New Dictionary(Of Int32, Hashtable)
        Dim entities_list = TreeViewsUtilities.GetNodesKeysList(entitiesTV)
        entities_list = TreeViewsUtilities.GetNoChildrenNodesList(entities_list, entitiesTV)
        For Each entity_id In entities_list
            entities_dic.Add(entity_id, EntitiesAttributes.GetRecord(entity_id))
        Next
        View.FillDGV(entities_dic)

    End Sub

#End Region


#Region "Interface"

    Protected Friend Function UpdateGasFormula(ByRef entity_id As String, ByVal value As String) As Boolean

        Select Case FormulasGenericCheck.CheckFormula(value, market_indexes_list)
            Case 0
                MsgBox("The formula must only contains references to registered market indexes. " & Chr(13) & "Please verify that all indexes in the formula are registered.")
                Return False
            Case 1
                MsgBox("The syntax of the formula is not valid.")
                Return False
            Case 2
                EntitiesAttributes.UpdateEntity(entity_id, GDF_ENTITIES_AS_GAS_FORMULA_VAR, value)
                Return True
        End Select

    End Function

    Protected Friend Function UpdateLiquidsFormula(ByRef entity_id As String, ByVal value As String) As Boolean

        Select Case FormulasGenericCheck.CheckFormula(value, market_indexes_list)
            Case 0
                MsgBox("The formula must only contains references to registered market indexes. " & Chr(13) & "Please verify that all indexes in the formula are registered.")
                Return False
            Case 1
                MsgBox("The syntax of the formula is not valid.")
                Return False
            Case 2
                EntitiesAttributes.UpdateEntity(entity_id, GDF_ENTITIES_AS_LIQUID_FORMULA_VAR, value)
                Return True
        End Select

    End Function

    Protected Friend Sub UpdateTaxrate(ByRef entity_id As String, ByVal value As Double)

        EntitiesAttributes.UpdateEntity(entity_id, GDF_ENTITIES_AS_TAX_RATE_VAR, value)

    End Sub

#End Region



End Class
