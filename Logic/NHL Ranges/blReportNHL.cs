using Data.Connection;
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
    public class blReportNHL : csComponentsConnection
    {
        private blLastLine lastLine = new blLastLine();
        public static ObservableCollection<csReportNHL> ListNull = new ObservableCollection<csReportNHL>();

        public blReportNHL() { }

        public ObservableCollection<csReportNHL> SportListNHL(string date1, string date2,int league, string side, string team, bool alternative, DataSet dt)
        {
            ObservableCollection<csReportNHL> list = new ObservableCollection<csReportNHL>();

            try
            {
                parameters.Clear();
                parameters.Add("@startD", date1);
                parameters.Add("@endD", date2);
                parameters.Add("@leagueID", league);
                parameters.Add("@side", side);
                parameters.Add("@team", team);
                ListNull.Clear();

                dt.Clear();
                dt = csConnection.ExecutePA("[dbo].[_web_GenerateSportRange]", parameters);
                csReportNHL data = null;

                if (dt.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dt.Tables[0].Rows)
                    {
                        data = new csReportNHL();

                        try { data.HomeScore = Convert.ToInt32(fila["HomeScore"]); } catch (Exception) { data.HomeScore = 0; }
                        try { data.VisitorTeam = Convert.ToString(fila["VisitorTeam"]); } catch (Exception) { data.VisitorTeam = ""; }
                        try { data.HomeNumber = Convert.ToInt32(fila["HomeNumber"]); } catch (Exception) { data.HomeNumber = -1; }
                        try { data.HomeTeam = Convert.ToString(fila["HomeTeam"]); } catch (Exception) { data.HomeTeam = ""; }
                        try { data.WINNER = Convert.ToString(fila["WINNER"]); } catch (Exception) { data.WINNER = "NONE"; }
                        try { data.IdGame = Convert.ToInt32(fila["IdGame"]); } catch (Exception) { data.IdGame = -1; }
                        try { data.GameDateTime = Convert.ToDateTime(fila["GameDateTime"]); } catch (Exception) { data.GameDateTime = DateTime.Now; }
                        try { data.VisitorNumber = Convert.ToInt32(fila["VisitorNumber"]); } catch (Exception) { data.VisitorNumber = -1; }
                        try { data.VisitorScore = Convert.ToInt32(fila["VisitorScore"]); } catch (Exception) { data.VisitorScore = 0; }
                        try { data.VisitorSpecial = Convert.ToDouble(fila["VisitorSpecial"]); } catch { data.VisitorSpecial = null; }
                        try { data.VisitorSpecialOdds = Convert.ToInt32(fila["VisitorSpecialOdds"]); } catch { data.VisitorSpecialOdds = null; }
                        try { data.HomeSpecial = Convert.ToDouble(fila["HomeSpecial"]); } catch { data.HomeSpecial = null; }
                        try { data.HomeSpecialOdds = Convert.ToInt32(fila["HomeSpecialOdds"]); } catch { data.HomeSpecialOdds = null; }
                        try { data.VisitorOdds = Convert.ToInt32(fila["VisitorOdds"]); } catch { data.VisitorOdds = null; }
                        try { data.HomeOdds = Convert.ToInt32(fila["HomeOdds"]); } catch { data.HomeOdds = null; }
                        try { data.TotalOver = Convert.ToDouble(fila["TotalOver"]); } catch { data.TotalOver = null; }
                        try { data.OverOdds = Convert.ToInt32(fila["OverOdds"]); } catch { data.OverOdds = null; }
                        try { data.UnderOdds = Convert.ToInt32(fila["UnderOdds"]); } catch { data.UnderOdds = null; }                       

                        list.Add(data);
                    }

                    list = FindNulls(list,alternative);
                }
                else
                {
                    list = null;
                }

            }
            catch(NoNullAllowedException ex)
            {
                list = null;
            }
            catch (Exception ex)
            {
                list = null;
            }
            finally
            {
                parameters.Clear();
            }

            return list;

        }



        private ObservableCollection<csReportNHL> FindNulls(ObservableCollection<csReportNHL> list, Boolean alternative)
        {
            if(list != null && list.Count > 0)
            {
                foreach (var data in list)
                {
                    if ((data.HomeSpecial == null && data.HomeOdds == null && data.HomeSpecialOdds == null) || (data.VisitorOdds == null && data.VisitorSpecial == null &&
                         data.VisitorSpecialOdds == null) || (data.HomeSpecial == null && data.HomeOdds == null) || (data.VisitorOdds == null && data.VisitorSpecial == null) ||
                         (data.HomeSpecialOdds == null && data.HomeOdds == null) || (data.VisitorOdds == null && data.VisitorSpecialOdds == null) ||
                          data.HomeSpecialOdds == null || data.VisitorSpecialOdds == null && !alternative)
                    {
                        csReportNHL n = data;
                        lastLine.SetLastLine(data);
                        if (n != null)
                        {
                            data.VisitorSpecial = n.VisitorSpecial;
                            data.HomeSpecial = n.HomeSpecial;
                            data.VisitorOdds = n.VisitorOdds;
                            data.HomeOdds = n.HomeOdds;
                            data.VisitorSpecialOdds = n.VisitorSpecialOdds;
                            data.HomeSpecialOdds = n.HomeSpecialOdds;
                        }
                    }
 

                        if (!alternative) lastLine.SetFisrtLine(data);
                   
                }
            }

            return list;
        }
            

    }
}
