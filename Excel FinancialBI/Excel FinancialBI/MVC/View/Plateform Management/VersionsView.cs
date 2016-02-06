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

  public partial class VersionsView : UserControl, IView
  {
    VersionsController m_controller;
    FbiTreeView<Version> m_versionsTreeview;
    private bool m_isClosing;
    private bool m_isDisplaying;
    private vTreeNode m_currentNode;

    public VersionsView()
    {
      InitializeComponent();
      this.InitView();
    }

    public void InitView()
    {
      m_versionsTreeview = new FbiTreeView<Version>(VersionModel.Instance.GetDictionary());
      m_versionsTreeview.ImageList = m_versionsTreeviewImageList;
      m_versionsTVPanel.Controls.Add(m_versionsTreeview);
      m_versionsTreeview.Dock = DockStyle.Fill;

      // Combobox Treeviews loading TO DO -> cf. function Loading TV Nath

      // Models events
      VersionModel.Instance.CreationEvent += AfterCreate;
      VersionModel.Instance.ReadEvent += AfterRead;
      VersionModel.Instance.UpdateEvent += AfterUpdate;
      VersionModel.Instance.CopyEvent += AfterCopy;
      VersionModel.Instance.DeleteEvent += AfterDelete;

      // View events
      m_versionsTreeview.AfterSelect += VersionsTreeview_AfterSelect;
      m_versionsTreeview.KeyDown += VersionsTreeview_KeyDown;
      m_versionsTreeview.MouseClick += VersionsTreeview_MouseClick;
      //   m_versionsTreeview.DragEnter += versionsTV_DragEnter;
      //   m_versionsTreeview.DragOver += versionsTV_DragOver;
      //   m_versionsTreeview.DragDrop += versionsTV_DragDrop;
      m_exchangeRatesVersionVTreeviewbox.TreeView.AfterSelect += ExchangeRatesVTreebox_TextChanged;
      m_factsVersionVTreeviewbox.TreeView.AfterSelect += FactsRatesVTreebox_TextChanged;

      // Main menu events
      this.m_newVersionMenuBT.Click += new System.EventHandler(this.m_newVersionMenuBT_Click);
      this.m_newFolderMenuBT.Click += new System.EventHandler(this.m_newFolderMenuBT_Click);
      this.m_renameMenuBT.Click += new System.EventHandler(this.m_renameMenuBT_Click);
      this.m_deleteVersionMenuBT.Click += new System.EventHandler(this.m_deleteVersionMenuBT_Click);

      // Right click menu events
      this.m_new_VersionRCMButton.Click += new System.EventHandler(this.m_new_VersionRCMButton_Click);
      this.m_copyVersionRCMButton.Click += new System.EventHandler(this.m_copyVersionRCMButton_Click);
      this.m_newFolderRCMButton.Click += new System.EventHandler(this.m_newFolderRCMButton_Click);
      this.m_renameRCMButton.Click += new System.EventHandler(this.m_renameRCMButton_Click);
      this.m_deleteRCMButton.Click += new System.EventHandler(this.m_deleteRCMButton_Click);


      //this.DefineUIPermissions(); TODO : RightManager
      //this.DesactivateUnallowed(); TODO : RightManager  
      this.MultilanguageSetup();
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
        lockedCB.Checked = true;
        LockedDateT.Text = p_version.LockDate;
      }
      else
      {
        lockedCB.Checked = false;
        LockedDateT.Text = Local.GetValue("facts_versions.version_not_locked");
      }
    }

    private void SetControlsEnabled(bool p_allowed)
    {
      lockedCB.Checked = p_allowed;
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

    #region View events

    public delegate void VersionsTreeview_AfterSelect_Delegate(object sender, VIBlend.WinForms.Controls.vTreeViewEventArgs e);
    private void VersionsTreeview_AfterSelect(object sender, VIBlend.WinForms.Controls.vTreeViewEventArgs e)
    {
      if (InvokeRequired)
      {
        VersionsTreeview_AfterSelect_Delegate MyDelegate = new VersionsTreeview_AfterSelect_Delegate(VersionsTreeview_AfterSelect);
        this.Invoke(MyDelegate, new object[] { sender, e });
      }
      else
      {
        m_currentNode = e.Node;
        m_isDisplaying = true;

        Version l_version = VersionModel.Instance.GetValue((uint)m_currentNode.Value);
        if (l_version == null) return;
        DisplayVersion(l_version);
        m_isDisplaying = false;

        // DesactivateUnallowed();
      }

    }

    private void VersionsTreeview_MouseClick(object sender, MouseEventArgs e)
    {
      m_currentNode = m_versionsTreeview.FindAtPosition(e.Location);
    }

    private void VersionsTreeview_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Control == true && m_currentNode != null)
      {
        switch (e.KeyCode)
        {
          case Keys.Up:
            {
              m_versionsTreeview.moveNodeUp(m_currentNode);
            }
            return;

          case Keys.Down:
            {
              m_versionsTreeview.moveNodeDown(m_currentNode);
            }
            return;
        }
      }
      if (e.KeyCode == Keys.Delete)
      {
        // m_controller.Delete();
      }
    }

    private void LockedCheckBox_CheckedChanged(object sender, EventArgs e)
    {

      if (m_isDisplaying == false)
      {
        if ((m_versionsTreeview.SelectedNode != null) && m_isDisplaying == false)
        {
          if (lockedCB.Checked)
          {
            // m_controller.LockVersion(m_versionsTreeview.SelectedNode.Value);
          }
          else
          {
            // m_controller.UnlockVersion(m_versionsTreeview.SelectedNode.Value);
          }
        }
      }
    }

    private void ExchangeRatesVTreebox_TextChanged(object sender, EventArgs e)
    {
      if (m_currentNode != null && m_isDisplaying == false)
      {
        Version l_version = VersionModel.Instance.GetValue((uint)m_currentNode.Value);
        if (l_version != null)
        {
          uint l_ratesVersionId = (uint)m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Value;

          // Time configuration validity control
          if (m_controller.IsRatesVersionCompatible(l_version, l_ratesVersionId))
          {
            Version l_versionCopy = l_version.Clone();
            l_versionCopy.RateVersionId = l_ratesVersionId;
            m_controller.Update(l_versionCopy);
          }
          else
          {
            MessageBox.Show(Local.GetValue("facts_versions.msg_rates_version_mismatch"));
            m_isDisplaying = true;
            SetExchangeVersion(l_version);
            m_isDisplaying = false;
          }
        }
      }
    }

    private void FactsRatesVTreebox_TextChanged(object sender, EventArgs e)
    {
      if (m_currentNode != null && m_isDisplaying == false)
      {
        Version l_version = VersionModel.Instance.GetValue((uint)m_currentNode.Value);
        if (l_version != null)
        {
          uint l_globalFactsVersionId = (uint)m_factsVersionVTreeviewbox.TreeView.SelectedNode.Value;

          // Time configuration validity control
          if (m_controller.IsGlobalFactsVersionCompatible(l_version, l_globalFactsVersionId))
          {
            Version l_versionCopy = l_version.Clone();
            l_versionCopy.GlobalFactVersionId = l_globalFactsVersionId;
            m_controller.Update(l_versionCopy);
          }
          else
          {
            MessageBox.Show(Local.GetValue("facts_versions.msg_rates_version_mismatch"));
            m_isDisplaying = true;
            SetGlobalFactsVersion(l_version);
            m_isDisplaying = false;
          }
        }
      }
    }


    #endregion


    #region Model events

    private void AfterRead(ErrorMessage status, CRUDEntity p_version)
    {
      if (status == ErrorMessage.SUCCESS && m_isClosing == false)
      {
        // TO DO
      }
    }

    private void AfterCreate(ErrorMessage status, UInt32 id)
    {
      string l_message = Local.GetValue("facts_versions.msg_error_create") + ": ";

      switch (status)
      {
        case ErrorMessage.SUCCESS:
          return;

        case ErrorMessage.INVALID_ATTRIBUTE:
          l_message = Local.GetValue("facts_versions.msg_invalid_attribute");
          break;

        case ErrorMessage.SYSTEM:
          l_message = Local.GetValue("facts_versions.msg_system");
          break;

        default:
          l_message = Local.GetValue("facts_versions.msg_unknown");
          break;
      }
      MessageBox.Show(l_message);
    }

    private void AfterDelete(ErrorMessage status, UInt32 id)
    {
      if (status == ErrorMessage.SUCCESS && m_isClosing == false)
      {
        // TO DO
      }

      string l_message = Local.GetValue("facts_versions.msg_error_delete") + ": ";

      switch (status)
      {
        case ErrorMessage.SUCCESS:
          return;

        case ErrorMessage.NOT_FOUND:
          l_message += Local.GetValue("facts_versions.msg_not_found");
          break;

        case ErrorMessage.SYSTEM:
          l_message += Local.GetValue("facts_versions.msg_system");
          break;

        default:
          l_message += Local.GetValue("facts_versions.msg_unknown");
          break;
      }
      MessageBox.Show(l_message);
    }

    private void AfterUpdate(ErrorMessage status, UInt32 id)
    {
      string l_message = Local.GetValue("facts_versions.msg_error_update") + ": ";

      switch (status)
      {
        case ErrorMessage.SUCCESS:
          return;

        case ErrorMessage.NOT_FOUND:
          l_message += Local.GetValue("facts_versions.msg_not_found");
          break;

        default:
          l_message += Local.GetValue("facts_versions.msg_unknown");
          break;
      }
      MessageBox.Show(l_message);
    }

    private void AfterCopy(ErrorMessage status, UInt32 id)
    {
      string l_message = Local.GetValue("facts_versions.msg_error_copy") + ": ";

      switch (status)
      {
        case ErrorMessage.SUCCESS:
          return;

        case ErrorMessage.NOT_FOUND:
          l_message += Local.GetValue("facts_versions.msg_not_found");
          break;
        case ErrorMessage.SYSTEM:
          l_message += Local.GetValue("facts_versions.msg_system");
          break;
        default:
          l_message += Local.GetValue("facts_versions.msg_unknown");
          break;
      }
      MessageBox.Show(l_message);
    }

    #endregion


    #region Main menu call back

    private void m_newVersionMenuBT_Click(object sender, EventArgs e)
    {
      m_controller.ShowNewVersionView();
    }

    private void m_newFolderMenuBT_Click(object sender, EventArgs e)
    {
      CreateFolder();
    }

    private void m_renameMenuBT_Click(object sender, EventArgs e)
    {
      RenameVersion(m_currentNode);
    }

    private void m_deleteVersionMenuBT_Click(object sender, EventArgs e)
    {
      DeleteVersion(m_currentNode);
    }

    #endregion

    #region ""Right click menu call backs

    private void m_new_VersionRCMButton_Click(object sender, EventArgs e)
    {
      m_controller.ShowNewVersionView();
    }

    private void m_copyVersionRCMButton_Click(object sender, EventArgs e)
    {
      m_controller.ShowVersionCopyView();
    }

    private void m_newFolderRCMButton_Click(object sender, EventArgs e)
    {
      CreateFolder();
    }

    private void m_renameRCMButton_Click(object sender, EventArgs e)
    {
      RenameVersion(m_currentNode);
    }

    private void m_deleteRCMButton_Click(object sender, EventArgs e)
    {
      DeleteVersion(m_currentNode);
    }

    #endregion

    private void CreateFolder()
    {
      Version l_newFolderVersion = new Version();
      string l_name = Microsoft.VisualBasic.Interaction.InputBox(Local.GetValue("versions.msg_folder_name"));
      l_newFolderVersion.Name = l_name;
      l_newFolderVersion.IsFolder = true;
      if (m_controller.Create(l_newFolderVersion) == false)
        MessageBox.Show(m_controller.Error);
    }

    private void DeleteVersion(vTreeNode p_node)
    {
      if (p_node != null)
      {
        Version l_version = VersionModel.Instance.GetValue((uint)m_currentNode.Value);
        if (l_version != null)
        {
          if (m_controller.Delete(l_version) == false)
            MessageBox.Show(m_controller.Error);
        }
       }
    }

    private void RenameVersion(vTreeNode p_node)
    {
      if (p_node != null)
      {
        Version l_version = VersionModel.Instance.GetValue((uint)m_currentNode.Value).Clone();
        if (l_version != null)
        {
          string l_name = Microsoft.VisualBasic.Interaction.InputBox(Local.GetValue("versions.msg_new_name"));
          l_version.Name = l_name;
          if (m_controller.Update(l_version) == false)
            MessageBox.Show(m_controller.Error);
        }
      }
    }


  }
}




