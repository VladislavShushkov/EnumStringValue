using System;
using System.Linq;
using System.Reflection;

namespace EnumStringValue
{
    internal static class Helper
    {
        public static EnumValueDescriptor<TEnum>[] GetEnumValueDescriptors<TEnum>() where TEnum : struct, Enum
        {
            var enumType = typeof(TEnum);
            return enumType
                .GetMembers()
                .Where(x => x.DeclaringType == enumType)
                .Skip(1)
                .Select(MemberInfoToEnumValueDescriptor<TEnum>)
                .ToArray();
        }

        public static EnumValueDescriptor<TEnum> MemberInfoToEnumValueDescriptor<TEnum>(MemberInfo memberInfo) where TEnum : struct, Enum
        {
            var attributes = memberInfo
                .GetCustomAttributes(typeof(EnumStringValueAttribute), false)
                .Select(x => ((EnumStringValueAttribute)x).Value)
                .ToArray();

            var value = (TEnum) Enum.Parse(typeof(TEnum), memberInfo.Name);

            return new EnumValueDescriptor<TEnum>(value, attributes);
        }
    }
}
