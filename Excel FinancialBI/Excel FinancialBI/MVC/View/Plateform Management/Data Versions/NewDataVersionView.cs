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
  using Forms;
  using FBI.MVC.Model;
  using Model.CRUD;
  using VIBlend.WinForms.Controls;
  using System.Windows.Forms;

  public partial class NewDataVersionUI : Form, IView
  {
    VersionsController m_controller;
    public UInt32 m_parentId { set; private get; }
    public NewDataVersionUI()
    {
      InitializeComponent();
      this.LoadView();
    }

    public void SetController(IController p_controller)
    {
      this.m_controller = p_controller as VersionsController;
    }

    private void LoadView()
    {
      // Rates and global facts versions treeviewBoxes
      FbiTreeView<ExchangeRateVersion>.Load(m_exchangeRatesVersionVTreeviewbox.TreeView.Nodes, RatesVersionModel.Instance.GetDictionary());
      FbiTreeView<GlobalFactVersion>.Load(m_factsVersionVTreeviewbox.TreeView.Nodes, GlobalFactVersionModel.Instance.GetDictionary());

      // Time config combobox
      ListItem l_yearlyConfigItem = m_timeConfigCB.Items.Add(Local.GetValue("period.timeconfig.year"));
      l_yearlyConfigItem.Value = TimeConfig.YEARS;
      m_timeConfigCB.SelectedItem = l_yearlyConfigItem;
      ListItem l_monthlyConfigItem = m_timeConfigCB.Items.Add(Local.GetValue("period.timeconfig.month"));
      l_monthlyConfigItem.Value = TimeConfig.MONTHS;
      ListItem l_dailyConfigItem = m_timeConfigCB.Items.Add(Local.GetValue("period.timeconfig.day"));
      l_dailyConfigItem.Value = TimeConfig.DAYS;

      this.m_startingPeriodDatePicker.Value = DateTime.Now;

      MultilangueSetup();
      SuscribeEvents();
    }

    void SuscribeEvents()
    {
      Addin.SuscribeAutoLock(this);
      this.m_CancelButton.Click += new EventHandler(this.CancelBT_Click);
      this.m_createVersionButton.Click += new EventHandler(this.CreateVersionBT_Click);
      this.m_timeConfigCB.SelectedItemChanged += new EventHandler(this.m_timeConfigCB_SelectedItemChanged);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewDataVersionUI_FormClosing);
    }

    private void MultilangueSetup()
    {
      m_versionNameLabel.Text = Local.GetValue("facts_versions.version_name");
      m_periodConfigLabel.Text = Local.GetValue("facts_versions.period_config");
      m_startingPeriodLabel.Text = Local.GetValue("facts_versions.starting_period");
      m_nbPeriodsLabel.Text = Local.GetValue("facts_versions.nb_periods");
      m_rateVersionLabel.Text = Local.GetValue("facts_versions.exchange_rates_version");
      m_factVersionLabel.Text = Local.GetValue("facts_versions.global_facts_version");
      m_CancelButton.Text = Local.GetValue("general.cancel");
      m_createVersionButton.Text = Local.GetValue("general.create");
      this.Text = Local.GetValue("facts_versions.version_new");
    }

    #region Events and call backs

    private void m_timeConfigCB_SelectedItemChanged(object sender, EventArgs e)
    {
      TimeConfig l_timeConfig = (TimeConfig)m_timeConfigCB.SelectedItem.Value;
      switch (l_timeConfig)
      {
        case TimeConfig.MONTHS:
          m_startingPeriodDatePicker.CustomFormat = "MMMM yyyy";
          break;

        case TimeConfig.YEARS:
          m_startingPeriodDatePicker.CustomFormat = "dd MMMM yyyy";
        break;
      }
    }

    private void CreateVersionBT_Click(object sender, EventArgs e)
    {
      if (m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode == null)
      {
        Forms.MsgBox.Show(Local.GetValue("versions.error.rates_version_not_selected"));
        return;
      }
      if (m_factsVersionVTreeviewbox.TreeView.SelectedNode == null)
      {
        Forms.MsgBox.Show(Local.GetValue("versions.error.gfacts_version_not_selected"));
        return;
      }
      Version l_version = new Version();
      l_version.Name = this.m_versionNameTextbox.Text;
      l_version.ParentId = m_parentId;
      l_version.StartPeriod = (UInt32)m_startingPeriodDatePicker.Value.ToOADate();
      l_version.NbPeriod = (ushort)m_nbPeriods.Value;
      l_version.IsFolder = false;
      l_version.Locked = false;
      l_version.ItemPosition = 0;
      l_version.TimeConfiguration = (TimeConfig)this.m_timeConfigCB.SelectedItem.Value;
      l_version.RateVersionId = (UInt32)m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Value;
      l_version.GlobalFactVersionId = (UInt32)m_factsVersionVTreeviewbox.TreeView.SelectedNode.Value;
      l_version.CreatedAt = DateTime.Now.ToShortDateString();
      l_version.FormulaPeriodIndex = 1;
      l_version.FormulaNbPeriod = l_version.NbPeriod - (uint)1;

      if (m_controller.Create(l_version) == false)
      {
        Forms.MsgBox.Show(m_controller.Error);
      }
      else
        this.Hide();
    }

    private void CancelBT_Click(object sender, EventArgs e)
    {
      this.Hide();
    }

    private void NewDataVersionUI_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;
      this.Hide();
    }

    #endregion


  }
}
