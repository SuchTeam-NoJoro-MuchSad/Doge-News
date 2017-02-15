<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsSlider.ascx.cs" Inherits="DogeNews.Web.UserControls.NewsSlider" %>

<asp:Panel CssClass="slider" runat="server">
    <ul class="slides">
        <asp:Repeater runat="server" ItemType="DogeNews.Web.Models.NewsWebModel" DataSource="<%# this.News %>">
            <ItemTemplate>
                <li>
                    <img src="<%#: ("/Resources/Images/" + Item.Author.Username + "/" + Item.Image.Name)  %>" />
                    <div class="caption right-align">
                        <h3>
                            <asp:HyperLink NavigateUrl='<%# $"~/News/Article.aspx?Title={Item.Title}" %>' runat="server"><%#: Item.Title %></asp:HyperLink>
                        </h3>
                    </div>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</asp:Panel>

<script src="../Scripts/Default.js"></script>