<%@ Page MasterPageFile="~/Site.Master" ValidateRequest="true" Language="C#" AutoEventWireup="true" CodeBehind="Article.aspx.cs" Inherits="DogeNews.Web.News.Article" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
        <div class="parallax-container">
            <div class="parallax">
                <img src="<%#: ("/Resources/Images/" + this.Model.NewsModel.Author.Username + "/" +this.Model.NewsModel.Image.Name) %>">
            </div>
        </div>
        <div class="section white">
            <div class="row container">
                <h2 class="header center-align"><%: this.Model.NewsModel.Title %></h2>
                <div><%# this.Model.NewsModel.Content %></div>
            </div>
        </div>
        <script>
            $(document).ready(function () {
                $('.parallax').parallax();
            });
        </script>
</asp:Content>
