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

  public partial class CUI2RightPane : UserControl, IView
  {

    #region Variables

    private CUIRightPaneController m_controller = null;

    #endregion

    #region Initialize

    public CUI2RightPane()
    {
      InitializeComponent();
      MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      this.m_columnsLabel.Text = Local.GetValue("CUI.columns_label");
      this.m_rowsLabel.Text = Local.GetValue("CUI.rows_label");
      this.UpdateBT.Text = Local.GetValue("CUI.refresh");
      this.m_fieldChoiceLabel.Text = Local.GetValue("CUI.fields_choice");
    }

    public void SetController(IController p_controller)
    {
      this.m_controller = p_controller as CUIRightPaneController;
    }

    #endregion

  }
}
