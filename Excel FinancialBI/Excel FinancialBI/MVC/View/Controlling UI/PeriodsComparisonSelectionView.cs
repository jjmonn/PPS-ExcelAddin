using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIBlend.WinForms.Controls;

namespace FBI.MVC.View
{
  using Utils;
  using Controller;
  using Model;
  using Model.CRUD;

  public partial class PeriodsComparisonSelectionView : Form, IView
  {
    private CUIController m_controller;
    Version m_version1;
    Version m_version2;
    List<Int32> m_periodList1;
    List<Int32> m_periodList2;

    public PeriodsComparisonSelectionView()
    {
      InitializeComponent();
      MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      this.Text = "";
      m_groupbox1.Text = "";
      m_groupbox2.Text = "";
      m_period1Label.Text = "";
      m_period2Label.Text = "";
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as CUIController;
    }

    public void LoadView(Version p_version1, Version p_version2)
    {
      m_version1 = p_version1;
      m_version2 = p_version2;
      m_version1Label.Text = m_version1.Name;
      m_version2Label.Text = m_version2.Name;

      LoadPeriodCB(m_period1CB, m_periodList1 = PeriodModel.GetPeriodsList(p_version1.Id), m_version1.TimeConfiguration);
      LoadPeriodCB(m_period2CB, m_periodList2 = PeriodModel.GetPeriodsList(p_version2.Id), m_version2.TimeConfiguration);
    }

    public void LoadPeriodCB(vComboBox p_cb, List<Int32> p_periods, TimeConfig p_config)
    {
      p_cb.Items.Clear();
      foreach (Int32 l_period in p_periods)
      {
        ListItem l_item = new ListItem();

        l_item.Value = l_period;
        l_item.Text = PeriodModel.GetFormatedDate(l_period, p_config);
        p_cb.Items.Add(l_item);
        if (p_cb.SelectedItem == null)
          p_cb.SelectedItem = l_item;
      }
    }

    private void OnValidate(object p_sender, EventArgs p_e)
    {
      m_controller.PeriodDiff = true;
      SafeDictionary<TimeConfig, SafeDictionary<int, int>> l_periodDic = new SafeDictionary<TimeConfig, SafeDictionary<int, int>>();
      List<Int32> l_periodListA;
      List<Int32> l_periodListB;
      TimeConfig l_minBaseTimeConf =
        TimeUtils.IsParentConfig(m_version1.TimeConfiguration, m_version2.TimeConfiguration) ?
        m_version2.TimeConfiguration : m_version1.TimeConfiguration;

      if (m_period1CB.SelectedItem == null || m_period2CB.SelectedItem == null)
        return;
      if (!m_periodList1.Contains((Int32)m_period1CB.SelectedItem.Value) || !m_periodList2.Contains((Int32)m_period2CB.SelectedItem.Value))
        return;
      l_periodDic[m_version1.TimeConfiguration] = new SafeDictionary<int, int>();

      l_periodListA = PeriodModel.GetPeriodList((Int32)m_period1CB.SelectedItem.Value,
        m_periodList1.Count - GetPosInList(m_periodList1, (Int32)m_period1CB.SelectedItem.Value), m_version1.TimeConfiguration);
      l_periodListB = PeriodModel.GetPeriodList((Int32)m_period2CB.SelectedItem.Value,
       m_periodList2.Count - GetPosInList(m_periodList2, (Int32)m_period2CB.SelectedItem.Value), m_version2.TimeConfiguration);

      if (m_version1.TimeConfiguration == m_version2.TimeConfiguration)
        for (Int32 i = 0; i < l_periodListA.Count && i < l_periodListB.Count; ++i)
          l_periodDic[m_version1.TimeConfiguration][l_periodListA[i]] = l_periodListB[i];

      if (l_minBaseTimeConf != TimeUtils.GetParentConfig(l_minBaseTimeConf))
      {
        TimeConfig l_timeConf = TimeUtils.GetParentConfig(l_minBaseTimeConf);
        Int32 l_nbPeriod;
        
        if (l_periodListA.Count < l_periodListB.Count)
          l_nbPeriod = TimeUtils.GetParentConfigNbPeriods(m_version1.TimeConfiguration, l_periodListA.Count);
        else
          l_nbPeriod = TimeUtils.GetParentConfigNbPeriods(m_version2.TimeConfiguration, l_periodListB.Count);
        l_periodDic[l_timeConf] = new SafeDictionary<int, int>();

        l_periodListA = PeriodModel.GetPeriodList((Int32)m_period1CB.SelectedItem.Value,
          GetPosInList(m_periodList1, (Int32)m_period1CB.SelectedItem.Value) / l_nbPeriod, l_timeConf);
        l_periodListB = PeriodModel.GetPeriodList((Int32)m_period2CB.SelectedItem.Value,
          GetPosInList(m_periodList2, (Int32)m_period2CB.SelectedItem.Value) / l_nbPeriod, l_timeConf);

        for (Int32 i = 0; i < l_periodListA.Count && i < l_periodListB.Count; ++i)
          l_periodDic[l_timeConf][l_periodListA[i]] = l_periodListB[i];
      }
      m_controller.PeriodDiffAssociations = l_periodDic;
      m_controller.Compute();
      Hide();
    }

    public Int32 GetPosInList(List<Int32> p_list, Int32 p_value)
    {
      Int32 l_nb = 0;
      foreach (Int32 l_elem in p_list)
        if (l_elem == p_value)
          break;
        else
          l_nb++;
      return (l_nb);
    }
  }
}
