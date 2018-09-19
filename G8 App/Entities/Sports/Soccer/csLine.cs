using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.Sports.Soccer
{
    public class csLine
    {
        public int lineId1 { get; set; }
        public int price1 { get; set; }

        public int lineId2 { get; set; }
        public int price2 { get; set; }

        public int eventID { get; set; }
        public int specialID { get; set; }

        public int MaxBet { get; set; }


        public double? handicap1 { get; set; }
        public double? handicap2 { get; set; }

        public csLine()
        {
            this.price1 = 0;
            this.price2 = 0;
        }
    }
}