namespace Bluesky.Net.Json;

using Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DidJsonConverter : JsonConverter<Did>
{
    public override Did? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (value is null)
        {
            return default;
        }
        
        try
        {
            return new Did(value);
        }
        catch (Exception)
        {
            return default;
        }
    }

    public override void Write(Utf8JsonWriter writer, Did? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}
