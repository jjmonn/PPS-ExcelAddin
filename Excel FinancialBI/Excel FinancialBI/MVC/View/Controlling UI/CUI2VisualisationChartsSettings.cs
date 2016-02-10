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

  public partial class CUI2VisualisationChartsSettings : Form, IView
  {
    public CUI2VisualisationChartsSettings()
    {
      InitializeComponent();
      MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      this.m_chartTitleLabel.Text = Local.GetValue("CUI_Charts.chart_title");
      this.m_chartSerie2Label.Text = Local.GetValue("CUI_Charts.serie_2");
      this.m_chartSerie1Label.Text = Local.GetValue("CUI_Charts.serie_1");
      this.m_AccountLabel.Text = Local.GetValue("general.account");
      this.m_ColorLabel.Text = Local.GetValue("general.couleur");
      this.m_typeLabel.Text = Local.GetValue("general.type");
      this.m_saveButton.Text = Local.GetValue("general.save");
      this.Text = Local.GetValue("CUI_Charts.charts_settings");
    }

    public void SetController(IController p_controller)
    {

    }
  }
}
