using G8_App.Entities;
using G8_App.Entities.Games;
using G8_App.Entities.Lines;
using G8_App.Logic.Games;
using G8_App.Logic.Lines;
using HouseReport_BL.Logic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G8_App.Views
{
    public partial class GraphByGame : System.Web.UI.Page
    {


        private blSport sportDB = new blSport();
        private blLeague leagueDB = new blLeague();
        private static blGame gameDB = new blGame();
        private static blLine lineDB = new blLine();
        private static ObservableCollection<csGame> GamesList = null;
        private static List<int> IntList = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                userName.InnerText = csUser.Name + " - " + csUser.Profile;
                if (!IsPostBack)
                {
                    LoadSport(sender, e);
                    AddInt();
                }
            }
            else Response.Redirect("Login.aspx");
        }


        private void AddInt()
        {
            IntList = new List<int>();
            IntList.Add(37); //Pinnacle
            IntList.Add(849); //Cris
            IntList.Add(52); //Jazz
            IntList.Add(364);//PPH
            IntList.Add(92);//5Dimes
            IntList.Add(39);//Grande
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

            var spli1 = startD.Split('/');
            var spli2 = endD.Split('/');
            if (IdSport == null || IdSport == "ALL") IdSport = "";
            try { league = (inLeague.Items[inLeague.SelectedIndex].Text == "ALL") ? -1 : Convert.ToInt32(inLeague.Items[inLeague.SelectedIndex].Value); } catch (Exception) { league = -1; }

            startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
            endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
            endD = GetPlusDate(endD);

            GamesList = new ObservableCollection<csGame>();
            GamesList = gameDB.ListGamesFromGraph(startD, endD, IdSport, league);

            rptTable.DataSource = GamesList;
            rptTable.DataBind();
        }

        private string GetPlusDate(string endD)
        {
            DateTime dt2 = Convert.ToDateTime(endD);
            dt2 = dt2.AddDays(1);
            endD = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;
            return endD;
        }



        //web request
        [System.Web.Services.WebMethod()]
        //[WebMethod]
        //[ScriptMethod(UseHttpGet = true)]
        public static Object GetLines(int idgame, string type, string by, string period, string side, int idPeriod, int idEvent)
        {
            Object obj = "";
            try
            {
                if (idEvent != 0)
                {
                    if (type == "ML") obj = Json.Encode(lineDB.GetMoneyLines(idEvent, period, side, "", 0, "", idgame, idPeriod));
                    else if (type == "DR") obj = Json.Encode(lineDB.GetDraws(idEvent, period, "", 0, "", idgame, idPeriod));
                    else if (type == "TOT") obj = Json.Encode(lineDB.GetTotals(idEvent, period, side, "", 0, "", idgame, idPeriod));
                    else if (type == "SP") obj = Json.Encode(lineDB.GetSP(idEvent, period, side, "", 0, "", idgame, idPeriod));
                }
            }
            catch (Exception ex)
            {
                int df = 2;
            }

            return obj;
        }


        [System.Web.Services.WebMethod]
        public static Object GetStats(int idgame)
        {
            return JsonConvert.SerializeObject(gameDB.GameStats(idgame));
        }

    }
}