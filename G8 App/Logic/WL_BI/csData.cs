using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Logic.WL_BI
{
    public class csData
    {
        public int? IdWager = null;
        public int? IdWagerDetail = null;
        public int? IdAgent = null;
        public String Agent = "";
        public int? IdPlayer = null;
        public String Player = "";
        public int? IdLineType = null;
        public String IdLineTypeName = "";
        public String LoginName = "";
        public Double? WinAmount = null;
        public Double? RiskAmount = null;
        public String Result = "";
        public Double? Net = null;
        public String GamePeriod = "";
        public String League = "";
        public String CompleteDescription = "";
        public String DetailDescription = "";
        public String Team = "";
        public int? IdGame = null;
        public int? IdLeague = null;
        public int? Period = null;
        public int? Play = null;
        public String WagerPlay = "";
        public String FavDog = "";
        public String IdSport = "";
        public DateTime? PlacedDate;
        public DateTime? SeattledDate;
        public int? Odds = null;
        public Double? Points = null;
        public String Score = "";
        public String IP = "";
        public Double? OpeningPoints = null;
        public int? OpeningOdds = null;
        public Double? ClosingPoints = null;
        public int? ClosingOdds = null;
        public String BeatLine = "";
        public csData() { }
    }
}