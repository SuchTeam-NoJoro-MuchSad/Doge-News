<%@ Page MasterPageFile="~/Site.Master" ValidateRequest="false" Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="DogeNews.Web.News.Add" %>

<%@ Register TagPrefix="uc" Src="~/UserControls/AddNewsArticle.ascx" TagName="AddNewsArticle" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <uc:AddNewsArticle runat="server" ID="AddNewsArticleControl"></uc:AddNewsArticle>
</asp:Content>
