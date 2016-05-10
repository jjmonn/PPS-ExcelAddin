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
  using Utils;
  using Controller;
  using Model;
  using Model.CRUD;

  public partial class PeriodsComparisonSelectionView : Form, IView
  {
    private PeriodsComparisonsSelectionController m_controller;

    public PeriodsComparisonSelectionView()
    {
      InitializeComponent();
      this.MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      this.Text = "";
      m_groupbox1.Text = "";
      m_groupbox2.Text = "";
      m_version1Label.Text = "";
      m_version2Label.Text = "";
      m_period1Label.Text = "";
      m_period2Label.Text = "";
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as PeriodsComparisonsSelectionController;
    }

     public void LoadView()
    {
      this.SuscribeEvents();
      this.LoadListBoxes();
    }

    private void SuscribeEvents()
    {
     // m_periodComboBox1.SelectedValueChanged += 
     // m_periodComboBox2.SelectedValueChanged +=
     // m_validateButton.Click += 
    }

    private void CloseView()
    {
      // m_periodComboBox1.SelectedValueChanged -= 
      // m_periodComboBox2.SelectedValueChanged -=
      // m_validateButton.Click -= 
    }

    #region Events

    // version selection 1 change : update m_periodComboBox1 list
    // version selection 2 change : update m_periodComboBox2 list    

    #endregion


    #region Utils

    private void LoadListBoxes()
    {
      // fill m_versionTreeView1
      // fill m_versionTreeView2
      // clean m_periodComboBox1
      // clean m_periodComboBox2
    }
   
#endregion


  }
}
