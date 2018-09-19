using G8_App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G8_App.Views.ProfileTools
{
    public partial class BusinessIntelligence : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                if (!IsPostBack)
                {
                    userName.InnerText = csUser.Name + " - " + csUser.Profile;
                    //LoadSport();
                    //LoadAgents();
                }
            }
            else Response.Redirect("Login.aspx");
        }
    }
}