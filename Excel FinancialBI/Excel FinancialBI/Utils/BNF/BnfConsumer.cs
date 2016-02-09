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

    public static readonly char NA = (char)0;
    public static readonly string m_spaces = " \t\v\f";

    public BnfConsumer()
    {
      this.Clear();
    }

    #region Setters

    public void Set(string p_str)
    {
      m_str = p_str;
    }

    public void SetPtr(int p_ptr)
    {
      m_ptr = p_ptr;
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
        return (NA);
      return (m_str[m_ptr++]);
    }

    public bool ReadChar(char p_c)
    {
      if (m_str[m_ptr] == p_c)
      {
        ++m_ptr;
        return (true);
      }
      return (false);
    }

    public bool IsChar(char p_c)
    {
      return (m_str[m_ptr] == p_c);
    }

    public bool ReadRange(char p_a, char p_b)
    {
      if (m_str[m_ptr] >= p_a && m_str[m_ptr] <= p_b)
      {
        ++m_ptr;
        return (true);
      }
      return (false);
    }

    public bool ReadOf(string chars)
    {
      foreach (char c in chars)
      {
        if (this.ReadChar(c))
          return (true);
      }
      return (false);
    }

    public bool ReadText(string text)
    {
      if (m_str.Substring(m_ptr).StartsWith(text))
      {
        m_ptr += text.Length;
        return (true);
      }
      return (false);
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
