using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.Scraping.Covers
{
    public class csCovers
    {
        public string Date { get; set; }
        public string VS { get; set; }
        public int Result { get; set; }
        public string AwayStarter { get; set; }
        public string HomeStarter { get; set; }
        public int AriLine { get; set; }
        public string OU { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }

        public csCovers(){}

        public csCovers(string date, string vS, int result, string awayStarter, string homeStarter, int ariLine, string oU, int homeScore, int visitorScore)
        {
            Date = date ?? throw new ArgumentNullException(nameof(date));
            VS = vS ?? throw new ArgumentNullException(nameof(vS));
            Result = result;
            AwayStarter = awayStarter ?? throw new ArgumentNullException(nameof(awayStarter));
            HomeStarter = homeStarter ?? throw new ArgumentNullException(nameof(homeStarter));
            AriLine = ariLine;
            OU = oU ?? throw new ArgumentNullException(nameof(oU));
            HomeScore = homeScore;
            AwayScore = visitorScore;
        }
    }
}