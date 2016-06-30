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

    private static string[] m_functions = { "SIN", "COS", "TAN", "LOG2", "LOG10", "LOG", "LN", "EXP", "SQRT", "SIGN", "RINT", "ABS", "MIN", "MAX", "SUM", "AVG" };

    private static List<UInt32> m_accounts = new List<UInt32>();

    public static List<UInt32> Accounts
    {
      get { return (m_accounts); }
    }

    public static void ClearAccounts()
    {
      m_accounts.Clear();
    }

    #region ServerToken

    //acc366 To "Account"
    private static string GetServerAccount(Consumer p_consumer)
    {
      string l_str;
      UInt32 l_accId;
      Account l_account = null;

      if (!p_consumer.Save("accId") || !p_consumer.ReadNum() || !p_consumer.Stop("accId", out l_str) ||
        !UInt32.TryParse(l_str, out l_accId))
        return (null);
      if ((l_account = AccountModel.Instance.GetValue(l_accId)) == null)
        return (null);
      m_accounts.Add(l_accId);
      return ("\"" + l_account.Name + "\"");
    }

    //gfact366 To "Global Fact"
    private static string GetServerGFact(Consumer p_consumer)
    {
      string l_str;
      UInt32 l_gfactId;
      GlobalFact l_gfact = null;

      if (!p_consumer.Save("gfactId") || !p_consumer.ReadNum() || !p_consumer.Stop("gfactId", out l_str) ||
        !UInt32.TryParse(l_str, out l_gfactId))
        return (null);
      if ((l_gfact = GlobalFactModel.Instance.GetValue(l_gfactId)) == null)
        return (null);
      return ("\"" + l_gfact.Name + "\"");
    }

    //T42 To [42]
    private static string GetServerPeriod(Consumer p_consumer)
    {
      string l_str = "";
      string l_tmp = "";

      if (!p_consumer.ReadChar('T'))
        return (null);
      l_str = ("[" +
        (p_consumer.ReadChar('n') ? "n" : "") +
        (p_consumer.ReadChar('p') ? "+" : "") +
        (p_consumer.ReadChar('m') ? "-" : ""));
      p_consumer.ReadMultipleWhitespace();
      if (!p_consumer.Save("period") || !p_consumer.ReadNum() || !p_consumer.Stop("period", out l_tmp))
        return (l_str + "]");
      return (l_str + l_tmp + "]");
    }

    //acc366T42 To "Account"[42] OR gfact42T1 To "Global Fact"[1]
    private static string GetServerToken(string p_token)
    {
      string l_token = "";
      string l_period = "";
      Consumer l_consumer = new Consumer(p_token);

      if (l_consumer.ReadText("acc"))
      {
        l_token = FbiGrammar.GetServerAccount(l_consumer);
      }
      else if (l_consumer.ReadText("gfact"))
      {
        l_token = FbiGrammar.GetServerGFact(l_consumer);
      }
      if (l_token == null || (l_period = FbiGrammar.GetServerPeriod(l_consumer)) == null)
        return (null);
      return (l_token + l_period);
    }

    #endregion

    #region HumanToken

    //"Account" To acc42 OR "Global Fact" To gfact42
    public static string GetHumanAccountOrFact(Consumer p_consumer)
    {
      string l_str;
      Account l_account = null;
      GlobalFact l_fact = null;

      if (!p_consumer.ReadChar('\"') || !p_consumer.Save("token") || !p_consumer.ReadToken() || !p_consumer.Stop("token", out l_str) || !p_consumer.ReadChar('\"'))
        return (null);
      if ((l_account = AccountModel.Instance.GetValue(l_str)) == null && (l_fact = GlobalFactModel.Instance.GetValue(l_str)) == null)
        return (null);
      return (l_account == null ? "gfact" + l_fact.Id.ToString() : "acc" + l_account.Id.ToString());
    }

    //[42] To T42
    public static string GetHumanPeriod(Consumer p_consumer)
    {
      bool l_hasTime;
      string l_tmp = "";
      string l_period = "T";

      if (!p_consumer.ReadChar('['))
        return (null);
      p_consumer.ReadMultipleWhitespace();
      l_period += (p_consumer.ReadChar('n') ? "n" : "");
      p_consumer.ReadMultipleWhitespace();
      l_period += (p_consumer.ReadChar('+') ? "p" : (p_consumer.ReadChar('-') ? "m" : ""));
      p_consumer.ReadMultipleWhitespace();
      l_hasTime = p_consumer.Save("period") && p_consumer.ReadNum() && p_consumer.Stop("period", out l_tmp);
      p_consumer.ReadMultipleWhitespace();
      if (!p_consumer.ReadChar(']'))
        return (null);
      return (l_period + l_tmp);
    }

    public static string GetHumanToken(string p_token)
    {
      string l_token;
      string l_period;
      Consumer l_consumer = new Consumer(p_token);

      if ((l_token = FbiGrammar.GetHumanAccountOrFact(l_consumer)) == null)
        return (null);
      l_consumer.ReadMultipleWhitespace();
      if ((l_period = FbiGrammar.GetHumanPeriod(l_consumer)) == null)
        return (null);
      return (l_token + l_period);
    }

    #endregion

    public static void AddGrammar(BNF p_bnf)
    {
      p_bnf.AddRule("number", "num [ '.' num ]");
      p_bnf.AddRule("std_ops", "'-' | '+' | '*' | '/'");
      p_bnf.AddRule("conditional_ops", "'<' | '>' | \"<=\" | \">=\" | \"==\" | \"!=\"");
      p_bnf.AddRule("ops", "std_ops | conditional_ops");
      p_bnf.AddRule("func_name", StringUtils.Join(m_functions, " | ", "^"));

      /* SERVER RULES */

      p_bnf.AddEvent("GetServerToken", GetServerToken);

      p_bnf.AddRule("server_add_sub", "'p' | 'm'");
      p_bnf.AddRule("server_period", "'n' [ server_add_sub num ] | num");
      p_bnf.AddRule("server_token_prefix", "\"acc\" | \"gfact\"");
      p_bnf.AddRule("server_token", "server_token_prefix num 'T' [ server_period ]");

      p_bnf.AddRule("server_expr", "server_token~GetServerToken | print");

      p_bnf.AddRule(TO_HUMAN, "*server_expr"); 

      /* HUMAN RULES */

      p_bnf.AddEvent("GetHumanToken", GetHumanToken);

      p_bnf.AddRule("val", "number | account~GetHumanToken | if | func");
      p_bnf.AddRule("value", "[ '-' ] $ val");
      p_bnf.AddRule("add_sub", "'+' | '-'");
      p_bnf.AddRule("period_1", "$ '[' $ num $ ']' $");
      p_bnf.AddRule("period_2", "$ '[' $ 'n' $ [ add_sub $ num ] $ ']' $");
      p_bnf.AddRule("period", "period_1 | period_2");
      p_bnf.AddRule("account", "'\"' token '\"' period");

      p_bnf.AddRule("if", "^IF^ $ '(' $ +expr_cmp $ ',' $ +expr $ ',' $ +expr $ ')' $");
      p_bnf.AddRule("func", "func_name $ '(' $ +expr [ *func_param ] $ ')'");
      p_bnf.AddRule("func_param", "$ ',' $ +expr $");

      p_bnf.AddRule("expr", "$ parameter *param_list");
      p_bnf.AddRule("param_list", "$ std_ops $ parameter");
      p_bnf.AddRule("parameter", "$ value $ | $ '(' $ expr $ ')' $");
      p_bnf.AddRule("expr_cmp", "$ expr $ conditional_ops $ expr");

      p_bnf.AddRule(TO_SERVER, "expr | $");
    }
  }
}
