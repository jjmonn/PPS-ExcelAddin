using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace FBI.MVC.View
{
  using Model.CRUD;
  using Model;
  using Controller;
  using Utils;

  class AccountSnapshot : IView
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

    AccountSnapshotController m_controller;
    SafeDictionary<string, Column> m_columnNameDic;
    SafeDictionary<Column, int> m_columnScanDic;
    Worksheet m_worksheet;
    int m_beginRow = -1;
    BNF m_bnf;

    SafeDictionary<Column, Action<Account, object>> m_propertiesDic;

    public AccountSnapshot(Worksheet p_worksheet)
    {
      m_worksheet = p_worksheet;
      m_bnf = new BNF();
      FbiGrammar.AddGrammar(m_bnf);
      BuildColumnNameDic();
      BuildPropertiesDic();
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as AccountSnapshotController;
    }

    void BuildPropertiesDic()
    {
      m_propertiesDic = new SafeDictionary<Column, Action<Account, object>>();

      m_propertiesDic[Column.NAME] = ReadName;
      m_propertiesDic[Column.PARENT] = ReadParent;
      m_propertiesDic[Column.FORMULA_TYPE] = ReadFormulaType;
      m_propertiesDic[Column.FORMULA] = ReadFormula;
      m_propertiesDic[Column.TYPE] = ReadType;
      m_propertiesDic[Column.CONSOLIDATION_OPTION] = ReadConsolidationOption;
      m_propertiesDic[Column.CONVERSION_OPTION] = ReadConversionOption;
      m_propertiesDic[Column.PERIOD_AGGREGATION_OPTION] = ReadPeriodAggregationOption;
      m_propertiesDic[Column.ITEM_POSITION] = ReadItemPosition;
      m_propertiesDic[Column.DESCRIPTION] = ReadDescription;
      m_propertiesDic[Column.PROCESS] = ReadProcess;
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
              try { m_propertiesDic[l_column.Key](l_account, l_value); }
              catch (InvalidCastException) { }
            }
          }
        if (l_account != null)
          l_list.Add(l_account);
      }

      return (l_list);
    }

    #endregion

    #region Properties

    void ReadName(Account p_account, object p_value)
    {
      p_account.Name = p_value as string;
    }

    void ReadParent(Account p_account, object p_value)
    {
      p_account.ParentId = AccountModel.Instance.GetValueId((string)p_value);
    }

    void ReadFormulaType(Account p_account, object p_value)
    {
      p_account.FormulaType = (Account.FormulaTypes)Enum.Parse(typeof(Account.FormulaTypes), (string)p_value, true);
    }

    void ReadFormula(Account p_account, object p_value)
    {
      if (!m_bnf.Parse((string)p_value, FbiGrammar.TO_SERVER))
        return;
      p_account.Formula = m_bnf.Concatenated;
    }

    void ReadConsolidationOption(Account p_account, object p_value)
    {
      p_account.ConsolidationOptionId = (Account.ConsolidationOptions)Enum.Parse(typeof(Account.ConsolidationOptions), (string)p_value, true);
    }

    void ReadConversionOption(Account p_account, object p_value)
    {
      p_account.ConversionOptionId = (Account.ConversionOptions)Enum.Parse(typeof(Account.ConversionOptions), (string)p_value, true);
    }

    void ReadPeriodAggregationOption(Account p_account, object p_value)
    {
      p_account.PeriodAggregationOptionId = (Account.PeriodAggregationOptions)Enum.Parse(typeof(Account.PeriodAggregationOptions), (string)p_value, true);
    }

    void ReadItemPosition(Account p_account, object p_value)
    {
      p_account.ItemPosition = (Int32)(double)p_value;
    }

    void ReadDescription(Account p_account, object p_value)
    {
      p_account.Description = p_value as string;
    }

    void ReadProcess(Account p_account, object p_value)
    {
      p_account.Process = (Account.AccountProcess)Enum.Parse(typeof(Account.AccountProcess), (string)p_value, true);
    }

    void ReadType(Account p_account, object p_value)
    {
      p_account.Type = (Account.AccountType)Enum.Parse(typeof(Account.AccountType), (string)p_value, true);
    }

    #endregion

  }
}
