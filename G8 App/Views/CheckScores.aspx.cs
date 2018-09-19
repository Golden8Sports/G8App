using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using G8_App.Entities;
using HouseReport_BL.Logic;
using G8_App.Entities.Games;
using G8_App.Logic.Administration;
using System.Collections.ObjectModel;

namespace G8_App.Views
{
    public partial class CheckScores : System.Web.UI.Page
    {
        private blSport sportDB = new blSport();
        private blCheckScores gameDB = new blCheckScores();
        private static ObservableCollection<csGame> GamesList = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                userName.InnerText = csUser.Name + " - " + csUser.Profile;

                if (!IsPostBack)
                {
                    LoadSport(sender, e);
                    gameDB.SyncUp();
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



        protected void LoadGames(object sender, EventArgs e)
        {
            string startD = Request[startDate.UniqueID];
            string endD = Request[endDate.UniqueID];
            string IdSport = inSport.Items[inSport.SelectedIndex].Value.Trim();
            string IdSportDonBest = (IdSport == null || IdSport == "ALL") ? "-1" : IdSport;
            string Status = inStatus.Items[inStatus.SelectedIndex].Value.Trim();
            if (IdSport == null || IdSport == "ALL") IdSport = "";
            var spli1 = startD.Split('/');
            var spli2 = endD.Split('/');                
            startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
            endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
            DateTime dt2 = Convert.ToDateTime(endD);
            dt2 = dt2.AddDays(1);
            endD = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;

            try
            {
                GamesList = gameDB.ListGamesFromDGS(startD, endD, IdSport, IdSportDonBest, Status);
                rptTable.DataSource = GamesList;
                rptTable.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "SetColor()", true);
                //Page..RegisterStartupScript(this.Page, Page.GetType(), "text", "SetColor()", true);
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('Error to get all the games: " + ex.Message + "');</script>");
            }
        }


        protected void ReSelect(object sender, EventArgs e)
        {
            LoadGames(sender,e);
        }



        protected void SetColor(object sender, EventArgs e)
        {
           
        }
    }
}