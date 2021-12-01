using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusWpf.Data
{
    internal class ArrivalBusDataInstance
    {
        private List<ArrivalBusData> arrivalBusDataList;

        private static ArrivalBusDataInstance arrivalBusDataInstance;

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

        public void SetBusArrivalTimeByBusName(string _routeName, int _time)
        {
            for(int i = 0; i < arrivalBusDataList.Count; ++i)
            {
                if(arrivalBusDataList[i].GetRouteName() == _routeName)
                {
                    arrivalBusDataList[i].SetBusArrivalTime(_time);
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
    }
}
