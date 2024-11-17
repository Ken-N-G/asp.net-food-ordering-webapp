<%@ Page Title="Feedback" Language="C#" MasterPageFile="~/CustomerMasterPage.Master" AutoEventWireup="true" CodeBehind="CustomerFeedback.aspx.cs" Inherits="MunchHub.WebForm9" %>
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
    <div class="col-sm-12 d-flex flex-column justify-content-center align-items-center min-vh-100">
    <div class="info-card col-md-9 col-sm-12 d-flex flex-column align-items-center justify-content-center" >
        <asp:Label ID="orderLabel" runat="server" style="font-size:36px;font-weight:600;margin-bottom:50px;"></asp:Label>
        <h5 style="margin-bottom:20px;">
            How was your order?
        </h5>
            <div class="col-10">
                <div id="ratingRow" runat="server" class="custom-inline-text d-flex justify-content-center" style="margin-bottom:20px">

                 </div>
                <div class="custom-inline-text d-flex justify-content-center">
                    <asp:LinkButton ID="removeRatingButton" runat="server" style="color:#ffae30;margin-right:10px" OnClick="removeRatingButton_Click">
                        <i class="bi bi-dash-circle" style="font-size:36px"></i>
                    </asp:LinkButton>
                    <asp:Label ID="ratingCountLabel" runat="server" class="d-flex align-items-center justify-content-center"></asp:Label>
                    <asp:LinkButton ID="addRatingButton" runat="server" style="color:#ffae30;margin-left:10px" OnClick="addRatingButton_Click">
                        <i class="bi bi-plus-circle" style="font-size:36px"></i>
                    </asp:LinkButton>
                </div>
                <div class="col-12">
                    <h5 style="margin-bottom:20px">Any comments?</h5>
                    <asp:TextBox ID="commentInput" runat="server" CssClass="form-control" TextMode="Multiline" placeholder="What did you like about your order?" style="min-width:100%;min-height:200px"></asp:TextBox>
                    <asp:Label ID="feedbackErrorLabel" runat="server" style="color:#ff0000"></asp:Label>
                </div>
            </div>
        <asp:Button ID="sendFeedbackButton" runat="server" CssClass="btn custom-btn-primary" Text="Send Feedback" style="margin-top:20px" OnClick="sendFeedbackButton_Click"/>
        </div>
    </div>
</asp:Content>
