using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class CurrenciesView : System.Windows.Forms.UserControl
  {

    //UserControl overrides dispose to clean up the component list.
    [System.Diagnostics.DebuggerNonUserCode()]
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (disposing && components != null)
        {
          components.Dispose();
        }
      }
      finally
      {
        base.Dispose(disposing);
      }
    }

    //Required by the Windows Form Designer

    private System.ComponentModel.IContainer components;
    //NOTE: The following procedure is required by the Windows Form Designer
    //It can be modified using the Windows Form Designer.  
    //Do not modify it using the code editor.
    [System.Diagnostics.DebuggerStepThrough()]
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CurrenciesView));
      this.VContextMenu1 = new VIBlend.WinForms.Controls.vContextMenu();
      this.SetMainCurrencyCallBack = new System.Windows.Forms.MenuItem();
      this.EditButtonsImagelist = new System.Windows.Forms.ImageList(this.components);
      this.SuspendLayout();
      // 
      // VContextMenu1
      // 
      this.VContextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.SetMainCurrencyCallBack});
      // 
      // SetMainCurrencyCallBack
      // 
      this.SetMainCurrencyCallBack.Index = 0;
      this.SetMainCurrencyCallBack.Text = "Set_main_currency";
      // 
      // EditButtonsImagelist
      // 
      this.EditButtonsImagelist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("EditButtonsImagelist.ImageStream")));
      this.EditButtonsImagelist.TransparentColor = System.Drawing.Color.Transparent;
      this.EditButtonsImagelist.Images.SetKeyName(0, "1420498403_340208.ico");
      // 
      // CurrenciesView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Name = "CurrenciesView";
      this.Size = new System.Drawing.Size(776, 511);
      this.ResumeLayout(false);

    }
    internal VIBlend.WinForms.Controls.vContextMenu VContextMenu1;
    internal System.Windows.Forms.MenuItem SetMainCurrencyCallBack;

    internal System.Windows.Forms.ImageList EditButtonsImagelist;
  }
}