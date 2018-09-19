using G8_App.Entities;
using G8_App.Entities.Profiling;
using G8_App.Logic.Profiling;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G8_App.Views
{
    public partial class ProfilingPlayer : System.Web.UI.Page
    {

        private blPlayer playerDB = new blPlayer();
        private blProfile profileDB = new blProfile();
        private static ObservableCollection<csProfile> data = new ObservableCollection<csProfile>();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                if (!IsPostBack)
                {
                    userName.InnerText = csUser.Name + " - " + csUser.Profile;

                    try
                    {
                        inPlayer.DataSource = playerDB.GetPlayers();
                        inPlayer.DataTextField = "Player";
                        inPlayer.DataValueField = "IdPlayer";
                        inPlayer.DataBind();
                    }
                    catch (Exception){}
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }


        private void SetData()
        {
            try
            {
                string IdPlayer = (inPlayer.Items[inPlayer.SelectedIndex].Value);
                string startD = Request[startDate.UniqueID];
                string endD = Request[endDate.UniqueID];

                if (!string.IsNullOrWhiteSpace(IdPlayer) && !string.IsNullOrWhiteSpace(startD) && !string.IsNullOrWhiteSpace(endD))
                {
                    var spli1 = startD.Split('/');
                    var spli2 = endD.Split('/');
                    startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
                    endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];
                    data = profileDB.GetProfile(startD, endD, IdPlayer);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        protected string GetData()
        {
            string value = "";

            try
            {
                if (data == null || data.Count == 0) SetData();
                
                if (data != null && data.Count > 0)
                {
                        value += "[['Wager Play','Value'],";
                    foreach (var i in data)
                    {
                        value += "['Spread'," + (i.CountSP.ToString()) + "],";
                        value += "['Money Line'," + (i.CountML.ToString()) + "],";
                        value += "['Total Over'," + (i.CountOVER.ToString()) + "],";
                        value += "['Total Under'," + (i.CountUNDER.ToString()) + "],";
                        value += "['Draw'," + (i.CountDR.ToString()) + "]]";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return value;
        }




        protected string HomeVisitor()
        {
            string value = "";
            try
            {
                if (data == null || data.Count == 0) SetData();

                if (data != null && data.Count > 0)
                {
                    value += "[['Home/Visitor','Value'],";
                    foreach (var i in data)
                    {
                        value += "['Home'," + (i.CountHome.ToString()) + "],";
                        value += "['Visitor'," + (i.CountVisitor.ToString()) + "]]";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return value;
        }




        protected string MomentDay()
        {
            string value = "";
            try
            {
                if (data == null || data.Count == 0) SetData();

                if (data != null && data.Count > 0)
                {
                    value += "[['Moment Day','Value'],";
                    foreach (var i in data)
                    {
                        value += "['Morning'," + (i.CountMorning.ToString()) + "],";
                        value += "['Noon'," + (i.CountNoon.ToString()) + "],";
                        value += "['Night'," + (i.CountNight.ToString()) + "],";
                        value += "['Over Night'," + (i.CountOverNight.ToString()) + "]]";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return value;
        }



        protected string Result()
        {
            string value = "";
            try
            {
                if (data == null || data.Count == 0) SetData();

                if (data != null && data.Count > 0)
                {
                    value += "[['Result','Value'],";
                    foreach (var i in data)
                    {
                        value += "['Wins'," + (i.CountWins.ToString()) + "],";
                        value += "['Loses'," + (i.CountLoses.ToString()) + "],";
                        value += "['Draw'," + (i.CountDraws.ToString()) + "]]";
                    }
                }
            }
            catch (Exception)
            {
            }
            return value;
        }



        protected string FavDog()
        {
            string value = "";
            try
            {
                if (data == null || data.Count == 0) SetData();
                if (data != null && data.Count > 0)
                {
                    value += "[['Fav/Dog','Value'],";
                    foreach (var i in data)
                    {
                        value += "['Fav'," + (i.CountFav.ToString()) + "],";
                        value += "['Dog'," + (i.CountDog.ToString()) + "]]";
                    }
                }
            }
            catch (Exception)
            {
            }
            return value;
        }




        protected string LineMoved()
        {
            string value = "";
            try
            {
                if (data == null || data.Count == 0) SetData();
                if (data != null && data.Count > 0)
                {
                    value += "[['Line Moved','Value'],";
                    foreach (var i in data)
                    {
                        value += "['Line Moved'," + (i.CountLineMoved.ToString()) + "],";
                        value += "['Line Not Moved'," + (i.CountNoLineMoved.ToString()) + "]]";
                    }
                }
            }
            catch (Exception)
            {
            }
            return value;
        }




        protected string Acount()
        {
            string value = "";
            try
            {
                if (data == null || data.Count == 0) SetData();
                if (data != null && data.Count > 0)
                {
                    value += "[['Values','Value'],";
                    foreach (var i in data)
                    {
                        value += "['Net'," + (i.Net.ToString()) + "],";
                        value += "['Risk Amount'," + (i.RiskAmount.ToString()) + "],";
                        value += "['Win Amount'," + (i.WinAmount.ToString()) + "]]";
                    }
                }
            }
            catch (Exception)
            {

            }
            return value;
        }





        protected string Live()
        {
            string value = "";
            try
            {
                if (data == null || data.Count == 0) SetData();
                if (data != null && data.Count > 0)
                {
                    value += "[['Live','Value'],";
                    foreach (var i in data)
                    {
                        value += "['Live'," + (i.CountLive.ToString()) + "],";
                        value += "['No Live'," + (i.CountNoLive.ToString()) + "]]";
                    }
                }
            }
            catch (Exception)
            {

            }
            return value;
        }




        protected string Buy()
        {
            string value = "";
            try
            {
                if (data == null || data.Count == 0) SetData();
                if (data != null && data.Count > 0)
                {
                    value += "[['Buy','Value'],";
                    foreach (var i in data)
                    {
                        value += "['Buy'," + (i.CountBuy.ToString()) + "],";
                        value += "['No Buy'," + (i.CountNoBuy.ToString()) + "]]";
                    }
                }
            }
            catch (Exception)
            {

            }
            return value;
        }



    }
}