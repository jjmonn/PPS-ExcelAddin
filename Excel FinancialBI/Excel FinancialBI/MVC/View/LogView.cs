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
  using Utils;
  using Model;
  using Model.CRUD;
  using Forms;

  public partial class LogView : Form, IView
  {
    enum Column
    {
      USERNAME,
      DATE,
      CLIENT,
      PRODUCT,
      ADJUSTMENT,
      VALUE
    };

    LogController m_controller;

    public LogView()
    {
      InitializeComponent();
      m_logDataGridView.Dock = DockStyle.None;
      MultilangueSetup();
      SuscribeEvents();
    }

    private void MultilangueSetup()
    {
      this.Text = Local.GetValue("general.log");
      m_entityLabel.Text = Local.GetValue("general.entity");
      m_accountLabel.Text = Local.GetValue("general.account");
      m_versionLabel.Text = Local.GetValue("general.version");
      m_periodLabel.Text = Local.GetValue("general.period");
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as LogController;
    }

    void SuscribeEvents()
    {
      FactLogModel.Instance.ReadEvent += OnModelRead;
    }

    public void LoadView()
    {
      m_logDataGridView.SetDimension(FbiDataGridView.Dimension.COLUMN, (UInt32)Column.USERNAME, Local.GetValue("log.username"));
      m_logDataGridView.SetDimension(FbiDataGridView.Dimension.COLUMN, (UInt32)Column.DATE, Local.GetValue("log.date"));
      m_logDataGridView.SetDimension(FbiDataGridView.Dimension.COLUMN, (UInt32)Column.CLIENT, Local.GetValue("log.client"));
      m_logDataGridView.SetDimension(FbiDataGridView.Dimension.COLUMN, (UInt32)Column.PRODUCT, Local.GetValue("log.product"));
      m_logDataGridView.SetDimension(FbiDataGridView.Dimension.COLUMN, (UInt32)Column.ADJUSTMENT, Local.GetValue("log.adjustment"));
      m_logDataGridView.SetDimension(FbiDataGridView.Dimension.COLUMN, (UInt32)Column.VALUE, Local.GetValue("log.value"));
      m_logDataGridView.RowsHierarchy.Visible = false;
    }

    delegate void OnModelRead_delegate(bool p_status, List<FactLog> p_factLogList);
    void OnModelRead(bool p_status, List<FactLog> p_factLogList)
    {
      if (m_logDataGridView.InvokeRequired)
      {
        OnModelRead_delegate func = new OnModelRead_delegate(OnModelRead);
        Invoke(func, p_status, p_factLogList);
      }
      else
      {
        foreach (FactLog l_log in p_factLogList)
        {
          DateTime l_date = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);

          l_date.AddSeconds(l_log.Date);
          m_logDataGridView.SetDimension<Currency>(FbiDataGridView.Dimension.ROW, l_log.Id, "", 0, null, 0);
          m_logDataGridView.FillField(l_log.Id, (UInt32)Column.USERNAME, l_log.User);
          m_logDataGridView.FillField(l_log.Id, (UInt32)Column.DATE, l_date.ToShortDateString());
          m_logDataGridView.FillField(l_log.Id, (UInt32)Column.CLIENT, l_log.ClientId);
          m_logDataGridView.FillField(l_log.Id, (UInt32)Column.PRODUCT, l_log.ProductId);
          m_logDataGridView.FillField(l_log.Id, (UInt32)Column.ADJUSTMENT, l_log.AdjustmentId);
          m_logDataGridView.FillField(l_log.Id, (UInt32)Column.VALUE, l_log.Value);
        }
      }
    }

    public void ShowView(UInt32 p_entityId, UInt32 p_versionId, UInt32 p_accountId, UInt32 p_period)
    {
      AxisElem l_entity = AxisElemModel.Instance.GetValue(p_entityId);
      Version l_version = VersionModel.Instance.GetValue(p_versionId);
      Account l_account = AccountModel.Instance.GetValue(p_accountId);
      string l_period = DateTime.FromOADate(p_period).ToShortDateString();

      if (l_entity == null || l_version == null || l_account == null)
      {
        MessageBox.Show(Local.GetValue("general.error.system"));
        return;
      }
      m_entityTB.Text = l_entity.Name;
      m_versionTB.Text = l_version.Name;
      m_accountTB.Text = l_account.Name;
      m_periodTB.Text = l_period;
      m_logDataGridView.CellsArea.Clear();
      FactLogModel.Instance.GetFactLog(p_accountId, p_entityId, p_period, p_versionId);
      Show();
    }
  }
}
