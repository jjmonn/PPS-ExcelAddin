Imports System.Collections

Public Class SortableHashtable : Inherits Hashtable : Implements IComparable

    Dim m_sortKey As Object

    Public Sub New(ByRef p_sortKet As Object)
        m_sortKey = p_sortKet
    End Sub

    Public Overloads Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        If obj Is Nothing Then Return 0
        Dim cmpHashtable As Hashtable = TryCast(obj, Hashtable)

        If cmpHashtable Is Nothing Then Return 0
        If cmpHashtable(m_sortKey) > Me(m_sortKey) Then Return -1 Else Return 1
    End Function

End Class
