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
        private const string LIVECODING_TV_URL = "https://livecoding.tv/";
        private const string TOKEN_PATTERN = "rtmp://(.*)\"";
        public string Username { get; set; }
        public RTMPParser(string username)
        {
            Username = username;
        }

        public string ParseRTMPAddress()
        {
            Regex regex = new Regex(TOKEN_PATTERN);
            Match m = regex.Match(GetUrlContent(LIVECODING_TV_URL + Username));
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
