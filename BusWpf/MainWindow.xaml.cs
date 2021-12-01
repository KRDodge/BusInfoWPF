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

            //일단 Csv파일로 정류장 정보 읽어온다. 이것도 API있으면 그걸로 변경 예정
            BusStationCSV busStationCSV = new BusStationCSV();
            busStationCSV.GetBusStationInfobyCSV();
        }

        //사용자의 입력 받기 (enter 키 누르면 확인)
        private void OnBusStationInput(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                BusStationDataView.Items.Add(BusStationNameTextBox.Text);
                BusStationDataView.Items.Refresh();
                BusStationNameTextBox.Clear();
            }
        }

        //API에서 선택한 정류장의 버스도착정보 가져오기
        private void OnBusStationButtonClick(object sender, MouseButtonEventArgs e)
        {
            BusStationArrivalAPI arrivalAPIClass = new BusStationArrivalAPI();
            BusStationInfo busStationInfo = new BusStationInfo();

            int busStationID = busStationInfo.GetBusStationIDbyName((sender as ListViewItem).Content.ToString());
            arrivalAPIClass.FindStationInfoByID(busStationID);

            SetBusArrivalList();
        }

        //정류장목록 지우기
        private void OnDeleteStationButtonClick(object sender, RoutedEventArgs e)
        {
            BusStationDataView.Items.RemoveAt(BusStationDataView.SelectedIndex);
        }

        //도착하는 버스 리스트 UI에 출력
        private void SetBusArrivalList()
        {
            if(BusArrivalList.Items.IsEmpty ==false)
                BusArrivalList.Items.Clear();

            ArrivalBusDataInstance arrivalBusDataInstance = ArrivalBusDataInstance.GetInstance();
            List<ArrivalBusData> dummyBusDataList = arrivalBusDataInstance.GetArrivalBusDataList();
            if (dummyBusDataList == null)
                return;

            dummyBusDataList = SortBusArrival(dummyBusDataList);
            for(int i = 0; i < dummyBusDataList.Count; i++)
            {
                BusArrivalList.Items.Add(new ListViewArrivalBusData() { busRoute = dummyBusDataList[i].GetRouteName(), busArrivalTime = dummyBusDataList[i].GetBusArrivalTime().ToString() });
                //BusArrivalList.Items.Add(dummyBusDataList[i].GetRouteName());
            }
        }

        //도착하는 버스 오름차순 정렬
        private List<ArrivalBusData> SortBusArrival(List<ArrivalBusData> _dataList)
        {
            List<ArrivalBusData> sortDataList = _dataList;
            sortDataList.Sort((data1, data2) => data1.GetBusArrivalTime().CompareTo(data2.GetBusArrivalTime()));

            return sortDataList;
        }

        //ListView에서 busData선택한 busData 보기
        //busDataInstance에서 선택한 busData와 맞는 정보가져오기
        //세부정보 UI에 출력
        private void OnArrivalBusButtonClick(object sender, MouseButtonEventArgs e)
        {

            if (BusArrivalList.SelectedItem == null)
                return;

            ListViewArrivalBusData arrivingBus = BusArrivalList.SelectedItem as ListViewArrivalBusData;

            ArrivalBusDataInstance arrivalBusDataInstance = ArrivalBusDataInstance.GetInstance();
            ArrivalBusData arrivalbusData = arrivalBusDataInstance.FindBusInfoByRoute(arrivingBus.busRoute);
            if (arrivalbusData == null)
                return;
            

        }

    }
}
