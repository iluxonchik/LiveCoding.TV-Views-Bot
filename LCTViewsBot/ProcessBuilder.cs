using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LCTViewsBot
{
    class ProcessBuilder
    {
        public ProcessStartInfo PStartInfo { get; set; }
        public ProcessBuilder(string processFileName)
        {
            PStartInfo = BuildProcessStartInfo(processFileName);
        }
        private ProcessStartInfo BuildProcessStartInfo(string processFileName = @"C:\Program Files (x86)\Livestreamer\livestreamer.exe")
        {
            return new ProcessStartInfo()
            {
                FileName = processFileName,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };
        }

        public Process Build()
        {
            return new Process() { StartInfo = PStartInfo };
        }
    }
}