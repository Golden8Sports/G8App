using G8_App.Entities;
using HouseReport_BL.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G8_App.Views
{
    public partial class RangesReport : System.Web.UI.Page
    {
        private blSport sportDB = new blSport();
        private blLeague leagueDB = new blLeague();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                if (!IsPostBack)
                {
                    userName.InnerText = csUser.Name + " - " + csUser.Profile;
                    LoadSport(sender, e);
                }
            }
            else Response.Redirect("Login.aspx");
        }



        private void LoadSport(object sender, EventArgs e)
        {
            inSport.DataSource = sportDB.ListSportFromDGS();
            inSport.DataTextField = "SportName";
            inSport.DataValueField = "IdSport";
            inSport.DataBind();
        }



        protected void FillLeague(object sender, EventArgs e)
        {
            inLeague.DataSource = leagueDB.LeaguesBySport(inSport.Items[inSport.SelectedIndex].Value.Trim());
            inLeague.DataTextField = "LeagueDescription";
            inLeague.DataValueField = "IdLeague";
            inLeague.DataBind();
        }
    }
}