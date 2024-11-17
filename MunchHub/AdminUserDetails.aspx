<%@ Page Title="User Details" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminUserDetails.aspx.cs" Inherits="MunchHub.WebForm16" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightMarginWidgets" runat="server">
    <ul class="navbar-nav mr-auto">
        <li class="nav-item d-flex align-items-center"><asp:Button ID="logoutButton" CssClass="btn btn-outline-danger" Text="Logout" runat="server" OnClick="logoutButton_Click" style="margin-left:30px" /></li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PreMainContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-12 d-flex justify-content-center">
        <div class="col-12" style="border-radius:10px;padding:10px;box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);margin-top:50px">
            <asp:Label ID="userIDLabel" runat="server" style="font-size:32px;font-weight:600;margin-bottom:20px"></asp:Label>
            <hr />
            <div class="col-md-10 d-flex flex-column justify-content-center">
                <small style="color:#707070">Name</small>
                <asp:TextBox ID="nameInput" runat="server" style="margin-bottom:10px"></asp:TextBox>
                <asp:Label ID="nameErrorLabel" runat="server" style="color:#ff0000;margin-bottom:20px;"></asp:Label>
                <small style="color:#707070">Email</small>
                <asp:TextBox ID="emailInput" runat="server" style="margin-bottom:10px"></asp:TextBox>
                <asp:Label ID="emailErrorLabel" runat="server" style="color:#ff0000;margin-bottom:20px;"></asp:Label>
                <small style="color:#707070">Home Address</small>
                <asp:TextBox ID="homeAddressInput" runat="server" style="margin-bottom:40px"></asp:TextBox>
                <asp:Label ID="addressErrorLabel" runat="server" style="color:#ff0000;margin-bottom:20px;"></asp:Label>
                <small style="color:#707070">Change Password</small>
                <asp:TextBox ID="changePasswordInput" runat="server" style="margin-bottom:10px" TextMode="Password"></asp:TextBox>
                <asp:Label ID="passwordErrorLabel" runat="server" style="color:#ff0000;margin-bottom:20px;"></asp:Label>
                <small style="color:#707070">Confirm Change Password</small>
                <asp:TextBox ID="confirmPasswordInput" runat="server" style="margin-bottom:10px" TextMode="Password"></asp:TextBox>
                <asp:Label ID="confirmPasswordErrorLabel" runat="server" style="color:#ff0000;margin-bottom:40px;"></asp:Label>
                <div class="d-flex justify-content-center justify-content-md-start">
                    <asp:Button ID="saveButton" runat="server" CssClass="btn custom-btn-primary" Text="Save Changes" style="margin-right:30px" OnClick="saveButton_Click"/>
                    <asp:Button ID="suspendButton" runat="server" CssClass="btn btn-outline-danger" Text="Suspend User" style="margin-right:10px" OnClick="suspendButton_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
