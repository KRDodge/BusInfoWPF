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
using BusWpf.Model;

namespace BusWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            APIClass apiClass = new APIClass();
            
            apiClass.BusCsv();
            List<int> stationIndex = apiClass.FindStationNumber("홍대입구역");

            //apiClass.FindStationInfoByIndex(stationIndex[0]);
            for (int i = 0; i < stationIndex.Count; i++)
            {
                apiClass.FindStationInfoByIndex(stationIndex[i]);
            }
        }
    }
}
