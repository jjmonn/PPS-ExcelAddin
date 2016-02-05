using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI
{
  using Network;
  using Properties;
  using Utils;

  static class Addin
  {
    static NetworkLauncher m_networkLauncher = new NetworkLauncher();
    static bool m_connectionTaskPaneVisible;

    static void SelectLanguage()
    {
      switch (Settings.Default.language)
      {
        case 0:
          Local.LoadLocalFile(Properties.Resources.english);
          break;
        case 1:
          Local.LoadLocalFile(Properties.Resources.french);
          break;
        default:
          Local.LoadLocalFile(Properties.Resources.english);
          break;
      }
    }

    public static void Main()
    {
      //SelectLanguage();
      Local.LoadLocalFile(Properties.Resources.english);
      m_networkLauncher.Launch("127.0.0.1", 4242);
    }

    #region Accessors

        public static bool ConnectionTaskPaneVisible
        {
            get 
            { 
                return m_connectionTaskPaneVisible; 
            }

            set
            {
                m_connectionTaskPaneVisible = value;
            }
        }

        

    #endregion


  }
}
