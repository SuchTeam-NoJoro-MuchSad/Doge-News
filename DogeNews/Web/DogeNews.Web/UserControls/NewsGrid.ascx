<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsGrid.ascx.cs" Inherits="DogeNews.Web.UserControls.NewsGrid" %>

<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <link rel="stylesheet" type="text/css" href="../Content/UserControls/news-grid.css" />

        <div class="row">
            <div>
                <div id="news-grid">
                    <!-- GENERATING GRID ELEMENTS -->
                    <asp:Repeater runat="server" ItemType="DogeNews.Web.Models.NewsWebModel" ID="NewsRepeater" DataSource="<%# this.Model.CurrentPageNews %>">
                        <ItemTemplate>
                            <div class="card small col s3">
                                <div class="card-image">
                                    <img src="<%#: ("/Resources/Images/" + Item.Author.Username + "/" + Item.Image.Name) %>">
                                    <span class="card-title"><%#: Item.Title %></span>
                                </div>

                                <div class="card-content">
                                    <p>
                                        I am a very simple card. I am good at containing small bits of information.
              I am convenient because I require little markup to use effectively.
                                    </p>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <!-- PAGINATION -->
                <ul class="pagination">
                    <li class="waves-effect">
                        <asp:Button runat="server" OnClick="ChangePageClick" Text="1"></asp:Button>
                    </li>
                    <asp:Repeater
                        runat="server"
                        ItemType="Int32"
                        DataSource="<%# Enumerable.Range(2, this.Model.NewsCount / this.Model.PageSize) %>">
                        <ItemTemplate>
                            <li class="waves-effect">
                                <asp:Button runat="server" OnClick="ChangePageClick" Text="<%#: Item %>"></asp:Button>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
