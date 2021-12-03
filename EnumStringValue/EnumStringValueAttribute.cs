using System;

namespace EnumStringValue
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class EnumStringValueAttribute : Attribute
    {
        public readonly string Value;

        public EnumStringValueAttribute(string value)
        {
            Value = value;
        }
    }
}
