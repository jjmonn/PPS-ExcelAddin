using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SafeDictionary<TKey, TValue> : Dictionary<TKey, TValue>
{
  new public TValue this[TKey key] 
  {
    get
    {
      TValue l_value;
      if (TryGetValue(key, out l_value))
        return (l_value);
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
