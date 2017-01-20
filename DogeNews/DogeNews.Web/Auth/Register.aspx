<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DogeNews.Web.Auth.Register" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../Content/Auth/css/register.css" />

    <div id="registrationFormContainer">
        <!-- Username -->
        <asp:RequiredFieldValidator
            ID="UsernameRequiredValidator"
            runat="server"
            ControlToValidate="UsernameInput"
            ErrorMessage="Username is required.">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator
            ID="UsernameRegexValdiator"
            runat="server"
            ValidationExpression="([A-Za-z0-9_]){3,20}"
            ErrorMessage="Username must contain only letters,digits and underscore and be with length between 3 and 20 characters."
            ControlToValidate="UsernameInput">
        </asp:RegularExpressionValidator>
        <div class="input-field col s3" id="usernameContainer">
            <input id="UsernameInput" value="" type="text" class="validate" runat="server" />
            <label for="UsernameInput" class="active">Username</label>
        </div>

        <!-- FirstName -->
        <asp:RequiredFieldValidator
            ID="FirstNameRequiredValdiator"
            runat="server"
            ControlToValidate="FirstNameInput"
            ErrorMessage="First name is required.">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator
            ID="FirstNameRegexValdiator"
            runat="server"
            ValidationExpression="([A-Za-z]){3,20}"
            ErrorMessage="First name must contain only letters and be with length between 3 and 20 characters."
            ControlToValidate="FirstNameInput">
        </asp:RegularExpressionValidator>
        <div class="input-field col s3" id="firstnameContainer">
            <input id="FirstNameInput" value="" type="text" class="validate" runat="server" />
            <label for="FirstNameInput" class="active">First Name</label>
        </div>

        <!-- LastName -->
        <asp:RequiredFieldValidator
            ID="LastNameRequiredValidator"
            runat="server"
            ControlToValidate="LastNameInput"
            ErrorMessage="Last name is required.">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator
            ID="LastNameRegexValidator"
            runat="server"
            ValidationExpression="([A-Za-z]){3,20}"
            ErrorMessage="Last name must contain only letters and be with length between 3 and 20 characters."
            ControlToValidate="LastNameInput">
        </asp:RegularExpressionValidator>
        <div class="input-field col s3" id="lastnameContainer">
            <input id="LastNameInput" value="" type="text" class="validate" runat="server" />
            <label for="LastNameInput" class="active">Last Name</label>
        </div>

        <!-- Email -->
        <asp:RequiredFieldValidator
            ID="EmailRequiredValidator"
            runat="server"
            ControlToValidate="EmailInput"
            ErrorMessage="Email is required.">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator
            ID="EmailRegexValidator"
            runat="server"
            ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
            ErrorMessage="Invalid email."
            ControlToValidate="EmailInput">
        </asp:RegularExpressionValidator>
        <div class="input-field col s3" id="emailContainer">
            <input id="EmailInput" value="" type="email" class="validate" runat="server" />
            <label for="EmailInput" class="active">Email</label>
        </div>

        <!-- Password -->
        <asp:RequiredFieldValidator
            ID="PasswordRequiredValidator"
            runat="server"
            ControlToValidate="PasswordInput"
            ErrorMessage="Password is required.">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator
            ID="PasswordRegexValidator"
            runat="server"
            ValidationExpression="(.){6,20}"
            ErrorMessage="Password must be between 6 and 20 characters."
            ControlToValidate="PasswordInput">
        </asp:RegularExpressionValidator>
        <div class="input-field col s3" id="passwordContainer">
            <input id="PasswordInput" value="" type="password" class="validate" runat="server" />
            <label for="PasswordInput" class="active">Password</label>
        </div>

        <!-- ConfirmPassword -->
        <asp:RequiredFieldValidator
            ID="ConfirmPasswordRequiredValidator"
            runat="server"
            ControlToValidate="ConfirmPasswordInput"
            ErrorMessage="Confirm password is required.">
        </asp:RequiredFieldValidator>
        <asp:CompareValidator 
            ID="ConfirmPasswordCompareValidator" 
            runat="server"
            ErrorMessage="Passwords dont match."
            ControlToCompare="PasswordInput"
            ControlToValidate="ConfirmPasswordInput">   
        </asp:CompareValidator>
        <div class="input-field col s3" id="confirmPasswordContainer">
            <input id="ConfirmPasswordInput" value="" type="password" class="validate" runat="server" />
            <label for="ConfirmPasswordInput" class="active">Confirm Password</label>
        </div>

        <asp:Button
            runat="server"
            Text="Register"
            ID="RegisterSubmitButton"
            CssClass="waves-effect waves-light btn"
            OnClick="RegisterSubmitButton_Click" />
    </div>

    <script src="../Content/Auth/js/register.js"></script>
</asp:Content>
