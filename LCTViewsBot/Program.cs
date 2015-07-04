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
           LiveStreamer livestreamer = new LiveStreamer(new RTMPParser(ap.Url).ParseRTMPAddress(), ap.ExtraArgs, ap.ExecutablePath);
           livestreamer.Run(ap.NumThreads);
            Console.ReadKey();
        }
    }
}
