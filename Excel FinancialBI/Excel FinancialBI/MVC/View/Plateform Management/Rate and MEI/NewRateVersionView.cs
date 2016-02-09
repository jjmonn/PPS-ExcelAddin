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

  class NewRateVersionView : NewVersionBaseView
  {
    ExchangeRatesController m_controller;

    public override void SetController(IController p_controller)
    {
      m_controller = p_controller as ExchangeRatesController;
    }

    public override void LoadView()
    {
      SuscribeEvents();
    }

    void SuscribeEvents()
    {
      ValidateBT.Click += OnValidate;
      CancelBT.Click += OnCancel;
    }

    void OnValidate(object p_sender, EventArgs p_args)
    {
      ExchangeRateVersion l_newVersion = new ExchangeRateVersion();

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
