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
  using FBI.MVC.Model;
  using Model.CRUD;
  using VIBlend.WinForms.Controls;
  using System.Windows.Forms;

  public partial class CopyVersionView : Form
  {
    VersionsController m_controller;
    private Version m_copiedVersion;
   
    public CopyVersionView()
    {
      InitializeComponent();
      MultilangueSetup();
      LoadView();
    }

    private void MultilangueSetup()
    {
      m_versionNameLabel.Text = Local.GetValue("facts_versions.version_name");
      m_startingPeriodLabel.Text = Local.GetValue("facts_versions.starting_period");
      m_nbPeriodsLabel.Text = Local.GetValue("facts_versions.nb_periods");
      m_CancelButton.Text = Local.GetValue("general.cancel");
      m_copyVersionButton.Text = Local.GetValue("general.copy");
      m_copiedVersionNameLabel.Text = Local.GetValue("facts_versions.copy_from");
      this.Text = Local.GetValue("facts_versions.version_copy_title");
    }

    public void SetController(IController p_controller)
    {
      this.m_controller = p_controller as VersionsController;
    }

    private void LoadView()
    {
      SuscribeEvents();
    }

    void SuscribeEvents()
    {
      this.m_CancelButton.Click += new System.EventHandler(this.m_CancelButton_Click);
      this.m_copyVersionButton.Click += new System.EventHandler(this.m_copyVersionButton_Click);
      Addin.SuscribeAutoLock(this);
    }

     public void SetCopiedVersion(Version p_copiedVersion)
     {
       m_copiedVersion = p_copiedVersion;
       m_copiedVersionName.Text = m_copiedVersion.Name;
       m_startingPeriodDatePicker.Value = DateTime.FromOADate(m_copiedVersion.StartPeriod);
       m_nbPeriods.Value = m_copiedVersion.NbPeriod;
     }

     private void m_copyVersionButton_Click(object sender, EventArgs e)
     {
       Version l_newVersion = m_copiedVersion.Clone();
       l_newVersion.CopyFrom(m_copiedVersion);
       l_newVersion.Name = m_versionNameTextbox.Text;
       l_newVersion.StartPeriod = (UInt32)m_startingPeriodDatePicker.Value.Value.ToOADate();
       l_newVersion.NbPeriod = (ushort)m_nbPeriods.Value;
       l_newVersion.Locked = false;
       l_newVersion.ItemPosition = l_newVersion.ItemPosition +1;
       l_newVersion.CreatedAt = DateTime.Now.ToShortDateString();
       if (m_controller.Create(l_newVersion) == false)
       {
         MessageBox.Show(m_controller.Error);
        return;
       }
       this.Hide();
     }

     private void m_CancelButton_Click(object sender, EventArgs e)
     {
       this.Hide();
     }

  }
}
