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
                "-X"
            };

            var args = new Args
            {
                Arguments = arguments
            };
            
            Assert.True(args.HasFlag("x"));
            Assert.True(args.HasFlag("X"));
        }

        [Fact]
        public void Arguments_and_flags_should_not_be_case_sensitive()
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

            Assert.NotNull(args["hello"]);
            Assert.Equal(args["hello"], "World");

            Assert.True(args.HasFlag("F"));
        }

        [Fact]
        public void Default_should_be_returned_when_the_argument_is_not_present()
        {
            var arguments = new[] {
                "Hello=World",
                "-f",
                "Environment=Development",
                "Something=\"Another Param\"",
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
                "Something=\"Another Param\"",
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
                "Something=\"Another Param\"",
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
        public void Implicit_int_should_parse_the_argument_to_int()
        {
            var arguments = new[] {
                "Integer=3"
            };

            const string defaultArg = "default";

            var args = new Args
            {
                Arguments = arguments,
                Default = defaultArg
            };

            int arg = args["Integer"];

            Assert.Equal(arg, 3);
        }

        [Fact]
        public void Implicit_double_should_parse_the_argument_to_double()
        {
            var arguments = new[] {
                "Double=3.14"
            };

            const string defaultArg = "default";

            var args = new Args
            {
                Arguments = arguments,
                Default = defaultArg
            };

            double arg = args["Double"];

            Assert.Equal(arg, 3.14);
        }

        [Fact]
        public void Implicit_decimal_should_parse_the_argument_to_decimal()
        {
            var arguments = new[] {
                "Decimal=22.8983"
            };

            const string defaultArg = "default";

            var args = new Args
            {
                Arguments = arguments,
                Default = defaultArg
            };

            decimal arg = args["Decimal"];

            Assert.Equal(arg, (decimal)22.8983);
        }

        [Fact]
        public void Implicit_bool_should_parse_the_argument_to_bool()
        {
            var arguments = new[] {
                "Bool=True"
            };

            const string defaultArg = "default";

            var args = new Args
            {
                Arguments = arguments,
                Default = defaultArg
            };

            bool arg = args["Bool"];

            Assert.Equal(arg, true);
        }

        [Fact]
        public void Implicit_dateTime_should_parse_the_argument_to_DateTime()
        {
            var arguments = new[] {
                "Date=03/11/2016",
            };

            const string defaultArg = "default";

            var args = new Args
            {
                Arguments = arguments,
                Default = defaultArg
            };

            DateTime arg = args["Date"];
            Assert.Equal(arg, new DateTime(2016, 3, 11));
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

            int intArg = args["MissingArg"];

            Assert.Equal(intArg, default(int));

            double doubleArg = args["DoesNotExist"];

            Assert.Equal(doubleArg, default(double));

            bool boolArg = args["Nope"];

            Assert.Equal(boolArg, default(bool));

            decimal decimalArg = args["Gone"];

            Assert.Equal(decimalArg, default(decimal));

            DateTime dateTimeArg = args["Why?"];

            Assert.Equal(dateTimeArg, default(DateTime));
        }

        [Fact]
        public void Args_string_constructor_should_poplate_the_args_dictionary()
        {
            var args = new Args("Hello=World -f Environment=Development Something=\"Another Param\" -t");

            Assert.Equal(args["hello"], "World");
            Assert.True(args.HasFlag("t"));
            Assert.Equal(args["something"], "Another Param");
        }
    }
}
