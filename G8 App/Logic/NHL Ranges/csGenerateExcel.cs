using NHL_BL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace NHL_BL.Logic
{
    public class csGenerateExcel
    {
        private csGroupTeam groupTeams = new csGroupTeam();
        private int lastRow = 3;
        private int posX = -1;

        private void GruopTeams(string name, string side, string league)
        {
            if(name != null && name != "")
            {
                csTeamRange TR = new csTeamRange(name, league, side, 0, 0,0);
                groupTeams.AddNewTeam(TR);
            }
        }


        private void Range(Excel.Worksheet ws, string cell, string txt, int size)
        {
            ws.Range[cell].Value = txt;
            ws.Range[cell].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ws.Range[cell].ColumnWidth = size;
            ws.Range[cell].BorderAround2();
            ws.Range[cell].Font.Bold = true;
            ws.Range[cell].Interior.Color = System.Drawing.Color.FromArgb(179, 179, 204);
            ws.Range[cell].Font.Size = 14;
        }

        private void CenterValue(Excel.Worksheet ws,string letter,int i)
        {
            ws.Range[letter + (i + 2).ToString()].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        }

        public void GenerateExcel(ObservableCollection<csReportNHL> OT, ObservableCollection<csReportNHL> RT, ObservableCollection<csReportNHL> Alternative, ObservableCollection<csReportNHL> Goal, string path)
        {
            try
            {
                Excel.Application app = new Excel.Application();
              //  app.Visible = true;
                //app.WindowState = Excel.XlWindowState.xlMaximized;
                Excel.Workbook wb = app.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                csRangos R = new csRangos();

              // ************************************** HOME RT  RAW **************************************
                if (RT != null)
                {       
                    Excel.Worksheet ws = wb.Worksheets[1];
                    ws.Name = "HOME RT RAW";

                    Range(ws,"A1", "Game Date Time",25);
                    Range(ws,"B1", "Home Team", 25);
                    Range(ws,"C1", "Home Score", 15);
                    Range(ws,"D1", "Visitor Score", 15);
                    Range(ws,"E1", "Coverage", 14);
                    Range(ws,"F1", "Winner", 12);
                    Range(ws,"G1", "Home Special", 18);
                    Range(ws,"H1", "Home Odds", 15);
                    Range(ws,"I1", "Home Special Odds", 23);
                    Range(ws, "J1", "Opening Special", 23);
                    Range(ws, "K1", "Opening Money Line", 28);
                    Range(ws, "L1", "Opening Juice", 26);


                    for (int i = 0; i < RT.Count; i++)
                    {
                        GruopTeams(RT[i].HomeTeam, "HOME", "RT");
                        GruopTeams(RT[i].VisitorTeam, "VISITOR", "RT");

                        ws.Range["A" + (i + 2).ToString()].Value = RT[i].GameDateTime.ToString();
                        ws.Range["B" + (i + 2).ToString()].Value = RT[i].HomeTeam;
                        ws.Range["C" + (i + 2).ToString()].Value = RT[i].HomeScore;
                        ws.Range["D" + (i + 2).ToString()].Value = RT[i].VisitorScore;

                        if (RT[i].HomeSpecial != null) ws.Range["E" + (i + 2).ToString()].Value = RT[i].Coverage = (RT[i].VisitorScore < RT[i].HomeScore + RT[i].HomeSpecial) ? "WIN" : "LOSS";
                        ws.Range["F" + (i + 2).ToString()].Value = RT[i].WINNER;
                        ws.Range["G" + (i + 2).ToString()].Value = RT[i].HomeSpecial;
                        ws.Range["G" + (i + 2).ToString()].NumberFormat = "0.00";
                        ws.Range["H" + (i + 2).ToString()].Value = RT[i].HomeOdds;
                        ws.Range["I" + (i + 2).ToString()].Value = RT[i].HomeSpecialOdds;

                        ws.Range["J" + (i + 2).ToString()].Value = RT[i].OpeningHomeSpecial;
                        ws.Range["K" + (i + 2).ToString()].Value = RT[i].OpeningHomeOdds;
                        ws.Range["L" + (i + 2).ToString()].Value = RT[i].OpeningHomeSpecialOdds;

                        CenterValue(ws, "A",i);
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


                        ws.Range["F" + (i + 2).ToString()].Interior.Color = (RT[i].WINNER != "HOME") ? System.Drawing.Color.FromArgb(255, 199, 206) : System.Drawing.Color.FromArgb(198, 239, 206);
                        ws.Range["F" + (i + 2).ToString()].Borders.Color = (RT[i].WINNER != "HOME") ? System.Drawing.Color.FromArgb(156, 0, 6) : System.Drawing.Color.FromArgb(0, 97, 0);


                             if (RT[i].HomeOdds <= -100 && RT[i].HomeOdds >= -140) R.HomeRT_EvenToMenos140_ML.Add(RT[i]);
                        else if (RT[i].HomeOdds <= -141 && RT[i].HomeOdds >= -180) R.HomeRT_Menos141ToMenos180_ML.Add(RT[i]);
                        else if (RT[i].HomeOdds <= -181 && RT[i].HomeOdds >= -220) R.HomeRT_Menos181ToMenos220_ML.Add(RT[i]);
                        else if (RT[i].HomeOdds <= -221 && RT[i].HomeOdds >= -260) R.HomeRT_Menos221ToMenos260_ML.Add(RT[i]);
                        else if (RT[i].HomeOdds <= -261 && RT[i].HomeOdds >= -300) R.HomeRT_Menos261ToMenos300_ML.Add(RT[i]);
                        else if (RT[i].HomeOdds <= -301 && RT[i].HomeOdds >= -340) R.HomeRT_Menos301ToMenos340_ML.Add(RT[i]);
                        else if (RT[i].HomeOdds <= -341 && RT[i].HomeOdds >= -380) R.HomeRT_Menos301ToMenos340_ML.Add(RT[i]);
                        else if (RT[i].HomeOdds <= -381 && RT[i].HomeOdds >= -420) R.HomeRT_Menos381ToMenos420_ML.Add(RT[i]);
                        else if (RT[i].HomeOdds <= -421 && RT[i].HomeOdds >= -460) R.HomeRT_Menos421ToMenos460_ML.Add(RT[i]);
                        else if (RT[i].HomeOdds <= -461 && RT[i].HomeOdds >= -500) R.HomeRT_Menos461ToMenos500_ML.Add(RT[i]);
                        //else if (RT[i].HomeOdds <= -100 && RT[i].HomeOdds > -140) R.HomeOT_EvenToMenos140.Add(RT[i]);

                             if (RT[i].HomeOdds >= 100 && RT[i].HomeOdds <= 140) R.HomeRT_EvenToPlus140_ML.Add(RT[i]);
                        else if (RT[i].HomeOdds >= 141 && RT[i].HomeOdds <= 180) R.HomeRT_Plus141ToPlus180_ML.Add(RT[i]);
                        else if (RT[i].HomeOdds >= 181 && RT[i].HomeOdds <= 220) R.HomeRT_Plus181ToPlus220_ML.Add(RT[i]);
                        else if (RT[i].HomeOdds >= 221 && RT[i].HomeOdds <= 260) R.HomeRT_Plus221ToPlus260_ML.Add(RT[i]);
                        else if (RT[i].HomeOdds >= 261 && RT[i].HomeOdds <= 300) R.HomeRT_Plus261ToPlus300_ML.Add(RT[i]);
                        else if (RT[i].HomeOdds >= 301 && RT[i].HomeOdds <= 340) R.HomeRT_Plus301ToPlus340_ML.Add(RT[i]);
                        else if (RT[i].HomeOdds >= 341 && RT[i].HomeOdds <= 380) R.HomeRT_Plus341ToPlus380_ML.Add(RT[i]);
                        else if (RT[i].HomeOdds >= 381 && RT[i].HomeOdds <= 420) R.HomeRT_Plus381ToPlus420_ML.Add(RT[i]);
                        else if (RT[i].HomeOdds >= 421 && RT[i].HomeOdds <= 460) R.HomeRT_Plus421ToPlus460_ML.Add(RT[i]);
                        else if (RT[i].HomeOdds >= 461 && RT[i].HomeOdds <= 500) R.HomeRT_Plus461ToPlus500_ML.Add(RT[i]);


                        if(RT[i].HomeSpecial == 0.50)
                        {
                            R.HomeRT_05.Add(RT[i]);
                        }else if(RT[i].HomeSpecial == -0.50)
                        {
                            R.HomeRT_Menos05.Add(RT[i]);
                        }

                    }

                   // (sender as BackgroundWorker).ReportProgress(9);
                    //*****************************************************************************************************
                    //******************************************** SHEET 2 *******************************************************

                    Excel.Worksheet ws2 = wb.Sheets.Add();
                    ws2.Name = "VISITOR RT RAW";

                    Range(ws2, "A1", "Game Date Time", 25);
                    Range(ws2, "B1", "Visitor Team", 25);
                    Range(ws2, "C1", "Home Score", 15);
                    Range(ws2, "D1", "Visitor Score", 15);
                    Range(ws2, "E1", "Coverage", 14);
                    Range(ws2, "F1", "Winner", 12);
                    Range(ws2, "G1", "Visitor Special", 22);
                    Range(ws2, "H1", "Visitor Odds", 20);
                    Range(ws2, "I1", "Visitor Special Odds", 23);
                    Range(ws2, "J1", "Opening Special", 23);
                    Range(ws2, "K1", "Opening Money Line", 28);
                    Range(ws2, "L1", "Opening Juice", 26);


                    for (int i = 0; i < RT.Count; i++)
                    {
                        GruopTeams(RT[i].VisitorTeam, "VISITOR", "RT");
                        GruopTeams(RT[i].HomeTeam, "HOME", "RT");

                        ws2.Range["A" + (i + 2).ToString()].Value = RT[i].GameDateTime.ToString();
                        ws2.Range["B" + (i + 2).ToString()].Value = RT[i].VisitorTeam;
                        ws2.Range["C" + (i + 2).ToString()].Value = RT[i].HomeScore;
                        ws2.Range["D" + (i + 2).ToString()].Value = RT[i].VisitorScore;
                        if (RT[i].VisitorSpecial != null) ws2.Range["E" + (i + 2).ToString()].Value = RT[i].Coverage = (RT[i].HomeScore < RT[i].VisitorScore + RT[i].VisitorSpecial) ? "WIN" : "LOSS";                       
                        ws2.Range["F" + (i + 2).ToString()].Value = RT[i].WINNER;
                        ws2.Range["G" + (i + 2).ToString()].Value = RT[i].VisitorSpecial;
                        ws2.Range["G" + (i + 2).ToString()].NumberFormat = "0.00";
                        ws2.Range["H" + (i + 2).ToString()].Value = RT[i].VisitorOdds;                       
                        ws2.Range["I" + (i + 2).ToString()].Value = RT[i].VisitorSpecialOdds;

                        ws2.Range["J" + (i + 2).ToString()].Value = RT[i].OpeningVisitorSpecial;
                        ws2.Range["K" + (i + 2).ToString()].Value = RT[i].OpeningVisitorOdds;
                        ws2.Range["L" + (i + 2).ToString()].Value = RT[i].OpeningVisitorSpecialOdds;


                        CenterValue(ws2, "A", i);
                        CenterValue(ws2, "B", i);
                        CenterValue(ws2, "C", i);
                        CenterValue(ws2, "D", i);
                        CenterValue(ws2, "E", i);
                        CenterValue(ws2, "F", i);
                        CenterValue(ws2, "G", i);
                        CenterValue(ws2, "H", i);
                        CenterValue(ws2, "I", i);
                        CenterValue(ws2, "J", i);
                        CenterValue(ws2, "K", i);
                        CenterValue(ws2, "L", i);

                        ws2.Range["F" + (i + 2).ToString()].Interior.Color = (RT[i].WINNER != "VISITOR") ? System.Drawing.Color.FromArgb(255, 199, 206) : System.Drawing.Color.FromArgb(198, 239, 206);
                        ws2.Range["F" + (i + 2).ToString()].Borders.Color = (RT[i].WINNER != "VISITOR") ? System.Drawing.Color.FromArgb(156, 0, 6) : System.Drawing.Color.FromArgb(0, 97, 0);


                             if (RT[i].VisitorOdds <= -100 && RT[i].VisitorOdds >= -140) R.VisitorRT_EvenToMenos140_ML.Add(RT[i]);
                        else if (RT[i].VisitorOdds <= -141 && RT[i].VisitorOdds >= -180) R.VisitorRT_Menos141ToMenos180_ML.Add(RT[i]);
                        else if (RT[i].VisitorOdds <= -181 && RT[i].VisitorOdds >= -220) R.VisitorRT_Menos181ToMenos220_ML.Add(RT[i]);
                        else if (RT[i].VisitorOdds <= -221 && RT[i].VisitorOdds >= -260) R.VisitorRT_Menos221ToMenos260_ML.Add(RT[i]);
                        else if (RT[i].VisitorOdds <= -261 && RT[i].VisitorOdds >= -300) R.VisitorRT_Menos261ToMenos300_ML.Add(RT[i]);
                        else if (RT[i].VisitorOdds <= -301 && RT[i].VisitorOdds >= -340) R.VisitorRT_Menos301ToMenos340_ML.Add(RT[i]);
                        else if (RT[i].VisitorOdds <= -341 && RT[i].VisitorOdds >= -380) R.VisitorRT_Menos341ToMenos380_ML.Add(RT[i]);
                        else if (RT[i].VisitorOdds <= -381 && RT[i].VisitorOdds >= -420) R.VisitorRT_Menos381ToMenos420_ML.Add(RT[i]);
                        else if (RT[i].VisitorOdds <= -421 && RT[i].VisitorOdds >= -460) R.VisitorRT_Menos421ToMenos460_ML.Add(RT[i]);
                        else if (RT[i].VisitorOdds <= -461 && RT[i].VisitorOdds >= -500) R.VisitorRT_Menos461ToMenos500_ML.Add(RT[i]);
                        //else if (RT[i].HomeOdds <= -100 && RT[i].HomeOdds > -140) R.HomeOT_EvenToMenos140.Add(RT[i]);

                             if (RT[i].VisitorOdds >= 100 && RT[i].VisitorOdds <= 140) R.VisitorRT_EvenToPlus140_ML.Add(RT[i]);
                        else if (RT[i].VisitorOdds >= 141 && RT[i].VisitorOdds <= 180) R.VisitorRT_Plus141ToPlus180_ML.Add(RT[i]);
                        else if (RT[i].VisitorOdds >= 181 && RT[i].VisitorOdds <= 220) R.VisitorRT_Plus181ToPlus220_ML.Add(RT[i]);
                        else if (RT[i].VisitorOdds >= 221 && RT[i].VisitorOdds <= 260) R.VisitorRT_Plus221ToPlus260_ML.Add(RT[i]);
                        else if (RT[i].VisitorOdds >= 261 && RT[i].VisitorOdds <= 300) R.VisitorRT_Plus261ToPlus300_ML.Add(RT[i]);
                        else if (RT[i].VisitorOdds >= 301 && RT[i].VisitorOdds <= 340) R.VisitorRT_Plus301ToPlus340_ML.Add(RT[i]);
                        else if (RT[i].VisitorOdds >= 341 && RT[i].VisitorOdds <= 380) R.VisitorRT_Plus341ToPlus380_ML.Add(RT[i]);
                        else if (RT[i].VisitorOdds >= 381 && RT[i].VisitorOdds <= 420) R.VisitorRT_Plus381ToPlus420_ML.Add(RT[i]);
                        else if (RT[i].VisitorOdds >= 421 && RT[i].VisitorOdds <= 460) R.VisitorRT_Plus421ToPlus460_ML.Add(RT[i]);
                        else if (RT[i].VisitorOdds >= 461 && RT[i].VisitorOdds <= 500) R.VisitorRT_Plus461ToPlus500_ML.Add(RT[i]);


                        //SP   
                        if (RT[i].VisitorSpecial == 0.50)
                        {
                            R.VisitorRT_05.Add(RT[i]);
                        }
                        else if (RT[i].VisitorSpecial == -0.50)
                        {
                            R.VisitorRT_Menos05.Add(RT[i]);
                        }

                    }

                   // (sender as BackgroundWorker).ReportProgress(18);
                }



                // *********************************************************** OT ************************************************************

                if (OT != null)
                {
                    Excel.Worksheet ws3 = wb.Sheets.Add();
                    ws3.Name = "HOME OT RAW";

                    Range(ws3, "A1", "Game Date Time", 25);
                    Range(ws3, "B1", "Home Team", 25);
                    Range(ws3, "C1", "Home Score", 15);
                    Range(ws3, "D1", "Visitor Score", 15);
                    Range(ws3, "E1", "Coverage", 14);
                    Range(ws3, "F1", "Winner", 12);
                    Range(ws3, "G1", "Home Special", 18);
                    Range(ws3, "H1", "Home Odds", 15);
                    Range(ws3, "I1", "Home Special Odds", 23);
                    Range(ws3, "J1", "Opening Special", 23);
                    Range(ws3, "K1", "Opening Money Line", 28);
                    Range(ws3, "L1", "Opening Juice", 26);



                    for (int i = 0; i < OT.Count; i++)
                    {
                        GruopTeams(OT[i].HomeTeam, "HOME", "OT");
                        GruopTeams(OT[i].VisitorTeam, "VISITOR", "OT");

                        ws3.Range["A" + (i + 2).ToString()].Value = OT[i].GameDateTime.ToString();
                        ws3.Range["B" + (i + 2).ToString()].Value = OT[i].HomeTeam;
                        ws3.Range["C" + (i + 2).ToString()].Value = OT[i].HomeScore;
                        ws3.Range["D" + (i + 2).ToString()].Value = OT[i].VisitorScore;
                        if (OT[i].HomeSpecial != null) ws3.Range["E" + (i + 2).ToString()].Value = OT[i].Coverage = (OT[i].VisitorScore < OT[i].HomeScore + OT[i].HomeSpecial) ? "WIN" : "LOSS";                       
                        ws3.Range["F" + (i + 2).ToString()].Value = OT[i].WINNER;                       
                        ws3.Range["G" + (i + 2).ToString()].Value = OT[i].HomeSpecial;
                        ws3.Range["G" + (i + 2).ToString()].NumberFormat = "0.00";
                        ws3.Range["H" + (i + 2).ToString()].Value = OT[i].HomeOdds;                       
                        ws3.Range["I" + (i + 2).ToString()].Value = OT[i].HomeSpecialOdds;

                        ws3.Range["J" + (i + 2).ToString()].Value = OT[i].OpeningHomeSpecial;
                        ws3.Range["K" + (i + 2).ToString()].Value = OT[i].OpeningHomeOdds;
                        ws3.Range["L" + (i + 2).ToString()].Value = OT[i].OpeningHomeSpecialOdds;


                        CenterValue(ws3, "A", i);
                        CenterValue(ws3, "B", i);
                        CenterValue(ws3, "C", i);
                        CenterValue(ws3, "D", i);
                        CenterValue(ws3, "E", i);
                        CenterValue(ws3, "F", i);
                        CenterValue(ws3, "G", i);
                        CenterValue(ws3, "H", i);
                        CenterValue(ws3, "I", i);
                        CenterValue(ws3, "J", i);
                        CenterValue(ws3, "K", i);
                        CenterValue(ws3, "L", i);

                        ws3.Range["F" + (i + 2).ToString()].Interior.Color = (OT[i].WINNER != "HOME") ? System.Drawing.Color.FromArgb(255, 199, 206) : System.Drawing.Color.FromArgb(198, 239, 206);
                        ws3.Range["F" + (i + 2).ToString()].Borders.Color = (OT[i].WINNER != "HOME") ? System.Drawing.Color.FromArgb(156, 0, 6) : System.Drawing.Color.FromArgb(0, 97, 0);

                        if (OT[i].HomeOdds <= -100 && OT[i].HomeOdds >= -140) R.HomeOT_EvenToMenos140_ML.Add(OT[i]);
                        else if (OT[i].HomeOdds <= -141 && OT[i].HomeOdds >= -180) R.HomeOT_Menos141ToMenos180_ML.Add(OT[i]);
                        else if (OT[i].HomeOdds <= -181 && OT[i].HomeOdds >= -220) R.HomeOT_Menos181ToMenos220_ML.Add(OT[i]);
                        else if (OT[i].HomeOdds <= -221 && OT[i].HomeOdds >= -260) R.HomeOT_Menos221ToMenos260_ML.Add(OT[i]);
                        else if (OT[i].HomeOdds <= -261 && OT[i].HomeOdds >= -300) R.HomeOT_Menos261ToMenos300_ML.Add(OT[i]);
                        else if (OT[i].HomeOdds <= -301 && OT[i].HomeOdds >= -340) R.HomeOT_Menos301ToMenos340_ML.Add(OT[i]);
                        else if (OT[i].HomeOdds <= -341 && OT[i].HomeOdds >= -380) R.HomeOT_Menos301ToMenos340_ML.Add(OT[i]);
                        else if (OT[i].HomeOdds <= -381 && OT[i].HomeOdds >= -420) R.HomeOT_Menos381ToMenos420_ML.Add(OT[i]);
                        else if (OT[i].HomeOdds <= -421 && OT[i].HomeOdds >= -460) R.HomeOT_Menos421ToMenos460_ML.Add(OT[i]);
                        else if (OT[i].HomeOdds <= -461 && OT[i].HomeOdds >= -500) R.HomeOT_Menos461ToMenos500_ML.Add(OT[i]);
                        //else if (RT[i].HomeOdds <= -100 && RT[i].HomeOdds > -140) R.HomeOT_EvenToMenos140.Add(RT[i]);

                        if (OT[i].HomeOdds >= 100 && OT[i].HomeOdds <= 140) R.HomeOT_EvenToPlus140_ML.Add(OT[i]);
                        else if (OT[i].HomeOdds >= 141 && OT[i].HomeOdds <= 180) R.HomeOT_Plus141ToPlus180_ML.Add(OT[i]);
                        else if (OT[i].HomeOdds >= 181 && OT[i].HomeOdds <= 220) R.HomeOT_Plus181ToPlus220_ML.Add(OT[i]);
                        else if (OT[i].HomeOdds >= 221 && OT[i].HomeOdds <= 260) R.HomeOT_Plus221ToPlus260_ML.Add(OT[i]);
                        else if (OT[i].HomeOdds >= 261 && OT[i].HomeOdds <= 300) R.HomeOT_Plus261ToPlus300_ML.Add(OT[i]);
                        else if (OT[i].HomeOdds >= 301 && OT[i].HomeOdds <= 340) R.HomeOT_Plus301ToPlus340_ML.Add(OT[i]);
                        else if (OT[i].HomeOdds >= 341 && OT[i].HomeOdds <= 380) R.HomeOT_Plus341ToPlus380_ML.Add(OT[i]);
                        else if (OT[i].HomeOdds >= 381 && OT[i].HomeOdds <= 420) R.HomeOT_Plus381ToPlus420_ML.Add(OT[i]);
                        else if (OT[i].HomeOdds >= 421 && OT[i].HomeOdds <= 460) R.HomeOT_Plus421ToPlus460_ML.Add(OT[i]);
                        else if (OT[i].HomeOdds >= 461 && OT[i].HomeOdds <= 500) R.HomeOT_Plus461ToPlus500_ML.Add(OT[i]);


                        if (OT[i].HomeSpecial == 1.50)
                        {
                            R.HomeOT_15.Add(OT[i]);
                        }
                        else if (OT[i].HomeSpecial == -1.50)
                        {
                            R.HomeOT_Menos15.Add(OT[i]);
                        }

                    }

                  //  (sender as BackgroundWorker).ReportProgress(27);
                    //*****************************************************************************************************
                    //******************************************** SHEET 2 *******************************************************

                    Excel.Worksheet ws4 = wb.Sheets.Add();
                    ws4.Name = "VISITOR OT RAW";


                    Range(ws4, "A1", "Game Date Time", 25);
                    Range(ws4, "B1", "Visitor Team", 25);
                    Range(ws4, "C1", "Home Score", 15);
                    Range(ws4, "D1", "Visitor Score", 15);
                    Range(ws4, "E1", "Coverage", 14);
                    Range(ws4, "F1", "Winner", 12);
                    Range(ws4, "G1", "Visitor Special", 22);
                    Range(ws4, "H1", "Visitor Odds", 20);
                    Range(ws4, "I1", "Visitor Special Odds", 23);
                    Range(ws4, "J1", "Opening Special", 23);
                    Range(ws4, "K1", "Opening Money Line", 28);
                    Range(ws4, "L1", "Opening Juice", 26);


                    for (int i = 0; i < OT.Count; i++)
                    {
                        GruopTeams(OT[i].VisitorTeam, "VISITOR", "OT");
                        GruopTeams(OT[i].HomeTeam, "HOME", "OT");

                        ws4.Range["A" + (i + 2).ToString()].Value = OT[i].GameDateTime.ToString();
                        ws4.Range["B" + (i + 2).ToString()].Value = OT[i].VisitorTeam;
                        ws4.Range["C" + (i + 2).ToString()].Value = OT[i].HomeScore;
                        ws4.Range["D" + (i + 2).ToString()].Value = OT[i].VisitorScore;
                        if (OT[i].VisitorSpecial != null) ws4.Range["E" + (i + 2).ToString()].Value = OT[i].Coverage = (OT[i].HomeScore < OT[i].VisitorScore + OT[i].VisitorSpecial) ? "WIN" : "LOSS";                       
                        ws4.Range["F" + (i + 2).ToString()].Value = OT[i].WINNER;
                        ws4.Range["G" + (i + 2).ToString()].Value = OT[i].VisitorSpecial;
                        ws4.Range["G" + (i + 2).ToString()].NumberFormat = "0.00";
                        ws4.Range["H" + (i + 2).ToString()].Value = OT[i].VisitorOdds;                        
                        ws4.Range["I" + (i + 2).ToString()].Value = OT[i].VisitorSpecialOdds;

                        ws4.Range["J" + (i + 2).ToString()].Value = OT[i].OpeningVisitorSpecial;
                        ws4.Range["K" + (i + 2).ToString()].Value = OT[i].OpeningVisitorOdds;
                        ws4.Range["L" + (i + 2).ToString()].Value = OT[i].OpeningVisitorSpecialOdds;

                        CenterValue(ws4, "A", i);
                        CenterValue(ws4, "B", i);
                        CenterValue(ws4, "C", i);
                        CenterValue(ws4, "D", i);
                        CenterValue(ws4, "R", i);
                        CenterValue(ws4, "F", i);
                        CenterValue(ws4, "G", i);
                        CenterValue(ws4, "H", i);
                        CenterValue(ws4, "I", i);
                        CenterValue(ws4, "J", i);
                        CenterValue(ws4, "K", i);
                        CenterValue(ws4, "L", i);

                        ws4.Range["F" + (i + 2).ToString()].Interior.Color = (OT[i].WINNER != "VISITOR") ? System.Drawing.Color.FromArgb(255, 199, 206) : System.Drawing.Color.FromArgb(198, 239, 206);
                        ws4.Range["F" + (i + 2).ToString()].Borders.Color = (OT[i].WINNER != "VISITOR") ? System.Drawing.Color.FromArgb(156, 0, 6) : System.Drawing.Color.FromArgb(0, 97, 0);


                        if (OT[i].VisitorOdds <= -100 && OT[i].VisitorOdds >= -140) R.VisitorOT_EvenToMenos140_ML.Add(OT[i]);
                        else if (OT[i].VisitorOdds <= -141 && OT[i].VisitorOdds >= -180) R.VisitorOT_Menos141ToMenos180_ML.Add(OT[i]);
                        else if (OT[i].VisitorOdds <= -181 && OT[i].VisitorOdds >= -220) R.VisitorOT_Menos181ToMenos220_ML.Add(OT[i]);
                        else if (OT[i].VisitorOdds <= -221 && OT[i].VisitorOdds >= -260) R.VisitorOT_Menos221ToMenos260_ML.Add(OT[i]);
                        else if (OT[i].VisitorOdds <= -261 && OT[i].VisitorOdds >= -300) R.VisitorOT_Menos261ToMenos300_ML.Add(OT[i]);
                        else if (OT[i].VisitorOdds <= -301 && OT[i].VisitorOdds >= -340) R.VisitorOT_Menos301ToMenos340_ML.Add(OT[i]);
                        else if (OT[i].VisitorOdds <= -341 && OT[i].VisitorOdds >= -380) R.VisitorOT_Menos341ToMenos380_ML.Add(OT[i]);
                        else if (OT[i].VisitorOdds <= -381 && OT[i].VisitorOdds >= -420) R.VisitorOT_Menos381ToMenos420_ML.Add(OT[i]);
                        else if (OT[i].VisitorOdds <= -421 && OT[i].VisitorOdds >= -460) R.VisitorOT_Menos421ToMenos460_ML.Add(OT[i]);
                        else if (OT[i].VisitorOdds <= -461 && OT[i].VisitorOdds >= -500) R.VisitorOT_Menos461ToMenos500_ML.Add(OT[i]);
                        //else if (RT[i].HomeOdds <= -100 && RT[i].HomeOdds > -140) R.HomeOT_EvenToMenos140.Add(RT[i]);

                        if (OT[i].VisitorOdds >= 100 && OT[i].VisitorOdds <= 140) R.VisitorOT_EvenToPlus140_ML.Add(OT[i]);
                        else if (OT[i].VisitorOdds >= 141 && OT[i].VisitorOdds <= 180) R.VisitorOT_Plus141ToPlus180_ML.Add(OT[i]);
                        else if (OT[i].VisitorOdds >= 181 && OT[i].VisitorOdds <= 220) R.VisitorOT_Plus181ToPlus220_ML.Add(OT[i]);
                        else if (OT[i].VisitorOdds >= 221 && OT[i].VisitorOdds <= 260) R.VisitorOT_Plus221ToPlus260_ML.Add(OT[i]);
                        else if (OT[i].VisitorOdds >= 261 && OT[i].VisitorOdds <= 300) R.VisitorOT_Plus261ToPlus300_ML.Add(OT[i]);
                        else if (OT[i].VisitorOdds >= 301 && OT[i].VisitorOdds <= 340) R.VisitorOT_Plus301ToPlus340_ML.Add(OT[i]);
                        else if (OT[i].VisitorOdds >= 341 && OT[i].VisitorOdds <= 380) R.VisitorOT_Plus341ToPlus380_ML.Add(OT[i]);
                        else if (OT[i].VisitorOdds >= 381 && OT[i].VisitorOdds <= 420) R.VisitorOT_Plus381ToPlus420_ML.Add(OT[i]);
                        else if (OT[i].VisitorOdds >= 421 && OT[i].VisitorOdds <= 460) R.VisitorOT_Plus421ToPlus460_ML.Add(OT[i]);
                        else if (OT[i].VisitorOdds >= 461 && OT[i].VisitorOdds <= 500) R.VisitorOT_Plus461ToPlus500_ML.Add(OT[i]);


                        //SP   
                        if (OT[i].VisitorSpecial == 1.50)
                        {
                             R.VisitorOT_15.Add(OT[i]);
                        }
                        else if (OT[i].VisitorSpecial == -1.50)
                        {
                             R.VisitorOT_Menos15.Add(OT[i]);
                        }

                    }

                   // (sender as BackgroundWorker).ReportProgress(36);

                }

                    AlternativeLines(Alternative,Goal,R);
                    ExcelRanges(wb,R);


                if(path == null)
                {
                    string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    wb.SaveAs(mydocpath + @"\" + DateTime.Now.Day + "." + DateTime.Now.Month + "." + DateTime.Now.Year + " NHL Report.xls");
                    wb.Saved = true;
                    wb.Close(true);
                    app.Quit();
                }
                else
                {
                    wb.SaveAs(path);
                    wb.Saved = true;
                    wb.Close(true);
                    app.Quit();
                }


            }
            catch (Exception)
            {
               // MessageBox.Show("Error to generate the excel: " + ex.Message, "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }


        private string Letra(string[] v, int pos)
        {
            return (v[pos] != null) ? v[pos] : "";
        }

        private void ExcelRanges(Excel.Workbook wb, csRangos r)
        {
                Excel.Worksheet ws5 = wb.Sheets.Add();
                ws5.Name = "HOME RT ML";
                List<string[]> l = new List<string[]>();

                l.Add(new string[] { "A", "B", "C", "D","E"});
                l.Add(new string[] { "G", "H", "I", "J","K"});
                l.Add(new string[] { "M", "N", "O", "P","Q"});
                l.Add(new string[] { "S", "T", "U", "V","W"});
                l.Add(new string[] { "Y", "Z", "AA", "AB","AC"});

                l.Add(new string[] { "AE", "AF", "AG", "AH" ,"AI"});
                l.Add(new string[] { "AK", "AL", "AM", "AN","AO" });
                l.Add(new string[] { "AQ", "AR", "AS", "AT","AU" });
                l.Add(new string[] { "AW", "AX", "AY", "AZ", "BA" });
                l.Add(new string[] { "BC", "BD", "BE", "BF", "BG"});


            //*************************************  ADDING INFORMATION ********************************
            //************************************** Worksheet ws5 ********************************************
            lastRow = 3;
                posX = -1;


                PrintCountable(ws5, r.HomeRT_EvenToMenos140_ML,l,"EVEN TO -140",true,false, "HOME", "RT");
                PrintCountable(ws5, r.HomeRT_Menos141ToMenos180_ML, l, "-141 TO -180", true, false, "HOME", "RT");
                PrintCountable(ws5, r.HomeRT_Menos181ToMenos220_ML, l,"-181 TO -220", true, false, "HOME", "RT");
                PrintCountable(ws5, r.HomeRT_Menos221ToMenos260_ML, l, "-221 TO -260", true, false, "HOME", "RT");
                PrintCountable(ws5, r.HomeRT_Menos261ToMenos300_ML, l,"-261 TO -300", true, false, "HOME", "RT");
                PrintCountable(ws5, r.HomeRT_Menos301ToMenos340_ML, l, "-301 TO -340", true, false, "HOME", "RT");
                PrintCountable(ws5, r.HomeRT_Menos341ToMenos380_ML, l, "-341 TO -380", true, false, "HOME", "RT");
                PrintCountable(ws5, r.HomeRT_Menos381ToMenos420_ML, l, "-381 TO -420", true, false, "HOME", "RT");
                PrintCountable(ws5, r.HomeRT_Menos421ToMenos460_ML, l, "-421 TO -460", true, false, "HOME", "RT");
                PrintCountable(ws5, r.HomeRT_Menos461ToMenos500_ML, l, "-461 TO -500", true, false, "HOME", "RT");

                //*******************   PLUS  ***********************
               // (sender as BackgroundWorker).ReportProgress(42);

                PrintCountable(ws5, r.HomeRT_EvenToPlus140_ML, l,"Even TO 140", true, true, "HOME", "RT");
                PrintCountable(ws5, r.HomeRT_Plus141ToPlus180_ML, l,"141 TO 180", true, false, "HOME", "RT");
                PrintCountable(ws5, r.HomeRT_Plus181ToPlus220_ML, l,"181 TO 220", true, false, "HOME", "RT");
                PrintCountable(ws5, r.HomeRT_Plus221ToPlus260_ML, l,"221 TO 260", true, false, "HOME", "RT");
                PrintCountable(ws5, r.HomeRT_Plus261ToPlus300_ML, l,"261 TO 300", true, false, "HOME", "RT");
                PrintCountable(ws5, r.HomeRT_Plus301ToPlus340_ML, l, "301 TO 340", true, false, "HOME", "RT");
                PrintCountable(ws5, r.HomeRT_Plus341ToPlus380_ML, l, "341 TO 380", true, false, "HOME", "RT");
                PrintCountable(ws5, r.HomeRT_Plus381ToPlus420_ML, l, "381 TO 420", true, false, "HOME", "RT");
                PrintCountable(ws5, r.HomeRT_Plus421ToPlus460_ML, l, "421 TO 460", true, false, "HOME", "RT");
                PrintCountable(ws5, r.HomeRT_Plus461ToPlus500_ML, l, "461 TO 500", true, false, "HOME", "RT");

            //(sender as BackgroundWorker).ReportProgress(45);
            //************************************** Worksheet ws6 ********************************************

            Excel.Worksheet ws6 = wb.Sheets.Add();
             ws6.Name = "VISITOR RT ML";
             lastRow = 3;
             posX = -1; 


            PrintCountable(ws6, r.VisitorRT_EvenToMenos140_ML, l, "EVEN TO -140", true, false, "VISITOR", "RT");
            PrintCountable(ws6, r.VisitorRT_Menos141ToMenos180_ML, l, "-141 TO -180", true, false, "VISITOR", "RT");
            PrintCountable(ws6, r.VisitorRT_Menos181ToMenos220_ML, l, "-181 TO -220", true, false, "VISITOR", "RT");
            PrintCountable(ws6, r.VisitorRT_Menos221ToMenos260_ML, l, "-221 TO -260", true, false, "VISITOR", "RT");
           // (sender as BackgroundWorker).ReportProgress(46);
            PrintCountable(ws6, r.VisitorRT_Menos261ToMenos300_ML, l, "-261 TO -300", true, false, "VISITOR", "RT");
            PrintCountable(ws6, r.VisitorRT_Menos301ToMenos340_ML, l, "-301 TO -340", true, false, "VISITOR", "RT");
            PrintCountable(ws6, r.VisitorRT_Menos341ToMenos380_ML, l, "-341 TO -380", true, false, "VISITOR", "RT");
            PrintCountable(ws6, r.VisitorRT_Menos381ToMenos420_ML, l, "-381 TO -420", true, false, "VISITOR", "RT");
            //(sender as BackgroundWorker).ReportProgress(47);
            PrintCountable(ws6, r.VisitorRT_Menos421ToMenos460_ML, l, "-421 TO -460", true, false, "VISITOR", "RT");
            PrintCountable(ws6, r.VisitorRT_Menos461ToMenos500_ML, l, "-461 TO -500", true, false, "VISITOR", "RT");

            //*******************   PLUS  ***********************

            PrintCountable(ws6, r.VisitorRT_EvenToPlus140_ML, l, "Even TO 140", true, true, "VISITOR", "RT");
            PrintCountable(ws6, r.VisitorRT_Plus141ToPlus180_ML, l, "141 TO 180", true, false, "VISITOR", "RT");
           //(sender as BackgroundWorker).ReportProgress(48);
            PrintCountable(ws6, r.VisitorRT_Plus181ToPlus220_ML, l, "181 TO 220", true, false, "VISITOR", "RT");
            PrintCountable(ws6, r.VisitorRT_Plus221ToPlus260_ML, l, "221 TO 260", true, false, "VISITOR", "RT");
            PrintCountable(ws6, r.VisitorRT_Plus261ToPlus300_ML, l, "261 TO 300", true, false, "VISITOR", "RT");
            PrintCountable(ws6, r.VisitorRT_Plus301ToPlus340_ML, l, "301 TO 340", true, false, "VISITOR", "RT");
           // (sender as BackgroundWorker).ReportProgress(49);
            PrintCountable(ws6, r.VisitorRT_Plus341ToPlus380_ML, l, "341 TO 380", true, false, "VISITOR", "RT");
            PrintCountable(ws6, r.VisitorRT_Plus381ToPlus420_ML, l, "381 TO 420", true, false, "VISITOR", "RT");
            PrintCountable(ws6, r.VisitorRT_Plus421ToPlus460_ML, l, "421 TO 460", true, false, "VISITOR", "RT");
            PrintCountable(ws6, r.VisitorRT_Plus461ToPlus500_ML, l, "461 TO 500", true, false, "VISITOR", "RT");

            //(sender as BackgroundWorker).ReportProgress(54);
            //************************************** Worksheet ws7 ********************************************

            if (r.HomeRT_05 != null || r.HomeRT_Menos05 != null)
            {
                Excel.Worksheet ws7 = wb.Sheets.Add();
                ws7.Name = "HOME RT SP";
                lastRow = 3;
                posX = -1;

                PrintCountable(ws7, r.HomeRT_05, l, "COVERAGE +0.5", false, false, "HOME", "RT");
              //  (sender as BackgroundWorker).ReportProgress(58);
                PrintCountable(ws7, r.HomeRT_Menos05, l, "COVERAGE -0.5", false, false, "HOME", "RT");
              //  (sender as BackgroundWorker).ReportProgress(63);
            }

            //************************************** Worksheet ws8 ********************************************
            if (r.VisitorRT_05 != null || r.VisitorRT_Menos05 != null)
            {
                Excel.Worksheet ws8 = wb.Sheets.Add();
                ws8.Name = "VISITOR RT SP";
                lastRow = 3;
                posX = -1;

                PrintCountable(ws8, r.VisitorRT_05, l, "COVERAGE +0.5", false, false, "VISITOR", "RT");
              //  (sender as BackgroundWorker).ReportProgress(67);
                PrintCountable(ws8, r.VisitorRT_Menos05, l, "COVERAGE -0.5", false, false, "VISITOR", "RT");
               // (sender as BackgroundWorker).ReportProgress(72);
            }


            //************************************** Worksheet ws9 ********************************************
            Excel.Worksheet ws9 = wb.Sheets.Add();
            ws9.Name = "HOME OT ML";
            lastRow = 3;
            posX = -1;


            PrintCountable(ws9, r.HomeOT_EvenToMenos140_ML, l, "EVEN TO -140", true, false, "HOME", "OT");
            PrintCountable(ws9, r.HomeOT_Menos141ToMenos180_ML, l, "-141 TO -180", true, false, "HOME", "OT");
            PrintCountable(ws9, r.HomeOT_Menos181ToMenos220_ML, l, "-181 TO -220", true, false, "HOME", "OT");
            PrintCountable(ws9, r.HomeOT_Menos221ToMenos260_ML, l, "-221 TO -260", true, false, "HOME", "OT");
           // (sender as BackgroundWorker).ReportProgress(74);
            PrintCountable(ws9, r.HomeOT_Menos261ToMenos300_ML, l, "-261 TO -300", true, false, "HOME", "OT");
            PrintCountable(ws9, r.HomeOT_Menos301ToMenos340_ML, l, "-301 TO -340", true, false, "HOME", "OT");
            PrintCountable(ws9, r.HomeOT_Menos341ToMenos380_ML, l, "-341 TO -380", true, false, "HOME", "OT");
            PrintCountable(ws9, r.HomeOT_Menos381ToMenos420_ML, l, "-381 TO -420", true, false, "HOME", "OT");
            //(sender as BackgroundWorker).ReportProgress(76);
            PrintCountable(ws9, r.HomeOT_Menos421ToMenos460_ML, l, "-421 TO -460", true, false, "HOME", "OT");
            PrintCountable(ws9, r.HomeOT_Menos461ToMenos500_ML, l, "-461 TO -500", true, false, "HOME", "OT");

            //*******************   PLUS  ***********************

            PrintCountable(ws9, r.HomeOT_EvenToPlus140_ML, l, "Even TO 140", true, true, "HOME", "OT");
            PrintCountable(ws9, r.HomeOT_Plus141ToPlus180_ML, l, "141 TO 180", true, false, "HOME", "OT");
           // (sender as BackgroundWorker).ReportProgress(78);
            PrintCountable(ws9, r.HomeOT_Plus181ToPlus220_ML, l, "181 TO 220", true, false, "HOME", "OT");
            PrintCountable(ws9, r.HomeOT_Plus221ToPlus260_ML, l, "221 TO 260", true, false, "HOME", "OT");
            PrintCountable(ws9, r.HomeOT_Plus261ToPlus300_ML, l, "261 TO 300", true, false, "HOME", "OT");
            PrintCountable(ws9, r.HomeOT_Plus301ToPlus340_ML, l, "301 TO 340", true, false, "HOME", "OT");
           // (sender as BackgroundWorker).ReportProgress(79);
            PrintCountable(ws9, r.HomeOT_Plus341ToPlus380_ML, l, "341 TO 380", true, false, "HOME", "OT");
            PrintCountable(ws9, r.HomeOT_Plus381ToPlus420_ML, l, "381 TO 420", true, false, "HOME", "OT");
            PrintCountable(ws9, r.HomeOT_Plus421ToPlus460_ML, l, "421 TO 460", true, false, "HOME", "OT");
            PrintCountable(ws9, r.HomeOT_Plus461ToPlus500_ML, l, "461 TO 500", true, false, "HOME", "OT");

            //(sender as BackgroundWorker).ReportProgress(81);

            //************************************** Worksheet ws10 ********************************************
            Excel.Worksheet ws10 = wb.Sheets.Add();
            ws10.Name = "VISITOR OT ML";
            lastRow = 3;
            posX = -1;

            PrintCountable(ws10, r.VisitorOT_EvenToMenos140_ML, l, "EVEN TO -140", true, false, "VISITOR", "OT");
            PrintCountable(ws10, r.VisitorOT_Menos141ToMenos180_ML, l, "-141 TO -180", true, false, "VISITOR", "OT");
            PrintCountable(ws10, r.VisitorOT_Menos181ToMenos220_ML, l, "-181 TO -220", true, false, "VISITOR", "OT");
            PrintCountable(ws10, r.VisitorOT_Menos221ToMenos260_ML, l, "-221 TO -260", true, false, "VISITOR", "OT");
            //(sender as BackgroundWorker).ReportProgress(83);
            PrintCountable(ws10, r.VisitorOT_Menos261ToMenos300_ML, l, "-261 TO -300", true, false, "VISITOR", "OT");
            PrintCountable(ws10, r.VisitorOT_Menos301ToMenos340_ML, l, "-301 TO -340", true, false, "VISITOR", "OT");
            PrintCountable(ws10, r.VisitorOT_Menos341ToMenos380_ML, l, "-341 TO -380", true, false, "VISITOR", "OT");
            //(sender as BackgroundWorker).ReportProgress(84);
            PrintCountable(ws10, r.VisitorOT_Menos381ToMenos420_ML, l, "-381 TO -420", true, false, "VISITOR", "OT");
            PrintCountable(ws10, r.VisitorOT_Menos421ToMenos460_ML, l, "-421 TO -460", true, false, "VISITOR", "OT");
            PrintCountable(ws10, r.VisitorOT_Menos461ToMenos500_ML, l, "-461 TO -500", true, false, "VISITOR", "OT");

            //*******************   PLUS  ***********************

            PrintCountable(ws10, r.VisitorOT_EvenToPlus140_ML, l, "Even TO 140", true, true, "VISITOR", "OT");
            //(sender as BackgroundWorker).ReportProgress(85);
            PrintCountable(ws10, r.VisitorOT_Plus141ToPlus180_ML, l, "141 TO 180", true, false, "VISITOR", "OT");
            PrintCountable(ws10, r.VisitorOT_Plus181ToPlus220_ML, l, "181 TO 220", true, false, "VISITOR", "OT");
            PrintCountable(ws10, r.VisitorOT_Plus221ToPlus260_ML, l, "221 TO 260", true, false, "VISITOR", "OT");
            PrintCountable(ws10, r.VisitorOT_Plus261ToPlus300_ML, l, "261 TO 300", true, false, "VISITOR", "OT");
            //(sender as BackgroundWorker).ReportProgress(86);
            PrintCountable(ws10, r.VisitorOT_Plus301ToPlus340_ML, l, "301 TO 340", true, false, "VISITOR", "OT");
            PrintCountable(ws10, r.VisitorOT_Plus341ToPlus380_ML, l, "341 TO 380", true, false, "VISITOR", "OT");
            PrintCountable(ws10, r.VisitorOT_Plus381ToPlus420_ML, l, "381 TO 420", true, false, "VISITOR", "OT");
            PrintCountable(ws10, r.VisitorOT_Plus421ToPlus460_ML, l, "421 TO 460", true, false, "VISITOR", "OT");
            PrintCountable(ws10, r.VisitorOT_Plus461ToPlus500_ML, l, "461 TO 500", true, false, "VISITOR", "OT");

            //(sender as BackgroundWorker).ReportProgress(87);

            //************************************** Worksheet ws11 ********************************************
            if (r.HomeOT_15 != null || r.HomeOT_Menos15 != null)
            {
                Excel.Worksheet ws11 = wb.Sheets.Add();
                ws11.Name = "HOME OT SP";
                lastRow = 3;
                posX = -1;

                PrintCountable(ws11, r.HomeOT_15, l, "COVERAGE +1.5", false, false, "HOME", "OT");
                //(sender as BackgroundWorker).ReportProgress(88);
                PrintCountable(ws11, r.HomeOT_Menos15, l, "COVERAGE -1.5", false, false, "HOME", "OT");
                //(sender as BackgroundWorker).ReportProgress(89);
            }

            //************************************** Worksheet ws12 ********************************************
            if (r.VisitorOT_15 != null || r.VisitorOT_Menos15 != null)
            {
                Excel.Worksheet ws12 = wb.Sheets.Add();
                ws12.Name = "VISITOR OT SP";
                lastRow = 3;
                posX = -1;

                PrintCountable(ws12, r.VisitorOT_15, l, "COVERAGE +1.5", false, false, "VISITOR", "OT");
                //(sender as BackgroundWorker).ReportProgress(90);
                PrintCountable(ws12, r.VisitorOT_Menos15, l, "COVERAGE -1.5", false, false, "VISITOR", "OT");
                //(sender as BackgroundWorker).ReportProgress(91);
            }


            //****************************************** ALTERNATIVE ********************************
            if((r.AlternativeHomeMas15 != null && r.AlternativeHomeMas15.Count > 0) || (r.AlternativeHomeMenos15 != null  && r.AlternativeHomeMenos15.Count > 0))
            {
                Excel.Worksheet ws13 = wb.Sheets.Add();
                ws13.Name = "HOME ALTERNATIVE";
                lastRow = 3;
                posX = -1;

                PrintCountable(ws13, r.AlternativeHomeMas15, l, "COVERAGE +1.5", false, false, "HOME", "ALTERNATIVE");
               // (sender as BackgroundWorker).ReportProgress(92);
                PrintCountable(ws13, r.AlternativeHomeMenos15, l, "COVERAGE -1.5", false, false, "HOME", "ALTERNATIVE");
                //(sender as BackgroundWorker).ReportProgress(93);
            }


            

            if ((r.AlternativeVisitorMas15 != null && r.AlternativeVisitorMas15.Count > 0) || (r.AlternativeVisitorMenos15 != null && r.AlternativeVisitorMenos15.Count > 0))
            {
                Excel.Worksheet ws14 = wb.Sheets.Add();
                ws14.Name = "VISITOR ALTERNATIVE";
                lastRow = 3;
                posX = -1;

                PrintCountable(ws14, r.AlternativeVisitorMas15, l, "COVERAGE +1.5", false, false, "VISITOR", "ALTERNATIVE");
               // (sender as BackgroundWorker).ReportProgress(94);
                PrintCountable(ws14, r.AlternativeVisitorMenos15, l, "COVERAGE -1.5", false, false, "VISITOR", "ALTERNATIVE");
               // (sender as BackgroundWorker).ReportProgress(95);
            }



            // ****************************************** 1 Goal **************************************
            if ((r.GoalHomeMas1 != null && r.GoalHomeMas1.Count > 0) || (r.GoalHomeMenos1 != null && r.GoalHomeMenos1.Count > 0))
            {
                Excel.Worksheet ws13 = wb.Sheets.Add();
                ws13.Name = "HOME GOAL";
                lastRow = 3;
                posX = -1;

                PrintCountable(ws13, r.GoalHomeMas1, l, "COVERAGE +1", false, false, "HOME", "GOAL");
               // (sender as BackgroundWorker).ReportProgress(96);
                PrintCountable(ws13, r.GoalHomeMenos1, l, "COVERAGE -1", false, false, "HOME", "GOAL");
               // (sender as BackgroundWorker).ReportProgress(97);
            }



            if ((r.GoalVisitorMas1 != null && r.GoalVisitorMas1.Count > 0) || (r.GoalVisitorMenos1 != null && r.GoalVisitorMenos1.Count > 0))
            {
                Excel.Worksheet ws15 = wb.Sheets.Add();
                ws15.Name = "VISITOR GOAL";
                lastRow = 3;
                posX = -1;

                PrintCountable(ws15, r.GoalVisitorMas1, l, "COVERAGE +1", false, false, "VISITOR", "GOAL");
               // (sender as BackgroundWorker).ReportProgress(98);
                PrintCountable(ws15, r.GoalVisitorMenos1, l, "COVERAGE -1", false, false, "VISITOR", "GOAL");
               // (sender as BackgroundWorker).ReportProgress(100);
            }



        }



        private void PrintCountable(Excel.Worksheet ws5, ObservableCollection<csReportNHL> list, List<string[]> l, string txt,bool money, bool reset, string side, string league)
        {

            if(reset)
            {
                posX = -1;
                lastRow = (groupTeams.Lista != null) ? lastRow = (groupTeams.Lista.Count + 5) : 3;
            }

            if (list != null && list.Count > 0)
            {
                posX = posX + 1;
                ExcelTable(ws5, txt, l[posX], lastRow - 1);
                ShowTeamNames(groupTeams.Lista, ws5, l[posX], 0, lastRow, side, list, money, league);
            }
        }


        private csTeamRange SumGP(ObservableCollection<csReportNHL> l, string name, string side, bool money)
        {
            csTeamRange r = new csTeamRange();

            if (l != null)
            {
                for (int i = 0; i < l.Count; i++)
                {

                    // *************************** HOME *************************
                    if (side == "HOME")
                    {

                        if (l[i].HomeTeam == name)
                        {
                            r.GP += 1;

                            if(money)
                            {
                                if (l[i].WINNER == "HOME") r.Wins += 1;

                                // **************** BEGIN MATH **********************

                                if (l[i].HomeOdds < 0)
                                {
                                    if (l[i].WINNER == "HOME")
                                    {
                                        r.NetAmount += 100;
                                        r.RiskAmount += Math.Abs(l[i].HomeOdds ?? default(int));
                                    }
                                    else
                                    {
                                        r.NetAmount += l[i].HomeOdds ?? default(int);
                                        r.RiskAmount += Math.Abs(l[i].HomeOdds ?? default(int));
                                    }

                                }
                                else
                                { ////*******************************
                                    if (l[i].WINNER == "HOME")
                                    {
                                        r.NetAmount += l[i].HomeOdds ?? default(int);
                                        r.RiskAmount += 100;
                                    }
                                    else
                                    {
                                        r.NetAmount += -100;
                                        r.RiskAmount += 100;
                                    }
                                }//*************************************************

                                // **************** FIN MATH **********************



                            } // ******************** FIN MONEY LINE HOME *****************
                            else
                            {
                                if (l[i].Coverage == "LOSS") r.Wins += 1;
                                else if (l[i].Coverage == "TIE") r.Ties += 1;
                                
                                // **************** BEGIN MATH **********************

                                if (l[i].HomeSpecialOdds < 0)
                                {
                                    if(l[i].Coverage == "LOSS")
                                    {
                                        r.NetAmount += 100;
                                        r.RiskAmount += Math.Abs(l[i].HomeSpecialOdds ?? default(int));
                                    }
                                    else
                                    {
                                        r.NetAmount += l[i].HomeSpecialOdds ?? default(int);
                                        r.RiskAmount += Math.Abs(l[i].HomeSpecialOdds ?? default(int));
                                    }

                                }else{

                                    if (l[i].Coverage == "LOSS")
                                    {
                                        r.NetAmount += l[i].HomeSpecialOdds ?? default(int);
                                        r.RiskAmount += 100;
                                    }
                                    else
                                    {
                                        r.NetAmount += -100;
                                        r.RiskAmount += 100;
                                    }
                                }

                               // **************** FIN MATH **********************
                            }

                        }
                    }     // ******************* FIN HOME ***********************


                    else  // ******************* INICIO VISITOR *******************
                    {
                        if (l[i].VisitorTeam == name)
                        {
                            r.GP += 1;

                            if (money)
                            {
                                if (l[i].WINNER == "VISITOR") r.Wins += 1;

                                // **************** BEGIN MATH **********************
                               if (l[i].VisitorOdds < 0)
                                {
                                        if (l[i].WINNER == "VISITOR")
                                        {
                                            r.NetAmount += 100;
                                            r.RiskAmount += Math.Abs(l[i].VisitorOdds ?? default(int));
                                        }
                                        else
                                        {
                                            r.NetAmount += l[i].VisitorOdds ?? default(int);
                                            r.RiskAmount += Math.Abs(l[i].VisitorOdds ?? default(int));
                                        }

                                }
                                else 
                                {

                                        if (l[i].WINNER == "VISITOR")
                                        {
                                            r.NetAmount += l[i].VisitorOdds ?? default(int);
                                            r.RiskAmount += 100;
                                        }
                                        else
                                        {
                                            r.NetAmount += -100;
                                            r.RiskAmount += 100;
                                        }
                                }

                                // **************** FIN MATH **********************


                            }   // ********** SPREAD **********
                            else
                            {
                                if (l[i].Coverage == "WIN") r.Wins += 1;
                                else if (l[i].Coverage == "TIE") r.Ties += 1;

                                // **************** BEGIN MATH **********************

                                if (l[i].VisitorSpecialOdds < 0)
                                {
                                        if (l[i].Coverage == "WIN")
                                        {
                                            r.NetAmount += 100;
                                            r.RiskAmount += Math.Abs(l[i].VisitorSpecialOdds ?? default(int));
                                        }
                                        else
                                        {
                                            r.NetAmount += l[i].VisitorSpecialOdds ?? default(int);
                                            r.RiskAmount += Math.Abs(l[i].VisitorSpecialOdds ?? default(int));
                                        }

                                }
                                else
                                {

                                        if (l[i].Coverage == "WIN")
                                        {
                                            r.NetAmount += l[i].VisitorSpecialOdds ?? default(int);
                                            r.RiskAmount += 100;
                                        }
                                        else
                                        {
                                            r.NetAmount += -100;
                                            r.RiskAmount += 100;
                                        }
                                }

                                // **************** FIN MATH **********************

                            }// ********** FIN SPREAD **********
                        }
                   }// ************************ FIN VISITOR ***********************
                }
            }

            return r;
        }



        private void ShowTeamNames(ObservableCollection<csTeamRange> l, Excel.Worksheet ws, string[] v, int index, int pos, string side, ObservableCollection<csReportNHL> l2, bool money, string league)
        {
            csTeamRange r = null;

            if (l != null)
            {

                for (int i = 0; i < l.Count; i++ )
                {
                        r = null;          
                        r = SumGP(l2, l[i].TeamName, side, money);

                    try
                    {
                        double amount = r.NetAmount ?? default(int);
                        double risk = r.RiskAmount ?? default(int);

                        double hold1 = 0;
                        if (risk > 0) hold1 = amount / risk;

                        double hold = Convert.ToDouble(hold1 * 100);

                        ws.Range[Letra(v, index + 4) + (i + pos).ToString()].Value = Math.Round(hold, 0, MidpointRounding.AwayFromZero) + "%";

                        if (Math.Round(hold, 0, MidpointRounding.AwayFromZero) < 10) { ws.Range[Letra(v, index + 4) + (i + pos).ToString()].Interior.Color = System.Drawing.Color.FromArgb(255, 199, 206); ws.Range[Letra(v, index + 4) + (i + pos).ToString()].Borders.Color = System.Drawing.Color.FromArgb(156, 0, 6); }
                        else if (Math.Round(hold, 0, MidpointRounding.AwayFromZero) >= 10 && Math.Round(hold, 0, MidpointRounding.AwayFromZero) <= 20) { ws.Range[Letra(v, index + 4) + (i + pos).ToString()].Interior.Color = System.Drawing.Color.FromArgb(255, 235, 156); ws.Range[Letra(v, index + 4) + (i + pos).ToString()].Borders.Color = System.Drawing.Color.FromArgb(156, 101, 0); }
                        else { ws.Range[Letra(v, index + 4) + (i + pos).ToString()].Interior.Color = System.Drawing.Color.FromArgb(198, 239, 206); ws.Range[Letra(v, index + 4) + (i + pos).ToString()].Borders.Color = System.Drawing.Color.FromArgb(0, 97, 0); }
                        }
                    catch (Exception)
                    {
                        ws.Range[Letra(v, index + 4) + (i + pos).ToString()].Value = "0%";
                    }


                        ws.Range[Letra(v, index + 3) + (i + pos).ToString()].Value = (r.GP <= 0) ? "0%" : (r.Wins * 100 / r.GP).ToString() + "%";
                        ws.Range[Letra(v, index + 2) + (i + pos).ToString()].Value = r.GP;
                        ws.Range[Letra(v, index + 1) + (i + pos).ToString()].Value = r.Wins;
                        ws.Range[Letra(v, index + 0) + (i + pos).ToString()].Value = l[i].TeamName;

                        ws.Range[Letra(v, index + 4) + (i + pos).ToString()].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        ws.Range[Letra(v, index + 3) + (i + pos).ToString()].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        ws.Range[Letra(v, index + 2) + (i + pos).ToString()].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        ws.Range[Letra(v, index + 1) + (i + pos).ToString()].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        ws.Range[Letra(v, index + 0) + (i + pos).ToString()].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    ws.Range[Letra(v, index + 2) + (i + pos).ToString()].BorderAround2();
                    ws.Range[Letra(v, index + 1) + (i + pos).ToString()].BorderAround2();
                    ws.Range[Letra(v, index + 0) + (i + pos).ToString()].BorderAround2();

                    //Interior Color
                    if ((r.GP <= 0) || ((r.Wins * 100) / r.GP) < 50) { ws.Range[Letra(v, index + 3) + (i + pos).ToString()].Interior.Color = System.Drawing.Color.FromArgb(255, 199, 206); ws.Range[Letra(v, index + 3) + (i + pos).ToString()].Borders.Color = System.Drawing.Color.FromArgb(156, 0, 6); }
                    else if (((r.Wins * 100) / r.GP) == 50) { ws.Range[Letra(v, index + 3) + (i + pos).ToString()].Interior.Color = System.Drawing.Color.FromArgb(255, 235, 156); ws.Range[Letra(v, index + 3) + (i + pos).ToString()].Borders.Color = System.Drawing.Color.FromArgb(156, 101, 0);}
                    else { ws.Range[Letra(v, index + 3) + (i + pos).ToString()].Interior.Color = System.Drawing.Color.FromArgb(198, 239, 206); ws.Range[Letra(v, index + 3) + (i + pos).ToString()].Borders.Color = System.Drawing.Color.FromArgb(0, 97, 0); }
                }
            }
        }



        private void ExcelTable(Excel.Worksheet ws, String txt, string[] l, int pos)
        {
            System.Drawing.Color c = System.Drawing.Color.FromArgb(179, 179, 204);

            ws.Range[l[0] + pos.ToString()].Value = txt;
            ws.Range[l[0] + pos.ToString()].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ws.Range[l[0] + pos.ToString()].ColumnWidth = 24;
            ws.Range[l[0] + pos.ToString()].BorderAround2();
            ws.Range[l[0] + pos.ToString()].Font.Bold = true;
            ws.Range[l[0] + pos.ToString()].Interior.Color = c;
            ws.Range[l[0] + pos.ToString()].Font.Size = 14;

            ws.Range[l[1] + pos.ToString()].Value = "WINS";
            ws.Range[l[1] + pos.ToString()].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ws.Range[l[1] + pos.ToString()].ColumnWidth = 7;
            ws.Range[l[1] + pos.ToString()].BorderAround2();
            ws.Range[l[1] + pos.ToString()].Font.Bold = true;
            ws.Range[l[1] + pos.ToString()].Interior.Color = c;
            ws.Range[l[1] + pos.ToString()].Font.Size = 14;

            ws.Range[l[2] + pos.ToString()].Value = "GP";
            ws.Range[l[2] + pos.ToString()].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ws.Range[l[2] + pos.ToString()].ColumnWidth = 5;
            ws.Range[l[2] + pos.ToString()].BorderAround2();
            ws.Range[l[2] + pos.ToString()].Font.Bold = true;
            ws.Range[l[2] + pos.ToString()].Interior.Color = c;
            ws.Range[l[2] + pos.ToString()].Font.Size = 14;

            ws.Range[l[3] + pos.ToString()].Value = "WIN %";
            ws.Range[l[3] + pos.ToString()].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ws.Range[l[3] + pos.ToString()].ColumnWidth = 9;
            ws.Range[l[3] + pos.ToString()].BorderAround2();
            ws.Range[l[3] + pos.ToString()].Font.Bold = true;
            ws.Range[l[3] + pos.ToString()].Interior.Color = c;
            ws.Range[l[3] + pos.ToString()].Font.Size = 14;

            ws.Range[l[4] + pos.ToString()].Value = "Hold %";
            ws.Range[l[4] + pos.ToString()].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ws.Range[l[4] + pos.ToString()].ColumnWidth = 9;
            ws.Range[l[4] + pos.ToString()].BorderAround2();
            ws.Range[l[4] + pos.ToString()].Font.Bold = true;
            ws.Range[l[4] + pos.ToString()].Interior.Color = c;
            ws.Range[l[4] + pos.ToString()].Font.Size = 14;
        }



        private void AlternativeLines(ObservableCollection<csReportNHL> Alternative, ObservableCollection<csReportNHL> Goal, csRangos R)
        {
            if(Alternative != null && Alternative.Count > 0)
            {
                for (int i = 0; i < Alternative.Count; i++)
                {
                    GruopTeams(Alternative[i].HomeTeam, "HOME", "ALTERNATIVE");
                    GruopTeams(Alternative[i].VisitorTeam, "VISITOR", "ALTERNATIVE");

                    Alternative[i].Coverage = (Alternative[i].VisitorScore < Alternative[i].HomeScore + Alternative[i].HomeSpecial) ? "WIN" : "LOSS";

                    if (Alternative[i].HomeSpecial < 0) R.AlternativeHomeMenos15.Add(Alternative[i]);
                    else R.AlternativeHomeMas15.Add(Alternative[i]);

                    if (Alternative[i].VisitorSpecial < 0) R.AlternativeVisitorMenos15.Add(Alternative[i]);
                    else R.AlternativeVisitorMas15.Add(Alternative[i]);
                }
            }

            //(sender as BackgroundWorker).ReportProgress(38);



            if (Goal != null && Goal.Count > 0)
            {

                for (int i = 0; i < Goal.Count; i++)
                {

                    GruopTeams(Goal[i].HomeTeam, "HOME", "GOAL");
                    GruopTeams(Goal[i].VisitorTeam, "VISITOR", "GOAL");

                    Goal[i].Coverage = (Goal[i].VisitorScore < Goal[i].HomeScore + Goal[i].HomeSpecial) ? "WIN" : "LOSS";

                    if (Goal[i].HomeScore + Goal[i].HomeSpecial == Goal[i].VisitorScore)
                    {
                        Goal[i].Coverage = "TIE";
                    }


                    if (Goal[i].HomeSpecial < 0) R.GoalHomeMenos1.Add(Goal[i]);
                    else R.GoalHomeMas1.Add(Goal[i]);

                    if (Goal[i].VisitorSpecial < 0) R.GoalVisitorMenos1.Add(Goal[i]);
                    else R.GoalVisitorMas1.Add(Goal[i]);
                }
            }

            //(sender as BackgroundWorker).ReportProgress(40);
        }

    }
}
