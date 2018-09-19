using Data.Connection;
using G8_App.Connection;
using G8_App.Entities.Profiling;
using NHL_BL.Connection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;

namespace G8_App.Logic.Profiling
{
    public class blPlayer : csComponentsConnection
    {
        public ObservableCollection<csPlayer> GetPlayers()
        {
            ObservableCollection<csPlayer> data = new ObservableCollection<csPlayer>();
            csPlayer player = null;

            try
            {
                parameters.Clear();
                dataset = csG8Apps.ExecutePA("[dbo].[web_getPlayers]", parameters);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        player = new csPlayer();
                        player.Player = Convert.ToString(fila["Player"]);
                        player.IdPlayer = Convert.ToString(fila["IdPlayer"]);
                        data.Add(player);
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
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return data;
        }




        public ObservableCollection<csPlayer> GetPlayersByAgent(int id)
        {
            ObservableCollection<csPlayer> data = new ObservableCollection<csPlayer>();
            csPlayer player = null;

            try
            {
                parameters.Clear();
                parameters.Add("@pIdAgent", id);
                dataset = csConnection.ExecutePA("[dbo].[web_getPlayerBYAgent]", parameters);

                data.Add(new csPlayer("ALL",""));
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in dataset.Tables[0].Rows)
                    {
                        player = new csPlayer();
                        player.Player = Convert.ToString(fila["Player"]);
                        player.IdPlayer = Convert.ToString(fila["IdPlayer"]);
                        data.Add(player);
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
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                parameters.Clear();
            }

            return data;
        }
    }
}