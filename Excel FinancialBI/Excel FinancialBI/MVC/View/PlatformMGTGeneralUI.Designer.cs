using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class PlatformMGTGeneralUI : System.Windows.Forms.Form
  {

    //Form overrides dispose to clean up the component list.
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlatformMGTGeneralUI));
      this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
      this.AccountsBT = new System.Windows.Forms.ToolStripMenuItem();
      this.m_entitiesBT = new System.Windows.Forms.ToolStripMenuItem();
      this.m_employeesButton = new System.Windows.Forms.ToolStripMenuItem();
      this.ClientsBT = new System.Windows.Forms.ToolStripMenuItem();
      this.ProductsBT = new System.Windows.Forms.ToolStripMenuItem();
      this.AdjustmentsBT = new System.Windows.Forms.ToolStripMenuItem();
      this.CategoriesBT = new System.Windows.Forms.ToolStripMenuItem();
      this.m_entitiesFiltersBT = new System.Windows.Forms.ToolStripMenuItem();
      this.m_employeesFiltersBT = new System.Windows.Forms.ToolStripMenuItem();
      this.m_clientsFiltersBT = new System.Windows.Forms.ToolStripMenuItem();
      this.m_productsFiltersBT = new System.Windows.Forms.ToolStripMenuItem();
      this.m_adjustmentsFiltersBT = new System.Windows.Forms.ToolStripMenuItem();
      this.VersionsBT = new System.Windows.Forms.ToolStripMenuItem();
      this.CurrenciesBT = new System.Windows.Forms.ToolStripMenuItem();
      this.ExchangeRatesButton = new System.Windows.Forms.ToolStripMenuItem();
      this.GlobalFact_BT = new System.Windows.Forms.ToolStripMenuItem();
      this.GroupsBT = new System.Windows.Forms.ToolStripMenuItem();
      this.Panel1 = new System.Windows.Forms.Panel();
      this.MenuStrip1.SuspendLayout();
      this.SuspendLayout();
      //
      //MenuStrip1
      //
      this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.AccountsBT,
			this.m_entitiesBT,
			this.m_employeesButton,
			this.ClientsBT,
			this.ProductsBT,
			this.AdjustmentsBT,
			this.CategoriesBT,
			this.VersionsBT,
			this.CurrenciesBT,
			this.ExchangeRatesButton,
			this.GlobalFact_BT,
			this.GroupsBT
		});
      this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
      this.MenuStrip1.Name = "MenuStrip1";
      this.MenuStrip1.Size = new System.Drawing.Size(1445, 59);
      this.MenuStrip1.TabIndex = 0;
      this.MenuStrip1.Text = "MenuStrip1";
      //
      //AccountsBT
      //
      this.AccountsBT.CheckOnClick = true;
      this.AccountsBT.Image = global::FBI.Properties.Resources.fbiIcon;
      this.AccountsBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.AccountsBT.Name = "AccountsBT";
      this.AccountsBT.Size = new System.Drawing.Size(77, 55);
      this.AccountsBT.Text = "Accounts";
      this.AccountsBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.AccountsBT.ToolTipText = "[GeneralEditionUI.tool_tip_account]";
      //
      //m_entitiesBT
      //
      this.m_entitiesBT.CheckOnClick = true;
      this.m_entitiesBT.Image = global::FBI.Properties.Resources.elements_hierarchy;
      this.m_entitiesBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.m_entitiesBT.Name = "m_entitiesBT";
      this.m_entitiesBT.Size = new System.Drawing.Size(65, 55);
      this.m_entitiesBT.Text = "Entities";
      this.m_entitiesBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.m_entitiesBT.ToolTipText = "[GeneralEditionUI.tool_tip_entities]";
      //
      //m_employeesButton
      //
      this.m_employeesButton.Image = global::FBI.Properties.Resources.engineer;
      this.m_employeesButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.m_employeesButton.Name = "m_employeesButton";
      this.m_employeesButton.Size = new System.Drawing.Size(94, 55);
      this.m_employeesButton.Text = "Consultants";
      this.m_employeesButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      //
      //ClientsBT
      //
      this.ClientsBT.CheckOnClick = true;
      this.ClientsBT.Image = global::FBI.Properties.Resources.users_relation;
      this.ClientsBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.ClientsBT.Name = "ClientsBT";
      this.ClientsBT.Size = new System.Drawing.Size(62, 55);
      this.ClientsBT.Text = "Clients";
      this.ClientsBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.ClientsBT.ToolTipText = "[GeneralEditionUI.tool_tip_clients]";
      //
      //ProductsBT
      //
      this.ProductsBT.CheckOnClick = true;
      this.ProductsBT.Image = global::FBI.Properties.Resources.barcode1;
      this.ProductsBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.ProductsBT.Name = "ProductsBT";
      this.ProductsBT.Size = new System.Drawing.Size(75, 55);
      this.ProductsBT.Text = "Products";
      this.ProductsBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.ProductsBT.ToolTipText = "[GeneralEditionUI.tool_tip_products]";
      //
      //AdjustmentsBT
      //
      this.AdjustmentsBT.Image = global::FBI.Properties.Resources.elements4;
      this.AdjustmentsBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.AdjustmentsBT.Name = "AdjustmentsBT";
      this.AdjustmentsBT.Size = new System.Drawing.Size(98, 55);
      this.AdjustmentsBT.Text = "Adjustments";
      this.AdjustmentsBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.AdjustmentsBT.ToolTipText = "[GeneralEditionUI.tool_tip_adjustments]";
      //
      //CategoriesBT
      //
      this.CategoriesBT.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.m_entitiesFiltersBT,
			this.m_employeesFiltersBT,
			this.m_clientsFiltersBT,
			this.m_productsFiltersBT,
			this.m_adjustmentsFiltersBT
		});
      this.CategoriesBT.Image = global::FBI.Properties.Resources.filter_and_sort;
      this.CategoriesBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.CategoriesBT.Name = "CategoriesBT";
      this.CategoriesBT.Size = new System.Drawing.Size(86, 55);
      this.CategoriesBT.Text = "Categories";
      this.CategoriesBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.CategoriesBT.ToolTipText = "[GeneralEditionUI.tool_tip_categories]";
      //
      //m_entitiesFiltersBT
      //
      this.m_entitiesFiltersBT.Image = global::FBI.Properties.Resources.elements_hierarchy;
      this.m_entitiesFiltersBT.Name = "m_entitiesFiltersBT";
      this.m_entitiesFiltersBT.Size = new System.Drawing.Size(155, 24);
      this.m_entitiesFiltersBT.Text = "Entities";
      //
      //m_employeesFiltersBT
      //
      this.m_employeesFiltersBT.Image = global::FBI.Properties.Resources.engineer;
      this.m_employeesFiltersBT.Name = "m_employeesFiltersBT";
      this.m_employeesFiltersBT.Size = new System.Drawing.Size(155, 24);
      this.m_employeesFiltersBT.Text = "Consultants";
      //
      //m_clientsFiltersBT
      //
      this.m_clientsFiltersBT.Image = global::FBI.Properties.Resources.users_relation;
      this.m_clientsFiltersBT.Name = "m_clientsFiltersBT";
      this.m_clientsFiltersBT.Size = new System.Drawing.Size(155, 24);
      this.m_clientsFiltersBT.Text = "Clients";
      //
      //m_productsFiltersBT
      //
      this.m_productsFiltersBT.Image = global::FBI.Properties.Resources.barcode1;
      this.m_productsFiltersBT.Name = "m_productsFiltersBT";
      this.m_productsFiltersBT.Size = new System.Drawing.Size(155, 24);
      this.m_productsFiltersBT.Text = "Products";
      //
      //m_adjustmentsFiltersBT
      //
      this.m_adjustmentsFiltersBT.Image = global::FBI.Properties.Resources.elements4;
      this.m_adjustmentsFiltersBT.Name = "m_adjustmentsFiltersBT";
      this.m_adjustmentsFiltersBT.Size = new System.Drawing.Size(155, 24);
      this.m_adjustmentsFiltersBT.Text = "Adjustments";
      //
      //VersionsBT
      //
      this.VersionsBT.CheckOnClick = true;
      this.VersionsBT.Image = global::FBI.Properties.Resources.Excel_dark_24_24;
      this.VersionsBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.VersionsBT.Name = "VersionsBT";
      this.VersionsBT.Size = new System.Drawing.Size(72, 55);
      this.VersionsBT.Text = "Versions";
      this.VersionsBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.VersionsBT.ToolTipText = "[GeneralEditionUI.tool_tip_versions]";
      //
      //CurrenciesBT
      //
      this.CurrenciesBT.CheckOnClick = true;
      this.CurrenciesBT.Image = global::FBI.Properties.Resources.symbol_dollar_euro1;
      this.CurrenciesBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.CurrenciesBT.Name = "CurrenciesBT";
      this.CurrenciesBT.Size = new System.Drawing.Size(85, 55);
      this.CurrenciesBT.Text = "Currencies";
      this.CurrenciesBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.CurrenciesBT.ToolTipText = "[GeneralEditionUI.tool_tip_currencies]";
      //
      //ExchangeRatesButton
      //
      this.ExchangeRatesButton.Image = global::FBI.Properties.Resources.money_interest2;
      this.ExchangeRatesButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.ExchangeRatesButton.Name = "ExchangeRatesButton";
      this.ExchangeRatesButton.Size = new System.Drawing.Size(112, 55);
      this.ExchangeRatesButton.Text = "Exchange rates";
      this.ExchangeRatesButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.ExchangeRatesButton.ToolTipText = "[GeneralEditionUI.tool_tip_exchange_rates]";
      //
      //GlobalFact_BT
      //
      this.GlobalFact_BT.Image = global::FBI.Properties.Resources.chart_line;
      this.GlobalFact_BT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.GlobalFact_BT.Name = "GlobalFact_BT";
      this.GlobalFact_BT.Size = new System.Drawing.Size(185, 55);
      this.GlobalFact_BT.Text = "Macro Economic indicators";
      this.GlobalFact_BT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.GlobalFact_BT.ToolTipText = "[GeneralEditionUI.tool_tip_economic_indicators]";
      //
      //GroupsBT
      //
      this.GroupsBT.Image = global::FBI.Properties.Resources.users_family;
      this.GroupsBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.GroupsBT.Name = "GroupsBT";
      this.GroupsBT.Size = new System.Drawing.Size(104, 55);
      this.GroupsBT.Text = "Users Groups";
      this.GroupsBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.GroupsBT.ToolTipText = "[GeneralEditionUI.tool_tip_users_groups]";
      //
      //Panel1
      //
      this.Panel1.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
      this.Panel1.Location = new System.Drawing.Point(0, 83);
      this.Panel1.Name = "Panel1";
      this.Panel1.Size = new System.Drawing.Size(1445, 432);
      this.Panel1.TabIndex = 1;
      //
      //PlatformMGTGeneralUI
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1445, 515);
      this.Controls.Add(this.Panel1);
      this.Controls.Add(this.MenuStrip1);
      this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
      this.MainMenuStrip = this.MenuStrip1;
      this.Name = "PlatformMGTGeneralUI";
      this.Text = "[GeneralEditionUI.platform_config]";
      this.MenuStrip1.ResumeLayout(false);
      this.MenuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }
    internal System.Windows.Forms.MenuStrip MenuStrip1;
    internal System.Windows.Forms.ToolStripMenuItem AccountsBT;
    internal System.Windows.Forms.ToolStripMenuItem m_entitiesBT;
    internal System.Windows.Forms.ToolStripMenuItem ClientsBT;
    internal System.Windows.Forms.ToolStripMenuItem ProductsBT;
    internal System.Windows.Forms.ToolStripMenuItem VersionsBT;
    internal System.Windows.Forms.ToolStripMenuItem CurrenciesBT;
    internal System.Windows.Forms.Panel Panel1;
    internal System.Windows.Forms.ToolStripMenuItem AdjustmentsBT;
    internal System.Windows.Forms.ToolStripMenuItem CategoriesBT;
    internal System.Windows.Forms.ToolStripMenuItem m_clientsFiltersBT;
    internal System.Windows.Forms.ToolStripMenuItem m_entitiesFiltersBT;
    internal System.Windows.Forms.ToolStripMenuItem m_productsFiltersBT;
    internal System.Windows.Forms.ToolStripMenuItem m_adjustmentsFiltersBT;
    internal System.Windows.Forms.ToolStripMenuItem GroupsBT;
    internal System.Windows.Forms.ToolStripMenuItem ExchangeRatesButton;
    private System.Windows.Forms.ToolStripMenuItem GlobalFact_BT;
    internal System.Windows.Forms.ToolStripMenuItem m_employeesButton;
    internal System.Windows.Forms.ToolStripMenuItem m_employeesFiltersBT;
  }
}