<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/menu.Master" CodeBehind="CheckScores.aspx.cs" Inherits="G8_App.Views.CheckScores" %>

<asp:Content runat="server" ContentPlaceHolderID="head1" ID="head1">

<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
  <script type="text/javascript">

      function SetColor()
      {
          for (var i = 0; i < document.getElementById('table1').rows.length; i++)
          {
              text = document.getElementById('table1').rows[i].cells[12].innerHTML;

              if (text == 'WRONG')
              {
                  document.getElementById('table1').rows[i].style.backgroundColor = "#FE642E";
                  document.getElementById('table1').rows[i].style.color = "#FFFFFF";
              }
              else if (text == 'MISSING')
              {
                  document.getElementById('table1').rows[i].style.backgroundColor = "#F4FA58";
              }
          } 
      } 

  </script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Content1">
      <div class="mainpanel" onmousemove="SetColor()" onmouseover="SetColor()">
    
    <div class="headerbar">    
      <a class="menutoggle"><i class="fa fa-bars"></i></a>   
      <div class="header-right">    
      </div><!-- header-right -->    
    </div><!-- headerbar -->
    
    <div class="pageheader">
      <h2><i class="fa fa-bullhorn"></i> Check Scores <span>Subtitle goes here...</span></h2>
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
                           <label class="col-sm-2 control-label">Sport:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inSport" runat="server"  CssClass="form-control chosen-select">
                                </asp:DropDownList>
                            </div>

                            <label class="col-sm-2 control-label">Status:</label>
                            <div class="col-sm-3">

                                <asp:DropDownList ID="inStatus" runat="server" CssClass="form-control chosen-select" OnSelectedIndexChanged="ReSelect">
                                    <asp:ListItem Selected="True" Value="">ALL</asp:ListItem>
                                    <asp:ListItem Value="WRONG">WRONG</asp:ListItem>
                                    <asp:ListItem Value="RIGHT">RIGHT</asp:ListItem>
                                    <asp:ListItem Value="MISSING">MISSING</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                      <div class="form-group">
                      <label class="col-sm-8 control-label"></label>
                         <div class="col-sm-2">
                           <asp:Button class="btn btn-block btn-success" runat="server" type="submit" Text="Check" OnClick="LoadGames"/>
                         </div>
                      </div>



                  </form>    
                </div>
              </div>
          </div>
        </div>
      </div> <!-- Fin Row -->


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
                           <table class="table" id="table1" onload="SetColor()" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th>Date</th>
                                             <th>Visitor Rot</th>
                                             <th>Visitor Team</th>
                                             <th>Home Rot</th>
                                             <th>Home Team</th>
                                             <th>Visitor Score Don Best</th>
                                             <th>Home Score Dos Best</th>
                                             <th>Visitor Score DGS</th>
                                             <th>Home Score DGS</th>
                                             <th>Sport</th>
                                             <th>League</th>
                                             <th>Period</th>
                                             <th style="cursor:pointer;">Status<a class="fa fa-angle-down"></a></th>
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptTable">
                                      <ItemTemplate>
                                         <tr>
                                            <td ><%# Eval("GameDate") %></td>
                                            <td ><%# Eval("VisitorNumberAux") %></td>
                                            <td ><%# Eval("VisitorTeam") %></td>
                                            <td ><%# Eval("HomeNumberAux") %></td>
                                            <td ><%# Eval("HomeTeam") %></td>
                                            <td ><%# Eval("VisitorScoreDonBest") %></td>
                                            <td ><%# Eval("HomeScoreDonBest") %></td>
                                            <td ><%# Eval("VisitorScoreDGS") %></td>
                                            <td ><%# Eval("HomeScoreDGS") %></td>
                                            <td ><%# Eval("IdSportDGS") %></td>
                                            <td ><%# Eval("LeagueName") %></td>
                                            <td ><%# Eval("GamePeriod") %></td>
                                            <td ><%# Eval("Status") %></td>
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