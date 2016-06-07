using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.View
{
  using Model.CRUD;

  public interface IResultView : IView
  {
    void LoadView();
    void PrepareDgv(ComputeConfig p_config);
    void FillDGV(SafeDictionary<uint, ComputeResult> p_data);
    void DropOnExcel(bool p_copyOnlyExpanded);
  }
}
