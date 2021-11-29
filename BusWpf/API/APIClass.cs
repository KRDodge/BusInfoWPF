using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.IO;


namespace BusWpf.API
{
    internal class APIClass
    {
        private List<int> StationIDList;
        private List<int> StationARS_IDList;
        private List<string> StationNameList;
        private List<float> StationXAxisList;
        private List<float> StationYAxisList;

        public List<int> GetStationID{ get {return StationIDList; } }
        public List<int> GetStationARS_ID() { return StationARS_IDList; }
        public List<string> GetStationName() { return StationNameList; }
        public List<float> GetStationXAxis() { return StationXAxisList; }
        public List<float> GetStationYAxis() { return StationYAxisList; }

        public void FindStationInfoByIndex(int stationIndex)
        {
            string stationID = StationIDList[stationIndex].ToString();

            string url = "http://ws.bus.go.kr/api/rest/arrive/getLowArrInfoByStId"; // URL
            url += "?ServiceKey=" + "0dpmdc2FE6BlxQH1ApIaxodKsJobGA9yu7qG4lTln1Y9WAXFJEu48Lsn1avbVzt3wrr%2FvBuiWYZITzi%2Bc6u%2Fzg%3D%3D"; // Service Key
            url += "&resultType=json";
            url += "&stId=";
            url += stationID;


            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            string results = string.Empty;
            HttpWebResponse response;
            using (response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                results = reader.ReadToEnd();
            }

            JObject jObject = JObject.Parse(results);
            JToken jToken = jObject.SelectToken("msgBody");
            JToken jToken2 = jToken.SelectToken("itemList");

            foreach (JToken members in jToken2)
            {
                Console.WriteLine(members["rtNm"]);
            }
        }

        public void BusCsv()
        {
            StreamReader reader = new StreamReader(File.OpenRead(@"C:\Users\media\Documents\GitHub\BusWpf\BusWpf\API\BusData.csv"));
            reader.ReadLine();

            StationIDList = new List<int>();
            StationARS_IDList = new List<int>();
            StationNameList = new List<string>();
            StationXAxisList = new List<float>();
            StationYAxisList = new List<float>();

            while (!reader.EndOfStream)
            {
                
                var line = reader.ReadLine();
                string[] values = line.Split(',');

                StationIDList.Add(Int32.Parse(values[0]));
                StationARS_IDList.Add((Int32.Parse(values[1])));
                StationNameList.Add(values[2]);
                StationXAxisList.Add(float.Parse(values[3]));
                StationYAxisList.Add(float.Parse(values[4]));
            }
        }

        public List<int> FindStationNumber(string stationName)
        {
            List<int> stationID = new List<int>();

            List<string> stationLameList = GetStationName();
            for (int i = 0; i < stationLameList.Count; i++)
            {
                if (stationLameList[i] == stationName)
                {
                    stationID.Add(i);
                }
            }

            return stationID;
        }
    }
}
