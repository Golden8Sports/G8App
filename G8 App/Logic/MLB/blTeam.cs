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
    public class blTeam : csComponentsConnection
    {
        public ObservableCollection<csTeam> GetAllSeries()
        {
            ObservableCollection<csTeam> data = new ObservableCollection<csTeam>();
            csTeams mlb = null;

            try
            {
                parameters.Clear();
                parameters.Add("@pTeam", "");
                parameters.Add("@pStartDate", "2018-03-28");
                parameters.Add("@pEndDate", DateTime.Now);


                dataset = csDonBest.ExecutePA("[dbo].[web_getMLBSeries]", parameters);
                data.Add(new csTeam("ALL",""));

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        mlb = new csTeams(Convert.ToString(fila["name"]));


                        bool has = data.Any(x => x.Name == mlb.Name1);
                        if (!has)
                        {
                            data.Add(new csTeam(mlb.Name1, mlb.Name1));
                        }



                        bool has1 = data.Any(x => x.Name == mlb.Name2);
                        if (!has1)
                        {
                            data.Add(new csTeam(mlb.Name2, mlb.Name2));
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
                data = null;
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