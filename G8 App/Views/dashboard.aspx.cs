using G8_App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using G8_App.G8AppService;
using HouseReport_BL.Logic;
using G8_App.Logic.Profiling;
using System.Net;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Data;
using HtmlAgilityPack;
using System.Web.Script.Services;
using System.Web.Services;
using G8_App.Entities.Pinnacle_Appi;
using G8_App.Entities.Sports.Soccer;
using System.Xml;
using G8_App.Entities.Scores;
using G8_App.Logic.Scores;

namespace G8_App.Views
{
    public partial class dashboard : System.Web.UI.Page
    {
        private List<String> Headers = new List<String>();
        private List<String> Opp1 = new List<String>();
        private List<String> Opp2 = new List<String>();
        private List<csScore> ScoreList = new List<csScore>();
        private string sport = "";

        protected void Page_Load(object sender, EventArgs e)
        { 
            if (Session["Login"] != null)
            {
                if (!IsPostBack) userName.InnerText = csUser.Name + " - " + csUser.Profile;
                //SetInfo("2018-07-13");

            }else Response.Redirect("Login.aspx");
        }


        private void SetInfo(string date)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://server1.donbest.com/scores/" + date +  "/all.html?cb=0.6528608926953476");
            myRequest.Method = "GET";
            try
            {
                WebResponse myResponse = myRequest.GetResponse();
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                string result = sr.ReadToEnd();

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(result);

                foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
                {
                    int tableNumber = 0;
                    bool flag = false, first = false;

                    foreach (HtmlNode row in table.SelectNodes("tr"))
                    {
                        string f = row.InnerText;

                        if (f.ToUpper().Contains("SCORES"))
                        {
                            sport = CastIdSportDonBest(f);
                        }

                        foreach (HtmlNode cell in row.SelectNodes("td"))
                        {
                            String c = cell.InnerText.ToString().Replace("&nbsp;","").Trim().Replace("&nbsp","").Trim();

                            if (DetectNumber(c))
                            {
                                first = flag = true;
                            }

                            if (flag)
                            {
                                if (tableNumber == 0) //save the headers
                                {
                                    if (first)
                                    {
                                        Headers.Add("rot");
                                        Headers.Add("team");
                                        first = false;
                                    }
                                    else
                                    {
                                        if (sport != "10" && sport != "8" && c.Trim() == "T")
                                        {
                                            Headers.Add(c);
                                            Headers.Add("status");
                                        }
                                        else if (sport == "8" && c.Trim() == "T")
                                        {
                                            Headers.Add("status");
                                        }
                                        else
                                        {
                                            Headers.Add(c);
                                        }
                                    }
                                }
                                else if (tableNumber == 1)//add values for the opponent 1
                                {
                                    Opp1.Add(c);
                                }
                                else if (tableNumber == 2)
                                {
                                    Opp2.Add(c);
                                }
                            }
                        }

                        if (flag)
                        {
                            tableNumber++;
                        }
                    }


                    int n1 = Headers.Count;
                    int n2 = Opp1.Count;
                    int n3 = Opp2.Count;

                    if (Headers.Count == Opp1.Count && Opp1.Count == Opp2.Count && Headers.Count > 0)
                    {
                        csScore score = new csScore();
                        for (int i = 0; i < Headers.Count; i++)
                        {
                            score.Values.Add(new csDictionary(Headers[i], Opp1[i], Opp2[i]));
                        }
                        score.sportId = sport;
                        ScoreList.Add(score);
                    }
                    else if (Headers.Count > 0)
                    {
                        int dssd = 0;
                    }

                    ClearValues();
                }

                blScores scoreDB = new blScores();
                if (ScoreList != null && ScoreList.Count > 0)
                {
                    foreach (var i in ScoreList)
                    {                      
                        csScore score = i;
                        score.awayRot = i.Values[0].Val1;
                        score.homeRot = i.Values[0].Val2;

                        for (int j = 2; j < i.Values.Count; j++)
                        {
                            if (i.Values[j].Header.ToUpper().Contains("OPEN"))
                            {
                                break;
                            }

                            score.period = i.Values[j].Header;
                            score.awayScore = i.Values[j].Val1;
                            score.homeScore = i.Values[j].Val2;
                            score.sportId = i.sportId;
                            score.date = date;

                            if ((j + 1) < (ScoreList.Count - 1) && i.Values[j + 1].Header == "status")
                            {
                                score.description1 = i.Values[j + 1].Val1.Replace("&nbsp;", "").Replace("\n", "").Trim();
                                score.description2 = i.Values[j + 1].Val2.Replace("&nbsp;", "").Replace("\n", "").Trim();
                            }
                            else
                            {
                                score.description1 = "";
                                score.description2 = "";
                            }

                            if(i.Values[j].Header != "status")
                            scoreDB.InsertScore(score);                            
                        }
                    }
                }
            }
            catch (WebException)
            {
            }
        }


        private void ClearValues()
        {
            this.Headers = new List<string>();
            this.Opp1 = new List<string>();
            this.Opp2 = new List<string>();
        }


        private bool DetectNumber(string c)
        {
            if(c.Contains(":"))
            {
                c = c.Replace("pm", "").Trim().Replace("am","").Trim().Replace("md","").Trim();
                var split = c.Split(':');
                int num1;

                int count = split.Length;
                if(count >= 2)
                {
                    split[0] = split[0].Trim();
                    split[1] = split[1].Trim();
                }

                if(split[1].Length >= 3 && split[1].Length <= 30)
                {
                    string m = split[1];
                    if(split[1].ToUpper().Contains("(") && split[1].ToUpper().Contains(")"))
                    {
                        split[1] = split[1].Substring(0, 2);
                    }               
                    string f = split[1];
                }

                return (int.TryParse(split[0], out num1) && int.TryParse(split[1], out num1));
            }

            return false;
        }



        private string CastIdSportDonBest(string sport)
        {
            if (sport.ToUpper().Contains("SOCCER")) return "5";
            else if (sport.ToUpper().Contains("NHL") || sport.ToUpper().Contains("HOCKEY")) return "4";
            else if (sport.ToUpper().Contains("NFL") || sport.ToUpper().Contains("CFB") || sport.ToUpper().Contains("FOOTBALL")) return "1";
            else if (sport.ToUpper().Contains("NBA")  || sport.ToUpper().Contains("CBB") || sport.ToUpper().Contains("BASKETBALL") || sport.ToUpper().Contains("WNBA")) return "2";
            else if (sport.ToUpper().Contains("BASEBALL") || sport.ToUpper().Contains("MLB")) return "3";
            else if (sport.ToUpper().Contains("BOXING") || sport.ToUpper().Contains("FIGHT") || sport.ToUpper().Contains("MMA")) return "6";
            else if (sport.ToUpper().Contains("GOLF")) return "7";
            else if (sport.ToUpper().Contains("TENNIS")) return "8";
            else if (sport.ToUpper().Contains("RACING")) return "9";
            else return "10";
        }
    }
}