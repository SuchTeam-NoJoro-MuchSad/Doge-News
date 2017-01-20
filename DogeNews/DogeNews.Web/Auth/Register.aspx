<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DogeNews.Web.Auth.Register" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div id="registrationFormContainer">
        <div class="input-field col s3">
            <input id="Username" value="" type="text" class="validate" runat="server" />
            <label for="UsernameInput" class="active">Username</label>
        </div>

        <div class="input-field col s3">
            <input id="FirstNameInput" value="" type="text" class="validate" runat="server" />
            <label for="FirstNameInput" class="active">First Name</label>
        </div>

        <div class="input-field col s3">
            <input id="LastNameInput" value="" type="text" class="validate" runat="server" />
            <label for="LastNameInput" class="active">Last Name</label>
        </div>

        <div class="input-field col s3">
            <input id="EmailInput" value="" type="email" class="validate" runat="server" />
            <label for="EmailInput" class="active">Email</label>
        </div>

        <div class="input-field col s3">
            <input id="PassWordInput" value="" type="password" class="validate" runat="server" />
            <label for="PassWordInput" class="active">Password</label>
        </div>

        <div class="input-field col s3">
            <input id="ConfirmPasswordInput" value="" type="password" class="validate" runat="server" />
            <label for="ConfirmPasswordInput" class="active">Confirm Password</label>
        </div>

        <asp:Button 
            runat="server" 
            Text="Register" 
            ID="RegisterSubmitButton" 
            CssClass="waves-effect waves-light btn" 
            OnClick="RegisterSubmitButton_Click"/>
    </div>
</asp:Content>
