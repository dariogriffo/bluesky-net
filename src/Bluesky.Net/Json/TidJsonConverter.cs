namespace Bluesky.Net.Json;

using Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class TidJsonConverter : JsonConverter<Tid>
{
    public override Tid? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (value is null)
        {
            return default;
        }
        
        try
        {
            return new Tid(value);
        }
        catch (Exception)
        {
            return default;
        }
    }

    public override void Write(Utf8JsonWriter writer, Tid? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}
