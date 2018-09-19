using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.MLB
{
    public class csTeamStat
    {
        public string Name { get; set; }
        public int AB { get; set; }
        public int PA { get; set; }
        public int H { get; set; }
        public int B1 { get; set; }
        public int B2 { get; set; }
        public int B3 { get; set; }
        public int HR { get; set; }
        public int R { get; set; }
        public int RBI { get; set; }
        public int BB { get; set; }
        public int K { get; set; }
        public int SF { get; set; }
        public int GDP { get; set; }
        public int SB { get; set; }
        public double AVG { get; set; }
        public double OBP { get; set; }
        public double SLG { get; set; }
        public double OPS { get; set; }

        public csTeamStat()
        {
        }
    }
}