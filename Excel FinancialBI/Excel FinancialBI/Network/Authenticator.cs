using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Network
{
  using Utils;

  class Authenticator
  {
    static Authenticator s_instance = new Authenticator();
    public static Authenticator Instance { get { return (s_instance); } }
    public static string Username { get; private set; }
    static string m_password;
    public const string FBIVersionId = "1.0.1";
    public static event AuthenticationEventHandler AuthenticationEvent;
    public delegate void AuthenticationEventHandler(ErrorMessage p_status);

    Authenticator()
    {
      NetworkManager.SetCallback((UInt16)ServerMessage.SMSG_AUTH_REQUEST_ANSWER, OnAuthRequestAnswer);
      NetworkManager.SetCallback((UInt16)ServerMessage.SMSG_AUTH_ANSWER, OnAuthAnswer);
    }
    
    public void AskAuthentication(string p_username, string p_password)
    {
      ByteBuffer l_packet = new ByteBuffer((UInt16)ClientMessage.CMSG_AUTH_REQUEST);
      Username = p_username;
      m_password = Hash.GetSHA1(p_password + p_username);
      l_packet.WriteString(FBIVersionId);
      l_packet.Release();
      NetworkManager.Send(l_packet);
    }

    static void OnAuthRequestAnswer(ByteBuffer p_packet)
    {
      string l_authToken;
      ByteBuffer l_answer = new ByteBuffer((UInt16)ClientMessage.CMSG_AUTHENTIFICATION);

      if (p_packet.GetError() != ErrorMessage.SUCCESS)
      {
        if (AuthenticationEvent != null)
          AuthenticationEvent(p_packet.GetError());
        return;
      }
      l_authToken = p_packet.ReadString();
      l_answer.WriteString(Username);
      l_answer.WriteString(Hash.GetSHA1(m_password + l_authToken));
      l_answer.Release();

      NetworkManager.Send(l_answer);
    }

    static void OnAuthAnswer(ByteBuffer p_packet)
    {
      if (AuthenticationEvent != null)
        AuthenticationEvent(p_packet.GetError());
    } 
  }
}
