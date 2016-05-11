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
using System.Diagnostics;

namespace FBI.MVC.View
{
  using Controller;
  using Forms;
  using Model;
  using Model.CRUD;
  using Utils;
  using Network;

  public partial class CUI2VisualisationChartsSettings : Form, IView
  {
    private static readonly Int32 DEFAULT_SIZE = 30;
    private static readonly Color DEFAULT_COLOR = Color.Gray;

    private CUIVisualizationController m_controller;
    private vTreeView m_treeView = new vTreeView();

    private List<Control[]> m_series = new List<Control[]>(); //Containing controls of every serie. { Label, vTreeViewBox, vColorPicker, vButton }
    private ChartSettings m_chartSettings = null;

    public CUI2VisualisationChartsSettings()
    {
      this.InitializeComponent();
      this.MultilangueSetup();
      this.LoadView();
    }

    public void LoadView()
    {
      this.SuscribeEvents();
      FbiTreeView<Account>.Load(m_treeView.Nodes, AccountModel.Instance.GetDictionary());
      this.ResetView();
    }

    private void MultilangueSetup()
    {
      m_chartTitleLabel.Text = Local.GetValue("CUI_Charts.chart_title");
      m_AccountLabel.Text = Local.GetValue("general.account");
      m_ColorLabel.Text = Local.GetValue("general.color");
      m_addSerie.Text = Local.GetValue("CUI_Charts.add_serie");
      m_saveButton.Text = Local.GetValue("general.save");
      this.Text = Local.GetValue("CUI_Charts.charts_settings");
    }

    private void SuscribeEvents()
    {
      this.FormClosing += OnClosing;
      m_addSerie.Click += OnAddSerieClicked;
      m_saveButton.Click += OnSaveClicked;
    }

    public void Reload()
    {
      this.ResetView();
      ChartSettingsModel.Instance.ReadEvent += OnChartSettingRead;
    }

    private void UnsuscribeEvents()
    {
      ChartSettingsModel.Instance.ReadEvent -= OnChartSettingRead;
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as CUIVisualizationController;
    }

    public void LoadSettings(ChartSettings p_settings)
    {
      List<ChartAccount> l_accounts;

      m_chartSettings = p_settings;
      if (p_settings == null || (l_accounts = ChartAccountModel.Instance.GetList(p_settings.Id)) == null)
      {
        this.AddDefaultEmptySerie();
        return;
      }
      m_chartTitle.Text = p_settings.Name;
      if (l_accounts.Count == 0)
      {
        this.AddDefaultEmptySerie();
      }
      for (int i = 0; i < l_accounts.Count; ++i)
      {
        this.AddSerie(this.AccountName(l_accounts[i].AccountId), Color.FromArgb(l_accounts[i].Color));
      }
    }

    public bool SaveChartSettings()
    {
      if (m_chartTitle.Text.Trim() == "")
      {
        MessageBox.Show(Local.GetValue("CUI_Charts.error.no_name"));
        return (false);
      }
      if (!this.AreTreeViewBoxesFilled())
      {
        MessageBox.Show(Local.GetValue("CUI_Charts.error.incomplete_series"));
        return (false);
      }
      if (!this.AreTreeViewBoxValid())
      {
        MessageBox.Show(Local.GetValue("CUI_Charts.error.invalid_serie"));
        return (false);
      }
      return (m_controller.CRUChartSettings(m_chartSettings, m_chartTitle.Text.Trim()));
    }

    private bool SaveChartAccounts()
    {
      List<Tuple<string, Color>> l_accounts = new List<Tuple<string, Color>>();

      m_controller.ApplyLastCompute(m_chartSettings, true);
      if (!this.TooMuchInfoSelection())
        return (false);
      foreach (Control[] l_control in m_series) //Save every serie
      {
        l_accounts.Add(new Tuple<string, Color>(l_control[1].Text, ((vColorPicker)l_control[2]).SelectedColor));
      }
      if (!m_controller.CRUDChartAccounts(m_chartSettings, l_accounts))
      {
        MessageBox.Show(m_controller.Error);
        return (false);
      }
      return (true);
    }

    private void ResetView()
    {
      m_chartTitle.ResetText();
      for (int i = m_series.Count - 1; i >= 0; --i)
        this.RemoveSerie(i, true);
    }

    #region Events

    private void OnClosing(object sender, FormClosingEventArgs e)
    {
      this.UnsuscribeEvents();
    }

    private void OnAddSerieClicked(object sender, EventArgs e)
    {
      this.AddSerie();
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
      this.SaveChartSettings();
    }

    private void OnRemoveSerieClicked(object sender, EventArgs e)
    {
      Int32 l_pos;
      vButton l_button = (vButton)sender;

      if ((l_pos = this.FindButtonFromSeries(l_button)) != -1)
        this.RemoveSerie(l_pos);
    }

    private void OnvTreeViewBoxEnter(object sender, EventArgs e)
    {
      vTreeViewBox l_clickedTVBox = (vTreeViewBox)sender;

      if (l_clickedTVBox.TreeView.Nodes.Count == 0)
        AFbiTreeView.Copy(l_clickedTVBox.TreeView, m_treeView);
    }

    delegate void OnChartSettingRead_delegate(ErrorMessage p_status, ChartSettings p_settings);
    void OnChartSettingRead(ErrorMessage p_status, ChartSettings p_settings)
    {
      if (InvokeRequired)
      {
        OnChartSettingRead_delegate func = new OnChartSettingRead_delegate(OnChartSettingRead);
        Invoke(func, p_status, p_settings);
      }
      else
      {
        if (p_status != Network.ErrorMessage.SUCCESS)
        {
          MessageBox.Show(Local.GetValue("CUI_Charts.error.settings"));
        }
        else
        {
          m_chartSettings = p_settings;
          if (this.SaveChartAccounts())
            this.Close();
        }
      }
    }

    #endregion

    #region Utils

    private void AddDefaultEmptySerie()
    {
      if (m_series.Count == 0)
        this.AddSerie();
    }

    private void AddSerie(string p_accountName = "", Color? p_color = null)
    {
      vLabel l_text = new vLabel();
      vTreeViewBox l_serie = this.GetSerievTVB(p_accountName);
      vColorPicker l_color = this.GetSerieColorPicker(p_color);
      vButton l_remove = GetSerieRemoveButton();

      l_text.Text = Local.GetValue("CUI_Charts.serie");
      m_series.Add(new Control[] { l_text, l_serie, l_color, l_remove });

      m_flowPanel.Controls.Add(l_text);
      m_flowPanel.Controls.Add(l_serie);
      m_flowPanel.Controls.Add(l_color);
      m_flowPanel.Controls.Add(l_remove);
      this.MoveStandardControls(DEFAULT_SIZE);
    }

    //Move standard controls using m_location, and a p_loc value.
    //Used to move 'save', 'add' button and resize the window height.
    private void MoveStandardControls(Int32 p_loc)
    {
      m_saveButton.Location = new Point(m_saveButton.Location.X, m_saveButton.Location.Y + p_loc);
      m_addSerie.Location = new Point(m_addSerie.Location.X, m_addSerie.Location.Y + p_loc);
      m_flowPanel.Height += p_loc;
      this.Height += p_loc;
    }

    private void RemoveSerie(Int32 p_pos, bool p_force = false)
    {
      Control[] l_controls = m_series[p_pos];

      l_controls[1].ResetText();
      ((vColorPicker)l_controls[2]).SelectedColor = DEFAULT_COLOR;
      if (p_force || p_pos != 0 || (p_pos == 0 && m_series.Count > 1))
      {
        foreach (Control l_control in l_controls)
          m_flowPanel.Controls.Remove(l_control);
        m_series.RemoveAt(p_pos);
        this.MoveStandardControls(-DEFAULT_SIZE);
      }
    }

    private Int32 FindButtonFromSeries(vButton p_button)
    {
      Int32 i = 0;
      foreach (Control[] l_control in m_series)
      {
        if (l_control[3] == p_button)
          return (i);
        ++i;
      }
      return (-1);
    }

    private bool AreTreeViewBoxesFilled()
    {
      foreach (Control[] l_controls in m_series)
      {
        if (l_controls[1].Text.Trim() == "")
          return (false);
      }
      return (true);
    }

    private bool AreTreeViewBoxValid()
    {
      Account l_account;
      HashSet<string> l_accounts = new HashSet<string>();

      foreach (Control[] l_controls in m_series)
      {
        if ((l_account = AccountModel.Instance.GetValue(l_controls[1].Text)) == null)
          return (false);
        if (l_account.FormulaType == Account.FormulaTypes.TITLE)
          return (false);
        if (!l_accounts.Contains(l_controls[1].Text))
          l_accounts.Add(l_controls[1].Text);
        else
          MessageBox.Show(Local.GetValue("CUI_Charts.warning.multiple_account"));
      }
      return (true);
    }

    private string AccountName(UInt32 p_accountId)
    {
      Account l_account;

      if ((l_account = AccountModel.Instance.GetValue(p_accountId)) == null)
        return (Local.GetValue("CUI_Charts.error.invalid_account"));
      return (l_account.Name);
    }

    private List<string> VersionNames(List<UInt32> p_versions)
    {
      List<string> l_versions = new List<string>();

      foreach (UInt32 l_versionId in p_versions)
      {
        try
        {
          l_versions.Add(VersionModel.Instance.GetValue(l_versionId).Name);
        }
        catch
        {
          l_versions.Add("N/A");
        }
      }
      return (l_versions);
    }

    public bool TooMuchInfoSelection()
    {
      if (m_chartSettings.Versions == null)
        return (true);
      if (m_series.Count > 1 && m_chartSettings.Versions.Count > 1 && m_chartSettings.HasDeconstruction) //If you can't deconstruct because there is too much informations
      {
        DialogResult result = MessageBox.Show(Local.GetValue("CUI_Charts.choose_version_or_deconstruction"), Local.GetValue("CUI_Charts.chart_title"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        if (result == DialogResult.Yes) //Use version view, ignore deconstruction.
        {
          m_chartSettings.HasDeconstruction = false;
        }
        else if (result == DialogResult.No) //Use deconstruction, specify a version
        {
          DialogResult l_result = FbiUserBox.ShowDialog(Local.GetValue("CUI_Charts.choose_version"),
             Local.GetValue("CUI_Charts.choose_version"),
             this.VersionNames(m_chartSettings.Versions));
          if (l_result != DialogResult.OK)
            return (false);
          m_chartSettings.Versions = new List<UInt32>();
          m_chartSettings.Versions.Add(m_controller.LastConfig.Request.Versions[FbiUserBox.Index]);
        }
      }
      return (true);
    }

    #endregion

    #region STDControls

    private vTreeViewBox GetSerievTVB(string p_name)
    {
      vTreeViewBox l_serie = new vTreeViewBox();

      l_serie.Text = p_name;
      l_serie.Size = new System.Drawing.Size(246, 23);
      l_serie.Enter += OnvTreeViewBoxEnter;
      return (l_serie);
    }

    private vColorPicker GetSerieColorPicker(Color? p_color)
    {
      vColorPicker l_color = new vColorPicker();

      l_color.SelectedColor = p_color ?? DEFAULT_COLOR;
      l_color.Size = new System.Drawing.Size(143, 23);
      return (l_color);
    }

    private vButton GetSerieRemoveButton()
    {
      vButton l_remove = new vButton();

      l_remove.Size = new System.Drawing.Size(23, 23);
      l_remove.Click += OnRemoveSerieClicked;
      return (l_remove);
    }

    #endregion
  }
}