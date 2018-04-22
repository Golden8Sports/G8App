using Data.Connection;
using G8_App.Connection;
using NHL_BL.Connection;
using NHL_BL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_BL.Logic
{
    public class blLastLine : csComponentsConnection
    {

        private csReportNHL GetLastLine(int idGame)
        {
            csReportNHL NHL = new csReportNHL();

            try
            {
                parameters.Clear();
                parameters.Add("@pIdGame", idGame);
                parameters.Add("@pPlay", 0);
                parameters.Add("@pFisrtEnd", "E");

                dataset.Clear();
                dataset = csG8Apps.ExecutePA("[dbo].[web_getOrderLines]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        try { NHL.HomeSpecial = Convert.ToDouble(fila["HomeSpread"]); } catch (Exception) { NHL.HomeSpecial = 0; }
                        try { NHL.VisitorSpecial = Convert.ToDouble(fila["AwaySpread"]); } catch (Exception) { NHL.HomeSpecial = 0; }

                        try { NHL.HomeSpecialOdds = Convert.ToInt32(fila["HomeJuice"]); } catch (Exception) { NHL.HomeSpecialOdds = 0; }
                        try { NHL.VisitorSpecialOdds = Convert.ToInt32(fila["AwayJuice"]); } catch (Exception) { NHL.VisitorSpecialOdds = 0; }
                    }
                }

                parameters.Clear();
                parameters.Add("@pIdGame", idGame);
                parameters.Add("@pPlay", 4);
                parameters.Add("@pFisrtEnd", "E");

                dataset.Clear();
                dataset = csG8Apps.ExecutePA("[dbo].[web_getOrderLines]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        try { NHL.HomeOdds = Convert.ToInt32(fila["HomeMoneyLine"]); } catch (Exception) { NHL.HomeOdds = 0; }
                        try { NHL.VisitorOdds = Convert.ToInt32(fila["AwayMoneyLine"]); } catch (Exception) { NHL.VisitorOdds = 0; }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                parameters.Clear();
            }

            return NHL;
        }






        private csReportNHL GetFisrtLine(int idGame)
        {
            csReportNHL NHL = new csReportNHL();

            try
            {
                parameters.Clear();
                parameters.Add("@pIdGame", idGame);
                parameters.Add("@pPlay", 0);
                parameters.Add("@pFisrtEnd", "F");

                dataset.Clear();
                dataset = csG8Apps.ExecutePA("[dbo].[web_getOrderLines]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        try { NHL.OpeningHomeSpecial = Convert.ToDouble(fila["HomeSpread"]); } catch (Exception) { NHL.OpeningHomeSpecial = 0; }
                        try { NHL.OpeningVisitorSpecial = Convert.ToDouble(fila["AwaySpread"]); } catch (Exception) { NHL.OpeningVisitorSpecial = 0; }

                        try { NHL.OpeningHomeSpecialOdds = Convert.ToInt32(fila["HomeJuice"]); } catch (Exception) { NHL.OpeningHomeSpecialOdds = 0; }
                        try { NHL.OpeningVisitorSpecialOdds = Convert.ToInt32(fila["AwayJuice"]); } catch (Exception) { NHL.OpeningVisitorSpecialOdds = 0; }
                    }
                }

                parameters.Clear();
                parameters.Add("@pIdGame", idGame);
                parameters.Add("@pPlay", 4);
                parameters.Add("@pFisrtEnd", "F");

                dataset.Clear();
                dataset = csG8Apps.ExecutePA("[dbo].[web_getOrderLines]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        try { NHL.OpeningHomeOdds = Convert.ToInt32(fila["HomeMoneyLine"]); } catch (Exception) { NHL.OpeningHomeOdds = 0; }
                        try { NHL.OpeningVisitorOdds = Convert.ToInt32(fila["AwayMoneyLine"]); } catch (Exception) { NHL.OpeningVisitorOdds = 0; }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                parameters.Clear();
            }

            return NHL;

        }


        public void SetFisrtLine(csReportNHL NHL)
        {
            try
            {
                csReportNHL l = GetFisrtLine(NHL.IdGame);

                try { NHL.OpeningVisitorSpecial = (l == null || l.OpeningVisitorSpecial == null || l.OpeningVisitorSpecial == 0) ? NHL.VisitorSpecial : l.OpeningVisitorSpecial; } catch (Exception) { NHL.OpeningVisitorSpecial = NHL.VisitorSpecial; }
                try { NHL.OpeningVisitorSpecialOdds = (l == null || l.OpeningVisitorSpecialOdds == null || l.OpeningVisitorSpecialOdds == 0) ? NHL.VisitorSpecialOdds : l.OpeningVisitorSpecialOdds; } catch (Exception) { NHL.OpeningVisitorSpecialOdds = NHL.VisitorSpecialOdds; }

                try { NHL.OpeningHomeSpecial = (l == null || l.OpeningHomeSpecial == null || l.OpeningHomeSpecial == 0) ? NHL.HomeSpecial : l.OpeningHomeSpecial; } catch (Exception) { NHL.OpeningHomeSpecial = NHL.HomeSpecial; }
                try { NHL.OpeningHomeSpecialOdds = (l == null || l.OpeningHomeSpecialOdds == null || l.OpeningHomeSpecialOdds == 0) ? NHL.HomeSpecialOdds : l.OpeningHomeSpecialOdds; } catch (Exception) { NHL.OpeningHomeSpecialOdds = NHL.HomeSpecialOdds; }

                try { NHL.OpeningVisitorOdds = (l == null || l.OpeningVisitorOdds == null || l.OpeningVisitorOdds == 0) ? NHL.VisitorOdds : l.OpeningHomeOdds; } catch (Exception) { NHL.OpeningVisitorOdds = NHL.VisitorOdds; }
                try { NHL.OpeningHomeOdds = (l == null || l.OpeningHomeOdds == null || l.OpeningHomeOdds == 0) ? NHL.HomeOdds : l.OpeningHomeOdds; } catch (Exception) { NHL.OpeningHomeOdds = NHL.HomeOdds; }


                if (NHL.OpeningVisitorSpecial == null) NHL.OpeningVisitorSpecial = NHL.VisitorSpecial;
                if (NHL.OpeningVisitorSpecialOdds == null) NHL.OpeningVisitorSpecialOdds = NHL.VisitorSpecialOdds;
                if (NHL.OpeningHomeSpecial == null) NHL.OpeningHomeSpecial = NHL.HomeSpecial;
                if (NHL.OpeningHomeSpecialOdds == null) NHL.OpeningHomeSpecialOdds = NHL.HomeSpecialOdds;
                if (NHL.OpeningVisitorOdds == null) NHL.OpeningVisitorOdds = NHL.VisitorOdds;
                if (NHL.OpeningHomeOdds == null) NHL.OpeningHomeOdds = NHL.HomeOdds;


            }
            catch (Exception)
            {
                //MessageBox.Show("Error to set the first line: " + ex.Message);
            }
        }




        public void SetLastLine(csReportNHL NHL)
        {
            csReportNHL l = GetLastLine(NHL.IdGame);

            if (l != null)
            {
                NHL.VisitorSpecial = (l.VisitorSpecial == 0) ? null : l.VisitorSpecial;
                NHL.VisitorSpecialOdds = (l.VisitorSpecialOdds == 0) ? null : l.VisitorSpecialOdds;

                NHL.HomeSpecial = (l.HomeSpecial == 0) ? null : l.HomeSpecial;
                NHL.HomeSpecialOdds = (l.HomeSpecialOdds == 0) ? null : l.HomeSpecialOdds;

                NHL.VisitorOdds = (l.VisitorOdds == 0) ? null : l.VisitorOdds;
                NHL.HomeOdds = (l.HomeOdds == 0) ? null : l.HomeOdds;
            }
        }
    }
}
