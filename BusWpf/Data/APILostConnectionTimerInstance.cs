﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BusWpf.Data
{
    internal class APILostConnectionTimerInstance
    {
        private static APILostConnectionTimerInstance apiTimerInstance;

        public event EventHandler LostConnectionTimerDone;
        private DispatcherTimer lostConnectionTimer;

        public void StartLostConnectionTimer()
        {
            lostConnectionTimer.Stop();
            lostConnectionTimer.Interval = TimeSpan.FromSeconds(60);
            lostConnectionTimer.Start();
        }

        private void OnLostConnectionTimer_Tick(object sender, EventArgs e)
        {
            if (LostConnectionTimerDone != null)
                LostConnectionTimerDone(this, EventArgs.Empty);
        }

        public APILostConnectionTimerInstance()
        {
            initialize();
        }

        private void initialize()
        {
            lostConnectionTimer = new DispatcherTimer();
            lostConnectionTimer.Tick += new EventHandler(OnLostConnectionTimer_Tick);
        }

        public static APILostConnectionTimerInstance GetInstance()
        {
            if (apiTimerInstance == null)
                apiTimerInstance = new APILostConnectionTimerInstance();

            return apiTimerInstance;
        }
    }
}
