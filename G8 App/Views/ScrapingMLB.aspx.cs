using G8_App.Entities;
using G8_App.Entities.MLB;
using G8_App.Logic.MLB;
using G8_App.Logic.Scraping.Covers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G8_App.Views
{
    public partial class ScrapingMLB : System.Web.UI.Page
    {

        private blCovers coverDB = new blCovers();
        private static ObservableCollection<csTeamStat> ScrapMLB_StatList = new ObservableCollection<csTeamStat>();
        private blStatMLB statMLB = new blStatMLB();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                userName.InnerText = csUser.Name + " - " + csUser.Profile;
            }
            else Response.Redirect("Login.aspx");
        }



        protected void Scrap(object sender, EventArgs e)
        {
            ScrapMLB_StatList = coverDB.ScrapMLB_Stats(Request[url.UniqueID]);
            rptTable.DataSource = ScrapMLB_StatList;
            rptTable.DataBind();
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('Message here');", true);
        }


        protected void SaveToDB(object sender, EventArgs e)
        {
            if(ScrapMLB_StatList != null)
            {
                foreach (var i in ScrapMLB_StatList)
                {
                    statMLB.InsertDataMLB(i);
                }
            }
        }


    }
}