using Data.Connection;
using G8_App.Connection;
using G8_App.Entities.Business_Intelligence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;

namespace G8_App.Logic.Business_Intelligence
{
    public class blBet : csComponentsConnection
    {
        public ObservableCollection<csBet> BetList(string d1, string d2, string s, int a, string wagerPlay, string betType, int league, int player)
        {
            ObservableCollection<csBet> data = new ObservableCollection<csBet>();

            try
            {
                parameters.Clear();
                parameters.Add("@pStartDate", d1);
                parameters.Add("@pEndDate", d2);
                parameters.Add("@pSport", s.Trim());
                parameters.Add("@pAgent", a);
                parameters.Add("@pWagerPlay", wagerPlay);
                parameters.Add("@pBetType", betType);

                parameters.Add("@pIdLeague", league);
                parameters.Add("@pPlayer", player);

                dataset = csG8Apps.ExecutePA("[dbo].[webGetBets]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        csBet u = new csBet(
                        Convert.ToInt64(fila["Id_BI"]),
                        Convert.ToInt32(fila["IdWager"]),
                        Convert.ToInt32(fila["IdWagerDetail"]),
                        Convert.ToInt32(fila["IdAgent"]),
                        Convert.ToString(fila["Agent"]),
                        Convert.ToInt32(fila["IdPlayer"]),
                        Convert.ToString(fila["Player"]),
                        Convert.ToInt32(fila["IdLineType"]),
                        Convert.ToString(fila["LineTypeName"]),
                        Convert.ToString(fila["LoginName"]),
                        Convert.ToDouble(fila["WinAmount"]),
                        Convert.ToInt32(fila["RiskAmount"]),
                        Convert.ToString(fila["Result"]),
                        Convert.ToDouble(fila["Net"]),
                        Convert.ToString(fila["GamePeriod"]),
                        Convert.ToString(fila["League"]),
                        Convert.ToString(fila["CompleteDescription"]),
                        Convert.ToString(fila["DetailDescription"]),
                        Convert.ToString(fila["Team"]),
                        Convert.ToInt32(fila["IdGame"]),
                        Convert.ToInt32(fila["IdLeague"]),
                        Convert.ToInt32(fila["Period"]),
                        Convert.ToString(fila["FAV_DOG"]),
                        Convert.ToInt32(fila["Play"]),
                        Convert.ToString(fila["WagerPlay"]),
                        Convert.ToString(fila["IdSport"]),
                        Convert.ToDateTime(fila["SettledDate"]),
                        Convert.ToDateTime(fila["PlacedDate"]),
                        Convert.ToInt32(fila["Odds"]),
                        Convert.ToDouble(fila["Points"]),
                        Convert.ToString(fila["Score"]),
                        Convert.ToString(fila["IP"]),
                        Convert.ToString(fila["BeatLine"]));
                        data.Add(u);
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
            finally {parameters.Clear();}

            return data;
        }

    }
}