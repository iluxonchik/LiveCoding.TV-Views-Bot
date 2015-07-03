using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCTViewsBot
{
    class LiveStreamer 
    {
        const string WRITING_TO_OUT_MSG = "[cli][debug] Writing stream to output";
        const string DEFAULT_ARGS = "worst -o deleteme -f -l debug";

        public string Args { get; set; }
        public ProcessStartInfo PStartInfo { get; set; }
        public LiveStreamer(string rtmpURL, string args = DEFAULT_ARGS, string processFileName = @"C:\Program Files (x86)\Livestreamer\livestreamer.exe")
        {
            Args = rtmpURL + " " + args;
            PStartInfo = BuildProcessStartInfo(processFileName, Args);
        }

        public void Run(int numThreads)
        {
            Task[] tasks = new Task[numThreads];
            Console.WriteLine(tasks.Length);

            for (int i = 0; i < numThreads; i++)
            {
                tasks[i] = Task.Factory.StartNew(() =>
                {
                    Process p = new Process() { StartInfo = PStartInfo };
                    p.Start();
                    p.BeginOutputReadLine();
                    p.OutputDataReceived += Process_OutputDataReceived;
                    p.WaitForExit();
                });

                Console.WriteLine("Starting thread {0}", i);
            }
            
            Task.WaitAll(tasks);
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
            if(e.Data == WRITING_TO_OUT_MSG)
            {
                ((Process)sender).Kill();
            }
        }

        private ProcessStartInfo BuildProcessStartInfo(string processFileName, string args)
        {
            return new ProcessStartInfo()
            {
                Arguments = args,
                FileName = processFileName,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };
        }
    }
}
