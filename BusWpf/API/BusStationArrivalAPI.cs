using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.IO;
using BusWpf.Data;


namespace BusWpf.API
{
    public class BusStationArrivalAPI
    {
        //API 클래스 내부에서만 세팅됨
        public void FindStationInfoByID(int _busStationID)
        {
            if (_busStationID == -1)
                return;

            ArrivalBusDataInstance busDataList = ArrivalBusDataInstance.GetInstance();
            busDataList.ClearArrivalBusDataList();

            JToken ItemJToken = getBusArrivalJToken(_busStationID);
            if (ItemJToken == null)
                return;

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

                busDataList.AddArrivalBusDataList(busData);
            }
        }

        public void UpdateStationInfoByID(int _busStationID)
        {
            if (_busStationID == -1)
                return;

            ArrivalBusDataInstance busDataList = ArrivalBusDataInstance.GetInstance();

            JToken ItemJToken = getBusArrivalJToken(_busStationID);
            if (ItemJToken == null)
                return;

            foreach (JToken members in ItemJToken)
            {
                System.Diagnostics.Debug.WriteLine(members["rtNm"]);

                busDataList.SetBusArrivalTimeByBusName(members["rtNm"].ToString(),(int)members["neus1"]);
            }
        }

        private JToken getBusArrivalJToken(int _busStationID)
        {
            //정류장ID로 API에서 버스 정보 받아오기
            string url = "http://ws.bus.go.kr/api/rest/arrive/getLowArrInfoByStId"; // URL
            url += "?ServiceKey=" + "0dpmdc2FE6BlxQH1ApIaxodKsJobGA9yu7qG4lTln1Y9WAXFJEu48Lsn1avbVzt3wrr%2FvBuiWYZITzi%2Bc6u%2Fzg%3D%3D"; // Service Key
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
    }
}
