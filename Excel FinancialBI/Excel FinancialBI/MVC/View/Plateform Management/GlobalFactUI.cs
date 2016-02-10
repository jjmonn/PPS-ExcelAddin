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

  public partial class GlobalFactUI : UserControl, IView
  {
    public GlobalFactUI()
    {
      InitializeComponent();
      this.MultilangueSetup();
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
