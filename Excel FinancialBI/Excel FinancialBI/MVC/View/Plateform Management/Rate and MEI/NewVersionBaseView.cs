using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBI.MVC.View
{
  using Controller;

  public abstract partial class NewVersionBaseView : Form, IView
  {
    public UInt32 SelectedParent { get; set;}

    public NewVersionBaseView()
    {
      InitializeComponent();
    }

    public abstract void SetController(IController p_controller);
    public abstract void LoadView();
  }
}
