<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/menu.Master" CodeBehind="PlayerModule.aspx.cs" Inherits="G8_App.Views.PlayerModule" %>


<asp:Content runat="server" ID="head" ContentPlaceHolderID="head1">
  <script src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>

  <script>

    function DrawChart1()
        {
         var chart = new CanvasJS.Chart("chart1", {
	        theme: "light2", // "light1", "light2", "dark1", "dark2"
	        animationEnabled: true,
	        title: {
		        text: "Bet Type"
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
    function DrawChart2()
    {
         var chart = new CanvasJS.Chart("chart2", {
	        animationEnabled: true,
	        theme: "light2", // "light1", "light2", "dark1", "dark2"
	        title: {
		        text: "Bet Type"
	        },
	        axisY: {
		        title: "Count",
		        suffix: "%",
		        includeZero: false
	        },
	        axisX: {
		        title: "Types"
	        },
	        data: [{
		        type: "column",
		        yValueFormatString: "#,##0.0#\"%\"",
		        dataPoints: [
			        { label: "Straight", y: 40.1 },	
			        { label: "Parlay", y: 35.70 },	
                    { label: "Teaser", y: 30.00 },
                    { label: "Other", y: 25.00 }
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
		        text: "Bet Type"
	        },
	        axisY: {
		        title: "Count",
		        suffix: "%",
		        includeZero: false
	        },
	        axisX: {
		        title: "Types"
	        },
	        data: [{
		        type: "column",
		        yValueFormatString: "#,##0.0#\"%\"",
		        dataPoints: [
			        { label: "Straight", y: 40.1 },	
			        { label: "Parlay", y: 35.70 },	
                    { label: "Teaser", y: 30.00 },
                    { label: "Other", y: 25.00 }
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
		        text: "Site Traffic"
	        },
	        axisX:{
		        valueFormatString: "DD MMM",
		        crosshair: {
			        enabled: true,
		        }
	        },
	        axisY: {
		        title: "Number of Visits",
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
		        name: "Total Visit",
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
		        name: "Unique Visit",
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
		        text: "Bet Type"
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
		        text: "Bet Type"
	        },
	        axisY: {
		        title: "Count",
		        suffix: "%",
		        includeZero: false
	        },
	        axisX: {
		        title: "Types"
	        },
	        data: [{
		        type: "column",
		        yValueFormatString: "#,##0.0#\"%\"",
		        dataPoints: [
			        { label: "Straight", y: 40.1 },	
			        { label: "Parlay", y: 35.70 },	
                    { label: "Teaser", y: 30.00 },
                    { label: "Other", y: 25.00 }
		        ]
	        }]
         });
         chart.render();
      }

    function LoadChartsFinancial()
    {
        setTimeout(function () {
          DrawChart1();
            DrawChart2();
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
          }, 150);
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
              <div class="modal-header" style="width:100%;">
              <div class="form-group whites">
                  <div class="col-sm-6">
                      <h4 class="modal-title" id="idPlayer">Player: TM005</h4>
                  </div>
                  <div class="col-sm-6">
                      <h4 id="idCategory">Category: None</h4>
                  </div>
              </div>
             <%--  --%>

             <div class="form-group whites">
                  <div class="col-sm-6">
                      <h4 id="idAgent">Agent: VTML</h4>                    
                  </div>
                  <div class="col-sm-6">
                      <h4 id="idRegDate">Reg Date: 2018/06/03 - 2018/09/20</h4>
                  </div>
              </div>  

                 <%--  --%>
               <div class="form-group whites">
                  <div class="col-sm-6">
                      <h4 id="idPhone">Phone: +506 8563-6533</h4>                    
                  </div>
                  <div class="col-sm-6">
                      <h4 id="idEmail">Email: agent123@gmail.com</h4>
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
        <li ><a data-toggle="tab" href="#menu2" onclick="LoadChartsBets();" id="txtLeans" runat="server">Bets</a></li>
        <li><a data-toggle="tab" href="#menu3" onclick="LoadChartsToday();" id="txtWith" runat="server">Today's  Action</a></li>
      </ul>
    </div>
</div>

       <div class="tab-content">  
        <div id="financial" class="tab-pane active">
         <div class="row" style="margin-top:10px;">            
            <div class="form-group" style="margin-top:10px;">
                   <button class="btn btn-success" style="float:right; margin-right:2px;">Go</button>
                   <div class="col-sm-2" style="float:right;">
                      <select ID="inSport" runat="server"  class="form-control chosen-select2">
                        <option Selected="selected" Value="">Sports</option>
                        <option Value="S0C">SOC</option>
                        <option Value="NFL">NFL</option>
                        <option Value="CFB">CFB</option>
                        <option Value="MLB">MLB</option>
                        <option Value="NBA">NBA</option>
                      </select>
                   </div>

                   <div class="col-sm-2" style="float:right;">
                      <select ID="inBetType" runat="server"  class="form-control chosen-select2">
                        <option Selected="selected" Value="">Bet Type</option>
                        <option Value="STRAIGHT">Straight Bet</option>
                        <option Value="TEASER">Teaser</option>
                        <option Value="Parlay">Parlay</option>
                      </select>
                   </div>                 
                </div>
        </div>

           <div class="right_col" role="main">
            <div class="row">
              <div class="col-md-6 col-sm-6 col-xs-12 boxes">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Grouped</h2>
                  </div>
                  <div class="x_content" style="overflow-x:auto;">

                    <table class="table table-responsive table-hover" style="overflow-x:auto;">
                      <thead>
                        <tr>
                          <th>Date Range</th>
                          <th>Bets</th>
                          <th>Risk</th>
                          <th>Net</th>
                          <th>Hold</th>
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
                    <h2>Balance</h2>
                  </div>
                  <div class="x_content" style="height:100%;">

                    <table class="table table-responsive table-hover">
                      <thead>
                        <tr>
                          <th>Date Range</th>
                          <th>Balance</th>
                          <th>Hold%</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                          <td>This Week</td>
                          <td>$5000</td>
                          <td>50%</td>
                        </tr>
                        <tr>
                          <td>Last Week</td>
                          <td>$8000</td>
                          <td>47.5%</td>
                        </tr>
                        <tr>
                          <td>This Month</td>
                          <td>$10.000</td>
                          <td>35.9%</td>
                        </tr>
                        <tr>
                          <td>Last Month</td>
                          <td>$15.000</td>
                          <td>89.67%</td>
                        </tr>
                      </tbody>
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
                  <div class="x_title">
                    <h2>Financial Stats</h2>
                  </div>
                  <div class="x_content" style="height:100%; margin-bottom:10px;">
                      <div style="height:425px; width:100%;" id="chart1"></div>
                  </div>
                </div>
              </div>


              <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title">
                    <h2>Type Stats</h2>
                  </div>
                  <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px;">
                      <div style="height:425px; width:100%;" id="chart2"></div>
                  </div>
                </div>
              </div>
            </div>
        </div>
      </div>

       </div>
        <div id="menu2" class="tab-pane fade" style="width:100%;">
           <div class="right_col" role="main">
            <div class="row">
              <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title">
                    <h2>Stats</h2>
                  </div>
                  <div class="x_content" style="height:100%; overflow-x:auto;">
                   <table class="table table-responsive table-hover" style="overflow-x:auto;">
                      <thead>
                        <tr>
                          <th>Date Range</th>
                          <th>Bets</th>
                          <th>Risk</th>
                          <th>Net</th>
                          <th>Hold</th>
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
                      </tbody>
                    </table>                 
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
                          <td>15%</td>
                        </tr>
                        <tr>
                          <th>Beat Line</th>
                          <td>30%</td>
                        </tr>
                        <tr>
                          <th>Over all Scalp</th>
                          <td>60%</td>
                        </tr>
                         <tr>
                          <th>Syndicate</th>
                          <td>65%</td>
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
                  <div class="x_title">
                    <h2>Type Stats</h2>
                  </div>
                  <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px;">
                      <div style="height:416px; width:100%;" id="betChart"></div>
                  </div>
                </div>
              </div>

              <div class="col-md-12 col-sm-6 col-xs-12 boxes" style="height:100%; margin-top:10px;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title">
                    <h2>Line Chart</h2>
                  </div>
                  <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px;">
                      <div style="height:416px; width:100%;" id="lineChart"></div>
                  </div>
                </div>
              </div>

            </div>
        </div>
        </div>
         <div id="menu3" class="tab-pane fade">
           <div class="right_col" role="main">
            <div class="row">
              <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title">
                    <h2>Stats</h2>
                  </div>
                    <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px;">
                      <div style="height:416px; width:100%;" id="todayChart"></div>
                  </div>
                </div>
              </div>

              <div class="col-md-6 col-sm-6 col-xs-12 boxes" style="height:100%;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title">
                    <h2>Type Stats</h2>
                  </div>
                  <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px;">
                      <div style="height:416px; width:100%;" id="scalpingChart"></div>
                  </div>
                </div>
              </div>

              <div class="col-md-12 col-sm-6 col-xs-12 boxes" style="height:100%; margin-top:10px;">
                <div class="x_panel h-10 d-inline-block p-2">
                  <div class="x_title">
                    <h2>Leans</h2>
                  </div>
                  <div class="x_content" style="height:100%; width:100%; margin-right:10px; margin-bottom:10px; overflow-x:auto;">
                      
                    <table class="table table-responsive table-hover" style="overflow-x:auto;">
                      <thead>
                        <tr>
                          <th>Date Range</th>
                          <th>Bets</th>
                          <th>Risk</th>
                          <th>Net</th>
                          <th>Hold</th>
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

              <div class="col-md-12 col-sm-6 col-xs-12 boxes">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Chart</h2>
                  </div>
                  <div class="x_content" style="overflow-x:auto;">
                      <div style="height:416px; width:100%;" id="leansChart"></div>
                  </div>
                </div>
              </div>

            </div>
        </div>
         </div>
        </div>

      
    </div><!-- contentpanel -->
  </div><!-- mainpanel -->

</asp:Content>