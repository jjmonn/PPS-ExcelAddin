using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace FBI.MVC.View
{
  using Model.CRUD;
  using Model;
  using Controller;
  using Utils;
  using Network;

  class AccountEditSnapshot : IView
  {
    enum Column
    {
      NAME,
      PARENT,
      FORMULA_TYPE,
      FORMULA,
      TYPE,
      CONSOLIDATION_OPTION,
      CONVERSION_OPTION,
      PERIOD_AGGREGATION_OPTION,
      ITEM_POSITION,
      DESCRIPTION,
      PROCESS,
      UNDEFINED
    };

    const int m_nbColumns = 200;
    const int m_nbRows = 1000;

    AccountEditSnapshotController m_controller;
    SafeDictionary<string, Column> m_columnNameDic;
    SafeDictionary<Column, int> m_columnScanDic;
    Worksheet m_worksheet;
    int m_beginRow = -1;
    BNF m_bnf;
    public string Error { get; private set; }

    delegate bool propertyFunc(Account p_account, object p_value);

    SafeDictionary<Column, KeyValuePair<propertyFunc, string>> m_propertiesDic;

    public AccountEditSnapshot(Worksheet p_worksheet)
    {
      m_worksheet = p_worksheet;
      m_bnf = new BNF();
      FbiGrammar.AddGrammar(m_bnf);
      BuildColumnNameDic();
      BuildPropertiesDic();
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as AccountEditSnapshotController;
    }

    void BuildPropertiesDic()
    {
      m_propertiesDic = new SafeDictionary<Column, KeyValuePair<propertyFunc, string>>();

      m_propertiesDic[Column.NAME] = new KeyValuePair<propertyFunc, string>(ReadName, Local.GetValue("accounts.name"));
      m_propertiesDic[Column.PARENT] = new KeyValuePair<propertyFunc, string>(ReadParent, Local.GetValue("accounts.parent"));
      m_propertiesDic[Column.FORMULA_TYPE] = new KeyValuePair<propertyFunc, string>(ReadFormulaType, Local.GetValue("accounts.formula_type"));
      m_propertiesDic[Column.FORMULA] = new KeyValuePair<propertyFunc, string>(ReadFormula, Local.GetValue("accounts.formula"));
      m_propertiesDic[Column.TYPE] = new KeyValuePair<propertyFunc, string>(ReadType, Local.GetValue("accounts.type"));
      m_propertiesDic[Column.CONSOLIDATION_OPTION] = new KeyValuePair<propertyFunc, string>(ReadConsolidationOption, Local.GetValue("accounts.consolidation_option"));
      m_propertiesDic[Column.CONVERSION_OPTION] = new KeyValuePair<propertyFunc, string>(ReadConversionOption, Local.GetValue("accounts.currencies_conversion"));
      m_propertiesDic[Column.PERIOD_AGGREGATION_OPTION] = new KeyValuePair<propertyFunc, string>(ReadPeriodAggregationOption, Local.GetValue("accounts.period_aggregation"));
      m_propertiesDic[Column.ITEM_POSITION] = new KeyValuePair<propertyFunc, string>(ReadItemPosition, Local.GetValue("accounts.position"));
      m_propertiesDic[Column.DESCRIPTION] = new KeyValuePair<propertyFunc, string>(ReadDescription, Local.GetValue("accounts.description"));
      m_propertiesDic[Column.PROCESS] = new KeyValuePair<propertyFunc, string>(ReadProcess, Local.GetValue("accounts.process"));
    }

    void BuildColumnNameDic()
    {
      m_columnNameDic = new SafeDictionary<string,Column>();
      foreach (Column l_col in Enum.GetValues(typeof(Column)))
        m_columnNameDic[l_col.ToString()] = l_col;
    }

    public void CreateReport()
    {
      List<Account> l_accountList = AccountModel.Instance.GetDictionary().SortedValues;

      Range l_beginCell = Addin.AddinModule.ExcelApp.ActiveCell;

      int l_index = 0;
      foreach (Column l_column in Enum.GetValues(typeof(Column)))
        if (l_column != Column.UNDEFINED)
          m_worksheet.Cells[l_beginCell.Row, l_beginCell.Column + l_index++] = l_column.ToString();
      l_index = 1;
      foreach (Account l_account in l_accountList)
      {
        if (!m_bnf.Parse(l_account.Formula, FbiGrammar.TO_HUMAN))
          continue;
        WriteReportValue(l_beginCell, Column.NAME, l_account.Name, l_index);
        WriteReportValue(l_beginCell, Column.PARENT, AccountModel.Instance.GetValueName(l_account.ParentId), l_index);
        WriteReportValue(l_beginCell, Column.FORMULA_TYPE, ((Account.FormulaTypes)l_account.FormulaType).ToString(), l_index);
        WriteReportValue(l_beginCell, Column.FORMULA_TYPE, ((Account.FormulaTypes)l_account.FormulaType).ToString(), l_index);
        WriteReportValue(l_beginCell, Column.FORMULA, m_bnf.Concatenated, l_index);
        WriteReportValue(l_beginCell, Column.TYPE, ((Account.AccountType)l_account.Type).ToString(), l_index);
        WriteReportValue(l_beginCell, Column.CONSOLIDATION_OPTION, ((Account.ConsolidationOptions)l_account.ConsolidationOptionId).ToString(), l_index);
        WriteReportValue(l_beginCell, Column.CONVERSION_OPTION, ((Account.ConversionOptions)l_account.ConversionOptionId).ToString(), l_index);
        WriteReportValue(l_beginCell, Column.PERIOD_AGGREGATION_OPTION, ((Account.PeriodAggregationOptions)l_account.PeriodAggregationOptionId).ToString(), l_index);
        WriteReportValue(l_beginCell, Column.ITEM_POSITION, l_account.ItemPosition.ToString(), l_index);
        WriteReportValue(l_beginCell, Column.DESCRIPTION, l_account.Description, l_index);
        WriteReportValue(l_beginCell, Column.PROCESS, ((Account.AccountProcess)l_account.Process).ToString(), l_index);

        l_index++;
      }
    }

    void WriteReportValue(Range p_baseCell, Column p_column, string p_value, int p_row)
    {
      Array l_cols = Enum.GetValues(typeof(Column));

      for (int i = 0; i < l_cols.Length; ++i)
      {
        Range l_cell = (Range)m_worksheet.Cells[p_baseCell.Row, p_baseCell.Column + i];

        try
        {
          if ((string)l_cell.Value2 == p_column.ToString())
          {
            l_cell = (Range)m_worksheet.Cells[p_baseCell.Row + p_row, p_baseCell.Column + i];
            l_cell.Value2 = p_value;
          }
        }
        catch (InvalidCastException) { }
      }
    }

    #region Scan

    public void ScanColumns()
    {
      m_columnScanDic = new SafeDictionary<Column, int>();

      for (int l_row = 1; l_row < m_nbRows && m_beginRow < 0; l_row++)
        for (int l_col = 1; l_col < m_nbColumns; l_col++)
          {
            Column l_colFound = GetColumn(((Range)m_worksheet.Cells[l_row, l_col]).Value2);

            if (l_colFound != Column.UNDEFINED)
            {
              m_columnScanDic[l_colFound] = l_col;
              if (m_beginRow < 0)
                m_beginRow = l_row + 1;
            }
          }
    }

    Column GetColumn(object p_value)
    {
      if (p_value != null)
      {
        try
        {
          string l_value = p_value as string;
          l_value = Utils.StringUtils.RemoveDiacritics(l_value).ToUpper();

          if (m_columnNameDic.ContainsKey(l_value))
            return (m_columnNameDic[l_value]);
        }
        catch (InvalidCastException) { }
      }

      return (Column.UNDEFINED);
    }

    public List<Account> ExtractAccounts()
    {
      List<Account> l_list = new List<Account>();

      for (int l_row = m_beginRow; l_row < m_nbRows; l_row++)
      {
        Account l_account = null;

        foreach (KeyValuePair<Column, int> l_column in m_columnScanDic)
          if (m_propertiesDic.ContainsKey(l_column.Key))
          {
            object l_value = ((Range)m_worksheet.Cells[l_row, l_column.Value]).Value2;

            if (l_value != null)
            {
              if (l_account == null)
                l_account = new Account();
              try 
              {
                if (!m_propertiesDic[l_column.Key].Key(l_account, l_value))
                {
                  Error = Local.GetValue("general.error.account_snapshot_at_line") + " " + l_row.ToString() + ": " + m_propertiesDic[l_column.Key].Value;
                  return (null);
                }
              }
              catch (InvalidCastException)
              {
                Error = Local.GetValue("general.error.account_snapshot_at_line") + " " + l_row.ToString() + ": " + m_propertiesDic[l_column.Key].Value;
                return (null);
              }
            }
          }
        if (l_account != null)
          l_list.Add(l_account);
      }

      return (l_list);
    }

    #endregion

    #region Properties

    bool ReadName(Account p_account, object p_value)
    {
      p_account.Name = p_value as string;
      return (true);
    }

    bool ReadParent(Account p_account, object p_value)
    {
      p_account.ParentId = AccountModel.Instance.GetValueId((string)p_value);
      return (true);
    }

    bool ReadFormulaType(Account p_account, object p_value)
    {
      Account.FormulaTypes l_type = p_account.FormulaType;
      if (!Enum.TryParse<Account.FormulaTypes>((string)p_value, true, out l_type))
        return (false);
      p_account.FormulaType = l_type;
      return (true);
    }

    bool ReadFormula(Account p_account, object p_value)
    {
      if (!m_bnf.Parse((string)p_value, FbiGrammar.TO_SERVER))
        return (false);
      p_account.Formula = m_bnf.Concatenated;
      return (true);
    }

    bool ReadConsolidationOption(Account p_account, object p_value)
    {
      Account.ConsolidationOptions l_option = p_account.ConsolidationOptionId;
      if (!Enum.TryParse<Account.ConsolidationOptions>((string)p_value, true, out l_option))
        return (false);
      p_account.ConsolidationOptionId = l_option;
      return (true);
    }

    bool ReadConversionOption(Account p_account, object p_value)
    {
      Account.ConversionOptions l_option = p_account.ConversionOptionId;
      if (!Enum.TryParse<Account.ConversionOptions>((string)p_value, true, out l_option))
        return (false);
      p_account.ConversionOptionId = l_option;
      return (true);
    }

    bool ReadPeriodAggregationOption(Account p_account, object p_value)
    {
      Account.PeriodAggregationOptions l_option = p_account.PeriodAggregationOptionId;
      if (!Enum.TryParse<Account.PeriodAggregationOptions>((string)p_value, true, out l_option))
        return (false);
      p_account.PeriodAggregationOptionId = l_option;
      return (true);
    }

    bool ReadItemPosition(Account p_account, object p_value)
    {
      p_account.ItemPosition = (Int32)(double)p_value;
      return (true);
    }

    bool ReadDescription(Account p_account, object p_value)
    {
      p_account.Description = p_value as string;
      return (true);
    }

    bool ReadProcess(Account p_account, object p_value)
    {
      Account.AccountProcess l_process = p_account.Process;
      if (!Enum.TryParse<Account.AccountProcess>((string)p_value, true, out l_process))
        return (false);
      p_account.Process = l_process;
      return (true);
    }

    bool ReadType(Account p_account, object p_value)
    {
      Account.AccountType l_type = p_account.Type;
      if (!Enum.TryParse<Account.AccountType>((string)p_value, true, out l_type))
        return (false);
      p_account.Type = l_type;
      return (true);
    }

    #endregion

  }
}
