<%@ Page Title="Manage Order" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminManageOrders.aspx.cs" Inherits="MunchHub.WebForm15" %>
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
        <h1 style="margin-bottom:50px">Orders</h1>
    </div>
    <div class="row d-flex">
        <div style="display:flex;align-items:center;margin-right:auto;">
            <asp:TextBox ID="searchInput" runat="server" CssClass="form-control" placeholder="Search order ID" style="margin-right:10px"></asp:TextBox>
            <asp:Button ID="searchButton" runat="server" CssClass="btn custom-btn-primary" Text="Search" OnClick="searchButton_Click"/>
        </div>
    </div>
    <div class="col-12" style="margin-top:30px;">
        <div class="table-responsive">
            <table class="table">
                <thead>
                    <tr>
                        <th>
                            ID
                        </th>
                        <th>
                            Date & Time Ordered
                        </th>
                        <th>
                            Status
                        </th>
                        <th>
                            Action
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:ListView ID="orderView" runat="server" OnItemCommand="orderView_itemCommand">
                        <ItemTemplate>
                             <tr>
                                 <td>
                                     <%# Eval("order_id") %>
                                 </td>
                                 <td>
                                     <%# Eval("datetime_ordered") %>
                                 </td>
                                 <td>
                                     <%# Eval("status") %>
                                 </td>
                                 <td>
                                     <asp:Button ID="viewDetailsButton" runat="server" CssClass="btn custom-btn-primary" Text="View Details" CommandName="viewDetails" CommandArgument='<%# Eval("order_id").ToString() %>'/>
                                 </td>
                             </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
