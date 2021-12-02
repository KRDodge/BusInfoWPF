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
            if (_arsID == null)
                return -1;

            BusStationDataInstance busStationData;
            busStationData = BusStationDataInstance.GetInstance();

            int stationID = -1;

            List<int> busStationNameList = busStationData.GetStationARSIDList();
            for (int i = 0; i < busStationNameList.Count; ++i)
            {
                if (busStationNameList[i] == arsID)
                {
                    stationID = busStationData.GetStationIDList()[i];
                    break;
                }
            }

            return stationID;
        }
    }
}
