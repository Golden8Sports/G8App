using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace G8_App.Entities.Profiling
{
    public class csSummary
    {
        public string Player { get; set; }
        public string Agent { get; set; }
        public string Sport { get; set; }
        public string League { get; set; }
        public string GamePeriod { get; set; }
        public string Team { get; set; }
        public string Day { get; set; }
        public string MomentDay { get; set; }
        public string FavDog { get; set; }
        public string WagerType { get; set; }
        public string Side { get; set; }
        public int RiskAmount { get; set; }
        public int WinAmount { get; set; }
        public int Net { get; set; }
        public int Bets { get; set; }
        public int Wins { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
        public double WinPercentaje { get; set; }
        public double HoldPercentaje { get; set; }

        public int ScalpingPPH { get; set; }
        public int ScalpingJazz { get; set; }
        public int ScalpingPinni { get; set; }
        public int Scalping5Dimes { get; set; }
        public int ScalpingCris { get; set; }
        public string WagerPlay { get; set; }

        public int MoveLine { get; set; }
        public int BeatLine { get; set; }
        public int Syndicate { get; set; }
        public int Players { get; set; }

        public DateTime PlacedDate { get; set; }
        public string PlacedDateString { get; set; }
        public string Date { get; set; }


        public string HomeTem { get; set; }
        public string VisitorTem { get; set; }
        public string DetailDescription { get; set; }
        public string EventDateString { get; set; }
        public double Points { get; set; }
        public double Odds { get; set; }
        public string Week { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string DateRange { get; set; }

        public int ParlayNet { get; set; }
        public int TeaserNet { get; set; }
        public int StraightNet { get; set; }
        public DateTime DateRangeGraph { get; set; }


        public csSummary()
        {
            this.Net = 0;
            this.HoldPercentaje = 0;
            this.WinPercentaje = 0;
            this.RiskAmount = 0;
            this.ParlayNet = 0;
            this.StraightNet = 0;
            this.TeaserNet = 0;
        }

        public csSummary(string player, int riskAmount, int winAmount, int net, int bets, int wins, int draw, int lost, int winPercentaje, double holdPercentaje, int scalpingPPH, int scalpingJazz, int scalpingPinni, int scalping5Dimes, int scalpingCris, int moveLine, int beatLine, int syndicate)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            RiskAmount = riskAmount;
            WinAmount = winAmount;
            Net = net;
            Bets = bets;
            Wins = wins;
            Draw = draw;
            Lost = lost;
            WinPercentaje = winPercentaje;
            HoldPercentaje = holdPercentaje;
            ScalpingPPH = scalpingPPH;
            ScalpingJazz = scalpingJazz;
            ScalpingPinni = scalpingPinni;
            Scalping5Dimes = scalping5Dimes;
            ScalpingCris = scalpingCris;
            MoveLine = moveLine;
            BeatLine = beatLine;
            Syndicate = syndicate;
        }



        public csSummary(string player,string agent, int riskAmount, int net, int bets, int wins)
        {
            this.Player = player ?? throw new ArgumentNullException(nameof(player));
            this.Agent = agent;
            this.RiskAmount = riskAmount;
            this.Net = net;
            this.Bets = bets;
            this.Wins = wins;
            double winP = Convert.ToDouble(((wins * 100) / bets));
            double holdP = Convert.ToDouble(((net * 100) / riskAmount));
            this.WinPercentaje = Math.Round(winP,2,MidpointRounding.AwayFromZero);
            this.HoldPercentaje = Math.Round(holdP, 2, MidpointRounding.AwayFromZero);
        }



        public csSummary(DateTime dt, int riskAmount, int net)
        {
            this.DateRangeGraph = dt;
            this.RiskAmount = riskAmount;
            this.Net = net;
            double holdP = Convert.ToDouble(((net * 100) / riskAmount));
            this.HoldPercentaje = Math.Round(holdP, 2, MidpointRounding.AwayFromZero);
        }





        public csSummary(int riskAmount, int net, DateTime dt, int players)
        {
            this.RiskAmount = riskAmount;
            this.Net = net * -1;
            int a1 = this.Net * 100;
            decimal holdP = (decimal)(a1 / riskAmount);
            double hold = Convert.ToDouble(holdP);
            this.HoldPercentaje = Math.Round(hold, 2, MidpointRounding.AwayFromZero); 
            this.Date = dt.Date.ToString("yyyy/MM/dd");
            this.Players = players;
        }


        public csSummary(int riskAmount, int net, string sport)
        {
            this.RiskAmount = riskAmount;
            this.Net = net;
            double holdP = Convert.ToDouble(((net * 100) / riskAmount));
            this.HoldPercentaje = Math.Round(holdP, 2, MidpointRounding.AwayFromZero);
            this.Sport = sport;
            this.HoldPercentaje *= 1;
            this.Net *= -1;
        }



        public csSummary(int net, int risk, int players)
        {
            this.RiskAmount = risk;
            this.Net = net * -1;
            int a1 = this.Net * 100;
            decimal holdP = (decimal)(a1 / risk);
            double hold = Convert.ToDouble(holdP);
            this.HoldPercentaje = Math.Round(hold, 2, MidpointRounding.AwayFromZero);
            this.Date = "OverAll";
            this.Players = players;
        }

    }
}