<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DogeNews.Web._Default" %>

<%@ Register TagPrefix="ng" TagName="NewsGrid" Src="~/UserControls/NewsGrid.ascx" %>
<%@ Register TagPrefix="ns" TagName="NewsSlider" Src="~/UserControls/NewsSlider.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <ns:NewsSlider runat="server" ID="NewsSlider"></ns:NewsSlider>
    <ng:NewsGrid runat="server" ID="NewsGrid"></ng:NewsGrid>
</asp:Content>
