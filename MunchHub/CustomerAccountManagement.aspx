<%@ Page Title="Account" Language="C#" MasterPageFile="~/CustomerMasterPage.Master" AutoEventWireup="true" CodeBehind="CustomerAccountManagement.aspx.cs" Inherits="MunchHub.WebForm7" %>
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
    <div class="col-12 d-flex justify-content-center">
        <div class="col-12" style="border-radius:10px;padding:10px;box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);margin-top:50px">
            <h5 style="margin-bottom:20px">Your Profile</h5>
            <hr />
            <div class="col-md-10 d-flex flex-column justify-content-center">
                <small style="color:#707070">Name</small>
                <asp:TextBox ID="nameInput" runat="server" CssClass="form-control" style="margin-bottom:10px"></asp:TextBox>
                <asp:Label ID="nameErrorLabel" runat="server" style="color:#ff0000;margin-bottom:20px;"></asp:Label>
                <small style="color:#707070">Email</small>
                <asp:TextBox ID="emailInput" runat="server" CssClass="form-control" style="margin-bottom:10px"></asp:TextBox>
                <asp:Label ID="emailErrorLabel" runat="server" style="color:#ff0000;margin-bottom:20px;"></asp:Label>
                <small style="color:#707070">Home Address</small>
                <asp:TextBox ID="homeAddressInput" runat="server" CssClass="form-control" style="margin-bottom:40px"></asp:TextBox>
                <asp:Label ID="addressErrorLabel" runat="server" style="color:#ff0000;margin-bottom:20px;"></asp:Label>
                <small style="color:#707070">Change Password</small>
                <asp:TextBox ID="changePasswordInput" runat="server" CssClass="form-control" style="margin-bottom:10px" TextMode="Password"></asp:TextBox>
                <asp:Label ID="passwordErrorLabel" runat="server" style="color:#ff0000;margin-bottom:20px;"></asp:Label>
                <small style="color:#707070">Confirm Change Password</small>
                <asp:TextBox ID="confirmPasswordInput" runat="server" CssClass="form-control" style="margin-bottom:10px" TextMode="Password"></asp:TextBox>
                <asp:Label ID="confirmPasswordErrorLabel" runat="server" style="color:#ff0000;margin-bottom:40px;"></asp:Label>
                <div class="d-flex justify-content-center justify-content-md-start">
                    <asp:Button ID="saveButton" runat="server" CssClass="btn custom-btn-primary" Text="Save Changes" style="margin-right:30px" OnClick="saveButton_Click"/>
                    <asp:Button ID="logoutButton" runat="server" CssClass="btn btn-outline-danger" Text="Logout" style="margin-right:10px" OnClick="logoutButton_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
