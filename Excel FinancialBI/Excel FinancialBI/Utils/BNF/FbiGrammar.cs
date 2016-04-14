using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Utils.BNF
{
  using MVC.Model;
  using MVC.Model.CRUD;

  class FbiGrammar
  {
    private static readonly char m_separator = ',';
    private static readonly char m_periodId = 'n';

    public static readonly char m_identifierFilter = '\"';
    public static readonly string m_operators = "+-*/";
    public static readonly string m_serverOperators = "pm..";
    public static readonly string m_serverAccount = "acc";
    public static readonly string m_serverStandardPeriod = "Tn";

    private static readonly string m_funcSeparators = "()";
    private static readonly string m_periodSeparators = "[]";
    private static readonly string[] m_funcs = { "IF", "SIN", "COS", "TAN", "LOG2", "LOG10", "LOG", "LN", "EXP", "SQRT", "SIGN", "RINT", "ABS", "MIN", "MAX", "SUM", "AVG" };

    private enum m_functions { PERIOD, IDENTIFICATOR, FUNCTION, NA };
    private SafeDictionary<m_functions, string> m_errors = new SafeDictionary<m_functions, string>();

    private string m_formula;
    private int m_inputPtr;
    private int m_formulaPtr;
    private string m_lastError;

    public string Formula
    {
      get { return (m_formula); }
    }

    public string LastError
    {
      get
      {
        if (m_lastError == "")
          return (m_errors[m_functions.NA]);
        return (m_lastError);
      }
    }

    public FbiGrammar()
    {
      m_errors[m_functions.PERIOD] = Local.GetValue("bnf.error.period");
      m_errors[m_functions.IDENTIFICATOR] = Local.GetValue("bnf.error.identificator");
      m_errors[m_functions.FUNCTION] = Local.GetValue("bnf.error.function");
      m_errors[m_functions.NA] = Local.GetValue("bnf.error.syntax");
    }

    #region Internal Utils

    public void Clear()
    {
      m_formula = "";
      m_inputPtr = 0;
      m_formulaPtr = 0;
      m_lastError = "";
      m_formula = "";
    }

    private void Add(string p_data)
    {
      if (p_data == null)
      {
        throw new Exception("FbiGrammar: Add(null was send)");
      }
      m_formula += p_data;
    }

    private void SavePtrs(BnfConsumer p_input)
    {
      m_formulaPtr = m_formula.Length;
      m_inputPtr = p_input.GetPtr();
    }

    private bool Error(BnfConsumer p_input, m_functions p_f)
    {
      if (p_f != m_functions.NA)
      {
        m_lastError = m_errors[p_f];
      }
      this.ResetPtrs(p_input);
      return (false);
    }

    private void ResetPtrs(BnfConsumer p_input)
    {
      if (m_formula.Length > m_formulaPtr)
      {
        m_formula = m_formula.Remove(m_formulaPtr);
      }
      p_input.SetPtr(m_inputPtr);
    }

    //Find the equivalence from simple operator to server operator (m_operator to m_serverOperator)
    //'-' to 'm', '+' to 'p'...
    private string OpToServerOp(string p_op)
    {
      char l_c;
      int i = 0;

      if (p_op == null || p_op.Length != 1)
        return (null);
      l_c = p_op[0];
      while (i < m_operators.Length)
      {
        if (l_c == m_operators[i])
          return (m_serverOperators[i].ToString());
        ++i;
      }
      return (null);
    }

    private string ServerOpToOp(string p_op)
    {
      char l_c;
      int i = 0;

      if (p_op == null || p_op.Length != 1)
        return (null);
      l_c = p_op[0];
      while (i < m_serverOperators.Length)
      {
        if (l_c == m_serverOperators[i])
          return (m_operators[i].ToString());
        ++i;
      }
      return (null);
    }

    //Return a string from an account name (Chiffre d'affaire) to a server-formatted name (acc2)
    private string AccountToServerAccount(string p_account)
    {
      Account l_account;

      if (p_account == null || (l_account = AccountModel.Instance.GetValue(p_account)) == null)
        return (null);
      return (m_serverAccount + l_account.Id.ToString());
    }

    private string GetInputNumber(BnfConsumer p_input)
    {
      UInt32 l_int;

      p_input.Save("number");
      if (!p_input.ReadNumber())
        return (null);
      if (UInt32.TryParse(p_input.Stop("number"), out l_int))
        return (l_int.ToString());
      return (null);
    }

    #endregion

    public bool ToGrammar(BnfConsumer p_input)
    {
      try
      {
        this.Clear();
        return (this.IsHumanExprList(p_input));
      }
      catch (Exception e)
      {
        System.Diagnostics.Debug.WriteLine(e.Message);
        return (false);
      }
    }

    public bool ToHuman(BnfConsumer p_input)
    {
      try
      {
        this.Clear();
        return (this.IsGrammarExprList(p_input));
      }
      catch (Exception e)
      {
        System.Diagnostics.Debug.WriteLine(e.Message);
        return (false);
      }
    }

    #region HumanToGrammar

    //Period like [9]
    public bool IsPeriodNumber(BnfConsumer p_input)
    {
      this.SavePtrs(p_input);
      if (p_input.ReadChar(m_periodSeparators[0])) //If read '['
      {
        p_input.ReadWhitespaces();
        p_input.Save("period_number");
        if (p_input.ReadNumber()) //Read a number -> ['9']
        {
          this.Add(p_input.Stop("period_number")); //Add the number to the formula
          p_input.ReadWhitespaces();
          if (!p_input.ReadChar(m_periodSeparators[1])) //Read NOT ']'
            return (this.Error(p_input, m_functions.PERIOD));
          return (true);
        }
      }
      return (this.Error(p_input, m_functions.PERIOD));
    }

    //Period like [n+1] OR [n3]
    public bool IsPeriodVariable(BnfConsumer p_input)
    {
      this.SavePtrs(p_input);
      if (p_input.ReadChar(m_periodSeparators[0])) //If read '['
      {
        p_input.ReadWhitespaces();
        if (!p_input.ReadChar(m_periodId)) //Read 'n' -> [n3] OR [n+1]
          return (this.Error(p_input, m_functions.PERIOD));
        p_input.ReadWhitespaces();
        p_input.Save("period_op");
        if (p_input.ReadChar(m_operators[0]) || p_input.ReadChar(m_operators[1])) //If read a valid operator: '-' or '+' [n+1] OR [n-1]
        {
          this.Add(this.OpToServerOp(p_input.Stop("period_op"))); //Add 'p' or 'm' to the formula
          p_input.ReadWhitespaces();
          p_input.Save("period_number");
          if (!p_input.ReadNumber()) //Read a number -> [n+'1']
            return (this.Error(p_input, m_functions.PERIOD));
          this.Add(p_input.Stop("period_number")); //Add the number to the formula
        }
        p_input.ReadWhitespaces();
        p_input.Save("period_number");
        p_input.ReadNumber(); //Read the number -> If forumla is like [n3]
        this.Add(p_input.Stop("period_number")); //Add the number to the formula [n'3']
        p_input.ReadWhitespaces();
        if (!p_input.ReadChar(m_periodSeparators[1])) //Read ']'
          return (this.Error(p_input, m_functions.PERIOD));
        return (true);
      }
      return (this.Error(p_input, m_functions.PERIOD));
    }

    //If the string is a valid period like [n + 1], [n], [n+8], [n5], [9], ...
    public bool IsPeriod(BnfConsumer p_input)
    {
      if (this.IsPeriodNumber(p_input) || this.IsPeriodVariable(p_input))
        return (true);
      return (false);
    }

    //If the string is a valid identificator, containing or not a period ("Chiffre d'affaire"[n + 9])
    public bool IsIdentificator(BnfConsumer p_input)
    {
      string l_account;

      this.SavePtrs(p_input);
      if (p_input.ReadChar(m_operators[1])) //If read a '-' at the start, add it to the formula :)
      {
        this.Add(m_operators[1].ToString());
      }
      if (p_input.ReadChar(m_identifierFilter) && p_input.Save("account_name") && p_input.ReadTo(m_identifierFilter) && (l_account = p_input.Stop("account_name")) != null && p_input.ReadChar(m_identifierFilter)) //Read "Chiffre d'affaire", and keeps Chiffre d'affaire stored inside a string.
      {
        this.Add(this.AccountToServerAccount(l_account));
        this.Add(m_serverStandardPeriod); //Add the 'Tn' period identificator
        p_input.ReadWhitespaces();
        this.IsPeriod(p_input); //If the identificator is followed by a time period ([n+1], etc), add it to the formula !
        return (true);
      }
      return (this.Error(p_input, m_functions.IDENTIFICATOR));
    }

    //If the string correspond to a number (1, -1, -1.0, 1.0)
    private bool IsNumber(BnfConsumer p_input)
    {
      this.SavePtrs(p_input);
      p_input.Save("number");
      if (p_input.ReadOptNegDouble() || p_input.ReadOptNegNumber())
      {
        this.Add(p_input.Stop("number"));
        return (true);
      }
      return (this.Error(p_input, m_functions.NA));
    }

    //If the string is a valid argument
    //Ex: "Chiffre d'affaire", 3, sum(...), -9.9, ...
    public bool IsArgument(BnfConsumer p_input)
    {
      p_input.ReadWhitespaces();
      if (this.IsIdentificator(p_input) || this.IsFunction(p_input) || this.IsNumber(p_input))
        return (true);
      return (false);
    }

    //If string is an expression : 3 + 5 + "Chiffre d'affaire"[n + 6]
    //Ex: (3 + 9), "Chiffre d'affaire" + 6, ...
    public bool IsExpr(BnfConsumer p_input)
    {
      bool l_hasPrio = false;
      bool l_isOperation = true;

      this.SavePtrs(p_input);
      p_input.ReadWhitespaces();
      if (p_input.ReadChar(m_funcSeparators[0])) //If read '('
      {
        this.Add(m_funcSeparators[0].ToString());
        l_hasPrio = true;
      }
      p_input.ReadWhitespaces();
      if (this.IsArgument(p_input))
      {
        while (l_isOperation)
        {
          p_input.ReadWhitespaces();
          p_input.Save("operator");
          if (p_input.ReadOf(m_operators))
          {
            this.Add(p_input.Stop("operator"));
            if (!this.IsExpr(p_input))
              return (this.Error(p_input, m_functions.NA));
          }
          else
            l_isOperation = false;
        }
        if (l_hasPrio)
        {
          p_input.ReadWhitespaces();
          if (p_input.ReadChar(m_funcSeparators[1])) //If read ')'
          {
            this.Add(m_funcSeparators[1].ToString());
            return (true);
          }
          return (this.Error(p_input, m_functions.NA));
        }
        return (true);
      }
      return (this.Error(p_input, m_functions.NA));
    }

    //List of arguments, separated by a ',' -> 1+1, "Input"[5] / 2 + (3 - 9)
    //The list of argument cannot be empty
    private bool IsFunctionArgument(BnfConsumer p_input)
    {
      int l_args = 0;
      bool l_hasArguments = true;

      this.SavePtrs(p_input);
      p_input.ReadWhitespaces();
      if (this.IsHumanExprList(p_input)) //If the function has at least one argument
      {
        while (l_hasArguments)
        {
          p_input.ReadWhitespaces();
          if (p_input.ReadChar(m_separator)) //If read ','
          {
            this.Add(m_separator.ToString());
            if (!this.IsHumanExprList(p_input)) //Read the expression
              return (this.Error(p_input, m_functions.FUNCTION));
          }
          else //If NOT ','
          {
            return (true);
          }
          ++l_args;
        }
      }
      return (this.Error(p_input, m_functions.FUNCTION));
    }

    //Return if a function is found, with parameters.
    public bool IsFunction(BnfConsumer p_input)
    {
      this.SavePtrs(p_input);
      if (p_input.ReadChar(m_operators[1])) //If read a '-' at the start, add it to the formula :)
      {
        this.Add(m_operators[1].ToString());
      }
      foreach (string l_str in m_funcs)
      {
        if (p_input.ReadText(l_str, false)) //Found a function
        {
          p_input.ReadWhitespaces();
          if (p_input.ReadChar(m_funcSeparators[0])) //If read '('
          {
            this.Add(l_str + m_funcSeparators[0].ToString());
            if (this.IsFunctionArgument(p_input) && p_input.ReadChar(m_funcSeparators[1])) //If read arguments followed by a ')'
            {
              this.Add(m_funcSeparators[1].ToString());
              return (true);
            }
          }
          else //If NOT read '('
            return (this.Error(p_input, m_functions.FUNCTION));
        }
      }
      return (this.Error(p_input, m_functions.FUNCTION));
    }

    //If the string is a list of expression.
    //Ex. "Chiffre d'affaire"[n + 3] + ((9.6 / 3) - "Test")
    public bool IsHumanExprList(BnfConsumer p_input)
    {
      bool l_isExpr = true;

      this.SavePtrs(p_input);
      if (this.IsExpr(p_input))
      {
        while (l_isExpr)
        {
          p_input.ReadWhitespaces();
          p_input.Save("operator");
          if (p_input.ReadOf(m_operators))
          {
            this.Add(p_input.Stop("operator"));
            if (!this.IsExpr(p_input))
              return (this.Error(p_input, m_functions.NA));
          }
          else
            l_isExpr = false;
        }
        return (true);
      }
      return (false);
    }
  
    #endregion

    #region GrammarToHuman

    public string ToHumanPeriod(BnfConsumer p_input)
    {
      if (!p_input.ReadChar('T'))
        return (null);
      if (p_input.ReadChar('n'))
      {
        this.Add("[n");
        p_input.Save("operator");
        if (p_input.ReadChar('p') || p_input.ReadChar('m')) //If there is a '+' or '-'
        {
          this.Add(" " + this.ServerOpToOp(p_input.Stop("operator")) + " " + this.GetInputNumber(p_input) + "]");
        }
        else //If there is NOT '+' or '-' to add...
        {
          this.Add(this.GetInputNumber(p_input) + "]");
        }
      }
      return (null);
    }

    public string ToHumanAccount(BnfConsumer p_input)
    {
      UInt32 l_accountId;
      Account l_account;

      p_input.Save("account_n");
      p_input.ReadNumber();
      if (UInt32.TryParse(p_input.Stop("account_n"), out l_accountId))
      {
        if ((l_account = AccountModel.Instance.GetValue(l_accountId)) != null)
        {
          this.Add("\"" + l_account.Name + "\"");
        }
      }
      return (null);
    }

    public bool IsGrammarExprList(BnfConsumer p_input)
    {
      while (!p_input.IsEOI())
      {
        if (p_input.ReadText("acc"))
        {
          if (this.ToHumanAccount(p_input) == null || this.ToHumanPeriod(p_input) == null)
            return (false);
        }
        else
        {
          this.Add(p_input.ReadChar().ToString());
        }
      }
      return (true);
    }

    #endregion

  }
}
