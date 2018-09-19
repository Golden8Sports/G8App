<%@ Page Language="C#" MasterPageFile="~/Views/menu.Master" AutoEventWireup="true" CodeBehind="NCAAFStats.aspx.cs" Inherits="G8_App.Views.NCAAFStats" %>


<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Content1">
  <div class="mainpanel">    
    <div class="headerbar">      
      <a class="menutoggle"><i class="fa fa-bars"></i></a>      
      <div class="header-right">     
      </div><!-- header-right -->
      
    </div><!-- headerbar -->
     <div class="pageheader">
      <h2><i class="glyphicon glyphicon-stats"></i> NCAAF Info <span>Subtitle goes here...</span></h2>
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
                  <h3 class="panel-title">NCAAF Trends</h3>
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
                                    <asp:ListItem Value="110" >AFC East</asp:ListItem>
                                    <asp:ListItem Value="111" >AFC North</asp:ListItem>
                                    <asp:ListItem Value="112" >AFC South</asp:ListItem>
                                    <asp:ListItem Value="113" >AFC West</asp:ListItem>
                                    <asp:ListItem Value="114" >NFC East</asp:ListItem>
                                    <asp:ListItem Value="115" >NFC North</asp:ListItem>
                                    <asp:ListItem Value="116" >NFC South</asp:ListItem>
                                    <asp:ListItem Value="117" >NFC West</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                      <div class="form-group">
                          
                            <label class="col-sm-1 control-label">Situation:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inSituation" runat="server"  CssClass="form-control chosen-select">
                                    <asp:ListItem Value="all_games" >All Games</asp:ListItem>
                                    <asp:ListItem Value="is_after_bye" >After A Bye</asp:ListItem>
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
                                    <asp:ListItem Value="is_conference" >Conference Games</asp:ListItem>
                                    <asp:ListItem Value="non_conference" >Non-Conference Games</asp:ListItem>
                                    <asp:ListItem Value="is_division" >Division Games</asp:ListItem>
                                    <asp:ListItem Value="non_division" >Non-Division Games</asp:ListItem>
                                    <asp:ListItem Value="is_bowlgame" >Bowl Games</asp:ListItem>
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
                                             <th><%=Line2()%></th>    <!-- dinamic -->
                                             <th><%=Line3()%></th>   <!-- dinamic -->
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