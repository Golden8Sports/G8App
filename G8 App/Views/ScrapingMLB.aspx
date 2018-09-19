<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/menu.Master" CodeBehind="ScrapingMLB.aspx.cs" Inherits="G8_App.Views.ScrapingMLB" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Content1">
      <div class="mainpanel">
    
    <div class="headerbar">
      
      <a class="menutoggle"><i class="fa fa-bars"></i></a>
      
      <div class="header-right">

      
      </div><!-- header-right -->
      
    </div><!-- headerbar -->
    
    <div class="pageheader">
      <h2><i class="fa fa-wrench"></i> Scraping MLB <span>Subtitle goes here...</span></h2>
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
                  <h3 class="panel-title">Past Results</h3>
                </div>

                <div class="panel-body">
                  <div class="row">
                    <div class="col-xs-12">			  

                  <form class="form-horizontal form-bordered" method="post" runat="server">                     
                        <div class="form-group">
                          <label class="col-sm-1 control-label">Url:</label>
                            <div class="col-sm-10">
                            <input type="text" class="form-control" placeholder="url" id="url" runat="server" name="url" required="required" value="https://rotogrinders.com/pages/mlb-team-stats-season-standard-269602" />
                            </div>
                        </div>

                      <label class="col-sm-7 control-label"></label>
                        <div class="col-sm-2">
                           <asp:Button class="btn btn-block btn-danger" runat="server" type="submit" Text="Populate" OnClick="SaveToDB"/>
                        </div>

                        <div class="col-sm-2">
                           <asp:Button class="btn btn-block btn-success" runat="server" type="submit" Text="Scrap" OnClick="Scrap"/>
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
                  <h3 class="panel-title">Records</h3>
                </div>


                <div class="panel-body">
                         <div class="table-responsive" style="overflow-x:auto;">
                           <table class="table" id="table1" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th>Name</th>
                                             <th>AB</th>
                                             <th>PA</th>
                                             <th>H</th>
                                             <th>1B</th>
                                             <th>2B</th>
                                             <th>3B</th>
                                             <th>HR</th>
                                             <th>R</th>
                                             <th>RBI</th>
                                             <th>BB</th>
                                             <th>K</th>
                                             <th>SF</th>
                                             <th>GDP</th>
                                             <th>SB</th>
                                             <th>AVG</th>
                                             <th>OBP</th>
                                             <th>SLG</th>
                                             <th>OPS</th>
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptTable">
                                      <ItemTemplate>
                                            <tr>
                                            <td ><%# Eval("Name") %></td>
                                            <td ><%# Eval("AB") %></td>
                                            <td ><%# Eval("PA") %></td>
                                            <td ><%# Eval("H") %></td>
                                            <td ><%# Eval("B1") %></td>
                                            <td ><%# Eval("B2") %></td>
                                            <td ><%# Eval("B3") %></td>
                                            <td ><%# Eval("HR") %></td>
                                            <td ><%# Eval("R") %></td>
                                            <td ><%# Eval("RBI") %></td>
                                            <td ><%# Eval("BB") %></td>
                                            <td ><%# Eval("K") %></td>
                                            <td ><%# Eval("SF") %></td>
                                            <td ><%# Eval("GDP") %></td>
                                            <td ><%# Eval("SB") %></td>
                                            <td ><%# Eval("AVG") %></td>
                                            <td ><%# Eval("OBP") %></td>
                                            <td ><%# Eval("SLG") %></td>
                                            <td ><%# Eval("OPS") %></td>
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