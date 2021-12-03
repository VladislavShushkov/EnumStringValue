using System;

namespace EnumStringValue
{
    internal readonly struct EnumValueDescriptor<TEnum> where TEnum: struct, Enum
    {
        public readonly TEnum Value;
        public readonly string[] EnumStringValues;

        public EnumValueDescriptor(TEnum value, string[] enumStringValues)
        {
            Value = value;
            EnumStringValues = enumStringValues ?? Array.Empty<string>();
        }
    }
}
