using G8_App.Entities;
using G8_App.Entities.Games;
using G8_App.Logic.Games;
using HouseReport_BL.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using G8_App.Logic.Lines;

namespace G8_App.Views
{
    public partial class Games : System.Web.UI.Page
    {
        private blSport sportDB = new blSport();
        private blLeague leagueDB = new blLeague();
        private blGame gameDB = new blGame();
        private blLineType lineTypeDB = new blLineType();
        private static ObservableCollection<csGame> GamesList = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                userName.InnerText = csUser.Name + " - " + csUser.Profile;

                if (!IsPostBack)
                {
                    LoadSport(sender, e);
                    LoadLinesType(sender, e);
                }
            }
            else Response.Redirect("Login.aspx");
        }

        private void LoadLinesType(object sender, EventArgs e)
        {
            inLineType.DataSource = lineTypeDB.GetLinesType();
            inLineType.DataTextField = "Description";
            inLineType.DataValueField = "IdLineType";
            inLineType.DataBind();
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

        protected void LoadGames(object sender, EventArgs e)
        {
            string startD = Request[startDate.UniqueID];
            string endD = Request[endDate.UniqueID];
            int league = -1;
            string IdSport = inSport.Items[inSport.SelectedIndex].Value.Trim();
            int idLineType = Convert.ToInt32(inLineType.Items[inLineType.SelectedIndex].Value);

            var spli1 = startD.Split('/');
            var spli2 = endD.Split('/');
            if (IdSport == null || IdSport == "ALL") IdSport = "";
            try { league = (inLeague.Items[inLeague.SelectedIndex].Text == "ALL") ? -1 : Convert.ToInt32(inLeague.Items[inLeague.SelectedIndex].Value); } catch (Exception) { league = -1; }

            startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
            endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
            endD = GetPlusDate(endD);

            GamesList = gameDB.ListGamesFromDGS(startD, endD, IdSport, league, idLineType);

            if (GamesList != null)
            {
                rptTable.DataSource = GamesList;
                rptTable.DataBind();
            }
        }



        private static string GetPlusDate(string endD)
        {
            DateTime dt2 = Convert.ToDateTime(endD);
            dt2 = dt2.AddDays(1);
            endD = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;
            return endD;
        }

    }
}