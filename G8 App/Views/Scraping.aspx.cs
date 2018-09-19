using G8_App.Entities;
using G8_App.Logic.Scraping.Covers;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G8_App.Views
{
    public partial class Scraping : System.Web.UI.Page
    {
        private blCovers coversDB = new blCovers();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                userName.InnerText = csUser.Name + " - " + csUser.Profile;
            }
            else Response.Redirect("Login.aspx");
        }

        


        protected void ScrapMiniPage(object sender, EventArgs e)
        {
            try
            {
                rptTable.DataSource = coversDB.Scrap(Request[url.UniqueID]);
                rptTable.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('Error to extract the information: " + ex.Message + "');</script>");
            }
        }




        protected void ScrapExcel(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();                
                Response.ContentType = "text/plain";
                Response.AddHeader("Content-Disposition", "attachment;filename=Scrap.csv");
                Response.Write(coversDB.ScrapToExcel(Request[url.UniqueID]));
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('Error to extract the information: " + ex.Message + "');</script>");
            }
        }



    }
}