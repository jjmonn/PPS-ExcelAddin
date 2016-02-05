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
  using Forms;
  using Model;
  using Controller;
  using Model.CRUD;

  public partial class AxisFiltersView : UserControl, IView
  {
    private AxisFilterController m_controller;
    private AxisType m_axisId;

    private FbiTreeView<FilterValue> m_tree;

    public AxisFiltersView()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as AxisFilterController;
    }

    /*
     * - Country          (Category)
     *   - City           (Category)
     *     - Cap-Breton   (Value)
     * The filterId is 'City'
     * The parentId is 'Country'
     * 
     */


  }
}
