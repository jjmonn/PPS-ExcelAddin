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
  //  using CRUD;
  using Utils;

  internal partial class PlatformMGTGeneralUI : Form, IView
  {
    private IPlatformManagementController m_currentController;

    #region Initialization

    public PlatformMGTGeneralUI()
    {
      InitializeComponent();
      MultilanguageSetup();
      Load += PlatformMGTGeneralUI_Load;
      FormClosing += PlatformMGTGeneralUI_FormClosing;
    }

    public void SetController(IController p_controller)
    {

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

    private void CloseCurrentControl()
    {
      if ((m_currentController != null))
      {
        m_currentController.Close();
        m_currentController = null;
      }

    }

    private void PlatformMGTGeneralUI_FormClosing(object sender, FormClosingEventArgs e)
    {
      if ((m_currentController != null))
      {
        m_currentController.Close();
      }

    }

    private void PlatformMGTGeneralUI_Load(object sender, EventArgs e)
    {
      this.WindowState = FormWindowState.Maximized;
    }

    #endregion

    #region Call backs

    private void AccountsBT_Click(object sender, EventArgs e)
    {
      CloseCurrentControl();
      AccountsView l_accountsView = new AccountsView();
      // m_currentController = new AccountsController(m_currentView);
      Panel1.Controls.Add(l_accountsView);
    }

    private void m_entitiesBT_Click(object sender, EventArgs e)
    {
      CloseCurrentControl();
      AxisView l_entitiesView = new AxisView(Model.CRUD.AxisType.Entities);
      // m_currentController = new EntitiesController(m_currentView);
      Panel1.Controls.Add(l_entitiesView);
    }

    private void m_employeesButton_Click(object sender, EventArgs e)
    {
      CloseCurrentControl();
      AxisView l_employeesView = new EmployeeView();
      // m_currentController = new EmployeesController(m_currentView);
      Panel1.Controls.Add(l_employeesView);
    }

    private void ClientsBT_Click(object sender, EventArgs e)
    {
      // to be filled with your controller and view like examples above
    }

    private void ProductsBT_Click(object sender, EventArgs e)
    {
      // to be filled with your controller and view like examples above
    }

    private void AdjustmentsBT_Click(object sender, EventArgs e)
    {
      // to be filled with your controller and view like examples above
    }

    private void m_entitiesFiltersBT_Click(object sender, EventArgs e)
    {
      // to be filled with your controller and view like examples above
    }

    private void m_employeesFiltersBT_Click(object sender, EventArgs e)
    {
      // to be filled with your controller and view like examples above
    }

    private void CategoriesBT_Click(object sender, EventArgs e)
    {
      // to be filled with your controller and view like examples above
    }

    private void m_clientsFiltersBT_Click(object sender, EventArgs e)
    {
      // to be filled with your controller and view like examples above
    }

    private void m_productsFiltersBT_Click(object sender, EventArgs e)
    {
      // to be filled with your controller and view like examples above
    }

    private void m_adjustmentsFiltersBT_Click(object sender, EventArgs e)
    {
      // to be filled with your controller and view like examples above
    }

    private void VersionsBT_Click(object sender, EventArgs e)
    {
      CloseCurrentControl();
      VersionsView l_versionsView = new VersionsView();
      m_currentController = new VersionsController(l_versionsView);
      Panel1.Controls.Add(l_versionsView);
    }

    private void CurrenciesBT_Click(object sender, EventArgs e)
    {
      // to be filled with your controller and view like examples above
    }

    private void ExchangeRatesButton_Click(object sender, EventArgs e)
    {
      // to be filled with your controller and view like examples above
    }

    private void GroupsBT_Click(object sender, EventArgs e)
    {
      // to be filled with your controller and view like examples above
    }
    #endregion


  }

}
