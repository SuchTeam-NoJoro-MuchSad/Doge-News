<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DogeNews.Web.Auth.Login" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div id="loginFormContainer">
        <div class="input-field col s3">
            <input id="Username" value="" type="text" class="validate" runat="server" />
            <label for="Username" class="active">Username</label>
        </div>

        <div class="input-field col s3">
            <input id="PasswordInput" value="" type="password" class="validate" runat="server" />
            <label for="PasswordInput" class="active">Password</label>
        </div>
        
        <asp:Button 
            runat="server"
            Text="Login"
            ID="LoginSubmitButton"
            CssClass="waves-effect waves-light btn"
            OnClick="LoginSubmitButton_OnClickSubmitButton_Click" />
    </div>
</asp:Content>

