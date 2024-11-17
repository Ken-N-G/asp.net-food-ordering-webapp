<%@ Page Title="Cart" Language="C#" MasterPageFile="~/CustomerMasterPage.Master" AutoEventWireup="true" CodeBehind="CustomerCart.aspx.cs" Inherits="MunchHub.WebForm5" %>
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
        <h1 style="margin-bottom:50px">Shopping Cart</h1>
    </div>
    <hr />
    <div class="col-12 d-flex justify-content-center">
        <div class="col-12" style="padding-left:30px;padding-right:30px;padding-top:20px;">
            <asp:ListView ID="cartListView" runat="server" OnItemCommand="cartListView_itemCommand">
                <ItemTemplate>
                    <div class="row d-flex justify-content-center" style="margin-bottom:20px">
                        <div class="col-md-4 col-6 d-flex justify-content-center">
                            <img src="<%# Eval("img_url") %>" style="height:220px;width:220px;object-fit:cover;border-radius:10px"/>
                        </div>
                        <div class="col-md-8 col-6 align-content-center">
                            <div class="row">
                                <div class="col-md-4 d-flex justify-content-center align-items-center">
                                    <span style="overflow:hidden;text-overflow:ellipsis;"><%# Eval("product_name") %></span>
                                </div>
                                <div class="col-md-4 d-flex justify-content-center align-items-center" style="font-weight:600">RM <%# Eval("price").ToString() %></div>
                                <div class="col-md-4 d-flex justify-content-center">
                                    <div style="align-items:center;justify-content:center;display: flex;">
                                        <asp:LinkButton ID="removeItemButton" runat="server" style="color:#ffae30;margin-right:5px" CommandName="removeQuantity" CommandArgument='<%# Eval("item_id").ToString() %>'>
                                            <i class="bi bi-dash-circle" style="font-size:24px"></i>
                                        </asp:LinkButton>
                                        <%# Eval("quantity").ToString() %>
                                        <asp:LinkButton ID="addItemButton" runat="server" style="color:#ffae30;margin-left:5px" CommandName="addQuantity" CommandArgument='<%# Eval("item_id").ToString() %>'>
                                            <i class="bi bi-plus-circle" style="font-size:24px"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
    <hr />
    <div class="col-12">
        <div style="align-items:center;justify-content:end;display:flex;margin-right:20px">
            <asp:Label ID="totalPriceLabel" runat="server" style="font-size:24px;font-weight:600"></asp:Label>
            <asp:Button ID="paymentButton" runat="server" CssClass="btn custom-btn-primary" Text="Pay" style="margin-left:50px" OnClick="paymentButton_Click"/>
        </div>
    </div>
</asp:Content>
