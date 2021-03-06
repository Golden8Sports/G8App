﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TopPlayers.aspx.cs" MasterPageFile="~/Views/menu.Master" Inherits="G8_App.Views.TopPlayers" %>


<asp:Content runat="server" ID="head" ContentPlaceHolderID="head1">

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <script type="text/javascript">

        function DrawTableAJX(player)
        {
            $("#idPlayer").text(player + " Stats");
            var parameter = { 'player': player }
            $.ajax({                    
                type: 'POST',
                url: 'TopPlayers.aspx/GetBreakDownPlayer',
                data: JSON.stringify(parameter),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data)
                {
                    Show('tablePlayers');
                    DrawChart(data.d);                  
                },
                error: function (data)
                {
                    alert('No Data' + data.statusText);
                    Hide('tablePlayers');
                }
            });
        }

    </script>


    <script type="text/javascript" src="../js/GoogleCharts.js"></script>
    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['table'] });

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

        function DrawChart(info)
        {
           var data = new google.visualization.DataTable();
           data.addColumn('string', 'Sport');
           data.addColumn('string', 'Wager Type');
           data.addColumn('string', 'Period');
           data.addColumn('string', 'Wager Play');
           data.addColumn('number', 'Risk Amount');
           data.addColumn('number', 'Net');
           data.addColumn('number', 'Hold%');

           var obj = JSON.parse(info);
           var index = obj.length;
         
           data.addRows(index);
           for (i = 0; i < index; i++)
           { 
             data.setCell(i, 0, obj[i]["Sport"]);
             data.setCell(i, 1, obj[i]["WagerType"]);
             data.setCell(i, 2, obj[i]["GamePeriod"]);
             data.setCell(i, 3, obj[i]["WagerPlay"]);
             data.setCell(i, 4, obj[i]["RiskAmount"]);
             data.setCell(i, 5, obj[i]["Net"]);
             data.setCell(i, 6, obj[i]["HoldPercentaje"]);
           }
            var table = new google.visualization.Table(document.getElementById('tablePlayers'));
            var formatter = new google.visualization.ArrowFormat();
            formatter.format(data, 5); 
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
      <h2><i class="fa fa-user"></i> Top Players <span>Subtitle goes here...</span></h2>
      <div class="breadcrumb-wrapper">
        <ol class="breadcrumb">
          <li class="active" runat="server" id="userName"></li>
        </ol>
      </div>      
    </div>

      <div class="container">
  <!-- Modal -->
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title" id="idPlayer">Player Stats</h4>
        </div>
        <div class="modal-body">
          <div id="tablePlayers" style="width:100%;"></div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
      
    </div>
  </div>
  
</div>



    

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
                                <asp:ListItem Value="visitormoney">Visitor Money Line</asp:ListItem>
                                <asp:ListItem Value="homemoney">Home Money Line</asp:ListItem>
                                <asp:ListItem Value="visitorspread">Visitor Spread</asp:ListItem>
                                <asp:ListItem Value="homespread">Home Spread</asp:ListItem>
                                <asp:ListItem Value="totalover">Total Over</asp:ListItem>
                                <asp:ListItem Value="totalunder">Total Under</asp:ListItem>
                                <asp:ListItem Value="draw">Draw</asp:ListItem>
                              </asp:DropDownList>
                          </div>

                            <label class="col-sm-2 control-label">Bet Type</label>
                            <div class="col-sm-3">
                              <asp:DropDownList ID="inBetType" runat="server"  CssClass="form-control chosen-select">
                                <asp:ListItem Selected="True" Value="">ALL</asp:ListItem>
                                <asp:ListItem Value="STRAIGHT">STRAIGHT BET</asp:ListItem>
                                <asp:ListItem Value="PARLAY">PARLAY</asp:ListItem>
                                <asp:ListItem Value="TEASER">TEASER</asp:ListItem>
                                <%--<asp:ListItem Value="OTHER">OTHER</asp:ListItem>--%>
                              </asp:DropDownList>
                          </div> 

                        </div>



                        <div class="form-group">
                            <label class="col-sm-8 control-label"></label>
                            <div class="col-sm-2">
                                <asp:Button class="btn btn-block btn-success" runat="server" type="submit" Text="Run" OnClick="LoadTopPlayers"/>
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
                           <table class="table customTable" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th>Agent</th>
                                             <th>Player</th>
                                             <th>Risk</th>
                                             <th>Net</th>
                                             <th>Hold%</th>
                                             <th>Bets</th>
                                             <th>Wins</th>
                                             <th>Win%</th>                       
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptTable">
                                      <ItemTemplate>
                                          <tr>
                                            <td ><%# Eval("AGENT") %></td>
                                            <td ><button class="btn btn-info" data-toggle="modal" data-target="#myModal" onclick="DrawTableAJX('<%# Eval("PLAYER") %>');"><%# Eval("PLAYER") %></button></td>
                                            <td ><%# Eval("RiskAmount") %></td>
                                            <td ><%# Eval("NET") %></td>
                                            <td ><%# Eval("HoldPercentaje") %></td>
                                            <td ><%# Eval("BETS") %></td>
                                            <td ><%# Eval("WINS") %></td>
                                            <td ><%# Eval("WinPercentaje") %></td>
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