using Data.Connection;
using G8_App.Connection;
using G8_App.Entities.MLB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Logic.MLB
{
    public class blStatMLB : csComponentsConnection 
    {
        public bool InsertDataMLB(csTeamStat stat)
        {

            try
            {
                parameters.Clear();
                parameters.Add("@pName", stat.Name);
                parameters.Add("@pAB", stat.AB);
                parameters.Add("@pPA", stat.PA);
                parameters.Add("@pH", stat.H);
                parameters.Add("@pB1", stat.B1);
                parameters.Add("@pB2", stat.B2);
                parameters.Add("@pB3", stat.B3);
                parameters.Add("@pHR", stat.HR);
                parameters.Add("@pR", stat.R);
                parameters.Add("@pRBI", stat.RBI);
                parameters.Add("@pBB", stat.BB);
                parameters.Add("@pK", stat.K);
                parameters.Add("@pSF", stat.SF);
                parameters.Add("@pGDP", stat.GDP);
                parameters.Add("@pSB", stat.SB);
                parameters.Add("@pAVG", stat.AVG);
                parameters.Add("@pOBP", stat.OBP);
                parameters.Add("@pSLG", stat.SLG);
                parameters.Add("@pOPS", stat.OPS);

                return csConnectionMLB.ExecutePAConfirmation("[dbo].[web_insertMLBstat]", parameters);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                parameters.Clear();
            }

        }
    }
}