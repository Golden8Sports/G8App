using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace G8_App.Entities.MLB
{
    public class csGroupSerie
    {
        public DateTime EventDate { get; set; }
        public string Name { get; set; }
        public int AwayRot { get; set; }
        public int HomeRot { get; set; }
        public int Id { get; set; }
        public string VisitorTeam { get; set; }
        public string HomeTeam { get; set; }

        public ObservableCollection<csGamesBySerie> GamesBySerieList = new ObservableCollection<csGamesBySerie>();

        public csGroupSerie() { }

        public csGroupSerie(DateTime eventDate, string name, int awayRot, int homeRot, int id)
        {
            EventDate = eventDate;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            AwayRot = awayRot;
            HomeRot = homeRot;
            Id = id;
            SetVisitorHome(name);
        }

        public void SetVisitorHome(string txt)
        {
            var split = txt.Split(' ');

            this.VisitorTeam = split[0].Replace("*", "").Trim();
            this.HomeTeam = split[2].Replace("*", "").Trim();
        }
    }
}