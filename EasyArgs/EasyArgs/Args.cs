using EasyArgs.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyArgs
{
    public class Args
    {
        private const string flagsExpression = "\\s*-(?<flag>[^\\s]+)";

        private const string namedArgsExpression = "\\s*(?<name>[^\\s=]+)=((\"(?<valueQuoted>.*?)\")|(?<value>([^\\s]+)))";

        private const string paramExpression = "(?<name>.*?)=(?<param>.+)";

        private readonly Dictionary<string, Argument> _namedArgs = new Dictionary<string, Argument>(StringComparer.OrdinalIgnoreCase);

        private readonly HashSet<string> _flags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        
        public Args() { }

        public Args(string [] args)
        {
            Arguments = args;
        }

        public Args(string args)
        {
            ArgsString = args;
        }

        public string [] Arguments
        {
            set
            {
                ExtractArgumnets(value);
            }
        }

        public string ArgsString
        {
            set
            {
                ExtractArgumnets(value);
            }
        }

        public Argument this[string name]
        {
            get
            {
                if (_namedArgs.ContainsKey(name))
                {
                    return _namedArgs[name];
                }

                return new Argument { Name = name};
            }
        }

        private void ExtractArgumnets(string [] args)
        {
            var builder = new StringBuilder();

            var regex = new Regex(paramExpression);

            foreach (var arg in args)
            {
                var match = regex.Match(arg);

                if (match.Success)
                {
                    builder.Append(match.Groups["name"].Value);
                    builder.Append("=");
                    builder.Append("\"");
                    builder.Append(match.Groups["param"].Value);
                    builder.Append("\"");
                }
                else
                {
                    builder.Append(arg);
                }

                builder.Append(" ");
            }

            var merged = builder.ToString();
            
            ExtractArgumnets(merged);
        }

        private void ExtractArgumnets(string args)
        {
            _flags.Clear();
            _namedArgs.Clear();

            foreach (var arg in ParseNamedArgs(args))
            {
                _namedArgs[arg.Name] = arg;
            }

            foreach (var flag in ParseFlags(args))
            {
                _flags.Add(flag);
            }
        }

        private IEnumerable<Argument> ParseNamedArgs(string args)
        {
            var regex = new Regex(namedArgsExpression);

            var match = regex.Match(args);

            while(match.Success)
            {
                var value = match.Groups["value"].Success ?
                    match.Groups["value"].Value :
                    match.Groups["valueQuoted"].Value;

                var name = match.Groups["name"].Value;

                yield return new Argument { Name = name, Value = value };

                match = match.NextMatch();
            }
        }

        private IEnumerable<string> ParseFlags(string args)
        {
            var regex = new Regex(flagsExpression);

            var match = regex.Match(args);

            while (match.Success)
            {
                yield return match.Groups["flag"].Value;

                match = match.NextMatch();
            }
        }
        
        public bool HasFlag(string flag)
        {
            return _flags.Contains(flag);
        }
    }
}
