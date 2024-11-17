<%@ Page Title="Product Details" Language="C#" MasterPageFile="~/CustomerMasterPage.Master" AutoEventWireup="true" CodeBehind="CustomerProductDetails.aspx.cs" Inherits="MunchHub.WebForm10" %>
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
        <div class="info-card col-11">
            <div class="row">
                <div class="col-lg-6 col-sm-12 d-flex flex-column justify-content-center align-items-center">
                    <asp:Image ID="image1" runat="server" CssClass="d-none d-xl-block" style="width:500px;height:500px;object-fit:cover;border-radius:10px"/>
                    <asp:Image ID="image2" runat="server" CssClass="d-sm-block d-xl-none" style="width:300px;height:300px;object-fit:cover;border-radius:10px"/>
                </div>
                <div class="col-lg-6 col-sm-12 d-flex flex-column">
                    <asp:Label ID="productNameLabel" runat="server" style="font-size:32px;font-weight:400;margin-bottom:40px"></asp:Label>
                    <asp:Label ID="priceLabel" runat="server" style="font-size:22px;margin-bottom:10px"></asp:Label>
                    <asp:Label ID="categoryLabel" runat="server" style="font-size:16px;margin-bottom:10px"></asp:Label>
                    <asp:Label ID="descriptionLabel" runat="server" style="font-size:16px;margin-bottom:10px"></asp:Label>
                    <div class="custom-inline-text d-flex justify-content-center">
                        <asp:LinkButton ID="removeItemButton" runat="server" style="color:#ffae30;margin-right:10px" OnClick="removeItemButton_Click">
                            <i class="bi bi-dash-circle" style="font-size:36px"></i>
                        </asp:LinkButton>
                        <asp:Label ID="quantityLabel" runat="server" CssClass="d-flex align-items-center justify-content-center"></asp:Label>
                        <asp:LinkButton ID="addItemButton" runat="server" style="color:#ffae30;margin-left:10px" OnClick="addItemButton_Click">
                            <i class="bi bi-plus-circle" style="font-size:36px"></i>
                        </asp:LinkButton>
                    </div>
                    <div class="d-flex justify-content-center" style="margin-top:30px">
                        <asp:Button ID="addToCartButton" runat="server" CssClass="btn custom-btn-primary" Text="Add To Cart" OnClick="addToCartButton_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
