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
                                <input type="text" class="form-control" placeholder="mm/dd/yyyy" id="startDate" runat="server" name="startDate" required="required" value="" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                              </div>
                            </div>

                            <label class="col-sm-2 control-label">End Date:</label>
                            <div class="col-sm-3">
                              <div class="input-group">
                                  <input type="text" class="form-control" placeholder="mm/dd/yyyy" id="endDate" runat="server" name="endDate" required="required" value="" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                              </div>
                            </div>

                        </div>

                        <div class="form-group">
                            <label class="col-sm-2 control-label"></label>
                            <label class="col-sm-2 control-label"></label>
                            <label class="col-sm-2 control-label"></label>
                            <label class="col-sm-1 control-label"></label>
                            <div class="col-sm-2">
                                <asp:Button class="btn btn-block btn-success" runat="server" type="submit" Text="Run" OnClick="LoadGames"/>
                            </div>
                           <div class="col-sm-1">
                                <asp:Button  class="btn btn-block btn-danger" runat="server" type="submit" Text="Excel" OnClick="GenerateExcelReport" />
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
                                             <th>Date Time</th>
                                             <th>Visitor Number</th>
                                             <th>Visitor Team</th>
                                             <th>Home Number</th>
                                             <th>Home Team</th>
                                             <th>Id Sport</th>
                                             <th>Id League</th>
                                             <th>Id Game</th>
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
