using EasyArgs.Models;
using System;
using System.Collections.Generic;

namespace EasyArgs
{
    public class Args
    {
        private const string tack = "-";

        private readonly Dictionary<string, Argument> namedArgs = new Dictionary<string, Argument>();
        
        private readonly HashSet<string> _flags = new HashSet<string>();

        private string [] _arguments;

        public Args() { }

        public Args(string [] args)
        {
            Arguments = args;
        }

        public string Default { get; set; }

        public string [] Arguments
        {
            get
            {
                return _arguments;
            }
            set
            {
                ExtractArgumnets(value);

                _arguments = value;
            }
        }

        public Argument this[string name]
        {
            get
            {
                var keyLower = name.ToLower();

                if (namedArgs.ContainsKey(keyLower))
                {
                    return namedArgs[keyLower];
                }

                return new Argument { Name = name, Value = Default };
            }
        }

        private void ExtractArgumnets(string [] args)
        {
            _flags.Clear();
            namedArgs.Clear();

            foreach (var arg in args)
            {
                if (arg.StartsWith(tack))
                {
                    var flag = arg.Substring(1, arg.Length - 1).ToLower();
                    
                    _flags.Add(flag);
                }
                else
                {
                    var split = arg.Split('=');

                    if (split.Length == 2)
                    {
                        var argument = new Argument { Name = split[0], Value = split[1] };

                        namedArgs[argument.Name.ToLower()] = argument;
                    }
                    else
                    {
                        throw new InvalidOperationException(string.Format("'{0}' does not follow the proper conventions for flags and named arguments", arg));
                    }
                }
            }
        }

        public bool HasFlag(string flag)
        {
            return _flags.Contains(flag.ToLower());
        }
    }
}
