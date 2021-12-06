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

        public void SetBusStationDatabyCSV()
        {
            BusStationCSV busStationCSV = new BusStationCSV();
            stationData = busStationCSV.GetBusStationInfobyCSV();
        }

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
