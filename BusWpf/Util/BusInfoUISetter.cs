using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BusWpf.Data;
using System.Windows.Documents;

namespace BusWpf.Util
{
    internal class BusInfoUISetter
    {
        //버스 노선명 + 저상, 만차, 막차 여부를 묶어 리턴해주는 함수
        //저상 만차 막차의 경우 삭제하지 않고 White처리해서 안보이도록 수정
        public RichTextBox GetBusInfoText(ArrivalBusData _arrivalbusData)
        {
            RichTextBox busInfoTextBlock = new RichTextBox();
            busInfoTextBlock.FontSize = 20;
            BUSCOLOR busColor = _arrivalbusData.GetBusColor();
            Color color = GetBusColor(busColor);
            busInfoTextBlock.Foreground = new SolidColorBrush(color);

            TextBlock arrivalTime = new TextBlock();
            arrivalTime.FontSize = 20;

            TextRange routeTextRange = new TextRange(busInfoTextBlock.Document.ContentEnd, busInfoTextBlock.Document.ContentEnd);
            routeTextRange.Text = _arrivalbusData.GetRouteName() + " ";
            routeTextRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Blue);
            routeTextRange.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);

            TextRange lowTextRange = new TextRange(busInfoTextBlock.Document.ContentEnd, busInfoTextBlock.Document.ContentEnd);
            lowTextRange.Text = "저상 ";
            if (_arrivalbusData.IsLowBus())
                lowTextRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
            else
                lowTextRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.White); //space유지를 위해 문자열 지우는게 아닌 white처리해서 안보이게함

            TextRange fullTextRange = new TextRange(busInfoTextBlock.Document.ContentEnd, busInfoTextBlock.Document.ContentEnd);
            lowTextRange.Text = "만차 ";
            if (_arrivalbusData.IsFull())
                fullTextRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
            else
                fullTextRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.White); //space유지를 위해 문자열 지우는게 아닌 white처리해서 안보이게함

            TextRange lastTextRange = new TextRange(busInfoTextBlock.Document.ContentEnd, busInfoTextBlock.Document.ContentEnd);
            lowTextRange.Text = "막차 ";
            if (_arrivalbusData.IsLast())
                fullTextRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
            else
                fullTextRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.White); //space유지를 위해 문자열 지우는게 아닌 white처리해서 안보이게함

            return busInfoTextBlock;
        }

        //버스 도착시간 리턴해주는 함수
        public TextBlock GetBusArrivalText(ArrivalBusData _arrivalbusData)
        {
            TextBlock arrivalTime = new TextBlock();

            //버스 시간이 int max값이면 정상 운행이 아니니 확인
            string busArriveTime = null;
            if (_arrivalbusData.IsRunning() == RUNNINGSTATUS.CLOSED)
                busArriveTime = "운행종료";
            else if (_arrivalbusData.IsRunning() == RUNNINGSTATUS.WAITING)
                busArriveTime = "출발대기";
            else if (_arrivalbusData.IsRunning() == RUNNINGSTATUS.RUNNING)
            {
                int minute = _arrivalbusData.GetBusArrivalTime() / 60;
                int second = _arrivalbusData.GetBusArrivalTime() % 60;
                busArriveTime += minute.ToString();
                busArriveTime += "분 후 도착";
            }
            arrivalTime.Text = busArriveTime;

            return arrivalTime;
        }

        //버스 색 지정해주는 함수.
        //지정된 색을 enum으로 옮기고 parsing할때 들어오는 int 일일이 enum이랑 매칭시킬까 고민중
        public Color GetBusColor(BUSCOLOR _busColor)
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
    }
}
