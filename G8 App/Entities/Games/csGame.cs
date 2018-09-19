using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.Games
{
    public class csGame
    {
        public int IdGame { set; get; }
        public int EventId { set; get; }
        public string HomeTeam { set; get; }
        public string VisitorTeam { set; get; }
        public string HomeTeamAux { set; get; }
        public string VisitorTeamAux { set; get; }
        public string IdSportDonBest { set; get; }
        public string SportDonBest { set; get; }
        public string IdSportDGS { set; get; }
        public string SportDGS1 { set; get; }
        public string SportDGS2 { set; get; }
        public int IdLeague { set; get; }
        public DateTime GameDate { set; get; }
        public int Period { set; get; }
        public int VisitorNumber { set; get; }
        public int HomeNumber { set; get; }
        public int VisitorNumberAux { set; get; }
        public int HomeNumberAux { set; get; }
        public int FamilyNumber { set; get; }
        public int ParentNumber { set; get; }
        public int? HomeScoreDGS { set; get; }
        public int? VisitorScoreDGS { set; get; }
        public string LeagueName { set; get; }
        public string GamePeriod { set; get; }
        public string Status { set; get; }
        public string EventName { set; get; }
        public string EventNameWithId { set; get; }
        public string ScoreString { set; get; }
        public string Score { set; get; }

        //openig
        public string OpMoneyLineVis { set; get; }
        public string OpMoneyLineHom { set; get; }
        public string OpSpreadHom { set; get; }
        public string OpSpreadVis { set; get; }      
        public string OpTotalOver { set; get; }
        public string OpTotalUnder { set; get; }
        public string OpDraw { set; get; }


        //closing
        public string ClMoneyLineVis { set; get; }
        public string ClMoneyLineHom { set; get; }
        public string ClSpreadHom { set; get; }
        public string ClSpreadVis { set; get; }
        public string ClTotalOver { set; get; }
        public string ClTotalUnder { set; get; }
        public string ClDraw { set; get; }
        public int isLive { set; get; }

        public Double BETS { set; get; }
        public Double RISK { set; get; }
        public Double NET { set; get; }


        //DonBest
        public int? HomeScoreDonBest { set; get; }
        public int? VisitorScoreDonBest { set; get; }


        public csGame()
        {
            this.VisitorScoreDGS = null;
            this.HomeScoreDGS = null;
            this.VisitorScoreDonBest = null;
            this.HomeScoreDonBest = null;
            this.OpTotalOver = null;
            this.OpTotalUnder = null;
            this.ClTotalOver = null;
            this.ClTotalUnder = null;
            this.isLive = 0;
            this.EventId = 0;
            this.ScoreString = "";
            this.Score = "";
            this.RISK = 0;
            this.BETS = 0;         
        }
    }
}