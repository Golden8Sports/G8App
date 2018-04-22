using Data.Connection;
using NHL_BL.Connection;
using NHL_BL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace HouseReport_BL.Logic
{
    public class blLeague : csComponentsConnection
    {
        public blLeague() { }

        public ObservableCollection<csLeague> LeaguesBySport(string idSport)
        {
            ObservableCollection<csLeague> leagueList = new ObservableCollection<csLeague>();
            try
            {
                if(idSport != "ALL")
                {
                    parameters.Add("@prmIdSport", idSport);
                    dataset = csConnection.ExecutePA("[dbo].[GetLeaguesBySport]", parameters);
                    if (dataset.Tables[0].Rows.Count > 0)
                    {
                        leagueList.Add(new csLeague("ALL", "ALL"));

                        foreach (DataRow fila in dataset.Tables[0].Rows)
                        {
                            csLeague league = new csLeague(Convert.ToString(fila["IdLeague"]),
                                                           Convert.ToString(fila["Description"]));
                            leagueList.Add(league);
                        }
                    }
                    else
                    {
                        leagueList = null;
                    }
                }
                else
                {
                    leagueList.Add(new csLeague("ALL", "ALL"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error,   " + ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return leagueList;
        }
    }
}
