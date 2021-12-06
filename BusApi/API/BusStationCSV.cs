//처음 시작할 때 버스 정류장 정보 CSV파일 읽어오는 클래스

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BusApi.Data;

namespace BusApi.Api
{
                
    public class BusStationCSV
    {
        //CSV파일에서 버스 정보 읽어와서 return해주는 함수
        //CSV파일 설치 폴더에 같이 들어가도록 설정
        public BusStationData GetBusStationInfobyCSV()
        {
            BusStationData busStationData = new BusStationData();

            try
            {
                StreamReader reader = new StreamReader(File.OpenRead(Directory.GetCurrentDirectory() + @"\CSV\BusData.csv"));
                reader.ReadLine(); //csv에 있는 첫 열 날리기 (header정보)
                    
                List<int> stationIDList = new List<int>();
                List<int> stationARSIDList = new List<int>();
                List<string> stationNameList = new List<string>();

                while (!reader.EndOfStream)
                {

                    var line = reader.ReadLine();
                    string[] values = line.Split(',');

                    stationIDList.Add(int.Parse(values[0]));
                    stationARSIDList.Add(int.Parse(values[1]));
                    stationNameList.Add(values[2]);
                }

                busStationData.SetStationIDList(stationIDList);
                busStationData.SetStationARSIDList(stationARSIDList);
                busStationData.SetStationNameList(stationNameList);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return busStationData;
        }
    }
}
