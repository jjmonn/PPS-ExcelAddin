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
  using FBI.MVC.Controller;
  using FBI.MVC.Model;
  using FBI.MVC.Model.CRUD;
  using FBI;
  using Utils;
  using VIBlend.WinForms.Controls;
  using FBI.MVC.View;

  public partial class RHSnapshotLaunchView : Form, IView
  {
    RHSnapshotLaunchController m_controller;
    PeriodRangeSelectionController m_periodRangeSelectionController;


    public RHSnapshotLaunchView()
    {
      InitializeComponent();
      MultilangueSelection();
      SubsribeEvents();  
    }

    private void SubsribeEvents()
    {
      this.m_validateButton.Click += new System.EventHandler(this.m_validateButton_Click);
    }

    private void MultilangueSelection()
    {
      this.Text = Local.GetValue("upload.periods_selection");
      m_validateButton.Text = Local.GetValue("general.validate");
      m_accountSelectionLabel.Text = Local.GetValue("upload.accounts_selection");
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as RHSnapshotLaunchController;
    }

    public void LoadView(UInt32 p_versionId)
    {
      LoadPeriodsSelectionControl(p_versionId);
      ReportEditionSidePane.InitRHAccountsCombobox(m_accountSelectionComboBox);
    }

    private void LoadPeriodsSelectionControl(UInt32 p_versionId)
    {
      m_periodRangeSelectionController = new PeriodRangeSelectionController(p_versionId);
      PeriodRangeSelectionControl l_periodRangeSelectionView = m_periodRangeSelectionController.View as PeriodRangeSelectionControl;
      m_periodSelectionPanel.Controls.Add(l_periodRangeSelectionView);
      l_periodRangeSelectionView.Dock = DockStyle.Fill;
    }

    private void m_validateButton_Click(object sender, EventArgs e)
    {
      if (m_controller.LaunchSnapshot(m_periodRangeSelectionController.GetPeriodList(), (UInt32)m_accountSelectionComboBox.SelectedItem.Value) == false)
        Forms.MsgBox.Show(m_controller.Error);
    }
  
  }
}
