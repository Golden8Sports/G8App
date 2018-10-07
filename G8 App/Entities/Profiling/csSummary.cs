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

        public double ScalpingPPH { get; set; }
        public double ScalpingJazz { get; set; }
        public double ScalpingPinni { get; set; }
        public double Scalping5Dimes { get; set; }
        public double ScalpingCris { get; set; }
        public string WagerPlay { get; set; }

        public double MoveLine { get; set; }
        public double BeatLine { get; set; }
        public int Syndicate { get; set; }
        public int Players { get; set; }

        public int ContCris { get; set; }
        public int ContJazz { get; set; }
        public int ContPinni { get; set; }
        public int Cont5Dimes { get; set; }
        public int ContPPH { get; set; }

        public int ContMoveLine { get; set; }
        public int ContBeatLine { get; set; }

        public int OverallScalp { get; set; }


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


        public int Y { get; set; }
        public int M { get; set; }
        public int D { get; set; }

        public csSummary()
        {
            this.Net = 0;
            this.HoldPercentaje = 0;
            this.WinPercentaje = 0;
            this.RiskAmount = 0;
            this.ParlayNet = 0;
            this.StraightNet = 0;
            this.TeaserNet = 0;
            this.DateRange = "";
            this.WagerPlay = "";
        }



        public csSummary(string wagerPlay, string dateRange)
        {
            this.WagerPlay = wagerPlay;
            this.DateRange = dateRange;
            this.Net = 0;
            this.HoldPercentaje = 0;
            this.WinPercentaje = 0;
            this.RiskAmount = 0;
        }




        public csSummary(string player, int riskAmount, int winAmount, int net, int bets, int wins, int draw, int lost, double winPercentaje, double holdPercentaje, double scalpingPPH, double scalpingJazz, double scalpingPinni, double scalping5Dimes, double scalpingCris, double moveLine, double beatLine, int syndicate)
        {
            ContCris = (int)Math.Round((scalpingCris * bets) / 100,0,MidpointRounding.AwayFromZero);
            ContJazz = (int)Math.Round((scalpingJazz * bets) / 100, 0, MidpointRounding.AwayFromZero);
            ContPPH = (int)Math.Round((scalpingPPH * bets) / 100, 0, MidpointRounding.AwayFromZero);
            ContPinni = (int)Math.Round((scalpingPinni * bets) / 100, 0, MidpointRounding.AwayFromZero);
            Cont5Dimes = (int)Math.Round((scalping5Dimes * bets) / 100, 0, MidpointRounding.AwayFromZero);

            ContBeatLine = (int)Math.Round((beatLine * bets) / 100, 0, MidpointRounding.AwayFromZero);
            ContMoveLine = (int)Math.Round((moveLine * bets) / 100, 0, MidpointRounding.AwayFromZero);

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

            OverallScalp = (int)(scalpingPPH + scalpingJazz + scalpingPinni + scalping5Dimes + scalpingCris) / 5;

        }



        public csSummary(string player,string agent, int riskAmount, int net, int bets, int wins)
        {
            this.Player = player ?? throw new ArgumentNullException(nameof(player));
            this.Agent = agent;
            this.RiskAmount = riskAmount;
            this.Net = net;
            this.Bets = bets;
            this.Wins = wins;
            double winP = (wins == 0 || bets == 0) ? 0 : Convert.ToDouble(((wins * 100) / bets));
            double holdP = (net == 0 || riskAmount == 0) ? 0 : Convert.ToDouble(((net * 100) / riskAmount));
            this.WinPercentaje = Math.Round(winP,2,MidpointRounding.AwayFromZero);
            this.HoldPercentaje = Math.Round(holdP, 2, MidpointRounding.AwayFromZero);
        }



        public csSummary(string player, string agent, int riskAmount, int net, int bets, int wins,string range, string play)
        {
            this.Player = player ?? throw new ArgumentNullException(nameof(player));
            this.Agent = agent;
            this.RiskAmount = riskAmount;
            this.Net = net;
            this.Bets = bets;
            this.Wins = wins;
            double winP = (wins == 0 || bets == 0) ? 0 : Convert.ToDouble(((wins * 100) / bets));
            double holdP = (net == 0 || riskAmount == 0) ? 0 : Convert.ToDouble(((net * 100) / riskAmount));
            this.WinPercentaje = Math.Round(winP, 2, MidpointRounding.AwayFromZero);
            this.HoldPercentaje = Math.Round(holdP, 2, MidpointRounding.AwayFromZero);
            this.WagerPlay = play;
            this.DateRange = range;
        }



        public csSummary(DateTime dt, int riskAmount, int net)
        {
            this.DateRangeGraph = dt;
            this.RiskAmount = riskAmount;
            this.Net = net;
            double holdP = Convert.ToDouble(((net * 100) / riskAmount));
            this.HoldPercentaje = Math.Round(holdP, 2, MidpointRounding.AwayFromZero);

            this.Y = this.DateRangeGraph.Year;
            this.M = (this.DateRangeGraph.Month - 1);
            this.D = this.DateRangeGraph.Day;
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


        private csSummary CastDate(csSummary s)
        {
            s.Y = s.DateRangeGraph.Year;
            s.M = s.DateRangeGraph.Month;
            s.D = s.DateRangeGraph.Day;

            return s;
        }

    }
}