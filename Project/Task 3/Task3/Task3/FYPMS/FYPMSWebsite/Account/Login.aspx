<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FYPMSWebsite.Account.Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h4><span style="text-decoration: underline; color: #191970" class="h4"><strong><%: Title %></strong></span></h4>
    <div class="row">
        <div class="col-xs-8">
            <section id="loginForm">
                <div class="form-horizontal">
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger" style="font-family: Arial; font-size: small">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <hr />
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Username" CssClass="col-xs-2 control-label" Font-Names="Arial" Font-Size="Small">Username</asp:Label>
                        <div class="col-xs-3">
                            <asp:TextBox runat="server" ID="Username" CssClass="form-control" TextMode="SingleLine" Font-Names="Arial" Font-Size="Small" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Username"
                                CssClass="text-danger" ErrorMessage="A username is required." Display="Dynamic" Font-Names="Arial" Font-Size="Small" />
                        </div>
                        <div class="col-xs-7">
                            <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="btn-sm" Font-Names="Arial" Font-Size="Small" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-xs-2 control-label" Visible="False">Password</asp:Label>
                        <div class="col-xs-10">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" Visible="False" Text="FYProject1#" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." Display="Dynamic" />
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
