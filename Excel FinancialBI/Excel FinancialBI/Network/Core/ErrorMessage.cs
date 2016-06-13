namespace FBI.Network
{
  using Utils;

  public enum ErrorMessage
  {
    SUCCESS,
    NOT_FOUND,
    ALREADY_EXIST,
    SYSTEM,
    NOT_IMPLEMENTED,
    INVALID_ATTRIBUTE,
    PERMISSION_DENIED,
    DEPENDENT_PARENT,
    NAME_ALREADY_USED,
    SYNTAX,
    VERSION_MISMATCH,
    UNALLOWED_PROCESS,
    CIRCULAR_DEP
  };

  static class Error
  {
    static SafeDictionary<ErrorMessage, string> m_messageList = null;

    static void InitMessages()
    {
      m_messageList = new SafeDictionary<ErrorMessage, string>();

      m_messageList[ErrorMessage.SUCCESS] = Local.GetValue("general.error.success");
      m_messageList[ErrorMessage.NOT_FOUND] = Local.GetValue("general.error.not_found");
      m_messageList[ErrorMessage.ALREADY_EXIST] = Local.GetValue("general.error.already_exist");
      m_messageList[ErrorMessage.SYSTEM] = Local.GetValue("general.error.system");
      m_messageList[ErrorMessage.NOT_IMPLEMENTED] = Local.GetValue("general.error.not_implemented");
      m_messageList[ErrorMessage.INVALID_ATTRIBUTE] = Local.GetValue("general.error.invalid_attribute");
      m_messageList[ErrorMessage.PERMISSION_DENIED] = Local.GetValue("general.error.permission_denied");
      m_messageList[ErrorMessage.DEPENDENT_PARENT] = Local.GetValue("general.error.dependent_parent");
      m_messageList[ErrorMessage.NAME_ALREADY_USED] = Local.GetValue("general.error.success");
      m_messageList[ErrorMessage.VERSION_MISMATCH] = Local.GetValue("general.error.version_mismatch");
      m_messageList[ErrorMessage.CIRCULAR_DEP] = Local.GetValue("general.error.circular_dep");
    }

    public static string GetMessage(ErrorMessage p_error)
    {
      if (m_messageList == null)
        InitMessages();
      return (m_messageList[p_error]);
    }
  }

}