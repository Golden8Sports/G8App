using Data.Connection;
using G8_App.Connection;
using G8_App.Entities.Profiling;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;

namespace G8_App.Logic.Profiling
{
    public class blSummary : csComponentsConnection
    {
        public ObservableCollection<csSummary> SummaryProfile(string dt1,string dt2, string idSport, string idLeague, int idAgent, string Player)
        {
            ObservableCollection<csSummary> data = new ObservableCollection<csSummary>();

            try
            {

                if(idAgent == -1)
                {
                    parameters.Clear();
                    parameters.Add("@pStartDate", dt1);
                    parameters.Add("@pEndDate", dt2);
                    parameters.Add("@pIdSport", idSport);
                    parameters.Add("@pIdLeague", idLeague);
                    parameters.Add("@pPlayer", Player);
                    dataset = csG8Apps.ExecutePA("[dbo].[spSummaryProfile]", parameters);
                }
                else if(idAgent != -1)
                {
                    parameters.Clear();
                    parameters.Add("@pStartDate", dt1);
                    parameters.Add("@pEndDate", dt2);
                    parameters.Add("@pIdSport", idSport);
                    parameters.Add("@pIdLeague", idLeague);
                    parameters.Add("@pIdAgent", idAgent);
                    parameters.Add("@pPlayer", Player);
                    dataset = csG8Apps.ExecutePA("[dbo].[spSummaryProfileWithAll]", parameters);
                }


                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        csSummary u = new csSummary(
                        Convert.ToString(fila["PLAYER"]),
                        Convert.ToInt32(fila["RISK"]),
                        Convert.ToInt32(fila["WIN"]),
                        Convert.ToInt32(fila["NET"]),
                        Convert.ToInt32(fila["BETS"]),
                        Convert.ToInt32(fila["WINS"]),
                        Convert.ToInt32(fila["DRAW"]),
                        Convert.ToInt32(fila["LOST"]),
                        Convert.ToInt32(fila["WIN_PER"]),
                        Math.Round(Convert.ToDouble(fila["HOLD_PER"]),2,MidpointRounding.AwayFromZero),
                        Convert.ToInt32(fila["SCALPINGPPH"]),
                        Convert.ToInt32(fila["SCALPINGJAZZ"]),
                        Convert.ToInt32(fila["SCALPINGPINNI"]),
                        Convert.ToInt32(fila["SCALPING5DIMES"]),
                        Convert.ToInt32(fila["SCALPINGCRIS"]),
                        Convert.ToInt32(fila["MOVELINE"]),
                        Convert.ToInt32(fila["BEATLINE"]),
                        Convert.ToInt32(fila["SYNDICATE"]));
                        data.Add(u);
                    }

                }
                else
                {
                    data = null;
                }

            }
            catch (Exception ex)
            {
                data = null;
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return data;
        }






        public ObservableCollection<csSummary> TopPlayerList(string dt1, string dt2, string idSport, string idLeague, int idAgent, string Player, string wagerType, string wagerPlay)
        {
            ObservableCollection<csSummary> data = new ObservableCollection<csSummary>();

            try
            {

                if (idAgent == -1)
                {
                    parameters.Clear();
                    parameters.Add("@pStartDate", dt1);
                    parameters.Add("@pEndDate", dt2);
                    parameters.Add("@pIdSport", idSport);
                    parameters.Add("@pIdLeague", idLeague);
                    parameters.Add("@pPlayer", Player);
                    parameters.Add("@pWagerType", wagerType);
                    parameters.Add("@pWagerPlay", wagerPlay);
                    dataset = csG8Apps.ExecutePA("[dbo].[web_topPlayers]", parameters);
                }
                else if (idAgent != -1)
                {
                    parameters.Clear();
                    parameters.Add("@pStartDate", dt1);
                    parameters.Add("@pEndDate", dt2);
                    parameters.Add("@pIdSport", idSport);
                    parameters.Add("@pIdLeague", idLeague);
                    parameters.Add("@pIdAgent", idAgent);
                    parameters.Add("@pPlayer", Player);
                    parameters.Add("@pWagerType", wagerType);
                    parameters.Add("@pWagerPlay", wagerPlay);
                    dataset = csG8Apps.ExecutePA("[dbo].[web_topPlayersAll]", parameters);
                }


                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        csSummary u = new csSummary(
                        Convert.ToString(fila["PLAYER"]),
                        Convert.ToString(fila["AGENT"]),
                        Convert.ToInt32(fila["RISK"]),
                        Convert.ToInt32(fila["NET"]),
                        Convert.ToInt32(fila["BETS"]),
                        Convert.ToInt32(fila["WINS"]));
                        data.Add(u);
                    }
                }
                else
                {
                    data = null;
                }

            }
            catch (Exception ex)
            {
                data = null;
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return (data == null) ? null : new ObservableCollection<csSummary>(data.Reverse());
        }




        //*************************************************************************

        public ObservableCollection<csSummary> OverAll(string dt1, string dt2, int idAgent, string IdSport)
        {
            ObservableCollection<csSummary> data = new ObservableCollection<csSummary>();
            int m = 0;
            try
            {
               parameters.Clear();
               parameters.Add("@pStartDate", dt1);
               parameters.Add("@pEndDate", dt2);
               parameters.Add("@pIdAgent", idAgent);
               parameters.Add("@pCase", 1);
               parameters.Add("@pIdSport", IdSport);
               dataset = csG8Apps.ExecutePA("[dbo].[web_reviewByDay]", parameters);               

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        if (!String.IsNullOrWhiteSpace(fila["NET"].ToString()) &&
                            !String.IsNullOrWhiteSpace(fila["RISK"].ToString()))
                        {
                            csSummary u = new csSummary(
                            Convert.ToInt32(fila["RISK"]),
                            Convert.ToInt32(fila["NET"]),
                            Convert.ToDateTime(fila["DAYY"]),
                            Convert.ToInt32(fila["PLAYERS"]));
                            data.Add(u);
                        }

                    }
                }


                parameters.Clear();
                parameters.Add("@pStartDate", dt1);
                parameters.Add("@pEndDate", dt2);
                parameters.Add("@pIdAgent", idAgent);
                parameters.Add("@pCase", 0);
                parameters.Add("@pIdSport", IdSport);
                dataset = csG8Apps.ExecutePA("[dbo].[web_reviewByDay]", parameters);
                

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {    
                        if(!String.IsNullOrWhiteSpace(fila["NET"].ToString()) &&
                           !String.IsNullOrWhiteSpace(fila["RISK"].ToString()))
                        {
                            csSummary u = new csSummary(
                            Convert.ToInt32(fila["NET"]),
                            Convert.ToInt32(fila["RISK"]),
                            Convert.ToInt32(fila["PLAYERS"]));
                            data.Add(u);
                        }
                        
                    }
                }


            }
            catch (Exception ex)
            {
                data = null;
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return new ObservableCollection<csSummary>(data.Reverse());
        }





        //*************************************************************************

        public ObservableCollection<csSummary> BusinessBySport(string dt1, string dt2, int idAgent, string sport)
        {
            ObservableCollection<csSummary> data = new ObservableCollection<csSummary>();

            try
            {

                parameters.Clear();
                parameters.Add("@pStartDate", dt1);
                parameters.Add("@pEndDate", dt2);
                parameters.Add("@pIdAgent", idAgent);
                parameters.Add("@pIdSport", sport);
                dataset = csG8Apps.ExecutePA("[dbo].[web_reviewBysPORT]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        csSummary u = new csSummary(
                        Convert.ToInt32(fila["RISK"]),
                        Convert.ToInt32(fila["NET"]),
                        Convert.ToString(fila["SPORT"]));
                        data.Add(u);
                    }
                }
                else
                {
                    data = null;
                }
            }
            catch (Exception ex)
            {
                data = null;
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return (data == null) ? null : new ObservableCollection<csSummary>(data.Reverse());
        }



        public csSummary InfoBySeasons(string dt1, string dt2, string idSport, string Player, string wagerType)
        {
            var data = new csSummary();

            try
            {
                parameters.Clear();
                parameters.Add("@pStartDate", dt1);
                parameters.Add("@pEndDate", dt2);
                parameters.Add("@pIdSport", idSport);
                parameters.Add("@pIdLeague", "-1");
                parameters.Add("@pPlayer", Player);
                parameters.Add("@pWagerType", wagerType.ToUpper());
                parameters.Add("@pWagerPlay", "");
                dataset = csG8Apps.ExecutePA("[dbo].[web_topPlayers]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        data = new csSummary(
                        Convert.ToString(fila["PLAYER"]),
                        Convert.ToString(fila["AGENT"]),
                        Convert.ToInt32(fila["RISK"]),
                        Convert.ToInt32(fila["NET"]),
                        Convert.ToInt32(fila["BETS"]),
                        Convert.ToInt32(fila["WINS"]));


                        data = TotalWagerType(dt1,dt2,idSport,Player,"STRAIGHT",data);
                        data = TotalWagerType(dt1, dt2, idSport, Player, "PARLAY", data);
                        data = TotalWagerType(dt1, dt2, idSport, Player, "TEASER", data);
                    }
                }
            }
            catch (Exception ex)
            {
                data = null;
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                parameters.Clear();
            }
            return data;
        }



        private csSummary TotalWagerType(string dt1, string dt2, string idSport, string Player, string wagerType, csSummary data)
        {
            try
            {
                parameters.Clear();
                parameters.Add("@pStartDate", dt1);
                parameters.Add("@pEndDate", dt2);
                parameters.Add("@pIdSport", idSport);
                parameters.Add("@pIdLeague", "-1");
                parameters.Add("@pPlayer", Player);
                parameters.Add("@pWagerType", wagerType.ToUpper());
                parameters.Add("@pWagerPlay", "");
                dataset = csG8Apps.ExecutePA("[dbo].[web_topPlayers]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        if (wagerType.ToUpper().Contains("STRAIGHT")) data.StraightNet = Convert.ToInt32(fila["NET"]);
                        else if (wagerType.ToUpper().Contains("PARLAY")) data.ParlayNet = Convert.ToInt32(fila["NET"]);
                        else if (wagerType.ToUpper().Contains("TEASER")) data.TeaserNet = Convert.ToInt32(fila["NET"]);
                    }
                }
            }
            catch (Exception ex)
            {
                data = null;
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                parameters.Clear();
            }
            return data;
        }





        public Object SumRangeDate(string dt1, string dt2, string idSport, string Player, string wagerType,int caso)
        {
            var data = new ObservableCollection<csSummary>();

            try
            {
                parameters.Clear();
                parameters.Add("@pStartDate", dt1);
                parameters.Add("@pEndDate", dt2);
                parameters.Add("@pIdSport", (idSport == "ALL") ? "" : idSport);
                parameters.Add("@pPlayer", Player);
                parameters.Add("@pCase", caso);
                parameters.Add("@pWagerType", wagerType.ToUpper());
                dataset = csG8Apps.ExecutePA("[dbo].[web_SummaryByRange]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        var s = new csSummary(
                        Convert.ToDateTime(fila["DAYY"]),
                        Convert.ToInt32(fila["RISK"]),
                        Convert.ToInt32(fila["NET"]));

                        data.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {
                data = null;
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return data;
        }









    }
}