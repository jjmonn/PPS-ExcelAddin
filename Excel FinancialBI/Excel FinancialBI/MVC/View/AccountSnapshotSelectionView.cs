﻿using System;
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
  using Forms;

  public partial class AccountSnapshotSelectionView : Form, IView
  {
    AccountCreateSnapshotController m_controller;
    List<object> m_values;
    CheckBoxEditor m_editor = new CheckBoxEditor();
    FbiDataGridView m_dgv = new FbiDataGridView();

    public AccountSnapshotSelectionView(List<object> p_values)
    {
      InitializeComponent();
      m_values = p_values.Distinct().ToList();
      tableLayoutPanel1.Controls.Add(m_dgv);
      m_dgv.SelectionMode = vDataGridView.SELECTION_MODE.FULL_ROW_SELECT;
      m_dgv.ContextMenuStrip = m_RCMenu;
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as AccountCreateSnapshotController;
    }

    void LoadLocals()
    {
      m_validateBT.Text = Local.GetValue("general.export_selected_account");
      this.Text = Local.GetValue("general.account_snapshot_selection");
      m_selectAllBT.Text = Local.GetValue("CUI.select_all");
      m_unselectAllBT.Text = Local.GetValue("CUI.unselect_all");
    }

    void SubscribeEvents()
    {
      m_dgv.CellMouseClick += OnCellMouseClick;
      m_selectAllBT.Click += OnSelectAllClick;
      m_unselectAllBT.Click += OnUnSelectAllClick;
    }

    void OnUnSelectAllClick(object sender, EventArgs e)
    {
      foreach (KeyValuePair<uint, HierarchyItem> l_pair in m_dgv.Rows)
      {
        m_dgv.FillField(l_pair.Key, 1, false);
        m_editor.EditorValue = false;
      }
      m_dgv.Refresh();
    }

    void OnSelectAllClick(object sender, EventArgs e)
    {
      foreach (KeyValuePair<uint, HierarchyItem> l_pair in m_dgv.Rows)
      {
        m_dgv.FillField(l_pair.Key, 1, true);
        m_editor.EditorValue = true;
      }
      m_dgv.Refresh();
    }

    void OnCellMouseClick(object p_sender, CellMouseEventArgs p_args)
    {
      uint l_row = (uint)p_args.Cell.RowItem.ItemValue;
      uint l_column = 1;

      bool l_currentState = (bool)m_dgv.GetCellValue(l_row, l_column);
      m_dgv.FillField(l_row, l_column, !l_currentState);
      m_editor.EditorValue = !l_currentState;
      m_dgv.Refresh();
    }

    public void LoadView()
    {
      SubscribeEvents();
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
    }

    private void OnValidate(object sender, EventArgs e)
    {
      SafeDictionary<string, Account> l_accountList = new SafeDictionary<string, Account>();
      foreach (uint l_row in m_dgv.Rows.Keys)
      {
        if ((bool)m_dgv.GetCellValue(l_row, 1))
        {
          Account l_account = new Account();

          l_account.Name = (string)m_dgv.GetCellValue(l_row, 0);
          l_account.PeriodAggregationOptionId = Account.PeriodAggregationOptions.SUM_OF_PERIODS;
          l_accountList[l_account.Name] = l_account;
        }
      }
      m_controller.SelectAccounts(l_accountList);
    }

  }
}