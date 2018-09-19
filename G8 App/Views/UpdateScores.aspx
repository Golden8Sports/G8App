<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/menu.Master" CodeBehind="UpdateScores.aspx.cs" Inherits="G8_App.Views.UpdateScores" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Content1">
  <div class="mainpanel">    
    <div class="headerbar">      
      <a class="menutoggle"><i class="fa fa-bars"></i></a>      
      <div class="header-right">     
      </div><!-- header-right -->   
    </div><!-- headerbar -->
    
    <div class="pageheader">
      <h2><i class="fa fa-home"></i> Update Scores <span>Subtitle goes here...</span></h2>
      <div class="breadcrumb-wrapper">
        <ol class="breadcrumb">
          <li class="active" runat="server" id="userName">Dashboard</li>
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

                            <label class="col-sm-1 control-label">Date</label>
                            <div class="col-sm-3">
                              <div class="input-group">
                                <input type="text" class="form-control" placeholder="mm/dd/yyyy" id="startDate" runat="server" name="startDate" required="required" autocomplete="off" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                              </div>
                            </div>

                        <div class="col-sm-2">
                            <asp:Button class="btn btn-block btn-success" ID="btnRun" runat="server" type="submit" Text="Update" OnClick="Update" />
                        </div>
                  </form>    
                </div>
                  
              </div>
                    <h4 id="idStaus" runat="server"></h4>
          </div>
        </div>
      </div>    
    </div><!-- contentpanel -->
  </div><!-- mainpanel -->



</asp:Content>