<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleComments.ascx.cs" Inherits="DogeNews.Web.UserControls.ArticleComments" %>

<asp:UpdatePanel ID="CommentsUpdatePanel" runat="server">
    <ContentTemplate>
        <div class="divider"></div>
        <div class="container">
            <span class="badge"><%# this.Model.Comments.Count() %></span>
            <ul class="collection">
                <asp:Repeater runat="server" ItemType="DogeNews.Web.Models.CommentWebModel" ID="CommentsRepeater"
                    DataSource="<% #this.Model.Comments %>">
                    <ItemTemplate>
                        <li class="collection-item avatar">
                            <img src="" alt="<%# Item.User.Username %>" class="circle">
                            <div>
                                <%# Item.Content %>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div>
                <% if (this.Context.User.Identity.IsAuthenticated)
                    { %>
                <asp:TextBox ID="AddCommentTextBox" runat="server"></asp:TextBox>
                <asp:Button runat="server" ID="CommnetSubmitButton" OnClick="ButtonSubmitComment" Text="Comment" />
                <%  } %>
            </div>

        </div>

    </ContentTemplate>
</asp:UpdatePanel>
