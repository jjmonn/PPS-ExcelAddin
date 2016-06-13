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
using VIBlend.Utilities;

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
      BORDER_COLOR
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
    SafeDictionary<Tuple<Rows, Columns>, dynamic> m_settings;

    public SettingsView()
    {
      InitializeComponent();
      m_settings = new SafeDictionary<Tuple<Rows, Columns>, dynamic>();
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
      m_saveFormatBT.Click += OnValidateFormatTab;
      m_saveConnectionButton.Click += OnValidateConnectionTab;
      m_otherValidateButton.Click += OnValidateOtherTab;
      m_formatDGV.CellValidated += OnFormatCellValidated;
      m_formatDGV.CellValueChanged += OnFormatCellValueChanged;
    }

    #region "Initialize"

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
      SetFormatColumn(Columns.TEXT_COLOR, Local.GetValue("settings.text_color"), new ColorPickerEditor());
      SetFormatColumn(Columns.BACKGROUND_COLOR, Local.GetValue("settings.background_color"), new ColorPickerEditor());
      SetFormatColumn(Columns.BOLD, Local.GetValue("settings.bold"), new CheckBoxEditor());
      SetFormatColumn(Columns.ITALIC, Local.GetValue("settings.italic"), new CheckBoxEditor());
      SetFormatColumn(Columns.BORDER_COLOR, Local.GetValue("settings.border"), new ColorPickerEditor());

      SetFormatRow(Rows.TITLE, Local.GetValue("settings.title"));
      SetFormatRow(Rows.IMPORTANT, Local.GetValue("settings.important"));
      SetFormatRow(Rows.NORMAL, Local.GetValue("settings.normal"));
      SetFormatRow(Rows.DETAIL, Local.GetValue("settings.detail"));

      FillFormatTab();
    }

    #region Formats

    void FillFormatTab()
    {
      m_formatDGV.FillField((UInt32)Rows.TITLE, (UInt32)Columns.PREVIEW, Local.GetValue("settings.title"));
      m_formatDGV.FillField((UInt32)Rows.TITLE, (UInt32)Columns.TEXT_COLOR, Settings.Default.titleFontColor);
      m_formatDGV.FillField((UInt32)Rows.TITLE, (UInt32)Columns.BACKGROUND_COLOR, Settings.Default.titleBackColor);
      m_formatDGV.FillField((UInt32)Rows.TITLE, (UInt32)Columns.BORDER_COLOR, Settings.Default.titleBordersColor);
      m_formatDGV.FillField((UInt32)Rows.TITLE, (UInt32)Columns.BOLD, Settings.Default.titleFontBold);
      m_formatDGV.FillField((UInt32)Rows.TITLE, (UInt32)Columns.ITALIC, Settings.Default.titleFontItalic);
      SetPreviewFormat(Rows.TITLE);

      m_formatDGV.FillField((UInt32)Rows.IMPORTANT, (UInt32)Columns.PREVIEW, Local.GetValue("settings.important"));
      m_formatDGV.FillField((UInt32)Rows.IMPORTANT, (UInt32)Columns.TEXT_COLOR, Settings.Default.importantFontColor);
      m_formatDGV.FillField((UInt32)Rows.IMPORTANT, (UInt32)Columns.BACKGROUND_COLOR, Settings.Default.importantBackColor);
      m_formatDGV.FillField((UInt32)Rows.IMPORTANT, (UInt32)Columns.BORDER_COLOR, Settings.Default.importantBordersColor);
      m_formatDGV.FillField((UInt32)Rows.IMPORTANT, (UInt32)Columns.BOLD, Settings.Default.importantFontBold);
      m_formatDGV.FillField((UInt32)Rows.IMPORTANT, (UInt32)Columns.ITALIC, Settings.Default.importantFontItalic);
      SetPreviewFormat(Rows.IMPORTANT);

      m_formatDGV.FillField((UInt32)Rows.NORMAL, (UInt32)Columns.PREVIEW, Local.GetValue("settings.normal"));
      m_formatDGV.FillField((UInt32)Rows.NORMAL, (UInt32)Columns.TEXT_COLOR, Settings.Default.normalFontColor);
      m_formatDGV.FillField((UInt32)Rows.NORMAL, (UInt32)Columns.BACKGROUND_COLOR, Settings.Default.normalBackColor);
      m_formatDGV.FillField((UInt32)Rows.NORMAL, (UInt32)Columns.BORDER_COLOR, Settings.Default.normalBordersColor);
      m_formatDGV.FillField((UInt32)Rows.NORMAL, (UInt32)Columns.BOLD, Settings.Default.normalFontBold);
      m_formatDGV.FillField((UInt32)Rows.NORMAL, (UInt32)Columns.ITALIC, Settings.Default.normalFontItalic);
      SetPreviewFormat(Rows.NORMAL);

      m_formatDGV.FillField((UInt32)Rows.DETAIL, (UInt32)Columns.PREVIEW, Local.GetValue("settings.detail"));
      m_formatDGV.FillField((UInt32)Rows.DETAIL, (UInt32)Columns.TEXT_COLOR, Settings.Default.detailFontColor);
      m_formatDGV.FillField((UInt32)Rows.DETAIL, (UInt32)Columns.BACKGROUND_COLOR, Settings.Default.detailBackColor);
      m_formatDGV.FillField((UInt32)Rows.DETAIL, (UInt32)Columns.BORDER_COLOR, Settings.Default.detailBordersColor);
      m_formatDGV.FillField((UInt32)Rows.DETAIL, (UInt32)Columns.BOLD, Settings.Default.detailFontBold);
      m_formatDGV.FillField((UInt32)Rows.DETAIL, (UInt32)Columns.ITALIC, Settings.Default.detailFontItalic);
      SetPreviewFormat(Rows.DETAIL);

      foreach (GridCell l_cell in m_formatDGV.CellsArea.Cells)
        SetCellFormat(l_cell, l_cell.Value);
    }

    void SaveFormats()
    {
      Settings.Default.titleFontColor = GetFormatValue<Color>(Rows.TITLE, Columns.TEXT_COLOR);
      Settings.Default.titleBackColor = GetFormatValue<Color>(Rows.TITLE, Columns.BACKGROUND_COLOR);
      Settings.Default.titleBordersColor = GetFormatValue<Color>(Rows.TITLE, Columns.BORDER_COLOR);
      Settings.Default.titleFontBold = GetFormatValue<bool>(Rows.TITLE, Columns.BOLD);
      Settings.Default.titleFontItalic = GetFormatValue<bool>(Rows.TITLE, Columns.ITALIC);

      Settings.Default.importantFontColor = GetFormatValue<Color>(Rows.IMPORTANT, Columns.TEXT_COLOR);
      Settings.Default.importantBackColor = GetFormatValue<Color>(Rows.IMPORTANT, Columns.BACKGROUND_COLOR);
      Settings.Default.importantBordersColor = GetFormatValue<Color>(Rows.IMPORTANT, Columns.BORDER_COLOR);
      Settings.Default.importantFontBold = GetFormatValue<bool>(Rows.IMPORTANT, Columns.BOLD);
      Settings.Default.importantFontItalic = GetFormatValue<bool>(Rows.IMPORTANT, Columns.ITALIC);

      Settings.Default.normalFontColor = GetFormatValue<Color>(Rows.NORMAL, Columns.TEXT_COLOR);
      Settings.Default.normalBackColor = GetFormatValue<Color>(Rows.NORMAL, Columns.BACKGROUND_COLOR);
      Settings.Default.normalBordersColor = GetFormatValue<Color>(Rows.NORMAL, Columns.BORDER_COLOR);
      Settings.Default.normalFontBold = GetFormatValue<bool>(Rows.NORMAL, Columns.BOLD);
      Settings.Default.normalFontItalic = GetFormatValue<bool>(Rows.NORMAL, Columns.ITALIC);

      Settings.Default.detailFontColor = GetFormatValue<Color>(Rows.DETAIL, Columns.TEXT_COLOR);
      Settings.Default.detailBackColor = GetFormatValue<Color>(Rows.DETAIL, Columns.BACKGROUND_COLOR);
      Settings.Default.detailBordersColor = GetFormatValue<Color>(Rows.DETAIL, Columns.BORDER_COLOR);
      Settings.Default.detailFontBold = GetFormatValue<bool>(Rows.DETAIL, Columns.BOLD);
      Settings.Default.detailFontItalic = GetFormatValue<bool>(Rows.DETAIL, Columns.ITALIC);

      Settings.Default.Save();
    }

    void SetFormatColumn(Columns p_id, string p_name, IEditor p_editor = null)
    {
      HierarchyItem l_column = m_formatDGV.SetDimension(Dimension.COLUMN, m_formatDGV.ColumnsHierarchy.Items, (UInt32)p_id, p_name);
      l_column.CellsEditor = p_editor;
    }

    void SetFormatRow(Rows p_id, string p_name)
    {
      m_formatDGV.SetDimension(Dimension.ROW, m_formatDGV.RowsHierarchy.Items, (UInt32)p_id, p_name);
    }

    void SetCellFormat(GridCell p_cell, object p_value)
    {
      if (p_value == null)
        return;
      if (p_value.GetType() == typeof(Color))
      {
        GridCellStyle l_style = GridTheme.GetDefaultTheme(p_cell.ColumnItem.DataGridView.VIBlendTheme).GridCellStyle;
        l_style.FillStyle = new FillStyleSolid((Color)p_value);
        l_style.TextColor = (Color)p_value;
        p_cell.DrawStyle = l_style;
      }
    }

    void SetPreviewFormat(Rows p_rowId)
    {
      HierarchyItem l_row = m_formatDGV.Rows[(UInt32)p_rowId];

      if (l_row == null)
        return;
      GridCell l_cell = l_row.Cells[0];
      if (l_cell == null)
        return;
      GridCellStyle l_style = GridTheme.GetDefaultTheme(l_cell.ColumnItem.DataGridView.VIBlendTheme).GridCellStyle;

      l_style.FillStyle = new FillStyleSolid(GetFormatValue<Color>(p_rowId, Columns.BACKGROUND_COLOR));
      l_style.TextColor = GetFormatValue<Color>(p_rowId, Columns.TEXT_COLOR);
      l_style.BorderColor = GetFormatValue<Color>(p_rowId, Columns.BORDER_COLOR);
      l_style.Font = new Font(l_style.Font.FontFamily, l_style.Font.Size, FontStyle.Regular);
      l_style.Font = new Font(l_style.Font.FontFamily, l_style.Font.Size,
        (GetFormatValue<bool>(p_rowId, Columns.BOLD) ? FontStyle.Bold : FontStyle.Regular) |
        (GetFormatValue<bool>(p_rowId, Columns.ITALIC) ? FontStyle.Italic : FontStyle.Regular));
      l_cell.DrawStyle = l_style;
    }

    T GetFormatValue<T>(Rows p_rowId, Columns p_columnId)
    {
      return ((T)m_formatDGV.GetCellValue((UInt32)p_rowId, (UInt32)p_columnId));
    }

    #endregion

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
    #endregion

    #region User callbacks

    void OnValidateConnectionTab(object p_sender, EventArgs p_e)
    {
      Settings.Default.serverIp = m_serverAddressTB.Text;
      Settings.Default.port_number = (ushort)m_portTB.Value;
      Settings.Default.user = m_userTB.Text;
      Settings.Default.Save();
    }

    void OnValidateFormatTab(object p_sender, EventArgs p_e)
    {
      SaveFormats();
    }

    void OnValidateOtherTab(object p_sender, EventArgs p_e)
    {
      if (m_languageComboBox.SelectedItem != null)
        Settings.Default.language = (UInt32)m_languageComboBox.SelectedItem.Value;
      Settings.Default.Save();
      Addin.SelectLanguage();
      MultilanguageSetup();
    }

    void OnFormatCellValidated(object p_sender, CellEventArgs p_args)
    {
      SetCellFormat(p_args.Cell, p_args.Cell.EditValue);
    }

    void OnFormatCellValueChanged(object p_sender, CellEventArgs p_args)
    {
      foreach (KeyValuePair<UInt32, HierarchyItem> l_pair in m_formatDGV.Rows)
        if (l_pair.Value == p_args.Cell.RowItem)
          SetPreviewFormat((Rows)l_pair.Key);
    }

    #endregion
  }
}
