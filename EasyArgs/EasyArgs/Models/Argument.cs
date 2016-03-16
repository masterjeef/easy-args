using System;
namespace EasyArgs.Models
{
    public class Argument
    {
        public string Name { get; set; }

        public string Value { get; set; }
        
        public static implicit operator string(Argument arg)
        {
            return arg.Value;
        }

        public static implicit operator int(Argument arg)
        {
            return arg.Value == null ? default(int) : int.Parse(arg.Value);
        }

        public static implicit operator double(Argument arg)
        {
            return arg.Value == null ? default(double) : double.Parse(arg.Value);
        }

        public static implicit operator decimal(Argument arg)
        {
            return arg.Value == null ? default(decimal) : decimal.Parse(arg.Value);
        }

        public static implicit operator bool(Argument arg)
        {
            return arg.Value == null ? default(bool) : bool.Parse(arg.Value);
        }

        public static implicit operator DateTime(Argument arg)
        {
            return arg.Value == null ? default(DateTime) : DateTime.Parse(arg.Value);
        }
    }
}
