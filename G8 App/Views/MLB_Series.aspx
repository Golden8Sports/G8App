<%@ Page Language="C#" MasterPageFile="~/Views/menu.Master" AutoEventWireup="true" CodeBehind="MLB_Series.aspx.cs" Inherits="G8_App.Views.MLB_Series" %>


<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Content1">
      <div class="mainpanel">
    
    <div class="headerbar">
      
      <a class="menutoggle"><i class="fa fa-bars"></i></a>
      
      <div class="header-right">

      
      </div><!-- header-right -->
      
    </div><!-- headerbar -->
    
    <div class="pageheader">
      <h2><i class="glyphicon glyphicon-flash"></i> MLB Series <span>Subtitle goes here...</span></h2>
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
                  <h3 class="panel-title">Filter</h3>
                </div>

                <div class="panel-body">
                  <div class="row">
                    <div class="col-xs-12">			  

                  <form class="form-horizontal form-bordered" method="post" runat="server">
                      
                       <div class="form-group">
                         <label class="col-sm-1 control-label">Start Date:</label>
                            <div class="col-sm-3">
                              <div class="input-group">
                                <input type="text" class="form-control" placeholder="mm/dd/yyyy" id="startDate" runat="server" name="startDate" required="required" autocomplete="off" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                              </div>
                            </div>

                            <label class="col-sm-1 control-label">End Date:</label>
                            <div class="col-sm-3">
                              <div class="input-group">
                                  <input type="text" class="form-control"  placeholder="mm/dd/yyyy" id="endDate" runat="server" name="endDate" required="required" autocomplete="off" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                              </div>
                            </div>

                           <label class="col-sm-1 control-label">Team:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inTeam" runat="server"  CssClass="form-control chosen-select">
                                </asp:DropDownList>
                            </div>
                        </div>

                      <div class="form-group">
                            <label class="col-sm-1 control-label">At:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inOrder" runat="server"  CssClass="form-control chosen-select">
                                    <asp:ListItem Value="B" >BOTH</asp:ListItem>
                                    <asp:ListItem Value="A" >AWAY</asp:ListItem>
                                    <asp:ListItem Value="H" >HOME</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                           <label class="col-sm-1 control-label">As:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inRef" runat="server"  CssClass="form-control chosen-select">
                                    <asp:ListItem Value="Fav" >FAV</asp:ListItem>
                                    <asp:ListItem Value="Dog" >DOG</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                           <label class="col-sm-1 control-label"></label>
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

               <div class="row">
                <div class="panel panel-default">
                 <div class="panel-heading">
                  <div class="panel-btns">
                    <a href="#" class="panel-close">×</a>
                    <a href="#" class="minimize">−</a>
                  </div>
                  <h3 class="panel-title">Series - Detailed</h3>
                </div>


                <div class="panel-body">
                         <div class="table-responsive" style="overflow-x:auto;" runat="server">
                           <table class="table" id="table1" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th>Date</th>
                                             <th>Home</th>
                                             <th>Away</th>
                                             <th>Range</th>
                                             <th>Price</th>
                                             <th><%=GetFavDog()%></th>
                                             <th>Type</th>
                                             <th>Game 1</th>
                                             <th>Game 2</th>
                                             <th>Game 3</th>
                                             <th>Game 4</th>
                                             <th>ML 1</th>
                                             <th>ML 2</th>
                                             <th>ML 3</th>
                                             <th>ML 4</th>
                                             <th>SP 1</th>
                                             <th>SP Odds 1</th>
                                             <th>SP 2</th>
                                             <th>SP Odds 2</th>
                                             <th>SP 3</th>
                                             <th>SP Odds 2</th>
                                             <th>SP 4</th>
                                             <th>SP Odds 2</th>
                                             <th>TOTAL 1</th>
                                             <th>TOTAL 2</th>
                                             <th>TOTAL 3</th>
                                             <th>TOTAL 4</th>
                                             <th>TOTAL OVER 1</th>
                                             <th>TOTAL OVER 2</th>
                                             <th>TOTAL OVER 3</th>
                                             <th>TOTAL OVER 4</th>
                                             <th>TOTAL UNDER 1</th>                                            
                                             <th>TOTAL UNDER 2</th>                                             
                                             <th>TOTAL UNDER 3</th>                                             
                                             <th>TOTAL UNDER 4</th>
                                             <th>HS 1</th>
                                             <th>VS 1</th>
                                             <th>HS 2</th>
                                             <th>VS 2</th>
                                             <th>HS 3</th>
                                             <th>VS 3</th>
                                             <th>HS 4</th>
                                             <th>VS 4</th>
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptTable">
                                      <ItemTemplate>
                                            <tr>
                                            <td ><%# Eval("Date") %></td>
                                            <td ><%# Eval("HTeam") %></td>
                                            <td ><%# Eval("VTeam") %></td>
                                            <td ><%# Eval("SeriesRange") %></td>
                                            <td ><%# Eval("Line") %></td>
                                            <td ><%# Eval("Fav") %></td>
                                            <td ><%# Eval("SeriesType") %></td>
                                            <td ><%# Eval("Game1") %></td>
                                            <td ><%# Eval("Game2") %></td>
                                            <td ><%# Eval("Game3") %></td>
                                            <td ><%# Eval("Game4") %></td>
                                            <td ><%# Eval("Game1Line") %></td>
                                            <td ><%# Eval("Game2Line") %></td>
                                            <td ><%# Eval("Game3Line") %></td>
                                            <td ><%# Eval("Game4Line") %></td>
                                            <td ><%# Eval("Spread1") %></td>
                                            <td ><%# Eval("SpreadOdds1") %></td>
                                            <td ><%# Eval("Spread2") %></td>
                                            <td ><%# Eval("SpreadOdds2") %></td>
                                            <td ><%# Eval("Spread3") %></td>
                                            <td ><%# Eval("SpreadOdds3") %></td>
                                            <td ><%# Eval("Spread4") %></td>
                                            <td ><%# Eval("SpreadOdds4") %></td>
                                            <td ><%# Eval("Total1") %></td>
                                            <td ><%# Eval("Total2") %></td>
                                            <td ><%# Eval("Total3") %></td>
                                            <td ><%# Eval("Total4") %></td>
                                            <td ><%# Eval("TotalOver1") %></td>
                                            <td ><%# Eval("TotalOver2") %></td>
                                            <td ><%# Eval("TotalOver3") %></td>
                                            <td ><%# Eval("TotalOver4") %></td>
                                            <td ><%# Eval("TotalUnder1") %></td>                                            
                                            <td ><%# Eval("TotalUnder2") %></td>                                           
                                            <td ><%# Eval("TotalUnder3") %></td>                                            
                                            <td ><%# Eval("TotalUnder4") %></td>
                                            <td ><%# Eval("HomeScore1") %></td>
                                            <td ><%# Eval("VisitorScore1") %></td>
                                            <td ><%# Eval("HomeScore2") %></td>
                                            <td ><%# Eval("VisitorScore2") %></td>
                                            <td ><%# Eval("HomeScore3") %></td>
                                            <td ><%# Eval("VisitorScore3") %></td>
                                            <td ><%# Eval("HomeScore4") %></td>
                                            <td ><%# Eval("VisitorScore4") %></td>
                                          </tr>
                                    </ItemTemplate>
                              </asp:Repeater>                            
                       </table>
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
                  <h3 class="panel-title">Grouped</h3>
                </div>


                <div class="panel-body">
                         <div class="table-responsive" style="overflow-x:auto;">
                           <table class="table" id="table2" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th>Type</th>
                                             <th>Range</th>
                                             <th><%=GetFavDog()%></th>
                                             <th>Total Series</th>
                                             <th>Total Games</th>
                                             <th>Fav Pos Sweep</th>
                                             <th>Fav Sweep</th>
                                             <th>Dog Pos Sweep</th>
                                             <th>Dog Sweep </th>                                           
                                             <th>Game 1</th>
                                             <th>Game 2</th>
                                             <th>Game 3</th>
                                             <th>Game 4</th>
                                             <th>Win% 1</th>                                             
                                             <th>Win% 2</th>                                             
                                             <th>Win% 3</th>                                             
                                             <th>Win% 4</th>
                                             <th>Risk 1</th>
                                             <th>Risk 2</th>
                                             <th>Risk 3</th>
                                             <th>Risk 4</th>
                                             <th>Net 1</th>
                                             <th>Net 2</th>
                                             <th>Net 3</th>
                                             <th>Net 4</th>
                                             <th>Hold 1</th>
                                             <th>Hold 2</th>
                                             <th>Hold 3</th>
                                             <th>Hold 4</th>
                                        </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rpTable2">
                                      <ItemTemplate>
                                            <tr>
                                            <td ><%# Eval("SeriesType") %></td>
                                            <td ><%# Eval("SeriesRange") %></td>
                                            <td ><%# Eval("Fav") %></td>
                                            <td ><%# Eval("TotalSeries") %></td>
                                            <td ><%# Eval("TotalGames") %></td>
                                            <td ><%# Eval("FavSwipPossible") %></td>
                                            <td ><%# Eval("FavSwip") %></td>
                                            <td ><%# Eval("DogSwipPossible") %></td>
                                            <td ><%# Eval("DogSwip") %></td>
                                            <td ><%# Eval("Game1") %></td>
                                            <td ><%# Eval("Game2") %></td>
                                            <td ><%# Eval("Game3") %></td>
                                            <td ><%# Eval("Game4") %></td>
                                            <td ><%# Eval("Game1Percent") %></td>                                          
                                            <td ><%# Eval("Game2Percent") %></td>                                            
                                            <td ><%# Eval("Game3Percent") %></td>                                            
                                            <td ><%# Eval("Game4Percent") %></td>
                                            <td ><%# Eval("Risk1") %></td>
                                            <td ><%# Eval("Risk2") %></td>
                                            <td ><%# Eval("Risk3") %></td>
                                            <td ><%# Eval("Risk4") %></td>
                                            <td ><%# Eval("Net1") %></td>
                                            <td ><%# Eval("Net2") %></td>
                                            <td ><%# Eval("Net3") %></td>
                                            <td ><%# Eval("Net4") %></td>
                                            <td ><%# Eval("Hold1") %></td>
                                            <td ><%# Eval("Hold2") %></td>
                                            <td ><%# Eval("Hold3") %></td>
                                            <td ><%# Eval("Hold4") %></td>
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