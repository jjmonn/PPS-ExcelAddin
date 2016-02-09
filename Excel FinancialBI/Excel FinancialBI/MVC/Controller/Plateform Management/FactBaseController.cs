using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBI.MVC.Controller
{
  using View;

  class FactBaseController<T> : NameController<T>
    where T : ContainerControl, IView
  {
    public override IView View { get { return (m_view); } }
    public UInt32 SelectedVersion { get; set; }
    NewVersionBaseView m_newVersionUI;

    public FactBaseController(NewVersionBaseView p_newVersionUI)
    {
      m_newVersionUI = p_newVersionUI;
    }

    public override void LoadView()
    {
      m_newVersionUI.LoadView();
    }

    public void SetNewVersionUIController(IController p_controller)
    {
      m_newVersionUI.SetController(p_controller);
    }

    public void ShowNewVersionUI()
    {
      m_newVersionUI.SelectedParent = SelectedVersion;
      m_newVersionUI.ShowDialog();
    }
  }
}
