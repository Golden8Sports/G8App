using G8_App.Entities.Dashboard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace G8_App.Logic.Dashboard
{
    public class blSeason
    {
        public ObservableCollection<csSeason> SeasonList = new ObservableCollection<csSeason>();

        public blSeason()
        {
            FillSeason();
        }


        private void FillSeason()
        {
            SeasonList.Add(new csSeason("MLB"));
            SeasonList.Add(new csSeason("NHL"));
            SeasonList.Add(new csSeason("NBA"));
            SeasonList.Add(new csSeason("NFL"));
            SeasonList.Add(new csSeason("CFB"));
            SeasonList.Add(new csSeason("CBB"));
            SeasonList.Add(new csSeason(""));
        }


        public csSeason GetSeasonBySport(string sport)
        {
            csSeason s = null;

            foreach (var i in this.SeasonList)
            {
                if (sport.ToUpper().Trim().Contains(i.Sport.ToUpper().Trim()))
                    s = i;
            }

            return s;
        }
    }
}