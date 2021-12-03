using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EnumStringValue.Json
{
    public class EnumStringValueJsonConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
    {
        private static readonly EnumStringValueConverter<TEnum> EnumStringValueConverter = new EnumStringValueConverter<TEnum>();
        
        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert != typeof(TEnum))
                throw new JsonException();
            if (reader.TokenType != JsonTokenType.String)
                throw new JsonException();

            var value = reader.GetString();
                
            return EnumStringValueConverter.FromString(value);
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(EnumStringValueConverter.ToString(value));
        }
    }
}
