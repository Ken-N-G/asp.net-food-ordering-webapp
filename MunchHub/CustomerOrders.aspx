<%@ Page Title="Orders" Language="C#" MasterPageFile="~/CustomerMasterPage.Master" AutoEventWireup="true" CodeBehind="CustomerOrders.aspx.cs" Inherits="MunchHub.WebForm8" %>
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
        <h1 style="margin-bottom:50px">Your Orders</h1>
    </div>
    <div class="col-12">
        <div style="display:flex;align-items:center">
            <asp:TextBox ID="searchInput" runat="server" CssClass="form-control" placeholder="Looking for a past order?" style="margin-right:10px"></asp:TextBox>
            <asp:Button ID="searchButton" runat="server" CssClass="btn custom-btn-primary" Text="Search" style="margin-right:auto;" OnClick="searchButton_Click" />
        </div>
        <asp:ListView ID="orderListView" runat="server" OnItemDataBound="orderListView_ItemDataBound" OnItemCommand="orderListView_itemCommand">
            <ItemTemplate>
                <div class="col-12" style="border-radius:10px;padding:10px 30px 10px 30px;box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);margin-top:50px">
                    <div class="row">
                        <div class="col-lg-6 col-md-8 col-sm-12">
                            <div class="custom-inline-text">
                                <h5 class="me-auto me-md-3">Order <%# Eval("order_id").ToString() %></h5>
                                <span><%# Eval("datetime_ordered").ToString() %></span>
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-4 col-sm-12 d-flex flex-column align-items-md-end mt- mt-md-0">
                            <h5><%# Eval("status").ToString() %></h5>
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
                    <div class="custom-inline-text d-flex justify-content-end align-items-center">
                        <asp:Panel ID="buttonPanel" runat="server" style="margin-right:auto;">
                            <asp:Button ID="reviewButton" runat="server" CssClass="btn custom-btn-primary" Text="Review" CommandName="reviewOrder" CommandArgument='<%# Eval("order_id").ToString() %>'/>
                        </asp:Panel>
                        <asp:Label ID="totalPriceLabel" runat="server" style="font-size:24px;font-weight:600"></asp:Label>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
