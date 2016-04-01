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
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace FBI.Network
{
  public static class NetworkManager
  {
    static Socket m_Sock;
    static SslStream m_StreamSSL;
    static List<Action<ByteBuffer>>[] m_callback = new List<Action<ByteBuffer>>[(byte)ServerMessage.OpcodeMax];

    public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
      return true;
      //if (sslPolicyErrors == SslPolicyErrors.None)
      //  return true;

      //Debug.WriteLine("Certificate error: " + sslPolicyErrors);
      //return false;
    }

    public static void SetCallback(UInt16 p_opcodeId, Action<ByteBuffer> p_newCallback)
    {
      if (p_opcodeId == 0 || p_opcodeId > (byte)ServerMessage.OpcodeMax)
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

    public static List<Action<ByteBuffer>> GetCallback(UInt16 p_opcodeId)
    {
      if (p_opcodeId < (byte)ServerMessage.OpcodeMax)
        return (m_callback[p_opcodeId]);
      return (null);
    }

    public static void RemoveCallback(UInt16 p_opcode, Action<ByteBuffer> p_oldCallback)
    {
      if (p_opcode == 0 || p_opcode > (byte)ServerMessage.OpcodeMax)
        return;
      if (m_callback[p_opcode] != null)
        m_callback[p_opcode].Remove(p_oldCallback);
    }

    public static bool Connect(String p_ip, int p_port)
    {
      Debug.WriteLine("Connecting to server...");
      try
      {
        IPHostEntry hostEntry = Dns.GetHostByName(p_ip);
        foreach (IPAddress address in hostEntry.AddressList)
        {
          IPEndPoint ipe = new IPEndPoint(address, p_port);

          m_Sock = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
          m_Sock.Connect(ipe);
          if (m_Sock.Connected)
            break;
        }
      }
      catch (Exception e)
      {
        Debug.WriteLine("Server unavailable: " + e.Message);
        return (false);
      }
      if (m_Sock == null)
      {
        Debug.WriteLine("No valid entry point found.");
        return (false);
      }
      try
      {
        m_StreamSSL = new SslStream(new NetworkStream(m_Sock), false,
          new RemoteCertificateValidationCallback(ValidateServerCertificate),
          null);
        m_StreamSSL.AuthenticateAsClient("pps");
      }
      catch (Exception e)
      {
        Debug.WriteLine(e.Message);
        if (e.InnerException != null)
        {
          Debug.WriteLine(e.InnerException.Message);
        }
        Debug.WriteLine("SSL authentication failed");
        return (false);
      }

      byte[] header = System.Text.Encoding.UTF8.GetBytes("NOWEBSOCKET");

      m_StreamSSL.Write(header, 0, header.Length);
      m_StreamSSL.Flush();

      byte[] answer = new byte[5];
      int ret = m_StreamSSL.Read(answer, 0, answer.Length);

      if (ret < answer.Length)
        return (false);
      return (System.Text.Encoding.UTF8.GetString(answer) == "READY");
    }

    public static bool Send(ByteBuffer p_data)
    {
      if (m_Sock.Connected == false)
        return false;
      try
      {
        m_StreamSSL.Write(p_data.GetBuffer(), 0, (int)p_data.Length);
        m_StreamSSL.Flush();
        return (true);
      }
      catch (Exception e)
      {
        m_Sock.Close();
        System.Diagnostics.Debug.WriteLine(e.Message);
        return (false);
      }
    }

    static ByteBuffer.Header FillHeader(byte[] p_buffer)
    {
      ByteBuffer.Header l_header = new ByteBuffer.Header();

      l_header.payloadSize = BitConverter.ToInt32(p_buffer, 0);
      l_header.realPayloadSize = BitConverter.ToInt32(p_buffer, 4);
      l_header.specialId = BitConverter.ToUInt32(p_buffer, 8);
      l_header.isCompressed = p_buffer[12] == 1;
      l_header.error = BitConverter.ToUInt32(p_buffer, 13);
      l_header.opcode = BitConverter.ToUInt16(p_buffer, 17);
      return (l_header);
    }

    public static ByteBuffer Receive()
    {
      try
      {
        byte[] l_buffer = new byte[19];
        ByteBuffer l_byteBuffer;
        ByteBuffer.Header l_header;
        int l_size = 0;
        int l_byteReaded = 0;

        while (l_size < 19)
        {
          int ret = m_StreamSSL.Read(l_buffer, l_size, 19 - l_size);
          l_size += ret;
          if (ret <= 0)
            return (null);
        }
        l_header = FillHeader(l_buffer);
        l_buffer = new byte[l_header.payloadSize];

        do
        {
          int sock = m_StreamSSL.Read(l_buffer, l_byteReaded, l_header.payloadSize - l_byteReaded);

          if (sock == 0)
            break;
          l_byteReaded += sock;
        }
        while (l_byteReaded != l_header.payloadSize);

        l_byteBuffer = new ByteBuffer(l_buffer, l_header);
        return (l_byteBuffer);
      }
      catch (Exception e)
      {
        Debug.WriteLine(e.Message);
        Disconnect();
        return null;
      }
    }

    public static void Disconnect()
    {
      m_StreamSSL.Close();
      m_Sock.Close();
    }

    public static bool IsConnected()
    {
      if (m_Sock == null)
        return (false);
      try
      {
        return !(m_Sock.Poll(1, SelectMode.SelectRead) && m_Sock.Available == 0);
      }
      catch (Exception) { return false; }
    }

    public static bool HandlePacket()
    {
      if (IsConnected() == false)
      {
        Debug.WriteLine("Disconnected by remote server");
        return (false);
      }
      UInt16 l_opcode;
      ByteBuffer l_packet;
      List<Action<ByteBuffer>> l_callback;

      if ((l_packet = Receive()) == null)
        return (true);
      l_opcode = l_packet.GetOpcode();
      if ((l_callback = GetCallback(l_opcode)) == null)
        System.Diagnostics.Debug.WriteLine("Undefined packet type " + (ServerMessage)(l_opcode) + " (" + l_opcode + ")");
      else
      {
        System.Diagnostics.Debug.WriteLine("Receive defined " + (ServerMessage)(l_opcode) + " (" + l_opcode + ")");
        for (int i = 0; i < l_callback.Count; i++)
          l_callback.ElementAt(i)(l_packet.Clone());
      }
      return (true);
    }
  }
}