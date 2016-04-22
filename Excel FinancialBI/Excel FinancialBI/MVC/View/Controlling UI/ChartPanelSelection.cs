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
  using Utils;
  using Controller;
  using Model;
  using Model.CRUD;

  public partial class ChartPanelSelection : Form, IView
  {
    private const UInt32 MAX_PANEL = 8;

    private CUIVisualizationController m_controller;

    public ChartPanelSelection()
    {
      InitializeComponent();
      this.MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      m_text.Text = "";
      m_save.Text = Local.GetValue("general.save");
      m_next.Text = Local.GetValue("general.next");
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as CUIVisualizationController;
    }

    public void LoadView()
    {
      this.SuscribeEvents();
      this.LoadListBox();
    }

    private void SuscribeEvents()
    {
      m_next.Click += OnNextClick;
      m_save.Click += OnSaveClick;
      m_list.SelectedItemChanged += OnListChanged;
      this.FormClosed += OnClosed;

      ChartPanelModel.Instance.CreationEvent += OnCreatePanel;
      ChartPanelModel.Instance.UpdateEvent += OnUpdatePanel;
      ChartPanelModel.Instance.ReadEvent += OnReadPanel;
    }

    private void CloseView()
    {
      ChartPanelModel.Instance.CreationEvent -= OnCreatePanel;
      ChartPanelModel.Instance.UpdateEvent -= OnUpdatePanel;
      ChartPanelModel.Instance.ReadEvent -= OnReadPanel;
    }

    #region Events

    private void OnClosed(object sender, FormClosedEventArgs e)
    {
      this.CloseView();
    }

    private void OnListChanged(object sender, EventArgs p_e)
    {
      m_text.Text = ((UInt32)m_list.SelectedItem.Value == ChartPanel.INVALID_ID ? "" : m_list.SelectedItem.Text);
    }

    private void OnSaveClick(object sender, EventArgs e)
    {
      if (m_list.SelectedItem == null || m_text.Text.Trim() == "")
        return;
      if (!m_controller.CRUPanel((UInt32)m_list.SelectedItem.Value, m_text.Text.Trim()))
      {
        MessageBox.Show("general.error.system");
      }
    }

    private void OnNextClick(object sender, EventArgs e)
    {
      if (m_list.SelectedItem == null || (UInt32)m_list.SelectedItem.Value == ChartPanel.INVALID_ID)
        return;

      this.DialogResult = System.Windows.Forms.DialogResult.OK;
      m_controller.PanelId = (UInt32)m_list.SelectedItem.Value;
      this.Close();
    }

    private void OnCreatePanel(Network.ErrorMessage p_status, uint p_id)
    {
      if (p_status != Network.ErrorMessage.SUCCESS)
      {
        MessageBox.Show(Local.GetValue("CUI_Charts.error.create_panel") + " " + Network.Error.GetMessage(p_status));
      }
    }

    private void OnUpdatePanel(Network.ErrorMessage p_status, uint p_id)
    {
      if (p_status != Network.ErrorMessage.SUCCESS)
      {
        MessageBox.Show(Local.GetValue("CUI_Charts.error.update_panel") + " " + Network.Error.GetMessage(p_status));
      }
    }

    private void OnReadPanel(Network.ErrorMessage p_status, ChartPanel p_panel)
    {
      if (p_status == Network.ErrorMessage.SUCCESS)
      {
        this.UpdateListItem(p_panel);
      }
    }

    #endregion

    #region Utils

    private void LoadListBox()
    {
      List<Tuple<string, UInt32>> l_charts = new List<Tuple<string, uint>>();

      m_list.Controls.Clear();
      m_list.DisplayMember = "Item1";
      m_list.ValueMember = "Item2";
      foreach (ChartPanel l_panel in ChartPanelModel.Instance.GetDictionary().SortedValues)
      {
        l_charts.Add(new Tuple<string, UInt32>(l_panel.Name, l_panel.Id));
      }
      for (Int32 i = l_charts.Count; i < MAX_PANEL; ++i)
      {
        l_charts.Add(new Tuple<string, UInt32>(Local.GetValue("CUI_Charts.new_panel"), ChartPanel.INVALID_ID));
      }
      m_list.DataSource = l_charts;
      m_list.SelectedIndex = 0;
    }

    private ListItem GetItem(UInt32 p_value)
    {
      for (Int32 i = 0; i < m_list.Items.Count; ++i)
      {
        if ((UInt32)m_list.Items[i].Value == p_value)
          return (m_list.Items[i]);
      }
      return (null);
    }

    private void UpdateListItem(ChartPanel p_panel)
    {
      ListItem l_item;

      if (p_panel == null)
        return;
      if ((l_item = this.GetItem(p_panel.Id)) == null) //If doesn't exists within the listbox
      {
        if ((l_item = m_list.SelectedItem) == null &&
          (l_item = this.GetItem(ChartPanel.INVALID_ID)) == null) //Get selectedItem, or first with null value
        {
          MessageBox.Show("general.error.system");
          return;
        }
        l_item.Value = p_panel.Id;
      }
      l_item.Text = p_panel.Name;
    }

    #endregion
  }
}
