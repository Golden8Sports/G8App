using G8_App.Entities;
using G8_App.Entities.NHL_Ranges;
using G8_App.Logic.NHL_Ranges;
using NHL_BL.Connection;
using NHL_BL.Entities;
using NHL_BL.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace G8_App.Views
{
    public partial class nhlRanges : System.Web.UI.Page
    {
        private System.Windows.Forms.SaveFileDialog saveDlg = new System.Windows.Forms.SaveFileDialog();
        private blTeam teamDB = new blTeam();
        private csRealTeamName realNames = new csRealTeamName();
        private blReportNHL reportDB = new blReportNHL();
        private int indexLeague = -1, indexSide = -1;
        private static ObservableCollection<csReportNHL> list3 = null;
        private static ObservableCollection<csReportNHL> OT = null;
        private static ObservableCollection<csReportNHL> RT = null;
        private static ObservableCollection<csReportNHL> Goal = null;
        private static ObservableCollection<csReportNHL> Alternative = null;
        private DataSet data = new DataSet();
        private csGenerateExcel GenExcel = new csGenerateExcel();

        protected void SetIndexeLeague(object sender, EventArgs e)
        {
            indexLeague = inputLeague.SelectedIndex;
        }

        protected void SetIndexeSide(object sender, EventArgs e)
        {
            indexSide = inputSide.SelectedIndex;
        }



        protected void LoadData(object sender, EventArgs e)
        {
            try
            {
                string side = inputSide.Items[inputSide.SelectedIndex].Value;//  Items[inputSide.SelectedIndex].Text;
                string team = inputTeam.Items[inputTeam.SelectedIndex].Text;
                string league = inputLeague.Items[inputLeague.SelectedIndex].Value;
                string startD = Request[startDate.UniqueID];
                string endD = Request[endDate.UniqueID];

                var spli1 = startD.Split('/');
                var spli2 = endD.Split('/');

                if (side == null || side == "ALL") side = "";
                if (team == null || team == "All") team = "";
                if (league == null) league = "";


                startD = spli1[2] + "-" + spli1[0] + "-" + spli1[1];
                endD = spli2[2] + "-" + spli2[0] + "-" + spli2[1];




                if (league == "ALL")
                {
                    OT = reportDB.SportListNHL(startD, endD, 7, side, team, false, data);
                    RT = reportDB.SportListNHL(startD, startD, 15, side, team, false, data);
                    Goal = reportDB.SportListNHL(startD, startD, 2007, side, team, true, data);
                    Alternative = reportDB.SportListNHL(startD, startD, 2039, side, team, true, data);

                    if (OT != null) OT = realNames.Process(OT);
                    if (RT != null) RT = realNames.Process(RT);
                    if (Goal != null) Goal = realNames.Process(Goal);
                    if (Alternative != null) Alternative = realNames.Process(Alternative);

                    list3 = new ObservableCollection<csReportNHL>(OT.Union(RT).ToList());

                }
                else if (league == "1G") { list3 = Goal = realNames.Process(reportDB.SportListNHL(startD, startD, 2007, side, team, true, data)); }
                else if (league == "ALT") { list3 = Alternative = realNames.Process(reportDB.SportListNHL(startD, startD, 2039, side, team, true, data)); }
                else if (league == "OT") { list3 = OT = realNames.Process(reportDB.SportListNHL(startD, endD, 7, side, team, false, data)); }
                else if (league == "RT") { list3 = RT = realNames.Process(reportDB.SportListNHL(startD, startD, 15, side, team, false, data)); }


                if (list3 != null)
                {
                    rptTable.DataSource = list3;
                    rptTable.DataBind();
                }


            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('" + ex.Message + "');</script>");
            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Login"] != null)
            {
                if (!IsPostBack)
                {
                    userName.InnerText = csUser.Name + " - " + csUser.Profile;

                    ObservableCollection<csTeam> TeamsListNHL = teamDB.TeamsListNHL(7, 15);
                    TeamsListNHL = realNames.GetNHLTeams(TeamsListNHL);

                    if (TeamsListNHL != null)
                    {
                        inputTeam.DataSource = TeamsListNHL;
                        inputTeam.DataTextField = "Name";
                        inputTeam.DataValueField = "Name";
                        inputTeam.DataBind();
                        inputTeam.SelectedIndex = inputTeam.Items.Count - 1;
                    }

                }
            }else
            {
                Response.Redirect("Login.aspx");
            }
        }



        protected void GenerateExcel(object sender, EventArgs e)
        {
            string pathToFiles = Server.MapPath("/NHL-Ranges");

                GenExcel.GenerateExcel(OT, RT, Alternative, Goal, null);
        }


    }
}