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
  using Forms;
  using Model;
  using Model.CRUD;

  public partial class FactBaseView<T> : UserControl, IView
    where T : BaseVersion, NamedHierarchyCRUDEntity
  {
    protected FbiTreeView<T> m_versionTV;
    protected FbiDataGridView<string> m_dgv;
    NamedCRUDModel<T> m_versionModel;

    public FactBaseView(NamedCRUDModel<T> p_versionModel)
    {
      InitializeComponent();

      m_versionModel = p_versionModel;
      m_dgv = new FbiDataGridView<string>();
    }

    public void LoadView()
    {
      m_versionTV = new FbiTreeView<T>(m_versionModel.GetDictionary());
      m_versionTV.InitTVFormat();

      m_mainContainer.Panel1.Controls.Add(m_versionTV);
    }

    public void SetController(IController p_controller)
    {

    }
  }
}
