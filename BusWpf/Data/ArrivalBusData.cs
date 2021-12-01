using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusWpf.Data
{
    internal class ArrivalBusData
    {
        private string busRoute;
        private int busArrivalTime;

        private bool isLowBus;
        private BUSCOLOR busColor;
        private bool isFull;
        private int lastBusTime;
        private bool isRunning;


        public string GetRouteName() { return busRoute; }
        public int GetBusArrivalTime() { return busArrivalTime; }
        
        public bool IsLowBus() { return isLowBus; }
        public BUSCOLOR GetBusColor() { return busColor; }
        public bool IsFull() { return isFull; }
        public int GetLastBusTime() { return lastBusTime; }
        public bool IsRunning() { return isRunning; }


        public void SetBusRoute(string _busRoute) { busRoute = _busRoute; }
        public void SetBusArrivalTime(int _busArrivalTime) { busArrivalTime = _busArrivalTime; }

        public void SetLowBus(int _isLowBus) 
        {
            if(_isLowBus == 0)
            {
                isLowBus = false;
            }
            else if(_isLowBus == 1)
            {
                isLowBus = true;
            }
            else if (_isLowBus == 2)
            {
                isLowBus = false;
            }
        }
        public void SetBusColor(int _busColor) {  busColor = (BUSCOLOR)_busColor; }
        public void SetIsFull(bool _isFull) {  isFull = _isFull; }
        public void SetLastBusTime(int _busTime) {  lastBusTime = _busTime; }
        public void SetIsRunning(bool _isRunning) {  isRunning = _isRunning; }
    }
}
