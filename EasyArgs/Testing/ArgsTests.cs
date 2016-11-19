using EasyArgs;
using System;
using Xunit;

namespace Testing
{
    public class ArgsTests
    {

        [Fact]
        public void Named_arguments_should_populate_the_named_args_dictionary()
        {
            var arguments = new[] {
                "Hello=World",
                "-f",
                "Environment=Development",
                "-t"
            };

            var args = new Args
            {
                Arguments = arguments
            };
            
            Assert.NotNull(args["Hello"]);
            Assert.Equal(args["Hello"], "World");
        }
        
        [Fact]
        public void Flags_should_populate_the_flags_set()
        {
            var arguments = new[] {
                "Hello=World",
                "-f",
                "Environment=Development",
                "-t",
                "-X"
            };

            var args = new Args
            {
                Arguments = arguments
            };

            Assert.True(args.HasFlag("f"));
            Assert.True(args.HasFlag("x"));
            Assert.False(args.HasFlag("d"));
        }

        [Fact]
        public void Flags_should_be_case_insensitive()
        {
            var arguments = new[] {
                "Hello=World",
                "-f",
                "Environment=Development",
                "-t",
                "-X",
                "-REINSTALL",
                "-Update-Package",
                "--TwoTacks"
            };

            var args = new Args
            {
                Arguments = arguments
            };
            
            Assert.True(args.HasFlag("x"));
            Assert.True(args.HasFlag("T"));
            Assert.True(args.HasFlag("reinstall"));
            Assert.True(args.HasFlag("update-package"));
            Assert.True(args.HasFlag("-TwoTacks"));
        }

        [Fact]
        public void Arguments_should_not_be_case_sensitive()
        {
            var arguments = new[] {
                "Hello=World",
                "-f",
                "Environment=Development",
                "-t"
            };

            var args = new Args
            {
                Arguments = arguments
            };

            Assert.Equal(args["hello"], "World");
        }

        [Fact]
        public void Default_should_be_returned_when_the_argument_is_not_present()
        {
            var arguments = new[] {
                "Hello=World",
                "-f",
                "Environment=Development",
                "AnotherTest===Test",
                "-t"
            };

            const string defaultArg = "default";

            var args = new Args
            {
                Arguments = arguments,
                Default = defaultArg
            };

            Assert.Equal(args["DoesNotExist"], defaultArg);
        }

        [Fact]
        public void Argument_parsing_should_handle_spaces()
        {
            var arguments = new[] {
                "Hello=World",
                "-f",
                "Environment=Development",
                "Something=Another Param",
                "AnotherTest===Test",
                "-t"
            };
            
            var args = new Args
            {
                Arguments = arguments
            };

            Assert.Equal(args["Something"], "Another Param");
        }

        [Fact]
        public void Argument_parsing_should_handle_equals()
        {
            var arguments = new[] {
                "Hello=World",
                "-f",
                "Environment=Development",
                "AnotherTest===Test",
                "-t"
            };
            
            var args = new Args
            {
                Arguments = arguments
            };

            Assert.Equal(args["AnotherTest"], "==Test");
        }

        [Fact]
        public void Implicit_byte_should_parse_the_argument_to_byte()
        {
            var arguments = new[] {
                "Byte=255"
            };

            var args = new Args
            {
                Arguments = arguments
            };

            byte arg = args["Byte"];

            Assert.Equal(arg, 255);
        }

        [Fact]
        public void Implicit_sbyte_should_parse_the_argument_to_sbyte()
        {
            var arguments = new[] {
                "sbyte=55"
            };

            var args = new Args
            {
                Arguments = arguments
            };

            sbyte arg = args["sbyte"];

            Assert.Equal(arg, 55);
        }

        [Fact]
        public void Implicit_int_should_parse_the_argument_to_int()
        {
            var arguments = new[] {
                "Integer=3"
            };

            var args = new Args
            {
                Arguments = arguments
            };

            int arg = args["Integer"];

            Assert.Equal(arg, 3);
        }

        [Fact]
        public void Implicit_uint_should_parse_the_argument_to_uint()
        {
            var arguments = new[] {
                "uint=5"
            };

            var args = new Args
            {
                Arguments = arguments
            };

            uint arg = args["uint"];

            Assert.Equal(arg, (uint) 5);
        }

        [Fact]
        public void Implicit_short_should_parse_the_argument_to_short()
        {
            var arguments = new[] {
                "short=2"
            };

            var args = new Args
            {
                Arguments = arguments
            };

            short arg = args["short"];

            Assert.Equal(arg, (short)2);
        }

        [Fact]
        public void Implicit_ushort_should_parse_the_argument_to_ushort()
        {
            var arguments = new[] {
                "ushort=10"
            };

            var args = new Args
            {
                Arguments = arguments
            };

            ushort arg = args["ushort"];

            Assert.Equal(arg, (ushort)10);
        }

        [Fact]
        public void Implicit_long_should_parse_the_argument_to_long()
        {
            var arguments = new[] {
                "long=10"
            };

            var args = new Args
            {
                Arguments = arguments
            };

            long arg = args["long"];

            Assert.Equal(arg, (long)10);
        }

        [Fact]
        public void Implicit_ulong_should_parse_the_argument_to_ulong()
        {
            var arguments = new[] {
                "ulong=10"
            };

            var args = new Args
            {
                Arguments = arguments
            };

            ulong arg = args["ulong"];

            Assert.Equal(arg, (ulong)10);
        }

        [Fact]
        public void Implicit_float_should_parse_the_argument_to_float()
        {
            var arguments = new[] {
                "float=2.1"
            };

            var args = new Args
            {
                Arguments = arguments
            };

            float arg = args["float"];

            Assert.Equal(arg, (float)2.1);
        }

        [Fact]
        public void Implicit_double_should_parse_the_argument_to_double()
        {
            var arguments = new[] {
                "Double=3.14"
            };

            var args = new Args
            {
                Arguments = arguments
            };

            double arg = args["Double"];

            Assert.Equal(arg, 3.14);
        }

        [Fact]
        public void Implicit_char_should_parse_the_argument_to_char()
        {
            var arguments = new[] {
                "char=a"
            };

            var args = new Args
            {
                Arguments = arguments
            };

            char arg = args["char"];

            Assert.Equal(arg, 'a');
        }

        [Fact]
        public void Implicit_bool_should_parse_the_argument_to_bool()
        {
            var arguments = new[] {
                "Bool=True"
            };

            var args = new Args
            {
                Arguments = arguments
            };

            bool arg = args["Bool"];

            Assert.Equal(arg, true);
        }

        [Fact]
        public void Implicit_string_should_parse_the_argument_to_string()
        {
            var arguments = new[] {
                "string=Hello"
            };

            var args = new Args
            {
                Arguments = arguments
            };

            string arg = args["string"];

            Assert.Equal(arg, "Hello");
        }

        [Fact]
        public void Implicit_decimal_should_parse_the_argument_to_decimal()
        {
            var arguments = new[] {
                "Decimal=22.8983"
            };

            var args = new Args
            {
                Arguments = arguments
            };

            decimal arg = args["Decimal"];

            Assert.Equal(arg, (decimal)22.8983);
        }

        [Fact]
        public void Implicit_dateTime_should_parse_the_argument_to_DateTime()
        {
            var arguments = new[] {
                "Date=03/11/2016",
            };

            var args = new Args
            {
                Arguments = arguments
            };

            DateTime arg = args["Date"];
            Assert.Equal(arg, new DateTime(2016, 3, 11));
        }


        [Fact]
        public void Implicit_guid_should_parse_the_argument_to_guid()
        {
            var arguments = new[] {
                "guid=994d96d5-fb74-43f4-ab41-cfc721179cc1",
            };

            var args = new Args
            {
                Arguments = arguments
            };

            Guid arg = args["guid"];
            Assert.Equal(arg, Guid.Parse("994d96d5-fb74-43f4-ab41-cfc721179cc1"));
        }

        [Fact]
        public void Implicit_casting_for_null_arg_should_return_type_default()
        {
            var arguments = new[] {
                "Integer=3"
            };

            var args = new Args
            {
                Arguments = arguments
            };

            byte byteArg = args["MissingArg"];

            Assert.Equal(byteArg, default(byte));

            sbyte sbyteArg = args["MissingArg"];

            Assert.Equal(sbyteArg, default(sbyte));

            int intArg = args["MissingArg"];

            Assert.Equal(intArg, default(int));

            uint uintArg = args["MissingArg"];

            Assert.Equal(uintArg, default(uint));

            short shortArg = args["MissingArg"];

            Assert.Equal(shortArg, default(short));

            ushort ushortArg = args["MissingArg"];

            Assert.Equal(ushortArg, default(ushort));

            long longArg = args["DoesNotExist"];

            Assert.Equal(longArg, default(long));

            ulong ulongArg = args["DoesNotExist"];

            Assert.Equal(ulongArg, default(ulong));

            float floatArg = args["DoesNotExist"];

            Assert.Equal(floatArg, default(float));

            double doubleArg = args["DoesNotExist"];

            Assert.Equal(doubleArg, default(double));

            char charArg = args["DoesNotExist"];

            Assert.Equal(charArg, default(char));

            bool boolArg = args["Nope"];

            Assert.Equal(boolArg, default(bool));

            string stringArg = args["Nope"];

            Assert.Equal(stringArg, default(string));

            decimal decimalArg = args["Gone"];

            Assert.Equal(decimalArg, default(decimal));

            DateTime dateTimeArg = args["Why?"];

            Assert.Equal(dateTimeArg, default(DateTime));

            Guid guidArg = args["Why?"];

            Assert.Equal(guidArg, default(Guid));
        }

        [Fact]
        public void Args_string_constructor_should_poplate_the_args_dictionary()
        {
            var args = new Args("Hello=World -f Environment=Development Something=\"Another Param\" -t Something2=\"Hey\"");

            Assert.Equal(args["hello"], "World");
            Assert.True(args.HasFlag("t"));
            Assert.Equal(args["something"], "Another Param");
            Assert.Equal(args["something2"], "Hey");
        }
    }
}
