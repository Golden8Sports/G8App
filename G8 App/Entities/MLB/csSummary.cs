using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.MLB
{
    public class csSummary
    {
        public string range { get; set; }
        public int games { get; set; }
        public int wins { get; set; }
        public int loses { get; set; }
        public int risk { get; set; }
        public int net { get; set; }
        public double hold { get; set; }

        public csSummary()
        {
            this.range = "";
            this.games = 0;
            this.wins = 0;
            this.loses = 0;
            this.risk = 0;
            this.net = 0;
            this.hold = 0.00;
        }
    }
}