using System;
using System.Linq;

namespace EnumStringValue
{
    public class EnumStringValueConverter<TEnum> where TEnum : struct, Enum
    {
        private readonly EnumValueDescriptor<TEnum>[] _descriptors = Helper.GetEnumValueDescriptors<TEnum>();

        public string ToString(TEnum value)
        {
            return value.StringValue();
        }

        public TEnum FromString(string value)
        {
            if (TryGetFromString(value, out var result))
                return result;
            
            throw new Exception("Can not parse string to enum");
        }

        public bool TryGetFromString(string value, out TEnum result)
        {
            if (Enum.TryParse<TEnum>(value, out result))
                return true;

            foreach (var descriptor in _descriptors)
            {
                if (descriptor.EnumStringValues.Contains(value))
                {
                    result = descriptor.Value;
                    return true;
                }
            }

            return false;
        }
    }
}
