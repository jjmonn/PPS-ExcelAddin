using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Utils.BNF
{
  class BnfConsumer
  {
    private int m_ptr;
    private string m_str;
    private SafeDictionary<string, int> m_save;

    public static readonly string m_spaces = " \t\v\f";

    public BnfConsumer(string p_consumer = "")
    {
      this.Set(p_consumer);
      m_save = new SafeDictionary<string, int>();
    }

    #region Get/Set

    public void Set(string p_str, int p_ptr = 0)
    {
      m_str = p_str;
      m_ptr = p_ptr;
    }

    public void SetPtr(int p_ptr)
    {
      m_ptr = p_ptr;
    }

    public int GetPtr()
    {
      return (m_ptr);
    }

    public string GetStr(bool p_current = false)
    {
      if (p_current)
        return (m_str.Substring(m_ptr));
      return (m_str);
    }

    #endregion

    public void Clear()
    {
      m_str = "";
      m_ptr = 0;
    }

    #region Standard tools

    public char GetChar()
    {
      return (m_str[m_ptr]);
    }

    public char ReadChar()
    {
      if (m_ptr >= m_str.Length)
        return ('\0');
      return (m_str[m_ptr++]);
    }

    public bool ReadChar(char p_c)
    {
      if (m_ptr >= m_str.Length)
        return (false);
      if (m_str[m_ptr] == p_c)
      {
        ++m_ptr;
        return (true);
      }
      return (false);
    }

    public bool IsChar(char p_c)
    {
      if (m_ptr >= m_str.Length)
        return (false);
      return (m_str[m_ptr] == p_c);
    }

    public bool ReadRange(char p_a, char p_b)
    {
      if (m_ptr >= m_str.Length)
        return (false);
      if (m_str[m_ptr] >= p_a && m_str[m_ptr] <= p_b)
      {
        ++m_ptr;
        return (true);
      }
      return (false);
    }

    public bool ReadOf(string p_chars)
    {
      foreach (char l_c in p_chars)
      {
        if (this.ReadChar(l_c))
          return (true);
      }
      return (false);
    }

    private bool ReadInsensitiveText(string p_text)
    {
      string l_str = m_str.Substring(m_ptr).ToUpper();

      if (l_str.StartsWith(p_text.ToUpper()))
      {
        m_ptr += p_text.Length;
        return (true);
      }
      return (false);
    }

    public bool ReadText(string p_text, bool p_sensitive = true)
    {
      if (p_sensitive)
      {
        if (m_str.Substring(m_ptr).StartsWith(p_text))
        {
          m_ptr += p_text.Length;
          return (true);
        }
        return (false);
      }
      return (this.ReadInsensitiveText(p_text));
    }

    public bool ReadFor(char p_c)
    {
      if (this.ReadTo(p_c))
      {
        ++m_ptr;
        return (true);
      }
      return (false);
    }

    public bool ReadTo(char p_c)
    {
      int i = 0;
      string tmp = m_str.Substring(m_ptr);

      while (i < tmp.Length)
      {
        if (tmp[i] == p_c)
        {
          m_ptr += i;
          return (true);
        }
        ++i;
      }
      return (false);
    }

    public bool ReadEnd()
    {
      while (!this.IsEOI())
      {
        this.ReadChar();
      }
      return (true);
    }

    public bool IsEOI()
    {
      return (m_ptr >= m_str.Length);
    }

#endregion

    #region Standard Methods

    public bool ReadAlpha()
    {
      return (this.ReadRange('a', 'z') || this.ReadRange('A', 'Z'));
    }

    public bool ReadAlphaNum()
    {
      return (this.ReadAlpha() || this.ReadNum());
    }

    public bool ReadNum()
    {
      return (this.ReadRange('0', '9'));
    }

    public bool ReadNumber()
    {
      if (!this.ReadNum())
        return (false);
      while (!this.IsEOI())
      {
        if (!this.ReadNum())
          return (true);
      }
      return (true);
    }

    public bool ReadOptNegNumber()
    {
      int l_ptr;

      l_ptr = m_ptr;
      this.ReadChar('-');
      if (!this.ReadNumber())
      {
        m_ptr = l_ptr;
        return (false);
      }
      return (true);
    }

    public bool ReadOptNegDouble()
    {
      int l_ptr;

      l_ptr = m_ptr;
      if (this.ReadOptNegNumber() && this.ReadChar('.') && this.ReadNum())
      {
        this.ReadNumber();
        return (true);
      }
      m_ptr = l_ptr;
      return (false);
    }

    public bool ReadIdentifier()
    {
      if (!(this.ReadAlpha() || this.ReadChar('_')))
        return (false);
      while (!this.IsEOI())
      {
        if (!(this.ReadAlphaNum() || this.ReadChar('_')))
          return (true);
      }
      return (true);
    }

    public bool ReadWhitespaces()
    {
      if (!this.ReadOf(m_spaces))
        return (false);
      while (!this.IsEOI())
      {
        if (!this.ReadOf(m_spaces))
          return (true);
      }
      return (true);
    }

    #endregion

    public bool Save(string p_name)
    {
      m_save[p_name] = m_ptr;
      return (true);
    }

    public string Stop(string p_name)
    {
      string str;

      try
      {
        str = m_str.Substring(m_save[p_name], m_ptr - m_save[p_name]);
        return (str);
      }
      catch
      {
        return (null);
      }
    }
  }
}
