using G8_App.Entities.MLB;
using G8_App.Entities.Scraping.Covers;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace G8_App.Logic.Scraping.Covers
{
    public class blCovers
    {
        public ObservableCollection<csCovers> Scrap(string url)
        {
            ObservableCollection<csCovers> data = new ObservableCollection<csCovers>();
            csCovers d = null;

            try
            {
                string result = string.Empty;
                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(url);

                HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");
                if (tables != null)
                    for (int k = 0; k < tables.Count; k++)
                    {
                        HtmlNodeCollection rows = tables[k].SelectNodes(".//tr");

                            if (rows != null)
                            for (int i = 1; i < rows.Count; ++i)
                            {

                                    d = new csCovers();

                                    HtmlNodeCollection cols = rows[i].SelectNodes(".//td");

                                if (cols != null)
                                {
                                    for (int j = 0; j < cols.Count; ++j)
                                    {

                                        string txt = cols[j].InnerText.Replace("\r\n", "").Trim().Replace("\t\t (R)", "").Trim().Replace("\t\t (L)", "").Trim();
                                        txt = txt.Replace("(OT)", "").Trim();
                                        txt = txt.Replace("@ ", "").Trim();
                                        txt = txt.Replace("(R)", "").Trim();

                                        if (j == 0) d.Date = txt;
                                        else if (j == 1) d.VS = txt.Replace("@", "").Trim();
                                        else if (j == 2)
                                        {
                                            d.Result = (txt.Contains("L") ? 0 : 1);
                                            string spl = txt.Replace("L", "").Trim().Replace("W", "").Trim();
                                            var split = spl.Split('-');

                                            d.AwayScore = Convert.ToInt32(split[0]);
                                            d.HomeScore = Convert.ToInt32(split[1]);
                                        }
                                        else if (j == 3) d.AwayStarter = txt;
                                        else if (j == 4) d.HomeStarter = txt;
                                        else if (j == 5) d.AriLine = Convert.ToInt32(txt.Replace("L", "").Trim().Replace("W", "").Trim());
                                        else if (j == 6) d.OU = txt;

                                    }

                                    data.Add(d);
                                }

                            }

                        //stringContent = stringContent.Replace("</script>", "");

                    }

                //Response.Clear();
                //Response.ContentType = "text/plain";
                //Response.AddHeader("Content-Disposition", "attachment;filename=Scrap.csv");
                //Response.Write(stringContent);
                //Response.End();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return data;
        }


        public ObservableCollection<csTeamStat> ScrapMLB_Stats(string url)
        {
            ObservableCollection<csTeamStat> data = new ObservableCollection<csTeamStat>();
            csTeamStat t = null;

            try
            {
                string result = string.Empty;
                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(url);


                // Get all tables in the document
                HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");
                //String stringContent = "";
                // Iterate all rows in the first table
                if (tables != null)
                    for (int k = 0; k < tables.Count; k++)
                    {
                        HtmlNodeCollection rows = tables[k].SelectNodes(".//tr");

                        if (rows != null)
                            for (int i = 1; i < rows.Count; ++i)
                            {

                                t = new csTeamStat();

                                HtmlNodeCollection cols = rows[i].SelectNodes(".//td");
                                //string stringCellText = "";
                                if (cols != null)
                                {
                                    for (int j = 0; j < cols.Count; ++j)
                                    {

                                        string txt = cols[j].InnerText.Replace("\r\n", "").Trim().Replace("\t\t (R)", "").Trim().Replace("\t\t (L)", "").Trim();

                                        if(!String.IsNullOrWhiteSpace(txt))
                                        {
                                            if (j == 0) t.Name = txt;
                                            else if (j == 1) t.AB = Convert.ToInt32(txt);
                                            else if (j == 2) t.PA = Convert.ToInt32(txt);
                                            else if (j == 3) t.H = Convert.ToInt32(txt);
                                            else if (j == 4) t.B1 = Convert.ToInt32(txt);
                                            else if (j == 5) t.B2 = Convert.ToInt32(txt);
                                            else if (j == 6) t.B3 = Convert.ToInt32(txt);
                                            else if (j == 7) t.HR = Convert.ToInt32(txt);
                                            else if (j == 8) t.R = Convert.ToInt32(txt);
                                            else if (j == 9) t.RBI = Convert.ToInt32(txt);
                                            else if (j == 10) t.BB = Convert.ToInt32(txt);
                                            else if (j == 11) t.K = Convert.ToInt32(txt);
                                            else if (j == 12) t.SF = Convert.ToInt32(txt);
                                            else if (j == 13) t.GDP = Convert.ToInt32(txt);
                                            else if (j == 14) t.SB = Convert.ToInt32(txt);
                                            else if (j == 15) t.AVG = Convert.ToDouble(txt);
                                            else if (j == 16) t.OBP = Convert.ToDouble(txt);
                                            else if (j == 17) t.SLG = Convert.ToDouble(txt);
                                            else if (j == 18) t.OPS = Convert.ToDouble(txt);
                                        }
                                    }


                                    data.Add(t);
                                }

                            }

                        //stringContent = stringContent.Replace("</script>", "");

                    }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return data;
        }










        public string ScrapToExcel(string url)
        {

            String stringContent = "";

            try
            {          
                string result = string.Empty;
                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(url);

                // Get all tables in the document
                HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");
                
                // Iterate all rows in the first table
                if (tables != null)
                    for (int k = 0; k < tables.Count; k++)
                    {
                        HtmlNodeCollection rows = tables[k].SelectNodes(".//tr");

                        if (rows != null)
                            for (int i = 0; i < rows.Count; ++i)
                            {

                                HtmlNodeCollection cols = rows[i].SelectNodes(".//td");
                                string stringCellText = "";
                                if (cols != null)
                                    for (int j = 0; j < cols.Count; ++j)
                                    {
                                        if (String.IsNullOrEmpty(stringCellText))
                                            stringCellText = "\"" + cols[j].InnerText.Replace("\r\n", "").Trim().Replace("&#149;", "").Replace("&nbsp;", "").Replace("</script>", "") + "\"";
                                        else
                                            stringCellText += "," + "\"" + cols[j].InnerText.Replace("\r\n", "").Trim().Replace("&#149;", "").Replace("&nbsp;", "").Replace("</script>", "") + "\"";
                                    }

                                if (String.IsNullOrEmpty(stringContent))
                                    stringContent = stringCellText;
                                else
                                    stringContent += "\n" + stringCellText;
                            }

                        stringContent = stringContent.Replace("</script>", "");
                    }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return stringContent;
        }
    }
}