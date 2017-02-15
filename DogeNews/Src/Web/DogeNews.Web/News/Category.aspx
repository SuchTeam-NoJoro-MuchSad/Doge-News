<%@ Page MasterPageFile="~/Site.Master" ValidateRequest="true" Language="C#" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="DogeNews.Web.News.Category" %>

<%@ Register TagPrefix="ng" TagName="NewsGrid" Src="~/UserControls/NewsGrid.ascx" %>


<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <ng:NewsGrid runat="server" ID="NewsGrid" NewsDataSource="<%# this.Model.News %>"></ng:NewsGrid>
</asp:Content>
