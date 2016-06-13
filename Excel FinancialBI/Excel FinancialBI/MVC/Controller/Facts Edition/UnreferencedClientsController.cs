using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using MVC.Model;
  using MVC.Model.CRUD;
  using MVC.View;
  using Network;

  public delegate void ClientsCreated(bool p_success);

  class UnreferencedClientsController : IController
  {
    UnReferencedClientsUI m_view;
    public IView View { get { return m_view; } }
    List<string> m_unreferencedClients;
    public event ClientsCreated OnClientsCreated;
    public string Error { get; set; }

    public UnreferencedClientsController(List<string> p_unreferencedClients)
    {
      m_unreferencedClients = p_unreferencedClients;
      AxisElemModel.Instance.CreationEvent += AfterAxisElemCreation;
      LoadView();
    }

    public void LoadView()
    {
      m_view = new UnReferencedClientsUI(m_unreferencedClients);
      m_view.SetController(this);
      m_view.Show();
    }

    public void UnsusbribeEvent()
    {
      AxisElemModel.Instance.CreationEvent -= AfterAxisElemCreation;
    }

    public void CreateClients(List<string> p_clientsToBeCreated)
    {
      foreach (string l_newClient in p_clientsToBeCreated)
      {
        AxisElem l_newAxisElem = new AxisElem();
        l_newAxisElem.Axis = AxisType.Client;
        l_newAxisElem.Name = l_newClient;
        l_newAxisElem.ParentId = (UInt32)AxisType.Client;
        l_newAxisElem.AllowEdition = true;
        l_newAxisElem.ItemPosition = 1;

        AxisElemModel.Instance.Create(l_newAxisElem);
      }
    }

    delegate void AfterAxisElemCreation_delegate(ErrorMessage p_status, UInt32 p_id);
    private void AfterAxisElemCreation(ErrorMessage p_status, UInt32 p_id)
    {
      if (m_view.InvokeRequired)
      {
        AfterAxisElemCreation_delegate func = new AfterAxisElemCreation_delegate(AfterAxisElemCreation);
        m_view.Invoke(func, p_status, p_id);
        return;
      }
      if (p_status == ErrorMessage.SUCCESS)
      {
        AxisElem l_axisElem = AxisElemModel.Instance.GetValue(p_id);
        if (l_axisElem == null)
          return;

        if (l_axisElem.Axis != AxisType.Client)
          return;

        if (m_unreferencedClients.Contains(l_axisElem.Name))
          m_unreferencedClients.Remove(l_axisElem.Name);

        if (m_unreferencedClients.Count == 0)
        {
          OnClientsCreated(true);
          m_view.Hide();
          m_view.Dispose();
        }
      }
    }

  }
}
