using G8_App.Entities;
using G8_App.Entities.Pinnacle_Appi;
using G8_App.Entities.Sports.Soccer;
using G8_App.Logic.Sports.Soc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G8_App.Views
{
    public partial class SoccerProps : System.Web.UI.Page
    {
        private PinniAppi AppPinni = new PinniAppi();
        private static ObservableCollection<csSocProp> PropList = null;
        private blSoccer soccerDB = new blSoccer();
        private static bool canPopulate = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                userName.InnerText = csUser.Name + " - " + csUser.Profile;

                if (!IsPostBack)
                {
                    inLeague.DataSource = soccerDB.LeaguesBySport(29);
                    inLeague.DataTextField = "LeagueDescription";
                    inLeague.DataValueField = "IdLeague";
                    inLeague.SelectedIndex = 61;
                    inLeague.DataBind();


                }
            }
            else Response.Redirect("Login.aspx");
        }


        protected void SocProps(object sender, EventArgs e)
        {
            try
            {
                PropList = AppPinni.LoadSoccerProps(inCategory.Items[inCategory.SelectedIndex].Value, inLeague.Items[inLeague.SelectedIndex].Value);
                rptTable.DataSource = PropList;               
                rptTable.DataBind();

                if(PropList != null)
                {
                    canPopulate = true;
                    btnPopulate.Visible = true;
                }
                else
                {
                    canPopulate = false;
                    btnPopulate.Visible = false;
                }               
            }
            catch (Exception)
            {
                canPopulate = false;
            }
        }


        protected void PopulateInfo(object sender, EventArgs e)
        {
                if (PropList != null && canPopulate)
                {
                    foreach (var i in PropList)
                    {
                       AppPinni.SaveProp(i);
                    }                   
                }

            canPopulate = false;
            btnPopulate.Visible = false;
        }   


    }
}