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
using System.Windows.Forms;

namespace NHL_BL.Logic
{
    public class blbet : csComponentsConnection
    {

        private blLeansLines LeansDB = new blLeansLines();

        public ObservableCollection<csBet> BetListFromIdGame(csGame game, string player, bool flag = true)
        {
            ObservableCollection<csBet> data = new ObservableCollection<csBet>();

            try
            {
                parameters.Clear();
                parameters.Add("@pIgGame", game.IdGame);
                parameters.Add("@pPlayer", player);

                dataset = csG8Apps.ExecutePA("[dbo].[web_GetBetsFromLeansByIdGame]", parameters);

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

                            if(flag)
                            {
                                u = LeansDB.OurNextLine(u, game);
                                u = LeansDB.CrisLines(u, game, 489);
                                u = LeansDB.PinniLines(u, game);
                            }
                            u.EventDate = game.EventDate;
                            u.EventName = game.VisitorTeam + " vs " + game.HomeTeam;
                            u.Pick = (game.VisitorNumber == u.Rot) ? game.VisitorTeam : game.HomeTeam;

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
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                parameters.Clear();
            }

            return data;
        }



        public ObservableCollection<csBet> GetBetsAfterLeans(int idGame, csBet bet, string wagerPlay,string player, int caso = 1)
        {
            ObservableCollection<csBet> data = new ObservableCollection<csBet>();


            if(!string.IsNullOrWhiteSpace(wagerPlay))
            {
                if (wagerPlay == "ML") wagerPlay = "MONEY";
                if (wagerPlay == "SP") wagerPlay = "SPREAD";
                if (wagerPlay == "DR") wagerPlay = "DRAW";
                if (wagerPlay == "TOT") wagerPlay = "TOTAL";
            }


            string date = bet.PlacedDate.Year + "-" + bet.PlacedDate.Month + "-" + bet.PlacedDate.Day + " " + bet.PlacedDate.Hour + ":" + bet.PlacedDate.Minute + ":" + bet.PlacedDate.Second + ".000";

            try
            {
                parameters.Clear();
                parameters.Add("@pIgGame", idGame);
                parameters.Add("@pPlacedDate", date);
                parameters.Add("@pWagerType", wagerPlay);
                parameters.Add("@pPlayer", player);

                string casito = (caso == 1) ? "[dbo].[web_GetAfterLeans]" : "[dbo].[web_GetAfterLeans2]";


                dataset = csG8Apps.ExecutePA(casito, parameters);

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
                        Convert.ToInt32(fila["Points"]),
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
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                parameters.Clear();
            }

            return data;
        }
    }
}