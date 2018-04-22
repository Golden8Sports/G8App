using G8_App.Entities;
using HouseReport_BL.Logic;
using NHL_BL.Entities;
using NHL_BL.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G8_App.Views
{
    public partial class leansReport : System.Web.UI.Page
    {

        private blSport sportDB = new blSport();
        private blLeague leagueDB = new blLeague();
        private blGame gameDB = new blGame();
        private static ObservableCollection<csGame> GamesList = null;
        private csGenerateLeansExcel LeansExcel = new csGenerateLeansExcel();


        protected void Page_Load(object sender, EventArgs e)
        {
            userName.InnerText = csUser.Name + " - " + csUser.Profile;

            if (!IsPostBack)
            {
                LoadSport(sender,e);
            }
        }

        protected void FillLeague(object sender, EventArgs e)
        {
            inLeague.DataSource = leagueDB.LeaguesBySport(inSport.Items[inSport.SelectedIndex].Value);
            inLeague.DataTextField = "LeagueDescription";
            inLeague.DataValueField = "IdLeague";
            inLeague.DataBind();
        }

        protected void LoadGames(object sender, EventArgs e)
        {
            string startD = Request[startDate.UniqueID];
            string endD = Request[endDate.UniqueID];
            int league = -1;
            string IdSport = inSport.Items[inSport.SelectedIndex].Value;

            var spli1 = startD.Split('/');
            var spli2 = endD.Split('/');
            if (IdSport == null || IdSport == "ALL") IdSport = "";
            try { league = (inLeague.Items[inLeague.SelectedIndex].Text == "ALL") ? -1 : Convert.ToInt32(inLeague.Items[inSport.SelectedIndex].Value); } catch (Exception) { league = -1; }


            startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
            endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];

            GamesList = gameDB.ListGames(startD,endD,IdSport,league);

            if(GamesList != null)
            {
                rptTable.DataSource = GamesList;
                rptTable.DataBind();
            }
        }



        private void LoadSport(object sender, EventArgs e)
        {
            inSport.DataSource = sportDB.ListSport();
            inSport.DataTextField = "SportName";
            inSport.DataValueField = "IdSport";
            inSport.DataBind();
        }


        protected void GenerateExcelReport(object sender, EventArgs e)
        {
            if(GamesList != null)
            {
                string pathToFiles = Server.MapPath("/LeansReport");
                LeansExcel.GenerateExcel(GamesList, pathToFiles);
            }
        }
    }
}