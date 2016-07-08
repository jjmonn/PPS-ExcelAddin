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
using System.Xml.Linq;

namespace LocalsTool
{
  static class Program
  {
    static XDocument m_localStruct;
    static XDocument m_localContent;
    static XDocument m_localResult;

    static Stack<string> GetParents(XElement p_elem)
    {
      Stack<string> l_parents = new Stack<string>();

      if (p_elem.Parent != null)
      {
        XAttribute l_attribute = p_elem.Parent.Attribute("name");

        if (l_attribute != null)
          l_parents.Push(l_attribute.Value);
        l_parents = new Stack<string>(l_parents.Concat(GetParents(p_elem.Parent)));
      }
      return (l_parents);
    }

    static string GetValue(XElement p_elem, XDocument p_content)
    {
      Stack<string> l_parents = GetParents(p_elem);

      l_parents.Pop();
      return (GetElem(p_content.Root, l_parents, p_elem.Attribute("name").Value));
    }

    static string GetElem(XElement p_base, Stack<string> l_parents, string l_elem)
    {
      XElement l_result;

      if (l_parents.Count > 0)
      {
        string l_parentName = l_parents.Pop();

        l_result = p_base.Descendants("category")
        .FirstOrDefault(el => el.Attribute("name") != null &&
                           el.Attribute("name").Value == l_parentName);

        if (l_result == null)
          return ("");
        return (GetElem(l_result, l_parents, l_elem));
      }
      l_result = p_base.Descendants("string")
       .FirstOrDefault(el => el.Attribute("name") != null &&
                          el.Attribute("name").Value == l_elem);
      if (l_result != null)
        return (l_result.Value);
      return ("");
    }

    static XDocument LoadXML(string p_path)
    {
      XmlReader r = XmlReader.Create(p_path);
      while (r.NodeType != XmlNodeType.Element)
        r.Read();
      XDocument e = XDocument.Load(r);
      return e;
    }

    [STAThread]
    static void Main()
    {
      m_localStruct = LoadXML("french.xml");
      m_localContent = LoadXML("english.xml");

      foreach (XElement l_elem in m_localStruct.Descendants())
        if (l_elem.Name == "string")
        {
          string l_value = GetValue(l_elem, m_localContent);

          l_elem.Value = l_value;
        }
      m_localStruct.Save("english_result.xml");
    }
  }
}
