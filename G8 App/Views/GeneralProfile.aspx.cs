using G8_App.Entities;
using G8_App.Entities.Pinnacle_Appi;
using G8_App.Entities.Profiling;
using G8_App.Logic.Business_Intelligence;
using G8_App.Logic.Profiling;
using HouseReport_BL.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace G8_App.Views
{
    public partial class GeneralProfile : System.Web.UI.Page
    {
        private blSummary sumaDB = new blSummary();
        private blSport sportDB = new blSport();
        private blLeague leagueDB = new blLeague();
        private blAgent agentDB = new blAgent();
        private blPlayer playerDB = new blPlayer();
        private static blProfile profileDB = new blProfile();
        private ObservableCollection<csSummary> profList = new ObservableCollection<csSummary>();

        private static string LeagueId = "";
        private static string SportName = "";
        private static string LeagueNameString = "";

        //static list
        private static ObservableCollection<csProfile> BySportList = new ObservableCollection<csProfile>();
        private static ObservableCollection<csProfile> ByLeagueList = new ObservableCollection<csProfile>();
        private static ObservableCollection<csProfile> ByFavDogList = new ObservableCollection<csProfile>();
        private static ObservableCollection<csProfile> BySideList = new ObservableCollection<csProfile>();
        private static ObservableCollection<csProfile> ByTeamList = new ObservableCollection<csProfile>();
        private static ObservableCollection<csProfile> ByWeekList = new ObservableCollection<csProfile>();
        private static ObservableCollection<csProfile> ByMomentDayList = new ObservableCollection<csProfile>();
        private static ObservableCollection<csProfile> ByPeriodList = new ObservableCollection<csProfile>();
        private static ObservableCollection<csProfile> ByWagerTypeList = new ObservableCollection<csProfile>();
        private static ObservableCollection<csProfile> ByBetsList = new ObservableCollection<csProfile>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] !=  null)
            {
                if (!IsPostBack)
                {
                    userName.InnerText = csUser.Name + " - " + csUser.Profile;
                    LoadSport(sender, e);
                    LoadAgents();
                    LoadPlayers(sender,e);                 
                }
            }
            else Response.Redirect("Login.aspx");
        }

        private void LoadAgents()
        {
            inAgent.DataSource = agentDB.AgentList();
            inAgent.DataTextField = "Name";
            inAgent.DataValueField = "Value";
            inAgent.DataBind();
            
        }

        public void GetSportsByPlayer()
        {
            if(SportName == "")
            {
                ObservableCollection<NHL_BL.Entities.csSport> l = profileDB.GetSportsByPlayer();
                inBySport.DataSource = l;
                inBySport.DataTextField = "SportName";
                inBySport.DataValueField = "IdSport";
                inBySport.DataBind();

                inBySportPeriod.DataSource = l;
                inBySportPeriod.DataTextField = "SportName";
                inBySportPeriod.DataValueField = "IdSport";
                inBySportPeriod.DataBind();

                inBySportFavDog.DataSource = l;
                inBySportFavDog.DataTextField = "SportName";
                inBySportFavDog.DataValueField = "IdSport";
                inBySportFavDog.DataBind();

                inBySportWagerType.DataSource = l;
                inBySportWagerType.DataTextField = "SportName";
                inBySportWagerType.DataValueField = "IdSport";
                inBySportWagerType.DataBind();

                inBySportSide.DataSource = l;
                inBySportSide.DataTextField = "SportName";
                inBySportSide.DataValueField = "IdSport";
                inBySportSide.DataBind();

                inBySportBets.DataSource = l;
                inBySportBets.DataTextField = "SportName";
                inBySportBets.DataValueField = "IdSport";
                inBySportBets.DataBind();

            }
            else
            {
                ObservableCollection<NHL_BL.Entities.csSport> l = new ObservableCollection<NHL_BL.Entities.csSport>();
                l.Add(new NHL_BL.Entities.csSport(SportName, SportName));
                inBySport.DataSource = l;
                inBySport.DataTextField = "SportName";
                inBySport.DataValueField = "IdSport";
                inBySport.DataBind();

                inBySportPeriod.DataSource = l;
                inBySportPeriod.DataTextField = "SportName";
                inBySportPeriod.DataValueField = "IdSport";
                inBySportPeriod.DataBind();

                inBySportFavDog.DataSource = l;
                inBySportFavDog.DataTextField = "SportName";
                inBySportFavDog.DataValueField = "IdSport";
                inBySportFavDog.DataBind();

                inBySportWagerType.DataSource = l;
                inBySportWagerType.DataTextField = "SportName";
                inBySportWagerType.DataValueField = "IdSport";
                inBySportWagerType.DataBind();

                inBySportSide.DataSource = l;
                inBySportSide.DataTextField = "SportName";
                inBySportSide.DataValueField = "IdSport";
                inBySportSide.DataBind();

                inBySportBets.DataSource = l;
                inBySportBets.DataTextField = "SportName";
                inBySportBets.DataValueField = "IdSport";
                inBySportBets.DataBind();
            }
        }

        protected void LoadPlayers(object sender, EventArgs e)
        {
            try
            {
                inPlayer.DataSource = playerDB.GetPlayersByAgent(Convert.ToInt32(inAgent.Items[inAgent.SelectedIndex].Value.Trim()));
                inPlayer.DataTextField = "Player";
                inPlayer.DataValueField = "IdPlayer";
                inPlayer.DataBind();
            }
            catch (Exception)
            {
            }
        }

        protected void FillLeague(object sender, EventArgs e)
        {
            inLeague.DataSource = leagueDB.LeaguesBySport(inSport.Items[inSport.SelectedIndex].Value.Trim());
            inLeague.DataTextField = "LeagueDescription";
            inLeague.DataValueField = "IdLeague";
            inLeague.DataBind();
        }

        protected void LoadSummary(object sender, EventArgs e)
        {
            try
            {
                string IdSport = (inSport.Items[inSport.SelectedIndex].Value == "ALL") ? "" : inSport.Items[inSport.SelectedIndex].Value.Trim();
                string IdLEague = (inLeague.Items[inLeague.SelectedIndex].Value == "ALL" || inLeague.Items[inLeague.SelectedIndex].Value == "-1") ? "-1" : inLeague.Items[inLeague.SelectedIndex].Value.Trim();
                LeagueId = inLeague.Items[inLeague.SelectedIndex].Value.Trim();
                if (LeagueId == "ALL") LeagueId = "-1";
                string startD = Request[startDate.UniqueID];           
                string endD = Request[endDate.UniqueID];
                idDates.InnerHtml = "Dates: " + startD + " - " + endD;
                string Agent = (inAgent.Items[inAgent.SelectedIndex].Value == "ALL") ? "" : inAgent.Items[inAgent.SelectedIndex].Value.Trim();
                string Player = (inPlayer.Items[inPlayer.SelectedIndex].Text == "ALL") ? "" : inPlayer.Items[inPlayer.SelectedIndex].Text.Trim();
                var spli1 = startD.Split('/');
                var spli2 = endD.Split('/');
                startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
                endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
                endD = GetPlusDate(endD);
                int IdAgent = (Convert.ToInt32(Agent) == 0) ? -1 : Convert.ToInt32(Agent);
                SportName = IdSport;             
                profList = sumaDB.SummaryProfile(startD, endD, IdSport, IdLEague, IdAgent, Player);
                GetSportsByPlayer();
                idFilters.InnerHtml = "Sport: " + ((SportName == "") ? "ALL" : SportName);
                LeagueNameString = (inLeague.Items[inLeague.SelectedIndex].Text.Trim() == "ALL") ? "" : inLeague.Items[inLeague.SelectedIndex].Text.Trim();

                idLeagueFilter.InnerHtml = "League: " + ((LeagueId == "-1") ? "ALL" : LeagueNameString);
                

                if (inExtraFilters.Checked == true) DetailedFilters();
                else
                {
                    rptTable.DataSource = profList;
                    rptTable.DataBind();
                }

                inExtraFilters.Checked = false;
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('Error: " + ex + "');</script>");
            }
        }

        private void LoadSport(object sender, EventArgs e)
        {
            inSport.DataSource = sportDB.ListSportFromDGS();
            inSport.DataTextField = "SportName";
            inSport.DataValueField = "IdSport";
            inSport.DataBind();
        }

        private void DetailedFilters()
        {
            if(profList != null)
            {
                ObservableCollection<csSummary> data = new ObservableCollection<csSummary>();
                int hold,minNet, bets, lineMov, scalpCris, scalpPPH, scalpPinni, scalpJaz, scalp5Dimes, maxNet,beatLine;
                double winP;

                foreach (var i in profList)
                {

                    if(!string.IsNullOrWhiteSpace(Request[inNet.UniqueID])) minNet = Convert.ToInt32(Request[inNet.UniqueID]); else minNet = i.Net;
                    if(!string.IsNullOrWhiteSpace(Request[inHold.UniqueID])) hold = Convert.ToInt32(Request[inHold.UniqueID]); else hold = (int)i.HoldPercentaje;
                    if (!string.IsNullOrWhiteSpace(Request[inBet.UniqueID])) bets = Convert.ToInt32(Request[inBet.UniqueID]); else bets = i.Bets;
                    if (!string.IsNullOrWhiteSpace(Request[inWins.UniqueID])) winP = Convert.ToInt32(Request[inWins.UniqueID]); else winP = i.WinPercentaje;
                    if (!string.IsNullOrWhiteSpace(Request[inLineMover.UniqueID])) lineMov = Convert.ToInt32(Request[inLineMover.UniqueID]); else lineMov = i.MoveLine;
                    if (!string.IsNullOrWhiteSpace(Request[inCris.UniqueID])) scalpCris = Convert.ToInt32(Request[inCris.UniqueID]); else scalpCris = i.ScalpingCris;
                    if (!string.IsNullOrWhiteSpace(Request[inPPH.UniqueID])) scalpPPH = Convert.ToInt32(Request[inPPH.UniqueID]); else scalpPPH = i.ScalpingPPH;
                    if (!string.IsNullOrWhiteSpace(Request[inPinni.UniqueID])) scalpPinni = Convert.ToInt32(Request[inPinni.UniqueID]); else scalpPinni = i.ScalpingPinni;
                    if (!string.IsNullOrWhiteSpace(Request[inJaz.UniqueID])) scalpJaz = Convert.ToInt32(Request[inJaz.UniqueID]); else scalpJaz = i.ScalpingJazz;
                    if (!string.IsNullOrWhiteSpace(Request[in5Dimes.UniqueID])) scalp5Dimes = Convert.ToInt32(Request[in5Dimes.UniqueID]); else scalp5Dimes = i.Scalping5Dimes;
                    if (!string.IsNullOrWhiteSpace(Request[inMaxNet.UniqueID])) maxNet = Convert.ToInt32(Request[inMaxNet.UniqueID]); else maxNet = i.Net;
                    if (!string.IsNullOrWhiteSpace(Request[inBeatLine.UniqueID])) beatLine = Convert.ToInt32(Request[inBeatLine.UniqueID]); else beatLine = i.BeatLine;


                    if ((i.Net >= minNet && i.Net <= maxNet) && i.HoldPercentaje >= hold && i.Bets >= bets && i.WinPercentaje >= winP && i.MoveLine >= lineMov &&
                       i.ScalpingCris >= scalpCris && i.ScalpingPPH >= scalpPPH && i.ScalpingPinni >= scalpPinni && i.ScalpingJazz >= scalpJaz &&
                       i.Scalping5Dimes >= scalp5Dimes && i.BeatLine >= beatLine)
                    {
                        data.Add(i);
                    }


                }

                rptTable.DataSource = data;
                rptTable.DataBind();
            }


            frm.Attributes.Clear();
            
        }

        #region WeRequest Funtions

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string BySport(string startD, string endD, string player)
        {
            var spli1 = startD.Split('/'); startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
            var spli2 = endD.Split('/'); endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
            endD = GetPlusDate(endD);
            BySportList = new ObservableCollection<csProfile>();
            BySportList = profileDB.ProfileBySport(startD, endD, player,SportName);
            var obj = Json.Encode(BySportList);
            return obj;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ByWeek(string startD, string endD, string player)
        {
            var spli1 = startD.Split('/'); startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
            var spli2 = endD.Split('/'); endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
            endD = GetPlusDate(endD);
            ByWeekList = new ObservableCollection<csProfile>();
            ByWeekList = profileDB.ProfileByWeek(startD, endD, player, SportName, LeagueId);
            var obj = Json.Encode(ByWeekList);
            return obj;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ByMomentDay(string startD, string endD, string player)
        {
            var spli1 = startD.Split('/'); startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
            var spli2 = endD.Split('/'); endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
            endD = GetPlusDate(endD);
            ByMomentDayList = new ObservableCollection<csProfile>();
            ByMomentDayList = profileDB.ProfileByMomentDay(startD, endD, player, SportName, LeagueId);
            var obj = Json.Encode(ByMomentDayList);
            return obj;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetLeaguesPLayer(string d1, string d2, string player, string sport)
        {
            var spli1 = d1.Split('/'); d1 = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
            var spli2 = d2.Split('/'); d2 = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
            d2 = GetPlusDate(d2);
            var obj = Json.Encode(new blLeague().LeaguesByPlayerBI(d1, d2, player, (sport == null) ? SportName : sport));
            GeneralProfile fdf = new GeneralProfile();
            return obj;           
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ByTeam(string startD, string endD, string player)
        {
            var spli1 = startD.Split('/'); startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
            var spli2 = endD.Split('/'); endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
            endD = GetPlusDate(endD);
            ByTeamList = new ObservableCollection<csProfile>();
            ByTeamList = profileDB.ProfileByTeam(startD, endD, player, SportName, LeagueId);
            var obj = Json.Encode(ByTeamList);
            return obj;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ByPeriodR(string startD, string endD, string player,string sport, string league, string favdog)
        {
            var spli1 = startD.Split('/'); startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
            var spli2 = endD.Split('/'); endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
            endD = GetPlusDate(endD);
            ByPeriodList = new ObservableCollection<csProfile>();
            ByPeriodList = profileDB.ProfileByPeriod(startD, endD, player, ((SportName == "") ? ((sport == null) ? "" : sport) : SportName), ((LeagueId == "-1") ? ((league == null || league == "-1") ? "-1" : league) : LeagueId), favdog);
            var obj = Json.Encode(ByPeriodList);
            return obj;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string BySide(string startD, string endD, string player, string sport, string league, string favdog)
        {
            var spli1 = startD.Split('/'); startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
            var spli2 = endD.Split('/'); endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
            endD = GetPlusDate(endD);
            BySideList = new ObservableCollection<csProfile>();
            BySideList = profileDB.ProfileBySide(startD, endD, player, ((SportName == "") ? ((sport == null) ? "" : sport) : SportName), ((LeagueId == "-1") ? ((league == null || league == "-1") ? "-1" : league) : LeagueId), favdog);
            var obj = Json.Encode(BySideList);
            return obj;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ByWagerPlayR(string startD, string endD, string player, string sport, string league, string favdog)
        {

            var spli1 = startD.Split('/'); startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
            var spli2 = endD.Split('/'); endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
            endD = GetPlusDate(endD);
            ByWagerTypeList = new ObservableCollection<csProfile>();
            ByWagerTypeList = profileDB.ProfileByWagerType(startD, endD, player, ((SportName == "") ? ((sport == null) ? "" : sport) : SportName), ((LeagueId == "-1") ? ((league == null || league == "-1") ? "-1" : league) : LeagueId), favdog);
            var obj = Json.Encode(ByWagerTypeList);
            return obj;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ByFavDogR(string startD, string endD, string player, string sport, string league)
        {
            var spli1 = startD.Split('/'); startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
            var spli2 = endD.Split('/'); endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
            endD = GetPlusDate(endD);
            ByFavDogList = new ObservableCollection<csProfile>();
            ByFavDogList = profileDB.ProfileByFavDog(startD, endD, player, ((SportName == "") ? ((sport == null) ? "" : sport) : SportName), ((LeagueId == "-1") ? ((league == null || league == "-1") ? "-1" : league) : LeagueId));
            var obj = Json.Encode(ByFavDogList);
            return obj;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ByLeagueR(string startD, string endD, string player, string sport, string favdog)
        {
            var spli1 = startD.Split('/'); startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
            var spli2 = endD.Split('/'); endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
            endD = GetPlusDate(endD);
            ByLeagueList = new ObservableCollection<csProfile>();
            ByLeagueList = profileDB.ProfileByLeague(startD, endD, player, ((SportName == "") ? ((sport == null) ? "" : sport) :  SportName), favdog, LeagueNameString);
            var obj = Json.Encode(ByLeagueList);
            return obj;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetBets(string startD, string endD, string player,string sport, string league, string favdog, string wagerplay)
        {
            var spli1 = startD.Split('/'); startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
            var spli2 = endD.Split('/'); endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
            endD = GetPlusDate(endD);
            ByBetsList = new ObservableCollection<csProfile>();
            ByBetsList = profileDB.ProfileByBets(startD, endD, player, ((SportName == "") ? ((sport == null) ? "" : sport) : SportName), ((LeagueId == "-1") ? ((league == null || league == "-1") ? "-1" : league) : LeagueId), favdog, (wagerplay == null) ? "": wagerplay);
            var obj = Json.Encode(ByBetsList);
            return obj;
        }
        #endregion

        private static string GetPlusDate(string endD)
        {
            DateTime dt2 = Convert.ToDateTime(endD);
            dt2 = dt2.AddDays(1);
            endD = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;
            return endD;
        }

    }
}