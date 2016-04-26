using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Utils
{
  using MVC.Model;
  using MVC.Model.CRUD;

  class FbiGrammar
  {
    public const string TO_HUMAN = "ToHuman";
    public const string TO_SERVER = "ToServer";

    private static string[] m_functions = { "IF", "SIN", "COS", "TAN", "LOG2", "LOG10", "LOG", "LN", "EXP", "SQRT", "SIGN", "RINT", "ABS", "MIN", "MAX", "SUM", "AVG" };

    private static List<UInt32> m_accounts = new List<UInt32>();

    public static List<UInt32> Accounts
    {
      get { return (m_accounts); }
    }

    public static void ClearAccounts()
    {
      m_accounts.Clear();
    }

    //acc366T42 To "Account"[42]
    private static string GetServerAccount(string p_account)
    {
      UInt32 l_accId;
      Account l_account;
      string l_accountStr;
      Consumer l_consumer = new Consumer(p_account);

      if (!l_consumer.ReadText("acc") || !l_consumer.Save("accId") || !l_consumer.ReadNum() ||
        !l_consumer.Stop("accId", out l_accountStr) || !l_consumer.ReadChar('T') ||
        !UInt32.TryParse(l_accountStr, out l_accId))
        return (null);
      m_accounts.Add(l_accId); //Add to Accounts
      if ((l_account = AccountModel.Instance.GetValue(l_accId)) == null)
        return (null);
      return ("\"" + l_account.Name + "\"[" +
        (l_consumer.ReadChar('n') ? "n" : "") +
        (l_consumer.ReadChar('p') ? "+" : "") +
        (l_consumer.ReadChar('m') ? "-" : "") +
        l_consumer.GetString() + "]");
    }

    //"Account"[42] To acc366T42
    private static string GetHumanAccount(string p_account)
    {
      string l_str;
      string l_tmp;
      Account l_account;
      Consumer l_consumer = new Consumer(p_account);

      if (!l_consumer.ReadChar('\"') || !l_consumer.Save("acc") || !l_consumer.ReadAccount() || !l_consumer.Stop("acc", out l_tmp) ||
        !l_consumer.ReadChar('\"') || !l_consumer.ReadChar('['))
        return (null);
      if ((l_account = AccountModel.Instance.GetValue(l_tmp)) == null)
        return (null);
      l_str = "acc" + l_account.Id.ToString() + "T" +
        (l_consumer.ReadChar('n') ? "n" : "") +
        (l_consumer.ReadChar('+') ? "p" : (l_consumer.ReadChar('-') ? "m" : ""));
      if (!l_consumer.Save("period") || !l_consumer.ReadNum() || !l_consumer.Stop("period", out l_tmp))
        return (l_str);
      return (l_str + l_tmp);
    }

    public static void AddGrammar(BNF p_bnf)
    {
      p_bnf.AddRule("number", "num [ '.' num ]");
      p_bnf.AddRule("operator", "'-' | '+' | '*' | '/'");
      p_bnf.AddRule("funcName", StringUtils.Join(m_functions, " | ", "\""));

      /* SERVER RULES */

      p_bnf.AddEvent("GetServerAccount", GetServerAccount);

      p_bnf.AddRule("serverVal", "number | serverAccount~GetServerAccount | serverFunction");
      p_bnf.AddRule("serverValue", "[ '-' ] serverVal");
      p_bnf.AddRule("serverPeriodAddSub", "'p' | 'm'");
      p_bnf.AddRule("serverAccount", "\"acc\" num 'T' [ 'n' [ serverPeriodAddSub ] ] [ num ]");

      p_bnf.AddRule("serverFuncParams", "$ ',' $ +serverExpr $");
      p_bnf.AddRule("serverFunction", "funcName $ '(' $ +serverExpr [ *serverFuncParams ] $ ')'");

      p_bnf.AddRule("serverExpr", "$ serverParam *serverList $");
      p_bnf.AddRule("serverParam", "serverValue $ | '(' $ serverExpr $ ')'");
      p_bnf.AddRule("serverList", "$ operator $ serverParam");

      p_bnf.AddRule(TO_HUMAN, "*serverExpr");

      /* HUMAN RULES */

      p_bnf.AddEvent("GetHumanAccount", GetHumanAccount);

      p_bnf.AddRule("humanVal", "number | humanAccount~GetHumanAccount | humanFunction");
      p_bnf.AddRule("humanValue", "[ '-' ] $ humanVal");
      p_bnf.AddRule("humanPeriodAddSub", "'+' | '-'");
      p_bnf.AddRule("humanPeriod1", "$ '[' $ num $ ']' $");
      p_bnf.AddRule("humanPeriod2", "$ '[' $ 'n' $ [ humanPeriodAddSub ] $ [ num ] $ ']' $");
      p_bnf.AddRule("humanPeriod", "humanPeriod1 | humanPeriod2");
      p_bnf.AddRule("humanAccount", "'\"' account '\"' humanPeriod");

      p_bnf.AddRule("humanFuncParams", "$ ',' $ +humanExpr $");
      p_bnf.AddRule("humanFunction", "funcName $ '(' $ +humanExpr [ *humanFuncParams ] $ ')'");

      p_bnf.AddRule("humanExpr", "$ humanParam *humanList $");
      p_bnf.AddRule("humanParam", "humanValue $ | '(' $ humanExpr $ ')'");
      p_bnf.AddRule("humanList", "$ operator $ humanParam");

      p_bnf.AddRule(TO_SERVER, "*humanExpr");
    }
  }
}
