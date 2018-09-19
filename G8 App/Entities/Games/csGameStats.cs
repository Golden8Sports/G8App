using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.Games
{
    public class csGameStats
    {
        public string WagerType { set; get; }
        public string WagerPlay { set; get; }
        public int Bets { set; get; }
        public double Risk { set; get; }
        public int Win { set; get; }
        public double Net { set; get; }
        public string GamePeriod { set; get; }

        public csGameStats()
        {
            this.WagerPlay = "";
            this.WagerType = "";
            this.Bets = 0;
            this.Risk = 0;
            this.Win = 0;
            this.GamePeriod = "";
            this.Net = 0;
        }
    }
}