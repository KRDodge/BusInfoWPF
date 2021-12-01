using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using BusWpf.API;
using BusWpf.Data;

namespace BusWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();

            BusStationCSV busStationCSV = new BusStationCSV();
            busStationCSV.GetBusStationInfobyCSV();
        }

        private void OnBusStationInput(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                BusStationDataView.Items.Add(BusStationNameTextBox.Text);
                BusStationDataView.Items.Refresh();
            }
        }

        private void OnBusStationButtonClick(object sender, MouseButtonEventArgs e)
        {
            BusStationArrivalAPI arrivalAPIClass = new BusStationArrivalAPI();
            BusStationInfo busStationInfo = new BusStationInfo();

            int busStationID = busStationInfo.GetBusStationIDbyName((sender as ListViewItem).Content.ToString());
            arrivalAPIClass.FindStationInfoByID(busStationID);

            SetBusArrivalList();
        }

        private void OnDeleteStationButtonClick(object sender, RoutedEventArgs e)
        {
            BusStationDataView.Items.RemoveAt(BusStationDataView.SelectedIndex);
        }

        private void SetBusArrivalList()
        {
            if(BusArrivalList.Items.IsEmpty ==false)
                BusArrivalList.Items.Clear();

            ArrivalBusDataInstance arrivalBusDataInstance = ArrivalBusDataInstance.GetInstance();
            List<ArrivalBusData> dummyBusDataList = arrivalBusDataInstance.GetArrivalBusDataList();

            dummyBusDataList = SortBusArrival(dummyBusDataList);
            for(int i = 0; i < dummyBusDataList.Count; i++)
            {
                BusArrivalList.Items.Add(new DummyArrivalBusData() { busRoute = dummyBusDataList[i].GetRouteName(), busArrivalTime = dummyBusDataList[i].GetBusArrivalTime() });
                //BusArrivalList.Items.Add(dummyBusDataList[i].GetRouteName());
            }
        }

        private List<ArrivalBusData> SortBusArrival(List<ArrivalBusData> _dataList)
        {
            List<ArrivalBusData> sortDataList = _dataList;
            sortDataList.Sort((data1, data2) => data1.GetBusArrivalTime().CompareTo(data2.GetBusArrivalTime()));

            return sortDataList;
        }
    }
}
