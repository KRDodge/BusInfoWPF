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
using BusWPFAPI.BusStationAPI.Arrival;
using BusWPFAPI.BusWPFData.Arrival;

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
            BusStationDataInstance busStationData = new BusStationDataInstance();
            busStationData.SetBusStationDatabyCSV();

            APIPollingTimerInstance pollingTimer = APIPollingTimerInstance.GetInstance();
            pollingTimer.PollingTimerDone += OnPollingTimer;

            APILostConnectionTimerInstance lostConnectionTimer = APILostConnectionTimerInstance.GetInstance();
            lostConnectionTimer.LostConnectionTimerDone += OnLostConnectionTimer;

            setClockTime();
        }

        //사용자의 입력 받기 (enter 키 누르면 확인)
        private void OnAddStationButtonClick(object sender, RoutedEventArgs e)
        {
            if (StationDescriptionTextBlock.Text == "" || StationIDTextBlock.Text == "")
                return;

            Border border = setStationName();

            BusStationList.Items.Add(border);
            BusStationList.Items.Refresh();
            StationDescriptionTextBlock.Clear();
            StationIDTextBlock.Clear();
        }

        //API에서 선택한 정류장의 버스도착정보 가져오기
        private void OnBusStationButtonClick(object sender, MouseButtonEventArgs e)
        {
            ArrivalBusDataInstance arrivalAPIClass = ArrivalBusDataInstance.GetInstance();
            BusStationDataInstance busStationInfo = BusStationDataInstance.GetInstance();

            string busStationARSString = (BusStationList.Items.GetItemAt(BusStationList.SelectedIndex) as Border).Name.ToString(); //이게 최선인가? 나중에 방법 더 찾아보기
            int busStationARSID = 0;
            try
            {
                busStationARSID = int.Parse(busStationARSString.Substring(2, busStationARSString.Length - 2)); //ID Prefix 부분 잘라주기
            }
            catch
            {
                //이렇게 할게 아니라 그냥 문자 입력 안되게 막아버리는게 나을듯?
                StationIDTextBlock.Text = "잘못 된 역 번호"; 
            }
            int busStationID = busStationInfo.GetBusStationIDbyARSID(busStationARSID);
            bool connected = arrivalAPIClass.FindStationInfoByID(busStationID);

            APILostConnectionTimerInstance lostConnectionTimer = APILostConnectionTimerInstance.GetInstance();
            if (connected)
                lostConnectionTimer.StopLostConnectionTimer();
            else
                lostConnectionTimer.StartLostConnectionTimer();


            updateBusArrivalList();

            APIPollingTimerInstance pollingTimer = APIPollingTimerInstance.GetInstance();
            int pollingTime;
            try
            {
                pollingTime = int.Parse(PollingTextBlock.Text.ToString());
            }
            catch(Exception ex)
            {
                pollingTime = 60;
            }
            pollingTimer.SetPollingTimer(pollingTime);
        }

        //polling 버튼 선택
        private void OnPollingButtonClick(object sender, RoutedEventArgs e)
        {
            APIPollingTimerInstance pollingTimer = APIPollingTimerInstance.GetInstance();
            int pollingTime;
            try
            {
                pollingTime = int.Parse(PollingTextBlock.Text.ToString());
            }
            catch (Exception ex)
            {
                pollingTime = 60;
            }
            pollingTimer.SetPollingTimer(pollingTime);
        }

        //정류장목록 지우기
        private void OnDeleteStationButtonClick(object sender, RoutedEventArgs e)
        {
            BusStationList.Items.RemoveAt(BusStationList.SelectedIndex);
            APIPollingTimerInstance pollingTimer = APIPollingTimerInstance.GetInstance();
            pollingTimer.StopPollingTimer(); //polling timer 돌면서 문제 일으킬수도 있으니까 일단 stop
        }

        public void OnPollingTimer(object sender, EventArgs e)
        {
            ArrivalBusDataInstance arrivalAPIClass = ArrivalBusDataInstance.GetInstance();
            BusStationDataInstance busStationInfo = BusStationDataInstance.GetInstance();

            string busStationARSString = (BusStationList.Items.GetItemAt(BusStationList.SelectedIndex) as Border).Name.ToString(); //이게 최선인가? 나중에 방법 더 찾아보기
            int busStationARSID = int.Parse(busStationARSString.Substring(2, busStationARSString.Length - 2)); //ID Prefix 부분 잘라주기
            int busStationID = busStationInfo.GetBusStationIDbyARSID(busStationARSID);
            bool connected = arrivalAPIClass.UpdateStationInfoByID(busStationID);

            APILostConnectionTimerInstance lostConnectionTimer = APILostConnectionTimerInstance.GetInstance();
            if (connected)
                lostConnectionTimer.StopLostConnectionTimer();
            else
                lostConnectionTimer.StartLostConnectionTimer();

            updateBusArrivalList();
        }

        private void OnLostConnectionTimer(object sender, EventArgs e)
        {
            if (BusArrivalList.Items.IsEmpty == false)
                BusArrivalList.Items.Clear();

            PollingTextBlock.Text = "통신불가";
        }

        private Border setStationName()
        {
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
                Name = "ID" + StationIDTextBlock.Text,
            };
            border.Child = stack;

            return border;
        }

        private void updateBusArrivalList()
        {
            if (BusArrivalList.Items.IsEmpty == false)
                BusArrivalList.Items.Clear();

            ArrivalBusDataInstance arrivalBusDataInstance = ArrivalBusDataInstance.GetInstance();
            List<ArrivalBusData> dummyBusDataList = arrivalBusDataInstance.GetArrivalBusDataList();
            if (dummyBusDataList == null)
                return;

            BusArrivalUISetter setter = new BusArrivalUISetter();

            dummyBusDataList = setter.SortBusArrival(dummyBusDataList);
            for (int i = 0; i < dummyBusDataList.Count; i++)
            {
                if (dummyBusDataList[i] == null)
                    return;

                Border border = setter.SetBusDetailUI(dummyBusDataList[i]);
                BusArrivalList.Items.Add(border);
            }
        }

        private void setClockTime()
        {
            DateTime time = DateTime.Now;

            string timeText = null;
            int hour = time.Hour;
            if (hour < 12)
            {
                timeText = " 오전 " + hour.ToString("00") + ":" + time.Minute.ToString("00");    
            }
            else
            {
                hour -= 12;
                timeText = " 오후 " + hour.ToString("00") + ":" + time.Minute.ToString("00");
            }

            Clock.Text = time.Year.ToString("00") + "-" + time.Month.ToString("00") + "-" + time.Day.ToString("00") + timeText;
        }
    }
}
