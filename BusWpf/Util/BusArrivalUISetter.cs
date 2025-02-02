﻿//도착하는 버스 리스트 UI출력해주는 클래스
//여기서 시간순으로 정렬한다

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

            //버스 노선명에 특수기호가 들어간 것이 가끔 있다
            //ex 1000-1 1000-2 1000(퇴근) 1000(출근)
            //버스 ID에서 숫자만 추출해 ID로 놓자
            //1000-1 => 10001, 1000-2 => 10002번으로 바껴서 나중에 분별 가능
            //1000(출근) => 1000출근, 1000(퇴근) => 1000퇴근으로 변경
            //추후에 BorderName과 노선명 비교할 때 노선명에도 같은 작업 필요함
            //(현재 해당 Border의 Name을 사용하고 있는 부분은 없음)
            string extractBusID = _arrivalbusData.GetRouteName().ToString();
            extractBusID = extractBusID.Replace("(", "");
            extractBusID = extractBusID.Replace(")", "");
            extractBusID = extractBusID.Replace("-", "");

            Border border = new Border()
            {
                BorderBrush = new SolidColorBrush(color),
                BorderThickness = new Thickness(1),
                Width = 560,
                Height = 60,
                Name = "ID" + extractBusID,
            };
            border.Child = stack;

            return border;
        }
    }
}
