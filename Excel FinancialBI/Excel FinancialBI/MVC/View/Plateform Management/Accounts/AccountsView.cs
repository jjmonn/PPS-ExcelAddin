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
      m_controller = p_controller as AccountController;
    }

    public void LoadView()
    {
      try
      {
        m_accountTV = new FbiTreeView<Account>(AccountModel.Instance.GetDictionary(), null, true);
        m_globalFactsTV = new FbiTreeView<GlobalFact>(GlobalFactModel.Instance.GetDictionary());
      }
      catch (Exception e)
      {
        Forms.MsgBox.Show(Local.GetValue("CUI.msg_error_system"), Local.GetValue("general.accounts"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        Debug.WriteLine(e.Message + e.StackTrace);
      }

      AccountsTVInit();
      GlobalFactsTVInit();
      ComboBoxesInit();

      SuscribeEvents();

      DefineUIPermissions();
      DesactivateUnallowed();
      
      MultilangueSetup();
      SetFormulaEditionState(false);
    }

    private void SuscribeEvents()
    {
      Addin.SuscribeAutoLock(this);
      AccountModel.Instance.UpdateEvent += OnModelUpdate;
      AccountModel.Instance.ReadEvent += OnModelRead;
      AccountModel.Instance.CreationEvent += OnAccountModelCreation;
      AccountModel.Instance.DeleteEvent += OnModelDelete;

      GlobalFactModel.Instance.ReadEvent += OnGlobalFactModelRead;
      GlobalFactModel.Instance.DeleteEvent += OnGlobalFactModelDelete;

      AddCategoryToolStripMenuItem.Click += OnAddCategoryClick;
      CreateANewCategoryToolStripMenuItem.Click += OnAddCategoryClick;
      SaveDescriptionBT.Click += OnSaveDescriptionBTClick;
      DeleteAccountToolStripMenuItem.Click += OnCheckDeleteAccount;
      DeleteAccountToolStripMenuItem1.Click += OnCheckDeleteAccount;
      Name_TB.KeyDown += OnNameTextBoxKeyDown;
      CreateANewAccountToolStripMenuItem.Click += OnNewAccountClick;
      AddSubAccountToolStripMenuItem.Click += OnNewAccountClick;
      m_formulaEditionButton.Click += OnFormulaEditionButtonClick;
      m_cancelFormulaEditionButton.Click += OnCancelFormulaEditionButtonClick;
      m_validateFormulaButton.Click += OnValidateFormulaButtonClick;
      m_allocationKeyButton.Click += OnAllocationKeyButtonClick;

      m_accountTV.MouseDown += OnAccountsTreeviewMouseDown;
      m_accountTV.NodeDropped += OnAccountsTreeviewNodeDropped;

    }

    private void DefineUIPermissions()
    {
      m_rightMgr[SaveDescriptionBT] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[Name_TB] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[ProcessComboBox] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[FormulaTypeComboBox] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[TypeComboBox] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[CurrencyConversionComboBox] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[m_descriptionTextBox] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[ConsolidationOptionComboBox] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[AddSubAccountToolStripMenuItem] = Group.Permission.CREATE_ACCOUNT;
      m_rightMgr[AddCategoryToolStripMenuItem] = Group.Permission.CREATE_ACCOUNT;
      m_rightMgr[DeleteAccountToolStripMenuItem] = Group.Permission.DELETE_ACCOUNT;
      m_rightMgr[DeleteAccountToolStripMenuItem1] = Group.Permission.DELETE_ACCOUNT;
      m_rightMgr[m_validateFormulaButton] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[m_formulaEditionButton] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[CreateANewAccountToolStripMenuItem] = Group.Permission.CREATE_ACCOUNT;
      m_rightMgr[CreateANewCategoryToolStripMenuItem] = Group.Permission.CREATE_ACCOUNT;
    }

    private void AccountsTVInit()
    {
      m_accountTV.ContextMenuStrip = TVRCM;
      m_accountTV.Dock = DockStyle.Fill;
      m_accountTV.AllowDrop = true;
      m_accountTV.LabelEdit = false;
      m_accountTV.CollapseAll();
      m_accountTV.ImageList = accountsIL;
      m_accountTV.BorderColor = Color.Transparent;
      AccountsTVPanel.Controls.Add(m_accountTV);

      m_accountTV.KeyDown += OnAccountTVKeyDown;
      m_accountTV.MouseDown += OnAccountTVMouseDown;
      m_accountTV.AfterSelect += OnAccountTVAfterSelect;
      m_accountTV.MouseDoubleClick += OnAccountTVMouseDoubleClick;
    }

    private void MultilangueSetup()
    {
      m_ProcessLabel.Text = Local.GetValue("process.process_selection");
      m_accountDescriptionGroupbox.Text = Local.GetValue("accounts.description");
      SaveDescriptionBT.Text = Local.GetValue("accounts.save_description");
      m_accountFormulaGroupbox.Text = Local.GetValue("accounts.formula");
      m_formulaEditionButton.Text = Local.GetValue("accounts.edit_formula");
      m_validateFormulaButton.Text = Local.GetValue("general.save");
      m_cancelFormulaEditionButton.Text = Local.GetValue("general.cancel");
      m_accountInformationGroupbox.Text = Local.GetValue("accounts.information");
      m_accountNameLabel.Text = Local.GetValue("accounts.name");
      m_accountFormulaTypeLabel.Text = Local.GetValue("accounts.formula_type");
      m_accountTypeLabel.Text = Local.GetValue("accounts.type");
      m_accountConsolidationOptionLabel.Text = Local.GetValue("accounts.consolidation_option");
      m_accountCurrenciesConversionLabel.Text = Local.GetValue("accounts.currencies_conversion");
      m_globalFactsLabel.Text = Local.GetValue("accounts.macro_economic_indicators");
      AddSubAccountToolStripMenuItem.Text = Local.GetValue("accounts.new_account");
      AddCategoryToolStripMenuItem.Text = Local.GetValue("accounts.add_tab_account");
      DeleteAccountToolStripMenuItem.Text = Local.GetValue("accounts.delete_account");
      DropHierarchyToExcelToolStripMenuItem.Text = Local.GetValue("accounts.drop_to_excel");
      NewToolStripMenuItem.Text = Local.GetValue("general.account");
      CreateANewAccountToolStripMenuItem.Text = Local.GetValue("accounts.new_account");
      CreateANewCategoryToolStripMenuItem.Text = Local.GetValue("accounts.add_tab_account");
      DeleteAccountToolStripMenuItem1.Text = Local.GetValue("accounts.delete_account");
      DropAllAccountsHierarchyToExcelToolStripMenuItem.Text = Local.GetValue("accounts.drop_to_excel");
      DropSelectedAccountHierarchyToExcelToolStripMenuItem.Text = Local.GetValue("accounts.drop_selected_hierarchy_to_excel");
      HelpToolStripMenuItem.Text = Local.GetValue("general.help");
    }

    private void GlobalFactsTVInit()
    {
      m_globalFactsTV.Dock = DockStyle.Fill;
      m_globalFactsTV.LabelEdit = false;
      m_globalFactsTV.CollapseAll();
      m_globalFactsTV.ImageList = m_globalFactsImageList;
      GlobalFactsPanel.Controls.Add(m_globalFactsTV);

      m_globalFactsTV.MouseDoubleClick += OnGlobalFactsTVMouseDoubleClick;
    }

    private void ComboBoxesInit()
    {
      //Process
      ListItem l_financialListItem = new ListItem();
      l_financialListItem.Text = Local.GetValue("process.process_financial");
      l_financialListItem.Value = Account.AccountProcess.FINANCIAL;
      ProcessComboBox.Items.Add(l_financialListItem);
      m_processIdItemDict[(Account.AccountProcess)l_financialListItem.Value] = l_financialListItem;

      ListItem l_rhListItem = new ListItem();
      l_rhListItem.Text = Local.GetValue("process.process_rh");
      l_rhListItem.Value = Account.AccountProcess.RH;
      ProcessComboBox.Items.Add(l_rhListItem);
      m_processIdItemDict[(Account.AccountProcess)l_rhListItem.Value] = l_rhListItem;

      ProcessComboBox.SelectedIndex = 0;
      ProcessComboBox.DropDownList = true;
      ProcessComboBox.SelectedItemChanged += OnProcessComboBoxSelectedItemChanged;

      //Formula Type
      ListItem l_inputListItem = new ListItem();
      l_inputListItem.Text = Local.GetValue("accounts.formula_type_input");
      l_inputListItem.Value = Account.FormulaTypes.HARD_VALUE_INPUT;
      FormulaTypeComboBox.Items.Add(l_inputListItem);
      m_formulasTypesIdItemDict[(Account.FormulaTypes)l_inputListItem.Value] = l_inputListItem;

      ListItem l_formulaListItem = new ListItem();
      l_formulaListItem.Text = Local.GetValue("accounts.formula_type_formula");
      l_formulaListItem.Value = Account.FormulaTypes.FORMULA;
      FormulaTypeComboBox.Items.Add(l_formulaListItem);
      m_formulasTypesIdItemDict[(Account.FormulaTypes)l_formulaListItem.Value] = l_formulaListItem;

      ListItem l_aggregationListItem = new ListItem();
      l_aggregationListItem.Text = Local.GetValue("accounts.formula_type_sub");
      l_aggregationListItem.Value = Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS;
      FormulaTypeComboBox.Items.Add(l_aggregationListItem);
      m_formulasTypesIdItemDict[(Account.FormulaTypes)l_aggregationListItem.Value] = l_aggregationListItem;

      ListItem l_firstPeriodInputListItem = new ListItem();
      l_firstPeriodInputListItem.Text = Local.GetValue("accounts.formula_type_first");
      l_firstPeriodInputListItem.Value = Account.FormulaTypes.FIRST_PERIOD_INPUT;
      FormulaTypeComboBox.Items.Add(l_firstPeriodInputListItem);
      m_formulasTypesIdItemDict[(Account.FormulaTypes)l_firstPeriodInputListItem.Value] = l_firstPeriodInputListItem;

      ListItem l_titleListItem = new ListItem();
      l_titleListItem.Text = Local.GetValue("accounts.formula_type_title");
      l_titleListItem.Value = Account.FormulaTypes.TITLE;
      FormulaTypeComboBox.Items.Add(l_titleListItem);
      m_formulasTypesIdItemDict[(Account.FormulaTypes)l_titleListItem.Value] = l_titleListItem;

      FormulaTypeComboBox.SelectedIndex = 0;
      FormulaTypeComboBox.DropDownList = true;
      FormulaTypeComboBox.SelectedItemChanged += OnFormulaTypeComboBoxSelectedItemChanged;

      // Account Type
      ListItem l_monetaryFormatLI = new ListItem();
      l_monetaryFormatLI.Text = Local.GetValue("accounts.type_monetary");
      l_monetaryFormatLI.Value = Account.AccountType.MONETARY;
      TypeComboBox.Items.Add(l_monetaryFormatLI);
      m_formatsIdItemDict[(Account.AccountType)l_monetaryFormatLI.Value] = l_monetaryFormatLI;

      ListItem l_normalFormatLI = new ListItem();
      l_normalFormatLI.Text = Local.GetValue("accounts.type_number");
      l_normalFormatLI.Value = Account.AccountType.NUMBER;
      TypeComboBox.Items.Add(l_normalFormatLI);
      m_formatsIdItemDict[(Account.AccountType)l_normalFormatLI.Value] = l_normalFormatLI;

      ListItem l_percentageFormatLI = new ListItem();
      l_percentageFormatLI.Text = Local.GetValue("accounts.type_percentage");
      l_percentageFormatLI.Value = Account.AccountType.PERCENTAGE;
      TypeComboBox.Items.Add(l_percentageFormatLI);
      m_formatsIdItemDict[(Account.AccountType)l_percentageFormatLI.Value] = l_percentageFormatLI;

      ListItem l_dateFormatLI = new ListItem();
      l_dateFormatLI.Text = Local.GetValue("accounts.type_date");
      l_dateFormatLI.Value = Account.AccountType.DATE_;
      TypeComboBox.Items.Add(l_dateFormatLI);
      m_formatsIdItemDict[(Account.AccountType)l_dateFormatLI.Value] = l_dateFormatLI;

      TypeComboBox.SelectedIndex = 0;
      TypeComboBox.DropDownList = true;
      TypeComboBox.SelectedItemChanged += OnTypeComboBoxSelectedItemChanged;

      // Currencies Conversion
      ListItem l_averageRateLI = new ListItem();
      l_averageRateLI.Text = Local.GetValue("accounts.currencies_type_average");
      l_averageRateLI.Value = Account.ConversionOptions.AVERAGE_RATE;
      CurrencyConversionComboBox.Items.Add(l_averageRateLI);
      m_currenciesConversionIdItemDict[(Account.ConversionOptions)l_averageRateLI.Value] = l_averageRateLI;

      ListItem l_endOfPeriodRateLI = new ListItem();
      l_endOfPeriodRateLI.Text = Local.GetValue("accounts.currencies_type_end");
      l_endOfPeriodRateLI.Value = Account.ConversionOptions.END_OF_PERIOD_RATE;
      CurrencyConversionComboBox.Items.Add(l_endOfPeriodRateLI);
      m_currenciesConversionIdItemDict[(Account.ConversionOptions)l_endOfPeriodRateLI.Value] = l_endOfPeriodRateLI;

      CurrencyConversionComboBox.SelectedIndex = 0;
      CurrencyConversionComboBox.DropDownList = true;
      CurrencyConversionComboBox.SelectedItemChanged += OnCurrencyConversionComboBoxSelectedItemChanged;

      // Consolidation Option
      ListItem l_aggregatedLI = new ListItem();
      l_aggregatedLI.Text = Local.GetValue("accounts.consolidation_type_aggregated");
      l_aggregatedLI.Value = Account.ConsolidationOptions.AGGREGATION;
      ConsolidationOptionComboBox.Items.Add(l_aggregatedLI);
      m_consoOptionIdItemDict[(Account.ConsolidationOptions)l_aggregatedLI.Value] = l_aggregatedLI;

      ListItem l_recomputedLI = new ListItem();
      l_recomputedLI.Text = Local.GetValue("accounts.consolidation_type_recomputed");
      l_recomputedLI.Value = Account.ConsolidationOptions.RECOMPUTATION;
      ConsolidationOptionComboBox.Items.Add(l_recomputedLI);
      m_consoOptionIdItemDict[(Account.ConsolidationOptions)l_recomputedLI.Value] = l_recomputedLI;

      ListItem l_noneLI = new ListItem();
      l_noneLI.Text = Local.GetValue("accounts.consolidation_type_none");
      l_noneLI.Value = Account.ConsolidationOptions.NONE;
      ConsolidationOptionComboBox.Items.Add(l_noneLI);
      m_consoOptionIdItemDict[(Account.ConsolidationOptions)l_noneLI.Value] = l_noneLI;

      ConsolidationOptionComboBox.SelectedIndex = 0;
      ConsolidationOptionComboBox.DropDownList = true;
      ConsolidationOptionComboBox.SelectedItemChanged += OnConsolidationOptionComboBoxSelectedItemChanged;
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
          Forms.MsgBox.Show(Local.GetValue("accounts.error.create") + "\r\n" + Error.GetMessage(p_status));
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
        if (m_accountTV.FindNode(p_attributes.Id) != null)
        {
          vTreeNode l_oldNode = m_accountTV.FindNode(p_attributes.Id);
          l_oldNode.Text = p_attributes.Name;
          l_oldNode.ImageIndex = (Int32)p_attributes.Image;
          if (m_currentNode == l_oldNode)
          {
            DesactivateUnallowed();
            DisplayAttributes();
          }
        }
        else
        {
          vTreeNode l_node = new vTreeNode();
          l_node.Value = p_attributes.Id;
          l_node.Text = p_attributes.Name;
          l_node.ImageIndex = (Int32)p_attributes.Image;
          if (p_attributes.ParentId == 0)
            m_accountTV.Add(l_node);
          else
          {
            vTreeNode l_parent = m_accountTV.FindNode(p_attributes.ParentId);
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
          Forms.MsgBox.Show(Local.GetValue("accounts.error.update"));
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
          Forms.MsgBox.Show(Error.GetMessage(p_status));
          return;
        }
        vTreeNode l_toDelete = m_accountTV.FindNode(p_id);
        if (l_toDelete == m_currentNode)
        {
          m_currentNode = null;
          m_accountTV.SelectedNode = null;
          SetEnableStatusEdition(false, true, null);
        }
        if (l_toDelete != null)
        {
          if (l_toDelete.Parent == null)
            m_accountTV.Nodes.Remove(l_toDelete);
          else
            l_toDelete.Parent.Nodes.Remove(l_toDelete);
        }
      }
    }

    #endregion

    #region Others

    private void OnCheckDeleteAccount(object p_sender, EventArgs p_e)
    {
      if (m_accountTV.SelectedNode == null)
        return;

      string l_result = PasswordBox.Open(Local.GetValue("accounts.msg_account_deletion1") + "\n\r" + "\n"
         + Local.GetValue("accounts.msg_account_deletion4")
         , Local.GetValue("accounts.msg_account_deletion_confirmation"));
      if (l_result != PasswordBox.Canceled && l_result != Addin.Password)
        MsgBox.Show(Local.GetValue("accounts.msg_incorrect_password"), Local.GetValue("general.accounts"), MessageBoxButtons.OK, MessageBoxIcon.Error);
      else if (m_controller.DeleteAccount((UInt32)m_accountTV.SelectedNode.Value) == false)
        MsgBox.Show(m_controller.Error);
    }

    private void OnNameTextBoxKeyDown(object p_sender, KeyEventArgs p_e)
    {
      if (p_e.KeyCode == Keys.Enter)
      {
        if (m_currentNode != null)
        {
          Account l_currentAccount;

          if ((l_currentAccount = AccountModel.Instance.GetValue((UInt32)m_currentNode.Value)) != null)
          {
            l_currentAccount = l_currentAccount.Clone();
            l_currentAccount.Name = Name_TB.Text;
            if (m_controller.UpdateAccount(l_currentAccount) == false)
              MsgBox.Show(m_controller.Error);
          }
        }
      }
    }

    #endregion

    #region Click

    private void OnAllocationKeyButtonClick(object p_sender, EventArgs p_e)
    {
      if (m_accountTV.SelectedNode != null)
        m_controller.CreateAllocationKeysView(m_accountTV.SelectedNode);
    }

    private void OnValidateFormulaButtonClick(object p_sender, EventArgs p_e)
    {
      if (m_formulaTextBox.Text == "")
      {
        if (MessageBox.Show(Local.GetValue("accounts.msg_formula_empty"), Local.GetValue("general.accounts"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
          return;
      }
      else
        if (MessageBox.Show(Local.GetValue("accounts.msg_formula_validation_confirmation"), Local.GetValue("general.accounts"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
          return;
      if (m_currentNode != null)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue((UInt32)m_currentNode.Value)) != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          if (m_formulaTextBox.Text == "")
          {
            l_currentAccount.Formula = m_grammar.Formula;
            if (m_controller.UpdateAccount(l_currentAccount) == false)
              MsgBox.Show(m_controller.Error); 
            SetEditingFormulaUI(false);
            return;
          }
          m_bnf.AddRule("fbi_to_grammar", m_grammar.ToGrammar);
          if (m_bnf.Parse("fbi_to_grammar", m_formulaTextBox.Text))
          {
            l_currentAccount.Formula = m_grammar.Formula;
            if (m_controller.UpdateAccount(l_currentAccount) == false)
              MsgBox.Show(m_controller.Error); 
            SetEditingFormulaUI(false);
          }
          else
            MsgBox.Show(m_grammar.LastError);
        }
      }
    }

    private void OnCancelFormulaEditionButtonClick(object p_sender, EventArgs p_e)
    {
      m_formulaTextBox.Text = m_saveFormula;
      SetEditingFormulaUI(false);
    }

    private void OnFormulaEditionButtonClick(object p_sender, EventArgs p_e)
    {
      m_saveFormula = m_formulaTextBox.Text;
      SetEditingFormulaUI(true);
    }

    private void OnNewAccountClick(object p_sender, EventArgs p_e)
    {
      m_controller.CreateNewUI(m_accountTV.SelectedNode);
    }

    private void OnAddCategoryClick(object p_sender, EventArgs p_e)
    {
      m_isEditingFormulaFlag = false;

      string l_nameAccount = Interaction.InputBox(Local.GetValue("accounts.msg_new_tab_name"),
                                                 Local.GetValue("accounts.title_new_tab_name"), "");
      if (m_controller.CreateAccount(0, l_nameAccount, Account.AccountProcess.FINANCIAL, Account.FormulaTypes.TITLE, "", Account.AccountType.DATE_, Account.ConsolidationOptions.AGGREGATION,
        Account.PeriodAggregationOptions.AVERAGE_PERIOD, "t", (UInt32)Account.FormulaTypes.TITLE, m_accountTV.Nodes.Count) == false)
        MsgBox.Show(m_controller.Error);
    }

    private void OnSaveDescriptionBTClick(object p_sender, EventArgs p_e)
    {
      if (m_currentNode != null)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue((UInt32)m_currentNode.Value)) != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          l_currentAccount.Description = m_descriptionTextBox.Text;
          if (m_controller.UpdateAccount(l_currentAccount) == false)
            MsgBox.Show(m_controller.Error);
        }
      }
    }

    #endregion

    #region TreeView

    private void OnAccountTVMouseDoubleClick(object p_sender, EventArgs p_e)
    {
      if (m_isEditingFormulaFlag == true)
      {
        vTreeNode l_node = m_accountTV.FindAtPosition(((MouseEventArgs)p_e).Location);
        if (l_node != null)
          m_formulaTextBox.Text += "\"" + l_node.Text + "\"";
      }
    }

    private void OnAccountTVKeyDown(object p_sender, KeyEventArgs p_e)
    {
      switch (p_e.KeyCode)
      {
        case Keys.Down:
          if (p_e.Control == true)
          {
            m_accountTV.MoveNodeDown(m_accountTV.SelectedNode);
            m_currentNode = m_accountTV.SelectedNode;
          }
          break;
        case Keys.Up:
          if (p_e.Control == true)
          {
            m_accountTV.MoveNodeUp(m_accountTV.SelectedNode);
            m_currentNode = m_accountTV.SelectedNode;
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
      if (m_dragAndDropFlag == false && m_isEditingFormulaFlag == false
        && m_isDisplayingAccountFlag == false)
      {
        m_currentNode = p_e.Node;
        if (m_currentNode != null)
        {
          DesactivateUnallowed();
          DisplayAttributes();
        }
        else
          m_accountTV.Capture = false;
      }
      else
        ((FbiTreeView<Account>)p_sender).SelectedNode = m_currentNode;
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
      if (m_controller.UpdateAccount(l_account) == false)
        MsgBox.Show(m_controller.Error);
    }

    #endregion

    #region ComboBox

    private void OnProcessComboBoxSelectedItemChanged(object p_sender, EventArgs p_e)
    {
      if (m_isDisplayingAccountFlag == false && m_currentNode != null)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue((UInt32)m_currentNode.Value)) != null && ((vComboBox)p_sender).SelectedItem != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          l_currentAccount.Process = (Account.AccountProcess)((vComboBox)p_sender).SelectedItem.Value;
          if (m_controller.UpdateAccount(l_currentAccount) == false)
            MsgBox.Show(m_controller.Error);
        }
      }
    }

    private void OnTypeComboBoxSelectedItemChanged(object p_sender, EventArgs p_e)
    {
      if (m_currentNode == null)
        return;
      if (m_isDisplayingAccountFlag == false)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue((UInt32)m_currentNode.Value)) != null && ((vComboBox)p_sender).SelectedItem != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          l_currentAccount.Type = (Account.AccountType)((vComboBox)p_sender).SelectedItem.Value;
          if (m_controller.UpdateAccount(l_currentAccount) == false)
            MsgBox.Show(m_controller.Error);
        }
      }
      if ((Account.AccountType)((vComboBox)p_sender).SelectedItem.Value == Account.AccountType.MONETARY)
      {
        CurrencyConversionComboBox.Visible = true;
        m_accountCurrenciesConversionLabel.Visible = true;
      }
      else
      {
        CurrencyConversionComboBox.Visible = false;
        m_accountCurrenciesConversionLabel.Visible = false;
      }
    }

    private void OnCurrencyConversionComboBoxSelectedItemChanged(object p_sender, EventArgs p_e)
    {
      if (m_isDisplayingAccountFlag == false && m_currentNode != null)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue((UInt32)m_currentNode.Value)) != null && ((vComboBox)p_sender).SelectedItem != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          l_currentAccount.ConversionOptionId = (Account.ConversionOptions)((vComboBox)p_sender).SelectedItem.Value;
          if (m_controller.UpdateAccount(l_currentAccount) == false)
            MsgBox.Show(m_controller.Error);
        }
      }
    }

    private void OnConsolidationOptionComboBoxSelectedItemChanged(object p_sender, EventArgs p_e)
    {
      if (m_isDisplayingAccountFlag == false && m_currentNode != null)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue((UInt32)m_currentNode.Value)) != null && ((vComboBox)p_sender).SelectedItem != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          l_currentAccount.ConsolidationOptionId = (Account.ConsolidationOptions)((vComboBox)p_sender).SelectedItem.Value;
          if (m_controller.UpdateAccount(l_currentAccount) == false)
            MsgBox.Show(m_controller.Error);
        }
      }
    }

    private void OnFormulaTypeComboBoxSelectedItemChanged(object p_sender, EventArgs p_e)
    {
      if (m_isDisplayingAccountFlag == false && m_currentNode != null)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue((UInt32)m_currentNode.Value)) != null && ((vComboBox)p_sender).SelectedItem != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          Account.FormulaTypes l_value = (Account.FormulaTypes)((vComboBox)p_sender).SelectedItem.Value;

          if (l_currentAccount.FormulaType == Account.FormulaTypes.HARD_VALUE_INPUT || l_currentAccount.FormulaType == Account.FormulaTypes.FIRST_PERIOD_INPUT)
          {
            if (l_value != Account.FormulaTypes.HARD_VALUE_INPUT || l_value != Account.FormulaTypes.FIRST_PERIOD_INPUT)
            {
              string l_result = PasswordBox.Open(Local.GetValue("accounts.msg_password_required"),
                Local.GetValue("general.accounts"));

              if (l_result == PasswordBox.Canceled || l_result != Addin.Password)
              {
                if (l_result != PasswordBox.Canceled)
                  Forms.MsgBox.Show(Local.GetValue("accounts.msg_incorrect_password"), Local.GetValue("general.accounts"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (m_controller.UpdateAccount(l_currentAccount) == false)
                  MsgBox.Show(m_controller.Error); 
                return;
              }
            }
          }
          l_currentAccount.FormulaType = l_value;
          if (m_controller.UpdateAccount(l_currentAccount) == false)
            MsgBox.Show(m_controller.Error);
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
          return;
        vTreeNode l_toDelete = m_globalFactsTV.FindNode(p_id);
        if (l_toDelete == m_globalFactsTV.SelectedNode)
          m_globalFactsTV.SelectedNode = null;
        if (l_toDelete != null)
          if (l_toDelete.Parent == null)
            m_globalFactsTV.Nodes.Remove(l_toDelete);
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
          return;
        if (m_globalFactsTV.FindNode(p_attributes.Id) != null)
        {
          vTreeNode l_oldNode = m_globalFactsTV.FindNode(p_attributes.Id);
          l_oldNode.Text = p_attributes.Name;
          l_oldNode.ImageIndex = (Int32)p_attributes.Image;
        }
        else
        {
          vTreeNode l_node = new vTreeNode();
          l_node.Value = p_attributes.Id;
          l_node.Text = p_attributes.Name;
          l_node.ImageIndex = (Int32)p_attributes.Image;
          m_globalFactsTV.Nodes.Add(l_node);
        }
      }
    }

    #endregion

    private void OnGlobalFactsTVMouseDoubleClick(object p_sender, MouseEventArgs p_e)
    {
      if (m_isEditingFormulaFlag == true)
      {
        vTreeNode l_node = m_accountTV.FindAtPosition(((MouseEventArgs)p_e).Location);
        if (l_node != null)
          m_formulaTextBox.Text += "\"" + l_node.Text + "\"";
      }
    }

    #endregion

    #endregion

    #region Utils

    private void SetFormulaEditionState(Boolean p_state)
    {
      m_isEditingFormulaFlag = p_state;
      m_formulaTextBox.Enabled = p_state;
      m_validateFormulaButton.Visible = p_state;
      m_cancelFormulaEditionButton.Visible = p_state;
      m_formulaEditionButton.Visible = p_state;
      if (p_state == true)
        m_formulaTextBox.BackColor = Color.White;
      else
        m_formulaTextBox.BackColor = Color.LightGray;
    }

    private void DisplayAttributes()
    {
      if ((m_currentNode != null) && m_isEditingFormulaFlag == false)
      {
        Account l_account = AccountModel.Instance.GetValue((UInt32)m_currentNode.Value);

        if (l_account == null)
          return;

        m_isDisplayingAccountFlag = true;

        //Name
        Name_TB.Text = m_currentNode.Text;

        //Process
        if (m_processIdItemDict.ContainsKey(l_account.Process))
        {
          ListItem l_processLI = m_processIdItemDict[l_account.Process];
          ProcessComboBox.SelectedItem = l_processLI;
        }

        //Formula Type
        if (m_formulasTypesIdItemDict.ContainsKey(l_account.FormulaType))
        {
          ListItem l_formulaTypeLI = m_formulasTypesIdItemDict[l_account.FormulaType];
          FormulaTypeComboBox.SelectedItem = l_formulaTypeLI;

          bool l_isRootAccount = false;
          if (m_currentNode.Parent == null)
            l_isRootAccount = true;
          if ((Account.FormulaTypes)l_formulaTypeLI.Value == Account.FormulaTypes.TITLE)
            SetEnableStatusEdition(false, l_isRootAccount, l_account);
          else
            SetEnableStatusEdition(true, l_isRootAccount, l_account);
        }

        if (m_formatsIdItemDict.ContainsKey(l_account.Type))
        {
          ListItem l_formatLI = m_formatsIdItemDict[l_account.Type];
          TypeComboBox.SelectedItem = l_formatLI;
          if ((Account.AccountType)l_formatLI.Value == Account.AccountType.MONETARY)
          {
            if (m_currenciesConversionIdItemDict.ContainsKey(l_account.ConversionOptionId))
            {
              ListItem conversionLI = m_currenciesConversionIdItemDict[l_account.ConversionOptionId];
              CurrencyConversionComboBox.SelectedItem = conversionLI;
            }
          }
        }

        //Consolidation
        if (m_consoOptionIdItemDict.ContainsKey(l_account.ConsolidationOptionId))
        {
          ListItem consolidationLI = m_consoOptionIdItemDict[l_account.ConsolidationOptionId];
          ConsolidationOptionComboBox.SelectedItem = consolidationLI;
        }

        // Formula TB
        m_formulaTextBox.Text = "";
        m_bnf.AddRule("fbi_to_human_grammar", m_grammar.ToHuman);
        if (m_bnf.Parse("fbi_to_human_grammar", l_account.Formula))
          m_formulaTextBox.Text = m_grammar.Formula;
        else
          Forms.MsgBox.Show(m_grammar.LastError);

        //Description
        m_descriptionTextBox.Text = l_account.Description;

        m_isDisplayingAccountFlag = false;
      }
    }

    private void SetEnableStatusEdition(Boolean p_status, Boolean p_isRootAccount, Account p_account)
    {
      if (p_isRootAccount == true)
        FormulaTypeComboBox.Enabled = false;
      if (p_account != null)
      {
        if (p_account.FormulaType == Account.FormulaTypes.FORMULA || p_account.FormulaType == Account.FormulaTypes.FIRST_PERIOD_INPUT)
          m_formulaEditionButton.Visible = true;
        else
          m_formulaEditionButton.Visible = false;
      }
      else
        m_formulaEditionButton.Visible = false;
      m_descriptionTextBox.BackColor = Color.White;
    }

    private void SetAccountUIState(Boolean p_uiState)
    {
      SaveDescriptionBT.Enabled = p_uiState;
      Name_TB.Enabled = p_uiState;
      ProcessComboBox.Enabled = p_uiState;
      FormulaTypeComboBox.Enabled = p_uiState;
      TypeComboBox.Enabled = p_uiState;
      CurrencyConversionComboBox.Enabled = p_uiState;
      m_descriptionTextBox.Enabled = p_uiState;
      if (p_uiState == true)
        m_descriptionTextBox.BackColor = Color.White;
      else
        m_descriptionTextBox.BackColor = Color.LightGray;
      ConsolidationOptionComboBox.Enabled = p_uiState;
      m_validateFormulaButton.Enabled = p_uiState;
    }

    private void DesactivateUnallowed()
    {
      m_rightMgr.Enable(UserModel.Instance.GetCurrentUserRights());
    }

    private void SetEditingFormulaUI(bool p_state)
    {
      m_isEditingFormulaFlag = p_state;
      m_formulaEditionButton.Visible = !p_state;
      m_validateFormulaButton.Visible = p_state;
      m_validateFormulaButton.Enabled = p_state;
      m_cancelFormulaEditionButton.Visible = p_state;
      m_cancelFormulaEditionButton.Enabled = p_state;
      m_formulaTextBox.Enabled = p_state;
      if (p_state == false)
        m_formulaTextBox.BackColor = Color.LightGray;
      else
        m_formulaTextBox.BackColor = Color.White;
    }

    #endregion

  }
}
