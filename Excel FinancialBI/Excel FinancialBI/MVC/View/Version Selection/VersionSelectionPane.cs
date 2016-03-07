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
      TableLayoutPanel1.Controls.Add(m_versionTV);
      m_versionTV.Dock = DockStyle.Fill;
      SuscribeEvents();
    }

    void SuscribeEvents()
    {
      VersionModel.Instance.ObjectInitialized += OnModelVersionList;
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
        m_versionTV = new FbiTreeView<Version>(VersionModel.Instance.GetDictionary(), null, true);
        m_versionTV.ImageList = m_versionsTreeviewImageList;
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
