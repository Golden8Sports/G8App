using G8_App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using G8_App.G8AppService;
using HouseReport_BL.Logic;
using G8_App.Logic.Profiling;

namespace G8_App.Views
{
    public partial class dashboard : System.Web.UI.Page
    {
        private G8ServiceClient cli = new G8ServiceClient();
        private blSummary sumaDB = new blSummary();
        private blSport sportDB = new blSport();

        protected void Page_Load(object sender, EventArgs e)
        {
            userName.InnerText = csUser.Name + " - " + csUser.Profile;
            if (!IsPostBack)
            {
                LoadSport(sender, e);

            }
        }


        protected void LoadSummary(object sender, EventArgs e)
        {
            try
            {
                string IdSport = (inSport.Items[inSport.SelectedIndex].Value == "ALL") ? "" : inSport.Items[inSport.SelectedIndex].Value.Trim();
                string startD = Request[startDate.UniqueID];
                string endD = Request[endDate.UniqueID];
                var spli1 = startD.Split('/');
                var spli2 = endD.Split('/');
                startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
                endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];


                rptTable.DataSource = sumaDB.SummaryProfile(startD, endD,IdSport);
                rptTable.DataBind();
               // DateTime t = t.addM
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('Error: " + ex + "');</script>");
            }
        }

        private void LoadSport(object sender, EventArgs e)
        {
            inSport.DataSource = sportDB.ListSport();
            inSport.DataTextField = "SportName";
            inSport.DataValueField = "IdSport";
            inSport.DataBind();
        }
    }
}