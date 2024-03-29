﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCTViewsBot
{
    class ArgumentParser
    {
        private const int DEFAULT_THREADS_NUMBER = 10;
        private const int DEFAULT_RTMP_TIMEOUT = 10;
        private const string RTMP_START = "rtmp://";
        private const string HHTP_START = "http://";
        private const string HHTPS_START = "https://";
        private const string LIVECODINGTV_URL = "https://livecoding.tv/";

        public string Url { set; get; }
        public string ExtraArgs { get; set; }
        public int NumThreads { get; set; }
        public string ExecutablePath { get; set; }
        public int MyProperty { get; set; }
        public int NumThreadsPerRun { get; set; }
        public int RTMPTimeout { get; set; }

        public ArgumentParser(string[] args)
        {
            NumThreads = DEFAULT_THREADS_NUMBER;
            RTMPTimeout = DEFAULT_RTMP_TIMEOUT;

            if (args.Length < 1)
            {
                throw new ArgumentException("At least username/rtmp url required as an argument.");
            }

            Url = (args[0].StartsWith(RTMP_START) || args[0].StartsWith(HHTPS_START) 
                || args[0].StartsWith(HHTP_START)) ? (args[0]) : (LIVECODINGTV_URL + args[0]);

            for(int i = 0; i < args.Length; i++)
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

                    case "-n":
                        NumThreadsPerRun = Int32.Parse(args[++i]);
                        break;

                    case "-r":
                        RTMPTimeout = Int32.Parse(args[++i]);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
