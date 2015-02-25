' LogModel.vb
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 20/01/2015


Imports System.Collections.Generic


Friend Class LogModel

#Region "Instance Variables"

    ' Objects
    Private Computer As New GenericSingleEntityDLL3Computer(GlobalVariables.GlobalDBDownloader)
    Private Versions As New Version

#End Region


#Region "Interface"

    Protected Friend Function GetPeriodList(ByRef version_id As String) As List(Of Int32)

        Return Versions.GetPeriodList(version_id)

    End Function

    Protected Friend Function GetTimeConfig(ByRef version_id As String) As String

        Return Versions.ReadVersion(version_id, VERSIONS_TIME_CONFIG_VARIABLE)

    End Function

    Protected Friend Sub ComputeEntity(ByRef entity_id As String, _
                                       ByRef version_id As String)

        Computer.ComputeSingleEntity(version_id, entity_id)

    End Sub

    Protected Friend Function GetData(ByRef account_id As String, _
                                      ByRef period As Int32) As Double

        Return Computer.GetDataFromDLL3Computer(account_id, period)

    End Function

    ' get the history of a data



#End Region


End Class
