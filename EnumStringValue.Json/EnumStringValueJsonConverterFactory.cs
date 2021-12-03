using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EnumStringValue.Json
{
    public class EnumStringValueJsonConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum && typeToConvert.IsValueType;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return Activator.CreateInstance(
                typeof(EnumStringValueJsonConverter<>).MakeGenericType(typeToConvert),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                args: null,
                culture: null) as JsonConverter;
        }
    }
}
