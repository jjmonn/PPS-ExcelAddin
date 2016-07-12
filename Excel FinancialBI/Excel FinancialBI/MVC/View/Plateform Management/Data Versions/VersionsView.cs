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
    SafeDictionary<Int32, ListItem> m_periodListItems = new SafeDictionary<int, ListItem>();
    vTreeNode m_currentNode = null;
    protected SafeDictionary<UInt32, Int32> m_updatedVersionPos = new SafeDictionary<uint, int>();

    public VersionsView()
    {
      InitializeComponent();
      LoadView();
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

      AFbiTreeView.InitTVFormat(m_exchangeRatesVersionVTreeviewbox.TreeView);
      AFbiTreeView.InitTVFormat(m_factsVersionVTreeviewbox.TreeView);
      DefineUIPermissions(); 
      DesactivateUnallowed();  
      MultilanguageSetup();
      SuscribeEvents();
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

      m_versionsTreeview.MouseDown += VersionsTV_MouseDown;
      m_versionsTreeview.NodeDropped += VersionsTV_NodeDropped;

      m_exchangeRatesVersionVTreeviewbox.TreeView.AfterSelect += OnChangeVersionExchangeRate;
      m_factsVersionVTreeviewbox.TreeView.AfterSelect += OnChangeVersionFactRate;
      m_formulaPeriodIndexCB.SelectedItemChanged += OnSelectedPeriodIndexChanged;
      m_nbFormulaPeriodCB.SelectedItemChanged += OnSelectedNbFormulaPeriodChanged;

      // Main menu events
      m_newVersionMenuBT.Click += new System.EventHandler(OnClickNewVersion);
      m_newFolderMenuBT.Click += new System.EventHandler(OnClickNewFolder);
      m_renameMenuBT.Click += new System.EventHandler(OnClickRename);
      m_deleteVersionMenuBT.Click += new System.EventHandler(OnClickDelete);

      // Right click menu events
      m_new_VersionRCMButton.Click += new System.EventHandler(OnClickNewVersion);
      m_copyVersionRCMButton.Click += new System.EventHandler(OnClickCopyVersion);
      m_newFolderRCMButton.Click += new System.EventHandler(OnClickNewFolder);
      m_renameRCMButton.Click += new System.EventHandler(OnClickRename);
      m_deleteRCMButton.Click += new System.EventHandler(OnClickDelete);

      Addin.SuscribeAutoLock(this);
    }

    public void CloseView()
    {
      VersionModel.Instance.CreationEvent -= AfterCreate;
      VersionModel.Instance.ReadEvent -= AfterRead;
      VersionModel.Instance.UpdateEvent -= AfterUpdate;
      VersionModel.Instance.CopyEvent -= AfterCopy;
      VersionModel.Instance.DeleteEvent -= AfterDelete;
      SendUpdatePosition();
    }

    private void MultilanguageSetup()
    {
      m_new_VersionRCMButton.Text = Local.GetValue("versions.add_version");
      m_newFolderRCMButton.Text = Local.GetValue("versions.add_folder");
      m_renameRCMButton.Text = Local.GetValue("general.rename");
      m_deleteRCMButton.Text = Local.GetValue("general.delete");
      m_globalFactsVersionLabel.Text = Local.GetValue("facts_versions.global_facts_version");
      m_exchangeRatesVersionLabel.Text = Local.GetValue("facts_versions.exchange_rates_version");
      m_numberOfYearsLabel.Text = Local.GetValue("facts_versions.nb_periods");
      m_startingPeriodLabel.Text = Local.GetValue("facts_versions.starting_period");
      m_periodConfigLabel.Text = Local.GetValue("facts_versions.period_config");
      m_nameLabel.Text = Local.GetValue("facts_versions.version_name");
      m_lockedLabel.Text = Local.GetValue("facts_versions.version_locked");
      m_lockedDateLabel.Text = Local.GetValue("facts_versions.locked_date");
      m_creationDateLabel.Text = Local.GetValue("facts_versions.creation_date");
      VersionsToolStripMenuItem.Text = Local.GetValue("general.versions");
      m_newVersionMenuBT.Text = Local.GetValue("versions.add_version");
      m_newFolderMenuBT.Text = Local.GetValue("versions.add_folder");
      m_copyVersionRCMButton.Text = Local.GetValue("versions.copy_version");
      m_deleteVersionMenuBT.Text = Local.GetValue("general.delete");
      m_renameMenuBT.Text = Local.GetValue("general.rename");
      m_formulaPeriodLabel.Text = Local.GetValue("versions.formula_period_range");
      m_formulaPeriodIndexLabel.Text = Local.GetValue("facts_versions.starting_period");
      m_nbFormulaPeriodLabel.Text = Local.GetValue("facts_versions.end_period");
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as VersionsController;
    }

    public void LoadPeriods(Version p_version)
    {
      bool l_isDisplaying = m_isDisplaying;
      m_isDisplaying = true;
      m_periodListItems.Clear();
      m_formulaPeriodIndexCB.Items.Clear();
      m_nbFormulaPeriodCB.Items.Clear();

      List<Int32> l_periodList = PeriodModel.GetPeriodList((Int32)p_version.StartPeriod, (Int32)p_version.NbPeriod, p_version.TimeConfiguration);

      int l_index = 0;
      foreach (Int32 l_period in l_periodList)
      {
        m_periodListItems[l_period] = m_formulaPeriodIndexCB.Items.Add(PeriodModel.GetFormatedDate(l_period, p_version.TimeConfiguration));
        if (l_index == p_version.FormulaPeriodIndex)
          m_formulaPeriodIndexCB.SelectedItem = m_periodListItems[l_period];
        l_index++;
      }
      l_index = 0;
      uint l_dest = (p_version.FormulaPeriodIndex + p_version.FormulaNbPeriod - 1);
      foreach (ListItem l_item in m_periodListItems.Values)
      {
        m_nbFormulaPeriodCB.Items.Add(l_item);
        if (l_index == l_dest)
          m_nbFormulaPeriodCB.SelectedItem = l_item;
        l_index++;
      }
      m_isDisplaying = l_isDisplaying;
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
      LoadPeriods(p_version);

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
      m_lockCombobox.Enabled = p_allowed;
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

    void MoveVersion(vTreeNode p_node1, vTreeNode p_node2)
     {
       if (p_node1 == null || p_node2 == null)
         return;
       Version l_version = VersionModel.Instance.GetValue((UInt32)p_node1.Value);
       Version l_version2 = VersionModel.Instance.GetValue((UInt32)p_node2.Value);
 
       if (l_version != null && l_version2 != null)
       {
         Int32 l_prevPos = (m_updatedVersionPos.ContainsKey(l_version.Id)) ? m_updatedVersionPos[l_version.Id] : l_version.ItemPosition;
         Int32 l_prevPos2 = (m_updatedVersionPos.ContainsKey(l_version2.Id)) ? m_updatedVersionPos[l_version2.Id] : l_version2.ItemPosition;
 
         m_updatedVersionPos[l_version.Id] = l_prevPos2;
         m_updatedVersionPos[l_version2.Id] = l_prevPos;
       }
     }
 
    private void OnSelectedPeriodIndexChanged(object p_sender, EventArgs p_e)
    {
      if (m_isDisplaying)
        return;
      Version l_version = VersionModel.Instance.GetValue(m_controller.SelectedVersion);

      if (l_version == null)
        return;
      l_version = l_version.Clone();
      uint l_index = 0;

      foreach (ListItem l_item in m_periodListItems.Values)
      {
        if (l_item == m_formulaPeriodIndexCB.SelectedItem)
          break;
        l_index++;
      }
      l_version.FormulaNbPeriod += l_version.FormulaPeriodIndex - l_index;
      l_version.FormulaPeriodIndex = l_index;
      if (m_controller.Update(l_version) == false)
        MessageBox.Show(m_controller.Error);
    }

    private void OnSelectedNbFormulaPeriodChanged(object p_sender, EventArgs p_e)
    {
      if (m_isDisplaying)
        return;
      Version l_version = VersionModel.Instance.GetValue(m_controller.SelectedVersion);

      if (l_version == null)
        return;
      l_version = l_version.Clone();
      uint l_index = 1;

      foreach (ListItem l_item in m_periodListItems.Values)
      {
        if (l_item == m_nbFormulaPeriodCB.SelectedItem)
          break;
        l_index++;
      }
      l_version.FormulaNbPeriod = l_index - l_version.FormulaPeriodIndex;
      if (m_controller.Update(l_version) == false)
      {
        MessageBox.Show(m_controller.Error);
        LoadPeriods(l_version);
      }
    }

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
        Invoke(MyDelegate, new object[] { sender, e });
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
        Invoke(MyDelegate, new object[] { p_id });
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
        Invoke(MyDelegate, new object[] { p_version });
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
          case Keys.Down:
            if (e.Control == true)
            {
              m_currentNode = m_versionsTreeview.SelectedNode;
              if (m_currentNode != null)
              {
                MoveVersion(m_currentNode, m_currentNode.NextSiblingNode);
                m_versionsTreeview.MoveNodeDown(m_currentNode);
                m_versionsTreeview.SelectedNode = m_currentNode;
              }
            }
            break;
          case Keys.Up:
            if (e.Control == true)
            {
              m_currentNode = m_versionsTreeview.SelectedNode;
              if (m_currentNode != null)
              {
                MoveVersion(m_currentNode, m_currentNode.PrevSiblingNode);
                m_versionsTreeview.MoveNodeUp(m_currentNode);
                m_versionsTreeview.SelectedNode = m_currentNode;
              }
            }
            break;
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
        
      if (p_draggedNode.Equals(p_targetNode) == true || l_targetVersion.IsFolder == false || (p_draggedNode.Parent != null && p_draggedNode.Parent.Equals(p_targetNode)))
        return;

      Version l_version = VersionModel.Instance.GetValue((UInt32)p_draggedNode.Value).Clone();
      if (l_version == null)
        return;

      m_versionsTreeview.DoDragDrop(p_draggedNode, DragDropEffects.None);
      l_version.ParentId = (UInt32)p_targetNode.Value;
      if (m_controller.Update(l_version) == false)
        MsgBox.Show(m_controller.Error);
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
      l_newFolderVersion.FormulaPeriodIndex = 0;
      l_newFolderVersion.FormulaNbPeriod = 1;
      l_newFolderVersion.NbPeriod = 1;

      if (m_controller.Create(l_newFolderVersion) == false)
        MsgBox.Show(m_controller.Error);
    }

    private void DeleteVersion(UInt32 p_versionId)
    {
      string l_password = PasswordBox.Open(Local.GetValue("versions.warning_delete"));

      if (l_password == PasswordBox.Canceled || Addin.Password != l_password)
        return;
      if (m_controller.Delete(p_versionId) == false)
        MsgBox.Show(m_controller.Error);
    }

    private void RenameVersion(UInt32 p_versionId)
    {

      Version l_version = VersionModel.Instance.GetValue(p_versionId);

      if (l_version != null)
      {
        l_version = l_version.Clone();
        l_version.Name = Interaction.InputBox(Local.GetValue("versions.msg_new_name"));
        if (m_controller.Update(l_version) == false)
          MsgBox.Show(m_controller.Error);
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
        MsgBox.Show(m_controller.Error);
    }

    void SendUpdatePosition()
    {
      List<Version> l_updatedVersionList = new List<Version>();

      foreach (KeyValuePair<UInt32, Int32> l_pair in m_updatedVersionPos)
      {
        Version l_version = VersionModel.Instance.GetValue(l_pair.Key);

        if (l_version == null || l_version.ItemPosition == l_pair.Value)
          continue;
        l_version = l_version.Clone();
        l_version.ItemPosition = l_pair.Value;
        l_updatedVersionList.Add(l_version);
      }
      if (l_updatedVersionList.Count > 0)
        if (m_controller.UpdateVersionList(l_updatedVersionList) == false)
          MsgBox.Show(m_controller.Error);
      }
  
  }
}




