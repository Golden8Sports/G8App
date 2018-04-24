using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.Profiling
{
    public class csSummary
    {
        public string Player { get; set; }
        public int RiskAmount { get; set; }
        public int WinAmount { get; set; }
        public int Net { get; set; }
        public int Bets { get; set; }
        public int Wins { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
        public int WinPercentaje { get; set; }
        public int HoldPercentaje { get; set; }
        public int Scalping { get; set; }
        public int MoveLine { get; set; }
        public int BeatLine { get; set; }
        public int Syndicate { get; set; }

        public csSummary()
        {
        }

        public csSummary(string player, int riskAmount, int winAmount, int net, int bets, int wins, int draw, int lost, int winPercentaje, int holdPercentaje, int scalping, int moveLine, int beatLine)
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
            Scalping = scalping;
            MoveLine = moveLine;
            BeatLine = beatLine;
        }
    }
}