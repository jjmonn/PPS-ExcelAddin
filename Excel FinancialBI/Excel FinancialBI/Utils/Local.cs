using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Xml;
using System.IO;

namespace FBI.Utils
{
  public class Local
  {

    private static SafeDictionary<string, string> m_localDic = new SafeDictionary<string, string>();
    public static string GetValue(string p_name)
    {
      if (m_localDic.ContainsKey("FBI." + p_name))
        return (m_localDic["FBI." + p_name]);
      Debug.WriteLine("Local " + p_name + " never defined.");
      return ("[" + p_name + "]");
    }

    public static bool LoadLocalFile(string p_file)
    {
      try
      {
        if (LoadLocalFile_intern(ref p_file) == true)
          return true;
        m_localDic.Clear();
        return false;
      }
      catch (Exception ex)
      {
        m_localDic.Clear();
        System.Diagnostics.Debug.WriteLine(ex.Message);
        if ((ex.InnerException != null))
          System.Diagnostics.Debug.WriteLine(ex.InnerException.Message);
        return false;
      }
    }

    private static bool LoadLocalFile_intern(ref string p_file)
    {
      XmlTextReader reader = new XmlTextReader(new StringReader(p_file));
      string currentPath = "";
      bool insideString = false;
      Int32 insideCategory = 0;
      string currentStringName = "";

      while ((reader.Read()))
      {
        switch (reader.NodeType)
        {
          case XmlNodeType.Element:
            switch (reader.Name)
            {
              case "category":
                if (!string.IsNullOrEmpty(currentPath))
                  currentPath += ".";
                currentPath += reader.GetAttribute("name");
                insideCategory += 1;
                break;
              case "string":
                if (insideCategory <= 0)
                  Debug.WriteLine("Local file is bad formated: found opening <string> outside of any <category> at line " + reader.LineNumber + ". Abort loading.");
                currentStringName = reader.GetAttribute("name");
                insideString = true;
                break;
              default:
                Debug.WriteLine("Local file contain unknown tag at line " + reader.LineNumber + ". Abort loading.");
                return false;
            }
            break;
          case XmlNodeType.EndElement:
            switch (reader.Name)
            {
              case "category":
                if (insideCategory <= 0)
                  Debug.WriteLine("Local file is bad formated: closing tag </category> without matching <category> at line " + reader.LineNumber + ". Abort loading.");
                currentPath = RemoveLastPathElem(ref currentPath);
                insideCategory -= 1;
                break;
              case "string":
                if (insideString == false)
                {
                  Debug.WriteLine("Local file is bad formated: closing tag </string> without matching <string> at line " + reader.LineNumber + ". Abort loading.");
                  return false;
                }
                insideString = false;
                break;
              default:
                Debug.WriteLine("Local file contain unknown tag at line " + reader.LineNumber + ". Abort loading.");
                return false;
            }
            break;
          case XmlNodeType.Text:
            if (insideString == false)
              continue;
            SetLocalEntry(currentPath + "." + currentStringName, reader.Value);
            break;
        }
      }
      reader.Close();
      return true;
    }

    private static void SetLocalEntry(string p_path, string p_value)
    {
      if (m_localDic.ContainsKey(p_path))
      {
        m_localDic[p_path] = p_value;
        System.Diagnostics.Debug.WriteLine("Warning: " + p_path + " defined multiple times");
      }
      else
      {
        m_localDic.Add(p_path, p_value);
      }
    }

    private static string RemoveLastPathElem(ref string p_path)
    {
      Int32 newEnd = p_path.Length;
      string newPath = "";

      for (Int32 i = 0; i <= p_path.Length - 1; i++)
      {
        if ((p_path[p_path.Length - i - 1] == '.'))
        {
          newEnd = p_path.Length - i - 1;
        }
      }
      for (Int32 i = 0; i <= newEnd - 1; i++)
      {
        newPath += p_path[i];
      }
      return newPath;
    }

  }
}