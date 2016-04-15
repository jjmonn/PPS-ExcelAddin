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
  using MVC.Model;
  using MVC.Model.CRUD;
  using Utils;

  public partial class CUI2VisualisationChartsSettings : Form, IView
  {
    private static readonly Int32 SEP_SIZE = 40;
    private static readonly Color DEFAULT_COLOR = Color.Gray;

    private CUIVisualizationController m_controller;
    private Int32 m_location = 0; //Default location (axis Y) of m_serieLabel
    private vTreeView m_treeView = new vTreeView();

    private List<Control[]> m_series = new List<Control[]>(); //Containing controls of every serie. { Label, vTreeViewBox, vColorPicker }

    private ChartSettings m_settings = null;

    public CUI2VisualisationChartsSettings()
    {
      this.InitializeComponent();
      this.MultilangueSetup();
      this.LoadView();
    }

    public void LoadView()
    {
      this.SuscribeEvents();
      m_location = m_serieLabel.Location.Y;
      m_serieColor.SelectedColor = DEFAULT_COLOR;
      FbiTreeView<Account>.Load(m_treeView.Nodes, AccountModel.Instance.GetDictionary());
      m_series.Add(new Control[] { m_serieLabel, m_serie, m_serieColor });
    }

    private void MultilangueSetup()
    {
      m_chartTitleLabel.Text = Local.GetValue("CUI_Charts.chart_title");
      m_serieLabel.Text = Local.GetValue("CUI_Charts.serie");
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
      m_removeSerie.Click += OnRemoveSerieClicked;
      m_serie.Enter += OnvTreeViewBoxEnter;
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as CUIVisualizationController;
    }

    public void LoadSettings(ChartSettings p_settings)
    {
      int i = 1;

      if (p_settings == null)
        return;

      m_settings = p_settings;
      m_chartTitle.Text = m_settings.Name;
      if (p_settings.Series.Count >= 1)
      {
        m_serie.Text = p_settings.Series[0].Account.Name;
        m_serieColor.SelectedColor = p_settings.Series[0].Color;
      }
      while (i < p_settings.Series.Count)
      {
        this.AddSerie(p_settings[i].Account.Name, p_settings[i].Color);
        ++i;
      }
    }

    public bool SaveSettings()
    {
      int i = 0;

      if (m_settings == null)
      {
        m_settings = new ChartSettings();
      }
      if ((m_settings.Name = m_chartTitle.Text.Trim()) == "")
      {
        MessageBox.Show(Local.GetValue("CUI_Charts.error.no_name"));
        return (false);
      }
      m_controller.ApplyLastCompute(m_settings, true);

      if (m_series.Count > 1 && m_settings.Versions.Count > 1 && m_settings.HasDeconstruction) //If you can't deconstruct because there is too much informations
      {
        DialogResult result = MessageBox.Show(Local.GetValue("CUI_Charts.choose_version_or_deconstruction"), Local.GetValue("CUI_Charts.chart_title"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        if (result == DialogResult.Yes) //Use version view, ignore deconstruction.
        {
          m_settings.HasDeconstruction = false;
        }
        else if (result == DialogResult.No) //Use deconstruction, specify a version
        {
           DialogResult l_result = FbiUserBox.ShowDialog(Local.GetValue("CUI_Charts.choose_version"),
              Local.GetValue("CUI_Charts.choose_version"),
              this.VersionNames(m_settings.Versions));
          if (l_result != DialogResult.OK)
            return (false);
          m_settings.Versions = new List<UInt32>();
          m_settings.Versions.Add(m_controller.LastConfig.Request.Versions[FbiUserBox.Index]);
        }
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

      foreach (Control[] l_control in m_series) //Save every serie
      {
        m_settings.AddUpdateSerie(i++, l_control[1].Text, ((vColorPicker)l_control[2]).SelectedColor);
      }
      m_settings.Series.RemoveRange(i, m_settings.Series.Count - i);
      if (!m_controller.CreateUpdateSettings(m_settings))
      {
        MessageBox.Show(Local.GetValue(m_controller.Error));
        return (false);
      }
      return (true);
    }

    public void ResetView()
    {
      m_serie.Text = "";
      m_chartTitle.Text = "";
      m_serieColor.SelectedColor = DEFAULT_COLOR;
      while (this.HasSeries())
      {
        this.RemoveLastSerie();
      }
      m_location = m_serieLabel.Location.Y;
      m_settings = null;
    }

    #region Events

    private void OnClosing(object sender, FormClosingEventArgs e)
    {
      this.ResetView();
    }

    private void OnAddSerieClicked(object sender, EventArgs e)
    {
      this.AddSerie();
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
      if (this.SaveSettings())
      {
        this.Close();
      }
    }

    private void OnRemoveSerieClicked(object sender, EventArgs e)
    {
      this.RemoveLastSerie();
    }

    private void OnvTreeViewBoxEnter(object sender, EventArgs e)
    {
      vTreeViewBox l_clickedTVBox = (vTreeViewBox)sender;

      if (l_clickedTVBox.TreeView.Nodes.Count == 0)
        AFbiTreeView.Copy(l_clickedTVBox.TreeView, m_treeView);
    }

    #endregion

    #region Utils

    private void AddSerie(string p_accountName = "", Color? p_color = null)
    {
      vLabel l_text;
      vTreeViewBox l_serie;
      vColorPicker l_color;

      m_location += SEP_SIZE;
      l_text = ControlUtils.Clone(m_serieLabel);
      l_text.Location = new Point(m_serieLabel.Location.X, m_location);
      l_serie = ControlUtils.Clone(m_serie);
      l_serie.Location = new Point(m_serie.Location.X, m_location);
      l_serie.Text = p_accountName;
      l_serie.Enter += OnvTreeViewBoxEnter;
      l_color = ControlUtils.Clone(m_serieColor);
      l_color.Location = new Point(m_serieColor.Location.X, m_location);
      l_color.SelectedColor = p_color ?? DEFAULT_COLOR;

      this.MoveStandardControls(SEP_SIZE);
      m_series.Add(new Control[] { l_text, l_serie, l_color });
      this.Controls.Add(l_text);
      this.Controls.Add(l_serie);
      this.Controls.Add(l_color);
    }

    //Move standard controls using m_location, and a p_loc value.
    //Used to move 'save', 'remove', 'add' button and resize the window height.
    private void MoveStandardControls(Int32 p_loc)
    {
      m_removeSerie.Location = new Point(m_removeSerie.Location.X, m_location);
      m_saveButton.Location = new Point(m_saveButton.Location.X, m_saveButton.Location.Y + p_loc);
      m_addSerie.Location = new Point(m_addSerie.Location.X, m_addSerie.Location.Y + p_loc);
      this.Height += p_loc;
    }

    private void RemoveLastSerie()
    {
      Control[] l_lastSerie;

      if (!this.HasSeries())
        return;

      m_location -= SEP_SIZE;
      l_lastSerie = m_series[m_series.Count - 1];//last serie created
      m_series.Remove(l_lastSerie);
      foreach (Control l_control in l_lastSerie)
        this.Controls.Remove(l_control);
      this.MoveStandardControls(-SEP_SIZE);
    }

    private bool HasSeries()
    {
      return (m_series.Count > 1); //Don't remove the first serie !
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

      foreach (Control[] l_controls in m_series)
      {
        if ((l_account = AccountModel.Instance.GetValue(l_controls[1].Text)) == null)
          return (false);
        if (l_account.FormulaType == Account.FormulaTypes.TITLE)
          return (false);
      }
      return (true);
    }

    private List<string> VersionNames(List<UInt32> p_versions)
    {
      List<string> l_versions = new List<string>();

      foreach (UInt32 l_versionId in p_versions)
      {
        try
        {
          Debug.WriteLine(">> " + l_versionId + " " + VersionModel.Instance.GetValue(l_versionId).Name);
          l_versions.Add(VersionModel.Instance.GetValue(l_versionId).Name);
        }
        catch
        {
          l_versions.Add("N/A");
        }
      }
      return (l_versions);
    }

    #endregion
  }
}
