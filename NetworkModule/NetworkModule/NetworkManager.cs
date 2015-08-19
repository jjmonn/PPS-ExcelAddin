using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Reflection;

public class NetworkManager
{
    TcpClient m_Sock;
    static NetworkManager s_networkMgr = null;
    List<Action<ByteBuffer>>[] m_callback;

    public NetworkManager()
    {
        m_Sock = new TcpClient();
        m_callback = new List<Action<ByteBuffer>>[(byte)ServerMessage.OpcodeMax];
    }

    public void SetCallback(UInt16 p_opcodeId, Action<ByteBuffer> p_newCallback)
    {
        if (p_opcodeId > (byte)ServerMessage.OpcodeMax)
            return;
        if (m_callback[p_opcodeId] == null)
            m_callback[p_opcodeId] = new List<Action<ByteBuffer>>();
        for (int i = 0; i < (byte)ServerMessage.OpcodeMax; i++)
        {
            if (m_callback[i] != null && m_callback[i].Contains(p_newCallback))
                return;
        }
        m_callback[p_opcodeId].Add(p_newCallback);
    }

    public List<Action<ByteBuffer>> GetCallback(UInt16 p_opcodeId)
    {
        if (p_opcodeId < (byte)ServerMessage.OpcodeMax)
            return (m_callback[p_opcodeId]);
        return (null);
    }

    public void RemoveCallback(int p_opcode, Action<ByteBuffer> p_oldCallback)
    {
        if (p_opcode > (byte)ServerMessage.OpcodeMax)
            return;
        if (m_callback[p_opcode] != null)
            m_callback[p_opcode].Remove(p_oldCallback);
    }

    public bool Connect(String p_ip, int p_port)
    {
        Console.WriteLine("Connecting to server...");
        try
        {
            m_Sock.Connect(p_ip, p_port);
        }
        catch
        {
            Console.WriteLine("Server unavailable.");
            return (false);
        }
        return (true);
    }

    public void Send(ByteBuffer p_data)
    {
        m_Sock.GetStream().Write(p_data.GetBuffer(), 0, (int)p_data.Length);
    }

    public ByteBuffer Receive()
    {
        byte[] l_buffer = new byte[8];
        ByteBuffer l_byteBuffer = new ByteBuffer();
        int l_size;
        int l_sizeBuffer;
        int l_realSize;
        int l_byteReaded = 0;

        l_size = m_Sock.GetStream().Read(l_buffer, 0, 8);
        if (l_size < 8)
        {
           // Console.WriteLine("Invalid packet received");
            System.Diagnostics.Debug.WriteLine("Invalid packet received");
             return (null);
        }
        l_sizeBuffer = l_buffer[3] << 24 | l_buffer[2] << 16 | l_buffer[1] << 8 | l_buffer[0];
        l_realSize = l_buffer[7] << 24 | l_buffer[6] << 16 | l_buffer[5] << 8 | l_buffer[4];
        l_buffer = new byte[l_sizeBuffer];

        Debug.WriteLine("Receive packet of size " + l_sizeBuffer);
        do
        {
            int sock = m_Sock.GetStream().Read(l_buffer, l_byteReaded, l_sizeBuffer - l_byteReaded);

            if (sock == 0)
                break;
            l_byteReaded += sock;
        }
        while (l_byteReaded != l_sizeBuffer);

        l_byteBuffer.Write(l_buffer, 0, l_byteReaded);
        l_byteBuffer.Uncompress(l_realSize);
        return (l_byteBuffer);
    }

    public void Disconnect()
    {
        m_Sock.GetStream().Close();
        m_Sock.Close();
    }

    public bool HandlePacket()
    {
        if (m_Sock.Connected == false)
        {
            Console.WriteLine("Disconnected by remote server");
            return (false);
        }
        if (m_Sock.Available > 0)
        {
            UInt16 l_opcode;
            ByteBuffer l_packet;
            List<Action<ByteBuffer>> l_callback;

            if ((l_packet = this.Receive()) == null)
                return (true);
            l_packet.Position = 0;
            l_opcode = l_packet.ReadUint16();
            if ((l_callback = this.GetCallback(l_opcode)) == null)
            {
               // Console.WriteLine("Undefined packet type " + l_opcode.ToString("X2"));
                System.Diagnostics.Debug.WriteLine("Undefined packet type " + l_opcode.ToString("X2"));
            }
            else
            {
               // Console.WriteLine("Receive defined " + l_opcode.ToString("X2"));
                System.Diagnostics.Debug.WriteLine("Receive defined " + l_opcode.ToString("X2"));
                for (int i = 0; i < l_callback.Count; i++)
                    l_callback.ElementAt(i)(l_packet);
            }
        }
        return (true);
    }

    static public NetworkManager GetInstance()
    {
        if (s_networkMgr == null)
            s_networkMgr = new NetworkManager();
        return (s_networkMgr);
    }

}