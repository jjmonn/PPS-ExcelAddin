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

  public abstract partial class FactBaseView<TCrudType> : UserControl, IView
    where TCrudType : BaseVersion, NamedHierarchyCRUDEntity
  {
    protected FbiTreeView<TCrudType> m_versionTV;
    protected FbiDataGridView m_dgv;
    NamedCRUDModel<TCrudType> m_versionModel;

    public FactBaseView(NamedCRUDModel<TCrudType> p_versionModel)
    {
      InitializeComponent();

      m_versionModel = p_versionModel;
    }

    public virtual void LoadView()
    {
      m_dgv = new FbiDataGridView();
      m_versionTV = new FbiTreeView<TCrudType>(m_versionModel.GetDictionary());
      m_dgv.Dock = DockStyle.Fill;
      m_versionTV.Dock = DockStyle.Fill;
      m_mainContainer.Panel1.Controls.Add(m_versionTV);
      m_mainContainer.Panel2.Controls.Add(m_dgv);
      SuscribeEvents();
    }

    protected virtual void SuscribeEvents()
    {
      m_versionTV.ContextMenuStrip = m_versionMenu;
      m_versionTopMenu.SetContextMenuStrip(m_versionMenu, this);
    }

    public abstract void SetController(IController p_controller);
  }
}
