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
            Assert.Equal(args["Hello"], "World");
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
    }
}
