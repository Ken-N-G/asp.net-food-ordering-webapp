<%@ Page Title="Menu" Language="C#" MasterPageFile="~/CustomerMasterPage.Master" AutoEventWireup="true" CodeBehind="CustomerMenu.aspx.cs" Inherits="MunchHub.WebForm4" %>
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
         <h1 style="margin-bottom:50px">Menu</h1>
    </div>
    <hr />
    <div class="row">
        <div style="display:flex;align-items:center">
            <asp:TextBox ID="searchInput" runat="server" CssClass="form-control" placeholder="What do you want to eat?" style="margin-right:10px"></asp:TextBox>
            <asp:Button ID="searchButton" runat="server" CssClass="btn custom-btn-primary" Text="Search" style="margin-right:auto;" OnClick="searchButton_Click" />
            <div class="dropdown">
                <asp:DropDownList id="categoryDropdownList" AutoPostBack="True" OnSelectedIndexChanged="categoryDropdownList_selectionChange" runat="server" CssClass="btn custom-btn-primary dropdown-toggle">
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="row" style="margin-top:30px;">
        <div class="col-12" style="padding-left:20px; padding-right:20px">
            <div class="row d-flex justify-content-center align-items-center">
                <asp:ListView ID="menuListView" runat="server" OnItemCommand="menuListView_itemCommand">
                    <ItemTemplate>
                        <div class="col-xl-3 col-lg-4 col-md-6 col-sm-12 d-flex flex-column justify-content-center align-items-center" style="margin:10px">
                            <div class="card" style="height:480px; width:250px">
                                <img class="card-img-top" src="<%# Eval("img_url") %>"/>
                                <div class="card-body" style="display:flex;flex-direction:column">
                                    <h5><%# Eval("product_name") %></h5>
                                    <p class="card-text" style="margin-top:20px;font-style:italic"><%# Eval("category_name") %></p>
                                    <p class="card-text" style="overflow:hidden;text-overflow:ellipsis;display:-webkit-box;-webkit-line-clamp: 2;-webkit-box-orient:vertical;margin-bottom:auto;"><%# Eval("short_description") %></p>
                                    <p class="card-text" style="margin-bottom:20px">RM <%# Eval("price").ToString() %></p>
                                    <asp:Button ID="viewDetailButton" runat="server" CssClass="btn custom-btn-primary" Text="View Details" CommandName="viewDetails" CommandArgument='<%# Eval("product_id").ToString() %>'/>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </div>
    <script></script>
</asp:Content>
