<%@ Page Title="Home" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="MunchHub.WebForm11" %>
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
    <div class="col-12">
        <h1 style="margin-bottom:50px">Welcome, user</h1>
    </div>
    <div class="row d-flex justify-content-center" style="margin-bottom:50px">
        <div class="col-12 d-flex flex-column rounded-3 p-3 bg-white custom-shadow mb-3 mb-xl-0 me-md-3" style="min-height:430px">
            <div class="custom-inline-text d-flex justify-content-center align-items-center">  
                <span style="font-size:22px;font-weight:600">Recent Orders</span>
                <a href="AdminManageOrders.aspx" class="custom-text-icon-button" style="margin-left:auto">
                    <span style="margin-right:5px">See all orders</span>
                    <i class="bi bi-arrow-right-circle-fill" style="font-size:24px;"></i>
                </a>
            </div>
            <hr />
            <div class="col-12">
                <div class="row" style="margin-bottom:20px">
                    <div class="col-3 d-flex flex-column justify-content-center align-items-center" style="text-align:center">Order ID</div>
                    <div class="col-2 d-flex flex-column justify-content-center align-items-center" style="text-align:center">Date Ordered</div>
                    <div class="col-2 d-flex flex-column justify-content-center align-items-center" style="text-align:center">No. Of Items</div>
                </div>
                <asp:ListView ID="recentOrderView" runat="server" OnItemCommand="recentOrderView_itemCommand">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-3 d-flex flex-column justify-content-center align-items-center" style="text-align:center"><%# Eval("order_id").ToString() %></div>
                            <div class="col-2 d-flex flex-column justify-content-center align-items-center" style="text-align:center"><%# Eval("datetime_ordered").ToString() %></div>
                            <div class="col-2 d-flex flex-column justify-content-center align-items-center" style="text-align:center"><%# Eval("quantity") %></div>
                            <div class="col-5 d-flex flex-column align-items-end">
                                <asp:Button ID="manageOrderButton" runat="server" CssClass="btn custom-btn-outline-primary" Text="Manage Order" CommandName="manageOrder" CommandArgument='<%# Eval("order_id").ToString() %>'/>
                            </div>
                        </div>
                        <hr />
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </div>
</asp:Content>
