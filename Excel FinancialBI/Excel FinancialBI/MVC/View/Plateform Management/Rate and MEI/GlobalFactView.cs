using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIBlend.WinForms.Controls;
using VIBlend.WinForms.DataGridView;
using System.Globalization;
using Microsoft.VisualBasic;

namespace FBI.MVC.View
{
  using Model;
  using Model.CRUD;
  using Forms;
  using Utils;
  using Controller;
  using Network;

  public partial class GlobalFactView : FactBaseView<GlobalFactVersion, GlobalFactController>
  {
    UInt32 m_selectedFact = 0;

    public GlobalFactView()
      : base(GlobalFactVersionModel.Instance)
    {
      m_excelImportController = new ExcelGlobalFactController();
    }

    override public void SetController(IController p_controller)
    {
      m_controller = p_controller as GlobalFactController;
    }

    public override void LoadView()
    {
      base.LoadView();
      m_dgv.ContextMenuStrip = this.m_dgvMenu;
      SuscribeEvents();
    }

    protected override void SuscribeEvents()
    {
      base.SuscribeEvents();
      m_dgv.CellChangedAndValidated += OnCellChanged;
      m_dgv.MouseDown += OnGFactRightClick;
      m_copyValueDown.Click += OnCopyValueDown;
      m_renameGFact.Click += OnRenameFactClick;
      m_newGFact.Click += OnNewFactClick;
      m_deleteGFact.Click += OnDeleteFactClick;

      GlobalFactDataModel.Instance.ReadEvent += OnModelReadGFactData;
      GlobalFactDataModel.Instance.UpdateEvent += OnModelUpdateGFactData;
      GlobalFactDataModel.Instance.CreationEvent += OnModelUpdateGFactData;
      GlobalFactDataModel.Instance.DeleteEvent += OnModelDeleteGFactData;
      GlobalFactModel.Instance.ReadEvent += OnModelReadGFact;
      GlobalFactModel.Instance.DeleteEvent += OnModelDeleteGFact;
    }

    #region Initialize

    void InitPeriods(List<Int32> p_monthList)
    {
      m_dgv.ClearRows();
      foreach (Int32 l_monthId in p_monthList)
        m_dgv.SetDimension(FbiDataGridView.Dimension.ROW, (UInt32)l_monthId, DateTime.FromOADate(l_monthId).ToString("Y", CultureInfo.CreateSpecificCulture("en-US")));
    }

    void InitGlobalFacts(List<GlobalFact> p_globalFactList)
    {
      m_dgv.ClearColumns();
      foreach (GlobalFact l_gfact in p_globalFactList)
        m_dgv.SetDimension(FbiDataGridView.Dimension.COLUMN, l_gfact.Id, l_gfact.Name);
    }

    void InitGlobalFactData(UInt32 p_versionId, List<Int32> p_monthList, List<GlobalFact> p_gfactList)
    {
      TextBoxEditor l_tbEditor = null;

      foreach (Int32 l_monthId in p_monthList)
        foreach (GlobalFact l_fact in p_gfactList)
        {
          GlobalFactData l_data = GlobalFactDataModel.Instance.GetValue(l_fact.Id, (UInt32)l_monthId, p_versionId);

          if (UserModel.Instance.CurrentUserHasRight(Group.Permission.EDIT_GFACTS) == true)
            l_tbEditor = new TextBoxEditor();
          m_dgv.FillField((UInt32)l_monthId, l_fact.Id, ((l_data != null) ? l_data.Value : 0), l_tbEditor);
        }
    }

    protected override void DisplayVersion(UInt32 p_versionId)
    {
      List<Int32> l_monthList = GlobalFactVersionModel.Instance.GetMonthsList(p_versionId);
      List<GlobalFact> l_gfactList = GlobalFactModel.Instance.GetDictionary().SortedValues;

      if (l_monthList == null)
      {
        MessageBox.Show(Local.GetValue("exchange_rate_version.error.not_found"));
        return;
      }
      InitPeriods(l_monthList);
      InitGlobalFacts(l_gfactList);
      InitGlobalFactData(m_controller.SelectedVersion, l_monthList, l_gfactList);
      m_dgv.Refresh();
    }

    #endregion

    #region User Callback

    void OnGFactRightClick(object p_sender, MouseEventArgs p_e)
    {
      if ((p_e.Button != MouseButtons.Right))
        return;
      HierarchyItem l_target = m_dgv.ColumnsHierarchy.HitTest(p_e.Location);
      if (l_target == null)
      {
        l_target = m_dgv.RowsHierarchy.HitTest(p_e.Location);
        if (l_target == null)
          return;
      }
      m_gfactMenu.Visible = true;
      m_gfactMenu.Bounds = new Rectangle(MousePosition, new Size(m_gfactMenu.Width, m_gfactMenu.Height));
      m_selectedFact = (UInt32)l_target.ItemValue;
    }

    void OnCellChanged(object p_sender, CellEventArgs p_args)
    {
      UInt32 l_period = (UInt32)p_args.Cell.RowItem.ItemValue;
      UInt32 l_currencyId = (UInt32)p_args.Cell.ColumnItem.ItemValue;
      UInt32 l_versionId = m_controller.SelectedVersion;

      SetGFactData(l_currencyId, l_versionId, l_period, double.Parse((string)p_args.Cell.Value));
    }

    bool SetGFactData(UInt32 p_gfactId, UInt32 p_versionId, UInt32 p_period, double p_value)
    {
      GlobalFactData l_gfactData = GlobalFactDataModel.Instance.GetValue(p_gfactId, p_period, p_versionId);

      m_dgv.FillField(p_period, p_gfactId, (l_gfactData == null) ? 0 : l_gfactData.Value);
      if (l_gfactData == null)
      {
        l_gfactData = new GlobalFactData();
        l_gfactData.Period = p_period;
        l_gfactData.GlobalFactId = p_gfactId;
        l_gfactData.VersionId = p_versionId;
        l_gfactData.Value = p_value;
        return (m_controller.CreateGFactData(l_gfactData));
      }
      else
      {
        l_gfactData = l_gfactData.Clone();
        l_gfactData.Value = p_value;
        return (m_controller.UpdateGFactData(l_gfactData));
      }
    }

    void OnCopyValueDown(object p_sender, EventArgs p_e)
    {
      GridCell l_cell = m_dgv.HoveredCell;
      if (l_cell == null)
        return;
      UInt32 l_gfactId = (UInt32)m_dgv.HoveredColumn.ItemValue;
      UInt32 l_versionId = m_controller.SelectedVersion;
      double l_value = Convert.ToDouble(l_cell.Value);

      for (int i = m_dgv.HoveredRow.ItemIndex; i < m_dgv.RowsHierarchy.Items.Count; ++i)
      {
        UInt32 l_period = (UInt32)m_dgv.RowsHierarchy.Items[i].ItemValue;

        SetGFactData(l_gfactId, l_versionId, l_period, l_value);
      }
    }

    void OnNewFactClick(object p_sender, EventArgs p_e)
    {
      string l_result = Interaction.InputBox(Local.GetValue("gfact.new"));
      GlobalFact l_gfact = new GlobalFact();

      if (l_result == "")
        return;
      l_gfact.Name = l_result;
      if (m_controller.CreateGFact(l_gfact) == false)
        MessageBox.Show(m_controller.Error);
    }

    void OnDeleteFactClick(object p_sender, EventArgs p_e)
    {
      if (m_controller.DeleteGFact(m_selectedFact) == false)
        MessageBox.Show(m_controller.Error);
    }

    void OnRenameFactClick(object p_sender, EventArgs p_e)
    {
      GlobalFact l_gfact = GlobalFactModel.Instance.GetValue(m_selectedFact);

      if (l_gfact == null)
        return;
      string l_result = Interaction.InputBox(Local.GetValue("general.rename"));

      if (l_result == "")
        return;
      l_gfact.Name = l_result;
      if (m_controller.UpdateGFact(l_gfact) == false)
        MessageBox.Show(m_controller.Error);
    }

    #endregion

    #region Model Callback

    delegate void OnModelReadGFactData_delegate(ErrorMessage p_status, GlobalFactData p_gfactData);
    void OnModelReadGFactData(ErrorMessage p_status, GlobalFactData p_gfactData)
    {
      if (m_dgv.InvokeRequired)
      {
        OnModelReadGFactData_delegate func = new OnModelReadGFactData_delegate(OnModelReadGFactData);
        Invoke(func, p_status, p_gfactData);
      }
      else
      {
        TextBoxEditor l_tbEditor = null;
        if (UserModel.Instance.CurrentUserHasRight(Group.Permission.EDIT_GFACTS) == true)
          l_tbEditor = new TextBoxEditor();
        if (p_gfactData.VersionId == m_controller.SelectedVersion)
          m_dgv.FillField(p_gfactData.Period, p_gfactData.GlobalFactId, p_gfactData.Value, l_tbEditor);
      }
    }

    void OnModelUpdateGFactData(ErrorMessage p_status, UInt32 p_id)
    {
      if (p_status != ErrorMessage.SUCCESS)
        MessageBox.Show(Error.GetMessage(p_status));
    }

    void OnModelDeleteGFactData(ErrorMessage p_status, UInt32 p_id)
    {
      if (p_status != ErrorMessage.SUCCESS)
        MessageBox.Show(Error.GetMessage(p_status));
    }

    delegate void OnModelReadGFact_delegate(ErrorMessage p_status, GlobalFact p_gfact);
    void OnModelReadGFact(ErrorMessage p_status, GlobalFact p_gfact)
    {
      if (m_dgv.InvokeRequired)
      {
        OnModelReadGFact_delegate func = new OnModelReadGFact_delegate(OnModelReadGFact);
        Invoke(func, p_status, p_gfact);
      }
      else
        DisplayVersion(m_controller.SelectedVersion);
    }

    delegate void OnModelDeleteGFact_delegate(ErrorMessage p_status, UInt32 p_gfactId);
    void OnModelDeleteGFact(ErrorMessage p_status, UInt32 p_gfactId)
    {
      if (m_dgv.InvokeRequired)
      {
        OnModelDeleteGFact_delegate func = new OnModelDeleteGFact_delegate(OnModelDeleteGFact);
        Invoke(func, p_status, p_gfactId);
      }
      else
      {
        if (p_status == ErrorMessage.SUCCESS)
        {
          m_dgv.DeleteColumn(p_gfactId);
          m_dgv.Refresh();
        }
        else
          MessageBox.Show(Error.GetMessage(p_status));
      }
    }

    void OnModelCreateGFact(ErrorMessage p_status, UInt32 p_gfactId)
    {
      if (p_status != ErrorMessage.SUCCESS)
        MessageBox.Show(Error.GetMessage(p_status));
    }

    void OnModelUpdateGFact(ErrorMessage p_status, UInt32 p_gfactId)
    {
      if (p_status != ErrorMessage.SUCCESS)
        MessageBox.Show(Error.GetMessage(p_status));
    }
    
    #endregion

  }
}
