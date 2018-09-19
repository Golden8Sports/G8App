using Data.Connection;
using G8_App.Connection;
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
                        leagueList.Add(new csLeague("-1", "ALL"));

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






        public ObservableCollection<csLeague> LeaguesByPlayerBI(string d1, string d2, string player, string sport)
        {
            ObservableCollection<csLeague> leagueList = new ObservableCollection<csLeague>();
            try
            {
                if (sport != "ALL")
                {
                    parameters.Add("@pStartDate", d1);
                    parameters.Add("@pEndDate", d2);
                    parameters.Add("@pPlayer", player);
                    parameters.Add("@pSport", sport);
                    dataset = csG8Apps.ExecutePA("[dbo].[web_LeaguesByPLayer]", parameters);
                    if (dataset.Tables[0].Rows.Count > 0)
                    {
                        leagueList.Add(new csLeague("-1", "ALL"));

                        foreach (DataRow fila in dataset.Tables[0].Rows)
                        {
                            csLeague league = new csLeague(Convert.ToString(fila["IdLeague"]),
                                                           Convert.ToString(fila["League"]));
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
