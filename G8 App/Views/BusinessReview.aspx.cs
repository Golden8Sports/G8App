using G8_App.Entities;
using G8_App.Logic.Business_Intelligence;
using G8_App.Logic.Profiling;
using HouseReport_BL.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G8_App.Views
{
    public partial class BusinessReview : System.Web.UI.Page
    {
        private static blProfile profileDB = new blProfile();
        private blAgent agentDB = new blAgent();
        private static blSummary sumaDB = new blSummary();
        private blSport sportDB = new blSport();
        private static string StartDate = "";
        private static string EndDate = "";
        private static int IdAgent = 0;
        private static string Sport = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                userName.InnerText = csUser.Name + " - " + csUser.Profile;

                if (!IsPostBack)
                {
                    LoadAgents();
                    LoadSport();
                }
            }
            else Response.Redirect("Login.aspx");
        }


        private void LoadTopPlayers(object sender, EventArgs e)
        {
            try
            {
                StartDate = Request[startDate.UniqueID];
                EndDate = Request[endDate.UniqueID];
                string Agent = (inAgent.Items[inAgent.SelectedIndex].Value == "ALL") ? "" : inAgent.Items[inAgent.SelectedIndex].Value.Trim();
                var spli1 = StartDate.Split('/');
                var spli2 = EndDate.Split('/');
                StartDate = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
                EndDate = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
                EndDate = GetPlusDate(EndDate);
                int IdAgent = (Convert.ToInt32(Agent) == 0) ? -1 : Convert.ToInt32(Agent);
                Sport = inSport.Items[inSport.SelectedIndex].Value;
                if (Sport == "ALL")
                {
                    Sport = "";
                }

                rptTopPlayers.DataSource = sumaDB.TopPlayerList(StartDate, EndDate, Sport, "-1", IdAgent, "", "", "");
                rptTopPlayers.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('Error: " + ex + "');</script>");
            }
        }





        private void LoadOverAll()
        {
            try
            {
                StartDate = Request[startDate.UniqueID];
                EndDate = Request[endDate.UniqueID];
                string Agent = (inAgent.Items[inAgent.SelectedIndex].Value == "ALL") ? "" : inAgent.Items[inAgent.SelectedIndex].Value.Trim();
                var spli1 = StartDate.Split('/');
                var spli2 = EndDate.Split('/');
                StartDate = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
                EndDate = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
                EndDate = GetPlusDate(EndDate);
                int IdAgent = (Convert.ToInt32(Agent) == 0) ? -1 : Convert.ToInt32(Agent);

                rptOver.DataSource = sumaDB.OverAll(StartDate, EndDate, IdAgent, Sport);
                rptOver.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('Error: " + ex + "');</script>");
            }
        }




        private void LoadSportOverAll()
        {
            try
            {
                StartDate = Request[startDate.UniqueID];
                EndDate = Request[endDate.UniqueID];
                string Agent = (inAgent.Items[inAgent.SelectedIndex].Value == "ALL") ? "" : inAgent.Items[inAgent.SelectedIndex].Value.Trim();
                var spli1 = StartDate.Split('/');
                var spli2 = EndDate.Split('/');
                StartDate = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
                EndDate = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
                EndDate = GetPlusDate(EndDate);
                IdAgent = (Convert.ToInt32(Agent) == 0) ? -1 : Convert.ToInt32(Agent);

                rptSport.DataSource = sumaDB.BusinessBySport(StartDate, EndDate, IdAgent, Sport);
                rptSport.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('Error: " + ex + "');</script>");
            }
        }



        protected void LoadData(object sender, EventArgs e)
        {
            LoadTopPlayers(sender, e);
            LoadOverAll();
            LoadSportOverAll();
        }

        private static string GetPlusDate(string endD)
        {
            DateTime dt2 = Convert.ToDateTime(endD);
            dt2 = dt2.AddDays(1);
            endD = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;
            return endD;
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



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object GetBreakDownPlayer(string player)
        {
            return Json.Encode(profileDB.BreakDownPlayer(StartDate, EndDate, Sport, "-1", player, "", ""));
        }





        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object GetBreakDownSport(string sport)
        {
            return Json.Encode(sumaDB.OverAll(StartDate, EndDate, IdAgent, sport));
        }
    }
}