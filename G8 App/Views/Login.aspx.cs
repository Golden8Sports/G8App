using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using G8_App.Logic;
using G8_App.Views;
using NHL_BL.Connection;

namespace G8_App.Views
{
    public partial class Login : System.Web.UI.Page
    {
        private blLogin loginDB = new blLogin();

        protected void Page_Load(object sender, EventArgs e)
        {

        }



        public void ClickLogin(object sender, EventArgs e)
        {
            string user = Request[u.UniqueID];
            string pass = Request[p.UniqueID];

            if (user != null && pass != null)
            {
                // csConnection.OpenConnection();            
                string log = loginDB.Login(user, pass);
                if (log == "TRUE") Response.Redirect("dashboard.aspx");
                else if (log == "FALSE") Response.Write("<script>window.alert('User or Password Incorrect');</script>");
                else Response.Write("<script>window.alert('" + log + "');</script>");
                p.Value = "";
            }
        }
    }
}