using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BusApi.Data.Station;

namespace BusApi
{
namespace Api
{
namespace CSV
{
                
    public class BusStationCSV
    {
        public BusStationData GetBusStationInfobyCSV()
        {
            BusStationData busStationData = new BusStationData();

            try
            {
                StreamReader reader = new StreamReader(File.OpenRead(@"C:\Users\media\Documents\GitHub\BusInfoWPF\BusWpf\API\BusData.csv"));
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
}
}
