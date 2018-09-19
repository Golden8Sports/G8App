using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de csSocProp
/// </summary>
public class csSocProp
{
    public int sportId { get; set; }
    public int leagueId { get; set; }
    public int idSpeacial { get; set; }
    public string betType { get; set; }
    public string name { get; set; }
    public DateTime cutOff { get; set; }
    public string category { get; set; }
    public string units { get; set; }
    public string status { get; set; }
    public int eventId { get; set; }
    public int? parentID { get; set; }
    public int periodNumber { get; set; }
    public DateTime Date { get; set; }
    public string homeTeam { get; set; }
    public string awayTeam { get; set; }
    public string Event { get; set; }
    public string League { get; set; }
    public long Last { get; set; }
    public string Prefix { get; set; }
    public int MaxBet { get; set; }

    //contestants and lines
    //contestant 1
    public int oppId1 { get; set; }
    public string oppName1 { get; set; }
    public int oppRot1 { get; set; }
    //line 1
    public int lineId1 { get; set; }
    public int? price1 { get; set; }
    public double? handicap1 { get; set; }

    //contestant 2
    public int oppId2 { get; set; }
    public string oppName2 { get; set; }
    public int oppRot2 { get; set; }
    //line 2
    public int lineId2 { get; set; }
    public int? price2 { get; set; }
    public double? handicap2 { get; set; }
    public string sport { get; set; }
    public double? Total { get; set; }
    public int? TotalOver { get; set; }
    public int? TotalUnder { get; set; }
    public string oppNameAux { get; set; }
    public int IdLeagueDGS { get; set; }


    public ObservableCollection<csContestant> contestantsList = new ObservableCollection<csContestant>();

    public csSocProp()
    {
        this.MaxBet = 0;
        this.price1 = 0;
        this.price2 = 0;
        this.sport = "";
        this.parentID = null;
    }

    public DateTime CastDate(string d)
    {
        DateTime dt = DateTimeOffset.Parse(d).UtcDateTime;
        TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        return TimeZoneInfo.ConvertTimeFromUtc(dt, easternZone);
    }
}