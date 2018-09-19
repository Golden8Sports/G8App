<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/menu.Master" CodeBehind="Scraping.aspx.cs" Inherits="G8_App.Views.Scraping" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Content1">
      <div class="mainpanel">
    
    <div class="headerbar">
      
      <a class="menutoggle"><i class="fa fa-bars"></i></a>
      
      <div class="header-right">

      
      </div><!-- header-right -->
      
    </div><!-- headerbar -->
    
    <div class="pageheader">
      <h2><i class="fa fa-wrench"></i> Scraping Covers <span>Subtitle goes here...</span></h2>
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
                            <input type="text" class="form-control" placeholder="url" id="url" runat="server" name="url" required="required" value="" />
                            </div>
                        </div>
                        <label class="col-sm-7 control-label"></label>
                        <div class="col-sm-2">
                           <asp:Button class="btn btn-block btn-success" runat="server" type="submit" Text="Scrap" OnClick="ScrapMiniPage"/>
                        </div>

                        <div class="col-sm-2">
                           <asp:Button class="btn btn-block btn-danger" runat="server" type="submit" Text="CSV" OnClick="ScrapExcel"/>
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
                  <h3 class="panel-title">Result:</h3>
                </div>


                <div class="panel-body">
                         <div class="table-responsive" style="overflow-x:auto;">
                           <table class="table" id="table1" style="overflow:auto;">
                                     <thead style="margin:auto;">
                                         <tr>
                                             <th>Date</th>
                                             <th>Vs</th>
                                             <th>Result</th>
                                             <th>AwayStarter</th>
                                             <th>HomeStarter</th>
                                             <th>Line</th>
                                             <th>O/U</th>
                                             <th>HomeScore</th>
                                             <th>AwayScore</th>
                                         </tr>
                                      </thead>
                                  <asp:Repeater runat="server" ID="rptTable">
                                      <ItemTemplate>
                                            <tr>
                                            <td ><%# Eval("Date") %></td>
                                            <td ><%# Eval("VS") %></td>
                                            <td ><%# Eval("Result") %></td>
                                            <td ><%# Eval("AwayStarter") %></td>
                                            <td ><%# Eval("HomeStarter") %></td>
                                            <td ><%# Eval("AriLine") %></td>
                                            <td ><%# Eval("OU") %></td>
                                            <td ><%# Eval("HomeScore") %></td>
                                            <td ><%# Eval("AwayScore") %></td>
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



