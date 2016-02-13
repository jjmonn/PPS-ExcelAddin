using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;
  using Model;
  using Utils;
  using Model.CRUD;

  class ExcelGlobalFactController : IExcelImportController
  {
    UInt32 m_versionId;
    ExcelImportView m_view;

    public string Error { get; set; }
    public IView View { get { return (m_view); } }

    public ExcelGlobalFactController()
    {
      m_view = new ExcelImportView(ExcelImportView.ViewType.GLOBAL_FACT);
      m_view.SetController(this);
      m_view.LoadView();
    }

    public void LoadView(UInt32 p_version)
    {
      m_versionId = p_version;
      if (p_version > 0)
      {
        m_view.Reload();
      }
    }

    private bool SetError(string p_localName)
    {
      Error = Local.GetValue(p_localName);
      return (false);
    }

    public bool IsFactValid(GlobalFactData p_gfactData)
    {
      if (GlobalFactModel.Instance.GetValue(p_gfactData.GlobalFactId) == null)
      {
        Error = Local.GetValue("gfactdata.error.gfact_not_found");
        return (false);
      }
      if (GlobalFactVersionModel.Instance.GetValue(p_gfactData.VersionId) == null)
      {
        Error = Local.GetValue("gfactdata.error.version_not_found");
        return (false);
      }
      return (true);
    }

    public bool Create(UInt32 p_id, Int32 p_period, double p_value)
    {
      GlobalFactData l_fact = GlobalFactDataModel.Instance.GetValue(p_id, (UInt32)p_period, m_versionId);

      if (l_fact == null)
      {
        l_fact = new GlobalFactData();
        l_fact.Period = (UInt32)p_period;
        l_fact.GlobalFactId = p_id;
        l_fact.VersionId = m_versionId;
        l_fact.Value = p_value;
        if (IsFactValid(l_fact) == false)
          return (false);
        GlobalFactDataModel.Instance.Create(l_fact);
      }
      else
      {
        l_fact = l_fact.Clone();
        l_fact.Value = p_value;
        if (IsFactValid(l_fact) == false)
          return (false);
        GlobalFactDataModel.Instance.Update(l_fact);
      }
      return (true);
    }

    public bool Create(UInt32 p_id, List<Int32> p_periods, List<double> p_values)
    {
      Int32 i = 0;

      if (p_values == null || p_periods == null)
        return (this.SetError("upload.not_complete"));
      if (p_periods.Count != p_values.Count)
        return (this.SetError("upload.range_mismatch"));
      for (i = 0; i < p_periods.Count; ++i)
      {
        if (!this.Create(p_id, p_periods[i], p_values[i]))
          return (false);
      }
      return (true);
    }
  }
}
