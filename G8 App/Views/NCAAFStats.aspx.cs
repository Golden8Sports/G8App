using G8_App.Entities;
using G8_App.Entities.SportsIntelligence;
using G8_App.Logic.SportsIntelligence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G8_App.Views
{
    public partial class NCAAFStats : System.Web.UI.Page
    {
        private MlbInformation MLBInfo = new MlbInformation();
        private static ObservableCollection<csMlbTrends> TrendList = new ObservableCollection<csMlbTrends>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                if (!IsPostBack)
                {
                    userName.InnerText = csUser.Name + " - " + csUser.Profile;
                    FillYears();
                }
            }
            else Response.Redirect("Login.aspx");
        }


        private void FillYears()
        {
            DateTime dt = DateTime.Now;
            List<csYear> YearList = new List<csYear>();

            for (int i = dt.Year; i >= 2003; i--)
            {
                YearList.Add(new csYear(i));
            }
            inYear.DataSource = YearList;
            inYear.DataTextField = "Year";
            inYear.DataValueField = "Year";
            inYear.DataBind();
        }


        protected void Trends(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(inYear.Items[inYear.SelectedIndex].Value);
            TrendList = MLBInfo.ScrapTrends(year, year.ToString(), inShow.Items[inShow.SelectedIndex].Value, inSituation.Items[inSituation.SelectedIndex].Value, inType.Items[inType.SelectedIndex].Value, 1,"NCAAF", "");

            rptTable.DataSource = TrendList;
            rptTable.DataBind();

            MLBInfo.PuclicateTrends(TrendList);
        }


        protected string Line2()
        {
            if (TrendList != null && TrendList.Count > 0)
            {
                return TrendList[0].H2;
            }

            return "";
        }


        protected string Line3()
        {
            if (TrendList != null && TrendList.Count > 0)
            {
                return TrendList[0].H3;
            }

            return "";
        }


        protected string Line4()
        {
            if (TrendList != null && TrendList.Count > 0)
            {
                return TrendList[0].H4;
            }

            return "";
        }


        protected string Line5()
        {
            if (TrendList != null && TrendList.Count > 0)
            {
                return TrendList[0].H5;
            }

            return "";
        }



        protected string Team()
        {
            if (TrendList != null && TrendList.Count > 0)
            {
                return TrendList[0].H1;
            }
            return "";
        }
    }
}