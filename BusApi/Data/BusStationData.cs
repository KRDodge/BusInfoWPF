//버스 정류장 정보모음 클래스
//CSV파일 긁어온거 저장
//일반인이 버스정류장에서 보는 ID와 API에서 처리하는 ID가 다름
//일반인이 보는 ID는 ARSID ex)종로2가사거리의 ARSID는 01001, 실제 ID는 100000001

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusApi.Data
{ 

    public class BusStationData
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

        public BusStationData()
        {
            stationIDList = new List<int>();
            stationARSIDList = new List<int>();
            stationNameList = new List<string>();
        }
    }
}
