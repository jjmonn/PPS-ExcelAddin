using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIBlend.WinForms.Controls;
using VIBlend.WinForms.DataGridView;

namespace FBI.MVC.View
{
  using Controller;
  using Utils;
  using Model;
  using Model.CRUD;
  using Forms;
  using Settings = Properties.Settings;
  using Dimension = FBI.Forms.BaseFbiDataGridView<UInt32>.Dimension;

  public partial class SettingsView : Form, IView
  {
    enum Columns
    {
      PREVIEW,
      TEXT_COLOR,
      BACKGROUND_COLOR,
      BOLD,
      ITALIC,
      BORDER
    };

    enum Rows
    {
      TITLE,
      IMPORTANT,
      NORMAL,
      DETAIL
    }

    SettingsController m_controller;
    SafeDictionary<UInt32, ListItem> m_languagesItems = new SafeDictionary<UInt32, ListItem>();
    SafeDictionary<UInt32, ListItem> m_currenciesItems = new SafeDictionary<UInt32, ListItem>();
    BaseFbiDataGridView<UInt32> m_formatDGV = new BaseFbiDataGridView<UInt32>();

    public SettingsView()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as SettingsController;
    }

    void MultilanguageSetup()
    {
      this.Text = Local.GetValue("settings.settings");
      m_serverAddressLabel.Text = Local.GetValue("settings.server_address");
      m_portNumberLabel.Text = Local.GetValue("settings.port_number");
      m_userIdLabel.Text = Local.GetValue("connection.user_id");
      m_saveConnectionButton.Text = Local.GetValue("general.save");
      m_formatsGroup.Text = Local.GetValue("settings.report_format");
      m_consolidationCurrencyLabel.Text = Local.GetValue("settings.currency");
      m_languageLabel.Text = Local.GetValue("settings.language");
      m_otherValidateButton.Text = Local.GetValue("general.save");
      m_connectionTab.Text = Local.GetValue("connection.connection");
      m_formatsTab.Text = Local.GetValue("settings.display_options");
      m_otherTab.Text = Local.GetValue("settings.preferences");
    }

    void SuscribeEvents()
    {
      m_saveConnectionButton.Click += OnValidateConnectionTab;
      m_otherValidateButton.Click += OnValidateOtherTab;
    }

    void OnValidateConnectionTab(object sender, EventArgs e)
    {
      Settings.Default.serverIp = m_serverAddressTB.Text;
      Settings.Default.port_number = (ushort)m_portTB.Value;
      Settings.Default.user = m_userTB.Text;
      Settings.Default.Save();
    }

    void OnValidateOtherTab(object sender, EventArgs e)
    {
      if (m_languageComboBox.SelectedItem != null)
        Settings.Default.language = (UInt32)m_languageComboBox.SelectedItem.Value;
      Settings.Default.Save();
      Addin.SelectLanguage();
      MultilanguageSetup();
    }

    public void LoadView()
    {
      MultilanguageSetup();
      LoadConnectionTab();
      LoadFormatTab();
      LoadOtherTab();
      SuscribeEvents();
    }

    void LoadFormatTab()
    {
      m_formatsGroup.Controls.Add(m_formatDGV);
      m_formatDGV.Dock = DockStyle.Fill;
      SetFormatColumn(Columns.PREVIEW, Local.GetValue("settings.format_preview"));
      SetFormatColumn(Columns.TEXT_COLOR, Local.GetValue("settings.text_color"));
      SetFormatColumn(Columns.BACKGROUND_COLOR, Local.GetValue("settings.background_color"));
      SetFormatColumn(Columns.BOLD, Local.GetValue("settings.bold"));
      SetFormatColumn(Columns.ITALIC, Local.GetValue("settings.italic"));
      SetFormatColumn(Columns.BORDER, Local.GetValue("settings.border"));

      SetFormatRow(Rows.TITLE, Local.GetValue("settings.title"));
      SetFormatRow(Rows.IMPORTANT, Local.GetValue("settings.important"));
      SetFormatRow(Rows.NORMAL, Local.GetValue("settings.normal"));
      SetFormatRow(Rows.DETAIL, Local.GetValue("settings.detail"));
    }

    void SetFormatColumn(Columns p_id, string p_name)
    {
      m_formatDGV.SetDimension(Dimension.COLUMN, m_formatDGV.ColumnsHierarchy.Items, (UInt32)p_id, p_name);
    }

    void SetFormatRow(Rows p_id, string p_name)
    {
      m_formatDGV.SetDimension(Dimension.ROW, m_formatDGV.RowsHierarchy.Items, (UInt32)p_id, p_name);
    }

    void LoadOtherTab()
    {
      m_languageComboBox.Items.Clear();
      m_currenciesCombobox.Items.Clear();
      m_languagesItems[0] = CreateListItem(Local.GetValue("settings.english"), 0, m_languageComboBox);
      m_languagesItems[1] = CreateListItem(Local.GetValue("settings.french"), 1, m_languageComboBox);
      if (m_languagesItems[Settings.Default.language] != null)
       m_languageComboBox.SelectedItem = m_languagesItems[Settings.Default.language];

      foreach (Currency l_currency in CurrencyModel.Instance.GetUsedCurrenciesDic().Values)
        m_currenciesItems[l_currency.Id] = CreateListItem(l_currency.Name, l_currency.Id, m_currenciesCombobox);
      m_currenciesCombobox.SelectedItem = m_currenciesItems[Settings.Default.currentCurrency];
      m_currenciesCombobox.Enabled = Network.NetworkManager.IsConnected();
    }

    void LoadConnectionTab()
    {
      m_serverAddressTB.Text = Settings.Default.serverIp;
      m_portTB.Value = Settings.Default.port_number;
      m_userTB.Text = Settings.Default.user;
    }

    ListItem CreateListItem(string p_text, UInt32 p_value, vComboBox p_cb)
    {
      ListItem l_item = new ListItem();

      l_item.Text = p_text;
      l_item.Value = p_value;
      p_cb.Items.Add(l_item);
      return (l_item);
    }
  }
}
