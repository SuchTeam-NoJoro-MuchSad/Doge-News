<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="DogeNews.Web.User.Settings.Settings" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Content/User/Settings/settings.css" />

    <div class="row">
        <div class="col s12">
            <ul class="tabs">
                <li class="tab col s3">
                    <a href="#test1">Change Password</a>
                </li>
                <li class="tab col s3">
                    <a href="#test2">Change Profile Pricture</a>
                </li>
            </ul>
        </div>

        <!-- CHANGE PASSWORD -->
        <div id="test1" class="col s12">
            <div id="change-password-container" class="col s6">
                <!-- New Password -->
                <asp:RequiredFieldValidator
                    runat="server"
                    ControlToValidate="NewPassword"
                    ErrorMessage="New password is required."
                    Display="Dynamic">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator
                    runat="server"
                    ControlToValidate="NewPassword"
                    ErrorMessage="New password must be between 6 and 20 characters."
                    ValidationExpression="(.){6,20}"
                    Display="Dynamic">
                </asp:RegularExpressionValidator>
                <div class="input-field">
                    <asp:TextBox runat="server" ID="NewPassword" TextMode="Password"></asp:TextBox>
                    <label for="New Password">New Password</label>
                </div>


                <!-- Repeat New Password -->
                <asp:CompareValidator
                    runat="server"
                    ControlToCompare="NewPassword"
                    ControlToValidate="RepeatNewPassword"
                    Display="Dynamic">
                </asp:CompareValidator>
                <div class="input-field">
                    <asp:TextBox runat="server" ID="RepeatNewPassword" TextMode="Password"></asp:TextBox>
                    <label for="RepeatNewPassword">Repeat New Password</label>
                </div>

                <button id="btn-change-password" class="btn waves-effect waves-light" type="submit" name="action">
                    <i class="material-icons right">send</i>Submit
                </button>
            </div>
        </div>

        <!-- CHANGE PROFILE PIC -->
        <div id="test2" class="col s12">Picture</div>
    </div>
</asp:Content>
