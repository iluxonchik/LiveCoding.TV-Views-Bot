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
            LiveStreamer l = new LiveStreamer("rtmp://eumedia1.livecoding.tv:1935/livecodingtv/kitty_cent?t=6812FAC8CF464F2D8340D7FCA92AA033");
            l.Run(50);
        }
    }
}
