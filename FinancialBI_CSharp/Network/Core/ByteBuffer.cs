using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ionic.Zlib;

public class ByteBuffer : MemoryStream
{
  public struct Header
  {
    public Int32 payloadSize;
    public Int32 realPayloadSize;
    public UInt32 specialId;
    public bool isCompressed;
    public UInt32 error;
    public UInt16 opcode;
  }

  static Int32 m_nextId = 1;
  Int32 m_requestId = -1;
  Header m_header;
  long m_sizeHeader;

  public ByteBuffer(ushort p_opcode)
  {
    m_header.opcode = p_opcode;
    m_header.error = 0;
    m_header.isCompressed = false;
    WriteHeader();
    m_sizeHeader = Length;
  }

  public UInt32 GetError()
  {
    ErrorMessage error = (ErrorMessage)m_header.error;
    ServerMessage opcode = (ServerMessage)m_header.opcode;

    System.Diagnostics.Debug.WriteLine(opcode.ToString() + "(" + (UInt32)(opcode) + ") : " + error.ToString());
    return m_header.error;
  }
  public ByteBuffer() { }

  public UInt16 GetOpcode() { return (m_header.opcode); }

  public ByteBuffer(byte[] p_buffer, Header p_header)
  {
    m_header = p_header;
    WriteHeader();
    m_sizeHeader = Length;
    Write(p_buffer, 0, (int)m_header.payloadSize);
    if (m_header.isCompressed)
      Uncompress(m_header.realPayloadSize);
    m_header.isCompressed = false;
    Position = m_sizeHeader;
  }

  public ByteBuffer Clone()
  {
    ByteBuffer clone = new ByteBuffer();
    long pos = Position;

    clone.m_header = m_header;
    clone.m_sizeHeader = m_sizeHeader;
    clone.m_requestId = m_requestId;
    Position = 0;
    CopyTo(clone);
    clone.Position = pos;
    Position = pos;
    return (clone);
  }

  public void Release()
  {
    long prevPos = Position;

    m_header.payloadSize = (Int32)(Length - m_sizeHeader);
    m_header.realPayloadSize = m_header.payloadSize;
    Position = 0;
    WriteHeader();
    Position = prevPos;
  }

  private void WriteHeader()
  {
    WriteInt32(m_header.payloadSize);
    WriteInt32(m_header.realPayloadSize);
    WriteUint32(m_header.specialId);
    WriteBool(m_header.isCompressed);
    WriteUint32(m_header.error);
    WriteUint16(m_header.opcode);
  }

  public void WriteUint8(Byte value)
  {
    WriteByte(value);
  }

  public void WriteBool(bool value)
  {
    Write(BitConverter.GetBytes(value), 0, 1);
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
    if (value != null)
    {
      byte[] tmp = System.Text.Encoding.UTF8.GetBytes(value);
      Write(tmp, 0, tmp.Length);
    }
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
    int count = 0;

    do
    {
      l_Byte = ReadUint8();
      l_Bytes.Add(l_Byte);
      count++;
    }
    while (l_Byte != 0x00);
    l_Bytes.RemoveAt(l_Bytes.Count - 1);

    //  System.Diagnostics.Debug.WriteLine("Read " + count + " characters for string \"" + System.Text.Encoding.UTF8.GetString(l_Bytes.ToArray()) + "\"");
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
    byte[] array = this.ToArray();
    byte[] resultArray = new byte[m_header.payloadSize];

    for (int i = 0; i < m_header.payloadSize; ++i)
      resultArray[i] = array[m_sizeHeader + i];
    ZlibStream stream = new ZlibStream(new MemoryStream(resultArray), CompressionMode.Decompress);
    byte[] uncompressed = new byte[p_realSize];
    int size = stream.Read(uncompressed, 0, p_realSize);

    byte[] buffer = GetBuffer();
    Array.Clear(buffer, (int)m_sizeHeader, buffer.Length - (int)m_sizeHeader);
    Position = m_sizeHeader;
    SetLength(m_sizeHeader);
    Write(uncompressed, 0, size);
  }
}
