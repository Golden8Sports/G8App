using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.Lines
{
    public class csLine
    {
        public DateTime Date { get; set; }
        public int Casino { get; set; }
        public string DateString { get; set; }
        public string WagerPlay { get; set; }
        public string WagerType { get; set; }
        public string Player { get; set; }
        public string Time { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public double Line { get; set; }
        public int Juice { get; set; }
        public int IdWager { get; set; }
        public double Risk { get; set; }

        public csLine()
        {
            this.Date = DateTime.Now;
            this.Casino = 0;
            this.DateString = "";
            this.WagerPlay = "";
            this.Player = "";
            this.Year = 0;
            this.Month = 0;
            this.Day = 0;
            this.Hour = 0;
            this.Minute = 0;
            this.Second = 0;
            this.Line = 0;
            this.Juice = 0;
            this.Risk = 0;
            this.WagerType = "";
            this.IdWager = 0;
            this.Time = "";
        }
    }
}