﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.View
{
  using Controller;

  public interface IPlatformMgtView : IView
  {
    void CloseView();
  }
}