<%@ Page Language="C#" MasterPageFile="~/Views/menu.Master" AutoEventWireup="true" CodeBehind="leansReport.aspx.cs" Inherits="G8_App.Views.leansReport" %>


<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Content1">
       <div class="mainpanel">
    
    <div class="headerbar">
      
      <a class="menutoggle"><i class="fa fa-bars"></i></a>
      
      <div class="header-right">
      
      </div><!-- header-right -->
      
    </div><!-- headerbar -->
    
    <div class="pageheader">
      <h2><i class="fa fa-joomla"></i> Leans Report <span>Subtitle goes here...</span></h2>
      <div class="breadcrumb-wrapper">
        <ol class="breadcrumb">
          <li class="active" runat="server" id="userName"></li>
        </ol>
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
                  <h3 class="panel-title">Filter Information</h3>
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
                              <label class="col-sm-2 control-label">Player:</label>
                              <div class="col-sm-3">
                                <asp:DropDownList ID="inPlayer" runat="server" CssClass="form-control chosen-select">
                                </asp:DropDownList>
                              </div>

                           <label class="col-sm-2 control-label"></label>
                            <div class="col-sm-1">
                                <asp:Button class="btn btn-block btn-success" runat="server" type="submit" Text="Run" OnClick="LoadGames"/>
                            </div>
                            <div class="col-sm-2">
                                <asp:Button  class="btn btn-block btn-danger" runat="server" type="submit" Text="Update" OnClick="UpdateBI" />
                            </div>
                          </div>
                  </form>    
                </div>
              </div>
          </div>
        </div>
      </div>


   <div class="row">
    <div class="col-xs-12">
       <ul class="nav nav-tabs nav-justified">
        <li class="nav-item active"><a data-toggle="tab" href="#menu1">By Game</a></li>
        <li ><a data-toggle="tab" href="#menu2" id="txtLeans" runat="server">Bets</a></li>
        <li><a data-toggle="tab" href="#menu3" id="txtWith" runat="server">With</a></li>
        <li><a data-toggle="tab" href="#menu4" id="txtAgainst" runat="server">Against</a></li>
      </ul>
    </div>
</div>

        <div class="tab-content">      
         <div id="menu1" class="tab-pane fade">
                     <div class="panel-body">	  
                       <div class="table-responsive" style="overflow-x:auto;">
                           <table class="table tables" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th>Event</th>
                                             <th>Date</th>
                                             <th><%=GetPick()%></th>
                                             <th><%=GetPickType()%></th>
                                             <th><%=GetPickLine()%></th>
                                             <th>Cris</th>
                                             <th>Pinacle</th>
                                             <th>Next Line</th>
                                             <th>Sport</th>
                                             <th>Bets With</th>
                                             <th>Risk With</th>
                                             <th>Net With</th>
                                             <th>Hold With</th>
                                             <th>Bets Agaisnt</th>
                                             <th>Risk Agaisnt</th>
                                             <th>Net Agaisnt</th>
                                             <th>Hold Agaisnt</th>
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptGame">
                                      <ItemTemplate>
                                            <tr>
                                                <td ><%# Eval("Event") %></td>
                                                <td ><%# Eval("GameDate") %></td>
                                                <td ><%# Eval("Team") %></td>
                                                <td ><%# Eval("WagerPlay") %></td>
                                                <td ><%# Eval("Line") %></td>                                               
                                                <td ><%# Eval("CrisLine") %></td>
                                                <td ><%# Eval("PinniLine") %></td>
                                                <td ><%# Eval("OurLine") %></td>
                                                <td ><%# Eval("Sport") %></td>
                                                <td ><%# Eval("ContLeansBets") %></td>
                                                <td ><%# Eval("RiskLeans") %></td>
                                                <td ><%# Eval("NetLeans") %></td>
                                                <td ><%# Eval("LeansHold") %></td>
                                                <td ><%# Eval("ContNoLeansBets") %></td>
                                                <td ><%# Eval("RiskNoLeans") %></td>
                                                <td ><%# Eval("NetNoLeans") %></td>
                                                <td ><%# Eval("NoLeansHold") %></td>
                                          </tr>
                                    </ItemTemplate>
                              </asp:Repeater>                            
                       </table>
                 </div>  
            </div>
         </div>
         <div id="menu2" class="tab-pane fade">
                             <div class="panel-body">	  
                       <div class="table-responsive" style="overflow-x:auto;">
                           <table class="table tables" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th>Event</th>
                                             <th>Date</th>
                                             <th>Player</th>
                                             <th>Pick</th>
                                             <th>Risk Amount</th>
                                             <th>Net</th>
                                             <th>Result</th>
                                             <th>Period</th>
                                             <th>Sport</th>
                                             <th>League</th>
                                             <th>Wager Play</th>
                                             <th>Odds</th>
                                             <th>Points</th>
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rpfLeans">
                                      <ItemTemplate>
                                            <tr>
                                            <td ><%# Eval("EventName") %></td>
                                            <td ><%# Eval("GameDate") %></td>
                                            <td ><%# Eval("Player") %></td>
                                            <td ><%# Eval("Pick") %></td>
                                            <td ><%# Eval("RiskAmount") %></td>
                                            <td ><%# Eval("Net") %></td>
                                            <td ><%# Eval("Result") %></td>
                                            <td ><%# Eval("GamePeriod") %></td>
                                            <td ><%# Eval("IdSport") %></td>
                                            <td ><%# Eval("League") %></td>
                                            <td ><%# Eval("WagerPlay") %></td>
                                            <td ><%# Eval("Odds") %></td>
                                            <td ><%# Eval("Points") %></td>
                                          </tr>
                                    </ItemTemplate>
                              </asp:Repeater>                            
                       </table>
                 </div>  
          </div>
         </div>
         <div id="menu3" class="tab-pane fade">   
                   <div class="panel-body">	  
                       <div class="table-responsive" style="overflow-x:auto;">
                           <table class="table tables" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th>Event</th>
                                             <th>Date</th>
                                             <th>Player</th>
                                             <th>Pick</th>
                                             <th>Risk Amount</th>
                                             <th>Net</th>
                                             <th>Result</th>
                                             <th>Period</th>
                                             <th>Sport</th>
                                             <th>League</th>
                                             <th>Wager Play</th>
                                             <th>Odds</th>
                                             <th>Points</th>
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptWith">
                                      <ItemTemplate>
                                            <tr>
                                            <td ><%# Eval("EventName") %></td>
                                            <td ><%# Eval("GameDate") %></td>
                                            <td ><%# Eval("Player") %></td>
                                            <td ><%# Eval("Pick") %></td>
                                            <td ><%# Eval("RiskAmount") %></td>
                                            <td ><%# Eval("Net") %></td>
                                            <td ><%# Eval("Result") %></td>
                                            <td ><%# Eval("GamePeriod") %></td>
                                            <td ><%# Eval("IdSport") %></td>
                                            <td ><%# Eval("League") %></td>
                                            <td ><%# Eval("WagerPlay") %></td>
                                            <td ><%# Eval("Odds") %></td>
                                            <td ><%# Eval("Points") %></td>
                                          </tr>
                                    </ItemTemplate>
                              </asp:Repeater>                            
                       </table>
                 </div>  
          </div>
         </div>
         <div id="menu4" class="tab-pane fade">          
                          <div class="panel-body">	  
                       <div class="table-responsive" style="overflow-x:auto;">
                           <table class="table tables" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th>Event</th>
                                             <th>Date</th>
                                             <th>Player</th>
                                             <th>Pick</th>
                                             <th>Risk Amount</th>
                                             <th>Net</th>
                                             <th>Result</th>
                                             <th>Period</th>
                                             <th>Sport</th>
                                             <th>League</th>
                                             <th>Wager Play</th>
                                             <th>Odds</th>
                                             <th>Points</th>
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptAgainst">
                                      <ItemTemplate>
                                            <tr>
                                            <td ><%# Eval("EventName") %></td>
                                            <td ><%# Eval("GameDate") %></td>
                                            <td ><%# Eval("Player") %></td>
                                            <td ><%# Eval("Pick") %></td>
                                            <td ><%# Eval("RiskAmount") %></td>
                                            <td ><%# Eval("Net") %></td>
                                            <td ><%# Eval("Result") %></td>
                                            <td ><%# Eval("GamePeriod") %></td>
                                            <td ><%# Eval("IdSport") %></td>
                                            <td ><%# Eval("League") %></td>
                                            <td ><%# Eval("WagerPlay") %></td>
                                            <td ><%# Eval("Odds") %></td>
                                            <td ><%# Eval("Points") %></td>
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
