﻿using System;
using System.Threading;
using System.Web.Hosting;

namespace AppFail.Reporting
{
    /// <summary>
    /// Runs some logic on a background thread that is registered with ASP.NET
    /// to be notified when tearing down the appdomain
    /// </summary>
    internal abstract class BackgroundWorker : IRegisteredObject
    {
        private ManualResetEvent _shuttingDownSignal = new ManualResetEvent(false);
        private ManualResetEvent _shutDownSignal = new ManualResetEvent(false);
        private Thread _runningThread;
        private object lockObj = new object();

        public BackgroundWorker()
        {
            // Tells ASP.NET not to shut down the app domain without calling stop first
            HostingEnvironment.RegisterObject(this);
        }

        public void Stop(bool immediate)
        {
            _shuttingDownSignal.Set();
            
            // Wait for up to 30 seconds for the background thread to complete.
            if (!immediate)
            {
                _shutDownSignal.WaitOne(30000);
            }

            HostingEnvironment.UnregisterObject(this);
        }

        public void Start()
        {
            lock (lockObj)
            {
                if (_runningThread == null)
                {
                    // Start the background thread worker
                    _runningThread = new Thread(new ThreadStart(Run));
                    _runningThread.Start();
                    }
            }
        }

        public void Run()
        {
            WaitHandle waitHandle;
 
            do
            {
                // Do the work here
                waitHandle = DoWork();
            }
            while (WaitHandle.WaitAny(new WaitHandle[] { _shuttingDownSignal, waitHandle }, TimeSpan.FromSeconds(30)) != 0);

            _shutDownSignal.Set();
        }

        /// <summary>
        /// Performs the background work. Returns a wait handle to wait on 
        /// for the next invocation.
        /// </summary>
        /// <returns></returns>
        public abstract WaitHandle DoWork();
    }
}
