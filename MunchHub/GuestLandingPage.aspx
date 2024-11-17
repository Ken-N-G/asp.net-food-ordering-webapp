<%@ Page Title="Home" Language="C#" MasterPageFile="~/GuestMasterPage.Master" AutoEventWireup="true" CodeBehind="GuestLandingPage.aspx.cs" Inherits="MunchHub.GuestLandingPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightMarginWidgets" runat="server">
    <ul class="navbar-nav mr-auto">
        <li class="nav-item"><a class="nav-link" runat="server" href="CustomerCart.aspx"><i class="bi bi-basket" style="font-size: 24px;"></i></a></li>
        <li class="nav-item d-flex align-items-center"><a class="btn custom-btn-outline-primary" runat="server" href="Login.aspx" style="margin-left:30px">Login</a></li>
        <li class="nav-item d-flex align-items-center"><a class="btn custom-btn-primary" runat="server" href="Register.aspx" style="margin-left:10px;">Register</a></li>
    </ul>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PreMainContent" runat="server">
    <section id="hero" class="min-vh-100 d-flex align-items-center text-center">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <h1 style="color:white">Ready to Munch?</h1>
                    <h5 style="color:white">Order your favorite meals and munch on with MunchHub!</h5>
                    <a href="~/Register" runat="server" class="btn custom-btn-primary" style="margin-top:20px;">Get Started</a>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
        <div class="row min-vh-100 d-flex align-items-center">
            <div class="col-lg-4 col-md-6 col-sm-12 d-flex justify-content-center text-center">
                <div class="info-card" style="width:350px;height: 350px;">
                    <i class="bi bi-journal-richtext" style="font-size: 64px"></i>
                    <h3 style="margin-bottom:20px">Varied Meals</h3>
                    <p>Browse through our wide and diverse selection of meals and beverages designed to leave a lasting impression on you</p>
                </div>
            </div>
            <div class="col-lg-4 col-md-6 col-sm-12 d-flex justify-content-center text-center">
                <div class="info-card" style="width:350px;height: 350px;">
                    <i class="bi bi-cup-hot-fill" style="font-size: 64px"></i>
                    <h3 style="margin-bottom:20px">Made To Order</h3>
                    <p>All our food are made as soon as you place your order, ensuring you always get the freshest meals</p>
                </div>
            </div>
            <div class="col-lg-4 col-md-12 col-sm-12 d-flex justify-content-center text-center">
                <div class="info-card" style="width:350px;height: 350px;">
                    <i class="bi bi-hourglass-split" style="font-size: 64px"></i>
                    <h3 style="margin-bottom:20px">Speedy Delivery</h3>
                    <p>Our 45-minute policy and street-smart drivers always make sure your food gets there as soon as possible</p>
                </div>
            </div>
        </div>
</asp:Content>
