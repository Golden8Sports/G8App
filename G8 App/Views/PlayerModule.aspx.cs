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

            //****************************** THIS WEEK *********************************
            var obj = new ObservableCollection<Object>();
            sport = (sport == "ALL") ? "" : sport;

            DateTime dt1 = DateTime.Now;
            dt1 = dt1.AddDays(TW());

            DateTime dt2 = DateTime.Now;
            dt2 = dt2.AddDays(1);

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


            //************************************ LAST WEEK *********************************

            dt1 = DateTime.Now;
            dt1 = dt1.AddDays(TW() - 7);

            dt2 = DateTime.Now;
            dt2 = dt2.AddDays(TW());

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
            dt1 = dt1.AddMonths(-1);
            dt1 = dt1.AddDays(TM());

            dt2 = DateTime.Now;
            dt2 = dt2.AddDays(TM());

            date1 = dt1.Year + "-" + dt1.Month + "-01";
            date2 = dt2.Year + "-" + dt2.Month + "-01";

            p = summaryDB.InfoBySeasons(date1, date2, sport, player, wagertype);
            if (p != null)
            {
                p.DateRange = "Last Month";
                obj.Add(p);
            }




            // *************************  CURRENT SEASON *******************************
            var current = seasonDB.GetSeasonBySport(sport);
            p = summaryDB.InfoBySeasons(current.startDateCurrentSeason, current.endDateCurrentSeason, sport, player, wagertype);
            if (p != null)
            {
                p.DateRange = "Current Season";
                obj.Add(p);
            }



            // *************************  LAST SEASON *******************************
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
        public static string SumDateRange(string range, string player, string wagertype, string sport)
        {

            DateTime dt1 = DateTime.Now;
            DateTime dt2 = DateTime.Now;
            dt2 = dt2.AddDays(1);
            int caso = 1;

            if (range == "MMMM YYYY")//month
            {
                dt1 = dt1.AddMonths(-24);
                caso = 3;
            }
            else if (range == "DD MMMM YYYY") //day
            {
                dt1 = dt1.AddDays(-90);
                caso = 1;
            }
            else if (range == "DD MMMM") //week
            {
                dt1 = dt1.AddMonths(-6);
                caso = 2;
            }

            var date1 = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;
            var date2 = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;

            var d = summaryDB.SumRangeDate(date1, date2, sport, player, wagertype, caso);
            return Json.Encode(d);
        }

        // ***************************************  BETS SECTION ******************************

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string BetInfo(string player, string wagertype, string sport)
        {
            //****************************** THIS WEEK *********************************
            var obj = new ObservableCollection<Object>();
            sport = (sport == "ALL") ? "" : sport;

            DateTime dt1 = DateTime.Now;
            dt1 = dt1.AddDays(TW());

            DateTime dt2 = DateTime.Now;
            dt2 = dt2.AddDays(1);

            string date1 = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;
            string date2 = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;

            var p = summaryDB.InfoByBets(date1, date2, sport, player, wagertype);
            if (p != null)
            {
                p.DateRange = "Current Week";
                obj.Add(p);
            }

            // *************************  CURRENT SEASON *******************************
            var current = seasonDB.GetSeasonBySport(sport);
            p = summaryDB.InfoByBets(current.startDateCurrentSeason, current.endDateCurrentSeason, sport, player, wagertype);
            if (p != null)
            {
                p.DateRange = "Current Season";
                obj.Add(p);
            }


            //***************************  OVER ALL ************************************

            dt1 = DateTime.Now;
            dt1 = dt1.AddYears(-10);

            dt2 = DateTime.Now;
            dt2 = dt2.AddDays(1);

            date1 = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;
            date2 = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;

            p = summaryDB.InfoByBets(date1, date2, sport, player, wagertype);
            if (p != null)
            {
                p.DateRange = "Overall";
                obj.Add(p);
            }

            return Json.Encode(obj);
        }



        private static double TW()
        {
            DateTime dt = DateTime.Now;

            if (((int)dt.DayOfWeek) == 0) return -6;
            else if (((int)dt.DayOfWeek) == 1) return 0;

            return (((int)dt.DayOfWeek) - 1) * -1;
        }



        private static double TM()
        {
            DateTime dt = DateTime.Now;
            return -((int)dt.Day) + 1;
        }




        // ********************************** PARLAY / TEASER *****************************

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ParlayTeaser(string player, string range, string sport)
        {
            var obj = new ObservableCollection<Entities.Profiling.csSummary>();
            sport = (sport == "ALL") ? "" : sport;

            DateTime dt1 = DateTime.Now;
            DateTime dt2 = DateTime.Now;
            string date1 = "", date2 = "";
            //dt2 = dt2.AddDays(1);

            if (range == "T") //today
            {
                dt2 = dt2.AddDays(1);
                date1 = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;
                date2 = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;
            }
            else if (range == "Y") //yesterday
            {
                dt1 = dt1.AddDays(-1);
                date1 = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;
                date2 = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;
            }
            else if (range == "TW") //this week
            {
                dt1 = dt1.AddDays(TW());
                dt2 = dt2.AddDays(1);
                date1 = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;
                date2 = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;
            }
            else if (range == "LW") //last week
            {
                dt1 = dt1.AddDays(TW() - 7);
                dt2 = dt2.AddDays(TW());
                date1 = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;
                date2 = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;
            }
            else if (range == "TS") //this season
            {
                var current = seasonDB.GetSeasonBySport(sport);
                date1 = current.startDateCurrentSeason;
                date2 = current.endDateCurrentSeason;
            }
            else if (range == "LS") //last season
            {
                var current = seasonDB.GetSeasonBySport(sport);
                date1 = current.startDateLastSeason;
                date2 = current.endDateLastSeason;
            }

            var p = summaryDB.InfoByBets(date1, date2, sport, player, "TEASER");
            p.WagerType = "Teaser";
            obj.Add(p);

            p = summaryDB.InfoByBets(date1, date2, sport, player, "PARLAY");
            p.WagerType = "Parlay";
            obj.Add(p);

            if (obj != null && obj.Count == 2)
            {
                var aux = new Entities.Profiling.csSummary(player,
                                                            "",
                                                            obj[0].RiskAmount + obj[1].RiskAmount,
                                                            obj[0].Net + obj[1].Net,
                                                            obj[0].Bets + obj[1].Bets,
                                                            obj[0].Wins + obj[1].Wins);
                aux.WagerType = "Overall";
                obj.Add(aux);


            }

            return Json.Encode(obj);
        }






        // ********************************** ********* *****************************
        // ********************************** BET STATS *****************************
        // ********************************** ********* *****************************



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string BetStats(string player, string wagerType, string sport)
        {
            var obj = new ObservableCollection<Entities.Profiling.csSummary>();
            sport = (sport == "ALL") ? "" : sport;

            DateTime dt1 = DateTime.Now;
            DateTime dt2 = DateTime.Now;
            string date1 = "", date2 = "";





            //***************************** CURRRENT SEASON ********************************


            var current = seasonDB.GetSeasonBySport(sport);
            date1 = current.startDateLastSeason;
            date2 = current.endDateLastSeason;

            var p = summaryDB.InfoByBets(date1, date2, sport, player, wagerType, "Spread", "Current Season");
            obj.Add(p);

            p = summaryDB.InfoByBets(date1, date2, sport, player, wagerType, "Money", "Current Season");
            obj.Add(p);

            p = summaryDB.InfoByBets(date1, date2, sport, player, wagerType, "Total", "Current Season");
            obj.Add(p);


            //***************************** CURRRENT WEEK ********************************

            dt1 = dt1.AddDays(TW());
            dt2 = dt2.AddDays(1);
            date1 = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;
            date2 = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;
            p = summaryDB.InfoByBets(date1, date2, sport, player, wagerType, "Spread", "Current Week");
            obj.Add(p);

            p = summaryDB.InfoByBets(date1, date2, sport, player, wagerType, "Money", "Current Week");
            obj.Add(p);

            p = summaryDB.InfoByBets(date1, date2, sport, player, wagerType, "Total", "Current Week");
            obj.Add(p);





            // *********************************** FINAL PROCESS ************************
            if (obj != null && obj.Count == 6)
            {
                var aux = new Entities.Profiling.csSummary(player,
                                                            "",
                                                            obj[0].RiskAmount + obj[3].RiskAmount,
                                                            obj[0].Net + obj[3].Net,
                                                            obj[0].Bets + obj[3].Bets,
                                                            obj[0].Wins + obj[3].Wins,
                                                            "Overall",
                                                            "Spread");
                obj.Add(aux);


                aux = new Entities.Profiling.csSummary(player,
                                            "",
                                            obj[1].RiskAmount + obj[4].RiskAmount,
                                            obj[1].Net + obj[4].Net,
                                            obj[1].Bets + obj[4].Bets,
                                            obj[1].Wins + obj[4].Wins,
                                            "Overall",
                                            "Money");
                obj.Add(aux);



                aux = new Entities.Profiling.csSummary(player,
                                            "",
                                            obj[2].RiskAmount + obj[5].RiskAmount,
                                            obj[2].Net + obj[5].Net,
                                            obj[2].Bets + obj[5].Bets,
                                            obj[2].Wins + obj[5].Wins,
                                            "Overall",
                                            "Total");
                obj.Add(aux);



            }

            return Json.Encode(obj);
        }





        // ***************************************  PRO PLAY SECTION ******************************

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ProPlay(string range, string player, string sport)
        {
            var obj = new ObservableCollection<Entities.Profiling.csSummary>();
            sport = (sport == "ALL") ? "" : sport;

            DateTime dt1 = DateTime.Now;
            DateTime dt2 = DateTime.Now;
            string date1 = "", date2 = "";
            //dt2 = dt2.AddDays(1);

            if (range == "T") //today
            {
                dt2 = dt2.AddDays(1);
                date1 = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;
                date2 = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;
            }
            else if (range == "Y") //yesterday
            {
                dt1 = dt1.AddDays(-1);
                date1 = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;
                date2 = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;
            }
            else if (range == "TW") //this week
            {
                dt1 = dt1.AddDays(TW());
                dt2 = dt2.AddDays(1);
                date1 = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;
                date2 = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;
            }
            else if (range == "LW") //last week
            {
                dt1 = dt1.AddDays(TW() - 7);
                dt2 = dt2.AddDays(TW());
                date1 = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;
                date2 = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;
            }
            else if (range == "TS") //this season
            {
                var current = seasonDB.GetSeasonBySport(sport);
                date1 = current.startDateCurrentSeason;
                date2 = current.endDateCurrentSeason;
            }
            else if (range == "LS") //last season
            {
                var current = seasonDB.GetSeasonBySport(sport);
                date1 = current.startDateLastSeason;
                date2 = current.endDateLastSeason;
            }

            obj = summaryDB.SummaryProfile(date1, date2, sport, "-1", -1, player, true);
            return Json.Encode(obj);
        }




        //*************************** today's action ************************************

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string TodayChart(string player, string sport)
        {
            var obj = new ObservableCollection<Entities.Profiling.csSummary>();
            sport = (sport == "ALL") ? "" : sport;

            var dt = DateTime.Now;
            var dt2 = DateTime.Now;
            dt2 = dt2.AddDays(1);

            var date1 = dt.Year + "-" + dt.Month + "-" + dt.Day;
            var date2 = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;

            var p = summaryDB.InfoByBets(date1, date2, sport, player, "STRAIGHT");
            p.WagerType = "Straight Bet";
            obj.Add(p);

            p = summaryDB.InfoByBets(date1, date2, sport, player, "PARLAY");
            p.WagerType = "Parlay";
            obj.Add(p);

            p = summaryDB.InfoByBets(date1, date2, sport, player, "TEASER");
            p.WagerType = "Teaser";
            obj.Add(p);

            return Json.Encode(obj);
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string TodayTable(string player, string sport)
        {
            sport = (sport == "ALL") ? "" : sport;

            var dt = DateTime.Now;
            var dt2 = DateTime.Now;
            dt2 = dt2.AddDays(1);

            var date1 = dt.Year + "-" + dt.Month + "-" + dt.Day;
            var date2 = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;

            var d = summaryDB.SummaryProfile(date1, date2, sport, "-1", -1, player, true);

            return Json.Encode(d);
        }




        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string TodayPlay(string player, string sport, string wagerType)
        {
            var obj = new ObservableCollection<Entities.Profiling.csSummary>();
            sport = (sport == "ALL") ? "" : sport;

            var dt = DateTime.Now;
            var dt2 = DateTime.Now;
            dt2 = dt2.AddDays(1);

            var date1 = dt.Year + "-" + dt.Month + "-" + dt.Day;
            var date2 = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;

            var d = summaryDB.InfoByBets(date1, date2, sport, player, wagerType,"SPREAD");
            d.WagerPlay = "SPREAD";
            obj.Add(d);

            d = summaryDB.InfoByBets(date1, date2, sport, player, wagerType, "MONEY");
            d.WagerPlay = "MONEY";
            obj.Add(d);

            d = summaryDB.InfoByBets(date1, date2, sport, player, wagerType, "TOTAL");
            d.WagerPlay = "TOTAL";
            obj.Add(d);

            return Json.Encode(obj);
        }



    }
} 