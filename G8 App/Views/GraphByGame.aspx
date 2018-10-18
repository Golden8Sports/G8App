<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/menu.Master" CodeBehind="GraphByGame.aspx.cs" Inherits="G8_App.Views.GraphByGame" %>


<asp:Content runat="server" ContentPlaceHolderID="head1" ID="headASP">

        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
        <script type="text/javascript">
                
            function ActionAjax(idgame,period, event, idPeriod, eventId,gradded)
            {
                $("#idGame").text(idgame);
                $("#idPeriod").text(period);
                $("#idUserShow").text(event);
                $("#idPeriodShow").text("Period: " + period);
                $("#idPeriodId").text(idPeriod);
                $("#idEventID").text(eventId);
                $("#idGradded").text(gradded);
                DrawChart();
            }

   </script>


    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script src="../js/canvas.js"></script>
    <script type="text/javascript">
       google.charts.load('current', { 'packages': ['table'] });

       var interval;
       var lineType = "line";
       var isGradded = 1;

       function Hide(t)
       {
           obj = document.getElementById(t);
           obj.style.display = 'none';
       }
       function Show(t)
       {
           obj = document.getElementById(t);
           obj.style.display = 'block';
       }

       function DrawChart() {
          Pause();
          var Tool;
          var ToolPlayer;
          var Wager = document.getElementById("inType").value;

          isGradded = (document.getElementById("idGradded").innerHTML == "NO") ? 0 : 1;          
         
          if (document.getElementById("inBy").value == "PT") {
                    if (Wager == "DR" || Wager == "ML") {
                        Tool = "<strong>{name}</strong> </br>  {t}</br> <strong>{y}</strong>";
                        ToolPlayer = "<strong>{w}</strong> </br>   {t}</br> {r}</br> <strong>{y}</strong>";
                    } else {
                        Tool = "<strong>{name}</strong> </br>  {t}</br> <strong>{y}</strong> </br> {z[1]}";
                        ToolPlayer = "<strong>{w}</strong> </br>  {t}</br> {r}</br> <strong>{y}</strong> </br> {z[1]}";
                    }

                } else {
             if (Wager == "DR" || Wager == "ML") {
                        Tool = "<strong>{name}</strong> </br> {t}</br> <strong>{y}</strong>";
                        ToolPlayer = "<strong>{w}</strong> </br>  {t}</br> <strong>{y}</strong>";
                    } else {
                        Tool = "<strong>{name}</strong> </br>  {t}</br> <strong>{y}</strong> </br> {z[0]}";
                        ToolPlayer = "<strong>{w}</strong> </br>  {t}</br> <strong>{y}</strong> </br> {z[0]}";
                    }
          }

          var chart = new CanvasJS.Chart("divChart", {
                    animationEnabled: true,
                    zoomEnabled: true,
                    exportEnabled: true,
                    zoomType: 'xy',
                    theme: "light2",

                    axisX: {
                        crosshair: {
                            enabled: true
                        },
                        valueFormatString: "DD MMM",
                        labelFontSize: 12,
                    },

                    axisY: {
                        title: "Values",
                        suffix: "",
                        crosshair: {
                            enabled: true
                        },
                        includeZero: false,
                        labelFontSize: 12,
                    },

                    legend: {
                        cursor: "pointer",
                        itemclick: toggleDataSeries
                    },

                    data: [{   //pinnacle
                        type: lineType,
                        name: "Pinnacle",
                        color: "#C900FF",
                        markerSize: 4,
                        showInLegend: true,
                        toolTipContent: Tool,
                        dataPoints: []
                    },
                    {
                        type: lineType, //Jaz
                        name: "Jazz",
                        color: "#7E4721",
                        markerSize: 4,
                        showInLegend: true,
                        toolTipContent: Tool,
                        dataPoints: []
                    },
                    {
                        type: lineType, //PPH
                        name: "PPH",
                        color: "#17ce24",
                        markerSize: 4,
                        showInLegend: true,
                        toolTipContent: Tool,
                        dataPoints: []
                    },
                    {
                        type: lineType, //Cris
                        name: "Cris",
                        color: "#FF0000",
                        markerSize: 4,
                        showInLegend: true,
                        toolTipContent: Tool,
                        dataPoints: []
                    },
                    {
                        type: lineType, //Grande
                        name: "Grande",
                        color: "#FFF400",
                        markerSize: 4,
                        showInLegend: true,
                        toolTipContent: Tool,
                        dataPoints: []
                    },
                    {
                        type: lineType, // 5Dimes
                        name: "5Dimes",
                        color: "#0079CF",
                        markerSize: 4,
                        showInLegend: true,
                        toolTipContent: Tool,
                        dataPoints: []
                    },
                    {
                        type: lineType,
                        name: 'Bets',
                        color: "#000",
                        markerType: "triangle",
                        markerColor: "black",
                        markerSize: 13,
                        lineDashType: "dot",
                        showInLegend: true,
                        toolTipContent: ToolPlayer,
                        dataPoints: []
                    }]  //pinnacle
                });
          var flag = true;

          setTimeout(function () {
            updateChart();
          }, 250);
          

           function updateChart()
           {
                    var bet = 0;
                    var risk = 0;
                    if (flag == true) {
                        var obj;
                        var index = 0;
                        var type = document.getElementById("inType").value;
                        var by = document.getElementById("inBy").value;
                        var parameter = { 'idgame': $("#idGame").text(), 'type': $("#inType").val(), 'by': $("#inBy").val(), 'period': $("#idPeriod").text(), 'side': $("#inSide").val(), 'idPeriod': $("#idPeriodId").text(), 'idEvent': $("#idEventID").text() };

                        $.ajax({
                            type: 'POST',
                            url: 'GraphByGame.aspx/GetLines',
                            dataType: 'json',
                            data: JSON.stringify(parameter),
                            contentType: 'application/json; charset=utf-8',
                            async: true,
                            success: function (response) {
                                //alert(response.d);
                                Show('divChart');
                                obj = JSON.parse(response.d);
                                index = obj.length;
                                var dataPoints0 = [];// Pinnacle 37
                                var dataPoints1 = [];// Jazz 52
                                var dataPoints2 = [];// PPH 364
                                var dataPoints3 = [];// Cris 489
                                var dataPoints4 = [];// Grande 39
                                var dataPoints5 = [];// 5Dimes 92
                                var playerList = [];// Bets -1

                                for (var i = 0; i < index; i++) {
                                    if (obj[i]["Casino"] == 37 || obj[i]["Casino"] == '37') {
                                        if (type == 'ML' || type == 'DR') {
                                            dataPoints0.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"], obj[i]["Juice"]], t: obj[i]["Time"] });
                                            chart.options.data[0].legendText = "Pinnacle " + obj[i]["Juice"];
                                        }
                                        else {
                                            if (by == "OD") {
                                                dataPoints0.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"], obj[i]["Juice"]], t: obj[i]["Time"] });
                                                chart.options.data[0].legendText = "Pinnacle " + obj[i]["Juice"];
                                            } else {
                                                dataPoints0.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Line"], z: [obj[i]["Line"], obj[i]["Juice"]], t: obj[i]["Time"] });;
                                                chart.options.data[0].legendText = "Pinnacle " + obj[i]["Line"];
                                            }
                                        }
                                    }
                                    else if (obj[i]["Casino"] == 52 || obj[i]["Casino"] == '52') //
                                    {
                                        if (type == 'ML' || type == 'DR') {
                                            dataPoints1.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"], obj[i]["Juice"]], t: obj[i]["Time"] });
                                            chart.options.data[1].legendText = "Jazz " + obj[i]["Juice"];
                                        }
                                        else {
                                            if (by == "OD") {
                                                dataPoints1.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"], obj[i]["Juice"]], t: obj[i]["Time"] });
                                                chart.options.data[1].legendText = "Jazz " + obj[i]["Juice"];
                                            } else {
                                                dataPoints1.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Line"], z: [obj[i]["Line"], obj[i]["Juice"]], t: obj[i]["Time"] });
                                                chart.options.data[1].legendText = "Jazz " + obj[i]["Line"];
                                            }
                                        }
                                    }
                                    else if (obj[i]["Casino"] == 364 || obj[i]["Casino"] == '364') {
                                        if (type == 'ML' || type == 'DR') {
                                            dataPoints2.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"], obj[i]["Juice"]], t: obj[i]["Time"] });
                                            chart.options.data[2].legendText = "PPH " + obj[i]["Juice"];
                                        }
                                        else {
                                            if (by == "OD") {
                                                dataPoints2.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"], obj[i]["Juice"]], t: obj[i]["Time"] });
                                                chart.options.data[2].legendText = "PPH " + obj[i]["Juice"];
                                            } else {
                                                dataPoints2.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Line"], z: [obj[i]["Line"], obj[i]["Juice"]], t: obj[i]["Time"] });
                                                chart.options.data[2].legendText = "PPH " + obj[i]["Line"];
                                            }
                                        }
                                    }
                                    else if (obj[i]["Casino"] == 489 || obj[i]["Casino"] == '489') {
                                        if (type == 'ML' || type == 'DR') {
                                            dataPoints3.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"], obj[i]["Juice"]], t: obj[i]["Time"] });
                                            chart.options.data[3].legendText = "Cris " + obj[i]["Juice"];
                                        }
                                        else {
                                            if (by == "OD") {
                                                dataPoints3.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"], obj[i]["Juice"]], t: obj[i]["Time"] });
                                                chart.options.data[3].legendText = "Cris " + obj[i]["Juice"];
                                            } else {
                                                dataPoints3.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Line"], z: [obj[i]["Line"], obj[i]["Juice"]], t: obj[i]["Time"] });
                                                chart.options.data[3].legendText = "Cris " + obj[i]["Line"];
                                            }
                                        }
                                    }
                                    else if (obj[i]["Casino"] == 39 || obj[i]["Casino"] == '39') {
                                        if (type == 'ML' || type == 'DR') {
                                            dataPoints4.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"], obj[i]["Juice"]], t: obj[i]["Time"] });
                                            chart.options.data[4].legendText = "Grande " + obj[i]["Juice"];
                                        }
                                        else {
                                            if (by == "OD") {
                                                dataPoints4.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"], obj[i]["Juice"]], t: obj[i]["Time"] });
                                                chart.options.data[4].legendText = "Grande " + obj[i]["Juice"];
                                            } else {
                                                dataPoints4.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Line"], z: [obj[i]["Line"], obj[i]["Juice"]], t: obj[i]["Time"] });
                                                chart.options.data[4].legendText = "Grande " + obj[i]["Line"];
                                            }
                                        }
                                    }
                                    else if (obj[i]["Casino"] == 92 || obj[i]["Casino"] == '92') {
                                        if (type == 'ML' || type == 'DR') {
                                            dataPoints5.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"], obj[i]["Juice"]], t: obj[i]["Time"] });
                                            chart.options.data[5].legendText = "5 Dimes " + obj[i]["Juice"];
                                        }
                                        else {
                                            if (by == "OD") {
                                                dataPoints5.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"], obj[i]["Juice"]], t: obj[i]["Time"] });
                                                chart.options.data[5].legendText = "5 Dimes " + obj[i]["Juice"];
                                            } else {
                                                dataPoints5.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Line"], z: [obj[i]["Line"], obj[i]["Juice"]], t: obj[i]["Time"] });
                                                chart.options.data[5].legendText = "5 Dimes " + obj[i]["Line"];
                                            }
                                        }
                                    }
                                    else if (obj[i]["Casino"] == -1 || obj[i]["Casino"] == '-1') {
                                        bet = bet + 1;
                                        risk = risk + obj[i]["Risk"];

                                        if (type == 'ML' || type == 'DR') {
                                            playerList.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"], obj[i]["Juice"]], w: obj[i]["Player"], t: obj[i]["Time"], r: '$' + obj[i]["Risk"] });
                                        }
                                        else {
                                            if (by == "OD") {
                                                playerList.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"], obj[i]["Juice"]], w: obj[i]["Player"], t: obj[i]["Time"], r: '$' + obj[i]["Risk"] });
                                            } else {
                                                playerList.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Line"], z: [obj[i]["Line"], obj[i]["Juice"]], w: obj[i]["Player"], t: obj[i]["Time"], r: '$' + obj[i]["Risk"] });
                                            }
                                        }

                                        chart.options.data[6].legendText = "Bets " + bet;
                                    }

                                    document.getElementById("idBets").innerHTML = "Risk Amount: $" + risk;
                                }

                                chart.options.data[0].dataPoints = dataPoints0;
                                chart.options.data[1].dataPoints = dataPoints1;
                                chart.options.data[2].dataPoints = dataPoints2;
                                chart.options.data[3].dataPoints = dataPoints3;
                                chart.options.data[4].dataPoints = dataPoints4;
                                chart.options.data[5].dataPoints = dataPoints5;
                                chart.options.data[6].dataPoints = playerList;
                                chart.render();

                            },
                            error: function (response) {
                                alert('No Data: ' + response.statusText);
                                Hide('divChart');
                                Pause();
                                //error.Show;
                            }
                        });
                    }
           }

           if (isGradded == 0)
           {
               interval = setInterval(updateChart, 4000);
           }       
       }

       function toggleDataSeries(e) {
	       if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
               e.dataSeries.visible = false;
               e.chart.data.visible = false;
	       } else {
               e.dataSeries.visible = true;
               e.chart.data.visible = true;
	       }
	       e.chart.render();
       }

       function changeLine(txt)
       {
            lineType = txt;
            DrawChart();
       }

       function Pause()
       {
            clearInterval(interval);
       }

       function GameStatsJava(event, id,score,period)
       {
           document.getElementById("modal2Title").innerHTML = event;
           document.getElementById("h4Score").innerHTML = "Score: " + score;
           document.getElementById("h4Period").innerHTML = "Period: " + period;

               var parameter = { 'idgame': id };
               $.ajax({
                   type: 'POST',
                   url: 'GraphByGame.aspx/GetStats',
                   data: JSON.stringify(parameter),
                   contentType: 'application/json; charset=utf-8',
                   dataType: 'json',
                   success: function (data) {
                       Show('tableChart');
                       DrawStatsChart(data.d);
                   },
                   error: function (data) {
                       alert('No Data' + data.statusText);
                       Hide('tableChart');
                   }
               });
       }

   
        function DrawStatsChart(info)
        {
           var data = new google.visualization.DataTable();
           data.addColumn('string', 'Wager Type');
           data.addColumn('string', 'Wager Play');          
           data.addColumn('number', 'Risk Amount');
           data.addColumn('number', 'Net');

           var obj = JSON.parse(info);
           var index = obj.length;
         
           data.addRows(index);
           for (i = 0; i < index; i++)
           { 
             data.setCell(i, 0, obj[i]["WagerType"]);
             data.setCell(i, 1, obj[i]["WagerPlay"]);
             data.setCell(i, 2, obj[i]["Risk"]);
             data.setCell(i, 3, obj[i]["Net"]);           
           }
            var table = new google.visualization.Table(document.getElementById('tableChart'));
            var formatter = new google.visualization.ArrowFormat();
            formatter.format(data, 3);
           table.draw(data, { showRowNumber: false, width: '100%', height: '100%' });
        }

    </script>
</asp:Content>




<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Content1">
  
    <%-- BEGIN OPEN MODAL WINDOW  --%>

     <%-- Modal Window --%>
 <div class="container" style="width:100%;" id="container">
  <div class="modal fade" id="myModal" style="width:100%;" role="dialog">
    <div class="modal-dialog modal-lg" style="width:100%; margin:2px;">
      <div class="modal-content" style="width:100%;">
        <div class="modal-header" style="width:100%;">
          <button type="button" onclick="Pause();" class="close" data-dismiss="modal">&times;</button>     
          <h4 class="modal-title" id="idUserShow">User</h4>  
          <h4 id="idPeriod" style="display:none;">Period</h4>
          <h5 id="idPeriodShow">Period</h5>
          <h4 id="idGame" style="display:none;">Game</h4>
          <h4 id="idWager" style="display:none;">Game</h4>
          <h4 id="idPeriodId" style="display:none;">Game</h4>
          <h4 id="idEventID" style="display:none;"></h4>
          <h4 id="idGradded" style="display:none;"></h4>
          <h5 id="idBets">Bets</h5>
        </div>
          <div class="form-group" style="margin-top:20px;">            
             <label class="col-sm-1 control-label">Type:</label>
             <div class="col-sm-2">
                 <select class="form-control chosen-select" id="inType" style="margin-top:20px; margin-bottom:20px;">
                   <option selected="selected" value="ML"> Money Line</option>
                   <option value="SP">Spread</option>
                   <option value="TOT">Total</option>
                   <option value="DR">Draw</option>
                 </select>
              </div>

              
              <label class="col-sm-1 control-label">By:</label>
              <div class="col-sm-2"> 
              <select class="form-control chosen-select" id="inBy" style="margin-top:20px; margin-bottom:20px;">
                   <option selected="selected" value="PT">Points</option>
                   <option value="OD">Odds</option>
               </select>
              </div>

              <label class="col-sm-1 control-label">Side:</label>
              <div class="col-sm-2"> 
              <select class="form-control chosen-select" id="inSide" style="margin-top:20px; margin-bottom:20px;">
                   <option selected="selected" value="V">Visitor/Total Over</option>
                   <option value="H">Home/Total Under</option>
               </select>
              </div>

             <div class="col-sm-1">
               <button class="btn btn-block btn-success" onclick="DrawChart();">Go</button>
             </div>

          </div>
          <div style="float: left; margin-left: 4px; margin-bottom: 4px;" class="form-group">              
              <button onclick="changeLine('spline');" class="btn btn-white fa fa-area-chart" data-toggle="tooltip" title="Spline Chart"></button>
              <button onclick="changeLine('line');" class="btn btn-white fa fa-line-chart" data-toggle="tooltip" title="Line Chart"></button>
              <button onclick="changeLine('stepLine');" class="btn btn-white fa fa-tasks" data-toggle="tooltip" title="Step Chart"></button>
          </div>
        <div class="scrollable">
            <div id="divChart" style="height:650px; width:100%; margin-top:50px;"></div>
        </div>                    
        <div class="modal-footer form-group">
          <button type="button" class="btn btn-default" onclick="Pause();" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
</div>
<%-- End Modal Window --%>  



 
 <div class="container">
  <!-- Modal -->
  <div class="modal fade" id="myModal2" role="dialog">
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title" id="modal2Title">Game Stats</h4>
          <h4 id="h4Score">Score</h4>
          <h4 id="h4Period">Score</h4>
        </div>
        <div class="modal-body">
          <div id="tableChart" style="width:100%;"></div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
</div>

    <div class="mainpanel">    
    <div class="headerbar">      
      <a class="menutoggle"><i class="fa fa-bars"></i></a>      
      <div class="header-right">     
      </div><!-- header-right -->      
    </div><!-- headerbar -->
    <div class="pageheader">
      <h2><i class="fa fa-home"></i> Graph by Game <span>Subtitle goes here...</span></h2>
      <div class="breadcrumb-wrapper">
        <ol class="breadcrumb">
          <li class="active" runat="server" id="userName"></li>
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
                  <h3 class="panel-title">Filters</h3>
                </div>

                <div class="panel-body">
                  <div class="row">
                    <div class="col-xs-12">			  

                  <form class="form-horizontal form-bordered" method="post" runat="server">                     
                        <div class="form-group">
                        	<label class="col-sm-2 control-label">Sport:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inSport" runat="server" AutoPostBack="true"  CssClass="form-control chosen-select" OnSelectedIndexChanged="FillLeague">
                                </asp:DropDownList>
                            </div>
                            <label class="col-sm-2 control-label">League:</label>
                            <div class="col-sm-3">
                              <asp:DropDownList ID="inLeague" CssClass="form-control chosen-select" AutoPostBack="false" runat="server">
                              <asp:ListItem Value="-1" >ALL</asp:ListItem>
                              </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">

                          <label class="col-sm-2 control-label">Start Date:</label>
                            <div class="col-sm-3">
                              <div class="input-group">
                                <input type="text" class="form-control" placeholder="mm/dd/yyyy" id="startDate" runat="server" value="" name="startDate" required="required" autocomplete="off" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                              </div>
                            </div>

                            <label class="col-sm-2 control-label">End Date:</label>
                            <div class="col-sm-3">
                              <div class="input-group">
                                 <input type="text" class="form-control" placeholder="mm/dd/yyyy" id="endDate" runat="server" value="" name="endDate" required="required" autocomplete="off" />
                                 <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                              </div>
                            </div>
                        </div>
                          
                        <div class="form-group">
                            <div id="lbPass" style="display:none;"></div>
                            <div id="lbType" style="display:none;"></div>
                            <label class="col-sm-8 control-label"></label>
                            <div class="col-sm-2">
                                <asp:Button class="btn btn-block btn-success" runat="server" type="submit" Text="Run" OnClick="LoadGames"/>
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
                  <h3 class="panel-title" runat="server" id="resulTitle">Result</h3>
                </div>


                <div class="panel-body">
                          
                         <div class="table-responsive" style="overflow-x:auto;">
                           <table class="table table-hover" id="table1" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th>Event</th>
                                             <th>Date</th>
                                             <th>Rot</th>
                                             <th>Sport</th>
                                             <th>League</th>
                                             <th>Period</th>
                                             <th>Bets</th>
                                             <th>Risk</th>
                                             <th>Chart</th>
                                             <th>Stats</th>
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptTable">
                                      <ItemTemplate>
                                            <tr>
                                            <td ><%# Eval("EventNameWithId") %></td>
                                            <td ><%# Eval("GameDate") %></td>
                                            <td ><%# Eval("VisitorNumber") %></td>
                                            <td ><%# Eval("IdSportDGS") %></td>
                                            <td ><%# Eval("LeagueName") %></td>
                                            <td ><%# Eval("GamePeriod") %></td>
                                            <td ><%# Eval("BETS") %></td>
                                            <td ><%# Eval("RISK") %></td>
                                            <td ><button class="btn btn-block btn-primary fa fa-line-chart" data-toggle="modal" data-target="#myModal"  onclick="ActionAjax('<%# Eval("IdGame") %>','<%# Eval("GamePeriod") %>','<%# Eval("EventName") %>','<%# Eval("Period") %>','<%# Eval("EventId") %>','<%# Eval("GraddedDate") %>');"></button></td>
                                            <td><button class="btn btn-block btn-danger fa fa-bar-chart" data-toggle="modal" data-target="#myModal2" onclick="GameStatsJava('<%# Eval("EventName") %>', '<%# Eval("IdGame") %>','<%# Eval("Score") %>','<%# Eval("GamePeriod") %>');"></button></td>
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