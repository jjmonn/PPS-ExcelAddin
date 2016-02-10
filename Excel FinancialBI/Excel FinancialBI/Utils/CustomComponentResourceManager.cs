using System;
using System.ComponentModel;

namespace FBI.Utils
{
  public class CustomComponentResourceManager : ComponentResourceManager
  {
    public CustomComponentResourceManager(Type type, string resourceName)
      : base(type)
    {
      this.BaseNameField = resourceName;
    }
  }
}
