using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusWpf.Data;
using System.IO;

namespace BusWpf.API
{
    internal class BusStationCSV
    {
        public void GetBusStationInfobyCSV()
        {
            BusStationDataInstance busStationData;
            busStationData = BusStationDataInstance.GetInstance();

            try
            {
                StreamReader reader = new StreamReader(File.OpenRead(@"C:\Users\media\Documents\GitHub\BusInfoWPF\BusWpf\API\BusData.csv"));
                reader.ReadLine(); //csv에 있는 첫 열 날리기 (header정보)

                List<int> stationIDList = new List<int>();
                List<string> stationNameList = new List<string>();

                while (!reader.EndOfStream)
                {

                    var line = reader.ReadLine();
                    string[] values = line.Split(',');

                    stationIDList.Add(Int32.Parse(values[0]));
                    stationNameList.Add(values[2]);
                }

                busStationData.SetStationIDList(stationIDList);
                busStationData.SetStationNameList(stationNameList);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
