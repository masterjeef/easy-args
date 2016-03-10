using System;
using System.Collections.Generic;

namespace EasyArgs
{
    public class Args
    {
        private const string tack = "-";

        private readonly Dictionary<string, string> namedArgs = new Dictionary<string, string>();
        
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

        public string this[string key]
        {
            get
            {
                var keyLower = key.ToLower();

                if (namedArgs.ContainsKey(keyLower))
                {
                    return namedArgs[keyLower];
                }

                return Default;
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
                        namedArgs[split[0].ToLower()] = split[1];
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
