namespace Bluesky.Net.Internals;

using System;

internal static class ArgumentNullExceptionExtensions
{
    internal static T ThrowIfNull<T>(this T? t)
    {
        ArgumentNullException.ThrowIfNull(t);
        return t;
    }
}
