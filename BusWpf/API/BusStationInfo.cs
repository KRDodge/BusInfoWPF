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
        public int GetBusStationIDbyName(string _name)
        {
            if (_name == null)
                return -1;

            BusStationDataInstance busStationData;
            busStationData = BusStationDataInstance.GetInstance();

            int stationID = -1;

            List<string> busStationNameList = busStationData.GetStationNameList();
            for (int i = 0; i < busStationNameList.Count; ++i)
            {
                if (busStationNameList[i] == _name)
                {
                    stationID = busStationData.GetStationIDList()[i];
                    break;
                }
            }

            return stationID;
        }
    }
}
