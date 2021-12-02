﻿namespace BusWpf.Data
{
    public enum BUSCOLOR
    {
        WHITE = 0,      //공용버스 
        AIRPORTBLUE,    //공항버스
        VILLAGEGREEN,   //마을버스
        BLUE,           //간선버스
        GREEN,          //지선버스
        YELLOW,         //순환버스
        RED,            //광역버스 (노선명 앞에 G들어가는 버스 포함)
        ICNBLUE,        //인천버스 (인천버스여도 앞에 M이 붙으면 광역급행버스(하늘) 색)
        GYUGREEN,       //경기버스 (경기버스여도 앞에 M이 붙으면 광역급행버스(하늘) 색, G가 붙으면 광역버스색) 
        DEPRECATED,     //폐지버스
        MSKYBLUE,       //광역급행버스 (노선명 앞에 M들어가는 버스)
        NSKYBLUE,       //심야버스 (노선명 앞에 N들어가는 버스)
        NONE,
    }

    public enum RUNNINGSTATUS
    {
        RUNNING = 0,    //운행중
        WAITING,        //출발대기
        CLOSED,         //운행종료
        NONE,
    }
}