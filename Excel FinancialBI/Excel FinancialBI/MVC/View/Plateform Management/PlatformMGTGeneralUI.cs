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
  using System.ComponentModel;
  using System.Threading.Tasks;
  using Utils;
  using Model.CRUD;
  using Model;

  public partial class PlatformMGTGeneralUI : Form, IView
  {
    PlatformMgtController m_controller;
    Color m_defaultColor;
    ToolStripMenuItem m_selectedBT;

    #region Initialization

    public PlatformMGTGeneralUI()
    {
      InitializeComponent();
      m_defaultColor = AccountsBT.BackColor;
      SubscribeEvents();
      MultilanguageSetup();
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as PlatformMgtController;
    }

    private void MultilanguageSetup()
    {
      this.AccountsBT.Text = Local.GetValue("GeneralEditionUI.accounts");
      this.AccountsBT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_account");
      this.m_entitiesBT.Text = Local.GetValue("GeneralEditionUI.entities");
      this.m_entitiesBT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_entities");
      this.CategoriesBT.Text = Local.GetValue("GeneralEditionUI.categories");
      this.CategoriesBT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_categories");
      this.m_entitiesFiltersBT.Text = Local.GetValue("general.entities_filters");
      this.m_clientsFiltersBT.Text = Local.GetValue("general.clients_filters");
      this.m_productsFiltersBT.Text = Local.GetValue("general.products_filters");
      this.m_adjustmentsFiltersBT.Text = Local.GetValue("general.adjustments_filters");
      this.m_employeesFiltersBT.Text = Local.GetValue("general.employees_filters");
      this.ClientsBT.Text = Local.GetValue("general.clients");
      this.ClientsBT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_clients");
      this.ProductsBT.Text = Local.GetValue("general.products");
      this.ProductsBT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_products");
      this.AdjustmentsBT.Text = Local.GetValue("general.adjustments");
      this.AdjustmentsBT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_adjustments");
      this.VersionsBT.Text = Local.GetValue("general.versions");
      this.VersionsBT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_versions");
      this.CurrenciesBT.Text = Local.GetValue("general.currencies");
      this.CurrenciesBT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_currencies");
      this.GlobalFact_BT.Text = Local.GetValue("general.economic_indicators");
      this.GlobalFact_BT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_economic_indicators");
      this.ExchangeRatesButton.Text = Local.GetValue("GeneralEditionUI.exchange_rates");
      this.ExchangeRatesButton.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_exchange_rates");
      this.GroupsBT.Text = Local.GetValue("GeneralEditionUI.users_groups");
      this.GroupsBT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_users_groups");
      this.Text = Local.GetValue("GeneralEditionUI.platform_config");
    }

    private void SubscribeEvents()
    {
      this.Load += PlatformMGTGeneralUI_Load;
      this.FormClosing += PlatformMGTGeneralUI_FormClosing;
      this.AccountsBT.Click += new System.EventHandler(this.AccountsBT_Click);
      this.m_entitiesBT.Click += new System.EventHandler(this.m_entitiesBT_Click);
      this.m_employeesButton.Click += new System.EventHandler(this.m_employeesButton_Click);
      this.ClientsBT.Click += new System.EventHandler(this.ClientsBT_Click);
      this.ProductsBT.Click += new System.EventHandler(this.ProductsBT_Click);
      this.AdjustmentsBT.Click += new System.EventHandler(this.AdjustmentsBT_Click);
      this.m_entitiesFiltersBT.Click += new System.EventHandler(this.m_entitiesFiltersBT_Click);
      this.m_employeesFiltersBT.Click += new System.EventHandler(this.m_employeesFiltersBT_Click);
      this.m_clientsFiltersBT.Click += new System.EventHandler(this.m_clientsFiltersBT_Click);
      this.m_productsFiltersBT.Click += new System.EventHandler(this.m_productsFiltersBT_Click);
      this.m_adjustmentsFiltersBT.Click += new System.EventHandler(this.m_adjustmentsFiltersBT_Click);
      this.VersionsBT.Click += new System.EventHandler(this.VersionsBT_Click);
      this.CurrenciesBT.Click += new System.EventHandler(this.CurrenciesBT_Click);
      this.ExchangeRatesButton.Click += new System.EventHandler(this.ExchangeRatesButton_Click);
      this.GroupsBT.Click += new System.EventHandler(this.GroupsBT_Click);
    }

    private void PlatformMGTGeneralUI_FormClosing(object sender, FormClosingEventArgs e)
    {
      
    }

    private void PlatformMGTGeneralUI_Load(object sender, EventArgs e)
    {
      this.WindowState = FormWindowState.Maximized;
    }

    #endregion

    #region Call backs

    private void AccountsBT_Click(object sender, EventArgs e)
    {
      SelectBT((ToolStripMenuItem)sender);
      m_controller.SwitchView<AccountsView, AccountController>(new AccountController());
    }

    private void m_entitiesBT_Click(object sender, EventArgs e)
    {
      SelectBT((ToolStripMenuItem)sender);
      m_controller.SwitchView<EntityView, EntityController>(new EntityController());
    }

    private void m_employeesButton_Click(object sender, EventArgs e)
    {
      SelectBT((ToolStripMenuItem)sender);
      AxisConfiguration l_config = AxisConfigurationModel.Instance.GetValue((uint)AxisType.Employee);
      if (l_config != null && l_config.Owned)
        m_controller.SwitchView<AxisOwnedView, AxisOwnedController>(new AxisOwnedController(l_config.AxisOwner, l_config.Axis));
      else
        m_controller.SwitchView<AxisView, AxisController>(new AxisController(AxisType.Employee));    
    }

    private void ClientsBT_Click(object sender, EventArgs e)
    {
      SelectBT((ToolStripMenuItem)sender);
      AxisConfiguration l_config = AxisConfigurationModel.Instance.GetValue((uint)AxisType.Client);
      if (l_config != null && l_config.Owned)
        m_controller.SwitchView<AxisOwnedView, AxisOwnedController>(new AxisOwnedController(l_config.AxisOwner, l_config.Axis));
      else
        m_controller.SwitchView<AxisView, AxisController>(new AxisController(AxisType.Client));
    }

    private void ProductsBT_Click(object sender, EventArgs e)
    {
      SelectBT((ToolStripMenuItem)sender);
      AxisConfiguration l_config = AxisConfigurationModel.Instance.GetValue((uint)AxisType.Product);
      if (l_config != null && l_config.Owned)
        m_controller.SwitchView<AxisOwnedView, AxisOwnedController>(new AxisOwnedController(l_config.AxisOwner, l_config.Axis));
      else
        m_controller.SwitchView<AxisView, AxisController>(new AxisController(AxisType.Product));
    }

    private void AdjustmentsBT_Click(object sender, EventArgs e)
    {
      SelectBT((ToolStripMenuItem)sender);
      AxisConfiguration l_config = AxisConfigurationModel.Instance.GetValue((uint)AxisType.Adjustment);
      if (l_config != null && l_config.Owned)
        m_controller.SwitchView<AxisOwnedView, AxisOwnedController>(new AxisOwnedController(l_config.AxisOwner, l_config.Axis));
      else
        m_controller.SwitchView<AxisView, AxisController>(new AxisController(AxisType.Adjustment));
    }

    private void m_entitiesFiltersBT_Click(object sender, EventArgs e)
    {
      SelectBT(CategoriesBT);
      m_controller.SwitchView<FilterView, FilterController>(new FilterController(AxisType.Entities));
    }

    private void m_employeesFiltersBT_Click(object sender, EventArgs e)
    {
      SelectBT(CategoriesBT);
      m_controller.SwitchView<FilterView, FilterController>(new FilterController(AxisType.Employee));
    }

    private void m_clientsFiltersBT_Click(object sender, EventArgs e)
    {
      SelectBT(CategoriesBT);
      m_controller.SwitchView<FilterView, FilterController>(new FilterController(AxisType.Client));
    }

    private void m_productsFiltersBT_Click(object sender, EventArgs e)
    {
      SelectBT(CategoriesBT);
      m_controller.SwitchView<FilterView, FilterController>(new FilterController(AxisType.Product));
    }

    private void m_adjustmentsFiltersBT_Click(object sender, EventArgs e)
    {
      SelectBT(CategoriesBT);
      m_controller.SwitchView<FilterView, FilterController>(new FilterController(AxisType.Adjustment));
    }

    private void VersionsBT_Click(object sender, EventArgs e)
    {
      SelectBT((ToolStripMenuItem)sender);
      m_controller.SwitchView<VersionsView, VersionsController>(new VersionsController());
    }

    private void CurrenciesBT_Click(object sender, EventArgs e)
    {
      SelectBT((ToolStripMenuItem)sender);
      m_controller.SwitchView<CurrenciesView, CurrenciesController>(new CurrenciesController());
    }

    private void ExchangeRatesButton_Click(object sender, EventArgs e)
    {
      SelectBT((ToolStripMenuItem)sender);
      m_controller.SwitchView<ExchangeRatesView, ExchangeRatesController>(new ExchangeRatesController());
    }

    private void GroupsBT_Click(object sender, EventArgs e)
    {
      SelectBT((ToolStripMenuItem)sender);
      m_controller.SwitchView<UsersView, UserController>(new UserController());
    }
    #endregion

    private void GlobalFact_BT_Click(object sender, EventArgs e)
    {
      SelectBT((ToolStripMenuItem)sender);
      m_controller.SwitchView<GlobalFactView, GlobalFactController>(new GlobalFactController());
    }

    private void PlatformMGTGeneralUI_FormClosed(object sender, FormClosedEventArgs e)
    {
      m_controller.Close();
    }

    void SelectBT(ToolStripMenuItem p_bt)
    {
      if (m_selectedBT != null)
        m_selectedBT.BackColor = m_defaultColor;
      p_bt.BackColor = Color.White;
      m_selectedBT = p_bt;
    }
  }

}
