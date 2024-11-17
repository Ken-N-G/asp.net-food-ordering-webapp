<%@ Page Title="Login" Language="C#" MasterPageFile="~/GeneralPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MunchHub.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container d-flex justify-content-center align-items-center min-vh-100">
        <div class="row rounded-3 p-3 bg-white shadow" style="width:950px;">
            <div class="col-md-6 rounded-2 d-flex justify-content-center align-items-center flex-column">
                <div class="featured-image">
                    <img src="../Images/delivery-login.jpg" class="img-fluid" style="border-radius:10px"/>
                </div>
            </div>
            <div class="col-md-6 rounded-2 d-flex flex-column">
                <h3 style="margin-bottom:30px;">Welcome user!</h3>
                <asp:Label ID="loginErrorLabel" runat="server" style="color:#ff0000;margin-bottom:20px;"></asp:Label>
                <small>Email</small>
                <asp:TextBox ID="emailInput" runat="server"></asp:TextBox>
                <asp:Label ID="emailErrorLabel" runat="server" style="color:#ff0000"></asp:Label>
                <small>Password</small>
                <asp:TextBox ID="passwordInput" runat="server" TextMode="Password"></asp:TextBox>
                <asp:Label ID="passwordErrorLabel" runat="server" style="color:#ff0000"></asp:Label>
                <a href="Register.aspx" style="color:#0094ff;margin-bottom:20px;">Don't have an account? Register here</a>
                <div class="col">
                    <asp:button ID="btnLogin" runat="server" Text="Login" CssClass="btn custom-btn-primary" OnClick="btnLogin_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
