using Data.Connection;
using G8_App.Connection;
using G8_App.Entities.Sports.Soccer;
using Newtonsoft.Json.Linq;
using NHL_BL.Connection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace G8_App.Entities.Pinnacle_Appi
{
    public class PinniAppi : csComponentsConnection
    {
        public ObservableCollection<csSocProp> LoadSoccerProps(string cat, string league)
        {
            //prop list
            ObservableCollection<csSocProp> PropList = new ObservableCollection<csSocProp>();
            ObservableCollection<csSocProp> PropListAux = new ObservableCollection<csSocProp>();

            try
            {
                HttpClient cli = new HttpClient();
                var authInfo = Convert.ToBase64String(Encoding.Default.GetBytes("testalfa:test123"));
                cli.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
                cli.BaseAddress = new Uri("https://api.pinnacle.com/v1/fixtures/special?sportid=29&leagueids=" + league);// + "&category=" + cat);
                HttpResponseMessage res = cli.GetAsync("").Result;
                string contenido = res.Content.ReadAsStringAsync().Result;

                var results = JObject.Parse(contenido);
                int count = results["leagues"].Count();



                for (int i = 0; i < count; i++)
                {
                    var data = results["leagues"][i]["specials"].ToList();
                    int LeagueId = Convert.ToInt32(results["leagues"][i]["id"].ToString());
                    long last = Convert.ToInt64(results["last"].ToString().Trim());

                    int countResults = results["leagues"][i]["specials"].Count();
                    for (int k = 0; k < countResults; k++)
                    {
                        csSocProp prop = new csSocProp();
                        prop.leagueId = LeagueId;
                        prop.Last = last;

                        bool flag = false;
                        string cleanData = data[k].ToString().Replace("[\r\n  {\r\n", "").Trim().Replace("\r\n", "").Trim().Replace("{", "").Trim().Replace("}", "").Trim().Replace("[", "").Trim().Replace("]", "").Trim().Replace("\"", "").Trim();
                        csContestant contestant = null;

                        var split = cleanData.Split(',');

                        for (int j = 0; j < split.Length; j++)
                        {
                            string key = split[j].Trim();

                            if (flag)
                            {
                                if (key.ToUpper().Contains("NAME")) contestant.oppName = key.Replace("name:", "").Trim();
                                else if (key.ToUpper().Contains("ROT")) contestant.oppRot = Convert.ToInt32(key.Replace("rotNum:", "").Trim());
                                else if (key.ToUpper().Contains("ID"))
                                {
                                    prop.oppName1 = contestant.oppName;
                                    prop.oppRot1 = contestant.oppRot;
                                    prop.contestantsList.Add(contestant);
                                    contestant = new csContestant();
                                    contestant.oppId = Convert.ToInt32(key.Replace("id:", "").Trim());
                                }
                            }
                            else if (key.ToUpper().Contains("ID") && j == 0) prop.idSpeacial = Convert.ToInt32(key.Replace("id:", "").Trim());
                            else if (key.ToUpper().Contains("BETTYPE")) prop.betType = key.Replace("betType:", "").Trim();
                            else if (key.ToUpper().Contains("NAME") && j == 2) prop.name = key.Replace("name:", "").Trim();
                            else if (key.ToUpper().Contains("DATE")) prop.Date = prop.CastDate(key.Replace("date:", "").Trim());
                            else if (key.ToUpper().Contains("CUTOFF")) prop.cutOff = prop.CastDate(key.Replace("cutoff:", "").Trim());
                            else if (key.ToUpper().Contains("CATEGORY")) prop.category = key.Replace("category:", "").Trim();
                            else if (key.ToUpper().Contains("STATUS")) prop.status = key.Replace("status:", "").Trim();
                            else if (key.ToUpper().Contains("EVENT")) prop.eventId = Convert.ToInt32(key.Replace("event:", "").Trim().Replace("id:", "").Trim());
                            else if (key.ToUpper().Contains("PERIODNUMBER")) prop.periodNumber = Convert.ToInt32(key.Replace("periodNumber:", "").Trim());
                            else if (key.ToUpper().Contains("UNITS")) prop.units = key.Replace("units:", "").Trim();
                            else if (key.ToUpper().Contains("CONTESTANTS"))
                            {
                                contestant = new csContestant();
                                contestant.oppId = Convert.ToInt32(key.Replace("contestants:", "").Trim().Replace("id:", "").Trim());
                                flag = true;

                            }
                        }


                        if (prop.category.Contains(cat.Trim()))
                        {
                            if (contestant != null)
                            {
                                prop.oppName2 = contestant.oppName;
                                prop.oppRot2 = contestant.oppRot;
                                prop.contestantsList.Add(contestant);
                            }


                            prop = GetEvent(prop);
                            if (prop.contestantsList.Count == 2 && !String.IsNullOrWhiteSpace(prop.Event) &&
                                prop.parentID != null)
                            {
                                PropList.Add(prop);
                            }
                        }
                    }
                }





                if (PropList != null)
                {

                    ObservableCollection<csLine> Lines = LoadLine(PropList[0].leagueId, PropList[0].Last);
                    for (int i = 0; i < PropList.Count; i++)
                    {
                        if (Lines != null)
                        {
                            for (int k = 0; k < Lines.Count; k++)
                            {
                                if (PropList[i].idSpeacial == Lines[k].specialID)
                                {
                                    PropList[i].price1 = Lines[k].price1;
                                    PropList[i].price2 = Lines[k].price2;
                                    PropList[i].MaxBet = Lines[k].MaxBet;
                                    PropList[i].handicap1 = Lines[k].handicap1;
                                    PropList[i].handicap2 = Lines[k].handicap2;
                                    Lines.RemoveAt(k);
                                }
                            }
                        }

                        if (PropList[i].price1 == null || PropList[i].price2 == null || PropList[i].price1 == 0 ||
                           PropList[i].price2 == 0 || PropList[i].MaxBet == 0)
                        {

                        } else
                        {
                            PropListAux.Add(PropList[i]);
                        }
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }

            return new ObservableCollection<csSocProp>(PropListAux.OrderBy(x => x.Event).OrderBy(x => x.Event));
        }



        private csSocProp GetEvent(csSocProp p)
        {
            try
            {
                parameters.Clear();
                parameters.Add("@pIdGame", p.eventId);
                dataset = csPinnacle.ExecutePA("[dbo].[web_getGame]", parameters);

                if (dataset.Tables[0].Rows.Count == 1)
                {
                    p.homeTeam = dataset.Tables[0].Rows[0]["homeTeam"].ToString();
                    p.awayTeam = dataset.Tables[0].Rows[0]["awayTeam"].ToString();
                    p.Event = p.homeTeam + " vs " + p.awayTeam;
                }


                if (String.IsNullOrWhiteSpace(p.Event)) return p;


                parameters.Clear();
                parameters.Add("@pIdLeague", p.leagueId);
                dataset = csPinnacle.ExecutePA("[dbo].[web_getLeague]", parameters);

                if (dataset.Tables[0].Rows.Count == 1)
                {
                    p.League = dataset.Tables[0].Rows[0]["leagueName"].ToString();
                }



                parameters.Clear();
                parameters.Add("@pEventId", p.eventId);
                parameters.Add("@pPeriodID", p.periodNumber);
                dataset = csConnection.ExecutePA("[dbo].[web_getParentID]", parameters);

                if (dataset.Tables[0].Rows.Count == 1)
                {
                    p.parentID = Convert.ToInt32(dataset.Tables[0].Rows[0]["IdGame"].ToString());
                }




            }
            catch (Exception)
            {
                return p;
            }
            finally
            {
                parameters.Clear();
            }

            return p;
        }





        //get lines
        public ObservableCollection<csLine> LoadLine(int leagueId, long Last)
        {
            //prop list
            ObservableCollection<csLine> LineList = new ObservableCollection<csLine>();

            try
            {
                HttpClient cli = new HttpClient();
                var authInfo = Convert.ToBase64String(Encoding.Default.GetBytes("testalfa:test123"));
                cli.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
                cli.BaseAddress = new Uri("https://api.pinnacle.com/v1/odds/special?sportid=29&leagueids=" + leagueId + "&since=" + Last);
                HttpResponseMessage res = cli.GetAsync("").Result;
                string contenido = res.Content.ReadAsStringAsync().Result;

                csLine line = null;



                var results = JObject.Parse(contenido);

                var specials = results["leagues"][0]["specials"].ToList();
                for (int i = 0; i < specials.Count; i++)
                {

                    if (results["leagues"][0]["specials"][i]["contestantLines"].ToList().Count == 2)
                    {

                        var Special = results["leagues"][0]["specials"][i].ToList();
                        var Line1 = results["leagues"][0]["specials"][i]["contestantLines"][0].ToList();
                        var Line2 = results["leagues"][0]["specials"][i]["contestantLines"][1].ToList();

                        if (Reemplace(Line1[2].ToString()).Replace("price:", "").Trim() != "null" ||
                           Reemplace(Line2[2].ToString()).Replace("price:", "").Trim() != "null")
                        {
                            line = new csLine();
                            line.specialID = Convert.ToInt32(Reemplace(Special[0].ToString()).Replace("id:", "").Trim());
                            line.MaxBet = Convert.ToInt32(Convert.ToDouble(Reemplace(Special[1].ToString()).Replace("maxBet:", "").Trim()));
                            line.price1 = Convert.ToInt32(Convert.ToDouble(Reemplace(Line1[2].ToString()).Replace("price:", "").Trim()));

                            if (Reemplace(Line1[3].ToString()).Replace("handicap:", "").Trim() != "null") line.handicap1 = Convert.ToDouble(Reemplace(Line1[3].ToString()).Replace("handicap:", "").Trim()); else line.handicap1 = null;
                            if (Reemplace(Line2[3].ToString()).Replace("handicap:", "").Trim() != "null") line.handicap2 = Convert.ToDouble(Reemplace(Line2[3].ToString()).Replace("handicap:", "").Trim()); else line.handicap2 = null;

                            line.price2 = Convert.ToInt32(Convert.ToDouble(Reemplace(Line2[2].ToString()).Replace("price:", "").Trim()));
                            LineList.Add(line);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return LineList;
        }





        private string Reemplace(string txt)
        {
            return (txt.ToString().Replace("[\r\n  {\r\n", "").Trim().Replace("\r\n", "").Trim().Replace("{", "").Trim().Replace("}", "").Trim().Replace("[", "").Trim().Replace("]", "").Trim().Replace("\"", "").Trim()).Trim();
        }




        public bool SaveProp(csSocProp prop)
        {
            try
            {
                csSocProp p = prop;
                p = CastValues(p);

                parameters.Clear();
                parameters.Add("@pIdSport", p.sport);
                parameters.Add("@pIdLeague", 1597);
                parameters.Add("@pIdParent", p.parentID);
                parameters.Add("@pPeriod", p.periodNumber);
                parameters.Add("@pDate", p.Date);
                parameters.Add("@pRot1", p.oppRot1);
                parameters.Add("@pRot2", p.oppRot2);
                parameters.Add("@pOption1", (p.oppNameAux + " " + p.oppName1 + p.Prefix).ToUpper());
                parameters.Add("@pOption2", (p.oppNameAux + " " + p.oppName2 + p.Prefix).ToUpper());
                parameters.Add("@pPrice1", p.price1);
                parameters.Add("@pPrice2", p.price2);
                parameters.Add("@pSpread1", p.handicap1);
                parameters.Add("@pSpread2", p.handicap2);
                parameters.Add("@pTotal", p.Total);
                parameters.Add("@pTotalOver", p.TotalOver);
                parameters.Add("@pTotalUnder", p.TotalUnder);
                parameters.Add("@pIdSpecial", p.idSpeacial);
                parameters.Add("@pDescription", (p.name + " (" + p.Event + ")").ToUpper());

                return csConnection.ExecutePAConfimation("[dbo].[web_insert_gameImporterTemp]", parameters);
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




        private csSocProp CastValues(csSocProp p)
        {

            if (p.category.ToUpper().Contains("TOTAL") &&
               p.oppName1.ToUpper().Trim() == "OVER" &&
               p.oppName2.ToUpper().Trim() == "UNDER")
            {
                p.Total = p.handicap1;
                p.TotalOver = p.price1;
                p.TotalUnder = p.price2;

                p.handicap1 = null;
                p.handicap2 = null;
                p.price1 = null;
                p.price2 = null;
            }
            else
            {
                p.Total = null;
                p.TotalOver = null;
                p.TotalUnder = null;
                p.handicap1 = (p.handicap1 == null) ? 0 : p.handicap1;
                p.handicap2 = (p.handicap2 == null) ? 0 : p.handicap2;
            }


            if (p.name.ToUpper().Contains("ASIAN HANDICAP"))
            {
                p.name = p.name.Replace("Asian Handicap", "").Trim();
            }

            return Swap(p);
        }



        private csSocProp Swap(csSocProp p)
        {
            p.oppNameAux = p.name.Replace("Will", "").Trim().Replace("Will there be a", "").Trim().Replace("Odd/Even", "").Trim().Replace("Asian Handicap", "").Trim().Replace("Total", "").Trim().Replace("there be a","").Trim().Replace("?","").Trim();

            string T1 = "";
            string T2 = "";

            T1 = (p.homeTeam.ToUpper().Contains("COSTA")) ? "CRC" : p.homeTeam.Substring(0, 3);
            T2 = (p.awayTeam.ToUpper().Contains("COSTA")) ? "CRC" : p.awayTeam.Substring(0, 3);

            p.Prefix = " (" + T1 + "/" + T2 + ")";

            return p;
        }
    }
}


