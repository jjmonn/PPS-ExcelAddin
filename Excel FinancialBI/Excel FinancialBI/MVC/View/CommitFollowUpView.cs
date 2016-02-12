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
using VIBlend.Utilities;
using VIBlend.WinForms.Controls;

namespace FBI.MVC.View
{
  using Utils;
  using Forms;
  using Network;
  using MVC.Model;
  using MVC.Controller;
  using MVC.Model.CRUD;

  public partial class CommitFollowUpView : Form, IView
  {
    private CommitFollowUpController m_controller;
    private FbiDataGridView m_dataGridView;
    private List<Int32> m_dates;

    public CommitFollowUpView()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as CommitFollowUpController;
    }

    public void LoadView()
    {
      try
      {
        m_dataGridView = new FbiDataGridView();
        this.MultilangueSetup();
        this.PeriodRangeSetup();
        this.RegisterEvents();
        this.InitDataGridView();
        m_dataGridView.Dock = DockStyle.Fill;
        m_gridViewPanel.Controls.Add(m_dataGridView);
        this.Show();
      }
      catch (Exception e)
      {
        System.Diagnostics.Debug.WriteLine(e.Message);
        System.Diagnostics.Debug.WriteLine(e.StackTrace);
      }
    }

    public void RegisterEvents()
    {
      m_dateFromPicker.ValueChanged += OndateChanged;
      m_dateToPicker.ValueChanged += OndateChanged;
      m_dataGridView.CellMouseClick += OnClickCell;
      m_dataGridView.CellValueChanged += OnCellChanged;
      m_dataGridView.CellChangedAndValidated += OnCellChangedValidated;
      CommitModel.Instance.UpdateEvent += OnCommitModelUpdate;
      CommitModel.Instance.ReadEvent += OnCommitModelRead;
    }

    private void MultilangueSetup()
    {
      this.Text = Local.GetValue("submissionsFollowUp.submissions_tracking");
      m_dateFromText.Text = Local.GetValue("submissionsFollowUp.start_date");
      m_dateToText.Text = Local.GetValue("submissionsFollowUp.end_date");
    }

    private void PeriodRangeSetup()
    {
      DateTime l_dateStart = DateTime.Today.AddDays(-PeriodModel.m_nbDaysInWeek);
      DateTime l_dateEnd = DateTime.Today.AddDays(PeriodModel.m_nbDaysInWeek * 2);

      m_dateFromPicker.Text = l_dateStart.ToString();
      m_dateToPicker.Text = l_dateEnd.ToString();
      m_dates = PeriodModel.GetWeeksPeriodListFromPeriodsRange(l_dateStart, l_dateEnd);
    }

    private void InitDataGridView()
    {
      Int32 l_date;
      string l_name;

      m_dataGridView.InitializeRows(AxisElemModel.Instance, AxisElemModel.Instance.GetDictionary(AxisType.Entities));
      foreach (Int32 l_item in m_dates)
      {
        l_date = l_item;
        l_name = Local.GetValue("general.week") + " " + PeriodModel.GetWeekNumberFromDateId(ref l_date) + ", " + DateTime.FromOADate((double)l_date).Year;
        m_dataGridView.SetDimension(FbiDataGridView.Dimension.COLUMN, (UInt32)l_date, l_name);
      }
      this.SetRows();
      m_dataGridView.RowsHierarchy.ExpandAllItems();
    }

    private void SetRows()
    {
      MultiIndexDictionary<UInt32, UInt32, Commit> l_commits;

      foreach (AxisElem l_entity in AxisElemModel.Instance.GetDictionary(AxisType.Entities).Values)
      {
        l_commits = CommitModel.Instance.GetDictionary(l_entity.Id);
        foreach (UInt32 l_date in m_dates)
        {
          if (l_entity.AllowEdition && l_commits != null && l_commits.ContainsSecondaryKey(l_date))
          {
            m_dataGridView.FillField<string, IEditor>(l_entity.Id, l_date, this.GetStatusString((Commit.Status)(l_commits.SecondaryKeyItem(l_date).Value)), null);
          }
          else
          {
            m_dataGridView.FillField<string, IEditor>(l_entity.Id, l_date, this.GetStatusString(Commit.Status.NOT_EDITED), null);
          }
        }
      }
    }

    #region Utils

    private void SetParentStatus(Commit.Status p_status, UInt32 p_entity, UInt32 p_date)
    {
      Commit l_commit;
      AxisElem l_entity;
      List<AxisElem> l_list;
      Commit.Status l_status;
      bool l_hasEdition = false;
      byte l_state = (byte)Commit.Status.COMMITTED;

      if ((l_entity = AxisElemModel.Instance.GetValue(p_entity)) == null)
        return;
      if (l_entity.ParentId == 0)
        return;
      l_list = AxisElemModel.Instance.GetChildren(AxisType.Entities, l_entity.ParentId);
      foreach (AxisElem l_item in l_list)
      {
        if (AxisElemModel.Instance.IsParent(l_item.Id))
        {
          string val = (string)m_dataGridView.GetCellValue(l_item.Id, p_date);
          l_status =  this.GetStatusFromString(val);
        }
        else
        {
          l_status = (((l_commit = CommitModel.Instance.GetValue(l_item.Id, p_date)) != null) ? (Commit.Status)l_commit.Value : Commit.Status.NOT_EDITED);
        }
        if (l_status == Commit.Status.COMMITTED || l_status == Commit.Status.EDITED)
          l_hasEdition = true;
        if ((byte)l_status < l_state)
          l_state = (byte)l_status;
      }
      l_state = (l_hasEdition && l_state != (byte)Commit.Status.COMMITTED ? (byte)Commit.Status.EDITED : l_state);
      m_dataGridView.FillField(l_entity.ParentId, p_date, this.GetStatusString((Commit.Status)l_state));
    }

    private ComboBoxEditor CreateComboBoxEditor()
    {
      ComboBoxEditor l_cbEditor = new ComboBoxEditor();
      ListItem l_itemCommitted = new ListItem();
      ListItem l_itemEdited = new ListItem();
      ListItem l_itemNotCommitted = new ListItem();

      l_itemCommitted.Value = Commit.Status.COMMITTED;
      l_itemCommitted.Text = Local.GetValue("submissionsFollowUp.green_status");
      l_itemEdited.Value = Commit.Status.EDITED;
      l_itemEdited.Text = Local.GetValue("submissionsFollowUp.orange_status");
      l_itemNotCommitted.Value = Commit.Status.NOT_EDITED;
      l_itemNotCommitted.Text = Local.GetValue("submissionsFollowUp.red_status");

      l_cbEditor.Items.Add(l_itemCommitted);
      l_cbEditor.Items.Add(l_itemEdited);
      l_cbEditor.Items.Add(l_itemNotCommitted);
      l_cbEditor.DropDownList = true;
      return (l_cbEditor);
    }

    private ListItem GetStatus(Commit p_commit)
    {
      ListItem l_item = new ListItem();
      Commit.Status l_status = (p_commit != null ?
        (Commit.Status)p_commit.Value :
        Commit.Status.NOT_EDITED
      );

      l_item.Text = this.GetStatusString(l_status);
      l_item.Value = l_status;
      return (l_item);
    }

    private string GetStatusString(Commit.Status p_status)
    {
      switch (p_status)
      {
        case Commit.Status.COMMITTED:
          return (Local.GetValue("submissionsFollowUp.green_status"));
        case Commit.Status.EDITED:
          return (Local.GetValue("submissionsFollowUp.orange_status"));
      }
      return (Local.GetValue("submissionsFollowUp.red_status"));
    }

    private Commit.Status GetStatusFromString(string p_commitName)
    {
      if (p_commitName != null)
      {
        if (p_commitName == Local.GetValue("submissionsFollowUp.green_status"))
          return (Commit.Status.COMMITTED);
        if (p_commitName == Local.GetValue("submissionsFollowUp.orange_status"))
          return (Commit.Status.EDITED);
      }
      return (Commit.Status.NOT_EDITED);
    }

    private Color GetStatusColor(string p_commitName)
    {
      if (p_commitName != null)
      {
        if (p_commitName == Local.GetValue("submissionsFollowUp.green_status"))
          return (Color.Green);
        if (p_commitName == Local.GetValue("submissionsFollowUp.orange_status"))
          return (Color.Orange);
      }
      return (Color.DarkRed);
    }

    private Color GetStatusColor(Commit.Status p_status)
    {
      switch (p_status)
      {
        case Commit.Status.COMMITTED:
          return (Color.Green);
        case Commit.Status.EDITED:
          return (Color.Orange);
      }
      return (Color.DarkRed);
    }

    private void SetCell(GridCell p_cell)
    {
      FillStyle l_style;
      GridCellStyle l_gridStyle;

      if (p_cell == null)
        return;
      l_style = new FillStyleSolid(this.GetStatusColor((string)p_cell.Value));
      l_gridStyle = GridTheme.GetDefaultTheme(m_dataGridView.VIBlendTheme).GridCellStyle;
      l_gridStyle.FillStyle = l_style;
      p_cell.DrawStyle = l_gridStyle;
    }

    #endregion

    //Create comboBox, cannot modify parents
    private void OnClickCell(object p_sender, CellMouseEventArgs p_e)
    {
      if (p_e.Cell != null && !AxisElemModel.Instance.IsParent((UInt32)p_e.Cell.RowItem.ItemValue)) //Add comboBox if NOT a parent
      {
        ComboBoxEditor l_cb = this.CreateComboBoxEditor();
        m_dataGridView.CellsArea.SetCellEditor(p_e.Cell.RowItem, p_e.Cell.ColumnItem, l_cb);
      }
    }

    private void OnCellChangedValidated(object p_sender, CellEventArgs p_e)
    {
      ComboBoxEditor l_editor;
      UInt32 l_entityId, l_date;

      if (p_e.Cell != null && p_e.Cell.Editor != null)
      {
        l_entityId = (UInt32)p_e.Cell.RowItem.ItemValue;
        l_date = (UInt32)p_e.Cell.ColumnItem.ItemValue;
        l_editor = (ComboBoxEditor)p_e.Cell.Editor;
        if (l_editor.SelectedItem != null)
        {
          m_controller.Update((Commit.Status)l_editor.SelectedItem.Value, l_date, l_entityId);
        }
      }
    }

    //Set correct color, and handle parents
    private void OnCellChanged(object p_sender, CellEventArgs p_e)
    {
      UInt32 l_entityId, l_date;

      if (p_e.Cell != null)
      {
        l_entityId = (UInt32)p_e.Cell.RowItem.ItemValue;
        l_date = (UInt32)p_e.Cell.ColumnItem.ItemValue;
        this.SetParentStatus(this.GetStatusFromString((string)p_e.Cell.Value), l_entityId, l_date);
        this.SetCell(p_e.Cell);
      }
    }

    private void OndateChanged(object p_sender, EventArgs p_e)
    {
      m_dates = PeriodModel.GetWeeksPeriodListFromPeriodsRange(m_dateFromPicker.Value, m_dateToPicker.Value);
      m_dataGridView.ClearColumns();
      m_dataGridView.ClearRows();
      this.InitDataGridView();
    }

    private void OnCommitModelUpdate(ErrorMessage p_status, uint p_id)
    {
      if (p_status != ErrorMessage.SUCCESS)
      {
        MessageBox.Show("{UPDATE}");
      }
    }

    delegate void OnModelRead_delegate(ErrorMessage p_status, Commit p_attributes);
    void OnCommitModelRead(Network.ErrorMessage p_status, Commit p_attributes)
    {
      if (InvokeRequired)
      {
        OnModelRead_delegate func = new OnModelRead_delegate(OnCommitModelRead);
        Invoke(func, p_status, p_attributes);
      }
      else
      {
        m_dataGridView.FillField<string, IEditor>(p_attributes.EntityId, p_attributes.Period, this.GetStatusString((Commit.Status)p_attributes.Value), null);
      }
    }
  }
}
