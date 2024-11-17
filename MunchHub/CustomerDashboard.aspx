<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/CustomerMasterPage.Master" AutoEventWireup="true" CodeBehind="CustomerDashboard.aspx.cs" Inherits="MunchHub.WebForm3" %>
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
    <div class="col-12">
        <h1 ID="header" runat="server" style="margin-bottom:50px"></h1>
    </div>
    <h3>Menu Showcase</h3>
    <div class="row d-flex justify-content-center" style="margin-bottom:50px;">
        <div class="info-card col-md-7 d-flex flex-column me-md-2 mb-md-0 mb-2 align-items-center" style="height:420px">
            <asp:Image ID="itemOfTheDayImg" runat="server" style="height:300px;width:100%;object-fit:cover;border-radius:20px;margin-bottom:10px"/>
            <asp:Label ID="itemOfTheDayLabel" runat="server" style="font-size:32px"></asp:Label>
        </div>
        <div class="info-card col-md-4 d-flex flex-column" style="height:420px">
            <h5 style="margin-bottom:30px;">Current Deliveries</h5>
            <asp:Panel ID="noOrderPanel" runat="server" CssClass="col d-flex flex-column justify-content-center align-items-center">
                <span style="margin-bottom:10px">You have not made an order!</span>
                <a href="CustomerMenu.aspx" class="btn custom-btn-outline-primary">Make one here</a>
            </asp:Panel>
            <asp:Panel ID="orderPanel" runat="server" CssClass="col-12 d-flex flex-column">
                <asp:ListView ID="deliveryListView" runat="server" OnItemCommand="deliveryListView_itemCommand">
                    <ItemTemplate>
                        <div class="custom-inline-text d-flex align-items-center" style="margin-bottom:10px">
                            <asp:Label ID="quantityLabel" runat="server" style="margin-right:10px"><%# Eval("quantity").ToString() %>x</asp:Label>
                            <asp:Label ID="itemNameLabel" runat="server" style="margin-right:auto"><%# Eval("product_name") %></asp:Label>
                            <asp:Button ID="viewDetailButton" runat="server" CssClass="btn custom-btn-primary" Text="View Details" CommandName="viewDetails" CommandArgument='<%# Eval("order_id").ToString() %>'/>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </asp:Panel>
        </div>
    </div>

    <div class="row">
        <div class="col-6">
            <h3>Revisit Your Recents</h3>
        </div>
        <div class="col-6 d-flex flex-column justify-content-end align-items-end">
            <a href="CustomerMenu.aspx" style="text-decoration:none">
                <div class="custom-text-icon-button">
                    <span style="margin-right:5px">See more meals</span>
                    <i class="bi bi-arrow-right-circle-fill" style="font-size:24px;"></i>
                </div>
            </a>
        </div>
    </div>
    <asp:Panel ID="noRecentsPanel" runat="server" CssClass="col d-flex flex-column justify-content-center align-items-center" style="margin-bottom:30px;">
        <span style="margin-bottom:10px">You have not made an order!</span>
        <a href="CustomerMenu.aspx" class="btn custom-btn-outline-primary">Make one here</a>
    </asp:Panel>
    <asp:Panel ID="recentsPanel" runat="server" Visible="false" CssClass="row d-flex justify-content-center align-items-center" style="margin-bottom:30px;">
        <asp:ListView ID="recentsListView" runat="server" OnItemCommand="recentsListView_itemCommand">
            <ItemTemplate>
                <div class="col-lg-3 col-md-4 col-sm-6 d-flex justify-content-center align-items-center mb-">
                    <div class="card" style="height:480px; width:250px">
                        <img class="card-img-top" src="<%# Eval("img_url") %>"/>
                        <div class="card-body" style="display:flex;flex-direction:column">
                            <h5><%# Eval("product_name") %></h5>
                            <p class="card-text" style="margin-bottom:20px;font-style:italic"><%# Eval("category_name") %></p>
                            <p class="card-text" style="overflow:hidden;text-overflow:ellipsis;display:-webkit-box;-webkit-line-clamp: 2;-webkit-box-orient:vertical;margin-bottom:auto;"><%# Eval("short_description") %></p>
                            <p class="card-text" style="margin-bottom:20px"><%# Eval("price").ToString() %></p>
                            <asp:Button ID="viewDetailButton" runat="server" CssClass="btn custom-btn-primary" Text="View Details" CommandName="viewDetails" CommandArgument='<%# Eval("product_id").ToString() %>'/>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </asp:Panel>
</asp:Content>
