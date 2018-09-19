using G8_App.Entities;
using G8_App.Entities.MLB;
using G8_App.Logic.MLB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G8_App.Views
{
    public partial class MLB_Series : System.Web.UI.Page
    {
        private blMlbSeries MlbSeriesDB = new blMlbSeries();
        private blGamesSerie GamesSerieDB = new blGamesSerie();
        private blGroupSerie GroupSerieDB = new blGroupSerie();
        private blTeam TeamDB = new blTeam();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                if (!IsPostBack)
                {
                    userName.InnerText = csUser.Name + " - " + csUser.Profile;

                    try
                    {                       
                        inTeam.DataSource = TeamDB.GetAllSeries();
                        inTeam.DataTextField = "Name";
                        inTeam.DataValueField = "Real";
                        inTeam.DataBind();
                    }
                    catch (Exception){}
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }


        protected string GetFavDog()
        {
            return inRef.Items[inRef.SelectedIndex].Value;
        }


        protected void LoadData(object sender, EventArgs e)
        {
            try
            {
                string HAB = inOrder.Items[inOrder.SelectedIndex].Value;
                string favDog = inRef.Items[inRef.SelectedIndex].Value;
                string Team = inTeam.Items[inTeam.SelectedIndex].Value.ToUpper();

                string startD = Request[startDate.UniqueID];
                string endD = Request[endDate.UniqueID];

                var spli1 = startD.Split('/');
                var spli2 = endD.Split('/');

                startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
                endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];

                ObservableCollection<csMlbSeries> data = MlbSeriesDB.GetAllSeries(Team, favDog, startD, endD);
                ObservableCollection<csMlbStat> List = new ObservableCollection<csMlbStat>();
                ObservableCollection<csAgrupation> Agrupation = new ObservableCollection<csAgrupation>();
                ObservableCollection<csSummary> SummaryList = new ObservableCollection<csSummary>();
                csMlbStat mlbStat = null;


                if (data != null)
                {
                    foreach (var i in data)
                    {
                        i.CompleteSerieList = GroupSerieDB.GetAllGamesBySerie(i);

                        mlbStat = new csMlbStat();
                        mlbStat.SeriesRange = i.Range;
                        mlbStat.Name = i.Name;
                        mlbStat.Date = i.EventDate.Month + "/" + i.EventDate.Day + "/" + i.EventDate.Year;
                        mlbStat.SeriesType = (i.CompleteSerieList != null) ? i.CompleteSerieList.Count.ToString() + " Games" : 0  +  " Games";
                        mlbStat.CountGames = (i.CompleteSerieList != null) ? i.CompleteSerieList.Count : 0;
                        mlbStat.Fav = i.Reference;
                        mlbStat.VTeam = i.VisitorTeam;
                        mlbStat.HTeam = i.HomeTeam;
                        mlbStat.Line = i.Line;

                        if (i.CompleteSerieList != null)
                        {
                            int k = 0;
                            foreach (var g in i.CompleteSerieList)
                            {
                                g.GamesBySerieList = GamesSerieDB.GetGamesBySerie(g, i.Reference);

                                if (g.GamesBySerieList != null && g.GamesBySerieList.Count > 0)
                                {
                                    foreach (var game in g.GamesBySerieList)
                                    {

                                        if (game != null)
                                        {
                                            if (k == 0) //is the first game
                                            {
                                                if (i.Reference == "AWAY")
                                                {
                                                    if (game.VisitorScore > game.HomeScore) mlbStat.Game1 = 1;
                                                    else mlbStat.Game1 = 0;
                                                }
                                                else
                                                {
                                                    if (game.VisitorScore < game.HomeScore) mlbStat.Game1 = 1;
                                                    else mlbStat.Game1 = 0;
                                                }

                                                mlbStat.Game1Line = game.Line;
                                                mlbStat.Spread1 = game.Spread;
                                                mlbStat.SpreadOdds1 = game.SpreadOdds;
                                                mlbStat.Total1 = game.Total;
                                                mlbStat.TotalUnder1 = game.TotalUnder;
                                                mlbStat.TotalOver1 = game.TotalOver;
                                                mlbStat.HomeScore1 = game.HomeScore;
                                                mlbStat.VisitorScore1 = game.VisitorScore;
                                            }
                                            else if (k == 1)//is the second game
                                            {
                                                if (i.Reference == "AWAY")
                                                {
                                                    if (game.VisitorScore > game.HomeScore) mlbStat.Game2 = 1;
                                                    else mlbStat.Game2 = 0;
                                                }
                                                else
                                                {
                                                    if (game.VisitorScore < game.HomeScore) mlbStat.Game2 = 1;
                                                    else mlbStat.Game2 = 0;
                                                }

                                                mlbStat.Game2Line = game.Line;
                                                mlbStat.Spread2 = game.Spread;
                                                mlbStat.SpreadOdds2 = game.SpreadOdds;
                                                mlbStat.Total2 = game.Total;
                                                mlbStat.TotalUnder2 = game.TotalUnder;
                                                mlbStat.TotalOver2 = game.TotalOver;
                                                mlbStat.HomeScore2 = game.HomeScore;
                                                mlbStat.VisitorScore2 = game.VisitorScore;
                                            }
                                            else if (k == 2)//is the third game
                                            {

                                                if (i.Reference == "AWAY")
                                                {
                                                    if (game.VisitorScore > game.HomeScore) mlbStat.Game3 = 1;
                                                    else mlbStat.Game3 = 0;
                                                }
                                                else
                                                {
                                                    if (game.VisitorScore < game.HomeScore) mlbStat.Game3 = 1;
                                                    else mlbStat.Game3 = 0;
                                                }

                                                mlbStat.Game3Line = game.Line;
                                                mlbStat.Spread3 = game.Spread;
                                                mlbStat.SpreadOdds3 = game.SpreadOdds;
                                                mlbStat.Total3 = game.Total;
                                                mlbStat.TotalUnder3 = game.TotalUnder;
                                                mlbStat.TotalOver3 = game.TotalOver;
                                                mlbStat.HomeScore3 = game.HomeScore;
                                                mlbStat.VisitorScore3 = game.VisitorScore;
                                            }
                                            else if (k == 3) //is the fourth game
                                            {
                                                if (i.Reference == "AWAY")
                                                {
                                                    if (game.VisitorScore > game.HomeScore) mlbStat.Game4 = 1;
                                                    else mlbStat.Game4 = 0;
                                                }
                                                else
                                                {
                                                    if (game.VisitorScore < game.HomeScore) mlbStat.Game4 = 1;
                                                    else mlbStat.Game4 = 0;
                                                }

                                                mlbStat.Game4Line = game.Line;
                                                mlbStat.Spread4 = game.Spread;
                                                mlbStat.SpreadOdds4 = game.SpreadOdds;
                                                mlbStat.Total4 = game.Total;
                                                mlbStat.TotalUnder4 = game.TotalUnder;
                                                mlbStat.TotalOver4 = game.TotalOver;
                                                mlbStat.HomeScore4 = game.HomeScore;
                                                mlbStat.VisitorScore4 = game.VisitorScore;
                                            }

                                            k += 1;
                                        }                                       
                                    }
                                }
                                else
                                {
                                    if (k == 0) mlbStat.Game1 = 2;
                                    else if (k == 1) mlbStat.Game2 = 2;
                                    else if (k == 2) mlbStat.Game3 = 2;
                                    else if (k == 3) mlbStat.Game4 = 2;

                                    k += 1;
                                }
                            }
                        }


                        // detect Sweep
                        if (mlbStat.CountGames == i.CountGames && i.CountGames == 3)
                        {
                            if (favDog == "Fav") // favorites
                            {
                                if (mlbStat.Game1 == 1 && mlbStat.Game2 == 1 && mlbStat.Game3 == 1) mlbStat.FavSwip = 1;
                                else if (mlbStat.Game1 == 0 && mlbStat.Game2 == 0 && mlbStat.Game3 == 0) mlbStat.DogSwip = 1;

                                if (mlbStat.Game1 == 1 && mlbStat.Game2 == 1) mlbStat.FavSwipPossible = 1;
                                else if (mlbStat.Game1 == 0 && mlbStat.Game2 == 0) mlbStat.DogSwipPossible = 1;
                            }
                            else // dogs
                            {
                                if (mlbStat.Game1 == 1 && mlbStat.Game2 == 1 && mlbStat.Game3 == 1) mlbStat.DogSwip = 1;
                                else if (mlbStat.Game1 == 0 && mlbStat.Game2 == 0 && mlbStat.Game3 == 0) mlbStat.FavSwip = 1;

                                if (mlbStat.Game1 == 1 && mlbStat.Game2 == 1) mlbStat.DogSwipPossible = 1;
                                else if (mlbStat.Game1 == 0 && mlbStat.Game2 == 0) mlbStat.FavSwipPossible = 1;
                            }
                        }
                        else if (mlbStat.CountGames == i.CountGames && i.CountGames == 4)
                        {
                            if (favDog == "Fav")
                            {
                                if (mlbStat.Game1 == 1 && mlbStat.Game2 == 1 && mlbStat.Game3 == 1 && mlbStat.Game4 == 1) mlbStat.FavSwip = 1;
                                else if (mlbStat.Game1 == 0 && mlbStat.Game2 == 0 && mlbStat.Game3 == 0 && mlbStat.Game4 == 0) mlbStat.DogSwip = 1;

                                if (mlbStat.Game1 == 1 && mlbStat.Game2 == 1 && mlbStat.Game3 == 1) mlbStat.FavSwipPossible = 1;
                                else if (mlbStat.Game1 == 0 && mlbStat.Game2 == 0 && mlbStat.Game3 == 0) mlbStat.DogSwipPossible = 1;
                            }
                            else
                            {
                                if (mlbStat.Game1 == 1 && mlbStat.Game2 == 1 && mlbStat.Game3 == 1 && mlbStat.Game4 == 1) mlbStat.DogSwip = 1;
                                else if (mlbStat.Game1 == 0 && mlbStat.Game2 == 0 && mlbStat.Game3 == 0 && mlbStat.Game4 == 0) mlbStat.FavSwip = 1;

                                if (mlbStat.Game1 == 1 && mlbStat.Game2 == 1 && mlbStat.Game3 == 1) mlbStat.DogSwipPossible = 1;
                                else if (mlbStat.Game1 == 0 && mlbStat.Game2 == 0 && mlbStat.Game3 == 0) mlbStat.FavSwipPossible = 1;
                            }
                        }





                        if (i.Reference == "AWAY" && (HAB == "A" || HAB == "B"))
                        {
                            if (i.VisitorTeam.ToUpper().Contains(inTeam.Items[inTeam.SelectedIndex].Value) ||
                               inTeam.Items[inTeam.SelectedIndex].Value == "")
                            {
                                if (mlbStat.CountGames == i.CountGames)
                                {
                                    List.Add(mlbStat);
                                }
                            }
                        }
                        else if (i.Reference == "HOME" && (HAB == "H" || HAB == "B"))
                        {
                            if (i.HomeTeam.ToUpper().Contains(inTeam.Items[inTeam.SelectedIndex].Value) ||
                                inTeam.Items[inTeam.SelectedIndex].Value == "")
                            {
                                if (mlbStat.CountGames == i.CountGames)
                                {
                                    List.Add(mlbStat);
                                }
                            }
                        }
                    }
                }


                csAgrupation agrupation3_180H = new csAgrupation();
                csAgrupation agrupation3_179H = new csAgrupation();
                csAgrupation agrupation3_130H = new csAgrupation();
                csAgrupation agrupation4_180H = new csAgrupation();
                csAgrupation agrupation4_179H = new csAgrupation();
                csAgrupation agrupation4_130H = new csAgrupation();

                csAgrupation agrupation3_180V = new csAgrupation();
                csAgrupation agrupation3_179V = new csAgrupation();
                csAgrupation agrupation3_130V = new csAgrupation();
                csAgrupation agrupation4_180V = new csAgrupation();
                csAgrupation agrupation4_179V = new csAgrupation();
                csAgrupation agrupation4_130V = new csAgrupation();

                csSummary range3_130 = new csSummary();
                csSummary range3_179 = new csSummary();
                csSummary range3_180 = new csSummary();


                agrupation3_180H.SeriesType = agrupation3_179H.SeriesType = agrupation3_130H.SeriesType = "3 Games";
                agrupation4_180H.SeriesType = agrupation4_179H.SeriesType = agrupation4_130H.SeriesType = "4 Games";
                agrupation3_180V.SeriesType = agrupation3_179V.SeriesType = agrupation3_130V.SeriesType = "3 Games";
                agrupation4_180V.SeriesType = agrupation4_179V.SeriesType = agrupation4_130V.SeriesType = "4 Games";


                if (List != null)
                {
                    foreach (var i in List)
                    {
                        // *********************   HOME  **********************
                        if ((i.SeriesRange == "-100 to -130" || i.SeriesRange == "100 to 130") && i.CountGames == 3 &&
                              ((HAB == "H" || HAB == "B") && i.Fav == "HOME"))
                        {
                            agrupation3_130H = Set(agrupation3_130H, i);
                            range3_130 = setSummary3Games(agrupation3_130H, range3_130, i);
                        }
                        else if ((i.SeriesRange == "-131 to -179" || i.SeriesRange == "131 to 179") && i.CountGames == 3 &&
                                ((HAB == "H" || HAB == "B") && i.Fav == "HOME"))
                        {
                            agrupation3_179H = Set(agrupation3_179H, i);
                            range3_179 = setSummary3Games(agrupation3_179H, range3_179, i);
                        }
                        else if ((i.SeriesRange == "-180 and above" || i.SeriesRange == "180 and above") && i.CountGames == 3 &&
                                ((HAB == "H" || HAB == "B") && i.Fav == "HOME"))
                        {
                            agrupation3_180H = Set(agrupation3_180H, i);
                            range3_180 = setSummary3Games(agrupation3_180H, range3_180, i);
                        }



                        // *********************************  RANGE 4  ************************************
                        else if ((i.SeriesRange == "-100 to -130" || i.SeriesRange == "100 to 130") && i.CountGames == 4 &&
                                ((HAB == "H" || HAB == "B") && i.Fav == "HOME"))
                        {
                            agrupation4_130H = Set(agrupation4_130H, i);
                        }
                        else if ((i.SeriesRange == "-131 to -179" || i.SeriesRange == "131 to 179") && i.CountGames == 4 &&
                                ((HAB == "H" || HAB == "B") && i.Fav == "HOME"))
                        {
                            agrupation4_179H = Set(agrupation4_179H, i);
                        }
                        else if ((i.SeriesRange == "-180 and above" || i.SeriesRange == "180 and above") && i.CountGames == 4 &&
                                ((HAB == "H" || HAB == "B") && i.Fav == "HOME"))
                        {
                            agrupation4_180H = Set(agrupation4_180H, i);
                        }


                        // *****************************    VISITOR    **********************

                        else if ((i.SeriesRange == "-100 to -130" || i.SeriesRange == "100 to 130") && i.CountGames == 3 &&
                               ((HAB == "A" || HAB == "B") && i.Fav == "AWAY"))
                        {
                            agrupation3_130V = Set(agrupation3_130V, i);
                            range3_130 = setSummary3Games(agrupation3_130V, range3_130, i);
                        }
                        else if ((i.SeriesRange == "-131 to -179" || i.SeriesRange == "131 to 179") && i.CountGames == 3 &&
                                ((HAB == "A" || HAB == "B") && i.Fav == "AWAY"))
                        {
                            agrupation3_179V = Set(agrupation3_179V, i);
                            range3_179 = setSummary3Games(agrupation3_179V, range3_179, i);
                        }
                        else if ((i.SeriesRange == "-180 and above" || i.SeriesRange == "180 and above") && i.CountGames == 3 &&
                                ((HAB == "A" || HAB == "B") && i.Fav == "AWAY"))
                        {
                            agrupation3_180V = Set(agrupation3_180V, i);
                            range3_180 = setSummary3Games(agrupation3_180V, range3_180, i);
                        }


                        // *********************************  RANGE 4  ************************************
                        else if ((i.SeriesRange == "-100 to -130" || i.SeriesRange == "100 to 130") && i.CountGames == 4 &&
                                ((HAB == "A" || HAB == "B") && i.Fav == "AWAY"))
                        {
                            agrupation4_130V = Set(agrupation4_130V, i);
                        }
                        else if ((i.SeriesRange == "-131 to -179" || i.SeriesRange == "131 to 179") && i.CountGames == 4 &&
                                ((HAB == "A" || HAB == "B") && i.Fav == "AWAY"))
                        {
                            agrupation4_179V = Set(agrupation4_179V, i);
                        }
                        else if ((i.SeriesRange == "-180 and above" || i.SeriesRange == "180 and above") && i.CountGames == 4 &&
                                ((HAB == "A" || HAB == "B") && i.Fav == "AWAY"))
                        {
                            agrupation4_180V = Set(agrupation4_180V, i);
                        }
                    }



                    //ADD THE INFORMATION TO THE FINAL LIST WHERE WE AGROUPED THE INFORMATION
                    if (HAB == "H" || HAB == "B")
                    {
                        if (agrupation3_130H.TotalGames > 0) Agrupation.Add(agrupation3_130H);
                        if (agrupation3_179H.TotalGames > 0) Agrupation.Add(agrupation3_179H);
                        if (agrupation3_180H.TotalGames > 0) Agrupation.Add(agrupation3_180H);

                        if (agrupation4_130H.TotalGames > 0) Agrupation.Add(agrupation4_130H);
                        if (agrupation4_179H.TotalGames > 0) Agrupation.Add(agrupation4_179H);
                        if (agrupation4_180H.TotalGames > 0) Agrupation.Add(agrupation4_180H);
                    }


                    if (HAB == "A" || HAB == "B")
                    {
                        if (agrupation3_130V.TotalGames > 0) Agrupation.Add(agrupation3_130V);
                        if (agrupation3_179V.TotalGames > 0) Agrupation.Add(agrupation3_179V);
                        if (agrupation3_180V.TotalGames > 0) Agrupation.Add(agrupation3_180V);

                        if (agrupation4_130V.TotalGames > 0) Agrupation.Add(agrupation4_130V);
                        if (agrupation4_179V.TotalGames > 0) Agrupation.Add(agrupation4_179V);
                        if (agrupation4_180V.TotalGames > 0) Agrupation.Add(agrupation4_180V);
                    }

                    rpTable2.DataSource = Agrupation;
                    rpTable2.DataBind();
                }


                if (range3_130.games > 0) SummaryList.Add(range3_130);
                if (range3_179.games > 0) SummaryList.Add(range3_179);
                if (range3_180.games > 0) SummaryList.Add(range3_180);


                if (List != null)
                {
                    rptTable.DataSource = List;
                    rptTable.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('Error to get the series: " + ex.Message + "');</script>");
            }
        }


        private csAgrupation Set(csAgrupation a, csMlbStat i)
        {
            a.SeriesRange = i.SeriesRange;
            a.TotalGames += i.CountGames;
            a.TotalSeries += 1;
            a.Fav = i.Fav;
            a.DogSwip += i.DogSwip;
            a.FavSwip += i.FavSwip;
            a.FavSwipPossible += i.FavSwipPossible;
            a.DogSwipPossible += i.DogSwipPossible;

            a.Game1 += (i.Game1 == 1) ? 1 : 0;
            a.Game1Percent = ((a.Game1 * 100) / a.TotalSeries);

            a.Game2 += (i.Game2 == 1) ? 1 : 0;
            a.Game2Percent = ((a.Game2 * 100) / a.TotalSeries);

            a.Game3 += (i.Game3 == 1) ? 1 : 0;
            a.Game3Percent = ((a.Game3 * 100) / a.TotalSeries);


            //how many games
            if(i.CountGames == 4)
            {
                a.Game4 += (i.Game4 == 1) ? 1 : 0;
                a.Game4Percent = ((a.Game4 * 100) / a.TotalSeries);
            }
            else
            {
                a.Game4 = null;
                a.Game4Percent = null;
            }




            // net and hold%
            if (i.Game1Line != null)
            {
                a.Risk1 += (i.Game1Line > 0) ? 100 : Math.Abs(Convert.ToInt32(i.Game1Line));
                int win = (a.Risk1 == 100) ? Convert.ToInt32(i.Game1Line) : 100;
                a.Net1 += (a.Game1 == 0 || a.Game1 == 2) ? -(a.Risk1) : win;
                a.Hold1 = Math.Round(Convert.ToDouble(Convert.ToDouble(a.Net1) / Convert.ToDouble(a.Risk1)),2,MidpointRounding.AwayFromZero);
                a.Hold1 *= 100;
            }


            if (i.Game2Line != null)
            {
                a.Risk2 += (i.Game2Line > 0) ? 100 : Math.Abs(Convert.ToInt32(i.Game2Line));
                int win = (a.Risk2 == 100) ? Convert.ToInt32(i.Game2Line) : 100;
                a.Net2 += (a.Game2 == 0 || a.Game2 == 2) ? -(a.Risk2) : win;
                a.Hold2 = Math.Round(Convert.ToDouble(Convert.ToDouble(a.Net2) / Convert.ToDouble(a.Risk2)),2,MidpointRounding.AwayFromZero);
                a.Hold2 *= 100;
            }


            if (i.Game3Line != null)
            {
                a.Risk3 += (i.Game3Line > 0) ? 100 : Math.Abs(Convert.ToInt32(i.Game3Line));
                int win = (a.Risk3 == 100) ? Convert.ToInt32(i.Game3Line) : 100;
                a.Net3 += (a.Game3 == 0 || a.Game1 == 3) ? -(a.Risk3) : win;
                a.Hold3 = Math.Round(Convert.ToDouble(Convert.ToDouble(a.Net3) / Convert.ToDouble(a.Risk3)), 2, MidpointRounding.AwayFromZero);
                a.Hold3 *= 100;
            }



            if (i.Game4Line != null)
            {
                a.Risk4 += (i.Game4Line > 0) ? 100 : Math.Abs(Convert.ToInt32(i.Game4Line));
                int win = (a.Risk4 == 100) ? Convert.ToInt32(i.Game4Line) : 100;
                a.Net4 += (a.Game4 == 0 || a.Game4 == 2) ? -(a.Risk4) : win;
                a.Hold4 = Math.Round(Convert.ToDouble(Convert.ToDouble(a.Net4) / Convert.ToDouble(a.Risk4)),2,MidpointRounding.AwayFromZero);
                a.Hold4 *= 100;
            }

            return a;
        }



        private csSummary setSummary3Games(csAgrupation a, csSummary s, csMlbStat i)
        {
            s.range = i.SeriesRange;
            s.games += 3;

            if (a.Game1 == 1 && a.Game2 == 1) s.wins += 1;
            else if(a.Game1 == 0 && a.Game2 == 0) s.loses += 1;

            s.risk += Convert.ToInt32(a.Risk1 + a.Risk2);
            s.net += Convert.ToInt32(a.Net1 + a.Net2);
            s.hold = Convert.ToDouble(s.net) / Convert.ToDouble(s.risk);
            return s;
        }

    }
}