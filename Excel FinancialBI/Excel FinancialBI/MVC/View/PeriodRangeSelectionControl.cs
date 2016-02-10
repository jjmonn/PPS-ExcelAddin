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

  public partial class PeriodRangeSelectionControl : UserControl, IView
  {
    public PeriodRangeSelectionControl()
    {
      InitializeComponent();
      MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      m_startDateLabel.Text = Local.GetValue("submissionsFollowUp.start_date");
      m_endDateLabel.Text = Local.GetValue("submissionsFollowUp.end_date");
    }

    public void SetController(IController p_controller)
    {

    }
  }
}
