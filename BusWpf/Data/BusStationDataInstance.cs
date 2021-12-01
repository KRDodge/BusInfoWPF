using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusWpf.Data
{
    internal class BusStationDataInstance
    {
        private List<int> StationIDList;
        private List<string> StationNameList;

        public List<int> GetStationIDList() { return StationIDList; }
        public List<string> GetStationNameList() { return StationNameList; }

        public void SetStationIDList(List<int> _stationIDList) { StationIDList = _stationIDList; }
        public void SetStationNameList(List<string> _stationNameList) { StationNameList = _stationNameList; }

        private static BusStationDataInstance busStationDataInstance;

        public static BusStationDataInstance GetInstance()
        {
            if (busStationDataInstance == null)
                busStationDataInstance = new BusStationDataInstance();
            return busStationDataInstance;
        }
    }
}
