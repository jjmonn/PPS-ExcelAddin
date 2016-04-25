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
  using Forms;
  using Model;
  using Model.CRUD;
  using FBI.Network;
  using Microsoft.VisualBasic;

  public partial class VersionsView : UserControl, IPlatformMgtView
  {
    private VersionsController m_controller;
    private FbiTreeView<Version> m_versionsTreeview;
    private RightManager m_rightMgr = new RightManager();
    private bool m_isDisplaying;

    public VersionsView()
    {
      InitializeComponent();
      this.LoadView();
    }

    public void LoadView()
    {
      m_versionsTreeview = new FbiTreeView<Version>(VersionModel.Instance.GetDictionary(), null, true);
      m_versionsTreeview.ImageList = m_versionsTreeviewImageList;
      m_versionsTVPanel.Controls.Add(m_versionsTreeview);
      m_versionsTreeview.Dock = DockStyle.Fill;
      m_versionsTreeview.ContextMenuStrip = m_versionsRightClickMenu;

      FbiTreeView<ExchangeRateVersion>.Load(m_exchangeRatesVersionVTreeviewbox.TreeView.Nodes, RatesVersionModel.Instance.GetDictionary());
      FbiTreeView<GlobalFactVersion>.Load(m_factsVersionVTreeviewbox.TreeView.Nodes, GlobalFactVersionModel.Instance.GetDictionary());

      this.DefineUIPermissions(); 
      this.DesactivateUnallowed();  
      this.MultilanguageSetup();
      this.SuscribeEvents();
    }

    private void DefineUIPermissions()
    {
      m_rightMgr[m_lockCombobox] = Group.Permission.EDIT_BASE;
      m_rightMgr[m_exchangeRatesVersionVTreeviewbox] = Group.Permission.EDIT_BASE;
      m_rightMgr[m_factsVersionVTreeviewbox] = Group.Permission.EDIT_BASE;
      m_rightMgr[m_newFolderMenuBT] = Group.Permission.EDIT_BASE;
      m_rightMgr[m_newVersionMenuBT] = Group.Permission.EDIT_BASE;
      m_rightMgr[m_renameMenuBT] = Group.Permission.EDIT_BASE;
      m_rightMgr[m_deleteVersionMenuBT] = Group.Permission.EDIT_BASE;
      m_rightMgr[m_new_VersionRCMButton] = Group.Permission.EDIT_BASE;
      m_rightMgr[m_newFolderRCMButton] = Group.Permission.EDIT_BASE;
      m_rightMgr[m_deleteRCMButton] = Group.Permission.EDIT_BASE;
      m_rightMgr[m_deleteRCMButton] = Group.Permission.EDIT_BASE;
      m_rightMgr[m_renameRCMButton] = Group.Permission.EDIT_BASE;
      m_rightMgr[m_copyVersionRCMButton] = Group.Permission.EDIT_BASE;
    }

    private void DesactivateUnallowed()
    {
      m_rightMgr.Enable(UserModel.Instance.GetCurrentUserRights());
    }

    void SuscribeEvents()
    {
      // Models events
      VersionModel.Instance.CreationEvent += AfterCreate;
      VersionModel.Instance.ReadEvent += AfterRead;
      VersionModel.Instance.UpdateEvent += AfterUpdate;
      VersionModel.Instance.CopyEvent += AfterCopy;
      VersionModel.Instance.DeleteEvent += AfterDelete;

      // View events
      m_versionsTreeview.NodeMouseDown += OnVersionTVSelectNode;
      m_versionsTreeview.KeyDown += OnVersionTVKeyDown;
      m_versionsTreeview.MouseClick += OnVersionTVMouseClick;
      m_lockCombobox.CheckedChanged += OnChangeVersionLock;

      m_versionsTreeview.MouseDown += this.VersionsTV_MouseDown;
      m_versionsTreeview.NodeDropped += this.VersionsTV_NodeDropped;

      m_exchangeRatesVersionVTreeviewbox.TreeView.AfterSelect += OnChangeVersionExchangeRate;
      m_factsVersionVTreeviewbox.TreeView.AfterSelect += OnChangeVersionFactRate;

      // Main menu events
      this.m_newVersionMenuBT.Click += new System.EventHandler(this.OnClickNewVersion);
      this.m_newFolderMenuBT.Click += new System.EventHandler(this.OnClickNewFolder);
      this.m_renameMenuBT.Click += new System.EventHandler(this.OnClickRename);
      this.m_deleteVersionMenuBT.Click += new System.EventHandler(this.OnClickDelete);

      // Right click menu events
      this.m_new_VersionRCMButton.Click += new System.EventHandler(this.OnClickNewVersion);
      this.m_copyVersionRCMButton.Click += new System.EventHandler(this.OnClickCopyVersion);
      this.m_newFolderRCMButton.Click += new System.EventHandler(this.OnClickNewFolder);
      this.m_renameRCMButton.Click += new System.EventHandler(this.OnClickRename);
      this.m_deleteRCMButton.Click += new System.EventHandler(this.OnClickDelete);

      Addin.SuscribeAutoLock(this);
    }

    public void CloseView()
    {
      VersionModel.Instance.CreationEvent -= AfterCreate;
      VersionModel.Instance.ReadEvent -= AfterRead;
      VersionModel.Instance.UpdateEvent -= AfterUpdate;
      VersionModel.Instance.CopyEvent -= AfterCopy;
      VersionModel.Instance.DeleteEvent -= AfterDelete;
    }

    private void MultilanguageSetup()
    {
      this.m_new_VersionRCMButton.Text = Local.GetValue("versions.add_version");
      this.m_newFolderRCMButton.Text = Local.GetValue("versions.add_folder");
      this.m_renameRCMButton.Text = Local.GetValue("general.rename");
      this.m_deleteRCMButton.Text = Local.GetValue("general.delete");
      this.m_globalFactsVersionLabel.Text = Local.GetValue("facts_versions.global_facts_version");
      this.m_exchangeRatesVersionLabel.Text = Local.GetValue("facts_versions.exchange_rates_version");
      this.m_numberOfYearsLabel.Text = Local.GetValue("facts_versions.nb_periods");
      this.m_startingPeriodLabel.Text = Local.GetValue("facts_versions.starting_period");
      this.m_periodConfigLabel.Text = Local.GetValue("facts_versions.period_config");
      this.m_nameLabel.Text = Local.GetValue("facts_versions.version_name");
      this.m_lockedLabel.Text = Local.GetValue("facts_versions.version_locked");
      this.m_lockedDateLabel.Text = Local.GetValue("facts_versions.locked_date");
      this.m_creationDateLabel.Text = Local.GetValue("facts_versions.creation_date");
      this.VersionsToolStripMenuItem.Text = Local.GetValue("general.versions");
      this.m_newVersionMenuBT.Text = Local.GetValue("versions.add_version");
      this.m_newFolderMenuBT.Text = Local.GetValue("versions.add_folder");
      this.m_copyVersionRCMButton.Text = Local.GetValue("versions.copy_version");
      this.m_deleteVersionMenuBT.Text = Local.GetValue("general.delete");
      this.m_renameMenuBT.Text = Local.GetValue("general.rename");
    }

    public void SetController(IController p_controller)
    {
      this.m_controller = p_controller as VersionsController;
    }

    #region Display version attributes

    private void DisplayVersion(Version p_version)
    {
      m_isDisplaying = true;
      if (p_version.IsFolder)
      {
        DisplayFolderVersion();
        return;
      }
      SetControlsEnabled(true);
      m_nameTextbox.Text = p_version.Name;
      m_CreationDateTextbox.Text = p_version.CreatedAt;
      m_startPeriodTextbox.Text = SetStartPeriodAndPeriodConfig(p_version);
      m_nbPeriodsTextbox.Text = p_version.NbPeriod.ToString();
      SetGlobalFactsVersion(p_version);
      SetExchangeVersion(p_version);

      if (p_version.Locked)
      {
        m_lockCombobox.Checked = true;
        LockedDateT.Text = p_version.LockDate;
      }
      else
      {
        m_lockCombobox.Checked = false;
        LockedDateT.Text = Local.GetValue("facts_versions.version_not_locked");
      }
      m_isDisplaying = false;
    }

    private void SetControlsEnabled(bool p_allowed)
    {
      m_lockCombobox.Checked = p_allowed;
      m_exchangeRatesVersionVTreeviewbox.Enabled = p_allowed;
      m_factsVersionVTreeviewbox.Enabled = p_allowed;
    }

    private string SetStartPeriodAndPeriodConfig(Version p_version)
    {
      string startPeriod;
      switch (p_version.TimeConfiguration)
      {
        case TimeConfig.YEARS:
          m_timeConfigTB.Text = Local.GetValue("period.timeconfig.year");
          startPeriod = DateTime.FromOADate(p_version.StartPeriod).ToString("yyyy");
          break;
        case TimeConfig.MONTHS:
          m_timeConfigTB.Text = Local.GetValue("period.timeconfig.month");
          startPeriod = DateTime.FromOADate(p_version.StartPeriod).ToString("MMM yyyy");
          break;
        case TimeConfig.WEEK:
          m_timeConfigTB.Text = Local.GetValue("period.timeconfig.week");
          startPeriod = DateTime.FromOADate(p_version.StartPeriod).ToString("WW");
          break;
        case TimeConfig.DAYS:
          m_timeConfigTB.Text = Local.GetValue("period.timeconfig.day");
          startPeriod = DateTime.FromOADate(p_version.StartPeriod).ToString("MMMM dd, yyyy");
          break;
        default:
          startPeriod = "";
          break;
      }
      return startPeriod;
    }

    private void SetGlobalFactsVersion(Version p_version)
    {
      vTreeNode activeFactsRatesNode = TreeviewUtils.FindNode(m_factsVersionVTreeviewbox.TreeView, p_version.GlobalFactVersionId);
      TreeviewUtils.TreeviewboxNodeSelection(m_factsVersionVTreeviewbox, activeFactsRatesNode);
    }

    private void SetExchangeVersion(Version p_version)
    {
      vTreeNode activeExchangeRatesNode = TreeviewUtils.FindNode(m_exchangeRatesVersionVTreeviewbox.TreeView, p_version.RateVersionId);
      TreeviewUtils.TreeviewboxNodeSelection(m_exchangeRatesVersionVTreeviewbox, activeExchangeRatesNode);
    }

    private void DisplayFolderVersion()
    {
      m_nameTextbox.Text = "";
      m_CreationDateTextbox.Text = "";
      LockedDateT.Text = "";
      m_timeConfigTB.Text = "";
      m_startPeriodTextbox.Text = "";
      m_nbPeriodsTextbox.Text = "";
      m_exchangeRatesVersionVTreeviewbox.Text = "";
      m_factsVersionVTreeviewbox.Text = "";
      SetControlsEnabled(false);
    }

    #endregion

    #region Model events

    private void AfterRead(ErrorMessage p_status, CRUDEntity p_version)
    {
      if (p_status == ErrorMessage.SUCCESS)
      {
        UpdateNode(p_version as Version);
      }
    }

    private void AfterCreate(ErrorMessage p_status, UInt32 p_id)
    {
      if (p_status != ErrorMessage.SUCCESS)
        DisplayErrorMessage(Local.GetValue("facts_versions.error.create"), p_status);
    }

    private void AfterDelete(ErrorMessage p_status, UInt32 p_id)
    {
      if (p_status == ErrorMessage.SUCCESS)
      {
        DeleteNode(p_id);
      }
      else
        DisplayErrorMessage(Local.GetValue("facts_versions.error.delete"), p_status);
    }

    private void AfterUpdate(ErrorMessage p_status, UInt32 p_id)
    {
      if (p_status != ErrorMessage.SUCCESS)
        DisplayErrorMessage(Local.GetValue("facts_versions.error.update"), p_status);
    }

    private void AfterCopy(ErrorMessage p_status, UInt32 p_id)
    {
      if (p_status != ErrorMessage.SUCCESS)
        DisplayErrorMessage(Local.GetValue("facts_versions.error.copy"), p_status);
    }

    void DisplayErrorMessage(string p_message, ErrorMessage p_status)
    {
      p_message += ": " + Error.GetMessage(p_status);
      Forms.MsgBox.Show(p_message);
    }

    #endregion

    #region User Callback

    private void OnClickNewVersion(object sender, EventArgs e)
    {
      CreateVersion();
    }

    private void OnClickCopyVersion(object sender, EventArgs e)
    {
      if (m_controller.SelectedVersion != 0)
        m_controller.ShowVersionCopyView(m_controller.SelectedVersion);
    }

    private void OnClickNewFolder(object sender, EventArgs e)
    {
      CreateFolder();
    }

    private void OnClickRename(object sender, EventArgs e)
    {
      RenameVersion(m_controller.SelectedVersion);
    }

    private void OnClickDelete(object sender, EventArgs e)
    {
      DeleteVersion(m_controller.SelectedVersion);
    }

    public delegate void VersionsTreeview_AfterSelect_Delegate(object sender, vTreeViewMouseEventArgs e);
    private void OnVersionTVSelectNode(object sender, vTreeViewMouseEventArgs e)
    {
      if (InvokeRequired)
      {
        VersionsTreeview_AfterSelect_Delegate MyDelegate = new VersionsTreeview_AfterSelect_Delegate(OnVersionTVSelectNode);
        this.Invoke(MyDelegate, new object[] { sender, e });
      }
      else
      {
        m_controller.SelectedVersion = (UInt32)e.Node.Value;
        m_isDisplaying = true;

        Version l_version = VersionModel.Instance.GetValue(m_controller.SelectedVersion);
        if (l_version == null) return;
        DisplayVersion(l_version);
        m_isDisplaying = false;
        DesactivateUnallowed();
      }

    }

    private delegate void DeleteNode_Delegate(UInt32 p_id);
    private void DeleteNode(UInt32 p_id)
    {
      if (InvokeRequired)
      {
        DeleteNode_Delegate MyDelegate = new DeleteNode_Delegate(DeleteNode);
        this.Invoke(MyDelegate, new object[] { p_id });
      }
      else
      {
        vTreeNode l_node = m_versionsTreeview.FindNode(p_id);
        if (l_node == null)
          return;
        l_node.Remove();
        m_versionsTreeview.Refresh();
      }
    }

    private delegate void UpdateNode_Delegate(Version p_version);
    private void UpdateNode(Version p_version)
    {
      if (InvokeRequired)
      {
        UpdateNode_Delegate MyDelegate = new UpdateNode_Delegate(UpdateNode);
        this.Invoke(MyDelegate, new object[] { p_version });
      }
      else
        m_versionsTreeview.FindAndAdd(p_version);
    }

    #endregion

    #region Events

    private void OnVersionTVMouseClick(object sender, MouseEventArgs e)
    {
      vTreeNode l_node = m_versionsTreeview.FindAtPosition(e.Location);
      if (l_node == null)
      {
        m_versionsTreeview.SelectedNode = null;
        m_controller.SelectedVersion = 0;
      }
    }

    private void OnVersionTVKeyDown(object sender, KeyEventArgs e)
    {
      if (e.Control == true && m_versionsTreeview.SelectedNode != null)
      {
        switch (e.KeyCode)
        {
          case Keys.Up:
            m_versionsTreeview.MoveNodeUp(m_versionsTreeview.SelectedNode);
            return;

          case Keys.Down:
            m_versionsTreeview.MoveNodeDown(m_versionsTreeview.SelectedNode);
            return;
        }
      }
      if (e.KeyCode == Keys.Delete)
        DeleteVersion(m_controller.SelectedVersion);
    }

    private void OnChangeVersionLock(object sender, EventArgs e)
    {
      if (m_isDisplaying == false)
      {
        UpdateLocked(m_controller.SelectedVersion, m_lockCombobox.Checked);
      }
    }

    private void OnChangeVersionExchangeRate(object sender, EventArgs e)
    {
      if (m_isDisplaying == false)
      {
        Version l_version = VersionModel.Instance.GetValue(m_controller.SelectedVersion);
        if (l_version != null && m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode != null)
        {
          UInt32 l_ratesVersionId = (UInt32)m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Value;

          Version l_versionCopy = l_version.Clone();
          l_versionCopy.RateVersionId = l_ratesVersionId;
          if (m_controller.Update(l_versionCopy) == false)
          {
            Forms.MsgBox.Show(m_controller.Error);
            m_isDisplaying = true;
            SetExchangeVersion(l_version);
            m_isDisplaying = false;
          }
        }
      }
    }

    private void OnChangeVersionFactRate(object sender, EventArgs e)
    {
      if (m_isDisplaying == false)
      {
        Version l_version = VersionModel.Instance.GetValue(m_controller.SelectedVersion);
        if (l_version != null && m_factsVersionVTreeviewbox.TreeView.SelectedNode != null)
        {
          UInt32 l_gfactsVersionId = (UInt32)m_factsVersionVTreeviewbox.TreeView.SelectedNode.Value;

          Version l_versionCopy = l_version.Clone();
          l_versionCopy.GlobalFactVersionId = l_gfactsVersionId;
          if (m_controller.Update(l_versionCopy) == false)
          {
            Forms.MsgBox.Show(m_controller.Error);
            m_isDisplaying = true;
            SetGlobalFactsVersion(l_version);
            m_isDisplaying = false;
          }
        }
      }
    }

    private void VersionsTV_MouseDown(object sender, MouseEventArgs e)
    {
      vTreeNode l_node;

      l_node = m_versionsTreeview.FindAtPosition(new Point(e.X, e.Y));
      if (l_node != null && ModifierKeys.HasFlag(Keys.Control) == true)
      {
        m_versionsTreeview.DoDragDrop(l_node, DragDropEffects.Move);
      }
    }

    private void VersionsTV_NodeDropped(vTreeNode p_draggedNode, vTreeNode p_targetNode)
    {
      if (p_targetNode == null)
      return;

      Version l_targetVersion = VersionModel.Instance.GetValue((UInt32)p_targetNode.Value);
        
      if (p_draggedNode.Equals(p_targetNode) == true || l_targetVersion.IsFolder == false || p_draggedNode.Parent.Equals(p_targetNode.Value))
        return;

      Version l_version = VersionModel.Instance.GetValue((UInt32)p_draggedNode.Value).Clone();
      if (l_version == null)
        return;

      vTreeNode l_newNode = new vTreeNode();
      l_newNode.Value = p_draggedNode.Value;
      l_newNode.Text = p_draggedNode.Text;
      l_newNode.ImageIndex = p_draggedNode.ImageIndex;
      p_draggedNode.Remove();
      m_versionsTreeview.DoDragDrop(p_draggedNode, DragDropEffects.None);
      l_version.ParentId = (UInt32)p_targetNode.Value;
      m_controller.Update(l_version);
    }
   
    #endregion

    private void CreateVersion()
    {
      m_controller.ShowNewVersionView(m_controller.SelectedVersion);
    }

    private void CreateFolder()
    {
      Version l_newFolderVersion = new Version();
      l_newFolderVersion.Name = Interaction.InputBox(Local.GetValue("versions.msg_folder_name"));
      l_newFolderVersion.IsFolder = true;
      l_newFolderVersion.ParentId = m_controller.SelectedVersion;
      if (m_controller.Create(l_newFolderVersion) == false)
        Forms.MsgBox.Show(m_controller.Error);
    }

    private void DeleteVersion(UInt32 p_versionId)
    {
      string l_password = PasswordBox.Open(Local.GetValue("versions.warning_delete"));

      if (l_password == PasswordBox.Canceled || Addin.Password != l_password)
        return;
      if (m_controller.Delete(p_versionId) == false)
        Forms.MsgBox.Show(m_controller.Error);
    }

    private void RenameVersion(UInt32 p_versionId)
    {

      Version l_version = VersionModel.Instance.GetValue(p_versionId);

      if (l_version != null)
      {
        l_version = l_version.Clone();
        l_version.Name = Interaction.InputBox(Local.GetValue("versions.msg_new_name"));
        if (m_controller.Update(l_version) == false)
          Forms.MsgBox.Show(m_controller.Error);
      }
    }

    private void UpdateLocked(UInt32 p_versionId, bool p_locked)
    {
      Version l_version = VersionModel.Instance.GetValue(p_versionId);

      if (l_version == null)
        return;
      l_version = l_version.Clone();
      l_version.Locked = p_locked;
      if (p_locked == true)
        l_version.LockDate = DateTime.Now.ToShortDateString();
      else
        l_version.LockDate = "";
      if (m_controller.Update(l_version) == false)
        Forms.MsgBox.Show(m_controller.Error);
    }

  }
}




