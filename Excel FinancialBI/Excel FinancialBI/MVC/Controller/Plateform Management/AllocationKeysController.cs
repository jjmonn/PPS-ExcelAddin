using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIBlend.WinForms.DataGridView;

namespace FBI.MVC.Controller
{
  using View;
  using Utils;
  using Forms;
  using Model;
  using Model.CRUD;

  class AllocationKeysController : NameController<AllocationKeysView>
  {

    #region Variables

    public override IView View { get { return (m_view); } }
    private Account m_account = null;

    #endregion

    #region Initialize

    public AllocationKeysController()
    {
      this.m_view = new AllocationKeysView();
      this.m_view.SetController(this);
      LoadView();
    }

    public override void LoadView()
    {
      m_view.LoadView();
    }

    public void ShowView(Account p_account)
    {
      this.m_account = p_account;
      this.m_view.ShowView(this.m_account);
    }

    public override void Close()
    {
      base.Close();
      m_view.CloseView();
    }

    #endregion

    #region Server

    bool CheckEntityDistribution(UInt32 p_entityId, double p_entityValue)
    {
      AxisElem l_entity = AxisElemModel.Instance.GetValue(p_entityId);
      Account l_account = AccountModel.Instance.GetValue(m_account.Id);

      if (TotalPercentageValid(p_entityId) + p_entityValue > 100)
      {
        Error = Local.GetValue("allocationKeys.msg_percentageOver100");
        return (false);
      }
      if (l_entity == null || AxisElemModel.Instance.IsParent(l_entity.Id))
      {
        Error = Local.GetValue("general.error.invalid_attribute");
        return (false);
      }
      if (l_account == null || l_account.FormulaType != Account.FormulaTypes.HARD_VALUE_INPUT)
      {
        Error = Local.GetValue("general.error.invalid_attribute");
        return (false);
      }
      return (true);
    }

    public bool UpdateEntityDistribution(UInt32 p_entityId, double p_entityValue)
    {
      EntityDistribution l_entityDistribution;

      if (CheckEntityDistribution(p_entityId, p_entityValue) == false)
        return (false);
      if ((l_entityDistribution = EntityDistributionModel.Instance.GetValue(p_entityId, this.m_account.Id)) == null)
      {
        l_entityDistribution = new EntityDistribution();
        l_entityDistribution.AccountId = this.m_account.Id;
        l_entityDistribution.EntityId = p_entityId;
        l_entityDistribution.Percentage = (byte)p_entityValue;
        if (EntityDistributionModel.Instance.Create(l_entityDistribution))
          return (true);
      }
      else
      {
        if (p_entityValue != -1)
          l_entityDistribution.Percentage = (byte)p_entityValue;
        if (EntityDistributionModel.Instance.Update(l_entityDistribution))
          return (true);
      }
      Error = Local.GetValue("general.error.system");
      return (false);
    }

    #endregion

    #region Check

    public double TotalPercentageValid(UInt32 p_entityId)
    {
      double l_totalPercentage = 0.0;
      MultiIndexDictionary<UInt32, string, AxisElem> l_axisElemMID = AxisElemModel.Instance.GetDictionary(AxisType.Entities);

      foreach (AxisElem l_entity in l_axisElemMID.SortedValues)
      {
        EntityDistribution l_entityDistrib = EntityDistributionModel.Instance.GetValue(l_entity.Id, this.m_account.Id);

        if (l_entityDistrib != null)
          if (l_entity.Id != p_entityId)
            if (l_entity.AllowEdition)
              l_totalPercentage += l_entityDistrib.Percentage;
        if (l_totalPercentage > 100)
          return (l_totalPercentage);
      }
      return (l_totalPercentage);
    }

    #endregion

  }
}
