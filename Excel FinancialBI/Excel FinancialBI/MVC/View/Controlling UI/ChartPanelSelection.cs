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
  using Network;

  public partial class ChartPanelSelection : Form, IView
  {
    private CUIVisualizationController m_controller;

    public ChartPanelSelection()
    {
      InitializeComponent();
      this.MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      m_text.ResetText();
      this.Text = Local.GetValue("CUI_Charts.panel_selection");
      m_new.Text = Local.GetValue("CUI_Charts.new_panel");
      m_next.Text = Local.GetValue("general.next");
      m_delete.Text = Local.GetValue("general.delete");
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
      m_new.Click += OnNewClick;
      m_delete.Click += OnDeleteClick;
      m_list.SelectedItemChanged += OnListChanged;
      this.FormClosed += OnClosed;

      ChartPanelModel.Instance.CreationEvent += OnCreatePanel;
      ChartPanelModel.Instance.UpdateEvent += OnUpdatePanel;
      ChartPanelModel.Instance.DeleteEvent += OnDeletePanel;
      ChartPanelModel.Instance.ReadEvent += OnReadPanel;
    }

    private void CloseView()
    {
      ChartPanelModel.Instance.CreationEvent -= OnCreatePanel;
      ChartPanelModel.Instance.UpdateEvent -= OnUpdatePanel;
      ChartPanelModel.Instance.DeleteEvent -= OnDeletePanel;
      ChartPanelModel.Instance.ReadEvent -= OnReadPanel;
    }

    #region Events

    private void OnClosed(object sender, FormClosedEventArgs e)
    {
      this.CloseView();
    }

    private void OnListChanged(object sender, EventArgs p_e)
    {
      this.SetListItemIndex(m_list.SelectedItem);
    }

    private void OnNewClick(object sender, EventArgs e)
    {
      ListItem l_item;

      l_item = m_list.Items.FirstOrDefault(x => x.Text == Local.GetValue("CUI_Charts.new_panel"));
      if (l_item == null)
      {
        string l_panelName = m_text.Text.Trim() == "" ? Local.GetValue("CUI_Charts.new_panel") : m_text.Text.Trim();
        m_controller.CRUPanel(ChartPanel.INVALID_ID, l_panelName);
      }
      else
      {
        m_list.SelectedItem = l_item;
        m_list.Refresh();
      }
    }

    private void OnNextClick(object sender, EventArgs e)
    {
      if (m_list.SelectedItem == null || (UInt32)m_list.SelectedItem.Value == ChartPanel.INVALID_ID)
        return;

      m_controller.CRUPanel((UInt32)m_list.SelectedItem.Value, m_text.Text.Trim());
      if (!m_controller.SetPanel((UInt32)m_list.SelectedItem.Value))
      {
        MessageBox.Show(Local.GetValue("CUI_Charts.error.same_panel"));
        return;
      }
      this.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.Close();
    }

    private void OnDeleteClick(object sender, EventArgs e)
    {
      if (m_list.SelectedItem == null || (UInt32)m_list.SelectedItem.Value == ChartPanel.INVALID_ID)
        return;

      if (!m_controller.DPanel((UInt32)m_list.SelectedItem.Value))
      {
        MessageBox.Show("general.error.system");
      }
    }

    private void OnCreatePanel(Network.ErrorMessage p_status, UInt32 p_id)
    {
      if (p_status != Network.ErrorMessage.SUCCESS)
      {
        MessageBox.Show(Local.GetValue("general.error.system") + " " + Network.Error.GetMessage(p_status));
      }
    }

    private void OnUpdatePanel(Network.ErrorMessage p_status, UInt32 p_id)
    {
      if (p_status != Network.ErrorMessage.SUCCESS)
      {
        MessageBox.Show(Local.GetValue("general.error.system") + " " + Network.Error.GetMessage(p_status));
      }
    }

    delegate void OnDeletePanel_delegate(ErrorMessage p_status, UInt32 p_id);
    private void OnDeletePanel(ErrorMessage p_status, UInt32 p_id)
    {
      if (InvokeRequired)
      {
        OnDeletePanel_delegate func = new OnDeletePanel_delegate(OnDeletePanel);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status != Network.ErrorMessage.SUCCESS)
        {
          MessageBox.Show(Local.GetValue("general.error.system") + " " + Network.Error.GetMessage(p_status));
        }
        else
        {
          this.DeleteListItem(p_id);
        }
      }
    }

    delegate void OnReadPanel_delegate(ErrorMessage p_status, ChartPanel p_panel);
    private void OnReadPanel(ErrorMessage p_status, ChartPanel p_panel)
    {
      if (InvokeRequired)
      {
        OnReadPanel_delegate func = new OnReadPanel_delegate(OnReadPanel);
        Invoke(func, p_status, p_panel);
      }
      else
      {
        if (p_status == Network.ErrorMessage.SUCCESS)
        {
          this.UpdateListItem(p_panel);
        }
      }
    }

    #endregion

    #region Utils

    private void SetListItemIndex(ListItem p_list)
    {
      if (p_list == null)
      {
        m_text.ResetText();
      }
      else
      {
        m_list.SelectedItem = p_list;
        m_text.Text = p_list.Text;
        m_list.Refresh();
      }
    }

    private void LoadListBox()
    {
      List<Tuple<string, UInt32>> l_panels = new List<Tuple<string, uint>>();

      m_list.Controls.Clear();
      m_list.DisplayMember = "Item1";
      m_list.ValueMember = "Item2";
      foreach (ChartPanel l_panel in ChartPanelModel.Instance.GetDictionary().SortedValues)
      {
        l_panels.Add(new Tuple<string, UInt32>(l_panel.Name, l_panel.Id));
      }
      m_list.DataSource = l_panels;
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

    private void DeleteListItem(UInt32 p_panelId)
    {
      ListItem l_item;

      if ((l_item = this.GetItem(p_panelId)) != null)
      {
        m_list.Items.Remove(l_item);
 
      }
    }

    private void UpdateListItem(ChartPanel p_panel)
    {
      ListItem l_item;

      if (p_panel == null)
        return;

      if ((l_item = this.GetItem(p_panel.Id)) == null) //If doesn't exists within the listbox
      {
        l_item = new ListItem();
        l_item.Value = p_panel.Id;
        m_list.Items.Add(l_item);
      }
      l_item.Text = p_panel.Name;
      this.SetListItemIndex(l_item);
    }

    #endregion
  }
}
