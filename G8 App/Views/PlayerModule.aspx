﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/menu.Master" CodeBehind="PlayerModule.aspx.cs" Inherits="G8_App.Views.PlayerModule" %>


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

        table.dataTable thead th {
          border-bottom: 0;
          border-bottom:groove;
        }

        table.dataTable tbody td{
          border-top: 0;      
        }

        .table
        {
            border:none;
        }

    </style>
    
  <script type="text/javascript" src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>
  <script>
    var obj;
    var obj2;
    var typeFinancial;


      function FinancialAJX()
      {
          LastThisWeekAJX();
          RangeSummaryAJX();
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
              //ThisLastWeekJava(data.d);
               //DrawChart2();
               //DrawChart1();
               //Spinner(2);
           },
           error: function (data)
           {
              alert('No Data' + data.statusText);
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
		        title: "Amount",
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
               if (typeFinancial == "BET")
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
           }
        }

        chart.options.data[0].dataPoints = dataPoints;
        chart.render();
    }
    function DrawChart3()
    {
         var chart = new CanvasJS.Chart("betChart", {
	        animationEnabled: true,
	        theme: "light2", // "light1", "light2", "dark1", "dark2"
	        title: {
                text: "Summary",
                fontSize: 20,
	        },
	        axisY: {
		        title: "Values",
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
		        yValueFormatString: "$#,##0.0#",
		        dataPoints: [
                    { label: "Current Week", y: 40000 },	
			        { label: "Current Season", y: 35000},	
                    { label: "Over All", y: 30000},
		        ]
	        }]
         });
         chart.render();
      }
    function DrawChart4() 
    {
        var chart = new CanvasJS.Chart("lineChart", {
	        animationEnabled: true,
	        theme: "light2",
	        title:{
                text: "Player's Stats Between Date Range",
                fontSize: 20,
	        },
	        axisX:{
		        valueFormatString: "DD MMM",
		        crosshair: {
			        enabled: true,
		        }
	        },
	        axisY: {
                title: "Values",
                fontSize: 16,
		        crosshair: {
			        enabled: true
		        }
	        },  
	        legend:{
		        cursor:"pointer",
		        verticalAlign: "bottom",
		        horizontalAlign: "center",
		        dockInsidePlotArea: true,
		        itemclick: toogleDataSeries
	        },
	        data: [{
		        type: "line",
		        showInLegend: true,
		        name: "Net",
		        markerType: "square",
		        xValueFormatString: "DD MMM, YYYY",
		        color: "#F08080",
		        dataPoints: [
			        { x: new Date(2017, 0, 3), y: 650 },
			        { x: new Date(2017, 0, 4), y: 700 },
			        { x: new Date(2017, 0, 5), y: 710 },
			        { x: new Date(2017, 0, 6), y: 658 },
			        { x: new Date(2017, 0, 7), y: 734 },
			        { x: new Date(2017, 0, 8), y: 963 },
			        { x: new Date(2017, 0, 9), y: 847 },
			        { x: new Date(2017, 0, 10), y: 853 },
			        { x: new Date(2017, 0, 11), y: 869 },
			        { x: new Date(2017, 0, 12), y: 943 },
			        { x: new Date(2017, 0, 13), y: 970 },
			        { x: new Date(2017, 0, 14), y: 869 },
			        { x: new Date(2017, 0, 15), y: 890 },
			        { x: new Date(2017, 0, 16), y: 930 }
		        ]
	        },
	        {
		        type: "line",
		        showInLegend: true,
		        name: "Amount",
		        lineDashType: "dash",
		        dataPoints: [
			        { x: new Date(2017, 0, 3), y: 510 },
			        { x: new Date(2017, 0, 4), y: 560 },
			        { x: new Date(2017, 0, 5), y: 540 },
			        { x: new Date(2017, 0, 6), y: 558 },
			        { x: new Date(2017, 0, 7), y: 544 },
			        { x: new Date(2017, 0, 8), y: 693 },
			        { x: new Date(2017, 0, 9), y: 657 },
			        { x: new Date(2017, 0, 10), y: 663 },
			        { x: new Date(2017, 0, 11), y: 639 },
			        { x: new Date(2017, 0, 12), y: 673 },
			        { x: new Date(2017, 0, 13), y: 660 },
			        { x: new Date(2017, 0, 14), y: 562 },
			        { x: new Date(2017, 0, 15), y: 643 },
			        { x: new Date(2017, 0, 16), y: 570 }
		        ]
	        }]
        });
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
    function DrawChart6()
    {
         var chart = new CanvasJS.Chart("scalpingChart", {
	        animationEnabled: true,
	        theme: "light2", // "light1", "light2", "dark1", "dark2"
	        title: {
                text: "Bet Type",
                fontSize: 20,
	        },
	        axisY: {
		        title: "Amount",
		        preffix: "$",
                includeZero: false,
                fontSize: 14,
	        },
	        axisX: {
                title: "Type",
                fontSize: 14
	        },
	        data: [{
		        type: "column",
		        yValueFormatString: "$#,##0.0#",
		        dataPoints: [
			        { label: "Straight", y: 4000 },	
			        { label: "Parlay", y: 3500 },	
                    { label: "Teaser", y: 3000 },
                    { label: "Other", y: 2500 }
		        ]
	        }]
         });
         chart.render();
      }
    function DrawChart7() 
    {
      var chart = new CanvasJS.Chart("finHistory", {
	    animationEnabled: true,
	    theme: "light2", // "light1", "light2", "dark1", "dark2"
	    exportEnabled: true,
	    title:{
            text: "Information by Date",
             fontSize: 20,
	    },
	    axisX: {
		    valueFormatString: "MMM"
	    },
	    axisY: {
		    includeZero:false, 
		    prefix: "$",
		    title: "Values"
	    },
	    legend: {
		    cursor: "pointer",
		    itemclick: toogleDataSeries
	    },
	    data: [{
		    type: "candlestick",
		    showInLegend: true,
		    name: "Straight",
		    yValueFormatString: "$###0.00",
            xValueFormatString: "MMMM YY",
            color: "#0079CF",
		    dataPoints: [
			    { x: new Date(2016, 00, 01), y: [34.080002, 36.060001, 33.410000, 36.060001] },
			    { x: new Date(2016, 01, 01), y: [36.040001, 37.500000, 35.790001, 36.950001] },
			    { x: new Date(2016, 02, 01), y: [37.099998, 39.720001, 37.060001, 39.169998] },
			    { x: new Date(2016, 03, 01), y: [38.669998, 39.360001, 37.730000, 38.820000] },
			    { x: new Date(2016, 04, 01), y: [38.869999, 39.669998, 37.770000, 39.150002] },
			    { x: new Date(2016, 05, 01), y: [39.099998, 43.419998, 38.580002, 43.209999] },
			    { x: new Date(2016, 06, 01), y: [43.209999, 43.889999, 41.700001, 43.290001] },
			    { x: new Date(2016, 07, 01), y: [43.250000, 43.500000, 40.549999, 40.880001] },
			    { x: new Date(2016, 08, 01), y: [40.849998, 41.700001, 39.549999, 40.610001] },
			    { x: new Date(2016, 09, 01), y: [40.619999, 41.040001, 36.270000, 36.790001] },
			    { x: new Date(2016, 10, 01), y: [36.970001, 39.669998, 36.099998, 38.630001] },
                { x: new Date(2016, 11, 01), y: [38.630001, 42.840000, 38.160000, 40.380001] },
                { x: new Date(2017, 00, 01), y: [48.630001, 46.840000, 32.160000, 42.380001] },
                { x: new Date(2017, 01, 01), y: [58.630001, 43.840000, 35.160000, 43.380001] },
                { x: new Date(2017, 02, 01), y: [38.630001, 49.840000, 37.160000, 48.380001] },
                { x: new Date(2017, 03, 01), y: [48.630001, 38.840000, 32.160000, 45.380001] },
                { x: new Date(2017, 04, 01), y: [38.630001, 32.840000, 33.160000, 44.380001] },
                { x: new Date(2017, 05, 01), y: [35.630001, 44.840000, 35.160000, 40.380001] },
                { x: new Date(2017, 06, 01), y: [38.630001, 40.840000, 36.160000, 44.380001] },
                { x: new Date(2017, 07, 01), y: [32.630001, 38.840000, 39.160000, 38.380001] },
                { x: new Date(2017, 08, 01), y: [31.630001, 42.840000, 31.160000, 45.380001] }

		    ]
	    }]
      });
      chart.render();

        function toogleDataSeries(e) {
	        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
		        e.dataSeries.visible = false;
	        } else {
		        e.dataSeries.visible = true;
	        }
	        e.chart.render();
        }
    }

    function LoadChartsFinancial()
    {
        setTimeout(function () {
         // DrawChart1();
          //DrawChart2();

          setTimeout(function () {
              DrawChart7();   
          }, 700);

        }, 1);
    }
    function LoadChartsBets()
    {
        setTimeout(function () {
          DrawChart3();
          DrawChart4();
        }, 10);
    }
    function LoadChartsToday()
    {
        setTimeout(function () {
          DrawChart5();
          DrawChart6();
          DrawChart7();
        }, 1);
    }
    
    window.onload = function ()
    {
        LoadChartsFinancial();
        $("#btnSection").mouseup(function () {
            LoadCharts();
        });

        FinancialAJX();
    }

      function LoadCharts() {
          setTimeout(function () {
              //DrawChart1();
              //DrawChart2();
              DrawChart3();
              DrawChart4();
              DrawChart5();
              DrawChart6();
              DrawChart7();
          }, 15);
      }



      function ThisLastWeekJava(info)
      {
         obj = JSON.parse(info);
         typeFinancial = $("#inFinancialStats").val();

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
               $('#groupTable').dataTable().fnAddData( [
                    obj[i]["DateRange"],
                    obj[i]["Bets"],
                    "$" + obj[i]["RiskAmount"],
                    "$" + obj[i]["Net"],
                    obj[i]["HoldPercentaje"] + "%"
               ]);
             }
         }
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
                    <div class="col-sm-4">
                         <select class="form-control chosen-select" name="inputSide" id="inSolutionCategory" runat="server">
                           <option value="">Solution 1</option>
                           <option value="">Solution 2</option>
                           <option value="">Solution 3</option>
                   </select>
               </div>
          </div>

          <div class="form-group">
            <label for="recipient-name" class="col-form-label">Name:</label>
            <input type="text" class="form-control" id="recipient-name">
          </div>
          
          <div class="form-group">
            <label for="message-text" class="col-form-label">Comments:</label>
            <textarea class="form-control" style="resize:none; height:140px;" id="message-text"></textarea>
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
                      <select id="inPlayer" runat="server"  class="form-control chosen-select" onchange="FinancialAJX();">
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
        <li><a data-toggle="tab" href="#leans" onclick="LoadChartsToday();" runat="server">Leans</a></li>
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
                         <option Selected="selected" Value="BET">Bets</option>
                         <option Value="RISK">Risk</option>
                         <option Value="NET">Net</option>
                         <option Value="HOLD">Hold%</option>
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
                          <h3>Chart</h3>
                        </div>
                     <div class="col-sm-2" style="right:0; top:0; position:absolute; margin-top:10px;">
                       <select class="form-control" onchange="DrawChart7();" id="inDateRangeFinancial">
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
                 <select ID="Select4" runat="server" class="form-control chosen-select">
                     <option Selected="selected" Value="">All Types</option>
                     <option Value="STRAIGHT">Straight Bet</option>
                     <option Value="TEASER">Teaser</option>
                     <option Value="Parlay">Parlay</option>
                  </select>
              </div>

               <button class="btn btn-success">Go</button>

           </div>
           <div class="right_col" role="main">
            <div class="row">
              <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="form-group">
                      <div class="x_title">
                        <h4>Bet Stats</h4>
                      </div>

                      <div class="col-sm-3" style="right:0; top:0; position:absolute; margin-top:5px;">
                      <select class="form-control" onchange="DrawChart1();">
                          <option selected="selected" value="">Today</option>
                          <option value="">Yesterday</option>
                          <option value="">This Week</option>
                          <option value="">Last Week</option>
                          <option value="">This Season</option>
                          <option value="">Last Season</option>
                      </select>
                    </div>
                  </div>

                 
                  <div class="x_content" style="height:100%; overflow-x:auto;">
                   <table class="table table-responsive table-hover" style="overflow-x:auto;">
                      <thead>
                        <tr>
                          <th>Action</th>
                          <th>Percentage</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                          <th>Move Line</th>
                          <td>12</td>
                        </tr>
                        <tr>
                          <th>Time Pattern</th>
                          <td>Curve</td>
                        </tr>
                        <tr>
                          <th>Beat the Line</th>
                          <td>15</td>
                        </tr>
                        <tr>
                          <th>Scalp Cris</th>
                          <td>40%</td>
                        </tr>
                        <tr>
                          <th>Scalp Pinni</th>
                          <td>65%</td>
                        </tr>
                        <tr>
                          <th>Scalp 5Dimes</th>
                          <td>34%</td>
                        </tr>
                        <tr>
                          <th>Scalp PPH</th>
                          <td>78%</td>
                        </tr>
                        <tr>
                          <th>Scalp Jazz</th>
                          <td>18%</td>
                        </tr>
                        <tr>
                          <th>Over all Scalp</th>
                          <td>68%</td>
                        </tr>
                         <tr>
                          <th>Syndicate</th>
                          <td>Yes</td>
                        </tr>
                          <tr>
                          <th>Adjusted</th>
                          <td>75%</td>
                        </tr>
                      </tbody>
                    </table>
                  </div>
                </div>
              </div>

              <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%;">
                <div class="x_panel h-10 d-inline-block p-2">
                <div class="form-group">
              
                 <h4>Stats</h4>

                <div class="col-sm-3" style="float:right; position:absolute;top:0;right:0; margin-top:5px; margin-bottom:5px;">
                 <select ID="Select5" runat="server" class="form-control" onchange="DrawChart3();">
                     <option Selected="selected" Value="">Bets</option>
                     <option Value="">Risk</option>
                     <option Value="">Net</option>
                     <option Value="">Hold%</option>
                     <option Value="">Win%</option>
                  </select>
                </div>

                </div>            
                  <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px;">
                      <div style="height:462px; width:100%;" id="betChart"></div>
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
                      <select class="form-control" style="height:35px;">
                          <option selected="selected" value="">Today</option>
                          <option value="">Yesterday</option>
                          <option value="">This Week</option>
                          <option value="">Last Week</option>
                          <option value="">This Season</option>
                          <option value="">Last Season</option>
                      </select>
                    </div>
                  </div>
                  <div class="x_content" style="overflow-x:auto;">
                    <table class="table table-responsive table-hover" style="overflow-x:auto;">
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
                      <tbody>
                        <tr>
                          <th>Parlay</th>
                          <td>15</td>
                          <td>$1500</td>
                          <td>$1100</td>
                          <td>73.33%</td>
                          <td>43.33%</td>
                        </tr>
                        <tr>
                          <th>Teaser</th>
                          <td>30</td>
                          <td>$15.000</td>
                          <td>$11.100</td>
                          <td>74%</td>
                          <td>33.33%</td>
                        </tr>
                         <tr>
                          <th>Overall</th>
                          <td>45</td>
                          <td>$16.500</td>
                          <td>$12.100</td>
                          <td>67%</td>
                          <td>73.33%</td>
                        </tr>
                      </tbody>
                    </table>
                  </div>
                </div>
              </div>


              <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%; margin-top:10px;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title">
                    <h4>Bet Stats</h4>
                  </div>
                  <div class="x_content" style="height:100%; overflow-x:auto; overflow-y:auto; margin-bottom:5px;">
                   <table class="table table-responsive table-hover" style="overflow-x:auto;">
                      <thead>
                        <tr>
                          <th>Date Range</th>
                          <th>Bets</th>
                          <th>Risk</th>
                          <th>Net</th>
                          <th>Hold%</th>
                          <th>Win%</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                          <th>Current Week</th>
                          <td>15</td>
                          <td>$1500</td>
                          <td>$1100</td>
                          <td>73.33%</td>
                          <td>73.33%</td>
                        </tr>
                        <tr>
                          <th>Current Season</th>
                          <td>30</td>
                          <td>$15.000</td>
                          <td>$11.100</td>
                          <td>74%</td>
                          <td>74%</td>
                        </tr>
                        <tr>
                          <th>Over All</th>
                          <td>45</td>
                          <td>$150.000</td>
                          <td>$110.100</td>
                          <td>74%</td>
                          <td>74%</td>
                        </tr>
                      </tbody>
                    </table> 
                  </div>
                </div>
              </div>





             <div class="col-md-12 col-sm-6 col-xs-12 boxes" style="height:100%; margin-top:10px;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title">
                    <h4>Solutions</h4>
                  </div>
                   <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px;">                

                     <table class="table table-responsive table-hover" style="overflow-x:auto;">
                      <thead>
                        <tr>
                          <th>#</th>
                          <th>Solution</th>
                          <th>Comments</th>
                          <th>Edit</th>
                          <th>Remove</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                          <th>1</th>
                          <td>Solution Name</td>
                          <td>comments...</td>                         
                          <td><button class="btn btn-warning glyphicon glyphicon-pencil"></button></td>
                          <td><button class="btn btn-danger glyphicon glyphicon-trash"></button></td>
                        </tr>
                        <tr>
                          <th>2</th>
                          <td>Solution Name</td>
                          <td>comments...</td>                         
                          <td><button class="btn btn-warning glyphicon glyphicon-pencil"></button></td>
                          <td><button class="btn btn-danger glyphicon glyphicon-trash"></button></td>
                        </tr>
                      </tbody>
                    </table>

                    <div class="form-group">
                      <div class="col-sm-1" style="float:right;">
                          <button class="btn btn-block btn-success glyphicon glyphicon-plus" data-toggle="modal" data-target="#exampleModal" data-placement="top" title="add new solution"></button>
                      </div>                    
                    </div>

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
                 <select ID="Select9" runat="server" class="form-control chosen-select">
                     <option Selected="selected" Value="">All Types</option>
                     <option Value="STRAIGHT">Straight Bet</option>
                     <option Value="TEASER">Teaser</option>
                     <option Value="Parlay">Parlay</option>
                  </select>
              </div>

               <button class="btn btn-success">Go</button>

           </div>


           <div class="right_col" role="main">
            <div class="row">
              <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title">
                    <h4>Action Stats</h4>
                  </div>
                    <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px;">
                      <div style="height:416px; width:100%;" id="todayChart"></div>
                  </div>
                </div>
              </div>

              <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title">
                    <h4>Type Stats</h4>
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
                    <table class="table table-responsive table-hover" style="overflow-x:auto;">
                      <thead>
                        <tr>
                          <th>Player</th>
                          <th>Risk Amount</th>
                          <th>Bets</th>
                          <th>Move the Line</th>
                          <th>Beat The Line</th>
                          <th>Scalping %</th>
                          <th>Adjusted %</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                          <td>TM005</td>
                          <td>$1000</td>
                          <td>10</td>
                          <td>2</td>
                          <td>4</td>
                          <td>17%</td>
                          <td>28%</td>
                        </tr>
                      </tbody>
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

                <div class="col-md-12 col-sm-6 col-xs-12 boxes" style="height:100%; margin-top:10px;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title">
                    <h3>Leans</h3>
                  </div>
                  <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px; overflow-x:auto;">
                      
                    <table class="table table-responsive table-hover" style="overflow-x:auto;">
                      <thead>
                        <tr>
                          <th>Action</th>
                          <th>Net</th>
                          <th>Lines Played</th>
                          <th>Current Line</th>
                          <th>Negative Lines</th>
                          <th>Positive Lines Win</th>
                          <th>Result</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                          <td>Overall w/adjustment</td>
                          <td>$1000</td>
                          <td>10</td>
                          <td>17</td>
                          <td>43</td>
                          <td>23</td>
                          <td>44</td>
                        </tr>
                        <tr>
                          <td>Overall w/o adjustment</td>
                          <td>$1500</td>
                          <td>25</td>
                          <td>67</td>
                          <td>43</td>
                          <td>23</td>
                          <td>44</td>
                        </tr>
                        <tr>
                          <td>Sport w/adjustment</td>
                          <td>$2000</td>
                          <td>35</td>
                          <td>27</td>
                          <td>74</td>
                          <td>23</td>
                          <td>44</td>
                        </tr>
                         <tr>
                          <td>Sport w/o adjustment</td>
                          <td>$2500</td>
                          <td>12</td>
                          <td>18</td>
                          <td>25</td>
                          <td>23</td>
                          <td>44</td>
                        </tr>
                      </tbody>
                    </table>

                  </div>
                </div>
              </div>


        </div>
      </div>

      
    </div><!-- contentpanel -->
  </div><!-- mainpanel -->

</asp:Content>