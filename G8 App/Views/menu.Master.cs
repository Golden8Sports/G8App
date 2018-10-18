using G8_App.Entities;
using G8_App.Security;
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
    public partial class menu : System.Web.UI.MasterPage
    {
        protected static string variable;
        protected static blAccessUser user = new blAccessUser();
        protected static int flag = 1;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }

        }


        public string GetData()
        {
           if(csUser.obj == null)
           {
                csUser.obj = user.GetAccessPages(csUser.LoginName);
           }

            return Json.Encode(csUser.obj);
        }


    }
}