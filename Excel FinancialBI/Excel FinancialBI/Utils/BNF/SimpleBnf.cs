using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Utils.BNF
{
  class SimpleBnf
  {
    private string m_input;
    private BnfConsumer m_inputConsumer;
    private SafeDictionary<string, string> m_save;
    private SafeDictionary<string, Func<BnfConsumer, bool>> m_extRules;

    private static readonly char m_saveChar = ':';

    public SimpleBnf()
    {
      m_inputConsumer = new BnfConsumer();
      m_save = new SafeDictionary<string, string>();
      m_extRules = new SafeDictionary<string, Func<BnfConsumer, bool>>();
    }

    private bool CallRule(BnfConsumer p_bnf, string p_ruleId)
    {
      return (m_extRules[p_ruleId](m_inputConsumer));
    }

    private bool ParseRule(BnfConsumer p_bnf, string p_ruleId, string p_saveId)
    {
      bool l_result;

      if (p_saveId != "")
      {
        string l_str;
        m_inputConsumer.Save(p_saveId);
        l_result = this.CallRule(p_bnf, p_ruleId);
        if ((l_str = m_inputConsumer.Stop(p_saveId)) == null)
        {
          throw new Exception("BNF Parsing: ParseRule(Cannot save)");
        }
        m_save[p_saveId] = l_str;
        return (l_result);
      }
      return (this.CallRule(p_bnf, p_ruleId));
    }

    private void GetRuleId(BnfConsumer p_bnf, ref string p_id, ref string p_save)
    {
      bool l_isIdentifier = false;

      p_bnf.Save("rule_id");
      if (!(l_isIdentifier = p_bnf.ReadIdentifier()))
      {
        throw new Exception("BNF Parsing: GetRuleId(Not bnf symbol or identifier)");
      }
      if ((p_id = p_bnf.Stop("rule_id")) == null)
      {
        throw new Exception("BNF Parsing: GetRuleId(Cannot save ruleId)");
      }
      if (l_isIdentifier && p_bnf.ReadChar(m_saveChar))
      {
        p_bnf.Save("save");
        if (!p_bnf.ReadIdentifier())
        {
          throw new Exception("BNF Parsing: GetRuleId(Is not identifier)");
        }
        if ((p_save = p_bnf.Stop("save")) == null)
        {
          throw new Exception("BNF Parsing: GetRuleId(Cannot save saveId)");
        }
      }
    }

    private bool EvalRule(BnfConsumer p_bnf)
    {
      string l_ruleId = "";
      string l_saveId = "";

      this.GetRuleId(p_bnf, ref l_ruleId, ref l_saveId);
      if (m_extRules.ContainsKey(l_ruleId))
        return (this.ParseRule(p_bnf, l_ruleId, l_saveId));
      throw new Exception("BNF Parsing: EvalExpr(No rule '" + l_ruleId + "' defined)"); //If no rule is defined, go fuck yourself with a shovel.
    }

    public bool Parse(string p_rule, string p_input)
    {
      try
      {
        m_input = p_input;
        m_inputConsumer.Set(m_input);
        return (this.ParseInput(new BnfConsumer(p_rule)));
      }
      catch (Exception e)
      {
        System.Diagnostics.Debug.WriteLine(e.Message);
        System.Diagnostics.Debug.WriteLine(e.StackTrace);
      }
      return (false);
    }

    public void AddRule(string p_ruleName, Func<BnfConsumer, bool> p_func)
    {
      m_extRules[p_ruleName] = p_func;
    }

    public string GetId(string p_id)
    {
      return (m_save[p_id]);
    }

    private bool ParseInput(BnfConsumer p_bnf)
    {
      while (!p_bnf.IsEOI())
      {
        p_bnf.ReadWhitespaces();
        if (!this.EvalRule(p_bnf))
          return (false);
        p_bnf.ReadWhitespaces();
        if (m_inputConsumer.IsEOI() && p_bnf.IsEOI())
          return (true);
      }
      return (false);
    }
  }
}
