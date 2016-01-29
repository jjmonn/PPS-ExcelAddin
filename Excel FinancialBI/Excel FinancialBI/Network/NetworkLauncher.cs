using System;
using System.Threading;

namespace FBI.Network
{
  public class NetworkLauncher
  {
    Thread m_thread;
    ClientState m_state;
    Action m_diconnectCallback;

    public NetworkLauncher()
    {
      m_state = ClientState.not_connected;
    }

    public bool Launch(string p_ip, UInt16 p_port, Action p_disconnectCallback = null)
    {
      if (NetworkManager.Connect(p_ip, p_port) == true)
      {
        m_state = ClientState.running;
        m_thread = new Thread(new ThreadStart(HandleLoop));
        m_thread.Name = "Network Module";
        m_thread.Start();
        m_diconnectCallback = p_disconnectCallback;
        return (true);
      }
      else
        return (false);
    }

    public void Stop()
    {
      if (m_state != ClientState.running)
        return;
      NetworkManager.Disconnect();
      m_state = ClientState.shuting_down;
      if (m_thread != null)
        m_thread.Join();
    }

    void HandleLoop()
    {
      while (m_state == ClientState.running)
      {
        try
        {
          if (NetworkManager.HandlePacket() == false)
          {
            m_state = ClientState.not_connected;
            if (m_diconnectCallback != null)
              m_diconnectCallback();
          }
        }
        catch (OutOfMemoryException e)
        {
          System.Diagnostics.Debug.WriteLine(e.Message);
        }

        Thread.Sleep(2);
      }
    }

    ClientState GetState()
    {
      return (m_state);
    }
  }
}