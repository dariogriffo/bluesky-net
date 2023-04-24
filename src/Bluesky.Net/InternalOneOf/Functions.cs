namespace Bluesky.Net.InternalOneOf;

internal static class Functions {
    internal static string FormatValue<T>(T value) => $"{typeof(T).FullName}: {value?.ToString()}";
}