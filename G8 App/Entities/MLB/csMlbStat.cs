using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.MLB
{
    public class csMlbStat
    {
        public string SeriesType { get; set; }
        public string SeriesRange { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public int TotalSeries { get; set; }
        public int TotalGames { get; set; }
        public string Fav { get; set; }
        public int? Line { get; set; }
        public string VTeam { get; set; }
        public string HTeam { get; set; }

        public int CountGames { get; set; }

        //Game 1
        public double? Spread1 { get; set; }
        public double? SpreadOdds1 { get; set; }
        public double? Total1 { get; set; }
        public double? TotalOver1 { get; set; }
        public double? TotalUnder1 { get; set; }

        //Game 2
        public double? Spread2 { get; set; }
        public double? SpreadOdds2 { get; set; }
        public double? Total2 { get; set; }
        public double? TotalOver2 { get; set; }
        public double? TotalUnder2 { get; set; }


        //Game 3
        public double? Spread3 { get; set; }
        public double? SpreadOdds3 { get; set; }
        public double? Total3 { get; set; }
        public double? TotalOver3 { get; set; }
        public double? TotalUnder3 { get; set; }


        //Game 4
        public double? Spread4 { get; set; }
        public double? SpreadOdds4 { get; set; }
        public double? Total4 { get; set; }
        public double? TotalOver4 { get; set; }
        public double? TotalUnder4 { get; set; }



        public int FavSwip { get; set; }
        public int DogSwip { get; set; }

        public int FavSwipPossible { get; set; }
        public int DogSwipPossible { get; set; }

        //Games
        //game 1
        public int Game1 { get; set; }
        public int? Game1Line { get; set; }

        //game 2
        public int Game2 { get; set; }
        public int? Game2Line { get; set; }

        //game 3
        public int Game3 { get; set; }
        public int? Game3Line { get; set; }

        //game 4
        public int? Game4 { get; set; }
        public int? Game4Line { get; set; }


        public int? HomeScore1 { get; set; }
        public int? VisitorScore1 { get; set; }
        public int? HomeScore2 { get; set; }
        public int? VisitorScore2 { get; set; }
        public int? HomeScore3 { get; set; }
        public int? VisitorScore3 { get; set; }
        public int? HomeScore4 { get; set; }
        public int? VisitorScore4 { get; set; }



        public csMlbStat()
        {
            this.FavSwip = 0;
            this.DogSwip = 0;
            this.FavSwipPossible = 0;
            this.DogSwipPossible = 0;
        }
    }
}