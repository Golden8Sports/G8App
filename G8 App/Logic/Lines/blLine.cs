using Data.Connection;
using G8_App.Connection;
using G8_App.Entities.Lines;
using Newtonsoft.Json;
using NHL_BL.Connection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace G8_App.Logic.Lines
{
    public class blLine : csComponentsConnection
    {

        public ObservableCollection<csLine> BetByGame(int idGame, int period, string s, string type)
        {
            ObservableCollection<csLine> data = new ObservableCollection<csLine>();
            ObservableCollection<csLine> BetListAux = new ObservableCollection<csLine>();

            csLine line = null;
            string side = ((type == "MONEY" || type == "SPREAD") && s == "V") ? "VISITOR" : "NONE";
            side = ((type == "MONEY" || type == "SPREAD") && s == "H") ? "HOME" : side;
            side = ((type == "TOTAL") && s == "V") ? "OVER" : side;
            side = ((type == "TOTAL") && s == "H") ? "UNDER" : side;
            side = (type == "DRAW") ? "DRAW" : side;

            int play = 0;

            if(type == "SPREAD")
            {
                if (s == "V") play = 0;
                else play = 1;
            }else if(type == "MONEY")
            {
                if (s == "V") play = 4;
                else play = 5;
            }
            else if (type == "TOTAL")
            {
                if (s == "V") play = 2;
                else play = 3;
            }else
            {
                play = 6;
            }

            try
            {
                parameters.Clear();
                parameters.Add("@pIdGame", idGame);
                parameters.Add("@pPlay", play);
                dataset = csG8Apps.ExecutePA("[dbo].[web_betsByGame]", parameters); ;

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (System.Data.DataRow fila in dataset.Tables[0].Rows)
                    {
                        line = new csLine();
                        line.Date = Convert.ToDateTime(fila["PlacedDate"]);
                        line.Juice = Convert.ToInt32(fila["Odds"]);
                        line.Line = Convert.ToDouble(fila["Points"]);
                        line.IdWager = Convert.ToInt32(fila["IdWager"]);
                        line.Casino = -1;
                        line.Player = fila["Player"].ToString();
                        line.WagerPlay = fila["WagerPlay"].ToString().ToUpper();                       
                        line.Risk = Convert.ToDouble(fila["RiskAmount"]); 
                        line.WagerType = fila["WAGERTYPE"].ToString();                      
                        line.Time = CastTime(line.Date);
                        if (line.WagerPlay.Contains("TOTAL")) line.Line = Math.Abs(Convert.ToInt32(line.Line));
                        line = CastDate(line);
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
                                    Enteros.Add(Convert.ToInt32(i.IdWager));
                                    BetListAux.Add(i);
                                }
                            }
                            else
                            {
                                BetListAux.Add(i);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                data = null;
                throw new Exception(ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return BetListAux;
        }

        public ObservableCollection<csLine> GetBetPlayer(int idWager, string player, string userplay, string s, string type)
        {
            ObservableCollection<csLine> data = new ObservableCollection<csLine>();
            csLine line = null;
            string side = (userplay.ToUpper().ToUpper().Contains("VISI") && s == "V") ? "VISITOR" : "NONE";
            side = (userplay.ToUpper().Contains("HOME") && s == "H") ? "HOME" : side;
            side = (userplay.ToUpper().Contains("OVER")) && s == "V" ? "OVER" : side;
            side = (userplay.ToUpper().Contains("UNDER") && s == "H") ? "UNDER" : side;
            side = (userplay.ToUpper().Contains("DRAW")) ? "DRAW" : side;

            try
            {
                parameters.Clear();
                parameters.Add("@pPlayer", player);
                parameters.Add("@pIdWager", idWager);
                dataset = csG8Apps.ExecutePA("[dbo].[web_betIdWager]", parameters); ;

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (System.Data.DataRow fila in dataset.Tables[0].Rows)
                    {
                        line = new csLine();
                        line.Juice = Convert.ToInt32(fila["Odds"]);
                        line.Line = Convert.ToDouble(fila["Points"]);
                        line.Casino = -1;
                        line.WagerPlay = fila["WagerPlay"].ToString().ToUpper();
                        if (line.WagerPlay.Contains("TOTAL")) line.Line = Math.Abs(Convert.ToInt32(line.Line));
                        line.Date = Convert.ToDateTime(fila["PlacedDate"]);
                        line = CastDate(line);
                        line.Time = CastTime(line.Date);

                        if (userplay.ToUpper() == line.WagerPlay && line.WagerPlay.Contains(side) &&
                           line.WagerPlay.Contains(type))
                        data.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                data = null;
                throw new Exception(ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return data;
        }


        public Object GetMoneyLines(int IdEvent, string period, string side,string player,int idwager, string userplay,int idGame, int idPeriod)
        {
            var data = new ObservableCollection<csLine>();
            csLine line = null;

            try
                {
                parameters.Clear();
                parameters.Add("@pIdEvent",IdEvent);
                parameters.Add("@pPeriod", period);
                dataset = csDonBest.ExecutePA("[dbo].[web_getMLForEvent]", parameters); ;

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (System.Data.DataRow fila in dataset.Tables[0].Rows)
                    {
                        line = new csLine();
                        line.Casino = Convert.ToInt32(fila["sportsbook"]);
                        line.Date = Convert.ToDateTime(fila["timeReceived"]);
                        line = CastDate(line);
                        line.Time = CastTime(line.Date);

                        if (side == "V")
                        {
                            line.Juice = Convert.ToInt32(fila["ml_away_price"]);
                            data.Add(line);
                        }
                        else if (side == "H")
                        {
                            line.Juice = Convert.ToInt32(fila["ml_home_price"]);
                            data.Add(line);
                        }
                    }

                    if(player != "")
                    {
                        var list = GetBetPlayer(idwager, player, userplay, side, "MONEY");
                        if (list != null && list.Count > 0)
                        {
                            foreach (var i in list)
                            {
                                data.Add(i);
                            }
                        }
                    }else
                    {
                        var list = BetByGame(idGame, idPeriod, side, "MONEY");
                        if (list != null && list.Count > 0)
                        {
                            foreach (var i in list)
                            {
                                data.Add(i);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                data = null;
                throw new Exception(ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return data;
        }





        public ObservableCollection<csLine> GetDraws(int IdEvent, string period, string player, int idwager, string userplay, int idGame, int idPeriod)
        {
            ObservableCollection<csLine> data = new ObservableCollection<csLine>();
            csLine line = null;

            try
            {
                parameters.Clear();
                parameters.Add("@pIdEvent", IdEvent);
                parameters.Add("@pPeriod", period);
                dataset = csDonBest.ExecutePA("[dbo].[web_getDRForEvent]", parameters); ;

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (System.Data.DataRow fila in dataset.Tables[0].Rows)
                    {
                        line = new csLine();
                        line.Casino = Convert.ToInt32(fila["sportsbook"]);
                        line.Juice = Convert.ToInt32(fila["draw_price"]);
                        line.Date = Convert.ToDateTime(fila["timeReceived"]);
                        line = CastDate(line);
                        line.Time = CastTime(line.Date);
                        data.Add(line);
                    }


                    if(player != "")
                    {
                        var list = GetBetPlayer(idwager, player, userplay, "DRAW", "DRAW");
                        if (list != null && list.Count > 0)
                        {
                            foreach (var i in list)
                            {
                                data.Add(i);
                            }
                        }
                    }else
                    {
                        var list = BetByGame(idGame, idPeriod, "DRAW", "DRAW");
                        if (list != null && list.Count > 0)
                        {
                            foreach (var i in list)
                            {
                                data.Add(i);
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                data = null;
                throw new Exception(ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return data;
        }





        public Object GetTotals(int IdEvent, string period, string side, string player, int idwager, string userplay, int idGame, int idPeriod)
        {
            ObservableCollection<csLine> data = new ObservableCollection<csLine>();
            csLine line = null;

            try
            {
                parameters.Clear();
                parameters.Add("@pIdEvent", IdEvent);
                parameters.Add("@pPeriod", period);
                dataset = csDonBest.ExecutePA("[dbo].[web_getTOTForEvent]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (System.Data.DataRow fila in dataset.Tables[0].Rows)
                    {
                        line = new csLine();
                        line.Casino = Convert.ToInt32(fila["sportsbook"]);
                        line.Date = Convert.ToDateTime(fila["timeReceived"]);
                        line = CastDate(line);
                        line.Time = CastTime(line.Date);


                        if (side == "V") //over
                        {
                            line.Line = Math.Abs(Convert.ToDouble(fila["total"]));
                            line.Juice = Convert.ToInt32(fila["over_price"]);
                            data.Add(line);
                        }
                        else if(side == "H")
                        {
                            line.Line = Math.Abs(Convert.ToDouble(fila["total"]));
                            line.Juice = Convert.ToInt32(fila["under_price"]);
                            data.Add(line);
                        }

                        
                    }

                    if(player != "")
                    {
                        var list = GetBetPlayer(idwager, player, userplay, side, "TOTAL");
                        if (list != null && list.Count > 0)
                        {
                            foreach (var i in list)
                            {
                                data.Add(i);
                            }
                        }
                    }else
                    {
                        var list = BetByGame(idGame, idPeriod, side, "TOTAL");
                        if (list != null && list.Count > 0)
                        {
                            foreach (var i in list)
                            {
                                data.Add(i);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                data = null;
                throw new Exception(ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return data;
        }





        public ObservableCollection<csLine> GetSP(int IdEvent, string period, string side, string player, int idwager, string userplay, int idGame, int idPeriod)
        {
            ObservableCollection<csLine> data = new ObservableCollection<csLine>();
            csLine line = null;

            try
            {
                parameters.Clear();
                parameters.Add("@pIdEvent", IdEvent);
                parameters.Add("@pPeriod", period);
                dataset = csDonBest.ExecutePA("[dbo].[web_getSPForEvent]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (System.Data.DataRow fila in dataset.Tables[0].Rows)
                    {
                        line = new csLine(); 
                        line.Casino = Convert.ToInt32(fila["sportsbook"]);
                        line.Date = Convert.ToDateTime(fila["timeReceived"]);
                        line = CastDate(line);
                        line.Time = CastTime(line.Date);

                        if (side == "H")
                        {
                            line.Juice = Convert.ToInt32(fila["ps_home_price"]);
                            line.Line = Convert.ToDouble(fila["ps_home_spread"]);
                            data.Add(line);
                        }
                        else if (side == "V")
                        {
                            line.Juice = Convert.ToInt32(fila["ps_away_price"]);
                            line.Line = Convert.ToDouble(fila["ps_away_spread"]);
                            data.Add(line);
                        }                      
                    }

                    if(player != "")
                    {
                        var list = GetBetPlayer(idwager, player, userplay, side, "SPREAD");
                        if (list != null && list.Count > 0)
                        {
                            foreach (var i in list)
                            {
                                data.Add(i);
                            }
                        }
                    }else
                    {
                        var list = BetByGame(idGame, idPeriod, side, "SPREAD");
                        if (list != null && list.Count > 0)
                        {
                            foreach (var i in list)
                            {
                                data.Add(i);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                data = null;
                throw new Exception(ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return data;
        }



        private csLine CastDate(csLine l)
        {
            l.Year = l.Date.Year;
            l.Month = (l.Date.Month - 1);
            l.Day = l.Date.Day;
            l.Hour = l.Date.Hour;
            l.Minute = l.Date.Minute;
            l.Second = l.Date.Second;
            return l;
        }


        private string CastTime(DateTime dt)
        {
            return dt.ToString("hh:mm:ss tt");
        }

    }
}