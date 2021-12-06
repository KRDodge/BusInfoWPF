//폴링(리프레시) 타이머 클래스
//사용자가 설정한 타이머 시간이 되면 정류장에 도착하는 버스 정보 Update

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

        public void StopPollingTimer()
        {
            pollingTimer.Stop();
        }

        private void OnPollingTimer_Tick(object sender, EventArgs e)
        {
            if (PollingTimerDone != null)
                PollingTimerDone(this, EventArgs.Empty);
        }
    }
}
