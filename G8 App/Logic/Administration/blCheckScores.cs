using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Connection;
using System.Collections.ObjectModel;
using G8_App.Entities.Games;
using System.Data;
using NHL_BL.Connection;
using G8_App.Connection;
using Excel = Microsoft.Office.Interop.Excel;
using G8_App.Logic.Scores;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using G8_App.Entities.Scores;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace G8_App.Logic.Administration
{
    public class blCheckScores : csComponentsConnection
    {
        private blInterpreter inter = new blInterpreter();

        public ObservableCollection<csGame> ListGamesFromDGS(string dt1, string dt2, String idSport, string idDonBest, string status)
        {
            ObservableCollection<csGame> DGSList = null;
            string idSportDonBest = (idDonBest == "TNT" || idDonBest == "MU") ? inter.Interpretar(idDonBest).Trim() : idDonBest.Trim();
            if (idDonBest != "-1") idSportDonBest = CastIdSportDonBest(idSport.Trim()).ToString();

            try
            {
                parameters.Add("@pStartDate", dt1);
                parameters.Add("@pEndDate", dt2);
                parameters.Add("@pIdSport", idSport.Trim());
                csGame g = null;          

                dataset = csConnection.ExecutePA("[dbo].[web_getGamesFilters]", parameters);
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DGSList = new ObservableCollection<csGame>();
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        g = new csGame();
                        g.IdGame = Convert.ToInt32(fila["IdGame"]);
                        g.HomeTeam = Convert.ToString(fila["HomeTeam"]);
                        g.VisitorTeam = Convert.ToString(fila["VisitorTeam"]);
                        g.IdLeague = Convert.ToInt32(fila["IdLeague"]);
                        g.IdSportDGS = Convert.ToString(fila["IdSport"]).Trim();
                        g.HomeNumber = Convert.ToInt32(fila["HomeNumber"]);
                        g.VisitorNumber = Convert.ToInt32(fila["VisitorNumber"]);
                        g.GameDate = Convert.ToDateTime(fila["EventDate"]);
                        g.ParentNumber = Convert.ToInt32(fila["ParentGame"]);
                        g.FamilyNumber = Convert.ToInt32(fila["FamilyGame"]);
                        g.Period = Convert.ToInt32(fila["Period"]);
                        g.LeagueName = Convert.ToString(fila["DESCRIP"]);
                        g.IdSportDonBest = CastIdSportDonBest((g.IdSportDGS == "TNT" || g.IdSportDGS == "MU") ? inter.Interpretar(g.LeagueName).Trim() : g.IdSportDGS.Trim()).ToString();
                        g.VisitorNumberAux = g.VisitorNumber;
                        g.HomeNumberAux = g.HomeNumber;
                        g.GamePeriod = CastPeriod(g);

                        if (!String.IsNullOrWhiteSpace(fila["HomeScore"].ToString())) g.HomeScoreDGS = Convert.ToInt32(fila["HomeScore"]); else g.HomeScoreDGS = null;
                        if (!String.IsNullOrWhiteSpace(fila["VisitorScore"].ToString())) g.VisitorScoreDGS = Convert.ToInt32(fila["VisitorScore"]); else g.VisitorScoreDGS = null;

                        if (g.LeagueName.ToUpper().Contains("LIVE"))
                        {
                            g = GetParent(g);
                            g.isLive = 1;
                        }

                        if (g.HomeScoreDGS != null && g.VisitorScoreDGS != null)
                        {
                            csGame G1 = FindEvent(g);
                            if(G1 != null)
                            {
                                g.HomeScoreDonBest = G1.HomeScoreDonBest;
                                g.VisitorScoreDonBest = G1.VisitorScoreDonBest;
                                g.Status = (g.VisitorScoreDGS == g.VisitorScoreDonBest && g.HomeScoreDGS == g.HomeScoreDonBest) ? "RIGHT" : "WRONG";
                                if (g.Status.ToUpper().Contains(status)) DGSList.Add(g);
                            }
                            else
                            {
                                g.Status = "MISSING";
                                if(g.Status.ToUpper().Contains(status)) DGSList.Add(g);
                            }                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error to get Games from DGS: " +  ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return (DGSList != null && DGSList.Count > 0) ? new ObservableCollection<csGame>(DGSList.OrderByDescending(x => x.Status).ToList()) : null;
        }


        private csGame GetParent(csGame g)
        {
            if (g != null)
            {
                try
                {
                    parameters.Clear();
                    parameters.Add("@pParentId", g.ParentNumber);
                    dataset = csConnection.ExecutePA("[dbo].[web_getParentScore]", parameters);

                    if(dataset.Tables[0].Rows.Count == 1)
                    {
                        g.IdGame = Convert.ToInt32(dataset.Tables[0].Rows[0]["IdGame"]);
                        g.HomeNumber = Convert.ToInt32(dataset.Tables[0].Rows[0]["HomeNumber"]);
                        g.VisitorNumber = Convert.ToInt32(dataset.Tables[0].Rows[0]["VisitorNumber"]);
                        g.VisitorTeamAux = dataset.Tables[0].Rows[0]["VisitorTeam"].ToString();
                        g.HomeTeamAux = dataset.Tables[0].Rows[0]["HomeTeam"].ToString();
                        return g;
                    }
                }
                catch (Exception ex)
                {
                    return g;
                }
            }
            return g;
        }



        private int CastIdSportDonBest(string sport)
        {
            if (sport == "SOC") return 5;
            else if (sport == "NHL") return 4;
            else if (sport == "NFL" || sport == "CFB") return 1;
            else if (sport == "NBA" || sport == "CBB") return 2;
            else if (sport == "MLB") return 3;
            else if (sport == "BOXING" || sport == "MMA") return 6;
            else if (sport == "GOLF") return 7;
            else if (sport == "TENNIS") return 8;
            else if (sport == "RAC") return 9;
            else return 10;
        }

        private csGame setScoresFromDonBest(csGame g, csGame DGS)
        {
            return CastSeleccion(g,DGS);
        }

        private csGame CastSeleccion(csGame g,csGame DGS)
        {

            if(DGS.VisitorNumber == 305911)
            {
                int SD = 01;
            }

            if (g.IdSportDonBest == "1") return ScoresGeneral(g, DGS);
            else if (g.IdSportDonBest == "2") return ScoresGeneral(g, DGS);
            else if (g.IdSportDonBest == "3") return ScoresGeneral(g, DGS);
            else if (g.IdSportDonBest == "4") return ScoresGeneral(g, DGS);
            else if (g.IdSportDonBest == "5")
            { if (DGS.Period == 0)
                {
                    if(DGS.isLive == 1)
                    {
                        if(DGS.VisitorTeam.ToUpper().Contains(" ET") || DGS.VisitorTeam.ToUpper().Contains("ET ")) return ScoresGeneral(g, DGS);
                        else if (DGS.VisitorTeam.ToUpper().Contains(" PEN") || DGS.VisitorTeam.ToUpper().Contains("PEN ")) return ScoresGeneral(g, DGS);
                        else if (DGS.VisitorTeam.ToUpper().Contains(" FULL") || DGS.VisitorTeam.ToUpper().Contains("FULL ")) return ScoresGeneral(g, DGS);
                    }
                    else
                    {
                        return ScoresSoccer(g, DGS);
                    }                   
                }
                else return ScoresGeneral(g, DGS);
            }
            else if (g.IdSportDonBest == "6") return ScoresFighting(g, DGS);
            else if (g.IdSportDonBest == "7") return ScoresGolf2(g, DGS);
            else if (g.IdSportDonBest == "8") return ScoresTennis2(g, DGS);
           // else if (g.IdSportDonBest == "9") return ScoresTennis(g, DGS);
            return g;
        }


        //get the rot between games
        private int CastRot(csGame g)
        {
            if (g.IdSportDonBest == "4" && g.VisitorNumber >= 1000) return Converter(g);
            return (g.Period == 0) ? (g.IdSportDonBest == "5" && g.VisitorNumber > 9000 && g.VisitorNumber < 10000) ? Converter(g) :
            (g.IdSportDonBest == "3" && (g.VisitorNumber > 4000 && g.VisitorNumber < 6000)) ? Converter(g) : g.VisitorNumber
            : Converter(g);
        }


        private int Converter(csGame g)
        {
            return Convert.ToInt32(g.VisitorNumber.ToString().Substring(1, g.VisitorNumber.ToString().Length - 1));
        }


        private csGame FindEvent(csGame g)
        {
            csGame G1 = null;
            try
            {
                parameters.Add("@pRot", CastRot(g));
                parameters.Add("@pDate", g.GameDate);
                parameters.Add("@pIdSport", Convert.ToInt32(g.IdSportDonBest.Trim()));
                dataset = csDonBest.ExecutePA("[dbo].[web_findEvent]", parameters);
                
                if (dataset.Tables[0].Rows.Count == 1)
                {
                    G1 = new csGame();
                    G1.EventId = Convert.ToInt32(dataset.Tables[0].Rows[0]["id"].ToString());
                    G1.VisitorNumber = Convert.ToInt32(dataset.Tables[0].Rows[0]["participant_away_rot"].ToString());
                    G1.HomeNumber = Convert.ToInt32(dataset.Tables[0].Rows[0]["participant_home_rot"].ToString());
                    G1.IdSportDonBest = dataset.Tables[0].Rows[0]["sport_id"].ToString();
                    G1.GameDate = Convert.ToDateTime(dataset.Tables[0].Rows[0]["date"].ToString());
                    G1 = FindLive(G1);
                    G1 = setScoresFromDonBest(G1, g);

                    if (G1.VisitorScoreDonBest == null && G1.HomeScoreDonBest == null)
                        return null;
                }

                if(G1 == null || G1.VisitorScoreDonBest == null || G1.HomeScoreDonBest == null)
                {

                    if(g.VisitorNumber == 225265)
                    {
                        int asfs = 0;
                    }

                    if (g.VisitorTeam.ToUpper().Contains("ALT LINE") || g.HomeTeam.ToUpper().Contains("ALT LINE") ||
                        g.LeagueName.ToUpper().Contains("ALTERN"))
                    {
                        parameters.Add("@pVisitorTeam", g.VisitorTeam.ToUpper().Replace("ALT LINE", "").Trim());
                        parameters.Add("@pHomeTeam", g.HomeTeam.ToUpper().Replace("ALT LINE", "").Trim());
                        parameters.Add("@pDate", g.GameDate.Year + "-" + g.GameDate.Month + "-" + g.GameDate.Day);
                        dataset = csDonBest.ExecutePA("[dbo].[web_getGameWithValue]", parameters);

                        if(dataset.Tables[0].Rows.Count == 1)
                        {
                            G1 = new csGame();
                            G1.EventId = Convert.ToInt32(dataset.Tables[0].Rows[0]["id"].ToString());
                            G1.VisitorNumber = Convert.ToInt32(dataset.Tables[0].Rows[0]["participant_away_rot"].ToString());
                            G1.HomeNumber = Convert.ToInt32(dataset.Tables[0].Rows[0]["participant_home_rot"].ToString());
                            G1.IdSportDonBest = dataset.Tables[0].Rows[0]["sport_id"].ToString();
                            G1.GameDate = Convert.ToDateTime(dataset.Tables[0].Rows[0]["date"].ToString());
                           // G1 = FindLive(G1);
                            G1 = setScoresFromDonBest(G1, g);
                            if (G1.VisitorScoreDonBest == null && G1.HomeScoreDonBest == null)
                            return null;
                        }else
                        {
                            csGame gAux = new csGame();
                            gAux = GetParent(g);
                            parameters.Add("@pVisitorTeam", gAux.VisitorTeamAux.ToUpper().Replace("ALT LINE","").Trim());
                            parameters.Add("@pHomeTeam", gAux.HomeTeamAux.ToUpper().Replace("ALT LINE", "").Trim());
                            parameters.Add("@pDate", g.GameDate.Year + "-" + g.GameDate.Month + "-" + g.GameDate.Day);
                            dataset = csDonBest.ExecutePA("[dbo].[web_getGameWithValue]", parameters);

                            if (dataset.Tables[0].Rows.Count == 1)
                            {
                                G1 = new csGame();
                                G1.EventId = Convert.ToInt32(dataset.Tables[0].Rows[0]["id"].ToString());
                                G1.VisitorNumber = Convert.ToInt32(dataset.Tables[0].Rows[0]["participant_away_rot"].ToString());
                                G1.HomeNumber = Convert.ToInt32(dataset.Tables[0].Rows[0]["participant_home_rot"].ToString());
                                G1.IdSportDonBest = dataset.Tables[0].Rows[0]["sport_id"].ToString();
                                G1.GameDate = Convert.ToDateTime(dataset.Tables[0].Rows[0]["date"].ToString());
                                // G1 = FindLive(G1);
                                G1 = setScoresFromDonBest(G1, g);
                                if (G1.VisitorScoreDonBest == null && G1.HomeScoreDonBest == null)
                                    return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in get Event: " + ex.Message);
            }

            return G1;
        }




        private csGame FindLive(csGame g)
        {
            try
            {
                g.isLive = ((g.VisitorNumber > 9000 || g.VisitorNumber < 10000) && ( g.IdSportDonBest == "3" || g.IdSportDonBest == "1")) ? 1 : 0;

                // ******************   is Live   *****************
                if (g.isLive == 1)
                {
                    parameters.Add("@pRot", g.VisitorNumber.ToString().Substring(1, g.VisitorNumber.ToString().Length - 1));
                    parameters.Add("@pDate", g.GameDate);
                    parameters.Add("@pIdSport", Convert.ToInt32(g.IdSportDonBest.Trim()));
                    dataset = csDonBest.ExecutePA("[dbo].[web_findEvent]", parameters);
                    if (dataset.Tables[0].Rows.Count == 1)
                    {
                        g.EventId = Convert.ToInt32(dataset.Tables[0].Rows[0]["id"].ToString());
                        g.VisitorNumber = Convert.ToInt32(dataset.Tables[0].Rows[0]["participant_away_rot"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in chech if it is Live: " + ex.Message);
            }
            return g;
        }


        //CAST PERIOD TO SCORE
        private string CastPeriodScore(csGame g, csGame DGS)
        {
            try
            {
                if (g.IdSportDonBest.Trim() == "1") //football
                {
                    if (DGS.Period == 0) return "T";
                    else if (DGS.Period == 1) return "1H";
                    else if (DGS.Period == 2) return "2H";
                    else if (DGS.Period == 3) return "1";
                    else if (DGS.Period == 4) return "2";
                    else if (DGS.Period == 5) return "3";
                    else if (DGS.Period == 6) return "4";
                    else if (DGS.Period == 10) return "OT";
                }
                else if (g.IdSportDonBest.Trim() == "2") //basketball
                {
                    if (DGS.Period == 0) return "T";
                    else if (DGS.Period == 1) return "1H";
                    else if (DGS.Period == 2) return "2H";
                    else if (DGS.Period == 3) return "1";
                    else if (DGS.Period == 4) return "2";
                    else if (DGS.Period == 5) return "3";
                    else if (DGS.Period == 6) return "4";
                    else if (DGS.Period == 10) return "OT";

                }
                else if (g.IdSportDonBest.Trim() == "3") //baseball
                {
                    if (DGS.Period == 0) return "T";
                    else if (DGS.Period == 1) return "1H";
                    else if (DGS.Period == 2) return "2H";
                    else if (DGS.Period == 3) return "1";
                    else if (DGS.Period == 4) return "2";
                    else if (DGS.Period == 5) return "3";
                    else if (DGS.Period == 6) return "4";
                    else if (DGS.Period == 10) return "OT";
                }
                else if (g.IdSportDonBest.Trim() == "4") //hockey
                {
                    if (DGS.Period == 0) return "T";
                    else if (DGS.Period == 7) return "1";
                    else if (DGS.Period == 8) return "2";
                    else if (DGS.Period == 9) return "3";
                    else if (DGS.Period == 10) return "OT";
                }
                else if (g.IdSportDonBest.Trim() == "5") //soccer
                {
                    if (DGS.Period == 0)
                    {
                        if(DGS.isLive == 1)
                        {
                            if (DGS.VisitorTeam.ToUpper().Contains(" ET") || (DGS.VisitorTeam.ToUpper().Contains("ET "))) return "ET";
                            else if (DGS.VisitorTeam.ToUpper().Contains(" PEN") || DGS.VisitorTeam.ToUpper().Contains("PEN ")) return "PK";
                            else return "T";
                        }else
                        {
                            return "T";
                        }
                    }
                    else if (DGS.Period == 1) return "1";
                    else if (DGS.Period == 2) return "2";
                    else if (DGS.Period == 10) return "ET";
                }
                else if (g.IdSportDonBest.Trim() == "6")//boxing
                {
                    if (DGS.Period == 0) return "T";
                    else if (DGS.Period == 10) return "OT";
                }
                else if (g.IdSportDonBest.Trim() == "7") //golf
                {
                    if (DGS.Period == 0) return "T";
                    else if (DGS.Period == 10) return "OT";
                }
                else if (g.IdSportDonBest.Trim() == "8") //tennis
                {
                    if (DGS.Period == 0) return "T";
                    else if (DGS.Period == 10) return "OT";
                }
                else if (g.IdSportDonBest.Trim() == "9") //auto racing
                {
                    if (DGS.Period == 0) return "T";
                    else if (DGS.Period == 10) return "OT";
                }
                else if (g.IdSportDonBest.Trim() == "10") //other
                {
                    if (DGS.Period == 0) return "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return "";
        }


        private csGame ScoresGeneral(csGame g, csGame DGS)
        {
            try
            {
                parameters.Add("@pIdEvent", g.EventId);
                parameters.Add("@pPeriod", CastPeriodScore(g,DGS));
                dataset = csDonBest.ExecutePA("[dbo].[web_GetScore]", parameters);
                int sum = dataset.Tables[0].Rows.Count;

                if (sum == 1)
                {
                    g.VisitorScoreDonBest = (!String.IsNullOrWhiteSpace(dataset.Tables[0].Rows[0]["away_score"].ToString()))? Convert.ToInt32(dataset.Tables[0].Rows[0]["away_score"].ToString()) : g.VisitorScoreDonBest;
                    g.HomeScoreDonBest = (!String.IsNullOrWhiteSpace(dataset.Tables[0].Rows[0]["home_score"].ToString())) ? Convert.ToInt32(dataset.Tables[0].Rows[0]["home_score"].ToString()) : g.HomeScoreDonBest;
                }
            }
            catch (Exception ex)
            {
                g.VisitorScoreDonBest = null;
                g.HomeScoreDonBest = null;
            }
            return g;
        }



        private csGame ScoresFighting(csGame g, csGame DGS)
        {
            try
            {
                parameters.Add("@pIdEvent", g.EventId);
                parameters.Add("@pPeriod", CastPeriodScore(g, DGS));
                dataset = csDonBest.ExecutePA("[dbo].[web_GetScore]", parameters);
                int sum = dataset.Tables[0].Rows.Count;

                if (sum == 1)
                {
                    String V = dataset.Tables[0].Rows[0]["away_description"].ToString();
                    String H = dataset.Tables[0].Rows[0]["home_description"].ToString();

                    if(H.ToUpper() == "WIN")
                    {
                        g.VisitorScoreDonBest = 0;
                        g.HomeScoreDonBest = 1;
                    }else if (V.ToUpper() == "WIN")
                    {
                        g.VisitorScoreDonBest = 1;
                        g.HomeScoreDonBest = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                g.VisitorScoreDonBest = null;
                g.HomeScoreDonBest = null;
            }

            return g;
        }





        private csGame ScoresGolf2(csGame g, csGame DGS)
        {
            try
            {
                parameters.Add("@pIdEvent", g.EventId);
                parameters.Add("@pPeriod", CastPeriodScore(g, DGS));
                dataset = csDonBest.ExecutePA("[dbo].[web_GetScore]", parameters);
                int sum = dataset.Tables[0].Rows.Count;

                if (sum == 1)
                {
                    int V = Convert.ToInt32(dataset.Tables[0].Rows[0]["away_score"].ToString());
                    int H = Convert.ToInt32(dataset.Tables[0].Rows[0]["home_score"].ToString());

                    if (V == H)
                    {
                        g.HomeScoreDonBest = 1;
                        g.VisitorScoreDonBest = 1;
                    }
                    else if (V > H)
                    {
                        g.HomeScoreDonBest = 1;
                        g.VisitorScoreDonBest = 0;
                    }
                    else if (V < H)
                    {
                        g.HomeScoreDonBest = 0;
                        g.VisitorScoreDonBest = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                g.VisitorScoreDonBest = null;
                g.HomeScoreDonBest = null;
            }
            return g;
        }



        private csGame ScoresTennis2(csGame g, csGame DGS)
        {
            try
            {
                parameters.Add("@pIdEvent", g.EventId);
                dataset = csDonBest.ExecutePA("[dbo].[web_getTennisScore]", parameters);
                int sum = dataset.Tables[0].Rows.Count;


                if (DGS.VisitorNumber == 8061)
                {
                    int asd = 0;
                }

                if (sum > 0)
                {
                    int homeSum = 0, visSum = 0;
                    int setHom = 0, setVis = 0;
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        int V = (!String.IsNullOrWhiteSpace(fila["away_score"].ToString())) ? Convert.ToInt32(fila["away_score"].ToString()) : 0;
                        int H = (!String.IsNullOrWhiteSpace(fila["home_score"].ToString())) ? Convert.ToInt32(fila["home_score"].ToString()) : 0;

                        if (fila["period"].ToString().Trim() != "5")
                        {
                            homeSum += H;
                            visSum += V;
                        }
                            
                            if(V == H)
                            {
                                setVis += 1;
                                setHom += 1;
                            }
                            else if (V > H)
                            {
                                setVis += 1;
                            }
                            else if (V < H)
                            {
                                setHom += 1;
                            }
    
                    }



                    if(homeSum > visSum && setHom < setVis)
                    {
                        g.HomeScoreDonBest = visSum;
                        g.VisitorScoreDonBest = homeSum;
                    }
                    else if (homeSum < visSum && setHom > setVis)
                    {
                        g.HomeScoreDonBest = visSum;
                        g.VisitorScoreDonBest = homeSum;

                    }else if(homeSum == visSum)
                    {
                        if (setHom > setVis)
                        {
                            g.HomeScoreDonBest = homeSum;
                            g.VisitorScoreDonBest = visSum;
                            g.HomeScoreDonBest += 1;
                        }
                        else if (setHom < setVis)
                        {
                            g.HomeScoreDonBest = homeSum;
                            g.VisitorScoreDonBest = visSum;
                            g.VisitorScoreDonBest += 1;
                        }
                    }else
                    {
                        g.HomeScoreDonBest = homeSum;
                        g.VisitorScoreDonBest = visSum;
                    }                 
                }
            }
            catch (Exception ex)
            {
                g.VisitorScoreDonBest = null;
                g.HomeScoreDonBest = null;
            }
            return g;
        }



        private string CastPeriod(csGame g)
        {
            if (g.Period == 0) return "FG";
            else if (g.Period == 1) return "1H";
            else if (g.Period == 2) return "2H";
            else if (g.Period == 3) return "1Q";
            else if (g.Period == 4) return "2Q";
            else if (g.Period == 5) return "3Q";
            else if (g.Period == 6) return "4Q";
            else if (g.Period == 7) return "1P";
            else if (g.Period == 8) return "2P";
            else if (g.Period == 9) return "3P";
            else if (g.Period == 10) return "OT";
            return "";
        }





        //SOCCER
        private csGame ScoresSoccer(csGame g, csGame DGS)
        {
            try
            {
                parameters.Add("@pIdEvent", g.EventId);
                dataset = csDonBest.ExecutePA("[dbo].[web_getSoccerScore]", parameters);
                int sum = dataset.Tables[0].Rows.Count;

                if (sum > 0 && sum <= 2)
                {
                    g.VisitorScoreDonBest = 0;
                    g.HomeScoreDonBest = 0;

                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        g.VisitorScoreDonBest += (!String.IsNullOrWhiteSpace(fila["away_score"].ToString())) ? Convert.ToInt32(fila["away_score"].ToString()) : 0;
                        g.HomeScoreDonBest += (!String.IsNullOrWhiteSpace(fila["home_score"].ToString())) ? Convert.ToInt32(fila["home_score"].ToString()) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                g.VisitorScoreDonBest = null;
                g.HomeScoreDonBest = null;
            }
            return g;
        }



        private DateTime? GetLastUpdate()
        {
            try
            {
                dataset = csDonBest.ExecutePA("[dbo].[web_getLastUpdateScore]", parameters);
                int sum = dataset.Tables[0].Rows.Count;
                if(sum == 1)
                {
                    return Convert.ToDateTime(dataset.Tables[0].Rows[0]["maxupdate"]);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }



        public void SyncUp()
        {
            DateTime? dt = GetLastUpdate();
            blScores scoreDB = new blScores();

            if (dt != null)
            {
                TimeSpan t = DateTime.Now - Convert.ToDateTime(dt);
                DateTime date = Convert.ToDateTime(dt);
                int days = Convert.ToInt32(t.TotalDays);

                for (int i = 0; i <= days; i++)
                {
                    SetInfo(date.Year + "-" + ((date.Month < 10) ? "0" + date.Month.ToString() : date.Month.ToString()) + "-" + ((date.Day < 10) ? "0" + date.Day.ToString() : date.Day.ToString()));
                    date = date.AddDays(1);
                }
            }

            // SetInfo("2018-08-02");
        }




        public void SyncUp(string date)
        {
           SetInfo(date);
        }



        private List<String> Headers = new List<String>();
        private List<String> Opp1 = new List<String>();
        private List<String> Opp2 = new List<String>();
        private List<csScore> ScoreList = new List<csScore>();
        private string sport = "";
        private void SetInfo(string date)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://server1.donbest.com/scores/" + date + "/all.html?");
            myRequest.Method = "GET";
            try
            {
                WebResponse myResponse = myRequest.GetResponse();
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                string result = sr.ReadToEnd();
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(result);

                foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
                {
                    int tableNumber = 0;
                    bool flag = false, first = false;

                    foreach (HtmlNode row in table.SelectNodes("tr"))
                    {
                        string f = row.InnerText;

                        if (f.ToUpper().Contains("SCORES"))
                        {
                            sport = CastIdSportDonBest2(f);
                        }

                        foreach (HtmlNode cell in row.SelectNodes("td"))
                        {
                            String c = cell.InnerText.ToString().Replace("&nbsp;", "").Trim().Replace("&nbsp", "").Trim();

                            if (DetectNumber(c))
                            {
                                first = flag = true;
                            }

                            if (flag)
                            {
                                if (tableNumber == 0) //save the headers
                                {
                                    if (first)
                                    {
                                        Headers.Add("rot");
                                        Headers.Add("team");
                                        first = false;
                                    }
                                    else
                                    {
                                        if (sport != "10" && sport != "8" && c.Trim() == "T")
                                        {
                                            Headers.Add(c);
                                            Headers.Add("status");
                                        }
                                        else if (sport == "8" && c.Trim() == "T")
                                        {
                                            Headers.Add("status");
                                        }
                                        else
                                        {
                                            Headers.Add(c);
                                        }
                                    }
                                }
                                else if (tableNumber == 1)//add values for the opponent 1
                                {
                                    Opp1.Add(c);
                                }
                                else if (tableNumber == 2)
                                {
                                    Opp2.Add(c);
                                }
                            }
                        }

                        if (flag)
                        {
                            tableNumber++;
                        }
                    }


                    int n1 = Headers.Count;
                    int n2 = Opp1.Count;
                    int n3 = Opp2.Count;
                    

                    if (Headers.Count == Opp1.Count && Opp1.Count == Opp2.Count && Headers.Count > 0)
                    {
                        csScore score = new csScore();

                        for (int i = 0; i < Headers.Count; i++)
                        {
                            score.Values.Add(new csDictionary(Headers[i], Opp1[i], Opp2[i]));
                        }

                        score.sportId = sport;
                        if(sport == "2")
                        {
                            int ds = 1;
                        }

                        ScoreList.Add(score);
                    }
                    else if (Headers.Count > 0)
                    {
                        int dssd = 0;
                    }

                    ClearValues();
                }

                blScores scoreDB = new blScores();
                if (ScoreList != null && ScoreList.Count > 0)
                {
                    foreach (var i in ScoreList)
                    {

                        csScore score = i;
                        score.awayRot = i.Values[0].Val1;
                        score.homeRot = i.Values[0].Val2;

                        for (int j = 2; j < i.Values.Count; j++)
                        {
                            if (i.Values[j].Header.ToUpper().Contains("OPEN"))
                            {
                                break;
                            }

                            score.period = i.Values[j].Header;
                            score.awayScore = i.Values[j].Val1;
                            score.homeScore = i.Values[j].Val2;
                            score.sportId = i.sportId;
                            score.date = date;

                            if ((j + 1) < (ScoreList.Count - 1) && i.Values[j + 1].Header == "status")
                            {
                                score.description1 = i.Values[j + 1].Val1.Replace("&nbsp;", "").Replace("\n", "").Trim();
                                score.description2 = i.Values[j + 1].Val2.Replace("&nbsp;", "").Replace("\n", "").Trim();
                            }
                            else
                            {
                                score.description1 = "";
                                score.description2 = "";
                            }

                            if (i.Values[j].Header != "status")
                                scoreDB.InsertScore(score);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                //lblNotFound.Text = "There is nothing to read. Error: " + ex.ToString() + "";
            }
        }



        private void ClearValues()
        {
            this.Headers = new List<string>();
            this.Opp1 = new List<string>();
            this.Opp2 = new List<string>();
        }


        private bool DetectNumber(string c)
        {
            if (c.Contains(":"))
            {
                c = c.Replace("pm", "").Trim().Replace("am", "").Trim().Replace("md", "").Trim();
                var split = c.Split(':');
                int num1;

                int count = split.Length;
                if (count >= 2)
                {
                    split[0] = split[0].Trim();
                    split[1] = split[1].Trim();
                }

                if (split[1].Length >= 3 && split[1].Length <= 30)
                {
                    string m = split[1];
                    if (split[1].ToUpper().Contains("(") && split[1].ToUpper().Contains(")"))
                    {
                        split[1] = split[1].Substring(0, 2);
                    }
                    string f = split[1];
                }
                return (int.TryParse(split[0], out num1) && int.TryParse(split[1], out num1));
            }
            return false;
        }



        private string CastIdSportDonBest2(string sport)
        {
            if (sport.ToUpper().Contains("SOCCER")) return "5";
            else if (sport.ToUpper().Contains("NHL") || sport.ToUpper().Contains("HOCKEY")) return "4";
            else if (sport.ToUpper().Contains("NFL") || sport.ToUpper().Contains("CFB") || sport.ToUpper().Contains("FOOTBALL")) return "1";
            else if (sport.ToUpper().Contains("NBA") || sport.ToUpper().Contains("CBB") || sport.ToUpper().Contains("BASKETBALL") || sport.ToUpper().Contains("WNBA") || sport.ToUpper().Contains("EUROLEAGUE")) return "2";
            else if (sport.ToUpper().Contains("BASEBALL") || sport.ToUpper().Contains("MLB")) return "3";
            else if (sport.ToUpper().Contains("BOXING") || sport.ToUpper().Contains("FIGHT") || sport.ToUpper().Contains("MMA")) return "6";
            else if (sport.ToUpper().Contains("GOLF")) return "7";
            else if (sport.ToUpper().Contains("TENNIS")) return "8";
            else if (sport.ToUpper().Contains("RACING")) return "9";
            else return "10";
        }




        public void GetFlash()
        {
            try
            {

                string test = GetHtmlPage("https://www.flashscore.com/");


                string result = string.Empty;
                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(test);

                HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//td");
                if (tables != null)
                    for (int k = 0; k < tables.Count; k++)
                    {
                        //tables
                        if(tables[k].Attributes.Contains("class"))
                        {
                            int wew = 0;
                            string dsds = tables[k].InnerHtml;
                        }

                        HtmlNodeCollection t = tables[k].SelectNodes("//table");
                        if(t != null)
                        {
                            foreach (var ta in t)
                            {
                                HtmlNodeCollection rows = ta.SelectNodes(".//tr");

                                if (rows != null)
                                    for (int i = 1; i < rows.Count; ++i)
                                    {
                                        HtmlNodeCollection cols = rows[i].SelectNodes(".//td");

                                        if (cols != null)
                                        {
                                            for (int j = 0; j < cols.Count; ++j)
                                            {

                                                string txt = cols[j].InnerText.Replace("\r\n", "").Trim().Replace("\t\t (R)", "").Trim().Replace("\t\t (L)", "").Trim();
                                                txt = txt.Replace("(OT)", "").Trim();
                                                txt = txt.Replace("@ ", "").Trim();
                                                txt = txt.Replace("(R)", "").Trim();
                                            }
                                        }

                                    }
                            }
                        }

                    }

                //Response.Clear();
                //Response.ContentType = "text/plain";
                //Response.AddHeader("Content-Disposition", "attachment;filename=Scrap.csv");
                //Response.Write(stringContent);
                //Response.End();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }



        private string GetHtmlPage(string strURL)
        {

            String strResult;
            WebResponse objResponse;
            WebRequest objRequest = HttpWebRequest.Create(strURL);
            objResponse = objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                strResult = sr.ReadToEnd();
                sr.Close();
            }
            // strResult = strResult.Remove(0, strResult.LastIndexOf("<table>"));
            string[] values = strResult.Split(new string[] { "<tbody>", "</tbody>" }, StringSplitOptions.RemoveEmptyEntries);

            // Response.Write("<table>" + values[1] + "</table>");
            ConvertHTMLTablesToDataSet("<table>" + values[0] + "</table>");
            //  List<string> list = new List<string>(values);

            return values[0];
        }

        private DataSet ConvertHTMLTablesToDataSet(string HTML)
        {
            // Declarations 
            DataSet ds = new DataSet();
            DataTable dt = null;
            DataRow dr = null;
            DataColumn dc = null;
            string TableExpression = "<table[^>]*>(.*?)</string></string></table>";
            string HeaderExpression = "<th[^>]*>(.*?)";
            string RowExpression = "<tr[^>]*>(.*?)";
            string ColumnExpression = "<td[^>]*>(.*?)";
            bool HeadersExist = false;
            int iCurrentColumn = 0;
            int iCurrentRow = 0;

            // Get a match for all the tables in the HTML 
            MatchCollection Tables = Regex.Matches(HTML, TableExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

            // Loop through each table element 
            foreach (Match Table in Tables)
            {
                // Reset the current row counter and the header flag 
                iCurrentRow = 0;
                HeadersExist = false;

                // Add a new table to the DataSet 
                dt = new DataTable();

                //Create the relevant amount of columns for this table (use the headers if they exist, otherwise use default names) 
                if (Table.Value.Contains("<th"))
                {
                    // Set the HeadersExist flag 
                    HeadersExist = true;

                    // Get a match for all the rows in the table 
                    MatchCollection Headers = Regex.Matches(Table.Value, HeaderExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    // Loop through each header element 
                    foreach (Match Header in Headers)
                    {
                        dt.Columns.Add(Header.Groups[1].ToString());
                    }
                }
            else
            {
                    for (int iColumns = 1; iColumns <= Regex.Matches(Regex.Matches(Regex.Matches(Table.Value, TableExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase)[0].ToString(), RowExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase)[0].ToString(), ColumnExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase).Count; iColumns++)
                    {
                        dt.Columns.Add("Column " + iColumns);
                    }
                }


                //Get a match for all the rows in the table 

                MatchCollection Rows = Regex.Matches(Table.Value, RowExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

                // Loop through each row element 
                foreach (Match Row in Rows)
                {
                    // Only loop through the row if it isn't a header row 
                    if (!(iCurrentRow == 0 && HeadersExist))
                    {
                        // Create a new row and reset the current column counter 
                        dr = dt.NewRow();
                        iCurrentColumn = 0;

                        // Get a match for all the columns in the row 
                        MatchCollection Columns = Regex.Matches(Row.Value, ColumnExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

                        // Loop through each column element 
                        foreach (Match Column in Columns)
                        {
                            // Add the value to the DataRow 
                            dr[iCurrentColumn] = Column.Groups[1].ToString();

                            // Increase the current column  
                            iCurrentColumn++;
                        }

                        // Add the DataRow to the DataTable 
                        dt.Rows.Add(dr);

                    }

                    // Increase the current row counter 
                    iCurrentRow++;
                }


                // Add the DataTable to the DataSet 
                ds.Tables.Add(dt);

            }
            //GridView1.DataSource = ds;
            //GridView1.DataBind();
            return ds;

        }






    }
}