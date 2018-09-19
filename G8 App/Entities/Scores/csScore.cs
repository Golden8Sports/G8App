using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace G8_App.Entities.Scores
{
    public class csScore
    {
        public ObservableCollection<csDictionary> Values = new ObservableCollection<csDictionary>();
        public string sportId { get; set; }
        public string period { get; set; }
        public string description1 { get; set; }
        public string description2 { get; set; }
        public string homeRot { get; set; }
        public string awayRot { get; set; }
        public string awayScore { get; set; }
        public string homeScore { get; set; }
        public string date { get; set; }
        public csScore(){}
    }
}