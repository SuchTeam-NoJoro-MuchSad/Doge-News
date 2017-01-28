<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="DefaultErrorPage.aspx.cs" Inherits="DogeNews.Web.Errors.DefaultErrorPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../Content/Errors/error-page.css" />

    <div class="container">
        <div class="row">
            <div id="error-container">
                <h1 class="status-code">Oops!</h1>
                <h4 class="error-text">Something went wrong!</h4>
            </div>

            <img src="../Images/404-monster.png" />
        </div>
    </div>
</asp:Content>
