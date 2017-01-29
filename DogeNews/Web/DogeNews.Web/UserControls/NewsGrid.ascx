<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsGrid.ascx.cs" Inherits="DogeNews.Web.UserControls.NewsGrid" %>

<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <link rel="stylesheet" type="text/css" href="../Content/UserControls/news-grid.css" />

        <div class="row">
            <div id="news-grid">
                <!-- SORT DROPDOWN -->
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
                                    <span class="card-title"><%#: Item.Title %></span>
                                </div>

                                <div class="card-content">
                                    <asp:Literal runat="server" Text="<%# Item.Content %>"></asp:Literal>
                                </div>
                                <div class="card-action">
                                    <asp:Literal runat="server" Text="<%#: Item.CreatedOn %>"></asp:Literal>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <!-- PAGINATION -->
                <ul class="pagination">
                    <% if (this.Model.NewsCount != 0)
                        { %>
                    <asp:Repeater
                        runat="server"
                        ItemType="Int32"
                        DataSource="<%# Enumerable.Range(1, (this.Model.NewsCount / this.Model.PageSize) + 1) %>">
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
