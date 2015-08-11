using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ionic.Zlib;

public class ByteBuffer : MemoryStream
{
    static Int32 m_nextId = 1;
    Int32 m_requestId = -1;

    public ByteBuffer(ushort p_opcode)
    {
        this.WriteUint32(0);
        this.WriteUint16(p_opcode);
    }

    public ByteBuffer() { }

    public void Release()
    {
        this.ReplaceUint32((UInt32)this.Length - 4, 0);
    }

    public void WriteUint8(Byte value)
    {
        WriteByte(value);
    }

    public void WriteUint16(UInt16 value)
    {
        Write(BitConverter.GetBytes(value), 0, 2);
    }

    public void WriteUint32(UInt32 value)
    {
        Write(BitConverter.GetBytes(value), 0, 4);
    }

    public void WriteInt32(Int32 value)
    {
        Write(BitConverter.GetBytes(value), 0, 4);
    }

    public void WriteUint64(UInt64 value)
    {
        Write(BitConverter.GetBytes(value), 0, 8);
    }

    public void WriteFloat(float value)
    {
        Write(BitConverter.GetBytes(value), 0, 4);
    }

    public void WriteDouble(double value)
    {
        Write(BitConverter.GetBytes(value), 0, sizeof(double));
    }

    public void WriteString(String value)
    {
        byte[] tmp = System.Text.Encoding.UTF8.GetBytes(value);
        Write(tmp, 0, tmp.Length);
        WriteByte(0x00);
    }

    public bool ReadBool()
    {
        return ReadUint8() != 0 ? true : false;
    }
    public Byte ReadUint8()
    {
        byte[] tmp = new byte[1];
        Read(tmp, 0, 1);

        return tmp[0];
    }

    public UInt16 ReadUint16()
    {
        byte[] tmp = new byte[2];
        Read(tmp, 0, 2);

        return BitConverter.ToUInt16(tmp, 0);
    }

    public UInt32 ReadUint32()
    {
        byte[] tmp = new byte[4];
        Read(tmp, 0, 4);

        return BitConverter.ToUInt32(tmp, 0);
    }

    public Int32 ReadInt32()
    {
        byte[] tmp = new byte[4];
        Read(tmp, 0, 4);

        return BitConverter.ToInt32(tmp, 0);
    }

    public UInt64 ReadUint64()
    {
        byte[] tmp = new byte[8];
        Read(tmp, 0, 8);

        return BitConverter.ToUInt64(tmp, 0);
    }

    public float ReadFloat()
    {
        byte[] tmp = new byte[4];
        Read(tmp, 0, 4);

        return BitConverter.ToSingle(tmp, 0);
    }

    public double ReadDouble()
    {
        byte[] tmp = new byte[sizeof(double)];
        Read(tmp, 0, sizeof(double));

        return BitConverter.ToDouble(tmp, 0);
    }

    public String ReadString()
    {
        List<Byte> l_Bytes = new List<Byte>();
        Byte l_Byte;

        do
        {
            l_Byte = ReadUint8();
            l_Bytes.Add(l_Byte);
        }
        while (l_Byte != 0x00);
        l_Bytes.RemoveAt(l_Bytes.Count - 1);

        return System.Text.Encoding.UTF8.GetString(l_Bytes.ToArray());
    }

    public void ReplaceUint8(byte p_value, int p_pos)
    {
        Write(BitConverter.GetBytes(p_value), p_pos, sizeof(byte));
    }

    public void ReplaceUint16(UInt16 p_value, int p_pos)
    {
        Write(BitConverter.GetBytes(p_value), p_pos, sizeof(UInt16));
    }

    public void ReplaceUint32(UInt32 p_value, int p_pos)
    {
        long l_savePos = Position;

        Position = p_pos;
        Write(BitConverter.GetBytes(p_value), 0, sizeof(UInt32));
        Position = l_savePos;
    }

    public Int32 AssignRequestId()
    {
        WriteInt32(m_nextId);
        m_nextId++;
        return (m_nextId - 1);
    }

    public Int32 GetRequestId()
    {
        if (m_requestId == -1)
            m_requestId = this.ReadInt32();
        return (m_requestId);
    }

    public void Uncompress(int p_realSize)
    {
        ZlibStream stream = new ZlibStream(new MemoryStream(this.ToArray()), CompressionMode.Decompress);
        byte[] uncompressed = new byte[p_realSize];
        int size = stream.Read(uncompressed, 0, p_realSize);

        byte[] buffer = GetBuffer();
        Array.Clear(buffer, 0, buffer.Length);
        Position = 0;
        SetLength(0);
        Write(uncompressed, 0, size);
    }
}
