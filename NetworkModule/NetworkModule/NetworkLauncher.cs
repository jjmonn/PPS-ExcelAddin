using System;
using System.Threading;

public class NetworkLauncher
{
    Thread m_thread;
    NetworkManager m_netMgr;
    ClientState m_state;
    Action m_diconnectCallback;

    public NetworkLauncher()
    {
        m_state = ClientState.not_connected;
        m_netMgr = NetworkManager.GetInstance();
    }

    public bool Launch(string p_ip, UInt16 p_port, Action p_disconnectCallback = null)
    {
        if (m_netMgr.Connect(p_ip, p_port) == true)
        {
            m_state = ClientState.running;
            m_thread = new Thread(new ThreadStart(HandleLoop));
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
      m_netMgr.Disconnect();
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
            if (m_netMgr.HandlePacket() == false)
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