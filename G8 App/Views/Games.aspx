<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/menu.Master" CodeBehind="Games.aspx.cs" Inherits="G8_App.Views.Games" %>


<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Content1">
      
   <div class="mainpanel">
    <div class="headerbar">      
      <a class="menutoggle"><i class="fa fa-bars"></i></a>   
      <div class="header-right">  
      </div><!-- header-right -->
    </div><!-- headerbar -->


    <div class="pageheader">
      <h2><i class="glyphicon glyphicon-tower"></i> Games <span>stats</span></h2>
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

                           <label class="col-sm-2 control-label">Line Type:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inLineType" runat="server"  CssClass="form-control chosen-select">
                                </asp:DropDownList>
                            </div>


                            <label class="col-sm-3 control-label"></label>
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
                           <table class="table" id="table1" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th>Date</th>
                                             <th>IdGame</th>
                                             <th>Home Team</th>
                                             <th>Visitor Team</th>
                                             <th>Home Number</th>
                                             <th>Visitor Number</th>
                                             <th>Sport</th>
                                             <th>League</th>
                                             <th>Period</th>
                                             <th>Hom Score</th>
                                             <th>Vis Score</th>
                                             <th>CL Hom ML</th>
                                             <th>CL Vis ML</th>
                                             <th>CL Hom SP</th>
                                             <th>CL Vis SP</th>
                                             <th>CL Tot Over</th>
                                             <th>CL Tot Under</th>
                                             <th>CL Draw</th>
                                             <th>Op Hom ML</th>
                                             <th>Op Vis ML</th>
                                             <th>Op Hom SP</th>
                                             <th>Op Vis SP</th>
                                             <th>Op Tot Over</th>
                                             <th>Op Tot Under</th>
                                             <th>Op Draw</th>
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptTable">
                                      <ItemTemplate>
                                            <tr>
                                            <td ><%# Eval("GameDate") %></td>
                                            <td ><%# Eval("IdGame") %></td>
                                            <td ><%# Eval("HomeTeam") %></td>
                                            <td ><%# Eval("VisitorTeam") %></td>
                                            <td ><%# Eval("HomeNumber") %></td>
                                            <td ><%# Eval("VisitorNumber") %></td>
                                            <td ><%# Eval("IdSportDGS") %></td>
                                            <td ><%# Eval("LeagueName") %></td>
                                            <td ><%# Eval("Period") %></td>
                                            <td ><%# Eval("HomeScoreDGS") %></td>
                                            <td ><%# Eval("VisitorScoreDGS") %></td>
                                            <td ><%# Eval("ClMoneyLineHom") %></td>
                                            <td ><%# Eval("ClMoneyLineVis") %></td>
                                            <td ><%# Eval("ClSpreadHom") %></td>
                                            <td ><%# Eval("ClSpreadVis") %></td>
                                            <td ><%# Eval("ClTotalOver") %></td>
                                            <td ><%# Eval("ClTotalUnder") %></td>
                                            <td ><%# Eval("ClDraw") %></td>
                                            <td ><%# Eval("OpMoneyLineHom") %></td>
                                            <td ><%# Eval("OpMoneyLineVis") %></td>
                                            <td ><%# Eval("OpSpreadHom") %></td>
                                            <td ><%# Eval("OpSpreadVis") %></td>
                                            <td ><%# Eval("OpTotalOver") %></td>
                                            <td ><%# Eval("OpTotalUnder") %></td>
                                            <td ><%# Eval("OpDraw") %></td>
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
