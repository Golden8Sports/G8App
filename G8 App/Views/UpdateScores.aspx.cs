using G8_App.Entities;
using G8_App.Logic.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G8_App.Views
{
    public partial class UpdateScores : System.Web.UI.Page
    {
        private blCheckScores gameDB = new blCheckScores();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                if (!IsPostBack)
                {
                    userName.InnerText = csUser.Name + " - " + csUser.Profile;
                }
            }
            else Response.Redirect("Login.aspx");
        }

        protected void Update(object sender, EventArgs e)
        {
            idStaus.InnerHtml = "";
            string startD = Request[startDate.UniqueID];
            DateTime date = Convert.ToDateTime(startD);            
            gameDB.SyncUp(date.Year + "-" + ((date.Month < 10) ? "0" + date.Month.ToString() : date.Month.ToString()) + "-" + ((date.Day < 10) ? "0" + date.Day.ToString() : date.Day.ToString()));
            idStaus.InnerHtml = "Scores Updated for " + startD;
        }
    }
}