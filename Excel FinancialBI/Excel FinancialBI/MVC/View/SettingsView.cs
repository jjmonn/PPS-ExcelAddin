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
  using Settings = Properties.Settings;

  public partial class SettingsView : Form, IView
  {
    SettingsController m_controller;

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
        m_formatsGroup.Text = Local.GetValue("settings.report_formats");
        m_consolidationCurrencyLabel.Text = Local.GetValue("settings.report_formats");
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
      m_serverAddressTB.Text = Settings.Default.serverIp;
      m_portTB.Text = Settings.Default.port_number.ToString();
      m_userTB.Text = Settings.Default.user;
    }
  }
}
