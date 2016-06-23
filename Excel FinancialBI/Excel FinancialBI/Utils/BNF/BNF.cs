using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Utils
{
  using InternalRule = Func<Consumer, bool>;
  using EventRule = Func<string, string>;

  class BNF
  {
    private delegate bool EvalRuleFunc(Consumer p_consumer, Rule p_rule, out bool p_res);

    private Consumer m_input = new Consumer();
    private SafeDictionary<string, InternalRule> m_internalRules = new SafeDictionary<string, InternalRule>();

    private List<Tuple<Int32, string, string>> m_eventContent = new List<Tuple<Int32, string, string>>();
    private SafeDictionary<string, EventRule> m_eventRules = new SafeDictionary<string, EventRule>();

    private SafeDictionary<string, string> m_stringRules = new SafeDictionary<string, string>();

    private List<EvalRuleFunc> m_evalRules = new List<EvalRuleFunc>();
    private string m_concatenated = null;

    public string LastError = "";
    private int m_errorPtr = 0;

    private const char CHAR_DELIM = '\'';
    private const char STRING_DELIM = '\"';
    private const char EVENT_DELIM = '~';
    private const char SPLIT_DELIM = '|';
    private const string OPTIONAL_DELIM = "[]";
    private const string SYMBOLS = "$\'\"[*+";

    public BNF()
    {
      m_evalRules.Add(EvalInternalRule);
      m_evalRules.Add(EvalStringRule);

      m_internalRules["digit"] = RuleDigit;
      m_internalRules["num"] = RuleNum;
      m_internalRules["alpha"] = RuleAlpha;
      m_internalRules["word"] = RuleWord;
      m_internalRules["alnum"] = RuleAlNum;
      m_internalRules["print"] = RulePrintable;
      m_internalRules["identifier"] = RuleIdentifier;
      m_internalRules["token"] = RuleToken;
      m_internalRules["ws"] = RuleWhitespace;
      m_internalRules["$"] = RuleWhitespaces;
      m_internalRules["\'"] = RuleChar;
      m_internalRules["\""] = RuleString;
      m_internalRules["["] = RuleOptional;
      m_internalRules["*"] = RuleMultiple;
      m_internalRules["+"] = RuleSingleMore;
    }

    public void AddRule(string p_ruleName, string p_rule)
    {
      m_stringRules[p_ruleName] = p_rule;
    }

    public void AddEvent(string p_eventName, EventRule p_rule)
    {
      m_eventRules[p_eventName] = p_rule;
    }

    public bool Parse(string p_input, string p_bnfRule)
    {
      m_errorPtr = 0;
      m_eventContent.Clear();
      Consumer p_bnf = new Consumer(p_bnfRule);

      try
      {
        m_input.Set(p_input);
        if (!this.EvalRuleList(p_bnf) || !m_input.IsEOI())
          return (false);
        this.BuildConcatenedString();
        return (true);
      }
      catch (Exception e)
      {
        this.LastError = Local.GetValue("bnf.error.bnf_is_fucked") + " " + e.Message;
        return (false);
      }
    }

    public string Concatenated
    {
      get { return (m_concatenated); }
    }

    private void BuildConcatenedString()
    {
      Int32 l_ptr = 0;

      m_concatenated = m_input.Str;
      foreach (var l_elem in m_eventContent)
      {
        m_concatenated = m_concatenated.Insert(l_elem.Item1 + l_ptr, l_elem.Item3);
        m_concatenated = m_concatenated.Remove(l_elem.Item1 + l_elem.Item3.Length + l_ptr, l_elem.Item2.Length);
        l_ptr += (l_elem.Item3.Length - l_elem.Item2.Length);
      }
    }

    private void FillRule(Consumer p_bnf, Rule p_rule)
    {
      bool l_identifier = false;
      string l_rule = "";
      string l_event = "";

      p_rule.Clear();
      p_bnf.Save("rule");
      if (!p_bnf.ReadOf(SYMBOLS) && !(l_identifier = p_bnf.ReadIdentifier()))
      {
        throw new Exception("BNF::FillRule");
      }
      p_bnf.Stop("rule", out l_rule);
      p_rule.Name = l_rule;
      if (l_identifier && p_bnf.ReadChar(EVENT_DELIM))
      {
        p_bnf.Save("event");
        if (!p_bnf.ReadIdentifier())
        {
          throw new Exception("BNF::FillRule");
        }
        p_bnf.Stop("event", out l_event);
        p_rule.Event = l_event;
      }
    }

    private bool EvalStringRule(Consumer p_bnf, Rule p_rule, out bool p_result)
    {
      Consumer l_bnf;

      p_result = false;
      if (m_stringRules.ContainsKey(p_rule.Name))
      {
        l_bnf = new Consumer(m_stringRules[p_rule.Name]);
        p_result = this.EvalRuleList(l_bnf);
        return (true);
      }
      return (false);
    }

    private bool EvalInternalRule(Consumer p_bnf, Rule p_rule, out bool p_result)
    {
      p_result = false;
      if (m_internalRules.ContainsKey(p_rule.Name))
      {
        p_result = m_internalRules[p_rule.Name](p_bnf);
        return (true);
      }
      return (false);
    }

    //Return false ONLY when the string returned is null.
    //Means that the input was erroneous.
    private bool Concatenate(Consumer p_bnf, Rule p_rule, bool l_result)
    {
      string l_str = null;
      string l_nstr = null;

      if (!l_result || !p_rule.HasEvent)
        return (true);
      m_input.Stop("event", out l_str);
      l_nstr = m_eventRules[p_rule.Event](l_str);
      m_eventContent.Add(new Tuple<int, string, string>(m_input.Ptr - l_str.Length, l_str, l_nstr));
      return (l_nstr == null ? false : true);
    }

    private bool EvalRule(Consumer p_bnf, Rule p_rule)
    {
      bool l_result = false;

      if (p_rule.HasEvent)
      {
        m_input.Save("event");
      }
      for (Int32 i = 0; i < m_evalRules.Count; ++i)
      {
        if (m_evalRules[i](p_bnf, p_rule, out l_result))
        {
          return (l_result && this.Concatenate(p_bnf, p_rule, l_result));
        }
      }
      throw new Exception("BNF::EvalRule");
    }

    private bool EvalSimpleRuleList(Consumer p_bnf)
    {
      Rule l_rule = new Rule();
      Int32 l_inputPtr = m_input.Ptr;

      while (!p_bnf.IsEOI())
      {
        p_bnf.ReadMultipleWhitespace();
        this.FillRule(p_bnf, l_rule);
        if (!this.EvalRule(p_bnf, l_rule))
        {
          this.BuildLastError(p_bnf, l_rule);
          m_input.Set(l_inputPtr);
          return (false);
        }
        p_bnf.ReadMultipleWhitespace();
      }
      return (true);
    }

    private bool EvalRuleList(Consumer p_bnf)
    {
      Consumer l_splittedBnf = new Consumer();
      List<string> l_list = StringUtils.SplitToken(p_bnf.GetString(), SPLIT_DELIM, "\"\'");

      for (Int32 i = 0; i < l_list.Count; ++i)
      {
        l_splittedBnf.Set(l_list[i]);
        if (this.EvalSimpleRuleList(l_splittedBnf))
          return (true);
      }
      return (false);
    }

    private void BuildLastError(Consumer p_bnf, Rule p_rule)
    {
      if (m_input.Ptr > m_errorPtr && !m_internalRules.ContainsKey(p_rule.Name)) //Default rules are not considered as part of LastError
      {
        m_errorPtr = m_input.Ptr;
        this.LastError = Local.GetValue("bnf.error.at") + " " + m_input.Ptr.ToString() + ": " + Local.GetValue("bnf.error." + p_rule.Name);
      }
    }

    private bool RuleDigit(Consumer p_bnf)
    {
      return (m_input.ReadDigit());
    }

    private bool RuleNum(Consumer p_bnf)
    {
      return (m_input.ReadNum());
    }

    private bool RuleAlpha(Consumer p_bnf)
    {
      return (m_input.ReadAlpha());
    }

    private bool RuleWord(Consumer p_bnf)
    {
      return (m_input.ReadWord());
    }

    private bool RuleAlNum(Consumer p_bnf)
    {
      return (m_input.ReadAlnum());
    }

    private bool RulePrintable(Consumer p_bnf)
    {
      return (m_input.ReadPrintable());
    }

    private bool RuleIdentifier(Consumer p_bnf)
    {
      return (m_input.ReadIdentifier());
    }

    private bool RuleToken(Consumer p_bnf)
    {
      return (m_input.ReadToken());
    }

    private bool RuleWhitespace(Consumer p_bnf)
    {
      return (m_input.ReadWhitespace());
    }

    private bool RuleWhitespaces(Consumer p_bnf)
    {
      m_input.ReadMultipleWhitespace();
      return (true);
    }

    private bool RuleChar(Consumer p_bnf)
    {
      char l_c;

      if (((l_c = p_bnf.ReadChar()) == Consumer.INVALID_CHAR) || !p_bnf.ReadChar(CHAR_DELIM))
      {
        throw new Exception("BNF::RuleChar");
      }
      return (m_input.ReadChar(l_c));
    }

    private bool RuleString(Consumer p_bnf)
    {
      string l_str = null;

      p_bnf.Save("string");
      if (!p_bnf.ReadTo(STRING_DELIM) || !p_bnf.Stop("string", out l_str) || !p_bnf.ReadChar(STRING_DELIM))
      {
        throw new Exception("BNF::RuleString");
      }
      return (m_input.ReadText(l_str));
    }

    private bool RuleOptional(Consumer p_bnf)
    {
      string l_optionalArgs = null;
      Consumer l_optionalBnf = new Consumer();

      p_bnf.Set(p_bnf.Ptr - 1);
      p_bnf.Save("optional");
      if (!p_bnf.ReadOperator(OPTIONAL_DELIM[0], OPTIONAL_DELIM[1]))
      {
        throw new Exception("BNF::RuleOptional");
      }
      p_bnf.Stop("optional", out l_optionalArgs);
      l_optionalArgs = l_optionalArgs.Substring(1, l_optionalArgs.Length - 2); //Remove the [] operator
      l_optionalBnf.Set(l_optionalArgs);
      this.EvalRuleList(l_optionalBnf);
      return (true);
    }

    private bool RuleMultiple(Consumer p_bnf)
    {
      Rule l_rule = new Rule();

      this.FillRule(p_bnf, l_rule);
      while (this.EvalRule(p_bnf, l_rule)) ;
      return (true);
    }

    private bool RuleSingleMore(Consumer p_bnf)
    {
      Rule l_rule = new Rule();

      this.FillRule(p_bnf, l_rule);
      if (!this.EvalRule(p_bnf, l_rule))
        return (false);
      while (this.EvalRule(p_bnf, l_rule)) ;
      return (true);
    }
  }
}
