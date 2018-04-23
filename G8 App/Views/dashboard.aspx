<%@ Page Language="C#" Title="" MasterPageFile="~/Views/menu.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="G8_App.Views.dashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Content1">
      <div class="mainpanel">
    
    <div class="headerbar">
      
      <a class="menutoggle"><i class="fa fa-bars"></i></a>
      
      <div class="header-right">
      
      </div><!-- header-right -->
      
    </div><!-- headerbar -->
    
    <div class="pageheader">
      <h2><i class="fa fa-home"></i> Dashboard <span>Subtitle goes here...</span></h2>
      <div class="breadcrumb-wrapper">
        <ol class="breadcrumb">
          <li class="active" runat="server" id="userName">Dashboard</li>
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
                        	<label class="col-sm-2 control-label">Sport:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inSport" runat="server" AutoPostBack="true"  CssClass="form-control chosen-select">
                                </asp:DropDownList>
                            </div>
                            <label class="col-sm-2 control-label">Date:</label>
                            <div class="col-sm-3">
                              <asp:DropDownList ID="inLeague" CssClass="form-control chosen-select" AutoPostBack="false" runat="server">
                                  <asp:ListItem Value="0" >This Week</asp:ListItem>
                                  <asp:ListItem Value="1" >Last Week</asp:ListItem>
                                  <asp:ListItem Value="2" >This Month</asp:ListItem>
                                  <asp:ListItem Value="3" >Last Month</asp:ListItem>
                              </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-2 control-label"></label>
                            <label class="col-sm-2 control-label"></label>
                            <label class="col-sm-2 control-label"></label>
                            <label class="col-sm-1 control-label"></label>
                            <div class="col-sm-2">
                                <asp:Button class="btn btn-block btn-success" runat="server" type="submit" Text="Run"/>
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
                           <table class="table" id="table1" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th>Player</th>
                                             <th>Period</th>
                                             <th>Net</th>
                                             <th>Hold%</th>
                                             <th>Win%</th>
                                             <th>Scalping</th>
                                             <th>Line Moved</th>
                                             <th>Beat Line</th>
                                             <th>Syndicate</th>
                                             <th>Bets</th>
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptTable">
                                      <ItemTemplate>
                                            <tr>
                                            <td ><%# Eval("EventDate") %></td>
                                            <td ><%# Eval("VisitorNumber") %></td>
                                            <td ><%# Eval("VisitorTeam") %></td>
                                            <td ><%# Eval("HomeNumber") %></td>
                                            <td ><%# Eval("HomeTeam") %></td>
                                            <td ><%# Eval("IdSport") %></td>
                                            <td ><%# Eval("IdLeague") %></td>
                                            <td ><%# Eval("IdGame") %></td>
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
