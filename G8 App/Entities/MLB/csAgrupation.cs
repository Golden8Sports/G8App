using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.MLB
{
    public class csAgrupation
    {
        public string SeriesType { get; set; }
        public string SeriesRange { get; set; }
        public string Fav { get; set; }
        public int TotalGames { get; set; }
        public int TotalSeries { get; set; }

        public int TotalWinPercent { get; set; }

        public int Game1 { get; set; }
        public double Game1Percent { get; set; }

        public int FavSwip { get; set; }
        public int DogSwip { get; set; }

        public int FavSwipPossible { get; set; }
        public int DogSwipPossible { get; set; }

        //game 2
        public int Game2 { get; set; }
        public double Game2Percent { get; set; }

        //game 3
        public int Game3 { get; set; }
        public double Game3Percent { get; set; }

        //game 4
        public int? Game4 { get; set; }
        public double? Game4Percent { get; set; }


        public int? Risk1 { get; set; }
        public int? Risk2 { get; set; }
        public int? Risk3 { get; set; }
        public int? Risk4 { get; set; }

        public int? Win1 { get; set; }
        public int? Win2 { get; set; }
        public int? Win3 { get; set; }
        public int? Win4 { get; set; }

        public int? Net1 { get; set; }
        public int? Net2 { get; set; }
        public int? Net3 { get; set; }
        public int? Net4 { get; set; }

        public double? Hold1 { get; set; }
        public double? Hold2 { get; set; }
        public double? Hold3 { get; set; }
        public double? Hold4 { get; set; }



        public csAgrupation()
        {
            this.Game4 = 0;
            this.DogSwip = 0;
            this.FavSwip = 0;
            this.FavSwipPossible = 0;
            this.FavSwipPossible = 0;
            this.Hold1 = 0;
            this.Hold2 = 0;
            this.Hold3 = 0;
            this.Hold4 = 0;
            this.Net1 = 0;
            this.Net2 = 0;
            this.Net3 = 0;
            this.Net4 = 0;
            this.Risk1 = 0;
            this.Risk2 = 0;
            this.Risk3 = 0;
            this.Risk4 = 0;
        }
    }
}