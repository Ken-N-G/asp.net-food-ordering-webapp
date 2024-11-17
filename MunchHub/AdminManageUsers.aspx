<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminManageUsers.aspx.cs" Inherits="MunchHub.WebForm12" %>
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
         <h1 style="margin-bottom:50px">Users</h1>
    </div>
    <div class="row">
        <div style="display:flex;align-items:center">
            <asp:TextBox ID="searchInput" runat="server" CssClass="form-control" placeholder="Search user ID" style="margin-right:10px"></asp:TextBox>
            <asp:Button ID="searchButton" runat="server" CssClass="btn custom-btn-primary" Text="Search" style="margin-right:auto;" OnClick="searchButton_Click" />
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
                            Name
                        </th>
                        <th>
                            Email
                        </th>
                        <th>
                            Status
                        </th>
                        <th>
                            Actions
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:ListView ID="userView" runat="server" OnItemDataBound="userView_ItemDataBound" OnItemCommand="userView_itemCommand">
                        <ItemTemplate>
                             <tr>
                                 <td>
                                     <%# Eval("user_id") %>
                                 </td>
                                 <td>
                                     <%# Eval("name") %>
                                 </td>
                                 <td>
                                     <%# Eval("email") %>
                                 </td>
                                 <td>
                                     <%# Eval("status") %>
                                 </td>
                                 <td>
                                     <asp:Button ID="viewDetails" runat="server" CssClass="btn custom-btn-primary" Text="View Details" CommandName="viewDetails" CommandArgument='<%# Eval("user_id").ToString() %>'/>
                                     <asp:Button ID="suspendButton" runat="server" CssClass="btn btn-danger" Text="Suspend User" CommandName="suspendUser" CommandArgument='<%# Eval("user_id").ToString() %>'/>
                                 </td>
                             </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
