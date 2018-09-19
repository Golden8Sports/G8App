<%@ Page Language="C#" MasterPageFile="~/Views/menu.Master" AutoEventWireup="true" CodeBehind="nhlRanges.aspx.cs" Inherits="G8_App.Views.nhlRanges" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Content1">
      <div class="mainpanel">
    
    <div class="headerbar">      
      <a class="menutoggle"><i class="fa fa-bars"></i></a>  
      <div class="header-right">
      
      </div><!-- header-right -->
      
    </div><!-- headerbar -->
    
    <div class="pageheader">
      <h2><i class="fa fa-joomla"></i> NHL - Ranges <span>Subtitle goes here...</span></h2>
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
                        	<label class="col-sm-2 control-label">League:</label>
                            <div class="col-sm-3">
                              <select class="form-control chosen-select" name="inputLeague" id="inputLeague" runat="server" >
                                <option value="ALL">All</option>
                                  <option value="1G">1 Goal</option>
                                  <option value="ALT">Alternative</option>
                                  <option value="OT">OT</option>
                                  <option value="RT">RT</option>
                              </select>
                            </div>
                            <label class="col-sm-2 control-label">Team:</label>
                            <div class="col-sm-3">
                              <select class="form-control chosen-select" name="inputTeam" id="inputTeam" runat="server">
                              </select>
                            </div>
                        </div>
                        <div class="form-group">
                           <label class="col-sm-2 control-label">Side:</label>
                            <div class="col-sm-3">
                              <select class="form-control chosen-select" name="inputSide" id="inputSide" runat="server">
                                <option value="ALL">All</option>
                                <option value="HOME">Home</option>
                                <option value="AWAY">Away</option>
                              </select>
                            </div>

                          <label class="col-sm-2 control-label">Start Date:</label>
                            <div class="col-sm-3">
                              <div class="input-group">
                                <input type="text" class="form-control" placeholder="mm/dd/yyyy" id="startDate" runat="server" name="startDate" required="required" autocomplete="off" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                              </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-2 control-label">End Date:</label>
                            <div class="col-sm-3">
                              <div class="input-group">
                                  <input type="text" class="form-control" placeholder="mm/dd/yyyy" id="endDate" runat="server" name="endDate" required="required" autocomplete="off" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                              </div>
                            </div>
                            <label class="col-sm-2 control-label"></label>
                            <div class="col-sm-2">
                                <asp:Button class="btn btn-block btn-success" runat="server" type="submit" Text="Run" OnClick="LoadData"/>
                            </div>
                           <div class="col-sm-1">
                                <asp:Button  class="btn btn-block btn-danger" runat="server" type="submit" Text="Excel" OnClick="GenerateExcel" />
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
                                             <th>Visitor Score</th>
                                             <th>Home Score</th>
                                             <th>Winner</th>
                                             <th>Id Game</th>
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptTable">
                                      <ItemTemplate>
                                            <tr>
                                            <td ><%# Eval("GameDateTime") %></td>
                                            <td ><%# Eval("VisitorNumber") %></td>
                                            <td ><%# Eval("VisitorTeam") %></td>
                                            <td ><%# Eval("HomeNumber") %></td>
                                            <td ><%# Eval("HomeTeam") %></td>
                                            <td ><%# Eval("VisitorScore") %></td>
                                            <td ><%# Eval("HomeScore") %></td>
                                            <td ><%# Eval("WINNER") %></td>
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



