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
using BusWpf.Util;

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
        private void OnAddStationButtonClick(object sender, RoutedEventArgs e)
        {
            if (StationDescriptionTextBlock.Text == "" || StationIDTextBlock.Text == "")
                return;
            
            StackPanel stack = new StackPanel();
            TextBlock desTextBlock = new TextBlock();
            desTextBlock.FontSize = 20;
            desTextBlock.Text = StationDescriptionTextBlock.Text;
            TextBlock idTextBlock = new TextBlock();
            idTextBlock.Text = "ID " + StationIDTextBlock.Text;
            idTextBlock.FontSize = 20;

            stack.Children.Add(desTextBlock);
            stack.Children.Add(idTextBlock);

            Border border = new Border()
            {
                BorderBrush = new SolidColorBrush(Color.FromRgb(50, 50, 50)),
                BorderThickness = new Thickness(1),
                Width = 260,
                Height = 60,
                Name = "ID"+ StationIDTextBlock.Text,
            };
            border.Child = stack;

            BusStationList.Items.Add(border);
            BusStationList.Items.Refresh();
            StationDescriptionTextBlock.Clear();
            StationIDTextBlock.Clear();
        }

        //API에서 선택한 정류장의 버스도착정보 가져오기
        private void OnBusStationButtonClick(object sender, MouseButtonEventArgs e)
        {
            BusStationArrivalAPI arrivalAPIClass = new BusStationArrivalAPI();
            BusStationInfo busStationInfo = new BusStationInfo();

            string busStationARSString = (BusStationList.Items.GetItemAt(BusStationList.SelectedIndex) as Border).Name.ToString(); //이게 최선인가? 나중에 방법 더 찾아보기
            int busStationARSID = int.Parse(busStationARSString.Substring(2, busStationARSString.Length - 2)); //ID Prefix 부분 잘라주기
            int busStationID = busStationInfo.GetBusStationIDbyARSID(busStationARSID);
            arrivalAPIClass.FindStationInfoByID(busStationID);

            SetBusArrivalList();
        }

        //정류장목록 지우기
        private void OnDeleteStationButtonClick(object sender, RoutedEventArgs e)
        {
            BusStationList.Items.RemoveAt(BusStationList.SelectedIndex);
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
                if (dummyBusDataList[i] == null)
                    return;

                SetBusDetailUI(dummyBusDataList[i]);

                Border border = SetBusDetailUI(dummyBusDataList[i]);
                BusArrivalList.Items.Add(border);
            }
        }

        //도착하는 버스 오름차순 정렬
        private List<ArrivalBusData> SortBusArrival(List<ArrivalBusData> _dataList)
        {
            List<ArrivalBusData> sortDataList = _dataList;
            sortDataList.Sort((data1, data2) => data1.GetBusArrivalTime().CompareTo(data2.GetBusArrivalTime()));

            return sortDataList;
        }

        //선택한 버스 세부정보 UI에 출력
        private Border SetBusDetailUI(ArrivalBusData _arrivalbusData)
        {
            BusInfoUISetter setter = new BusInfoUISetter();
            RichTextBox busInfoTextBlock = setter.GetBusInfoText(_arrivalbusData);
            TextBlock arrivalTime = setter.GetBusArrivalText(_arrivalbusData);

            StackPanel stack = new StackPanel();
            stack.Children.Add(busInfoTextBlock);
            stack.Children.Add(arrivalTime);

            Color color;
            if (_arrivalbusData.GetBusArrivalTime() < 60)
                color = Color.FromRgb(255, 0, 0);
            else
                color = Color.FromRgb(50, 50, 50);
            
            Border border = new Border()
            {
                BorderBrush = new SolidColorBrush(color),
                BorderThickness = new Thickness(1),
                Width = 260,
                Height = 60,
                Name = "ID" + StationIDTextBlock.Text,
            };

            return border;
        }

    }
}
