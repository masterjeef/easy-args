using System;

namespace EasyArgs.Models
{
    public class Argument
    {
        public string Name { get; set; }

        public string Value { get; set; }

        private static T GetDefaultOrParse<T>(Argument arg, Func<T> func)
        {
            if (arg.Value == null)
            {
                return default(T);
            }

            return func();
        }

        public static implicit operator byte(Argument arg)
        {
            return GetDefaultOrParse(arg, () => byte.Parse(arg.Value));
        }

        public static implicit operator sbyte(Argument arg)
        {
            return GetDefaultOrParse(arg, () => sbyte.Parse(arg.Value));
        }

        public static implicit operator int(Argument arg)
        {
            return GetDefaultOrParse(arg, () => int.Parse(arg.Value));
        }

        public static implicit operator uint(Argument arg)
        {
            return GetDefaultOrParse(arg, () => uint.Parse(arg.Value));
        }

        public static implicit operator short(Argument arg)
        {
            return GetDefaultOrParse(arg, () => short.Parse(arg.Value));
        }

        public static implicit operator ushort(Argument arg)
        {
            return GetDefaultOrParse(arg, () => ushort.Parse(arg.Value));
        }

        public static implicit operator long(Argument arg)
        {
            return GetDefaultOrParse(arg, () => long.Parse(arg.Value));
        }

        public static implicit operator ulong(Argument arg)
        {
            return GetDefaultOrParse(arg, () => ulong.Parse(arg.Value));
        }

        public static implicit operator float(Argument arg)
        {
            return GetDefaultOrParse(arg, () => float.Parse(arg.Value));
        }

        public static implicit operator double(Argument arg)
        {
            return GetDefaultOrParse(arg, () => double.Parse(arg.Value));
        }

        public static implicit operator char(Argument arg)
        {
            return GetDefaultOrParse(arg, () => char.Parse(arg.Value));
        }

        public static implicit operator bool(Argument arg)
        {
            return GetDefaultOrParse(arg, () => bool.Parse(arg.Value));
        }

        public static implicit operator string(Argument arg)
        {
            return GetDefaultOrParse(arg, () => arg.Value);
        }

        public static implicit operator decimal(Argument arg)
        {
            return GetDefaultOrParse(arg, () => decimal.Parse(arg.Value));
        }

        public static implicit operator DateTime(Argument arg)
        {
            return GetDefaultOrParse(arg, () => DateTime.Parse(arg.Value));
        }

        public static implicit operator Guid(Argument arg)
        {
            return GetDefaultOrParse(arg, () => Guid.Parse(arg.Value));
        }
    }
}
