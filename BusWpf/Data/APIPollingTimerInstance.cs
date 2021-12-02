using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BusWpf.Data
{
    internal class APIPollingTimerInstance
    {
        private static APIPollingTimerInstance apiTimerInstance;
        public event EventHandler PollingTimerDone;

        private DispatcherTimer pollingTimer;

        public APIPollingTimerInstance()
        {
            initialize();
        }

        private void initialize()
        {
            pollingTimer = new DispatcherTimer();
        }

        public static APIPollingTimerInstance GetInstance()
        {
            if (apiTimerInstance == null)
                apiTimerInstance = new APIPollingTimerInstance();

            return apiTimerInstance;
        }

        public void SetPollingTimer(int _time)
        {
            pollingTimer.Stop();
            pollingTimer.Interval = TimeSpan.FromSeconds(_time);
            pollingTimer.Tick += new EventHandler(OnPollingTimer_Tick);
            pollingTimer.Start();
        }

        private void OnPollingTimer_Tick(object sender, EventArgs e)
        {
            if (PollingTimerDone != null)
                PollingTimerDone(this, EventArgs.Empty);
        }
    }
}
