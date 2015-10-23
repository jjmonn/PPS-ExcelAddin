using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MultiIndexDictionary<KeyA, KeyB, Value>
{
  private UInt32 m_id = 0;
  private Dictionary<KeyA, UInt32> m_firstDic = new Dictionary<KeyA,uint>();
  private Dictionary<KeyB, UInt32> m_secondDic = new Dictionary<KeyB, uint>();
  private SortedDictionary<UInt32, Value> m_mainDic = new SortedDictionary<uint,Value>();

  public SortedDictionary<UInt32, Value>.ValueCollection Values
  {
    get
    {
      return m_mainDic.Values;
    }
  }

  public Dictionary<KeyA, uint>.KeyCollection Keys
  {
    get { return m_firstDic.Keys; }
  }

  public Dictionary<KeyB, uint>.KeyCollection SecondaryKeys
  {
    get { return m_secondDic.Keys; }
  }

  public Value this[KeyA index]
  {
    get
    {
      if (!m_firstDic.ContainsKey(index))
        return default(Value);
      if (!m_mainDic.ContainsKey(m_firstDic[index]))
        return default(Value);
      return m_mainDic[m_firstDic[index]];
    }
  }

  public Value this[KeyB index]
  {
    get
    {
      if (!m_secondDic.ContainsKey(index))
        return default(Value);
      if (!m_mainDic.ContainsKey(m_secondDic[index]))
        return default(Value);
      return m_mainDic[m_secondDic[index]];
    }
  }

  public bool Set(KeyA p_keyA, KeyB p_keyB, Value p_value)
  {
    UInt32 id;

    if (m_firstDic.ContainsKey(p_keyA) == false && m_secondDic.ContainsKey(p_keyB) == false)
    {
      m_mainDic[m_id] = p_value;
      id = m_id;
      ++m_id;
    }
    else if (m_firstDic[p_keyA] == 0 || m_secondDic[p_keyB] == 0)
    {
      return false;
    }
    else
      id = m_firstDic[p_keyA];
      
    m_firstDic[p_keyA] = id;
    m_secondDic[p_keyB] = id;
    return (true);
  }

  public void Remove(KeyA p_key)
  {
    if (this[p_key] != null)
    {
      UInt32 id = m_firstDic[p_key];

      m_mainDic.Remove(id);
      m_firstDic.Remove(p_key);
      RemoveValue<KeyB>(m_secondDic, id);
    }
  }

  public void Remove(KeyB p_key)
  {
    if (this[p_key] != null)
    {
      UInt32 id = m_secondDic[p_key];

      m_mainDic.Remove(id);
      m_secondDic.Remove(p_key);
      RemoveValue<KeyA>(m_firstDic, id);
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
}

