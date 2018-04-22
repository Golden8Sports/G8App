using G8_App.Entities.NHL_Ranges;
using NHL_BL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_BL.Logic
{
    public class csRealTeamName
    {
        public ObservableCollection<csReal> realNames = new ObservableCollection<csReal>();

        public csRealTeamName()
        {
            realNames.Add(new csReal("DUCKS", "ANA DUCKS"));
            realNames.Add(new csReal("COYOTES", "ARI COYOTES"));
            realNames.Add(new csReal("AVALANCHE", "COL AVALANCHE"));
            realNames.Add(new csReal("BLACKHAWKS", "CHI BLACKHAWKS"));
            realNames.Add(new csReal("BLUEJACKETS", "CLM BLUEJACKETS"));
            realNames.Add(new csReal("BLUES", "ST LOUIS BLUES"));
            realNames.Add(new csReal("BRUINS", "BOS BRUINGS"));
            realNames.Add(new csReal("SABRES", "BUF SABRES"));
            realNames.Add(new csReal("FLAMES", "CAL FLAMES"));
            realNames.Add(new csReal("CANADIENS", "MTL CANADIENS"));
            realNames.Add(new csReal("CANUCKS", "VAN CANUCKS"));
            realNames.Add(new csReal("CAPITALS", "WAS CAPITALS"));
            realNames.Add(new csReal("HURRICANES", "CAR HURRICANES"));
            realNames.Add(new csReal("STARS", "DAL STARS"));
            realNames.Add(new csReal("WINGS", "DET RED WINGS"));
            realNames.Add(new csReal("DEVILS", "NJ DEVILS"));
            realNames.Add(new csReal("OILERS", "EDM OILERS"));
            realNames.Add(new csReal("PANTHERS", "FLA PANTHERS"));
            realNames.Add(new csReal("FLYERS", "PHI FLYERS"));
            realNames.Add(new csReal("KNIGHTS", "VEGAS GOLDEN KNIGHTS"));
            realNames.Add(new csReal("ISLANDERS", "NY ISLANDERS"));
            realNames.Add(new csReal("JETS", "WIN JETS"));
            realNames.Add(new csReal("KINGS", "LA KINGS"));
            realNames.Add(new csReal("LEAFS", "TOR MAPLE LEAFS"));
            realNames.Add(new csReal("LIGHTNING", "TB LIGHTNING"));
            realNames.Add(new csReal("WILD", "MIN WILD"));
            realNames.Add(new csReal("PREDATORS", "NAS PREDATORS"));
            realNames.Add(new csReal("RANGERS", "NY RANGERS"));
            realNames.Add(new csReal("SENATORS", "OTT SENATORS"));
            realNames.Add(new csReal("PENGUINS", "PIT PENGUINS"));
            realNames.Add(new csReal("SHARKS", "SJ SHARKS"));
        }




        public ObservableCollection<csTeam> GetNHLTeams(ObservableCollection<csTeam> l)
        {

            ObservableCollection<csTeam> list = new ObservableCollection<csTeam>();

            if (l != null)
            {
                for (int i = 0; i < l.Count; i++)
                {
                    for (int j = 0; j < realNames.Count; j++)
                    {
                        if (l[i].Name.Contains(realNames[j].SearchName)) l[i].Name = realNames[j].RealName;
                    }

                    bool has = list.Any(x => x.Name == l[i].Name);
                    if (!has)
                    {
                        list.Add(l[i]);
                    }
                }
            }

            return list;
        }



        public ObservableCollection<csReportNHL> Process(ObservableCollection<csReportNHL> l)
        {

            if (l != null)
            {
                for (int i = 0; i < l.Count; i++)
                {
                    for (int j = 0; j < realNames.Count; j++)
                    {
                        if (l[i].VisitorTeam.Contains(realNames[j].SearchName)) l[i].VisitorTeam = realNames[j].RealName;
                        if (l[i].HomeTeam.Contains(realNames[j].SearchName)) l[i].HomeTeam = realNames[j].RealName;
                    }
                }
            }

            return l;
        }

    }
}
