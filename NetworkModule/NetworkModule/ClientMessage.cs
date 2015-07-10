using System;

public enum ClientMessage : ushort
{
    CMSG_TEST = 0x00,
    CMSG_AUTH_REQUEST = 0x01,
    CMSG_COMPUTE_REQUEST = 0x02,
    OpcodeMax
}
