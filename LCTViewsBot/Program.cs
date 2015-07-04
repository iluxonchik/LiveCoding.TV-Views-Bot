using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCTViewsBot
{
    class Program
    {
        static void Main(string[] arg)
        {
           ArgumentParser ap = new ArgumentParser(arg);
            Console.WriteLine(ap.RTMPTimeout);
            Console.WriteLine(ap.NumThreadsPerRun);
            LiveStreamer livestreamer = new LiveStreamer(new RTMPParser(ap.Url).ParseRTMPAddress(), ap.ExtraArgs,ap.RTMPTimeout, ap.ExecutablePath);

            if (ap.NumThreadsPerRun > 0)
                livestreamer.Run(ap.NumThreads, ap.NumThreadsPerRun);
            else
                livestreamer.Run(ap.NumThreads);

            Console.ReadKey();
        }
    }
}
