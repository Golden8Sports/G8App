<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/menu.Master" CodeBehind="ProfilingPlayer.aspx.cs" Inherits="G8_App.Views.ProfilingPlayer" %>


<asp:Content runat="server" ContentPlaceHolderID="head1" ID="head1">

  <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
  <script type="text/javascript">
    google.charts.load('current', {packages: ['corechart']});
    google.charts.setOnLoadCallback(drawChart);

      function drawChart()
      {
          var data = new google.visualization.arrayToDataTable(<%=GetData()%>);
          var chart = new google.visualization.PieChart(document.getElementById('myPieChart'));
          var options = { title: 'Action By Wager Type',is3D: true,};
          chart.draw(data, options);

          //Visitor/Home
          var data2 = new google.visualization.arrayToDataTable(<%=HomeVisitor()%>);
          var chart2 = new google.visualization.PieChart(document.getElementById('ViHomChart'));
          var options2 = { title: 'Visitor/Home',is3D: true,};
          chart2.draw(data2, options2);

          //Moment day
          var data3 = new google.visualization.arrayToDataTable(<%=MomentDay()%>);
          var chart3 = new google.visualization.PieChart(document.getElementById('mdChart'));
          var options3 = { title: 'Moment Day',is3D: true,};
          chart3.draw(data3, options3);

          //Result
          var data4 = new google.visualization.arrayToDataTable(<%=Result()%>);
          var chart4 = new google.visualization.PieChart(document.getElementById('rChart'));
          var options4 = { title: 'Result',is3D: true,};
          chart4.draw(data4, options4);

          //fav/dog
          var data5 = new google.visualization.arrayToDataTable(<%=FavDog()%>);
          var chart5 = new google.visualization.PieChart(document.getElementById('rFavDog'));
          var options5 = { title: 'Fav/Dog',is3D: true,};
          chart5.draw(data5, options5);

          //acount
          var data6 = new google.visualization.arrayToDataTable(<%=Acount()%>);
          var chart6 = new google.visualization.PieChart(document.getElementById('rAcount'));
          var options6 = { title: 'Values',is3D: true,};
          //chart6.draw(data6, options6);

          //acount
          var data7 = new google.visualization.arrayToDataTable(<%=LineMoved()%>);
          var chart7 = new google.visualization.PieChart(document.getElementById('rLine'));
          var options7 = { title: 'Line Moved',is3D: true,};
          chart7.draw(data7, options7);


          //live
          var data8 = new google.visualization.arrayToDataTable(<%=Live()%>);
          var chart8 = new google.visualization.PieChart(document.getElementById('rLive'));
          var options8 = { title: 'Lives',is3D: true,};
          chart8.draw(data8, options8);


          //buy
          var data9 = new google.visualization.arrayToDataTable(<%=Buy()%>);
          var chart9 = new google.visualization.PieChart(document.getElementById('rBuy'));
          var options9 = { title: 'Points Purchased',is3D: true,};
          chart9.draw(data9, options9);
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
      <h2><i class="fa fa-user"></i> Profiling by Player <span>Subtitle goes here...</span></h2>
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
                          <label class="col-sm-2 control-label">Start Date:</label>
                            <div class="col-sm-3">
                              <div class="input-group">
                                <input type="text" class="form-control" placeholder="mm/dd/yyyy" id="startDate" runat="server" name="startDate" required="required" autocomplete="off" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                              </div>
                            </div>

                            <label class="col-sm-2 control-label">End Date:</label>
                            <div class="col-sm-3">
                              <div class="input-group">
                                  <input type="text" class="form-control" placeholder="mm/dd/yyyy" id="endDate" runat="server" name="endDate" required="required" autocomplete="off" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                              </div>
                            </div>

                        </div>
                        

                        <div class="form-group">

                          <label class="col-sm-2 control-label">Player:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="inPlayer" runat="server"  CssClass="form-control chosen-select">
                                </asp:DropDownList>
                            </div>

                            <label class="col-sm-3 control-label"></label>

                            <div class="col-sm-2">
                                <asp:Button class="btn btn-block btn-success" runat="server" type="submit" Text="Run" OnClientClick="drawChart"/>
                            </div>
                        </div>
                  </form>    

                </div>
              </div>
          </div>
        </div>
      </div> <!-- Fin Row -->


        <!--   INFORTMATION   -->

           <div class="row">
               <div class="panel panel-default">
                <div class="panel-heading">
                  <div class="panel-btns">
                    <a href="#" class="panel-close">×</a>
                    <a href="#" class="minimize">−</a>
                  </div>
                  <h3 class="panel-title">Wager Type</h3>
                </div>
 
               <div class="panel-body">
                 <div class="table-responsive" style="overflow-x:auto;">
                    <div id="myPieChart" style="width:auto; height: 90vh;"></div>
                    <div id="ViHomChart" style="width:300px; height:300px; float:left;"></div>
                    <div id="mdChart" style="width:300px; height:300px; float:left;"></div>
                    <div id="rChart" style="width:300px; height:300px; float:left;"></div>
                    <div id="rFavDog" style="width:300px; height:300px; float:left;"></div>
                    <div id="rAcount" style="width:300px; height:300px; float:left;"></div>
                    <div id="rLine" style="width:300px; height:300px; float:left;"></div>
                    <div id="rLive" style="width:300px; height:300px; float:left;"></div>
                    <div id="rBuy" style="width:300px; height:300px; float:left; font-size:medium;"></div>
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
                  <h3 class="panel-title">History</h3>
                </div>
                <div class="panel-body">	  
                  <div class="table-responsive" style="overflow-x:auto;">
                    <div id="line_top_x"/>
                  </div>  
                </div>
            </div>
        </div>
        

    </div><!-- contentpanel -->
  </div><!-- mainpanel -->



</asp:Content>

