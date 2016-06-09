using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIBlend.WinForms.DataGridView;
using VIBlend.WinForms.Controls;

using DGVDimension = FBI.Forms.BaseFbiDataGridView<uint>.Dimension;

namespace FBI.MVC.View
{
  using Controller;
  using Utils;
  using Model.CRUD;
  using Model;

  public partial class AccountSnapshotSelectionView : Form, IView
  {
    AccountCreateSnapshotController m_controller;
    List<object> m_values;
    CheckBoxEditor m_editor = new CheckBoxEditor();

    public AccountSnapshotSelectionView(List<object> p_values)
    {
      InitializeComponent();
      m_values = p_values;
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as AccountCreateSnapshotController;
    }

    void LoadLocals()
    {
      m_validateBT.Text = Local.GetValue("general.export_selected_account");
    }

    public void LoadView()
    {
      LoadLocals();

      m_dgv.RowsHierarchy.Visible = false;
      m_dgv.ColumnsHierarchy.Visible = false;
      m_dgv.Dock = DockStyle.Fill;

      m_dgv.SetDimension(DGVDimension.COLUMN, 0, "");
      m_dgv.SetDimension(DGVDimension.COLUMN, 1, "");

      uint l_index = 0;
      foreach (object l_value in m_values)
      {
        if (AccountModel.Instance.GetValue((string)l_value) != null)
          continue;
        m_dgv.SetDimension(DGVDimension.ROW, l_index, "");
        m_dgv.FillField(l_index, 0, (string)l_value);
        m_dgv.FillField(l_index, 1, false, m_editor);
        l_index++;
      }

      m_dgv.RowsHierarchy.AutoResize(AutoResizeMode.FIT_ALL);
      m_dgv.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL);
      this.Size = new Size(m_dgv.ColumnsHierarchy.Width + ((this.Height < m_dgv.RowsHierarchy.Height) ? 28 : 0), this.Height);
    }

    private void OnValidate(object sender, EventArgs e)
    {
      List<Account> l_accountList = new List<Account>();
      foreach (uint l_row in m_dgv.Rows.Keys)
      {
        if ((bool)m_dgv.GetCellValue(l_row, 1))
        {
          Account l_account = new Account();

          l_account.Name = (string)m_dgv.GetCellValue(l_row, 0);
          l_accountList.Add(l_account);
        }
      }
      m_controller.SelectAccounts(l_accountList);
    }

  }
}