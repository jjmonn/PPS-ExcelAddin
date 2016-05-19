using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;
  using Model.CRUD;
  using Model;

  class CUILeftPaneController : IController
  {

    #region Variables

    private CUIController m_parentController;
    private CUI2LeftPane m_view;
    public PeriodRangeSelectionController PeriodController { get; set; }
    public IView View { get { return (m_view); } }
    public string Error { get; set; }

    #endregion

    #region Initialize

    public CUILeftPaneController(CUIController p_parentController)
    {
      m_parentController = p_parentController;
      PeriodController = new PeriodRangeSelectionController(Properties.Settings.Default.version_id);
      m_view = new CUI2LeftPane();
      m_view.SetController(this);
      LoadView();
    }

    private void LoadView()
    {
      m_view.LoadView();
    }

    public bool PeriodDiff
    {
      get { return (m_parentController.PeriodDiff); }
    }

    public SafeDictionary<TimeConfig, SafeDictionary<Int32, Int32>> PeriodDiffAssociations
    {
      get { return (m_parentController.PeriodDiffAssociations); }
    }

    public void ReloadPeriods()
    {
      m_view.ReloadPeriods();
    }

    public UInt32 GetCurrency()
    {
      SafeDictionary<Type, List<UInt32>> l_dic = m_view.GetCheckedElements();

      if (l_dic[typeof(Currency)] == null)
        return (0);
      return (l_dic[typeof(Currency)][0]);
    }

    public List<UInt32> GetVersions()
    {
      SafeDictionary<Type, List<UInt32>> l_dic = m_view.GetCheckedElements();
      List<UInt32> l_list = new List<uint>();

      if (l_dic[typeof(Version)] == null)
        return (null);
      foreach (UInt32 l_versionId in l_dic[typeof(Version)])
      {
        Version l_version = VersionModel.Instance.GetValue(l_versionId);

        if (l_version != null && !l_version.IsFolder)
          l_list.Add(l_versionId);
      }
      return (l_list);
    }

    public List<Int32> GetPeriods()
    {
      SafeDictionary<Type, List<UInt32>> l_dic = m_view.GetCheckedElements();
      List<Int32> l_list = new List<int>();

      if (l_dic[typeof(PeriodModel)] == null)
        return (null);
      foreach (UInt32 l_period in l_dic[typeof(PeriodModel)])
        l_list.Add((Int32)l_period);
      return (l_list);
    }

    public List<Tuple<AxisType, UInt32>> GetAxisElems()
    {
      SafeDictionary<Type, List<UInt32>> l_dic = m_view.GetCheckedElements();
      List<Tuple<AxisType, UInt32>> l_list = new List<Tuple<AxisType, uint>>();

      if (l_dic[typeof(AxisElem)] == null)
        return (l_list);
      foreach (UInt32 l_axisElemId in l_dic[typeof(AxisElem)])
      {
        AxisElem l_axisElem = AxisElemModel.Instance.GetValue(l_axisElemId);

        if (l_axisElem == null)
          continue;
        l_list.Add(new Tuple<AxisType,uint>(l_axisElem.Axis, l_axisElem.Id));
      }
      return (l_list);
    }

    public List<Tuple<AxisType, UInt32, UInt32>> GetFilters()
    {
      SafeDictionary<Type, List<UInt32>> l_dic = m_view.GetCheckedElements();
      List<Tuple<AxisType, UInt32, UInt32>> l_list = new List<Tuple<AxisType, uint, uint>>();

      if (l_dic[typeof(Filter)] == null)
        return (l_list);
      foreach (UInt32 l_fvId in l_dic[typeof(Filter)])
      {
        FilterValue l_fv = FilterValueModel.Instance.GetValue(l_fvId);

        if (l_fv == null)
          continue;
        Filter l_filter = FilterModel.Instance.GetValue(l_fv.FilterId);
        if (l_filter == null)
          continue;
        l_list.Add(new Tuple<AxisType, uint, uint>(l_filter.Axis, l_filter.Id, l_fv.Id));
      }
      return (l_list);
    }

    public UInt32 GetEntity()
    {
      if (m_view.GetSelectedEntity() != 0)
        return (m_view.GetSelectedEntity());

      AxisElem l_entity = AxisElemModel.Instance.GetTopEntity();

      if (l_entity != null)
        return (l_entity.Id);
      return (0);
    }

    public Int32 GetStartPeriod()
    {
      return (PeriodController.GetStartDate());
    }

    public Int32 GetNbPeriod()
    {
      return (PeriodController.GetNbPeriods());
    }

    #endregion

  }
}
