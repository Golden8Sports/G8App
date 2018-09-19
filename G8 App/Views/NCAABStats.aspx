<%@ Page Language="C#" MasterPageFile="~/Views/menu.Master" AutoEventWireup="true" CodeBehind="NCAABStats.aspx.cs" Inherits="G8_App.Views.NCAABStats" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Content1">
  <div class="mainpanel">    
    <div class="headerbar">      
      <a class="menutoggle"><i class="fa fa-bars"></i></a>      
      <div class="header-right">     
      </div><!-- header-right -->
      
    </div><!-- headerbar -->
     <div class="pageheader">
      <h2><i class="glyphicon glyphicon-stats"></i> NCAAB Info <span>Subtitle goes here...</span></h2>
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
                  <h3 class="panel-title">NCAAB Trends</h3>
                </div>

                <div class="panel-body">
                  <div class="row">
                    <div class="col-xs-12">			  

                  <form class="form-horizontal form-bordered" method="post" runat="server">                
                        <div class="form-group">
                            <label class="col-sm-1 control-label">Year:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inYear" runat="server"  CssClass="form-control chosen-select">
                                </asp:DropDownList>
                            </div>

                            <label class="col-sm-1 control-label">Show:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inShow" runat="server"  CssClass="form-control chosen-select">
                                    <asp:ListItem Value="0" >All</asp:ListItem>
                                    <asp:ListItem Value="420" >America East</asp:ListItem>
                                    <asp:ListItem Value="467" >American</asp:ListItem>
                                    <asp:ListItem Value="421" >Atlantic 10</asp:ListItem>
                                    <asp:ListItem Value="422" >ACC</asp:ListItem>
                                    <asp:ListItem Value="423" >Atlantic Sun</asp:ListItem>
                                    <asp:ListItem Value="424" >Big 12</asp:ListItem>
                                    <asp:ListItem Value="425" >Big East</asp:ListItem>
                                    <asp:ListItem Value="426" >Big Sky</asp:ListItem>
                                    <asp:ListItem Value="427" >Big South</asp:ListItem>
                                    <asp:ListItem Value="428" >Big Ten</asp:ListItem>
                                    <asp:ListItem Value="429" >Big West</asp:ListItem>
                                    <asp:ListItem Value="430" >CAA</asp:ListItem>
                                    <asp:ListItem Value="431" >CUSA</asp:ListItem>
                                    <asp:ListItem Value="432" >Horizon League</asp:ListItem>
                                    <asp:ListItem Value="433" >Independets</asp:ListItem>
                                    <asp:ListItem Value="434" >Ivy</asp:ListItem>
                                    <asp:ListItem Value="435" >MAAC</asp:ListItem>
                                    <asp:ListItem Value="436" >MAC</asp:ListItem>
                                    <asp:ListItem Value="437" >MEAC</asp:ListItem>
                                    <asp:ListItem Value="438" >MVC</asp:ListItem>
                                    <asp:ListItem Value="439" >Mountain West</asp:ListItem>
                                    <asp:ListItem Value="440" >Northwest</asp:ListItem>
                                    <asp:ListItem Value="441" >Ohio Valley</asp:ListItem>
                                    <asp:ListItem Value="442" >Pac-12</asp:ListItem>
                                    <asp:ListItem Value="443" >Patriot</asp:ListItem>
                                    <asp:ListItem Value="444" >SEC</asp:ListItem>
                                    <asp:ListItem Value="445" >Southern</asp:ListItem>
                                    <asp:ListItem Value="446" >Southland</asp:ListItem>
                                    <asp:ListItem Value="447" >SWAC</asp:ListItem>
                                    <asp:ListItem Value="448" >Summit  </asp:ListItem>
                                    <asp:ListItem Value="449" >Sun Belt</asp:ListItem>
                                    <asp:ListItem Value="450" >WCC</asp:ListItem>
                                    <asp:ListItem Value="451" >WAC</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                      <div class="form-group">
                          
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
                                    <asp:ListItem Value="is_conference" >Conference Games</asp:ListItem>
                                    <asp:ListItem Value="non_conference" >Non-Conference Games</asp:ListItem>
                                    <asp:ListItem Value="is_division" >Division Games</asp:ListItem>
                                    <asp:ListItem Value="non_division" >Non-Division Games</asp:ListItem>
                                    <asp:ListItem Value="is_ncaa" >NCAA Tournaments Games</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <label class="col-sm-1 control-label">Type</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inType" runat="server"  CssClass="form-control chosen-select">
                                <asp:ListItem Value="win" >Win/Loss</asp:ListItem>
                                <asp:ListItem Value="ats" >ATS</asp:ListItem>
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

                           <div class="table-responsive" style="overflow-x:auto;">
                           <table class="table" id="table1" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th><%=Team()%></th>
                                             <th><%=Line2()%></th>  <!-- dinamic -->
                                             <th><%=Line3()%></th>  <!-- dinamic -->
                                             <th><%=Line4()%></th>  <!-- dinamic -->
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
      
    </div><!-- contentpanel -->
  </div><!-- mainpanel -->



</asp:Content>