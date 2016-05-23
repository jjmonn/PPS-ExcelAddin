using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Utils
{
  class CircularBuffer<T>
  {
    T[] m_buffer;
    int m_cursor;
    int m_virtualSize;

    public CircularBuffer(uint p_size)
    {
      m_buffer = new T[p_size];
      m_cursor = 0;
      m_virtualSize = 0;
    }

    int Cursor
    {
      set
      {
        m_cursor = value;
        m_cursor %= m_buffer.Length;
        if (m_cursor < 0)
          m_cursor = m_buffer.Length + m_cursor;
      }
      get
      {
        return (m_cursor);
      }
    }

    public T this[int p_index]
    {
      get
      {
        if (p_index < 0 || p_index >= m_buffer.Length)
          throw new IndexOutOfRangeException();
        return (m_buffer[(Cursor + p_index) % m_buffer.Length]);
      }
    }

    public void Push(T p_value)
    {
      Cursor++;
      m_buffer[Cursor] = p_value;
      if (m_virtualSize < m_buffer.Length)
        m_virtualSize++;
    }

    public T Top()
    {
      return (m_buffer[Cursor]);
    }

    public void Pop()
    {
      if (m_virtualSize <= 0)
        return;
      m_buffer[Cursor] = default(T);
      Cursor--;
      m_virtualSize--;
    }

    public void Clear()
    {
      for (int i = 0; i < m_buffer.Length; ++i)
        m_buffer[i] = default(T);
      Cursor = 0;
      m_virtualSize = 0;
    }

    public int ContentSize
    {
      get { return (m_virtualSize); }
    }
  }
}
