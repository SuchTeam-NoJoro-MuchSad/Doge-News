<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="404Page.aspx.cs" Inherits="DogeNews.Web.Errors._404Page" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../Content/Errors/error-page.css" />

    <div class="container">
        <div class="row">
            <div id="error-container">
                <h1 class="status-code">404</h1>
                <h4 class="error-text">The page does not exist!</h4>
            </div>

            <img src="../Images/404-monster.png" />
        </div>
    </div>
</asp:Content>
