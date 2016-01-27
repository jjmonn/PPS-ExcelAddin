using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBI_ServerTest
{
  class Authenticator
  {
    string m_FBIVersion;
    string m_username;
    string m_password;
    Action<NetworkManager> m_callback;
    NetworkManager m_netMgr;
    static int m_answer = 0;

    public Authenticator(NetworkManager p_netMgr)
    {
      m_netMgr = p_netMgr;
    }

    public void RequestAuthentication(string p_FBIVersion, string p_username, string p_password, Action<NetworkManager> p_callback)
    {
      m_FBIVersion = p_FBIVersion;
      m_username = p_username;
      m_password = p_password;
      m_callback = p_callback;

      ByteBuffer packet = new ByteBuffer((UInt16)ClientMessage.CMSG_AUTH_REQUEST);

      packet.WriteString(m_FBIVersion);
      packet.Release();

      m_netMgr.SetCallback((UInt16)ServerMessage.SMSG_AUTH_REQUEST_ANSWER, onSMSG_AUTH_REQUEST_ANSWER);
      m_netMgr.Send(packet);
    }

    void onSMSG_AUTH_REQUEST_ANSWER(ByteBuffer p_packet)
    {
      System.Diagnostics.Debug.WriteLine(m_answer++);
      if (p_packet.GetError() != (uint)ErrorMessage.SUCCESS)
        return;
      string authToken = p_packet.ReadString();
      m_netMgr.SetCallback((UInt16)ServerMessage.SMSG_AUTH_ANSWER, onSMSG_AUTH_ANSWER);

      ByteBuffer answer = new ByteBuffer((UInt16)ClientMessage.CMSG_AUTHENTIFICATION);
      answer.WriteString(m_username);
      answer.WriteString(Utils.GetSHA1(Utils.GetSHA1(m_username + m_password) + authToken));
      answer.Release();

      System.Diagnostics.Debug.WriteLine("Send CMSG_AUTHENTIFICATION");
      m_netMgr.Send(answer);
    }

    void onSMSG_AUTH_ANSWER(ByteBuffer p_packet)
    {
      if (p_packet.GetError() == (UInt16)ErrorMessage.SUCCESS)
        m_callback(m_netMgr);
    }
  }
}
