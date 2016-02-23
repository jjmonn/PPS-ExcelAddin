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
    UInt32 m_versionId;

    public PeriodRangeSelectionControl(UInt32 p_versionId)
    {
      InitializeComponent();
      m_versionId = p_versionId;
    }

    public void LoadView()
    {
      MultilangueSetup();
      m_startDate.FormatValue = "MM-dd-yy";
      m_endDate.FormatValue = "MM-dd-yy";
      PeriodsRangeSetup(m_versionId);
      m_startDate.Validated += Date_ValueChanged;
      m_endDate.Validated += Date_ValueChanged;
    }

    private void MultilangueSetup()
    {
      m_startDateLabel.Text = Local.GetValue("submissionsFollowUp.start_date");
      m_endDateLabel.Text = Local.GetValue("submissionsFollowUp.end_date");
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as PeriodRangeSelectionController;
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

    public void PeriodsRangeSetup(UInt32 p_versionId)
    {
      DateTime l_startDate = m_controller.GetDefaultStartDate();
      DateTime l_endDate = m_controller.GetDefaultEndDate();

      m_startDate.DateTimeEditor.Value = l_startDate;
      m_endDate.DateTimeEditor.Value = l_endDate;

      FillWeeksTextbox();
    }

    public DateTime MinDate
    {
      set
      {
        m_startDate.MinDate = value;
        m_endDate.MinDate = value;
        m_startDate.DateTimeEditor.Value = m_controller.GetDefaultStartDate();
        m_endDate.DateTimeEditor.Value = m_controller.GetDefaultEndDate();
      }
    }

    public DateTime MaxDate
    {
      set
      {
        m_startDate.MaxDate = value;
        m_endDate.MaxDate = value;
        m_startDate.DateTimeEditor.Value = m_controller.GetDefaultStartDate();
        m_endDate.DateTimeEditor.Value = m_controller.GetDefaultEndDate();
      }
    }

    public List<Int32> GetPeriodList()
    {
      return m_controller.GetPeriodRange(m_startDate.DateTimeEditor.Value.Value, m_endDate.DateTimeEditor.Value.Value);
    }


  }
}
