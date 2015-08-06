﻿' LogModel.vb
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


#End Region


#Region "Interface"

    Friend Function GetPeriodList(ByRef version_id As String) As Int32()

        Return GlobalVariables.Versions.GetPeriodsList(version_id)

    End Function

    Protected Friend Function GetTimeConfig(ByRef version_id As String) As String

        Return GlobalVariables.Versions.versions_hash(version_id)(VERSIONS_TIME_CONFIG_VARIABLE)

    End Function

    Protected Friend Sub ComputeEntity(ByRef entity_node As System.Windows.Forms.TreeNode, _
                                       ByRef version_id As String)

      
    End Sub

    Protected Friend Function GetData(ByRef account_id As String, _
                                      ByRef period As Int32) As Double


    End Function

    ' get the history of a data



#End Region


End Class
