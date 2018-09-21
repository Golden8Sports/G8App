<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/menu.Master" CodeBehind="BusinessIntelligence.aspx.cs" Inherits="G8_App.Views.BusinessIntelligence" %>

<asp:Content runat="server" ContentPlaceHolderID="head1" ID="h1">
    
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    
    <script src="https://code.jquery.com/jquery-1.10.2.js"></script>
   
    <script type="text/javascript">

        function Before(game,user,period,idWager,play)
        {
            $("#idGame").text(game);
            $("#idUSer").text(user);
            $("#idPeriod").text(period);
            $("#idUserShow").text("Player: " + user);
            $("#idPeriodShow").text("Period: " + period);
            $("#idWager").text(idWager);
            $("#idModal").text("Wager Play: " + play);            
            ShowChartAJX();
        }

        function ShowChartAJX()
        {
            var parameter = { 'idgame': $("#idGame").text(), 'user': $("#idUSer").text(), 'type': $("#inType").val(), 'by': $("#inBy").val(), 'period': $("#idPeriod").text(), 'side': $("#inSide").val(),'wager': $("#idWager").text(), 'userplay': $("#idModal").text()};

              $.ajax({                    
                type: 'POST',
                url: 'BusinessIntelligence.aspx/GetLines',
                data: JSON.stringify(parameter),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data)
                {
                    Show('divChart');
                    DrawChart(data.d);                  
                },
                error: function (data)
                {
                    alert('No Data' + data.statusText);
                    Hide('divChart');
                }
              });
        }



    </script>


    
    <script src="../js/canvas.js"></script>
    <script type="text/javascript">

        var lineType = "line";

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



      function DrawChart(data)
      {
        var obj = JSON.parse(data);
        var index = obj.length;
        var type = document.getElementById("inType").value;
        var by = document.getElementById("inBy").value;
        var player = document.getElementById("idUSer").innerHTML;

        var Tool;
        var ToolPlayer;

           if (by == "PT")
           {
               if (type == "DR" || type == "ML")
               {
                 Tool = "<strong>{name}</strong> </br>  {x}</br> {t}</br> <strong>{y}</strong>";
                 ToolPlayer = "<strong>{name}</strong> </br>  {x}</br> {t}</br> <strong>{y}</strong>";
               } else
               {
                  Tool = "<strong>{name}</strong> </br>  {x}</br> {t}</br> <strong>{y}</strong> </br> {z[1]}";
                  ToolPlayer = "<strong>{name}</strong> </br>  {x}</br> {t}</br> <strong>{y}</strong> </br> {z[1]}";
               }           
           } else
           {
               if (type == "DR" || type == "ML")
               {
                 Tool = "<strong>{name}</strong> </br>  {x}</br> {t}</br> <strong>{y}</strong>";
                 ToolPlayer = "<strong>{w}</strong> </br>  {x}</br> {t}</br> <strong>{y}</strong>";
               } else
               {
                  Tool = "<strong>{name}</strong> </br>  {x}</br> {t}</br> <strong>{y}</strong> </br> {z[0]}";
                  ToolPlayer = "<strong>{w}</strong> </br>  {x}</br> {t}</br> <strong>{y}</strong> </br> {z[0]}";
               }
           }

        
	    var chart = new CanvasJS.Chart("divChart", {
            animationEnabled: true,
            zoomEnabled: true,
            exportEnabled: true,
            zoomType: 'xy',
            theme: "light2",
            title: {
                text: "Games Lines per Casino",
                fontSize: 20,
            },
            axisX:{
		        crosshair: {
			        enabled: true
                },
                valueFormatString: "hh:mm:ss TT",
                labelFontSize:12,
	        },
	        axisY:{
                suffix: "",
                crosshair: {
			        enabled: true
                },
                includeZero: false,
                labelFontSize:12,
	        },
	        legend: {
		        cursor: "pointer",
		        itemclick: toggleDataSeries
	        },
	        data: [{
		        type: lineType,
		        name: "Pinnacle",
                color: "#C900FF",
                markerSize:4,
		        showInLegend: true,
                axisYIndex: 0,
                toolTipContent: Tool,
                dataPoints: []               
	        },
	        {
		        type: lineType,
		        name: "Jazz",
		        color: "#7E4721",
                markerSize:4,
                showInLegend: true,
                toolTipContent: Tool,
		        dataPoints: []
	        },
	        {
		        type: lineType,
		        name: "PPH",
                color: "#17ce24",
                markerSize:4,
                showInLegend: true,
                toolTipContent: Tool,
		        dataPoints: []
	        },
            {
		        type: lineType,
		        name: "Cris",
                color: "#FF0000",
                markerSize:4,
                showInLegend: true,
                toolTipContent: Tool,
		        dataPoints: []
	        },
            {
		        type: lineType,
		        name: "Grande",
                color: "#FFF400",
                markerSize:4,
                showInLegend: true,
                toolTipContent: Tool,
		        dataPoints: []
	        },
            {
		        type: lineType,
		        name: "5Dimes",
                color: "#0079CF",
                markerSize:4,
                showInLegend: true,
                toolTipContent: Tool,
		        dataPoints: []
	        },
            {
                type: lineType,
                name: player,
                color: "#000",
                markerType: "triangle",
                markerColor: "black",
                markerSize: 13,
                showInLegend: true,
                toolTipContent: ToolPlayer,
		        dataPoints: []
	        }]
          });

          var dataPoints0 = [];// Jazz
          var dataPoints1 = [];// Pinnacle
          var dataPoints2 = [];// PPH
          var dataPoints3 = [];// Grande
          var dataPoints4 = [];// 5Dimes
          var dataPoints5 = [];// Cris
          var playerList = [];// Cris

          for (var i = 0; i < index; i++)
          {
              if (obj[i]["Casino"] == 52 || obj[i]["Casino"] == '52')
              {
                  if (type == 'ML' || type == 'DR')
                  {
                      dataPoints1.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                  }
                  else
                  {
                      if (by == "OD")
                      {
                          dataPoints1.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                      } else
                      {
                          dataPoints1.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Line"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                      }
                  }
              }
              else if (obj[i]["Casino"] == 37 || obj[i]["Casino"] == '37')
              {
                  if (type == 'ML' || type == 'DR')
                  {
                      dataPoints0.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                  }
                  else
                  {
                      if (by == "OD")
                      {
                          dataPoints0.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                      } else
                      {
                          dataPoints0.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Line"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                      }
                  }
              }
              else if (obj[i]["Casino"] == 364 || obj[i]["Casino"] == '364')
              {
                  if (type == 'ML' || type == 'DR')
                  {
                      dataPoints2.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                  }
                  else
                  {
                      if (by == "OD")
                      {
                          dataPoints2.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                      } else
                      {
                          dataPoints2.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Line"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                      }
                  }
              }
              else if (obj[i]["Casino"] == 489 || obj[i]["Casino"] == '489')
              {
                  if (type == 'ML' || type == 'DR')
                  {
                      dataPoints3.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                  }
                  else
                  {
                      if (by == "OD")
                      {
                          dataPoints3.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                      } else
                      {
                          dataPoints3.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Line"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                      }
                  }
              }
              else if (obj[i]["Casino"] == 39 || obj[i]["Casino"] == '39')
              {
                  if (type == 'ML' || type == 'DR')
                  {
                      dataPoints4.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                  }
                  else
                  {
                      if (by == "OD")
                      {
                          dataPoints4.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                      } else {
                          dataPoints4.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Line"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                      }
                  }
              }
              else if (obj[i]["Casino"] == 92 || obj[i]["Casino"] == '92')
              {
                  if (type == 'ML' || type == 'DR') {
                      dataPoints5.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                  }
                  else
                  {
                      if (by == "OD")
                      {
                          dataPoints5.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                      } else {
                          dataPoints5.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Line"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                      }
                  }
              }
              else if (obj[i]["Casino"] == -1 || obj[i]["Casino"] == '-1')
              {
                  if (type == 'ML' || type == 'DR')
                  {
                      playerList.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                  }
                  else
                  {
                      if (by == "OD")
                      {
                          playerList.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Juice"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                      } else
                      {
                          playerList.push({ x: new Date(obj[i]["Year"], obj[i]["Month"], obj[i]["Day"], obj[i]["Hour"], obj[i]["Minute"], obj[i]["Second"]), y: obj[i]["Line"], z: [obj[i]["Line"],obj[i]["Juice"]], t: obj[i]["Time"]});
                      }
                  }
              }
          }

          chart.options.data[0].dataPoints = dataPoints0;
          chart.options.data[1].dataPoints = dataPoints1;
          chart.options.data[2].dataPoints = dataPoints2;
          chart.options.data[3].dataPoints = dataPoints3;
          chart.options.data[4].dataPoints = dataPoints4;
          chart.options.data[5].dataPoints = dataPoints5;
          chart.options.data[6].dataPoints = playerList;
          

          chart.render();
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
            ShowChartAJX();
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
      <h2><i class="fa fa-briefcase"></i> Business Intelligence <span>Subtitle goes here...</span></h2>
      <div class="breadcrumb-wrapper">
        <ol class="breadcrumb">
          <li class="active" runat="server" id="userName"></li>
        </ol>
      </div>      
    </div>

  
 <%-- Modal Window --%>
 <div class="container" style="width:auto; height:100%;" >
  <div class="modal fade" id="myModal" style="width:100%;" role="dialog">
    <div class="modal-dialog modal-lg" style="width:100%; margin:2px;">
      <div class="modal-content" style="width:100%;">
        <div class="modal-header" style="width:100%;">
          <button type="button" class="close" data-dismiss="modal">&times;</button>     
          <h4 class="modal-title" id="idUserShow">User</h4>
          <h4 id="idUSer" style="display:none;">User</h4>
          <h5 id="idModal">Wager Play</h5>      
          <h4 id="idPeriod" style="display:none;">Period</h4>
          <h5 id="idPeriodShow">Period</h5>
          <h4 id="idGame" style="display:none;">Game</h4>
          <h4 id="idWager" style="display:none;">Game</h4>
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
               <button class="btn btn-block btn-success" onclick="ShowChartAJX();">Go</button>
             </div>
          </div>
          <div style="float: left; margin-left: 4px; margin-bottom: 4px;" class="form-group">              
              <button onclick="changeLine('spline');" class="btn btn-white fa fa-area-chart" data-toggle="tooltip" title="Spline Chart"></button>
              <button onclick="changeLine('line');" class="btn btn-white fa fa-line-chart" data-toggle="tooltip" title="Line Chart"></button>
              <button onclick="changeLine('stepLine');" class="btn btn-white fa fa-tasks" data-toggle="tooltip" title="Step Chart"></button>
          </div>
           <div id="divChart" style="height:600px; width:100%; margin-top:50px;"></div>              
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

                  <form class="form-horizontal form-bordered" method="post" runat="server">
                      <div class="form-group">
                            <label class="col-sm-2 control-label">Sport:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inSport" runat="server"  CssClass="form-control chosen-select" AutoPostBack="true" OnSelectedIndexChanged="FillLeague">
                                </asp:DropDownList>
                            </div>

                            <label class="col-sm-2 control-label">League:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inLeague" runat="server"  CssClass="form-control chosen-select">
                                <asp:ListItem Selected="True" Value="">ALL</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                      </div>


                           <div class="form-group">
                            <label class="col-sm-2 control-label">Agent:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inAgent" runat="server"  CssClass="form-control chosen-select" AutoPostBack="true" OnSelectedIndexChanged="LoadPlayers">
                                </asp:DropDownList>
                            </div>

                            <label class="col-sm-2 control-label">Player:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inPlayer" runat="server"  CssClass="form-control chosen-select">
                                <asp:ListItem Selected="True" Value="-1">ALL</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                      </div>
                      
                      <div class="form-group">
                          <label class="col-sm-2 control-label">Start Date:</label>
                            <div class="col-sm-3">
                              <div class="input-group">
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
                          <label class="col-sm-2 control-label">Wager Play</label>
                          <div class="col-sm-3">
                              <asp:DropDownList ID="inWagerPlay" runat="server"  CssClass="form-control chosen-select">
                                <asp:ListItem Selected="True" Value="">ALL</asp:ListItem>
                                <asp:ListItem Value="money">MONEY LINE</asp:ListItem>
                                <asp:ListItem Value="spread">SPREAD</asp:ListItem>
                                <asp:ListItem Value="total">TOTAL</asp:ListItem>
                                <asp:ListItem Value="draw">DRAW</asp:ListItem>
                              </asp:DropDownList>
                          </div>

                            <label class="col-sm-2 control-label">Bet Type</label>
                            <div class="col-sm-3">
                              <asp:DropDownList ID="inBetType" runat="server"  CssClass="form-control chosen-select">
                                <asp:ListItem Selected="True" Value="">ALL</asp:ListItem>
                                <asp:ListItem Value="STRAIGHT">STRAIGHT BET</asp:ListItem>
                                <asp:ListItem Value="PARLAY">PARLAY</asp:ListItem>
                                <asp:ListItem Value="TEASER">TEASER</asp:ListItem>
                                <asp:ListItem Value="OTHER">OTHER</asp:ListItem>
                              </asp:DropDownList>
                          </div> 

                        </div>



                        <div class="form-group">
                            <label class="col-sm-8 control-label"></label>
                            <div class="col-sm-2">
                                <asp:Button class="btn btn-block btn-success" runat="server" type="submit" Text="Run" OnClick="LoadBets"/>
                            </div>
                        </div>
                  </form>    
                </div>
              </div>
          </div>
        </div>
      </div>


    <!-- here  must be the table with the information about bets -->
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
                           <table class="table" id="table1" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th>Wager</th>
                                             <th>Agent</th>
                                             <th>Player</th>
                                             <th>Line Type</th>
                                             <th>Win</th>
                                             <th>Risk</th>
                                             <th>Result</th>
                                             <th>Net</th>
                                             <th>Period</th>
                                             <th>League</th>
                                             <th>Wager Type</th>
                                             <th>Bet Description</th>
                                             <th>Team</th>
                                             <th>Fav Dog</th>
                                             <th>Wager Play</th>
                                             <th>Sport</th>
                                             <th>Settled Date</th>
                                             <th>Placed Date</th>
                                             <th>Line</th>
                                             <th>Juice</th>
                                             <th>Visitor Score</th>
                                             <th>Home Score</th>                        
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptTable">
                                      <ItemTemplate>
                                          <tr>
                                            <td ><%# Eval("IdWager") %></td>
                                            <td ><%# Eval("Agent") %></td>
                                            <td ><%# Eval("Player") %></td>
                                            <td ><%# Eval("LineTypeName") %></td>
                                            <td ><%# Eval("WinAmount") %></td>
                                            <td ><%# Eval("RiskAmount") %></td>
                                            <td ><button class="btn btn-success" data-toggle="modal" data-target="#myModal" onclick="Before('<%#Eval("IdGame")%>','<%#Eval("Player")%>','<%#Eval("GamePeriod")%>','<%#Eval("IdWager")%>','<%#Eval("WagerPlay")%>');"><%#Eval("Result")%></button></td>
                                            <td ><%# Eval("Net") %></td>
                                            <td ><%# Eval("GamePeriod") %></td>
                                            <td ><%# Eval("League") %></td>
                                            <td ><%# Eval("CompleteDescription") %></td>
                                            <td ><%# Eval("DetailDescription") %></td>
                                            <td ><%# Eval("Team") %></td>
                                            <td ><%# Eval("FAV_DOG") %></td>
                                            <td ><%# Eval("WagerPlay") %></td>
                                            <td ><%# Eval("IdSport") %></td>
                                            <td ><%# Eval("SettledDate") %></td>
                                            <td ><%# Eval("PlacedDate") %></td>
                                            <td ><%# Eval("Points") %></td>
                                            <td ><%# Eval("Odds") %></td>
                                            <td ><%# Eval("VisitorScore") %></td>
                                            <td ><%# Eval("HomeScore") %></td>
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
