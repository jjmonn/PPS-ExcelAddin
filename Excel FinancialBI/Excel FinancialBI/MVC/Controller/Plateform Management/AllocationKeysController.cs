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

    public AllocationKeysController(Account p_account)
    {
      this.m_account = p_account;
      if (this.m_account == null)
        return;
      this.m_view = new AllocationKeysView();
      this.m_view.SetController(this);
      this.LoadView();
    }

    public override void LoadView()
    {
      this.m_view.LoadView(this.m_account);
      this.m_view.ShowDialog();
    }

    #endregion

    #region Server

    public void UpdateAllocationKey(UInt32 p_entityId, double p_entityValue)
    {
      EntityDistribution l_entityDistribution;

      if ((l_entityDistribution = EntityDistributionModel.Instance.GetValue(p_entityId, this.m_account.Id)) == null)
      {
        if (p_entityValue == -1)
          return;
        l_entityDistribution = new EntityDistribution();
        l_entityDistribution.AccountId = this.m_account.Id;
        l_entityDistribution.EntityId = p_entityId;
        l_entityDistribution.Percentage = (byte)p_entityValue;
        EntityDistributionModel.Instance.Create(l_entityDistribution);
      }
      else
      {
        if (p_entityValue != -1)
          l_entityDistribution.Percentage = (byte)p_entityValue;
        EntityDistributionModel.Instance.Update(l_entityDistribution);
      }
    }

    #endregion

    #region Check

    public double TotalPercentageValid(UInt32 l_id)
    {
      double l_totalPercentage = 0.0;
      MultiIndexDictionary<UInt32, string, AxisElem> l_axisElemMID = AxisElemModel.Instance.GetDictionary(AxisType.Entities);

      foreach (AxisElem l_entity in l_axisElemMID.SortedValues)
      {
        EntityDistribution l_entityDistrib = EntityDistributionModel.Instance.GetValue(l_entity.Id, this.m_account.Id);

        if (l_entityDistrib != null)
          if (l_entity.Id != l_id)
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
