using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusWpf.Data;

namespace BusWpf.API
{
    public class BusStationInfo
    {
        public int GetBusStationIDbyARSID(int _arsID)
        {
            BusStationDataInstance busStationData;
            busStationData = BusStationDataInstance.GetInstance();

            int stationID = -1;

            List<int> busStationNameList = busStationData.GetStationARSIDList();
            for (int i = 0; i < busStationNameList.Count; ++i)
            {
                if (busStationNameList[i] == _arsID)
                {
                    stationID = busStationData.GetStationIDList()[i];
                    break;
                }
            }

            return stationID;
        }
    }
}
