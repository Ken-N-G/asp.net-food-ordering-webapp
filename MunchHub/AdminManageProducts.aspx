<%@ Page Title="Manage Products" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminManageProducts.aspx.cs" Inherits="MunchHub.WebForm14" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightMarginWidgets" runat="server">
    <ul class="navbar-nav mr-auto">
        <li class="nav-item d-flex align-items-center"><asp:Button ID="logoutButton" CssClass="btn btn-outline-danger" Text="Logout"  runat="server" OnClick="logoutButton_Click" style="margin-left:30px" /></li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PreMainContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-12">
        <h1 style="margin-bottom:50px">Products</h1>
    </div>
    <div class="row d-flex">
        <div style="display:flex;align-items:center;margin-right:auto;margin-bottom:10px">
            <asp:TextBox ID="searchInput" runat="server" CssClass="form-control" placeholder="Search product ID" style="margin-right:10px"></asp:TextBox>
            <asp:Button ID="searchButton" runat="server" CssClass="btn custom-btn-primary me-1 me-sm-auto" Text="Search" OnClick="searchButton_Click"/>
            <asp:Button ID="addButton" runat="server" CssClass="btn custom-btn-primary" Text="Add Product" OnClick="addButton_Click" />
        </div>
    </div>
    <asp:Label ID="productErrorLabel" runat="server" style="color:#ff0000;margin-bottom:20px;"></asp:Label>
    <div class="col-12" style="margin-top:30px;">
        <div class="table-responsive">
            <table class="table">
                <thead>
                    <tr>
                        <th>
                            ID
                        </th>
                        <th>
                            Product Name
                        </th>
                        <th>
                            Short Description
                        </th>
                        <th>
                            Long Description
                        </th>
                        <th>
                            Price
                        </th>
                        <th>
                            Action
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:ListView ID="productView" runat="server" OnItemCommand="productView_itemCommand">
                        <ItemTemplate>
                             <tr>
                                 <td>
                                     <%# Eval("product_id") %>
                                 </td>
                                 <td>
                                     <%# Eval("product_name") %>
                                 </td>
                                 <td>
                                     <%# Eval("short_description") %>
                                 </td>
                                 <td>
                                     <%# Eval("description") %>
                                 </td>
                                 <td>
                                    <%# Eval("price").ToString() %>
                                </td>
                                 <td>
                                     <asp:Button ID="viewDetails" runat="server" CssClass="btn custom-btn-primary" Text="View Details" CommandName="viewDetails" CommandArgument='<%# Eval("product_id").ToString() %>'/>
                                     <asp:Button ID="suspendButton" runat="server" CssClass="btn btn-danger" Text="Delete" CommandName="delete" CommandArgument='<%# Eval("product_id").ToString() %>'/>
                                 </td>
                             </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
