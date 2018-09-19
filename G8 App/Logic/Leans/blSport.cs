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
using G8_App.Connection;

namespace HouseReport_BL.Logic
{
    public class blSport : csComponentsConnection
    {
        public blSport() { }
        public ObservableCollection<csSport> ListSportFromDGS() 
        {
            ObservableCollection<csSport> sportList = new ObservableCollection<csSport>();
            try
            {
                dataset = csConnection.ExecutePA("[dbo].[Sport_GetList]", parameters);
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    sportList.Add(new csSport("ALL", "ALL", "ALL"));

                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        csSport sport = new csSport(Convert.ToString(fila["IdSport"]).Trim(),
                                                    Convert.ToString(fila["SportName"]).Trim(),
                                                    Convert.ToString(fila["SportOrder"]).Trim());
                        sportList.Add(sport);
                    }
                }
                else
                {
                    sportList = null;
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

            return sportList;
        }




        public ObservableCollection<csSport> ListSportFromDonBest()
        {
            ObservableCollection<csSport> sportList = new ObservableCollection<csSport>();
            try
            {
                dataset = csDonBest.ExecutePA("[dbo].[web_getSports]", parameters);
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    sportList.Add(new csSport("-1", "ALL"));

                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        csSport sport = new csSport(Convert.ToString(fila["id"]).Trim(),
                                                    Convert.ToString(fila["name"]).Trim());
                        sportList.Add(sport);
                    }
                }
                else
                {
                    sportList = null;
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

            return sportList;
        }



    }
}
