using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIBlend.WinForms.DataGridView;
using System.Windows.Forms;

namespace FBI.MVC.View
{
  using Controller;
  using Model;
  using Model.CRUD;
  using Utils;
  using Forms;

  class EntityView : AxisBaseView<EntityController>
  {
    public EntityView()
    {
      SuscribeEvents();
    }

    void SuscribeEvents()
    {
      m_dgv.CellMouseClick += OnClickCell;
      m_dgv.CellChangedAndValidated += OnEntityCurrencyChanged;
    }

    public override void LoadView()
    {
      base.LoadView();

      MultiIndexDictionary<UInt32, Tuple<UInt32, UInt32>, AxisFilter> l_axisFilterDic = AxisFilterModel.Instance.GetDictionary(AxisType.Entities);
      MultiIndexDictionary<UInt32, string, AxisElem> l_entityDic = AxisElemModel.Instance.GetDictionary(AxisType.Entities);

      if (l_axisFilterDic != null)
        LoadDGV(l_entityDic, l_axisFilterDic.SortedValues);
      m_dgv.SetDimension(FbiDataGridView.Dimension.COLUMN, 0, Local.GetValue("general.currency"));
      foreach (AxisElem l_entity in l_entityDic.Values)
      {
        if (l_entity.AllowEdition == false)
          continue;
        EntityCurrency l_entityCurrency = EntityCurrencyModel.Instance.GetValue(l_entity.Id);

        if (l_entityCurrency == null)
          return;
        m_dgv.FillField(l_entity.Id, 0, CurrencyModel.Instance.GetValueName(l_entityCurrency.CurrencyId));
      }
      m_dgv.Refresh();
    }

    private void OnClickCell(object p_sender, CellMouseEventArgs p_e)
    {
      if (p_e.Cell.ColumnItem.ItemValue == null || p_e.Cell.RowItem.ItemValue == null)
        return;
      if ((UInt32)p_e.Cell.ColumnItem.ItemValue != 0)
        return;
      UInt32 l_entityId = (UInt32)p_e.Cell.RowItem.ItemValue;
      AxisElem l_entity = AxisElemModel.Instance.GetValue(AxisType.Entities, l_entityId);
      EntityCurrency l_entityCurrency = EntityCurrencyModel.Instance.GetValue(l_entityId);

      if (l_entityCurrency == null || l_entity == null || l_entity.AllowEdition == false)
        return;
      m_dgv.CellsArea.SetCellEditor(p_e.Cell.RowItem, p_e.Cell.ColumnItem, BuildCurrencyCB());
    }

    ComboBoxEditor BuildCurrencyCB()
    {
      ComboBoxEditor l_cbEditor = new ComboBoxEditor();

      foreach (UInt32 l_currencyId in CurrencyModel.Instance.GetUsedCurrencies())
        l_cbEditor.Items.Add(CurrencyModel.Instance.GetValueName(l_currencyId));
      return (l_cbEditor);
    }

    void OnEntityCurrencyChanged(object p_sender, CellEventArgs p_args)
    {
      if (p_args.Cell.ColumnItem.ItemValue == null || p_args.Cell.RowItem.ItemValue == null)
        return;
      if ((UInt32)p_args.Cell.ColumnItem.ItemValue != 0)
        return;
      string l_currencyName = (string)p_args.Cell.Value;
      UInt32 l_entityId = (UInt32)p_args.Cell.RowItem.ItemValue;
      Currency l_currency = CurrencyModel.Instance.GetValue(l_currencyName);
      EntityCurrency l_entity = EntityCurrencyModel.Instance.GetValue(l_entityId);

      if (l_currency == null || l_entity == null)
        return;
      l_entity = l_entity.Clone();
      l_entity.CurrencyId = l_currency.Id;
      if (m_controller.UpdateEntityCurrency(l_entity) == false)
        Forms.MsgBox.Show(m_controller.Error);
    }
  }
}
