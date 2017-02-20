<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminActionAudit.ascx.cs" Inherits="DogeNews.Web.UserControls.AdminActionAudit" %>

<asp:UpdatePanel runat="server">
    <ContentTemplate>    
        <ul class="collapsible" data-collapsible="accordion">
            <asp:Repeater runat="server" ItemType="DogeNews.Web.Models.AdminActionLogWebModel" ID="LogsRepeater" DataSource="<%# this.Model.Logs %>">
                <ItemTemplate>
                    <li>
                        <div class="collapsible-header">
                            <i class="material-icons">whatshot</i>
                            <%#Item.Date.ToString("d") %>, <%# Item.InvokedMethodName %> - <%# Item.User.Username %>
                        </div>
                        <div class="collapsible-body"><span><%# HttpUtility.HtmlEncode(Item.InvokedMethodArguments)%></span></div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </ContentTemplate>
</asp:UpdatePanel>