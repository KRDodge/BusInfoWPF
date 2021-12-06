//버스 정류장 정보 저장하는 클래스

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusApi.Api;
using BusApi.Data;

namespace BusWpf.API
{
    public class BusStationDataInstance
    {
        private static BusStationDataInstance busStationDataInstance;
        private BusStationData stationData;

        public BusStationDataInstance()
        {
            stationData = new BusStationData();
        }

        public static BusStationDataInstance GetInstance()
        {
            if (busStationDataInstance == null)
                busStationDataInstance = new BusStationDataInstance();
            return busStationDataInstance;
        }

        //API에 있는 메소드로 CSV 긁어옴
        public void SetBusStationDatabyCSV()
        {
            BusStationCSV busStationCSV = new BusStationCSV();
            stationData = busStationCSV.GetBusStationInfobyCSV();
        }


        //여기에 저장된 Data로 ARSID이용해서 정류장 실제 ID긁어옴
        public int GetBusStationIDbyARSID(int _arsID)
        {
            int stationID = -1;
            List<int> busStationNameList = stationData.GetStationARSIDList();
            for (int i = 0; i < busStationNameList.Count; ++i)
            {
                if (busStationNameList[i] == _arsID)
                {
                    stationID = stationData.GetStationIDList()[i];
                    break;
                }
            }
            return stationID;
        }
    }
}
