using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.MLB
{
    public class csGamesBySerie
    {
        public DateTime EventDate { get; set; }
        public int VisitorNumber { get; set; }
        public int HomeNumber { get; set; }
        public int Id { get; set; }
        public string VisitorTeam { get; set; }
        public string HomeTeam { get; set; }
        public string VisitorPitcher { get; set; }
        public string HomePitcher { get; set; }
        public int? HomeScore { get; set; }
        public int? VisitorScore { get; set; }

        public int? HomeOdds { get; set; }
        public int? VisitorOdds { get; set; }

        public double? Spread { get; set; }
        public double? SpreadOdds { get; set; }

        //total
        public double? Total { get; set; }
        public double? TotalOver { get; set; }
        public double? TotalUnder { get; set; }

        public int? Line { get; set; }

        public csGamesBySerie()
        {
        }
    }
}