// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.PropertyBagEx`1
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System.Collections.Generic;

namespace VIBlend.WinForms.DataGridView
{
  internal class PropertyBagEx<T>
  {
    private Dictionary<string, Dictionary<T, object>> namedPropertyTables = new Dictionary<string, Dictionary<T, object>>();

    public string[] PropertyTables
    {
      get
      {
        List<string> stringList = new List<string>();
        Dictionary<string, Dictionary<T, object>>.Enumerator enumerator = this.namedPropertyTables.GetEnumerator();
        while (enumerator.MoveNext())
          stringList.Add(enumerator.Current.Key);
        return stringList.ToArray();
      }
    }

    public Dictionary<T, object> GetPropertyTable(string name)
    {
      if (!this.namedPropertyTables.ContainsKey(name))
        this.namedPropertyTables.Add(name, new Dictionary<T, object>());
      return this.namedPropertyTables[name];
    }

    public int GetPropertyCount(string name)
    {
      Dictionary<T, object> propertyTable = this.GetPropertyTable(name);
      if (propertyTable == null)
        return 0;
      return propertyTable.Count;
    }

    public object GetPropertyValue(string name, T key)
    {
      Dictionary<T, object> propertyTable = this.GetPropertyTable(name);
      if (propertyTable == null)
        return (object) null;
      if (propertyTable.ContainsKey(key))
        return propertyTable[key];
      return (object) null;
    }

    public bool ContainsPropertyValue(string name, T key)
    {
      Dictionary<T, object> propertyTable = this.GetPropertyTable(name);
      if (propertyTable == null)
        return false;
      return propertyTable.ContainsKey(key);
    }

    public bool RemovePropertyValue(string name, T key)
    {
      Dictionary<T, object> propertyTable = this.GetPropertyTable(name);
      if (propertyTable == null)
        return false;
      return propertyTable.Remove(key);
    }

    public void SetPropertyValue(string name, T key, object value)
    {
      Dictionary<T, object> propertyTable = this.GetPropertyTable(name);
      if (propertyTable == null)
        return;
      if (value == null)
        propertyTable.Remove(key);
      else
        propertyTable[key] = value;
    }

    public void Clear(string name)
    {
      this.GetPropertyTable(name).Clear();
    }

    public void Clear()
    {
      Dictionary<string, Dictionary<T, object>>.Enumerator enumerator = this.namedPropertyTables.GetEnumerator();
      while (enumerator.MoveNext())
        enumerator.Current.Value.Clear();
      this.namedPropertyTables.Clear();
    }
  }
}
