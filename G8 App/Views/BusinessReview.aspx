<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusinessReview.aspx.cs" MasterPageFile="~/Views/menu.Master" Inherits="G8_App.Views.BusinessReview" %>

<asp:Content ID="head" ContentPlaceHolderID="head1" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>


<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Content1">
  <div class="mainpanel">    
    <div class="headerbar">      
      <a class="menutoggle"><i class="fa fa-bars"></i></a>      
      <div class="header-right">     
      </div><!-- header-right -->
      
    </div><!-- headerbar -->
    
    <div class="pageheader">
      <h2><i class="fa fa-home"></i> Business Review <span>Subtitle goes here...</span></h2>
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
                  <h3 class="panel-title">Filters</h3>
                </div>

                <div class="panel-body">
                  <div class="row">
                    <div class="col-xs-12">			  

                  <form class="form-horizontal form-bordered" method="post" runat="server">
                      
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
                            <label class="col-sm-2 control-label">Agent:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inAgent" runat="server"  CssClass="form-control chosen-select">
                                </asp:DropDownList>
                            </div>
                       </div>


                        <div class="form-group">
                            <label class="col-sm-8 control-label"></label>
                            <div class="col-sm-2">
                                <asp:Button class="btn btn-block btn-success" runat="server" type="submit" Text="Run" OnClick="LoadData"/>
                            </div>
                        </div>
                  </form>    
                </div>
              </div>
          </div>
        </div>
      </div>

   <div class="row" id="hola" runat="server">
    <div class="col-xs-12">
       <ul class="nav nav-tabs nav-justified">
        <li class="nav-item active"><a data-toggle="tab" href="#menu1">Daily Summary</a></li>
        <li ><a data-toggle="tab" href="#menu2" id="txtLeans" runat="server">Top Players</a></li>
        <li><a data-toggle="tab" href="#menu3" id="txtWith" runat="server">By Sport</a></li>
      </ul>
    </div>
</div>

        <div class="tab-content">      
         <div id="menu1" class="tab-pane fade">
                            <div class="row">
                <div class="panel panel-default">
                 <div class="panel-heading">
                  <div class="panel-btns">
                    <a href="#" class="panel-close">×</a>
                    <a href="#" class="minimize">−</a>
                  </div>
                  <h3 class="panel-title">Summary per Day</h3>
                </div>

                <p id="txt" runat="server"></p>
                <div class="panel-body">
                         <div class="table-responsive" style="overflow-x:auto;">
                           <table class="table tables" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th>Date</th>
                                             <th>Risk</th>
                                             <th>Net</th>
                                             <th>Hold%</th>
                                             <th>Players</th>
                                        </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptOver">
                                      <ItemTemplate>
                                          <tr>
                                            <td ><%# Eval("DATE") %></td>
                                            <td ><%# Eval("RiskAmount") %></td>
                                            <td ><%# Eval("NET") %></td>
                                            <td ><%# Eval("HoldPercentaje") %></td>
                                            <td ><%# Eval("PLAYERS") %></td>
                                         </tr>
                                    </ItemTemplate>
                              </asp:Repeater>                           
                       </table>
                 </div>  
                </div>
        </div>
     </div>
         </div>
         <div id="menu2" class="tab-pane fade" style="width:100%;">
              <div class="row" style="width:100%;">
               <div class="panel panel-default" style="width:100%;">
                <div class="panel-heading">
                  <div class="panel-btns">
                    <a href="#" class="panel-close">×</a>
                    <a href="#" class="minimize">−</a>
                  </div>
                  <h3 class="panel-title">Top Players:</h3>
               </div>

                <div class="panel-body" style="width:100%;">	  
                       <div class="table-responsive" style="width:100%;">
                           <table class="table customTable"  style="width:100%;">
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
                                  <asp:Repeater runat="server" ID="rptTopPlayers">
                                      <ItemTemplate>
                                          <tr>
                                            <td ><%# Eval("AGENT") %></td>
                                            <td ><button class="btn btn-success" data-toggle="modal" data-target="#myModal"><%# Eval("PLAYER") %></button></td>
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
         </div>
         <div id="menu3" class="tab-pane fade">   
                 <div class="row" style="width:100%;">
               <div class="panel panel-default" style="width:100%;">
                <div class="panel-heading">
                  <div class="panel-btns">
                    <a href="#" class="panel-close">×</a>
                    <a href="#" class="minimize">−</a>
                  </div>
                  <h3 class="panel-title">Summary by Sport:</h3>
               </div>

                <div class="panel-body" style="width:100%;">	  
                       <div class="table-responsive" style="width:100%;">
                           <table class="table tables"  style="width:100%;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th>Sport</th>
                                             <th>Risk</th>
                                             <th>Net</th>
                                             <th>Hold%</th>                      
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptSport">
                                      <ItemTemplate>
                                          <tr>
                                            <td ><button class="btn btn-success"><%# Eval("SPORT") %></button></td>
                                            <td ><%# Eval("RiskAmount") %></td>
                                            <td ><%# Eval("NET") %></td>
                                            <td ><%# Eval("HoldPercentaje") %></td>
                                         </tr>
                                    </ItemTemplate>
                              </asp:Repeater>                            
                        </table>
                     </div>  
                  </div>
                </div>
             </div>
         </div>
        </div>
      
    </div><!-- contentpanel -->
  </div><!-- mainpanel -->

</asp:Content>
