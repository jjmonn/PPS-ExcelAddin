' AlternativeScenariosController.vb
'
'
' To do: 
'       -
'
'
'
' Author:Julien Monnereau
' Last modified:16/01/2015



Friend Class AlternativeScenariosController


#Region "Instance Variables"

    ' Objects
    Private Model As New AlternativeScenarioModel
    Private InputsController As ASInputsController
    Private View As AlternativeScenariosUI

    ' Variables



#End Region


#Region "Initialize"

    Protected Friend Sub New()

        InputsController = New ASInputsController(Me)
        View = New AlternativeScenariosUI(Me, InputsController)


    End Sub


#End Region



#Region "Interface"

    Protected Friend Sub ComputeAlternativeScenario()


        If InputsController.ValidateInputsSelection = True Then



        End If

        Dim version_id As String = View.VersionTB.Text
        ' Compute base scenario -> model
        ' for each sensi
        '   -> compute sensi -> store result

        ' Aggregate sensis and compute new scenario -> Model

        ' Display -> View

    End Sub



#End Region


#Region "Utilities"


    Private Sub InitializePBar()

        '   Dim LoadingBarMax As Integer = Model.inputs_entities_list.Count + 7
        '   View.PBar.Launch(1, LoadingBarMax)

    End Sub

#End Region


End Class
