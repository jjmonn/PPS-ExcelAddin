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

  public abstract partial class NewVersionBaseView : Form, IView
  {
    public UInt32 SelectedParent { get; set;}

    public NewVersionBaseView()
    {
      InitializeComponent();
      MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      m_nameLabel.Text = Local.GetValue("general.name");
      m_startingPeriodLabel.Text = Local.GetValue("facts_versions.starting_period");
      m_numberPeriodsLabel.Text = Local.GetValue("facts_versions.nb_years");
      CancelBT.Text = Local.GetValue("general.cancel");
      ValidateBT.Text = Local.GetValue("general.create");
      this.Text = Local.GetValue("global_facts.new_version");
    }

    public abstract void SetController(IController p_controller);
    public abstract void LoadView();
  }
}
