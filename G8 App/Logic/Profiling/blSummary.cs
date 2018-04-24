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
        public ObservableCollection<csSummary> SummaryProfile(string dt1,string dt2, string idSport)
        {
            ObservableCollection<csSummary> data = new ObservableCollection<csSummary>();

            try
            {
                parameters.Clear();
                parameters.Add("@pStartDate", dt1);
                parameters.Add("@pEndDate", dt2);
                parameters.Add("@pIdSport", idSport);

                dataset = csG8Apps.ExecutePA("[dbo].[spSummaryProfile]", parameters);

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
                        Convert.ToInt32(fila["HOLD_PER"]),
                        Convert.ToInt32(fila["SCALPING"]),
                        Convert.ToInt32(fila["MOVELINE"]),
                        Convert.ToInt32(fila["BEATLINE"]));
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
    }
}