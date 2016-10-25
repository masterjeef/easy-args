﻿using EasyArgs.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EasyArgs
{
    public class Args
    {

        private const string flagsExpression = "\\s*-(?<flag>[^\\s]+)";

        private const string namedArgsExpression = "\\s*(?<name>[^\\s=]+)=((\"(?<valueQuoted>.+)\")|(?<value>([^\\s]+)))";

        private readonly Dictionary<string, Argument> _namedArgs = new Dictionary<string, Argument>();
        
        private readonly HashSet<string> _flags = new HashSet<string>();
        
        public Args() { }

        public Args(string [] args)
        {
            Arguments = args;
        }

        public Args(string args)
        {
            ArgsString = args;
        }

        public string Default { get; set; }

        public string [] Arguments
        {
            set
            {
                ExtractArgumnets(value);
            }
        }

        public string ArgsString {
            set
            {
                ExtractArgumnets(value);
            }
        }

        public Argument this[string name]
        {
            get
            {
                var keyLower = name.ToLower();

                if (_namedArgs.ContainsKey(keyLower))
                {
                    return _namedArgs[keyLower];
                }

                return new Argument { Name = name, Value = Default };
            }
        }

        private void ExtractArgumnets(string [] args)
        {
            var merged = string.Join(" ", args);
            
            ExtractArgumnets(merged);
        }

        private void ExtractArgumnets(string args)
        {
            _flags.Clear();
            _namedArgs.Clear();

            foreach (var arg in ParseNamedArgs(args))
            {
                _namedArgs[arg.Name.ToLower()] = arg;
            }

            foreach (var flag in ParseFlags(args))
            {
                _flags.Add(flag.ToLower());
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
            return _flags.Contains(flag.ToLower());
        }
    }
}
