using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
[ServiceContract]
public interface IG8Service
{
    [OperationContract]
    int NuevoTest(myTest a);

    [OperationContract]
    int EditarTest(myTest a);

    [OperationContract]
    List<myTest> ListarTest(string id);
}

[DataContract]
public class myTest
{
    private int _id;
    private string _nombre;

    [DataMember]
    public int Id
    {
        get{return _id;}
        set{ _id = value;}
    }

    [DataMember]
    public string Nombre
    {
        get{return _nombre;}
        set{ _nombre = value;}
    }
}
