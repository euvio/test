using System;
using System.Collections.Generic;

public sealed class CrcGenerator
{
    public static (byte low, byte high) GetCrc16Modbus(IList<byte> bytes)
    {
        return GetCrc16Modbus(bytes, 0, bytes.Count);
    }

    public static (byte low, byte high) GetCrc16Modbus(IList<byte> bytes, int length)
    {
        return GetCrc16Modbus(bytes, 0, length);
    }

    public static (byte low, byte high) GetCrc16Modbus(IList<byte> bytes, int startIndex, int length)
    {
        ushort wCrc = 0xFFFF;
        for (int i = startIndex; i < startIndex + length; i++)
        {
            wCrc ^= Convert.ToUInt16(bytes[i]);
            for (var j = 0; j < 8; j++)
            {
                if ((wCrc & 0x0001) == 1)
                {
                    wCrc >>= 1;
                    wCrc ^= 0xA001;//异或多项式
                }
                else
                {
                    wCrc >>= 1;
                }
            }
        }
        return ((byte)(wCrc & 0x00FF), (byte)((wCrc & 0xFF00) >> 8));
    }
}

public class Program
{
    public static void Main()
    {
        var bytes = CrcGenerator.GetCrc16Modbus(new byte[] { 0x9e, 0x03, 0x08, 0x99, 0x00, 0x2d }, 0, 3);
        Console.WriteLine(bytes.low.ToString("X2"));
        Console.WriteLine(bytes.high.ToString("X2"));
        Console.Read();
    }
}