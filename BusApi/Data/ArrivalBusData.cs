using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusApi.Data
{

    public class ArrivalBusData
    {
        private string busRoute;    //버스 번호
        private string busArrivalMessage; //도착 메세지 (이걸로 운행여부 및 도착 시간 추출)
        private int busArrivalTime; //첫번째 도착예정 시간

        private bool isLowBus;      //저상버스
        private BUSCOLOR busColor;  //버스 색
        private bool isFull;        //만차
        private bool isLast;        //막차
        private RUNNINGSTATUS runningStatus;     //운행여부


        public string GetRouteName() { return busRoute; }
        public int GetBusArrivalMessage() { return busArrivalTime; }
        public int GetBusArrivalTime() { return busArrivalTime; }

        public bool IsLowBus() { return isLowBus; }
        public BUSCOLOR GetBusColor() { return busColor; }
        public bool IsFull() { return isFull; }
        public bool IsLast() { return isLast; }
        public RUNNINGSTATUS IsRunning() { return runningStatus; }



        public void SetBusRoute(string _busRoute) { busRoute = _busRoute; }
        public void SetBusArrivalMessage(string _busArrival)
        {
            busArrivalMessage = _busArrival;

            if (busArrivalMessage == "운행종료")
            {
                SetBusArrivalTime(int.MaxValue);
                SetIsRunning(RUNNINGSTATUS.CLOSED);
            }
            else if (busArrivalMessage == "출발대기")
            {
                SetBusArrivalTime(int.MaxValue - 1);
                SetIsRunning(RUNNINGSTATUS.WAITING);
            }
            else
            {
                SetIsRunning(RUNNINGSTATUS.RUNNING);
            }
        }
        public void SetBusArrivalTime(int _busArrival) { busArrivalTime = _busArrival; }

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

        public void SetBusColor(int _busColor)
        {
            string busRouteColor = null;
            if (busRoute.Length != 0)
                busRouteColor = busRoute.Substring(0, 1);

            if (busRouteColor == "M") //M버스면 노선상관없이 광역급행버스 색
            {
                busColor = BUSCOLOR.MSKYBLUE;
            }
            else if (busRouteColor == "G") //G버스면 노선상관없이 광역버스 색
            {
                busColor = BUSCOLOR.RED;
            }
            else if (busRouteColor == "N") //G버스면 노선상관없이 심야버스 색
            {
                busColor = BUSCOLOR.NSKYBLUE;
            }
            else
            {
                busColor = (BUSCOLOR)_busColor;
            }
        }

        public void SetIsFull(int _isFull)
        {
            if (_isFull == 0)
                isFull = false;
            else
                isFull = true;
        }

        public void SetIsLast(int _isLast)
        {
            if (_isLast == 0)
                isLast = false;
            else
                isLast = true;
        }

        public void SetIsRunning(RUNNINGSTATUS _running)
        {
            runningStatus = _running;
        }


        /// /////생성자////////
        public ArrivalBusData()
        {
            initialize();
        }

        private void initialize()
        {
            busRoute = "";
            busArrivalTime = int.MaxValue;

            isLowBus = false;
            busColor = BUSCOLOR.NONE;
            isFull = false;
            runningStatus = RUNNINGSTATUS.NONE;
        }

    }
}