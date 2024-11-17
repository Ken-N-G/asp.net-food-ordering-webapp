<%@ Page Title="Payment Confirm" Language="C#" MasterPageFile="~/CustomerMasterPage.Master" AutoEventWireup="true" CodeBehind="CustomerPaymentConfirmation.aspx.cs" Inherits="MunchHub.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightMarginWidgets" runat="server">
    <ul class="navbar-nav mr-auto">
        <li class="nav-item"><a class="nav-link" runat="server" href="CustomerCart.aspx"><i class="bi bi-basket" style="font-size: 24px;"></i></a></li>
        <li class="nav-item"><a class="nav-link" runat="server" href="CustomerAccountManagement.aspx"><i class="bi bi-person-circle" style="font-size: 24px;"></i></a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PreMainContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-sm-12 d-flex flex-column justify-content-center align-items-center min-vh-100">
        <div class="info-card col-lg-8 col-md-8 col-sm-12 align-items-center" >
            <h1 style="margin-bottom:50px;text-align:center">Payment Successful!</h1>
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 d-flex align-items-center justify-content-center">
                    <img src="Images/Check_green_icon.svg"/>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 d-flex flex-column">
                    <asp:Label ID="orderIDLabel" runat="server" style="font-size:32px;font-weight:600;margin-bottom:20px"></asp:Label>
                    <div class="custom-inline-text">
                        <p style="margin-right:10px;">Date & Time Ordered:</p>
                        <asp:Label ID="dateOrderedLabel" runat="server"></asp:Label>
                    </div>
                    <div class="custom-inline-text">
                        <p style="margin-right:10px;">Order Status:</p>
                        <asp:Label ID="statusLabel" runat="server"></asp:Label>
                    </div>
                    <div class="custom-inline-text" style="margin-bottom:auto">
                        <p style="margin-right:10px;">Total:</p>
                        <asp:Label ID="totalPriceLabel" runat="server"></asp:Label>
                    </div>
                    <div style="align-items:center;justify-content:center;display:flex;">
                        <asp:Button ID="reviewOrderButton" runat="server" Text="Review Your Order" CssClass="btn custom-btn-primary" OnClick="reviewOrderButton_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
