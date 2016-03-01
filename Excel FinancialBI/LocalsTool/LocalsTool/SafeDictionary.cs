using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SafeDictionary<TKey, TValue> : Dictionary<TKey, TValue>
{
  public SafeDictionary()
  {

  }

  public SafeDictionary(int p_capacity) : base(p_capacity)
  {

  }

  new public TValue this[TKey key] 
  {
    get
    {
      if (ContainsKey(key))
        return (base[key]);
      return (default(TValue));
    }
    set
    {
      if (ContainsKey(key))
        base[key] = value;
      else
        Add(key, value);
    }
  }
}
