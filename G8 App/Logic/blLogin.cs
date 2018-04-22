using Data.Connection;
using G8_App.Entities;
using NHL_BL.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace G8_App.Logic
{
    public partial class blLogin : csComponentsConnection
    {
        public string Login(string user, string pass)
        {
            try
            {
                parameters.Add("@pUser", user);
                parameters.Add("@pPassword", pass);

                dataset = csConnection.ExecutePA("[dbo].[spLogin]", parameters);

                if(dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        csUser.Name = fila["Name"].ToString();
                        csUser.Profile = fila["ProfileName"].ToString();
                    }
                }

                return (dataset.Tables[0].Rows.Count > 0) ? "TRUE" : "FALSE";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                parameters.Clear();
                dataset.Clear();
            }

        }

    }
}