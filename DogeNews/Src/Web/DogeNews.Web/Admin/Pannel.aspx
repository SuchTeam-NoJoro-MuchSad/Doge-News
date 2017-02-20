<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Pannel.aspx.cs" Inherits="DogeNews.Web.Admin.Pannel" %>

<%@ Register TagPrefix="uc" Src="~/UserControls/AddNewsArticle.ascx" TagName="AddNewsArticle" %>
<%@ Register TagPrefix="uc" Src="~/UserControls/AdminActionAudit.ascx" TagName="AdminActionAudit" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="container">
        <div class="row">
            <div class="col s12">
                <ul class="tabs">
                    <li class="tab col s6"><a class="active" href="#add-article">Add new article</a></li>
                    <li class="tab col s6"><a href="#aduit">Admin actions audit</a></li>
                </ul>
            </div>
            <div id="add-article" class="col s12">
                <uc:AddNewsArticle runat="server" ID="AddNewsArticleControl"></uc:AddNewsArticle>
            </div>
            <div id="aduit" class="col s12">
                <uc:AdminActionAudit runat="server" ID="AdminActionAuditControl"/>
            </div>
        </div>
    </div>
</asp:Content>
