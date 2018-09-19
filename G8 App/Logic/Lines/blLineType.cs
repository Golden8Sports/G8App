using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using Data.Connection;
using G8_App.Connection;
using NHL_BL.Connection;
using NHL_BL.Entities;
using G8_App.Entities.Lines;


namespace G8_App.Logic.Lines
{
    public class blLineType : csComponentsConnection
    {
        public ObservableCollection<csLineType> GetLinesType()
        {
            ObservableCollection<csLineType> data = new ObservableCollection<csLineType>();
            csLineType type = null;

            try
            {
                parameters.Clear();
                dataset = csConnection.ExecutePA("[dbo].[web_getLinesType]", parameters);;

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (System.Data.DataRow fila in dataset.Tables[0].Rows)
                    {
                        type = new csLineType(Convert.ToInt32(fila["IdLineType"]),
                                              Convert.ToString(fila["Description"]));
                        data.Add(type);
                    }

                }
                else
                {
                    data = null;
                }

            }
            catch (Exception ex)
            {
                data = null;
                throw new Exception(ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return data;
        }
    }
}