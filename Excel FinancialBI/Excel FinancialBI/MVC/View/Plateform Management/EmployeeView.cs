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

  public partial class EmployeeView : AxisView
  {
    public EmployeeView() : base(Model.CRUD.AxisType.Employee)
    {
      
    }
  }
}
