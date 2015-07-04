using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LCTViewsBot
{
    class RTMPParser
    {

        private const string RTMP_START = "rtmp://";
        private const string TOKEN_PATTERN = "rtmp://(.*)\"";
        public string Url { get; set; }

        public RTMPParser(string url)
        {
            Url= url;
            Console.WriteLine(url);
        }

        public string ParseRTMPAddress()
        {
            if (Url.StartsWith(RTMP_START))
            {
                return Url;
            }

            Regex regex = new Regex(TOKEN_PATTERN);
            Match m = regex.Match(GetUrlContent(Url));
            if (m.Length == 0)
                throw new RTMPUrlNotFoundException();
            return m.Value.Substring(0, m.Value.Length - 1);
        }

        private string GetUrlContent(string url)
        {
            WebRequest wr = WebRequest.Create(url);
            WebResponse response = wr.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            return sr.ReadToEnd();
        }
    }
}
