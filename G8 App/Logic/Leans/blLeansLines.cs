﻿using Data.Connection;
using G8_App.Connection;
using NHL_BL.Connection;
using NHL_BL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NHL_BL.Logic
{
    public class blLeansLines : csComponentsConnection
    {
        public blLeansLines() { }


        public csBet CrisLines(csBet b, csGame g, int sportbook)
        {

            try
            {
                parameters.Add("@pDate", b.PlacedDate);
                parameters.Add("@pRot", g.HomeNumber);
                parameters.Add("@pIdSportbook", sportbook);
                parameters.Add("@pPeriod", b.GamePeriod);
                parameters.Add("@pRot2", g.VisitorNumber);
                parameters.Add("@pWagerPlay", b.WagerPlay);
                parameters.Add("@pIdSport", b.IdSportDonBest);
                parameters.Add("@pDateEvent", g.EventDate);
                parameters.Add("@pPoints", b.Points);
                parameters.Add("@pOdds", b.Odds);

                dataset = csDonBest.ExecutePA("[dbo].[spGetLeansLines]", parameters);
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {

                        if (b.VisHome == "H") // HOME
                        {
                            if (b.WagerPlay == "SP")
                            {
                                try { b.CrisPoints = Convert.ToDouble(fila["ps_home_spread"]); } catch (Exception) { b.CrisPoints = 0; }
                                try { b.CrisJuice = Convert.ToInt32(fila["ps_home_price"]); } catch (Exception) { b.CrisJuice = 0; }
                            }
                            else if (b.WagerPlay == "ML" || b.WagerPlay == "DR")
                            {
                                b.CrisPoints = 0;
                                try { b.CrisJuice = Convert.ToInt32(fila["ml_home_price"]); } catch (Exception) { b.CrisJuice = 0; }

                            } else if(b.WagerPlay == "TOT")
                            {
                                if (b.OverUnder == "u")
                                {
                                    try { b.CrisPoints = Convert.ToDouble(fila["total"]); } catch (Exception) { b.CrisPoints = 0; }
                                    try { b.CrisJuice = Convert.ToInt32(fila["under_price"]); } catch (Exception) { b.CrisJuice = 0; }
                                }
                                else if (b.OverUnder == "o")
                                {
                                    try { b.CrisPoints = Convert.ToDouble(fila["total"]); } catch (Exception) { b.CrisPoints = 0; }
                                    try { b.CrisJuice = Convert.ToInt32(fila["over_price"]); } catch (Exception) { b.CrisJuice = 0; }
                                }
                            }

                        }
                        else //VISITOR
                        {
                            if (b.WagerPlay == "SP")
                            {
                                try { b.CrisPoints = Convert.ToDouble(fila["ps_away_spread"]); } catch (Exception) { b.CrisPoints = 0; }
                                try { b.CrisJuice = Convert.ToInt32(fila["ps_away_price"]); } catch (Exception) { b.CrisJuice = 0; }
                            }
                            else if (b.WagerPlay == "ML" || b.WagerPlay == "DR")
                            {
                                b.CrisPoints = 0;
                                try { b.CrisJuice = Convert.ToInt32(fila["ml_away_price"]); } catch (Exception) { b.CrisJuice = 0; }
                            }
                            else if (b.WagerPlay == "TOT")
                            {
                                if (b.OverUnder == "u")
                                {
                                    try { b.CrisPoints = Convert.ToDouble(fila["total"]); } catch (Exception) { b.CrisPoints = 0; }
                                    try { b.CrisJuice = Convert.ToInt32(fila["under_price"]); } catch (Exception) { b.CrisJuice = 0; }
                                }
                                else if (b.OverUnder == "o")
                                {
                                    try { b.CrisPoints = Convert.ToDouble(fila["total"]); } catch (Exception) { b.CrisPoints = 0; }
                                    try { b.CrisJuice = Convert.ToInt32(fila["over_price"]); } catch (Exception) { b.CrisJuice = 0; }
                                }
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                b.CrisJuice = 0;
                b.CrisPoints = 0;
            }
            finally
            {
                parameters.Clear();
            }

            return b;
        }








        // pinnacle lines
        public csBet PinniLines(csBet b, csGame g)
        {

            try
            {
                parameters.Add("@pDate", b.PlacedDate);
                parameters.Add("@pRot", g.HomeNumber);
                parameters.Add("@pPeriod", b.Period);
                parameters.Add("@pRot2", g.VisitorNumber);
                parameters.Add("@pWagerPlay", b.WagerPlay);

                dataset = csPinnacle.ExecutePA("[dbo].[spGetLeansLines]", parameters);
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {

                        if (b.VisHome == "H") // HOME
                        {
                            if (b.WagerPlay == "SP")
                            {
                                try { b.PinniPoints = Convert.ToDouble(fila["homeSpread"]); } catch (Exception) { b.PinniPoints = 0; }
                                try { b.PinniJuice = Convert.ToInt32(fila["homeOdds"]); } catch (Exception) { b.PinniJuice = 0; }
                            }
                            else if (b.WagerPlay == "ML" || b.WagerPlay == "DR")
                            {
                                b.PinniPoints = 0;
                                try { b.PinniJuice = Convert.ToInt32(fila["homeML"]); } catch (Exception) { b.PinniJuice = 0; }

                            } else if(b.WagerPlay == "TOT")
                            {
                                if(b.OverUnder == "u")
                                {
                                    try { b.PinniPoints = Convert.ToDouble(fila["total"]); } catch (Exception) { b.PinniPoints = 0; }
                                    try { b.PinniJuice = Convert.ToInt32(fila["totalUnder"]); } catch (Exception) { b.PinniJuice = 0; }
                                }
                                else if (b.OverUnder == "o")
                                {
                                    try { b.PinniPoints = Convert.ToDouble(fila["total"]); } catch (Exception) { b.PinniPoints = 0; }
                                    try { b.PinniJuice = Convert.ToInt32(fila["totalOver"]); } catch (Exception) { b.PinniJuice = 0; }
                                }
                            }

                        }
                        else //VISITOR
                        {
                            if (b.WagerPlay == "SP")
                            {
                                try { b.PinniPoints = Convert.ToDouble(fila["visitorSpread"]); } catch (Exception) { b.PinniPoints = 0; }
                                try { b.PinniJuice = Convert.ToInt32(fila["visitorOdds"]); } catch (Exception) { b.PinniJuice = 0; }
                            }
                            else if (b.WagerPlay == "ML" || b.WagerPlay == "DR")
                            {
                                b.PinniPoints = 0;
                                try { b.PinniJuice = Convert.ToInt32(fila["visitorML"]); } catch (Exception) { b.PinniJuice = 0; }
                            }
                            else if (b.WagerPlay == "TOT")
                            {
                                if (b.OverUnder == "u")
                                {
                                    try { b.PinniPoints = Convert.ToDouble(fila["total"]); } catch (Exception) { b.PinniPoints = 0; }
                                    try { b.PinniJuice = Convert.ToInt32(fila["totalUnder"]); } catch (Exception) { b.PinniJuice = 0; }

                                }
                                else if (b.OverUnder == "o")
                                {
                                    try { b.PinniPoints = Convert.ToDouble(fila["total"]); } catch (Exception) { b.PinniPoints = 0; }
                                    try { b.PinniJuice = Convert.ToInt32(fila["totalOver"]); } catch (Exception) { b.PinniJuice = 0; }
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception)
            {
                b.PinniJuice = 0;
                b.PinniPoints = 0;
            }
            finally
            {
                parameters.Clear();
            }

            return b;
        }


        //  Our Lines
        public csBet OurNextLine(csBet b, csGame g)
        {
            try
            {
                parameters.Add("@pDate", b.PlacedDate);
                parameters.Add("@pIdGame", g.IdGame);
                parameters.Add("@pPlay", b.Play);

                dataset = csConnection.ExecutePA("[dbo].[web_getOurNextLine]", parameters);
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    b.OurNextLine = dataset.Tables[0].Rows[0]["ChangeValue"].ToString();
                }
            }
            catch (Exception ex)
            {
                b.OurJuice = 0;
                b.OurPoints = 0;
            }
            finally
            {
                parameters.Clear();
            }

            return b;
        }





    }
}
