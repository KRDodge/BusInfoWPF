//도착하는 버스 정보 서버에서 받아오고 parsing하는 클래스

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using BusApi.Data;

namespace BusApi.Api
{

    public class BusStationArrivalAPI
    {
        List<ArrivalBusData> busDataList;

        public BusStationArrivalAPI()
        {
            initialize();
        }

        private void initialize()
        {
            busDataList = new List<ArrivalBusData>();
        }

        //버스정류장ID로 버스정류장에 들어오는 버스 정보들 찾기 
        public List<ArrivalBusData> FindStationInfoByID(int _busStationID)
        {
            busDataList = new List<ArrivalBusData>();

            if (_busStationID == -1)
                return busDataList;

            JToken ItemJToken = getBusArrivalJToken(_busStationID);
            if (ItemJToken.Type == JTokenType.Null)
                return busDataList;

            foreach (JToken members in ItemJToken)
            {
                System.Diagnostics.Debug.WriteLine(members["rtNm"]);
                ArrivalBusData busData = new ArrivalBusData();

                busData.SetBusRoute(members["rtNm"].ToString());
                busData.SetBusArrivalTime((int)members["neus1"]);
                busData.SetBusArrivalMessage(members["arrmsg1"].ToString());
                busData.SetLowBus((int)members["busType1"]);
                busData.SetBusColor((int)members["routeType"]);
                busData.SetIsFull((int)members["full1"]);
                busData.SetIsLast((int)members["isLast1"]);

                busDataList.Add(busData);
            }

            return busDataList;
        }

        //버스정류장ID로 버스정류장에 들어오는 버스 정보 업데이트
        //기존에 있던 버스정류장에 도착하는 버스 정보 리스트 보내줘서 해당 리스트에 세팅
        public List<ArrivalBusData> UpdateStationInfoByID(int _busStationID, List<ArrivalBusData> _busDataList)
        {
            busDataList = _busDataList;

            if (_busStationID == -1)
                return busDataList;

            JToken ItemJToken = getBusArrivalJToken(_busStationID);
            if (ItemJToken.Type == JTokenType.Null)
                return busDataList;

            foreach (JToken members in ItemJToken)
            {
                System.Diagnostics.Debug.WriteLine(members["rtNm"]);

                setBusArrivalTimeByBusName(members["rtNm"].ToString(), members["arrmsg1"].ToString(), (int)members["neus1"]);
            }

            return busDataList;
        }

        //Json파일에서 필요한 정보 추출하는 함수
        private JToken getBusArrivalJToken(int _busStationID)
        {
            //정류장ID로 API에서 버스 정보 받아오기
            string url = "http://ws.bus.go.kr/api/rest/arrive/getLowArrInfoByStId"; // URL
            url += "?ServiceKey=" + "v04713J3B4C%2F2LnbYgHFa4G84CIQOYWuD%2FWYd6bBUrbyALrVYUrOKeQ5m%2B9nTq6zGzrORvWAslQ5N5toYipiuw%3D%3D"; // Service Key
            url += "&resultType=json";
            url += "&stId=";
            url += _busStationID;


            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            string results = string.Empty;
            HttpWebResponse response;
            using (response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                results = reader.ReadToEnd();
            }

            JObject busJOBcect = JObject.Parse(results);
            JToken BodyJToken = busJOBcect.SelectToken("msgBody");
            JToken ItemJToken = BodyJToken.SelectToken("itemList");

            return ItemJToken;
        }

        //노선번호에 맞는 도착시간 세팅
        public void setBusArrivalTimeByBusName(string _routeName, string _message, int _time)
        {
            for (int i = 0; i < busDataList.Count; ++i)
            {
                if (busDataList[i].GetRouteName() == _routeName)
                {
                    busDataList[i].SetBusArrivalTime(_time);
                    busDataList[i].SetBusArrivalMessage(_message);
                    return;
                }
            }
        }
    }
}
