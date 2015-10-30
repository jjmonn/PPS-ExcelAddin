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
  }

  public MultiIndexDictionary<KeyA, KeyB, DValue> Cast<DValue>()
  {
    MultiIndexDictionary<KeyA, KeyB, DValue> dest = new MultiIndexDictionary<KeyA, KeyB, DValue>();

    dest.m_firstDic = m_firstDic;
    dest.m_secondDic = m_secondDic;
    dest.m_id = m_id;
    dest.m_mainDic = m_mainDic as SortedDictionary<uint, DValue>;
    return dest;
  }
}

