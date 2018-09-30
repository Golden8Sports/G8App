<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayerMod.aspx.cs" MasterPageFile="~/Views/menu.Master" Inherits="G8_App.Views.PlayerMod" %>

<asp:Content runat="server" ID="head" ContentPlaceHolderID="head1">
  <script src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>
  <script type="text/javascript" src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>
  <script>

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
		        dataPoints: [
			        { y: 51000, label: "Straight" },
			        { y: 47000, label: "Parlay" },
                    { y: 20000, label: "Teaser" },
                    { y: 10000, label: "Other" }
		        ]
	        }]
        });
        chart.render();
        }
    function DrawChart2()
    {
         var chart = new CanvasJS.Chart("chart2", {
	        animationEnabled: true,
	        theme: "light2", // "light1", "light2", "dark1", "dark2"
	        title: {
                text: "General Summary",
                fontSize: 20,
	        },
	        axisY: {
		        title: "Net",
		        prefix: "$",
                includeZero: false,
                fontSize: 5,
	        },
	        axisX: {
	        },
	        data: [{
		        type: "column",
		        yValueFormatString: "$#,##0.0#",
		        dataPoints: [
			        { label: "Current Week", y: 40000 },	
			        { label: "Last Month", y: 35000 },	
                    { label: "Current Season", y: 30000 },
                    { label: "Last Season", y: 25000 }
		        ]
	        }]
         });
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
		        title: "Amount",
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
      var chart = new CanvasJS.Chart("leansChart", {
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
          DrawChart1();
          DrawChart2();

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
    }

    function LoadCharts()
      {
          setTimeout(function () {
              DrawChart1();
              DrawChart2();
              DrawChart3();
              DrawChart4();
              DrawChart5();
              DrawChart6();
              DrawChart7();
          }, 10);
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
              <div class="form-group whites">
                    <label class="col-sm-1 control-label" style="font-weight:bold;">Player:</label>
                    <div class="col-sm-2">
                      <select id="inPlayer" runat="server"  class="form-control chosen-select">
                        <option selected="selected" value="">TM005</option>
                      </select>
                    </div>
                  
                  <label class="col-sm-3 control-label"></label>
                    <label class="col-sm-1 control-label" style="font-weight:bold;">Category:</label>
                    <div class="col-sm-1">
                      <div class="input-group">
                          <label class="col-sm-6 control-label">WiseGuy</label>
                     </div>
                   </div>
              </div>
             <%--  --%>

             <div class="form-group whites" style="margin-top:10px;">
                  <label class="col-sm-1 control-label" style="font-weight:bold;">Agent:</label>
                    <div class="col-sm-2">
                      <div class="input-group">
                          <label class="col-sm-6 control-label">VTML</label>
                     </div>
                   </div>
                  
                    <div class="col-sm-2">
                       <div class="input-group">
                        <input type="text" class="form-control" placeholder="mm/dd/yyyy" id="Text3" runat="server" name="startDate" required="required" autocomplete="off" />
                         <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                         </div>
                     </div>
              </div>  

                 <%--  --%>
               <div class="form-group whites" style="margin-top:10px;">
                  <label class="col-sm-1 control-label" style="font-weight:bold;">Phone:</label>
                    <div class="col-sm-2">
                      <div class="input-group">
                          <label class="col-sm-6 control-label">506873266743</label>
                     </div>
                   </div>
                  
                   <label class="col-sm-3 control-label"></label>
                   <label class="col-sm-1 control-label" style="font-weight:bold;">Email:</label>
                    <div class="col-sm-2">
                      <div class="input-group">
                          <label class="col-sm-6 control-label">player123@gmail.com</label>
                     </div>
                   </div>
              </div>                                 
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

           <div class="right_col" role="main">
            <div class="row">
              <div class="col-md-6 col-sm-6 col-xs-12 boxes">
                <div class="x_panel">
                      <div class="x_title">
                        <h3>Grouped</h3>
                      </div>
                  <div class="x_content" style="overflow-x:auto;">

                    <table class="table table-responsive table-hover" style="overflow-x:auto;">
                      <thead>
                        <tr>
                          <th>Date Range</th>
                          <th>Bets</th>
                          <th>Risk</th>
                          <th>Net</th>
                          <th>Hold%</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                          <th>Current Week</th>
                          <td>15</td>
                          <td>$1500</td>
                          <td>$1100</td>
                          <td>73.33%</td>
                        </tr>
                        <tr>
                          <th>Last Month</th>
                          <td>30</td>
                          <td>$15.000</td>
                          <td>$11.100</td>
                          <td>74%</td>
                        </tr>
                        <tr>
                          <th>Current Season</th>
                          <td>60</td>
                          <td>$150.000</td>
                          <td>$110.100</td>
                          <td>74%</td>
                        </tr>
                         <tr>
                          <th>Last Season</th>
                          <td>65</td>
                          <td>$150.000</td>
                          <td>$110.100</td>
                          <td>74%</td>
                        </tr>
                      </tbody>
                    </table>
                  </div>
                </div>
              </div>
                 <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title">
                    <h3>Balance</h3>
                  </div>
                   <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px;">
                     <div id="medio">
                       <div class="form-group" style="margin-top:43px; border-bottom:groove;">
                        <label class="col-sm-2 control-label">This Week:</label>
                         <div class="col-sm-4">
                           <div class="input-group">
                            <input type="text" class="form-control" style="background-color:white; font-weight:bold;" placeholder="$0" value="$1500" id="inThisWeek" runat="server" required="required" autocomplete="off" readonly="readonly" />
                           </div>
                        </div>

                        <label class="col-sm-2 control-label">Bet Type:</label>
                        <div class="col-sm-4">
                          <select ID="Select2" runat="server" class="form-control">
                            <option Selected="selected" Value="">All</option>
                            <option Value="STRAIGHT">Straight Bet</option>
                            <option Value="TEASER">Teaser</option>
                            <option Value="Parlay">Parlay</option>
                          </select>
                       </div> 
                     </div>
                        <div class="form-group" style="margin-top:30px; margin-bottom:40px; border-bottom:groove;">
                             <label class="col-sm-2 control-label">Last Week:</label>
                             <div class="col-sm-4">
                               <div class="input-group">
                                <input type="text" class="form-control" style="background-color:white; font-weight:bold;" placeholder="$0" id="Text1" value="$2500" runat="server" required="required" autocomplete="off" readonly="readonly" />
                               </div>
                            </div>
    
                           <label class="col-sm-2 control-label">Sport:</label>
                           <div class="col-sm-4">
                              <select ID="Select1" runat="server"  class="form-control">
                                <option Selected="selected" Value="">All</option>
                                <option Value="S0C">SOC</option>
                                <option Value="NFL">NFL</option>
                                <option Value="CFB">CFB</option>
                                <option Value="MLB">MLB</option>
                                <option Value="NBA">NBA</option>
                              </select>
                           </div>
                        </div>
                     </div>
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
                  <div class="x_title">
                    <h3>Financial Stats</h3>
                  </div>
                  <div class="x_content" style="height:100%; margin-bottom:10px;">
                      <div style="height:425px; width:100%;" id="chart1"></div>
                  </div>
                </div>
              </div>

              <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title">
                    <h3>Type Stats</h3>
                  </div>
                  <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px;">
                      <div style="height:425px; width:100%;" id="chart2"></div>
                  </div>
                </div>
              </div>
            </div>
        </div>
      </div>

        <div class="row">
                <div class="col-md-12 col-sm-6 col-xs-12 boxes" style="height:100%; width:100%; margin-top:10px;">
                  <div class="x_panel">
                   <div class="x_title">
                  <h3>Chart</h3>
                  </div>
                  <div class="x_content">
                     <div style="height:350px; width:100%; margin-bottom:10px; margin-right:10px;" id="leansChart"></div>
                 </div>
             </div>
           </div>
        </div>


       </div>
        <div id="menu2" class="tab-pane fade" style="width:100%;">
           <div class="form-group">
               <div class="col-sm-2">
                 <select ID="Select3" runat="server" class="form-control chosen-select">
                     <option Selected="selected" Value="">All Sports</option>
                        <option Value="S0C">SOC</option>
                        <option Value="NFL">NFL</option>
                        <option Value="CFB">CFB</option>
                        <option Value="MLB">MLB</option>
                        <option Value="NBA">NBA</option>
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
                  <div class="x_title">
                    <h4>Bet Stats</h4>
                  </div>
                  <div class="x_content" style="height:100%; overflow-x:auto;">
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
                   <h4>Pro Play</h4>
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
                          <th>Over all Scalp</th>
                          <td>60%</td>
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
                 <select ID="Select5" runat="server" class="form-control">
                     <option Selected="selected" Value="">Bets</option>
                  </select>
                </div>

                </div>
            
                  <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px;">
                      <div style="height:482px; width:100%;" id="betChart"></div>
                  </div>
                </div>
              </div>



              <div class="col-md-6 col-sm-6 col-xs-12 boxes">
                <div class="x_panel">
                      <div class="x_title">
                        <h4>Parlay/Teaser</h4>
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
                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                          <th>Parlay</th>
                          <td>15</td>
                          <td>$1500</td>
                          <td>$1100</td>
                          <td>73.33%</td>
                        </tr>
                        <tr>
                          <th>Teaser</th>
                          <td>30</td>
                          <td>$15.000</td>
                          <td>$11.100</td>
                          <td>74%</td>
                        </tr>
                      </tbody>
                    </table>
                  </div>
                </div>
              </div>


                 <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title">
                    <h4 class="negri">Solutions</h4>
                  </div>
                   <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px;">                
                     <div class="form-group">
                         <label class="col-sm-2 control-label">Solution:</label>
                         <div class="col-sm-4">
                           <select ID="Select6" runat="server" class="form-control">
                               <option Selected="selected" Value="">Item 1</option>
                               <option Value="">Item 2</option>
                               <option Value="">Item 3</option>
                               <option Value="">Item 4</option>
                           </select>
                        </div>
                     </div>

                       <label class="col-sm-2 control-label">Notes:</label>
                       <div class="col-sm-9">
                           <div class="input-group" style="width:100%; margin-bottom:10px;">
                            <textarea class="form-control" style="background-color:white; resize:none; overflow-y:auto; height:75px;" id="Text2" runat="server"></textarea>
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
                 <select ID="Select8" runat="server" class="form-control chosen-select">
                     <option Selected="selected" Value="">All Sports</option>
                        <option Value="S0C">SOC</option>
                        <option Value="NFL">NFL</option>
                        <option Value="CFB">CFB</option>
                        <option Value="MLB">MLB</option>
                        <option Value="NBA">NBA</option>
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
                 <select ID="Select7" runat="server" class="form-control chosen-select">
                     <option Selected="selected" Value="">All Sports</option>
                        <option Value="S0C">SOC</option>
                        <option Value="NFL">NFL</option>
                        <option Value="CFB">CFB</option>
                        <option Value="MLB">MLB</option>
                        <option Value="NBA">NBA</option>
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
