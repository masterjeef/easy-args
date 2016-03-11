using EasyArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Assert.Equal(args["Hello"].Value, "World");
        }
        
        [Fact]
        public void Flags_should_populate_the_flags_set()
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

            Assert.True(args.HasFlag("f"));
            Assert.False(args.HasFlag("d"));
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
            Assert.Equal(args["hello"].Value, "World");

            Assert.True(args.HasFlag("F"));
        }

        [Fact]
        public void Default_should_be_returned_when_the_argument_is_not_present()
        {
            var arguments = new[] {
                "Hello=World",
                "-f",
                "Environment=Development",
                "-t"
            };

            const string defaultArg = "default";

            var args = new Args
            {
                Arguments = arguments,
                Default = defaultArg
            };

            Assert.Equal(args["DoesNotExist"].Value, defaultArg);
        }

        [Fact]
        public void AsInt_should_parse_the_argument_to_int()
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

            Assert.Equal(args["Integer"].AsInt(), 3);
        }

        [Fact]
        public void AsDouble_should_parse_the_argument_to_double()
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

            Assert.Equal(args["Double"].AsDouble(), 3.14);
        }

        [Fact]
        public void AsDecimal_should_parse_the_argument_to_decimal()
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

            Assert.Equal(args["Decimal"].AsDecimal(), (decimal)22.8983);
        }

        [Fact]
        public void AsBool_should_parse_the_argument_to_bool()
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

            Assert.Equal(args["Bool"].AsBool(), true);
        }

        [Fact]
        public void AsDateTime_should_parse_the_argument_to_DateTime()
        {
            var arguments = new[] {
                "Date=03/11/2016"
            };

            const string defaultArg = "default";

            var args = new Args
            {
                Arguments = arguments,
                Default = defaultArg
            };

            Assert.Equal(args["Date"].AsDateTime(), new DateTime(2016, 3, 11));
        }
    }
}
