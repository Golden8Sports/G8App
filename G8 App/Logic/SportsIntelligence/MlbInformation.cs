using Data.Connection;
using G8_App.Connection;
using G8_App.Entities.SportsIntelligence;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace G8_App.Logic.SportsIntelligence
{
    public class MlbInformation : csComponentsConnection
    {
        public ObservableCollection<csMlbTrends> GetTrends(csMlbTrends t)
        {
            ObservableCollection<csMlbTrends> data = new ObservableCollection<csMlbTrends>();

            try
            {
                //code here 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return data;
        }



        public ObservableCollection<csMlbTrends> ScrapTrends(int yearInt, string year, string zone, string situation, string type, int idSport, string sportName, string team)
        {
            ObservableCollection<csMlbTrends> data = new ObservableCollection<csMlbTrends>();

            try
            {
                string result = string.Empty;              
                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();
                
                if(sportName == "NFL")
                {
                   doc = web.Load("https://www.teamrankings.com/nfl/trends/"+ type +"_trends/?range=yearly_"+ year +"&group="+ zone +"&sc=" + situation);
                }
                else if(sportName == "MLB")
                {
                    doc = web.Load("https://www.teamrankings.com/mlb/trends/" + type + "_trends/?range=yearly_mlb_" + year + "&group=" + zone + "&sc=" + situation);
                }
                else if (sportName == "NBA")
                {
                    doc = web.Load("https://www.teamrankings.com/nba/trends/" + type + "_trends/?range=yearly_" + year + "&group=" + zone + "&sc=" + situation);
                }
                else if (sportName == "NCAAF")
                {
                    //https://www.teamrankings.com/ncf/trends/win_trends/?range=yearly_2011&group=0&sc=all_games
                    doc = web.Load("https://www.teamrankings.com/ncf/trends/" + type + "_trends/?range=yearly_" + year + "&group=" + zone + "&sc=" + situation);
                }
                else if (sportName == "NCAAB")
                {
                    //https://www.teamrankings.com/ncb/trends/win_trends/?range=yearly_2011&group=0&sc=all_games
                    doc = web.Load("https://www.teamrankings.com/ncb/trends/"+ type +"_trends/?range=yearly_"+ year +"&sc="+ situation +"&group=" + zone);
                }

                HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");

                if (tables != null)
                    for (int k = 0; k < tables.Count; k++)
                    {
                        HtmlNodeCollection rows = tables[k].SelectNodes(".//tr");

                        if (rows != null)
                            for (int i = 1; i < rows.Count; ++i)
                            {
                                csMlbTrends t = new csMlbTrends();
                                t.IdSport = idSport;
                                t.Year = yearInt.ToString();
                                t.Situation = situation;
                                t.Zone = zone;
                                t.Type = type;
                                t.SportName = sportName;

                                //Headers
                                HtmlNodeCollection headers = tables[k].SelectNodes(".//th");
                                if (headers != null)
                                {
                                    for (int j = 0; j < headers.Count; ++j)
                                    {
                                        string txt = headers[j].InnerText.Trim();
                                        if (j == 0) t.H1 = txt;
                                        else if (j == 1) t.H2 = txt;
                                        else if (j == 2) t.H3 = txt;
                                        else if (j == 3) t.H4 = txt;
                                        else if (j == 4) t.H5 = txt;
                                    }
                                }

                                HtmlNodeCollection cols = rows[i].SelectNodes(".//td");
                                if (cols != null)
                                {
                                    for (int j = 0; j < cols.Count; ++j)
                                    {
                                        string txt = cols[j].InnerText.Replace("\r\n", "").Trim().Replace("\t\t (R)", "").Trim().Replace("\t\t (L)", "").Trim().Replace("\n\t\t","").Trim();

                                        if (!String.IsNullOrWhiteSpace(txt))
                                        {
                                            if (j == 0)
                                            {
                                                t.Team = t.TeamValue = txt;
                                                if (!(team.ToUpper().Contains(t.Team.ToUpper().Trim()) || t.Team.ToUpper().Contains(team.ToUpper().Trim()))) break;
                                            }
                                            else if (j == 1) t.Line1 = txt;
                                            else if (j == 2) t.Line2 = txt;
                                            else if (j == 3) t.Line3 = txt;
                                            else if (j == 4) t.Line4 = txt;
                                        }
                                    }

                                    if (team.ToUpper().Contains(t.Team.ToUpper().Trim()) || t.Team.ToUpper().Contains(team.ToUpper().Trim()))
                                    data.Add(t);
                                }
                            }
                    }
            }
            catch (Exception ex)
            {
                int dsd = 0;
            }
            return data;
        }






        public ObservableCollection<csMlbTrends> ScrapTeams(string url)
        {
            ObservableCollection<csMlbTrends> data = new ObservableCollection<csMlbTrends>();

            try
            {
                string result = string.Empty;
                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();
                csMlbTrends t1 = new csMlbTrends();
                t1.TeamValue = "";
                t1.Team = "ALL";
                data.Add(t1);

                    doc = web.Load(url);

                HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");

                if (tables != null)
                    for (int k = 0; k < tables.Count; k++)
                    {
                        HtmlNodeCollection rows = tables[k].SelectNodes(".//tr");

                        if (rows != null)
                            for (int i = 1; i < rows.Count; ++i)
                            {
                                csMlbTrends t = new csMlbTrends();
                                
                                HtmlNodeCollection cols = rows[i].SelectNodes(".//td");
                                if (cols != null)
                                {
                                    for (int j = 0; j < (cols.Count - 1); ++j)
                                    {
                                        string txt = cols[j].InnerText.Replace("\r\n", "").Trim().Replace("\t\t (R)", "").Trim().Replace("\t\t (L)", "").Trim().Replace("\n\t\t", "").Trim();

                                        if (!String.IsNullOrWhiteSpace(txt))
                                        {
                                            if (j == 0) t.Team = t.TeamValue = txt;
                                        }
                                    }

                                    data.Add(t);
                                }
                            }
                    }
            }
            catch (Exception ex)
            {
                int dsd = 0;
            }
            return data;
        }




        public csMlbTrends GetRank(string url)
        {
            csMlbTrends t = null;

            try
            {
                string result = string.Empty;
                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();
                doc = web.Load(url);
                HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");

                    if (tables != null)
                    for (int k = 0; k < 1; k++)
                    {
                        HtmlNodeCollection rows = tables[k].SelectNodes(".//tr");

                        if (rows != null)
                            for (int i = 1; i < 2; ++i)
                            {
                                t = new csMlbTrends();
                                HtmlNodeCollection cols = rows[i].SelectNodes(".//td");
                                if (cols != null)
                                {
                                    for (int j = 0; j < cols.Count; ++j)
                                    {
                                        string txt = cols[j].InnerText.ToUpper().Replace("\r\n", "").Trim().Replace("\t\t (R)", "").Trim().Replace("\t\t (L)", "").Trim().Replace("\n\t\t", "").Trim().Replace("RECORD","").Trim().Replace("PREDICTIVE RANK","").Trim().Replace("#","").Trim().Replace("STREAK","").Trim();

                                        if (!String.IsNullOrWhiteSpace(txt))
                                        {
                                            if (j == 0) t.Record = txt;
                                            else if (j == 1) t.Rank = txt;
                                            else if (j == 2) t.Streak = txt;
                                            }
                                    }
                                }
                            }
                    }
            }
            catch (Exception ex)
            {
                int dsd = 0;
            }
            return t;
        }




        public Object Scrap(string url)
        {
            Object result = string.Empty;

            try
            {              
                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();
                doc = web.Load(url);

                HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table"); //find a specific node
                    if (tables != null)
                    for (int k = 0; k < tables.Count; k++)
                    {
                        HtmlNodeCollection rows = tables[k].SelectNodes(".//tr"); //find each <tr> by each table
                            if (rows != null)
                            for (int i = 1; i < rows.Count; ++i)
                            {
                                HtmlNodeCollection cols = rows[i].SelectNodes(".//td");
                                if (cols != null)
                                {
                                    for (int j = 0; j < cols.Count; ++j)
                                    {
                                        //go through a specific label
                                        //here must be the code as to need
                                    }
                                }
                            }
                    }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }





        public ObservableCollection<csMlbTrends> RunsPerGame(string url, string team, int year)
        {
            ObservableCollection<csMlbTrends> data = new ObservableCollection<csMlbTrends>();

            try
            {
                string result = string.Empty;
                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();

                doc = web.Load(url);

                HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");
                if (tables != null)
                    for (int k = 0; k < tables.Count; k++)
                    {
                        HtmlNodeCollection rows = tables[k].SelectNodes(".//tr");
                        if (rows != null)
                            for (int i = 1; i < rows.Count; ++i)
                            {
                                csMlbTrends t = new csMlbTrends();
                                t.YearRuns = year;

                                HtmlNodeCollection cols = rows[i].SelectNodes(".//td");
                                if (cols != null)
                                {
                                    for (int j = 0; j < cols.Count; ++j)
                                    {
                                        string txt = cols[j].InnerText.Replace("\r\n", "").Trim().Replace("\t\t (R)", "").Trim().Replace("\t\t (L)", "").Trim().Replace("\n\t\t", "").Trim();

                                        if (!String.IsNullOrWhiteSpace(txt))
                                        {
                                            if (j == 0) t.Rank = txt;
                                            else if (j == 1)
                                            {
                                                t.Team = t.TeamValue = txt;
                                                if (!(team.ToUpper().Contains(t.Team.ToUpper().Trim()) || t.Team.ToUpper().Contains(team.ToUpper().Trim()))) break;
                                            }
                                            else if (j == 2) t.Year = txt;
                                            else if (j == 3) t.Last3 = txt;
                                            else if (j == 4) t.Last1 = txt;
                                            else if (j == 5) t.Home = txt;
                                            else if (j == 6) t.Away = txt;
                                            else if (j == 7) t.YearBefore = txt;
                                        }
                                    }

                                 if (team.ToUpper().Contains(t.Team.ToUpper().Trim()) || t.Team.ToUpper().Contains(team.ToUpper().Trim()))
                                 data.Add(t);

                                }
                            }
                    }
            }
            catch (Exception ex)
            {
                int dsd = 0;
            }
            return data;
        }




        public ObservableCollection<csMlbTrends> OutsPitched(string url, string team, int year)
        {
            ObservableCollection<csMlbTrends> data = new ObservableCollection<csMlbTrends>();

            try
            {
                string result = string.Empty;
                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();

                doc = web.Load(url);

                HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");
                if (tables != null)
                    for (int k = 0; k < tables.Count; k++)
                    {
                        HtmlNodeCollection rows = tables[k].SelectNodes(".//tr");
                        if (rows != null)
                            for (int i = 1; i < rows.Count; ++i)
                            {
                                csMlbTrends t = new csMlbTrends();
                                HtmlNodeCollection cols = rows[i].SelectNodes(".//td");
                                if (cols != null)
                                {
                                    for (int j = 0; j < cols.Count; ++j)
                                    {
                                        string txt = cols[j].InnerText.Replace("\r\n", "").Trim().Replace("\t\t (R)", "").Trim().Replace("\t\t (L)", "").Trim().Replace("\n\t\t", "").Trim();

                                        if (!String.IsNullOrWhiteSpace(txt))
                                        {
                                            if (j == 0) t.Rank = txt;
                                            else if (j == 1)
                                            {
                                                t.Team = t.TeamValue = txt;
                                                if (!(team.ToUpper().Contains(t.Team.ToUpper().Trim()) || t.Team.ToUpper().Contains(team.ToUpper().Trim()))) break;
                                            }
                                            else if (j == 2) t.Year = txt;
                                            else if (j == 3) t.Last3 = txt;
                                            else if (j == 4) t.Last1 = txt;
                                            else if (j == 5) t.Home = txt;
                                            else if (j == 6) t.Away = txt;
                                            else if (j == 7) t.YearBefore = txt;
                                        }
                                    }

                                    if (team.ToUpper().Contains(t.Team.ToUpper().Trim()) || t.Team.ToUpper().Contains(team.ToUpper().Trim()))
                                        data.Add(t);

                                }
                            }
                    }
            }
            catch (Exception ex)
            {
                int dsd = 0;
            }
            return data;
        }





        public ObservableCollection<csMlbTrends> RunsAllowed(string url, string team, int year)
        {
            ObservableCollection<csMlbTrends> data = new ObservableCollection<csMlbTrends>();

            try
            {
                string result = string.Empty;
                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();

                doc = web.Load(url);

                HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");
                if (tables != null)
                    for (int k = 0; k < tables.Count; k++)
                    {
                        HtmlNodeCollection rows = tables[k].SelectNodes(".//tr");
                        if (rows != null)
                            for (int i = 1; i < rows.Count; ++i)
                            {
                                csMlbTrends t = new csMlbTrends();
                                HtmlNodeCollection cols = rows[i].SelectNodes(".//td");
                                if (cols != null)
                                {
                                    for (int j = 0; j < cols.Count; ++j)
                                    {
                                        string txt = cols[j].InnerText.Replace("\r\n", "").Trim().Replace("\t\t (R)", "").Trim().Replace("\t\t (L)", "").Trim().Replace("\n\t\t", "").Trim();

                                        if (!String.IsNullOrWhiteSpace(txt))
                                        {
                                            if (j == 0) t.Rank = txt;
                                            else if (j == 1) t.Player = txt;
                                            else if (j == 2)
                                            {
                                                t.Team = t.TeamValue = txt;
                                                if (!(team.ToUpper().Contains(t.Team.ToUpper().Trim()) || t.Team.ToUpper().Contains(team.ToUpper().Trim()))) break;
                                            }                                         
                                            else if (j == 3) t.Pos = txt;
                                            else if (j == 4) t.Value = txt;
                                        }
                                    }

                                    if (team.ToUpper().Contains(t.Team.ToUpper().Trim()) || t.Team.ToUpper().Contains(team.ToUpper().Trim()))
                                        data.Add(t);

                                }
                            }
                    }
            }
            catch (Exception ex)
            {
                int dsd = 0;
            }
            return data;
        }











        public void PuclicateTrends(ObservableCollection<csMlbTrends> list)
        {

            try
            {
                if (list != null)
                {
                    foreach (var i in list)
                    {
                        parameters.Clear();
                        parameters.Add("@pYear", i.Year);
                        parameters.Add("@pSituation", i.Situation);
                        parameters.Add("@pSport", i.SportName);
                        parameters.Add("@pZone", i.Zone);
                        parameters.Add("@pType", i.Type);
                        parameters.Add("@pTeam", i.Team);
                        parameters.Add("@pIdSport", i.IdSport);
                        parameters.Add("@pLine1", i.Line1);
                        parameters.Add("@pLine2", i.Line2);
                        parameters.Add("@pLine3", i.Line3);
                        parameters.Add("@pLine4", i.Line4);

                        csStats.ExecutePA("[dbo].[web_insertTrend]", parameters);
                    }
                }
            }
            catch (Exception ex)
            {
                int zxfc = 0;
            }
        }



        public void PuclicateRank(csMlbTrends t)
        {
            try
            {
                if (t != null)
                {
                    parameters.Clear();
                    parameters.Add("@pTeam", t.Team);
                    parameters.Add("@pRank", t.Rank);
                    parameters.Add("@pRecord", t.Record);
                    parameters.Add("@pStreak", t.Streak);
                    parameters.Add("@pSport", t.SportName);
                    parameters.Add("@pDescription", null);
                    parameters.Add("@pZone", null);
                    csStats.ExecutePA("[dbo].[web_insertTeam]", parameters);
                }
            }
            catch (Exception ex)
            {
                int zxfc = 0;
            }
        }






        
        //runs per game
        public void PuclicateStats(ObservableCollection<csMlbTrends> list, string description, int year, string sport)
        {

            try
            {
                if (list != null)
                {
                    foreach (var i in list)
                    {
                        parameters.Clear();
                        parameters.Add("@pYearDate", year);
                        parameters.Add("@pYear", i.Year);
                        parameters.Add("@pYearBefore", i.YearBefore);
                        parameters.Add("@pRank", i.Rank);
                        parameters.Add("@pLast1", i.Last1);
                        parameters.Add("@pLast3", i.Last3);
                        parameters.Add("@pTeam", i.Team);
                        parameters.Add("@pSport", sport);
                        parameters.Add("@pHome", i.Home);
                        parameters.Add("@pAway", i.Away);
                        parameters.Add("@pPlayer", i.Player);
                        parameters.Add("@pPosition", i.Pos);
                        parameters.Add("@pDescription", description);
                        parameters.Add("@pValue", i.Value);

                        csStats.ExecutePA("[dbo].[web_insertStat]", parameters);
                    }
                }
            }
            catch (Exception ex)
            {
                int zxfc = 0;
            }
        }




    }
}