<%@ Page Language="C#" Title="" AutoEventWireup="true" MasterPageFile="~/Views/menu.Master" CodeBehind="GeneralProfile.aspx.cs" Inherits="G8_App.Views.GeneralProfile" %>


<asp:Content runat="server" ContentPlaceHolderID="head1" ID="headContent">

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://code.jquery.com/jquery-1.10.2.js"></script>
    <script src="../js/loading.js"></script>
    <script type="text/javascript">


      function Show(user)
      {
          $("#idModal").text('Player: ' + user);
          $("#MainContent_idUser").text(user);
          HideComponents('table_div', 'l1');
          HideComponents('div_League', 'l2');
          HideComponents('div_Period', 'l3');
          HideComponents('div_FavDog', 'l4');
          HideComponents('div_WagerType', 'l5');
          HideComponents('div_Side', 'l6');
          HideComponents('div_Week', 'l7');
          HideComponents('div_MomentDay', 'l8');
          HideComponents('div_Team', 'l9');
          HideComponents('div_Bets', '20');
          HideComp('sportChart');
          HideComp('weekChart');
          HideComp('momentDayChart');
         
          var parameter = { 'startD': $("#MainContent_startDate").val(), 'endD': $("#MainContent_endDate").val(), 'player': user };

          $.ajax({                    
            type: 'POST',
            url: 'GeneralProfile.aspx/BySport',
            data: JSON.stringify(parameter),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data)
            {
                drawTable(data.d);
                ShowComponents('table_div', 'l1');
                ShowComp('sportChart');
            }
          });
          ByLeagueAJX();
          ByPeriodAJX();
          ByFavDogAJX();
          BySideAJX();
          ByWagerTypeAJX();
          $.ajax({                    
            type: 'POST',
            url: 'GeneralProfile.aspx/ByWeek',
            data: JSON.stringify(parameter),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data)
            {
                drawTableWeekDay(data.d);
                ShowComponents('div_Week', 'l7');
                ShowComp('weekChart');
            }
          });
          ByBetsAJX();
          $.ajax({                    
            type: 'POST',
            url: 'GeneralProfile.aspx/ByMomentDay',
            data: JSON.stringify(parameter),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data)
            {
                drawTableMomentDay(data.d);
                ShowComponents('div_MomentDay', 'l8');
                ShowComp('momentDayChart');
            }
          });
          $.ajax({                    
            type: 'POST',
            url: 'GeneralProfile.aspx/ByTeam',
            data: JSON.stringify(parameter),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data)
            {
                drawTableTeam(data.d);
                ShowComponents('div_Team', 'l9');
            }
          });
         LeaguesPlayerAJX('slt','#MainContent_inBySportPeriod');
         LeaguesPlayerAJX('inByLeagueFavDog','#MainContent_inBySportFavDog');
         LeaguesPlayerAJX('inByLeagueWagerType','#MainContent_inBySportWagerType');
         LeaguesPlayerAJX('inByLeagueSide','#MainContent_inBySportSide');
         LeaguesPlayerAJX('inByLeagueBets','#MainContent_inBySportBets');
         if ($("#MainContent_idFilters").text() != "Sport: ALL") {
              $("#MainContent_secSportWagerType").hide();
              $("#MainContent_secSportPeriod").hide();
              $("#MainContent_secSportFAvDog").hide();
              $("#MainContent_secSportSide").hide();
              $("#MainContent_secSportLeague").hide();
              $("#MainContent_secSportBets").hide();
          } else
          {
              $("#MainContent_secSportWagerType").show();
              $("#MainContent_secSportPeriod").show();
              $("#MainContent_secSportFAvDog").show();
              $("#MainContent_secSportSide").show();
              $("#MainContent_secSportLeague").show();
              $("#MainContent_secSportBets").sho();
          }
         if ($("#MainContent_idLeagueFilter").text() != "League: ALL") {
              $("#MainContent_secLeagueWagerType").hide();
              $("#MainContent_secLeaguePeriod").hide();
              $("#MainContent_secLeagueFavDog").hide();
              $("#MainContent_secLeagueWagerType").hide();
              $("#MainContent_secLeagueSide").hide();
              $("#MainContent_secLeagueBets").hide();
          } else
          {
              $("#MainContent_secLeagueWagerType").show();
              $("#MainContent_secLeaguePeriod").show();
              $("#MainContent_secLeagueFavDog").show();
              $("#MainContent_secLeagueWagerType").show();
              $("#MainContent_secLeagueSide").show();
              $("#MainContent_secLeagueBets").sho();
          }
      }

        function ByLeagueAJX()
        {
            HideComponents('div_League', 'l2');
            var parameter = { 'startD': $("#MainContent_startDate").val(), 'endD': $("#MainContent_endDate").val(), 'player': $("#MainContent_idUser").text(), 'sport': $("#MainContent_inBySport").val(), 'favdog' : $("#inFavDogLeague").val() };   
              $.ajax({                    
                type: 'POST',
                url: 'GeneralProfile.aspx/ByLeagueR',
                data: JSON.stringify(parameter),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data)
                {
                    if (data.d != 'null' && data.d != null)
                    {
                        drawTableLeague(data.d);
                        ShowComponents('div_League', 'l2');

                    } else
                    {
                        HideAll('div_League', 'l2');
                        HideComp('div_League');
                    }                    
                }
              });
        }
        function BySideAJX()
        {
              HideComponents('div_Side', 'l6');
              var parameter = { 'startD': $("#MainContent_startDate").val(), 'endD': $("#MainContent_endDate").val(), 'player': $("#MainContent_idUser").text(), 'sport': $("#MainContent_inBySportSide").val(), 'league' : $("#inByLeagueSide").val(), 'favdog' : $("#inFavDogSide").val() };   
              $.ajax({                    
                type: 'POST',
                url: 'GeneralProfile.aspx/BySide',
                data: JSON.stringify(parameter),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data)
                {
                    if (data.d != 'null' && data.d != null)
                    {
                        drawTableSide(data.d);
                        ShowComponents('div_Side', 'l6');

                    } else
                    {
                        HideAll('div_Side', 'l6');
                        HideComp('div_Side');
                    }                    
                }
              });
        }
        function ByFavDogAJX()
        {
            HideComponents('div_FavDog', 'l4');
            var parameter = { 'startD': $("#MainContent_startDate").val(), 'endD': $("#MainContent_endDate").val(), 'player': $("#MainContent_idUser").text(), 'sport': $("#MainContent_inBySportFavDog").val(), 'league' : $("#inByLeagueFavDog").val() };   
              $.ajax({                    
                type: 'POST',
                url: 'GeneralProfile.aspx/ByFavDogR',
                data: JSON.stringify(parameter),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data)
                {
                    if (data.d != 'null' && data.d != null)
                    {
                        drawTableFavDog(data.d);
                        ShowComponents('div_FavDog', 'l4');
                    } else
                    {
                        HideAll('div_FavDog', 'l4');
                        HideComp('div_FavDog');
                    }                    
                }
              });
        }
        function ByPeriodAJX()
        {
            HideComponents('div_Period', 'l3');
            var parameter = { 'startD': $("#MainContent_startDate").val(), 'endD': $("#MainContent_endDate").val(), 'player': $("#MainContent_idUser").text(), 'sport': $("#MainContent_inBySportPeriod").val(), 'league' : $("#slt").val(), 'favdog' : $("#inFavDogPeriod").val() };   
              $.ajax({                    
                type: 'POST',
                url: 'GeneralProfile.aspx/ByPeriodR',
                data: JSON.stringify(parameter),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data)
                {
                    if (data.d != 'null' && data.d != null)
                    {
                        drawTablePeriod(data.d);
                        ShowComponents('div_Period', 'l3');

                    } else
                    {
                        HideAll('div_Period', 'l3');
                        HideComp('div_Period');
                    }                    
                }
              });
        }
        function ByWagerTypeAJX()
        {
            HideComponents('div_WagerType', 'l5');
            var parameter = { 'startD': $("#MainContent_startDate").val(), 'endD': $("#MainContent_endDate").val(), 'player': $("#MainContent_idUser").text(), 'sport': $("#MainContent_inBySportWagerType").val(), 'league' : $("#inByLeagueWagerType").val(), 'favdog' : $("#inFavDogWagerType").val() };   
              $.ajax({                    
                type: 'POST',
                url: 'GeneralProfile.aspx/ByWagerPlayR',
                data: JSON.stringify(parameter),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data)
                {
                    if (data.d != 'null' && data.d != null)
                    {
                        drawTableWagerType(data.d);
                        ShowComponents('div_WagerType', 'l5');

                    } else
                    {
                        HideAll('div_WagerType', 'l5');
                        HideComp('div_WagerType');
                    }                    
                }
              });
        }
        function LeaguesPlayerAJX(id, sel)
        {
            var parameter = { 'd1': $("#MainContent_startDate").val(), 'd2': $("#MainContent_endDate").val(), 'player': $("#MainContent_idUser").text(), 'sport': $(sel).val() };   
              $.ajax({                    
                type: 'POST',
                url: 'GeneralProfile.aspx/GetLeaguesPLayer',
                data: JSON.stringify(parameter),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data)
                {
                    ClearSelect(id);
                    if (data.d != null && data.d != 'NULL')
                    {
                        var obj = JSON.parse(data.d);
                        var index = obj.length;
                        for (i = 0; i < index; i++) {
                            var l = obj[i]["IdLeague"];
                            var d = obj[i]["LeagueDescription"];
                            $('#' + id).append('<option value="' + l + '">' + d + '</option>');
                        }

                    }
                }
              });
        }
        function ByBetsAJX()
        {
           HideComponents('div_Bets', '20');   
           var parameter = { 'startD': $("#MainContent_startDate").val(), 'endD': $("#MainContent_endDate").val(), 'player': $("#MainContent_idUser").text(), 'sport': $("#MainContent_inBySportBets").val(), 'league' : $("#inByLeagueBets").val(), 'favdog' : $("#inFavDogBets").val(), 'wagerplay' :  $("#inFavDogWagerPlay").val()};
          
          $.ajax({                    
            type: 'POST',
            url: 'GeneralProfile.aspx/GetBets',
            data: JSON.stringify(parameter),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data)
            {                
               if (data.d != 'null' && data.d != null)
               {
                  DrawBets(data.d);
                  ShowComponents('div_Bets', '20');
               } else
               {
                 HideAll('div_Bets', '20');
                 HideComp('div_Bets');
               }    
            }
          });
        }
      </script>



   <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
   <script lang=Javascript>
      google.charts.load('current', {'packages':['table']});
      google.charts.load('current', {packages: ['corechart', 'bar']});
      google.charts.load('current', { packages: ['corechart', 'line'] });
      google.charts.load('current', {packages: ['corechart', 'bar']});
      google.charts.load('current', { 'packages': ['line'] });
      google.charts.load('current', {'packages':['corechart']});
      google.charts.setOnLoadCallback(drawTable);

       function isNumberKey(evt)
       {
         var charCode = (evt.which) ? evt.which : evt.keyCode;
         if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;    
         return true;
       }
       function ShowComponents(t,d)
       {
           obj = document.getElementById(t);
           obj.style.display = 'block';

           obj2 = document.getElementById(d);
           obj2.style.display = 'none';
       }
       function ClearSelect(id)
       {
           document.getElementById(id).options.length = 0
           document.getElementById(id).className = "form-control chosen-select";
       }
       function AddOption(id,txt,v)
       {          
           x = document.getElementById(id);
           option = document.createElement("option");
           option.text = txt;
           option.val = v;
           x.add(option);
           document.getElementById(id).className = "form-control chosen-select";
       }

       function HideComponents(t,d)
       {
           obj = document.getElementById(t);
           obj.style.display = 'none';
           obj2 = document.getElementById(d);
           obj2.style.display = 'block';
       }
       function HideAll(t,d)
       {
           obj = document.getElementById(t);
           obj.style.display = 'none';
           obj2 = document.getElementById(d);
           obj2.style.display = 'none';
       }
       function HideComp(t)
       {
           obj = document.getElementById(t);
           obj.style.display = 'none';
       }
       function ShowComp(t)
       {
           obj = document.getElementById(t);
           obj.style.display = 'block';
       }
       function show(bloq)
       {
           obj = document.getElementById(bloq);
           obj.style.display = (obj.style.display == 'none') ? 'block' : 'none';
       }     
       function drawTable(info)
       {
           var data = new google.visualization.DataTable();
           var wd = new google.visualization.DataTable();
           data.addColumn('string', 'Sport');
           data.addColumn('number', 'Risk Amo');
           data.addColumn('number', 'Win Amo');
           data.addColumn('number', 'Net');
           data.addColumn('number', 'Win%');
           data.addColumn('number', 'Hold%');
           data.addColumn('number', 'Bets');
           data.addColumn('number', 'Wins');
           data.addColumn('number', 'Loses');
           data.addColumn('number', 'Push');
           data.addColumn('number', 'Scalp Pinni%');
           data.addColumn('number', 'Scalp Jaz%');
           data.addColumn('number', 'Scalp Cris%');
           data.addColumn('number', 'Scalp PPH%');
           data.addColumn('number', 'Scalp 5Dimes%');
           data.addColumn('number', 'Move Line%');
           data.addColumn('number', 'Beat Line%');
           wd.addColumn('string', 'WeekDay');
           wd.addColumn('number', 'Bets');

           var obj = JSON.parse(info);
           var index = obj.length;      
           data.addRows(index);
           wd.addRows(index);
           for (i = 0; i < index; i++)
           { 
             data.setCell(i, 0, obj[i]["Sport"]);
             data.setCell(i, 1, obj[i]["RiskAmount"]);
             data.setCell(i, 2, obj[i]["WinAmount"]);
             data.setCell(i, 3, obj[i]["Net"]);
             data.setCell(i, 4, obj[i]["WinPercentaje"]);
             data.setCell(i, 5, obj[i]["HoldPercentaje"]);
             data.setCell(i, 6, obj[i]["Bets"]);
             data.setCell(i, 7, obj[i]["CountWins"]);
             data.setCell(i, 8, obj[i]["CountLoses"]);
             data.setCell(i, 9, obj[i]["CountDraws"]);
             data.setCell(i, 10, obj[i]["ScalpingPinni"]);
             data.setCell(i, 11, obj[i]["ScalpingJazz"]);
             data.setCell(i, 12, obj[i]["ScalpingCris"]);
             data.setCell(i, 13, obj[i]["ScalpingPPH"]);
             data.setCell(i, 14, obj[i]["Scalping5Dimes"]);
             data.setCell(i, 15, obj[i]["MoveLine"]);
             data.setCell(i, 16, obj[i]["BeatLine"]);
             wd.setCell(i,0,obj[i]["Sport"]);
             wd.setCell(i,1,obj[i]["Bets"]);
           }

           var options = {
              title: 'Count by Sport',
              is3D: true,
              width: '100%'
           };

           var table = new google.visualization.Table(document.getElementById('table_div'));
           var formatter = new google.visualization.ArrowFormat();
           formatter.format(data, 3);
           formatter.format(data, 5);
           table.draw(data, { showRowNumber: false, width: '100%', height: '100%' });

           var t2 = new google.visualization.PieChart(document.getElementById('sportChart'));
           t2.draw(wd, options);
       }

       function DrawBets(info)
       {
           var data = new google.visualization.DataTable();
           data.addColumn('string', 'Event Date');
           data.addColumn('string', 'Visitor Team');
           data.addColumn('string', 'Home Team');
           data.addColumn('string', 'Sport');
           data.addColumn('string', 'League');
           data.addColumn('string', 'Placed Date');
           data.addColumn('number', 'Points');
           data.addColumn('number', 'Odds');
           data.addColumn('string', 'Wager Play');
           data.addColumn('string', 'Fav/Dog');
           data.addColumn('number', 'Risk');
           data.addColumn('number', 'Net');
           data.addColumn('string', 'Description');

           var obj = JSON.parse(info);
           var index = obj.length;
         
           data.addRows(index);
           for (i = 0; i < index; i++)
           { 
             data.setCell(i, 0, obj[i]["EventDateString"]);
             data.setCell(i, 1, obj[i]["VisitorTem"]);
             data.setCell(i, 2, obj[i]["HomeTem"]);
             data.setCell(i, 3, obj[i]["Sport"]);
             data.setCell(i, 4, obj[i]["League"]);
             data.setCell(i, 5, obj[i]["PlacedDateString"]);
             data.setCell(i, 6, obj[i]["Points"]);
             data.setCell(i, 7, obj[i]["Odds"]);
             data.setCell(i, 8, obj[i]["WagerType"]);
             data.setCell(i, 9, obj[i]["FavDog"]);
             data.setCell(i, 10, obj[i]["RiskAmount"]);
             data.setCell(i, 11, obj[i]["Net"]);
             data.setCell(i, 12, obj[i]["DetailDescription"]);
             
           }
           var table = new google.visualization.Table(document.getElementById('div_Bets'));
           var formatter = new google.visualization.ArrowFormat();
           formatter.format(data, 11);
           table.draw(data, { showRowNumber: false, width: '100%', height: '100%' });
       }

       function drawTableLeague(info)
       {
           var data = new google.visualization.DataTable();
           data.addColumn('string', 'League');
           data.addColumn('number', 'Risk Amo');
           data.addColumn('number', 'Win Amo');
           data.addColumn('number', 'Net');
           data.addColumn('number', 'Win%');
           data.addColumn('number', 'Hold%');
           data.addColumn('number', 'Bets');
           data.addColumn('number', 'Wins');
           data.addColumn('number', 'Loses');
           data.addColumn('number', 'Push');
           data.addColumn('number', 'Scalp Pinni%');
           data.addColumn('number', 'Scalp Jaz%');
           data.addColumn('number', 'Scalp Cris%');
           data.addColumn('number', 'Scalp PPH%');
           data.addColumn('number', 'Scalp 5Dimes%');
           data.addColumn('number', 'Move Line%');
           data.addColumn('number', 'Beat Line%');

           var obj = JSON.parse(info);
           var index = obj.length;
         
           data.addRows(index);
           for (i = 0; i < index; i++)
           { 
             data.setCell(i, 0, obj[i]["League"]);
             data.setCell(i, 1, obj[i]["RiskAmount"]);
             data.setCell(i, 2, obj[i]["WinAmount"]);
             data.setCell(i, 3, obj[i]["Net"]);
             data.setCell(i, 4, obj[i]["WinPercentaje"]);
             data.setCell(i, 5, obj[i]["HoldPercentaje"]);
             data.setCell(i, 6, obj[i]["Bets"]);
             data.setCell(i, 7, obj[i]["CountWins"]);
             data.setCell(i, 8, obj[i]["CountLoses"]);
             data.setCell(i, 9, obj[i]["CountDraws"]);
             data.setCell(i, 10, obj[i]["ScalpingPinni"]);
             data.setCell(i, 11, obj[i]["ScalpingJazz"]);
             data.setCell(i, 12, obj[i]["ScalpingCris"]);
             data.setCell(i, 13, obj[i]["ScalpingPPH"]);
             data.setCell(i, 14, obj[i]["Scalping5Dimes"]);
             data.setCell(i, 15, obj[i]["MoveLine"]);
             data.setCell(i, 16, obj[i]["BeatLine"]);         
           }

           var table = new google.visualization.Table(document.getElementById('div_League'));
           var formatter = new google.visualization.ArrowFormat();
           formatter.format(data, 3);
           formatter.format(data, 5);
           table.draw(data, { showRowNumber: false, width: '100%', height: '100%' });
       }
       // Draw table by period
       function drawTablePeriod(info)
       {
           var data = new google.visualization.DataTable();
           data.addColumn('string', 'Period');
           data.addColumn('number', 'Risk Amo');
           data.addColumn('number', 'Win Amo');
           data.addColumn('number', 'Net');
           data.addColumn('number', 'Win%');
           data.addColumn('number', 'Hold%');
           data.addColumn('number', 'Bets');
           data.addColumn('number', 'Wins');
           data.addColumn('number', 'Loses');
           data.addColumn('number', 'Push');
           data.addColumn('number', 'Scalp Pinni%');
           data.addColumn('number', 'Scalp Jaz%');
           data.addColumn('number', 'Scalp Cris%');
           data.addColumn('number', 'Scalp PPH%');
           data.addColumn('number', 'Scalp 5Dimes%');
           data.addColumn('number', 'Move Line%');
           data.addColumn('number', 'Beat Line%');

           var obj = JSON.parse(info);
           var index = obj.length;
         
           data.addRows(index);
           for (i = 0; i < index; i++)
           { 
             data.setCell(i, 0, obj[i]["GamePeriod"]);
             data.setCell(i, 1, obj[i]["RiskAmount"]);
             data.setCell(i, 2, obj[i]["WinAmount"]);
             data.setCell(i, 3, obj[i]["Net"]);
             data.setCell(i, 4, obj[i]["WinPercentaje"]);
             data.setCell(i, 5, obj[i]["HoldPercentaje"]);
             data.setCell(i, 6, obj[i]["Bets"]);
             data.setCell(i, 7, obj[i]["CountWins"]);
             data.setCell(i, 8, obj[i]["CountLoses"]);
             data.setCell(i, 9, obj[i]["CountDraws"]);
             data.setCell(i, 10, obj[i]["ScalpingPinni"]);
             data.setCell(i, 11, obj[i]["ScalpingJazz"]);
             data.setCell(i, 12, obj[i]["ScalpingCris"]);
             data.setCell(i, 13, obj[i]["ScalpingPPH"]);
             data.setCell(i, 14, obj[i]["Scalping5Dimes"]);
             data.setCell(i, 15, obj[i]["MoveLine"]);
             data.setCell(i, 16, obj[i]["BeatLine"]);
             
           }
           var table = new google.visualization.Table(document.getElementById('div_Period'));
           var formatter = new google.visualization.ArrowFormat();
           formatter.format(data, 3);
           formatter.format(data, 5);
           table.draw(data, { showRowNumber: false, width: '100%', height: '100%' });
       }
       // Draw table by side
       function drawTableSide(info)
       {
           var data = new google.visualization.DataTable();
           data.addColumn('string', 'Side');
           data.addColumn('number', 'Risk Amo');
           data.addColumn('number', 'Win Amo');
           data.addColumn('number', 'Net');
           data.addColumn('number', 'Win%');
           data.addColumn('number', 'Hold%');
           data.addColumn('number', 'Bets');
           data.addColumn('number', 'Wins');
           data.addColumn('number', 'Loses');
           data.addColumn('number', 'Push');
           data.addColumn('number', 'Scalp Pinni%');
           data.addColumn('number', 'Scalp Jaz%');
           data.addColumn('number', 'Scalp Cris%');
           data.addColumn('number', 'Scalp PPH%');
           data.addColumn('number', 'Scalp 5Dimes%');
           data.addColumn('number', 'Move Line%');
           data.addColumn('number', 'Beat Line%');

           var obj = JSON.parse(info);
           var index = obj.length;
         
           data.addRows(index);
           for (i = 0; i < index; i++)
           { 
             data.setCell(i, 0, obj[i]["Side"]);
             data.setCell(i, 1, obj[i]["RiskAmount"]);
             data.setCell(i, 2, obj[i]["WinAmount"]);
             data.setCell(i, 3, obj[i]["Net"]);
             data.setCell(i, 4, obj[i]["WinPercentaje"]);
             data.setCell(i, 5, obj[i]["HoldPercentaje"]);
             data.setCell(i, 6, obj[i]["Bets"]);
             data.setCell(i, 7, obj[i]["CountWins"]);
             data.setCell(i, 8, obj[i]["CountLoses"]);
             data.setCell(i, 9, obj[i]["CountDraws"]);
             data.setCell(i, 10, obj[i]["ScalpingPinni"]);
             data.setCell(i, 11, obj[i]["ScalpingJazz"]);
             data.setCell(i, 12, obj[i]["ScalpingCris"]);
             data.setCell(i, 13, obj[i]["ScalpingPPH"]);
             data.setCell(i, 14, obj[i]["Scalping5Dimes"]);
             data.setCell(i, 15, obj[i]["MoveLine"]);
             data.setCell(i, 16, obj[i]["BeatLine"]);
             
           }
           var table = new google.visualization.Table(document.getElementById('div_Side'));
           var formatter = new google.visualization.ArrowFormat();
           formatter.format(data, 3);
           formatter.format(data, 5);
           table.draw(data, { showRowNumber: false, width: '100%', height: '100%' });
       }
       // Draw table by side
       function drawTableFavDog(info)
       {
           var data = new google.visualization.DataTable();
           data.addColumn('string', 'Fav/Dog');
           data.addColumn('number', 'Risk Amo');
           data.addColumn('number', 'Win Amo');
           data.addColumn('number', 'Net');
           data.addColumn('number', 'Win%');
           data.addColumn('number', 'Hold%');
           data.addColumn('number', 'Bets');
           data.addColumn('number', 'Wins');
           data.addColumn('number', 'Loses');
           data.addColumn('number', 'Push');
           data.addColumn('number', 'Scalp Pinni%');
           data.addColumn('number', 'Scalp Jaz%');
           data.addColumn('number', 'Scalp Cris%');
           data.addColumn('number', 'Scalp PPH%');
           data.addColumn('number', 'Scalp 5Dimes%');
           data.addColumn('number', 'Move Line%');
           data.addColumn('number', 'Beat Line%');

           var obj = JSON.parse(info);
           var index = obj.length;
         
           data.addRows(index);
           for (i = 0; i < index; i++)
           { 
             data.setCell(i, 0, obj[i]["FavDog"]);
             data.setCell(i, 1, obj[i]["RiskAmount"]);
             data.setCell(i, 2, obj[i]["WinAmount"]);
             data.setCell(i, 3, obj[i]["Net"]);
             data.setCell(i, 4, obj[i]["WinPercentaje"]);
             data.setCell(i, 5, obj[i]["HoldPercentaje"]);
             data.setCell(i, 6, obj[i]["Bets"]);
             data.setCell(i, 7, obj[i]["CountWins"]);
             data.setCell(i, 8, obj[i]["CountLoses"]);
             data.setCell(i, 9, obj[i]["CountDraws"]);
             data.setCell(i, 10, obj[i]["ScalpingPinni"]);
             data.setCell(i, 11, obj[i]["ScalpingJazz"]);
             data.setCell(i, 12, obj[i]["ScalpingCris"]);
             data.setCell(i, 13, obj[i]["ScalpingPPH"]);
             data.setCell(i, 14, obj[i]["Scalping5Dimes"]);
             data.setCell(i, 15, obj[i]["MoveLine"]);
             data.setCell(i, 16, obj[i]["BeatLine"]);
             
           }
           var table = new google.visualization.Table(document.getElementById('div_FavDog'));
           var formatter = new google.visualization.ArrowFormat();
           formatter.format(data, 3);
           formatter.format(data, 5);
           table.draw(data, { showRowNumber: false, width: '100%', height: '100%' });
       }
       // Draw table by wager type
       function drawTableWagerType(info)
       {
           var data = new google.visualization.DataTable();
           data.addColumn('string', 'Wager Type');
           data.addColumn('string', 'Side');
           data.addColumn('number', 'Risk Amo');
           data.addColumn('number', 'Win Amo');
           data.addColumn('number', 'Net');
           data.addColumn('number', 'Win%');
           data.addColumn('number', 'Hold%');
           data.addColumn('number', 'Bets');
           data.addColumn('number', 'Wins');
           data.addColumn('number', 'Loses');
           data.addColumn('number', 'Push');
           data.addColumn('number', 'Scalp Pinni%');
           data.addColumn('number', 'Scalp Jaz%');
           data.addColumn('number', 'Scalp Cris%');
           data.addColumn('number', 'Scalp PPH%');
           data.addColumn('number', 'Scalp 5Dimes%');
           data.addColumn('number', 'Move Line%');
           data.addColumn('number', 'Beat Line%');

           var obj = JSON.parse(info);
           var index = obj.length;
         
           data.addRows(index);
           for (i = 0; i < index; i++)
           { 
             data.setCell(i, 0, obj[i]["WagerType"]);
             data.setCell(i, 1, obj[i]["Side"]);
             data.setCell(i, 2, obj[i]["RiskAmount"]);
             data.setCell(i, 3, obj[i]["WinAmount"]);
             data.setCell(i, 4, obj[i]["Net"]);
             data.setCell(i, 5, obj[i]["WinPercentaje"]);
             data.setCell(i, 6, obj[i]["HoldPercentaje"]);
             data.setCell(i, 7, obj[i]["Bets"]);
             data.setCell(i, 8, obj[i]["CountWins"]);
             data.setCell(i, 9, obj[i]["CountLoses"]);
             data.setCell(i, 10, obj[i]["CountDraws"]);
             data.setCell(i, 11, obj[i]["ScalpingPinni"]);
             data.setCell(i, 12, obj[i]["ScalpingJazz"]);
             data.setCell(i, 13, obj[i]["ScalpingCris"]);
             data.setCell(i, 14, obj[i]["ScalpingPPH"]);
             data.setCell(i, 15, obj[i]["Scalping5Dimes"]);
             data.setCell(i, 16, obj[i]["MoveLine"]);
             data.setCell(i, 17, obj[i]["BeatLine"]);
             
           }
           var table = new google.visualization.Table(document.getElementById('div_WagerType'));
           var formatter = new google.visualization.ArrowFormat();
           formatter.format(data, 4);
           formatter.format(data, 6);
           table.draw(data, { showRowNumber: false, width: '100%', height: '100%' });
       }
       // Draw table by weekday
       function drawTableWeekDay(info)
       {
           var data = new google.visualization.DataTable();
           var wd = new google.visualization.DataTable();
           data.addColumn('string', 'WeekDay');
           data.addColumn('number', 'Risk Amo');
           data.addColumn('number', 'Win Amo');
           data.addColumn('number', 'Net');
           data.addColumn('number', 'Win%');
           data.addColumn('number', 'Hold%');
           data.addColumn('number', 'Bets');
           data.addColumn('number', 'Wins');
           data.addColumn('number', 'Loses');
           data.addColumn('number', 'Push');
           data.addColumn('number', 'Scalp Pinni%');
           data.addColumn('number', 'Scalp Jaz%');
           data.addColumn('number', 'Scalp Cris%');
           data.addColumn('number', 'Scalp PPH%');
           data.addColumn('number', 'Scalp 5Dimes%');
           data.addColumn('number', 'Move Line%');
           data.addColumn('number', 'Beat Line%');
           wd.addColumn('string', 'WeekDay');
           wd.addColumn('number', 'Bets');

           var obj = JSON.parse(info);
           var index = obj.length;
         
           data.addRows(index);
           wd.addRows(index);
           for (i = 0; i < index; i++)
           { 
             data.setCell(i, 0, obj[i]["Day"]);
             data.setCell(i, 1, obj[i]["RiskAmount"]);
             data.setCell(i, 2, obj[i]["WinAmount"]);
             data.setCell(i, 3, obj[i]["Net"]);
             data.setCell(i, 4, obj[i]["WinPercentaje"]);
             data.setCell(i, 5, obj[i]["HoldPercentaje"]);
             data.setCell(i, 6, obj[i]["Bets"]);
             data.setCell(i, 7, obj[i]["CountWins"]);
             data.setCell(i, 8, obj[i]["CountLoses"]);
             data.setCell(i, 9, obj[i]["CountDraws"]);
             data.setCell(i, 10, obj[i]["ScalpingPinni"]);
             data.setCell(i, 11, obj[i]["ScalpingJazz"]);
             data.setCell(i, 12, obj[i]["ScalpingCris"]);
             data.setCell(i, 13, obj[i]["ScalpingPPH"]);
             data.setCell(i, 14, obj[i]["Scalping5Dimes"]);
             data.setCell(i, 15, obj[i]["MoveLine"]);
             data.setCell(i, 16, obj[i]["BeatLine"]);
             wd.setCell(i,0,obj[i]["Day"]);
             wd.setCell(i,1,obj[i]["Bets"]);
           }


         var options = {
            title: 'Count per Weekday',
            hAxis: {
              title: 'WeekDay',
            },
            vAxis: {
              title: 'Bets'
            },
            height: 450,
            width:1000,
         };

           var table = new google.visualization.Table(document.getElementById('div_Week'));
           var formatter = new google.visualization.ArrowFormat();
           formatter.format(data, 3);
           formatter.format(data, 5);
           table.draw(data, { showRowNumber: false, width: '100%', height: '100%' });

           var t2 = new google.visualization.ColumnChart(document.getElementById('weekChart'));
           t2.draw(wd, options);

       }
       // Draw table by momentDay
       function drawTableMomentDay(info)
       {
           var data = new google.visualization.DataTable();
           var wd = new google.visualization.DataTable();
           data.addColumn('string', 'Moment Day');
           data.addColumn('number', 'Risk Amo');
           data.addColumn('number', 'Win Amo');
           data.addColumn('number', 'Net');
           data.addColumn('number', 'Win%');
           data.addColumn('number', 'Hold%');
           data.addColumn('number', 'Bets');
           data.addColumn('number', 'Wins');
           data.addColumn('number', 'Loses');
           data.addColumn('number', 'Push');
           data.addColumn('number', 'Scalp Pinni%');
           data.addColumn('number', 'Scalp Jaz%');
           data.addColumn('number', 'Scalp Cris%');
           data.addColumn('number', 'Scalp PPH%');
           data.addColumn('number', 'Scalp 5Dimes%');
           data.addColumn('number', 'Move Line%');
           data.addColumn('number', 'Beat Line%');
           wd.addColumn('string', 'WeekDay');
           wd.addColumn('number', 'Bets');

           var obj = JSON.parse(info);
           var index = obj.length;
         
           data.addRows(index);
           wd.addRows(index);
           for (i = 0; i < index; i++)
           { 
             data.setCell(i, 0, obj[i]["MomentDay"]);
             data.setCell(i, 1, obj[i]["RiskAmount"]);
             data.setCell(i, 2, obj[i]["WinAmount"]);
             data.setCell(i, 3, obj[i]["Net"]);
             data.setCell(i, 4, obj[i]["WinPercentaje"]);
             data.setCell(i, 5, obj[i]["HoldPercentaje"]);
             data.setCell(i, 6, obj[i]["Bets"]);
             data.setCell(i, 7, obj[i]["CountWins"]);
             data.setCell(i, 8, obj[i]["CountLoses"]);
             data.setCell(i, 9, obj[i]["CountDraws"]);
             data.setCell(i, 10, obj[i]["ScalpingPinni"]);
             data.setCell(i, 11, obj[i]["ScalpingJazz"]);
             data.setCell(i, 12, obj[i]["ScalpingCris"]);
             data.setCell(i, 13, obj[i]["ScalpingPPH"]);
             data.setCell(i, 14, obj[i]["Scalping5Dimes"]);
             data.setCell(i, 15, obj[i]["MoveLine"]);
             data.setCell(i, 16, obj[i]["BeatLine"]);
             wd.setCell(i,0,obj[i]["MomentDay"]);
             wd.setCell(i,1,obj[i]["Bets"]);
           }

           var options = {
                title: 'Count per MomentDay',
                hAxis: {
                  title: 'MomentDay',
                },
                vAxis: {
                  title: 'Bets'
                },
                animation:{
                  duration: 1000,
                  easing: 'out',
               },
                height: 450,
                width: 1000,
           };

           var table = new google.visualization.Table(document.getElementById('div_MomentDay'));
           var formatter = new google.visualization.ArrowFormat();
           formatter.format(data, 3);
           formatter.format(data, 5);
           table.draw(data, { showRowNumber: false, width: '100%', height: '100%' });

           var t2 = new google.visualization.ColumnChart(document.getElementById('momentDayChart'));
           t2.draw(wd, options);
       }
      // draw table by team
      function drawTableTeam(info)
       {
           var data = new google.visualization.DataTable();
           data.addColumn('string', 'Team');
           data.addColumn('string', 'Sport');
           data.addColumn('number', 'Risk Amo');
           data.addColumn('number', 'Win Amo');
           data.addColumn('number', 'Net');
           data.addColumn('number', 'Win%');
           data.addColumn('number', 'Hold%');
           data.addColumn('number', 'Bets');
           data.addColumn('number', 'Wins');
           data.addColumn('number', 'Loses');
           data.addColumn('number', 'Push');
           data.addColumn('number', 'Scalp Pinni%');
           data.addColumn('number', 'Scalp Jaz%');
           data.addColumn('number', 'Scalp Cris%');
           data.addColumn('number', 'Scalp PPH%');
           data.addColumn('number', 'Scalp 5Dimes%');
           data.addColumn('number', 'Move Line%');
           data.addColumn('number', 'Beat Line%');

           var obj = JSON.parse(info);
           var index = obj.length;
         
           data.addRows(index);
           for (i = 0; i < index; i++)
           { 
             data.setCell(i, 0, obj[i]["Team"]);
             data.setCell(i, 1, obj[i]["Sport"]);
             data.setCell(i, 2, obj[i]["RiskAmount"]);
             data.setCell(i, 3, obj[i]["WinAmount"]);
             data.setCell(i, 4, obj[i]["Net"]);
             data.setCell(i, 5, obj[i]["WinPercentaje"]);
             data.setCell(i, 6, obj[i]["HoldPercentaje"]);
             data.setCell(i, 7, obj[i]["Bets"]);
             data.setCell(i, 8, obj[i]["CountWins"]);
             data.setCell(i, 9, obj[i]["CountLoses"]);
             data.setCell(i, 10, obj[i]["CountDraws"]);
             data.setCell(i, 11, obj[i]["ScalpingPinni"]);
             data.setCell(i, 12, obj[i]["ScalpingJazz"]);
             data.setCell(i, 13, obj[i]["ScalpingCris"]);
             data.setCell(i, 14, obj[i]["ScalpingPPH"]);
             data.setCell(i, 15, obj[i]["Scalping5Dimes"]);
             data.setCell(i, 16, obj[i]["MoveLine"]);
             data.setCell(i, 17, obj[i]["BeatLine"]);
             
           }
           var table = new google.visualization.Table(document.getElementById('div_Team'));
           var formatter = new google.visualization.ArrowFormat();
           formatter.format(data, 4);
           formatter.format(data, 6);
           table.draw(data, { showRowNumber: false, width: '100%', height: '100%' });
      }

   </script>

</asp:Content>


<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Content1">
      <div class="mainpanel">
    
    <div class="headerbar">
      
      <a class="menutoggle"><i class="fa fa-bars"></i></a>
      
      <div class="header-right">
      </div><!-- header-right -->     
    </div><!-- headerbar --> 
    <div class="pageheader">
      <h2><i class="fa fa-user"></i> Profiling <span>Subtitle goes here...</span></h2>
      <div class="breadcrumb-wrapper">
        <ol class="breadcrumb">
          <li class="active" runat="server" id="userName">Dashboard</li>
        </ol>
      </div>
    </div>


<%-- Modal Window --%>
<div class="container">

  <!-- Modal -->
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog modal-lg" style="width:auto; height:auto; margin:4px;">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>     
          <h4 class="modal-title" style="font-weight:bold;" id="idModal"></h4>
          <h4 id="idDates" runat="server"></h4>
          <h4 id="idUser" runat="server" style="display:none;"></h4>
          <h4 id="idFilters" runat="server" style="display:block;"></h4>
          <h4 id="idLeagueFilter" runat="server" style="display:block;"></h4>
        </div>
        
<div class="row">
    <div class="col-xs-12">
       <ul class="nav nav-tabs nav-justified">
        <li class="nav-item active"><a data-toggle="tab" id="m1" href="#home">Sport</a></li>
        <li><a data-toggle="tab" href="#menu1">League</a></li>
        <li><a data-toggle="tab" href="#menu2">Period</a></li>
        <li><a data-toggle="tab" href="#menu3">Fav/Dog</a></li>
        <li><a data-toggle="tab" href="#menu4">Wager Type</a></li>
        <li><a data-toggle="tab" href="#menu5">Side</a></li>
        <li><a data-toggle="tab" href="#menu6">Weekday</a></li>
        <li><a data-toggle="tab" href="#menu7">Moment Day</a></li>
        <li><a data-toggle="tab" href="#menu8">Team</a></li>
        <li><a data-toggle="tab" href="#menu9">Bets</a></li>
        <li><a data-toggle="tab" href="#menu10">Graphs</a></li>
      </ul>
    </div>
</div>

  <div class="tab-content">
    <div id="home" class="tab-pane fade in active">     
      <h3>By Sport</h3>
      <div id="MainSport">
        <div id="l1" style="text-align:center;"><div class="loader" style="text-align:center"></div></div>
        <div style="margin-top:50px; margin-bottom:50px;" id="table_div"></div>
        <div style="margin-top:50px; margin-bottom:50px; width:auto; height:20%; overflow-x:auto;" id="sportChart"></div>
      </div>
    </div>
    <div id="menu1" class="tab-pane fade">
      <h3>By League</h3>
         <div id="MainLeague" class="row">
           <div class="panel panel-default">
                <div class="panel-body">
                  <div class="row">                               
                    <div class="col-xs-12">	
                        <div class="form-group" id="secSportLeague" runat="server">
                            <label runat="server" id="lbSportLeague" class="col-sm-1 control-label">Sport:</label>
                            <div class="col-sm-2">
                              <select id="inBySport" runat="server" class="form-control chosen-select" >
                                  <option selected="selected" value="">ALL</option>
                              </select>
                            </div>
                         </div>

                          <div class="form-group">
                             <label class="col-sm-1 control-label">As:</label>
                             <div class="col-sm-2">
                               <select id="inFavDogLeague" class="form-control chosen-select">
                                   <option selected="selected" value="">ALL</option>
                                   <option value="FAV">FAV</option>
                                   <option value="DOG">DOG</option>
                                   <option value="OVER">OVER</option>
                                   <option value="UNDER">UNDER</option>
                                   <option value="DRAW">PUSH/DRAW</option>
                               </select>
                             </div>
                           

                            <div class="col-sm-2">
                              <button class="btn btn-success" onclick="ByLeagueAJX();">Filter</button>
                            </div> 
                         </div>
                  </div>
              </div>
          </div>
        </div>
      </div>
      <div id="l2" style="text-align:center;"><div class="loader" style="text-align:center"></div></div>
      <div style="margin-top:50px; margin-bottom:50px;" id="div_League"></div>
    </div>    
    <div id="menu2" class="tab-pane fade">
      <h3>By Period</h3>
         <div id="MainPeriod" class="row">
           <div class="panel panel-default">
                <div class="panel-body">
                  <div class="row">                     
                    <div class="col-xs-12">			                                          
                        <div class="form-group" id="secSportPeriod" runat="server">
                            <label id="lbSportPeriod" runat="server" class="col-sm-1 control-label">Sport:</label>
                            <div class="col-sm-2">
                              <select id="inBySportPeriod" runat="server" onchange="LeaguesPlayerAJX('slt','#MainContent_inBySportPeriod');" class="form-control chosen-select" >
                                  <option selected="selected" value="">ALL</option>
                              </select>
                            </div>
                        </div> 

                           <div class="form-group" id="secLeaguePeriod" runat="server">
                             <label id="lbLeaguePeriod" class="col-sm-1 control-label">League:</label>
                             <div class="col-sm-3">
                               <select id="slt" class="form-control">
                                   <option selected="selected" value="-1">ALL</option>
                               </select>
                             </div>
                           </div>

                          <div class="form-group">
                             <label class="col-sm-1 control-label">As:</label>
                             <div class="col-sm-2">
                               <select id="inFavDogPeriod" class="form-control chosen-select">
                                   <option selected="selected" value="">ALL</option>
                                   <option value="FAV">FAV</option>
                                   <option value="DOG">DOG</option>
                                   <option value="OVER">OVER</option>
                                   <option value="UNDER">UNDER</option>
                                   <option value="DRAW">PUSH/DRAW</option>
                               </select>
                             </div>
                          
                            <div class="col-sm-3">
                              <button class="btn btn-success" onclick="ByPeriodAJX();">Filter</button>
                            </div>
                        </div>
                  </div>
              </div>
          </div>
        </div>
      </div>
      <div id="l3" style="text-align:center;"><div class="loader" style="text-align:center"></div></div>
      <div style="margin-top:50px; margin-bottom:50px;" id="div_Period"></div>
    </div>
    <div id="menu3" class="tab-pane fade">
      <h3>By Fav/Dog</h3>
         <div id="MainFavDog" class="row">
           <div class="panel panel-default">
                <div class="panel-body">
                  <div class="row">
                    <div class="col-xs-12">			                                          
                        <div class="form-group" runat="server" id="secSportFAvDog">
                            <label class="col-sm-1 control-label">Sport:</label>
                            <div class="col-sm-2">
                              <select id="inBySportFavDog" runat="server" onchange="LeaguesPlayerAJX('inByLeagueFavDog','#MainContent_inBySportFavDog');" class="form-control chosen-select" >
                                  <option selected="selected" value="">ALL</option>
                              </select>
                            </div>
                         </div> 

                        <div class="form-group" runat="server" id="secLeagueFavDog">
                            <label class="col-sm-1 control-label">League:</label>
                             <div class="col-sm-3">
                               <select id="inByLeagueFavDog" class="form-control">
                                   <option selected="selected" value="-1">ALL</option>
                               </select>
                             </div>
                        
                            <div class="col-sm-3">
                              <button class="btn btn-success"  onclick="ByFavDogAJX();">Filter</button>
                            </div> 
                        </div>
                  </div>
              </div>
          </div>
        </div>
      </div>
      <div id="l4" style="text-align:center;"><div class="loader" style="text-align:center"></div></div>
      <div style="margin-top:50px; margin-bottom:50px;" id="div_FavDog"></div>
    </div>
    <div id="menu4" class="tab-pane fade">
      <h3>By Wager Type</h3>
          <div id="MainWagerType" class="row">
           <div class="panel panel-default">
                <div class="panel-body">
                  <div class="row">                    
                    <div class="col-xs-12">			                                          
                        <div class="form-group" id="secSportWagerType" runat="server">
                            <label id="lbSportWagerType" class="col-sm-1 control-label">Sport:</label>
                            <div class="col-sm-2">
                              <select id="inBySportWagerType" runat="server" onchange="LeaguesPlayerAJX('inByLeagueWagerType','#MainContent_inBySportWagerType');" class="form-control chosen-select" >
                                  <option selected="selected" value="">ALL</option>
                              </select>
                            </div>
                         </div>

                         <div class="form-group" id="secLeagueWagerType" runat="server">
                             <label id="lbLeagueWagerType" class="col-sm-1 control-label">League:</label>
                             <div class="col-sm-3">
                               <select id="inByLeagueWagerType" class="form-control">
                                   <option selected="selected" value="-1">ALL</option>
                               </select>
                             </div>
                          </div>

                            <div class="form-group">
                             <label class="col-sm-1 control-label">As:</label>
                             <div class="col-sm-2">
                               <select id="inFavDogWagerType" class="form-control chosen-select">
                                   <option selected="selected" value="">ALL</option>
                                   <option value="FAV">FAV</option>
                                   <option value="DOG">DOG</option>
                                   <option value="OVER">OVER</option>
                                   <option value="UNDER">UNDER</option>
                                   <option value="DRAW">PUSH/DRAW</option>
                               </select>
                             </div>
                           
                            <div class="col-sm-3">
                              <button class="btn btn-success" onclick="ByWagerTypeAJX()">Filter</button>
                            </div>
                        </div> 
                  </div>
              </div>
          </div>
        </div>
      </div>
      <div id="l5" style="text-align:center;"><div class="loader" style="text-align:center"></div></div>
      <div style="margin-top:50px; margin-bottom:50px;" id="div_WagerType"></div> 
    </div>
    <div id="menu5" class="tab-pane fade">
      <h3>By Side</h3>
         <div id="MainBySide" class="row">
           <div class="panel panel-default">
                <div class="panel-body">
                  <div class="row">                       
                    <div class="col-xs-12">			                                          
                        <div class="form-group"  runat="server" id="secSportSide">
                            <label id="lbSportSide" runat="server" class="col-sm-1 control-label">Sport:</label>
                            <div class="col-sm-2">
                              <select id="inBySportSide" runat="server" onchange="LeaguesPlayerAJX('inByLeagueSide','#MainContent_inBySportSide');" class="form-control chosen-select" >
                                  <option selected="selected" value="">ALL</option>
                              </select>
                            </div>
                          </div>

                        <div class="form-group" id="secLeagueSide" runat="server">
                             <label id="idLeagueSide" runat="server" class="col-sm-1 control-label">League:</label>
                             <div class="col-sm-3">
                               <select id="inByLeagueSide" class="form-control">
                                   <option selected="selected" value="-1">ALL</option>
                               </select>
                             </div>
                          </div>

                           <div class="form-group">
                             <label class="col-sm-1 control-label">As:</label>
                             <div class="col-sm-2">
                               <select id="inFavDogSide" class="form-control chosen-select">
                                   <option selected="selected" value="">ALL</option>
                                   <option  value="FAV">FAV</option>
                                   <option  value="DOG">DOG</option>
                                   <option  value="OVER">OVER</option>
                                   <option  value="UNDER">UNDER</option>
                               </select>
                             </div>
                          
                            <div class="col-sm-3">
                              <button class="btn btn-success" onclick="BySideAJX();">Filter</button>
                            </div>
                          </div>
                         
                  </div>
              </div>
          </div>
        </div>
      </div>
      <div id="l6" style="text-align:center;"><div class="loader" style="text-align:center"></div></div>
      <div style="margin-top:50px; margin-bottom:50px;" id="div_Side"></div>
    </div>
    <div id="menu6" class="tab-pane fade">
      <h3>By Weekday</h3>
      <div id="MainWeekDay" class="row">
        <div id="l7" style="text-align:center;"><div class="loader" style="text-align:center"></div></div>
        <div style="margin-top:50px; margin-bottom:50px;" id="div_Week"></div>
        <div class="col-md">
          <div style="margin-top:50px; overflow-x:auto; margin-bottom:50px; width:auto; height:20%;" id="weekChart"></div>
        </div>
        </div>
    </div>
    <div id="menu7" class="tab-pane fade">
      <h3>By Moment Day</h3>
      <div id="MainMomentDay" class="row">
       <div id="l8" style="text-align:center;"><div class="loader" style="text-align:center"></div></div>
       <div style="margin-top:50px; margin-bottom:50px;" id="div_MomentDay"></div>
         <div class="col-md" style="position: relative;">
           <div style="overflow-y:auto; margin-top:50px; margin-bottom:50px; width:auto;" id="momentDayChart"></div>
         </div>
       </div>
    </div>
    <div id="menu8" class="tab-pane fade">
      <h3>By Team</h3>
      <div id="MainTeam" class="row">
       <div id="l9" style="text-align:center;"><div class="loader" style="text-align:center"></div></div>
       <div style="margin-top:50px; margin-bottom:50px; width:100%; height:20%; overflow-x:auto;" id="div_Team"></div>
      </div>
    </div>
    <div id="menu9" class="tab-pane fade">
      <h3>Bets</h3>
      <div id="MainBets" class="row">
           <div class="panel panel-default">
                <div class="panel-body">
                  <div class="row" style="margin:0 !important;">                     
                    <div class="col-xs-12">			                                          
                        <div class="form-group" id="secSportBets" runat="server">
                            <label id="Label1" runat="server" class="col-sm-1 control-label">Sport:</label>
                            <div class="col-sm-2">
                              <select id="inBySportBets" runat="server" onchange="LeaguesPlayerAJX('inByLeagueBets','#MainContent_inBySportBets');" class="form-control chosen-select" >
                                  <option selected="selected" value="">ALL</option>
                              </select>
                            </div>
                        </div> 

                           <div class="form-group" id="secLeagueBets" runat="server">
                             <label id="lbLeagueBets" class="col-sm-1 control-label">League:</label>
                             <div class="col-sm-3">
                               <select id="inByLeagueBets" class="form-control">
                                   <option selected="selected" value="-1">ALL</option>
                               </select>
                             </div>
                           </div>

                         <div class="form-group">
                            <label class="col-sm-1 control-label">Wager Play:</label>
                             <div class="col-sm-2">
                               <select id="inFavDogWagerPlay" class="form-control chosen-select">
                                   <option selected="selected" value="">ALL</option>
                                   <option value="ML">Money Line</option>
                                   <option value="SP">Spread</option>
                                   <option value="TOT">Total</option>
                                   <option value="DR">Draw</option>
                               </select>
                             </div>
                         </div>

                          <div class="form-group">
                             <label class="col-sm-1 control-label">As:</label>
                             <div class="col-sm-2">
                               <select id="inFavDogBets" class="form-control chosen-select">
                                   <option selected="selected" value="">ALL</option>
                                   <option value="FAV">FAV</option>
                                   <option value="DOG">DOG</option>
                                   <option value="OVER">OVER</option>
                                   <option value="UNDER">UNDER</option>
                                   <option value="DRAW">PUSH/DRAW</option>
                               </select>
                             </div>
                          
                            <div class="col-sm-3">
                              <button class="btn btn-success" onclick="ByBetsAJX();">Filter</button>
                            </div>
                        </div>
                  </div>
              </div>
          </div>
        </div>
      </div>

      <div id="20" style="text-align:center;"><div class="loader" style="text-align:center"></div></div>
      <div class="col-md-12 text-center" style="margin-top:50px; margin-bottom:50px; width:100%; height:20%;" id="div_Bets"></div>
    </div>
    <div id="menu10" class="tab-pane fade">
      <h3>Graphs</h3>
         <p>on construction...</p>
      <%--<div id="l8" style="text-align:center;"><div class="loader" style="text-align:center"></div></div>--%>
      <div style="margin-top:50px; margin-bottom:50px;" id="div_Graphs"></div>
    </div>

  </div>

            <%-- Content --%>            
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
</div>
<%-- End Modal Window --%>  
         
    <div class="contentpanel">  
      <div class="row">
           <div class="panel panel-default">
                <div class="panel-heading">
                  <div class="panel-btns">
                    <a href="#" class="panel-close">×</a>
                    <a href="#" class="minimize">−</a>
                  </div>
                  <h3 class="panel-title">Filters</h3>
                </div>

                <div class="panel-body">
                  <div class="row">
                    <div class="col-xs-12">			  

                  <form class="form-horizontal form-bordered" method="post" runat="server" id="frm">                      
                        <div class="form-group">
                          <label class="col-sm-1 control-label">Start Date:</label>
                            <div class="col-sm-3">

                              <div class="input-group date">
                                <input type="text" class="form-control" placeholder="mm/dd/yyyy" id="startDate" runat="server" name="startDate" required="required" autocomplete="off" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                              </div>
                            </div>

                            <label class="col-sm-2 control-label">End Date:</label>
                            <div class="col-sm-3">
                              <div class="input-group">
                                  <input type="text" class="form-control" placeholder="mm/dd/yyyy" id="endDate" runat="server" name="endDate" required="required" autocomplete="off" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                              </div>
                            </div>
                        </div>
                        

                        <div class="form-group">
                          <label class="col-sm-1 control-label">Sport:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inSport" runat="server"  AutoPostBack="true"  CssClass="form-control chosen-select" OnSelectedIndexChanged="FillLeague">
                                </asp:DropDownList>
                            </div>

                            <label class="col-sm-2 control-label">League:</label>
                            <div class="col-sm-3">
                              <asp:DropDownList ID="inLeague" CssClass="form-control chosen-select" AutoPostBack="false" runat="server">
                              <asp:ListItem Selected="True" Value="-1" >ALL</asp:ListItem>
                              </asp:DropDownList>
                            </div>
                        </div>


                         <div class="form-group">
                          <label class="col-sm-1 control-label">Agent:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inAgent" runat="server"  AutoPostBack="true"  CssClass="form-control chosen-select" OnSelectedIndexChanged="LoadPlayers">
                                </asp:DropDownList>
                            </div>

                            <label class="col-sm-2 control-label">Player:</label>
                            <div class="col-sm-3">
                              <asp:DropDownList ID="inPlayer" CssClass="form-control chosen-select" runat="server">
                              <asp:ListItem Value="-1" >ALL</asp:ListItem>
                              </asp:DropDownList>
                            </div>

                           <label class="col-sm-1 control-label">Filters:</label>
                            <div class="col-sm-1">
                                <input type="checkbox" class="form-control" id="inExtraFilters" runat="server" name="extraFilters" onchange="show('extra')" />
                            </div>
                        </div>




                      <div id="extra" style="display:none;">
                      <div class="form-group">
                          <label class="col-sm-1 control-label">Hold%  ></label>
                            <div class="col-sm-1">
                              <div class="input-group">
                                <input type="text" onkeypress="return isNumberKey(event)" class="form-control" placeholder="" id="inHold" runat="server" name="hold"  autocomplete="off" />
                              </div>
                            </div>

                            <label class="col-sm-1 control-label">Min Net</label>
                            <div class="col-sm-1">
                              <div class="input-group">
                                  <input type="text" onkeypress="return isNumberKey(event)" class="form-control" id="inNet" runat="server" name="net" autocomplete="off" />
                              </div>
                            </div>

                            <label class="col-sm-1 control-label">Max Net</label>
                            <div class="col-sm-1">
                              <div class="input-group">
                                  <input type="text" onkeypress="return isNumberKey(event)" class="form-control" id="inMaxNet" runat="server" name="inMaxNet" autocomplete="off" />
                              </div>
                            </div>


                           <label class="col-sm-1 control-label">Bets  ></label>
                            <div class="col-sm-1">
                              <div class="input-group">
                                  <input type="text" onkeypress="return isNumberKey(event)" class="form-control" id="inBet" runat="server" name="bets" autocomplete="off" />
                              </div>
                            </div>

                          <label class="col-sm-1 control-label">Win%  ></label>
                            <div class="col-sm-1">
                              <div class="input-group">
                                  <input type="text" onkeypress="return isNumberKey(event)" class="form-control"  id="inWins" runat="server" name="wins" autocomplete="off" />
                              </div>
                            </div>



                          <label class="col-sm-1 control-label">Line Mover%  ></label>
                            <div class="col-sm-1">
                              <div class="input-group">
                                  <input type="text" onkeypress="return isNumberKey(event)" class="form-control" id="inLineMover" runat="server" name="lineMover" autocomplete="off" />
                              </div>
                            </div>

                        </div>
                        

                        <div class="form-group">
                           <label class="col-sm-1 control-label">Scalp Cris%  ></label>
                            <div class="col-sm-1">
                              <div class="input-group">
                                  <input type="text" onkeypress="return isNumberKey(event)" class="form-control" id="inCris" runat="server" name="cris" autocomplete="off" />
                              </div>
                            </div>


                           <label class="col-sm-1 control-label">Scalp PPH%  ></label>
                            <div class="col-sm-1">
                              <div class="input-group">
                                  <input type="text" onkeypress="return isNumberKey(event)" class="form-control" id="inPPH" runat="server" name="pph" autocomplete="off" />
                              </div>
                            </div>


                           <label class="col-sm-1 control-label">Scalp Pinni%  ></label>
                            <div class="col-sm-1">
                              <div class="input-group">
                                  <input type="text" onkeypress="return isNumberKey(event)" class="form-control" id="inPinni" runat="server" name="pinni" autocomplete="off" />
                              </div>
                            </div>


                           <label class="col-sm-1 control-label">Scalp Jaz%  ></label>
                            <div class="col-sm-1">
                              <div class="input-group">
                                  <input type="text" onkeypress="return isNumberKey(event)" class="form-control" id="inJaz" runat="server" name="inJaz" autocomplete="off" />
                              </div>
                            </div>


                          <label class="col-sm-1 control-label">Scalp 5Dimes%  ></label>
                            <div class="col-sm-1">
                              <div class="input-group">
                                  <input type="text" onkeypress="return isNumberKey(event)" class="form-control" id="in5Dimes" runat="server" name="dimes5" autocomplete="off" />
                              </div>
                            </div>


                            <label class="col-sm-1 control-label">Beat Line%  ></label>
                            <div class="col-sm-1">
                              <div class="input-group">
                                  <input type="text" onkeypress="return isNumberKey(event)" class="form-control" id="inBeatLine" runat="server" name="dimes5" autocomplete="off" />
                              </div>
                            </div>

                        </div>

                      </div>


                      <div id="btnShow">
                           <label class="col-sm-9 control-label"></label>
                           <div class="col-sm-2">
                            <asp:Button class="btn btn-block btn-success" runat="server" type="submit" Text="Run" OnClick="LoadSummary"/>
                          </div>  
                      </div>
                  </form>    
                </div>
              </div>
          </div>
        </div>
      </div>
      <div class="row">
               <div class="panel panel-default">
                <div class="panel-heading">
                  <div class="panel-btns">
                    <a href="#" class="panel-close">×</a>
                    <a href="#" class="minimize">−</a>
                  </div>
                  <h3 class="panel-title">Records:</h3>
                </div>

            <div class="panel-body">               
                <div class="table-responsive" style="overflow-x:auto;">
                           <table class="table tables"  style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th style="cursor:pointer;">Player <a class="fa fa-angle-down"></a></th>
                                             <th style="cursor:pointer;">Net <a class="fa fa-angle-down"></a></th>
                                             <th style="cursor:pointer;">Hold% <a class="fa fa-angle-down"></a></th>
                                             <th style="cursor:pointer;">Win% <a class="fa fa-angle-down"></a></th>
                                             <th style="cursor:pointer;">Bets <a class="fa fa-angle-down"></a></th>
                                             <th style="cursor:pointer;">Scalping PPH% <a class="fa fa-angle-down"></a></th>
                                             <th style="cursor:pointer;">Scalping Jazz% <a class="fa fa-angle-down"></a></th>
                                             <th style="cursor:pointer;">Scalping Pinni% <a class="fa fa-angle-down"></a></th>
                                             <th style="cursor:pointer;">Scalping 5Dimes% <a class="fa fa-angle-down"></a></th>
                                             <th style="cursor:pointer;">Scalping Cris% <a class="fa fa-angle-down"></a></th>
                                             <th style="cursor:pointer;">Line Moved% <a class="fa fa-angle-down"></a></th>
                                             <th style="cursor:pointer;">Beat Line% <a class="fa fa-angle-down"></a></th>                                      
                                             <th style="cursor:pointer;">Lost<a class="fa fa-angle-down"></a></th>
                                             <th style="cursor:pointer;">Win<a class="fa fa-angle-down"></a></th>
                                             <th style="cursor:pointer;">Push<a class="fa fa-angle-down"></a></th>
                                             <th style="cursor:pointer;">Stats<a class="fa fa-angle-down"></a></th>
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptTable">
                                      <ItemTemplate>
                                            <tr>
                                            <td ><%# Eval("Player")%></td>
                                            <td ><%# Eval("Net") %></td>
                                            <td ><%# Eval("HoldPercentaje") %></td>
                                            <td ><%# Eval("WinPercentaje") %></td>
                                            <td ><%# Eval("Bets") %></td>                                         
                                            <td ><%# Eval("ScalpingPPH") %></td>
                                            <td ><%# Eval("ScalpingJazz") %></td>
                                            <td ><%# Eval("ScalpingPinni") %></td>
                                            <td ><%# Eval("Scalping5Dimes") %></td>
                                            <td ><%# Eval("ScalpingCris") %></td>
                                            <td ><%# Eval("MoveLine") %></td>
                                            <td ><%# Eval("BeatLine") %></td>
                                            <td ><%# Eval("Lost") %></td>
                                            <td ><%# Eval("Wins") %></td>
                                            <td ><%# Eval("Draw") %></td>
                                            <td ><button onclick="Show('<%# Eval("Player")%>')" data-toggle="modal" data-target="#myModal" class="btn btn-block btn-success">Stats</button></td>
                                          </tr>
                                    </ItemTemplate>
                              </asp:Repeater>                            
                       </table>
                 </div>  
          </div>
        </div>
     </div>
    </div><!-- contentpanel -->
  </div><!-- mainpanel -->


</asp:Content>



