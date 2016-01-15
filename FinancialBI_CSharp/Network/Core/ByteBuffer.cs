using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ionic.Zlib;

public class ByteBuffer
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

  const long NbArray = 8;
  const long MaxSizeArray = 256 * 1024 * 1024;
  const long LimitTotalSize = NbArray * MaxSizeArray;
  static Int32 m_nextId = 1;
  Int32 m_requestId = -1;
  Header m_header;
  long m_sizeHeader;
  byte[][] m_storage = new byte[NbArray][];
  long m_position = 0;
  long m_length = 0;
  long m_capacity = 0;

  void SetCapacity(long p_newCapacity)
  {
    // TODO: manage case with inferior capacity

    if (p_newCapacity >= LimitTotalSize)
      throw new ArgumentOutOfRangeException("p_newCapacity", "Try to set a capacity of " + p_newCapacity + ", max allowed capacity is " + LimitTotalSize + " bytes");
    long startArray = (m_capacity + 1) / MaxSizeArray;
    long endArray = p_newCapacity / MaxSizeArray;

    for (long currentArray = startArray; currentArray <= endArray; ++currentArray)
    {
      long sizeCurrent = p_newCapacity - startArray * MaxSizeArray;
      sizeCurrent = (sizeCurrent > MaxSizeArray) ? MaxSizeArray : sizeCurrent;

      byte[] newArray = new byte[sizeCurrent];
      if (m_storage[currentArray] != null)
        m_storage[currentArray].CopyTo(newArray, 0);
      m_storage[currentArray] = newArray;
    }
    m_capacity = p_newCapacity;
  }

  public ByteBuffer(ushort p_opcode)
  {
    SetCapacity(4096);
    m_header.opcode = p_opcode;
    m_header.error = 0;
    m_header.isCompressed = false;
    WriteHeader();
    m_sizeHeader = m_length;
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
    if (p_header.isCompressed && p_header.payloadSize > MaxSizeArray)
      throw new ArgumentOutOfRangeException("p_buffer", "Maximum compressed ByteBuffer size is " + LimitTotalSize + " bytes");
    SetCapacity(p_header.realPayloadSize + 4096);
    m_header = p_header;
    WriteHeader();
    m_sizeHeader = m_length;
    Write(p_buffer, 0, (int)m_header.payloadSize);
    if (m_header.isCompressed)
      Uncompress(m_header.realPayloadSize);
    m_header.isCompressed = false;
    m_position = m_sizeHeader;
  }

  public ByteBuffer Clone()
  {
    ByteBuffer clone = new ByteBuffer();

    clone.m_header = m_header;
    clone.m_sizeHeader = m_sizeHeader;
    clone.m_requestId = m_requestId;
    for (int i = 0; i < NbArray; ++i)
    {
      if (m_storage[i] != null)
      {
        clone.m_storage[i] = new byte[m_storage[i].Length];

        m_storage[i].CopyTo(clone.m_storage[i], 0);
      }
    }
    clone.m_position = m_position;
    clone.m_capacity = m_capacity;
    clone.m_length = m_length;
    return (clone);
  }

  public void Release()
  {
    long prevPos = m_position;

    m_header.payloadSize = (Int32)(m_length - m_sizeHeader);
    m_header.realPayloadSize = m_header.payloadSize;
    m_position = 0;
    WriteHeader();
    m_position = prevPos;
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

  public void Write(byte[] p_array, long p_offset, long p_size)
  {
    if (p_offset + p_size + m_position > m_capacity)
    {
      if (p_offset + p_size + m_position > m_capacity * 2)
        SetCapacity(p_offset + p_size + m_position);
      else if (m_capacity * 2 > LimitTotalSize)
        SetCapacity(LimitTotalSize - 1);
      else
        SetCapacity(m_capacity * 2);
    }
    for (long i = 0; i < p_size; ++i)
    {
      long array = m_position / MaxSizeArray;
      long pos = m_position - array * MaxSizeArray;

      m_storage[array][pos] = p_array[i + p_offset];
      m_position++;
    }
    if (m_position > m_length)
      m_length = m_position;
  }

  public void WriteUint8(byte value)
  {
    Write(BitConverter.GetBytes(value), 0, 1);
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
    WriteUint8(0x00);
  }

  public bool ReadBool()
  {
    return ReadUint8() != 0 ? true : false;
  }

  void Read(byte[] p_array, long p_offset, long p_size)
  {
    for (long i = 0; i < p_size; ++i)
    {
      if (m_position >= m_length)
        return;
      long array = m_position / MaxSizeArray;
      long pos = m_position - array * MaxSizeArray;

      p_array[i + p_offset] = m_storage[array][pos];
      m_position++;
    }
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
    long l_savePos = m_position;

    m_position = p_pos;
    Write(BitConverter.GetBytes(p_value), 0, sizeof(UInt32));
    m_position = l_savePos;
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

  public byte[] GetBuffer()
  {
    return (m_storage[0]);
  }

  public long Length
  {
    get { return (m_length); }
  }

  public void Uncompress(int p_realSize)
  {
    byte[] uncompressed;
    int size;
    int readed = 0;

    byte[] array = m_storage[0];
    byte[] resultArray = new byte[m_header.payloadSize];

    for (int i = 0; i < m_header.payloadSize; ++i)
      resultArray[i] = array[m_sizeHeader + i];
    m_position = m_sizeHeader;
    m_length = m_sizeHeader;

    ZlibStream stream = new ZlibStream(new MemoryStream(resultArray), CompressionMode.Decompress);
    Array.Clear(GetBuffer(), (int)m_sizeHeader, GetBuffer().Length - (int)m_sizeHeader);

    try
    {
      while (readed < p_realSize)
      {
        uncompressed = new byte[4096];
        size = stream.Read(uncompressed, 0, 4096);

        Write(uncompressed, 0, size);
        readed += size;
      }
    }
    catch (ArgumentOutOfRangeException e)
    {
      System.Diagnostics.Debug.WriteLine("ByteBuffer.Uncompress: " + e.Message);
      return;
    }
  }
}
