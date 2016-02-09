using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Utils
{
  public class MultiIndexDictionary<KeyA, KeyB, Value>
  {
    private UInt32 m_id = 0;
    private Dictionary<KeyA, UInt32> m_firstDic = new Dictionary<KeyA, uint>();
    private Dictionary<KeyB, UInt32> m_secondDic = new Dictionary<KeyB, uint>();
    private Dictionary<UInt32, Value> m_mainDic = new Dictionary<uint, Value>();
    private List<Value> m_sortedList = null;

    public Dictionary<UInt32, Value>.ValueCollection Values
    {
      get
      {
        return m_mainDic.Values;
      }
    }

    public List<Value> SortedValues
    {
      get
      {
        if (m_sortedList == null)
          m_sortedList = m_mainDic.OrderBy(x => x.Value).Select(p => p.Value).ToList();
        return m_sortedList;
      }
    }

    public Int32 Count
    {
      get { return (m_mainDic.Count); }
    }

    public Dictionary<KeyA, uint>.KeyCollection Keys
    {
      get { return m_firstDic.Keys; }
    }

    public Dictionary<KeyB, uint>.KeyCollection SecondaryKeys
    {
      get { return m_secondDic.Keys; }
    }

    public bool ContainsKey(KeyA p_key)
    {
      return m_firstDic.ContainsKey(p_key);
    }

    public bool ContainsSecondaryKey(KeyB p_key)
    {
      return m_secondDic.ContainsKey(p_key);
    }

    public Value this[KeyA index]
    {
      get
      {
        return PrimaryKeyItem(index);
      }
    }

    public Value this[KeyB index]
    {
      get
      {
        return SecondaryKeyItem(index);
      }
    }

    public Value PrimaryKeyItem(KeyA p_key)
    {
      if (!m_firstDic.ContainsKey(p_key))
        return default(Value);
      if (!m_mainDic.ContainsKey(m_firstDic[p_key]))
        return default(Value);
      return m_mainDic[m_firstDic[p_key]];
    }

    public Value SecondaryKeyItem(KeyB p_key)
    {
      if (!m_secondDic.ContainsKey(p_key))
        return default(Value);
      if (!m_mainDic.ContainsKey(m_secondDic[p_key]))
        return default(Value);
      return m_mainDic[m_secondDic[p_key]];
    }

    public bool Set(KeyA p_keyA, KeyB p_keyB, Value p_value)
    {
      UInt32 id;

      if (m_firstDic.ContainsKey(p_keyA))
        id = m_firstDic[p_keyA];
      else if (m_secondDic.ContainsKey(p_keyB))
        id = m_secondDic[p_keyB];
      else
      {
        id = m_id;
        ++m_id;
      }
      foreach (KeyValuePair<KeyA, UInt32> pair in m_firstDic)
        if (pair.Value == id)
        {
          m_firstDic.Remove(pair.Key);
          break;
        }
      foreach (KeyValuePair<KeyB, UInt32> pair in m_secondDic)
        if (pair.Value == id)
        {
          m_secondDic.Remove(pair.Key);
          break;
        }
      m_firstDic[p_keyA] = id;
      m_secondDic[p_keyB] = id;
      m_mainDic[id] = p_value;
      m_sortedList = null;
      return (true);
    }

    public void Remove(KeyA p_key)
    {
      RemovePrimary(p_key);
    }

    public void Remove(KeyB p_key)
    {
      RemoveSecondary(p_key);
    }

    public void RemovePrimary(KeyA p_key)
    {
      if (this[p_key] != null)
      {
        UInt32 id = m_firstDic[p_key];

        m_mainDic.Remove(id);
        m_firstDic.Remove(p_key);
        RemoveValue<KeyB>(m_secondDic, id);
        m_sortedList = null;
      }
    }

    public void RemoveSecondary(KeyB p_key)
    {
      if (this[p_key] != null)
      {
        UInt32 id = m_secondDic[p_key];

        m_mainDic.Remove(id);
        m_secondDic.Remove(p_key);
        RemoveValue<KeyA>(m_firstDic, id);
        m_sortedList = null;
      }
    }

    public void RemoveValue<TKey>(Dictionary<TKey, UInt32> p_dic, UInt32 p_value)
    {
      foreach (KeyValuePair<TKey, UInt32> elem in p_dic)
      {
        if (elem.Value == p_value)
        {
          p_dic.Remove(elem.Key);
          return;
        }
      }
    }

    public void Clear()
    {
      m_firstDic.Clear();
      m_secondDic.Clear();
      m_mainDic.Clear();
      m_sortedList = null;
    }

    public MultiIndexDictionary<KeyA, KeyB, DValue> Cast<DValue>()
    {
      MultiIndexDictionary<KeyA, KeyB, DValue> dest = new MultiIndexDictionary<KeyA, KeyB, DValue>();

      dest.m_firstDic = m_firstDic;
      dest.m_secondDic = m_secondDic;
      dest.m_id = m_id;
      dest.m_mainDic = m_mainDic as Dictionary<uint, DValue>;
      return dest;
    }
  }
}