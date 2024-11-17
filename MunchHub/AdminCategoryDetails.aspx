<%@ Page Title="Category Details" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminCategoryDetails.aspx.cs" Inherits="MunchHub.WebForm18" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightMarginWidgets" runat="server">
    <ul class="navbar-nav mr-auto">
        <li class="nav-item d-flex align-items-center"><asp:Button ID="logoutButton" CssClass="btn btn-outline-danger" runat="server" Text="Logout"  OnClick="logoutButton_Click" style="margin-left:30px" /></li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PreMainContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-sm-12 d-flex flex-column justify-content-center align-items-center min-vh-100">
        <div class="info-card col-lg-4 col-8 d-flex flex-column align-items-center" >
            <asp:Label ID="categoryLabel" runat="server" style="margin-bottom:50px;text-align:center;font-size:32px;font-weight:600"></asp:Label>
            <small>Category Name</small>
            <asp:TextBox ID="nameInput" runat="server"></asp:TextBox>
            <asp:Label ID="nameErrorLabel" runat="server" style="color:#ff0000;margin-bottom:20px;"></asp:Label>
            <div style="align-items:center;justify-content:center;display:flex;">
                <asp:Button ID="saveChangesButton" runat="server" Text="Save Change" CssClass="btn custom-btn-primary" OnClick="saveChangesButton_Click"/>
            </div>
        </div>
    </div>
</asp:Content>
