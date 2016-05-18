using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Utils
{
  using MVC.Model;

  class Consumer
  {
    public const char INVALID_CHAR = (char)0;
    private const string WHITESPACE = " \t\v\f";

    private string m_str;
    private Int32 m_ptr;
    private Dictionary<string, Int32> m_tags = new Dictionary<string, Int32>();

    public Consumer(string p_str = "")
    {
      this.Set(p_str, 0);
    }

    public Int32 Ptr
    {
      get { return (m_ptr); }
    }

    public string Str
    {
      get { return (m_str); }
    }

    public void Set(string p_str)
    {
      this.Set(p_str, 0);
    }

    public void Set(Int32 p_ptr)
    {
      m_ptr = p_ptr;
    }

    public void Set(string p_str, Int32 p_ptr)
    {
      m_str = p_str;
      m_ptr = p_ptr;
    }

    public bool IsChar(char p_c)
    {
      return (m_str[m_ptr] == p_c);
    }

    public bool HasChar(string p_chars)
    {
      if (m_ptr >= m_str.Length)
        return (false);
      for (Int32 i = 0; i < p_chars.Length; ++i)
      {
        if (m_str[m_ptr] == p_chars[i])
          return (true);
      }
      return (false);
    }

    public char GetChar()
    {
      if (m_ptr < m_str.Length)
        return (m_str[m_ptr]);
      return (INVALID_CHAR);
    }

    public bool IsEOI()
    {
      return (m_ptr >= m_str.Length);
    }

    private bool Incr(Int32 p_len = 1)
    {
      m_ptr += p_len;
      return (true);
    }

    public char ReadChar()
    {
      if (m_ptr < m_str.Length)
        return (m_str[m_ptr++]);
      return (INVALID_CHAR);
    }

    public bool ReadChar(char p_c)
    {
      if (m_ptr < m_str.Length && m_str[m_ptr] == p_c)
        return (this.Incr());
      return (false);
    }

    public bool ReadRange(char p_x, char p_y)
    {
      if (m_ptr < m_str.Length && m_str[m_ptr] >= p_x && m_str[m_ptr] <= p_y)
        return (this.Incr());
      return (false);
    }

    public bool ReadPrintable()
    {
      return (this.ReadRange(' ', '~'));
    }

    public bool ReadOf(string p_chars)
    {
      if (this.HasChar(p_chars))
        return (this.Incr());
      return (false);
    }

    public bool ReadText(string p_str)
    {
      if (String.Compare(m_str, m_ptr, p_str, 0, p_str.Length) == 0)
        return (this.Incr(p_str.Length));
      return (false);
    }

    public bool ReadTo(char p_c)
    {
      Int32 i = m_ptr;

      while (i < m_str.Length)
      {
        if (m_str[i] == p_c)
        {
          m_ptr = i;
          return (true);
        }
        ++i;
      }
      return (false);
    }

    public bool ReadDigit()
    {
      return (this.ReadRange('0', '9'));
    }

    public bool ReadAlpha()
    {
      return (this.ReadRange('a', 'z') || this.ReadRange('A', 'Z'));
    }

    public bool ReadAlnum()
    {
      return (this.ReadAlpha() || this.ReadDigit());
    }

    public bool ReadWhitespace()
    {
      return (this.ReadOf(WHITESPACE));
    }

    public bool ReadNum()
    {
      if (!this.ReadDigit())
        return (false);
      while (this.ReadDigit()) ;
      return (true);
    }

    public bool ReadWord()
    {
      if (!this.ReadAlpha())
        return (false);
      while (this.ReadAlpha()) ;
      return (true);
    }

    public bool ReadIdentifier()
    {
      if (!(this.ReadAlpha() || this.ReadChar('_')))
        return (false);
      while (this.ReadAlnum() || this.ReadChar('_')) ;
      return (true);
    }

    public bool ReadAccount()
    {
      if (this.HasChar(Constants.FORBIDEN_CHARS))
        return (false);
      while (!this.HasChar(Constants.FORBIDEN_CHARS) && this.ReadChar() != INVALID_CHAR);
      return (true);
    }

    public bool ReadMultipleWhitespace()
    {
      if (!this.ReadWhitespace())
        return (false);
      while (this.ReadWhitespace()) ;
      return (true);
    }

    //Read from operator 'a' to operator 'b'
    //Examples using a = '(' and b = ')'
    //str = "(being), and I like chocolate" -> Will consume "(being)"
    //str = "(being, and i (like programming)), and so chocolate" -> Will consume "(being, and i (like programming))"
    //str = "hello" -> Will consume nothing
    public bool ReadOperator(char p_x, char p_b)
    {
      char c;
      Int32 i = 1;
      Int32 ptr = m_ptr;

      if (!this.ReadChar(p_x))
        return (false);
      while (i > 0 && ((c = this.ReadChar()) != INVALID_CHAR))
        i = (c == p_x ? i + 1 : c == p_b ? i - 1 : i);
      if (i != 0)
      {
        m_ptr = ptr;
        return (false);
      }
      return (true);
    }

    public bool Save(string p_id)
    {
      if (!m_tags.ContainsKey(p_id))
        m_tags.Add(p_id, m_ptr);
      else
        m_tags[p_id] = m_ptr;
      return (true);
    }

    public bool Stop(string p_id, out string p_return)
    {
      p_return = null;
      Int32 l_val = 0;

      try
      {
        if (!m_tags.TryGetValue(p_id, out l_val))
          return (false);
        p_return = m_str.Substring(l_val, m_ptr - l_val);
        return (true);
      }
      catch
      {
        return (false);
      }
    }

    public void ClearTag(string p_id)
    {
      if (m_tags.ContainsKey(p_id))
        m_tags.Remove(p_id);
    }

    public void ClearTags()
    {
      m_tags.Clear();
    }

    public string GetString()
    {
      return (m_str.Substring(m_ptr));
    }
  }
}
