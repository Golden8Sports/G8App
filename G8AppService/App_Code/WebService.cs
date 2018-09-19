using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Collections.ObjectModel;

public class WebService : System.Web.Services.WebService
{

    [WebMethod]
    public string PopulateProps(string idSport, string LeagueID)
    {
        try
        {
            PinniAppi AppPinni = new PinniAppi();
            ObservableCollection<csSocProp> PropList = AppPinni.LoadSoccerProps("", LeagueID,idSport);
            if (PropList != null && PropList.Count > 0)
            {
                foreach (var i in PropList)
                {
                    if (!AppPinni.SaveProp(i)) return "N";
                }
            }else
            {
                return "N";
            }
        }
        catch (Exception)
        {
            return "N";
        }

        return "Y";
    }

}
