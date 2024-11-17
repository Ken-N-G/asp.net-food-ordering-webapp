<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminEditProduct.aspx.cs" Inherits="MunchHub.WebForm20" %>
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
    <div class="col-sm-12 d-flex flex-column justify-content-center align-items-center min-vh-100">
        <div class="info-card col-11">
            <div class="row">
                <div class="col-lg-6 col-sm-12 d-flex flex-column justify-content-center align-items-center">
                    <asp:Label ID="imageErrorLabel" runat="server" style="color:#ff0000;margin-top:10px;"></asp:Label>
                    <asp:FileUpload ID="fileUpload" runat="server" Accept=".jpg,.jpeg,.png" style="margin-top:30px;margin-bottom:10px"/>
                </div>
                <div class="col-lg-6 col-sm-12 d-flex flex-column">
                    <small style="margin-bottom:5px">Product Name</small>
                    <asp:TextBox ID="productNameInput" runat="server" style="font-size:18px;;margin-bottom:5px"></asp:TextBox>
                    <asp:Label ID="nameErrorLabel" runat="server" style="color:#ff0000;margin-bottom:10px;"></asp:Label>
                    <small style="margin-bottom:5px">Price</small>
                    <asp:TextBox ID="priceInput" runat="server" style="font-size:18px;margin-bottom:10px"></asp:TextBox>
                    <asp:Label ID="priceErrorLabel" runat="server" style="color:#ff0000;margin-bottom:10px;"></asp:Label>
                    <small style="margin-bottom:5px">Category</small>
                    <div class="dropdown" style="margin-bottom:40px">
                        <asp:DropDownList id="categoryDropdownList" AutoPostBack="True" runat="server" CssClass="btn custom-btn-primary dropdown-toggle">
                        </asp:DropDownList>
                    </div>
                    <asp:Label ID="categoryErrorLabel" runat="server" style="color:#ff0000;margin-bottom:10px;"></asp:Label>
                    <small style="margin-bottom:5px">Description</small>
                    <asp:TextBox ID="descriptionInput" runat="server" style="font-size:18px;margin-bottom:10px"></asp:TextBox>
                    <asp:Label ID="descriptionErrorLabel" runat="server" style="color:#ff0000;margin-bottom:10px;"></asp:Label>
                    <small style="margin-bottom:5px">Short Description</small>
                    <asp:TextBox ID="shortDescriptionInput" runat="server" style="font-size:18px;margin-bottom:40px"></asp:TextBox>
                    <asp:Label ID="shortDescriptionErrorLabel" runat="server" style="color:#ff0000;margin-bottom:10px;"></asp:Label>
                    <div class="custom-inline-text d-flex justify-content-center">
                        <asp:Button ID="saveChanges" runat="server" CssClass="btn custom-btn-primary" Text="Save Changes" OnClick="saveChanges_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
