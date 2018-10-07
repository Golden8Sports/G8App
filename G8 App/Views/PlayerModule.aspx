<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/menu.Master" CodeBehind="PlayerModule.aspx.cs" Inherits="G8_App.Views.PlayerModule" %>


<asp:Content runat="server" ID="head" ContentPlaceHolderID="head1">
  

<%--<script src="http://code.jquery.com/jquery-1.11.1.min.js"></script>
<script src="../js/jquery-loader.js" type="text/javascript"></script>
<script type="text/javascript">
    

         	$data = {
                autoCheck: 32,
                size: 32, 
                bgColor: "#FFF",
                bgOpacity: "0.7",   
                fontColor: "#000",
                title: "", 
                isOnly: false
            };

     function Spinner(caso)
     {      
		    //if(caso == 1) $.loader.open($data);
      //      else $.loader.close(true);
     }

</script>--%>

    <style>
        table.dataTable thead th {border-bottom: 0; border-bottom:2px groove; background-color:white;}
        table.dataTable tbody td{border-top: 0;}
        .table{border:none;}
    </style>
    
  <script type="text/javascript" src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>
  <script>
    var obj;
    var obj2;
    var typeFinancial;
    var page = 1;

    //Bets
    var objB;
    var objPT;
    var objBetStats;

   //Today
     var objT;
     var objTP;
    
    function NavigationMain()
      {
          if (page == 1) {
              FinancialAJX();
              BetsAJX();
              TodayAJX();
          } else if (page == 2) {
              BetsAJX();
              FinancialAJX();
              TodayAJX();
          } else if (page == 4)
          {
              TodayAJX();
              BetsAJX();
              FinancialAJX();
          }
      }

    function FinancialAJX()
      {
          LastThisWeekAJX();
          RangeSummaryAJX();
      }

    function BetsAJX()
    {
       ProPlayAJX();
       ChartBetsAJX();
       ParlayTeaserAJX();
       BetStatsAJX();        
    }

    function TodayAJX()
    {
        TodayChartPlayAJX();
        TodayChartAJX();      
        TableTodayAJX();      
    }
    function ProPlayAJX()
    {       
        var sport = $("#MainContent_inSportBets").val();
        var range = $("#inProPlay").val();
        var player = $("#MainContent_inPlayer").val();

        var parameter = { 'range': range,'player': player, 'sport': sport }
        $.ajax({                    
           type: 'POST',
           url: 'PlayerModule.aspx/ProPlay',
           data: JSON.stringify(parameter),
           contentType: 'application/json; charset=utf-8',
           dataType: 'json',
           success: function (data)
           {
               $("#tableProPlay").DataTable().clear();
               var ob = JSON.parse(data.d);
               for (var i = 0; i < ob.length; i++)
               {
                   $('#tableProPlay').dataTable().fnAddData([ // moveline
                       "Move Line",
                       ob[i]["MoveLine"] + "% " + ob[i]["ContMoveLine"] + "/" + ob[i]["Bets"]                     
                    ]);

                    $('#tableProPlay').dataTable().fnAddData( [ //time pattern
                        "Time Pattern",
                        "Curve"                     
                    ]);

                   $('#tableProPlay').dataTable().fnAddData( [  //beat line
                        "Beat Line",
                        ob[i]["BeatLine"] + "% " + ob[i]["ContBeatLine"] + "/" + ob[i]["Bets"]                     
                   ]);

                   $('#tableProPlay').dataTable().fnAddData( [
                        "Scalp Cris",
                        ob[i]["ScalpingCris"]  + "% " + ob[i]["ContCris"] + "/" + ob[i]["Bets"]                    
                   ]);

                   $('#tableProPlay').dataTable().fnAddData( [
                        "Scalp 5Dimes",
                        ob[i]["Scalping5Dimes"] + "% " + ob[i]["Cont5Dimes"] + "/" + ob[i]["Bets"]                     
                   ]);

                   $('#tableProPlay').dataTable().fnAddData( [
                        "Scalp Pinni",
                        ob[i]["ScalpingPinni"] + "% " + ob[i]["ContPinni"] + "/" + ob[i]["Bets"]                    
                   ]);

                   $('#tableProPlay').dataTable().fnAddData( [
                        "Scalp Jazz",
                         ob[i]["ScalpingJazz"] + "% " + ob[i]["ContJazz"] + "/" + ob[i]["Bets"]                     
                   ]);

                   $('#tableProPlay').dataTable().fnAddData([
                       "Scalp PPH",
                        ob[i]["ScalpingPPH"] + "% " + ob[i]["ContPPH"] + "/" + ob[i]["Bets"]                   
                   ]);

                   $('#tableProPlay').dataTable().fnAddData( [
                        "Overall Scalp",
                        ob[i]["OverallScalp"] + "%"                     
                   ]);


                   $('#tableProPlay').dataTable().fnAddData( [
                        "Syndicate",
                        "No"                     
                   ]);


                   $('#tableProPlay').dataTable().fnAddData( [
                        "Adjusted",
                        "0%"                     
                   ]);

               }

               document.getElementById("tableProPlay_length").style.display = "none";
               document.getElementById("tableProPlay_filter").style.display = "none";
               document.getElementById("tableProPlay_info").style.display = "none";
               document.getElementById("tableProPlay_paginate").style.display = "none";
           },
           error: function (data)
           {
              alert('No Data' + data.statusText);
           }
        });
    }
    function TableTodayAJX()
    {       
        var sport = $("#MainContent_inSportBets").val();
        var player = $("#MainContent_inPlayer").val();

        var parameter = { 'player': player, 'sport': sport }
        $.ajax({                    
           type: 'POST',
           url: 'PlayerModule.aspx/TodayTable',
           data: JSON.stringify(parameter),
           contentType: 'application/json; charset=utf-8',
           dataType: 'json',
           success: function (data)
           {
               $("#tableToday").DataTable().clear();
               var ob = JSON.parse(data.d);
               for (var i = 0; i < ob.length; i++)
               {
                   $('#tableToday').dataTable().fnAddData([ // moveline
                       ob[0]["Player"],
                       ob[0]["RiskAmount"],
                       ob[0]["Bets"],
                       ob[0]["MoveLine"],
                       ob[0]["BeatLine"],
                       ob[0]["OverallScalp"],
                       "0",
                   ]);
               }
               document.getElementById("tableToday_length").style.display = "none";
               document.getElementById("tableToday_filter").style.display = "none";
               document.getElementById("tableToday_info").style.display = "none";
               document.getElementById("tableToday_paginate").style.display = "none";
           },
           error: function (data)
           {
              alert('No Data' + data.statusText);
           }
        });
    }
    function TodayChartAJX()
    {
        var sport = $("#MainContent_inSPortToday").val();
        var player = $("#MainContent_inPlayer").val();
        var parameter = { 'player': player, 'sport': sport }
        $.ajax({                    
           type: 'POST',
           url: 'PlayerModule.aspx/TodayChart',
           data: JSON.stringify(parameter),
           contentType: 'application/json; charset=utf-8',
           dataType: 'json',
           success: function (data)
           {
               objT = JSON.parse(data.d);
               DrawChart6();
           },
           error: function (data)
           {
              alert('No Data: ' + data.statusText);
           }
        });
    }
    function BetStatsAJX()
    { 
        var sport = $("#MainContent_inSportBets").val();
        var wagerType = $("#MainContent_inWagerTypeBets").val();
        var player = $("#MainContent_inPlayer").val();

        var parameter = { 'player': player, 'wagerType': wagerType, 'sport': sport }
        $.ajax({                    
           type: 'POST',
           url: 'PlayerModule.aspx/BetStats',
           data: JSON.stringify(parameter),
           contentType: 'application/json; charset=utf-8',
           dataType: 'json',
           success: function (data)
           {     
               $("#talbeBetStats").DataTable().clear();
               objBetStats = JSON.parse(data.d);
               for (var i = 0; i < objBetStats.length; i++)
               {
                    $('#talbeBetStats').dataTable().fnAddData( [
                        objBetStats[i]["DateRange"],
                        objBetStats[i]["WagerPlay"],
                        objBetStats[i]["Bets"],
                        "$" + objBetStats[i]["RiskAmount"],
                        "$" + objBetStats[i]["Net"],
                        objBetStats[i]["HoldPercentaje"] + "%",
                        objBetStats[i]["WinPercentaje"] + "%"                       
                    ]);
               }


              $("#talbeBetStats td").each(function () {

                 var n = $(this).text().includes("%");
                 if (n == "true" || n == true)
                 {
                     var d = $(this).text().replace("%", "");

                     if (d >= 80) $(this).css('color', 'yellow');
                     else if (d < 80 && d >= 0) $(this).css('color', 'green');
                     else $(this).css('color', 'red');
                 }
                
             });

               document.getElementById("talbeBetStats_length").style.display = "none";
               document.getElementById("talbeBetStats_filter").style.display = "none";
               document.getElementById("talbeBetStats_paginate").style.display = "none";
               document.getElementById("talbeBetStats_info").style.display = "none";
           },
           error: function (data)
           {
              alert('No Data' + data.statusText);
           }
        });
      }
    function ParlayTeaserAJX()
    {       
        var sport = $("#MainContent_inSportBets").val();
        var range = $("#inParlayTeaser").val();
        var player = $("#MainContent_inPlayer").val();

        var parameter = { 'player': player, 'range': range, 'sport': sport }
        $.ajax({                    
           type: 'POST',
           url: 'PlayerModule.aspx/ParlayTeaser',
           data: JSON.stringify(parameter),
           contentType: 'application/json; charset=utf-8',
           dataType: 'json',
           success: function (data)
           {
               $("#ParTeaTable").DataTable().clear();
               objPT = JSON.parse(data.d);
               for (var i = 0; i < objPT.length; i++)
               {
                    $('#ParTeaTable').dataTable().fnAddData( [
                        objPT[i]["WagerType"],
                        objPT[i]["Bets"],
                        "$" + objPT[i]["RiskAmount"],
                        "$" + objPT[i]["Net"],
                        objPT[i]["HoldPercentaje"] + "%",
                        objPT[i]["WinPercentaje"] + "%"                       
                    ]);
               }


             $("#ParTeaTable td").each(function () {

                 var n = $(this).text().includes("%");
                 if (n == "true" || n == true)
                 {
                     var d = $(this).text().replace("%", "");

                     if (d >= 80) $(this).css('color', 'yellow');
                     else if (d < 80 && d >= 0) $(this).css('color', 'green');
                     else $(this).css('color', 'red');
                 }
                
             });

               document.getElementById("ParTeaTable_length").style.display = "none";
               document.getElementById("ParTeaTable_filter").style.display = "none";
               document.getElementById("ParTeaTable_info").style.display = "none";
               document.getElementById("ParTeaTable_paginate").style.display = "none";

               DrawChart9();
           },
           error: function (data)
           {
              alert('No Data' + data.statusText);
           }
        });
    }
    function ChartBetsAJX()
    {       
        var sport = $("#MainContent_inSportBets").val();
        var wagerType = $("#MainContent_inWagerTypeBets").val();
        var player = $("#MainContent_inPlayer").val();

        var parameter = { 'player': player, 'wagertype': wagerType, 'sport': sport }
        $.ajax({                    
           type: 'POST',
           url: 'PlayerModule.aspx/BetInfo',
           data: JSON.stringify(parameter),
           contentType: 'application/json; charset=utf-8',
           dataType: 'json',
           success: function (data)
           {
               objB = JSON.parse(data.d);
               DrawChart3();
           },
           error: function (data)
           {
              alert('No Data' + data.statusText);
           }
        });
      }
    function LastThisWeekAJX()
    {
        
        //Spinner(1);
        var sport = $("#MainContent_inSportFinancial").val();
        var wagerType = $("#MainContent_inWagerTypeFinancial").val();
        var player = $("#MainContent_inPlayer").val();        

        var parameter = { 'player': player, 'wagertype': wagerType, 'sport' : sport }
        $.ajax({                    
           type: 'POST',
           url: 'PlayerModule.aspx/LastWeeks',
           data: JSON.stringify(parameter),
           contentType: 'application/json; charset=utf-8',
           dataType: 'json',
           success: function (data)
           {
              ThisLastWeekJava(data.d);
               DrawChart2();
               DrawChart1();
               //Spinner(2);
           },
           error: function (data)
           {
              $("#MainContent_inThisWeek").val('$ 0');
              $("#MainContent_inLastWeek").val('$ 0');
              alert('No Data' + data.statusText);
           }
        });
    }
    function RangeSummaryAJX()
    {       
        //Spinner(1);
        var sport = $("#MainContent_inSportFinancial").val();
        var wagerType = $("#MainContent_inWagerTypeFinancial").val();
        var player = $("#MainContent_inPlayer").val();
        var range = $("#inDateRangeFinancial").val();

        var parameter = { 'range': range,  'player': player, 'wagertype': wagerType, 'sport': sport }
        $.ajax({                    
           type: 'POST',
           url: 'PlayerModule.aspx/SumDateRange',
           data: JSON.stringify(parameter),
           contentType: 'application/json; charset=utf-8',
           dataType: 'json',
           success: function (data)
           {
               obj2 = JSON.parse(data.d);
               DrawChart4();
           },
           error: function (data)
           {
              alert('No Data' + data.statusText);
           }
        });
      }
    function TodayChartPlayAJX()
    {
        var sport = $("#MainContent_inSPortToday").val();
        var player = $("#MainContent_inPlayer").val();
        var type = $("#MainContent_inTypeToday").val();

        var parameter = { 'player': player, 'sport': sport, 'wagerType': type }
        $.ajax({                    
           type: 'POST',
           url: 'PlayerModule.aspx/TodayPlay',
           data: JSON.stringify(parameter),
           contentType: 'application/json; charset=utf-8',
           dataType: 'json',
           success: function (data)
           {
               objTP = JSON.parse(data.d);
               DrawChart5();
           },
           error: function (data)
           {
              alert('No Data: ' + data.statusText);
           }
        });
      }

    function DrawChart1()
    {
         var chart = new CanvasJS.Chart("chart1", {
	        theme: "light2", // "light1", "light2", "dark1", "dark2"
	        animationEnabled: true,
	        title: {
                text: "Bet Type",
                fontSize: 20,
	        },
	        data: [{
		        type: "pie",
		        startAngle: 25,
		        toolTipContent: "<b>{label}</b>: ${y}",
		        showInLegend: "true",
		        legendText: "{label}",
		        indexLabelFontSize: 16,
		        indexLabel: "{label} - ${y}",
		        dataPoints: []
	        }]
        });

        var betTypeFinancial = $("#inBetTypeFinancial").val();

        var dataPoints = [];

        if (obj.length > 1)
        {
            if (betTypeFinancial == "TW") {
                dataPoints.push({ y: obj[0]["StraightNet"], label: "Straight Bet" },
                                { y: obj[0]["ParlayNet"], label: "Parlay" },
                                { y: obj[0]["TeaserNet"], label: "Teaser" });
            } else if (betTypeFinancial == "LW")
            {
                dataPoints.push({ y: obj[1]["StraightNet"], label: "Straight Bet" },
                                { y: obj[1]["ParlayNet"], label: "Parlay" },
                                { y: obj[1]["TeaserNet"], label: "Teaser" });

            }else if (betTypeFinancial == "TS")
            {
                dataPoints.push({ y: obj[3]["StraightNet"], label: "Straight Bet" },
                                { y: obj[3]["ParlayNet"], label: "Parlay" },
                                { y: obj[3]["TeaserNet"], label: "Teaser" });

            }else if (betTypeFinancial == "LS")
            {
                dataPoints.push({ y: obj[4]["StraightNet"], label: "Straight Bet" },
                                { y: obj[4]["ParlayNet"], label: "Parlay" },
                                { y: obj[4]["TeaserNet"], label: "Teaser" });
            }
        }

        chart.options.data[0].dataPoints = dataPoints;
        chart.render();
    }   
    function DrawChart2()
    {
        typeFinancial = $("#inFinancialStats").val();
         var chart = new CanvasJS.Chart("chart2", {
	        animationEnabled: true,
	        theme: "light2", // "light1", "light2", "dark1", "dark2"
	        title: {
                text: "General Summary",
                fontSize: 20,
	        },
	        axisY: {
		        title:typeFinancial,
		        //prefix: "$",
                includeZero: false,
                fontSize: 5,
	        },
	        axisX: {
	        },
	        data: [{
		        type: "column",
                yValueFormatString: "#,##0.0#",
                dataPoints: []
	        }]
        });
      
        var dataPoints = [];
        for (var i = 0; i < obj.length; i++)
        {
           if (i != 1)
           {
               if (typeFinancial == "BETS")
               {
                  dataPoints.push({ label: obj[i]["DateRange"], y: obj[i]["Bets"] })

               } else if (typeFinancial == "NET")
               {
                  dataPoints.push({ label: obj[i]["DateRange"], y: obj[i]["Net"] })

               }else if (typeFinancial == "HOLD")
               {
                  dataPoints.push({ label: obj[i]["DateRange"], y: obj[i]["HoldPercentaje"] })

               }else if (typeFinancial == "RISK")
               {
                  dataPoints.push({ label: obj[i]["DateRange"], y: obj[i]["RiskAmount"] })
               }
               else if (typeFinancial == "WIN")
               {
                  dataPoints.push({ label: obj[i]["DateRange"], y: obj[i]["WinPercentaje"] });
               }
           }
        }

        chart.options.data[0].dataPoints = dataPoints;
        chart.render();
    }
    function DrawChart3()
    {
        var typeBet = $("#MainContent_inBetsChartFilter").val();

         var chart = new CanvasJS.Chart("betChart", {
	        animationEnabled: true,
	        theme: "light2", // "light1", "light2", "dark1", "dark2"
	        title: {
                text: "Summary",
                fontSize: 20,
	        },
	        axisY: {
		        title: typeBet,
		        preffix: "$",
                includeZero: false,
                fontSize: 14,
	        },
	        axisX: {
                title: "Ranges",
                fontSize: 14
	        },
	        data: [{
		        type: "column",
                yValueFormatString: "#,##0.0#",
                dataPoints: []
	        }]
        });


        var dataPoints = [];
        for (var i = 0; i < objB.length; i++)
        {
            if (typeBet == "BETS")
            {
               dataPoints.push({ label: objB[i]["DateRange"], y: objB[i]["Bets"] })

            } else if (typeBet == "NET")
            {
                dataPoints.push({ label: objB[i]["DateRange"], y: objB[i]["Net"] })

             }else if (typeBet == "HOLD")
             {
                dataPoints.push({ label: objB[i]["DateRange"], y: objB[i]["HoldPercentaje"] })

             }else if (typeBet == "RISK")
             {
                dataPoints.push({ label: objB[i]["DateRange"], y: objB[i]["RiskAmount"] })

             }else if (typeBet == "WIN")
             {
                dataPoints.push({ label: objB[i]["DateRange"], y: objB[i]["WinPercentaje"] })
             }
        }

        chart.options.data[0].dataPoints = dataPoints;
        chart.render();
      }
    function DrawChart4() 
    {
        var chart = new CanvasJS.Chart("finHistory", {
	        animationEnabled: true,
            theme: "light2",
            zoomEnabled:true,
	        title:{
                text: "Player's History",
                fontSize: 18,
	        },
	        axisX:{
		        valueFormatString: $("#inDateRangeFinancial").val(),
		        crosshair: {
			        enabled: true,
                },
                fontSize: 8,
	        },
	        axisY: {
                title: "Values",
                fontSize: 16,
		        crosshair: {
			        enabled: true
                },
                includeZero: false
	        },  
	        legend:{
		        cursor:"pointer",
		        verticalAlign: "bottom",
		        horizontalAlign: "center",
		        dockInsidePlotArea: true,
                itemclick: toogleDataSeries,
                fontSize: 20,
	        },
	        data: [{
		        type: "line",
		        showInLegend: true,
                name: "Net",
                toolTipContent: "<strong>{x}</strong> </br> {name}:  ${y}</br> Hold:  {z}%",
		        markerType: "square",
		        xValueFormatString: $("#inDateRangeFinancial").val(),
                color: "#F08080",
                lineDashType: "dash",
		        dataPoints: []
	        },
	        {
		        type: "line",
		        showInLegend: true,
                name: "Risk",
                toolTipContent: "<strong>{x}</strong> </br> {name}:  ${y}",
                lineDashType: "dash",
                xValueFormatString: $("#inDateRangeFinancial").val(),
		        dataPoints: []
	        }]
        });


        var dataPoints1 = [];
        var dataPoints2 = [];


        for (var i = 0; i < obj2.length; i++)
        {
            dataPoints1.push({ x: new Date(obj2[i]["Y"], obj2[i]["M"], obj2[i]["D"]), y: obj2[i]["Net"], z: obj2[i]["HoldPercentaje"] });
            dataPoints2.push({ x: new Date(obj2[i]["Y"], obj2[i]["M"], obj2[i]["D"]), y: obj2[i]["RiskAmount"] });
        }

        chart.options.data[0].dataPoints = dataPoints1;
        chart.options.data[1].dataPoints = dataPoints2;

        chart.render();

        function toogleDataSeries(e){
	        if (typeof(e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
		        e.dataSeries.visible = false;
	        } else{
		        e.dataSeries.visible = true;
	        }
	        chart.render();
        }
    }
    function DrawChart5()
    {
        var type = $("#inPlayToday").val().trim();
        //alert(type);
        var tool = (type == "BETS") ? "<b>{label}</b>: {y} bets" : "<b>{label}</b>: ${y}";
        var tool2 = (type == "BETS") ? "{label}: {y} bets" : "{label}: ${y}";

        if (type != "BETS")
        {
            tool = (type == "HOLD" || type == "WIN") ? "<b>{label}</b>: {y}%" : "<b>{label}</b>: ${y}";
            tool2 = (type == "HOLD" || type == "WIN") ? "{label}: {y}%" : "{label}: ${y}";
        }
         

         var chart = new CanvasJS.Chart("todayChart", {
	        theme: "light2", // "light1", "light2", "dark1", "dark2"
	        animationEnabled: true,
	        title: {
                text: "Actions",
                fontSize: 20,
	        },
	        data: [{
		        type: "pie",
		        startAngle: 25,
		        toolTipContent: tool,
		        showInLegend: "true",
		        legendText: "{label}",
		        indexLabelFontSize: 16,
		        indexLabel: tool2,
		        dataPoints: []
	        }]
        });

        
        var dataPoints = [];
        for (var i = 0; i < objTP.length; i++)
        {
            if (type == "BETS") {
                dataPoints.push({ y: objTP[i]["Bets"], label: objTP[i]["WagerPlay"] });
            } else if(type == "RISK")
            {
                dataPoints.push({ y: objTP[i]["RiskAmount"], label: objTP[i]["WagerPlay"] });
            }else if(type == "NET")
            {
                dataPoints.push({ y: objTP[i]["Net"], label: objTP[i]["WagerPlay"] });
            }else if(type == "HOLD")
            {
                dataPoints.push({ y: objTP[i]["HoldPercentaje"], label: objTP[i]["WagerPlay"] });
            }else if(type == "WIN")
            {
                dataPoints.push({ y: objTP[i]["WinPercentaje"], label: objTP[i]["WagerPlay"] });
            }
        }
            
        chart.options.data[0].dataPoints = dataPoints;
        chart.render();
    }
    function DrawChart6() //Today Chart
    {
         var chart = new CanvasJS.Chart("scalpingChart", {
	        animationEnabled: true,
	        theme: "light2", // "light1", "light2", "dark1", "dark2"
	        title: {
                text: "Bet Type",
                fontSize: 20,
	        },
	        axisY: {
		        title: $("#inTypeTodayChart").val(),
		        preffix: "$",
                includeZero: false,
                fontSize: 10,
	        },
	        axisX: {
                title: "Type",
                fontSize: 12
	        },
	        data: [{
		        type: "column",
		        yValueFormatString: "#,##0.0#",
		        dataPoints: []
	        }]
        });

        var type = $("#inTypeTodayChart").val();
        var dataPoints = [];
        for (var i = 0; i < objT.length; i++)
        {
            if (type == "BETS") {
                dataPoints.push({ label: objT[i]["WagerType"], y: objT[i]["Bets"] });
            } else if (type == "RISK")
            {
                dataPoints.push({ label: objT[i]["WagerType"], y: objT[i]["RiskAmount"] });
            }else if (type == "NET")
            {
                dataPoints.push({ label: objT[i]["WagerType"], y: objT[i]["Net"] });
            }else if (type == "HOLD")
            {
                dataPoints.push({ label: objT[i]["WagerType"], y: objT[i]["HoldPercentaje"] });
            }else if (type == "WIN")
            {
                dataPoints.push({ label: objT[i]["WagerType"], y: objT[i]["WinPercentaje"] });
            }
        }
            
        chart.options.data[0].dataPoints = dataPoints;
        chart.render();
    }
    function DrawChart7() 
    {
         var chart = new CanvasJS.Chart("chart1", {
	        theme: "light2", // "light1", "light2", "dark1", "dark2"
	        animationEnabled: true,
	        title: {
                text: "Bet Type",
                fontSize: 20,
	        },
	        data: [{
		        type: "pie",
		        startAngle: 25,
		        toolTipContent: "<b>{label}</b>: {y}%",
		        showInLegend: "true",
		        legendText: "{label}",
		        indexLabelFontSize: 16,
		        indexLabel: "{label} - {y}%",
		        dataPoints: [
			        { y: 51.08, label: "Straight" },
			        { y: 27.34, label: "Parlay" },
			        { y: 10.62, label: "Teaser" }
		        ]
	        }]
        });

        chart.render();
      }
    function DrawChart8()
    {
        var chart = new CanvasJS.Chart("leansChart", {
	    animationEnabled: true,
	    title: {
            text: "Leans Info",
            fontSize: 20
	    },
	    axisX: {
		    interval: 1
	    },
	    axisY: {
            title: "Values",
            fontSize: 10,
		    scaleBreaks: {
			    type: "wavy",
			    customBreaks: [{
				    startValue: 80,
				    endValue: 210
				    },
				    {
					    startValue: 230,
					    endValue: 600
				    }
		    ]}
	    },
	    data: [{
		    type: "bar",
		    toolTipContent: "<img src=\"https://canvasjs.com/wp-content/uploads/images/gallery/javascript-column-bar-charts/\"{url}\"\" style=\"width:40px; height:20px;\"> <b>{label}</b><br>Budget: ${y}bn<br>{gdp}% of GDP",
		    dataPoints: [
			    { label: "NFL", y: 25, gdp: 5.8, url: "israel.png" },
			    { label: "SOC", y: 30, gdp: 5.7, url: "uae.png" },
			    { label: "MLB", y: 35, gdp: 1.3, url: "brazil.png"},
			    { label: "Overall", y: 40, gdp: 2.0, url: "australia.png" }
		    ]
	    }]
    });
    chart.render();
      }
    function DrawChart9() 
    {       
        var chart = new CanvasJS.Chart("donutChart", {
	        animationEnabled: true,
	        theme: "light2",
	        title: {
		        text: ""
	        },
	        subtitles: [{
		        backgroundColor: "#2eacd1",
		        fontSize: 16,
		        fontColor: "white",
		        padding: 5
	        }],
	        data: [{
		        explodeOnClick: false,
		        innerRadius: "75%",
		        legendMarkerType: "square",
		        name: "",
		        radius: "100%",
		        showInLegend: true,
		        startAngle: 90,
		        type: "doughnut",
		        dataPoints: [],
	        }]
        });

        var datapoints = [];
        for (var i = 0; i < objPT.length; i++)
        {
            if (objPT[i]["WagerType"] != "Overall")
            {
                datapoints.push({ y: objPT[i]["Bets"], name: objPT[i]["WagerType"] });
            }
        }
        
        chart.options.data[0].dataPoints = datapoints;
        chart.render();

    }

    function LoadChartsFinancial()
    {
        page = 1;
        setTimeout(function () {
          DrawChart1();
          DrawChart2();

          setTimeout(function () {
              DrawChart4();   
          }, 50);

        }, 1);
    }
    function LoadChartsBets()
    {
        page = 2;
        setTimeout(function () {
          DrawChart3();
          DrawChart4();
          DrawChart9();
        }, 10);
    }
    function LoadChartsToday()
    {
        page = 4;
        setTimeout(function () {
          DrawChart5();
          DrawChart6();
          DrawChart4();
        }, 1);
    }
    function LoadChartLeans()
    {
       page = 3;
        setTimeout(function () {
            DrawChart8();
        }, 1);
    }

    window.onload = function ()
    {
        $("#btnSection").mouseup(function () {
            LoadCharts();
        });

        NavigationMain();
    }

      function LoadCharts() {
          setTimeout(function () {
              DrawChart1();
              DrawChart2();
              DrawChart3();
              DrawChart4();
              DrawChart5();
              DrawChart6();
              DrawChart8();
              DrawChart9();
          }, 15);
      }

     function ThisLastWeekJava(info)
     {
         obj = JSON.parse(info);
         typeFinancial = $("#inFinancialStats").val();
         var table;

          $("#groupTable").DataTable().clear();

         for (i = 0; i < obj.length; i++)
         {                        
             if (i == 0) {
                 $("#MainContent_inThisWeek").val('$ ' + obj[i]["Net"]);
                 $("#MainContent_inAgent").text(obj[i]["Agent"]);
                 $("#MainContent_inphone").text(obj[i]["Phone"]);
                 $("#MainContent_inEmail").text(obj[i]["Email"]);

                 if (obj[i]["Net"] < 0) {
                     $("#MainContent_inThisWeek").css("color", "#FF3333");
                 } else {
                     $("#MainContent_inThisWeek").css("color", "#3DBE98");
                 }
             }
             else if (i == 1)
             {
                 $("#MainContent_inLastWeek").val('$ ' + obj[i]["Net"]);

                 if (obj[i]["Net"] < 0) {
                     $("#MainContent_inLastWeek").css("color", "#FF3333");
                 } else
                 {
                     $("#MainContent_inLastWeek").css("color", "#3DBE98");
                 }
             }

             
             if (i != 1)
             {
                table = $('#groupTable').dataTable().fnAddData( [
                    obj[i]["DateRange"],
                    obj[i]["Bets"],
                    "$" + obj[i]["RiskAmount"],
                    "$" + obj[i]["Net"],
                    obj[i]["HoldPercentaje"] + "%"
               ]);
             }
         }

             $("#groupTable td").each(function () {

                 var n = $(this).text().includes("%");
                 if (n == "true" || n == true)
                 {
                     var d = $(this).text().replace("%", "");

                     if (d >= 80) $(this).css('color', 'yellow');
                     else if (d < 80 && d >= 0) $(this).css('color', 'green');
                     else $(this).css('color', 'red');
                 }
                
             });

     }

  </script>

</asp:Content>


<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Content1">
  <div class="mainpanel">    
    <div class="headerbar">      
      <a id="btnSection" class="menutoggle"><i class="fa fa-bars"></i></a>      
      <div class="header-right">     
      </div><!-- header-right -->
    </div><!-- headerbar -->
    
    <div class="pageheader">
      <h2><i class="fa fa-home"></i> Player Module <span>Subtitle goes here...</span></h2>
      <div class="breadcrumb-wrapper">
        <ol class="breadcrumb">
          <li class="active" runat="server" id="userName">Dashboard</li>
        </ol>
      </div>      
    </div>



<!-- Modal -->
<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Solution Config
        </h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
        </button>
      </div>
      <div class="modal-body">
        <form>

           <div class="form-group">
            <label class="col-sm-2 control-label">Solution:</label>
                    <div class="col-sm-7">
                         <select class="form-control chosen-select" name="inputSide" id="inSolutionCategory" runat="server">
                           <option value="">A New Solution 1</option>
                           <option value="">A New Solution 2</option>
                           <option value="">A New Solution 3</option>
                   </select>
               </div>
          </div>


           <div class="form-group">
            <label class="col-sm-2 control-label">Tigger:</label>
                    <div class="col-sm-7">
                         <select class="form-control chosen-select" name="inputSide" id="inTrigger" runat="server">
                           <option selected="selected"  value="none">None</option>
                           <option value="">Trigger 1</option>
                           <option value="">Trigger 2</option>
                   </select>
               </div>
          </div>


          <div class="form-group">
            <label class="col-sm-2 control-label">Comments:</label>
              <div class="col-sm-9">
                 <textarea class="form-control" style="resize:none; height:160px;" id="message-text"></textarea>   
             </div>
          </div>
        </form>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save</button>
      </div>
    </div>
  </div>
</div>
    

 <div class="contentpanel">  
   <!-- content here -->
   <div class="row">
           <div class="panel panel-default">
                <div class="panel-heading">
                  <div class="panel-btns">
                    <a href="#" class="panel-close">×</a>
                    <a href="#" class="minimize">−</a>
                  </div>
                  <h3 class="panel-title">Information</h3>
                </div>

         <div class="panel-body">
          <div class="row">
           <div class="col-xs-12">	
             <form runat="server">
              <div class="form-group whites">           
                    <label class="col-sm-1 control-label" style="font-weight:bold;">Player:</label>
                    <div class="col-sm-2">
                      <select id="inPlayer" runat="server"  class="form-control chosen-select" onchange="NavigationMain();">
                      </select>
                    </div>
                  
                  <label class="col-sm-3 control-label"></label>
                    <label class="col-sm-1 control-label" style="font-weight:bold;">Category:</label>
                    <div class="col-sm-1">
                      <div class="input-group">
                          <label class="col-sm-6 control-label" id="idCategory" runat="server">....</label>
                     </div>
                   </div>
              </div>
             <%--  --%>

             <div class="form-group whites" style="margin-top:10px;">
                  <label class="col-sm-1 control-label" style="font-weight:bold;">Agent:</label>
                    <div class="col-sm-2">
                      <div class="input-group">
                          <label class="col-sm-6 control-label" runat="server" id="inAgent">VTML</label>
                     </div>
                   </div>
                  
                  <label class="col-sm-3 control-label"></label>
                  <label class="col-sm-1 control-label" style="font-weight:bold;">Reg Date:</label>
                  <div class="col-sm-2">
                      <div class="input-group">
                        <input type="text" class="form-control" placeholder="mm/dd/yyyy" id="inRegDate" runat="server" autocomplete="off" />
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                      </div>
                  </div>
              </div>  

                 <%--  --%>
               <div class="form-group whites" style="margin-top:10px;">
                  <label class="col-sm-1 control-label" style="font-weight:bold;">Phone:</label>
                    <div class="col-sm-2">
                      <div class="input-group">
                          <label class="col-sm-6 control-label" runat="server" id="inphone">506873266743</label>
                     </div>
                   </div>
                  
                   <label class="col-sm-3 control-label"></label>
                   <label class="col-sm-1 control-label" style="font-weight:bold;">Email:</label>
                    <div class="col-sm-3">
                      <div class="input-group">
                          <label class="col-sm-6 control-label" runat="server" id="inEmail">player123@gmail.com</label>
                     </div>
                   </div>

              </div>   
            </form>                              
             </div>
           </div>
          </div>
        </div>
      </div>
   <div class="row" runat="server">
    <div class="col-xs-12">
       <ul class="nav nav-tabs nav-justified">
        <li class="nav-item active"><a data-toggle="tab" onclick="LoadChartsFinancial();" href="#financial">Financial</a></li>
        <li ><a data-toggle="tab" href="#menu2" onclick="LoadChartsBets();" runat="server">Bets</a></li>
        <li><a data-toggle="tab" href="#leans" onclick="LoadChartLeans();" runat="server">Leans</a></li>
        <li><a data-toggle="tab" href="#menu3" onclick="LoadChartsToday();" runat="server">Today's  Action</a></li>
       </ul>
    </div>
</div>

      <div class="tab-content">  
        <div id="financial" class="tab-pane active">
        <div class="row">
           <div class="lef_col" role="main">           
              <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title" style="margin-bottom:20px;">
                    <h3>Balance</h3>
                  </div>
                   <div class="x_content" style="height:195px; width:100%; overflow:auto;">
                     <div id="medio" class="centerVer" style="overflow:auto;">
                       <div class="form-group" style="border-bottom:groove;">
                        <label class="col-sm-2 control-label">This Week:</label>
                         <div class="col-sm-4">
                           <div class="input-group">
                            <input type="text" class="form-control" style="background-color:white; font-weight:bold;" placeholder="$0" value="$0" id="inThisWeek" runat="server" required="required" autocomplete="off" readonly="readonly" />
                           </div>
                        </div>

                        <label class="col-sm-2 control-label">Bet Type:</label>
                        <div class="col-sm-4">
                          <select ID="inWagerTypeFinancial" runat="server" class="form-control" onchange="FinancialAJX();">
                            <option Selected="selected" Value="">All</option>
                            <option Value="STRAIGHT">Straight Bet</option>
                            <option Value="TEASER">Teaser</option>
                            <option Value="Parlay">Parlay</option>
                          </select>
                       </div> 
                     </div>
                        <div class="form-group" style="border-bottom:groove; margin-top:20px; margin-bottom:20px;">
                             <label class="col-sm-2 control-label">Last Week:</label>
                             <div class="col-sm-4">
                               <div class="input-group">
                                <input type="text" class="form-control" style="background-color:white; font-weight:bold;" placeholder="$0" id="inLastWeek" value="$2500" runat="server" required="required" autocomplete="off" readonly="readonly" />
                               </div>
                            </div>
    
                           <label class="col-sm-2 control-label">Sport:</label>
                           <div class="col-sm-4">
                              <select ID="inSportFinancial" runat="server"  class="form-control" onchange="FinancialAJX();">
                              </select>
                           </div>
                        </div>
                     </div>
                   </div>
                </div>
              </div>

              <div class="col-md-6 col-sm-6 col-xs-12 boxes">
                <div class="x_panel" style="height:241px;">
                      <div class="x_title">
                        <h3>Grouped</h3>
                      </div>
                  <div class="x_content" style="overflow:auto;">
                    <table class="table table-responsive table-hover tablin" id="groupTable" style="width:100%; border:none;">
                      <thead>
                        <tr>
                          <th>Date Range</th>
                          <th>Bets</th>
                          <th>Risk</th>
                          <th>Net</th>
                          <th>Hold%</th>
                        </tr>
                      </thead>
                    </table>
                  </div>
                </div>
              </div>
            </div>
        </div>


      <div style="margin-top:20px;">      
        <div class="right_col" role="main">
            <div class="row">
              <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%;">
                <div class="x_panel">
                  <div class="form-group">
                      <div class="x_title">
                        <h3>Bet Type Stats</h3>
                      </div>
                    <div class="col-sm-3" style="right:0; top:0; position:absolute; margin-top:5px;">
                      <select class="form-control" id="inBetTypeFinancial" onchange="DrawChart1();">
                          <option selected="selected" value="TW">This Week</option>
                          <option value="LW">Last Week</option>
                          <option value="TS">This Season</option>
                          <option value="LS">Last Season</option>
                      </select>
                    </div>
                  </div>
                  <div class="x_content" style="height:100%; margin-bottom:10px;">
                      <div style="height:430px; width:100%;" id="chart1"></div>
                  </div>
                </div>
              </div>

              <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="form-group">
                      <div class="x_title">
                        <h3>Financial Stats</h3>
                      </div>
                    <div class="col-sm-3" style="right:0; top:0; position:absolute; margin-top:5px;">
                      <select class="form-control" onchange="DrawChart2();" id="inFinancialStats">
                         <option Selected="selected" Value="BETS">Bets</option>
                         <option Value="RISK">Risk</option>
                         <option Value="NET">Net</option>
                         <option Value="HOLD">Hold%</option>
                         <option Value="WIN">Win%</option>
                      </select>
                    </div>
                  </div>
                  <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px;">
                      <div style="height:430px; width:100%;" id="chart2"></div>
                  </div>
                </div>
              </div>
            </div>
        </div>
      </div>

        <div class="row">
                <div class="col-md-12 col-sm-6 col-xs-12 boxes" style="height:100%; width:100%; margin-top:10px;">
                  <div class="x_panel">
                   <div class="form-group">
                       <div class="x_title">
                          <h3>Player History Stats</h3>
                        </div>
                     <div class="col-sm-2" style="right:0; top:0; position:absolute; margin-top:10px;">
                       <select class="form-control" onchange="RangeSummaryAJX();" id="inDateRangeFinancial">
                           <option selected="selected" value="MMMM YYYY">Month</option>
                           <option selected="selected" value="DD MMMM">Week</option>
                           <option selected="selected" value="DD MMMM YYYY">Day</option>
                       </select>
                    </div>
                   </div>
                  <div class="x_content">
                     <div style="height:350px; width:100%; margin-bottom:10px; margin-right:10px;" id="finHistory"></div>
                 </div>
             </div>
           </div>
        </div>


       </div>
        <div id="menu2" class="tab-pane fade" style="width:100%;">
           <div class="form-group">

               <div class="col-sm-2">
                 <select ID="inSportBets" runat="server" class="form-control chosen-select">
                  </select>
              </div>

               <div class="col-sm-2" style="float:left;">
                 <select ID="inWagerTypeBets" runat="server" class="form-control chosen-select">
                     <option Selected="selected" Value="">All Types</option>
                     <option Value="STRAIGHT">Straight Bet</option>
                     <option Value="TEASER">Teaser</option>
                     <option Value="Parlay">Parlay</option>
                  </select>
              </div>

               <button class="btn btn-success" onclick="BetsAJX();">Go</button>

           </div>
           <div class="right_col" role="main">
            <div class="row">
              <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="form-group">
                      <div class="x_title">
                        <h4>Pro Play</h4>
                      </div>

                      <div class="col-sm-3" style="right:0; top:0; position:absolute; margin-top:5px;">
                      <select class="form-control" id="inProPlay" onchange="ProPlayAJX();">
                          <option selected="selected" value="T">Today</option>
                          <option value="Y">Yesterday</option>
                          <option value="TW">This Week</option>
                          <option value="LW">Last Week</option>
                          <option value="TS">This Season</option>
                          <option value="LS">Last Season</option>
                      </select>
                    </div>
                  </div>

                 
                  <div class="x_content" style="height:100%; overflow-x:auto; margin-bottom:10px;">
                   <table class="table table-responsive table-hover  tablin" id="tableProPlay" style="width:100%; border:none;">
                      <thead>
                        <tr>
                          <th>Action</th>
                          <th>Value</th>
                        </tr>
                      </thead>
                    </table>
                  </div>
                </div>
              </div>

              <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%;">
                <div class="x_panel h-10 d-inline-block p-2">
                <div class="form-group">
              
                 <h4>Stats</h4>

                <div class="col-sm-3" style="float:right; position:absolute;top:0;right:0; margin-top:5px; margin-bottom:5px;">
                 <select ID="inBetsChartFilter" runat="server" class="form-control" onchange="DrawChart3();">
                     <option Selected="selected" Value="BETS">Bets</option>
                     <option Value="RISK">Risk</option>
                     <option Value="NET">Net</option>
                     <option Value="HOLD">Hold%</option>
                     <option Value="WIN">Win%</option>
                  </select>
                </div>

                </div>            
                  <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px;">
                      <div style="height:402px; width:100%;" id="betChart"></div>
                  </div>
                </div>
              </div>

                <%-- section --%>

              <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="margin-top:10px;">
                <div class="x_panel">
                    <div class="form-group">
                      <div class="x_title">
                        <h4>Parlay/Teaser</h4>
                      </div>

                    <div class="col-sm-3" style="right:0; top:0; position:absolute; margin-top:5px; margin-bottom:0">
                      <select class="form-control" style="height:35px;" id="inParlayTeaser" onchange="ParlayTeaserAJX();">
                          <option selected="selected" value="T">Today</option>
                          <option value="Y">Yesterday</option>
                          <option value="TW">This Week</option>
                          <option value="LW">Last Week</option>
                          <option value="TS">This Season</option>
                          <option value="LS">Last Season</option>
                      </select>
                    </div>
                  </div>
                  <div class="x_content" style="overflow-x:auto; margin-bottom:10px;">
                    <table class="table table-responsive tablin" id="ParTeaTable" style="width:100%; border:none;">
                      <thead>
                        <tr>
                          <th>Wager Type</th>
                          <th>Bets</th>
                          <th>Risk</th>
                          <th>Net</th>
                          <th>Hold%</th>
                          <th>Win%</th>
                        </tr>
                      </thead>
                    </table>
                  </div>
                </div>
              </div>


               <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="margin-top:10px;">
                <div class="x_panel h-10 d-inline-block p-2">
                      <div class="x_title">
                        <h4>Parlay/Teaser Chart</h4>
                      </div>
                  <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px;">
                      <div style="height:155px; width:100%;" id="donutChart"></div>
                  </div>
                </div>
              </div>



<%-- BET STATS  --%>

              <div class="col-md-12 col-sm-6 col-xs-12 boxes" style="height:100%; margin-top:10px;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title">
                    <h4>Bet Stats</h4>
                  </div>
                  <div class="x_content" style="height:100%; overflow-x:auto; overflow-y:auto; margin-bottom:5px;">
                   <table class="table table-responsive table-hover tablin" id="talbeBetStats" style="width:100%; border:none;">
                      <thead>
                        <tr>
                          <th>Date Range</th>
                          <th>Type</th>
                          <th>Bets</th>
                          <th>Risk</th>
                          <th>Net</th>
                          <th>Hold%</th>
                          <th>Win%</th>
                        </tr>
                      </thead>
                    </table> 
                  </div>
                </div>
              </div>
<%--  --%>



                <%-- SOLUTIONS --%>

               <div class="col-md-12 col-sm-6 col-xs-12 boxes" style="height:auto; margin-top:10px;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title">

                    <div class="form-group">
                       <h4>Solutions</h4>
                       <div class="col-sm-1" style="right:0; top:0; position:absolute; margin-top:5px;">
                          <button class="btn btn-block btn-success glyphicon glyphicon-plus" data-toggle="modal" data-target="#exampleModal" data-placement="top" title="add new solution"></button>
                      </div>
                    </div>
                    
                  </div>
                   <div class="x_content" style="height:100%; width:100%; overflow:auto; margin-right:10px; margin-bottom:10px;">                

                     <table class="table table-responsive table-hover" style="overflow:auto;">
                      <thead>
                        <tr>
                          <th>#</th>
                          <th>Solution</th>
                          <th>Trigger</th>
                          <th>Comments</th>
                          <th>Edit</th>
                          <th>Remove</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                          <th>1</th>
                          <td>Solution Name</td>
                          <td>Trigger Name</td>
                          <td>comments...</td>                         
                          <td><button class="btn btn-warning glyphicon glyphicon-pencil"></button></td>
                          <td><button class="btn btn-danger glyphicon glyphicon-trash"></button></td>
                        </tr>
                        <tr>
                          <th>2</th>
                          <td>Solution Name</td>
                          <td>Trigger Name</td>
                          <td>comments...</td>                         
                          <td><button class="btn btn-warning glyphicon glyphicon-pencil"></button></td>
                          <td><button class="btn btn-danger glyphicon glyphicon-trash" ></button></td>
                        </tr>
                      </tbody>
                    </table>
              
                   </div>
                </div>
              </div>



            </div>
        </div>
        </div>
        <div id="menu3" class="tab-pane fade">

             <div class="form-group">
               <div class="col-sm-2">
                 <select ID="inSPortToday" runat="server" class="form-control chosen-select">
                  </select>
              </div>

               <div class="col-sm-2" style="float:left;">
                 <select ID="inTypeToday" runat="server" class="form-control chosen-select">
                     <option Selected="selected" Value="">All Types</option>
                     <option Value="STRAIGHT">Straight Bet</option>
                     <option Value="TEASER">Teaser</option>
                     <option Value="Parlay">Parlay</option>
                  </select>
              </div>
               <button class="btn btn-success" onclick="TodayAJX();">Go</button>
           </div>


           <div class="right_col" role="main">
            <div class="row">
              <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%;">
                <div class="x_panel h-10 d-inline-block p-2">
                     <div class="form-group">
                      <div class="x_title">
                        <h4>Action Stats</h4>
                      </div>

                    <div class="col-sm-3" style="right:0; top:0; position:absolute; margin-top:5px; margin-bottom:0">
                      <select class="form-control" style="height:35px;" id="inPlayToday" onchange="DrawChart5();">
                          <option selected="selected" value="BETS">Bets</option>
                          <option value="RISK">Risk</option>
                          <option value="NET">Net</option>
                          <option value="HOLD">Hold%</option>
                          <option value="WIN">Win%</option>
                      </select>
                    </div>
                  </div>
                    <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px;">
                      <div style="height:416px; width:100%;" id="todayChart"></div>
                  </div>
                </div>
              </div>

              <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="form-group">
                      <div class="x_title">
                        <h4>Type Stats</h4>
                      </div>

                    <div class="col-sm-3" style="right:0; top:0; position:absolute; margin-top:5px; margin-bottom:0">
                      <select class="form-control" style="height:35px;" id="inTypeTodayChart" onchange="DrawChart6();">
                          <option selected="selected" value="BETS">Bets</option>
                          <option value="RISK">Risk</option>
                          <option value="NET">Net</option>
                          <option value="HOLD">Hold%</option>
                          <option value="WIN">Win%</option>
                      </select>
                    </div>
                  </div>
                  <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px;">
                      <div style="height:416px; width:100%;" id="scalpingChart"></div>
                  </div>
                </div>
              </div>
            </div>
        </div>
            <div class="row">
              <div class="col-md-12 col-sm-6 col-xs-12 boxes" style="height:100%; margin-top:10px;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title">
                    <h4>Actions</h4>
                  </div>
                    
                    <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px; overflow-x:auto;">                
                    <table class="table table-responsive table-hover tablin" id="tableToday" style="width:100%; border:none;">
                      <thead>
                        <tr>
                          <th>Player</th>
                          <th>Risk Amount</th>
                          <th>Bets</th>
                          <th>Move Line%</th>
                          <th>Beat Line%</th>
                          <th>Scalping%</th>
                          <th>Adjusted%</th>
                        </tr>
                      </thead>
                    </table>
                  </div>
                 </div>

                </div>
              </div>


         </div>
        <div id="leans" class="tab-pane fade">
              <div class="form-group">

                    <div class="col-sm-2">
                       <div class="input-group">
                        <input type="text" class="form-control" placeholder="mm/dd/yyyy" id="startDate" runat="server" name="startDate" required="required" autocomplete="off" />
                         <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                         </div>
                     </div>

                     <div class="col-sm-2">
                      <div class="input-group">
                         <input type="text" class="form-control" placeholder="mm/dd/yyyy" id="endDate" runat="server" name="endDate" required="required" autocomplete="off" />
                         <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                     </div>
                   </div>



               <div class="col-sm-2">
                 <select ID="inSportLeans" runat="server" class="form-control chosen-select">
                  </select>
              </div>

                  <button class="btn btn-success">Go</button>
           </div>

                <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%; margin-top:10px;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title">
                    <h3>Leans</h3>
                  </div>
                  <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px; overflow-x:auto;">
                      
                    <table class="table table-responsive table-hover" style="overflow-x:auto;">
                      <thead>
                        <tr>
                          <th>Values</th>
                          <th>Overall w/adj</th>
                          <th>Overall w/adj</th>
                          <th>Sport w/adj</th>
                          <th>Sport w/o adj </th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                          <td>Net</td>
                          <td>15</td>
                          <td>10</td>
                          <td>17</td>
                          <td>43</td>
                        </tr>
                        <tr>
                          <td>Lines Played</td>
                          <td>23</td>
                          <td>25</td>
                          <td>67</td>
                          <td>43</td>
                        </tr>
                        <tr>
                          <td>Current Line</td>
                          <td>10</td>
                          <td>35</td>
                          <td>27</td>
                          <td>74</td>
                        </tr>
                         <tr>
                          <td>Negative Line</td>
                          <td>50</td>
                          <td>12</td>
                          <td>18</td>
                          <td>25</td>
                        </tr>
                        <tr>
                          <td>Positive Line</td>
                          <td>25</td>
                          <td>12</td>
                          <td>18</td>
                          <td>25</td>
                        </tr>
                        <tr>
                          <td>Result</td>
                          <td>20</td>
                          <td>12</td>
                          <td>18</td>
                          <td>25</td>
                        </tr>
                      </tbody>
                    </table>

                  </div>
                </div>
              </div>


            <%-- leans graph --%>

               <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%; margin-top:10px;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title">
                  <div class="form-group">
              
               <h4>Leans Chart</h4>
               <div class="col-sm-3" style="float:right; position:absolute;top:0;right:0; margin-top:5px; margin-bottom:5px;">
                 <select ID="inLeansChart" runat="server" class="form-control" onchange="DrawChart8();">
                     <option Selected="selected" Value="BET">Bets</option>
                     <option Value="RISK">Risk</option>
                     <option Value="NET">Net</option>
                     <option Value="HOLD">Hold%</option>
                     <option Value="WIN">Win%</option>
                  </select>
                </div>

                </div>  
                  </div>
                  <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px;">
                      <div style="height:294px; width:100%;" id="leansChart"></div>
                  </div>
                </div>
              </div>

            <%-- end leans grapgh --%>


        </div>
      </div>

      
    </div><!-- contentpanel -->
  </div><!-- mainpanel -->

</asp:Content>