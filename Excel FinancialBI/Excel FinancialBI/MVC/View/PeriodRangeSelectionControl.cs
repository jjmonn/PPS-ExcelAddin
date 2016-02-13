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
  using Model;


  public partial class PeriodRangeSelectionControl : UserControl, IView
  {
    PeriodRangeSelectionController m_controller;

    public PeriodRangeSelectionControl(UInt32 p_versionId)
    {
      InitializeComponent();
      m_controller = new PeriodRangeSelectionController(p_versionId);
      MultilangueSetup();
      m_startDate.FormatValue = "MM-dd-yy";
      m_endDate.FormatValue = "MM-dd-yy";
      PeriodsRangeSetup(p_versionId);
      m_startDate.ValueChanged += Date_ValueChanged;
      m_endDate.ValueChanged += Date_ValueChanged;
    }

    private void MultilangueSetup()
    {
      m_startDateLabel.Text = Local.GetValue("submissionsFollowUp.start_date");
      m_endDateLabel.Text = Local.GetValue("submissionsFollowUp.end_date");
    }

    public void SetController(IController p_controller)
    {

    }

    private void Date_ValueChanged(object sender, EventArgs e)
    {
      FillWeeksTextbox();
    }

    private void FillWeeksTextbox()
    {
      m_startWeekTB.Text = PeriodModel.GetDateAsStringWeekFormat(m_startDate.DateTimeEditor.Value.Value);
      m_endWeekTB.Text = PeriodModel.GetDateAsStringWeekFormat(m_endDate.DateTimeEditor.Value.Value);
    }

    private void PeriodsRangeSetup(UInt32 p_versionId)
    {
      m_startDate.MinDate = m_controller.GetMinDate();
      m_startDate.MaxDate = m_controller.GetMaxDate();
      m_endDate.MinDate = m_controller.GetMinDate();
      m_endDate.MaxDate = m_controller.GetMaxDate();

      DateTime l_startDate = m_controller.GetStartDate();
      DateTime l_endDate = m_controller.GetEndDate();

      m_startDate.Text = l_startDate.ToShortDateString();
      m_endDate.Text = l_endDate.ToShortDateString();

      m_startDate.DateTimeEditor.Value = l_startDate;
      m_endDate.DateTimeEditor.Value = l_endDate;

      FillWeeksTextbox();
    }

    public List<UInt32> GetPeriodList()
    {
      return m_controller.GetPeriodRange(m_startDate.DateTimeEditor.Value.Value, m_endDate.DateTimeEditor.Value.Value);
    }


  }
}
