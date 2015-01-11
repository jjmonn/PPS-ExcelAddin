' LogModel.vb
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 07/01/2015


Imports System.Collections.Generic


Friend Class LogModel

#Region "Instance Variables"

    ' Objects
    Private Computer As New GenericSingleEntityComputer


#End Region


#Region "Interface"

    Protected Friend Function GetPeriodList(ByRef version_id As String) As List(Of Int32)

        Return Computer.VERSIONSMGT.GetPeriodList(version_id)

    End Function

    Protected Friend Function GetTimeConfig(ByRef version_id As String) As String

        Return Computer.VERSIONSMGT.versionsCodeTimeSetUpDict(version_id)(VERSIONS_TIME_CONFIG_VARIABLE)

    End Function

    Protected Friend Sub ComputeEntity(ByRef entity_id As String, _
                                       ByRef version_id As String)

        Computer.ComputeSingleEntity(version_id, entity_id)

    End Sub

    Protected Friend Function GetData(ByRef account_id As String, _
                                      ByRef period As Int32) As Double

        Return Computer.GetDataFromComputer(account_id, period, MAIN_CURRENCY)

    End Function

    ' get the history of a data



#End Region


End Class
