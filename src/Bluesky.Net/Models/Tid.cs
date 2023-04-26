namespace Bluesky.Net.Models;

using Internals;
using System;

public struct Tid
{
    private readonly string _value;

    public Tid(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (!Parser.IsValidTid(value))
        {
            throw new ArgumentException("Invalid Tid", nameof(value));
        }

        _value = value;
    }

    public Tid(ulong micros, ushort clockId)
    {
        micros &= 0x001FFFFFFFFFFFFF;
        // 10 bits of clock ID
        ulong shiftedClockId = (ushort)(clockId & 0x03FF);
        ulong value = (micros << 10) | shiftedClockId;
        byte[] intBytes = BitConverter.GetBytes(value);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(intBytes);
        }

        string result = Base32Sort.Encode(intBytes);
        _value = $"{result[..4]}-{result[4..7]}-{result[7..11]}-{result[11..13]}";
    }

    public override string ToString() => _value;

    public static implicit operator string(Tid value) => value._value;
}
