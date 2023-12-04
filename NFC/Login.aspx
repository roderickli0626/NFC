<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NFC.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <title>NFC</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport">
    <meta content="" name="keywords">
    <meta content="" name="description">

    <!-- Favicon -->
    <link href="Content/Images/logo.png" rel="icon">

    <!-- Google Web Fonts -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@400;600&family=Roboto:wght@500;700&display=swap" rel="stylesheet"> 
    
    <!-- Icon Font Stylesheet -->
    <link href="Content/CSS/all.min.css" rel="stylesheet">
    <link href="Content/CSS/bootstrap-icons.css" rel="stylesheet">

    <!-- Libraries Stylesheet -->
    <link href="Content/CSS/owl.carousel.min.css" rel="stylesheet">
    <link href="Content/CSS/tempusdominus-bootstrap-4.min.css" rel="stylesheet" />

    <!-- Customized Bootstrap Stylesheet -->
    <link href="Content/CSS/bootstrap.min.css" rel="stylesheet">

    <!-- Template Stylesheet -->
    <link href="Content/CSS/style.css" rel="stylesheet">
</head>
<body>
    <div class="container-fluid position-relative d-flex p-0">
        <!-- Spinner Start -->
        <div id="spinner" class="show bg-dark position-fixed translate-middle w-100 vh-100 top-50 start-50 d-flex align-items-center justify-content-center">
            <div class="spinner-border text-primary" style="width: 3rem; height: 3rem;" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
        <!-- Spinner End -->


        <!-- Sign In Start -->
        <div class="container-fluid">
            <form id="form1" runat="server">
                <div class="row h-100 align-items-center justify-content-center" style="min-height: 100vh;">
                    <div class="col-12 col-sm-8 col-md-6 col-lg-5 col-xl-4">
                        <div class="bg-secondary rounded p-4 p-sm-5 my-4 mx-3">
                            <div class="d-flex align-items-center justify-content-between mb-3">
                                <a href="index.html" class="">
                                    <h3 class="text-primary"><i class="fa fa-credit-card me-2"></i>NFC</h3>
                                </a>
                                <h3>Sign In</h3>
                            </div>
                            <div class="form-floating mb-3">
                                <asp:TextBox runat="server" ID="TxtEmail" ClientIDMode="Static" CssClass="form-control text-white" placeholder="Email" TextMode="Email"></asp:TextBox>
                                <label for="TxtEmail">Email address</label>
                            </div>
                            <div class="form-floating mb-4">
                                <asp:TextBox runat="server" ID="TxtPassword" ClientIDMode="Static" placeholder="Password" CssClass="form-control text-white" TextMode="Password"></asp:TextBox>
                                <label for="TxtPassword">Password</label>
                            </div>
                            <asp:Button runat="server" ID="BtnSignIn" CssClass="btn btn-primary py-3 w-100 mb-4" Text="Sign In" OnClick="BtnSignIn_Click" />

                            <asp:ValidationSummary ID="ValSummary" runat="server" CssClass="mt-lg mb-lg text-left bg-gradient" ClientIDMode="Static" />
                            <asp:RequiredFieldValidator ID="ReqValEmail" runat="server" ErrorMessage="Inserire l'email." CssClass="text-bg-danger" ControlToValidate="TxtEmail" Display="None"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="ReqValPassword" runat="server" ErrorMessage="Inserire la password." CssClass="text-black" ControlToValidate="TxtPassword" Display="None"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="ServerValidator" runat="server" ErrorMessage="Email o Password errata." Display="None"></asp:CustomValidator>
                        </div>
                    </div>
                </div>
            </form>
        </div>
        <!-- Sign In End -->
    </div>

    <!-- JavaScript Libraries -->
    <script src="Scripts/JS/jquery-3.4.1.min.js"></script>
    <script src="Scripts/JS/bootstrap.bundle.min.js"></script>
    <script src="Scripts/JS/chart.min.js"></script>
    <script src="Scripts/JS/easing.min.js"></script>
    <script src="Scripts/JS/waypoints.min.js"></script>
    <script src="Scripts/JS/owl.carousel.min.js"></script>
    <script src="Scripts/JS/moment.min.js"></script>
    <script src="Scripts/JS/moment-timezone.min.js"></script>
    <script src="Scripts/JS/tempusdominus-bootstrap-4.min.js"></script>

    <script src="Scripts/JS/main.js"></script>
</body>
</html>
