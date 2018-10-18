using Data.Connection;
using G8_App.Connection;
using G8_App.Entities.Scores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Logic.Scores
{
    public class blScores : csComponentsConnection
    {
        public bool InsertScore(csScore s)
        {
            try
            {

                int o;
                parameters.Clear();
                parameters.Add("@pAwayRot", Convert.ToInt32(s.awayRot.Trim()));
                parameters.Add("@pHomeRot", Convert.ToInt32(s.homeRot.Trim()));
                parameters.Add("@pAwayScore", (s.awayScore == "x" || !int.TryParse(s.awayScore, out o)) ? null : s.awayScore.Trim());
                parameters.Add("@pHomeScore", (s.homeScore == "x" || !int.TryParse(s.homeScore, out o)) ? null : s.homeScore.Trim());
                parameters.Add("@pPeriod", s.period);
                parameters.Add("@pDescription1", (String.IsNullOrWhiteSpace(s.description1) && (s.awayScore == "x")) ? "X" : s.description1);
                parameters.Add("@pSportId", Convert.ToInt32(s.sportId.Trim()));
                parameters.Add("@pDate", s.date);
                parameters.Add("@pDescription2", (String.IsNullOrWhiteSpace(s.description2) && (s.homeScore == "x")) ? "X" : s.description2);

                csDonBest.ExecutePAConfimation("[dbo].[web_insertScore]", parameters);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                parameters.Clear();
            }

            return true;
        }
    }
}