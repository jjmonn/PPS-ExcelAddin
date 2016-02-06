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
    IController m_controller;
    FbiTreeView<Version> m_versionsTreeview;
    private bool m_isClosing;
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
       //   m_exchangeRatesVersionVTreeviewbox.TreeView.AfterSelect += ExchangeRatesVTreebox_TextChanged;
       //   m_factsVersionVTreeviewbox.TreeView.AfterSelect += FactsRatesVTreebox_TextChanged;

      //this.DefineUIPermissions(); TODO : RightManager
      //this.DesactivateUnallowed(); TODO : RightManager  
      this.MultilanguageSetup();
    }

    private void MultilanguageSetup()
    {
      this.new_version_bt.Text = Local.GetValue("versions.add_version");
      this.new_folder_bt.Text = Local.GetValue("versions.add_folder");
      this.rename_bt.Text = Local.GetValue("general.rename");
      this.delete_bt.Text = Local.GetValue("general.delete");
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
      this.NewVersionMenuBT.Text = Local.GetValue("versions.add_version");
      this.NewFolderMenuBT.Text = Local.GetValue("versions.add_folder");
      this.DeleteVersionMenuBT.Text = Local.GetValue("general.delete");
      this.RenameMenuBT.Text = Local.GetValue("general.rename");
    }


    public void SetController(IController p_controller)
    {
      this.m_controller = p_controller;
    }



    #region View events

    public delegate void VersionsTreeview_AfterSelect_Delegate(object sender, VIBlend.WinForms.Controls.vTreeViewEventArgs e);
    private void VersionsTreeview_AfterSelect(object sender, VIBlend.WinForms.Controls.vTreeViewEventArgs e)
    {
      if (InvokeRequired)
      {
        VersionsTreeview_AfterSelect_Delegate MyDelegate = new VersionsTreeview_AfterSelect_Delegate(VersionsTreeview_AfterSelect);
        this.Invoke(MyDelegate, new object[] {sender,e});
      }
      else
      {
        //m_currentNode = e.Node;
        //m_isDisplaying = true;
        //Display(m_currentNode);
        //m_isDisplaying = false;
        //DesactivateUnallowed();
      }

    }

    private void VersionsTreeview_MouseClick(object sender, MouseEventArgs e)
    {
      m_currentNode = m_versionsTreeview.FindAtPosition(e.Location);
    }

    private void VersionsTreeview_KeyDown(object sender, KeyEventArgs e)
    {
      // priority high -> keydown sur le TV à ctacher !!
      switch (e.KeyCode)
      {
        case Keys.Delete:
         // TO DO delete_bt_Click(sender, e);
          break;
        case Keys.Up:
          if (e.Control)
          {
            if ((m_currentNode != null))
            {
            //  m_versionsTreeview.MoveNodeUp(m_currentNode);
            }
          }
          break;
        case Keys.Down:
          if (e.Control)
          {
            if ((m_currentNode != null))
            {
             // m_versionsTreeview.MoveNodeDown(m_currentNode);
            }
          }
          break;
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

  }
}

