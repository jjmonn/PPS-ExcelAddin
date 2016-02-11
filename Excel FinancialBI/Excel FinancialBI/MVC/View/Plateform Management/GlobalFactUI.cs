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
  using FBI;
  using Utils;
  using Model.CRUD;

  public partial class GlobalFactUI : UserControl, IView
  {
    RightManager m_rightMgr = new RightManager();
    public GlobalFactUI()
    {
      InitializeComponent();
      this.MultilangueSetup();
      this.DefineUIPermissions();
    }

    private void DefineUIPermissions()
    {
      m_rightMgr[AddRatesVersionRCM] = Group.Permission.EDIT_BASE;
      m_rightMgr[DeleteVersionRCM] = Group.Permission.EDIT_BASE;
      m_rightMgr[AddFolderRCM] = Group.Permission.EDIT_BASE;
      m_rightMgr[ImportFromExcelBT] = Group.Permission.EDIT_GFACTS;
      m_rightMgr[RenameBT] = Group.Permission.EDIT_GFACTS;
      m_rightMgr[RenameVersionBT] = Group.Permission.EDIT_BASE;
      m_rightMgr[CopyFactDownToolStripMenuItem] = Group.Permission.EDIT_GFACTS;
      m_rightMgr[DeleteBT] = Group.Permission.DELETE_GFACTS;
      m_rightMgr[CreateNewFact] = Group.Permission.CREATE_GFACTS;
      m_rightMgr[CreateNewFact2] = Group.Permission.CREATE_GFACTS;
    }

    private void MultilangueSetup()
    {
      this.select_version.Text = Local.GetValue("global_facts.display_version");
      this.AddRatesVersionRCM.Text = Local.GetValue("versions.new_version");
      this.AddFolderRCM.Text = Local.GetValue("versions.new_folder");
      this.DeleteVersionRCM.Text = Local.GetValue("general.delete");
      this.RenameVersionBT.Text = Local.GetValue("general.rename");
      this.CopyFactDownToolStripMenuItem.Text = Local.GetValue("general.copy_down");
      this.ImportFromExcelBT.Text = Local.GetValue("currencies.import");
      this.CreateNewFact.Text = Local.GetValue("global_facts.new");
      this.VersionLabel.Text = Local.GetValue("general.version");
      this.RenameBT.Text = Local.GetValue("general.rename");
      this.DeleteBT.Text = Local.GetValue("general.delete");
      this.CreateNewFact2.Text = Local.GetValue("global_facts.new");
      this.m_importFromExcelBT2.Text = Local.GetValue("currencies.import");
    }

    public void SetController(IController p_controller)
    {

    }
  }
}
