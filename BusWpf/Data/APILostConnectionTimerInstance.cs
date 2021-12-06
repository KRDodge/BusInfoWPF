//연결 끊김 타이머 인스턴스
//해당 타이머가 60을 찍으면 연결실패
//PollingTimer가 돌았을 때 버스 데이터를 받아오지 못하면 시작

using System;
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
            lostConnectionTimer.Interval = TimeSpan.FromSeconds(60);
            lostConnectionTimer.Start();
        }

        public void StopLostConnectionTimer()
        {
            lostConnectionTimer.Stop();
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
        }

        public static APILostConnectionTimerInstance GetInstance()
        {
            if (apiTimerInstance == null)
                apiTimerInstance = new APILostConnectionTimerInstance();

            return apiTimerInstance;
        }
    }
}
