using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using VIBlend.WinForms.Controls;
using VIBlend.WinForms.DataGridView;

namespace FBI.MVC.View
{
  using Controller;
  using Forms;
  using Model;
  using Model.CRUD;
  using Utils;
  using Network;

  public abstract partial class FactBaseView<TVersion, TControllerType> : UserControl, IView
    where TVersion : BaseVersion, NamedHierarchyCRUDEntity, new()
    where TControllerType : IFactBaseController<TVersion>
  {
    protected FbiTreeView<TVersion> m_versionTV;
    protected FbiDataGridView m_dgv;
    NamedCRUDModel<TVersion> m_versionModel;

    protected TControllerType m_controller;
    protected IExcelImportController m_excelImportController;
    
    RightManager m_rightMgr = new RightManager();

    public FactBaseView(NamedCRUDModel<TVersion> p_versionModel)
    {
      InitializeComponent();

      m_versionModel = p_versionModel;
    }

    public virtual void LoadView()
    {
      m_dgv = new FbiDataGridView();
      m_versionTV = new FbiTreeView<TVersion>(m_versionModel.GetDictionary());
      m_versionTV.ImageList = m_versionsTreeviewImageList;
      m_dgv.Dock = DockStyle.Fill;
      m_versionTV.Dock = DockStyle.Fill;
      m_mainContainer.Panel1.Controls.Add(m_versionTV);
      m_mainContainer.Panel2.Controls.Add(m_dgv);
      MultilanguageSetup();
      DefineUIPermissions();
      DesactivateUnallowed();
    }

    private void DefineUIPermissions()
    {
      m_rightMgr[m_addRatesVersionRCM] = Group.Permission.EDIT_BASE;
      m_rightMgr[m_deleteVersionRCM] = Group.Permission.EDIT_BASE;
      m_rightMgr[m_addFolderRCM] = Group.Permission.EDIT_BASE;
      m_rightMgr[m_renameGFact] = Group.Permission.EDIT_GFACTS;
      m_rightMgr[m_deleteGFact] = Group.Permission.EDIT_GFACTS;
      m_rightMgr[m_newGFact] = Group.Permission.EDIT_GFACTS;
      m_rightMgr[m_importExcelMenu] = Group.Permission.EDIT_RATES;
      m_rightMgr[m_renameBT] = Group.Permission.EDIT_RATES;
      m_rightMgr[m_copyValueDown] = Group.Permission.EDIT_RATES;
    }

    private void DesactivateUnallowed()
    {
      m_rightMgr.Enable(UserModel.Instance.GetCurrentUserRights());
    }

    private void MultilanguageSetup()
    {
      m_versionTopMenu.Text = Local.GetValue("general.versions");
      m_importExcelMenu.Text = Local.GetValue("upload.upload");
      m_importExcelRightClick.Text = Local.GetValue("upload.upload");
      VersionLabel.Text = Local.GetValue("facts_versions.fact_version");
      select_version.Text = Local.GetValue("versions.select_version");
      m_addRatesVersionRCM.Text = Local.GetValue("versions.new_version");
      m_addFolderRCM.Text = Local.GetValue("versions.new_folder");
      m_deleteVersionRCM.Text = Local.GetValue("general.delete");
      m_renameBT.Text = Local.GetValue("general.rename");
      m_copyValueDown.Text = Local.GetValue("general.copy_down");
    }

    protected virtual void SuscribeEvents()
    {
      m_versionTV.ContextMenuStrip = m_versionMenu;
      m_versionTopMenu.SetContextMenuStrip(m_versionMenu, this);
      m_versionTV.MouseClick += OnMouseClick;
      m_renameBT.Click += OnRenameVersionClick;
      m_addFolderRCM.Click += OnCreateFolderClick;
      m_addRatesVersionRCM.Click += OnCreateVersionClick;
      m_deleteVersionRCM.Click += OnDeleteVersionClick;
      m_versionTV.NodeMouseDown += OnNodeSelect;

      m_importExcelMenu.Click += OnImportExcel;
      m_importExcelRightClick.Click += OnImportExcel;

      m_versionModel.ReadEvent += OnModelReadVersion;
      m_versionModel.UpdateEvent += OnModelUpdateVersion;
      m_versionModel.CreationEvent += OnModelCreateVersion;
      m_versionModel.DeleteEvent += OnModelDeleteVersion;
      Addin.SuscribeAutoLock(this);
    }

    #region User Callback

    private void OnImportExcel(object sender, EventArgs e)
    {
      m_excelImportController.LoadView(m_controller.SelectedVersion);
    }

    void OnNodeSelect(object p_sender, vTreeViewMouseEventArgs p_event)
    {
      m_controller.SelectedVersion = (UInt32)p_event.Node.Value;
      TVersion l_version = m_versionModel.GetValue(m_controller.SelectedVersion);

      if (l_version == null || l_version.IsFolder)
        m_dgv.Clear();
      else
        DisplayVersion(l_version.Id);
    }

    void OnMouseClick(object p_sender, MouseEventArgs p_event)
    {
      if (m_versionTV.FindAtPosition(new Point(p_event.Location.X, p_event.Location.Y)) == null)
      {
        m_versionTV.SelectedNode = null;
        if (m_controller != null)
          m_controller.SelectedVersion = 0;
      }
    }

    void OnCreateFolderClick(object p_sender, EventArgs p_args)
    {
      string l_result = Interaction.InputBox(Local.GetValue("version.create_folder"));

      if (l_result == "")
        return;
      TVersion l_version = new TVersion();

      l_version.ParentId = m_controller.SelectedVersion;
      l_version.IsFolder = true;
      l_version.Name = l_result;
      if (m_controller.CreateVersion(l_version) == false)
        MessageBox.Show(m_controller.Error);
    }

    void OnRenameVersionClick(object p_sender, EventArgs p_args)
    {
      string l_result = Interaction.InputBox(Local.GetValue("version.rename"));

      if (l_result == "")
        return;
      TVersion l_version = m_versionModel.GetValue(m_controller.SelectedVersion);
      if (l_version == null)
        return;
      l_version = l_version.BaseClone() as TVersion;
      l_version.Name = l_result;
      if (m_controller.UpdateVersion(l_version) == false)
        MessageBox.Show(m_controller.Error);
    }

    void OnCreateVersionClick(object p_sender, EventArgs p_args)
    {
      m_controller.ShowNewVersionUI();
    }

    void OnDeleteVersionClick(object p_sender, EventArgs p_args)
    {
      m_controller.DeleteVersion(m_controller.SelectedVersion);
    }

    #endregion

    #region Model Callback

    delegate void OnModelReadVersion_delegate(ErrorMessage p_status, TVersion p_version);
    void OnModelReadVersion(ErrorMessage p_status, TVersion p_version)
    {
      if (m_dgv.InvokeRequired)
      {
        OnModelReadVersion_delegate func = new OnModelReadVersion_delegate(OnModelReadVersion);
        Invoke(func, p_status, p_version);
      }
      else
      {
        m_versionTV.FindAndAdd(p_version);
        DesactivateUnallowed();
      }
    }

    void OnModelUpdateVersion(ErrorMessage p_status, UInt32 p_id)
    {
      if (p_status != ErrorMessage.SUCCESS)
        MessageBox.Show(Error.GetMessage(p_status));
      DesactivateUnallowed();
    }

    void OnModelCreateVersion(ErrorMessage p_status, UInt32 p_id)
    {
      if (p_status != ErrorMessage.SUCCESS)
        MessageBox.Show(Error.GetMessage(p_status));
      DesactivateUnallowed();
    }

    delegate void OnModelDeleteVersion_delegate(ErrorMessage p_status, UInt32 p_id);
    void OnModelDeleteVersion(ErrorMessage p_status, UInt32 p_id)
    {
      if (m_dgv.InvokeRequired)
      {
        OnModelDeleteVersion_delegate func = new OnModelDeleteVersion_delegate(OnModelDeleteVersion);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status != ErrorMessage.SUCCESS)
          MessageBox.Show(Error.GetMessage(p_status));
        else
        {
          m_versionTV.FindAndRemove(p_id);
          m_versionTV.Refresh();
        }
      }
    }

    #endregion

    public abstract void SetController(IController p_controller);
    protected abstract void DisplayVersion(UInt32 p_versionId);
  }
}
