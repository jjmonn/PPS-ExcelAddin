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
    public NewDataVersionUI()
    {
      InitializeComponent();
      this.InitView();
    }

    public void SetController(IController p_controller)
    {
      this.m_controller = p_controller as VersionsController;
    }

    private void InitView()
    {
      // Rates and global facts versions treeviewBoxes
      FbiTreeView<ExchangeRateVersion>.Load(m_exchangeRatesVersionVTreeviewbox.TreeView.Nodes, RatesVersionModel.Instance.GetDictionary());
      FbiTreeView<GlobalFactVersion>.Load(m_factsVersionVTreeviewbox.TreeView.Nodes, GlobalFactVersionModel.Instance.GetDictionary());

      // Time config combobox
      ListItem l_monthlyConfigItem = m_timeConfigCB.Items.Add(Local.GetValue("period.timeconfig.month"));
      l_monthlyConfigItem.Value = TimeConfig.MONTHS;
      ListItem l_yearlyConfigItem = m_timeConfigCB.Items.Add(Local.GetValue("period.timeconfig.year"));
      l_yearlyConfigItem.Value = TimeConfig.YEARS;

      this.m_startingPeriodDatePicker.Value = DateTime.Now;

      // Events handlers
      this.m_CancelButton.Click += new EventHandler(this.CancelBT_Click);
      this.m_createVersionButton.Click += new EventHandler(this.CreateVersionBT_Click);
      this.m_timeConfigCB.SelectedItemChanged += new EventHandler(this.m_timeConfigCB_SelectedItemChanged);
      this.m_startingPeriodDatePicker.Calendar.MouseClick += new MouseEventHandler(this.m_startingPeriodDatePicker_MouseClick);

      MultilangueSetup();
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
          m_startingPeriodDatePicker.DateTimeEditor.DefaultDateTimeFormat = DefaultDateTimePatterns.LongDatePattern;
          m_startingPeriodDatePicker.Calendar.SetCalendarView(vCalendarView.Month);
          break;

        case TimeConfig.YEARS:
        m_startingPeriodDatePicker.DateTimeEditor.DefaultDateTimeFormat = DefaultDateTimePatterns.ShortDatePattern;
        m_startingPeriodDatePicker.Calendar.SetCalendarView(vCalendarView.Year);
        break;
      }
    }

    private void CreateVersionBT_Click(object sender, EventArgs e)
    {
      Version l_version = new Version();
      l_version.Name = this.m_versionNameTextbox.Text;
      l_version.TimeConfiguration = (TimeConfig)this.m_timeConfigCB.SelectedItem.Value;
      l_version.StartPeriod = 0;
      l_version.NbPeriod = 

    }

    private void CancelBT_Click(object sender, EventArgs e)
    {
      this.Hide();
    }

    private void m_startingPeriodDatePicker_MouseClick(object sender, MouseEventArgs e)
    {
      TimeConfig l_timeConfig = (TimeConfig)m_timeConfigCB.SelectedItem.Value;
      switch (l_timeConfig)
      {
        case TimeConfig.MONTHS:
          // e.location -> get the item clicked then
          // case month clicked: automatically set the period to end of month
          break;

        case TimeConfig.YEARS:
          // case year click: automatically set the period to end of year
          break;
      }
    }

    #endregion


  }
}
