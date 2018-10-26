using G8_App.Entities;
using G8_App.Entities.Games;
using G8_App.Logic.Business_Intelligence;
using G8_App.Logic.Games;
using G8_App.Logic.Lines;
using G8_App.Logic.Profiling;
using HouseReport_BL.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G8_App.Views
{
    public partial class BusinessIntelligence : System.Web.UI.Page
    {
        private blBet betDB = new blBet();
        private blSport sportDB = new blSport();
        private blAgent agentDB = new blAgent();
        private blPlayer playerDB = new blPlayer();
        private blLeague leagueDB = new blLeague();
        private static blLine lineDB = new blLine();
        private static blGame gameDB = new blGame();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                if (!IsPostBack)
                {
                    userName.InnerText = csUser.Name + " - " + csUser.Profile;
                    LoadSport();
                    LoadAgents();
                }
            }
            else Response.Redirect("Login.aspx");
        }


        private void LoadSport()
        {
            inSport.DataSource = sportDB.ListSportFromDGS();
            inSport.DataTextField = "SportName";
            inSport.DataValueField = "IdSport";
            inSport.DataBind();
        }


        private void LoadAgents()
        {
            inAgent.DataSource = agentDB.AgentList();
            inAgent.DataTextField = "Name";
            inAgent.DataValueField = "Value";
            inAgent.DataBind();
        }

        protected void LoadPlayers(object sender, EventArgs e)
        {
            try
            {
                inPlayer.DataSource = playerDB.GetPlayersByAgent(Convert.ToInt32(inAgent.Items[inAgent.SelectedIndex].Value.Trim()));
                inPlayer.DataTextField = "Player";
                inPlayer.DataValueField = "IdPlayer";
                inPlayer.DataBind();
            }
            catch (Exception)
            {
            }
        }

        protected void FillLeague(object sender, EventArgs e)
        {
            inLeague.DataSource = leagueDB.LeaguesBySport(inSport.Items[inSport.SelectedIndex].Value.Trim());
            inLeague.DataTextField = "LeagueDescription";
            inLeague.DataValueField = "IdLeague";
            inLeague.DataBind();
        }

        protected void LoadBets(object sender, EventArgs e)
        {
            string startD = Request[startDate.UniqueID];
            string endD = Request[endDate.UniqueID];

            var spli1 = startD.Split('/');
            var spli2 = endD.Split('/');

            startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
            endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];

            endD = GetPlusDate(endD);

            string sport = (inSport.Items[inSport.SelectedIndex].Value == "ALL") ? "" : inSport.Items[inSport.SelectedIndex].Value;
            int agent = Convert.ToInt32(inAgent.Items[inAgent.SelectedIndex].Value);

            string wagerPlay = inWagerPlay.Items[inWagerPlay.SelectedIndex].Value;
            string betType = inBetType.Items[inBetType.SelectedIndex].Value;
            int idLeague = (inLeague.Items[inLeague.SelectedIndex].Text == "ALL") ? -1 : Convert.ToInt32(inLeague.Items[inLeague.SelectedIndex].Value);
            int player = (inPlayer.Items[inPlayer.SelectedIndex].Text == "ALL") ? -1 : Convert.ToInt32(inPlayer.Items[inPlayer.SelectedIndex].Value);

            ObservableCollection<G8_App.Entities.Business_Intelligence.csBet> BetList = betDB.BetList(startD, endD, sport, agent, wagerPlay, betType, idLeague, player);
            ObservableCollection<G8_App.Entities.Business_Intelligence.csBet> BetListAux = new ObservableCollection<Entities.Business_Intelligence.csBet>();
            List<int> Enteros = new List<int>();

            if (BetList != null)
            {
                foreach (var i in BetList)
                {
                    if(!i.CompleteDescription.ToUpper().Contains("STRAIGHT"))
                    {
                        if(!Enteros.Contains(i.IdWager))
                        {
                            Enteros.Add(i.IdWager);
                            foreach (var option in BetList)
                            {
                                if (!option.CompleteDescription.ToUpper().Contains("STRAIGHT"))
                                {
                                    if (i.IdWager == option.IdWager && i.IdWagerDetail != option.IdWagerDetail)
                                    {
                                        i.DetailDescription += " " + option.DetailDescription;                                       
                                    }
                                }
                            }
                            BetListAux.Add(i);
                        }
                    }else
                    {
                        BetListAux.Add(i);
                    }
                }
            }

            rptTable.DataSource = BetListAux;
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
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object GetLines(int idgame, string user, string type, string by, string period, string side, int wager,string userplay)
        {
            var obj = "";
            string uplay = userplay.Split(':')[1].Trim();
            int EventId = gameDB.FindEventLight(gameDB.GetGame(idgame)).EventId;

            if (EventId != 0)
            {
                if (type == "ML")
                {
                    obj = Json.Encode(lineDB.GetMoneyLines(EventId, period, side,user,wager, uplay, idgame,-1));
                }
                else if (type == "DR")
                {
                    obj = Json.Encode(lineDB.GetDraws(EventId, period, user, wager, uplay, idgame,-1));
                }
                else if (type == "TOT")
                {
                    obj = Json.Encode(lineDB.GetTotals(EventId, period,side, user, wager, uplay, idgame,-1)); ;
                }
                else if (type == "SP")
                {
                    obj = Json.Encode(lineDB.GetSP(EventId, period, side, user, wager, uplay, idgame,-1));
                }
            }
            return obj;
        }

    }
}