﻿using System;
using System.ComponentModel;

namespace FBI.Utils
{
  internal class CustomComponentResourceManager : ComponentResourceManager
  {
    public CustomComponentResourceManager(Type type, string resourceName)
      : base(type)
    {
      this.BaseNameField = resourceName;
    }
  }
}
