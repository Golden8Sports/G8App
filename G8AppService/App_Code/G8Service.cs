using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
public class G8Service : IG8Service
{

    string cadenaConection = ConfigurationManager.ConnectionStrings["jfallas"].ConnectionString;


    public int EditarTest(myTest a)
    {
        int res = 0;
        SqlConnection cnn = new SqlConnection(cadenaConection);

        try
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand("spUpdate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@pId", a.Id);
            cmd.Parameters.AddWithValue("@pName", a.Nombre);
            res = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception("Error to update the record", ex);
        }
        finally
        {
            cnn.Close();
        }


        return res;
    }

    public string GetData(int value)
	{
		return string.Format("You entered: {0}", value);
	}

    public List<myTest> ListarTest(string id)
    {
        SqlConnection cnn = new SqlConnection(cadenaConection);
        List<myTest> l = new List<myTest>();
        myTest mt = null;
        try
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand("spListTest", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pName", id);
            SqlDataReader r = cmd.ExecuteReader();

            if(r.HasRows)
            {
                while(r.Read())
                {
                    mt = new myTest();
                    mt.Id = r.GetInt32(0);
                    mt.Nombre = r.GetString(1);
                    l.Add(mt);
                }
            }

        }
        catch (Exception ex)
        {
            l = null;
            throw new Exception("Error to insert the record", ex);
        }
        finally
        {
            cnn.Close();
        }

        return l;
    }

    public int NuevoTest(myTest a)
    {
        int res = 0;
        SqlConnection cnn = new SqlConnection(cadenaConection);

        try
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand("spInsertTest", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pName", a.Nombre);
            res = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception("Error to insert the record", ex);  
        }
        finally
        {
            cnn.Close();
        }


        return res;
    }
}
