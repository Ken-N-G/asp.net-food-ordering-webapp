<%@ Page Title="Order Details" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminOrderDetails.aspx.cs" Inherits="MunchHub.WebForm17" %>
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
    <div class="col-12">
        <asp:Label ID="orderIDLabel" runat="server" style="font-size:32px;font-weight:600;margin-bottom:50px"></asp:Label>
    </div>
    <div class="col-12" style="border-radius:10px;padding:10px 30px 10px 30px;box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);margin-top:50px">
        <div class="row">
            <div class="col-lg-6 col-md-8 col-sm-12">
                <div class="custom-inline-text d-flex align-items-center">
                    <asp:Label ID="idLabel" runat="server" CssClass="me-auto me-md-3" style="font-size:24px;font-weight:600;"></asp:Label>
                    <asp:Label ID="dateLabel" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-lg-6 col-md-4 col-sm-12 d-flex flex-column align-items-md-end mt- mt-md-0">
                <asp:Label ID="statusLabel" runat="server" style="font-size:24px;font-weight:600;"></asp:Label>
            </div>
        </div>
        <hr />
        <asp:ListView ID="deliveryItemListView" runat="server">
            <ItemTemplate>
                <div class="row" style="margin-bottom:30px">
                    <div class="col-lg-3 col-md-6">
                        <img src="<%# Eval("img_url").ToString() %>" style="height:7rem;width:10rem;object-fit:cover;border-radius:10px"/>
                    </div>
                    <div class="col-lg-3 col-md-6 d-flex flex-column justify-content-center me-lg-auto">
                        <span style="font-size:20px"><%# Eval("product_name").ToString() %></span>
                        <span>Quantity: <%# Eval("quantity").ToString() %></span>
                    </div>
                    <div class="col-lg-6 col-md-12 d-flex flex-column justify-content-center align-items-lg-end">
                        <span>RM <%# Eval("price").ToString() %></span>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
        <hr />
        <div class="row">
            <div class="col-md-3 col-6 d-flex flex-column justify-content-end align-items-center">
                <asp:Button ID="pendingButton" runat="server" CssClass="btn custom-btn-primary" Text="Set Pending" CommandName="setToPending" CommandArgument='<%# Eval("order_id").ToString() %>' style="margin-right:20px" OnClick="pendingButton_Click"/>
            </div>
            <div class="col-md-3 col-6 d-flex flex-column justify-content-end align-items-center">
                <asp:Button ID="cancelButton" runat="server" CssClass="btn custom-btn-primary" Text="Set Cancelled" CommandName="setToCancelled" CommandArgument='<%# Eval("order_id").ToString() %>' style="margin-right:20px" OnClick="cancelButton_Click"/>
            </div>
            <div class="col-md-3 col-6 d-flex flex-column justify-content-end align-items-center mt-2">
                <asp:Button ID="deliverButton" runat="server" CssClass="btn custom-btn-primary" Text="Set Deliver" CommandName="setToDeliver" CommandArgument='<%# Eval("order_id").ToString() %>' style="margin-right:20px" OnClick="deliverButton_Click"/>
            </div>
            <div class="col-md-3 col-6 d-flex flex-column justify-content-end align-items-center mt-2">
                <asp:Button ID="deliveredButton" runat="server" CssClass="btn custom-btn-primary" Text="Set Delivered" CommandName="setToDelivered" CommandArgument='<%# Eval("order_id").ToString() %>' style="margin-right:20px" OnClick="deliveredButton_Click"/>
            </div>
        </div>
    </div>
</asp:Content>
