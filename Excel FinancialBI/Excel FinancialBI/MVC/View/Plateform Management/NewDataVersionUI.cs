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
    internal uint m_parentId { set; private get; }
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
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewDataVersionUI_FormClosing);

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
      l_version.ParentId = m_parentId;   
      l_version.StartPeriod = 0;
      l_version.NbPeriod = (ushort)m_nbPeriods.Value;
      l_version.IsFolder = false;
      l_version.ItemPosition = 0;
      l_version.TimeConfiguration = (TimeConfig)this.m_timeConfigCB.SelectedItem.Value;
      l_version.RateVersionId = p_packet.ReadUint32();
      l_version.GlobalFactVersionId = p_packet.ReadUint32();
      l_version.CreatedAt = DateTime.Now.ToShortDateString();
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

    private void NewDataVersionUI_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;
      this.Hide();
    }

    #endregion



    private bool IsFormValid(ref string name)
    {

      if (string.IsNullOrEmpty(m_timeConfigCB.Text))
      {
        MessageBox.Show(Local.GetValue("facts_versions.msg_config_selection"));
        return false;
      }

      if (string.IsNullOrEmpty(m_startingPeriodDatePicker.Text) | Information.IsDate(m_startingPeriodDatePicker.Value) == false)
      {
        MessageBox.Show(Local.GetValue("facts_versions.msg_starting_period"));
        return false;
      }

      // Check exchange rates and global facts selection
      if (m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode != null)
      {
        if (m_controller.IsRatesVersionValid(m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Value) == false)
        {
          MessageBox.Show(m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Text + Local.GetValue("facts_versions.msg_cannot_use_exchange_rates_folder"));
          return false;
        }
      }
      else
      {
        MessageBox.Show(Local.GetValue("facts_versions.msg_select_rates_version"));
      }

      if (m_factsVersionVTreeviewbox.TreeView.SelectedNode != null)
      {
        if (m_controller.IsFactsVersionValid(m_factsVersionVTreeviewbox.TreeView.SelectedNode.Value) == false)
        {
          MessageBox.Show(m_factsVersionVTreeviewbox.TreeView.SelectedNode.Text + Local.GetValue("facts_versions.msg_cannot_use_global_fact_folder"));
          return false;
        }
      }
      else
      {
        MessageBox.Show(Local.GetValue("facts_versions.msg_select_global_facts_version"));
      }

      // Check exchange rates and global facts validity
      switch ((TimeConfig)this.m_timeConfigCB.SelectedItem.Value)
      {
        case TimeConfig.YEARS:

        case TimeConfig.MONTHS:
          DateTime l_startDate = this.m_startingPeriodDatePicker.Value.Value;
          dynamic l_startPeriodCheck = GetLastDayOfPeriod(m_timeConfigCB.SelectedValue, l_startDate.ToOADate);

          if (m_controller.IsRatesVersionCompatible(l_startPeriodCheck, this.m_nbPeriods.Value, m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Value) == false)
          {
            MessageBox.Show(Local.GetValue("facts_versions.msg_rates_version_mismatch"));
            return false;
          }

          if (m_controller.IsFactVersionCompatibleWithPeriods(l_startPeriodCheck, this.m_nbPeriods.Value, m_factsVersionVTreeviewbox.TreeView.SelectedNode.Value) == false)
          {
            MessageBox.Show(Local.GetValue("facts_versions.msg_fact_version_mismatch"));
            return false;
          }
          break;

        // So far only Months and Years period config are allowed

      }
      return true;

    }



  }
}
