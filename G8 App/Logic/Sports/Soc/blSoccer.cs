using Data.Connection;
using G8_App.Connection;
using NHL_BL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;

namespace G8_App.Logic.Sports.Soc
{
    public class blSoccer : csComponentsConnection
    {
        public ObservableCollection<csLeague> LeaguesBySport(int idSport)
        {
            ObservableCollection<csLeague> leagueList = new ObservableCollection<csLeague>();
            try
            {
                    parameters.Add("@pIdSport", idSport);
                    dataset = csPinnacle.ExecutePA("[dbo].[web_getLeagues]", parameters);
                    if (dataset.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in dataset.Tables[0].Rows)
                        {
                            csLeague league = new csLeague(Convert.ToString(fila["leagueID"]),
                                                           Convert.ToString(fila["leagueName"]));
                            leagueList.Add(league);
                        }
                    }
                    else
                    {
                        leagueList = null;
                    }
            }
            catch (Exception)
            {
               // MessageBox.Show("Error,   " + ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return leagueList;
        }
    }
}