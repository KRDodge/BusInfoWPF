using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BusApi.Data;

namespace BusWpf.Util
{
    internal class BusArrivalUISetter
    {


        //도착하는 버스 오름차순 정렬
        public List<ArrivalBusData> SortBusArrival(List<ArrivalBusData> _dataList)
        {
            List<ArrivalBusData> sortDataList = _dataList;
            sortDataList.Sort((data1, data2) => data1.GetBusArrivalTime().CompareTo(data2.GetBusArrivalTime()));

            return sortDataList;
        }

        //선택한 버스 세부정보 UI에 출력
        public Border SetBusDetailUI(ArrivalBusData _arrivalbusData)
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
                Width = 560,
                Height = 60,
                Name = "ID" + _arrivalbusData.GetRouteName().ToString(),
            };
            border.Child = stack;

            return border;
        }
    }
}
