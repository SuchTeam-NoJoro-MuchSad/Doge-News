<%@ Page MasterPageFile="~/Site.Master" ValidateRequest="true" Language="C#" AutoEventWireup="true" CodeBehind="Article.aspx.cs" Inherits="DogeNews.Web.News.Article" %>

<%@ Register TagPrefix="uc" TagName="ArticleComments" Src="~/UserControls/ArticleComments.ascx" %>


<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <div class="parallax-container">
                <div class="parallax">
                    <img src="<%#: ("/Resources/Images/" + this.Model.NewsModel.Author.Username + "/" +this.Model.NewsModel.Image.Name) %>">
                </div>
            </div>
            <div class="section white">
                <div class="row container">
                    <% if (this.Context.User.IsInRole(DogeNews.Common.Constants.Roles.Admin))
                        { %>
                    <a class="modal-trigger orange-text" href="#<%#Model.NewsModel.Id %>-edit">Edit</a>

                    <span runat="server" visible="<%# Model.NewsModel.DeletedOn ==null %>">
                        <a class="modal-trigger red-text" href="#<%# Model.NewsModel.Id %>-delete">Delete</a>
                    </span>

                    <span runat="server" visible="<%# Model.NewsModel.DeletedOn !=null %>">
                        <a class="modal-trigger green-text" href="#<%# Model.NewsModel.Id %>-restore">Restore</a>
                    </span>
                    <%  } %>
                    <h2 class="header center-align"><%: this.Model.NewsModel.Title %></h2>
                    <div><%# this.Model.NewsModel.Content %></div>
                </div>
                <div>
                    <div id="<%#Model.NewsModel.Id %>-edit" class="modal">
                        <div class="modal-content">
                            <h4>Are you sure you want to edit article: "<%#Model.NewsModel.Title %></h4>
                        </div>
                        <div class="modal-footer">
                            <a href="#!" class=" modal-action modal-close waves-effect waves-green btn-flat">No</a>
                            <asp:Button ID="ArticleEditButton"
                                OnClick="ArticleEditButtonClick"
                                CssClass="modal-action modal-close waves-effect waves-light btn orange"
                                Text="Edit"
                                runat="server"
                                CommandArgument="<%# Model.NewsModel.Id %>" />
                        </div>
                    </div>
                    <div id="<%#Model.NewsModel.Id %>-delete" class="modal">
                        <div class="modal-content">
                            <h4>Are you sure you want to delete article: "<%#Model.NewsModel.Title %></h4>
                            <span>ID: <%#Model.NewsModel.Id %></span>
                        </div>
                        <div class="modal-footer">
                            <a href="#!" class=" modal-action modal-close waves-effect waves-green btn-flat">No</a>
                            <asp:Button ID="ArticleDeleteButton"
                                OnClick="ArticleDeleteButtonClick"
                                CssClass="modal-action modal-close waves-effect waves-light btn red"
                                Text="Delete"
                                runat="server"
                                CommandArgument="<%# Model.NewsModel.Id %>" />
                        </div>
                    </div>
                    <div id="<%#Model.NewsModel.Id %>-restore" class="modal">
                        <div class="modal-content">
                            <h4>Are you sure you want to restore article: "<%#Model.NewsModel.Title %></h4>
                            <span>ID: <%#Model.NewsModel.Id %></span>
                        </div>
                        <div class="modal-footer">
                            <a href="#!" class=" modal-action modal-close waves-effect waves-green btn-flat">No</a>
                            <asp:Button
                                ID="ArticleRestoreButton"
                                OnClick="ArticleRestoreButtonClick"
                                CssClass="modal-action modal-close waves-effect waves-light btn green"
                                Text="Restore"
                                runat="server"
                                CommandArgument="<%# Model.NewsModel.Id %>" />
                        </div>
                    </div>
                </div>
                <uc:ArticleComments runat="server"></uc:ArticleComments>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ArticleEditButton"/>
            <asp:PostBackTrigger ControlID="ArticleDeleteButton"/>
            <asp:PostBackTrigger ControlID="ArticleRestoreButton"/>
        </Triggers>
    </asp:UpdatePanel>
    <script>
        $(document).ready(function () {
            $('.parallax').parallax();
            $('.modal').modal();
        });
        Sys.WebForms.PageRequestManager.getInstance()
    .add_pageLoaded(function () {
        $('.modal').modal();
    });
    </script>
</asp:Content>
