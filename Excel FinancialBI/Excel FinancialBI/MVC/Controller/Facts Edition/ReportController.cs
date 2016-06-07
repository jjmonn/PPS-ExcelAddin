using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;

  class ReportController : AResultController<ReportView>
  {
    public void ShowView()
    {
      m_view.Show();
    }
  }
}
