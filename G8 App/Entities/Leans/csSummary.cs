using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_BL.Entities
{
    public class csSummary
    {
        public double? NetLeans = 0, WinLeans = 0, RiskLeans = 0, WinPerLeans = 0, WinPerNoLeans = 0, NetNoLeans = 0, WinNoLeans = 0, RiskNoLeans = 0;
        public int ContLeansBets = 0, TotalBets = 0, ContNoLeansBets = 0;
        public int ContWins = 0;
        public string sport = "";
        public string Event = "";
        public string Odds = "";
        public string Points = "";
        public string Line = "";
        public string Time = "";
        public int WinBetsLeans = 0, LostBetsLeans = 0;
        public int WinBetsNoLeans = 0, LostBetsNoLeans = 0;
        public double? LeansHold = 0, NoLeansHold = 0;
        public string CrisLine = "";
        public string PinniLine = "";
        public string OurLine = "";
        public string Player = "";
        public int? IdPlayer = 0;
        public String Team = "";

        public double? WinAmount = 0, RiskAmount = 0, Net = 0;

        public csSummary() { }
    }
}
