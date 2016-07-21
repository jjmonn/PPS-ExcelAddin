using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;
using AddinExpress.MSO;
using Microsoft.Office.Interop;
using Microsoft.Office;

namespace FBI
{
    /// <summary>
    ///   Add-in Express XLL Add-in Module
    /// </summary>
    [ComVisible(true)]
    public partial class FinancialBiXLL : AddinExpress.MSO.ADXXLLModule
    {
        public FinancialBiXLL()
        {
            InitializeComponent();
          // Please add any initialization code to the OnInitialize event handler
        }
 
        #region Add-in Express automatic code
 
        // Required by Add-in Express - do not modify
        // the methods within this region
 
        public override System.ComponentModel.IContainer GetContainer()
        {
            if (components == null)
                components = new System.ComponentModel.Container();
            return components;
        }
 
        [ComRegisterFunctionAttribute]
        public static void RegisterXLL(Type t)
        {
          AddinExpress.MSO.ADXXLLModule.RegisterXLLInternal(t);
        }
 
        [ComUnregisterFunctionAttribute]
        public static void UnregisterXLL(Type t)
        {
            AddinExpress.MSO.ADXXLLModule.UnregisterXLLInternal(t);
        }
 
        #endregion
 
        public static new FinancialBiXLL CurrentInstance
        {
            get
            {
                return AddinExpress.MSO.ADXXLLModule.CurrentInstance as FinancialBiXLL;
            }
        }

        #region Define your UDFs in this section
 
        /// <summary>
        /// The container for user-defined functions (UDFs). Every UDF is a public static (Public Shared in VB.NET) method that returns a value of any base type: string, double, integer.
        /// </summary>
        internal static class XLLContainer
        {
            /// <summary>
            /// Required by Add-in Express. Please do not modify this method.
            /// </summary>
            internal static FinancialBiXLL Module
            {
                get
                {
                    return AddinExpress.MSO.ADXXLLModule.
                        CurrentInstance as FBI.FinancialBiXLL;
                }
            }

            public static object FBI(object p_entity, object p_account, object p_aggregation, object p_period, object p_currency, object p_version,
              object p_clientsFilters, object p_productsFilters, object p_adjustmentsFilters, object p_categoriesFilters)
            {
              try
              {
                AddinModule l_module = AddinModule.CurrentInstance;
                Microsoft.Office.Interop.Excel.Application l_app = (dynamic)CurrentInstance.HostApplication;

                ADXExcelRef l_caller =
                            Module.CallWorksheetFunction(ADXExcelWorksheetFunction.Caller) as ADXExcelRef;

                if (l_app.ActiveSheet != l_module.ExcelApp.ActiveSheet)
                  return ("invalid worksheet");
                if (Network.NetworkManager.IsConnected() == false || l_module == null)
                  return ((l_caller == null) ? "not connected" : l_caller.GetValue());
                return (l_module.FBIFunctionController.FBI(p_entity, p_account, p_aggregation, p_period, p_currency, p_version, 
                  p_clientsFilters, p_productsFilters, p_adjustmentsFilters, p_categoriesFilters));
              }
              catch (Exception e)
              {
                System.Diagnostics.Debug.WriteLine(e.Message);
                if (e.InnerException != null)
                  System.Diagnostics.Debug.WriteLine(e.InnerException.Message);
                return ("Unable to load FBI function");
              }
            }
        }
 
        #endregion
    }
}

