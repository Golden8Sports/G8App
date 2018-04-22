using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_BL.Entities
{
    public class csReportNHL
    {
        public int IdGame { get; set; }
        public System.DateTime GameDateTime { get; set; }
        public int VisitorNumber { get; set; }
        public string VisitorTeam { get; set; }
        public int HomeNumber { get; set; }
        public string HomeTeam { get; set; }
        public Nullable<int> VisitorScore { get; set; }
        public Nullable<int> HomeScore { get; set; }
        public string WINNER { get; set; }
        public Nullable<double> VisitorSpecial { get; set; }
        public Nullable<double> OpeningVisitorSpecial { get; set; }
        public Nullable<int> VisitorSpecialOdds { get; set; }
        public Nullable<int> OpeningVisitorSpecialOdds { get; set; }
        public Nullable<double> HomeSpecial { get; set; }
        public Nullable<double> OpeningHomeSpecial { get; set; }
        public Nullable<int> HomeSpecialOdds { get; set; }
        public Nullable<int> OpeningHomeSpecialOdds { get; set; }
        public Nullable<int> VisitorOdds { get; set; }
        public Nullable<int> OpeningVisitorOdds { get; set; }
        public Nullable<int> HomeOdds { get; set; }
        public Nullable<int> OpeningHomeOdds { get; set; }
        public Nullable<double> TotalOver { get; set; }
        public Nullable<int> OverOdds { get; set; }
        public Nullable<int> UnderOdds { get; set; }
        public string Coverage { get; set; }
    }
}
