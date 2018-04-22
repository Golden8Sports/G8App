using G8_App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using G8_App.G8AppService;

namespace G8_App.Views
{
    public partial class dashboard : System.Web.UI.Page
    {
        G8ServiceClient cli = new G8ServiceClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            userName.InnerText = csUser.Name + " - " + csUser.Profile;
        }

        protected void Perilla(object sender, EventArgs e)
        {
            //rptTable.DataSource = cli.ListarTest("");
           // rptTable.DataBind();
        }
    }
}