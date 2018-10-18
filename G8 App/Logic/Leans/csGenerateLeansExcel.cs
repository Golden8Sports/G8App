using G8_App.Entities.Leans;
using NHL_BL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace NHL_BL.Logic
{
    public class csGenerateLeansExcel
    {
        private ObservableCollection<csBet> LeansBetsList = new ObservableCollection<csBet>();
        private ObservableCollection<csSummary> Players = new ObservableCollection<csSummary>();
        private blbet betDB = new blbet();
        private List<String> Sports = new List<String>();
        private csSummary summary = new csSummary();
        private ObservableCollection<csSummary> SummaryList = new ObservableCollection<csSummary>();

        public csGenerateLeansExcel() { }

        public void GenerateExcel(ObservableCollection<csGame> Games, string player, ObservableCollection<csBet> BetsWithLeans, ObservableCollection<csBet> NoLeansBets, ObservableCollection<csBet> L, ObservableCollection<csBet> SumGames)
        {
            try
            {
                if (Games != null)
                {
                    //Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    // Excel.Workbook wb = app.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                    //ObservableCollection<csBet> BetsWithLeans = new ObservableCollection<csBet>();
                    //ObservableCollection<csBet> NoLeansBets = new ObservableCollection<csBet>();
                    //ObservableCollection<csSummary> SummaryListGame = new ObservableCollection<csSummary>();

                    foreach (var g in Games)
                    {
                        ObservableCollection<csBet> LeansBets = betDB.BetListFromIdGame(g,player);
                        csBet GameSum = new csBet();
                        GameSum.Event = g.VisitorTeam + " vs " + g.HomeTeam;                      
                        GameSum.Sport = g.IdSport;
                        GameSum.GameDate = g.EventDate.Month + "/" + g.EventDate.Day + "/" + g.EventDate.Year;

                        if (LeansBets != null)
                        {
                            foreach (var l in LeansBets)
                            {
                                ObservableCollection<csBet> bets = betDB.GetBetsAfterLeans(g.IdGame, l, l.WagerPlay, player);
                                GameSum.Line +=  l.Odds + ", " + l.OverUnder + l.Points;
                                GameSum.CrisLine += l.CrisJuice + ", " + l.OverUnder + l.CrisPoints;
                                GameSum.PinniLine += l.PinniJuice + ", " + l.OverUnder + l.PinniPoints;
                                GameSum.OurLine += l.OurNextLine;
                                GameSum.Team += (l.Rot == g.HomeNumber) ? g.HomeTeam : g.VisitorTeam;
                                GameSum.WagerPlay = l.WagerPlay;
                                l.GameDate = GameSum.GameDate;
                                //GameSum.Time = l.PlacedDate.ToString();

                                LeansBetsList.Add(l);
                                L.Add(l);
                                // to check if the sport doesn't exist.
                                //bool has = Sports.Any(x => x == l.IdSport);
                                //if (!has)
                                //{
                                //    Sports.Add(l.IdSport);
                                //}

                                if (bets != null && bets.Count > 0)
                                {
                                    foreach (var b in bets)
                                    {
                                        // **************************** Aqui es donde hay que generar el excel ***********************
                                        b.EventDate = g.EventDate;
                                        b.EventName = g.VisitorTeam + " vs " + g.HomeTeam;
                                        b.GameDate = GameSum.GameDate;

                                        try
                                        {
                                            if (b.Rot == l.Rot)
                                            {
                                                //Same team with LEANS
                                                b.Pick = (g.VisitorNumber == b.Rot) ? g.VisitorTeam : g.HomeTeam;

                                                GameSum.ContLeansBets += 1;
                                                GameSum.WinLeans += b.WinAmount;
                                                GameSum.RiskLeans += b.RiskAmount;
                                                GameSum.NetLeans += b.Net;

                                                BetsWithLeans.Add(b);
                                                summary.ContLeansBets += 1;
                                            }
                                            else
                                            {
                                                //Different team with LEANS
                                                b.Pick = (g.VisitorNumber == b.Rot) ? g.VisitorTeam : g.HomeTeam;

                                                GameSum.ContNoLeansBets += 1;
                                                GameSum.WinNoLeans += b.WinAmount;
                                                GameSum.RiskNoLeans += b.RiskAmount;
                                                GameSum.NetNoLeans += b.Net;

                                                NoLeansBets.Add(b);
                                                summary.ContNoLeansBets += 1;
                                            }

                                            ////by player
                                            //bool flag = Players.Any(x => x.IdPlayer == b.IdPlayer);
                                            //if (!flag)
                                            //{
                                            //    csSummary s = new csSummary();
                                            //    s.IdPlayer = b.IdPlayer;
                                            //    s.Player = b.Player;
                                            //    Players.Add(s);
                                            //}
                                            // sum total
                                            //GameSum.TotalBets += 1;
                                            //GameSum.WinAmount += b.WinAmount;
                                            //GameSum.RiskAmount += b.RiskAmount;
                                            //GameSum.Net += b.Net;
                                        }
                                        catch (Exception)
                                        {
                                        }
                                        // fin generacion de Excel
                                    }
                                }
                            }
                        }

                        GameSum.LeansHold = Math.Round(Convert.ToDouble((GameSum.NetLeans * 100) / GameSum.RiskLeans), 2, MidpointRounding.AwayFromZero);
                        GameSum.NoLeansHold = Math.Round(Convert.ToDouble((GameSum.NetNoLeans * 100) / GameSum.RiskNoLeans), 2, MidpointRounding.AwayFromZero);

                        SumGames.Add(GameSum);
                    }

                    // ************************* bets with the same bet from Leans *********************
                    //if (Sports != null)
                    //{
                    //    ObservableCollection<csSummary> SummaryList = new ObservableCollection<csSummary>();

                    //    foreach (var sp in Sports)
                    //    {
                    //        //Excel.Worksheet wsSport = wb.Sheets.Add();
                    //        //wsSport.Name = sp;
                    //        //CompleteRange(wsSport);
                    //        index = 0;
                    //        bool flag = true;

                    //        foreach (var g in Games)
                    //        {
                    //            if (g.IdSport.Trim(' ') == sp)
                    //            {
                    //                if(!flag) index += 1;

                    //                if (LeansBetsList != null)
                    //                {

                    //                    for (int j = 0; j < LeansBetsList.Count; j++)
                    //                    {

                    //                        if (LeansBetsList[j].IdGame == g.IdGame)
                    //                        {
                    //                            if (BetsWithLeans != null)
                    //                            {
                    //                                if (LeansBetsList[j].IdSport == sp)
                    //                                {
                    //                                    if (!flag) index += 1;
                    //                                    else flag = false;

                    //                                    //ShowInfo(wsSport, index, j, LeansBetsList, 3, g);
                    //                                    //CenterAllInfo(wsSport, index);

                    //                                    for (int i = 0; i < BetsWithLeans.Count; i++)
                    //                                    {
                    //                                        if (LeansBetsList[j].IdGame == BetsWithLeans[i].IdGame &&
                    //                                           LeansBetsList[j].Rot == BetsWithLeans[i].Rot &&
                    //                                           LeansBetsList[j].WagerPlay == BetsWithLeans[i].WagerPlay)
                    //                                        {
                    //                                            index += 1;
                    //                                            //ShowInfo(wsSport, index, i, BetsWithLeans, 1, g);
                    //                                            //CenterAllInfo(wsSport, index);
                    //                                        }
                    //                                    }
                    //                                }
                    //                            }

                    //                            if (LeansBetsList[j].IdSport == sp)
                    //                            {
                    //                                if (NoLeansBets != null)
                    //                                {

                    //                                    for (int i = 0; i < NoLeansBets.Count; i++)
                    //                                    {

                    //                                        if (LeansBetsList[j].IdGame == NoLeansBets[i].IdGame &&
                    //                                           LeansBetsList[j].Rot != NoLeansBets[i].Rot &&
                    //                                           LeansBetsList[j].WagerPlay == NoLeansBets[i].WagerPlay)
                    //                                        {
                    //                                            index += 1;
                    //                                            //ShowInfo(wsSport, index, i, NoLeansBets, 2, g);
                    //                                            //CenterAllInfo(wsSport, index);
                    //                                        }
                    //                                    }
                    //                                }
                    //                            }
                    //                        }
                    //                    }
                    //                }

                    //                index += 2;
                    //            }

                    //        }

                    //        SummaryList.Add(GetValuesFromSummary(sp, BetsWithLeans, NoLeansBets));

                    //    }


                    //   // (sender as BackgroundWorker).ReportProgress(40);

                    //    //Excel.Worksheet ws3 = wb.Sheets.Add();
                    //    //ws3.Name = "SUMMARY";
                    //    index = 0;

                    //    csSummary general = new csSummary();

                    //    if (SummaryList != null)
                    //    {
                    //        //CompleteRangeSport(ws3);
                    //        for (int i = 0; i < SummaryList.Count; i++)
                    //        {
                    //            //ShowInfoSport(ws3, i, SummaryList[i].sport, SummaryList[i]);
                    //            //CenterAllInfoSport(ws3, i);

                    //            general.ContLeansBets += SummaryList[i].ContLeansBets;
                    //            general.RiskLeans += SummaryList[i].RiskLeans;
                    //            general.NetLeans += SummaryList[i].NetLeans;
                    //            general.WinBetsLeans += SummaryList[i].WinBetsLeans;

                    //            general.ContNoLeansBets += SummaryList[i].ContNoLeansBets;
                    //            general.RiskNoLeans += SummaryList[i].RiskNoLeans;
                    //            general.NetNoLeans += SummaryList[i].NetNoLeans;
                    //            general.WinBetsNoLeans += SummaryList[i].WinBetsNoLeans;

                    //            index = i;
                    //        }
                    //    }


                    //    try { general.WinPerLeans = (general.WinBetsLeans * 100 / general.ContLeansBets); } catch (Exception) { general.WinPerLeans = 0; }
                    //    try { general.WinPerNoLeans = (general.WinBetsNoLeans * 100 / general.ContNoLeansBets); } catch (Exception) { general.WinPerNoLeans = 0; }

                    //    try { general.LeansHold = (general.NetLeans * 100 / general.RiskLeans); } catch (Exception) { general.LeansHold = 0; }
                    //    try { general.NoLeansHold = (general.NetNoLeans * 100 / general.RiskNoLeans); } catch (Exception) { general.NoLeansHold = 0; }



                    //    index += 6;
                    //    //RangeGeneral(ws3, index);
                    //    index += 1;
                    //    //ShowInfoGeneral(ws3, index, general);
                    //    //CenterAllInfoGeneral(ws3, index - 2);


                    //   // (sender as BackgroundWorker).ReportProgress(60);

                    //}



                    //// ********************** area by player ********************************** //


                    //if (Players != null)
                    //{
                    //    //Excel.Worksheet wsp = wb.Sheets.Add();
                    //    //wsp.Name = "By Player";
                    //    index = 1;
                    //    //CompleteRangePlayer(wsp, index);
                    //    index -= 2;

                    //    foreach (var p in Players)
                    //    {
                    //        index += 1;

                    //        if (BetsWithLeans != null)
                    //        {
                    //            foreach (var s in BetsWithLeans)
                    //            {
                    //                if (p.IdPlayer == s.IdPlayer)
                    //                {
                    //                    p.ContLeansBets += 1;
                    //                    p.RiskLeans += s.RiskAmount;
                    //                    p.NetLeans += s.Net;

                    //                    if (s.Result == "WIN") p.WinBetsLeans += 1;
                    //                    else p.LostBetsLeans += 1;
                    //                }
                    //            }

                    //            try { p.WinPerLeans = (p.WinBetsLeans * 100 / p.ContLeansBets); } catch (Exception) { p.WinPerLeans = 0; }
                    //            try { if (p.ContLeansBets > 0) { p.LeansHold = (p.NetLeans * 100 / p.RiskLeans); } else p.LeansHold = 0; } catch (Exception) { p.LeansHold = 0; }

                    //        }


                    //        if (NoLeansBets != null)
                    //        {
                    //            foreach (var s in NoLeansBets)
                    //            {
                    //                if (p.IdPlayer == s.IdPlayer)
                    //                {
                    //                    p.ContNoLeansBets += 1;
                    //                    p.RiskNoLeans += s.RiskAmount;
                    //                    p.NetNoLeans += s.Net;

                    //                    if (s.Result == "WIN") p.WinBetsNoLeans += 1;
                    //                    else p.LostBetsNoLeans += 1;

                    //                }
                    //            }

                    //            try { p.WinPerNoLeans = (p.WinBetsNoLeans * 100 / p.ContNoLeansBets); } catch (Exception) { p.WinPerNoLeans = 0; }
                    //            try { if (p.ContNoLeansBets > 0) { p.NoLeansHold = (p.NetNoLeans * 100 / p.RiskNoLeans); } else p.NoLeansHold = 0; } catch (Exception) { p.NoLeansHold = 0; }
                    //        }

                    //        //ShowPlayer(wsp, index, p);
                    //        //CenterAllInfoSport(wsp, index);

                    //    }
                    //}  //fin area player


                    //// ********************** Shett by game ********************************** //
                    //if (SummaryListGame != null)
                    //{
                    //    //Excel.Worksheet ws4 = wb.Sheets.Add();
                    //    //ws4.Name = "BY GAME";

                    //    index = 0;
                    //    //CompleteRangeGame(ws4,player);

                    //    foreach (var i in SummaryListGame)
                    //    {
                    //        //ShowInfoGame(ws4, index, i);
                    //        //CenterAllInfoGame(ws4, index);
                    //        index += 2;
                    //    }
                    //   // (sender as BackgroundWorker).ReportProgress(100);
                    //}

                    //// ************************* SAVE THE DOCUMENT *********************
                    ////wb.SaveAs(path + @"/LeansReport.xls");
                    ////wb.Saved = true;
                    ////wb.Close(true);
                    ////app.Quit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        private void Range(Excel.Worksheet ws, string cell, string txt, int size, bool flag = true)
        {
            ws.Range[cell].Value = txt;
            ws.Range[cell].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            if (flag) ws.Range[cell].ColumnWidth = size;
            ws.Range[cell].BorderAround2();
            ws.Range[cell].Font.Bold = true;
            ws.Range[cell].Interior.Color = System.Drawing.Color.FromArgb(179, 179, 204);
            ws.Range[cell].Font.Size = 14;
        }




        private void RangeGeneral(Excel.Worksheet ws, string cell, string txt, bool flag)
        {
            ws.Range[cell].Value = txt;
            ws.Range[cell].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ws.Range[cell].BorderAround2();
            ws.Range[cell].Font.Bold = true;
            ws.Range[cell].Interior.Color = (flag) ? System.Drawing.Color.FromArgb(255, 199, 206) : System.Drawing.Color.FromArgb(198, 239, 206);
            ws.Range[cell].Font.Size = 14;
        }



        private void Bold(Excel.Worksheet ws, string cell)
        {
            ws.Range[cell].Font.Bold = true;
            ws.Range[cell].BorderAround2();
        }


        private void RangeNormal(Excel.Worksheet ws, string cell, string txt, int size)
        {
            ws.Range[cell].Value = txt;
            ws.Range[cell].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //ws.Range[cell].BorderAround2();
            ws.Range[cell].Font.Bold = true;
            ws.Range[cell].Interior.Color = System.Drawing.Color.FromArgb(255, 199, 206);
            ws.Range[cell].Font.Size = 10;
        }


        private void RangeSport(Excel.Worksheet ws, string cell, string txt, int size, bool flag = true)
        {
            ws.Range[cell].Value = txt;
            ws.Range[cell].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ws.Range[cell].ColumnWidth = size;
            ws.Range[cell].BorderAround2();
            ws.Range[cell].Font.Bold = true;
            ws.Range[cell].Interior.Color = (flag) ? System.Drawing.Color.FromArgb(255, 199, 206) : System.Drawing.Color.FromArgb(198, 239, 206);
            ws.Range[cell].Font.Size = 14;
        }


        private void CompleteRange(Excel.Worksheet ws)
        {
            Range(ws, "A1", "Event", 42);
            Range(ws, "B1", "Time", 23);
            Range(ws, "C1", "IdAgent", 12);
            Range(ws, "D1", "Agent", 12);
            Range(ws, "E1", "IdPlayer", 13);
            Range(ws, "F1", "Player", 12);
            Range(ws, "G1", "IdLineType", 17);
            Range(ws, "H1", "LineTypeName", 26);
            Range(ws, "I1", "LoginName", 16);
            Range(ws, "J1", "WinAmount", 18);
            Range(ws, "K1", "RinskAmount", 18);
            Range(ws, "L1", "Result", 11);
            Range(ws, "M1", "Net", 11);
            Range(ws, "N1", "GamePeriod", 17);
            Range(ws, "O1", "League", 28);
            Range(ws, "P1", "CompleteDescription", 26);
            Range(ws, "Q1", "DetailDescription", 37);
            Range(ws, "R1", "IdGame", 13);
            Range(ws, "S1", "IdLeague", 14);
            Range(ws, "T1", "Period", 13);
            Range(ws, "U1", "Fav Dog", 19);
            Range(ws, "V1", "Play", 10);
            Range(ws, "W1", "WagerPlay", 17);
            Range(ws, "X1", "IdSport", 12);
            Range(ws, "Y1", "SeattleDate", 21);
            Range(ws, "Z1", "PlacedDate", 21);
            Range(ws, "AA1", "Odds", 11);
            Range(ws, "AB1", "Points", 11);
            Range(ws, "AC1", "Visitor Score", 17);
            Range(ws, "AD1", "Home Score", 17);
            Range(ws, "AE1", "IP", 19);
        }



        private void ShowInfo(Excel.Worksheet ws, int posY, int i, ObservableCollection<csBet> l, int color, csGame g)
        {
            System.Drawing.Color coloring = System.Drawing.Color.FromArgb(198, 239, 206);

            if (color == 1) coloring = System.Drawing.Color.FromArgb(198, 239, 206);
            else if (color == 2) coloring = System.Drawing.Color.FromArgb(255, 199, 206);
            else if (color == 3) coloring = System.Drawing.Color.FromArgb(255, 235, 156);

            ws.Range["A" + (posY + 2).ToString()].Value = g.VisitorTeam + " vs " + g.HomeTeam;
            ws.Range["B" + (posY + 2).ToString()].Value = g.EventDate;
            ws.Range["C" + (posY + 2).ToString()].Value = l[i].IdAgent;
            ws.Range["D" + (posY + 2).ToString()].Value = l[i].Agent;
            ws.Range["E" + (posY + 2).ToString()].Value = l[i].IdPlayer;
            ws.Range["F" + (posY + 2).ToString()].Value = l[i].Player;
            ws.Range["G" + (posY + 2).ToString()].Value = l[i].IdLineType;
            ws.Range["H" + (posY + 2).ToString()].Value = l[i].LineTypeName;
            ws.Range["I" + (posY + 2).ToString()].Value = l[i].LoginName;
            ws.Range["J" + (posY + 2).ToString()].Value = l[i].WinAmount;
            ws.Range["K" + (posY + 2).ToString()].Value = l[i].RiskAmount;
            ws.Range["L" + (posY + 2).ToString()].Value = l[i].Result;
            ws.Range["M" + (posY + 2).ToString()].Value = l[i].Net;
            ws.Range["N" + (posY + 2).ToString()].Value = l[i].GamePeriod;
            ws.Range["O" + (posY + 2).ToString()].Value = l[i].League;
            ws.Range["P" + (posY + 2).ToString()].Value = l[i].CompleteDescription;
            ws.Range["Q" + (posY + 2).ToString()].Value = l[i].DetailDescription;
            ws.Range["R" + (posY + 2).ToString()].Value = l[i].IdGame;
            ws.Range["S" + (posY + 2).ToString()].Value = l[i].IdLeague;
            ws.Range["T" + (posY + 2).ToString()].Value = l[i].Period;
            ws.Range["U" + (posY + 2).ToString()].Value = l[i].FAV_DOG;
            ws.Range["V" + (posY + 2).ToString()].Value = l[i].Play;
            ws.Range["W" + (posY + 2).ToString()].Value = l[i].WagerPlay;
            ws.Range["X" + (posY + 2).ToString()].Value = l[i].IdSport;
            ws.Range["Y" + (posY + 2).ToString()].Value = l[i].SettledDate;
            ws.Range["Z" + (posY + 2).ToString()].Value = l[i].PlacedDate;
            ws.Range["AA" + (posY + 2).ToString()].Value = l[i].Odds;
            ws.Range["AB" + (posY + 2).ToString()].Value = l[i].Points;
            ws.Range["AC" + (posY + 2).ToString()].Value = l[i].VisitorScore;
            ws.Range["AD" + (posY + 2).ToString()].Value = l[i].HomeScore;
            ws.Range["AE" + (posY + 2).ToString()].Value = l[i].IP;


            ws.Range["A" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["B" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["C" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["D" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["E" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["F" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["G" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["H" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["I" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["J" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["K" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["L" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["M" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["N" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["O" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["P" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["Q" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["R" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["S" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["T" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["U" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["V" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["W" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["X" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["Y" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["Z" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["AA" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["AB" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["AC" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["AD" + (posY + 2).ToString()].Interior.Color = coloring;
            ws.Range["AE" + (posY + 2).ToString()].Interior.Color = coloring;



            if (color == 3)
            {
                Bold(ws, "A" + (posY + 2).ToString());
                Bold(ws, "B" + (posY + 2).ToString());
                Bold(ws, "C" + (posY + 2).ToString());
                Bold(ws, "D" + (posY + 2).ToString());
                Bold(ws, "E" + (posY + 2).ToString());
                Bold(ws, "F" + (posY + 2).ToString());
                Bold(ws, "G" + (posY + 2).ToString());
                Bold(ws, "H" + (posY + 2).ToString());
                Bold(ws, "I" + (posY + 2).ToString());
                Bold(ws, "J" + (posY + 2).ToString());
                Bold(ws, "K" + (posY + 2).ToString());
                Bold(ws, "L" + (posY + 2).ToString());
                Bold(ws, "M" + (posY + 2).ToString());
                Bold(ws, "N" + (posY + 2).ToString());
                Bold(ws, "O" + (posY + 2).ToString());
                Bold(ws, "P" + (posY + 2).ToString());
                Bold(ws, "Q" + (posY + 2).ToString());
                Bold(ws, "R" + (posY + 2).ToString());
                Bold(ws, "S" + (posY + 2).ToString());
                Bold(ws, "T" + (posY + 2).ToString());
                Bold(ws, "U" + (posY + 2).ToString());
                Bold(ws, "V" + (posY + 2).ToString());
                Bold(ws, "W" + (posY + 2).ToString());
                Bold(ws, "X" + (posY + 2).ToString());
                Bold(ws, "Y" + (posY + 2).ToString());
                Bold(ws, "Z" + (posY + 2).ToString());
                Bold(ws, "AA" + (posY + 2).ToString());
                Bold(ws, "AB" + (posY + 2).ToString());
                Bold(ws, "AC" + (posY + 2).ToString());
                Bold(ws, "AD" + (posY + 2).ToString());
                Bold(ws, "AE" + (posY + 2).ToString());
            }
        }


        private void CenterValue(Excel.Worksheet ws, string letter, int i)
        {
            ws.Range[letter + (i + 2).ToString()].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        }


        private void CenterAllInfo(Excel.Worksheet ws, int i)
        {
            CenterValue(ws, "A", i);
            CenterValue(ws, "B", i);
            CenterValue(ws, "C", i);
            CenterValue(ws, "D", i);
            CenterValue(ws, "E", i);
            CenterValue(ws, "F", i);
            CenterValue(ws, "G", i);
            CenterValue(ws, "H", i);
            CenterValue(ws, "I", i);
            CenterValue(ws, "J", i);
            CenterValue(ws, "K", i);
            CenterValue(ws, "L", i);
            CenterValue(ws, "M", i);
            CenterValue(ws, "N", i);
            CenterValue(ws, "O", i);
            CenterValue(ws, "P", i);
            CenterValue(ws, "Q", i);
            CenterValue(ws, "R", i);
            CenterValue(ws, "S", i);
            CenterValue(ws, "T", i);
            CenterValue(ws, "U", i);
            CenterValue(ws, "V", i);
            CenterValue(ws, "W", i);
            CenterValue(ws, "X", i);
            CenterValue(ws, "Y", i);
            CenterValue(ws, "Z", i);
            CenterValue(ws, "AA", i);
            CenterValue(ws, "AB", i);
            CenterValue(ws, "AC", i);
            CenterValue(ws, "AD", i);
            CenterValue(ws, "AE", i);
        }



        // *************** SPACE TO CRETE THE FINAL SHEET, ORDER THE INFORMATION AS SUMMARY ******************* //

        private void CompleteRangeSport(Excel.Worksheet ws)
        {
            RangeSport(ws, "A1", "Sport", 12,false);
            RangeSport(ws, "B1", "Risk", 17,false);
            RangeSport(ws, "C1", "Net", 17,false);
            RangeSport(ws, "D1", "Total Bets", 12,false);
            RangeSport(ws, "E1", "Win Bets", 12,false);
            RangeSport(ws, "F1", "Lost Bets", 12,false);
            RangeSport(ws, "G1", "Win %", 10,false);

            RangeSport(ws, "I1", "Risk", 17);
            RangeSport(ws, "J1", "Net", 17);
            RangeSport(ws, "K1", "Total Bets", 12);
            RangeSport(ws, "L1", "Win Bets", 12);
            RangeSport(ws, "M1", "Lost Bets", 12);
            RangeSport(ws, "N1", "Win %", 10);
        }

        private void ShowInfoSport(Excel.Worksheet ws, int posY, string sport, csSummary s)
        {
            ws.Range["A" + (posY + 2).ToString()].Value = sport;
            ws.Range["B" + (posY + 2).ToString()].Value = s.RiskLeans;
            ws.Range["C" + (posY + 2).ToString()].Value = s.NetLeans;
            ws.Range["D" + (posY + 2).ToString()].Value = s.ContLeansBets;
            ws.Range["E" + (posY + 2).ToString()].Value = s.WinBetsLeans;
            ws.Range["F" + (posY + 2).ToString()].Value = s.LostBetsLeans;
            ws.Range["G" + (posY + 2).ToString()].Value = s.WinPerLeans + "%";


            ws.Range["I" + (posY + 2).ToString()].Value = s.RiskNoLeans;
            ws.Range["J" + (posY + 2).ToString()].Value = s.NetNoLeans;
            ws.Range["K" + (posY + 2).ToString()].Value = s.ContNoLeansBets;
            ws.Range["L" + (posY + 2).ToString()].Value = s.WinBetsNoLeans;
            ws.Range["M" + (posY + 2).ToString()].Value = s.LostBetsNoLeans;
            ws.Range["N" + (posY + 2).ToString()].Value = s.WinPerNoLeans + "%";


            ws.Range["A" + (posY + 2).ToString()].BorderAround2();
            ws.Range["B" + (posY + 2).ToString()].BorderAround2();
            ws.Range["C" + (posY + 2).ToString()].BorderAround2();
            ws.Range["D" + (posY + 2).ToString()].BorderAround2();
            ws.Range["E" + (posY + 2).ToString()].BorderAround2();
            ws.Range["F" + (posY + 2).ToString()].BorderAround2();
            ws.Range["G" + (posY + 2).ToString()].BorderAround2();
            ws.Range["I" + (posY + 2).ToString()].BorderAround2();
            ws.Range["J" + (posY + 2).ToString()].BorderAround2();
            ws.Range["K" + (posY + 2).ToString()].BorderAround2();
            ws.Range["L" + (posY + 2).ToString()].BorderAround2();
            ws.Range["M" + (posY + 2).ToString()].BorderAround2();
            ws.Range["N" + (posY + 2).ToString()].BorderAround2();

        }



        private csSummary GetValuesFromSummary(string sport, ObservableCollection<csBet> LBets, ObservableCollection<csBet> NoLBets)
        {
            csSummary s = new csSummary();
            s.sport = sport;

            if (LBets != null)
            {
                foreach (var i in LBets)
                {

                    if (i.IdSport == sport)
                    {
                        s.ContLeansBets += 1;
                        s.NetLeans += i.Net;
                        s.WinLeans += i.WinAmount;
                        s.RiskLeans += i.RiskAmount;
                        s.sport = sport;

                        if (i.Result.Trim(' ') == "WIN") s.WinBetsLeans += 1;
                        else s.LostBetsLeans += 1;
                       
                    }
                }

                try{s.WinPerLeans = (s.WinBetsLeans * 100 / s.ContLeansBets);}catch (Exception){s.WinPerLeans = 0;}
            }



            if (NoLBets != null)
            {
                foreach (var i in NoLBets)
                {

                    if (i.IdSport == sport)
                    {
                        s.ContNoLeansBets += 1;
                        s.NetNoLeans += i.Net;
                        s.WinNoLeans += i.WinAmount;
                        s.RiskNoLeans += i.RiskAmount;

                        if (i.Result.Trim(' ') == "WIN") s.WinBetsNoLeans += 1;
                        else s.LostBetsNoLeans += 1;
                    }
                }

                try { s.WinPerNoLeans = (s.WinBetsNoLeans * 100 / s.ContNoLeansBets); } catch (Exception) { s.WinPerNoLeans = 0; }

            }

            return s;
        }



        private void CenterAllInfoSport(Excel.Worksheet ws, int i)
        {
            CenterValue(ws, "A", i);
            CenterValue(ws, "B", i);
            CenterValue(ws, "C", i);
            CenterValue(ws, "D", i);
            CenterValue(ws, "E", i);
            CenterValue(ws, "F", i);
            CenterValue(ws, "G", i);
            CenterValue(ws, "H", i);
            CenterValue(ws, "I", i);
            CenterValue(ws, "J", i);
            CenterValue(ws, "K", i);
            CenterValue(ws, "L", i);
            CenterValue(ws, "M", i);
            CenterValue(ws, "N", i);
            CenterValue(ws, "O", i);
            CenterValue(ws, "P", i);
            CenterValue(ws, "Q", i);
        }


        // ******************* last sheet filer by game ***************************** //


        private void CompleteRangeGame(Excel.Worksheet ws, string player)
        {
            Range(ws, "A1", "Game", 42);
            Range(ws, "B1", player, 50);
            Range(ws, "C1", player +" Line", 42);
            Range(ws, "D1", "Cris", 42);
            Range(ws, "E1", "Pinnacle", 42);
            Range(ws, "F1", "Next Line", 42);
            Range(ws, "G1", "Time", 23);
            Range(ws, "H1", "Sport", 10);
            Range(ws, "I1", "Bets", 9);
            Range(ws, "J1", "Win Amo", 14);
            Range(ws, "K1", "Risk Amo", 12);
            Range(ws, "L1", "Net", 8);
        }

        private void ShowInfoGame(Excel.Worksheet ws, int posY, csSummary s)
        {
            ws.Range["A" + (posY + 2).ToString()].Value = s.Event;
            ws.Range["B" + (posY + 2).ToString()].Value = s.Team;
            ws.Range["C" + (posY + 2).ToString()].Value = s.Line;
            ws.Range["D" + (posY + 2).ToString()].Value = s.CrisLine;
            ws.Range["E" + (posY + 2).ToString()].Value = s.PinniLine;
            ws.Range["F" + (posY + 2).ToString()].Value = s.OurLine;
            ws.Range["G" + (posY + 2).ToString()].Value = s.Time;
            ws.Range["H" + (posY + 2).ToString()].Value = s.sport;
            ws.Range["I" + (posY + 2).ToString()].Value = s.TotalBets;
            ws.Range["J" + (posY + 2).ToString()].Value = s.WinAmount;
            ws.Range["K" + (posY + 2).ToString()].Value = s.RiskAmount;
            ws.Range["L" + (posY + 2).ToString()].Value = s.Net;

            ws.Range["A" + (posY + 2).ToString()].BorderAround2();
            ws.Range["B" + (posY + 2).ToString()].BorderAround2();
            ws.Range["C" + (posY + 2).ToString()].BorderAround2();
            ws.Range["D" + (posY + 2).ToString()].BorderAround2();
            ws.Range["E" + (posY + 2).ToString()].BorderAround2();
            ws.Range["F" + (posY + 2).ToString()].BorderAround2();
            ws.Range["G" + (posY + 2).ToString()].BorderAround2();
            ws.Range["H" + (posY + 2).ToString()].BorderAround2();
            ws.Range["I" + (posY + 2).ToString()].BorderAround2();
            ws.Range["J" + (posY + 2).ToString()].BorderAround2();
            ws.Range["K" + (posY + 2).ToString()].BorderAround2();
            ws.Range["L" + (posY + 2).ToString()].BorderAround2();

            ws.Range["L" + (posY + 2).ToString()].Interior.Color = (s.Net < 0) ? System.Drawing.Color.FromArgb(255, 199, 206) : System.Drawing.Color.FromArgb(198, 239, 206);
            ws.Range["L" + (posY + 2).ToString()].Borders.Color = (s.Net < 0) ? System.Drawing.Color.FromArgb(156, 0, 6) : System.Drawing.Color.FromArgb(0, 97, 0);
        }



        private void CenterAllInfoGame(Excel.Worksheet ws, int i)
        {
            CenterValue(ws, "A", i);
            CenterValue(ws, "B", i);
            CenterValue(ws, "C", i);
            CenterValue(ws, "D", i);
            CenterValue(ws, "E", i);
            CenterValue(ws, "F", i);
            CenterValue(ws, "G", i);
            CenterValue(ws, "H", i);
            CenterValue(ws, "I", i);
            CenterValue(ws, "J", i);
            CenterValue(ws, "K", i);
            CenterValue(ws, "L", i);
        }


        // General Information

        private void ShowInfoGeneral(Excel.Worksheet ws, int posY, csSummary s)
        {
            ws.Range["A" + (posY).ToString()].Value = s.ContLeansBets;
            ws.Range["B" + (posY).ToString()].Value = s.RiskLeans;
            ws.Range["C" + (posY).ToString()].Value = s.NetLeans;
            ws.Range["D" + (posY).ToString()].Value = s.WinPerLeans + "%";
            ws.Range["E" + (posY).ToString()].Value = (Int32)s.LeansHold + "%";

            ws.Range["A" + (posY).ToString()].BorderAround2();
            ws.Range["B" + (posY).ToString()].BorderAround2();
            ws.Range["C" + (posY).ToString()].BorderAround2();
            ws.Range["D" + (posY).ToString()].BorderAround2();
            ws.Range["E" + (posY).ToString()].BorderAround2();


            // AGAINST LEANS
            ws.Range["I" + (posY).ToString()].Value = s.ContNoLeansBets;
            ws.Range["J" + (posY).ToString()].Value = s.RiskNoLeans;
            ws.Range["K" + (posY).ToString()].Value = s.NetNoLeans;
            ws.Range["L" + (posY).ToString()].Value = s.WinPerNoLeans + "%";
            ws.Range["M" + (posY).ToString()].Value = (Int32)s.NoLeansHold + "%";

            ws.Range["I" + (posY).ToString()].BorderAround2();
            ws.Range["J" + (posY).ToString()].BorderAround2();
            ws.Range["K" + (posY).ToString()].BorderAround2();
            ws.Range["L" + (posY).ToString()].BorderAround2();
            ws.Range["M" + (posY).ToString()].BorderAround2();

        }



        private void RangeGeneral(Excel.Worksheet ws, int posY)
        {
            RangeGeneral(ws, "A" + posY.ToString(), "Bets", false);
            RangeGeneral(ws, "B" + posY.ToString(), "Risk Amount", false);
            RangeGeneral(ws, "C" + posY.ToString(), "Net", false);
            RangeGeneral(ws, "D" + posY.ToString(), "Win %", false);
            RangeGeneral(ws, "E" + posY.ToString(), "Hold %", false);

            RangeGeneral(ws, "I" + posY.ToString(), "Bets", true);
            RangeGeneral(ws, "J" + posY.ToString(), "Risk Amount", true);
            RangeGeneral(ws, "K" + posY.ToString(), "Net", true);
            RangeGeneral(ws, "L" + posY.ToString(), "Win %", true);
            RangeGeneral(ws, "M" + posY.ToString(), "Hold %", true);
        }




        //By player
        private void ShowPlayer(Excel.Worksheet ws, int posY, csSummary s)
        {
            ws.Range["A" + (posY + 2).ToString()].Value = s.Player;
            ws.Range["B" + (posY + 2).ToString()].Value = s.RiskLeans;
            ws.Range["C" + (posY + 2).ToString()].Value = s.NetLeans;
            ws.Range["D" + (posY + 2).ToString()].Value = s.ContLeansBets;
            ws.Range["E" + (posY + 2).ToString()].Value = s.WinBetsLeans;
            ws.Range["F" + (posY + 2).ToString()].Value = s.LostBetsLeans;
            ws.Range["G" + (posY + 2).ToString()].Value = s.WinPerLeans + "%";
            ws.Range["H" + (posY + 2).ToString()].Value = (Int32)s.LeansHold + "%";


            ws.Range["J" + (posY + 2).ToString()].Value = s.Player;
            ws.Range["K" + (posY + 2).ToString()].Value = s.RiskNoLeans;
            ws.Range["L" + (posY + 2).ToString()].Value = s.NetNoLeans;
            ws.Range["M" + (posY + 2).ToString()].Value = s.ContNoLeansBets;
            ws.Range["N" + (posY + 2).ToString()].Value = s.WinBetsNoLeans;
            ws.Range["O" + (posY + 2).ToString()].Value = s.LostBetsNoLeans;
            ws.Range["P" + (posY + 2).ToString()].Value = s.WinPerNoLeans + "%";
            ws.Range["Q" + (posY + 2).ToString()].Value = (Int32)s.NoLeansHold + "%";


            ws.Range["A" + (posY + 2).ToString()].BorderAround2();
            ws.Range["B" + (posY + 2).ToString()].BorderAround2();
            ws.Range["C" + (posY + 2).ToString()].BorderAround2();
            ws.Range["D" + (posY + 2).ToString()].BorderAround2();
            ws.Range["E" + (posY + 2).ToString()].BorderAround2();
            ws.Range["F" + (posY + 2).ToString()].BorderAround2();
            ws.Range["G" + (posY + 2).ToString()].BorderAround2();
            ws.Range["H" + (posY + 2).ToString()].BorderAround2();
            ws.Range["J" + (posY + 2).ToString()].BorderAround2();
            ws.Range["K" + (posY + 2).ToString()].BorderAround2();
            ws.Range["L" + (posY + 2).ToString()].BorderAround2();
            ws.Range["M" + (posY + 2).ToString()].BorderAround2();
            ws.Range["N" + (posY + 2).ToString()].BorderAround2();
            ws.Range["O" + (posY + 2).ToString()].BorderAround2();
            ws.Range["P" + (posY + 2).ToString()].BorderAround2();
            ws.Range["Q" + (posY + 2).ToString()].BorderAround2();
        }


        private void CompleteRangePlayer(Excel.Worksheet ws,int posY)
        {
            RangeSport(ws, "A" + posY.ToString(), "Player", 12, false);
            RangeSport(ws, "B" + posY.ToString(), "Risk", 17, false);
            RangeSport(ws, "C" + posY.ToString(), "Net", 17, false);
            RangeSport(ws, "D" + posY.ToString(), "Total Bets", 12, false);
            RangeSport(ws, "E" + posY.ToString(), "Win Bets", 12, false);
            RangeSport(ws, "F" + posY.ToString(), "Lost Bets", 12, false);
            RangeSport(ws, "G" + posY.ToString(), "Win %", 10, false);
            RangeSport(ws, "H" + posY.ToString(), "Hold %", 10, false);

            RangeSport(ws, "J" + posY.ToString(), "Player", 12);
            RangeSport(ws, "K" + posY.ToString(), "Risk", 17);
            RangeSport(ws, "L" + posY.ToString(), "Net", 17);
            RangeSport(ws, "M" + posY.ToString(), "Total Bets", 12);
            RangeSport(ws, "N" + posY.ToString(), "Win Bets", 12);
            RangeSport(ws, "O" + posY.ToString(), "Lost Bets", 12);
            RangeSport(ws, "P" + posY.ToString(), "Win %", 10);
            RangeSport(ws, "Q" + posY.ToString(), "Hold %", 10);
        }


        private void CenterAllInfoGeneral(Excel.Worksheet ws, int i)
        {
            CenterValue(ws, "A", i);
            CenterValue(ws, "B", i);
            CenterValue(ws, "C", i);
            CenterValue(ws, "D", i);
            CenterValue(ws, "E", i);

            CenterValue(ws, "I", i);
            CenterValue(ws, "J", i);
            CenterValue(ws, "K", i);
            CenterValue(ws, "L", i);
            CenterValue(ws, "M", i);
        }


        private void RangeTab(Excel.Worksheet ws, string txt, int posY)
        {
            RangeNormal(ws, "A" + posY.ToString(), txt, 15);
        }


        // leans table 
        public csBet GenerateExcel(ObservableCollection<csGame> Games, string player, csBet  Bet)
        {
            ObservableCollection<csBet> Sports = new ObservableCollection<csBet>();
            ObservableCollection<csGame> SportsAux = new ObservableCollection<csGame>();

            try
            {
                if (Games != null)
                {
                    Bet = new csBet();
                    var sport = new csBet();

                    foreach (var g in Games)
                    {
                        ObservableCollection<csBet> LeansBets = betDB.BetListFromIdGame(g, "LEANS",false );
                        csBet GameSum = new csBet();
                        GameSum.Event = g.VisitorTeam + " vs " + g.HomeTeam;
                        if(g.VisitorTeam == "CANUCKS RT")
                        {
                            int asda = 0;
                        }

                        GameSum.Sport = g.IdSport;
                        GameSum.GameDate = g.EventDate.Month + "/" + g.EventDate.Day + "/" + g.EventDate.Year;


                        if (LeansBets != null)
                        {
                            foreach (var l in LeansBets)
                            {
                                ObservableCollection<csBet> bets = betDB.GetBetsAfterLeans(g.IdGame, l, l.WagerPlay, player,2);
                                GameSum.Line += l.Odds + ", " + l.OverUnder + l.Points;
                                GameSum.CrisLine += l.CrisJuice + ", " + l.OverUnder + l.CrisPoints;
                                GameSum.PinniLine += l.PinniJuice + ", " + l.OverUnder + l.PinniPoints;
                                GameSum.OurLine += l.OurNextLine;
                                GameSum.Team += (l.Rot == g.HomeNumber) ? g.HomeTeam : g.VisitorTeam;
                                GameSum.WagerPlay = l.WagerPlay;
                                l.GameDate = GameSum.GameDate;

                                //LeansBetsList.Add(l);
                                //L.Add(l);

                                if (bets != null && bets.Count > 0)
                                {
                                    foreach (var b in bets)
                                    {
                                        // **************************** Aqui es donde hay que generar el excel ***********************
                                        b.EventDate = g.EventDate;
                                        b.EventName = g.VisitorTeam + " vs " + g.HomeTeam;
                                        b.GameDate = GameSum.GameDate;

                                        try
                                        {
                                            if (b.Rot == l.Rot)
                                            {
                                                //Same team with LEANS
                                                b.Pick = (g.VisitorNumber == b.Rot) ? g.VisitorTeam : g.HomeTeam;

                                                GameSum.ContLeansBets += 1;
                                                GameSum.WinLeans += b.WinAmount;
                                                GameSum.RiskLeans += b.RiskAmount;
                                                GameSum.NetLeans += b.Net;

                                                //BetsWithLeans.Add(b);
                                                Bet.Risk_wADJ += b.RiskAmount;
                                                Bet.Net_wADJ += b.Net;
                                                Bet.LinesPlayed_wADJ += 1;

                                                if(b.FAV_DOG.ToUpper().Contains("FAV"))
                                                {
                                                    Bet.Fav_wADJ += 1;
                                                }else if(b.FAV_DOG.ToUpper().Contains("DOG"))
                                                {
                                                    Bet.Dog_wADJ += 1;
                                                }


                                                summary.ContLeansBets += 1;
                                            }
                                            else
                                            {
                                                //Different team with LEANS
                                                b.Pick = (g.VisitorNumber == b.Rot) ? g.VisitorTeam : g.HomeTeam;

                                                GameSum.ContNoLeansBets += 1;
                                                GameSum.WinNoLeans += b.WinAmount;
                                                GameSum.RiskNoLeans += b.RiskAmount;
                                                GameSum.NetNoLeans += b.Net;

                                                //NoLeansBets.Add(b);
                                                summary.ContNoLeansBets += 1;


                                                Bet.Risk_aADJ += b.RiskAmount;
                                                Bet.Net_aADJ += b.Net;
                                                Bet.LinesPlayed_aADJ += 1;

                                                if (b.FAV_DOG.ToUpper().Contains("FAV"))
                                                {
                                                    Bet.Fav_aADJ += 1;
                                                }
                                                else if (b.FAV_DOG.ToUpper().Contains("DOG"))
                                                {
                                                    Bet.Dog_aADJ += 1;
                                                }
                                            }
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                }
                            }
                        }

                        GameSum.LeansHold = Math.Round(Convert.ToDouble((GameSum.NetLeans * 100) / GameSum.RiskLeans), 2, MidpointRounding.AwayFromZero);
                        GameSum.NoLeansHold = Math.Round(Convert.ToDouble((GameSum.NetNoLeans * 100) / GameSum.RiskNoLeans), 2, MidpointRounding.AwayFromZero);

                        //SumGames.Add(GameSum);

                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return Bet;
        }
    }
}
