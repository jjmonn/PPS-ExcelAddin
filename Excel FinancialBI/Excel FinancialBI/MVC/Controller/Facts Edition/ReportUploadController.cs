using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using FBI.MVC.Model;
  using FBI.MVC.Model.CRUD;
  using FBI.MVC.View;
  using Utils;
  using VIBlend.WinForms.Controls;
  using Microsoft.Office.Interop.Excel;
  using Forms;


  class ReportUploadController
  {
    Version m_version;
    AxisElem m_entity;
    Account.AccountProcess m_process;
    public string m_errorMessage { private set; get; }
    Account m_RHAccount;
    List<Int32> m_periodList;


    public ReportUploadController(UInt32 p_versionId)
    {
      m_version = VersionModel.Instance.GetValue(p_versionId);
    }

    public bool CanLaunchReport(vTreeNode p_entityNode, Account.AccountProcess p_process, ListItem p_RHAccountItem, List<Int32> p_periodList)
    {
      if (m_version == null)
      {
        m_errorMessage = Local.GetValue("versions.error.no_selected_version");       
        return (false);
      }

      if (p_entityNode == null)
      {
        m_errorMessage = Local.GetValue("upload.msg_select_entity");
        return (false);
      }

      if (IsInputEntity((UInt32)p_entityNode.Value) == false)
        return (false);
       
      if (p_process == Account.AccountProcess.RH)
        return AreRHInputsValid(p_RHAccountItem, p_periodList);

      m_process = p_process;
      return (true);
    }

    public bool IsInputEntity(UInt32 p_entityId)
    {
      AxisElem l_entity = AxisElemModel.Instance.GetValue(p_entityId);
      if (l_entity == null)
        return (false);
      if (l_entity.AllowEdition == false)
        return (false);
      m_entity = l_entity;
      return (true);
    }

    public bool AreRHInputsValid(ListItem p_RHAccountItem, List<Int32> p_periodList)
    {
      if (p_RHAccountItem == null)
      {
        m_errorMessage = Local.GetValue("upload.msg_invalidRHAccount");
        return (false);
      }
      else
        m_RHAccount = AccountModel.Instance.GetValue((UInt32)p_RHAccountItem.Value);

      if (m_RHAccount == null)
      {
        m_errorMessage = Local.GetValue("upload.msg_invalidRHAccount");
        return (false);
      }
      if (p_periodList == null)
      {
        m_errorMessage = Local.GetValue("upload.invalid_period_range");
        return (false);
      }
      else
        m_periodList = p_periodList;
      return (true);
    }

    public bool CreateReport()
    {
      AddinModule.CurrentInstance.ExcelApp.ScreenUpdating = false;
      AddinModule.CurrentInstance.ExcelApp.Interactive = false;
      bool l_result = false;
      EntityCurrency l_entityCurrency = EntityCurrencyModel.Instance.GetValue(m_entity.Id);
      if (l_entityCurrency == null)
        return (l_result);

      if (m_process == Account.AccountProcess.FINANCIAL)
        l_result = InputReportCreationProcessFinancial(l_entityCurrency);
      else
        l_result = InputReportCreationProcessRH(l_entityCurrency);
    
      AddinModule.CurrentInstance.ExcelApp.Interactive = true;
      AddinModule.CurrentInstance.ExcelApp.ScreenUpdating = true; 
      return l_result;
    }

    private bool InputReportCreationProcessFinancial(EntityCurrency p_entityCurrency)
    {
	    Currency l_currency = CurrencyModel.Instance.GetValue(p_entityCurrency.CurrencyId);
      if (l_currency == null)
        return (false);

      string[] l_headerNames = new string[] {Local.GetValue("general.entity"),Local.GetValue("general.currency"),Local.GetValue("general.version")};
      string[] l_headerValues = new string[] {m_entity.Name, l_currency.Name, m_version.Name};

	    Range currentcell = ExcelUtils.CreateReceptionWS(m_entity.Name, l_headerNames, l_headerValues);
	    if (currentcell == null) 
      {
		    m_errorMessage = Local.GetValue("upload.msg_error_upload");
        return (false);
      }
      
	    List<Int32> l_periodList = PeriodModel.GetPeriodsList(m_version.Id);
	    if (l_periodList == null)
		    return (false);
	
      InsertFinancialInputReportOnWS(currentcell, l_periodList, l_currency);
	    return (true);
    }

    private void InsertFinancialInputReportOnWS(Range p_destinationcell, List<Int32> p_periodList, Currency p_currency)
    {
      FbiTreeView<Account> l_accountsTreeview = new FbiTreeView<Account>(AccountModel.Instance.GetDictionary());
      ExcelUtils.WriteAccountsFromTreeView(l_accountsTreeview, p_destinationcell, p_periodList);
      if (p_periodList.Count > 0) 
        ExcelFormatting.FormatFinancialExcelRange(p_destinationcell, p_currency.Id, DateTime.FromOADate(p_periodList.ElementAt(0)));
	 }

    private bool InputReportCreationProcessRH(EntityCurrency p_entityCurrency)
{
      string[] l_headerNames = new string[] {Local.GetValue("general.account"), Local.GetValue("general.entity"),Local.GetValue("general.version")};
      string[] l_headerValues = new string[] {m_RHAccount.Name, m_entity.Name, m_version.Name};
	
      Range currentcell = ExcelUtils.CreateReceptionWS(m_entity.Name, l_headerNames, l_headerValues);
	    if (currentcell == null) 
      {
		    m_errorMessage = Local.GetValue("upload.msg_error_upload");
        return (false);
      }

	    currentcell = currentcell.Offset[1, 0];
	    InsertRHInputReportOnWS(currentcell);
	    ExcelFormatting.FormatRHExcelRange(currentcell);
      return (true);
}

    private void InsertRHInputReportOnWS(Range p_destinationCell)
    {
      ExcelUtils.WritePeriodsOnWorksheet(p_destinationCell, m_periodList);

      List<string> p_employeesNameList = new List<string>();
      foreach (AxisElem l_employee in AxisElemModel.Instance.GetDictionary(AxisType.Employee).Values)
      {
        if (l_employee.ParentId == m_entity.Id)
          p_employeesNameList.Add(l_employee.Name);
      }
      ExcelUtils.WriteListOnWorksheet(p_destinationCell, p_employeesNameList);
    }

 
  }
}
