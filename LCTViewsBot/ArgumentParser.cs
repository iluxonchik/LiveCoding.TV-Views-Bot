using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCTViewsBot
{
    class ArgumentParser
    {
        public string Username { get; set; }
        public string ExtraArgs { get; set; }
        public int NumThreads { get; set; }
        public string ExecutablePath { get; set; }

        public ArgumentParser(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("At least username required as an argument.");
            }

            Username = args[0];
            for(int i = 1; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-t":
                        NumThreads = Int32.Parse(args[++i]);
                        break;

                    case "-a":
                        ExtraArgs = args[++i];
                        break;

                    case "-p":
                        ExecutablePath = args[++i];
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
