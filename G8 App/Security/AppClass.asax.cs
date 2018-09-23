﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace G8_App
{
    public class AppClass : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["App"] = "";
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["Login"] = null;
            Session.Timeout = 1800;           
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            //Response.AddHeader("Refresh", Session.Timeout + ";URL=Login.aspx");
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}