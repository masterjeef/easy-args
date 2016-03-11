using System;
namespace EasyArgs.Models
{
    public class Argument
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public int AsInt()
        {
            return Value == null ? default(int) : int.Parse(Value);
        }

        public double AsDouble()
        {
            return Value == null ? default(double) : double.Parse(Value);
        }

        public decimal AsDecimal()
        {
            return Value == null ? default(decimal) : decimal.Parse(Value);
        }

        public bool AsBool()
        {
            return Value == null ? default(bool) : bool.Parse(Value);
        }

        public DateTime AsDateTime()
        {
            return Value == null ? default(DateTime) : DateTime.Parse(Value);
        }
    }
}
