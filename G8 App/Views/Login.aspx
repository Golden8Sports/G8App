<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="G8_App.Views.Login" %>


<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta name="description" content="">
  <meta name="author" content="">
  <link rel="shortcut icon" href="/Img/favicon.png" type="image/png">


  <title>G8 Apps</title>

  <link href="/css/style.default.css" rel="stylesheet">

</head>

<body class="signin">
     
<!-- Preloader -->
<div id="preloader">
    <div id="status"><i class="fa fa-spinner fa-spin"></i></div>
</div>

<section>
  
    <div class="signinpanel">
        
        <div class="row">
            
            <div class="col-md-7">
                
                <div class="signin-info">
                    <div class="logopanel">
                        <h1><span>[</span> G8 Apps <span>]</span></h1>
                    </div><!-- logopanel -->
                </div><!-- signin0-info -->
            
            </div><!-- col-sm-7 -->
            
            <div class="col-md-5">
                
                <form method="post" runat="server">
                    <h4 class="nomargin">Sign In</h4>
                    <p class="mt5 mb20">Login to access your account.</p>
                
                    <input type="text" class="form-control uname" placeholder="Username" runat="server" name="u" id="u" style="background: #fff url(/Img/user.png) no-repeat 95% center;" />
                    <input type="password" class="form-control pword" placeholder="Password" runat="server" name="p" id="p" style="background: #fff url(/Img/locked.png) no-repeat 95% center;" />
                    <asp:Button class="btn btn-success btn-block" runat="server" type="submit" Text="Sign in" OnClick="ClickLogin"/>
                    
                </form>
            </div><!-- col-sm-5 -->
            
        </div><!-- row -->
        
    </div><!-- signin -->
  
</section>

<script src="/js/jquery-1.10.2.min.js"></script>
<script src="/js/jquery-migrate-1.2.1.min.js"></script>
<script src="/js/bootstrap.min.js"></script>
<script src="/js/modernizr.min.js"></script>
<script src="/js/retina.min.js"></script>
<script src="/js/custom.js"></script>

</body>
</html>