<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UniversityWebsite.Account.Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="col-xs-10">
            <section id="loginForm">
                <div class="form-horizontal">
                    <h4><span style="text-decoration: underline; color: #990000; font: arial">Log in</span></h4>
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-xs-1 control-label" Font-Names="Arial">Email</asp:Label>
                        <div class="col-xs-11">
                            <asp:TextBox runat="server" ID="Email" CssClass="form-control input-sm" MaxLength="15" Font-Names="Arial" Width="150px" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" ErrorMessage="An email is required." Font-Names="Arial" Font-Size="Small" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-xs-offset-1 col-xs-11">
                            <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="btn-sm" Font-Names="Arial" />
                        </div>
                    </div>
                    <%-- The Password textbox and RememberMe checkbox are not visible in this application --%>
                    <asp:Panel runat="server" Visible="false">
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-xs-2 control-label">Password</asp:Label>
                            <div class="col-xs-10">
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control">University1#</asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <div class="checkbox">
                                    <asp:CheckBox runat="server" ID="RememberMe" />
                                    <asp:Label runat="server" AssociatedControlID="RememberMe">Remember me?</asp:Label>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <%-- 
                <p>
                    <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register as a new user</asp:HyperLink>
                </p>
                <p>
                    <%-- Enable this once you have account confirmation enabled for password reset functionality
                    <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink" ViewStateMode="Disabled">Forgot your password?</asp:HyperLink>
                </p>
                --%>
            </section>
        </div>

        <%--
        <div class="col-xs-4">
            <section id="socialLoginForm">
                <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
            </section>
        --%>
    </div>
</asp:Content>