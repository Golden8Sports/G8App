using G8_App.Entities;
using G8_App.Entities.Leans;
using G8_App.Logic.Profiling;
using G8_App.Logic.WL_BI;
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
        private csWIBI biDB = new csWIBI();
        private static ObservableCollection<csGame> GamesList = null;
        private csGenerateLeansExcel LeansExcel = new csGenerateLeansExcel();
        private blPlayer playerDB = new blPlayer();
        private static ObservableCollection<csBet> BetsWithLeans = new ObservableCollection<csBet>();
        private static ObservableCollection<csBet> NoLeansBets = new ObservableCollection<csBet>();
        private static ObservableCollection<csBet> LeansBets = new ObservableCollection<csBet>();
        private static ObservableCollection<csBet> ByGameList = new ObservableCollection<csBet>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Login"] != null)
            {
                userName.InnerText = csUser.Name + " - " + csUser.Profile;

                if (!IsPostBack)
                {
                    LoadSport(sender, e);
                    FillPlayers();
                }
            }else Response.Redirect("Login.aspx");

        }

        private void FillPlayers()
        {
            inPlayer.DataSource = playerDB.GetPlayers();
            inPlayer.DataTextField = "Player";
            inPlayer.DataValueField = "IdPlayer";
            inPlayer.DataBind();
        }

        protected void FillLeague(object sender, EventArgs e)
        {
            inLeague.DataSource = leagueDB.LeaguesBySport(inSport.Items[inSport.SelectedIndex].Value);
            inLeague.DataTextField = "LeagueDescription";
            inLeague.DataValueField = "IdLeague";
            inLeague.DataBind();
        }



        protected void UpdateBI(object sender, EventArgs e)
        {
            biDB.UpdateBI();
        }

        protected void LoadGames(object sender, EventArgs e)
        {
            int league = -1;
            string IdSport = inSport.Items[inSport.SelectedIndex].Value;
            string startD = Request[startDate.UniqueID];
            string endD = Request[endDate.UniqueID];
            string player = inPlayer.Items[inPlayer.SelectedIndex].Text;

            var spli1 = startD.Split('/');
            var spli2 = endD.Split('/');

            startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
            endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];

            DateTime end = Convert.ToDateTime(endD);
            end = end.AddDays(1);
            endD = end.Year + "-" + end.Month + "-" + end.Day;

            if (IdSport == null || IdSport == "ALL") IdSport = "";
            try { league = (inLeague.Items[inLeague.SelectedIndex].Text == "ALL") ? -1 : Convert.ToInt32(inLeague.Items[inLeague.SelectedIndex].Value); } catch (Exception) { league = -1; }

            GamesList = new ObservableCollection<csGame>();
            GamesList = gameDB.ListGames(startD,endD,IdSport,league, player);

            if(GamesList != null)
            {

                try
                {
                  BetsWithLeans = new ObservableCollection<csBet>();
                  NoLeansBets = new ObservableCollection<csBet>();
                  LeansBets = new ObservableCollection<csBet>();
                  ByGameList = new ObservableCollection<csBet>();

                  LeansExcel.GenerateExcel(GamesList, inPlayer.Items[inPlayer.SelectedIndex].Text, BetsWithLeans, NoLeansBets, LeansBets, ByGameList);
                    
                    //txtGames.InnerHtml = "Games From " + player;
                    txtWith.InnerHtml = "Bets With " + player;
                    txtAgainst.InnerHtml = "Bets Against " + player;
                    txtLeans.InnerHtml = player + " Bets";

                    rptWith.DataSource = BetsWithLeans;
                    rptWith.DataBind();

                    rptAgainst.DataSource = NoLeansBets;
                    rptAgainst.DataBind();

                    rpfLeans.DataSource = LeansBets;
                    rpfLeans.DataBind();

                    rptGame.DataSource = ByGameList;
                    rptGame.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.alert('" + ex.Message + "');</script>");
                }
            }
        }



        private void LoadSport(object sender, EventArgs e)
        {
            inSport.DataSource = sportDB.ListSportFromDGS();
            inSport.DataTextField = "SportName";
            inSport.DataValueField = "IdSport";
            inSport.DataBind();
        }


        public string GetPick()
        {
            return inPlayer.Items[inPlayer.SelectedIndex].Text + " Pick";
        }


        public string GetPickType()
        {
            return inPlayer.Items[inPlayer.SelectedIndex].Text + " Type";
        }

        public string GetPickLine()
        {
            return inPlayer.Items[inPlayer.SelectedIndex].Text + " Line";
        }


        protected void GenerateExcelReport(object sender, EventArgs e)
        {

        }
    }
}