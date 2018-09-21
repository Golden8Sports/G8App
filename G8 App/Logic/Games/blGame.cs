using Data.Connection;
using G8_App.Entities.Games;
using NHL_BL.Connection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using G8_App.Logic.Administration;
using G8_App.Connection;
using G8_App.Entities.Lines;

namespace G8_App.Logic.Games
{
    public class blGame : csComponentsConnection
    {
        private blCheckScores donbest = new blCheckScores();
        private blInterpreter inter = new blInterpreter();

        public ObservableCollection<csGame> ListGamesFromDGS(string dt1, string dt2, String idSport, int? idLeague, int? idLineType)
        {
            ObservableCollection<csGame> list = new ObservableCollection<csGame>();

            try
            {
                
                parameters.Add("@pStartDate", dt1);
                parameters.Add("@pEndDate", dt2);
                parameters.Add("@pIdSport", idSport.Trim());
                parameters.Add("@pIdLeague", idLeague);
                csGame g = null;

                dataset = csConnection.ExecutePA("[dbo].[web_getGames]", parameters);
                if (dataset.Tables[0].Rows.Count > 0)
                {
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
                        g.EventName = g.HomeTeam + " vs " + g.VisitorTeam;

                        if (!String.IsNullOrWhiteSpace(fila["HomeScore"].ToString())) g.HomeScoreDGS = Convert.ToInt32(fila["HomeScore"]); else g.HomeScoreDGS = null;
                        if (!String.IsNullOrWhiteSpace(fila["VisitorScore"].ToString())) g.VisitorScoreDGS = Convert.ToInt32(fila["VisitorScore"]); else g.VisitorScoreDGS = null;
                      
                        if(idLineType != null)g = SetValues(g, idLineType);

                        list.Add(g);
                    }
                }
                else
                {
                    list = null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                parameters.Clear();
            }

            return list;
        }


        public csGame GetGame(int idGame)
        {
            parameters.Add("@pIdGame", idGame);
            dataset = csConnection.ExecutePA("[dbo].[web_getGameById]", parameters);
            csGame g = new csGame();

            if (dataset.Tables[0].Rows.Count == 1)
            {
                g.VisitorNumber = Convert.ToInt32(dataset.Tables[0].Rows[0]["VisitorNumber"].ToString());
                g.LeagueName = dataset.Tables[0].Rows[0]["Description"].ToString();
                g.VisitorTeam = dataset.Tables[0].Rows[0]["VisitorTeam"].ToString();
                g.HomeTeam = dataset.Tables[0].Rows[0]["HomeTeam"].ToString();
                g.IdSportDGS = dataset.Tables[0].Rows[0]["IdSport"].ToString();
                g.Period = Convert.ToInt32(dataset.Tables[0].Rows[0]["Period"].ToString());
                g.IdSportDonBest = CastIdSportDonBest((g.IdSportDGS == "TNT" || g.IdSportDGS == "MU") ? inter.Interpretar(g.LeagueName).Trim() : g.IdSportDGS.Trim()).ToString();
                g.GameDate = Convert.ToDateTime(dataset.Tables[0].Rows[0]["EventDate"].ToString());
            }

            return g;
        }



        public csGame FindEvent(csGame g)
        {

            parameters.Add("@pRot", CastRot(g));
            parameters.Add("@pDate", g.GameDate);
            parameters.Add("@pIdSport", Convert.ToInt32(g.IdSportDonBest.Trim()));
            dataset = csDonBest.ExecutePA("[dbo].[web_findEvent]", parameters);

            if (dataset.Tables[0].Rows.Count == 1)
            {
                g.EventId = Convert.ToInt32(dataset.Tables[0].Rows[0]["id"].ToString());
                //g = FindLive(g);
            }else
            {
                if (g.VisitorTeam.ToUpper().Contains("ALT LINE") || g.HomeTeam.ToUpper().Contains("ALT LINE") ||
                    g.LeagueName.ToUpper().Contains("ALTERN"))
                {
                    parameters.Add("@pVisitorTeam", g.VisitorTeam.ToUpper().Replace("ALT LINE", "").Trim());
                    parameters.Add("@pHomeTeam", g.HomeTeam.ToUpper().Replace("ALT LINE", "").Trim());
                    parameters.Add("@pDate", g.GameDate.Year + "-" + g.GameDate.Month + "-" + g.GameDate.Day);
                    dataset = csDonBest.ExecutePA("[dbo].[web_getGameWithValue]", parameters);

                    if (dataset.Tables[0].Rows.Count == 1)
                    {
                        g.EventId = Convert.ToInt32(dataset.Tables[0].Rows[0]["id"].ToString());
                    }
                    else
                    {
                        csGame gAux = new csGame();
                        gAux = GetParent(g);
                        parameters.Add("@pVisitorTeam", gAux.VisitorTeamAux.ToUpper().Replace("ALT LINE", "").Trim());
                        parameters.Add("@pHomeTeam", gAux.HomeTeamAux.ToUpper().Replace("ALT LINE", "").Trim());
                        parameters.Add("@pDate", g.GameDate.Year + "-" + g.GameDate.Month + "-" + g.GameDate.Day);
                        dataset = csDonBest.ExecutePA("[dbo].[web_getGameWithValue]", parameters);

                        if (dataset.Tables[0].Rows.Count == 1)
                        {
                            g.EventId = Convert.ToInt32(dataset.Tables[0].Rows[0]["id"].ToString());
                        }
                    }
                }
            }

            return g;
        }


        private csGame FindLive(csGame g)
        {
            try
            {
                g.isLive = ((g.VisitorNumber > 9000 || g.VisitorNumber < 10000) && (g.IdSportDonBest == "3" || g.IdSportDonBest == "1")) ? 1 : 0;

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
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in chech if it is Live: " + ex.Message);
            }
            return g;
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

                    if (dataset.Tables[0].Rows.Count == 1)
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



        public ObservableCollection<csGame> ListGamesFromGraph(string dt1, string dt2, String idSport, int? idLeague)
        {
            ObservableCollection<csGame> list = new ObservableCollection<csGame>();
            try
            {
                parameters.Add("@pStartDate", dt1);
                parameters.Add("@pEndDate", dt2);
                parameters.Add("@pIdSport", idSport.Trim());
                parameters.Add("@pIdLeague", idLeague);
                csGame g = null;

                dataset = csConnection.ExecutePA("[dbo].[web_getGames]", parameters);
                if (dataset.Tables[0].Rows.Count > 0)
                {
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
                        g.GamePeriod = CastPeriod(g);
                        if (!String.IsNullOrWhiteSpace(fila["GradedDate"].ToString())) g.GraddedDate = "YES"; else g.GraddedDate = "NO";

                         
                        if (!String.IsNullOrWhiteSpace(fila["HomeScore"].ToString())) g.HomeScoreDGS = Convert.ToInt32(fila["HomeScore"]); else g.HomeScoreDGS = null;
                        if (!String.IsNullOrWhiteSpace(fila["VisitorScore"].ToString())) g.VisitorScoreDGS = Convert.ToInt32(fila["VisitorScore"]); else g.VisitorScoreDGS = null;

                        g.Score = g.VisitorScoreDGS + " - " + g.HomeScoreDGS;

                        g = FindEvent(g);

                        g.EventNameWithId = g.VisitorTeam + " vs " + g.HomeTeam;
                        g.EventName = "[" + g.VisitorNumber + "] " + g.VisitorTeam + " vs  [" + g.HomeNumber + "] " + g.HomeTeam;
                        g = TotalRisk(g);

                        if (g.VisitorTeam.ToUpper().Contains("VS ") || g.VisitorTeam.ToUpper().Contains(" VS") ||
                           g.HomeTeam.ToUpper().Contains("VS ") || g.HomeTeam.ToUpper().Contains(" VS"))
                        {
                            int A = 0;
                        }else
                        {
                           if(g.EventId != 0) list.Add(g);
                        }
                            
                    }
                }
                else
                {
                    list = null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                parameters.Clear();
            }
            return list;
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




        public csGame SetValues(csGame g, int? idLineType)
        {
            try
            {
                parameters.Add("@pIdGame", g.IdGame);
                parameters.Add("@pIdLineType", idLineType);
                dataset = csConnection.ExecutePA("[dbo].[spListGameVales]", parameters);
                if (dataset.Tables[0].Rows.Count > 0)
                {
                        DataRow fila = dataset.Tables[0].Rows[0];

                        if (!String.IsNullOrWhiteSpace(fila["VisitorSpread"].ToString()) && !String.IsNullOrWhiteSpace(fila["VisitorSpreadOdds"].ToString()))
                            g.ClSpreadVis = fila["VisitorSpread"].ToString() + " " + fila["VisitorSpreadOdds"].ToString(); 
                        else g.ClSpreadVis = null;

                        if (!String.IsNullOrWhiteSpace(fila["HomeSpread"].ToString()) && !String.IsNullOrWhiteSpace(fila["HomeSpreadOdds"].ToString()))
                            g.ClSpreadHom = fila["HomeSpread"].ToString() + " " + fila["HomeSpreadOdds"].ToString();
                        else g.ClSpreadHom = null;

                        if (!String.IsNullOrWhiteSpace(fila["VisitorOdds"].ToString()))
                            g.ClMoneyLineVis = fila["VisitorOdds"].ToString();
                        else g.ClMoneyLineVis = null;

                        if (!String.IsNullOrWhiteSpace(fila["HomeOdds"].ToString()))
                            g.ClMoneyLineHom = fila["HomeOdds"].ToString();
                        else g.ClMoneyLineHom = null;

                        if (!String.IsNullOrWhiteSpace(fila["VisitorSpecialOdds"].ToString()))
                            g.ClDraw = fila["VisitorSpecialOdds"].ToString();
                        else g.ClDraw = null;

                        if (!String.IsNullOrWhiteSpace(fila["TotalOver"].ToString()) &&
                            !String.IsNullOrWhiteSpace(fila["OverOdds"].ToString()))
                            g.ClTotalOver = fila["TotalOver"].ToString() + " " + fila["OverOdds"].ToString();
                        else g.ClTotalOver = null;

                        if (!String.IsNullOrWhiteSpace(fila["TotalUnder"].ToString()) &&
                            !String.IsNullOrWhiteSpace(fila["UnderOdds"].ToString()))
                            g.ClTotalUnder = fila["TotalUnder"].ToString() + " " + fila["UnderOdds"].ToString();
                        else g.ClTotalUnder = null;

                        if (g.ClSpreadVis == null && g.ClSpreadHom == null) g = SetLine(g,"E", 0,1,idLineType);
                        if (g.ClTotalOver == null && g.ClTotalUnder == null) g = SetLine(g, "E", 2,3, idLineType);
                        if (g.ClMoneyLineVis == null && g.ClMoneyLineHom == null) g = SetLine(g, "E", 4,5, idLineType);
                        if (g.ClDraw == null) g = SetLine(g, "E", 6,6, idLineType);

                        if (g.OpSpreadVis == null && g.OpSpreadHom == null) g = SetLine(g, "F", 0, 1, idLineType);
                        if (g.OpTotalOver == null && g.OpTotalUnder == null) g = SetLine(g, "F", 2, 3, idLineType);
                        if (g.OpMoneyLineVis == null && g.OpMoneyLineHom == null) g = SetLine(g, "F", 4, 5, idLineType);
                        if (g.OpDraw == null) g = SetLine(g, "F", 6,6,idLineType);
                }
            }
            catch (Exception ex)
            {
                return g;
            }
            finally
            {
                parameters.Clear();
            }
            return g;
        }



        private csGame SetLine(csGame g, string order, int case1, int case2, int? idLineType)
        {
            parameters.Clear();
            parameters.Add("@pIdGame", g.IdGame);
            parameters.Add("@pPlay", case1);
            parameters.Add("@pPlay2", case2);
            parameters.Add("@pFisrtEnd", order);
            parameters.Add("@pIdLineType", idLineType);

            dataset = csConnection.ExecutePA("[dbo].[web_OrderLines]", parameters);

            if(dataset.Tables[0].Rows.Count > 0)
            {
                if (case1 == 0 && order == "E")
                {
                    g.ClSpreadVis = dataset.Tables[0].Rows[0]["ChangeValue"].ToString();
                    g.ClSpreadHom = dataset.Tables[0].Rows[0]["ChangeValue2"].ToString();
                }
                else if (case1 == 2 && order == "E")
                {
                    g.ClTotalOver = dataset.Tables[0].Rows[0]["ChangeValue"].ToString().Replace("o", "").Trim();
                    g.ClTotalUnder = dataset.Tables[0].Rows[0]["ChangeValue2"].ToString().Replace("u", "").Trim();
                }
                else if (case1 == 4 && order == "E")
                {
                    g.ClMoneyLineVis = dataset.Tables[0].Rows[0]["ChangeValue"].ToString();
                    g.ClMoneyLineHom = dataset.Tables[0].Rows[0]["ChangeValue2"].ToString();
                }
                else if (case1 == 6 && order == "E") g.ClDraw = dataset.Tables[0].Rows[0]["ChangeValue"].ToString();

                // ********************************     FINAL    **********************************+++++

                else if (case1 == 0 && order == "F")
                {
                    g.OpSpreadVis = dataset.Tables[0].Rows[0]["ChangeValue"].ToString();
                    g.OpSpreadHom = dataset.Tables[0].Rows[0]["ChangeValue2"].ToString();
                }
                else if (case1 == 2 && order == "F")
                {
                    g.OpTotalOver = dataset.Tables[0].Rows[0]["ChangeValue"].ToString().Replace("o", "").Trim();
                    g.OpTotalUnder = dataset.Tables[0].Rows[0]["ChangeValue2"].ToString().Replace("u", "").Trim();
                }
                else if (case1 == 4 && order == "F")
                {
                    g.OpMoneyLineVis = dataset.Tables[0].Rows[0]["ChangeValue"].ToString();
                    g.OpMoneyLineHom = dataset.Tables[0].Rows[0]["ChangeValue2"].ToString();
                }
                else if (case1 == 6 && order == "F") g.OpDraw = dataset.Tables[0].Rows[0]["ChangeValue"].ToString();
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




        public csGame FindEventLight(csGame g)
        {
            try
            {
                parameters.Add("@pRot", CastRot(g));
                parameters.Add("@pDate", g.GameDate);
                parameters.Add("@pIdSport", Convert.ToInt32(g.IdSportDonBest));
                dataset = csDonBest.ExecutePA("[dbo].[web_findEvent]", parameters);

                if (dataset.Tables[0].Rows.Count == 1)
                {
                    g.EventId = Convert.ToInt32(dataset.Tables[0].Rows[0]["id"].ToString());
                }
                else
                {
                    if (g.VisitorTeam.ToUpper().Contains("ALT LINE") || g.HomeTeam.ToUpper().Contains("ALT LINE") ||
                        g.LeagueName.ToUpper().Contains("ALTERN"))
                    {
                        parameters.Add("@pVisitorTeam", g.VisitorTeam.ToUpper().Replace("ALT LINE", "").Trim());
                        parameters.Add("@pHomeTeam", g.HomeTeam.ToUpper().Replace("ALT LINE", "").Trim());
                        parameters.Add("@pDate", g.GameDate.Year + "-" + g.GameDate.Month + "-" + g.GameDate.Day);
                        dataset = csDonBest.ExecutePA("[dbo].[web_getGameWithValue]", parameters);

                        if (dataset.Tables[0].Rows.Count == 1)
                        {
                            g.EventId = Convert.ToInt32(dataset.Tables[0].Rows[0]["id"].ToString());
                        }
                        else
                        {
                            parameters.Add("@pVisitorTeam", g.VisitorTeam.ToUpper().Replace("ALT LINE", "").Trim());
                            parameters.Add("@pHomeTeam", g.HomeTeam.ToUpper().Replace("ALT LINE", "").Trim());
                            parameters.Add("@pDate", g.GameDate.Year + "-" + g.GameDate.Month + "-" + g.GameDate.Day);
                            dataset = csDonBest.ExecutePA("[dbo].[web_getGameWithValue]", parameters);

                            if (dataset.Tables[0].Rows.Count == 1)
                            {
                                g.EventId = Convert.ToInt32(dataset.Tables[0].Rows[0]["id"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Errot to find the evendId: " + ex.Message + " Wager: " + g.IdGame);
                g.EventId = 0;
            }
            return g;
        }



        //*******************************************************************************

        public ObservableCollection<csGameStats> GameStats(int idGame)
        {
            ObservableCollection<csGameStats> list = new ObservableCollection<csGameStats>();
            try
            {
                parameters.Clear();
                parameters.Add("@LogIdUser", 74);
                parameters.Add("@StartDate", "");
                parameters.Add("@prmOffice", "");
                parameters.Add("@prmBook", "");
                parameters.Add("@prmIdGame", idGame);              
                parameters.Add("@prmGroupby", 0);
                parameters.Add("@prmOrderby", 0);
                csGameStats g = null;
                dataset = csConnection.ExecutePA("[dbo].[Report_Game_Statistics_Per_Game]", parameters);
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        g = new csGameStats();

                        if(!String.IsNullOrWhiteSpace(fila["Amount"].ToString()))
                        {
                            g.WagerType = fila["Group1"].ToString();
                            g.GamePeriod = fila["Group2"].ToString();
                            g.WagerPlay = fila["Group3"].ToString();
                            g.Risk = Convert.ToDouble(fila["Amount"]);
                            g.Net = Convert.ToDouble(fila["WinLost"]);
                            list.Add(g);
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                parameters.Clear();
            }
            return list;
        }



        //************************************************************************************

        public csGame TotalRisk(csGame g)
        {
            
            try
            {

                parameters.Clear();
                parameters.Add("@pIdGame", g.IdGame);
                parameters.Add("@pPlay", -1);
                dataset = csG8Apps.ExecutePA("[dbo].[web_betsByGame]", parameters); ;

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    ObservableCollection<csLine> data = new ObservableCollection<csLine>();
                    foreach (System.Data.DataRow fila in dataset.Tables[0].Rows)
                    {
                        csLine line = new csLine();
                        line.IdWager = Convert.ToInt32(fila["IdWager"]);
                        line.WagerPlay = fila["WagerPlay"].ToString().ToUpper();
                        line.Risk = Convert.ToDouble(fila["RiskAmount"]);
                        line.WagerType = fila["WAGERTYPE"].ToString();
                        data.Add(line);
                    }


                    if (data != null)
                    {
                        List<int> Enteros = new List<int>();
                        foreach (var i in data)
                        {
                            if (!i.WagerType.ToUpper().Contains("STRAIGHT"))
                            {
                                if (!Enteros.Contains(Convert.ToInt32(i.IdWager)))
                                {
                                    Enteros.Add(i.IdWager);
                                    g.RISK += i.Risk;
                                    g.BETS += 1;
                                }
                            }
                            else
                            {
                                g.RISK += i.Risk;
                                g.BETS += 1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                parameters.Clear();
            }

            return g;
        }





    }
}