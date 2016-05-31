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
      ACCOUNT_TAB,
      DESCRIPTION,
      PROCESS,
      UNDEFINED
    };

    const int m_nbColumns = 200;
    const int m_nbRows = 1000;

    SafeDictionary<string, Column> m_columnNameDic;
    SafeDictionary<Column, int> m_columnScanDic;
    Worksheet m_worksheet;
    int m_beginRow = -1;

    SafeDictionary<Column, Action<Account, object>> m_propertiesDic;

    public AccountSnapshot(Worksheet p_worksheet)
    {
      m_worksheet = p_worksheet;
      BuildColumnNameDic();
      BuildPropertiesDic();
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
      m_propertiesDic[Column.ACCOUNT_TAB] = ReadAccountTab;
      m_propertiesDic[Column.DESCRIPTION] = ReadDescription;
      m_propertiesDic[Column.PROCESS] = ReadProcess;
    }

    void BuildColumnNameDic()
    {
      m_columnNameDic = new SafeDictionary<string,Column>();
      foreach (Column l_col in Enum.GetValues(typeof(Column)))
        m_columnNameDic[l_col.ToString()] = l_col;
    }

    #region Scan

    public void ScanColumns()
    {
      m_columnScanDic = new SafeDictionary<Column, int>();

      for (int l_row = 0; l_row < m_nbRows && m_beginRow < 0; l_row++)
        for (int l_col = 0; l_col < m_nbColumns; l_col++)
          {
            Column l_colFound = GetColumn(m_worksheet.Cells[l_row, l_col]);

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
          l_value = Utils.StringUtils.RemoveDiacritics(l_value);

          if (m_columnNameDic.ContainsKey(l_value))
            return (m_columnNameDic[l_value]);
        }
        catch (InvalidCastException) { }
      }

      return (Column.UNDEFINED);
    }

    public List<Account> ExtractAccounts(SafeDictionary<Column, Action<Account, object>> p_propertiesFunc)
    {
      List<Account> l_list = new List<Account>();

      for (int l_row = m_beginRow; l_row < m_nbRows; l_row++)
      {
        Account l_account = null;

        foreach (KeyValuePair<Column, int> l_column in m_columnScanDic)
          if (p_propertiesFunc.ContainsKey(l_column.Key))
          {
            if (l_account == null)
              l_account = new Account();
            object l_value = m_worksheet.Cells[l_row, l_column.Value];

            if (l_value != null)
            {
              try
              {
                p_propertiesFunc[l_column.Key](l_account, l_value);
              }
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
      p_account.ParentId = AccountModel.Instance.GetValueId(p_value as string);
    }

    void ReadFormulaType(Account p_account, object p_value)
    {
      p_account.FormulaType = (Account.FormulaTypes)Enum.Parse(typeof(Account.FormulaTypes), p_value as string, true);
    }

    void ReadFormula(Account p_account, object p_value)
    {
      p_account.Formula = p_value as string;
    }

    void ReadConsolidationOption(Account p_account, object p_value)
    {
      p_account.ConsolidationOptionId = (Account.ConsolidationOptions)Enum.Parse(typeof(Account.ConsolidationOptions), p_value as string, true);
    }

    void ReadConversionOption(Account p_account, object p_value)
    {
      p_account.ConversionOptionId = (Account.ConversionOptions)Enum.Parse(typeof(Account.ConversionOptions), p_value as string, true);
    }

    void ReadPeriodAggregationOption(Account p_account, object p_value)
    {
      p_account.PeriodAggregationOptionId = (Account.PeriodAggregationOptions)Enum.Parse(typeof(Account.PeriodAggregationOptions), p_value as string, true);
    }

    void ReadItemPosition(Account p_account, object p_value)
    {
      p_account.ItemPosition = (Int32)p_value;
    }

    void ReadAccountTab(Account p_account, object p_value)
    {
      p_account.AccountTab = (Int32)p_value;
    }

    void ReadDescription(Account p_account, object p_value)
    {
      p_account.Description = p_value as string;
    }

    void ReadProcess(Account p_account, object p_value)
    {
      p_account.Process = (Account.AccountProcess)Enum.Parse(typeof(Account.AccountProcess), p_value as string, true);
    }

    void ReadType(Account p_account, object p_value)
    {
      p_account.Type = (Account.AccountType)Enum.Parse(typeof(Account.AccountType), p_value as string, true);
    }

    #endregion

  }
}
