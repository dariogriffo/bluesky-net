namespace Bluesky.Net.Internals;

public static class Base32Sort
{
    public static string Encode(byte[] bytes) {
        const string alphabet = "234567abcdefghijklmnopqrstuvwxyz";
        string output = "";
        for (int bitIndex = 0; bitIndex < bytes.Length * 8; bitIndex += 5) {
            int dualByte = bytes[bitIndex / 8] << 8;
            if (bitIndex / 8 + 1 < bytes.Length)
            {
                dualByte |= bytes[bitIndex / 8 + 1];
            }
            
            dualByte = 0x1f & (dualByte >> (16 - bitIndex % 8 - 5));
            output += alphabet[dualByte];
        }

        return output;
    }
}