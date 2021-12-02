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
                //버스 시간이 int max값이면 정상 운행이 아니니 확인
                string busArriveTime = null;
                if (dummyBusDataList[i].IsRunning() == RUNNINGSTATUS.CLOSED)
                    busArriveTime = "운행종료";
                else if(dummyBusDataList[i].IsRunning() == RUNNINGSTATUS.WAITING)
                    busArriveTime = "출발대기";
                else if(dummyBusDataList[i].IsRunning() == RUNNINGSTATUS.RUNNING)
                {
                    int minute = dummyBusDataList[i].GetBusArrivalTime() / 60;
                    int second = dummyBusDataList[i].GetBusArrivalTime() % 60;
                    busArriveTime += minute.ToString();
                    busArriveTime += "분 ";
                    busArriveTime += second.ToString();
                    busArriveTime += "초 후 도착 ";
                }

                BusArrivalList.Items.Add(new ListViewArrivalBusData() 
                { 
                    busRoute = dummyBusDataList[i].GetRouteName(), 
                    busArrivalTime = busArriveTime
                });
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
        private void OnArrivalBusButtonClick(object sender, MouseButtonEventArgs e)
        {

            if (BusArrivalList.SelectedItem == null)
                return;

            ListViewArrivalBusData arrivingBus = BusArrivalList.SelectedItem as ListViewArrivalBusData;

            ArrivalBusDataInstance arrivalBusDataInstance = ArrivalBusDataInstance.GetInstance();
            ArrivalBusData arrivalbusData = arrivalBusDataInstance.FindBusInfoByRoute(arrivingBus.busRoute);
            if (arrivalbusData == null)
                return;

            SetBusDetailUI(arrivalbusData);
        }

        //선택한 버스 세부정보 UI에 출력
        private void SetBusDetailUI(ArrivalBusData _arrivalbusData)
        {
            //string lowLabelString = _arrivalbusData.IsLowBus() ? "저상버스" : "일반버스";
            //LowLabel.Content = lowLabelString;


            //BUSCOLOR busColor = _arrivalbusData.GetBusColor();
            //Color color = getBusColor(busColor);   
            //ColorLabel.Stroke = new SolidColorBrush(color);
            //ColorLabel.Fill = new SolidColorBrush(color);


            //string fullLabelString = _arrivalbusData.IsFull() ? "만차" : "잔여좌석 있음";
            //FullLabel.Content = fullLabelString;


            //string lastLabelString = _arrivalbusData.IsLast() ? "막차" : "정상운행";
            //if (_arrivalbusData.IsRunning() == RUNNINGSTATUS.CLOSED)
            //{
            //    LastLabel.Content = "운행종료";
            //    EndLabel.Content = "운행종료";
            //}
            //else
            //{
            //    LastLabel.Content = lastLabelString;

            //    EndLabel.Content = "정상운행중";
            //}
        }

        private Color getBusColor(BUSCOLOR _busColor)
        {
            Color color;

            if (_busColor == BUSCOLOR.WHITE)
                color = (Color)ColorConverter.ConvertFromString("#FFFFFF");
            else if (_busColor == BUSCOLOR.AIRPORTBLUE)
                color = (Color)ColorConverter.ConvertFromString("#3D5BAB");
            else if (_busColor == BUSCOLOR.VILLAGEGREEN)
                color = (Color)ColorConverter.ConvertFromString("#53B332");
            else if (_busColor == BUSCOLOR.BLUE)
                color = (Color)ColorConverter.ConvertFromString("#0068B7");
            else if (_busColor == BUSCOLOR.GREEN)
                color = (Color)ColorConverter.ConvertFromString("#53B332");
            else if (_busColor == BUSCOLOR.YELLOW)
                color = (Color)ColorConverter.ConvertFromString("#F2B70A");
            else if (_busColor == BUSCOLOR.RED)
                color = (Color)ColorConverter.ConvertFromString("#E60012");
            else if (_busColor == BUSCOLOR.ICNBLUE)
                color = (Color)ColorConverter.ConvertFromString("#0068B7");
            else if (_busColor == BUSCOLOR.GYUGREEN)
                color = (Color)ColorConverter.ConvertFromString("#009E96");
            else if (_busColor == BUSCOLOR.MSKYBLUE)
                color = (Color)ColorConverter.ConvertFromString("#006896");
            else if (_busColor == BUSCOLOR.NSKYBLUE)
                color = (Color)ColorConverter.ConvertFromString("#3D5BAB");
            else if (_busColor == BUSCOLOR.DEPRECATED)
                color = (Color)ColorConverter.ConvertFromString("#FFFFFF");
            else if (_busColor == BUSCOLOR.NONE)
                color = (Color)ColorConverter.ConvertFromString("#FFFFFF");

            return color;
        }

        private void BusArrivalList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
