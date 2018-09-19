using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace G8_App.Entities.MLB
{
    public class csMlbSeries
    {
        public DateTime EventDate { get; set; }
        public string Name { get; set; }
        public int AwayRot { get; set; }
        public int HomeRot { get; set; }
        public int Id { get; set; }
        public string VisitorTeam { get; set; }
        public string HomeTeam { get; set; }
        public int CountGames { get; set; }

        public int? Line { get; set; }

        public string Fav { get; set; }
        public string RangeFav { get; set; }

        public int? HomeML { get; set; }
        public int? VisitorML { get; set; }



        public string Dog { get; set; }
        public string RangeDog { get; set; }


        public string Range { get; set; }
        public string Reference { get; set; }

        public ObservableCollection<csGroupSerie> CompleteSerieList = new ObservableCollection<csGroupSerie>();

        public csMlbSeries(){}

        public csMlbSeries(DateTime eventDate, string name, int awayRot, int homeRot, int id)
        {
            EventDate = eventDate;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            AwayRot = awayRot;
            HomeRot = homeRot;
            Id = id;
            SetVisitorHome(name);
            CountGames = (name.Contains("* ")) ? 4 : 3;
        }

        public void SetVisitorHome(string txt)
        {
            char[] v = { 'v', 's' };
            var split = txt.Split(v);

            this.VisitorTeam = split[0].Replace("*","").Trim();
            this.HomeTeam = split[2].Replace("*", "").Trim();
        }

        public void SetCountGames(int num)
        {
            this.CountGames = num;
        }

    }
}