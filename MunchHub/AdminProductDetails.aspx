<%@ Page Title="Product Details" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminProductDetails.aspx.cs" Inherits="MunchHub.WebForm19" %>
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
    <div class="col-sm-12 d-flex flex-column justify-content-center align-items-center min-vh-100">
        <div class="info-card col-11">
            <asp:Label ID="productErrorLabel" runat="server" style="color:#ff0000;margin-bottom:10px;"></asp:Label>
            <div class="row">
                <div class="col-lg-6 col-sm-12 d-flex flex-column justify-content-center align-items-center">
                    <asp:Image ID="image1" runat="server" CssClass="d-none d-xl-block" style="width:500px;height:500px;object-fit:cover;border-radius:10px"/>
                    <asp:Image ID="image2" runat="server" CssClass="d-sm-block d-xl-none" style="width:300px;height:300px;object-fit:cover;border-radius:10px"/>
                </div>
                <div class="col-lg-6 col-sm-12 d-flex flex-column">
                    <small style="margin-bottom:5px">Product ID</small>
                    <asp:Label ID="productIDLabel" runat="server" style="font-size:18px;;margin-bottom:10px"></asp:Label>
                    <small style="margin-bottom:5px">Product Name</small>
                    <asp:Label ID="productNameLabel" runat="server" style="font-size:18px;;margin-bottom:10px"></asp:Label>
                    <small style="margin-bottom:5px">Price</small>
                    <asp:Label ID="priceLabel" runat="server" style="font-size:18px;margin-bottom:10px"></asp:Label>
                    <small style="margin-bottom:5px">Category</small>
                    <asp:Label ID="categoryLabel" runat="server" style="font-size:18px;margin-bottom:40px"></asp:Label>
                    <small style="margin-bottom:5px">Description</small>
                    <asp:Label ID="descriptionLabel" runat="server" style="font-size:18px;margin-bottom:10px"></asp:Label>
                    <small style="margin-bottom:5px">Short Description</small>
                    <asp:Label ID="shortDescriptionLabel" runat="server" style="font-size:18px;margin-bottom:40px"></asp:Label>
                    <div class="custom-inline-text d-flex justify-content-center">
                        <asp:Button ID="editButton" runat="server" CssClass="btn custom-btn-primary" Text="Edit" style="margin-right:20px" OnClick="editButton_Click"/>
                        <asp:Button ID="deleteButton" runat="server" CssClass="btn btn-outline-danger" Text="Delete" OnClick="deleteButton_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
