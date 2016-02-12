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
  using Utils;
  using Forms;
  using Model;
  using Model.CRUD;

  public partial class CUI2LeftPane : UserControl, IView
  {

    #region Variables

    private CUILeftPaneController m_controller = null;

    private FbiTreeView<AxisElem> m_entitiesTV = null;

    #endregion

    #region Initialize

    public CUI2LeftPane()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {
      this.m_controller = p_controller as CUILeftPaneController;
    }

    public void InitView()
    {
      this.EntitiesTVInit();
      this.ComboBoxInit();
      this.MultilangueSetup();
    }

    private void ComboBoxInit()
    {
      
    }

    private void EntitiesTVInit()
    {
      this.m_entitiesTV = new FbiTreeView<AxisElem>(AxisElemModel.Instance.GetDictionary(AxisType.Entities));

      this.m_entitiesTV.CheckBoxes = true;
      this.m_entitiesTV.TriStateMode = true;
      this.m_entitiesTV.Dock = DockStyle.Fill;
      this.SplitContainer.Panel1.Controls.Add(this.m_entitiesTV);
    }

    private void MultilangueSetup()
    {
      this.SelectionCB.Text = Local.GetValue("CUI.selection");
      this.m_entitySelectionLabel.Text = Local.GetValue("CUI.entities_selection");
      this.SelectAllToolStripMenuItem.Text = Local.GetValue("CUI.select_all");
      this.UnselectAllToolStripMenuItem.Text = Local.GetValue("CUI.unselect_all");
    }

    #endregion

  }
}
