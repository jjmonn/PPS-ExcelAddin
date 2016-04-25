using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIBlend.WinForms.Controls;

namespace FBI.Forms
{
  class FlatFBITreeView : AFbiTreeView
  {
    public override bool Loaded { get; protected set; }
    SafeDictionary<Int32, string> m_items;
    private static readonly string ERR_GENERATE = "[FlatFBITreeView] Cannot generate vTreeView. Either the MultiIndexDictionary is null or incorrect";
    public Action LoadFunction { get; set; }

    public FlatFBITreeView(SafeDictionary<Int32, string> p_items, bool p_allowDragAndDrop = false) : base(p_allowDragAndDrop)
    {
      Loaded = false;
      m_items = p_items;
      if (Load() == false)
        throw new Exception(ERR_GENERATE);
    }

    public void SetItems(SafeDictionary<Int32, string> p_items)
    {
      m_items = p_items;
    }

    public override bool Load()
    {
      Nodes.Clear();
      if (LoadFunction != null)
        LoadFunction();
      if (m_items == null)
        return (Loaded = false);
      foreach (KeyValuePair<Int32, string> l_pair in m_items)
      {
        vTreeNode l_node = new vTreeNode();

        l_node.Text = l_pair.Value;
        l_node.Value = (UInt32)l_pair.Key;
        Nodes.Add(l_node);
      }
      return (true);
    }
  };
}
