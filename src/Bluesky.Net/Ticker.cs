namespace Bluesky.Net;

using System;
using System.Security.Cryptography;

public struct Ticker
{
    private static readonly RandomNumberGenerator Generator = RandomNumberGenerator.Create();
    private static readonly double MicroSecPerTick = 1000000D / System.Diagnostics.Stopwatch.Frequency;

    public Ticker()
    {
        LastTimestamp = 0;
        var bytes = new byte[sizeof(ushort)];
        Generator.GetNonZeroBytes(bytes);
        ClockId = BitConverter.ToUInt16(bytes);
        NextTid();
    }

    public Tid NextTid()
    {
        ulong now = Convert.ToUInt64((DateTimeOffset.UtcNow - DateTimeOffset.UnixEpoch).TotalMicroseconds)
                    & 0x001FFFFFFFFFFFFF;
        
        if (now > LastTimestamp)
        {
            LastTimestamp = now;
        }
        else
        {
            LastTimestamp += 1;
        }

        return new Tid(LastTimestamp, ClockId);
    }

    public ulong LastTimestamp { get; private set; }

    public ushort ClockId { get; }
}
