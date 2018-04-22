<%@ Page Language="C#" Title="" MasterPageFile="~/Views/menu.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="G8_App.Views.dashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

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
      
      <div class="row">
        	<div class="col-xs-12">
                <!-- HERE WILL BE PUT THE MAIN INFOMRATION -->

                    
          </div>
      </div>
    </div><!-- contentpanel -->
  </div><!-- mainpanel -->



</asp:Content>
