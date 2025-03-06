using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class TimeSpanToStringConverter : JsonConverter<TimeSpan>
{
  public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (reader.TokenType == JsonTokenType.String)
    {
      if (TimeSpan.TryParse(reader.GetString(), out TimeSpan result))
      {
        return result;
      }
    }
    throw new JsonException("Invalid TimeSpan format");
  }

  public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
  {
    writer.WriteStringValue(value.ToString(@"hh\:mm"));
  }
}
