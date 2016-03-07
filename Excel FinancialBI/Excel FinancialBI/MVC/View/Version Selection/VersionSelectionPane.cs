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
  using Model.CRUD;
  using Model;
  using Network;

  public partial class VersionSelectionPane : AddinExpress.XL.ADXExcelTaskPane
  {
    public bool m_shown { set; get; }
    public FbiTreeView<Version> m_versionTV;

    public VersionSelectionPane()
    {
      InitializeComponent();
      this.MultilangueSetup();
      m_shown = false;
      m_versionTV = new FbiTreeView<Version>();
      SuscribeEvents();
    }

    void SuscribeEvents()
    {
      VersionModel.Instance.ObjectInitialized += OnModelVersionList;
      m_validateButton.Click += OnValidateButtonClick;
    }

    void OnValidateButtonClick(object p_sender, EventArgs p_e)
    {
      if (m_versionTV.SelectedNode != null)
      {
        Version l_version = VersionModel.Instance.GetValue((UInt32)m_versionTV.SelectedNode.Value);

          if (l_version == null)
            return;
        if (l_version.IsFolder)
        {
          MessageBox.Show(Local.GetValue("versions.error.cannot_select_folder"));
          return;
        }
        Addin.VersionId = l_version.Id;
        Hide();
      }
    }

    delegate void OnModelVersionList_delegate(ErrorMessage p_status, Type p_type);
    void OnModelVersionList(ErrorMessage p_status, Type p_type)
    {
      if (m_versionTV.InvokeRequired)
      {
        OnModelVersionList_delegate func = new OnModelVersionList_delegate(OnModelVersionList);
        Invoke(func, p_status, p_type);
      }
      else
      {
        TableLayoutPanel1.Controls.Remove(m_versionTV);
        m_versionTV = new FbiTreeView<Version>(VersionModel.Instance.GetDictionary(), null, true);
        m_versionTV.ImageList = m_versionsTreeviewImageList;
        TableLayoutPanel1.Controls.Add(m_versionTV, 0, 1);
        m_versionTV.Dock = DockStyle.Fill;
        m_versionTV.SelectedNode = m_versionTV.FindNode(Properties.Settings.Default.version_id);
      }
    }

    void MultilangueSetup()
    {
      m_versionSelectionLabel.Text = Local.GetValue("general.select_version");
      m_validateButton.Text = Local.GetValue("general.validate");
    }

    void VersionSelectionPane_ADXBeforeTaskPaneShow(object sender, AddinExpress.XL.ADXBeforeTaskPaneShowEventArgs e)
    {
      if (m_shown == false) { this.Visible = false; }
    }
  }
}
