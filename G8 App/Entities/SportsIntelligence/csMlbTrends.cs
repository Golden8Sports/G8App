using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.SportsIntelligence
{
    public class csMlbTrends
    {
        public string Year { get; set; }
        public string Zone { get; set; }
        public string Type { get; set; }
        public string Situation { get; set; }
        public string Team { get; set; }
        public string TeamValue { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Line4 { get; set; }
        public int IdSport { get; set; }
        public int YearRuns { get; set; }
        public string SportName { get; set; }

        public string H1 { get; set; }
        public string H2 { get; set; }
        public string H3 { get; set; }
        public string H4 { get; set; }
        public string H5 { get; set; }

        public string Record { get; set; }
        public string Rank { get; set; }
        public string Streak { get; set; }

        public string Last3 { get; set; }
        public string Last1 { get; set; }
        public string Home { get; set; }
        public string Away { get; set; }
        public string YearBefore { get; set; }

        public string Player { get; set; }
        public string Pos { get; set; }
        public string Value { get; set; }

    }
}