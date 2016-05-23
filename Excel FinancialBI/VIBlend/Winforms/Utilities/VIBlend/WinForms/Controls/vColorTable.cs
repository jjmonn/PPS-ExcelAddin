// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vColorTable
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  public class vColorTable : ProfessionalColorTable
  {
    public static Color contextMenuBackGround = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color buttonPressedColor1 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color buttonPressedColor3 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color buttonPressedColor2 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color buttonSelectedColor1 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color buttonSelectedColor3 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color buttonSelectedColor2 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color itemSelectedColor1 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color itemSelectedColor2 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color checkBoxBackGround = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color gripColor1 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color gripColor2 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color imageAreaColor1 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color imageAreaColor2 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color imageAreaColor3 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color menuBorder = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color overflowColor1 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color overflowColor3 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color overflowColor2 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color menuToolBackColor1 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color menuToolBackColor2 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color separatorColor1 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color separatorColor2 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color statusStripBackColor1 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color statusStripBackColor2 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color toolStripBorderColor = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color toolStripInnerColor = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color toolStripBackColor1 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color toolStripBackColor3 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color toolStripBackColor2 = Color.FromKnownColor(KnownColor.ControlLight);
    public static Color buttonBorder = Color.FromKnownColor(KnownColor.ControlLight);

    /// <summary>
    /// Gets the solid color to use when a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> other than the top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the solid color to use when a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> other than the top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.
    /// </returns>
    public override Color MenuItemSelected
    {
      get
      {
        return vColorTable.menuToolBackColor1;
      }
    }

    /// <summary>
    /// Gets the starting color of the gradient used when the button is pressed.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the button is pressed.
    /// </returns>
    public override Color ButtonPressedGradientBegin
    {
      get
      {
        return vColorTable.buttonPressedColor1;
      }
    }

    /// <summary>
    /// Gets the end color of the gradient used when the button is pressed.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is pressed.
    /// </returns>
    public override Color ButtonPressedGradientEnd
    {
      get
      {
        return vColorTable.buttonPressedColor3;
      }
    }

    /// <summary>
    /// Gets the middle color of the gradient used when the button is pressed.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when the button is pressed.
    /// </returns>
    public override Color ButtonPressedGradientMiddle
    {
      get
      {
        return vColorTable.buttonPressedColor2;
      }
    }

    /// <summary>
    /// Gets the starting color of the gradient used when the button is selected.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the button is selected.
    /// </returns>
    public override Color ButtonSelectedGradientBegin
    {
      get
      {
        return vColorTable.buttonSelectedColor1;
      }
    }

    /// <summary>
    /// Gets the end color of the gradient used when the button is selected.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is selected.
    /// </returns>
    public override Color ButtonSelectedGradientEnd
    {
      get
      {
        return vColorTable.buttonSelectedColor3;
      }
    }

    /// <summary>
    /// Gets the middle color of the gradient used when the button is selected.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when the button is selected.
    /// </returns>
    public override Color ButtonSelectedGradientMiddle
    {
      get
      {
        return vColorTable.buttonSelectedColor2;
      }
    }

    /// <summary>
    /// Gets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedHighlight" />.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedHighlight" />.
    /// </returns>
    public override Color ButtonSelectedHighlightBorder
    {
      get
      {
        return vColorTable.buttonBorder;
      }
    }

    /// <summary>
    /// Gets the solid color to use when the button is checked and gradients are being used.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the solid color to use when the button is checked and gradients are being used.
    /// </returns>
    public override Color CheckBackground
    {
      get
      {
        return vColorTable.checkBoxBackGround;
      }
    }

    /// <summary>
    /// Gets the color to use for shadow effects on the grip (move handle).
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the color to use for shadow effects on the grip (move handle).
    /// </returns>
    public override Color GripDark
    {
      get
      {
        return vColorTable.gripColor1;
      }
    }

    /// <summary>
    /// Gets the color to use for highlight effects on the grip (move handle).
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the color to use for highlight effects on the grip (move handle).
    /// </returns>
    public override Color GripLight
    {
      get
      {
        return vColorTable.gripColor2;
      }
    }

    /// <summary>
    /// Gets the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.
    /// </returns>
    public override Color ImageMarginGradientBegin
    {
      get
      {
        return vColorTable.imageAreaColor1;
      }
    }

    /// <summary>
    /// Gets the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.
    /// </returns>
    public override Color ImageMarginGradientEnd
    {
      get
      {
        return vColorTable.imageAreaColor3;
      }
    }

    /// <summary>
    /// Gets the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.
    /// </returns>
    public override Color ImageMarginRevealedGradientMiddle
    {
      get
      {
        return vColorTable.imageAreaColor2;
      }
    }

    /// <summary>
    /// Gets the color that is the border color to use on a <see cref="T:System.Windows.Forms.MenuStrip" />.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the border color to use on a <see cref="T:System.Windows.Forms.MenuStrip" />.
    /// </returns>
    public override Color MenuBorder
    {
      get
      {
        return vColorTable.menuBorder;
      }
    }

    /// <summary>
    /// Gets the starting color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed.
    /// </returns>
    public override Color MenuItemPressedGradientBegin
    {
      get
      {
        return vColorTable.toolStripBackColor1;
      }
    }

    /// <summary>
    /// Gets the end color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed.
    /// </returns>
    public override Color MenuItemPressedGradientEnd
    {
      get
      {
        return vColorTable.toolStripBackColor3;
      }
    }

    /// <summary>
    /// Gets the middle color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed.
    /// </returns>
    public override Color MenuItemPressedGradientMiddle
    {
      get
      {
        return vColorTable.toolStripBackColor2;
      }
    }

    /// <summary>
    /// Gets the starting color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.
    /// </returns>
    public override Color MenuItemSelectedGradientBegin
    {
      get
      {
        return vColorTable.itemSelectedColor1;
      }
    }

    /// <summary>
    /// Gets the end color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.
    /// </returns>
    public override Color MenuItemSelectedGradientEnd
    {
      get
      {
        return vColorTable.itemSelectedColor2;
      }
    }

    /// <summary>
    /// Gets the starting color of the gradient used in the MenuStrip.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.
    /// </returns>
    public override Color MenuStripGradientBegin
    {
      get
      {
        return vColorTable.menuToolBackColor1;
      }
    }

    /// <summary>
    /// Gets the end color of the gradient used in the MenuStrip.
    /// </summary>
    public override Color MenuStripGradientEnd
    {
      get
      {
        return vColorTable.menuToolBackColor2;
      }
    }

    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStripOverflowButton.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.
    /// </returns>
    public override Color OverflowButtonGradientBegin
    {
      get
      {
        return vColorTable.overflowColor1;
      }
    }

    /// <summary>
    /// Gets the end color of the gradient used in the ToolStripOverflowButton.
    /// </summary>
    public override Color OverflowButtonGradientEnd
    {
      get
      {
        return vColorTable.overflowColor3;
      }
    }

    /// <summary>
    /// Gets the middle color of the gradient used in the ToolStripOverflowButton.
    /// </summary>
    public override Color OverflowButtonGradientMiddle
    {
      get
      {
        return vColorTable.overflowColor2;
      }
    }

    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStripContainer.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.
    /// </returns>
    public override Color RaftingContainerGradientBegin
    {
      get
      {
        return vColorTable.menuToolBackColor1;
      }
    }

    /// <summary>
    /// Gets the end color of the gradient used in the ToolStripContainer.
    /// </summary>
    public override Color RaftingContainerGradientEnd
    {
      get
      {
        return vColorTable.menuToolBackColor1;
      }
    }

    /// <summary>
    /// Gets the color to use to for shadow effects on the ToolStripSeparator.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the color to use to for shadow effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.
    /// </returns>
    public override Color SeparatorDark
    {
      get
      {
        return vColorTable.separatorColor1;
      }
    }

    /// <summary>
    /// Gets the color to use to for highlight effects on the ToolStripSeparator.
    /// </summary>
    public override Color SeparatorLight
    {
      get
      {
        return vColorTable.separatorColor2;
      }
    }

    /// <summary>
    /// Gets the starting color of the gradient used on the StatusStrip.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.
    /// </returns>
    public override Color StatusStripGradientBegin
    {
      get
      {
        return vColorTable.statusStripBackColor1;
      }
    }

    /// <summary>
    /// Gets the end color of the gradient used on the StatusStrip.
    /// </summary>
    public override Color StatusStripGradientEnd
    {
      get
      {
        return vColorTable.statusStripBackColor2;
      }
    }

    /// <summary>
    /// Gets the border color to use on the bottom edge of the ToolStrip.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the border color to use on the bottom edge of the <see cref="T:System.Windows.Forms.ToolStrip" />.
    /// </returns>
    public override Color ToolStripBorder
    {
      get
      {
        return vColorTable.toolStripBorderColor;
      }
    }

    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStripContentPanel.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.
    /// </returns>
    public override Color ToolStripContentPanelGradientBegin
    {
      get
      {
        return vColorTable.toolStripInnerColor;
      }
    }

    /// <summary>
    /// Gets the end color of the gradient used in the ToolStripContentPanel.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.
    /// </returns>
    public override Color ToolStripContentPanelGradientEnd
    {
      get
      {
        return vColorTable.menuToolBackColor1;
      }
    }

    /// <summary>
    /// Gets the solid background color of the ToolStripDropDown.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the solid background color of the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.
    /// </returns>
    public override Color ToolStripDropDownBackground
    {
      get
      {
        return vColorTable.contextMenuBackGround;
      }
    }

    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStrip background.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.
    /// </returns>
    public override Color ToolStripGradientBegin
    {
      get
      {
        return vColorTable.toolStripBackColor1;
      }
    }

    /// <summary>
    /// Gets the end color of the gradient used in the ToolStrip background.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.
    /// </returns>
    public override Color ToolStripGradientEnd
    {
      get
      {
        return vColorTable.toolStripBackColor3;
      }
    }

    /// <summary>
    /// Gets the middle color of the gradient used in the ToolStrip background.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.
    /// </returns>
    public override Color ToolStripGradientMiddle
    {
      get
      {
        return vColorTable.toolStripBackColor2;
      }
    }

    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStripPanel.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.
    /// </returns>
    public override Color ToolStripPanelGradientBegin
    {
      get
      {
        return vColorTable.menuToolBackColor1;
      }
    }

    /// <summary>
    /// Gets the end color of the gradient used in the ToolStripPanel.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.
    /// </returns>
    public override Color ToolStripPanelGradientEnd
    {
      get
      {
        return vColorTable.menuToolBackColor1;
      }
    }
  }
}
