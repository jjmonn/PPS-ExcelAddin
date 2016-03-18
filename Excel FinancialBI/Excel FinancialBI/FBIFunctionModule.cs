using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AddinExpress.MSO;
using Microsoft.Office.Interop; 

namespace FBI
{
    [GuidAttribute("25498587-2177-44F8-AF4C-FF5B4C74432D"),
    ProgId("FBI.FBIFunctionModule"), ClassInterface(ClassInterfaceType.AutoDual)]
    public partial class FBIFunctionModule : AddinExpress.MSO.ADXExcelAddinModule
    {
        public FBIFunctionModule()
        {
            InitializeComponent();
        }
 
        #region Add-in Express automatic code
 
        [ComRegisterFunctionAttribute]
        public static void AddinRegister(Type t)
        {
            AddinExpress.MSO.ADXExcelAddinModule.ADXExcelAddinRegister(t);
        }
 
        [ComUnregisterFunctionAttribute]
        public static void AddinUnregister(Type t)
        {
            AddinExpress.MSO.ADXExcelAddinModule.ADXExcelAddinUnregister(t);
        }

        public object FBI(object p_a, object p_b)
        {
          try
          {
    //       COMObject obj;
            Microsoft.Office.Interop.Excel._Application l_hostApplication = this.HostApplication as Microsoft.Office.Interop.Excel._Application;
            System.Diagnostics.Debug.WriteLine(AppDomain.CurrentDomain.ToString());

            dynamic l_module = l_hostApplication.COMAddIns.Item("FBI.AddinModule").Object;
            AddinModule l_m = AddinModule.CurrentInstance;

            if (Network.NetworkManager.IsConnected() == false)
              return ("Not connected");
            return (42);
          }
          catch
          {
            return ("Unable to load FBI function");
          }
        }
        #endregion
 
    }
}

