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
using Microsoft.Office.Interop.Excel;
using VIBlend.WinForms.Controls;

namespace FBI.MVC.View
{
  using Controller;
  using Utils;
  using Forms;
  using Model;
  using Network;
  using Model.CRUD;

  public partial class AccountsView : UserControl, IPlatformMgtView
  {

    #region Variables

    RightManager m_rightMgr = new RightManager();

    AccountController m_controller = null;
    FbiTreeView<Account> m_accountTV = null;
    FbiTreeView<GlobalFact> m_globalFactsTV = null;
    vTreeNode m_currentNode = null;

    SafeDictionary<Account.FormulaTypes, ListItem> m_formulaTypeItemDict = new SafeDictionary<Account.FormulaTypes, ListItem>();
    SafeDictionary<Account.AccountType, ListItem> m_formatsIdItemDict = new SafeDictionary<Account.AccountType, ListItem>();
    SafeDictionary<Account.ConversionOptions, ListItem> m_currencyItemDict = new SafeDictionary<Account.ConversionOptions, ListItem>();
    SafeDictionary<Account.ConsolidationOptions, ListItem> m_consoOptionItemDict = new SafeDictionary<Account.ConsolidationOptions, ListItem>();
    SafeDictionary<Account.AccountProcess, ListItem> m_processIdItemDict = new SafeDictionary<Account.AccountProcess, ListItem>();
    SafeDictionary<Account.Format, ListItem> m_formatItemDict = new SafeDictionary<Account.Format, ListItem>();
    bool m_dragAndDropFlag = false;
    bool m_isDisplayingAccountFlag = false;
    bool m_isEditingFormulaFlag = false;
    bool m_isValidAutoComplete = false;
    string m_saveFormula = "";
    private int m_rightDistance = 30;

    SafeDictionary<UInt32, Int32> m_updatedAccountPos = new SafeDictionary<uint,int>();
    UInt32 m_currentAccount = 0;

    vListBox m_autocomplete;
    Stopwatch m_formulaDoubleClickClock = Stopwatch.StartNew();
    int m_formulaSelectionStart = 0;
    Color[] m_formulaColor = new Color[] 
    {
      Color.Blue,
      Color.Purple,
      Color.Orange,
      Color.Green,
      Color.RoyalBlue,
      Color.SeaGreen,
      Color.Olive,
      Color.MidnightBlue,
      Color.MediumVioletRed
    };

    #endregion

    #region Initialize

    public AccountsView()
    {
      InitializeComponent();
      m_autocomplete = new vListBox();

      m_formulaTextBox.Controls.Add(m_autocomplete);
      m_autocomplete.Width = 200;
      m_autocomplete.Height = 200;
      m_autocomplete.Visible = false;
      m_autocomplete.BringToFront();
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
        m_globalFactsTV = new FbiTreeView<GlobalFact>(GlobalFactModel.Instance.GetDictionary(), null, true);

        AccountsTVInit();
        GlobalFactsTVInit();
        ComboBoxesInit();
      }
      catch (Exception e)
      {
        MessageBox.Show(Local.GetValue("CUI.msg_error_system"), Local.GetValue("general.accounts"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        Debug.WriteLine(e.Message + e.StackTrace);
      }

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
      m_globalFactsTV.MouseDown += OnGlobalFactTVMouseDown;
      m_accountTV.NodeDropped += OnAccountsTreeviewNodeDropped;
      m_dropToExcelRightClickMenu.Click += OnDropSelectedAccountToExcel;
      m_formulaTextBox.AllowDrop = true;
      m_formulaTextBox.DragDrop += OnFormulaDragDrop;
      m_formulaTextBox.DragOver += OnFormulaDragOver;
      m_formulaTextBox.TextChanged += OnFormulaChanged;
      m_formulaTextBox.KeyUp += OnFormulaKeyUp;
      m_formulaTextBox.MouseDown += OnFormulaMouseDown;
      m_autocomplete.KeyUp += OnFormulaKeyUp;
      m_autocomplete.MouseDoubleClick += OnAutoCompleteMouseDoubleClick;
      m_globalFactsTV.MouseDoubleClick += OnGlobalFactTVMouseDoubleClick;

      m_expandRightBT.MouseDown += OnExpandRightMouseDown;
    }

    public void CloseView()
    {
      AccountModel.Instance.UpdateEvent -= OnModelUpdate;
      AccountModel.Instance.ReadEvent -= OnModelRead;
      AccountModel.Instance.CreationEvent -= OnAccountModelCreation;
      AccountModel.Instance.DeleteEvent -= OnModelDelete;

      GlobalFactModel.Instance.ReadEvent -= OnGlobalFactModelRead;
      GlobalFactModel.Instance.DeleteEvent -= OnGlobalFactModelDelete;
      SendUpdatePosition();
    }

    void SendUpdatePosition()
    {
      List<Account> l_updatedAccountList = new List<Account>();

      foreach (KeyValuePair<UInt32, Int32> l_pair in m_updatedAccountPos)
      {
        Account l_account = AccountModel.Instance.GetValue(l_pair.Key);

        if (l_account == null || l_account.ItemPosition == l_pair.Value)
          continue;
        l_account = l_account.Clone();
        l_account.ItemPosition = l_pair.Value;
        l_updatedAccountList.Add(l_account);
      }
      if (l_updatedAccountList.Count > 0)
        if (m_controller.UpdateAccountList(l_updatedAccountList) == false)
          MsgBox.Show(m_controller.Error);
    }

    private void DefineUIPermissions()
    {
      m_rightMgr[SaveDescriptionBT] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[Name_TB] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[ProcessCB] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[FormulaTypeCB] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[TypeCB] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[CurrencyCB] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[m_descriptionTextBox] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[ConsolidationOptionCB] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[AddSubAccountToolStripMenuItem] = Group.Permission.CREATE_ACCOUNT;
      m_rightMgr[AddCategoryToolStripMenuItem] = Group.Permission.CREATE_ACCOUNT;
      m_rightMgr[DeleteAccountToolStripMenuItem] = Group.Permission.DELETE_ACCOUNT;
      m_rightMgr[DeleteAccountToolStripMenuItem1] = Group.Permission.DELETE_ACCOUNT;
      m_rightMgr[m_validateFormulaButton] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[m_formulaEditionButton] = Group.Permission.EDIT_ACCOUNT;
      m_rightMgr[CreateANewAccountToolStripMenuItem] = Group.Permission.CREATE_ACCOUNT;
      m_rightMgr[CreateANewCategoryToolStripMenuItem] = Group.Permission.CREATE_ACCOUNT;
      m_rightMgr[m_formatCB] = Group.Permission.EDIT_ACCOUNT;
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
      m_dropToExcelRightClickMenu.Text = Local.GetValue("accounts.drop_to_excel");
      NewToolStripMenuItem.Text = Local.GetValue("general.account");
      CreateANewAccountToolStripMenuItem.Text = Local.GetValue("accounts.new_account");
      CreateANewCategoryToolStripMenuItem.Text = Local.GetValue("accounts.add_tab_account");
      DeleteAccountToolStripMenuItem1.Text = Local.GetValue("accounts.delete_account");
      DropAllAccountsHierarchyToExcelToolStripMenuItem.Text = Local.GetValue("accounts.drop_to_excel");
      DropSelectedAccountHierarchyToExcelToolStripMenuItem.Text = Local.GetValue("accounts.drop_selected_hierarchy_to_excel");
      HelpToolStripMenuItem.Text = Local.GetValue("general.help");
      m_formatLabel.Text = Local.GetValue("accounts.format");
    }

    private void GlobalFactsTVInit()
    {
      m_globalFactsTV.Dock = DockStyle.Fill;
      m_globalFactsTV.LabelEdit = false;
      m_globalFactsTV.CollapseAll();
      m_globalFactsTV.ImageList = m_globalFactsImageList;
      splitContainer3.Panel2.Controls.Add(m_globalFactsTV);

      m_globalFactsTV.MouseDoubleClick += OnGlobalFactsTVMouseDoubleClick;
    }

    void AddListItem<T>(vComboBox p_cb, SafeDictionary<T, ListItem> p_collection, string p_local, T p_value)
    {
      ListItem l_item = new ListItem();
      l_item.Text = Local.GetValue(p_local);
      l_item.Value = p_value;
      p_cb.Items.Add(l_item);
      p_collection[p_value] = l_item;
      p_cb.SelectedIndex = 0;
      p_cb.DropDownList = true;
    }

    private void ComboBoxesInit()
    {
      AddListItem(ProcessCB, m_processIdItemDict, "process.process_financial", Account.AccountProcess.FINANCIAL);
      AddListItem(ProcessCB, m_processIdItemDict, "process.process_rh", Account.AccountProcess.RH);
      ProcessCB.SelectedItemChanged += OnProcessCBSelectedItemChanged;

      AddListItem(FormulaTypeCB, m_formulaTypeItemDict, "accounts.formula_type_input", Account.FormulaTypes.HARD_VALUE_INPUT);
      AddListItem(FormulaTypeCB, m_formulaTypeItemDict, "accounts.formula_type_formula", Account.FormulaTypes.FORMULA);
      AddListItem(FormulaTypeCB, m_formulaTypeItemDict, "accounts.formula_type_sub", Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS);
      AddListItem(FormulaTypeCB, m_formulaTypeItemDict, "accounts.formula_type_first", Account.FormulaTypes.FIRST_PERIOD_INPUT);
      AddListItem(FormulaTypeCB, m_formulaTypeItemDict, "accounts.formula_type_title", Account.FormulaTypes.TITLE);
      FormulaTypeCB.SelectedItemChanged += OnFormulaTypeCBSelectedItemChanged;

      AddListItem(TypeCB, m_formatsIdItemDict, "accounts.type_monetary", Account.AccountType.MONETARY);
      AddListItem(TypeCB, m_formatsIdItemDict, "accounts.type_number", Account.AccountType.NUMBER);
      AddListItem(TypeCB, m_formatsIdItemDict, "accounts.type_percentage", Account.AccountType.PERCENTAGE);
      AddListItem(TypeCB, m_formatsIdItemDict, "accounts.type_date", Account.AccountType.DATE);
      TypeCB.SelectedItemChanged += OnTypeCBSelectedItemChanged;

      AddListItem(CurrencyCB, m_currencyItemDict, "accounts.currencies_type_average", Account.ConversionOptions.AVERAGE_RATE);
      AddListItem(CurrencyCB, m_currencyItemDict, "accounts.currencies_type_end", Account.ConversionOptions.END_OF_PERIOD_RATE);
      CurrencyCB.SelectedItemChanged += OnCurrencyCBSelectedItemChanged;

      AddListItem(ConsolidationOptionCB, m_consoOptionItemDict, "accounts.consolidation_type_aggregated", Account.ConsolidationOptions.AGGREGATION);
      AddListItem(ConsolidationOptionCB, m_consoOptionItemDict, "accounts.consolidation_type_recomputed", Account.ConsolidationOptions.RECOMPUTATION);
      AddListItem(ConsolidationOptionCB, m_consoOptionItemDict, "accounts.consolidation_type_none", Account.ConsolidationOptions.NONE);
      ConsolidationOptionCB.SelectedItemChanged += OnConsolidationOptionCBSelectedItemChanged;

      AddListItem(m_formatCB, m_formatItemDict, "settings.important", Account.Format.important);
      AddListItem(m_formatCB, m_formatItemDict, "settings.detail", Account.Format.detail);
      AddListItem(m_formatCB, m_formatItemDict, "settings.title", Account.Format.title);
      AddListItem(m_formatCB, m_formatItemDict, "settings.normal", Account.Format.normal);
      m_formatCB.SelectedItemChanged += OnFormatCBSelectedItemChanged;
    }

    public void ExpandFactPane()
    {
      Control l_pane = splitContainer3.Panel2;

      if (l_pane.Visible)
      {
        l_pane.Visible = false;
        m_rightDistance = SplitContainer2.SplitterDistance;
        SplitContainer2.SplitterDistance = m_rightDistance + l_pane.Width - 30;
        m_expandRightBT.Text = "+";
      }
      else
      {
        l_pane.Visible = true;
        SplitContainer2.SplitterDistance = m_rightDistance;
        m_expandRightBT.Text = "-";
      }
    }

    #endregion

    #region Autocomplete

    string FindCurrentFormulaToken(out int p_position, out bool p_endQuote)
    {
      string token = "";
      int selectionStart = m_formulaTextBox.TextBox.SelectionStart;
      bool insideQuote = false;
      p_endQuote = false;
      p_position = m_formulaTextBox.Text.Length;

      for (int i = 0; i < m_formulaTextBox.Text.Length && i < selectionStart; ++i)
      {
        if (m_formulaTextBox.Text[i] == '\"')
        {
          insideQuote = !insideQuote;
          token = "";
          if (insideQuote)
            p_position = i + 1;
        }
        else
          if (insideQuote)
            token += m_formulaTextBox.Text[i];
        p_endQuote = (m_formulaTextBox.Text[i] == '\"');
      }
      p_endQuote = p_endQuote && !insideQuote;
      if (p_endQuote)
        p_position = selectionStart;
      return (token);
    }

    Dictionary<int, string> GetTokenList()
    {
      string l_token = "";
      int l_selectionStart = 0;
      bool l_insideQuote = false;
      Dictionary<int, string> l_tokenList = new Dictionary<int, string>();

      for (int i = 0; i < m_formulaTextBox.Text.Length; ++i)
      {
        if (m_formulaTextBox.Text[i] == '\"')
        {
          l_insideQuote = !l_insideQuote;
          if (l_insideQuote)
            l_selectionStart = i;
          else
            l_tokenList[l_selectionStart + 1] = l_token;
          l_token = "";
        }
        else
          if (l_insideQuote)
            l_token += m_formulaTextBox.Text[i];
      }
      return (l_tokenList);
    }

    string FindCompleteToken()
    {
      int l_posToken;
      bool l_endQuote;
      string l_token = FindCurrentFormulaToken(out l_posToken, out l_endQuote);

      if (!l_endQuote)
      {
        l_token = m_formulaTextBox.Text.Substring(l_posToken);
        int l_pos = l_token.IndexOf("\"");
        if (l_pos > 0)
          return l_token.Substring(0, l_pos);
      }
      return (l_token);
    }

    void OnFormulaChanged(object sender, EventArgs e)
    {
      if (m_isValidAutoComplete)
        return;

      ColorFormula();

      int l_posToken;
      bool l_endQuote;
      string l_token = FindCurrentFormulaToken(out l_posToken, out l_endQuote);
      List<string> l_accountList = AccountModel.Instance.GetMatchings(l_token);
      List<string> l_globalFactList = GlobalFactModel.Instance.GetMatchings(l_token);
      List<string> l_list = new List<string>();

      l_list.AddRange(l_accountList);
      l_list.AddRange(l_globalFactList);
      l_list.Sort();
      if (l_list.Count == 0)
      {
        if (l_endQuote)
        {
          l_list.Add("[n]");
          l_list.Add("[n+1]");
          l_list.Add("[n-1]");
        }
        else
        {
          m_autocomplete.Items.Clear();
          m_autocomplete.SelectedItem = null;
          return;
        }
      }
      m_autocomplete.DataSource = l_list;
      m_autocomplete.SelectedItem = m_autocomplete.Items[0];
      System.Drawing.Point l_point = new System.Drawing.Point();
      if (WinApi.GetCaretPos(out l_point))
      {
        l_point.Y += 15;
        m_autocomplete.Location = l_point;
      }
      m_autocomplete.Height = 2 + 20 * l_list.Count;
      m_autocomplete.Show();
    }

    void OnFormulaKeyUp(object sender, KeyEventArgs p_e)
    {
      int l_index = m_autocomplete.SelectedIndex;
      m_isValidAutoComplete = false;

      if (m_autocomplete.Visible)
      {
        switch (p_e.KeyCode)
        {
          case Keys.Escape:
            m_autocomplete.Hide();
            break;
          case Keys.Return:
            ValidateAutoComplete();
            break;
          case Keys.Down:
            m_autocomplete.SelectedItem = m_autocomplete.Items[(l_index + 1) % m_autocomplete.Items.Count];
            m_autocomplete.Refresh();
            break;
          case Keys.Up:
            m_autocomplete.SelectedItem = m_autocomplete.Items[(l_index - 1 < 0) ? 0 : l_index - 1];
            m_autocomplete.Refresh();
            break;
        }
        if (m_autocomplete.Items.Count == 0)
          m_autocomplete.Hide();
      }
    }

    void OnAutoCompleteMouseDoubleClick(object sender, MouseEventArgs e)
    {
      ValidateAutoComplete();
    }

    void ValidateAutoComplete()
    {
      if (m_autocomplete.SelectedItem != null)
      {
        m_isValidAutoComplete = true;
        int l_selectionStart = m_formulaTextBox.TextBox.SelectionStart;

        if (m_formulaTextBox.Text[l_selectionStart - 1] == '\n')
        {
          l_selectionStart -= 1;
          m_formulaTextBox.Text = m_formulaTextBox.Text.Remove(l_selectionStart, 1);
          m_formulaTextBox.TextBox.SelectionStart = l_selectionStart;
        }

        int l_posToken;
        bool l_endQuote;
        string l_token =  FindCurrentFormulaToken(out l_posToken, out l_endQuote);
        string l_text = m_formulaTextBox.Text.Substring(0, l_posToken);

        l_text += m_autocomplete.SelectedItem.Text;
        if (!l_endQuote)
          l_text += "\""; 
        l_text += m_formulaTextBox.Text.Substring(l_posToken + l_token.Length);
        m_isValidAutoComplete = false;
        m_formulaTextBox.Text = l_text;
        l_selectionStart = l_selectionStart + m_autocomplete.SelectedItem.Text.Length - l_token.Length + 1;
        m_formulaTextBox.TextBox.SelectionStart = l_selectionStart;
        m_autocomplete.Hide();
        m_formulaTextBox.Focus();
        OnFormulaChanged(null, null);
      }
    }

    void RestoreAccountTVColors()
    {
      foreach (vTreeNode l_node in m_accountTV.GetNodes())
        l_node.UseThemeTextColor = true;
    }

    void ColorTokenInAccountTV(string p_token, Color p_color)
    {
      vTreeNode l_node = m_accountTV.FindNode(AccountModel.Instance.GetValueId(p_token));

      if (l_node == null)
        l_node = m_globalFactsTV.FindNode(GlobalFactModel.Instance.GetValueId(p_token));
      if (l_node != null)
      {
        l_node.UseThemeTextColor = false;
        l_node.ForeColor = p_color;
      }
    }

    void ColorFormula()
    {
      RestoreAccountTVColors();

      if (!m_formulaTextBox.Enabled)
        return;
      int l_selectionStart = m_formulaTextBox.TextBox.SelectionStart;
      int l_selectionLenght = m_formulaTextBox.TextBox.SelectionLength;
      int l_colorIndex = 0;
      Dictionary<int, string> l_tokenDic = GetTokenList();
      List<string> l_tokenList = l_tokenDic.Values.ToList();

      foreach (KeyValuePair<int, string> l_pair in l_tokenDic)
      {
        m_formulaTextBox.TextBox.SelectionLength = l_pair.Value.Length;
        m_formulaTextBox.TextBox.SelectionStart = l_pair.Key;
        if (AccountModel.Instance.GetValue(l_pair.Value) == null && GlobalFactModel.Instance.GetValue(l_pair.Value) == null)
        {
          m_formulaTextBox.TextBox.SelectionColor = Color.Red;
          m_formulaTextBox.TextBox.SelectionBackColor = Color.LightGray;
        }
        else
        {
          l_colorIndex = l_tokenList.IndexOf(l_pair.Value);
          m_formulaTextBox.TextBox.SelectionBackColor = Color.Transparent;
          ColorTokenInAccountTV(l_pair.Value, m_formulaColor[l_colorIndex]);
          m_formulaTextBox.TextBox.SelectionColor = m_formulaColor[l_colorIndex];
        }
      }

      m_formulaTextBox.TextBox.SelectionLength = l_selectionLenght;
      m_formulaTextBox.TextBox.SelectionStart = l_selectionStart;
    }

    void OnFormulaMouseDown(object sender, EventArgs e)
    {
      if (m_formulaDoubleClickClock.ElapsedMilliseconds > 300)
      {
        m_formulaSelectionStart = m_formulaTextBox.TextBox.SelectionStart;
        m_formulaDoubleClickClock.Restart();
      }
      else
      {
        m_formulaTextBox.TextBox.SelectionStart = m_formulaSelectionStart;
        OnFormulaMouseDoubleClick();
      }
    }

    void OnFormulaMouseDoubleClick()
    {
      SelectCurrentToken();
    }

    bool SelectCurrentToken()
    {
      int l_posToken;
      bool l_endQuote;
      FindCurrentFormulaToken(out l_posToken, out l_endQuote);
      string l_token = FindCompleteToken();

      if (!l_endQuote)
      {
        m_formulaTextBox.TextBox.SelectionStart = l_posToken;
        m_formulaTextBox.TextBox.SelectionLength = l_token.Length;
        return (true);
      }
      return (false);
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
          MsgBox.Show(Local.GetValue("accounts.error.create") + "\r\n" + Error.GetMessage(p_status));
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
          if (m_currentNode == l_oldNode && !m_isEditingFormulaFlag)
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
          MessageBox.Show(Local.GetValue("accounts.error.update") + "\r\n" + Error.GetMessage(p_status));
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
          MessageBox.Show(Error.GetMessage(p_status));
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
         + Local.GetValue("accounts.msg_account_deletion2")
         , Local.GetValue("accounts.msg_account_deletion_confirmation"));
      if (l_result != PasswordBox.Canceled && l_result != Addin.Password)
        MsgBox.Show(Local.GetValue("accounts.msg_incorrect_password"), Local.GetValue("general.accounts"), MessageBoxButtons.OK, MessageBoxIcon.Error);
      else if (l_result != PasswordBox.Canceled && m_controller.DeleteAccount((UInt32)m_accountTV.SelectedNode.Value) == false)
        MsgBox.Show(m_controller.Error);
    }

    private void OnNameTextBoxKeyDown(object p_sender, KeyEventArgs p_e)
    {      
      if (p_e.KeyCode == Keys.Enter)
      {
        if (m_currentNode != null)
        {
          Account l_currentAccount;

          if ((l_currentAccount = AccountModel.Instance.GetValue(m_currentAccount)) != null)
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

    void OnFormulaDragOver(object sender, DragEventArgs e)
    {
      if (!m_isEditingFormulaFlag)
        return;
      if (e.Data.GetDataPresent("VIBlend.WinForms.Controls.vTreeNode", true))
        e.Effect = DragDropEffects.Move;
    }

    void OnFormulaDragDrop(object sender, DragEventArgs e)
    {
      if (!m_isEditingFormulaFlag)
        return;
      if (!e.Data.GetDataPresent("VIBlend.WinForms.Controls.vTreeNode", true))
        return;
      vTreeNode l_node = e.Data.GetData("VIBlend.WinForms.Controls.vTreeNode") as vTreeNode;

      m_formulaTextBox.Text += "\"" + l_node.Text + "\"[n]";
    }

    #region Click

    private Range GetActiveCell()
    {
      Worksheet l_activeWS = Addin.HostApplication.ActiveSheet;
      Range l_RNG = Addin.HostApplication.ActiveCell;
      DialogResult l_response = default(DialogResult);

      if (l_RNG == null)
        MessageBox.Show(Local.GetValue("accounts.msg_destination_cell_not_valid"));
      else
      {
        l_response = MessageBox.Show(Local.GetValue("accounts.msg_accounts_drop") + l_RNG.Address, "", MessageBoxButtons.OKCancel);
        if (l_response == DialogResult.OK)
          return (l_RNG);
      }
      return (null);
    }

    private void OnDropSelectedAccountToExcel(object p_sender, EventArgs p_e)
    {
      if (m_accountTV.SelectedNode == null)
        return;
      Range l_RNG = GetActiveCell();
      int l_indentLevel = 0;

      if (l_RNG != null)
        if (WorksheetWriter.WriteAccount(m_accountTV.SelectedNode, ref l_RNG, ref l_indentLevel) == false)
          MessageBox.Show(Local.GetValue("accounts.error.drop_failed"));
    }

    private void OnDropAllAccountOnExcel(object p_sender, EventArgs p_e)
    {
      Range l_RNG = GetActiveCell();

      if (l_RNG != null)
        if (WorksheetWriter.WriteAccountsFromTreeView(m_accountTV, l_RNG) == false)
          MessageBox.Show(Local.GetValue("accounts.error.drop_failed"));
    }

    private void OnAllocationKeyButtonClick(object p_sender, EventArgs p_e)
    {
      if (m_accountTV.SelectedNode != null)
        m_controller.ShowView(m_accountTV.SelectedNode);
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

        if ((l_currentAccount = AccountModel.Instance.GetValue(m_currentAccount)) != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          if (m_formulaTextBox.Text == "")
          {
            l_currentAccount.Formula = "";
            if (m_controller.UpdateAccount(l_currentAccount) == false)
              MsgBox.Show(m_controller.Error); 
            SetEditingFormulaUI(false);
            return;
          }
          if (m_controller.m_bnf.Parse(m_formulaTextBox.Text, FbiGrammar.TO_SERVER))
          {
            l_currentAccount.Formula = m_controller.m_bnf.Concatenated;
            if (m_controller.UpdateAccount(l_currentAccount) == false)
              MsgBox.Show(m_controller.Error); 
            SetEditingFormulaUI(false);
          }
          else
            MsgBox.Show(m_controller.m_bnf.LastError);
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
      string l_nameAccount = Interaction.InputBox(Local.GetValue("accounts.msg_new_tab_name"),
                                                 Local.GetValue("accounts.title_new_tab_name"), "");
      if (l_nameAccount == "")
        return;
      if (m_controller.CreateAccount(0, l_nameAccount, Account.AccountProcess.FINANCIAL, Account.FormulaTypes.TITLE, "", Account.AccountType.DATE, Account.ConsolidationOptions.AGGREGATION,
        Account.PeriodAggregationOptions.AVERAGE_PERIOD, Account.Format.title, (UInt32)Account.FormulaTypes.TITLE, m_accountTV.Nodes.Count) == false)
        MsgBox.Show(m_controller.Error);
    }

    private void OnSaveDescriptionBTClick(object p_sender, EventArgs p_e)
    {
      if (m_currentNode != null)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue(m_currentAccount)) != null)
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
          m_formulaTextBox.Text += "\"" + l_node.Text + "\"[n]";
      }
    }

    private void OnGlobalFactTVMouseDoubleClick(object p_sender, EventArgs p_e)
    {
      if (m_isEditingFormulaFlag == true)
      {
        vTreeNode l_node = m_globalFactsTV.FindAtPosition(((MouseEventArgs)p_e).Location);
        if (l_node != null)
          m_formulaTextBox.Text += "\"" + l_node.Text + "\"[n]";
      }
    }

    void MoveAccount(vTreeNode p_node1, vTreeNode p_node2)
    {
      if (p_node1 == null || p_node2 == null)
        return;
      Account l_account = AccountModel.Instance.GetValue((UInt32)p_node1.Value);
      Account l_account2 = AccountModel.Instance.GetValue((UInt32)p_node2.Value);

      if (l_account != null && l_account2 != null)
      {
        Int32 l_prevPos = (m_updatedAccountPos.ContainsKey(l_account.Id)) ? m_updatedAccountPos[l_account.Id] : l_account.ItemPosition;
        Int32 l_prevPos2 = (m_updatedAccountPos.ContainsKey(l_account2.Id)) ? m_updatedAccountPos[l_account2.Id] : l_account2.ItemPosition;

        m_updatedAccountPos[l_account.Id] = l_prevPos2;
        m_updatedAccountPos[l_account2.Id] = l_prevPos;
      }
    }

    private void OnAccountTVKeyDown(object p_sender, KeyEventArgs p_e)
    {
      switch (p_e.KeyCode)
      {
        case Keys.Down:
          if (p_e.Control == true)
          {
            m_currentNode = m_accountTV.SelectedNode;
            if (m_currentNode != null)
            {
              MoveAccount(m_currentNode, m_currentNode.NextSiblingNode);
              m_accountTV.MoveNodeDown(m_currentNode);
            }
          }
          break;
        case Keys.Up:
          if (p_e.Control == true)
          {
            m_currentNode = m_accountTV.SelectedNode;
            if (m_currentNode != null)
            {
              MoveAccount(m_currentNode, m_currentNode.PrevSiblingNode);
              m_accountTV.MoveNodeUp(m_currentNode);
            }
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
          m_currentAccount = (UInt32)m_currentNode.Value;
          DesactivateUnallowed();
          DisplayAttributes();
        }
        else
          m_accountTV.Capture = false;
      }
    }

    private void OnAccountsTreeviewMouseDown(object p_sender, MouseEventArgs p_e)
    {
      if (m_accountTV.FindAtPosition(new System.Drawing.Point(p_e.X, p_e.Y)) != null)
        m_currentNode = m_accountTV.FindAtPosition(new System.Drawing.Point(p_e.X, p_e.Y));
      if (m_currentNode != null && ModifierKeys.HasFlag(Keys.Control) == true)
        m_accountTV.DoDragDrop(m_currentNode, DragDropEffects.Move);
    }

    void OnGlobalFactTVMouseDown(object sender, MouseEventArgs p_e)
    {
      vTreeNode l_node = m_globalFactsTV.FindAtPosition(new System.Drawing.Point(p_e.X, p_e.Y));
      
      if (l_node != null)
        m_globalFactsTV.DoDragDrop(l_node, DragDropEffects.Move);
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

    private void OnProcessCBSelectedItemChanged(object p_sender, EventArgs p_e)
    {
      if (m_isDisplayingAccountFlag == false && m_currentNode != null)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue(m_currentAccount)) != null && ((vComboBox)p_sender).SelectedItem != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          l_currentAccount.Process = (Account.AccountProcess)((vComboBox)p_sender).SelectedItem.Value;
          if (m_controller.UpdateAccount(l_currentAccount) == false)
            MsgBox.Show(m_controller.Error);
        }
      }
    }

    private void OnTypeCBSelectedItemChanged(object p_sender, EventArgs p_e)
    {
      if (m_currentNode == null)
        return;
      if (m_isDisplayingAccountFlag == false)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue(m_currentAccount)) != null && ((vComboBox)p_sender).SelectedItem != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          l_currentAccount.Type = (Account.AccountType)((vComboBox)p_sender).SelectedItem.Value;
          if (m_controller.UpdateAccount(l_currentAccount) == false)
            MsgBox.Show(m_controller.Error);
        }
      }
      if ((Account.AccountType)((vComboBox)p_sender).SelectedItem.Value == Account.AccountType.MONETARY)
      {
        CurrencyCB.Visible = true;
        m_accountCurrenciesConversionLabel.Visible = true;
      }
      else
      {
        CurrencyCB.Visible = false;
        m_accountCurrenciesConversionLabel.Visible = false;
      }
    }

    void OnFormatCBSelectedItemChanged(object p_sender, EventArgs p_e)
    {
      if (m_isDisplayingAccountFlag == false && m_currentNode != null)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue(m_currentAccount)) != null && ((vComboBox)p_sender).SelectedItem != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          l_currentAccount.FormatId = (Account.Format)((vComboBox)p_sender).SelectedItem.Value;
          if (m_controller.UpdateAccount(l_currentAccount) == false)
            MsgBox.Show(m_controller.Error);
        }
      }
    }

    private void OnCurrencyCBSelectedItemChanged(object p_sender, EventArgs p_e)
    {
      if (m_isDisplayingAccountFlag == false && m_currentNode != null)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue(m_currentAccount)) != null && ((vComboBox)p_sender).SelectedItem != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          l_currentAccount.ConversionOptionId = (Account.ConversionOptions)((vComboBox)p_sender).SelectedItem.Value;
          if (m_controller.UpdateAccount(l_currentAccount) == false)
            MsgBox.Show(m_controller.Error);
        }
      }
    }

    private void OnConsolidationOptionCBSelectedItemChanged(object p_sender, EventArgs p_e)
    {
      if (m_isDisplayingAccountFlag == false && m_currentNode != null)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue(m_currentAccount)) != null && ((vComboBox)p_sender).SelectedItem != null)
        {
          l_currentAccount = l_currentAccount.Clone();
          l_currentAccount.ConsolidationOptionId = (Account.ConsolidationOptions)((vComboBox)p_sender).SelectedItem.Value;
          if (m_controller.UpdateAccount(l_currentAccount) == false)
            MsgBox.Show(m_controller.Error);
        }
      }
    }

    private void OnFormulaTypeCBSelectedItemChanged(object p_sender, EventArgs p_e)
    {
      if (m_isDisplayingAccountFlag == false && m_currentNode != null)
      {
        Account l_currentAccount;

        if ((l_currentAccount = AccountModel.Instance.GetValue(m_currentAccount)) != null && ((vComboBox)p_sender).SelectedItem != null)
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
                  MessageBox.Show(Local.GetValue("accounts.msg_incorrect_password"), Local.GetValue("general.accounts"),
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

    void OnExpandRightMouseDown(object sender, MouseEventArgs e)
    {
      ExpandFactPane();
    }

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
        Account l_account = AccountModel.Instance.GetValue(m_currentAccount);

        if (l_account == null)
          return;

        m_isDisplayingAccountFlag = true;

        //Name
        Name_TB.Text = m_currentNode.Text;

        //Process
        if (m_processIdItemDict.ContainsKey(l_account.Process))
        {
          ListItem l_processLI = m_processIdItemDict[l_account.Process];
          ProcessCB.SelectedItem = l_processLI;
        }

        //Formula Type
        if (m_formulaTypeItemDict.ContainsKey(l_account.FormulaType))
        {
          ListItem l_formulaTypeLI = m_formulaTypeItemDict[l_account.FormulaType];
          FormulaTypeCB.SelectedItem = l_formulaTypeLI;

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
          TypeCB.SelectedItem = l_formatLI;
          if ((Account.AccountType)l_formatLI.Value == Account.AccountType.MONETARY)
          {
            if (m_currencyItemDict.ContainsKey(l_account.ConversionOptionId))
            {
              ListItem conversionLI = m_currencyItemDict[l_account.ConversionOptionId];
              CurrencyCB.SelectedItem = conversionLI;
            }
          }
        }

        //Consolidation
        if (m_consoOptionItemDict.ContainsKey(l_account.ConsolidationOptionId))
        {
          ListItem consolidationLI = m_consoOptionItemDict[l_account.ConsolidationOptionId];
          ConsolidationOptionCB.SelectedItem = consolidationLI;
        }

        //Format
        if (m_formatItemDict.ContainsKey(l_account.FormatId))
          m_formatCB.SelectedItem = m_formatItemDict[l_account.FormatId];

        // Formula TB
        m_formulaTextBox.Text = "";
        if (m_controller.m_bnf.Parse(l_account.Formula, FbiGrammar.TO_HUMAN))
          m_formulaTextBox.Text = m_controller.m_bnf.Concatenated;
        else
          m_formulaTextBox.Text = m_controller.m_bnf.LastError;

        //Description
        m_descriptionTextBox.Text = l_account.Description;

        m_isDisplayingAccountFlag = false;
      }
    }

    private void SetEnableStatusEdition(Boolean p_status, Boolean p_isRootAccount, Account p_account)
    {
      if (p_isRootAccount == true)
        FormulaTypeCB.Enabled = false;
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
      ProcessCB.Enabled = p_uiState;
      FormulaTypeCB.Enabled = p_uiState;
      TypeCB.Enabled = p_uiState;
      CurrencyCB.Enabled = p_uiState;
      m_descriptionTextBox.Enabled = p_uiState;
      if (p_uiState == true)
        m_descriptionTextBox.BackColor = Color.White;
      else
        m_descriptionTextBox.BackColor = Color.LightGray;
      ConsolidationOptionCB.Enabled = p_uiState;
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
      ColorFormula();
      if (!p_state)
      {
        m_autocomplete.Hide();
        m_isValidAutoComplete = false;
      }
    }

    #endregion
  }
}
