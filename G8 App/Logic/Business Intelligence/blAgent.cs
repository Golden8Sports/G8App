using Data.Connection;
using G8_App.Connection;
using G8_App.Entities.Business_Intelligence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;

namespace G8_App.Logic.Business_Intelligence
{
    public class blAgent : csComponentsConnection
    {
        public ObservableCollection<csAgent> AgentList()
        {
            ObservableCollection<csAgent> list = new ObservableCollection<csAgent>();
            csAgent a = null;

            try
            {
                dataset = csG8Apps.ExecutePA("[dbo].[webGetAgents]");
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    list.Add(new csAgent("ALL", 0));

                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                       a = new csAgent(Convert.ToString(fila["Agent"]),
                                       Convert.ToInt32(fila["IdAgent"]));
                        list.Add(a);
                    }
                }
                else
                {
                    list = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return list;
        }
    }
}