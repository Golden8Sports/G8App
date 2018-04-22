using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_BL.Entities
{
    public class csTeamRange
    {
        public string TeamName = "";
        public string League = "";
        public string Side = "";
        public int Wins = 0;
        public int GP = 0;
        public int Ties = 0;
        public double WinPorcentaje = 0;
        public int holdPorcentaje = 0;
        public  Nullable <int> SumJuice = 0;

        public Nullable<int> RiskAmount = 0;
        public Nullable<int> NetAmount = 0;

        public csTeamRange() { }

        public csTeamRange(string name, string league, string side, int wins, int gp, double winPorcentaje)
        {
            this.TeamName = name;
            this.League = league;
            this.Side = side;
            this.Wins = wins;
            this.GP = gp;
            this.WinPorcentaje = winPorcentaje;
        }
    }
}
