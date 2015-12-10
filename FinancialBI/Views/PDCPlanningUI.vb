Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic

Public Class PDCPlanningUI

    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.m_PDCDataGridViewPivotDesign.DataGridView = m_PDCDataGridView
        TestDataBind()

    End Sub

    Private Sub TestDataBind()

        Dim list As New List(Of Record)()
        Dim l_consultant As String
        Dim l_contractType As String
        Dim l_bu As String = "France"
        Dim l_client As String = "Safran"
        Dim l_day As Date
        Dim l_week As Int32

        For i As Integer = 0 To 7500
            l_consultant = "Consultant" & i
            If i < 2000 Then
                l_contractType = "CDD"
            Else
                l_contractType = "CDI"
            End If

            Select Case i
                Case Is > 1000 : l_bu = "Bordeaux"
                Case Is > 2000 : l_bu = "Paris"
                Case Is > 3000 : l_bu = "Lyon"
                Case Is > 4000 : l_bu = "Ankarra"
                Case Is > 5000 : l_bu = "Madrid"
                Case Is > 6000 : l_bu = "London"
            End Select

            Select Case i
                Case Is > 800 : l_client = "Airbus"
                Case Is > 1500 : l_client = "PSA"
                Case Is > 2300 : l_client = "Tesla"
                Case Is > 4800 : l_client = "Ariane"
            End Select

            Dim l_date As Date = New DateTime(2016, 1, 1).Date
            For d = 0 To 365
                If l_date.DayOfWeek = DayOfWeek.Saturday _
                Or l_date.DayOfWeek = DayOfWeek.Sunday Then
                    ' treat week-ends
                End If

                l_day = l_date.ToString
                l_week = Math.Round(d / 7, 0) + 1

                list.Add(New Record(l_consultant, _
                                                    l_contractType, _
                                                    l_bu, _
                                                    l_client, _
                                                    l_day, _
                                                    l_week))
                l_date = l_date.AddDays(1)

            Next d
        Next i

            m_PDCDataGridView.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK

        '        m_PDCDataGridView.BoundFields.Add(New BoundField("Consultant", "Consultant"))
        '       m_PDCDataGridView.BoundFields.Add(New BoundField("Contract type", "ContractType"))
        '      m_PDCDataGridView.BoundFields.Add(New BoundField("Business Unit", "BusinessUnit"))
        m_PDCDataGridView.BoundPivotRows.Add(New BoundField("Client", "Client"))
        '  m_PDCDataGridView.BoundFields.Add(New BoundField("Day", "Day"))
        m_PDCDataGridView.BoundPivotColumns.Add(New BoundField("Week", "Week"))
        m_PDCDataGridView.BoundPivotValues.Add(New BoundValueField("Number of consultants", "Consultant", PivotFieldFunction.Count))

        'm_PDCDataGridView.BoundFields.Add(New BoundField("Month", "Month"))
        '_PDCDataGridView.BoundFields.Add(New BoundField("Year", "Year"))


        m_PDCDataGridView.DataSource = list
        '     m_PDCDataGridView.VirtualModeCellDefault = True
        m_PDCDataGridView.DataBind()

        m_PDCDataGridView.RowsHierarchy.AutoResize()
        m_PDCDataGridView.ColumnsHierarchy.AutoResize()
        m_PDCDataGridView.Refresh()

    End Sub



#Region "Data classes"


    Friend Class Record
        Public Sub New(ByVal p_consultant As String, _
                       ByVal p_contractType As String, _
                       ByRef p_businessUnit As String, _
                       ByVal p_client As String, _
                       ByVal p_day As String, _
                       ByVal p_week As Int32)

            Me.m_consultant = p_consultant
            Me.m_contractType = p_contractType
            Me.m_businessUnit = p_businessUnit
            Me.m_client = p_client
            Me.m_day = p_day
            Me.m_week = p_week
            '    Me.m_month = p_day.Month.ToString()
            '   Me.m_year = p_day.Year

        End Sub

#Region "Private Members"

        Private m_consultant As String
        Private m_contractType As String
        Private m_businessUnit As String
        Private m_client As String
        Private m_day As Date
        Private m_week As Int32
        Private m_month As String
        Private m_year As Int32

#End Region

#Region "Properties"

        Public Property Consultant() As String
            Get
                Return m_consultant
            End Get
            Set(ByVal value As String)
                m_consultant = value
            End Set
        End Property

        Public Property ContractType() As String
            Get
                Return m_contractType
            End Get
            Set(ByVal value As String)
                m_contractType = value
            End Set
        End Property

        Public Property BusinessUnit() As String
            Get
                Return m_businessUnit
            End Get
            Set(ByVal value As String)
                m_businessUnit = value
            End Set
        End Property

        Public Property Client() As String
            Get
                Return m_client
            End Get
            Set(ByVal value As String)
                m_client = value
            End Set
        End Property

        Public Property Day() As String
            Get
                Return m_day
            End Get
            Set(ByVal value As String)
                m_day = value
            End Set
        End Property

        Public Property Week() As Int32
            Get
                Return m_week
            End Get
            Set(ByVal value As Int32)
                m_week = value
            End Set
        End Property

        Public Property Month() As String
            Get
                Return m_month
            End Get
            Set(ByVal value As String)
                m_month = value
            End Set
        End Property

        Public Property Year() As Int32
            Get
                Return m_year
            End Get
            Set(ByVal value As Int32)
                m_year = value
            End Set
        End Property

#End Region
    End Class

#End Region

End Class