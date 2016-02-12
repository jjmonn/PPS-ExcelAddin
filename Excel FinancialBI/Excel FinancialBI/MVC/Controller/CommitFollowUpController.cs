using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBI.MVC.Controller
{
  using View;
  using Model;
  using Model.CRUD;
  using Utils;

  public class CommitFollowUpController : IController
  {
    CommitFollowUpView m_view;

    public string Error { get; set; }
    public IView View { get { return (m_view); } }

    public CommitFollowUpController()
    {
      m_view = new CommitFollowUpView();
      m_view.SetController(this);
      LoadView();
    }

    public void LoadView()
    {
      m_view.LoadView();
    }

    public bool Update(Commit.Status p_status, UInt32 p_date, UInt32 p_entity)
    {
      Commit l_commit, l_tmp;

      if ((l_tmp = CommitModel.Instance.GetValue(p_entity, p_date)) == null) //If the commit does not exists
      {
        l_tmp = new Commit();
        l_tmp.Period = p_date;
        l_tmp.Value = (byte)p_status;
        l_tmp.EntityId = p_entity;
        CommitModel.Instance.Create(l_tmp);
        return (true);
      }
      if ((l_commit = l_tmp.Clone()) == null)
        return (false);
      l_commit.Value = (byte)p_status;
      CommitModel.Instance.Create(l_commit);
      return (true);
    }
  }
}
