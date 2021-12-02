using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusWpf.Data;

namespace BusWpf.API
{
    public class BusStationInfoInstance
    {
        private static BusStationInfoInstance busStationInfoInstance;
        private int stationID;

        public BusStationInfoInstance()
        {
            intitialize();
        }

        private void intitialize()
        {
            stationID = -1;
        }

        public static BusStationInfoInstance GetInstance()
        {
            if (busStationInfoInstance == null)
                busStationInfoInstance = new BusStationInfoInstance();
            
            return busStationInfoInstance; 
        }

        public void SetBusStationIDbyARSID(int _arsID)
        {
            BusStationDataInstance busStationData;
            busStationData = BusStationDataInstance.GetInstance();

            List<int> busStationNameList = busStationData.GetStationARSIDList();
            for (int i = 0; i < busStationNameList.Count; ++i)
            {
                if (busStationNameList[i] == _arsID)
                {
                    stationID = busStationData.GetStationIDList()[i];
                    break;
                }
            }
        }

        public int GetStationID()
        {
            return stationID;
        }
    }
}
