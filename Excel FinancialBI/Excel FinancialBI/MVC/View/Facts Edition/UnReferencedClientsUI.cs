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
  using Controller;
  using FBI;
  using Utils;
  using Forms;
  using FBI.MVC.Model.CRUD;
  using VIBlend.WinForms.DataGridView;
  using VIBlend.WinForms.Controls;
  using DGV = FBI.Forms.BaseFbiDataGridView<Model.CRUD.AxisElem>;
  
  public partial class UnReferencedClientsUI : Form, IView
  {
    UnreferencedClientsController m_controller;
    DGV m_DGV = new DGV();
    HierarchyItem m_similarClientsColumn;
    HierarchyItem m_replaceOptionColumn;
    HierarchyItem m_createColumn;
    bool m_isUpdating;

    public UnReferencedClientsUI(List<string> p_unreferencedClients)
    {
      InitializeComponent();
      MultilangueSetup();
      SubscribeEvents();
      LoadView(p_unreferencedClients);
    }

    private void MultilangueSetup()
    {
      this.Text = Local.GetValue("clientAutoCreation.unerefenced_clients");
      m_createAllButton.Text = Local.GetValue("general.validate");
    }

    private void SubscribeEvents()
    {
      this.m_createAllButton.Click += new System.EventHandler(this.m_createAllButton_Click);
      this.UnselectBothOptionsToolStripMenuItem.Click += new System.EventHandler(this.UnselectBothOptionsToolStripMenuItem_Click);
      this.SelectAllOnColumnToolStripMenuItem.Click += new System.EventHandler(this.SelectAllOnColumnToolStripMenuItem_Click);
      this.UnselectAllOnColumnToolStripMenuItem.Click += new System.EventHandler(this.UnselectAllOnColumnToolStripMenuItem_Click);
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as UnreferencedClientsController;
    }

    private void LoadView(List<string> p_unreferencedClients)
    {
      InitializeClientsDataGridColumns();
      InitializeClientsDataGridRows(p_unreferencedClients);
      m_clientsDGVPanel.Controls.Add(m_DGV);
      // m_DGV.ContextMenuStrip = m_DGVContextMenuStrip;
    }

    #region Initialize DGV

    private void InitializeClientsDataGridColumns()
    {
      // m_similarClientsColumn = m_DGV.ColumnsHierarchy.Items.Add(Local.GetValue("clientAutoCreation.similar_clients_column_name"));
      // m_replaceOptionColumn = m_DGV.ColumnsHierarchy.Items.Add(Local.GetValue("clientAutoCreation.replace_column_name"));
      m_createColumn = m_DGV.ColumnsHierarchy.Items.Add(Local.GetValue("clientAutoCreation.create_column_name"));
    }

    private void InitializeClientsDataGridRows(List<string> p_unreferencedClients)
    {
      m_isUpdating = true;
      foreach (string l_clientName in p_unreferencedClients)
      {
        HierarchyItem l_row = m_DGV.RowsHierarchy.Items.Add(l_clientName);
        //CheckBoxEditor l_replaceOptionCheckBox = new CheckBoxEditor();
        //m_DGV.CellsArea.SetCellEditor(l_row, m_replaceOptionColumn, l_replaceOptionCheckBox);
        //m_replaceCheckEditorsItemsDict.Add(l_replaceOptionCheckBox, l_row);
        //l_replaceOptionCheckBox.CheckedChanged += ReplaceCheckBoxChanged;

        CheckBoxEditor l_createOptionCheckBox = new CheckBoxEditor();
        m_DGV.CellsArea.SetCellEditor(l_row, m_createColumn, l_createOptionCheckBox);
        //m_createCheckEditorsItemsDict.Add(l_createOptionCheckBox, l_row);
        //l_createOptionCheckBox.CheckedChanged += CreateCheckBoxChanged;
      }
      m_isUpdating = false;
    }

    #endregion

    #region Call  backs

    private void m_createAllButton_Click(object sender, EventArgs e)
    {
      m_controller.CreateClients(GetCreateNameList());
    }

    // Not in use (Future Feature)
    private void UnselectBothOptionsToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }
    // Not in use (Future Feature)
    private void SelectAllOnColumnToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }
    // Not in use (Future Feature)
    private void UnselectAllOnColumnToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }


    #endregion
    
    #region Utils

    private List<string> GetCreateNameList()
    {
      List<string>l_list = new List<string>();
      foreach (HierarchyItem l_row in m_DGV.RowsHierarchy.Items)
      {
        try
        {
          CheckBoxEditor l_createOptionCheckBox = m_DGV.CellsArea.GetCellEditor(l_row, m_createColumn) as CheckBoxEditor;
          if (l_createOptionCheckBox != null)
            l_list.Add(l_row.Caption);
        }
        catch (Exception e)
        {
          System.Diagnostics.Debug.WriteLine("Undefined clients UI: could not get check box " + e.Message);
        }
        }
      return l_list;
    }

    #endregion

  }
}
