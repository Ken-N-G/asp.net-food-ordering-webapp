<%@ Page Title="Manage Categories" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminManageCategories.aspx.cs" Inherits="MunchHub.WebForm13" %>
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
        <h1 style="margin-bottom:50px">Categories</h1>
    </div>
    <div class="row d-flex" style="margin-bottom:10px">
        <div style="display:flex;align-items:center;">
            <asp:TextBox ID="searchInput" runat="server" CssClass="form-control" placeholder="Search category ID" style="margin-right:10px"></asp:TextBox>
            <asp:Button ID="searchButton" runat="server" CssClass="btn custom-btn-primary me-1 me-sm-auto" Text="Search" OnClick="searchButton_Click" />
            <asp:TextBox ID="addInput" runat="server" CssClass="form-control" placeholder="Category name" style="margin-right:10px"></asp:TextBox>
            <asp:Button ID="addButton" runat="server" CssClass="btn custom-btn-primary" Text="Add Category" OnClick="addButton_Click" />
        </div>
    </div>
    <asp:Label ID="categoryErrorLabel" runat="server" style="color:#ff0000;margin-bottom:20px;"></asp:Label>
    <div class="col-12" style="margin-top:30px;">
        <div class="table-responsive">
            <table class="table">
                <thead>
                    <tr>
                        <th>
                            ID
                        </th>
                        <th>
                            Category Name
                        </th>
                        <th>
                            Actions
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:ListView ID="categoryView" runat="server" OnItemCommand="categoryView_itemCommand">
                        <ItemTemplate>
                             <tr>
                                 <td>
                                     <%# Eval("category_id") %>
                                 </td>
                                 <td>
                                     <%# Eval("category_name") %>
                                 </td>
                                 <td>
                                     <asp:Button ID="editButton" runat="server" CssClass="btn custom-btn-primary" Text="Edit Name" CommandName="edit" CommandArgument='<%# Eval("category_id").ToString() %>'/>
                                     <asp:Button ID="deleteButton" runat="server" CssClass="btn btn-outline-danger" Text="Delete" CommandName="deleteItem" CommandArgument='<%# Eval("category_id").ToString() %>'/>
                                 </td>
                             </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
