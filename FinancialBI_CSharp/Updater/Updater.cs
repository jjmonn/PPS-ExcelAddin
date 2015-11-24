using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.IO;

public class Updater
{
  string m_hostPath;
  string m_releaseName = "release.bin";
  string m_md5Name = "release.md5";
  string m_remoteMD5;

  public Updater(string p_hostPath)
  {
    m_hostPath = p_hostPath;
  }

  public bool CheckForUpdate()
  {
    using (WebClient client = new WebClient())
    {
      try
      {
        m_remoteMD5 = client.DownloadString(m_hostPath + m_md5Name);
        string localMD5 = Hash.GetMD5();

        return (m_remoteMD5 != localMD5);
      }
      catch (WebException e)
      {
        Debug.WriteLine("Unable to download md5: " + e.Message);
        return (false);
      }
    }
  }

  public bool DownloadUpdate()
  {
    if (m_remoteMD5 == "")
    {
      if (CheckForUpdate() == false)
        return (false);
    }
    using (WebClient client = new WebClient())
    {
      int nbTry = 5;

      try
      {
        do
        {
          nbTry--;
          client.DownloadFile(m_hostPath + m_releaseName, m_releaseName);
          try
          {
            FileStream stream = new FileStream(m_releaseName, FileMode.Open, FileAccess.Read);

            if (Hash.GetMD5(stream) == Hash.GetMD5())
              return (true);
            Debug.WriteLine("Update corrupted: " + nbTry + " try more");
          }
          catch (Exception e)
          {
            Debug.WriteLine("Unable to open update: " + e.Message);
            return (false);
          }
        }
        while (nbTry > 0);
        Debug.WriteLine("Unable to download update");
        return (false);
      }
      catch (WebException e)
      {
        Debug.WriteLine("Unable to download update: " + e.Message);
        return (false);
      }
    }
  }

  public bool LaunchUpdate()
  {
    using (Process releaseExe = Process.Start(m_releaseName))
    {
      releaseExe.WaitForExit();
      return (releaseExe.ExitCode == 0);
    }
  }
}
