using System;

public enum ServerMessage : byte
{
    SMSG_TEST_ANSWER = 0x00,
    SMSG_AUTH_RESPONSE = 0x01,
    SMSG_COMPUTE_ANSWER = 0x02,
    OpcodeMax
}