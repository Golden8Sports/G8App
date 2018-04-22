using Data.Connection;
using G8_App.Entities.NHL_Ranges;
using NHL_BL.Connection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;

namespace G8_App.Logic.NHL_Ranges
{
    public class blTeam : csComponentsConnection
    {
        public blTeam() { }

        public ObservableCollection<csTeam> TeamsListNHL(int id1, int id2)
        {
            ObservableCollection<csTeam> list = new ObservableCollection<csTeam>();
            csTeam data = null;

            try
            {
                parameters.Clear();
                parameters.Add("@IdLeague1", id1);
                parameters.Add("@IdLeague2", id2);
                dataset = csConnection.ExecutePA("[dbo].[web_ListTeamsNHL]", parameters);
               
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        data = new csTeam(Convert.ToInt32(fila["IdTeam"]),
                                          Convert.ToString(fila["TeamName"]));
                        list.Add(data);
                    }

                    data = new csTeam(0, "All");                       
                    list.Add(data);
                }
                else
                {
                    list = null;
                }


            }
            catch (Exception)
            {
                list.Clear();
                data = new csTeam(0, "All");
                list.Add(data);
            }
            finally
            {
                parameters.Clear();
            }

            return list;

        }
    }
}
