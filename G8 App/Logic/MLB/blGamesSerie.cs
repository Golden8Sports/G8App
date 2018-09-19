using Data.Connection;
using G8_App.Connection;
using G8_App.Entities.MLB;
using NHL_BL.Connection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;

namespace G8_App.Logic.MLB
{
    public class blGamesSerie : csComponentsConnection
    {

        public ObservableCollection<csGamesBySerie> GetGamesBySerie(csGroupSerie serie, string Ref)
        {
            ObservableCollection<csGamesBySerie> data = new ObservableCollection<csGamesBySerie>();
            csGamesBySerie mlb = null;
                      
            try
            {
                int n = Convert.ToInt32(serie.AwayRot.ToString().Substring(1, serie.AwayRot.ToString().Length - 1));
                parameters.Clear();
                parameters.Add("@pDate", serie.EventDate.Year + "-" + serie.EventDate.Month + "-" + serie.EventDate.Day);
                parameters.Add("@pRot", n);

                dataset = csG8Apps.ExecutePA("[dbo].[web_getGamesBySerie]", parameters);

                if (dataset.Tables[0].Rows.Count == 1)
                {
                    mlb = new csGamesBySerie();
                    mlb.EventDate = Convert.ToDateTime(dataset.Tables[0].Rows[0]["GameDateTime"]);
                    mlb.VisitorNumber = Convert.ToInt32(dataset.Tables[0].Rows[0]["VisitorNumber"]);
                    mlb.HomeNumber = Convert.ToInt32(dataset.Tables[0].Rows[0]["HomeNumber"]);
                    mlb.Id = Convert.ToInt32(dataset.Tables[0].Rows[0]["IdGame"]);
                    mlb.VisitorTeam = Convert.ToString(dataset.Tables[0].Rows[0]["VisitorTeam"]);
                    mlb.HomeTeam = Convert.ToString(dataset.Tables[0].Rows[0]["HomeTeam"]);
                    mlb.VisitorPitcher = (!String.IsNullOrWhiteSpace(dataset.Tables[0].Rows[0]["VisitorPitcher"].ToString())) ? dataset.Tables[0].Rows[0]["VisitorPitcher"].ToString() : null;
                    mlb.HomePitcher = (!String.IsNullOrWhiteSpace(dataset.Tables[0].Rows[0]["HomePitcher"].ToString())) ? dataset.Tables[0].Rows[0]["HomePitcher"].ToString() : null;
                    mlb.HomeScore = (!String.IsNullOrWhiteSpace(dataset.Tables[0].Rows[0]["HomeScore"].ToString())) ? Convert.ToInt32(dataset.Tables[0].Rows[0]["HomeScore"]) : mlb.HomeScore;
                    mlb.VisitorScore = (!String.IsNullOrWhiteSpace(dataset.Tables[0].Rows[0]["VisitorScore"].ToString())) ? Convert.ToInt32(dataset.Tables[0].Rows[0]["VisitorScore"]) : mlb.VisitorScore;
                    mlb.HomeOdds = (!String.IsNullOrWhiteSpace(dataset.Tables[0].Rows[0]["HomeOdds"].ToString())) ? Convert.ToInt32(dataset.Tables[0].Rows[0]["HomeOdds"]) : mlb.HomeOdds;
                    mlb.VisitorOdds = (!String.IsNullOrWhiteSpace(dataset.Tables[0].Rows[0]["VisitorOdds"].ToString())) ? Convert.ToInt32(dataset.Tables[0].Rows[0]["VisitorOdds"]) : mlb.VisitorOdds;
                    mlb.Spread = (!String.IsNullOrWhiteSpace(dataset.Tables[0].Rows[0]["VisitorSpread"].ToString())) ? (Ref == "AWAY") ? Convert.ToDouble(dataset.Tables[0].Rows[0]["VisitorSpread"]) : Convert.ToDouble(dataset.Tables[0].Rows[0]["HomeSpread"]) : mlb.Spread;
                    mlb.Total = (!String.IsNullOrWhiteSpace(dataset.Tables[0].Rows[0]["TotalOver"].ToString())) ? Convert.ToDouble(dataset.Tables[0].Rows[0]["TotalOver"]) : mlb.Total;
                    mlb.TotalOver = (!String.IsNullOrWhiteSpace(dataset.Tables[0].Rows[0]["OverOdds"].ToString())) ? Convert.ToDouble(dataset.Tables[0].Rows[0]["OverOdds"]) : mlb.TotalOver;
                    mlb.TotalUnder = (!String.IsNullOrWhiteSpace(dataset.Tables[0].Rows[0]["UnderOdds"].ToString())) ? Convert.ToDouble(dataset.Tables[0].Rows[0]["UnderOdds"]) : mlb.TotalUnder;
                    mlb.SpreadOdds = (!String.IsNullOrWhiteSpace(dataset.Tables[0].Rows[0]["VisitorSpreadOdds"].ToString())) ? (Ref == "AWAY") ? Convert.ToDouble(dataset.Tables[0].Rows[0]["VisitorSpreadOdds"]) : Convert.ToDouble(dataset.Tables[0].Rows[0]["HomeSpreadOdds"]) : mlb.SpreadOdds;

                      
                        if (mlb.HomeOdds != null)
                        {
                            mlb.Line = (Ref == "AWAY") ? mlb.VisitorOdds : mlb.HomeOdds;
                        }
                        else
                        {
                            // *********  HERE MOST BE THE SECOND CONSULT TO EXTRACT THE MONEY LINE   *************
                            mlb = GetLine(mlb, Ref);
                        }

                       data.Add(mlb);

                }else if(dataset.Tables[0].Rows.Count > 1)
                {
                    int ne = 0;
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





        public csGamesBySerie GetLine(csGamesBySerie mlb, string Ref)
        {

            try
            {
                parameters.Clear();
                parameters.Add("@pIdGame", mlb.Id);
                parameters.Add("@pPlay", (Ref == "AWAY") ? 4 : 5);
                parameters.Add("@pFisrtEnd", "E");
                parameters.Add("@pIdLineType", 140);

                dataset = csConnection.ExecutePA("[dbo].[web_getOrderLines]", parameters);


                if (dataset.Tables[0].Rows.Count == 1)
                {                  
                    mlb.Line = (!String.IsNullOrWhiteSpace(dataset.Tables[0].Rows[0]["ChangeValue"].ToString())) ? Convert.ToInt32(dataset.Tables[0].Rows[0]["ChangeValue"]) : mlb.Line;
                }
                else
                {
                    mlb.Line = null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return mlb;
        }






    }
}