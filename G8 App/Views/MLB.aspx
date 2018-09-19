<%@ Page Language="C#" MasterPageFile="~/Views/menu.Master" AutoEventWireup="true" CodeBehind="MLB.aspx.cs" Inherits="G8_App.Views.SportsIntelligence.MLB" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Content1">
  <div class="mainpanel">    
    <div class="headerbar">      
      <a class="menutoggle"><i class="fa fa-bars"></i></a>      
      <div class="header-right">     
      </div><!-- header-right -->
      
    </div><!-- headerbar -->
     <div class="pageheader">
      <h2><i class="glyphicon glyphicon-stats"></i> MLB Info <span>Subtitle goes here...</span></h2>
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
                            <label class="col-sm-1 control-label">Year:</label>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="inYear" runat="server"  CssClass="form-control chosen-select">
                                </asp:DropDownList>
                            </div>


                            <%-- here --%>
                           <label class="col-sm-1 control-label">Situation:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inSituation" runat="server"  CssClass="form-control chosen-select">
                                    <asp:ListItem Value="all_games" >All Games</asp:ListItem>
                                    <asp:ListItem Value="is_after_win" >After A Win</asp:ListItem>
                                    <asp:ListItem Value="is_after_loss" >After A Loss</asp:ListItem>
                                    <asp:ListItem Value="is_home" >As Home Team</asp:ListItem>
                                    <asp:ListItem Value="is_away" >As Away Team</asp:ListItem>
                                    <asp:ListItem Value="is_fav" >As Favorite</asp:ListItem>
                                    <asp:ListItem Value="is_dog" >As Underdog</asp:ListItem>
                                    <asp:ListItem Value="is_home_fav" >As Home Favorite</asp:ListItem>
                                    <asp:ListItem Value="is_home_dog" >As Home Underdog</asp:ListItem>
                                    <asp:ListItem Value="is_away_fav" >As Away Favorite</asp:ListItem>
                                    <asp:ListItem Value="is_away_dog" >As Away Underdog</asp:ListItem>
                                    <asp:ListItem Value="no_rest" >No Rest</asp:ListItem>
                                    <asp:ListItem Value="one_day_off" >1 Day Off</asp:ListItem>
                                    <asp:ListItem Value="two_three_days_off" >2-3 Day Off</asp:ListItem>
                                    <asp:ListItem Value="four_plus_days_off" >4+ Day Off</asp:ListItem>
                                    <asp:ListItem Value="is_division" >Division Games</asp:ListItem>
                                    <asp:ListItem Value="non_division" >Non-Division Games</asp:ListItem>
                                    <asp:ListItem Value="is_playoff" >Playoff Games</asp:ListItem>
                                    <asp:ListItem Value="is_league" >League Games</asp:ListItem>
                                    <asp:ListItem Value="non_league" >Non League Games</asp:ListItem>
                                </asp:DropDownList>
                            </div>


                            
                            <label class="col-sm-1 control-label">Team:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inTeam" runat="server"  CssClass="form-control chosen-select">
                                </asp:DropDownList>
                            </div>
                        </div>



                      <div class="form-group">                          
                            <label class="col-sm-1 control-label">Show:</label>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="inShow" runat="server"  CssClass="form-control chosen-select">
                                    <asp:ListItem Value="0" >All</asp:ListItem>
                                    <asp:ListItem Value="710" >Al West</asp:ListItem>
                                    <asp:ListItem Value="711" >Al East</asp:ListItem>
                                    <asp:ListItem Value="712" >Al Central</asp:ListItem>
                                    <asp:ListItem Value="713" >Nl Central</asp:ListItem>
                                    <asp:ListItem Value="714" >Nl West</asp:ListItem>
                                    <asp:ListItem Value="715" >Nl East</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <label class="col-sm-1 control-label">Type</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inType" runat="server"  CssClass="form-control chosen-select">
                                <asp:ListItem Value="win" >Win/Loss</asp:ListItem>
                                <asp:ListItem Value="ats" >Run Line</asp:ListItem>
                                <asp:ListItem Value="ou" >Over/Under</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                           <label class="col-sm-1 control-label"></label>
                           <div class="col-sm-2">
                            <asp:Button class="btn btn-block btn-success" runat="server" type="submit" Text="Run" OnClick="Trends"/>
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
                  <h3 class="panel-title">MLB Trends</h3>
                </div>

                <div class="panel-body">
                        <div class="table-responsive" style="overflow-x:auto;">
                           <table class="table" id="table1" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th><%=Team()%></th>
                                             <th><%=Line2()%></th>    
                                             <th><%=Line3()%></th>  
                                             <th><%=Line4()%></th>
                                             <th><%=Line5()%></th> 
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptTable">
                                      <ItemTemplate>
                                            <tr>
                                            <td ><%# Eval("Team") %></td>
                                            <td ><%# Eval("Line1") %></td>
                                            <td ><%# Eval("Line2") %></td>
                                            <td ><%# Eval("Line3") %></td>
                                            <td ><%# Eval("Line4") %></td>
                                          </tr>
                                    </ItemTemplate>
                              </asp:Repeater>                               
                       </table>
                 </div> 
          </div>
       </div>
    </div>


        <%-- Team Rank --%>

         <div class="row">
           <div class="panel panel-default">
                <div class="panel-heading">
                  <div class="panel-btns">
                    <a href="#" class="panel-close">×</a>
                    <a href="#" class="minimize">−</a>
                  </div>
                  <h3 class="panel-title">MLB Predicted Rank</h3>
                </div>

                <div class="panel-body">
                  <div class="row">
                    <div class="col-xs-12">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lbTeam" class="col-sm-3 control-label"></asp:Label>
                        </div>                       
                        <div class="form-group">
                            <label class="col-sm-1 control-label"></label>
                            <label class="col-sm-2 control-label" style="font-size:xx-large; color:darkcyan">Record:</label>
                            <asp:Label runat="server" ID="lbRecord" class="col-sm-2 control-label" style="font-size:xx-large;">-</asp:Label>
                            <label class="col-sm-2 control-label" style="font-size:xx-large;color:darkcyan"">Rank:</label>
                            <asp:Label runat="server" ID="lbRank" class="col-sm-1 control-label" style="font-size:xx-large;">-</asp:Label>
                            <label class="col-sm-2 control-label" style="font-size:xx-large;color:darkcyan"">Streak:</label>
                            <asp:Label runat="server" ID="lbStreak" class="col-sm-1 control-label" style="font-size:xx-large;">-</asp:Label>
                        </div>
                </div>
              </div>
          </div>
        </div> 
      </div>

     <%-- End By Team --%>



        <%-- RUNS ALLOWED --%>

                       <div class="row">
                <div class="panel panel-default">
                <div class="panel-heading">
                  <div class="panel-btns">
                    <a href="#" class="panel-close">×</a>
                    <a href="#" class="minimize">−</a>
                  </div>
                  <h3 class="panel-title">MLB Runs Allowed</h3>
                </div>

                <div class="panel-body">
                  

                        <div class="table-responsive" style="overflow-x:auto;">
                           <table class="table tables" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th><%=TagAllowed(1)%></th>
                                             <th><%=TagAllowed(2)%></th>    
                                             <th><%=TagAllowed(3)%></th>  
                                             <th><%=TagAllowed(4)%></th>
                                             <th><%=TagAllowed(5)%></th> 
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptRunsAllowed">
                                      <ItemTemplate>
                                            <tr>
                                            <td ><%# Eval("Rank") %></td>
                                            <td ><%# Eval("Player") %></td>
                                            <td ><%# Eval("Team") %></td>
                                            <td ><%# Eval("Pos") %></td>
                                            <td ><%# Eval("Value") %></td>
                                          </tr>
                                    </ItemTemplate>
                              </asp:Repeater>                               
                       </table>
                 </div> 
          </div>
       </div>
    </div>


        <%-- END RUNS ALLOWED --%>





        <%-- RUNS PER GAME --%>

               <div class="row">
                <div class="panel panel-default">
                <div class="panel-heading">
                  <div class="panel-btns">
                    <a href="#" class="panel-close">×</a>
                    <a href="#" class="minimize">−</a>
                  </div>
                  <h3 class="panel-title">MLB Game Runs</h3>
                </div>

                <div class="panel-body">
                   

                        <div class="table-responsive" style="overflow-x:auto;">
                           <table class="table" id="table2" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th><%=Tag(1)%></th>
                                             <th><%=Tag(2)%></th>    
                                             <th><%=Tag(3)%></th>  
                                             <th><%=Tag(4)%></th>
                                             <th><%=Tag(5)%></th>
                                             <th><%=Tag(6)%></th>
                                             <th><%=Tag(7)%></th>
                                             <th><%=Tag(8)%></th>
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="runsTable">
                                      <ItemTemplate>
                                            <tr>
                                            <td ><%# Eval("Rank") %></td>
                                            <td ><%# Eval("Team") %></td>
                                            <td ><%# Eval("Year") %></td>
                                            <td ><%# Eval("Last3") %></td>
                                            <td ><%# Eval("Last1") %></td>
                                            <td ><%# Eval("Home") %></td>
                                            <td ><%# Eval("Away") %></td>
                                            <td ><%# Eval("YearBefore") %></td>
                                          </tr>
                                    </ItemTemplate>
                              </asp:Repeater>                               
                       </table>
                 </div> 
          </div>
       </div>
    </div>

        <%-- END RUNS PER GAME --%>




        <%-- OUTS PITCHED --%>


                       <div class="row">
                <div class="panel panel-default">
                <div class="panel-heading">
                  <div class="panel-btns">
                    <a href="#" class="panel-close">×</a>
                    <a href="#" class="minimize">−</a>
                  </div>
                  <h3 class="panel-title">MLB Outs Pitched</h3>
                </div>

                <div class="panel-body">                  
                        <div class="table-responsive" style="overflow-x:auto;">
                           <table class="table tables" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th><%=Tag(1)%></th>
                                             <th><%=Tag(2)%></th>    
                                             <th><%=Tag(3)%></th>  
                                             <th><%=Tag(4)%></th>
                                             <th><%=Tag(5)%></th>
                                             <th><%=Tag(6)%></th>
                                             <th><%=Tag(7)%></th>
                                             <th><%=Tag(8)%></th>
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptOutPitched">
                                      <ItemTemplate>
                                            <tr>
                                            <td ><%# Eval("Rank") %></td>
                                            <td ><%# Eval("Team") %></td>
                                            <td ><%# Eval("Year") %></td>
                                            <td ><%# Eval("Last3") %></td>
                                            <td ><%# Eval("Last1") %></td>
                                            <td ><%# Eval("Home") %></td>
                                            <td ><%# Eval("Away") %></td>
                                            <td ><%# Eval("YearBefore") %></td>
                                          </tr>
                                    </ItemTemplate>
                              </asp:Repeater>                               
                       </table>
                 </div> 
          </div>
       </div>
    </div>



        <%-- END OUTS PITCHED --%>
       

      
    </div><!-- contentpanel -->
  </div><!-- mainpanel -->



</asp:Content>
