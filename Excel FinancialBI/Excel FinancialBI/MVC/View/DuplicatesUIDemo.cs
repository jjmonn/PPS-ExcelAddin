using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBI.MVC.View
{
  using VIBlend.WinForms.DataGridView;
  using VIBlend.Utilities;
  using Forms;

  public partial class DuplicatesUIDemo : Form
  {
    FbiDataGridView m_DGV = new FbiDataGridView();
    HierarchyItem m_employeesColumn;
    HierarchyItem m_periodColumn;
    HierarchyItem m_entityColumn;
    HierarchyItem m_clientColumn;
    HierarchyItem m_userColumn;
    HierarchyItem m_siteColumn;
    HierarchyItem m_actionColumn;

    public DuplicatesUIDemo()
    {
      InitializeComponent();
      LoadView();
    }

    private void DuplicatesUIDemo_Load(object sender, EventArgs e)
    {
      m_DGV.Refresh();
    }

    private void LoadView()
    {
      this.Controls.Add(m_DGV);
      m_DGV.Dock = DockStyle.Fill;
      InitializeColumns();
      StubFillRowsDemo();
    //  m_DGV.ColumnsHierarchy.AutoStretchColumns = true;
      m_DGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL);
      m_DGV.Refresh();
    }

    private void InitializeColumns()
    {
      m_employeesColumn = m_DGV.ColumnsHierarchy.Items.Add("Employee");
      m_periodColumn = m_DGV.ColumnsHierarchy.Items.Add("Period");
      m_entityColumn = m_DGV.ColumnsHierarchy.Items.Add("Entity");
      m_clientColumn = m_DGV.ColumnsHierarchy.Items.Add("Client");
      m_userColumn = m_DGV.ColumnsHierarchy.Items.Add("User");
      m_siteColumn = m_DGV.ColumnsHierarchy.Items.Add("Site SEGULA");
      m_actionColumn = m_DGV.ColumnsHierarchy.Items.Add("Action");
    }

    private void StubFillRowsDemo()
    {
      // Duplicates 1
      HierarchyItem l_row1 = m_DGV.RowsHierarchy.Items.Add("");
      FillRow(l_row1, "BOGIRAUD Aurélie", "03/05/2015","", "", "", "");
     
      HierarchyItem l_row1D1 = l_row1.Items.Add("");
      HierarchyItem l_row1D2 = l_row1.Items.Add("");
      FillRow(l_row1D1, "", "", "G03PVI", "IVECO Bus","user 1", "?");
      FillRow(l_row1D2, "", "", "G03STR", "IVECO Bus","user 1", "Lyon");
  

      // Duplicates 2
      HierarchyItem l_row2 = m_DGV.RowsHierarchy.Items.Add("");
      FillRow(l_row2,"CAUVIN Solenne","Week 12","","","","");

      HierarchyItem l_row2D1 = l_row2.Items.Add("");
      HierarchyItem l_row2D2 = l_row2.Items.Add("");
      FillRow(l_row2D1, "", "", "S09CLA","CLIENT 1", "User 12", "Trappes");
      FillRow(l_row2D2, "", "", "S09ISC","CLIENT 1", "User 5", "Trappes");


      // Duplicates 3
      HierarchyItem l_row3 = m_DGV.RowsHierarchy.Items.Add("");
      FillRow(l_row3, "CONQ Jean-Bernard", "26/10/2015", "", "", "", ""); 

      HierarchyItem l_row3D1 = l_row3.Items.Add("");
      HierarchyItem l_row3D2 = l_row3.Items.Add("");
      HierarchyItem l_row3D3 = l_row3.Items.Add("");
      FillRow(l_row3D1, "", "", "A11MTP", "CLIENT 4", "User 78", "Trappes");
      FillRow(l_row3D2, "", "", "A11PNC", "E", "User 100", "Trappes");
      FillRow(l_row3D3, "", "", "S09ISC", "S", "User 103", "Trappes");


      // Duplicates 4
      HierarchyItem l_row4 = m_DGV.RowsHierarchy.Items.Add("");
      FillRow(l_row4, "HAUVILLE Serge", "15/05/2015", "", "", "", ""); 

      HierarchyItem l_row4D1 = l_row4.Items.Add("");
      HierarchyItem l_row4D2 = l_row4.Items.Add("");
      FillRow(l_row4D1, "", "", "A11MTP", "CLIENT A", "User 22", "Trappes");
      FillRow(l_row4D2, "", "", "A11PNC", "CLIENT 4", "User 566", "Trappes");
    }

    private void FillRow(HierarchyItem p_row, string p_employee, string p_period, string p_entity, string p_client ,string p_user, string p_site)
    {
      m_DGV.CellsArea.SetCellValue(p_row, m_employeesColumn, p_employee);
      m_DGV.CellsArea.SetCellValue(p_row, m_periodColumn, p_period);
      m_DGV.CellsArea.SetCellValue(p_row, m_entityColumn, p_entity);
      m_DGV.CellsArea.SetCellValue(p_row, m_clientColumn, p_client);
      m_DGV.CellsArea.SetCellValue(p_row, m_userColumn, p_user);
      m_DGV.CellsArea.SetCellValue(p_row, m_siteColumn, p_site);
    }

  }
}
