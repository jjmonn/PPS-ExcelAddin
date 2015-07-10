using System;
using System.Threading;

public class NetworkLauncher
{
    Thread m_thread;
    NetworkManager m_netMgr;
    ClientState m_state;

    public NetworkLauncher(ref ClientState p_state)
    {
        m_state = p_state;
        m_netMgr = NetworkManager.GetInstance();
    }

    public bool Launch(string p_ip, UInt16 p_port)
    {
        if (m_netMgr.Connect(p_ip, p_port) == true)
        {
            m_state = ClientState.running;
            m_thread = new Thread(new ThreadStart(HandleLoop));
            m_thread.Start();
            return (true);
        }
        else
            return (false);
    }

    public void Stop()
    {
        m_state = ClientState.shuting_down;
        if (m_thread != null)
            m_thread.Join();
    }

    void HandleLoop()
    {
        while (m_state == ClientState.running)
        {
            if (m_netMgr.HandlePacket() == false)
                Thread.Sleep(3000);
            Thread.Sleep(10);
        }
    }
}