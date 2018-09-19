using G8_App.Entities;
using G8_App.Logic.Business_Intelligence;
using G8_App.Logic.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G8_App.Views
{
    public partial class BusinessR : System.Web.UI.Page
    {
        private blAgent agentDB = new blAgent();
        private blSummary sumaDB = new blSummary();
        private static string StartDate = "";
        private static string EndDate = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                userName.InnerText = csUser.Name + " - " + csUser.Profile;

                if (!IsPostBack)
                {
                    LoadAgents();
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

                rptTopPlayers.DataSource = sumaDB.TopPlayerList(StartDate, EndDate, "", "-1", IdAgent, "", "", "");
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

                rptOver.DataSource = sumaDB.OverAll(StartDate, EndDate, IdAgent);
                rptOver.DataBind();
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
        }

        private static string GetPlusDate(string endD)
        {
            DateTime dt2 = Convert.ToDateTime(endD);
            dt2 = dt2.AddDays(1);
            endD = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;
            return endD;
        }



        private void LoadAgents()
        {
            inAgent.DataSource = agentDB.AgentList();
            inAgent.DataTextField = "Name";
            inAgent.DataValueField = "Value";
            inAgent.DataBind();
        }
    }
}