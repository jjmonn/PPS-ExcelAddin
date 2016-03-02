using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using VIBlend.WinForms.DataGridView;
using FBI.Forms;

using Dimension = FBI.Forms.BaseFbiDataGridView<uint>.Dimension;

namespace LocalsTool
{
  enum Column
  {
    KEY,
    VALUE
  };


  public partial class LocalsView : Form
  {
    BaseFbiDataGridView<UInt32> m_dgv;

    public LocalsView(string p_path)
    {
      InitializeComponent();
      m_dgv = new BaseFbiDataGridView<UInt32>();
      Controls.Add(m_dgv);
      m_dgv.Dock = DockStyle.Fill;

      m_dgv.SetDimension(Dimension.COLUMN, m_dgv.ColumnsHierarchy.Items, (int)Column.VALUE, "Value");
      Load(p_path);
      m_dgv.Refresh();
    }

    bool Load(string p_path)
    {
      XmlTextReader reader = new XmlTextReader(p_path);
      bool insideString = false;
      Int32 insideCategory = 0;
      HierarchyItemsCollection l_items = m_dgv.RowsHierarchy.Items;
      uint id = 0;

      while ((reader.Read()))
      {
        switch (reader.NodeType)
        {
          case XmlNodeType.Element:
            switch (reader.Name)
            {
              case "category":
                l_items = m_dgv.SetDimension(Dimension.ROW, l_items, ++id, reader.GetAttribute("name")).Items;
                insideCategory += 1;
                break;
              case "string":
                m_dgv.SetDimension(Dimension.ROW, l_items, ++id, reader.GetAttribute("name"));
                insideString = true;
                break;
              default:
                return false;
            }
            break;
          case XmlNodeType.EndElement:
            switch (reader.Name)
            {
              case "category":
                if (l_items.OwnerItem.ParentItem != null)
                  l_items = l_items.OwnerItem.ParentItem.Items;
                insideCategory -= 1;
                break;
              case "string":
                if (insideString == false)
                  return false;
                insideString = false;
                break;
              default:
                return false;
            }
            break;
          case XmlNodeType.Text:
            if (insideString == false)
              continue;
            m_dgv.FillField(id, (int)Column.VALUE, reader.Value);
            break;
        }
      }
      reader.Close();
      return true;
    }
  }
}
