<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DogeNews.Web._Default" %>

<%@ Register TagPrefix="uc" TagName="NewsGrid" Src="~/UserControls/NewsGrid.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <uc:NewsGrid runat="server" ID="NewsGrid"></uc:NewsGrid>
</asp:Content>
