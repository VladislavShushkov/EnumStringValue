using System;
using System.Linq;

namespace EnumStringValue
{
    public static class EnumExtension
    {
        public static string StringValue<TEnum>(this TEnum value) where TEnum : struct, Enum
        {
            var stringValue = value.ToString();
            var memberInfos = typeof(TEnum).GetMember(stringValue);
            var hasNoMemberInfos = memberInfos.Length == 0;

            if (hasNoMemberInfos) return stringValue;

            var enumValueMemberInfo = memberInfos[0];
            var valueAttributes = enumValueMemberInfo.GetCustomAttributes(typeof(EnumStringValueAttribute), false);
            var hasValueAttributes = valueAttributes.Length > 0;

            return hasValueAttributes
                ? ((EnumStringValueAttribute)valueAttributes[0]).Value
                : stringValue;
        }

        public static bool TryGetEnumValue<TEnum>(this string value, out TEnum result) where TEnum : struct, Enum
        {
            if (Enum.TryParse<TEnum>(value, out result))
            {
                return true;
            }

            var descriptors = Helper.GetEnumValueDescriptors<TEnum>();
            
            foreach (var descriptor in descriptors)
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
