using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using G8_App.Entities.Sports;


namespace G8_App.Logic.Administration
{
    public class blInterpreter
    {
        private List<csSport> listaDeporte = new List<csSport>();

        public blInterpreter()
        {
            LlenarListaDeporte();
        }


        private void LlenarListaDeporte()
        {
            listaDeporte.Add(new csSport("TENNIS", "TENNIS"));
            listaDeporte.Add(new csSport("GOLF", "GOLF"));
            listaDeporte.Add(new csSport("UFC", "MMA"));
            listaDeporte.Add(new csSport("BELLATOR", "MMA"));
            listaDeporte.Add(new csSport("ATP", "TENNIS"));
            listaDeporte.Add(new csSport("BOXING", "BOXING"));
            listaDeporte.Add(new csSport("WTA", "TENNIS"));
            listaDeporte.Add(new csSport("UEFA", "SOC"));
            listaDeporte.Add(new csSport("NHL", "NHL"));
            listaDeporte.Add(new csSport("NBA", "NBA"));
            listaDeporte.Add(new csSport("MLB", "MLB"));
            listaDeporte.Add(new csSport("CFB", "CFB"));
            listaDeporte.Add(new csSport("CBB", "CBB"));
            listaDeporte.Add(new csSport("NFL", "NFL"));
            listaDeporte.Add(new csSport("PGA", "GOLF"));
            listaDeporte.Add(new csSport("CYCLING", "CYCLING"));
            listaDeporte.Add(new csSport("MEN'S ROLEX PARIS MASTERS", "TENNIS"));
        }


        public string Interpretar(string txt)
        {

            for (int i = 0; i < listaDeporte.Count; i++)
            {
                if (txt.ToUpper().Contains(listaDeporte[i].SearchName))
                    return listaDeporte[i].RealName;
            }

            return "";
        }
    }
}