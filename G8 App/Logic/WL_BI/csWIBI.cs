using Data.Connection;
using G8_App.Connection;
using NHL_BL.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace G8_App.Logic.WL_BI
{
    public class csWIBI : csComponentsConnection
    {
        public void UpdateBI()
        {
            DateTime dt = DateTime.Now;
            DateTime dt2 = DateTime.Now.AddDays(1);
            parameters.Clear();
            parameters.Add("@startDate", dt.Year + "-" + dt.Month + "-" + dt.Day);
            parameters.Add("@endDate", dt2.Year + "-" + dt2.Month + "-" + dt2.Day);

            dataset = csConnection.ExecutePA("[dbo].[spBIFeedJFallas2]", parameters);
            csData data = null;

            if (dataset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in dataset.Tables[0].Rows)
                {
                    data = new csData();
                    data.IdWager = Convert.ToInt32(fila["IdWager"]);
                    data.IdWagerDetail = Convert.ToInt32(fila["IdWagerDetail"]);
                    data.IdAgent = Convert.ToInt32(fila["IdAgent"]);
                    data.Agent = Convert.ToString(fila["Agent"]);
                    data.IdPlayer = Convert.ToInt32(fila["IdPlayer"]);
                    data.Player = Convert.ToString(fila["Player"]);
                    data.IdLineType = Convert.ToInt32(fila["IdLineType"]);
                    data.IdLineTypeName = Convert.ToString(fila["LineType"]);
                    data.LoginName = Convert.ToString(fila["LoginName"]);
                    data.WinAmount = Convert.ToDouble(fila["WinAmount"]);
                    data.RiskAmount = Convert.ToDouble(fila["RiskAmount"]);
                    data.Result = Convert.ToString(fila["Result"]);
                    data.Net = Convert.ToDouble(fila["Neto"]);
                    data.GamePeriod = Convert.ToString(fila["GamePeriod"]);
                    data.League = Convert.ToString(fila["LEAGUE"]);
                    data.CompleteDescription = Convert.ToString(fila["CompleteDescription"]);
                    data.DetailDescription = Convert.ToString(fila["DetailDescription"]);
                    data.Team = Convert.ToString(fila["TEAM"]);
                    data.IdGame = Convert.ToInt32(fila["IdGame"]);
                    data.IdLeague = Convert.ToInt32(fila["IdLeague"]);
                    data.Period = Convert.ToInt32(fila["Period"]);
                    data.Play = Convert.ToInt32(fila["Play"]);
                    data.WagerPlay = Convert.ToString(fila["WagerPlay"]);
                    data.FavDog = Convert.ToString(fila["FAV_DOG"]);
                    data.IdSport = Convert.ToString(fila["IdSport"]);
                    data.PlacedDate = Convert.ToDateTime(fila["PlacedDate"]);
                    data.SeattledDate = Convert.ToDateTime(fila["SettledDate"]);
                    data.Odds = Convert.ToInt32(fila["Odds"]);
                    data.Points = Convert.ToDouble(fila["Points"]);
                    data.Score = Convert.ToString(fila["Score"]);
                    data.IP = Convert.ToString(fila["IP"]);
                    data.BeatLine = Convert.ToString(fila["BeatLine"]);

                    if (!String.IsNullOrWhiteSpace(fila["OpeningPoints"].ToString())) data.OpeningPoints = Convert.ToDouble(fila["OpeningPoints"]);else data.OpeningPoints = null;
                    if (!String.IsNullOrWhiteSpace(fila["OpeningMoneyLine"].ToString())) data.OpeningOdds = Convert.ToInt32(fila["OpeningMoneyLine"]); else data.OpeningOdds = null;
                    if (!String.IsNullOrWhiteSpace(fila["ClosingPoints"].ToString())) data.ClosingPoints = Convert.ToDouble(fila["ClosingPoints"]); else data.ClosingPoints = null;
                    if (!String.IsNullOrWhiteSpace(fila["ClosingMoneyLine"].ToString())) data.ClosingOdds = Convert.ToInt32(fila["ClosingMoneyLine"]); else data.ClosingOdds = null;

                    spInsertWL(data);
                }
            }


        }





        public void spInsertWL(csData data)
        {
                    parameters.Clear();
                    parameters.Add("@IdWager", data.IdWager);
                    parameters.Add("@IdWagerDetail", data.IdWagerDetail);
                    parameters.Add("@IdAgent", data.IdAgent);
                    parameters.Add("@Agent", data.Agent);
                    parameters.Add("@IdPlayer", data.IdPlayer);
                    parameters.Add("@Player", data.Player);
                    parameters.Add("@IdLineType", data.IdLineType);
                    parameters.Add("@LineType", data.IdLineTypeName);
                    parameters.Add("@LoginName", data.LoginName);
                    parameters.Add("@WinAmount", data.WinAmount);
                    parameters.Add("@RiskAmount", data.RiskAmount);
                    parameters.Add("@Result", data.Result);
                    parameters.Add("@Neto", data.Net);
                    parameters.Add("@GamePeriod", data.GamePeriod);
                    parameters.Add("@LEAGUE", data.League);
                    parameters.Add("@CompleteDescription", data.CompleteDescription);
                    parameters.Add("@DetailDescription", data.DetailDescription);
                    parameters.Add("@TEAM", data.Team);
                    parameters.Add("@IdGame", data.IdGame);
                    parameters.Add("@IdLeague", data.IdLeague);
                    parameters.Add("@Period", data.Period);
                    parameters.Add("@Play", data.Play);
                    parameters.Add("@WagerPlay", data.WagerPlay);
                    parameters.Add("@FAV_DOG", data.FavDog);
                    parameters.Add("@IdSport", data.IdSport);
                    parameters.Add("@PlacedDate", data.PlacedDate);
                    parameters.Add("@SettledDate", data.SeattledDate);
                    parameters.Add("@Odds", data.Odds);
                    parameters.Add("@Points", data.Points);
                    parameters.Add("@Score", data.Score);
                    parameters.Add("@IP", data.IP);

                    if (data.OpeningPoints == null) parameters.Add("@OpeningPoints", -10000); else parameters.Add("@OpeningPoints", data.OpeningPoints);
                    if (data.OpeningOdds == null) parameters.Add("@OpeningMoneyLine", -10000); else parameters.Add("@OpeningMoneyLine", data.OpeningOdds);
                    if (data.ClosingPoints == null) parameters.Add("@ClosingPoints", -10000); else parameters.Add("@ClosingPoints", data.ClosingPoints);
                    if (data.ClosingOdds == null) parameters.Add("@ClosingMoneyLine", -10000); else parameters.Add("@ClosingMoneyLine", data.ClosingOdds);

                    parameters.Add("@BeatLine", data.BeatLine);

                    if (csG8Apps.ExecutePAConfimation("[dbo].[spInsertDataBIWL_Second]", parameters))
                    {
                        parameters.Clear();
                    }
        }



    }
}