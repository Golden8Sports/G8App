<%@ Page Language="C#" MasterPageFile="~/Views/menu.Master" AutoEventWireup="true" CodeBehind="RangesReport.aspx.cs" Inherits="G8_App.Views.RangesReport" %>


<asp:Content runat="server" ContentPlaceHolderID="head1" ID="headContent">
   <script lang=Javascript>
      function isNumberKey(evt)
      {
         var charCode = (evt.which) ? evt.which : evt.keyCode;
         if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;    
         return true;
      }
   </script>
</asp:Content>



<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Content1">
      <div class="mainpanel">
    
    <div class="headerbar">
      
      <a class="menutoggle"><i class="fa fa-bars"></i></a>
      
      <div class="header-right">

      
      </div><!-- header-right -->
      
    </div><!-- headerbar -->
    
    <div class="pageheader">
      <h2><i class="glyphicon glyphicon-flash"></i> Ranges Report <span>Subtitle goes here...</span></h2>
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

                            <label class="col-sm-1 control-label">Sport:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inSport" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FillLeague"  CssClass="form-control chosen-select">
                                </asp:DropDownList>
                            </div>                         
                        </div>


                      <div class="form-group">

                            <label class="col-sm-1 control-label">League:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inLeague" runat="server"  CssClass="form-control chosen-select">
                                </asp:DropDownList>
                            </div>

                            <label class="col-sm-1 control-label">Team:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inTeam" runat="server"  CssClass="form-control chosen-select">
                                </asp:DropDownList>
                            </div>


                            <label class="col-sm-1 control-label">At:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inOrder" runat="server"  CssClass="form-control chosen-select">
                                    <asp:ListItem Value="B" >BOTH</asp:ListItem>
                                    <asp:ListItem Value="A" >AWAY</asp:ListItem>
                                    <asp:ListItem Value="H" >HOME</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                         </div>

                      <div class="form-group">

                            <label class="col-sm-1 control-label">As:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inRef" runat="server"  CssClass="form-control chosen-select">
                                    <asp:ListItem Value="Fav" >FAV</asp:ListItem>
                                    <asp:ListItem Value="Dog" >DOG</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <label class="col-sm-1 control-label">Min Range</label>
                            <div class="col-sm-1">
                              <div class="input-group">
                                  <input type="text" onkeypress="return isNumberKey(event)" class="form-control" id="inMinRange" runat="server" name="inMinRange" autocomplete="off" />
                              </div>
                            </div>
                           
                            <label class="col-sm-1 control-label">Max Range</label>
                            <div class="col-sm-1">
                              <div class="input-group">
                                  <input type="text" onkeypress="return isNumberKey(event)" class="form-control" id="inMaxRange" runat="server" name="inMaxRange" autocomplete="off" />
                              </div>
                            </div>
                        
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
                  <h3 class="panel-title">Grouped</h3>
                </div>


                <div class="panel-body">
                         <div class="table-responsive" style="overflow-x:auto;">
                           <table class="table" id="table2" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th>Date</th>
                                             <th>Visitor Team</th>
                                             <th>Home Team</th>
                                             <th>At</th>
                                             <th>As</th>
                                             <th>Sport</th>
                                             <th>Range</th>
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