using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Utils
{
  using MVC.Model.CRUD;
  using MVC.Model;

  class FbiAccountFormat
  {
    public Color textColor;
    public Color backColor;
    public Color bordersColor;
    public bool isBold;
    public bool isItalic;
    public bool bordersPresent;
    public UInt32 indent;

    public FbiAccountFormat(string p_formatCode)
    {
      switch (p_formatCode)
      {
        case "t":
          textColor = FBI.Properties.Settings.Default.titleFontColor;
          backColor = FBI.Properties.Settings.Default.titleBackColor;
          isBold = FBI.Properties.Settings.Default.titleFontBold;
          isItalic = FBI.Properties.Settings.Default.titleFontItalic;
          bordersPresent = FBI.Properties.Settings.Default.titleBordersPresent;
          bordersColor = FBI.Properties.Settings.Default.titleBordersColor;
          indent = FBI.Properties.Settings.Default.titleIndent;
          break;
        case "i":
          textColor = FBI.Properties.Settings.Default.importantFontColor;
          backColor = FBI.Properties.Settings.Default.importantBackColor;
          isBold = FBI.Properties.Settings.Default.importantFontBold;
          isItalic = FBI.Properties.Settings.Default.importantFontItalic;
          bordersPresent = FBI.Properties.Settings.Default.importantBordersPresent;
          bordersColor = FBI.Properties.Settings.Default.importantBordersColor;
          indent = FBI.Properties.Settings.Default.importantIndent;
          break;
        case "n":
          textColor = FBI.Properties.Settings.Default.normalFontColor;
          backColor = FBI.Properties.Settings.Default.normalBackColor;
          isBold = FBI.Properties.Settings.Default.normalFontBold;
          isItalic = FBI.Properties.Settings.Default.normalFontItalic;
          bordersPresent = FBI.Properties.Settings.Default.normalBordersPresent;
          bordersColor = FBI.Properties.Settings.Default.normalBordersColor;
          indent = FBI.Properties.Settings.Default.normalIndent;
          break;
        case "d":
          textColor = FBI.Properties.Settings.Default.detailFontColor;
          backColor = FBI.Properties.Settings.Default.detailBackColor;
          isBold = FBI.Properties.Settings.Default.detailFontBold;
          isItalic = FBI.Properties.Settings.Default.detailFontItalic;
          bordersPresent = FBI.Properties.Settings.Default.detailBordersPresent;
          bordersColor = FBI.Properties.Settings.Default.detailBordersColor;
          indent = FBI.Properties.Settings.Default.detailIndent;
          break;
      }
    }

    public static string Get(Account.AccountType? p_accountType, Currency p_currency)
    {
      if (p_currency == null || p_accountType == null)
        return ("{0:N}");
      switch (p_accountType)
      {
        case Account.AccountType.MONETARY:
          return ("{0:" + p_currency.Symbol + "#,##0;(" + p_currency.Symbol + "#,##0)}");
        case Account.AccountType.PERCENTAGE:
          return ("{0:P}");
        case Account.AccountType.NUMBER:
          return ("{0:N}");
      }
      return ("{0:C0}");
    }
  }
}
