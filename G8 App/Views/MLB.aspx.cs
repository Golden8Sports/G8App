using G8_App.Entities;
using G8_App.Entities.SportsIntelligence;
using G8_App.Logic.SportsIntelligence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G8_App.Views.SportsIntelligence
{
    public partial class MLB : System.Web.UI.Page
    {
        private MlbInformation MLBInfo = new MlbInformation();
        private static ObservableCollection<csMlbTrends> TrendList = new ObservableCollection<csMlbTrends>();
        private static ObservableCollection<csMlbTrends> Runs = new ObservableCollection<csMlbTrends>();
        private static ObservableCollection<csMlbTrends> OutPitched = new ObservableCollection<csMlbTrends>();
        private static ObservableCollection<csMlbTrends> RunsAllowed = new ObservableCollection<csMlbTrends>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                if (!IsPostBack)
                {
                    userName.InnerText = csUser.Name + " - " + csUser.Profile;
                    FillYears();
                    FillTeams();
                }
            }
            else Response.Redirect("Login.aspx");
        }



        private void FillYears()
        {
            DateTime dt = DateTime.Now;
            List<csYear> YearList = new List<csYear>();
            for (int i = dt.Year; i >= 2007 ; i--)
            {
                YearList.Add(new csYear(i));
            }
            inYear.DataSource = YearList;
            inYear.DataTextField = "Year";
            inYear.DataValueField = "Year";
            inYear.DataBind();
        }



        private void FillTeams()
        {
            inTeam.DataSource = MLBInfo.ScrapTeams("https://www.teamrankings.com/mlb/teams/?group=0");
            inTeam.DataTextField = "Team";
            inTeam.DataValueField = "TeamValue";
            inTeam.DataBind();
        }


        protected void Trends(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(inYear.Items[inYear.SelectedIndex].Value);
            string team = inTeam.Items[inTeam.SelectedIndex].Value;
            string OriginalTeam = team;
            if (team == "Chicago White Sox") team = "sox";
            else if (team == "Chicago Cubs") team = "cubs";
            else if (team == "Los Angeles Angels") team = "angels";
            else if (team == "Los Angeles Dodgers") team = "dodgers";
            else if (team == "New York Mets") team = "mets";
            else if (team == "New York Yankees") team = "yankees";
            else if (team == "San Francisco Giants") team = "giants";
            TrendList = MLBInfo.ScrapTrends(year, year.ToString(), inShow.Items[inShow.SelectedIndex].Value, inSituation.Items[inSituation.SelectedIndex].Value, inType.Items[inType.SelectedIndex].Value,3,"MLB", team);

            if (OriginalTeam != "")
            {
                  csMlbTrends mlb = MLBInfo.GetRank("https://www.teamrankings.com/mlb/team/" + OriginalTeam.ToLower().Replace(" ", "-").Trim().Replace(".","").Trim());
                if (mlb != null)
                {
                    lbRecord.Text = mlb.Record;
                    lbRank.Text = mlb.Rank;
                    lbStreak.Text = mlb.Streak;
                    lbTeam.Text = OriginalTeam;
                    mlb.SportName = "MLB";
                    mlb.Team = OriginalTeam;
                    MLBInfo.PuclicateRank(mlb);
                } else SetDefaultValue();
            }
            else SetDefaultValue();
            rptTable.DataSource = TrendList;
            rptTable.DataBind();
            MLBInfo.PuclicateTrends(TrendList);

            //runs per game
            Runs = MLBInfo.RunsPerGame("https://www.teamrankings.com/mlb/stat/runs-per-game?date="+ year + "-"+ DateTime.Now.Month +"-"+ DateTime.Now.Day, team, year);
            if(Runs != null)
            {
                runsTable.DataSource = Runs;
                runsTable.DataBind();
                MLBInfo.PuclicateStats(Runs, "Runs Per Game", year,"MLB");
            }

            //outs pitched
            OutPitched = MLBInfo.OutsPitched("https://www.teamrankings.com/mlb/stat/outs-pitched-per-game?date=" + year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day, team,year);
            if (OutPitched != null)
            {
                rptOutPitched.DataSource = OutPitched;
                rptOutPitched.DataBind();
                MLBInfo.PuclicateStats(OutPitched, "Outs Pitched", year, "MLB");
            }

            //runs allowed          
            RunsAllowed = MLBInfo.RunsAllowed("https://www.teamrankings.com/mlb/player-stat/runs-allowed?season_id=" + (year - 1380), team, year);
            if (RunsAllowed != null)
            {
                rptRunsAllowed.DataSource = RunsAllowed;
                rptRunsAllowed.DataBind();
                MLBInfo.PuclicateStats(RunsAllowed, "Runs Allowed", year, "MLB");
            }

        }


        public string Line2()
        {
            if(TrendList != null && TrendList.Count > 0)
            {
                return TrendList[0].H2;
            }

            return "";
        }


        private void SetDefaultValue()
        {
            lbRecord.Text = "-";
            lbRank.Text = "-";
            lbStreak.Text = "-";
            lbTeam.Text = "";
        }


        public string Line3()
        {
            if (TrendList != null && TrendList.Count > 0)
            {
                return TrendList[0].H3;
            }
            return "";
        }


        public string Line4()
        {
            if (TrendList != null && TrendList.Count > 0)
            {
                return TrendList[0].H4;
            }
            return "";
        }


        public string Line5()
        {
            if (TrendList != null && TrendList.Count > 0)
            {
                return TrendList[0].H5;
            }
            return "";
        }


        public string Team()
        {
            if (TrendList != null && TrendList.Count > 0)
            {
                return TrendList[0].H1;
            }
            return "";
        }


        public string Tag(int c)
        {
            if (Runs != null && Runs.Count > 0)
            {
                switch(c)
                {
                    case 1:
                        return "Rank";
                    case 2:
                        return "Team";
                    case 3:
                        return Runs[0].YearRuns.ToString();
                    case 4:
                        return "Last 3";
                    case 5:
                        return "Last 1";
                    case 6:
                        return "Home";
                    case 7:
                        return "Away";
                    case 8:
                        return (Runs[0].YearRuns - 1).ToString();
                }
            }
            return "";
        }



        public string TagAllowed(int c)
        {
            if (Runs != null && Runs.Count > 0)
            {
                switch (c)
                {
                    case 1:
                        return "Rank";
                    case 2:
                        return "Player";
                    case 3:
                        return "Team";
                    case 4:
                        return "Pos";
                    case 5:
                        return "Value";
                }
            }
            return "";
        }

    }
}