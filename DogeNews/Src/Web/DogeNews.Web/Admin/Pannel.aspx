<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Pannel.aspx.cs" Inherits="DogeNews.Web.Admin.Pannel" %>

<%@ Register TagPrefix="uc" Src="~/UserControls/AddNewsArticle.ascx" TagName="AddNewsArticle" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="container">
        <div class="row">
            <div class="col s12">
                <ul class="tabs">
                    <li class="tab col s4"><a class="active" href="#add-article">Add new article</a></li>
                    <li class="tab col s4"><a href="#add-role">Add role to a user</a></li>
                    <li class="tab col s4"><a href="#aduit">Admin actions audit</a></li>
                </ul>
            </div>
            <div id="add-article" class="col s12">
                <uc:AddNewsArticle runat="server" ID="AddNewsArticleControl"></uc:AddNewsArticle>
            </div>
            <div id="add-role" class="col s12">Test 2</div>
            <div id="aduit" class="col s12">Test 3</div>
        </div>
    </div>
</asp:Content>
