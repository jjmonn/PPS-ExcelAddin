using System;

public enum ClientState : byte
{
    running = 0x00,
    shuting_down = 0x01,
    not_connected = 0x02
}
