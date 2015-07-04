using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCTViewsBot
{

    class LiveStreamer : IDisposable
    {
        private string TempFilePath { get; set; }
        private const string WRITING_TO_OUT_MSG = "[cli][debug] Writing stream to output";
        private const string DEFAULT_ARGS = "worst -l debug --rtmp-timeout 10";
        private const string DEFAULT_EXEC_PATH = @"C:\Program Files (x86)\Livestreamer\livestreamer.exe";
        private const string LIVESTREAMER_FORCE_OPT = "-f";
        private const string LIVESTREAMER_OUTPUT_OPT = "-o";

        private const int DEFAULT_NUM_THREADS_PER_RUN = 10;

        public string Args { get; set; }
        public ProcessStartInfo PStartInfo { get; set; }
        public LiveStreamer(string rtmpURL, string args, string processFileName = DEFAULT_EXEC_PATH)
        {
            if (processFileName == null)
            {
                processFileName = DEFAULT_EXEC_PATH;
            }

            TempFilePath = Path.GetTempFileName();
           

            Args = rtmpURL + " " + DEFAULT_ARGS + " " + LIVESTREAMER_OUTPUT_OPT + " " + TempFilePath + " " + LIVESTREAMER_FORCE_OPT + " " + args;
            PStartInfo = BuildProcessStartInfo(processFileName, Args);
        }

        public void Run(int numThreads, int threadsPerRun = DEFAULT_NUM_THREADS_PER_RUN)
        {
            
            int numRuns = (numThreads + threadsPerRun - 1) / threadsPerRun;

            Process[] processes = new Process[threadsPerRun];
            Task[] tasks = new Task[threadsPerRun];

            for (int i = 0; i < threadsPerRun; i++)
            {
                processes[i] = new Process();
                processes[i].StartInfo = PStartInfo;
                processes[i].OutputDataReceived += Process_OutputDataReceived;
                processes[i].EnableRaisingEvents = true;
                processes[i].Exited += new EventHandler((sender, e) =>
                { Console.WriteLine("Bye!"); ((Process)sender).CancelOutputRead();});
            }

            for (int i = 0; i < numRuns; i++)
            {
                Console.WriteLine("Deploying task batch {0}", i);
                RunTasks(threadsPerRun, processes, tasks);
            }

        }

        private void RunTasks(int threadsPerRun, Process[] processes, Task[] tasks)
        {
            Console.WriteLine(threadsPerRun);
            Console.WriteLine(PStartInfo.FileName);

            for (int i = 0; i < threadsPerRun; i++)
            {
                tasks[i] = Task.Factory.StartNew(o =>
                {
                    int index = (int)o;
                    Console.WriteLine(index);
                    processes[index].Start();
                    Console.WriteLine("task stared");
                    processes[index].BeginOutputReadLine();
                    Console.WriteLine("Now reading async!");

                    processes[index].WaitForExit();
                }, i);

                Console.WriteLine("Starting thread {0}", i);
            }
            Task.WaitAll(tasks);
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
            if (e.Data == WRITING_TO_OUT_MSG)
            {
                Console.WriteLine("Killing process");
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                if (TempFilePath != null)
                {
                    File.Delete(TempFilePath);
                }

                disposedValue = true;
            }
        }

        ~LiveStreamer()
        {
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}