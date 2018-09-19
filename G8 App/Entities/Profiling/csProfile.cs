using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.Profiling
{
    public class csProfile : csSummary
    {
        public int CountSP { get; set; }
        public int CountML { get; set; }
        public int CountDR { get; set; }
        public int CountOVER { get; set; }
        public int CountUNDER { get; set; }

        public int CountHome { get; set; }
        public int CountVisitor { get; set; }

        public int CountNoon { get; set; }
        public int CountNight { get; set; }
        public int CountOverNight { get; set; }
        public int CountMorning { get; set; }

        public int CountWins { get; set; }
        public int CountLoses { get; set; }
        public int CountDraws { get; set; }

        public int CountFav { get; set; }
        public int CountDog { get; set; }


        public int CountNoLineMoved { get; set; }
        public int CountLineMoved { get; set; }

        public int CountLive { get; set; }
        public int CountNoLive { get; set; }

        public int CountBuy { get; set; }
        public int CountNoBuy { get; set; }

       

        public csProfile():base() { }
    }
}