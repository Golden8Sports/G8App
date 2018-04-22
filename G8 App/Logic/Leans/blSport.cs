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
    public class blSport : csComponentsConnection
    {
        public blSport() { }
        public ObservableCollection<csSport> ListSport() 
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
                        csSport sport = new csSport(Convert.ToString(fila["IdSport"]),
                                                    Convert.ToString(fila["SportName"]),
                                                    Convert.ToString(fila["SportOrder"]));
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
