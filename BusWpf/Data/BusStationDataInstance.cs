using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusWpf.Data
{
    internal class BusStationDataInstance
    {
        private List<int> stationIDList;
        private List<int> stationARSIDList;
        private List<string> stationNameList;

        public List<int> GetStationIDList() { return stationIDList; }
        public List<int> GetStationARSIDList() { return stationARSIDList; }
        public List<string> GetStationNameList() { return stationNameList; }

        public void SetStationIDList(List<int> _stationIDList) { stationIDList = _stationIDList; }
        public void SetStationARSIDList(List<int> _stationARSIDList) { stationARSIDList = _stationARSIDList; }
        public void SetStationNameList(List<string> _stationNameList) { stationNameList = _stationNameList; }

        private static BusStationDataInstance busStationDataInstance;

        public static BusStationDataInstance GetInstance()
        {
            if (busStationDataInstance == null)
                busStationDataInstance = new BusStationDataInstance();
            return busStationDataInstance;
        }
    }
}
