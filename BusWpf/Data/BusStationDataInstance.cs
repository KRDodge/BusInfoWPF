using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusWPFAPI.BusWPFData.Station;

namespace BusWpf.Data
{
    internal class BusStationDataInstance
    {
        private static BusStationDataInstance busStationDataInstance;
        private BusStationData stationData;

        public BusStationDataInstance()
        {
            initialize();
        }

        private void initialize()
        {
            stationData = new BusStationData();
        }

        public static BusStationDataInstance GetInstance()
        {
            if (busStationDataInstance == null)
                busStationDataInstance = new BusStationDataInstance();
            return busStationDataInstance;
        }

        public void SetBusStationData(BusStationData _data)
        {
            stationData = _data;
        }
    }
}
