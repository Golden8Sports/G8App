using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.Dashboard
{
    public class csSeason
    {
        public int year { get; set; }
        public string startDateCurrentSeason { get; set; }
        public string endDateCurrentSeason { get; set; }

        public string startDateLastSeason { get; set; }
        public string endDateLastSeason { get; set; }

        public string Sport { get; set; }

        public csSeason(string sport)
        {
            this.Sport = sport;
            DateTime dt = DateTime.Now;
            
            if(sport == "MLB")
            {
                startDateCurrentSeason = dt.Year + "-03-01";
                endDateCurrentSeason = dt.Year + "-12-01";

                startDateLastSeason = (dt.Year - 1) + "-03-01";
                endDateLastSeason = (dt.Year - 1) + "-12-01";
            }
            else if (sport == "NHL")
            {
                if(dt.Month >= 10 || dt.Month <= 6)
                {
                    if(dt.Month >= 10)
                    {
                        startDateCurrentSeason = dt.Year + "-10-01";
                        endDateCurrentSeason = (dt.Year + 1) + "-07-01";

                        startDateLastSeason = (dt.Year - 1) + "-10-01";
                        endDateLastSeason = (dt.Year) + "-07-01";
                    }
                    else
                    {
                        startDateCurrentSeason = (dt.Year - 1) + "-10-01";
                        endDateCurrentSeason = (dt.Year) + "-07-01";

                        startDateLastSeason = (dt.Year - 2) + "-10-01";
                        endDateLastSeason = (dt.Year - 1) + "-07-01";
                    }
                }else
                {
                    startDateCurrentSeason = (dt.Year) + "-10-01";
                    endDateCurrentSeason = (dt.Year + 1) + "-07-01";

                    startDateLastSeason = (dt.Year - 1) + "-10-01";
                    endDateLastSeason = (dt.Year) + "-07-01";
                }
            }
            else if (sport == "NBA")
            {
                if (dt.Month >= 10 || dt.Month <= 6)
                {
                    if (dt.Month >= 10)
                    {
                        startDateCurrentSeason = dt.Year + "-10-01";
                        endDateCurrentSeason = (dt.Year + 1) + "-07-01";

                        startDateLastSeason = (dt.Year - 1) + "-10-01";
                        endDateLastSeason = (dt.Year) + "-07-01";
                    }
                    else
                    {
                        startDateCurrentSeason = (dt.Year - 1) + "-10-01";
                        endDateCurrentSeason = (dt.Year) + "-07-01";

                        startDateLastSeason = (dt.Year - 2) + "-10-01";
                        endDateLastSeason = (dt.Year - 1) + "-07-01";
                    }
                }
                else
                {
                    startDateCurrentSeason = (dt.Year) + "-10-01";
                    endDateCurrentSeason = (dt.Year + 1) + "-07-01";

                    startDateLastSeason = (dt.Year - 1) + "-10-01";
                    endDateLastSeason = (dt.Year) + "-07-01";
                }
            }
            else if (sport == "NFL")
            {
                if (dt.Month >= 9 || dt.Month <= 2)
                {
                    if (dt.Month >= 9)
                    {
                        startDateCurrentSeason = dt.Year + "-09-01";
                        endDateCurrentSeason = (dt.Year + 1) + "-03-01";

                        startDateLastSeason = (dt.Year - 1) + "-09-01";
                        endDateLastSeason = (dt.Year) + "-03-01";
                    }
                    else
                    {
                        startDateCurrentSeason = (dt.Year - 1) + "-09-01";
                        endDateCurrentSeason = (dt.Year) + "-03-01";

                        startDateLastSeason = (dt.Year - 2) + "-09-01";
                        endDateLastSeason = (dt.Year - 1) + "-03-01";
                    }
                }
                else
                {
                    startDateCurrentSeason = (dt.Year) + "-09-01";
                    endDateCurrentSeason = (dt.Year + 1) + "-03-01";

                    startDateLastSeason = (dt.Year - 1) + "-09-01";
                    endDateLastSeason = (dt.Year) + "-03-01";
                }
            }
            else if (sport == "CFB")
            {
                if (dt.Month >= 8 || dt.Month <= 1)
                {
                    if (dt.Month >= 8)
                    {
                        startDateCurrentSeason = dt.Year + "-08-01";
                        endDateCurrentSeason = (dt.Year + 1) + "-02-01";

                        startDateLastSeason = (dt.Year - 1) + "-08-01";
                        endDateLastSeason = (dt.Year) + "-02-01";
                    }
                    else
                    {
                        startDateCurrentSeason = (dt.Year - 1) + "-08-01";
                        endDateCurrentSeason = (dt.Year) + "-02-01";

                        startDateLastSeason = (dt.Year - 2) + "-08-01";
                        endDateLastSeason = (dt.Year - 1) + "-02-01";
                    }
                }
                else
                {
                    startDateCurrentSeason = (dt.Year) + "-08-01";
                    endDateCurrentSeason = (dt.Year + 1) + "-02-01";

                    startDateLastSeason = (dt.Year - 1) + "-08-01";
                    endDateLastSeason = (dt.Year) + "-02-01";
                }
            }
            else if (sport == "CBB")
            {
                if (dt.Month >= 11 || dt.Month <= 4)
                {
                    if (dt.Month >= 11)
                    {
                        startDateCurrentSeason = dt.Year + "-11-01";
                        endDateCurrentSeason = (dt.Year + 1) + "-05-01";

                        startDateLastSeason = (dt.Year - 1) + "-11-01";
                        endDateLastSeason = (dt.Year) + "-05-01";
                    }
                    else
                    {
                        startDateCurrentSeason = (dt.Year - 1) + "-11-01";
                        endDateCurrentSeason = (dt.Year) + "-05-01";

                        startDateLastSeason = (dt.Year - 2) + "-11-01";
                        endDateLastSeason = (dt.Year - 1) + "-05-01";
                    }
                }
                else
                {
                    startDateCurrentSeason = (dt.Year) + "-11-01";
                    endDateCurrentSeason = (dt.Year + 1) + "-05-01";

                    startDateLastSeason = (dt.Year - 1) + "-11-01";
                    endDateLastSeason = (dt.Year) + "-05-01";
                }
            }
            else
            {
                startDateCurrentSeason = dt.Year + "-01-01";
                endDateCurrentSeason = (dt.Year + 1) + "-01-01";

                startDateLastSeason = (dt.Year - 1) + "-01-01";
                endDateLastSeason = (dt.Year) + "-01-01";
            }


        }
    }
}