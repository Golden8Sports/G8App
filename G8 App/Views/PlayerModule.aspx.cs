using G8_App.Entities;
using G8_App.Entities.Profiling;
using G8_App.Logic.Profiling;
using HouseReport_BL.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using NHL_BL.Entities;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Helpers;
using G8_App.Logic.Dashboard;

namespace G8_App.Views
{
    public partial class PlayerModule : System.Web.UI.Page
    {
        private static blPlayer playerDB = new blPlayer();
        private blSport sportDB = new blSport();
        private static blProfile profileDB = new blProfile();
        private static blSummary summaryDB = new blSummary();
        private static blSeason seasonDB = new blSeason();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                userName.InnerText = csUser.Name + " - " + csUser.Profile;

                if (!IsPostBack)
                {
                    LoadPlayers(sender, e);
                    LoadSport();
                }
            }
            else Response.Redirect("Login.aspx");
        }


        private void LoadSport()
        {
            ObservableCollection<csSport> l = sportDB.ListSportFromDGS();
            inSportFinancial.DataSource = l;
            inSportFinancial.DataTextField = "SportName";
            inSportFinancial.DataValueField = "IdSport";
            inSportFinancial.DataBind();

            inSportBets.DataSource = l;
            inSportBets.DataTextField = "SportName";
            inSportBets.DataValueField = "IdSport";
            inSportBets.DataBind();

            inSportLeans.DataSource = l;
            inSportLeans.DataTextField = "SportName";
            inSportLeans.DataValueField = "IdSport";
            inSportLeans.DataBind();

            inSPortToday.DataSource = l;
            inSPortToday.DataTextField = "SportName";
            inSPortToday.DataValueField = "IdSport";
            inSPortToday.DataBind();
        }


        protected void LoadPlayers(object sender, EventArgs e)
        {
            try
            {
                inPlayer.DataSource = playerDB.GetPlayers();
                inPlayer.DataTextField = "Player";
                inPlayer.DataValueField = "Player";
                inPlayer.DataBind();
            }
            catch (Exception)
            {
            }
        }



        protected void LoadData(object sender, EventArgs e)
        {
            csPlayer p = playerDB.GetPlayerInfo(inPlayer.Items[inPlayer.SelectedIndex].Value);
            if (p != null)
            {
                inEmail.InnerText = p.Email;
                inAgent.InnerText = p.Agent;
                idCategory.InnerText = "...";
                inphone.InnerText = p.Phone;
            }

        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LastWeeks(string player, string wagertype, string sport)
        {

            //****************************** this week *********************************
            var obj = new ObservableCollection<Object>();
            sport = (sport == "ALL") ? "" : sport;

            DateTime dt1 = DateTime.Now;
            int s = 6 - Convert.ToInt32(dt1.DayOfWeek);
            dt1 = dt1.AddDays(-s);

            DateTime dt2 = DateTime.Now;

            string date1 = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;
            string date2 = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;


            csPlayer pla = playerDB.GetPlayerInfo(player);


            var p = summaryDB.InfoBySeasons(date1, date2, sport, player, wagertype);
            if (p != null)
            {
                p.Agent = pla.Agent;
                p.Phone = pla.Phone;
                p.Email = pla.Email;
                p.DateRange = "Current Week";
                obj.Add(p);
            }


            //******************************************** Last Week

            dt1 = DateTime.Now;
            s = (6 - Convert.ToInt32(dt1.DayOfWeek)) + 7;
            dt1 = dt1.AddDays(-s);

            dt2 = DateTime.Now;

            s = Convert.ToInt32(dt2.DayOfWeek);
            dt2 = dt2.AddDays(-s);

            date1 = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;
            date2 = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;

            p = summaryDB.InfoBySeasons(date1, date2, sport, player, wagertype);
            if (p != null)
            {
                p.DateRange = "Last Week";
                obj.Add(p);
            }


            // ******************************** LAST MONTH *****************************

            dt1 = DateTime.Now;
            dt2 = DateTime.Now;

            dt1 = dt1.AddMonths(-1);

            date1 = dt1.Year + "-" + dt1.Month + "-01";
            date2 = dt2.Year + "-" + dt2.Month + "-01";

            p = summaryDB.InfoBySeasons(date1, date2, sport, player, wagertype);
            if (p != null)
            {
                p.DateRange = "Last Month";
                obj.Add(p);
            }




            // *************************  Current season *******************************
            var current = seasonDB.GetSeasonBySport(sport);
            p = summaryDB.InfoBySeasons(current.startDateCurrentSeason, current.endDateCurrentSeason, sport, player, wagertype);
            if (p != null)
            {
                p.DateRange = "Current Season";
                obj.Add(p);
            }


            p = summaryDB.InfoBySeasons(current.startDateLastSeason, current.endDateLastSeason, sport, player, wagertype);
            if (p != null)
            {
                p.DateRange = "Last Season";
                obj.Add(p);
            }


            return Json.Encode(obj);
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string SumDateRange(string range,string player, string wagertype, string sport)
        {

            DateTime dt1 = DateTime.Now;
            DateTime dt2 = DateTime.Now;
            int caso = 1;

            if (range == "MMMM YYYY")//month
            {
                dt1 = dt1.AddMonths(-12);
                caso = 3;
            }
            else if(range == "DD MMMM YYYY") //day
            {
                dt1 = dt1.AddDays(-30);
                caso = 1;
            }
            else if (range == "DD MMMM") //week
            {
                dt1 = dt1.AddMonths(-5);
                caso = 2;
            }

            var date1 = dt1.Year + "-" + dt1.Month + "-01";
            var date2 = dt2.Year + "-" + dt2.Month + "-01";


            var d = summaryDB.SumRangeDate(date1,date2,sport,player,wagertype,caso);
            return Json.Encode(d);
        }


    }
} 