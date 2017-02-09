<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsGrid.ascx.cs" Inherits="DogeNews.Web.UserControls.NewsGrid" %>
<%@ Import Namespace="System.Activities.Statements" %>
<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="DogeNews.Web.Models" %>

<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <link rel="stylesheet" type="text/css" href="../Content/UserControls/news-grid.css" />

        <div class="row">
            <div id="news-grid">
                <div>
                    <asp:Button CssClass="btn" runat="server" Text="Date ascending" CommandArgument="Ascending" OnClick="OrderByDateClick" />
                    <asp:Button CssClass="btn" runat="server" Text="Date descending" CommandArgument="Descending" OnClick="OrderByDateClick" />
                </div>

                <div id="news-container">
                    <!-- GENERATING GRID ELEMENTS -->
                    <asp:Repeater runat="server" ItemType="DogeNews.Web.Models.NewsWebModel" ID="NewsRepeater" DataSource="<%# this.Model.CurrentPageNews %>">
                        <ItemTemplate>
                            <div class="card small col l3 s10">
                                <div class="card-image">
                                    <img src="<%#: ("/Resources/Images/" + Item.Author.Username + "/" + Item.Image.Name) %>">
                                    <span class="card-title">
                                        <asp:HyperLink NavigateUrl='<%# $"~/News/Article.aspx?Title={Item.Title}" %>' runat="server"><%#: Item.Title %></asp:HyperLink>
                                    </span>
                                </div>

                                <div class="card-content">
                                    <asp:Literal runat="server" Text="<%# Item.Content %>"></asp:Literal>
                                </div>
                                <div class="card-action">
                                    <% if (this.Context.User.IsInRole(DogeNews.Common.Constants.Roles.Admin))
                                        { %>
                                    <a class="modal-trigger orange-text" href="#<%#Item.Id %>-edit">Edit</a>

                                    <span runat="server" visible="<%# Item.DeletedOn ==null %>">
                                        <a class="modal-trigger red-text" href="#<%# Item.Id %>-delete">Delete</a>
                                    </span>

                                    <span runat="server" visible="<%# Item.DeletedOn !=null %>">
                                        <a class="modal-trigger green-text" href="#<%# Item.Id %>-restore">Restore</a>
                                    </span>
                                    <%  } %>
                                    <asp:Literal runat="server" Text="<%#: Item.CreatedOn %>"></asp:Literal>
                                </div>
                            </div>
                            <% if (this.Context.User.IsInRole(DogeNews.Common.Constants.Roles.Admin))
                                { %>
                            <%-- Conformation Modals --%>
                            <div>
                                <div id="<%#Item.Id %>-edit" class="modal">
                                    <div class="modal-content">
                                        <h4>Are you sure you want to edit article: "<%#Item.Title %></h4>
                                    </div>
                                    <div class="modal-footer">
                                        <a href="#!" class=" modal-action modal-close waves-effect waves-green btn-flat">No</a>
                                        <asp:Button ID="ArticleEditButton"
                                            OnClick="ArticleEditButtonClick"
                                            CssClass="modal-action modal-close waves-effect waves-light btn orange"
                                            Text="Edit"
                                            runat="server"
                                            CommandArgument="<%# Item.Id %>" />
                                    </div>
                                </div>
                                <div id="<%#Item.Id %>-delete" class="modal">
                                    <div class="modal-content">
                                        <h4>Are you sure you want to delete article: "<%#Item.Title %></h4>
                                        <span>ID: <%#Item.Id %></span>
                                    </div>
                                    <div class="modal-footer">
                                        <a href="#!" class=" modal-action modal-close waves-effect waves-green btn-flat">No</a>
                                        <asp:Button ID="ArticleDeleteButton"
                                            OnClick="ArticleDeleteButtonClick"
                                            CssClass="modal-action modal-close waves-effect waves-light btn red"
                                            Text="Delete"
                                            runat="server"
                                            CommandArgument="<%# Item.Id %>" />
                                    </div>
                                </div>
                                <div id="<%#Item.Id %>-restore" class="modal">
                                    <div class="modal-content">
                                        <h4>Are you sure you want to restore article: "<%#Item.Title %></h4>
                                        <span>ID: <%#Item.Id %></span>
                                    </div>
                                    <div class="modal-footer">
                                        <a href="#!" class=" modal-action modal-close waves-effect waves-green btn-flat">No</a>
                                        <asp:Button
                                            ID="ArticleRestoreButton"
                                            OnClick="ArticleRestoreButtonClick"
                                            CssClass="modal-action modal-close waves-effect waves-light btn green"
                                            Text="Restore"
                                            runat="server"
                                            CommandArgument="<%# Item.Id %>" />
                                    </div>
                                </div>
                            </div>
                            <%-- Conformation Modals --%>
                            <%  } %>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <!-- PAGINATION -->
                <ul class="pagination">
                    <% if (this.Model.NewsCount != 0)
                        { %>
                    <asp:Repeater
                        runat="server"
                        ItemType="System.Int32"
                        DataSource="<%# Enumerable.Range(1, this.Model.NewsCount / this.Model.PageSize + 1) %>">
                        <ItemTemplate>
                            <li class="waves-effect">
                                <asp:Button runat="server" OnClick="ChangePageClick" Text="<%#: Item %>"></asp:Button>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <% } %>
                </ul>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script>
    Sys.WebForms.PageRequestManager.getInstance()
        .add_pageLoaded(function () {
            $('.modal').modal();
        });
</script>
