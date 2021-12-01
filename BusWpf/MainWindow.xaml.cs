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
                //Button busStationButton = new Button();
                //busStationButton.Content = BusStationNameTextBox.Text;
                //busStationButton.Foreground = Brushes.Black;
                //busStationButton.Visibility = Visibility.Visible;
                //busStationButton.Click += new RoutedEventHandler(OnBusStationClick);


                //List<string> stringv = new List<string>();
                //stringv.Add(stationName);

                //ListViewItem listViewItem = new ListViewItem();
                //listViewItem.Content = stationName;

                //BusStationDataView.ItemsSource = stringv;
                BusStationDataView.Items.Add(BusStationNameTextBox.Text);
                BusStationDataView.Items.Refresh();

            }
        }

        private void OnBusStationClick(object sender, MouseButtonEventArgs e)
        {
            BusStationArrivalAPI arrivalAPIClass = new BusStationArrivalAPI();
            BusStationInfo busStationInfo = new BusStationInfo();

            int busStationID = busStationInfo.GetBusStationIDbyName((sender as ListViewItem).Content.ToString());
            arrivalAPIClass.FindStationInfoByID(busStationID);
        }
    }
}
