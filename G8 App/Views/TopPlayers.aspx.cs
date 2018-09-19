using G8_App.Entities;
using G8_App.Entities.Profiling;
using G8_App.Logic.Business_Intelligence;
using G8_App.Logic.Games;
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
    public partial class TopPlayers : System.Web.UI.Page
    {

        private blSport sportDB = new blSport();
        private blAgent agentDB = new blAgent();
        private blPlayer playerDB = new blPlayer();
        private blLeague leagueDB = new blLeague();
        private static blGame gameDB = new blGame();
        private blSummary sumaDB = new blSummary();
        private static string StartDate = "";
        private static string EndDate = "";
        private static string Player = "";
        private static string IdSport = "";
        private static string WagerPlay = "";
        private static string WagerType = "";
        private static string IdLeague = "-1";
        private static blProfile profileDB = new blProfile();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                userName.InnerText = csUser.Name + " - " + csUser.Profile;

                if (!IsPostBack)
                {
                    LoadSport();
                    LoadAgents();
                }
            }
            else Response.Redirect("Login.aspx");
        }

        private void LoadSport()
        {
            inSport.DataSource = sportDB.ListSportFromDGS();
            inSport.DataTextField = "SportName";
            inSport.DataValueField = "IdSport";
            inSport.DataBind();
        }


        private void LoadAgents()
        {
            inAgent.DataSource = agentDB.AgentList();
            inAgent.DataTextField = "Name";
            inAgent.DataValueField = "Value";
            inAgent.DataBind();
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


        protected void LoadTopPlayers(object sender, EventArgs e)
        {
            try
            {
                IdSport = (inSport.Items[inSport.SelectedIndex].Value == "ALL") ? "" : inSport.Items[inSport.SelectedIndex].Value.Trim();
                IdLeague = (inLeague.Items[inLeague.SelectedIndex].Text == "ALL") ? "-1" : inLeague.Items[inLeague.SelectedIndex].Value.Trim();
                StartDate = Request[startDate.UniqueID];
                EndDate = Request[endDate.UniqueID];
                string Agent = (inAgent.Items[inAgent.SelectedIndex].Value == "ALL") ? "" : inAgent.Items[inAgent.SelectedIndex].Value.Trim();
                Player = (inPlayer.Items[inPlayer.SelectedIndex].Text == "ALL") ? "" : inPlayer.Items[inPlayer.SelectedIndex].Text.Trim();
                var spli1 = StartDate.Split('/');
                var spli2 = EndDate.Split('/');
                StartDate = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
                EndDate = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
                EndDate = GetPlusDate(EndDate);
                int IdAgent = (Convert.ToInt32(Agent) == 0) ? -1 : Convert.ToInt32(Agent);

                WagerPlay = inWagerPlay.Items[inWagerPlay.SelectedIndex].Value;
                WagerType = inBetType.Items[inBetType.SelectedIndex].Value;

                rptTable.DataSource = sumaDB.TopPlayerList(StartDate, EndDate, IdSport, IdLeague, IdAgent, Player, WagerType, WagerPlay);
                rptTable.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('Error: " + ex + "');</script>");
            }
        }


        private static string GetPlusDate(string endD)
        {
            DateTime dt2 = Convert.ToDateTime(endD);
            dt2 = dt2.AddDays(1);
            endD = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;
            return endD;
        }



        //web method
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object GetBreakDownPlayer(string player)
        {
            return Json.Encode(profileDB.BreakDownPlayer(StartDate,EndDate,IdSport,IdLeague, player, WagerPlay,WagerType));
        }

    }
}