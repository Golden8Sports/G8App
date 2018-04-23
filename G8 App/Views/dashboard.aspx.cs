using G8_App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using G8_App.G8AppService;
using HouseReport_BL.Logic;

namespace G8_App.Views
{
    public partial class dashboard : System.Web.UI.Page
    {
        private G8ServiceClient cli = new G8ServiceClient();
        private blSport sportDB = new blSport();

        protected void Page_Load(object sender, EventArgs e)
        {
            userName.InnerText = csUser.Name + " - " + csUser.Profile;
            if (!IsPostBack)
            {
                LoadSport(sender, e);

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