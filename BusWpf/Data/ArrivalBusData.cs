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
        private int firstBusTime;
        private int lastBusTime;
        private bool isRunning;


        public string GetRouteName() { return busRoute; }
        public int GetBusArrivalTime() { return busArrivalTime; }

        public bool IsLowBus() { return isLowBus; }
        public BUSCOLOR GetBusColor() { return busColor; }
        public bool IsFull() { return isFull; }
        public int GetFirstBusTime() { return firstBusTime; }
        public int GetLastBusTime() { return lastBusTime; }
        public bool IsRunning() { return isRunning; }

        public void SetBusRoute(string _busRoute) { busRoute = _busRoute; }
        public void SetBusArrivalTime(int _busArrivalTime) { busArrivalTime = _busArrivalTime; }

        public void SetLowBus(int _isLowBus)
        {
            if (_isLowBus == 0)
            {
                isLowBus = false;
            }
            else if (_isLowBus == 1)
            {
                isLowBus = true;
            }
            else if (_isLowBus == 2)
            {
                isLowBus = false;
            }
        }
        public void SetBusColor(int _busColor) { busColor = (BUSCOLOR)_busColor; }
        public void SetIsFull(int _isFull)
        {
            if (_isFull == 0)
                isFull = false;
            else
                isFull = true;
        }

        public void SetFirstBusTime(string _busTime)
        {
            if (_busTime.Length == 14)
                _busTime = _busTime.Substring(7, 4);

            try
            {
                firstBusTime = int.Parse(_busTime);
            }
            catch (Exception e)
            {
                firstBusTime = 0;
            }
        }
        public void SetLastBusTime(string _busTime)
        {
            if (_busTime.Length == 14)
                _busTime = _busTime.Substring(7, 4);

            try
            {
                firstBusTime = int.Parse(_busTime);
            }
            catch (Exception e)
            {
                firstBusTime = 0;
            }
        }

        public ArrivalBusData()
        {
            initialize();
        }

        private void initialize()
        {
            busRoute = "";
            busArrivalTime = 0;

            isLowBus = false;
            busColor = BUSCOLOR.NONE;
            isFull = false;
            firstBusTime = 0;
            lastBusTime = 0;
            isRunning = false;
        }

    }

    internal class DummyArrivalBusData
    {

        public string busRoute { get; set; }
        public int busArrivalTime { get; set; }
    }
}
