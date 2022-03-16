using UnityEngine;


namespace TimeSystem
{
    public class TimeStamp
    {
        public int Minute { get; protected set; }
        public int Hour { get; protected set; }
        public int Day { get; protected set; }
        public int Month { get; protected set; }
        public int Year { get; protected set; }
        public Season Season { get; protected set; }

        public TimeStamp(int _minute, int _hour, int _day, int _month, int _year, Season _season)
        {
            Minute = _minute;
            Hour = _hour;
            Day = _day;
            Month = _month;
            Year = _year;
            Season = _season;
        }

        public TimeStamp(int _minute, int _hour, int _day, int _month, int _year)
        {
            Minute = _minute;
            Hour = _hour;
            Day = _day;
            Month = _month;
            Year = _year;
            Season = TimeManager.GetSeason(_month);
        }

        public override string ToString()
        {
            return $"{Hour:00}:{Minute:00} / {Day:00}.{Month:00}.{Year:0000} - {Season.ToString()}";
        }

        public static int GetTimeStampInMinutes(TimeStamp _timeStamp)
        {
            int timeStampInMinutes = _timeStamp.Year * 525600;
            timeStampInMinutes += _timeStamp.Month * 43800;
            timeStampInMinutes += _timeStamp.Day * 1440;
            timeStampInMinutes += _timeStamp.Hour * 60;
            timeStampInMinutes += _timeStamp.Minute;
            return timeStampInMinutes;
        }

        public int InMinutes() => GetTimeStampInMinutes(this);
    }
}