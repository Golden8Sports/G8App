<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/menu.Master" CodeBehind="PlayerModule.aspx.cs" Inherits="G8_App.Views.PlayerModule" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Content1">
  <div class="mainpanel">    
    <div class="headerbar">      
      <a class="menutoggle"><i class="fa fa-bars"></i></a>      
      <div class="header-right">     
      </div><!-- header-right -->
      
    </div><!-- headerbar -->
    
    <div class="pageheader">
      <h2><i class="fa fa-home"></i> Player Module <span>Subtitle goes here...</span></h2>
      <div class="breadcrumb-wrapper">
        <ol class="breadcrumb">
          <li class="active" runat="server" id="userName">Dashboard</li>
        </ol>
      </div>      
    </div>
    

    <div class="contentpanel">  
   <!-- content here -->

 <div class="row" id="hola" runat="server">
    <div class="col-xs-12">
       <ul class="nav nav-tabs nav-justified">
        <li class="nav-item active"><a data-toggle="tab" href="#financial">Financial</a></li>
        <li ><a data-toggle="tab" href="#menu2" id="txtLeans" runat="server">Bets</a></li>
        <li><a data-toggle="tab" href="#menu3" id="txtWith" runat="server">Today's  Action</a></li>
      </ul>
    </div>
</div>

        <div class="tab-content">      
         <div id="financial" class="tab-pane fade">
             <div>

             </div>
         </div>
         <div id="menu2" class="tab-pane fade" style="width:100%;">
         </div>
         <div id="menu3" class="tab-pane fade">   
         </div>
        </div>

      
    </div><!-- contentpanel -->
  </div><!-- mainpanel -->

</asp:Content>