using Data.Connection;
using G8_App.Connection;
using G8_App.Entities.MLB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;

namespace G8_App.Logic.MLB
{
    public class blMlbSeries : csComponentsConnection
    {

        public ObservableCollection<csMlbSeries> GetAllSeries(string team, string favDog, string dt1, string dt2)
        {
            ObservableCollection<csMlbSeries> data = new ObservableCollection<csMlbSeries>();
            csMlbSeries mlb = null;

            try
            {
                parameters.Clear();
                parameters.Add("@pTeam", team.ToUpper());
                parameters.Add("@pStartDate", dt1);
                parameters.Add("@pEndDate", dt2);
                dataset = csDonBest.ExecutePA("[dbo].[web_getMLBSeries]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        mlb = new csMlbSeries(
                        Convert.ToDateTime(fila["date"]),
                        Convert.ToString(fila["name"]),
                        Convert.ToInt32(fila["participant_away_rot"]),
                        Convert.ToInt32(fila["participant_home_rot"]),
                        Convert.ToInt32(fila["id"]));
                        mlb = SetValues(mlb, favDog);

                        if(mlb.HomeML != null)
                        data.Add(mlb);
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
                throw new Exception(ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return data;
        }




        public csMlbSeries SetValues(csMlbSeries serie, string favDog)
        {

            try
            {
                parameters.Clear();
                parameters.Add("@pIdEvent", serie.Id);
                dataset = csDonBest.ExecutePA("[dbo].[web_getMLBRange]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {                  
                        try { serie.HomeML = Convert.ToInt32(fila["ml_home_price"]); } catch (Exception) { serie.HomeML = null; }
                        try { serie.VisitorML = Convert.ToInt32(fila["ml_away_price"]); } catch (Exception) { serie.VisitorML = null; }
                        try { serie.Fav = Convert.ToString(fila["FAV"]); } catch (Exception) { serie.Fav = null; }
                        try { serie.RangeFav = Convert.ToString(fila["MLB_RANGE"]); } catch (Exception) { serie.RangeFav = null; }

                        try { serie.Dog = Convert.ToString(fila["DOG"]); } catch (Exception) { serie.Dog = null; }
                        try { serie.RangeDog = Convert.ToString(fila["MLB_RANGE_DOG"]); } catch (Exception) { serie.RangeDog = null; }


                        if (serie.HomeML != null)
                        {
                            if(favDog == "Fav")//search favorites
                            {
                                if (serie.Fav == "AWAY") serie.Line = serie.VisitorML;
                                else serie.Line = serie.HomeML;

                                serie.Range = serie.RangeFav;
                                serie.Reference = serie.Fav;
                            }
                            else if(favDog == "Dog") //search dogs
                            {
                                if (serie.Dog == "AWAY") serie.Line = serie.VisitorML;
                                else serie.Line = serie.HomeML;

                                serie.Range = serie.RangeDog;
                                serie.Reference = serie.Dog;
                            }
                        }else serie.Line = null;
                    }
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

            return serie;
        }
    }
}