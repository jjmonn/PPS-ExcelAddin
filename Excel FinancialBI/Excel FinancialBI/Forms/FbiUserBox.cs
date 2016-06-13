using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBI.Forms
{
  public partial class FbiUserBoxForm : Form
  {
    private FbiUserBox.Type m_type;
    private SafeDictionary<FbiUserBox.Type, Control> m_controls;

    public FbiUserBoxForm()
    {
      InitializeComponent();
      m_controls = new SafeDictionary<FbiUserBox.Type, Control>();
      m_controls[FbiUserBox.Type.Combobox] = m_combo;
      m_controls[FbiUserBox.Type.Textbox] = m_text;
      m_combo.DropDownList = true;
    }

    private void UpdateDisplay()
    {
      foreach (Control l_control in m_controls.Values)
        l_control.Visible = false;
      m_controls[m_type].Visible = true;
    }

    public string Title
    {
      get { return (m_title.Text); }
      set { m_title.Text = value; }
    }

    public FbiUserBox.Type Type
    {
      get { return (m_type); }
      set
      {
        m_type = value;
        this.UpdateDisplay();
      }
    }

    public List<string> ComboBox
    {
      set
      {
        m_combo.Text = "";
        m_combo.Items.Clear();
        foreach (string l_item in value)
          m_combo.Items.Add(l_item);
        if (value.Count > 0)
          m_combo.SelectedIndex = 0;
      }
    }

    public string TextBox
    {
      set { m_text.Text = value; }
    }

    public string SelectedText
    {
      get { return (m_controls[m_type].Text); }
    }

    public int SelectedIndex
    {
      get
      {
        return (m_type == FbiUserBox.Type.Textbox ? 0 : m_combo.SelectedIndex);
      }
    }

    private void m_ok_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void m_cancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }
  }

  public static class FbiUserBox
  {
    private static string m_enteredString = "";
    private static int m_index = 0;
    private static FbiUserBoxForm m_form = new FbiUserBoxForm();

    public enum Type
    {
      Textbox,
      Combobox
    };

    public static string EnteredString
    {
      get { return (m_enteredString); }
    }

    public static int Index
    {
      get { return (m_index); }
    }

    public static DialogResult ShowDialog(string p_name, string p_title,
      List<string> p_param)
    {
      m_form.Text = p_name;
      m_form.Title = p_title;
      m_form.Type = Type.Combobox;
      m_form.ComboBox = p_param;

      m_form.ShowDialog();
      m_enteredString = m_form.SelectedText;
      m_index = m_form.SelectedIndex;
      return (m_form.DialogResult);
    }
  }
}
