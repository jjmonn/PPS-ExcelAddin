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
  using Controller;
  using Utils;
  using Forms;
  using Model;
  using Model.CRUD;

  public partial class AccountsView : UserControl, IView
  {

    //private RightManager m_rightMGR; //TODO : not here

    IController m_controller;
    FbiTreeView<Account> m_accountTV;
    FbiTreeView<GlobalFact> m_globalFactsTV; //TODO : FbiTreeView
    vTreeNode m_currentNode;

    private SafeDictionary<Account.FormulaTypes, ListItem> m_formulasTypesIdItemDict = new SafeDictionary<Account.FormulaTypes,ListItem>();
    private SafeDictionary<Account.AccountType, ListItem> m_formatsIdItemDict = new SafeDictionary<Account.AccountType,ListItem>();
    private Dictionary<Account.ConversionOptions, ListItem>  m_currenciesConversionIdItemDict = new Dictionary<Account.ConversionOptions,ListItem>();
    private SafeDictionary<Account.ConsolidationOptions, ListItem> m_consoOptionIdItemDict = new SafeDictionary<Account.ConsolidationOptions,ListItem>();
    private Boolean m_dragAndDropFlag = false;
    private Boolean m_isDisplayingAccountFlag;
    private Boolean m_isEditingFormulaFlag = false;
    private Boolean m_isRevertingFTypeFlag = false;

    private const UInt32 MARGIN_SIZE = 15;
    private const UInt32 ACCOUNTS_VIEW_TV_MAX_WIDTH = 600;
    private const UInt32 MARGIN1 = 30;

    public AccountsView()
    {
      InitializeComponent();
    }
    
    public void SetController(IController p_controller)
    {
      this.m_controller = p_controller;
    }

    public void InitView(FbiTreeView<Account> p_accountTv, FbiTreeView<GlobalFact> p_globalFactsTv)
    {
      this.AccountsTVInit(p_accountTv);
      this.GlobalFactsTVInit(p_globalFactsTv);
      this.ComboBoxesInit();

      //this.DefineUIPermissions(); TODO : RightManager
      //this.DesactivateUnallowed(); TODO : RightManager
      //this.SetAccountUIState(false);

      this.MultilangueSetup();
      //this.SetFormulaEditionState(false);
    }

    private void MultilangueSetup()
    {
      this.m_accountDescriptionGroupbox.Text = Local.GetValue("accounts_edition.account_description");
      this.SaveDescriptionBT.Text = Local.GetValue("accounts_edition.save_description");
      this.m_accountFormulaGroupbox.Text = Local.GetValue("accounts_edition.account_formula");
      this.m_formulaEditionButton.Text = Local.GetValue("accounts_edition.edit_formula");
      this.m_validateFormulaButton.Text = Local.GetValue("general.save");
      this.m_cancelFormulaEditionButton.Text = Local.GetValue("general.cancel");
      this.m_accountInformationGroupbox.Text = Local.GetValue("accounts_edition.account_information");
      this.m_accountNameLabel.Text = Local.GetValue("accounts_edition.account_name");
      this.m_accountFormulaTypeLabel.Text = Local.GetValue("accounts_edition.formula_type");
      this.m_accountTypeLabel.Text = Local.GetValue("accounts_edition.account_type");
      this.m_accountConsolidationOptionLabel.Text = Local.GetValue("accounts_edition.consolidation_option");
      this.m_accountCurrenciesConversionLabel.Text = Local.GetValue("accounts_edition.currencies_conversion");
      this.m_globalFactsLabel.Text = Local.GetValue("accounts_edition.macro_economic_indicators");
      this.AddSubAccountToolStripMenuItem.Text = Local.GetValue("accounts_edition.new_account");
      this.AddCategoryToolStripMenuItem.Text = Local.GetValue("accounts_edition.add_tab_account");
      this.DeleteAccountToolStripMenuItem.Text = Local.GetValue("accounts_edition.delete_account");
      this.DropHierarchyToExcelToolStripMenuItem.Text = Local.GetValue("accounts_edition.drop_to_excel");
      this.NewToolStripMenuItem.Text = Local.GetValue("general.account");
      this.CreateANewAccountToolStripMenuItem.Text = Local.GetValue("accounts_edition.new_account");
      this.CreateANewCategoryToolStripMenuItem.Text = Local.GetValue("accounts_edition.add_tab_account");
      this.DeleteAccountToolStripMenuItem1.Text = Local.GetValue("accounts_edition.delete_account");
      this.DropAllAccountsHierarchyToExcelToolStripMenuItem.Text = Local.GetValue("accounts_edition.drop_to_excel");
      this.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Text = Local.GetValue("accounts_edition.drop_selected_hierarchy_to_excel");
      this.HelpToolStripMenuItem.Text = Local.GetValue("general.help");
    }

    private void SetAccountUIState(Boolean p_uiState)
    {
      this.SaveDescriptionBT.Enabled = p_uiState;
      this.Name_TB.Enabled = p_uiState;
      this.FormulaTypeComboBox.Enabled = p_uiState;
      this.TypeComboBox.Enabled = p_uiState;
      this.CurrencyConversionComboBox.Enabled = p_uiState;
      this.m_descriptionTextBox.Enabled = p_uiState;
      this.ConsolidationOptionComboBox.Enabled = p_uiState;
      this.m_validateFormulaButton.Enabled = p_uiState;
      this.m_formulaEditionButton.Enabled = p_uiState;
    }

    private void AccountsTVInit(FbiTreeView<Account> p_accountTV)
    {
      this.m_accountTV = p_accountTV;
      this.m_accountTV.ContextMenuStrip = TVRCM;
      this.m_accountTV.Dock = DockStyle.Fill;
      this.m_accountTV.AllowDrop = true;
      this.m_accountTV.LabelEdit = false;
      this.m_accountTV.CollapseAll();
      this.m_accountTV.ImageList = this.accountsIL;
      this.m_accountTV.BorderColor = Color.Transparent;
      this.AccountsTVPanel.Controls.Add(m_accountTV);

      //TODO : do event
      this.m_accountTV.KeyDown += AccountTV_KeyDown;
      this.m_accountTV.MouseDown += AccountTV_MouseDown;
      this.m_accountTV.DragOver += AccountTV_DragOver;
      this.m_accountTV.DragDrop += AccountTV_DragDrop;
      this.m_accountTV.AfterSelect += AccountTV_AfterSelect;
    }

    private void AccountTV_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Down) //TODO : Maybe switch
      {

      }
      if (e.KeyCode == Keys.Up)
      {

      }
    }

    private void AccountTV_DragDrop(object sender, DragEventArgs e)
    {
      if (this.m_isEditingFormulaFlag == false)
      {
        if (e.Data.GetDataPresent("VIBlend.WinForms.Controls.vTreeNode", true) == true)
        {
          //TODO : DO
        }
      }
    }

    private void AccountTV_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        TVRCM.Visible = false;
        TVRCM.Show(e.Location);
        TVRCM.Visible = true;
      }
      if (e.Button == MouseButtons.Left) // TODO : see for keydown
      {
        vTreeNode l_node = this.m_accountTV.FindAtPosition(e.Location);
        if (l_node != null)
          this.m_accountTV.DoDragDrop(l_node, DragDropEffects.Move);
      }
    }

    private void AccountTV_DragOver(object sender, DragEventArgs e)
    {
      if (this.m_isEditingFormulaFlag == false)
      {
        if (e.Data.GetDataPresent("VIBlend.WinForms.Controls.vTreeNode", true) == true)
        {
          //TODO : DO
        }
      }
    }

    private void AccountTV_AfterSelect(object sender, vTreeViewEventArgs e)
    {
      if (this.m_dragAndDropFlag == false && this.m_isEditingFormulaFlag == false
        && m_isDisplayingAccountFlag == false)
      {
        this.m_currentNode = e.Node;
        if (this.m_currentNode != null)
        {
          //this.DisplayAttributes(); TODO : Change dat thing
          //this.DesactivateUnallowed(); TODO : RightManager
        }
        else
          this.m_accountTV.Capture = false;
      }
    }

    private void GlobalFactsTVInit(FbiTreeView<GlobalFact> p_globalFactsTV)
    {
      this.m_globalFactsTV = p_globalFactsTV;
      this.m_globalFactsTV.Dock = DockStyle.Fill;
      this.m_globalFactsTV.LabelEdit = false;
      this.m_globalFactsTV.CollapseAll();
      this.m_globalFactsTV.ImageList = m_globalFactsImageList;
      this.GlobalFactsPanel.Controls.Add(m_globalFactsTV);

      //TODO : see event
    }

    private void ComboBoxesInit() //TODO : maybe put hardcoded strings away
    {
      ListItem l_inputListItem = new ListItem();
      l_inputListItem.Text = "Input";
      l_inputListItem.Value = Account.FormulaTypes.HARD_VALUE_INPUT;
      this.FormulaTypeComboBox.Items.Add(l_inputListItem);
      this.m_formulasTypesIdItemDict[(Account.FormulaTypes)l_inputListItem.Value] = l_inputListItem;

      ListItem l_formulaListItem = new ListItem();
      l_formulaListItem.Text = "Formula";
      l_formulaListItem.Value = Account.FormulaTypes.FORMULA;
      this.FormulaTypeComboBox.Items.Add(l_formulaListItem);
      this.m_formulasTypesIdItemDict[(Account.FormulaTypes)l_formulaListItem.Value] = l_formulaListItem;

      ListItem l_aggregationListItem = new ListItem();
      l_aggregationListItem.Text = "Aggregation of Sub Accounts";
      l_aggregationListItem.Value = Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS;
      this.FormulaTypeComboBox.Items.Add(l_aggregationListItem);
      this.m_formulasTypesIdItemDict[(Account.FormulaTypes)l_aggregationListItem.Value] = l_aggregationListItem;

      ListItem l_firstPeriodInputListItem = new ListItem();
      l_firstPeriodInputListItem.Text = "First Period Input";
      l_firstPeriodInputListItem.Value = Account.FormulaTypes.FIRST_PERIOD_INPUT;
      this.FormulaTypeComboBox.Items.Add(l_firstPeriodInputListItem);
      this.m_formulasTypesIdItemDict[(Account.FormulaTypes)l_firstPeriodInputListItem.Value] = l_firstPeriodInputListItem;

      ListItem l_titleListItem = new ListItem();
      l_titleListItem.Text = "Title";
      l_titleListItem.Value = Account.FormulaTypes.TITLE;
      this.FormulaTypeComboBox.Items.Add(l_titleListItem);
      this.m_formulasTypesIdItemDict[(Account.FormulaTypes)l_titleListItem.Value] = l_titleListItem;

      this.FormulaTypeComboBox.SelectedIndex = 0;
      this.FormulaTypeComboBox.DropDownList = true;

      // Type
      ListItem l_monetaryFormatLI = new ListItem();
      l_monetaryFormatLI.Text = "Monetary";
      l_monetaryFormatLI.Value = Account.AccountType.MONETARY;
      this.TypeComboBox.Items.Add(l_monetaryFormatLI);
      this.m_formatsIdItemDict[(Account.AccountType)l_monetaryFormatLI.Value] = l_monetaryFormatLI;

      ListItem l_normalFormatLI = new ListItem();
      l_normalFormatLI.Text = "Number";
      l_normalFormatLI.Value = Account.AccountType.NUMBER;
      this.TypeComboBox.Items.Add(l_normalFormatLI);
      this.m_formatsIdItemDict[(Account.AccountType)l_normalFormatLI.Value] = l_normalFormatLI;

      ListItem l_percentageFormatLI = new ListItem();
      l_percentageFormatLI.Text = "Percentage";
      l_percentageFormatLI.Value = Account.AccountType.PERCENTAGE;
      this.TypeComboBox.Items.Add(l_percentageFormatLI);
      this.m_formatsIdItemDict[(Account.AccountType)l_percentageFormatLI.Value] = l_percentageFormatLI;

      ListItem l_dateFormatLI = new ListItem();
      l_dateFormatLI.Text = "Date";
      l_dateFormatLI.Value = Account.AccountType.DATE_;
      this.TypeComboBox.Items.Add(l_dateFormatLI);
      this.m_formatsIdItemDict[(Account.AccountType)l_dateFormatLI.Value] = l_dateFormatLI;

      this.TypeComboBox.SelectedIndex = 0;
      this.TypeComboBox.DropDownList = true;

      // Currencies Conversion
      ListItem l_noConversionLI = new ListItem();
      l_noConversionLI.Text = "Non Converted";
      l_noConversionLI.Value = Account.ConversionOptions.NO_CONVERSION;
      this.CurrencyConversionComboBox.Items.Add(l_noConversionLI);
      this.m_currenciesConversionIdItemDict[(Account.ConversionOptions)l_noConversionLI.Value] = l_noConversionLI;

      ListItem l_averageRateLI = new ListItem();
      l_averageRateLI.Text = "Average Exchange Rate";
      l_averageRateLI.Value = Account.ConversionOptions.AVERAGE_RATE;
      this.CurrencyConversionComboBox.Items.Add(l_averageRateLI);
      this.m_currenciesConversionIdItemDict[(Account.ConversionOptions)l_noConversionLI.Value] = l_noConversionLI;

      ListItem l_endOfPeriodRateLI = new ListItem();
      l_endOfPeriodRateLI.Text = "End of Period Exchange Rate";
      l_endOfPeriodRateLI.Value = Account.ConversionOptions.END_OF_PERIOD_RATE;
      this.CurrencyConversionComboBox.Items.Add(l_endOfPeriodRateLI);
      this.m_currenciesConversionIdItemDict[(Account.ConversionOptions)l_endOfPeriodRateLI.Value] = l_endOfPeriodRateLI;

      this.CurrencyConversionComboBox.SelectedIndex = 0;
      this.CurrencyConversionComboBox.DropDownList = true;

      // Recomputation Option
      ListItem l_aggregatedLI = new ListItem();
      l_aggregatedLI.Text = "Aggregated";
      l_aggregatedLI.Value = Account.ConsolidationOptions.AGGREGATION;
      this.ConsolidationOptionComboBox.Items.Add(l_aggregatedLI);
      this.m_consoOptionIdItemDict[(Account.ConsolidationOptions)l_aggregatedLI.Value] = l_aggregatedLI;

      ListItem l_recomputedLI = new ListItem();
      l_recomputedLI.Text = "Recomputed";
      l_recomputedLI.Value = Account.ConsolidationOptions.RECOMPUTATION;
      this.ConsolidationOptionComboBox.Items.Add(l_recomputedLI);
      this.m_consoOptionIdItemDict[(Account.ConsolidationOptions)l_recomputedLI.Value] = l_recomputedLI;

      ListItem l_noneLI = new ListItem();
      l_noneLI.Text = "None";
      l_noneLI.Value = Account.ConsolidationOptions.NONE;
      this.ConsolidationOptionComboBox.Items.Add(l_noneLI);
      this.m_consoOptionIdItemDict[(Account.ConsolidationOptions)l_noneLI.Value] = l_noneLI;

      this.ConsolidationOptionComboBox.SelectedIndex = 0;
      this.ConsolidationOptionComboBox.DropDownList = true;
    }

    private void SetFormulaEditionState(Boolean p_state)
    {
      this.m_isEditingFormulaFlag = p_state;
      this.m_formulaTextBox.Enabled = p_state;
      this.m_validateFormulaButton.Visible = p_state;
      this.m_cancelFormulaEditionButton.Visible = p_state;
      this.m_formulaEditionButton.Visible = p_state = false;
      if (p_state == true)
        this.m_formulaTextBox.BackColor = Color.White;
      else
        this.m_formulaTextBox.BackColor = Color.LightGray;
    }

    private void DisplayAttributes()
    {
      //if ((this.m_currentNode != null) && this.m_isEditingFormulaFlag == false)
      //{
      //  this.m_isDisplayingAccountFlag = true;
      //  UInt32 l_account_id = (UInt32) m_currentNode.Value; //TODO : not sure about casting
      //  Account l_account = GlobalVariables.Accounts.GetValue(l_account_id);

      //  if (l_account == null)
      //    return;
      //  this.Name_TB.Text = m_currentNode.Text;

      //  // Formula Type ComboBox
      //  ListItem l_formulaTypeLI = m_formulasTypesIdItemDict[l_account.FormulaType];
      //  this.FormulaTypeComboBox.SelectedItem = l_formulaTypeLI;

      //  // Format ComboBox
      //  if (this.m_formatsIdItemDict.ContainsKey(l_account.Type))
      //  {
      //    /*dynamic l_formatLI = this.m_formatsIdItemDict[l_account.Type];
      //    this.TypeComboBox.SelectedItem = l_formatLI;
      //    if (l_formatLI.Value == Account.AccountType.MONETARY)
      //    {
      //      // Currency Conversion
      //      ListItem conversionLI = this.m_currenciesConversionIdItemDict[l_account.ConversionOptionId];
      //      CurrencyConversionComboBox.SelectedItem = conversionLI;
      //    }
      //  }
      //  else
      //  {
      //    TypeComboBox.SelectedItem = null;
      //    TypeComboBox.Text = "";
      //  }

      //  // Consolidation Option
      //  ListItem consolidationLI = this.m_consoOptionIdItemDict[l_account.ConsolidationOptionId];
      //  ConsolidationOptionComboBox.SelectedItem = consolidationLI;


      //  // Formula TB
      //  if (m_controller.m_formulaTypesToBeTested.Contains(l_account.FormulaType))
      //  {
      //    m_formulaTextBox.Text = m_controller.GetFormulaText(l_account_id); //TODO : Controller
      //  }
      //  else
      //  {
      //    m_formulaTextBox.Text = "";
      //  }*/

      //  m_descriptionTextBox.Text = l_account.Description;

      //  bool l_isRootAccount = false;
      //  if (m_currentNode.Parent == null)
      //    l_isRootAccount = true;
      //  /*if (l_formulaTypeLI.Value == Account.FormulaTypes.TITLE)
      //    SetEnableStatusEdition(false, l_isRootAccount, l_account.FormulaType);
      //  else
      //    SetEnableStatusEdition(true, l_isRootAccount, l_account.FormulaType);
      //  m_isDisplayingAccountFlag = false;
      //}
    }

    private void SetEnableStatusEdition(Boolean p_status, Boolean p_isRootAccount, Account.FormulaTypes p_accountFormulaType)
    {
      if (p_isRootAccount == true)
        this.FormulaTypeComboBox.Enabled = p_status;
      if (p_accountFormulaType == Account.FormulaTypes.FORMULA | p_accountFormulaType == Account.FormulaTypes.FIRST_PERIOD_INPUT)
        this.m_formulaEditionButton.Enabled = true;
      else
        this.m_formulaEditionButton.Enabled = false;
      this.TypeComboBox.Enabled = p_status;
      this.CurrencyConversionComboBox.Enabled = p_status;
      this.ConsolidationOptionComboBox.Enabled = p_status;
    }
  }
}
