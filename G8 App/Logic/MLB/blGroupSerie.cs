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
    public class blGroupSerie : csComponentsConnection
    {

        public ObservableCollection<csGroupSerie> GetAllGamesBySerie(csMlbSeries serie)
        {
            ObservableCollection<csGroupSerie> data = new ObservableCollection<csGroupSerie>();
            csGroupSerie mlb = null;
            int n = 0;

            try
            {
                DateTime dt = serie.EventDate.AddDays(serie.CountGames);
                parameters.Clear();
                parameters.Add("@pStartDate", serie.EventDate.Year + "-" + serie.EventDate.Month + "-" + serie.EventDate.Day);
                parameters.Add("@pEndDate", dt.Year + "-" + dt.Month + "-" + dt.Day);
                parameters.Add("@pHomeTeam", serie.HomeTeam);
                parameters.Add("@pvisitorTeam", serie.VisitorTeam);

                dataset = csDonBest.ExecutePA("[dbo].[web_getGameSameSerie]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    //serie.SetCountGames(dataset.Tables[0].Rows.Count);

                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        mlb = new csGroupSerie(
                        Convert.ToDateTime(fila["date"]),
                        Convert.ToString(fila["name"]),
                        Convert.ToInt32(fila["participant_away_rot"]),
                        Convert.ToInt32(fila["participant_home_rot"]),
                        Convert.ToInt32(fila["id"]));

                        data.Add(mlb);
                    }

                    n++;

                }
                else
                {
                    data = null;
                }

            }
            catch (Exception ex)
            {
                data = null;
                n = n;
                throw new Exception(ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return data;
        }
    }
}