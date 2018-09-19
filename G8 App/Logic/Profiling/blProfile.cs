using Data.Connection;
using G8_App.Connection;
using G8_App.Entities.Profiling;
using NHL_BL.Connection;
using NHL_BL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;

namespace G8_App.Logic.Profiling
{
    public class blProfile : csComponentsConnection
    {
        public blProfile() { }


        public ObservableCollection<csProfile> GetProfile(string d1, string d2, string idP)
        {
            ObservableCollection<csProfile> data = new ObservableCollection<csProfile>();
            csProfile profile = null;

            try
            {
                parameters.Clear();
                parameters.Add("@pStartDate",d1);
                parameters.Add("@pEndDate",d2);
                parameters.Add("@pIdPlayer",idP);
                dataset = csG8Apps.ExecutePA("[dbo].[web_ProfileByPlayer]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        profile = new csProfile();
                        profile.CountSP = Convert.ToInt32(fila["SP"]);
                        profile.CountML = Convert.ToInt32(fila["ML"]);
                        profile.CountDR = Convert.ToInt32(fila["DR"]);
                        profile.CountUNDER = Convert.ToInt32(fila["UND"]);
                        profile.CountOVER = Convert.ToInt32(fila["OVE"]);
                        profile.CountHome = Convert.ToInt32(fila["HOM"]);
                        profile.CountVisitor = Convert.ToInt32(fila["VIS"]);

                        profile.CountMorning = Convert.ToInt32(fila["MORNING"]);
                        profile.CountNoon = Convert.ToInt32(fila["NOON"]);
                        profile.CountNight = Convert.ToInt32(fila["NIGHT"]);
                        profile.CountOverNight = Convert.ToInt32(fila["OVERNIGHT"]);

                        profile.CountWins = Convert.ToInt32(fila["WINS"]);
                        profile.CountLoses = Convert.ToInt32(fila["LOSES"]);
                        profile.CountDraws = Convert.ToInt32(fila["DRAWS"]);

                        profile.CountFav = Convert.ToInt32(fila["FAV"]);
                        profile.CountDog = Convert.ToInt32(fila["DOG"]);

                        profile.CountLineMoved = Convert.ToInt32(fila["LMOVED"]);
                        profile.CountNoLineMoved = Convert.ToInt32(fila["LNOMOVED"]);

                        profile.Net = Convert.ToInt32(fila["NET"]);
                        profile.HoldPercentaje = Math.Round(Convert.ToDouble(fila["HOLD_PER"]),2,MidpointRounding.AwayFromZero);
                        profile.RiskAmount = Convert.ToInt32(fila["RISK"]);
                        profile.WinAmount = Convert.ToInt32(fila["WIN"]);

                        profile.CountLive = Convert.ToInt32(fila["LIVE"]);
                        profile.CountNoLive = Convert.ToInt32(fila["NOLIVE"]);

                        profile.CountBuy = Convert.ToInt32(fila["BUY"]);
                        profile.CountNoBuy = Convert.ToInt32(fila["NOBUY"]);

                        data.Add(profile);
                    }
                }
                else
                {
                    data = null;
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

            return data;
        }




        public ObservableCollection<csProfile> ProfileBySport(string d1, string d2, string idP, string sport)
        {
            ObservableCollection<csProfile> data = new ObservableCollection<csProfile>();
            csProfile profile = null;

            try
            {
                parameters.Clear();
                parameters.Add("@pStartDate", d1);
                parameters.Add("@pEndDate", d2);
                parameters.Add("@pPlayer", idP);

                dataset = csG8Apps.ExecutePA("[dbo].[web_profileBySport]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        profile = new csProfile();
                        profile.Sport = fila["IdSport"].ToString();
                        profile.RiskAmount = Convert.ToInt32(fila["RISK"]);
                        profile.Net = Convert.ToInt32(fila["NET"]);
                        profile.WinAmount = Convert.ToInt32(fila["WIN"]);
                        profile.Bets = Convert.ToInt32(fila["BETS"]);
                        profile.HoldPercentaje = Math.Round(Convert.ToDouble(fila["HOLD_PER"]),2,MidpointRounding.AwayFromZero);
                        profile.CountWins = Convert.ToInt32(fila["WINS"]);
                        profile.CountLoses = Convert.ToInt32(fila["LOSES"]);
                        profile.CountDraws = Convert.ToInt32(fila["DRAWS"]);
                        profile.WinPercentaje = Math.Round(Convert.ToDouble(fila["WIN_PER"]), 2, MidpointRounding.AwayFromZero);
                        profile.Scalping5Dimes = Convert.ToInt32(fila["Scalping5Dimes"]);
                        profile.ScalpingCris = Convert.ToInt32(fila["ScalpingCris"]);
                        profile.ScalpingJazz = Convert.ToInt32(fila["ScalpingJazz"]);
                        profile.ScalpingPinni = Convert.ToInt32(fila["ScalpingPinni"]);
                        profile.ScalpingPPH = Convert.ToInt32(fila["ScalpingPPH"]);
                        profile.MoveLine = Convert.ToInt32(fila["LineMover"]);
                        profile.BeatLine = Convert.ToInt32(fila["BeatLine"]);

                        if(profile.Sport.ToUpper().Trim().Contains(sport.ToUpper().Trim()))
                        {
                            data.Add(profile);
                        }
                        
                    }
                }
                else
                {
                    data = null;
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

            return data;
        }





        public ObservableCollection<csProfile> ProfileByLeague(string d1, string d2, string idP, string sport, string favdog, string league)
        {
            ObservableCollection<csProfile> data = new ObservableCollection<csProfile>();
            csProfile profile = null;

            try
            {
                parameters.Clear();
                parameters.Add("@pStartDate", d1);
                parameters.Add("@pEndDate", d2);
                parameters.Add("@pPlayer", idP);
                parameters.Add("@pSport", sport);
                parameters.Add("@pFavDog", favdog);
                dataset = csG8Apps.ExecutePA("[dbo].[web_profileByLeague]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        profile = new csProfile();
                        profile.League = fila["League"].ToString();
                        profile.RiskAmount = Convert.ToInt32(fila["RISK"]);
                        profile.Net = Convert.ToInt32(fila["NET"]);
                        profile.WinAmount = Convert.ToInt32(fila["WIN"]);
                        profile.Bets = Convert.ToInt32(fila["BETS"]);
                        profile.HoldPercentaje = Math.Round(Convert.ToDouble(fila["HOLD_PER"]), 2, MidpointRounding.AwayFromZero);
                        profile.CountWins = Convert.ToInt32(fila["WINS"]);
                        profile.CountLoses = Convert.ToInt32(fila["LOSES"]);
                        profile.CountDraws = Convert.ToInt32(fila["DRAWS"]);
                        profile.WinPercentaje = Math.Round(Convert.ToDouble(fila["WIN_PER"]), 2, MidpointRounding.AwayFromZero);
                        profile.Scalping5Dimes = Convert.ToInt32(fila["Scalping5Dimes"]);
                        profile.ScalpingCris = Convert.ToInt32(fila["ScalpingCris"]);
                        profile.ScalpingJazz = Convert.ToInt32(fila["ScalpingJazz"]);
                        profile.ScalpingPinni = Convert.ToInt32(fila["ScalpingPinni"]);
                        profile.ScalpingPPH = Convert.ToInt32(fila["ScalpingPPH"]);
                        profile.MoveLine = Convert.ToInt32(fila["LineMover"]);
                        profile.BeatLine = Convert.ToInt32(fila["BeatLine"]);


                        if (profile.League.ToUpper().Trim().Contains(league.ToUpper().Trim()))
                        {
                            data.Add(profile);
                        }
                    }
                }
                else
                {
                    data = null;
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

            return data;
        }





        public ObservableCollection<csProfile> ProfileByPeriod(string d1, string d2, string idP, string sport, string league, string favdog)
        {
            ObservableCollection<csProfile> data = new ObservableCollection<csProfile>();
            csProfile profile = null;

            try
            {
                parameters.Clear();
                parameters.Add("@pStartDate", d1);
                parameters.Add("@pEndDate", d2);
                parameters.Add("@pPlayer", idP);
                parameters.Add("@pSport", sport);
                parameters.Add("@pLeague", Convert.ToInt32(league));
                parameters.Add("@pFavDog", favdog);
                dataset = csG8Apps.ExecutePA("[dbo].[web_profileByPeriod]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        profile = new csProfile();
                        profile.GamePeriod = fila["GamePeriod"].ToString();
                        profile.RiskAmount = Convert.ToInt32(fila["RISK"]);
                        profile.Net = Convert.ToInt32(fila["NET"]);
                        profile.WinAmount = Convert.ToInt32(fila["WIN"]);
                        profile.Bets = Convert.ToInt32(fila["BETS"]);
                        profile.HoldPercentaje = Math.Round(Convert.ToDouble(fila["HOLD_PER"]), 2, MidpointRounding.AwayFromZero);
                        profile.CountWins = Convert.ToInt32(fila["WINS"]);
                        profile.CountLoses = Convert.ToInt32(fila["LOSES"]);
                        profile.CountDraws = Convert.ToInt32(fila["DRAWS"]);
                        profile.WinPercentaje = Math.Round(Convert.ToDouble(fila["WIN_PER"]), 2, MidpointRounding.AwayFromZero);
                        profile.Scalping5Dimes = Convert.ToInt32(fila["Scalping5Dimes"]);
                        profile.ScalpingCris = Convert.ToInt32(fila["ScalpingCris"]);
                        profile.ScalpingJazz = Convert.ToInt32(fila["ScalpingJazz"]);
                        profile.ScalpingPinni = Convert.ToInt32(fila["ScalpingPinni"]);
                        profile.ScalpingPPH = Convert.ToInt32(fila["ScalpingPPH"]);
                        profile.MoveLine = Convert.ToInt32(fila["LineMover"]);
                        profile.BeatLine = Convert.ToInt32(fila["BeatLine"]);
                        data.Add(profile);
                    }
                }
                else
                {
                    data = null;
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
            return data;
        }





        public ObservableCollection<csProfile> ProfileBySide(string d1, string d2, string idP,string sport, string league, string favdog)
        {
            ObservableCollection<csProfile> data = new ObservableCollection<csProfile>();
            csProfile profile = null;

            try
            {
                parameters.Clear();
                parameters.Add("@pStartDate", d1);
                parameters.Add("@pEndDate", d2);
                parameters.Add("@pPlayer", idP);
                parameters.Add("@pSport", sport);
                parameters.Add("@pLeague", league);
                parameters.Add("@pFavDog", favdog);
                dataset = csG8Apps.ExecutePA("[dbo].[web_profileByWagerSide]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        profile = new csProfile();
                        profile.Side = CastSide(fila["PickSide"].ToString());
                        profile.RiskAmount = Convert.ToInt32(fila["RISK"]);
                        profile.Net = Convert.ToInt32(fila["NET"]);
                        profile.WinAmount = Convert.ToInt32(fila["WIN"]);
                        profile.Bets = Convert.ToInt32(fila["BETS"]);
                        profile.HoldPercentaje = Math.Round(Convert.ToDouble(fila["HOLD_PER"]), 2, MidpointRounding.AwayFromZero);
                        profile.CountWins = Convert.ToInt32(fila["WINS"]);
                        profile.CountLoses = Convert.ToInt32(fila["LOSES"]);
                        profile.CountDraws = Convert.ToInt32(fila["DRAWS"]);
                        profile.WinPercentaje = Math.Round(Convert.ToDouble(fila["WIN_PER"]), 2, MidpointRounding.AwayFromZero);
                        profile.Scalping5Dimes = Convert.ToInt32(fila["Scalping5Dimes"]);
                        profile.ScalpingCris = Convert.ToInt32(fila["ScalpingCris"]);
                        profile.ScalpingJazz = Convert.ToInt32(fila["ScalpingJazz"]);
                        profile.ScalpingPinni = Convert.ToInt32(fila["ScalpingPinni"]);
                        profile.ScalpingPPH = Convert.ToInt32(fila["ScalpingPPH"]);
                        profile.MoveLine = Convert.ToInt32(fila["LineMover"]);
                        profile.BeatLine = Convert.ToInt32(fila["BeatLine"]);
                        data.Add(profile);
                    }
                }
                else
                {
                    data = null;
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

            return data;
        }



        public ObservableCollection<csProfile> ProfileByWagerType(string d1, string d2, string idP,string sport, string league,string favdog)
        {
            ObservableCollection<csProfile> data = new ObservableCollection<csProfile>();
            csProfile profile = null;

            try
            {
                parameters.Clear();
                parameters.Add("@pStartDate", d1);
                parameters.Add("@pEndDate", d2);
                parameters.Add("@pPlayer", idP);
                parameters.Add("@pSport", sport);
                parameters.Add("@pLeague", league);
                parameters.Add("@pFavDog", favdog);
                dataset = csG8Apps.ExecutePA("[dbo].[web_profileByWagerType]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        profile = new csProfile();
                        profile.Side = CastSide(fila["PickSide"].ToString());
                        profile.WagerType = CastWagerType(fila["WagerPlay"].ToString());
                        profile.RiskAmount = Convert.ToInt32(fila["RISK"]);
                        profile.Net = Convert.ToInt32(fila["NET"]);
                        profile.WinAmount = Convert.ToInt32(fila["WIN"]);
                        profile.Bets = Convert.ToInt32(fila["BETS"]);
                        profile.HoldPercentaje = Math.Round(Convert.ToDouble(fila["HOLD_PER"]), 2, MidpointRounding.AwayFromZero);
                        profile.CountWins = Convert.ToInt32(fila["WINS"]);
                        profile.CountLoses = Convert.ToInt32(fila["LOSES"]);
                        profile.CountDraws = Convert.ToInt32(fila["DRAWS"]);
                        profile.WinPercentaje = Math.Round(Convert.ToDouble(fila["WIN_PER"]), 2, MidpointRounding.AwayFromZero);
                        profile.Scalping5Dimes = Convert.ToInt32(fila["Scalping5Dimes"]);
                        profile.ScalpingCris = Convert.ToInt32(fila["ScalpingCris"]);
                        profile.ScalpingJazz = Convert.ToInt32(fila["ScalpingJazz"]);
                        profile.ScalpingPinni = Convert.ToInt32(fila["ScalpingPinni"]);
                        profile.ScalpingPPH = Convert.ToInt32(fila["ScalpingPPH"]);
                        profile.MoveLine = Convert.ToInt32(fila["LineMover"]);
                        profile.BeatLine = Convert.ToInt32(fila["BeatLine"]);
                        data.Add(profile);
                    }
                }
                else
                {
                    data = null;
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

            return data;
        }





        public ObservableCollection<csProfile> ProfileByWeek(string d1, string d2, string idP, string sport, string league)
        {
            ObservableCollection<csProfile> data = new ObservableCollection<csProfile>();
            csProfile profile = null;

            try
            {
                parameters.Clear();
                parameters.Add("@pStartDate", d1);
                parameters.Add("@pEndDate", d2);
                parameters.Add("@pPlayer", idP);
                parameters.Add("@pSport", sport);
                parameters.Add("@pLeague", league);
                dataset = csG8Apps.ExecutePA("[dbo].[web_profileByWeek]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        profile = new csProfile();
                        profile.Day = CastWeekDay(Convert.ToInt32(fila["WeekDay"].ToString()));
                        profile.RiskAmount = Convert.ToInt32(fila["RISK"]);
                        profile.Net = Convert.ToInt32(fila["NET"]);
                        profile.WinAmount = Convert.ToInt32(fila["WIN"]);
                        profile.Bets = Convert.ToInt32(fila["BETS"]);
                        profile.HoldPercentaje = Math.Round(Convert.ToDouble(fila["HOLD_PER"]), 2, MidpointRounding.AwayFromZero);
                        profile.CountWins = Convert.ToInt32(fila["WINS"]);
                        profile.CountLoses = Convert.ToInt32(fila["LOSES"]);
                        profile.CountDraws = Convert.ToInt32(fila["DRAWS"]);
                        profile.WinPercentaje = Math.Round(Convert.ToDouble(fila["WIN_PER"]), 2, MidpointRounding.AwayFromZero);
                        profile.Scalping5Dimes = Convert.ToInt32(fila["Scalping5Dimes"]);
                        profile.ScalpingCris = Convert.ToInt32(fila["ScalpingCris"]);
                        profile.ScalpingJazz = Convert.ToInt32(fila["ScalpingJazz"]);
                        profile.ScalpingPinni = Convert.ToInt32(fila["ScalpingPinni"]);
                        profile.ScalpingPPH = Convert.ToInt32(fila["ScalpingPPH"]);
                        profile.MoveLine = Convert.ToInt32(fila["LineMover"]);
                        profile.BeatLine = Convert.ToInt32(fila["BeatLine"]);
                        data.Add(profile);
                    }
                }
                else
                {
                    data = null;
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

            return data;
        }






        public ObservableCollection<csProfile> ProfileByFavDog(string d1, string d2, string idP, string sport, string league)
        {
            ObservableCollection<csProfile> data = new ObservableCollection<csProfile>();
            csProfile profile = null;

            try
            {
                parameters.Clear();
                parameters.Add("@pStartDate", d1);
                parameters.Add("@pEndDate", d2);
                parameters.Add("@pPlayer", idP);
                parameters.Add("@pSport", sport);
                parameters.Add("@pLeague", league);
                dataset = csG8Apps.ExecutePA("[dbo].[web_profileByFavDog]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        profile = new csProfile();
                        profile.FavDog = fila["PickFavDog"].ToString();
                        profile.RiskAmount = Convert.ToInt32(fila["RISK"]);
                        profile.Net = Convert.ToInt32(fila["NET"]);
                        profile.WinAmount = Convert.ToInt32(fila["WIN"]);
                        profile.Bets = Convert.ToInt32(fila["BETS"]);
                        profile.HoldPercentaje = Math.Round(Convert.ToDouble(fila["HOLD_PER"]), 2, MidpointRounding.AwayFromZero);
                        profile.CountWins = Convert.ToInt32(fila["WINS"]);
                        profile.CountLoses = Convert.ToInt32(fila["LOSES"]);
                        profile.CountDraws = Convert.ToInt32(fila["DRAWS"]);
                        profile.WinPercentaje = Math.Round(Convert.ToDouble(fila["WIN_PER"]), 2, MidpointRounding.AwayFromZero);
                        profile.Scalping5Dimes = Convert.ToInt32(fila["Scalping5Dimes"]);
                        profile.ScalpingCris = Convert.ToInt32(fila["ScalpingCris"]);
                        profile.ScalpingJazz = Convert.ToInt32(fila["ScalpingJazz"]);
                        profile.ScalpingPinni = Convert.ToInt32(fila["ScalpingPinni"]);
                        profile.ScalpingPPH = Convert.ToInt32(fila["ScalpingPPH"]);
                        profile.MoveLine = Convert.ToInt32(fila["LineMover"]);
                        profile.BeatLine = Convert.ToInt32(fila["BeatLine"]);
                        data.Add(profile);
                    }
                }
                else
                {
                    data = null;
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

            return data;
        }





        public ObservableCollection<csProfile> ProfileByMomentDay(string d1, string d2, string idP, string sport, string league)
        {
            ObservableCollection<csProfile> data = new ObservableCollection<csProfile>();
            csProfile profile = null;

            try
            {
                parameters.Clear();
                parameters.Add("@pStartDate", d1);
                parameters.Add("@pEndDate", d2);
                parameters.Add("@pPlayer", idP);
                parameters.Add("@pSport", sport);
                parameters.Add("@pLeague", league);
                dataset = csG8Apps.ExecutePA("[dbo].[web_profileByMomentDay]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        profile = new csProfile();
                        profile.MomentDay = fila["MomentDay"].ToString();
                        profile.RiskAmount = Convert.ToInt32(fila["RISK"]);
                        profile.Net = Convert.ToInt32(fila["NET"]);
                        profile.WinAmount = Convert.ToInt32(fila["WIN"]);
                        profile.Bets = Convert.ToInt32(fila["BETS"]);
                        profile.HoldPercentaje = Math.Round(Convert.ToDouble(fila["HOLD_PER"]), 2, MidpointRounding.AwayFromZero);
                        profile.CountWins = Convert.ToInt32(fila["WINS"]);
                        profile.CountLoses = Convert.ToInt32(fila["LOSES"]);
                        profile.CountDraws = Convert.ToInt32(fila["DRAWS"]);
                        profile.WinPercentaje = Math.Round(Convert.ToDouble(fila["WIN_PER"]), 2, MidpointRounding.AwayFromZero);
                        profile.Scalping5Dimes = Convert.ToInt32(fila["Scalping5Dimes"]);
                        profile.ScalpingCris = Convert.ToInt32(fila["ScalpingCris"]);
                        profile.ScalpingJazz = Convert.ToInt32(fila["ScalpingJazz"]);
                        profile.ScalpingPinni = Convert.ToInt32(fila["ScalpingPinni"]);
                        profile.ScalpingPPH = Convert.ToInt32(fila["ScalpingPPH"]);
                        profile.MoveLine = Convert.ToInt32(fila["LineMover"]);
                        profile.BeatLine = Convert.ToInt32(fila["BeatLine"]);
                        data.Add(profile);
                    }
                }
                else
                {
                    data = null;
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

            return data;
        }





        public ObservableCollection<csProfile> ProfileByTeam(string d1, string d2, string idP, string sport, string league)
        {
            ObservableCollection<csProfile> data = new ObservableCollection<csProfile>();
            csProfile profile = null;

            try
            {
                parameters.Clear();
                parameters.Add("@pStartDate", d1);
                parameters.Add("@pEndDate", d2);
                parameters.Add("@pPlayer", idP);
                parameters.Add("@pSport", sport);
                parameters.Add("@pLeague", league);
                dataset = csG8Apps.ExecutePA("[dbo].[web_profileByTeam]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        profile = new csProfile();
                        profile.Team = fila["PickTeam"].ToString();
                        profile.Sport = fila["IdSport"].ToString();
                        profile.RiskAmount = Convert.ToInt32(fila["RISK"]);
                        profile.Net = Convert.ToInt32(fila["NET"]);
                        profile.WinAmount = Convert.ToInt32(fila["WIN"]);
                        profile.Bets = Convert.ToInt32(fila["BETS"]);
                        profile.HoldPercentaje = Math.Round(Convert.ToDouble(fila["HOLD_PER"]), 2, MidpointRounding.AwayFromZero);
                        profile.CountWins = Convert.ToInt32(fila["WINS"]);
                        profile.CountLoses = Convert.ToInt32(fila["LOSES"]);
                        profile.CountDraws = Convert.ToInt32(fila["DRAWS"]);
                        profile.WinPercentaje = Math.Round(Convert.ToDouble(fila["WIN_PER"]), 2, MidpointRounding.AwayFromZero);
                        profile.Scalping5Dimes = Convert.ToInt32(fila["Scalping5Dimes"]);
                        profile.ScalpingCris = Convert.ToInt32(fila["ScalpingCris"]);
                        profile.ScalpingJazz = Convert.ToInt32(fila["ScalpingJazz"]);
                        profile.ScalpingPinni = Convert.ToInt32(fila["ScalpingPinni"]);
                        profile.ScalpingPPH = Convert.ToInt32(fila["ScalpingPPH"]);
                        profile.MoveLine = Convert.ToInt32(fila["LineMover"]);
                        profile.BeatLine = Convert.ToInt32(fila["BeatLine"]);
                        data.Add(profile);
                    }
                }
                else
                {
                    data = null;
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

            return data;
        }



        public ObservableCollection<csProfile> ProfileByBets(string d1, string d2, string idP, string sport, string league, string favdog, string wagerplay)
        {
            ObservableCollection<csProfile> data = new ObservableCollection<csProfile>();
            csProfile profile = null;

            try
            {
                parameters.Clear();
                parameters.Add("@pStartDate", d1);
                parameters.Add("@pEndDate", d2);
                parameters.Add("@pPlayer", idP);
                parameters.Add("@pSport", sport);
                parameters.Add("@pLeague", league);
                parameters.Add("@pFavDog", favdog);
                parameters.Add("@pWagerPlay", wagerplay);
                dataset = csG8Apps.ExecutePA("[dbo].[web_profileByBets]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        profile = new csProfile();
                        profile.Net = Convert.ToInt32(fila["Net"]);
                        profile.PlacedDateString = CastDate(Convert.ToDateTime(fila["PlacedDate"]));
                        profile.RiskAmount = Convert.ToInt32(fila["RiskAmount"]);
                        profile.Points = Convert.ToDouble(fila["Points"]);
                        profile.Odds = Convert.ToInt32(fila["Odds"]);
                        profile.EventDateString = CastDate(Convert.ToDateTime(fila["EventDate"]));
                        profile.HomeTem = Convert.ToString(fila["HomeTeam"]);
                        profile.VisitorTem = Convert.ToString(fila["VisitorTeam"]);
                        profile.Sport = Convert.ToString(fila["IdSport"]);
                        profile.League = Convert.ToString(fila["League"]);
                        profile.WagerType = Convert.ToString(fila["WagerPlay"]);
                        profile.FavDog = Convert.ToString(fila["PickFavDog"]);
                        profile.DetailDescription = Convert.ToString(fila["DetailDescription"]);
                        data.Add(profile);
                    }
                }
                else
                {
                    data = null;
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

            return data;
        }



        public ObservableCollection<csSport> GetSportsByPlayer()
        {
            ObservableCollection<csSport> data = new ObservableCollection<csSport>();
            try
            {
                parameters.Clear();
                dataset = csG8Apps.ExecutePA("[dbo].[web_SportsByPlayer]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    data.Add(new csSport("", "ALL"));
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        data.Add(new csSport(fila["IdSport"].ToString(), fila["IdSport"].ToString()));
                    }
                }
                else
                {
                    data = null;
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

            return data;
        }



        public ObservableCollection<csProfile> BreakDownPlayer(string d1, string d2, string sport, string league, string player, string wagerPlay, string wagerType)
        {
            ObservableCollection<csProfile> data = new ObservableCollection<csProfile>();
            csProfile p = null;

            try
            {
                    parameters.Clear();
                    parameters.Add("@LogIdUser", 74);
                    parameters.Add("@prmStartDate", d1);
                    parameters.Add("@prmEndDate", d2);
                    parameters.Add("@prmBook", "");
                    parameters.Add("@prmOffice", "");
                    parameters.Add("@prmPlayer", player);
                    parameters.Add("@prmLeague", (league == "-1") ? "" : league);
                    parameters.Add("@prmGroupby", 0);
                    parameters.Add("@prmOrderby", 0);
                    dataset = csConnection.ExecutePA("[dbo].[Report_Game_Statistic]", parameters);

                    if (dataset.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in dataset.Tables[0].Rows)
                        {
                            p = new csProfile();
                            p.Sport = fila["Group1"].ToString();
                            p.WagerType = fila["Group2"].ToString();
                            p.GamePeriod = fila["Group3"].ToString();
                            p.WagerPlay = fila["Group4"].ToString();
                            p.RiskAmount = Convert.ToInt32(fila["Amount"]);
                            //p.Bets = Convert.ToInt32(fila["BETS"]); 
                            p.Net = Convert.ToInt32(fila["WinLost"]);
                            p.HoldPercentaje = Math.Round(Convert.ToDouble((p.Net * 100) / p.RiskAmount), 2, MidpointRounding.AwayFromZero);

                            if (p.WagerType.ToUpper().Contains(wagerType.ToUpper()))
                            data.Add(p);
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

            return data;
        }










        private String CastDate(DateTime dt)
        {
            return dt.Year + "/" + dt.Month + "/" + dt.Day;
        }






        private string CastSide(string side)
        {
            if (side == "O") return "Total Over";
            else if (side == "U") return "Total Under";
            else if (side == "D") return "Draw";
            else if (side == "H") return "Home";
            else if (side == "V") return "Visitor";

            return "";
        }


        private string CastWagerType(string w)
        {
            if (w == "DR") return "Draw";
            else if (w == "ML") return "Money Line";
            else if (w == "SP") return "Spread";
            else if (w == "TOT") return "Total";
            return "";
        }



        private string CastWeekDay(int d)
        {
            if (d == 0) return "Sunday";
            else if (d == 1) return "Monday";
            else if (d == 2) return "Tuesday";
            else if (d == 3) return "Wednesday";
            else if (d == 4) return "Thursday";
            else if (d == 5) return "Friday";
            else if (d == 6) return "Saturday";
            return "";
        }



    }
}