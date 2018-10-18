using Data.Connection;
using G8_App.Entities;
using NHL_BL.Connection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;

namespace G8_App.Security
{
    public class blAccessUser : csComponentsConnection
    {
        public ObservableCollection<csAccessUser> GetAccessPages(string user)
        {
            ObservableCollection<csAccessUser> data = new ObservableCollection<csAccessUser>();
            csAccessUser a = null;

            try
            {
                parameters.Clear();
                parameters.Add("@pUserName", user);
                dataset = csConnection.ExecutePA("[dbo].[web_accessPages]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        a = new csAccessUser();
                        a.Page = fila["PAGE"].ToString().Trim();
                        data.Add(a);
                    }
                }
                else
                {
                    data = null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                parameters.Clear();
            }

            return data;
        }
    }
}