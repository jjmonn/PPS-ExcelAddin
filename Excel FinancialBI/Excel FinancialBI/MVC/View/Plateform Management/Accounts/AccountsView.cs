using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualBasic;

using VIBlend.WinForms.Controls;

namespace FBI.MVC.View
{
  using Controller;
  using Utils;
  using Forms;
  using Model;
  using Network;
  using Model.CRUD;
  using Utils.BNF;

  public partial class AccountsView : UserControl, IView
  {

    #region Variables

    private RightManager m_rightMgr = new RightManager();

    private AccountController m_controller = null;
    private FbiTreeView<Account> m_accountTV = null;
    private FbiTreeView<GlobalFact> m_globalFactsTV = null;
    private vTreeNode m_currentNode = null;

    private SafeDictionary<Account.FormulaTypes, ListItem> m_formulasTypesIdItemDict = new SafeDictionary<Account.FormulaTypes, ListItem>();
    private SafeDictionary<Account.AccountType, ListItem> m_formatsIdItemDict = new SafeDictionary<Account.AccountType, ListItem>();
    private SafeDictionary<Account.ConversionOptions, ListItem> m_currenciesConversionIdItemDict = new SafeDictionary<Account.ConversionOptions, ListItem>();
    private SafeDictionary<Account.ConsolidationOptions, ListItem> m_consoOptionIdItemDict = new SafeDictionary<Account.ConsolidationOptions, ListItem>();
    private SafeDictionary<Account.AccountProcess, ListItem> m_processIdItemDict = new SafeDictionary<Account.AccountProcess, ListItem>();
    private bool m_dragAndDropFlag = false;
    private bool m_isDisplayingAccountFlag = false;
    private bool m_isEditingFormulaFlag = false;
    private string m_saveFormula = "";

    private SimpleBnf m_bnf = new SimpleBnf();
    private FbiGrammar m_grammar = new FbiGrammar();

    #endregion

    #region Initialize

    public AccountsView()
    {
      InitializeComponent();
    }
    
    public void SetController(IController p_controller)
    {
      this.m_controller = p_controller as AccountController;
    }

    public void InitView()
    {
      try
      {
        this.m_accountTV = new FbiTreeView<Account>(AccountModel.Instance.GetDictionary(), null, true);
        this.m_globalFactsTV = new FbiTreeView<GlobalFact>(GlobalFactModel.Instance.GetDictionary());
      }
      catch (Exception e)
      {
        MessageBox.Show(Local.GetValue("CUI.msg_error_system"), Local.GetValue("general.accounts"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        Debug.WriteLine(e.Message + e.StackTrace);
      }

      this.AccountsTVInit();
      this.GlobalFactsTVInit();
      this.ComboBoxesInit();

      this.InitGeneralEvent();

      this.DefineUIPermissions();
      this.DesactivateUnallowed();
      
      this.MultilangueSetup();
      this.SetFormulaEditionState(false);
    }

    private void InitGeneralEvent()
    {
      AccountModel.Instance.UpdateEvent += OnModelUpdate;
      AccountModel.Instance.ReadEvent += OnModelRead;
      AccountModel.Instance.CreationEvent += OnAccountModelCreation;
      AccountModel.Instance.DeleteEvent += OnModelDelete;

      GlobalFactModel.Instance.UpdateEvent += OnGlobalFactModelUpdate;
      GlobalFactModel.Instance.ReadEvent += OnGlobalFactModelRead;
      GlobalFactModel.Instance.CreationEvent += OnGlobalFactModelCreation;
      GlobalFactModel.Instance.DeleteEvent += OnGlobalFactModelDelete;

      this.AddCategoryToolStripMenuItem.Click += OnAddCategoryClick;
      this.CreateANewCategoryToolStripMenuItem.Click += OnAddCategoryClick;
      this.SaveDescriptionBT.Click += OnSaveDescriptionBTClick;
      this.DeleteAccountToolStripMenuItem.Click += OnCheckDeleteAccount;
      this.DeleteAccountToolStripMenuItem1.Click += OnCheckDeleteAccount;
      this.Name_TB.KeyDown += OnNameTextBoxKeyDown;
      this.CreateANewAccountToolStripMenuItem.Click += OnNewAccountClick;
      this.AddSubAccountToolStripMenuItem.Click += OnNewAccountClick;
      this.m_formulaEditionButton.Click += OnFormulaEditionButtonClick;
      this.m_cancelFormulaEditionButton.Click += OnCancelFormulaEditionButtonClick;
      this.m_validateFormulaButton.Click += OnValidateFormulaButtonClick;
      this.m_allocationKeyButton.Click += OnAllocationKeyButtonClick;

      this.m_accountTV.MouseDown += OnAccountsTreeviewMouseDown;
      this.m_accountTV.NodeDropped += OnAccountsTreeviewNodeDropped;

    }

    private void DefineUIPermissions()
    {
      this.m_rightMgr[SaveDescriptionBT] = Group.Permission.EDIT_ACCOUNT;
      this.m_rightMgr[Name_TB] = Group.Permission.EDIT_ACCOUNT;
      this.m_rightMgr[ProcessComboBox] = Group.Permission.EDIT_ACCOUNT;
      this.m_rightMgr[FormulaTypeComboBox] = Group.Permission.EDIT_ACCOUNT;
      this.m_rightMgr[TypeComboBox] = Group.Permission.EDIT_ACCOUNT;
      this.m_rightMgr[CurrencyConversionComboBox] = Group.Permission.EDIT_ACCOUNT;
      this.m_rightMgr[m_descriptionTextBox] = Group.Permission.EDIT_ACCOUNT;
      this.m_rightMgr[ConsolidationOptionComboBox] = Group.Permission.EDIT_ACCOUNT;
      this.m_rightMgr[AddSubAccountToolStripMenuItem] = Group.Permission.CREATE_ACCOUNT;
      this.m_rightMgr[AddCategoryToolStripMenuItem] = Group.Permission.CREATE_ACCOUNT;
      this.m_rightMgr[DeleteAccountToolStripMenuItem] = Group.Permission.DELETE_ACCOUNT;
      this.m_rightMgr[DeleteAccountToolStripMenuItem1] = Group.Permission.DELETE_ACCOUNT;
      this.m_rightMgr[m_validateFormulaButton] = Group.Permission.EDIT_ACCOUNT;
      this.m_rightMgr[m_formulaEditionButton] = Group.Permission.EDIT_ACCOUNT;
      this.m_rightMgr[CreateANewAccountToolStripMenuItem] = Group.Permission.CREATE_ACCOUNT;
      this.m_rightMgr[CreateANewCategoryToolStripMenuItem] = Group.Permission.CREATE_ACCOUNT;
    }

    private void AccountsTVInit()
    {
      this.m_accountTV.ContextMenuStrip = TVRCM;
      this.m_accountTV.Dock = DockStyle.Fill;
      this.m_accountTV.AllowDrop = true;
      this.m_accountTV.LabelEdit = false;
      this.m_accountTV.CollapseAll();
      this.m_accountTV.ImageList = this.accountsIL;
      this.m_accountTV.BorderColor = Color.Transparent;
      this.AccountsTVPanel.Controls.Add(m_accountTV);

      this.m_accountTV.KeyDown += OnAccountTVKeyDown;
      this.m_accountTV.MouseDown += OnAccountTVMouseDown;
      this.m_accountTV.AfterSelect += OnAccountTVAfterSelect;
      this.m_accountTV.MouseDoubleClick += OnAccountTVMouseDoubleClick;
    }

    private void MultilangueSetup()
    {
      this.m_ProcessLabel.Text = Local.GetValue("process.process_selection");
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

    private void GlobalFactsTVInit()
    {
      this.m_globalFactsTV.Dock = DockStyle.Fill;
      this.m_globalFactsTV.LabelEdit = false;
      this.m_globalFactsTV.CollapseAll();
      this.m_globalFactsTV.ImageList = m_globalFactsImageList;
      this.GlobalFactsPanel.Controls.Add(m_globalFactsTV);

      this.m_globalFactsTV.MouseDoubleClick += OnGlobalFactsTVMouseDoubleClick;
    }

    private void ComboBoxesInit()
    {
      //Process
      ListItem l_financialListItem = new ListItem();
      l_financialListItem.Text = Local.GetValue("process.process_financial");
      l_financialListItem.Value = Account.AccountProcess.FINANCIAL;
      this.ProcessComboBox.Items.Add(l_financialListItem);
      this.m_processIdItemDict[(Account.AccountProcess)l_financialListItem.Value] = l_financialListItem;

      ListItem l_rhListItem = new ListItem();
      l_rhListItem.Text = Local.GetValue("process.process_rh");
      l_rhListItem.Value = Account.AccountProcess.RH;
      this.ProcessComboBox.Items.Add(l_rhListItem);
      this.m_processIdItemDict[(Account.AccountProcess)l_rhListItem.Value] = l_rhListItem;

      this.ProcessComboBox.SelectedIndex = 0;
      this.ProcessComboBox.DropDownList = true;
      this.ProcessComboBox.SelectedItemChanged += OnProcessComboBoxSelectedItemChanged;

      //Formula Type
      ListItem l_inputListItem = new ListItem();
      l_inputListItem.Text = Local.GetValue("accounts_edition.formula_type_input");
      l_inputListItem.Value = Account.FormulaTypes.HARD_VALUE_INPUT;
      this.FormulaTypeComboBox.Items.Add(l_inputListItem);
      this.m_formulasTypesIdItemDict[(Account.FormulaTypes)l_inputListItem.Value] = l_inputListItem;

      ListItem l_formulaListItem = new ListItem();
      l_formulaListItem.Text = Local.GetValue("accounts_edition.formula_type_formula");
      l_formulaListItem.Value = Account.FormulaTypes.FORMULA;
      this.FormulaTypeComboBox.Items.Add(l_formulaListItem);
      this.m_formulasTypesIdItemDict[(Account.FormulaTypes)l_formulaListItem.Value] = l_formulaListItem;

      ListItem l_aggregationListItem = new ListItem();
      l_aggregationListItem.Text = Local.GetValue("accounts_edition.formula_type_sub");
      l_aggregationListItem.Value = Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS;
      this.FormulaTypeComboBox.Items.Add(l_aggregationListItem);
      this.m_formulasTypesIdItemDict[(Account.FormulaTypes)l_aggregationListItem.Value] = l_aggregationListItem;

      ListItem l_firstPeriodInputListItem = new ListItem();
      l_firstPeriodInputListItem.Text = Local.GetValue("accounts_edition.formula_type_first");
      l_firstPeriodInputListItem.Value = Account.FormulaTypes.FIRST_PERIOD_INPUT;
      this.FormulaTypeComboBox.Items.Add(l_firstPeriodInputListItem);
      this.m_formulasTypesIdItemDict[(Account.FormulaTypes)l_firstPeriodInputListItem.Value] = l_firstPeriodInputListItem;

      ListItem l_titleListItem = new ListItem();
      l_titleListItem.Text = Local.GetValue("accounts_edition.formula_type_title");
      l_titleListItem.Value = Account.FormulaTypes.TITLE;
      this.FormulaTypeComboBox.Items.Add(l_titleListItem);
      this.m_formulasTypesIdItemDict[(Account.FormulaTypes)l_titleListItem.Value] = l_titleListItem;

      this.FormulaTypeComboBox.SelectedIndex = 0;
      this.FormulaTypeComboBox.DropDownList = true;
      this.FormulaTypeComboBox.SelectedItemChanged += OnFormulaTypeComboBoxSelectedItemChanged;

      // Account Type
      ListItem l_monetaryFormatLI = new ListItem();
      l_monetaryFormatLI.Text = Local.GetValue("accounts_edition.account_type_monetary");
      l_monetaryFormatLI.Value = Account.AccountType.MONETARY;
      this.TypeComboBox.Items.Add(l_monetaryFormatLI);
      this.m_formatsIdItemDict[(Account.AccountType)l_monetaryFormatLI.Value] = l_monetaryFormatLI;

      ListItem l_normalFormatLI = new ListItem();
      l_normalFormatLI.Text = Local.GetValue("accounts_edition.account_type_number");
      l_normalFormatLI.Value = Account.AccountType.NUMBER;
      this.TypeComboBox.Items.Add(l_normalFormatLI);
      this.m_formatsIdItemDict[(Account.AccountType)l_normalFormatLI.Value] = l_normalFormatLI;

      ListItem l_percentageFormatLI = new ListItem();
      l_percentageFormatLI.Text = Local.GetValue("accounts_edition.account_type_percentage");
      l_percentageFormatLI.Value = Account.AccountType.PERCENTAGE;
      this.TypeComboBox.Items.Add(l_percentageFormatLI);
      this.m_formatsIdItemDict[(Account.AccountType)l_percentageFormatLI.Value] = l_percentageFormatLI;

      ListItem l_dateFormatLI = new ListItem();
      l_dateFormatLI.Text = Local.GetValue("accounts_edition.account_type_date");
      l_dateFormatLI.Value = Account.AccountType.DATE_;
      this.TypeComboBox.Items.Add(l_dateFormatLI);
      this.m_formatsIdItemDict[(Account.AccountType)l_dateFormatLI.Value] = l_dateFormatLI;

      this.TypeComboBox.SelectedIndex = 0;
      this.TypeComboBox.DropDownList = true;
      this.TypeComboBox.SelectedItemChanged += OnTypeComboBoxSelectedItemChanged;

      // Currencies Conversion
      ListItem l_averageRateLI = new ListItem();
      l_averageRateLI.Text = Local.GetValue("accounts_edition.currencies_type_average");
      l_averageRateLI.Value = Account.ConversionOptions.AVERAGE_RATE;
      this.CurrencyConversionComboBox.Items.Add(l_averageRateLI);
      this.m_currenciesConversionIdItemDict[(Account.ConversionOptions)l_averageRateLI.Value] = l_averageRateLI;

      ListItem l_endOfPeriodRateLI = new ListItem();
      l_endOfPeriodRateLI.Text = Local.GetValue("accounts_edition.currencies_type_end");
      l_endOfPeriodRateLI.Value = Account.ConversionOptions.END_OF_PERIOD_RATE;
      this.CurrencyConversionComboBox.Items.Add(l_endOfPeriodRateLI);
      this.m_currenciesConversionIdItemDict[(Account.ConversionOptions)l_endOfPeriodRateLI.Value] = l_endOfPeriodRateLI;

      this.CurrencyConversionComboBox.SelectedIndex = 0;
      this.CurrencyConversionComboBox.DropDownList = true;
      this.CurrencyConversionComboBox.SelectedItemChanged += OnCurrencyConversionComboBoxSelectedItemChanged;

      // Consolidation Option
      ListItem l_aggregatedLI = new ListItem();
      l_aggregatedLI.Text = Local.GetValue("accounts_edition.consolidation_type_aggregated");
      l_aggregatedLI.Value = Account.ConsolidationOptions.AGGREGATION;
      this.ConsolidationOptionComboBox.Items.Add(l_aggregatedLI);
      this.m_consoOptionIdItemDict[(Account.ConsolidationOptions)l_aggregatedLI.Value] = l_aggregatedLI;

      ListItem l_recomputedLI = new ListItem();
      l_recomputedLI.Text = Local.GetValue("accounts_edition.consolidation_type_recomputed");
      l_recomputedLI.Value = Account.ConsolidationOptions.RECOMPUTATION;
      this.ConsolidationOptionComboBox.Items.Add(l_recomputedLI);
      this.m_consoOptionIdItemDict[(Account.ConsolidationOptions)l_recomputedLI.Value] = l_recomputedLI;

      ListItem l_noneLI = new ListItem();
      l_noneLI.Text = Local.GetValue("accounts_edition.consolidation_type_none");
      l_noneLI.Value = Account.ConsolidationOptions.NONE;
      this.ConsolidationOptionComboBox.Items.Add(l_noneLI);
      this.m_consoOptionIdItemDict[(Account.ConsolidationOptions)l_noneLI.Value] = l_noneLI;

      this.ConsolidationOptionComboBox.SelectedIndex = 0;
      this.ConsolidationOptionComboBox.DropDownList = true;
      this.ConsolidationOptionComboBox.SelectedItemChanged += OnConsolidationOptionComboBoxSelectedItemChanged;
    }

    #endregion

    #region Events

    #region Account

    #region Server

    delegate void OnAccountModelCreation_delegate(ErrorMessage p_status, uint p_id);
    private void OnAccountModelCreation(ErrorMessage p_status, uint p_id)
    {
      if (InvokeRequired)
      {
        OnAccountModelCreation_delegate func = new OnAccountModelCreation_delegate(OnAccountModelCreation);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status != ErrorMessage.SUCCESS)
        {
          MessageBox.Show(Local.GetValue("accounts_edition.msg_error_creation"));
        }
      }
    }

    delegate void OnModelRead_delegate(ErrorMessage p_status, Account p_attributes);
    private void OnModelRead(ErrorMessage p_status, Account p_attributes)
    {
      if (InvokeRequired)
      {
        OnModelRead_delegate func = new OnModelRead_delegate(OnModelRead);
        Invoke(func, p_status, p_attributes);
      }
      else
      {
        if (this.m_accountTV.FindNode(p_attributes.Id) != null)
        {
          vTreeNode l_oldNode = this.m_accountTV.FindNode(p_attributes.Id);
          l_oldNode.Text = p_attributes.Name;
          l_oldNode.ImageIndex = (Int32)p_attributes.Image;
          if (this.m_currentNode == l_oldNode)
          {
            this.DesactivateUnallowed();
            this.DisplayAttributes();
          }
        }
        else
        {
          vTreeNode l_node = new vTreeNode();
          l_node.Value = p_attributes.Id;
          l_node.Text = p_attributes.Name;
          l_node.ImageIndex = (Int32)p_attributes.Image;
          if (p_attributes.ParentId == 0)
            this.m_accountTV.Add(l_node);
          else
          {
            vTreeNode l_parent = this.m_accountTV.FindNode(p_attributes.ParentId);
            if (l_parent != null)
              l_parent.Nodes.Add(l_node);
          }
        }
      }
    }

    delegate void OnModelUpdate_delegate(ErrorMessage p_status, uint p_id);
    private void OnModelUpdate(ErrorMessage p_status, uint p_id)
    {
      if (InvokeRequired)
      {
        OnModelUpdate_delegate func = new OnModelUpdate_delegate(OnModelUpdate);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status != Network.ErrorMessage.SUCCESS)
        {
          MessageBox.Show(Local.GetValue("acounts_edition.msg_error_update"));
        }
      }
    }

    delegate void OnModelDelete_delegate(ErrorMessage p_status, uint p_id);
    private void OnModelDelete(ErrorMessage p_status, uint p_id)
    {
      if (InvokeRequired)
      {
        OnModelDelete_delegate func = new OnModelDelete_delegate(OnModelDelete);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status != Network.ErrorMessage.SUCCESS)
        {
          MessageBox.Show(Local.GetValue("global_facts.msg_error_creation"));
          return;
        }
        vTreeNode l_toDelete = this.m_accountTV.FindNode(p_id);
        if (l_toDelete == this.m_currentNode)
        {
          this.m_currentNode = null;
          this.m_accountTV.SelectedNode = null;
          this.SetEnableStatusEdition(false, true, null);
        }
        if (l_toDelete != null)
        {
          if (l_toDelete.Parent == null)
            this.m_accountTV.Nodes.Remove(l_toDelete);
          else
            l_toDelete.Parent.Nodes.Remove(l_toDelete);
        }
      }
    }

    #endregion

    #region Others

    private void OnCheckDeleteAccount(object p_sender, EventArgs p_e)
    {
      if (this.m_accountTV.SelectedNode != null)
        this.m_controller.DeleteAccount(this.m_accountTV.SelectedNode);
    }

    private void OnNameTextBoxKeyDown(object p_sender, KeyEventArgs p_e)
    {
      if (p_e.KeyCode == Keys.Enter)
      {
        if (m_currentNode != null)
        {
          Account l_currentAccount;

          if ((l_currentAccount = AccountModel.Instance.GetValue((UInt32)this.m_currentNode.Value)) != null)
          {
            l_currentAccount = l_currentAccount.Clone();
            l_currentAccount.Name = Name_TB.Text;
            this.m_controller.UpdateAccount(l_currentAccount);
          }
        }
      }
    }

    #endregion

    #region Click

    private void OnAllocationKeyButtonClick(object p_sender, EventArgs p_e)
    {
      if (this.m_accountTV.SelectedNode != null)
        this.m_controller.CreateAllocationKeysView(this.m_accountTV.SelectedNode);
    }

    private void OnValidateFormulaButtonClick(object p_sender, EventArgs p_e)
    {
      if (this.m_formulaTextBox.Text == "")
      {
        if (MessageBox.Show(Local.GetValue("accounts_edition.msg_formula_empty"), Local.GetValue("general.accounts"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
          return;
      }
      else
        if (MessageBox.Show(Local.GetValue("accounts_edition.msg_formula_validation_confirmation"), Local.GetValue("general.accounts"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
          return;
      if (m_currentNode != null)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue((UInt32)this.m_currentNode.Value)) != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          if (this.m_formulaTextBox.Text == "")
          {
            l_currentAccount.Formula = m_grammar.Formula;
            this.m_controller.UpdateAccount(l_currentAccount);
            this.SetEditingFormulaUI(false);
            return;
          }
          m_bnf.AddRule("fbi_to_grammar", m_grammar.ToGrammar);
          if (m_bnf.Parse("fbi_to_grammar", m_formulaTextBox.Text))
          {
            l_currentAccount.Formula = m_grammar.Formula;
            this.m_controller.UpdateAccount(l_currentAccount);
            this.SetEditingFormulaUI(false);
          }
          else
            MessageBox.Show(m_grammar.LastError);
        }
      }
    }

    private void OnCancelFormulaEditionButtonClick(object p_sender, EventArgs p_e)
    {
      this.m_formulaTextBox.Text = this.m_saveFormula;
      this.SetEditingFormulaUI(false);
    }

    private void OnFormulaEditionButtonClick(object p_sender, EventArgs p_e)
    {
      this.m_saveFormula = this.m_formulaTextBox.Text;
      this.SetEditingFormulaUI(true);
    }

    private void OnNewAccountClick(object p_sender, EventArgs p_e)
    {
      this.m_controller.CreateNewUI(this.m_accountTV.SelectedNode);
    }

    private void OnAddCategoryClick(object p_sender, EventArgs p_e)
    {
      m_isEditingFormulaFlag = false;

      string l_nameAccount = Interaction.InputBox(Local.GetValue("accounts_edition.msg_new_tab_name"),
                                                 Local.GetValue("accounts_edition.title_new_tab_name"), "");
      if (l_nameAccount != "")
      {
        if (this.m_controller.AccountNameCheck(l_nameAccount))
          this.m_controller.CreateAccount(0, l_nameAccount, Account.AccountProcess.FINANCIAL, Account.FormulaTypes.TITLE, "", Account.AccountType.DATE_, Account.ConsolidationOptions.AGGREGATION,
            Account.PeriodAggregationOptions.AVERAGE_PERIOD, "t", (UInt32)Account.FormulaTypes.TITLE, this.m_accountTV.Nodes.Count);
        else
          MessageBox.Show(Local.GetValue("accounts_edition.msg_error_create"));
      }
      else
        MessageBox.Show(Local.GetValue("accounts_edition.msg_error_create"));
    }

    private void OnSaveDescriptionBTClick(object p_sender, EventArgs p_e)
    {
      if (m_currentNode != null)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue((UInt32)this.m_currentNode.Value)) != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          l_currentAccount.Description = m_descriptionTextBox.Text;
          this.m_controller.UpdateAccount(l_currentAccount);
        }
      }
    }

    #endregion

    #region TreeView

    private void OnAccountTVMouseDoubleClick(object p_sender, EventArgs p_e)
    {
      if (this.m_isEditingFormulaFlag == true)
      {
        vTreeNode l_node = this.m_accountTV.FindAtPosition(((MouseEventArgs)p_e).Location);
        if (l_node != null)
          this.m_formulaTextBox.Text += "\"" + l_node.Text + "\"";
      }
    }

    private void OnAccountTVKeyDown(object p_sender, KeyEventArgs p_e)
    {
      switch (p_e.KeyCode)
      {
        case Keys.Down:
          if (p_e.Control == true)
          {
            this.m_accountTV.MoveNodeDown(this.m_accountTV.SelectedNode);
            this.m_currentNode = this.m_accountTV.SelectedNode;
          }
          break;
        case Keys.Up:
          if (p_e.Control == true)
          {
            this.m_accountTV.MoveNodeUp(this.m_accountTV.SelectedNode);
            this.m_currentNode = this.m_accountTV.SelectedNode;
          }
          break;
        case Keys.Delete:
          OnCheckDeleteAccount(p_sender, p_e);
          break;
      }
    }

    private void OnAccountTVMouseDown(object p_sender, MouseEventArgs p_e)
    {
      if (p_e.Button == MouseButtons.Right)
      {
        TVRCM.Visible = false;
        TVRCM.Show(p_e.Location);
        TVRCM.Visible = true;
      }
    }

    private void OnAccountTVAfterSelect(object p_sender, vTreeViewEventArgs p_e)
    {
      if (this.m_dragAndDropFlag == false && this.m_isEditingFormulaFlag == false
        && m_isDisplayingAccountFlag == false)
      {
        this.m_currentNode = p_e.Node;
        if (this.m_currentNode != null)
        {
          this.DesactivateUnallowed();
          this.DisplayAttributes();
        }
        else
          this.m_accountTV.Capture = false;
      }
      else
        ((FbiTreeView<Account>)p_sender).SelectedNode = this.m_currentNode;
    }

    private void OnAccountsTreeviewMouseDown(object p_sender, MouseEventArgs p_e)
    {
      if (m_accountTV.FindAtPosition(new Point(p_e.X, p_e.Y)) != null)
        m_currentNode = m_accountTV.FindAtPosition(new Point(p_e.X, p_e.Y));
      if (m_currentNode != null && ModifierKeys.HasFlag(Keys.Control) == true)
        m_accountTV.DoDragDrop(m_currentNode, DragDropEffects.Move);
    }

    private void OnAccountsTreeviewNodeDropped(vTreeNode p_draggedNode, vTreeNode p_targetNode)
    {
      if (p_targetNode == null)
        return;

      Account l_targetAccount = AccountModel.Instance.GetValue((UInt32)p_targetNode.Value);

      if (p_draggedNode.Equals(p_targetNode) == true || p_draggedNode.Parent.Equals(p_targetNode.Value))
        return;

      Account l_account;

      if ((l_account = AccountModel.Instance.GetValue((UInt32)p_draggedNode.Value)) == null)
        return;
      l_account = l_account.Clone();
      if (l_account == null)
        return;

      vTreeNode l_newNode = new vTreeNode();
      l_newNode.Value = p_draggedNode.Value;
      l_newNode.Text = p_draggedNode.Text;
      l_newNode.ImageIndex = p_draggedNode.ImageIndex;
      p_draggedNode.Remove();
      m_accountTV.DoDragDrop(p_draggedNode, DragDropEffects.None);
      p_targetNode.Nodes.Add(l_newNode);
      l_account.ParentId = (UInt32)p_targetNode.Value;
      m_controller.UpdateAccount(l_account);
    }

    #endregion

    #region ComboBox

    private void OnProcessComboBoxSelectedItemChanged(object p_sender, EventArgs p_e)
    {
      if (this.m_isDisplayingAccountFlag == false && m_currentNode != null)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue((UInt32)this.m_currentNode.Value)) != null && ((vComboBox)p_sender).SelectedItem != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          l_currentAccount.Process = (Account.AccountProcess)((vComboBox)p_sender).SelectedItem.Value;
          this.m_controller.UpdateAccount(l_currentAccount);
        }
      }
    }

    private void OnTypeComboBoxSelectedItemChanged(object p_sender, EventArgs p_e)
    {
      if (m_currentNode == null)
        return;
      if (this.m_isDisplayingAccountFlag == false)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue((UInt32)this.m_currentNode.Value)) != null && ((vComboBox)p_sender).SelectedItem != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          l_currentAccount.Type = (Account.AccountType)((vComboBox)p_sender).SelectedItem.Value;
          this.m_controller.UpdateAccount(l_currentAccount);
        }
      }
      if ((Account.AccountType)((vComboBox)p_sender).SelectedItem.Value == Account.AccountType.MONETARY)
      {
        this.CurrencyConversionComboBox.Visible = true;
        this.m_accountCurrenciesConversionLabel.Visible = true;
      }
      else
      {
        this.CurrencyConversionComboBox.Visible = false;
        this.m_accountCurrenciesConversionLabel.Visible = false;
      }
    }

    private void OnCurrencyConversionComboBoxSelectedItemChanged(object p_sender, EventArgs p_e)
    {
      if (this.m_isDisplayingAccountFlag == false && m_currentNode != null)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue((UInt32)this.m_currentNode.Value)) != null && ((vComboBox)p_sender).SelectedItem != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          l_currentAccount.ConversionOptionId = (Account.ConversionOptions)((vComboBox)p_sender).SelectedItem.Value;
          this.m_controller.UpdateAccount(l_currentAccount);
        }
      }
    }

    private void OnConsolidationOptionComboBoxSelectedItemChanged(object p_sender, EventArgs p_e)
    {
      if (this.m_isDisplayingAccountFlag == false && m_currentNode != null)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue((UInt32)this.m_currentNode.Value)) != null && ((vComboBox)p_sender).SelectedItem != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          l_currentAccount.ConsolidationOptionId = (Account.ConsolidationOptions)((vComboBox)p_sender).SelectedItem.Value;
          this.m_controller.UpdateAccount(l_currentAccount);
        }
      }
    }

    private void OnFormulaTypeComboBoxSelectedItemChanged(object p_sender, EventArgs p_e)
    {
      if (this.m_isDisplayingAccountFlag == false && m_currentNode != null)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue((UInt32)this.m_currentNode.Value)) != null && ((vComboBox)p_sender).SelectedItem != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          Account.FormulaTypes l_value = (Account.FormulaTypes)((vComboBox)p_sender).SelectedItem.Value;

          if (l_currentAccount.FormulaType == Account.FormulaTypes.HARD_VALUE_INPUT || l_currentAccount.FormulaType == Account.FormulaTypes.FIRST_PERIOD_INPUT)
          {
            if (l_value != Account.FormulaTypes.HARD_VALUE_INPUT || l_value != Account.FormulaTypes.FIRST_PERIOD_INPUT)
            {
              string l_result = PasswordBox.Open(Local.GetValue("accounts_edition.msg_password_required"),
                Local.GetValue("cateories.accpunts"));

              if (l_result == PasswordBox.Canceled || l_result != Addin.Password)
              {
                if (l_result != PasswordBox.Canceled)
                  MessageBox.Show(Local.GetValue("accounts_edition.msg_incorrect_password"), Local.GetValue("general.accounts"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.m_controller.UpdateAccount(l_currentAccount);
                return;
              }
            }
          }
          l_currentAccount.FormulaType = l_value;
          this.m_controller.UpdateAccount(l_currentAccount);
        }
      }
    }

    #endregion

    #endregion

    #region GlobalFact

    #region Server

    delegate void OnGlobalFactModelDelete_delegate(ErrorMessage p_status, uint p_id);
    private void OnGlobalFactModelDelete(ErrorMessage p_status, uint p_id)
    {
      if (InvokeRequired)
      {
        OnGlobalFactModelDelete_delegate func = new OnGlobalFactModelDelete_delegate(OnGlobalFactModelDelete);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status != Network.ErrorMessage.SUCCESS)
        {
          MessageBox.Show(Local.GetValue("global_facts.msg_error_delete"));
          return;
        }
        vTreeNode l_toDelete = this.m_globalFactsTV.FindNode(p_id);
        if (l_toDelete == this.m_globalFactsTV.SelectedNode)
          this.m_globalFactsTV.SelectedNode = null;
        if (l_toDelete != null)
          if (l_toDelete.Parent == null)
            this.m_globalFactsTV.Nodes.Remove(l_toDelete);
      }
    }

    delegate void OnGlobalFactModelCreation_delegate(ErrorMessage p_status, uint p_id);
    private void OnGlobalFactModelCreation(ErrorMessage p_status, uint p_id)
    {
      if (InvokeRequired)
      {
        OnGlobalFactModelCreation_delegate func = new OnGlobalFactModelCreation_delegate(OnGlobalFactModelCreation);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status != Network.ErrorMessage.SUCCESS)
        {
          MessageBox.Show(Local.GetValue("global_facts.msg_error_creation"));
        }
      }
    }

    delegate void OnGlobalFactModelRead_delegate(ErrorMessage p_status, GlobalFact p_attributes);
    private void OnGlobalFactModelRead(ErrorMessage p_status, GlobalFact p_attributes)
    {
      if (InvokeRequired)
      {
        OnGlobalFactModelRead_delegate func = new OnGlobalFactModelRead_delegate(OnGlobalFactModelRead);
        Invoke(func, p_status, p_attributes);
      }
      else
      {
        if (p_status != ErrorMessage.SUCCESS)
        {
          MessageBox.Show(Local.GetValue("global_facts.msg_unknown"));
        }
        else
        {
          if (this.m_globalFactsTV.FindNode(p_attributes.Id) != null)
          {
            vTreeNode l_oldNode = this.m_globalFactsTV.FindNode(p_attributes.Id);
            l_oldNode.Text = p_attributes.Name;
            l_oldNode.ImageIndex = (Int32)p_attributes.Image;
          }
          else
          {
            vTreeNode l_node = new vTreeNode();
            l_node.Value = p_attributes.Id;
            l_node.Text = p_attributes.Name;
            l_node.ImageIndex = (Int32)p_attributes.Image;
            this.m_globalFactsTV.Nodes.Add(l_node);
          }
        }
      }
    }

    delegate void OnGlobalFactModelUpdate_delegate(ErrorMessage p_status, uint p_id);
    private void OnGlobalFactModelUpdate(ErrorMessage p_status, uint p_id)
    {
      if (InvokeRequired)
      {
        OnGlobalFactModelUpdate_delegate func = new OnGlobalFactModelUpdate_delegate(OnGlobalFactModelUpdate);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status != Network.ErrorMessage.SUCCESS)
        {
          MessageBox.Show(Local.GetValue("accounts_edition.msg_error_creation"));
        }
      }
    }

    #endregion

    private void OnGlobalFactsTVMouseDoubleClick(object p_sender, MouseEventArgs p_e)
    {
      if (this.m_isEditingFormulaFlag == true)
      {
        vTreeNode l_node = this.m_accountTV.FindAtPosition(((MouseEventArgs)p_e).Location);
        if (l_node != null)
          this.m_formulaTextBox.Text += "\"" + l_node.Text + "\"";
      }
    }

    #endregion

    #endregion

    #region Utils

    private void SetFormulaEditionState(Boolean p_state)
    {
      this.m_isEditingFormulaFlag = p_state;
      this.m_formulaTextBox.Enabled = p_state;
      this.m_validateFormulaButton.Visible = p_state;
      this.m_cancelFormulaEditionButton.Visible = p_state;
      this.m_formulaEditionButton.Visible = p_state;
      if (p_state == true)
        this.m_formulaTextBox.BackColor = Color.White;
      else
        this.m_formulaTextBox.BackColor = Color.LightGray;
    }

    private void DisplayAttributes()
    {
      if ((this.m_currentNode != null) && this.m_isEditingFormulaFlag == false)
      {
        Account l_account = AccountModel.Instance.GetValue((UInt32)this.m_currentNode.Value);

        if (l_account == null)
          return;

        this.m_isDisplayingAccountFlag = true;

        //Name
        this.Name_TB.Text = m_currentNode.Text;

        //Process
        if (this.m_processIdItemDict.ContainsKey(l_account.Process))
        {
          ListItem l_processLI = m_processIdItemDict[l_account.Process];
          this.ProcessComboBox.SelectedItem = l_processLI;
        }

        //Formula Type
        if (this.m_formulasTypesIdItemDict.ContainsKey(l_account.FormulaType))
        {
          ListItem l_formulaTypeLI = m_formulasTypesIdItemDict[l_account.FormulaType];
          this.FormulaTypeComboBox.SelectedItem = l_formulaTypeLI;

          bool l_isRootAccount = false;
          if (this.m_currentNode.Parent == null)
            l_isRootAccount = true;
          if ((Account.FormulaTypes)l_formulaTypeLI.Value == Account.FormulaTypes.TITLE)
            this.SetEnableStatusEdition(false, l_isRootAccount, l_account);
          else
            this.SetEnableStatusEdition(true, l_isRootAccount, l_account);
        }

        if (this.m_formatsIdItemDict.ContainsKey(l_account.Type))
        {
          ListItem l_formatLI = this.m_formatsIdItemDict[l_account.Type];
          this.TypeComboBox.SelectedItem = l_formatLI;
          if ((Account.AccountType)l_formatLI.Value == Account.AccountType.MONETARY)
          {
            if (this.m_currenciesConversionIdItemDict.ContainsKey(l_account.ConversionOptionId))
            {
              ListItem conversionLI = this.m_currenciesConversionIdItemDict[l_account.ConversionOptionId];
              this.CurrencyConversionComboBox.SelectedItem = conversionLI;
            }
          }
        }

        //Consolidation
        if (this.m_consoOptionIdItemDict.ContainsKey(l_account.ConsolidationOptionId))
        {
          ListItem consolidationLI = this.m_consoOptionIdItemDict[l_account.ConsolidationOptionId];
          this.ConsolidationOptionComboBox.SelectedItem = consolidationLI;
        }

        // Formula TB
        m_formulaTextBox.Text = "";
        m_bnf.AddRule("fbi_to_human_grammar", m_grammar.ToHuman);
        if (m_bnf.Parse("fbi_to_human_grammar", l_account.Formula))
          m_formulaTextBox.Text = m_grammar.Formula;
        else
          MessageBox.Show(m_grammar.LastError);

        //Description
        this.m_descriptionTextBox.Text = l_account.Description;

        this.m_isDisplayingAccountFlag = false;
      }
    }

    private void SetEnableStatusEdition(Boolean p_status, Boolean p_isRootAccount, Account p_account)
    {
      if (p_isRootAccount == true)
        this.FormulaTypeComboBox.Enabled = false;
      if (p_account != null)
      {
        if (p_account.FormulaType == Account.FormulaTypes.FORMULA || p_account.FormulaType == Account.FormulaTypes.FIRST_PERIOD_INPUT)
          this.m_formulaEditionButton.Visible = true;
        else
          this.m_formulaEditionButton.Visible = false;
      }
      else
        this.m_formulaEditionButton.Visible = false;
      this.m_descriptionTextBox.BackColor = Color.White;
    }

    private void SetAccountUIState(Boolean p_uiState)
    {
      this.SaveDescriptionBT.Enabled = p_uiState;
      this.Name_TB.Enabled = p_uiState;
      this.ProcessComboBox.Enabled = p_uiState;
      this.FormulaTypeComboBox.Enabled = p_uiState;
      this.TypeComboBox.Enabled = p_uiState;
      this.CurrencyConversionComboBox.Enabled = p_uiState;
      this.m_descriptionTextBox.Enabled = p_uiState;
      if (p_uiState == true)
        this.m_descriptionTextBox.BackColor = Color.White;
      else
        this.m_descriptionTextBox.BackColor = Color.LightGray;
      this.ConsolidationOptionComboBox.Enabled = p_uiState;
      this.m_validateFormulaButton.Enabled = p_uiState;
    }

    private void DesactivateUnallowed()
    {
      this.m_rightMgr.Enable(UserModel.Instance.GetCurrentUserRights());
    }

    private void SetEditingFormulaUI(bool p_state)
    {
      this.m_isEditingFormulaFlag = p_state;
      this.m_formulaEditionButton.Visible = !p_state;
      this.m_validateFormulaButton.Visible = p_state;
      this.m_validateFormulaButton.Enabled = p_state;
      this.m_cancelFormulaEditionButton.Visible = p_state;
      this.m_cancelFormulaEditionButton.Enabled = p_state;
      this.m_formulaTextBox.Enabled = p_state;
      if (p_state == false)
        this.m_formulaTextBox.BackColor = Color.LightGray;
      else
        this.m_formulaTextBox.BackColor = Color.White;
    }

    #endregion

  }
}
