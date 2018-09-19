<%@ Page Language="C#" Title="" MasterPageFile="~/Views/menu.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="G8_App.Views.dashboard" %>


<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Content1">
  <div class="mainpanel">    
    <div class="headerbar">      
      <a class="menutoggle"><i class="fa fa-bars"></i></a>      
      <div class="header-right">     
      </div><!-- header-right -->
      
    </div><!-- headerbar -->
    
    <div class="pageheader">
      <h2><i class="fa fa-home"></i> Dashboard <span>Subtitle goes here...</span></h2>
      <div class="breadcrumb-wrapper">
        <ol class="breadcrumb">
          <li class="active" runat="server" id="userName">Dashboard</li>
        </ol>
      </div>      
    </div>
    

    <div class="contentpanel">  

   <!-- content here -->
      
    </div><!-- contentpanel -->
  </div><!-- mainpanel -->

</asp:Content>