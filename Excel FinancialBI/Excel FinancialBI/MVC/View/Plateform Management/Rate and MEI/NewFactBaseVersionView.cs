using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBI.MVC.View
{
  using Model;
  using Model.CRUD;
  using Controller;
  using Utils;

  partial class NewFactBaseVersionView<TController, TVersion>
    where TController : class, IFactBaseController<TVersion>
    where TVersion : BaseVersion, NamedCRUDEntity, new()
  {
    TController m_controller;
    public UInt32 SelectedParent { get; set; }

    public NewFactBaseVersionView()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as TController;
    }

    public void LoadView()
    {
      SuscribeEvents();
      MultiLangueSetup();
    }

    private void MultiLangueSetup()
    {
      this.Text = Local.GetValue("versions.new_version");
      m_nameLabel.Text = Local.GetValue("general.name");
      m_startingPeriodLabel.Text = Local.GetValue("facts_versions.starting_period");
      m_numberPeriodsLabel.Text = Local.GetValue("facts_versions.nb_years");
      CancelBT.Text = Local.GetValue("general.cancel");
      ValidateBT.Text = Local.GetValue("general.validate");
    }

    void SuscribeEvents()
    {
      ValidateBT.Click += OnValidate;
      CancelBT.Click += OnCancel;
      Addin.SuscribeAutoLock(this);
    }

    void OnValidate(object p_sender, EventArgs p_args)
    {
      TVersion l_newVersion = new TVersion();

      l_newVersion.StartPeriod = (UInt32)m_startPeriod.Value.GetValueOrDefault().ToOADate();
      l_newVersion.NbPeriod =(UInt16)m_nbPeriod.Value;
      l_newVersion.NbPeriod *= 12;
      l_newVersion.Name =  (string)NameTB.Text;
      l_newVersion.ParentId = SelectedParent;
      if (m_controller.CreateVersion(l_newVersion) == false)
        MessageBox.Show(m_controller.Error);
      Hide();
    }

    void OnCancel(object p_sender, EventArgs p_args)
    {
      Hide();
    }
  }
}
