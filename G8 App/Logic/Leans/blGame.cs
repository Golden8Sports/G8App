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
using System.Windows.Forms;

namespace NHL_BL.Logic
{
    public class blGame : csComponentsConnection
    {
        public ObservableCollection<csGame> ListGames(string dt1, string dt2, String idSport, int idLeague, string player)
        {
            ObservableCollection<csGame> list = new ObservableCollection<csGame>();
            try
            {
                parameters.Add("@pStartDate", dt1);
                parameters.Add("@pEndDate", dt2);
                parameters.Add("@pIdSport", idSport);
                parameters.Add("@pIdLeague", idLeague);
                parameters.Add("@pPlayer", player);

                dataset = csConnection.ExecutePA("[dbo].[web_GetLeansIdGames]", parameters);
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        list.Add(new csGame(Convert.ToInt32(fila["IdGame"]),
                                            Convert.ToString(fila["HomeTeam"]),
                                            Convert.ToString(fila["VisitorTeam"]),
                                            Convert.ToInt32(fila["IdLeague"]),
                                            Convert.ToString(fila["IdSport"]),
                                            Convert.ToInt32(fila["HomeNumber"]),
                                            Convert.ToInt32(fila["VisitorNumber"]),
                                            Convert.ToDateTime(fila["EventDate"])));
                    }
                }
                else
                {
                    list = null;
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

            return list;
        }



    }
}