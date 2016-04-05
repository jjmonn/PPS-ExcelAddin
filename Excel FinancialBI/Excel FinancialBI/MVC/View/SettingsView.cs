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

namespace FBI.MVC.View
{
  using Controller;
  using Utils;
  using Settings = Properties.Settings;

  public partial class SettingsView : Form, IView
  {
    SettingsController m_controller;
    SafeDictionary<int, ListItem> m_languagesItems = new SafeDictionary<int,ListItem>();

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
        m_consolidationCurrencyLabel.Text = Local.GetValue("settings.report_format");
        m_languageLabel.Text = Local.GetValue("settings.language");
        m_otherValidateButton.Text = Local.GetValue("general.save");
        m_connectionTab.Text = Local.GetValue("connection.connection");
        m_formatsTab.Text = Local.GetValue("settings.display_options");
        m_otherTab.Text = Local.GetValue("settings.preferences");
    }

    void SuscribeEvents()
    {
      m_saveConnectionButton.Click += OnValidateClick;
    }

    void OnValidateClick(object sender, EventArgs e)
    {
      Settings.Default.serverIp = m_serverAddressTB.Text;
      Settings.Default.port_number = (ushort)m_portTB.Value;
      Settings.Default.user = m_userTB.Text;
      Settings.Default.Save();
    }

    public void LoadView()
    {
      MultilanguageSetup();
      LoadConnectionTab();
      LoadOtherTab();
      SuscribeEvents();
    }

    void LoadOtherTab()
    {
      m_languagesItems[0] = CreateListItem(Local.GetValue("settings.english"));
      m_languagesItems[1] = CreateListItem(Local.GetValue("settings.french"));
      foreach (ListItem l_item in m_languagesItems.Values)
        m_languageComboBox.Items.Add(l_item);
      if (m_languagesItems[Properties.Settings.Default.language] != null)
       m_languageComboBox.SelectedItem = m_languagesItems[Properties.Settings.Default.language];
    }

    ListItem CreateListItem(string p_text)
    {
      ListItem l_item = new ListItem();

      l_item.Text = p_text;
      return (l_item);
    }

    void LoadConnectionTab()
    {
      m_serverAddressTB.Text = Settings.Default.serverIp;
      m_portTB.Value = Settings.Default.port_number;
      m_userTB.Text = Settings.Default.user;
    }
  }
}
