namespace Bluesky.Net.Json;

using Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class AtUriJsonConverter : JsonConverter<AtUri>
{
    public override AtUri? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (value is null)
        {
            return default;
        }
        
        try
        {
            return new AtUri(value);
        }
        catch (Exception)
        {
            return default;
        }
    }

    public override void Write(Utf8JsonWriter writer, AtUri? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}
