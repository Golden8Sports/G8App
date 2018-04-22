using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace G8_App.Connection
{
    public static class csPinnacle
    {
        private static SqlConnection conex = null;

        public static bool OpenConnection()
        {
            try { conex = new SqlConnection(ConnectionString());/* if (conex.State == 0 */ conex.Open(); } catch (Exception) { return false; }
            return true;
        }


        public static void CloseConnection()
        {
            try { conex.Close(); conex = null; } catch (Exception) { }
        }


        public static string ConnectionString()
        {
            try
            {
                return ConfigurationManager.ConnectionStrings["Pinnacle"].ConnectionString;
            }
            catch (Exception ex)
            {
                throw new Exception("Error to connect the database" + ex.Message.ToString());
            }
        }


        /// <summary>
        /// Permite ejecutar un procedimiento almacenado sin parametros de entrada
        /// </summary>
        /// <param name="ExecutePA">Nombre del procedimiento almacenado a ejecutar</param>
        /// <returns>DataSet con el resulado del proceso ejecutado</returns>
        public static DataSet ExecutePA(String namePA)
        {
            //SqlConnection sqlCon = new SqlConnection(ConnectionString());
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(namePA, conex);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("Error to execute the process." + ex.Message.ToString());
            }
            finally
            {

            }
        }


        /// <summary>
        /// Ejecuta un procedimiento almacenado que recibe paramtros de entrada
        /// </summary>
        /// <param name="ExecutePA">Nombre del procedimiento almacenado</param>
        /// <param name="parameters">Parametros de entrada para el procedimiento almacenado</param>
        /// <returns>DataSet con el resulado del proceso ejecutado</returns>
        public static DataSet ExecutePA(String namePA, Hashtable parameters)
        {
            //SqlConnection sqlCon = new SqlConnection(ConnectionString());
            DataSet ds = new DataSet();
            try
            {
                OpenConnection();
                SqlDataAdapter adapter = new SqlDataAdapter(namePA, conex);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                //load parameters
                IDictionaryEnumerator paramsPA = parameters.GetEnumerator();
                while (paramsPA.MoveNext())
                {
                    adapter.SelectCommand.Parameters.AddWithValue(paramsPA.Key.ToString(), paramsPA.Value);
                }

                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new Exception("Error to execute the process." + ex.Message.ToString());
            }
            finally
            {
                parameters.Clear();
                CloseConnection();
            }
            return ds;
        }


        /// <summary>
        /// Ejecuta un procedimiento almacenado e indica si se ejecuta o no
        /// </summary>
        /// <param name="ExecutePAConfimation">Nombre del procedimiento almacenado</param>
        /// <param name="parameters">Parametros de entrada para el procedimiento almacenado</param>
        /// <returns>Valor boleano que indica la ejecución exitosa o no del procedimiento almacenado</returns>
        public static Boolean ExecutePAConfimation(String namePA, Hashtable parameters)
        {
            //SqlConnection sqlCon = new SqlConnection(ConnectionString());
            DataSet ds = new DataSet();
            Boolean retorno;
            try
            {
                SqlCommand sqlComm = new SqlCommand();
                sqlComm = conex.CreateCommand();
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = namePA;
                //cargo los parametros
                IDictionaryEnumerator paramsPA = parameters.GetEnumerator();
                while (paramsPA.MoveNext())
                {
                    sqlComm.Parameters.AddWithValue(paramsPA.Key.ToString(), paramsPA.Value);
                }
                sqlComm.ExecuteNonQuery();
                retorno = true;
            }
            catch (Exception ex)
            {
                retorno = false;
                throw new Exception("Error to execute the process." + ex.Message.ToString());
            }
            finally
            {
                parameters.Clear();
            }
            return retorno;
        }
    }
}