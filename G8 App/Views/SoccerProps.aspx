<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/menu.Master" CodeBehind="SoccerProps.aspx.cs" Inherits="G8_App.Views.SoccerProps" %>


<asp:Content runat="server" ContentPlaceHolderID="head1" ID="head1Content">
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
      <h2><i class="fa fa-soccer-ball-o"></i> Soccer <span>Props</span></h2>
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
                            <label class="col-sm-1 control-label">Category:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inCategory" runat="server"  CssClass="form-control chosen-select">
                                    <asp:ListItem Value="" >ALL</asp:ListItem>
                                    <asp:ListItem Value="Alternate Asina Handicap">Alternate Asina Handicap</asp:ListItem>
                                    <asp:ListItem Value="Both Teams To Score">Both Teams To Score</asp:ListItem>
                                    <asp:ListItem Value="Clean Sheet">Clean Sheet</asp:ListItem>
                                    <asp:ListItem Value="Correct Score">Correct Score</asp:ListItem>
                                    <asp:ListItem Value="Draw No Bet">Draw No Bet</asp:ListItem>
                                    <asp:ListItem Value="First Team To Score">First Team To Score</asp:ListItem>
                                    <asp:ListItem Value="Futures">Futures</asp:ListItem>
                                    <asp:ListItem Value="Goalscorer">Goalscorer</asp:ListItem>
                                    <asp:ListItem Value="Group">Group</asp:ListItem>
                                    <asp:ListItem Value="Total Game Score">Total Game Score</asp:ListItem>
                                    <asp:ListItem Value="To Win And Both To Score">To Win And Both To Score</asp:ListItem>
                                    <asp:ListItem Value="Total Score">Total Score</asp:ListItem>
                                    <asp:ListItem Value="Outright">Outright</asp:ListItem>
                                    <asp:ListItem Value="Will Team Score">Will Team Score</asp:ListItem>
                                </asp:DropDownList>
                     </div>


                            <label class="col-sm-1 control-label">League:</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="inLeague" runat="server"  CssClass="form-control chosen-select">
                                </asp:DropDownList>
                            </div>

                        <div class="col-sm-1">
                            <asp:Button class="btn btn-block btn-success" ID="btnRun" runat="server" type="submit" Text="Run" OnClick="SocProps" />
                        </div>

                        <div class="col-sm-2">
                            <asp:Button class="btn btn-block btn-danger" ID="btnPopulate" runat="server" Visible="false" type="submit" Text="Populate" OnClick="PopulateInfo" />
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
                                             <th>Event</th>
                                             <th>League</th>
                                             <th>Name</th>
                                             <th>Category</th>
                                             <th>Period</th>
                                             <th>Op 1</th>
                                             <th>Price</th>
                                             <th>Handicap</th>
                                             <th>Op 2</th>
                                             <th>Price</th>
                                             <th>Handicap</th>
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptTable">
                                      <ItemTemplate>
                                            <tr>
                                            <td ><%# Eval("Date") %></td>
                                            <td ><%# Eval("Event") %></td>
                                            <td ><%# Eval("League") %></td>
                                            <td ><%# Eval("name") %></td>
                                            <td ><%# Eval("category") %></td>
                                            <td ><%# Eval("periodNumber") %></td>
                                            <td ><%# Eval("oppName1") %></td>
                                            <td ><%# Eval("price1") %></td>
                                            <td ><%# Eval("handicap1") %></td>
                                            <td ><%# Eval("oppName2") %></td>  
                                            <td ><%# Eval("price2") %></td>
                                            <td ><%# Eval("handicap2") %></td>
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
