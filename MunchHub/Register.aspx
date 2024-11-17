<%@ Page Title="Register" Language="C#" MasterPageFile="~/GeneralPage.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="MunchHub.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container d-flex justify-content-center align-items-center min-vh-100">
        <div class="col-xxl-3 rounded-2 d-flex flex-column p-3 bg-white shadow">
            <h3 style="margin-bottom:30px;">Register</h3>
            <small>Name</small>
            <asp:TextBox ID="nameInput" runat="server"></asp:TextBox>
            <asp:Label ID="nameErrorLabel" runat="server" style="color:#ff0000;margin-bottom:20px;"></asp:Label>
            <small>Email</small>
            <asp:TextBox ID="emailInput" runat="server"></asp:TextBox>
            <asp:Label ID="emailErrorLabel" runat="server" style="color:#ff0000;margin-bottom:20px;"></asp:Label>
            <small>Home Address</small>
            <asp:TextBox ID="homeAddressInput" runat="server"></asp:TextBox>
            <asp:Label ID="addressErrorLabel" runat="server" style="color:#ff0000;margin-bottom:20px;"></asp:Label>
            <div style="margin-bottom:20px"></div>
            <small>Password</small>
            <asp:TextBox ID="passwordInput" runat="server" TextMode="Password"></asp:TextBox>
            <asp:Label ID="passwordErrorLabel" runat="server" style="color:#ff0000;margin-bottom:20px;"></asp:Label>
            <small>Confirm Password</small>
            <asp:TextBox ID="conPasswordInput" runat="server" TextMode="Password"></asp:TextBox>
            <asp:Label ID="confirmPasswordErrorLabel" runat="server" style="color:#ff0000;margin-bottom:20px;"></asp:Label>
            <a href="Login.aspx" style="color:#0094ff;margin-bottom:20px;">Already have an account? Login here</a>
            <div class="col d-flex flex-column align-items-center">
                <asp:button ID="btnRegister" runat="server" Text="Register" CssClass="btn custom-btn-primary" OnClick="btnRegister_Click"/>
            </div>
        </div>
</div>
</asp:Content>
