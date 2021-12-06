using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusApi.Data.Arrival;
using BusApi.Api.Station;

//도착하는 버스 목록을 관리하는 클래스

namespace BusWpf.Data
{
    internal class ArrivalBusDataInstance
    { 
        private static ArrivalBusDataInstance arrivalBusDataInstance;
        private List<ArrivalBusData> arrivalBusDataList;

        public ArrivalBusDataInstance()
        {
            initialize();
        }

        private void initialize()
        {
            arrivalBusDataList = new List<ArrivalBusData>();
        }

        public static ArrivalBusDataInstance GetInstance()
        {
            if (arrivalBusDataInstance == null)
                arrivalBusDataInstance = new ArrivalBusDataInstance();

            return arrivalBusDataInstance;
        }

        public List<ArrivalBusData> GetArrivalBusDataList()
        {
            return arrivalBusDataList;
        }

        public void AddArrivalBusDataList(ArrivalBusData _data)
        {
            arrivalBusDataList.Add(_data);
        }
    
        public void ClearArrivalBusDataList()
        {
           arrivalBusDataList.Clear();
        }

        public void SetBusArrivalTimeByBusName(string _routeName, string _message, int _time)
        {
            for(int i = 0; i < arrivalBusDataList.Count; ++i)
            {
                if(arrivalBusDataList[i].GetRouteName() == _routeName)
                {
                    arrivalBusDataList[i].SetBusArrivalTime(_time);
                    arrivalBusDataList[i].SetBusArrivalMessage(_message);
                    return;
                }
            }
        }

        public ArrivalBusData FindBusInfoByRoute(string _busRoute)
        {
            ArrivalBusData arrivalBusData = null;
            for(int i = 0; i < arrivalBusDataList.Count; ++i)
            {
                if(arrivalBusDataList[i].GetRouteName() == _busRoute)
                {
                    arrivalBusData = arrivalBusDataList[i];
                }
            }
            return arrivalBusData;
        }

        //API에서 StationID로 도착예정버스들 받아온 후 arrivalBusDataList에 저장
        public bool FindStationInfoByID(int _stationID)
        {
            bool succes = false; //coonection 잘 됐는지 확인
            arrivalBusDataList.Clear();
            BusStationArrivalAPI arrivalAPI = new BusStationArrivalAPI();
            arrivalBusDataList = arrivalAPI.FindStationInfoByID(_stationID);

            if (arrivalBusDataList.Count > 0)
                succes = true;

            return succes;
        }

        //API에서 StationID로 도착예정버스들 받아온 후 arrivalBusDataList에 저장
        public bool UpdateStationInfoByID(int _stationID)
        {
            bool succes = false; //coonection 잘 됐는지 확인
            BusStationArrivalAPI arrivalAPI = new BusStationArrivalAPI();
            arrivalBusDataList = arrivalAPI.UpdateStationInfoByID(_stationID, arrivalBusDataList);

            if (arrivalBusDataList.Count > 0)
                succes = true;

            return succes;
        }
    }
}
