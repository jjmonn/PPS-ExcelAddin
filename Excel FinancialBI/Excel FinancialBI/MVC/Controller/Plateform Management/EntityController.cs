using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;
  using Model;
  using Model.CRUD;
  using Utils;

  class EntityController : AxisBaseController<EntityView, EntityController>
  {
    public EntityController() : base(AxisType.Entities)
    {
      m_view = new EntityView();
      m_view.SetController(this);
      LoadView();
    }

    public override void LoadView()
    {
      base.LoadView();
      m_view.LoadView();
    }

    public bool UpdateEntityCurrency(EntityCurrency p_entityCurrency)
    {
      AxisElem l_entity = AxisElemModel.Instance.GetValue(p_entityCurrency.Id);
      Currency l_currency = CurrencyModel.Instance.GetValue(p_entityCurrency.CurrencyId);

      if (l_entity == null)
      {
        Error = Local.GetValue("entity.error.entity_not_found");
        return (false);
      }
      if (l_currency == null)
      {
        Error = Local.GetValue("entity.error.currency_not_found");
        return (false);
      }
      if (EntityCurrencyModel.Instance.Update(p_entityCurrency))
        return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }
  }
}
