namespace Bluesky.Net.Internals;

internal static class Functions {
    internal static string FormatValue<T>(T value) => $"{typeof(T).FullName}: {value?.ToString()}";
}