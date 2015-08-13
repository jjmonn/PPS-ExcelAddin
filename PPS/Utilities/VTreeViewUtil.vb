Imports VIBlend.WinForms.Controls
Imports System.Drawing
Imports VIBlend.Utilities


Public Class VTreeViewUtil

    Public Shared Function AddNode(ByRef value As String, _
                                   ByRef text As String, _
                                   Optional ByRef parent As Object = Nothing) As vTreeNode

        Dim newNode As New vTreeNode()
        newNode.Text = text
        newNode.Value = value
        parent.Nodes.Add(newNode)
         Return newNode

    End Function


    Public Shared Sub InitTVFormat(ByRef TV As vTreeView)

        TV.VIBlendScrollBarsTheme = VIBLEND_THEME.OFFICESILVER

        ' Change the Expand/Collapse arrow color.
        TV.UseThemeIndicatorsColor = False
        TV.IndicatorsColor = Color.FromArgb(128, 128, 128)
        TV.IndicatorsHighlightColor = Color.FromArgb(128, 128, 128)
        TV.EnableIndicatorsAnimation = False
        TV.PaintNodesDefaultBorder = False
        TV.PaintNodesDefaultFill = False

    End Sub




End Class
